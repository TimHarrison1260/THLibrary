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
    /// A Generic class <c>MapSearchViewModelToSearchCriteria</c> is responsible for mapping
    /// a SearchViewModel class to a SearchCriteria class.  It also works for the CurrentSearchViewModel
    /// as it is generic and the CurrentSearchViewModel derives from SearchViewmodel.
    /// </summary>
    /// <remarks>
    /// This class is not static so that we can inject a dependency in the constructor
    /// </remarks>
    public class MapSearchViewModelToSearchCriteria<T> where T : SearchViewModel
    {
        /// <summary>
        /// Holds the instance of the SearchCriteriaFactory.
        /// </summary>
        private readonly SearchCriteriaFactory _factory;

        /// <summary>
        /// Constructore that takes in the dependency for the SearchCriteriaFactory at runtime.
        /// </summary>
        /// <param name="searchCriteriaFactory">Instance of the SearchCriteriaFactory</param>
        public MapSearchViewModelToSearchCriteria(SearchCriteriaFactory searchCriteriaFactory)
        {
            if (searchCriteriaFactory == null)
                throw new ArgumentNullException("SearchCriteriaFactory", "No valid SearchCriteriaFactory supplied");
            _factory = searchCriteriaFactory;
        }

        /// <summary>
        /// The <c>Map</c> method maps a <see cref="Core.Model.SearchCriteria"/> class or class derived from it, to a 
        /// <see cref="THLibrary.DataModel.CurrentSearchViewModel"/> class.
        /// </summary>
        /// <param name="currentSearch">The LibraryBook instance.</param>
        /// <returns>The SearchCriteria instance.</returns>
        public SearchCriteria Map(T currentSearch)
        {
            SearchCriteria newCriteria = _factory.Create();
            newCriteria.Type = (SearchTypeEnum) Enum.Parse(typeof(SearchTypeEnum), currentSearch.Type);
            newCriteria.SearchString = currentSearch.SearchString;
            newCriteria.SearchDate = DateTime.Parse(currentSearch.SearchDate);

            return newCriteria;
        }
    }
}
