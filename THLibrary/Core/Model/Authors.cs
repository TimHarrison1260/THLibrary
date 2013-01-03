using System.Collections.Generic;

namespace Core.Model
{
    /// <summary>
    /// Class <c>Authors</c> represents a complete list
    /// of Authors for all books within the library.
    /// It is intended to give a complete list of Authors
    /// who have titles within the library, primarily for
    /// use for selecting Authors from a list.
    /// </summary>
    /// <remarks>
    /// It inherits the List(string) so that they can be
    /// searched for by index, found using string values
    /// and added to easily.
    /// It is populated when the books are first loaded.
    /// </remarks>
    public class Authors: List<string>
    {
        /// <summary>
        /// Add the Author to the collection if it doesn't exist.
        /// </summary>
        /// <param name="Author">The Author to add</param>
        /// <remarks>
        /// The <c>new</c> keyword is used so that the base.Add
        /// method is completely replaced by this method.
        /// </remarks>
        public new void Add(string Author)
        {
            if (!base.Contains(Author))
                base.Add(Author);
        }

        //  TODO:   Replace the AddRange method
    }
}
