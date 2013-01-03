using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    /// <summary>
    /// Enumeration <c>SearchTypeEnum</c> defines the different
    /// types of search possible within this Library search facility.
    /// The default is to search for Author.
    /// </summary>
    public enum SearchTypeEnum
    {
        SearchString,
        Author,
        Title,
        ISBN,
        Keyword
    }
}
