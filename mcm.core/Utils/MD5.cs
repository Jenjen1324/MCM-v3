using System;
using System.Security.Cryptography;
using System.Text;

namespace MCM.Core.Utils
{
	public static class Hasher
	{
		public static string GenerateMD5(byte[] input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] hash = md5Hash.ComputeHash(input);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
	}
}

