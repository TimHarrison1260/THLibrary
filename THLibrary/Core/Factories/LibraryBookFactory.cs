//***************************************************************************************************
//Name of File:     LibraryBookFactory.cs
//Description:      Contract for the LibraryBookFactory
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using Core.Model;

namespace Core.Factories
{
    /// <summary>
    /// Abstract Class <c>LibraryBookFactory</c> 
    /// provides a contract for the LibraryBookFactory 
    /// class.
    /// </summary>
    /// <remarks>
    /// This is an implementation of the Abstract Factory pattern.
    /// </remarks>
    public abstract class LibraryBookFactory
    {
        /// <summary>
        /// Create an instance of the <see cref="Core.Model.LibraryBook"/> class
        /// </summary>
        /// <returns>An Instance of LibraryBook</returns>
        public abstract LibraryBook Create();
    }
}
