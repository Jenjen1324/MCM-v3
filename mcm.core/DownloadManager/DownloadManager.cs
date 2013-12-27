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

		private static List<DownloadJob> downloading = new List<DownloadJob>();

        /// <summary>
        /// Schedules a download to be downloaded
        /// </summary>
        /// <param name="dl">The download to be downloaded</param>
		public static void ScheduleDownload(DownloadJob dl)
        {
			lock (downloads) {
				downloads.Push (dl);
				StartNextJob();
			}
        }

        /// <summary>
        /// Schedules a single file to be downloaded
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="url">The URL to download from</param>
        /// <param name="MCRequire">Wheter Minecraft requires this before starting</param>
		public static DownloadJob ScheduleDownload(string Name, string Url, string Description, Action<DownloadJob,byte[]> finished = null)
        {
			DownloadJob dl = new DownloadJob(Url,Name,Description);
			if(finished == null) dl.DownloadComplete += finished;
            ScheduleDownload(dl);
			return dl;
        }

		/// <summary>
		/// Downloads the next scheduled DownloadJob
		/// </summary>
		public static void StartNextJob ()
		{
			lock (downloading)
			lock (downloads) {
				DownloadJob next = downloads.Pop ();
				next.DownloadComplete += JobComplete;
				next.DownloadComplete += (d,r) => {
					lock (downloading) {
						downloading.Remove(d);
					}
				};
				WaitForAll += next.GetWaitForCompleteDelegate ();
				DownloadProgressChanged += next.ProgressChanged;
				downloading.Add(next);
				next.StartDownload ();
				DownloadStarted(next);
			}
		}

		public static Action WaitForAll = delegate { };

		public static Action<DownloadJob> DownloadStarted = delegate {};

		public static Action<DownloadJob,byte[]> JobComplete = delegate {};

		public static Action<DownloadJob,int> DownloadProgressChanged = delegate {};

        /// <summary>
        /// Downloads all the scheduled downloads
        /// </summary>
		[Obsolete("All downloads are downloaded when scheduled, this should never be needed")]
		private static void Download ()
		{
			lock (downloads) {
				while (downloads.Count > 0) {
					StartNextJob ();
				}
			}
		}
    }
}
