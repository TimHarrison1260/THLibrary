//***************************************************************************************************
//Name of File:     MapLibraryBookToBookViewModel.cs
//Description:      Maps LibraryBook class to a BookViewModel class.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Collections.ObjectModel;

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
            if (book.ImagePath==string.Empty)
                bookVM.SetImage("Assets/Logo.png");     //  TODO: update to correct image file, default for now.
            else
                bookVM.SetImage(string.Format("Assets/{0}", book.ImagePath));

            return bookVM;
        }
    }
}
