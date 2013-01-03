using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using THLibrary.Common;

namespace THLibrary.DataModel
{
    public class SearchTypesViewModel: BindableBase, IEquatable<SearchTypesViewModel>
    {
        private string _type = string.Empty;
        private ObservableCollection<string> _values = new ObservableCollection<string>();

        public string Type
        {
            get { return this._type; }
            set { this.SetProperty(ref this._type, value); }
        }

        public ObservableCollection<string> Values
        {
            get { return this._values; }
            set { this.SetProperty(ref this._values, value); }
        }

        #region IEquatable interface

        /// <summary>
        /// IEquatable Interface.
        /// Deem instances equal if the Type is the same, ignoring the values.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if they are the same, otherwise False</returns>
        public bool Equals(SearchTypesViewModel other)
        {
            var result = this.Type.Equals(other.Type);
            return result;
        }

        #endregion

    }
}
