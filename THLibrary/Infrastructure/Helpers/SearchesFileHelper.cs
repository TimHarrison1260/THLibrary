//***************************************************************************************************
//Name of File:     SearchesFileHelper.cs
//Description:      Gets the file for the Search Criteria
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System.Threading.Tasks;       //  Async stuff.
using Windows.Storage;              //  Storage stuff.

namespace Infrastructure.Helpers
{
    /// <summary>
    /// Static class <c>SearchesFileHelper</c> gets the XML serialised
    /// file that is located within the local folder. 
    /// </summary>
    public static class SearchesFileHelper
    {
        private const string _dataFileName = "SearchesData.xml";

        /// <summary>
        /// GetDataFile gets the datafile 'SearchesData.xml' from the 
        /// local folder 
        /// </summary>
        /// <returns>Returns a Windows.Storage.StorageFile instance.</returns>
        /// <remarks>
        /// It is an asynchronous static method.
        /// </remarks>
        public static async Task<StorageFile> GetDataFile()
        {
            StorageFile dataFile = null;
            var localFolder = FileIOHelper.GetLocalFolder();
            dataFile = await FileIOHelper.GetFile(_dataFileName, localFolder);
            return dataFile;
        }

        /// <summary>
        /// CreateDataFile creates the datafile 'SearchesData.xml' in the
        /// local folder and returns the instance.
        /// </summary>
        /// <returns>returns a Windows.Storage.StorageFile instance.</returns>
        public static async Task<StorageFile> CreateDataFile()
        {
            StorageFile dataFile = null;
            var localFolder = FileIOHelper.GetLocalFolder();
            dataFile = await FileIOHelper.CreateFile(_dataFileName, localFolder);
            return dataFile;
        }
    }
}
