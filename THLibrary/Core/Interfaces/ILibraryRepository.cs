using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Model;

namespace Core.Interfaces
{
    public interface ILibraryRepository
    {
        IEnumerable<LibraryBook> GetAllBooks();
        LibraryBook GetBook(string ISBN);               //  Don't need this
        IEnumerable<string> GetSearchableBookValues(string ValueType);

        IEnumerable<LibraryBook> SearchBooks(SearchCriteria searchCriteria);

    }
}

