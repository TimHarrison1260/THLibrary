using System;
using System.Collections.Generic;

using System.IO;                    //  IO streams.

using Core.Model;
using Core.Factories;
using Core.Interfaces;

namespace Infrastructure.Data
{
    /// <summary>
    /// Class <c>Library</c> is responsible for holding all data
    /// relevent to the library; namely the Books.
    /// It also contains the references to the Keywords, Authors,
    /// Titles and ISBN references in the library.
    /// </summary>
    /// <remarks>
    /// This class holds a collection of the individual <see cref="Core.Model.LibraryBook"/> classes.
    /// The collection is exposes as a <c>Public</c> property and is held as a generic List of type
    /// <c>LibraryBook</c>.  The List(T) collection is used to facilitate working with Linq from 
    /// the repository.  The data is an unordered list, held in the sequence the items are added, and 
    /// this collection type offers the simplest way of providing that, and gives good access to the items
    /// individually.  However, the use of Linq will negate some of the index access requirements.
    /// The other generic collection types offer very specific type of access and data structures which 
    /// are not the most appropriate here.
    /// Makes use of the UnitOfWork pattern through implementing the <see cref="Core.Interfaces.IUnitOfWork"/>
    /// interface combined with Lifetime management of the class through the Unity IoC.
    /// </remarks>
    public class Library : IUnitOfWork
    {
        //  Instances of the data tables making up the model.
        /// <summary>
        /// Exposes the collection of library bookes in the library
        /// </summary>
        public List<LibraryBook> Books = new List<LibraryBook>();
        /// <summary>
        /// Exposes a collection of all Authors in the library, for selecting searchable Authors.
        /// </summary>
        public Authors Authors = new Authors();
        /// <summary>
        /// Exposes a collection of all Title in the library, for selecting searchable Titles.
        /// </summary>
        public Titles Titles = new Titles();
        /// <summary>
        /// Exposes a collection of all ISBN References in the n the in the library, for selecting searchable ISBN.
        /// </summary>
        public ISBNReferences Isbns = new ISBNReferences();
        /// <summary>
        /// Exposes a collection of all Keywords for the books in the library, for selecting searchable Keywords.
        /// </summary>
        public Keywords Keywords = new Keywords();

        /// <summary>
        /// Holds instance of the Library Book Factory
        /// </summary>
        private readonly LibraryBookFactory _libraryBookFactory;

        /// <summary>
        /// Ctor: to accept the injected dependencies.
        /// </summary>
        /// <param name="libraryBookFactory">Concrete instance of the LibraryBookFactory class</param>
        public Library(LibraryBookFactory libraryBookFactory)
        {
            if (libraryBookFactory == null)
                throw new ArgumentNullException("LibraryBookFactory", "No library book factory supplied to Db");
            _libraryBookFactory = libraryBookFactory;

            //  Now load the library data.
            this.loadData();
        }


        /// <summary>
        /// Low level function to load the Database from the 
        /// supplied CSV file, representing the database.
        /// </summary>
        /// <returns>An instance of a genereic List(LibraryBook) of library books.</returns>
        public void Load()
        {
            if (Books == null)
                loadData();
        }

        /// <summary>
        /// Method <c>loadData</c> performs the reading of
        /// the csv file containing the library extract data.
        /// It populates all data collection contained within
        /// this Library.
        /// It is called from the Library constructor, but
        /// can also be called from a Public method Load()
        /// </summary>
        private async void loadData()
        {
            //  Locate the csv file.
            var dataFile = await Helpers.DataFileHelper.GetDataFile();

            if (dataFile != null)
            {
                //  Set up the streamreader to access the file, Read only mode
                using (StreamReader reader = new StreamReader(await dataFile.OpenStreamForReadAsync()))
                {
                    while (!reader.EndOfStream)
                    {
                        string data = reader.ReadLine();
                        //  split the data into a Book record and add to the Books collection.
                        var book = Helpers.DeserialiseData.ToBook(data, _libraryBookFactory);
                        Books.Add(book);
                        //  Add to other model classes
                        Authors.Add(book.Author);
                        Titles.Add(book.Title);
                        Isbns.Add(book.ISBN);
                        //  Split the keywords and add them if they don't exist.
                        foreach (string kw in book.KeyWords)
                        {
                            Keywords.Add(kw);
                        }
                        Keywords.Sort();
                    }
                }
            }
        }
    }
}
