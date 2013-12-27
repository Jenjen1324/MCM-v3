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
        public Action<DownloadJob> FileFinished;

		public DownloadPackage(string Name,string Desciption) : base("",Name,Desciption)
		{
			jobs = new List<DownloadJob>();
		}

		/// <summary>
		/// Updates the progress.
		/// </summary>
		private void UpdateProgress ()
		{
			int size = jobs.Count;
			int finished = (from dlj in jobs select (dlj.Complete ? 1 : 0)).Sum ();
			int n = finished / size * 100;
			ProgressChanged (this, n);
			if(n == 100) DownloadComplete(this,null);
		}

        /// <summary>
        /// Downloads all the files in the package
        /// </summary>
        public override void StartDownload()
        {
            foreach(DownloadJob dl in jobs)
            {
				dl.DownloadComplete += delegate {
					FileFinished(dl);
					UpdateProgress();
				};
                dl.StartDownload();
            }
        }
    }
}
