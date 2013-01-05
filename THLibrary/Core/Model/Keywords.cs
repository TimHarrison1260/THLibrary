//***************************************************************************************************
//Name of File:     Keywords.cs
//Description:      Defines a complete list of Keywords for all books in the library
//Author:           Tim Harrison
//Date of Creation: 00/00/00
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

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
    public class Keywords : List<string>
    {
        //  TODO:   Replace the AddRange methodn and the Insert methods

        /// <summary>
        /// Add a keyword to the collection if it doesnt exist.
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

    }
}
