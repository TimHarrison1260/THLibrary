//***************************************************************************************************
//Name of File:     SearchViewModel.cs
//Description:      Defines the Search contract..
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Text;
using THLibrary.Common;

namespace THLibrary.DataModel
{
    /// <summary>
    /// Class<c>SearchViewModel</c> defines a search criteria for the UI layer.  It defines
    /// all properties as string to support the UI.
    /// </summary>
    /// <remarks>
    /// It implements the IEquatable(T) interface to support selection of the search from the
    /// combobox for selecting defined searches.
    /// </remarks>
    public class SearchViewModel : BindableBase, IEquatable<SearchViewModel>
    {
//        private string _title = string.Empty;
        private string _uniqeId = string.Empty;
        private string _searchType = string.Empty;
        private string _searchString = string.Empty;
        private string _searchDate = string.Empty;

        /// <summary>
        /// Gets the title of the search.  
        /// </summary>
        public string Title
        {
            get 
            {
                StringBuilder bldr = new StringBuilder("Search for: ");
                bldr.Append(this._searchString);
                bldr.Append(", in: ");
                bldr.Append(this._searchType);
                bldr.Append(" on ");
                bldr.Append(this._searchDate);
                return bldr.ToString();
            }
            //set { this.SetProperty(ref this._title, value); }
        }

        /// <summary>
        /// Gets or sets the UniqueId of the search.
        /// </summary>
        public string UniqueId
        {
            get { return this._uniqeId; }
            set { this.SetProperty(ref this._uniqeId, value); }
        }

        /// <summary>
        /// Gets or sets the SearchType of the search.
        /// </summary>
        public string Type 
        {
            get { return this._searchType; }
            set { this.SetProperty(ref this._searchType, value); }
        }

        /// <summary>
        /// Gets or sets the SearcString for thesearch
        /// </summary>
        public string SearchString 
        {
            get { return this._searchString; }
            set { this.SetProperty(ref this._searchString, value); }
        }

        /// <summary>
        /// Gets or sets the date of the search
        /// </summary>
        public string SearchDate 
        {
            get { return this._searchDate; }
            set { this.SetProperty(ref this._searchDate, value); }
        }

        #region IEquatable interface

        /// <summary>
        /// Provides Equality checking for sorting, searching through
        /// dropdown contents.
        /// Compares this instance with the 'other' instance for 
        /// equality.
        /// </summary>
        /// <param name="other">The other instance being compared to this instance</param>
        /// <returns>True if both instances are equal, otherwise False.</returns>
        public bool Equals(SearchViewModel other)
        {
            var idBool = this.UniqueId.Equals(other.UniqueId);
            var typeBool = this.Type.Equals(other.Type);
            var qBool = this.SearchString.Equals(other.SearchString);
            var dateBool = this.SearchDate.Equals(other.SearchDate);

            var result = idBool && typeBool && qBool && dateBool;

            return result;
        }

        #endregion
    }
}
