//***************************************************************************************************
//Name of File:     MapSearchViewModelToSearchCriteria.cs
//Description:      Maps SearchViewModel class to a SearchCriteria class.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;

using Core.Factories;
using Core.Model;
using THLibrary.DataModel;

namespace THLibrary.Infrastructure.MapperClasses
{
    /// <summary>
    /// A Generic class <c>MapSearchViewModelToSearchCriteria</c> is responsible for mapping
    /// a SearchViewModel class From the UI to a SearchCriteria class of the Business model.  
    /// </summary>
    /// <typeparam name="T">SearchViewMode class or class derived from SearchViewModel</typeparam>
    /// <remarks>
    /// <para>
    /// It also works for the CurrentSearchViewModel as it is generic and the 
    /// CurrentSearchViewModel derives from SearchViewmodel.
    /// </para>
    /// <para>
    /// This class is not static so that we can inject a dependency in the constructor
    /// </para>
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
