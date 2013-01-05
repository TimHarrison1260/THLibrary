//***************************************************************************************************
//Name of File:     SearchRepository.cs
//Description:      Controls access to the Search Criteria data.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

using Core.Interfaces;
using Core.Model;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Concrete inplmenetation of the <see cref="Core.Interfaces.ISearchRepository"/> interface.
    /// It is responsible for access to the Search Criteria, as well as controlling the loading and 
    /// persistance of the search criteria to the local storage file.
    /// </summary>
    /// <remarks>
    /// The Searches and Searchtypes are returned from two methods, which return IEnumerable collections of the
    /// <see cref="Core.Model.SearchCriteria"/> and <c>String</c> respectively, which supports the use of
    /// Linq to Objects.
    /// It also makes use of the UnitOfWork pattern through implementing the <see cref="Core.Interfaces.ISearchUnitOfWork"/>
    /// interface combined with Lifetime management of the class through the Unity IoC.  This ensures that the 
    /// data is always retrieved from the same instance of the <see cref="Infrastructure.Data.SearchData"/> class.
    /// </remarks>
    public class SearchRepository : ISearchRepository
    {
        private readonly SearchData _unitOfWork;

        /// <summary>
        /// Ctor to inject reference to Dataclass.
        /// </summary>
        /// <param name="UnitOfWork">The concrete instance of the Library</param>
        public SearchRepository(ISearchUnitOfWork UnitOfWork)
        {
            if (UnitOfWork == null)
                throw new ArgumentNullException("UnitOfWork", "No UnitOWork suppliect to SearchRepository");
            _unitOfWork = UnitOfWork as SearchData;
        }

        /// <summary>
        /// Gets a collection of all defined searches
        /// </summary>
        /// <returns>A collection of searches</returns>
        public IEnumerable<SearchCriteria> GetSearches()
        {
            var searches = _unitOfWork.Searches;
            return searches;
        }

        /// <summary>
        /// Returns a collection of Search Types.  Representation of the <see cref="Core.Model.SearchTypeEnum"/>
        /// enumeration.
        /// </summary>
        /// <returns>Collection of strings, search type names.</returns>
        public IEnumerable<string> GetSearchTypes()
        {
            var types = Enum.GetNames(typeof(SearchTypeEnum));
            return types.AsEnumerable<string>();
        }

        /// <summary>
        /// Adds a new <see cref="Core.Model.SearchCriteria"/> to the collection of Searches.
        /// </summary>
        /// <param name="criteria">The Search Criteria to be added</param>
        /// <returns>The UniqueId of the search just added</returns>
        public string AddSearch(SearchCriteria criteria)
        {
            int lastId = GetLastId();
            criteria.UniqueId = ++lastId;
            _unitOfWork.Searches.Add(criteria);
            return criteria.UniqueId.ToString();
        }


        /// <summary>
        /// CloseData method is responsible for persisting
        /// the searches to local storage.  
        /// It should be called only from the OnSuspending
        /// event within the App.xaml.cs.
        /// </summary>
        public void CloseData()
        {
            _unitOfWork.Save();
        }


        /// <summary>
        /// Gets the last Id number used for a search.
        /// </summary>
        /// <returns>int containing the last (highest) uniqueId in the searches.</returns>
        private int GetLastId()
        {
            if (_unitOfWork.Searches.Count() == 0)
                return 0;
            else
            {
                return _unitOfWork.Searches.Max(x => x.UniqueId);
            }
        }

    }
}
