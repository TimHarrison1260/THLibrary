using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Interfaces;
using Core.Model;
using Infrastructure.Data;
using Windows.Storage;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Concrete inplmenetation of the <see cref="Core.Interfaces.ISearchRepository"/> interface.
    /// It is responsible for access to the Search Criteria, as well as controlling the loading and 
    /// persistance of the search criteria to the local storage file.
    /// </summary>
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
        /// Retrieve all searches defined
        /// </summary>
        /// <returns>collection of searches</returns>
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

        ///// <summary>
        ///// Saves the specified search criteria
        ///// </summary>
        ///// <param name="criteria"></param>
        //public void SaveSearch(SearchCriteria criteria)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Deletes the specified Search Criteria
        ///// </summary>
        ///// <param name="criteria"></param>
        //public void DeleteSearch(SearchCriteria criteria)
        //{
        //    throw new NotImplementedException();
        //}

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
