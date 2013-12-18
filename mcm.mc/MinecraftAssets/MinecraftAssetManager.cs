using MCM.Core.DownloadManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.MC.MinecraftAssets
{
    /// <summary>
    /// Manages the assets
    /// Downloads them at start, etc.
    /// </summary>
    public class MinecraftAssetManager
    {
        /// <summary>
        /// The List of all Assets
        /// </summary>
        public static List<MinecraftAsset> assets;

        /// <summary>
        /// Downloads and Parses the List of assets
        /// </summary>
        public static void LoadAssets()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Downloads required Assets
        /// </summary>
        internal static void ScheduleAssetDownloads()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets called when an Asset has finished downloading
        /// </summary>
        /// <param name="dl">The download that has finished</param>
        private static void AssetDownloaded(Download dl)
        {
            throw new NotImplementedException();
        }
    }
}
