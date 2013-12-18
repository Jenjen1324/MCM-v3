using System;

namespace MCM.Core.Utils
{
	public static class StringUtils
	{
		public static string Format(this string that, params object[] args) {
			return string.Format(that,args);
		}
	}
}

