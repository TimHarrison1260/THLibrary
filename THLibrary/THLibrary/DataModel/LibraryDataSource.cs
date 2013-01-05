//***************************************************************************************************
//Name of File:     LibraryDataSource.cs
//Description:      The Main ViewModel for the Main Page.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Core.Interfaces;
using Core.Factories;
using THLibrary.Infrastructure.MapperClasses;   //  Mapping classes between data model and View models.
using Microsoft.Practices.Unity;       //  IoC configuration UI project.

namespace THLibrary.DataModel
{
    /// <summary>
    /// Class <c>LibraryDataSource</c> is the view model that supports the main page
    /// of this single page Library Search application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It contains a Private static instance of itself, which is the standard pattern for 
    /// use with a datasource to ensure a singleton as described in the MS Windows Store 
    /// App documentation.
    /// </para>
    /// <para>
    /// It allows for a design instance which makes data available at design time so that
    /// the pages look "realistic" when being coded.
    /// </para>
    /// </remarks>
    public class LibraryDataSource
    {
        //  Instances of the repositories to access the underlying Data Model are required.
        //
        //  Feeling is that this should be done through property injection instead of referencing
        //  properties of the main App.xaml class.  Unity doesn't appear to allow this at the moment.
        private readonly ILibraryRepository _libraryRepository = (App.Current as App).LibraryRepository;
        private readonly ISearchRepository _searchRepository = (App.Current as App).SearchRepository;
        private readonly IUnityContainer _ioc = (App.Current as App).IoCContainer;

        /// <summary>
        /// Constructor for the class.  It loads the data from the CSV file through the Library Repository
        /// and the searches using the Search Repository.
        /// </summary>
        public LibraryDataSource()
        {
            //  Instantiate the data from the Data Model
            LoadData();
            //SetUpTestData();
        }

        /// <summary>
        /// Singleton: This creates a singleton instance of this class.
        /// </summary>
        /// <remarks>
        /// This is the standard pattern for use with a datasource to ensure a singleton as 
        /// described in the MS Windows Store App documentation.
        /// </remarks>
        private static LibraryDataSource _libraryDataSource = new LibraryDataSource();


        private BookSortEnum _currentSortSequence = BookSortEnum.Title;
        /// <summary>
        /// Gets the Enumeration that defines the possible sort sequences of the search results.
        /// </summary>
        public BookSortEnum CurrentSortSequence
        {
            get { return _currentSortSequence; }
        }


        #region View Model Main Properties

        private ObservableCollection<BookViewModel> _allBooks = new ObservableCollection<BookViewModel>();
        /// <summary>
        /// Gets the ObservableCollection of Library Books.  It contains all books in the library
        /// </summary>
        public ObservableCollection<BookViewModel> AllBooks
        {
            get { return this._allBooks; }
        }

        private ObservableCollection<SearchViewModel> _allSearches = new ObservableCollection<SearchViewModel>();
        /// <summary>
        /// Gets the ObservableCollection of all defined searches.
        /// </summary>
        public ObservableCollection<SearchViewModel> AllSearches
        {
            get { return this._allSearches; }
        }

        private CurrentSearchViewModel _currentSearch = new CurrentSearchViewModel();
        /// <summary>
        /// Gets the search defined as the Current search.
        /// </summary>
        public CurrentSearchViewModel CurrentSearch
        {
            get { return this._currentSearch; }
        }

        private ObservableCollection<SearchTypesViewModel> _searchTypes = new ObservableCollection<SearchTypesViewModel>();
        /// <summary>
        /// Gets the ObservableColection of possible SearchTypes.
        /// </summary>
        public ObservableCollection<SearchTypesViewModel> SearchTypes
        {
            get { return this._searchTypes; }
        }

        #endregion


        #region Books View Model

        /// <summary>
        /// Returns a collection of Books sorted according to the current sort, which
        /// is either by Title or by Author name.
        /// </summary>
        /// <returns>Sorted collection of Books.</returns>
        public static IEnumerable<BookViewModel> GetBooks()
        {
            return GetBooks(_libraryDataSource.CurrentSortSequence);
        }
        
