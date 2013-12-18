using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.MC.MinecraftSession
{
    /// <summary>
    /// Session info for the Minecraft login
    /// Uses the Session system Yggdrasil
    /// </summary>
    public class SessionInfoYggdrasil : SessionInfo
    {
        /// <summary>
        /// Access token for Minecraft
        /// </summary>
        public string accessToken { get; private set; }

        /// <summary>
        /// Client token for Minecraft
        /// </summary>
        public string clientToken { get; private set; }

        /// <summary>
        /// Creates a new SessionInfo and fetches the sessionId
        /// </summary>
        /// <param name="username">The username (or email) that is used to login</param>
        /// <param name="password">The password that is used to log in</param>
        /// <returns>Session info for Minecraft</returns>
		public static SessionInfoYggdrasil Connect(string username, string uassword) 
		{
			throw new NotImplementedException ();
		}
    }
}
