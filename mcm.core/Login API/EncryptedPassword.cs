using System;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics.Contracts;

namespace mcm.core
{
	/// <summary>
	/// Encrypted password.
	/// </summary>
	public class EncryptedPassword
	{
		private byte[] passwordData;
		private EncryptKey key;

		public EncryptedPassword ()
		{
		}

		/// <summary>
		/// Encrypt the specified Password with the supplied Key
		/// </summary>
		/// <param name='Password'>
		/// Password to encrypt
		/// </param>
		/// <param name='Key'>
		/// Key for encryption
		/// </param>
		public static EncryptedPassword Encrypt (string Password, EncryptKey Key)
		{
			//Setup contracts for arguments
			Contract.Requires(!string.IsNullOrWhiteSpace(Password), "No password supplied!");
			Contract.Requires(Key != null, "No key supplied!");

			//Create instance and assign key
			EncryptedPassword EncryptedPassword = new EncryptedPassword ();
			EncryptedPassword.key = Key;

			//Create encryption algoritm
			using (RijndaelManaged AES = new RijndaelManaged())
			{
				//Assign aes keys from the supplied key
				AES.Key = Key.Key;
				AES.IV = Key.IV;

				//Create encryption streams
				var encryptor = AES.CreateEncryptor();
				using (MemoryStream ms = new MemoryStream()) {
					using (CryptoStream cryptStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
						using (StreamWriter cryptWriter = new StreamWriter(cryptStream)) {
							//Write password for encryption
							cryptWriter.Write(Password);
						}
						//Store encrypted password
						EncryptedPassword.passwordData = ms.ToArray();
					}
				}
			}

			//Return instance
			return EncryptedPassword;
		}

		/// <summary>
		/// Decrypts the password and returns as plaintext
		/// </summary>
		public string Decrypt ()
		{
			//Setup vars
			string decrypted = null;

			//Create AES
			using (RijndaelManaged AES = new RijndaelManaged())
			{
				AES.Key = this.key.Key;
				AES.IV = this.key.IV;

				//Create crypto streams
				using (MemoryStream ms = new MemoryStream(this.passwordData))
				using (CryptoStream cryptoStream = new CryptoStream(ms, Aes, CryptoStreamMode.Read))
				using (StreamReader cryptoReader = new StreamReader(cryptoStream)) {

					//Read decrypted data
					decrypted = cryptoReader.ReadToEnd();
				}
			}

			//Return decrypted data
			return decrypted;
		}
	}
}

