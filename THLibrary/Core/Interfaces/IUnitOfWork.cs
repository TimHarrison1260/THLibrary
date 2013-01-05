//***************************************************************************************************
//Name of File:     IUnitOfWork.cs
//Description:      Interface for the Unit of work pattern for the Library data.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

namespace Core.Interfaces
{
    /// <summary>
    /// Interface <c>IUnitOfWork</c> defines the interface suitable 
    /// for the Library class, allowing the implementation of the 
    /// UnitOfWork pattern to ensure that the same instance of the 
    /// underlying Library data is always accessed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It contains a single method, <c>Load</c> which allows the
    /// library to be populated from the underlying data source.
    /// </para>
    /// <para>
    /// Because this is part of a Windows Store Appllication, a
    /// specific Load method is needed so that file IO and data
    /// load can happen when the application is launched.
    /// Previously, problems have been experienced with IO calls
    /// to read application folders, when not performed directly
    /// from the Application Launch and suspend method (within the
    /// App.xaml.cs).
    /// </para>
    /// </remarks>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Load the data from the underlying data source to the library class.
        /// </summary>
        void Load();
    }
}
