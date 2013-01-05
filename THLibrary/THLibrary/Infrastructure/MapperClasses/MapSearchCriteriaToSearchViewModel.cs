//***************************************************************************************************
//Name of File:     MapSearchCriteriaToSearchViewModel.cs
//Description:      Maps SearchCriteria class to a SearchViewModel class.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;

using Core.Model;
using THLibrary.DataModel;

namespace THLibrary.Infrastructure.MapperClasses
{
    /// <summary>
    /// Static class <c>MapSearchCriteriaToSearchViewModel</c> is responsible for mapping
    /// a SearchCriteria class from the business model to a SearchViewModel class from the 
    /// UI View Models..
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
