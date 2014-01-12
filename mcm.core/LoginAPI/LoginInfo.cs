using System;
using System.IO;

namespace MCM.Core.LoginAPI
{
	/// <summary>
	/// Class for storing Login info for minecraft and mcm profiles.
	/// </summary>
	public class LoginInfo
	{ 
		private string name;
		private EncryptedPassword password;

		private LoginInfo ()
		{
		}

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

		public byte[] Serialize ()
		{
			using (MemoryStream ms = new MemoryStream())
			using (BinaryWriter bw = new BinaryWriter(ms)) {
				bw.Write (name);
				bw.Write(password.EncryptedPasswordData.Length);
				bw.Write (password.EncryptedPasswordData);
				bw.Flush ();
				bw.Close ();
				return ms.GetBuffer ();
			}
		}

		public static LoginInfo Load (byte[] data, EncryptKey Key)
		{
			using (MemoryStream ms = new MemoryStream(data))
			using (BinaryReader br = new BinaryReader(ms)) {
				LoginInfo li = new LoginInfo();
				li.name = br.ReadString();
				int length = br.ReadInt32();
				li.password = new EncryptedPassword(br.ReadBytes(length),Key);
				return li;
			}
		}
	}
}

