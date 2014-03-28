using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using MCM.Core.Utils;

namespace MCM.Core.DownloadManager
{
	/// <summary>
	/// New and improved Download Job
	/// </summary>
	public class DownloadJob
	{
		/// <summary>
		/// Gets the name of the Job.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; protected set; }

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public string Url { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="MCM.Core.DownloadManager.DownloadJob"/> required for minecraft.
		/// </summary>
		/// <value>
		/// <c>true</c> if required for minecraft; otherwise, <c>false</c>.
		/// </value>
		public bool RequiredForMinecraft { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MCM.Core.DownloadManager.DownloadJob"/> is complete.
		/// </summary>
		/// <value>
		/// <c>true</c> if complete; otherwise, <c>false</c>.
		/// </value>
		public bool Complete { get; protected set; }

		/// <summary>
		/// Gets the downloaded data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public byte[] Data { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MCM.Core.DownloadManager.DownloadJob"/> class.
		/// </summary>
		/// <param name='Url'>
		/// URL.
		/// </param>
		/// <param name='Name'>
		/// Name.
		/// </param>
		/// <param name='Description'>
		/// Description.
		/// </param>
		public DownloadJob (string Url, string Name, string Description)
		{
			this.Url = Url;
			this.Name = Name;
			this.Description = Description;

			this.RequiredForMinecraft = false;
			this.Complete = false;
			this.Data = null;

			DownloadComplete = delegate {};

			ProgressChanged = delegate {};
		}

		/// <summary>
		/// Fires when download complete.
		/// </summary>
		public Action<DownloadJob,byte[]> DownloadComplete;

		/// <summary>
		/// Fires when progress changes.
		/// </summary>
		public Action<DownloadJob, int> ProgressChanged;

		/// <summary>
		/// Gets a value indicating whether this <see cref="MCM.Core.DownloadManager.DownloadJob"/> was successful.
		/// </summary>
		/// <value>
		/// <c>true</c> if success; otherwise, <c>false</c>.
		/// </value>
		public bool Success { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="MCM.Core.DownloadManager.DownloadJob"/> is working.
		/// </summary>
		/// <value>
		/// <c>true</c> if working; otherwise, <c>false</c>.
		/// </value>
		public bool Working { get; protected set; }

		/// <summary>
		/// Gets the error generated if Success is false.
		/// </summary>
		/// <value>
		/// The error.
		/// </value>
		public WebException Error { get; protected set; }

		/// <summary>
		/// Start the <see cref="MCM.Core.DownloadManager.DownloadJob"/>
		/// </summary>
		public virtual void StartDownload ()
		{
			WebClient webClient = new WebClient ();
			webClient.DownloadProgressChanged += (sender, e) => {
				this.Working = true;
				ProgressChanged(this, e.ProgressPercentage);
			};
			webClient.DownloadDataCompleted += (sender, e) => {
				if(e.Error != null)
				{
					Logger.Write("Could not download: {0} ({1})\n{2}".format(this.Name,this.Description,e.Error.ToString()));
				}
				if(e.Result != null) {
					this.Working = false;
					this.Success = true;
					this.Complete = true;
					this.Data = e.Result;
					DownloadComplete(this,this.Data);
					Logger.Write("Download Complete: {0} ({1})".format(this.Name,this.Description));
				}
			};
			try {
				webClient.DownloadDataAsync (new Uri (this.Url));
			} catch (WebException ex) {
				this.Success = false;
				this.Complete = true;
				this.Error = new WebException("Could not download file: " + Url, ex);
				Logger.Write(this.Error.ToString());
			}
		}

		/// <summary>
		/// Returns a delegate to wait for this download to complete
		/// </summary>
		/// <returns>
		/// The minecraft.
		/// </returns>
		public Action GetWaitForCompleteDelegate ()
		{
			this.RequiredForMinecraft = true;
			return WaitForComplete;
		}

		/// <summary>
		/// Waits for the download to complete.
		/// </summary>
		public void WaitForComplete ()
		{
			while (!Complete) {
				Thread.Sleep(100);
			}
		}
	}

	/*
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
    */
}
