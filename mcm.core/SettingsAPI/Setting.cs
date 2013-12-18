using System;
using System.Text;
using MCM.Core.Utils;

namespace MCM.Core.Settings
{
	/// <summary>
	/// Setting.
	/// </summary>
	public class Setting
	{
		string key;
		object value;

		/// <summary>
		/// Initializes a new instance of the <see cref="MCM.Core.Settings.Setting"/> class.
		/// </summary>
		/// <param name='Key'>
		/// Key of the setting
		/// </param>
		public Setting (string Key)
		{
			this.key = Key;
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public string Key {
			get { return key; }
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public object Value {
			get { return value; }
			set { this.value = value; }
		}

		/// <summary>
		/// Serializes to xml.
		/// </summary>
		/// <param name='xmlBuilder'>
		/// Xml builder.
		/// </param>
		public void GenerateXML(StringBuilder xmlBuilder)
		{
			xmlBuilder.AppendLine("<setting name=\"{0}\" type=\"{2}\">{1}</setting>".Format(this.key,this.value.ToString(),this.value.GetType().FullName));
		}

	}
}

