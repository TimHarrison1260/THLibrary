using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface <c>IUnitOfWork</c> defines the interface suitable 
    /// for the Library class.
    /// </summary>
    /// <remarks>
    /// It contains a single method, <c>Load</c> which allows the
    /// library to be populated from the underlying data source.
    /// Because this is part of a Windows Store Appllication, a
    /// specific Load method is needed so that file IO and data
    /// load can happen when the application is launched.
    /// Previously, problems have been experienced with IO calls
    /// to read application folders, when not performed directly
    /// from the Application Launch and suspend method (within the
    /// App.xaml.cs).
    /// </remarks>
    public interface IUnitOfWork
    {
        void Load();
    }
}
