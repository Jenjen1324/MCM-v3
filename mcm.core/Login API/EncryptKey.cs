using System;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics.Contracts;

namespace mcm.core
{
	/// <summary>
	/// Encryption Key class, used for encryption
	/// Contains serialization methods and generation methods
	/// </summary>
	public class EncryptKey
	{
		//data
		byte[] data;
		byte[] dataIV;

		/// <summary>
		/// Generates a new EncryptionKey
		/// </summary>
		/// <returns>
		/// The key.
		/// </returns>
		public static EncryptKey GenerateKey ()
		{
			//Create key
			EncryptKey key = new EncryptKey();

			//create encryption algoritm to make the keys
			using(RijndaelManaged rj = new RijndaelManaged())
			{
				//Generate key data
				key.data = rj.GenerateKey();
				key.dataIV = rj.GenerateIV();

				//dispose encryptor
			}
			//Return key
			return key;
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public byte[] Key {
			get { return data; }
		}

		/// <summary>
		/// Gets the Initialization Vector
		/// </summary>
		/// <value>
		/// The Initialization Vector
		/// </value>
		public byte[] IV {
			get { return dataIV; }
		}

		/// <summary>
		/// Load a serialized key
		/// </summary>
		/// <param name='Data'>
		/// The serialized key
		/// </param>
		public static EncryptKey Load (byte[] Data)
		{
			//Setup var contracts
			Contract.Requires(Data != null && Data.Length > 0, "Data array null or zero");

			//Init stream and main vars
			MemoryStream ms = new MemoryStream(Data);
			BinaryReader br = new BinaryReader();
			byte[] data;
			byte[] dataIV;

			//read data
			int dl = br.ReadInt32();
			data = br.ReadBytes(dl);
			dl = br.ReadInt32();
			dataIV = br.ReadBytes(dl);

			//close stream
			br.Close();

			//Create instance
			EncryptKey key = new EncryptKey();
			key.data = data;
			key.dataIV = dataIV;

			//return key
			return key;
		}

		/// <summary>
		/// Serialize the key
		/// </summary>
		public byte[] Serialize ()
		{
			//Create stream
			MemoryStream ms = new MemoryStream(data.Length + 2 + dataIV.Length);
			BinaryWriter bw = new BinaryWriter(ms);

			//Write data
			bw.Write(data.Length);
			bw.Write(data);
			bw.Write(dataIV.Length);
			bw.Write(dataIV);
			bw.Close();

			//Return data
			return ms.GetBuffer();
		}
	}
}