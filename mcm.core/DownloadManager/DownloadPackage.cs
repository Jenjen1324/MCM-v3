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
    public class DownloadPackage : DownloadJob
    {
        /// <summary>
        /// The Downloads to be downloaded
        /// </summary>
        private List<DownloadJob> jobs;

        /// <summary>
        /// Calls when a file has finished downloading
        /// </summary>
        public Action<Download> FileFinished;

        /// <summary>
        /// Downloads all the files in the package
        /// </summary>
        public override void DoDownload()
        {
            foreach(DownloadJob dl in jobs)
            {
				dl.DownloadComplete += delegate {
					FileFinished(dl);
				};
                dl.StartDownload();
            }

            this.Complete = true;
            this.DownloadComplete(this);
        }
    }
}
