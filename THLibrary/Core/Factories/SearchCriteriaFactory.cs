using Core.Model;

namespace Core.Factories
{
    /// <summary>
    /// Abstract Class <c>SearchCriteriaFactory</c> 
    /// provides an interface to the SearhCriteriaFactory 
    /// class.
    /// </summary>
    public abstract class SearchCriteriaFactory
    {
        /// <summary>
        /// Create method signature
        /// </summary>
        /// <returns></returns>
        public abstract SearchCriteria Create();
    }
}
