//***************************************************************************************************
//Name of File:     MinPage.xaml..cs
//Description:      Code behind the Application Main Page.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

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
    /// This is the Single Page of the application.  It provides the complete search
    /// functionality and displays the results.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The page is divided into different sections:
    /// <list type="numbered">
    /// <item>
    /// <term>SearchSelector</term>
    /// <description>This is the dropdown box where saved searches are listed, for selection. 
    ///   It is the top left of the page</description>
    /// </item>
    /// <item>
    /// <term>CurrentSearch</term>
    /// <description>This shows the definition of the selected search, or allow entry of
    /// new search criteria.  It also allows the search to be initiated.  It is the top right of the page.</description>
    /// </item>
    /// <item>
    /// <term>ResultsList</term>
    /// <description>This shows the results of the search, and allows these results to be sorted by Title or Author.
    /// It is the bottom left of the page</description>
    /// </item>
    /// <item>
    /// <term>BookDetail</term>
    /// <description>This shows the details of the book currently selected from the ResultsList.  It is the bottom
    /// right of the page.</description>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// The page supports an AppBar, which is displayed by right clicking the mouse.  The AppBar provides
    /// the options:
    /// <list type="numbered">
    /// <item>
    /// <term>New Search</term>
    /// <description>Create a new search and initialised the search type.</description>
    /// </item>
    /// <item>
    /// <term>Save Search</term>
    /// <description>Saves the current search to the data source</description>
    /// </item>
    /// <item>
    /// <term>Reset Results</term>
    /// <description>Cancels the search and redisplays all books in the library.</description>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// The xaml uses a mixture of user Controls and built-in controls to format the page.  This is to
    /// demonstrate different ways to code the xaml.
    /// </para>
    /// </remarks>
    public sealed partial class MainPage : THLibrary.Common.LayoutAwarePage
    {
        /// <summary>
        /// constructor for <c>MainPage</c>, which initilises the components on the page.
        /// </summary>
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
        /// requirements of <see cref="THLibrary.Common.SuspensionManager.SessionState"/>.
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


        /// <summary>
        /// Handles the Checked event for the group of radio buttons on the page
        /// </summary>
        /// <param name="sender">The RadioButton that raised the event</param>
        /// <param name="e">The args for the event</param>
        private void SelectSortSequence (object sender, RoutedEventArgs e)
        {
            //  Don't process anything if the datamodel is not initialised, things are being loaded.
            if (this.DefaultViewModel.Count() == 0) return;

            //  The button is names according to the enumeration.
            BookSortEnum seq = (BookSortEnum) Enum.Parse(typeof(BookSortEnum), (sender as RadioButton).Name);
            //  Set the current sort sequence
            LibraryDataSource.SetSortSequence(seq);
            //  retrieve the books sorted accordingly
            var books = LibraryDataSource.GetBooks();
            this.DefaultViewModel["Items"] = books;
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
