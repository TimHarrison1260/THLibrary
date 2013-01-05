//***************************************************************************************************
//Name of File:     Authors.cs
//Description:      Defines a complete list of Authors for all books in the library
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Collections.Generic;

namespace Core.Model
{
    /// <summary>
    /// Class <c>Authors</c> represents a complete list
    /// of Authors for all books within the library.
    /// It is intended to give a complete list of Authors
    /// who have titles within the library, primarily for
    /// use when searching for an Authors books, allowing 
    /// the author to be selected from a list.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It inherits the List(string) so that they can be
    /// searched for by index, found using string values
    /// and added to easily.  Additionally it is sorted
    /// but the items are not added in sorted sequence.
    /// </para>
    /// <para>
    /// The standard contract <c>IComparable</c> is not required to facilitate
    /// the sort, as it would just replicate the comparison of strings.  Since
    /// List is a generic collection of strings, this will be the sort by default.
    /// </para>
    /// <para>
    /// It is populated when the books are first loaded.
    /// </para>
    /// <para>
    /// Future Development: To complete this class for robust control of
    /// adding elements and avoiding duplication, the AddRange, Insert and
    /// InsertRange methods should also be replaced.  They are not for this 
    /// implementation as these methods are not used for this application.
    /// </para>
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

        //  TODO:   Replace the AddRange method, the Insert and InsertRange methods too.
    }
}
