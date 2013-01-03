﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Model;
using Core.Factories;

namespace Infrastructure.Helpers
{
    /// <summary>
    /// Static class <c>DeserialiseData</c> is responsible for converting
    /// the CSV data file into a LibraryBook class instanc.
    /// </summary>
    public static class DeserialiseData
    {
        /// <summary>
        /// Static method <c>ToBook</c> accepts some csv data from the
        /// data file and deserialises it into an instance of a
        /// LibraryBook class.
        /// </summary>
        /// <param name="csvData">The commas separated data (1 line)</param>
        /// <param name="factory">concrete instance of the LibraryBookFactory class.</param>
        /// <returns>A populated instance of a LibraryBook class.</returns>
        public static LibraryBook ToBook(string csvData, LibraryBookFactory factory)
        {
            //  Create an instance of LibraryBook
            LibraryBook book = factory.Create();

            //  Split the data into individual items.
            string[] elements = csvData.Split(',');

            //  They are held in a strict order so reference by index.
            book.ISBN = elements[0].Trim();
            book.Title = elements[1].Trim();
            book.Author = elements[2].Trim();
            book.Synopsis = elements[4].Trim();
            
            //  Now split the keywords, they are space separated
            //  and held in element 3
            string[] words = elements[3].Trim().Split(' ');
            foreach (string kw in words)
            {
                book.KeyWords.Add(kw);
            }
            
            //  Return the book
            return book;
        }
    }
}
