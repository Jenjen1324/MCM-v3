using MCM.Core.DownloadManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using MCM.Core.Utils;
using MCM.MC.Version;

namespace MCM.MC.MinecraftAssets
{
    /// <summary>
    /// An asset for minecraft
    /// (Sounds, Textures, etc.)
    /// </summary>
    public class MinecraftAsset
    {
        /// <summary>
        /// The name of the Asset also the path
        /// </summary>
        public string key;

        /// <summary>
        /// The md5 hash value of the Asset
        /// Used to validate it
        /// </summary>
        public string md5;

		/// <summary>
		/// The size of the asset file in bytes
		/// </summary>
		public int size;

		/// <summary>
		/// The version the asset belongs to
		/// </summary>
		public MinecraftVersion version;

        /// <summary>
        /// The URL where the Asset is downloaded from
        /// </summary>
        public string url;

		public string AssetPath {
			get {
				string AssetPath = MinecraftAssetManager.GetPath(version);
				return Path.Combine(AssetPath,key);
			}
		}

        /// <summary>
        /// Stores the Downloaded data to the Asset
        /// </summary>
        /// <param name="d">The Download that the Asset was downloaded with</param>
        public void StoreAsset(DownloadJob d)
        {
			byte[] assetData = d.Data;
			string downloadedHash = Hasher.GenerateMD5(assetData);
			if (downloadedHash == md5) {

			}
        }

        /// <summary>
        /// Checks wether the Asset has already been downloaded or not
        /// </summary>
        /// <returns>True when it requires to be downloaded</returns>
        public bool NeedsDownload {
			get {
				if (!Directory.Exists(AssetPath))
				{
					if (!File.Exists(AssetPath))
						return true;
					else if (Hasher.GenerateMD5(File.ReadAllBytes(AssetPath)).ToLower() != this.md5.ToLower())
						return true;
					else
						return false; 
				}
				else
				{
					return false;
				}
			}
        }
    }
}
