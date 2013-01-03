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
    /// Static class <c>MapSearchCriteriaToSearchViewModel</c> is responsible for mapping
    /// a SearchCriteria class to a SearchViewModel class.
    /// </summary>
    public static class MapSearchCriteriaToSearchViewModel
    {
        /// <summary>
        /// Static <c>Map</c> method maps a <see cref="Core.Model.SearchCriteria"/> class to a 
        /// <see cref="THLibrary.DataModel.SearchViewModel"/> class.
        /// </summary>
        /// <param name="criteria">The SearchCriteria instance.</param>
        /// <returns>The SearchViewModel instance.</returns>
        public static SearchViewModel Map(SearchCriteria criteria)
        {
            SearchViewModel searchVM = new SearchViewModel()
            {
                UniqueId = "",
                Type = Enum.GetName(typeof(SearchTypeEnum), criteria.Type),
                SearchString = criteria.SearchString,
                SearchDate = criteria.SearchDate.ToString()
            };
            return searchVM;
        }
    }
}
