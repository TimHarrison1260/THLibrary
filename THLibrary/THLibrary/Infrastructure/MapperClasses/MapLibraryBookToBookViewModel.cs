using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Model;
using THLibrary.DataModel;

namespace THLibrary.Infrastructure.MapperClasses
{
    /// <summary>
    /// Static class <c>MapLibraryBookToBookViewModel</c> is responsible for mapping
    /// a LibraryBook class to a BookViewModel class.
    /// </summary>
    public static class MapLibraryBookToBookViewModel
    {
        /// <summary>
        /// Static <c>Map</c> method maps a <see cref="Core.Model.LibraryBook"/> class to a 
        /// <see cref="THLibrary.DataModel.BookViewModel"/> class.
        /// </summary>
        /// <param name="book">The LibraryBook instance.</param>
        /// <returns>The BookViewModel instance.</returns>
        public static BookViewModel Map(LibraryBook book)
        {
            //  Instantiate directly as this is only even used within this UI.
            //  TODO: (If Time) refactor this to an abstract factory to create the BookViewModel
            var bookVM = new BookViewModel()
            {
                Author = book.Author,
                Title = book.Title,
                ISBN = book.ISBN,
                Keywords = new ObservableCollection<string>(book.KeyWords),
                Synopsis = book.Synopsis
            };
            bookVM.SetImage("Assets/Logo.png");     //  TODO: update to correct image file, default for now.

            return bookVM;
        }
    }
}
