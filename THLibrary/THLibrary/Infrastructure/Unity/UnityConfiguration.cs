//***************************************************************************************************
//Name of File:     UnityConfiguration.cs
//Description:      Resolves the dependencies for the UI layer of the application
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using Microsoft.Practices.Unity;    //  Unity IoC

namespace THLibrary.Infrastructure.Unity
{
    /// <summary>
    /// Static class <c>UnityConfiguration</c> is responsible for defining the
    /// resolution of dependencies within the UI layer.  It calls the main
    /// <see cref="IoC.Configuration.RegisterTypes"/> to configure the applicatoin
    /// dependencies first.
    /// </summary>
    /// <remarks>
    /// There are no such dependencies in the application at the moment.  However, 
    /// all view Model classes should really be created using the factory pattern
    /// which would require dependencies to be resolved, this is where that would 
    /// be achieved.  (Future Development).
    /// </remarks>
    public static class UnityConfiguration
    {
        /// <summary>
        /// Method <c>RegisterTypes</c> defines the resolutions to the dependencies.
        /// </summary>
        /// <param name="container">Instance of the Unit IoC</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            //  Register types for the other components of the application
            IoC.Configuration.RegisterTypes(container);

            //  Register types for the UI layer only.
                
        }
    }
}
