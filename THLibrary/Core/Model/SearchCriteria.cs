using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;     //  Allow the serialisztion to work.

namespace Core.Model
{
    /// <summary>
    /// Abstract class that defines a search.
    /// </summary>
    /// <remarks>
    /// Implements the <c>IComparable(T)</c> interface to define a default sort sequence for any
    /// collection containing the search.
    /// Implements the <c>IEquatable(T)</c> interface to allow comparison with another instance
    /// of SearchCriteria for equality.
    /// Implements the <c>IEqualityComparer</c> to allow two instance of SearchCriteria to be
    /// compared for equality.
    /// 
    /// This SearchCriteria class will be serialised as XML, and therefore does not need
    /// any attributes to declare properties as serialisable, instead they
    /// just need to be public.
    /// </remarks>
    [XmlInclude(typeof(Core.Model.ConcreteClasses.ConcreteSearchCriteria))]
    public abstract class SearchCriteria : IComparable<SearchCriteria>,         //  Default Sort order
                                            IEquatable<SearchCriteria>,         //  Compare with another instance
                                            IEqualityComparer<SearchCriteria>   //  Compare two instances
    {
        /// <summary>
        /// Get or sets the uniquId for the search
        /// </summary>
        public int UniqueId { get; set; }
        /// <summary>
        /// A title for the search
        /// </summary>
        /// <remarks>
        /// Set the title to "Search for 'type': 'value(s)"
        /// </remarks>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the type of search, which defines whether
        /// the search is for Author, Title, ISBN or a keyword.
        /// </summary>
        public SearchTypeEnum Type { get; set; }
        /// <summary>
        /// The string to search for within the specified keyword.
        /// </summary>
        /// <remarks>
        /// A space separated collection of string values.
        /// </remarks>
        //  TODO: make this into a string[] array.  code the getter
        //          and setter explicitly to covert to srting[].
        public string SearchString { get; set; }
        /// <summary>
        /// Date and time the search was last run.
        /// </summary>
        public DateTime SearchDate { get; set; }



        #region IComparable<T> interface
        /// <summary>
        /// Implementation of the IComparable interface which is used
        /// by the sort method as the default sort sequence.
        /// The default sort is by SearchCriteria Title.
        /// </summary>
        /// <param name="other">The instance of SearchCriteria being compared with this instance</param>
        /// <returns>An <c>System.int</c>, indicating the relative position in the sort order of the SerchCriteria classes </returns>
        public int CompareTo(SearchCriteria other)
        {
            //  Default sort should be by Title
            return string.Compare(this.Title, other.Title);
        }
        #endregion


        #region IEquatable<T> interface
        /// <summary>
        /// Implementation of the IEquatable<> interface
        /// </summary>
        /// <param name="other">The instance of SearchCriteria being compared to this one</param>
        /// <returns>Returns TRUE if instances are equal otherwise FALSE.</returns>
        public bool Equals(SearchCriteria other)
        {
            return AreEqual(this, other);
        }
        #endregion


        #region IEqualityComparer<T> interface
        /// <summary>
        /// Implementation of the IEqualityComparer<> interface  Check
        /// for equality between the two supplied instances of 
        /// SearchCriteria.
        /// </summary>
        /// <param name="s1">The first instance of SearchCriteria</param>
        /// <param name="s2">The second instance of SearchCriteria</param>
        /// <returns>Returns TRUE if instances are equal otherwise FALSE.</returns>
        public bool Equals(SearchCriteria s1, SearchCriteria s2)
        {
            return AreEqual(s1, s2);
        }

        /// <summary>
        /// Part of the IEqualityComparer<> interface
        /// return a hashcode generated from the 
        /// Type, SearchString and SearchDate.  
        /// ie. the information that makes a searchCriteria
        /// equal.
        /// </summary>
        /// <param name="obj">An instance of SearchCriteria</param>
        /// <returns>The generated HashCode.</returns>
        public int GetHashCode(SearchCriteria obj)
        {
            StringBuilder bldr = new StringBuilder(Enum.GetName(typeof(SearchTypeEnum), obj.Type));
            bldr.Append(obj.SearchString);
            bldr.Append(obj.SearchDate);
            var hash = bldr.ToString().GetHashCode();
            return hash;
        }
        #endregion

        
        #region private methods
        /// <summary>
        /// Check the two instances of searchCriteria for 
        /// equality.  They are deemed equal if the 
        /// Type, SearchString and Date are the same.
        /// </summary>
        /// <param name="s1">The first instance of SearchCriteria</param>
        /// <param name="s2">The second instance of SearchCriteria</param>
        /// <returns>Returns TRUE if instances are equal otherwise FALSE.</returns>
        private bool AreEqual(SearchCriteria s1, SearchCriteria s2)
        {
            return (s1.Type == s2.Type) &&
                s1.SearchString.Equals(s2.SearchString) &&
                s1.SearchDate.Equals(s2.SearchDate);
        }
        #endregion
    }
}
