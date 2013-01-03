using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Model;

namespace Infrastructure.Data
{
    public class Db
    {
        /// <summary>
        /// Low level function to load the Database from the 
        /// supplied CSV file, representing the database.
        /// </summary>
        /// <returns>An instance of a genereic List(LibraryBook) of library books.</returns>
        List<LibraryBook> Load()
        {
            List<LibraryBook> library = new List<LibraryBook>();


            //  Locate the csv file



            //  Set up the streamreader to access the file, Read only mode



            //  Create a concrete instance of the librarybook
            LibraryBook book = Core.Factories.LibraryBookFactory.Create();


            //  Add it to the list



            //  Now return the list to the Repository, as the library database is loaded.
            return library;
        }
    }
}
