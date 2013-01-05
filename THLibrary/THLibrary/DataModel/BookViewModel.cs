//***************************************************************************************************
//Name of File:     BookViewModel.cs
//Description:      BookViewModel, supports the BookViewer user Control
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.ObjectModel;
using THLibrary.Common;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace THLibrary.DataModel
{
    /// <summary>
    /// View Model for a Book.
    /// </summary>
    /// <remarks>
    /// Implementing BindableBase provides the INotifyPropertyChanged functionality, which 
    /// provides for the PropertyChanged event which facilitates the automatic binding
    /// with the xaml controls on the page.
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

        /// <summary>
        /// An Observable collection of strings, containing the keywords attachecd
        /// to the book
        /// </summary>
        public ObservableCollection<string> Keywords
        {
            get { return this._keywords; }
            set { this._keywords = value; }
        }

        /// <summary>
        /// Gets the Image associated with the book
        /// </summary>
        /// <remarks>
        /// The path for the image is combined with the base Uri, to find
        /// the actual image which is loaded when required.
        /// The base Url points to the Assets folder within the appliation.
        /// </remarks>
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

        /// <summary>
        /// Sets the path to the image for the book.  This has the effect
        /// of changing the image.  The image itself is only loaded when
        /// the Getter of the Image property is called.
        /// </summary>
        /// <param name="path">The path for the book.</param>
        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }
    }
}
