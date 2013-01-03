using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Model;
using THLibrary.DataModel;

namespace THLibrary.Infrastructure.MapperClasses
{
    /// <summary>
    /// Static class <c>MapLIbraryBooksToBookViewModelCollection</c> is responsible for
    /// mapping an IEnumerable(T) collection of LibraryBooks to an IEnumerable(V)
    /// collection of BookViewModels
    /// </summary>
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
