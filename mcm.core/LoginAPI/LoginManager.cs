using System;
using System.Collections.Generic;
using System.Linq;

using MCM.Core.LoginAPI;
using MCM.Core.Utils;
using System.IO;
using System.Diagnostics.Contracts;

namespace MCM.Core.LoginAPI
{
	public static class LoginManager
	{
		/// <summary>
		/// A list containing the current registered logins in the system
		/// </summary>
		static List<LoginInfo> logins;

		/// <summary>
		/// The private encryption key used to encrypt the passwords
		/// </summary>
		static EncryptKey key;

		/// <summary>
		/// Initializes the variables in the class.
		/// </summary>
		static LoginManager ()
		{
			logins = new List<LoginInfo>();
			key = EncryptKey.GenerateKey();
		}

		/// <summary>
		/// Gets the login list.
		/// </summary>
		/// <value>
		/// The login list.
		/// </value>
		public static List<LoginInfo> LoginList {
			get { return logins; }
		}

		/// <summary>
		/// Create a new login info
		/// </summary>
		/// <returns>
		/// The LoginInfo
		/// </returns>
		/// <param name='Username'>
		/// Username.
		/// </param>
		/// <param name='Password'>
		/// Password.
		/// </param>
		public static LoginInfo CreateLogin (string Username, string Password)
		{
			EncryptedPassword ePass = EncryptedPassword.Encrypt(Password, key);
			LoginInfo loginInfo = new LoginInfo(Username, ePass);
			Logger.Write("Created new login {0}".format(Username));
			return loginInfo;
		}

		/// <summary>
		/// Register the specified Login to the list.
		/// </summary>
		/// <param name='Login'>
		/// Login.
		/// </param>
		public static void Register (LoginInfo Login)
		{
			if(logins.Any((li) => li.Name != Login.Name)) {
				Logger.Write("Given logininfo: {0} allready in list, ignoring".format(Login.Name));
			}
			logins.Add(Login);
		}

		/// <summary>
		/// Gets the login info with the specified name
		/// </summary>
		/// <returns>
		/// The login info.
		/// </returns>
		/// <param name='Username'>
		/// Username.
		/// </param>
		public static LoginInfo GetLoginInfo (string Username)
		{
			var login = logins.Find((li) => li.Name == Username);
			if(login != null) {
				return login;
			}
			else 
			{
				string error = "LoginInfo with name {0}, not found in registered logins".format(Username);
				Logger.Write(error);
				throw new KeyNotFoundException(error);
			}
		}

		/// <summary>
		/// Checks to see if the list contains a login with the given name
		/// </summary>
		/// <returns>
		/// True if the list contains the login
		/// </returns>
		/// <param name='Username'>
		/// The username to check
		/// </param>
		public static bool ContainsLogin (string Username)
		{
			return logins.Any ((li) => li.Name == Username);
		}

		/// <summary>
		/// Removes the login info with the given name.
		/// </summary>
		/// <param name='Username'>
		/// Username.
		/// </param>
		public static void RemoveLoginInfo (string Username)
		{
			if (ContainsLogin (Username)) {
				logins.RemoveAll ((li) => li.Name == Username);
				Logger.Write ("Login {0} removed from registered logins".format (Username));
			} else {
				string error = "Could not remove {0} from registered logins, not found".format(Username);
				Logger.Write(error);
				throw new KeyNotFoundException(error);
			}
		}

		/// <summary>
		/// Loads login info objects from a folder.
		/// </summary>
		/// <param name='Path'>
		/// Path.
		/// </param>
		public static void LoadFromFolder (string Path)
		{
			Contract.Requires(Directory.Exists(Path));
			//Load key
			string[] KeyFiles = Directory.GetFiles (Path,"*.key");
			if (KeyFiles.Length == 1) {
				key = EncryptKey.Load(File.ReadAllBytes(KeyFiles[0]));
			}

			//List files
			var LoginFiles = Directory.GetFiles(Path,"*.login");

			//Clear list
			logins.Clear();

			//Load each file
			foreach (var file in LoginFiles) {
				logins.Add(LoginInfo.Load(File.ReadAllBytes(file),key));
			}
		}

		/// <summary>
		/// Saves login info objects to a folder.
		/// </summary>
		/// <param name='Path'>
		/// Path.
		/// </param>
		public static void SaveToFolder(string Path) {
			Contract.Requires(Directory.Exists(Path));
			Directory.GetFiles(Path,"*.login").ToList().ForEach((f) => File.Delete(f));

			File.WriteAllBytes(System.IO.Path.Combine(Path,"encrypt.key"),key.Serialize());

			foreach (var login in logins) {
				File.WriteAllBytes(System.IO.Path.Combine(Path,login.Name + ".login"),login.Serialize());
			}
		}
	}
}

