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
    /// It is populated when the books are first loaded.
    /// </remarks>
    public class Titles : List<string>
    {
    }
}
