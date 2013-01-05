
namespace THLibrary.DataModel
{
    /// <summary>
    /// Namespace <c>THLibrary.DataModel</c> contains all View Models that support
    /// the components of the main page.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>LibraryDataSource</c> performs the role of "ViewModel" for the page
    /// and brings together the other classes to properly support the main page. Keeping them
    /// completely separate from the Business Models, means that no UI concerns creep into 
    /// the business model, and the view models can match exactly the Views they support which
    /// need bare no relation to the structure of the business model, this helps promote loose 
    /// coupling with the business model and good separation of concerns.
    /// </para>
    /// <para>
    /// The models support the automatic twoway binding offered by the xaml pages
    /// by deriving from the <see cref="THLibrary.Common.BindableBase"/> class.
    /// This implements the standard <c>INotifyPropertyChanged</c> interface which provides
    /// for the <c>PropertyChanged</c> event being raised.  The setters of properties 
    /// use the <see cref="THLibrary.Common.BindableBase.SetProperty"/> method
    /// which fires the event if the property value has changed.
    /// </para>
    /// <para>
    /// The models also implement collections as <c>ObservableCollections&lt;T&gt;</c>
    /// which follow the Observer design pattern, which is how the changes in the collections
    /// and the bound controls are propogated automatically.
    /// </para>
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGenerated()]
    class NamespaceDoc
    {
    }
}
