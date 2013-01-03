using System.Collections.Generic;

namespace Core.Model
{
    /// <summary>
    /// Class <c>Keywords</c> represents a complete list
    /// of Keyword tags for all books within the library.
    /// It is intended to give a complete list of Keywords
    /// to search for within the library.
    /// </summary>
    /// <remarks>
    /// It inherits the List(string) so that they can be
    /// searched for by index, found using string values
    /// and added to easily.
    /// It is populated when the books are first loaded.
    /// </remarks>
    public class Keywords : List<string>
    {
        /// <summary>
        /// Add a keyword to the collection if it doesn't exist.
        /// </summary>
        /// <param name="keyword">The keyword being added</param>
        /// <remarks>
        /// The <c>new</c> keyword is used so that the base.Add
        /// method is completely replaced by this method.
        /// </remarks>
        public new void Add(string keyword)
        {
            if (!base.Contains(keyword))
            {
                base.Add(keyword);
            }
        }

        //  TODO:   Replace the AddRange method
    }
}
