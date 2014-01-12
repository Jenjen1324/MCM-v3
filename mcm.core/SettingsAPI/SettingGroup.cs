using System;
using System.Collections.Generic;
using System.Text;
using MCM.Core.Utils;

namespace MCM.Core.SettingsAPI
{
	/// <summary>
	/// Setting group.
	/// </summary>
	public class SettingGroup :	List<Setting>
	{
		string name;

		public SettingGroup (string Name) : base()
		{
			this.name = Name;
		}

		/// <summary>
		/// Creates a setting or returns and existing one
		/// </summary>
		/// <returns>
		/// The setting.
		/// </returns>
		/// <param name='Key'>
		/// Key.
		/// </param>
		public Setting CreateSetting (string Key)
		{
			//if key exists, return it
			if (this.Exists ((i) => i.Key == Key)) {
				return this [Key];
			} 
			//Else create one and add it
			else {
				//Create setting
				Setting s = new Setting(Key);
				//default value null
				s.Value = null;
				this.Add(s);
				Logger.Write("Created setting {0} in group {1}".format(Key,this.name));
				return s;
			}
		}

		/// <summary>
		/// Gets the <see cref="MCM.Core.Settings.Setting"/> with the specified Key.
		/// </summary>
		/// <param name='Key'>
		/// The Key to check.
		/// </param>
		public Setting this [string Key] {
			get 
			{
				return this.Find((i) => i.Key == Key);
			}
		}

		/// <summary>
		/// Gets the name of the Group
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name {
			get { return name; }
		}

		/// <summary>
		/// Serializes to xml.
		/// </summary>
		/// <param name='xmlBuilder'>
		/// Xml builder.
		/// </param>
		public void GenerateXML (StringBuilder xmlBuilder)
		{
			xmlBuilder.AppendLine("<group name=\"{0}\">".format(name));
			this.ForEach((s) => s.GenerateXML(xmlBuilder));
			xmlBuilder.AppendLine("</group>");
		}
	}
}

