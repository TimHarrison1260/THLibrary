//***************************************************************************************************
//Name of File:     ConcreteLibraryBookFactory.cs
//Description:      Contract for the LibraryBookFactory
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

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
        /// Creates an initialised instance of the 
        /// <see cref="Core.Model.LibraryBook"/> class.
        /// </summary>
        /// <returns>Initialised LibraryBook class.</returns>
        /// <remarks>
        /// Derives from the <see cref="Core.Factories.LibraryBookFactory"/> abstract class.
        /// </remarks>
        public override LibraryBook Create()
        {
            return new ConcreteLibraryBook()
            {
                ISBN = "",
                Title = "",
                Author = "",
                Synopsis = "",
                ImagePath = "",
                KeyWords = new List<string>()
            };
        }
    }
}
