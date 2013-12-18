using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.MC.MinecraftSession
{
    /// <summary>
    /// Session info for the Minecraft login
    /// </summary>
    public class SessionInfo
    {
        /// <summary>
        /// The username (the display name) to log-into Minecraft
        /// </summary>
        public string username;

        /// <summary>
        /// The sessionID for multiplayer
        /// </summary>
        public string sessionId;

        /// <summary>
        /// Creates a new SessionInfo and fetches the sessionId
        /// </summary>
        /// <param name="username">The username (or email) that is used to login</param>
        /// <param name="password">The password that is used to log in</param>
        /// <returns>Session info for Minecraft</returns>
		public static SessionInfo Connect(string username, string password) {}
    }
}
