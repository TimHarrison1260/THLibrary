//***************************************************************************************************
//Name of File:     ILibraryRepository.cs
//Description:      Interface for the Library Repository
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Collections.Generic;
using Core.Model;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface <c>ILibraryRepository</c> defines the contract for 
    /// the <c>Infrastructure.Repositories.LibraryRepository</c>
    /// class.  
    /// </summary>
    public interface ILibraryRepository
    {
        /// <summary>
        /// Gets all the books in the Library
        /// </summary>
        /// <returns>A collection of library books.</returns>
        IEnumerable<LibraryBook> GetAllBooks();

        /// <summary>
        /// Gets the list of searchable values. Keywords, authors, titles, iSBN's.
        /// </summary>
        /// <param name="ValueType">The type of value.</param>
        /// <returns>A collection of values.</returns>
        IEnumerable<string> GetSearchableBookValues(string ValueType);

        /// <summary>
        /// Gets a collection of all books that match the specified search criteria
        /// </summary>
        /// <param name="searchCriteria">the search criteria</param>
        /// <returns>A collection of library books</returns>
        IEnumerable<LibraryBook> SearchBooks(SearchCriteria searchCriteria);

    }
}

