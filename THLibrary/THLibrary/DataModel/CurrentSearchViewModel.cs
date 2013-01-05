//***************************************************************************************************
//Name of File:     CurrentSearchViewModel.cs
//Description:      CurrentSearchViewModel, supports the CurrentSearchViewer user Control
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Collections.ObjectModel;
using System.Linq;

namespace THLibrary.DataModel
{
    /// <summary>
    /// Class <c>CurrentSearchViewModel</c> defines the view model that supports
    /// the <see cref="THLibrary.CustomControls.CurrentSearchViewer"/> user control.
    /// </summary>
    /// <remarks>
    /// The class derives from the <see cref="THLibrary.DataModel.SearchViewModel"/> class 
    /// which maps to the <see cref="Core.Model.SearchCriteria"/> class.  It adds the 
    /// SearchTypes and values to support the definition of the <c>SearchViewModel</c>.
    /// </remarks>
    public class CurrentSearchViewModel : SearchViewModel
    {
        //  Collection of Search Types, populated with the SearchTypesEnum.
        private ObservableCollection<SearchTypesViewModel> _searchTypes = new ObservableCollection<SearchTypesViewModel>();
        //  Index of type selected in the combobox.
        private int _selectedTypeIndex = 0;
        //  Index of the selected searchable value for the SearchType.
        private int _selectedTypeValueIndex = 0;

        /// <summary>
        /// Gets or sets the possiible SearchTypes for a search
        /// </summary>
        public ObservableCollection<SearchTypesViewModel> SearchTypes
        {
            get { return _searchTypes; }
            set { this.SetProperty(ref this._searchTypes, value); }
        }

        /// <summary>
        /// Gets or sets the Index of the SearchType selected from the SerchType Combobox.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the Index of the SearchType selected value.
        /// </summary>
        public int SelectedTypeValueIndex
        {
            get { return _selectedTypeValueIndex; }
            set { this.SetProperty(ref this._selectedTypeValueIndex, value); }
        }

        /// <summary>
        /// Method <c>SetSearch</c> sets the specified search as the current search
        /// within the CurrentSearchViewModel.
        /// </summary>
        /// <param name="newSearch">The SearchViewModel to be set as the current search.</param>
        /// <param name="types">Collection of SearchTypes</param>
        public void SetSearch(SearchViewModel newSearch, ObservableCollection<SearchTypesViewModel> types)
        {
            if (this.SearchTypes.Count() == 0)
            {
                //  load the search types
                this.SearchTypes = types;
            }

            //  Load each property of the search individually so that all property changes are notified
            this.UniqueId = newSearch.UniqueId;
            this.SearchString = newSearch.SearchString;
            this.SearchDate = newSearch.SearchDate;
            this.Type = newSearch.Type;

            //  Set the dropdown to show the correct value for the search.
            var idx = _searchTypes.IndexOf(new SearchTypesViewModel() { Type = newSearch.Type });
            this.SelectedTypeIndex = idx;
            this._selectedTypeValueIndex = -1;  //  Update without notifications, to force the change with the next update.
            this.SelectedTypeValueIndex = 0;    //  Update with notifications.
        }
    }
}
