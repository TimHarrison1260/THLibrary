using System;

using Core.Model;
using Core.Model.ConcreteClasses;

namespace Core.Factories
{
    /// <summary>
    /// Class <c>ConcreteSearchCriteriaFactory</c>implements
    /// SearchCriteriaFactory and is responsible for creating
    /// an concrete instance of the abstract SearchCriteria class.
    /// </summary>
    public class ConcreteSearchCriteriaFactory : SearchCriteriaFactory
    {
        /// <summary>
        /// Create the instance of the SearchCriteria
        /// </summary>
        /// <returns>The instanceof the SearchCriteria class</returns>
        public override SearchCriteria Create()
        {
            //  Set defaults
            //      Type: Author
            //      Date: Now
            var search = new ConcreteSearchCriteria()
            {
                Title = "",
                Type = SearchTypeEnum.Author,
                SearchString = "",
                SearchDate = DateTime.Now
            };
            return search;
        }
    }
}
