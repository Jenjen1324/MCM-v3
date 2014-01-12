using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCM.Core.Utils;

namespace MCM.MC.Version
{
    /// <summary>
    /// A Minecraft version with all the information needed to display it visually
    /// </summary>
    public class TinyMinecraftVersion
    {
        /// <summary>
        /// The version name
        /// </summary>
        public string key;

        /// <summary>
        /// The release type
        /// (alpha,beta,...)
        /// </summary>
        public ReleaseType type;

        /// <summary>
        /// The URL where the json file with all the information can be downloaded
        /// </summary>
        public string jsonUrl;

        /// <summary>
        /// The URL where the minecraft jar file can be downloaded
        /// </summary>
        public string jarUrl;

        /// <summary>
        /// The base URL (required for the other URL's)
        /// </summary>
        public string baseUrl;

        /// <summary>
        /// The local path where the json file can be found
        /// </summary>
        public string jsonPath;

        /// <summary>
        /// The local path where the version is stored
        /// </summary>
        public string localPath;

        /// <summary>
        /// The local path where the minecraft jar file is stored
        /// </summary>
        public string jarPath;

        /// <summary>
        /// Return the full version of the TinyMincraftVersion
        /// </summary>
        public MinecraftVersion FullVersion;

        /// <summary>
        /// A description of the version for (error)messages
        /// </summary>
        /// <returns>A short description about the version</returns>
        public override string ToString()
        {
			return "{0} {1}".format(key,type.ToString());
        }
    }
}
