using System;

namespace mcm.core
{
	/// <summary>
	/// Class for storing Login info for minecraft and mcm profiles.
	/// </summary>
	public class LoginInfo
	{
		private string name;
		private EncryptedPassword password;

		public LoginInfo (string Name, EncryptedPassword Password)
		{
			this.name = Name;
			this.password = Password;
		}

		/// <summary>
		/// Gets the username.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name {
			get { return name; }
		}

		/// <summary>
		/// Gets the encrypted password.
		/// </summary>
		/// <value>
		/// The encrypted password.
		/// </value>
		public EncryptedPassword Password {
			get { return password; }
		}
	}
}

