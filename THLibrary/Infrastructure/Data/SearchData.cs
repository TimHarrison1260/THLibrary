//***************************************************************************************************
//Name of File:     SearchData.cs
//Description:      Hold the Search Criteria data in memory
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using Windows.Storage;
using System.Xml.Serialization;

using Core.Interfaces;
using Core.Model;
using Core.Factories;


namespace Infrastructure.Data
{
    /// <summary>
    /// Class <c>SearchData</c> is responsible for holding all 
    /// data relevent to the searches;
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class holds a collection of the individual <see cref="Core.Model.SearchCriteria"/> classes.
    /// The collection is exposes as a <c>Public</c> property and is held as a generic List of type
    /// <c>SearchCriteria</c>.  The List(T) collection is used to facilitate working with Linq from 
    /// the repository.  The data is an unordered list, held in the sequence the items are added, and 
    /// this collection type offers the simplest way of providing that, and gives good access to the items
    /// individually.  However, the use of Linq will negate some of the index access requirements.
    /// The other generic collection types offer very specific type of access and data structures which 
    /// are not the most appropriate here.
    /// </para>
    /// <para>
    /// Makes use of the UnitOfWork pattern through implementing the <see cref="Core.Interfaces.ISearchUnitOfWork"/>
    /// interface combined with Lifetime management of the class through the Unity IoC.  This ensures the same instance
    /// of the SearchData is always accessed.
    /// </para>
    /// </remarks>
    public class SearchData : ISearchUnitOfWork
    {
        /// <summary>
        /// Exposes a generic List collection of all searchCriteria classes.
        /// </summary>
        public List<SearchCriteria> Searches = new List<SearchCriteria>();

        /// <summary>
        /// Holds the instance of the file handle to the underlying searches data file (Xml).
        /// </summary>
        private StorageFile SearchFile = null;

        /// <summary>
        /// Private instance of the SeacrhFactory, for creation of the SearchCriteria class.
        /// </summary>
        private readonly SearchCriteriaFactory _searchFactory;


        /// <summary>
        /// Ctor: to inject dependencies.
        /// </summary>
        /// <param name="SearchFactory">Search Factory dependency</param>
        /// <remarks>
        /// The SearchFactory is required to create the searchCriteria class.
        /// </remarks>
        public SearchData(SearchCriteriaFactory SearchFactory)
        {
            if (SearchFactory == null)
                throw new ArgumentNullException("SearchFactory", "No Search Factory supplied to SearchesData");
            _searchFactory = SearchFactory;
            //  Now load the Searches
            loadData();
        }

        /// <summary>
        /// Load method is responsible for retrieving the saved searches
        /// from the local storage file.
        /// </summary>
        public void Load()
        {
            if (Searches.Count() == 0)
                loadData();
        }

        /// <summary>
        /// Save method is responsible for persisting the searches in
        /// a local storage file.
        /// </summary>
        public void Save()
        {
            //  Save only if there are any searches to save.
            if (Searches != null && Searches.Count() != 0)
                saveData();
        }



        /// <summary>
        /// Private loadData method is responsible for reading the
        /// searches from the local storage file.  If the file does not
        /// exist, then an empty version of the file is created to that
        /// the handle can be stored and the Save method can assume that
        /// the file exists, and that it already has the appropriate 
        /// handle pointing to it.  
        /// </summary>
        /// <remarks>
        /// The handle to the searches data file is held within this class so
        /// that the same file can be used when the application closes to persist
        /// searches data.
        /// This avoids problems with reading directories and getting hold of 
        /// the file again, as the Async nature of the method calls can lead 
        /// to problems and cause the application to halt, without raising an exception.
        /// </remarks>
        private async void loadData()
        {
            //  Get the file if it exists.
            var searchFile = await Helpers.SearchesFileHelper.GetDataFile();

            //  Initialise the Searches collection
            Searches = new List<SearchCriteria>();

            //  If the file is null, it doesn't exist just initialise the Searches collection
            //  The file will be created if we have any searches to save.
            if (searchFile != null)
            {
                //  Store the handle to the search data file for use when the searches are store
                //  during the application close.
                SearchFile = searchFile;

                //  Deserialise the data from it.
                using (StreamReader reader = new StreamReader(await searchFile.OpenStreamForReadAsync()))
                {
                    //  Check for an empty stream, exceptions will occur if it is.
                    if (!reader.EndOfStream)
                    {
                        //  Set up the types for deserialising
                        Type[] extraTypes = new Type[1];
                        extraTypes[0] = typeof(SearchCriteria);
                        XmlSerializer serializer = new XmlSerializer(typeof(List<SearchCriteria>), extraTypes);
                        Searches = (List<SearchCriteria>)serializer.Deserialize(reader);
                    }
                }
            }
            else
            {
                //  Create an empty file and store the handel to the file.
                //  It will be needed when the application shutsdown so the 
                //  searches can be saved to file, from the App.xaml page.
                SearchFile = await Helpers.SearchesFileHelper.CreateDataFile();
            }
        }

        /// <summary>
        /// Private saveData method is responsible for writing the
        /// searches to the local storage file, serialised as XML.
        /// </summary>
        /// <remarks>
        /// This method uses the handel to the data file that is returned
        /// when the file is originally read in.
        /// </remarks>
        private async void saveData()
        {
            //  get the handle to the search data file.
            var searchFile = this.SearchFile;

            //  Now serialise the Searches as XML and write to the file.
            using (StreamWriter writer = new StreamWriter(await searchFile.OpenStreamForWriteAsync()))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(List<SearchCriteria>));
                serialiser.Serialize(writer, Searches);
            }
        }

    }
}