        /// <summary>
        /// Returns a collection of Books sorted according to the specified sort sequence.
        /// </summary>
        /// <param name="sequence">The Sort sequence</param>
        /// <returns>Sorted collection of Books</returns>
        public static IEnumerable<BookViewModel> GetBooks(BookSortEnum sequence)
        {
            IEnumerable<BookViewModel> sortedBooks = new List<BookViewModel>();
            switch (sequence)
            {
                case BookSortEnum.Title:
                    sortedBooks = _libraryDataSource.AllBooks.OrderBy(x => x.Title);
                    break;
                case BookSortEnum.Author:
                    sortedBooks = _libraryDataSource.AllBooks.OrderBy(x => x.Author);
                    break;
            }
            return sortedBooks;
        }

        /// <summary>
        /// Returns a collection of Books that match the specified search criteria, and 
        /// sorted according to the current sort sequence.
        /// </summary>
        /// <param name="criteria">The criteria use to match the bolks.</param>
        /// <returns>Sorted collection of matching Books.</returns>
        public static IEnumerable<BookViewModel> GetMatchingBooks(SearchViewModel criteria)
        {
            //  Return empty list if search criteria not specified.
            if (criteria == null)
                return new List<BookViewModel>();

            //  Clear out the existing books.
            _libraryDataSource.AllBooks.Clear();
 
            //  Map the search criteria.
            //  Instantiate the mapper class.
            var mapper = new MapSearchViewModelToSearchCriteria<SearchViewModel>(_libraryDataSource._ioc.Resolve<SearchCriteriaFactory>());
            //  Map the SearchViewModel to the SearchCriteria.
            var searchCriteria = mapper.Map(criteria);

            //  Call the repository, SearchBooks()
            var allBooks = _libraryDataSource._libraryRepository.SearchBooks(searchCriteria);

            foreach (var book in allBooks)
            {
                _libraryDataSource.AllBooks.Add(MapLibraryBookToBookViewModel.Map(book));
            }
            
            //  Call the GetBooks method as this will invoke the current sort for the results.
            return GetBooks();
        }

        /// <summary>
        /// Gets a collection of books, containing all books in the library, sorted according
        /// to the current sort sequence.
        /// </summary>
        /// <returns>A sorted collection of books</returns>
        public static IEnumerable<BookViewModel> ResetAllBooks()
        {
            //  Clear out any existing books.
            _libraryDataSource.AllBooks.Clear();
            //  Get all books from the data model.
            var allBooks = _libraryDataSource._libraryRepository.GetAllBooks();
            foreach (var book in allBooks)
            {
                _libraryDataSource.AllBooks.Add(MapLibraryBookToBookViewModel.Map(book));
            }

            //  Call the GetBooks method as this will invoke the current sort for the results.
            return GetBooks();
        }

        /// <summary>
        /// Sets the current sort sequence for displaying the list of books, as specified
        /// by the <see cref="THLibrary.DataModel.BookSortEnum"/> enumeration.
        /// </summary>
        /// <param name="sequence">The Sort Sequence to be set.</param>
        public static void SetSortSequence(BookSortEnum sequence)
        {
            _libraryDataSource._currentSortSequence = sequence;
        }

        #endregion


        #region Searches view Model

        /// <summary>
        /// Returns a collection of Defined Searches
        /// </summary>
        /// <returns>A Collection of searches</returns>
        public static IEnumerable<SearchViewModel> GetSearches()
        {
            return _libraryDataSource.AllSearches;
        }

        /// <summary>
        /// Gets the current search to support the CurrentSearchViewer
        /// </summary>
        /// <returns>An IEnumerable(CurrentSearchViewModel) containing the current Search</returns>
        /// <remarks>
        /// <para>
        /// This returns a collection instead of a single instance to avoid an "index out of range"
        /// exception caused when a single instance is returned.  As the value is returned from a
        /// method, the inbuilt framework for the Windows Store App seems to expect a collection.
        /// </para>
        /// <para>
        /// Further investigation is required to resolve this, as it cannot be the case that all
        /// datasource models added to the DefaultViewModel must be collections.
        /// </para>
        /// </remarks>
        public static IEnumerable<CurrentSearchViewModel> GetCurrentSearch()
        {
            //  TODO: Refactor this code now that it's tested.
            var cur = new List<CurrentSearchViewModel>();
            cur.Add(_libraryDataSource.CurrentSearch);
            return cur;
            //return _libraryDataSource.CurrentSearch;
        }

        /// <summary>
        /// Returns and Observablecollection of SearchTypes.
        /// </summary>
        /// <returns>A Collection of SearchTypes</returns>
        public static ObservableCollection<SearchTypesViewModel> GetSearchTypes()
        {
            return _libraryDataSource.SearchTypes;
        }

