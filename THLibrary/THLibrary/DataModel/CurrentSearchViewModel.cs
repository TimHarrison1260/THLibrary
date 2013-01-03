using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using THLibrary.Common;

using THLibrary.DataModel;

namespace THLibrary.DataModel
{
    public class CurrentSearchViewModel : SearchViewModel
    {
        private ObservableCollection<SearchTypesViewModel> _searchTypes = new ObservableCollection<SearchTypesViewModel>();
        private int _selectedTypeIndex = 0;
        private int _selectedTypeValueIndex = 0;
        //private ObservableCollection<string> _searchableItems = new ObservableCollection<string>();
        //private SearchViewModel _search = new SearchViewModel();

        public CurrentSearchViewModel()
        {
            //  Load the search types
            //  Set the search string to empty, the type to SearchTypes[0].
        }

        public ObservableCollection<SearchTypesViewModel> SearchTypes
        {
            get { return _searchTypes; }
            set { this.SetProperty(ref this._searchTypes, value); }
        }

        public int SelectedTypeIndex
        {
            get { return _selectedTypeIndex; }
            set 
            {
                //  Set the new value of the property
                this.SetProperty(ref this._selectedTypeIndex, value); 
                //  Set the corresponding Type of the base class
                this.Type = this.SearchTypes[value].Type;
            }
        }

        public int SelectedTypeValueIndex
        {
            get { return _selectedTypeValueIndex; }
            set { this.SetProperty(ref this._selectedTypeValueIndex, value); }
        }

        //public ObservableCollection<string> SearchableItems
        //{
        //    get { return this._searchableItems; }
        //    set { this.SetProperty(ref this._searchableItems, value); }
        //}

        //public SearchViewModel Search
        //{
        //    get { return this._search; }
        //    set { this.SetProperty(ref this._search, value); }
        //}

        public void SetSearch(SearchViewModel newSearch, ObservableCollection<SearchTypesViewModel> types)
        {
            if (this.SearchTypes.Count() == 0)
            {
                //  load the search types
                this.SearchTypes = types;
            }

            //  Load each property of the search individually so that all property changes are notified
            //
            //if (!this.Equals(newSearch))
            //{
            this.UniqueId = newSearch.UniqueId;
            this.SearchString = newSearch.SearchString;
            this.SearchDate = newSearch.SearchDate;
            this.Type = newSearch.Type;

            //  Set the dropdown to show the correct value for the search.
            var idx = _searchTypes.IndexOf(new SearchTypesViewModel() { Type = newSearch.Type });
            this.SelectedTypeIndex = idx;
            this._selectedTypeValueIndex = -1;  //  Update without notifications.
            this.SelectedTypeValueIndex = 0;    //  Update with notifications.
            //}
        }

    }
}
