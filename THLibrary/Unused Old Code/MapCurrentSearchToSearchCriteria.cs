using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Factories;
using Core.Model;
using THLibrary.DataModel;

namespace THLibrary.Infrastructure.MapperClasses
{
    /// <summary>
    /// Class <c>MapCurrentSearchToSearchCriteria</c> is responsible for mapping
    /// a CurrentSearchViewModel class to a SearchCriteria class.
    /// </summary>
    /// <remarks>
    /// This class is not static so that we can inject a dependency in the constructor
    /// </remarks>
    public class MapCurrentSearchToSearchCriteria
    {
        private readonly SearchCriteriaFactory _factory;

        public MapCurrentSearchToSearchCriteria(SearchCriteriaFactory searchCriteriaFactory)
        {
            if (searchCriteriaFactory == null)
                throw new ArgumentNullException("SearchCriteriaFactory", "No valid SearchCriteriaFactory supplied");
            _factory = searchCriteriaFactory;
        }

        /// <summary>
        /// The <c>Map</c> method maps a <see cref="Core.Model.SearchCriteria"/> class to a 
        /// <see cref="THLibrary.DataModel.CurrentSearchViewModel"/> class.
        /// </summary>
        /// <param name="currentSearch">The LibraryBook instance.</param>
        /// <returns>The SearchCriteria instance.</returns>
        public SearchCriteria Map(CurrentSearchViewModel currentSearch)
        {
            SearchCriteria newCriteria = _factory.Create();
            newCriteria.Type = (SearchTypeEnum) Enum.Parse(typeof(SearchTypeEnum), currentSearch.Type);
            newCriteria.SearchString = currentSearch.SearchString;
            newCriteria.SearchDate = DateTime.Parse(currentSearch.SearchDate);

            return newCriteria;
        }
    }
}
