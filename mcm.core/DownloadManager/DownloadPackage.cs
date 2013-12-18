using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.Core.DownloadManager
{
    /// <summary>
    /// A Package that contains multiple downloads but are for one cause
    /// </summary>
    public class DownloadPackage : Download
    {
        /// <summary>
        /// The Downloads to be downloaded
        /// </summary>
        private List<Download> files;

        /// <summary>
        /// Calls when a file has finished downloading
        /// </summary>
        public Action<Download> finishedFile;

        /// <summary>
        /// Downloads all the files in the package
        /// </summary>
        public override void DoDownload()
        {
            foreach(Download dl in files)
            {
                dl.DoDownload();
                dl.WaitForComplete();
            }

            this.complete = true;
            this.Downloaded(this);
        }
    }
}
