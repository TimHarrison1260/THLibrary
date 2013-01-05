//***************************************************************************************************
//Name of File:     BookSortEnum.cs
//Description:      Defines the possible sort sequences of matching books.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

namespace THLibrary.DataModel
{
    /// <summary>
    /// Enumeration <c>BookSortEnum</c> defins the possible sort sequences
    /// for displaying the list of matching books.  It is used within the UI only.
    /// </summary>
    public enum BookSortEnum
    {
        /// <summary>
        /// Sort books by Title
        /// </summary>
        Title,
        /// <summary>
        /// Sort books by Author
        /// </summary>
        Author
    }
}
