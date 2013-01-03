using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using THLibrary.DataModel;

namespace THLibrary.Infrastructure.EventArguments
{
    public class SearchEventArgs : System.EventArgs
    {
        public SearchViewModel SearchCriteria { get; set; }
    }
}
