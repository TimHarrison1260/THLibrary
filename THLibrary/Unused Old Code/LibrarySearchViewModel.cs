using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;   // ObervableCollection<>()
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THLibrary.Common;

using Core.Interfaces;

namespace THLibrary.DataModel
{
    /// <summary>
    /// Class <c>LibrarySearchViewModel</c> is the view model
    /// for the single page application, for searching the
    /// Library.
    /// </summary>
    /// <remarks>
    /// Inheriting from BindableBase, implements the INotifyPropertyChange
    /// and defines the event and OnChange
    /// </remarks>
   [Obsolete("Do NOT Use this class, instead use LibraryDataSource.cs",false)]
    public class LibrarySearchViewModel : INotifyPropertyChanged
    {
        //  Dependencies
        ILibraryRepository _libraryRepository;

        //  Internal Properties
        private ObservableCollection<BookViewModel> _books = new ObservableCollection<BookViewModel>();
        private int _selectedBookIndex = 0;

        //  Select a Search User Control.
        private ObservableCollection<SearchViewModel> _searches = new ObservableCollection<SearchViewModel>();
        private int _selectedSearchIndex = 0;

        //  Current / Advanced search criteria User Control
        private ObservableCollection<string> _searchTypes = new ObservableCollection<string>();
        private int _selectedSearchTypeIndex = 0;

        private ObservableCollection<string> _searchableItems = new ObservableCollection<string>();
        private int _searchableItemIndex = 0;


        /// <summary>
        /// Ctor: launched from XAML and therefore MUST have a parameterless ctor.
        /// </summary>
        /// <remarks>
        /// The App.xaml.cs file has to expose the repositories as Properties of the App.
        /// and the hack(App.Current as App).propertyname is used to get the Repository.
        /// It is instantiated and the file access has happened by then, as the App. ctor
        /// has executed, which is where the IoC container and the repositories are 
        /// instantiated.
        /// </remarks>
        public LibrarySearchViewModel()
        {

//            var rep = (App.Current as App).IoCContainer.Resolve<ILibraryRepository>();
            //_libraryRepository = (App.Current as App).LibraryRepository;

            //  Initialise with TempData to get the view model to work
            SetTempData();


            //  Set the current search to the first there is, provided some exist.
            if (_searches.Count() > 0)
                this.SelectedSearchIndex = 0;

            ////  get the data from the business model.
            //foreach (var book in _libraryRepository.GetAllBooks())
            //{
            //    var bookvm = new BookViewModel();
            //    //  TODO:   SEt up Mapper class to abstact the code to a helper method.
            //    //  Map the LibraryBook class to the BookViewModel class
            //    bookvm.Title = book.Title;
            //    bookvm.Author = book.Author;
            //    bookvm.ISBN = book.ISBN;
            //    bookvm.Synopsis = book.Synopsis;
            //    bookvm.Keywords = book.KeyWords.ToList<string>();
            //    _books.Add(bookvm);
            //}
        }

