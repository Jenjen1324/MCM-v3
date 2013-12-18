using MCM.MC.MinecraftLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.MC.MinecraftVersion
{
    /// <summary>
    /// A specific Minecraft Version
    /// </summary>
    public class MinecraftVersion
    {
        /// <summary>
        /// The format of the arguments
        /// </summary>
        public ProcessArguments arguments;

        /// <summary>
        /// Actual arguments
        /// </summary>
        public string minecraftArguments;

        /// <summary>
        /// The version of the launcher that is required
        /// </summary>
        public int minimumLauncherVersion;

        /// <summary>
        /// The main class to launch minecraft
        /// </summary>
        public string mainClass;

        /// <summary>
        /// Libraries required for minecraft
        /// </summary>
        public List<Library> libraries;
    }
}
