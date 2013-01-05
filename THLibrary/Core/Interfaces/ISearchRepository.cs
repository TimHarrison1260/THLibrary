//***************************************************************************************************
//Name of File:     ISearchRepository.cs
//Description:      Interface for the Search Repository
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
    /// Interface <c>ISearchRepository</c> defines the interface for the 
    /// <c>Infrastructure.Repositories.SearchRepository</c> class.
    /// </summary>
    public interface ISearchRepository
    {
        /// <summary>
        /// Gets a collection of all defined searches
        /// </summary>
        /// <returns>A collection of searches</returns>
        IEnumerable<SearchCriteria> GetSearches();

        /// <summary>
        /// Gets a string representation of the search types
        /// </summary>
        /// <returns>A collection of search types</returns>
        IEnumerable<string> GetSearchTypes();

        /// <summary>
        /// Adds a search to the searches
        /// </summary>
        /// <param name="criteria">Search being added</param>
        /// <returns>the assigned Id of the search added</returns>
        string AddSearch(SearchCriteria criteria);

        /// <summary>
        /// Persists the searches to the underlying local file.
        /// </summary>
        void CloseData();
    }
}
