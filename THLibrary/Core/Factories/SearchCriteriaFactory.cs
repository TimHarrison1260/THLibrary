//***************************************************************************************************
//Name of File:     SearchCriteriaFactory.cs
//Description:      Contract for the SearchCriteriaFactory
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using Core.Model;

namespace Core.Factories
{
    /// <summary>
    /// Abstract Class <c>SearchCriteriaFactory</c> 
    /// provides a contrat for the SearhCriteriaFactory 
    /// class.
    /// </summary>
    public abstract class SearchCriteriaFactory
    {
        /// <summary>
        /// Create an instance of the <see cref="Core.Model.SearchCriteria"/> class
        /// </summary>
        /// <returns>An instance of SearchCriteria</returns>
        public abstract SearchCriteria Create();
    }
}