        #region BookViewModel   List and current book User Controls
        public ObservableCollection<BookViewModel> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                OnPropertyChanged("Books");
            }
        }

        public BookViewModel CurrentBook
        {
            get { return _books[_selectedBookIndex]; }
            set 
            {
                this._books[_selectedBookIndex] = value;
                OnPropertyChanged("CurrentBook"); 
            }
        }

        public int SelectedBookIndex
        {
            get { return _selectedBookIndex; }
            set
            {
                _selectedBookIndex = value;
                //  Set the current record because it changes too and need to notify
                //  If it wasn't notifying this would not be necessary
                this.CurrentBook = _books[_selectedBookIndex];
                OnPropertyChanged("SelectedBookIndex");
            }
        }
        #endregion  

        public ObservableCollection<string> SearchTypes
        {
            get { return _searchTypes; }
            set
            {
                _searchTypes = value;
                OnPropertyChanged("SearchTypes");
            }
        }

        public string CurrentSearchType
        {
            get { return _searchTypes[_selectedSearchTypeIndex]; }
            set
            {
                this._searchTypes[_selectedSearchTypeIndex] = value;
                OnPropertyChanged("CurrentSearchType");
            }
        }

        public int SelectedSearchTypeIndex
        {
            get { return _selectedSearchTypeIndex; }
            set
            {
                _selectedSearchTypeIndex = value;
                this.CurrentSearchType = _searchTypes[_selectedSearchTypeIndex];

                //  May have to set the CurrentSearch Type
                var sType = (Core.Model.SearchTypeEnum)Enum.Parse(typeof(Core.Model.SearchTypeEnum), this.CurrentSearchType);
                this.SearchableItems = GetSearchableItems(sType);
                this.SearchableItemsIndex = 0;

                OnPropertyChanged("SelectedSearchTypeIndex");
            }
        }


        public ObservableCollection<string> SearchableItems
        {
            get { return _searchableItems; }
            set
            {
                _searchableItems = value;
                OnPropertyChanged("SearchableItems");
            }
        }

        public int SearchableItemsIndex
        {
            get { return _searchableItemIndex; }
            set
            {
                _searchableItemIndex = value;
                //Add the current item to the Search String
//                this._searches[this._selectedSearchIndex].SearchString += this._searchableItems[this._searchableItemIndex];
                if (this._searchableItemIndex != -1)
                {
                    this.AddToSearchString(this._searchableItems[this._searchableItemIndex]);

                    OnPropertyChanged("SearchableItemsIndex");
                }
            }
        }

        public SearchViewModel CurrentSearch
        {
            get { return _searches[_selectedSearchIndex]; }
            set
            {
                this._searches[_selectedSearchIndex] = value;

                //  Set the selected Search Type
                //var searchtype = this._searches[_selectedSearchIndex].Type;
                this.SelectedSearchTypeIndex = _searchTypes.IndexOf(this._searches[_selectedSearchIndex].Type);

                OnPropertyChanged("CurrentSearch");
            }
        }


        public void AddToSearchString(string value)
        {
            //  Check the value is not already in the search string.
            List<string> searchStringElements = (this._searches[this._selectedSearchIndex].SearchString).Split('|').ToList<string>();
            if (!searchStringElements.Contains(value))
            {
                //  Update the searchString
                this._searches[this._selectedSearchIndex].SearchString += string.Format("|{0}", value);
                //  call OnPropertyChanged for the Current Search to force the UI to show the update.
                OnPropertyChanged("CurrentSearch");
            }
        }


        #region Select Search Drop Down
        public ObservableCollection<SearchViewModel> Searches
        {
            get { return _searches; }
            set
            {
                this._searches = value;
                OnPropertyChanged("Searches");
            }
        }

        public int SelectedSearchIndex
        {
            get { return _selectedSearchIndex; }
            set
            {
                _selectedSearchIndex = value;
                this.CurrentSearch = _searches[_selectedSearchIndex];
                OnPropertyChanged("SelectedSearchIndex");
            }
        }

        #endregion


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        //Property Changed
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion



        private ObservableCollection<string> GetSearchableItems(Core.Model.SearchTypeEnum searchType)
        {
            IEnumerable<string> searchItems = new List<string>();
            switch (searchType)
            {
                case Core.Model.SearchTypeEnum.Author:
                    searchItems = GetAuthors();
                    break;
                case Core.Model.SearchTypeEnum.Keyword:
                    searchItems = GetKeywords();
                    break;
                case Core.Model.SearchTypeEnum.Title:
                    searchItems = GetTitles();
                    break;
                case Core.Model.SearchTypeEnum.ISBN:
                    searchItems = GetISBNReferences();
                    break;
            }
            ObservableCollection<string> results = new ObservableCollection<string>();
            foreach (var item in searchItems)
            {
                results.Add(item);
            }
            return results;
        }



        #region Set the Data for the viewmodel

        private IEnumerable<string> GetAuthors()
        {
            var values = new List<string>()
            {
                "Adam Freeman", "Scott Millett", "Mark Seemann"
            };
            return values.AsEnumerable<string>();
        }
        private IEnumerable<string> GetKeywords()
        {
            var values = new List<string>()
            {
                ".NET", "MVC", "Patterns", "DI"
            };
            return values.AsEnumerable<string>();
        }

        private IEnumerable<string> GetTitles()
        {
            var values = new List<string>()
            {
                "Pro ASP.NET MVC 3 Framework", 
                "ASP.NET Design Patterns", 
                "Dependency Injection in .NET"
            };
            return values.AsEnumerable<string>();
        }

        private IEnumerable<string> GetISBNReferences()
        {
            var values = new List<string>()
            {
                "123456789", 
                "234567890", 
                "345678901"
            };
            return values.AsEnumerable<string>();
        }


        
        private void SetTempData()
        {
            //  Set up some book data;
            BookViewModel book = new BookViewModel()
            {
                Author="Adam Freeman",
                Title="Pro ASP.NET MVC 3 Framework",
                ISBN="123456789",
                Synopsis="A damn good book",
                Keywords=new ObservableCollection<string>() {
                    ".NET", "MVC"
                }
            };
            book.SetImage("Assets/Logo.png");
            _books.Add(book);

            book = new BookViewModel()
            {
                Author="Scott Millett",
                Title="ASP.NET Design Patterns",
                ISBN="234567890",
                Synopsis="Excellent and extremely well documented examples",
                Keywords=new ObservableCollection<string>() {
                    ".NET", "MVC", "Patterns"
                }
            };
            book.SetImage("Assets/Logo.png");
            _books.Add(book);

            book = new BookViewModel()
            {
                Author = "Mark Seemann",
                Title = "Dependency Injection in .NET",
                ISBN = "345678901",
                Synopsis = "All you really need to know about DI, except MVC4",
                Keywords = new ObservableCollection<string>() {
                    ".NET", "DI", "Patterns"
                }
            };
            book.SetImage("Assets/Logo.png");
            _books.Add(book);

            //  Set up some SearchType data
            //_searchTypes.Add(new SearchTypeViewModel() { SearchType = "Author" });
            //_searchTypes.Add(new SearchTypeViewModel() { SearchType = "Keyword" });
            //_searchTypes.Add(new SearchTypeViewModel() { SearchType = "Title" });
            //_searchTypes.Add(new SearchTypeViewModel() { SearchType = "ISBN" });

            _searchTypes.Add("Author");
            _searchTypes.Add("Keyword");
            _searchTypes.Add("Title");
            _searchTypes.Add("ISBN");

            //  Set up some previous searches
            SearchViewModel search = new SearchViewModel()
            {
                SearchDate = "01/12/2012",
                SearchString = ".NET|MVC",
                Type = "Keyword"
            };
            _searches.Add(search);

            search = new SearchViewModel()
            {
                SearchDate = "02/12/2012",
                SearchString = "Adam Freeman",
                Type = "Author"
            };
            _searches.Add(search);

            search = new SearchViewModel()
            {
                SearchDate = "01/12/2012",
                SearchString = "ASP.NET MVC 3",
                Type = "Title"
            };
            _searches.Add(search);


        }   // end SetTempData

        



        #endregion  
    }
}
