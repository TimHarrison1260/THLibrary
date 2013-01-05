//***************************************************************************************************
//Name of File:     MapLibraryBookToBookViewModelCollection.cs
//Description:      Maps LibraryBook collection to a BookViewModel collection.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Collections.Generic;

using Core.Model;
using THLibrary.DataModel;

namespace THLibrary.Infrastructure.MapperClasses
{
    /// <summary>
    /// Static class <c>MapLIbraryBooksToBookViewModelCollection</c> is responsible for
    /// mapping an IEnumerable(T) collection of LibraryBooks to an IEnumerable(V)
    /// collection of BookViewModels
    /// </summary>
    /// <remarks>
    /// This calls the static class <see cref="THLibrary.Infrastructure.MapperClasses.MapLibraryBookToBookViewModel"/>
    /// class to perform the map for each item in the collection.
    /// </remarks>
    public static class MapLIbraryBooksToBookViewModelCollection
    {
        /// <summary>
        /// The static <c>Map</c> method maps the LibraryBook collection, which implements 
        /// IEnumerable(LibraryBook) to a collection of BookViewModel which implements 
        /// IEnumberable(BookViewModel).
        /// </summary>
        /// <param name="books">The collection of LibraryBooks</param>
        /// <returns>The collection of BookViewModels</returns>
        public static IEnumerable<BookViewModel> Map(IEnumerable<LibraryBook> books)
        {
            List<BookViewModel> BookVMCollection = new List<BookViewModel>();
            foreach (var book in books)
            {
                BookVMCollection.Add(MapLibraryBookToBookViewModel.Map(book));
            }
            return BookVMCollection;
        }
    }
}
