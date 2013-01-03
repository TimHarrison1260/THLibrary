using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using THLibrary.DataModel;
using THLibrary.Infrastructure.EventArguments;
using THLibrary.Infrastructure.MapperClasses;
using Core.Factories;
using Microsoft.Practices.Unity;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace THLibrary
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : THLibrary.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            //  Create the datasource for the Books.
            var libraryBooks = LibraryDataSource.GetBooks();
            this.DefaultViewModel["Items"] = libraryBooks;

            //  Create the datasource for the Searches binding.
            var searches = LibraryDataSource.GetSearches();
            this.DefaultViewModel["Searches"] = searches;

            //  Create the datasource for the CurrentSearch.
            var currentSearch = LibraryDataSource.GetCurrentSearch();
            this.DefaultViewModel["Current"] = currentSearch;
        }

        /// <summary>
        /// Preserves state associated with t his page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Handles the selection Changed event for the drop down list of saved searches.
        /// </summary>
        /// <param name="sender">Instance of the cbSelectSearch drop down box.</param> 
        /// <param name="e"></param>
        private void cbSelectSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  Try setting the selected search type for the current search (SelectedSearchTypeIndex)
            if (e.AddedItems.Count() == 1)
            {
                //  TODO: Refactor some of this to a New SearchViewModel constructor or Factory (IN the LibraryDataSource (the View Model)

                //  Retrieve the curent search, ready for update.
                //  must add to a collection, with only 1 item, to avoid an exception.
                var currentSearch = LibraryDataSource.GetCurrentSearch();
                //  Get the selected search from the drop down.
                var selectedSearch = (SearchViewModel)e.AddedItems[0];
                //  Get the available SearchTypes.
                var searchTypes = LibraryDataSource.GetSearchTypes();
                //  Update the CurrentSearch with the selected search.
                currentSearch.First().SetSearch(selectedSearch, searchTypes);
            }
        }


        /// <summary>
        /// Handles the ExecuteSearch event raised from the 
        /// <see cref="THLibrary.CustomControls.CurrentSearchViewer"/> custom
        /// control.  It is responsible for the search being executed against
        /// the underlying data model.
        /// </summary>
        /// <param name="sender">Instance of the CurrentSearchViewer Custom Control</param>
        /// <param name="e">Instance of the eventArgs which contain the search criteria</param>
        private void SearchViewer_Execute(object sender, SearchEventArgs e)
        {
            //  Get the matching books.
            var matchingBooks = LibraryDataSource.GetMatchingBooks(e.SearchCriteria);

            //  Add the search results to the binding datasource.  
            this.DefaultViewModel["Items"] = matchingBooks;
            if (matchingBooks.Count() > 0)
                this.resultsListView.SelectedIndex = 0;
        }

        #region AppBar handlers

        /// <summary>
        /// Handles the OnNewSearchButtonClicked even from the app bar button
        /// </summary>
        /// <param name="sender">Instance of the App Bar button</param>
        /// <param name="e">Instance of the event args.</param>
        private void OnNewSearchButtonClicked(object sender, RoutedEventArgs e)
        {
            //  TODO: Refactor some of this to a New SearchViewModel constructor or Factory (IN the LibraryDataSource (the View Model)
            
            //  Reset the setting in the CurrentSearchViewer.
            //  1   Blank Search String
            //  2   Search Types to "Select a search Type......"
            //  3   Searchable Values to empty
            var currentSearch = LibraryDataSource.GetCurrentSearch();
            var searchTypes = LibraryDataSource.GetSearchTypes();
            var selectedSearch = new SearchViewModel()
            {
                UniqueId = string.Empty,
                Type = searchTypes[0].Type,
                SearchString = string.Empty,
                SearchDate = DateTime.Now.ToString(),
            };
            currentSearch.First().SetSearch(selectedSearch, searchTypes);

        }

        /// <summary>
        /// Handles the OnSaveSearchButtonClicked event from the app bar button
        /// </summary>
        /// <param name="sender">Instance of the App Bar button</param>
        /// <param name="e">Instance of the event args.</param>
        private void OnSaveSearchButtonClicked(object sender, RoutedEventArgs e)
        {
            LibraryDataSource.AddSearch(LibraryDataSource.GetCurrentSearch().First());
        }

        /// <summary>
        /// Handles the OnResetButtonClicked event from the app bar button.  It refreshed
        /// the list with all the books in the library.  This is OK as there are only a
        /// handful of books in the library.
        /// </summary>
        /// <param name="sender">Instance of the App Bar button</param>
        /// <param name="e">Instance of the event args.</param>
        private void OnResetButtonClicked(object sender, RoutedEventArgs e)
        {
            //  Get the matching books.
            var books = LibraryDataSource.ResetAllBooks();

            //  Add the search results to the binding datasource.  
            this.DefaultViewModel["Items"] = books;
            if (books.Count() > 0)
                this.resultsListView.SelectedIndex = 0;
        }

        #endregion

    }
}
