using System.Collections.Generic;
using Core.Model.ConcreteClasses;   //  Concrete implementations of abstract classes.
using Core.Model;                   //  Business model classes

namespace Core.Factories
{
    /// <summary>
    /// Concrete implementation of the LibraryBookFactory
    /// </summary>
    public class ConcreteLibraryBookFactory : LibraryBookFactory
    {
        /// <summary>
        /// Creates a concrete implementation of 
        /// a LibraryBook.
        /// </summary>
        /// <returns>Initialised LibraryBook class.</returns>
        public override LibraryBook Create()
        {
            return new ConcreteLibraryBook()
            {
                ISBN = "",
                Title = "",
                Author = "",
                Synopsis = "",
                KeyWords = new List<string>()
            };
        }
    }
}
