using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.Core.DownloadManager
{
    /// <summary>
    /// The Manager that starts the downloads
    /// </summary>
    public class DownloadManager
    {
        /// <summary>
        /// Schedules a single file to be downloaded
        /// </summary>
        /// <param name="dl">The download to be downloaded</param>
        public void ScheduleDownload(Download dl);

        /// <summary>
        /// Schedules the download of all downloads in the Package
        /// </summary>
        /// <param name="dl">The Package to be downloaded</param>
        public void ScheduleDownload(DownloadPackage dl);
    }
}
