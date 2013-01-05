//***************************************************************************************************
//Name of File:     LibraryRepository.cs
//Description:      Controls access to the Library data.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

using Core.Interfaces;
using Core.Model;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Concrete implmenetation of the <see cref="Core.Interfaces.ILibraryRepository"/> interface.
    /// It is responsible for access to the library contents, as well as controlling the loading of
    /// the library information from the CSV source file.
    /// </summary>
    public class LibraryRepository : ILibraryRepository
    {
        private readonly Library _unitOfWork;

        /// <summary>
        /// Ctor to inject reference to Dataclass.
        /// </summary>
        /// <param name="UnitOfWork">The concrete instance of the Library</param>
        public LibraryRepository(IUnitOfWork UnitOfWork)
        {
            if (UnitOfWork == null)
                throw new ArgumentNullException("UnitOfWork", "No UnitOWork supplied to LibraryRepository");
            _unitOfWork = UnitOfWork as Library;
        }

        /// <summary>
        /// Returns a collection of all <see cref="Core.Model.LibraryBook"/> held in the library.
        /// </summary>
        /// <returns>IEnumerable(LibraryBook)</returns>
        public IEnumerable<LibraryBook> GetAllBooks()
        {
            var library = _unitOfWork.Books;
            return library;
        }


        /// <summary>
        /// Gets the collection of value that can be searched for depending
        /// on the supplies string representation of the SearchTypeEnum.
        /// </summary>
        /// <param name="type">String representation of <see cref="Core.Model.SearchTypeEnum"/></param>.
        /// <returns>IEnumerable collection of string values.</returns>
        /// <exception cref="System.ArgumentException">
        /// This exception is thrown if the supplied Type is not in the SearchTypeEnum enumeration.
        /// </exception>
        public IEnumerable<string> GetSearchableBookValues(string type)
        {
            //  Convert the string to the corresponding SystemTypeEnum property.
            var valueType = (SearchTypeEnum) Enum.Parse(typeof(SearchTypeEnum), type);
            switch (valueType)
            {
                case SearchTypeEnum.SearchString:
                    return new List<string>();
                case SearchTypeEnum.Author:
                    return _unitOfWork.Authors;
                case SearchTypeEnum.ISBN:
                    return _unitOfWork.Isbns;
                case SearchTypeEnum.Keyword:
                    return _unitOfWork.Keywords;
                case SearchTypeEnum.Title:
                    return _unitOfWork.Titles;
            }

            throw new ArgumentException("type", string.Format("Invalid Search Type supplied: {'{0}'",type));
        }

        /// <summary>
        /// Returns a collection of <see cref="Core.Model.LibraryBook"/> that match the specified 
        /// <see cref="Core.Model.SearchCriteria"/>.
        /// </summary>
        /// <param name="searchCriteria">The specified search criteria</param>
        /// <returns>collection of matching Library books.</returns>
        public IEnumerable<LibraryBook> SearchBooks(SearchCriteria searchCriteria)
        {
            //  Set up results collection
            IEnumerable<LibraryBook> results = new List<LibraryBook>();

            //  Split searchString contents on '|' to search for multiple values
            string[] searchValues = searchCriteria.SearchString.Split('|');

            //  Perform search depending on SearchType.
            var searchType = searchCriteria.Type;
            switch (searchType)
            {
                case SearchTypeEnum.SearchString:
                    results = _unitOfWork.Books.Where(x => x.Title.Contains(searchValues[0]) || x.Synopsis.Contains(searchValues[0])); 
                    break;
                case SearchTypeEnum.Author:
                    results = _unitOfWork.Books.Where(x => searchValues.Contains(x.Author));
                    //results = _unitOfWork.Books.Where(x => x.Author == searchCriteria.SearchString);
                    break;
                case SearchTypeEnum.ISBN:
                    results = _unitOfWork.Books.Where(x => searchValues.Contains(x.ISBN));
                    break;
                case SearchTypeEnum.Keyword:
                    List<LibraryBook> intResults = new List<LibraryBook>();
                    foreach (string searchKeyword in searchValues)
                    {
                        var r = _unitOfWork.Books.Where(x => x.KeyWords.Contains(searchKeyword));
                        intResults.AddRange(r);
                    }
                    results = intResults.Distinct();
                    break;
                case SearchTypeEnum.Title:
                    results = _unitOfWork.Books.Where(x => searchValues.Contains(x.Title));
                    break;
                default:
                    results = new List<LibraryBook>();
                    break;
            }
            return results;
        }
    }
}
