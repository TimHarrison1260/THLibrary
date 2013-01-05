//***************************************************************************************************
//Name of File:     SearchTypeEnum.cs
//Description:      Defines The types of search that are possible.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

namespace Core.Model
{
    /// <summary>
    /// Enumeration <c>SearchTypeEnum</c> defines the different
    /// types of search possible within this Library search facility.
    /// The default is to search by SearchString.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <listheader>
    /// <term>SearchType</term>
    /// <description>Resulting search</description>
    /// </listheader>
    /// <item>
    /// <term>SearchString</term>
    /// <description>Searches for the value entered into the searchsting, within the Title or Synopsis of the book</description>
    /// </item>
    /// <item>
    /// <term>Author</term>
    /// <description>Searches for the value(s) entered into the searchstring, within the Author</description>
    /// </item>
    /// <item>
    /// <term>Title</term>
    /// <description>Searches for the value entered into the searchsrting, within the Title of the book</description>
    /// </item>
    /// <item>
    /// <term>ISBN</term>
    /// <description>Searches for the value entered into the searchsrting, within the ISBN</description>
    /// </item>
    /// <item>
    /// <term>Keyword</term>
    /// <description>Searches for the value entered into the searchsrting, within the Keywords of the book</description>
    /// </item>
    /// </list>
    /// The searches will accept multiple values in the search string if they are separated by a '|' character.
    /// </remarks>
    public enum SearchTypeEnum
    {
        /// <summary>
        /// Search for the value(s) in the Title and Synopsis of the Book.
        /// </summary>
        SearchString,
        /// <summary>
        /// Search for the value(s) in the Author of the book.
        /// </summary>
        Author,
        /// <summary>
        /// Search for the value(s) in the Title of the book.
        /// </summary>
        Title,
        /// <summary>
        /// Search for the value(s) in the ISBN of the book.
        /// </summary>
        ISBN,
        /// <summary>
        /// Search for the Value(s) in the Keywors of the book.
        /// </summary>
        Keyword
    }
}
