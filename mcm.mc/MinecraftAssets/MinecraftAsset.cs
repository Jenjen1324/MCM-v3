using MCM.Core.DownloadManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.MC.MinecraftAssets
{
    /// <summary>
    /// An asset for minecraft
    /// (Sounds, Textures, etc.)
    /// </summary>
    public class MinecraftAsset
    {
        /// <summary>
        /// The name of the Asset
        /// </summary>
        public string key;

        /// <summary>
        /// The md5 hash value of the Asset
        /// Used to validate it
        /// </summary>
        public string md5;

        /// <summary>
        /// Checks wether it is a directory or not
        /// </summary>
        public bool isDirectory;

        /// <summary>
        /// The path where the Asset will be saved
        /// </summary>
        public string path;

        /// <summary>
        /// The URL where the Asset is downloaded from
        /// </summary>
        public string url;

        /// <summary>
        /// Stores the Downloaded data to the Asset
        /// </summary>
        /// <param name="d">The Download where the Asset was downloaded with</param>
        public void StoreAsset(Download d)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks wether the Asset has already been downloaded or not
        /// </summary>
        /// <returns>True when it requires to be downloaded</returns>
        public bool NeedsDownload()
        {
            throw new NotImplementedException();
        }
    }
}
