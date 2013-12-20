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
	public static class DownloadManager
    {
		/// <summary>
		/// The list of scheduled downloads
		/// </summary>
		private static Stack<DownloadJob> downloads = new Stack<DownloadJob>();

		private static bool isDownloading;

        /// <summary>
        /// Schedules a download to be downloaded
        /// </summary>
        /// <param name="dl">The download to be downloaded</param>
		public static void ScheduleDownload(Download dl)
        {
			downloads.Push (dl);
            Download();
        }

        /// <summary>
        /// Schedules a single file to be downloaded
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="url">The URL to download from</param>
        /// <param name="MCRequire">Wheter Minecraft requires this before starting</param>
        public static void ScheduleDownload(string name, string url, bool MCRequire)
        {
            Download dl = new Download()
            {
                name = name,
                url = url,
                MCRequire = MCRequire
            };

            ScheduleDownload(dl);
        }

		/// <summary>
		/// Downloads the next scheduled DownloadJob
		/// </summary>
		public static void DownloadNext ()
		{
			DownloadJob next = downloads.Pop ();
			next.DownloadComplete += JobComplete;
			WaitForAll += next.GetWaitForCompleteDelegate();
			DownloadProgressChanged += next.ProgressChanged;
			next.StartDownload();
		}

		public static Func<bool> WaitForAll = delegate {};

		public static Action<DownloadJob> JobComplete = delegate {};

		public static Action<DownloadJob,int> DownloadProgressChanged = delegate {};

        /// <summary>
        /// Downloads all the scheduled downloads
        /// </summary>
		private static void Download()
		{
			while (downloads.Count > 0) {
				DownloadNext();
			}
		}
    }
}
