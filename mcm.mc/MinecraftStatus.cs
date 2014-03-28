using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MC
{
    /// <summary>
    /// Checks wheter the minecraft services are down
    /// </summary>
    public class MinecraftStatus
    {
        /// <summary>
        /// Status of the login server
        /// </summary>
        public bool login;

        /// <summary>
        /// Status of the multiplayer server
        /// </summary>
        public bool multiplayer;

        /// <summary>
        /// Refreshes the status
        /// </summary>
        public void refreshStatus()
        {
            throw new NotImplementedException();
        }
    }
}
