﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    /// <summary>
    /// The Namespace <c>Infrastructure.Data</c> is part of the <c>Infrastructure</c> project and
    /// contains the classes responsible for accessing
    /// and furnishing the data to the business model.  The contents of each class are loaded
    /// at application startup and hold all data in memory until the application closes.
    /// The <c>Library</c> class holds the information relating to the libraryBooks and associated
    /// information.  The <c>SearchData</c> class holds all information relating to the searches
    /// that have been saved by users.
    /// </summary>
    /// <remarks>
    /// The <c>Infrastructure</c> namespace contains all elements of the
    /// Application design model, referred to as the 'Onion Model', considered
    /// to be part of the application dealing with cross cutting concerns.
    /// see <a href="http://jeffreypalermo.com/blog/the-onion-architecture-part-1/.">jeffreypalermo.com.</a>
    /// 
    /// This is a separate project within the Application, which therefore holds
    /// all objects in a separate assembly, thus improving the Separation of 
    /// Concerns required for reusable and unit testable code.
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGenerated()]
    class NamespaceDoc
    {
    }
}
