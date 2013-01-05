//***************************************************************************************************
//Name of File:     CurrentSearchviewer.xaml.cs
//Description:      Code behind the CurrentSearchViewer Custom control
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using THLibrary.DataModel;
using THLibrary.Infrastructure.EventArguments;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace THLibrary.CustomControls
{
    /// <summary>
    /// User Control class <c>CurrentSearchViewer</c> is responsible for maintaing and
    /// displaying the Search criteria for the app.
    /// This is the code behind which supports the Xaml, responsible for displaying
    /// and controlling the layout of the Custom control.
    /// </summary>
    /// <remarks>
    /// The public event <c>ExecuteSearch</c> is raised when the Search button is clicked
    /// and passes the current search in the <see cref="THLibrary.Infrastructure.EventArguments.SearchEventArgs"/>.
    /// </remarks>
    public sealed partial class CurrentSearchViewer : UserControl
    {
        /// <summary>
        /// Public Event <c>ExecuteSearch</c> which is raised
        /// when the SearchButton is clicked.
        /// </summary>
        public event EventHandler<SearchEventArgs> ExecuteSearch;

        /// <summary>
        /// Public, parameterless constructor of the <c>CurrentSearchViewer</c>
        /// which is a Custom Control.
        /// </summary>
        public CurrentSearchViewer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// This handles the click event of the Search Button.  This handler
        /// constructs a <see cref="THLibrary.DataModel.SearchViewModel"/> containing
        /// the search criteria, which are then passed to the ExecuteSearch event in 
        /// the <see cref="THLibrary.Infrastructure.EventArguments.SearchEventArgs"/>.
        /// </summary>
        /// <param name="sender">Object raising the event</param>
        /// <param name="e">Arguments for the event</param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //  Create the search Criteria
            var criteria = new SearchViewModel();
            criteria.UniqueId = string.Empty;
            criteria.SearchString = this.txtSearchString.Text;
            var selectedType = (SearchTypesViewModel)this.cbSearchType.SelectedItem;
            criteria.Type = selectedType.Type;
            criteria.SearchDate = DateTime.Now.ToString();

            //  Raise the Search event and pass the newly constructed SearchCriteria
            EventHandler<SearchEventArgs> handler = ExecuteSearch;
            if (handler != null)
            {
                handler(this, new SearchEventArgs() { SearchCriteria = criteria });
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event for the searchable values
        /// It addes the selected item to the SearchString if it hasn't already been added, 
        /// separating the values with a '|' character.
        /// This allows multiple search criteria to be submitted within a search
        /// </summary>
        /// <param name="sender">Instance of the Combobox containing the seearchable values</param>
        /// <param name="e">SelectionChange arguments for the combobox</param>
        /// <remarks>
        /// All possible collections of Searchable values have a "Select....." as the
        /// first item.  This is not selectable and allows us to avoid placing the first
        /// item in the searchstring by default.  We can limit it to only those selected
        /// by the user.  We get the indesxOf the "Select..." item and ignor the update
        /// if the index is 0;
        /// </remarks>
        private void cbSearchSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  Proceed if there is only one item selected.
            if (e.AddedItems.Count() == 1)
            {
                //  Get the selected item from the EventArgs.
                var selectedValue = e.AddedItems[0].ToString();
                
                //  Get the index of the selectedValue, and ignore if it is the zero value
                var searchableValues = this.cbSearchSelector.Items;
                var result = searchableValues.IndexOf(selectedValue);
                if (result != 0)
                {
                    //  Add it to the search string if it hasn't already been added.
                    //  Check if the value is already in the searchstring, by splitting that into
                    //  an array of string, and using "Contains" to locate it.
                    //  If it's not there, add it, otherwise ignore it.
                    string[] selectedVaues = this.txtSearchString.Text.Split('|');
                    if (selectedVaues.Count() == 0 || selectedVaues[0] == "")     // this is the first criteria being added.
                    {
                        this.txtSearchString.Text = selectedValue;
                    }
                    else if (!selectedVaues.Contains(selectedValue))    //  Does it contains the value?
                    {
                        this.txtSearchString.Text += string.Format("|{0}", selectedValue);
                    }
                }
                else
                {

                }
            }
        }

        /// <summary>
        /// Method <c>cbSearchType_SelectionChanged</c> handles the selectionChanged event
        /// It ensures that the SearchableValues combobox points to the first item.
        /// </summary>
        /// <param name="sender">Instance of the SearchableValues combobox.</param>
        /// <param name="e">Selectionchanged event arguments.</param>
        private void cbSearchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count() == 1 && e.RemovedItems.Count() != 0)
            {
                //  Ensure the Searchable values are reset to the "select' message.
                this.cbSearchSelector.SelectedIndex = 0;
            }
        }
    }
}
