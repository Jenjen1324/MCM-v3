using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.News
{
    /// <summary>
    /// Fetches the Minecraft Launcher news
    /// </summary>
    class MinecraftNews : NewsFeed
    {
		#region NewsFeed implementation
		public List<FeedItem> Items {
			get {
				throw new System.NotImplementedException ();
			}
		}
		#endregion
    }
}
