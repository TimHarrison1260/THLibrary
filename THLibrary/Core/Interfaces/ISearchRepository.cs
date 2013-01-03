using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Model;
using Windows.Storage;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface <c>ISearchRepository</c> defines the interface for the 
    /// <see cref="Infrastructure.Repositories.SearchRepository"/> class.
    /// </summary>
    public interface ISearchRepository
    {
        IEnumerable<SearchCriteria> GetSearches();
        IEnumerable<string> GetSearchTypes();

        string AddSearch(SearchCriteria criteria);
//        void SaveSearch(SearchCriteria criteria);
//        void DeleteSearch(SearchCriteria criteria);

        void CloseData();
    }
}
