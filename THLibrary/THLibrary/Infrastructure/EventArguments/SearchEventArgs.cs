//***************************************************************************************************
//Name of File:     SearchEventArgs.cs
//Description:      Defines the Arguments for the ExecuteSearch event.
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

using THLibrary.DataModel;

namespace THLibrary.Infrastructure.EventArguments
{
    /// <summary>
    /// Class <c>SearchEventArgs</c> defines the arguments for the ExecuteSearch
    /// event of the CurrentSearchvewer.  
    /// </summary>
    /// <remarks>
    /// The eventargs enable the current search to be passed to the event handler
    /// to allow the search to be performed.
    /// </remarks>
    public class SearchEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets or sets the SearchCriteria to be passed to the search.
        /// </summary>
        public SearchViewModel SearchCriteria { get; set; }
    }
}
