using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THLibrary.Common;

namespace THLibrary.DataModel
{
    public class SearchViewModel : BindableBase, IEquatable<SearchViewModel>
    {
//        private string _title = string.Empty;
        private string _uniqeId = string.Empty;
        private string _searchType = string.Empty;
        private string _searchString = string.Empty;
        private string _searchDate = string.Empty;

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

        public string UniqueId
        {
            get { return this._uniqeId; }
            set { this.SetProperty(ref this._uniqeId, value); }
        }

        public string Type 
        {
            get { return this._searchType; }
            set { this.SetProperty(ref this._searchType, value); }
        }

        public string SearchString 
        {
            get { return this._searchString; }
            set { this.SetProperty(ref this._searchString, value); }
        }

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
