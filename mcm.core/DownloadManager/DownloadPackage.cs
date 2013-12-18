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
        /// The Downloads the be downloaded
        /// </summary>
        public List<Download> files;

        /// <summary>
        /// Downloads
        /// </summary>
        public void Download();
    }
}