        /// <summary>
        /// Adds the search contained in the CurrentSearchviewModel to the collection of Searches.
        /// </summary>
        /// <param name="newSearch">The CurrentSearchViewModel</param>
        public static void AddSearch(CurrentSearchViewModel newSearch)
        {
            //  Map the SearchViewModel to the SearchCriteria.
            var mapper = new MapSearchViewModelToSearchCriteria<CurrentSearchViewModel>(_libraryDataSource._ioc.Resolve<SearchCriteriaFactory>());
            var searchCriteria = mapper.Map(newSearch);

            //  Save in the data model
            var id = _libraryDataSource._searchRepository.AddSearch(searchCriteria);

            //  Add to the UI.
            _libraryDataSource.AllSearches.Add(MapSearchCriteriaToSearchViewModel.Map(searchCriteria));
        }

        #endregion


        #region Setup Data (private methods)

        /// <summary>
        /// Method <c>LoadData</c> is called to load the view model with the data
        /// from the underlying Data Model, via the repositories, which access
        /// the underlying Data Model.
        /// </summary>
        private void LoadData()
        {
            //  Load _allBooks      (LibraryRepository)
            var allBooks = _libraryRepository.GetAllBooks();
            //  Process each indiviually to ensure the UI observable stuff is updated.
            foreach (var book in allBooks)
            {
                this.AllBooks.Add(MapLibraryBookToBookViewModel.Map(book));
            }

            //  Load _allSearches   (SearchRepository)
            var allSearches = _searchRepository.GetSearches();
            foreach (var search in allSearches)
            {
                this.AllSearches.Add(MapSearchCriteriaToSearchViewModel.Map(search));
            }

            var searchTypes = _searchRepository.GetSearchTypes();
            foreach (var searchType in searchTypes)
            {
                //  Create a SearchTypeViewModel
                SearchTypesViewModel typeVM = new SearchTypesViewModel()
                {
                    Type = searchType,
                    Values = new ObservableCollection<string>()
                };

                //  Add a "Select" message as the first value for the search Type.
                //  TODO: Correct the Grammer for this function, select An Author, Select a Keyword.
                if (searchType == "SearchString")
                    typeVM.Values.Add(string.Format("Type a value into the search string"));
                else
                    typeVM.Values.Add(string.Format("Select a {0}....", searchType));

                //  Get the possible values from the library.
                var values = _libraryRepository.GetSearchableBookValues(searchType);
                //  Add each possible value into the SearchTypesViewModel
                foreach (var val in values)
                {
                    typeVM.Values.Add(val);
                }

                //  Add the SearchType to the view model
                this.CurrentSearch.SearchTypes.Add(typeVM);
                this.SearchTypes.Add(typeVM);
            }

            //  Load _currentSearch (item 0 of the searches loaded, if there are any)
            if (this.AllSearches.Count() == 0)
            {
                //  set to new search
                this.CurrentSearch.UniqueId = string.Empty;
                this.CurrentSearch.Type = _searchTypes[0].Type;
                this.CurrentSearch.SearchString = string.Empty;
                this.CurrentSearch.SearchDate = DateTime.Now.ToString();
                this.CurrentSearch.SelectedTypeIndex = 0;
                this.CurrentSearch.SelectedTypeValueIndex = 0;
            }
            else
            {
                //  Set to the first search.
                this.CurrentSearch.UniqueId = this.AllSearches[0].UniqueId;
                this.CurrentSearch.Type = this.AllSearches[0].Type;
                this.CurrentSearch.SearchString = this.AllSearches[0].SearchString;
                this.CurrentSearch.SearchDate = this.AllSearches[0].SearchDate;
                int idx = this.SearchTypes.IndexOf(new SearchTypesViewModel() { Type = this.CurrentSearch.Type });
                this.CurrentSearch.SelectedTypeIndex = idx;
                this.CurrentSearch.SelectedTypeValueIndex = 0;
            }

            //  Set the default sort sequence for display, which is by Title
            this._currentSortSequence = BookSortEnum.Title;
        }

