using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MC.MinecraftLibraries
{
    public class Library
    {
        /// <summary>
        /// The name of the library
        /// </summary>
        public string name;

        /// <summary>
        /// REQUIRES SUMMARY
        /// </summary>
        public bool isNative;

        /// <summary>
        /// REQUIRES SUMMARY
        /// </summary>
        public List<string> extractExclusions;

        /// <summary>
        /// The URL where the library can be downloaded from
        /// </summary>
        public string url;

        /// <summary>
        /// The path where the library will be extracted to 
        /// </summary>
        public string extractPath;

        /// <summary>
        /// Schedules the download of the library
        /// </summary>
        public void ScheduleExtract()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Schedules the download of the library with a downloadpackage
        /// </summary>
        /// <param name="dl">The Downloadpackage where the library will be added</param>
        public void ScheduleExtract(DownloadPackage dl)
        {
            throw new NotImplementedException();
        }
    }
}
