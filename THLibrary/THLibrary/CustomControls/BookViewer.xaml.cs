//***************************************************************************************************
//Name of File:     BookViewer.xaml.cs
//Description:      Code Behind BookViewer User Control
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace THLibrary.CustomControls
{
    /// <summary>
    /// Class <c>Bookviewer</c> is the User control that displays the details of 
    /// a book.
    /// </summary>
    public sealed partial class BookViewer : UserControl
    {
        /// <summary>
        /// Constructor for the BookViewer class
        /// </summary>
        public BookViewer()
        {
            this.InitializeComponent();
        }
    }
}
