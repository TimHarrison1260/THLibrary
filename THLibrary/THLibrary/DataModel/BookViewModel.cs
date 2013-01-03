using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THLibrary.Common;
using THLibrary.DataModel;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace THLibrary.DataModel
{
    /// <summary>
    /// View Model for a Book.
    /// </summary>
    /// <remarks>
    /// Implementing BindableBase provides the INotifyPropertyChanged functionality
    /// </remarks>
    public class BookViewModel : BindableBase
    {
        //  private fields
        private string _uniqueId = string.Empty;
        private string _author = string.Empty;
        private string _title = string.Empty;
        private string _isbn = string.Empty;
        private string _synopsis = string.Empty;
        private ObservableCollection<string> _keywords = new ObservableCollection<string>();
        private ImageSource _image = null;
        private String _imagePath = null;

        private static Uri _baseUri = new Uri("ms-appx:///");

        /// <summary>
        /// Gets or sets the unique id of the Book.
        /// </summary>
        public string UniqueId
        {
            get { return _uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        /// <summary>
        /// Gets or Sets the Author for the BookViewModel
        /// </summary>
        public string Author
        {
            //  Simple return the _author
            get { return _author; }
            //  Use the base class SetProperty so changes are 
            //  notified if necessary
            set { this.SetProperty(ref this._author, value); }
        }

        /// <summary>
        /// Getsor ets the Title for the BookViewModel
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        /// <summary>
        /// Gets or sets the ISBN for the BookViewModel
        /// </summary>
        public string ISBN
        {
            get { return this._isbn; }
            set { this.SetProperty(ref this._isbn, value); }
        }

        /// <summary>
        /// Gets or sets the Synopsis for the BookViewModel
        /// </summary>
        public string Synopsis
        {
            get { return this._synopsis; }
            set { this.SetProperty(ref this._synopsis, value); }
        }

        public ObservableCollection<string> Keywords
        {
            get { return this._keywords; }
            set { this._keywords = value; }
        }


        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(_baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }




    }
}
