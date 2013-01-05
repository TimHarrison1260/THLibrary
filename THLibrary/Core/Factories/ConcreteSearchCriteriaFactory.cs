//***************************************************************************************************
//Name of File:     ConcreteSearchCriteriaFactory.cs
//Description:      Contract for the LibraryBookFactory
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;

using Core.Model;
using Core.Model.ConcreteClasses;

namespace Core.Factories
{
    /// <summary>
    /// An instance of the SearchCriteriaFactory class.
    /// </summary>
    public class ConcreteSearchCriteriaFactory : SearchCriteriaFactory
    {
        /// <summary>
        /// Creates an initialised instance of the 
        /// <see cref="Core.Model.SearchCriteria"/> class.
        /// </summary>
        /// <returns>Initialised SearchCriteria class.</returns>
        /// <remarks>
        /// Derives from the <see cref="Core.Factories.SearchCriteriaFactory"/> abstract class.
        /// </remarks>
        public override SearchCriteria Create()
        {
            //  Set defaults
            //      Type: Author
            //      Date: Now
            var search = new ConcreteSearchCriteria()
            {
                Title = "",
                //  TODO:   Change default to SearchTypeEnum.SearchString.
                Type = SearchTypeEnum.Author,
                SearchString = "",
                SearchDate = DateTime.Now
            };
            return search;
        }
    }
}
