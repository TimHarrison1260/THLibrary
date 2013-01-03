using Core.Model;

namespace Core.Factories
{
    /// <summary>
    /// Abstract Class <c>LibraryBookFactory</c> 
    /// provides an interface to the LibraryFactory 
    /// class.
    /// </summary>
    public abstract class LibraryBookFactory
    {
        public abstract LibraryBook Create();
    }
}
