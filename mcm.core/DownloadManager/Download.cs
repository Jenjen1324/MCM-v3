using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace MCM.Core.DownloadManager
{
    public class Download
    {
        /// <summary>
        /// Name of the package
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description of the package (Optional)
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// The URL where it will be downloaded from
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Wheter minecraft needs to wait until the download 
        /// </summary>
        public bool MCRequire { get; set; }

		/// <summary>
		/// Wheter the download has completet or not
		/// </summary>
        public bool complete { get; protected set; }

        /// <summary>
        /// The actual downloaded data
        /// </summary>
        public byte[] data { get; protected set; }

        /// <summary>
        /// Called when the download has finished downloading
        /// </summary>
		public Action<Download> Downloaded;

        /// <summary>
        /// Called when the downloadprogress has been changed
        /// </summary>
		public Action<int> ProgressUpdated = delegate { };

        /// <summary>
        /// Download the file
        /// </summary>
		public virtual void DoDownload()
		{
			WebClient wc = new WebClient ();
			try
			{
				wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wc_DownloadDataCompleted);
                wc.DownloadDataAsync(new Uri(url));
			}
			catch (WebException e) {
                throw e; // For now
			}
		}

        public void WaitForComplete()
        {
            while (!complete)
                Thread.Sleep(100);
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                this.data = e.Result;
                this.complete = true;
                Downloaded(this);
            }
            catch (Exception ex)
            {
                throw ex; // For now
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressUpdated(e.ProgressPercentage);
        }
    }
}
