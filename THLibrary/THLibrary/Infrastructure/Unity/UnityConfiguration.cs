using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;    //  Unite IoC
using IoC;                          //  Solution IoC

namespace THLibrary.Infrastructure.Unity
{
    public static class UnityConfiguration
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            //  Register types for the other components of the application
            IoC.Configuration.RegisterTypes(container);

            //  Register types for the UI layer only.
                
        }
    }
}
