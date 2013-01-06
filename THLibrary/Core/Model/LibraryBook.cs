//***************************************************************************************************
//Name of File:     LibraryBook.cs
//Description:      Defines the Library Book held within the Library
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Text;
using System.Collections.Generic;

namespace Core.Model
{
    /// <summary>
    /// Class <c>LibraryBook</c> is an abstract class that defines a library book.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Instances of this class are created using the <see cref="Core.Factories.LibraryBookFactory"/>, which
    /// is an implementation of the Abstract Factory pattern.
    /// </para>
    /// <para>
    /// Implements the <c>IComparable(T)</c> interface to define a default sort sequence for any
    /// collection containing the search.
    /// Implements the <c>IEquatable(T)</c> interface to allow comparison with another instance
    /// of LibraryBook for equality.
    /// Implements the <c>IEqualityComparer</c> to allow two instance of LibraryBook to be
    /// compared for equality.
    /// </para>
    /// <para>
    /// However, the use of Linq, within the repositories, rather negates the use of these standard
    /// contracts.  They are, nevertherless, left in the class, to allow the class to be used without
    /// Linq.
    /// </para>
    /// </remarks>
    public abstract class LibraryBook : IComparable<LibraryBook>,
                                        IEquatable<LibraryBook>,
                                        IEqualityComparer<LibraryBook>
    {
        /// <summary>
        /// Gets or sets the ISBN reference of the book
        /// </summary>
        public string ISBN { get; set; }
        /// <summary>
        /// Gets or sets the Title of the book
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Author of the book
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Gets or sets the Synopsis of the book
        /// </summary>
        public string Synopsis { get; set; }
        /// <summary>
        /// Gets or sets the path to the Image of the book cover.
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Gets or sets the collection of keywords
        /// associated with the book
        /// </summary>
        /// <remarks>
        /// Using a List(string) to hold the keywords so 
        /// that they can be referenced individually
        /// and by and index.  A dictionary is not
        /// appropriate as we just need a list of
        /// strings, keys are not required.
        /// </remarks>
        public IList<string> KeyWords { get; set; }


        #region IComparable<T> interface
        /// <summary>
        /// Implementation of the IComparable interface which is used
        /// by the sort method as the default sort sequence.
        /// The default sort is by Book Title.
        /// </summary>
        /// <param name="other">The instance of the LibraryBook being compared to this one.</param>
        /// <returns>An <c>System.int</c>, indicating the relative position in the sort order of the LibraryBook classes </returns>
        public int CompareTo(LibraryBook other)
        {
            return this.Title.CompareTo(other.Title);
        }

        #endregion


        #region IEquatable<T> interface

        /// <summary>
        /// Implementation of the IEquatable interface.
        /// </summary>
        /// <param name="other">The instance of the LibraryBook being compared to this one.</param>
        /// <returns>Returns TRUE if they are equal otherwise FALSE.</returns>
        public bool Equals(LibraryBook other)
        {
            return AreEqual(this, other);
        }

        #endregion


        #region IEqualityComparer<T> interface
        /// <summary>
        /// Implementation of the IEqualityComparer(T) interface.  Check
        /// for equality between the two supplied instances of 
        /// LibraryBook.
        /// </summary>
        /// <param name="b1">The first instance of LibraryBook</param>
        /// <param name="b2">the second instance of LibraryBook</param>
        /// <returns>Return TRUE if they are equal otherwise FALSE.</returns>
        public bool Equals(LibraryBook b1, LibraryBook b2)
        {
            return AreEqual(b1, b2);
        }

        /// <summary>
        /// Implementation of IEqualityComparer(T) interface.  Generates
        /// a HashCode for the instance of LibraryBook, from the
        /// ISBN, Title and Author.
        /// ie. the information that makes a LibraryBook
        /// equal.
        /// </summary>
        /// <param name="obj">The instance of LibraryBook to get the HashCode for.</param>
        /// <returns>The generated HashCode</returns>
        public int GetHashCode(LibraryBook obj)
        {
            StringBuilder bldr = new StringBuilder(obj.ISBN);
            bldr.Append(obj.Title);
            bldr.Append(obj.Author);
            var hash = bldr.ToString().GetHashCode();
            return hash;
        }
        #endregion


        #region private methods
        /// <summary>
        /// Check the two instances of LibraryBook for 
        /// equality.  They are deemed equal if the 
        /// ISBN, Title and Author are the same.
        /// </summary>
        /// <param name="b1">The first instance of LibraryBook</param>
        /// <param name="b2">The second instance of LibraryBook</param>
        /// <returns>Return TRUE if they are equal otherwise FALSE.</returns>
        private bool AreEqual(LibraryBook b1, LibraryBook b2)
        {
            return b1.ISBN.Equals(b2.ISBN) &&
                b1.Title.Equals(b2.Title) &&
                b1.Author.Equals(b2.Author);
        }
        #endregion
    }
}