        /// <summary>
        /// Method <c>SetUpTestData</c> load the view model with temporary data.
        /// </summary>
        private void SetUpTestData()
        {   
            //  Set up some book data;
            BookViewModel book = new BookViewModel()
            {
                UniqueId = "Book 1",
                Author = "Adam Freeman",
                Title = "Pro ASP.NET MVC 3 Framework",
                ISBN = "123456789",
                Synopsis = "A damn good book",
                Keywords = new ObservableCollection<string>() {
                    ".NET", "MVC"
                }
            };
            book.SetImage("Assets/Logo.png");
            this.AllBooks.Add(book);

            book = new BookViewModel()
            {
                UniqueId = "Book 2",
                Author = "Scott Millett",
                Title = "ASP.NET Design Patterns",
                ISBN = "234567890",
                Synopsis = "Excellent and extremely well documented examples",
                Keywords = new ObservableCollection<string>() {
                    ".NET", "MVC", "Patterns"
                }
            };
            book.SetImage("Assets/Logo.png");
            this.AllBooks.Add(book);

            book = new BookViewModel()
            {
                UniqueId = "Book 3",
                Author = "Mark Seemann",
                Title = "Dependency Injection in .NET",
                ISBN = "345678901",
                Synopsis = "All you really need to know about DI, except MVC4",
                Keywords = new ObservableCollection<string>() {
                    ".NET", "DI", "Patterns"
                }
            };
            book.SetImage("Assets/Logo.png");
            this.AllBooks.Add(book);


            //  Set up some previous searches
            SearchViewModel search = new SearchViewModel()
            {
                UniqueId = "Search 1",
                SearchDate = "01/12/2012",
                SearchString = ".NET|MVC",
                Type = "Keyword"
            };
            this.AllSearches.Add(search);

            search = new SearchViewModel()
            {
                UniqueId = "Search 2",
                SearchDate = "02/12/2012",
                SearchString = "Adam Freeman",
                Type = "Author"
            };
            this.AllSearches.Add(search);

            search = new SearchViewModel()
            {
                UniqueId = "Search 3",
                SearchDate = "01/12/2012",
                SearchString = "Pro ASP.NET MVC 3 Framework",
                Type = "Title"
            };
            this.AllSearches.Add(search);

            //  Set up the search Types and corresponding values
            SearchTypesViewModel type = new SearchTypesViewModel()
            {
                Type = "Select a Type...",
                Values = new ObservableCollection<string>()
                {
                    " "
                }
            };
            this.CurrentSearch.SearchTypes.Add(type);
            this.SearchTypes.Add(type);

            type = new SearchTypesViewModel()
            {
                Type = "Author",
                Values = new ObservableCollection<string>() 
                {
                    "Select an Author..", "Adam Freeman", "Scott Millett", "Mark Seemann"
                }
            };
            this.CurrentSearch.SearchTypes.Add(type);
            this.SearchTypes.Add(type);

            type = new SearchTypesViewModel()
            {
                Type = "Keyword",
                Values = new ObservableCollection<string>() 
                {
                    "Select a Keyword..", ".NET", "MVC", "Patterns", "DI"
                }
            };
            this.CurrentSearch.SearchTypes.Add(type);
            this.SearchTypes.Add(type);

            type = new SearchTypesViewModel()
            {
                Type = "ISBN",
                Values = new ObservableCollection<string>() 
                {
                    "Select an Isbn..", "123456789", "234567890", "345678901"
                }
            };
            this.CurrentSearch.SearchTypes.Add(type);
            this.SearchTypes.Add(type);

            type = new SearchTypesViewModel()
            {
                Type = "Title",
                Values = new ObservableCollection<string>() 
                {
                    "Select a Title..", 
                    "Pro ASP.NET MVC 3 Framework", 
                    "ASP.NET Design Patterns", 
                    "Dependency Injection in .NET"
                }
            };
            this.CurrentSearch.SearchTypes.Add(type);
            this.SearchTypes.Add(type);

            //  Set the curent search as the first one
            this.CurrentSearch.UniqueId = this.AllSearches[0].UniqueId;
            this.CurrentSearch.Type = this.AllSearches[0].Type;
            this.CurrentSearch.SearchString = this.AllSearches[0].SearchString;
            this.CurrentSearch.SearchDate = this.AllSearches[0].SearchDate;
            this.CurrentSearch.SelectedTypeIndex = 1;
            this.CurrentSearch.SelectedTypeValueIndex = -1;     //  Reset so the next update shows
            this.CurrentSearch.SelectedTypeValueIndex = 0;      //  Update the value drop down.
        }

        #endregion

    }
}
