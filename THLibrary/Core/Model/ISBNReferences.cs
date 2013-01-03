using System.Collections.Generic;

namespace Core.Model
{
    /// <summary>
    /// Class <c>ISBNReferences</c> represents a complete list
    /// of ISBN Numbers for all books within the library.
    /// It is intended primarily for use with quick search
    /// facilities.
    /// </summary>
    /// <remarks>
    /// It inherits the List(string) so that they can be
    /// searched for by index, found using string values
    /// and added to easily.
    /// It is populated when the books are first loaded.
    /// </remarks>
    public class ISBNReferences : List<string>
    {
    }
}
