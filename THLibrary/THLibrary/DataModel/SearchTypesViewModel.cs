//***************************************************************************************************
//Name of File:     SearchTypesViewModel.cs
//Description:      Defines the SearchTypes and the associated values.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using System.Collections.ObjectModel;

using THLibrary.Common;

namespace THLibrary.DataModel
{
    /// <summary>
    /// Class <c>SearchTypesViewModel</c> defines the SearchTypes, from the 
    /// <see cref="Core.Model.SearchTypeEnum"/> enumeration.  
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each SearchType contains the searchble values associated with the SearchType.
    /// </para>
    /// <para>
    /// The class implements the IEquatable(T) interface so that instances are deemed
    /// equal based on the Type and not including the values.  This functionality is
    /// required for the selection of Types from the SeachType combobox in the UI.
    /// </para>
    /// </remarks>
    public class SearchTypesViewModel: BindableBase, IEquatable<SearchTypesViewModel>
    {
        private string _type = string.Empty;
        private ObservableCollection<string> _values = new ObservableCollection<string>();

        /// <summary>
        /// Gets or sets the SearchType represented as a string.
        /// </summary>
        public string Type
        {
            get { return this._type; }
            set { this.SetProperty(ref this._type, value); }
        }

        /// <summary>
        /// Gets or sets the ObservableCollection of values associated with the SearchType.
        /// </summary>
        public ObservableCollection<string> Values
        {
            get { return this._values; }
            set { this.SetProperty(ref this._values, value); }
        }

        #region IEquatable interface

        /// <summary>
        /// IEquatable Interface.
        /// Instances are deemed equal if the Type is the same, ignoring the values.
        /// </summary>
        /// <param name="other">The instance being compared to this instance.</param>
        /// <returns>True if they are the same, otherwise False</returns>
        public bool Equals(SearchTypesViewModel other)
        {
            var result = this.Type.Equals(other.Type);
            return result;
        }

        #endregion

    }
}
