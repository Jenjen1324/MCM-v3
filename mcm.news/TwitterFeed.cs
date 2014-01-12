using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.News
{
    /// <summary>
    /// Fetches the Twitter feed from 
    /// </summary>
    class TwitterFeed : NewsFeed
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
