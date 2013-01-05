//***************************************************************************************************
//Name of File:     Titles.cs
//Description:      Defines a complete list of book Titles in the library
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
    /// Class <c>Titles</c> represents a complete list
    /// of Titles for all books within the library.
    /// It is primarily for use for selecting Titles
    /// from a list.
    /// </summary>
    /// <remarks>
    /// It inherits the List(string) so that they can be
    /// searched for by index, found using string values
    /// and added to easily.
    /// <para>
    /// The standard contract <c>IComparable</c> is not required to facilitate
    /// the sort, as it would just replicate the comparison of strings.  Since
    /// List is a generic collection of strings, this will be the sort by default.
    /// </para>
    /// <para>
    /// It is populated when the books are first loaded.
    /// </para>
    /// <para>
    /// Unlike <see cref="Core.Model.Authors"/>, it is not necessary to avoid 
    /// duplication of Titles as they are unique to a book.  
    /// Therefore there is no need to override the Add, AddRange, Insert and 
    /// InsertRange methods.
    /// </para>
    /// </remarks>
    public class Titles : List<string>
    {
    }
}
