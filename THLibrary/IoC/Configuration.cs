//***************************************************************************************************
//Name of File:     Configuration.cs
//Description:      Resolves the dependencies for the application
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;
using Core.Interfaces;
using Core.Factories;
using Infrastructure.Repositories;
using Infrastructure.Data;

namespace IoC
{
    /// <summary>
    /// Class <c>Configuration</c> is responsible for the runtime 
    /// configuration of the Unity IoC Container.  Within this
    /// IoC project, it will register the types for the Core
    /// and Infrastructure projects.  This avoids having to 
    /// reference the Infrastructure project from the THLivrary
    /// which would cause a circular reference.
    /// </summary>
    /// <remarks>
    /// A Unity LifetimeManager is used to manage the lifetime of 
    /// the instances of the Library and Search data models, which
    /// are instances of the UnitOfWork, so that the same instance
    /// is used by all references to it.  It effectively ensures
    /// that the Library and Search data classes are singletons.
    /// </remarks>
    public static class Configuration
    {
        /// <summary>
        /// Register the types to be injected using the Unity IoC
        /// </summary>
        /// <param name="container">The singleton instance of the IoC container</param>
        /// <remarks>
        /// This methos resolves the dependencies for all components within 
        /// the layers, other than the UI layer.  There is an additional RegisterTypes
        /// method within the UI layer, which resolves dependencies entirely within
        /// the UI layer.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            //  Register repositories (Core and Infrastructure projects)
            container
                //  Instantiate the UnitOfWork pattern for the  data model, and ensure
                //  that Unity serves up the same instance, by setting the lifetime
                //  managment to the entire application lifetime as the data is only
                //  ever in memory.  the ContainerControllerLifetimeManage included
                //  in the type registration should have the effect of serving up 
                //  singleton of the Library (UnitOfWork)
                .RegisterType<IUnitOfWork, Library>(new ContainerControlledLifetimeManager())
                //  Likewise for the SearchData (SearchUnitOfWork)
                .RegisterType<ISearchUnitOfWork, SearchData>(new ContainerControlledLifetimeManager())
                //  Instantiate the LibraryRepository
                .RegisterType<ILibraryRepository, LibraryRepository>()
                //  Instantiate the SearchRepository
                .RegisterType<ISearchRepository, SearchRepository>()
                ;

            //  Register factories (both in the Core project)
            container
                .RegisterType<LibraryBookFactory, ConcreteLibraryBookFactory>()
                .RegisterType<SearchCriteriaFactory, ConcreteSearchCriteriaFactory>()
                ;
        }
    }
}
