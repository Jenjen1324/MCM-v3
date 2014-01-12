using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.News
{
    /// <summary>
    /// Fetches news from the mojang.com website
    /// </summary>
    public class MojangFeed : NewsFeed
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
