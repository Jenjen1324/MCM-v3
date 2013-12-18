using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MCM.Core.Settings
{
	/// <summary>
	/// Contains all settings
	/// </summary>
	public class Settings : List<SettingGroup> 
	{
		public Settings () : base()
		{
		}

		/// <summary>
		/// Gets the group with the specified GroupName.
		/// </summary>
		/// <returns>
		/// The group.
		/// </returns>
		/// <param name='name'>
		/// The Group Name.
		/// </param>
		public SettingGroup GetGroup(string name)
		{
			return this.Find((i) => i.Name == name);
		}

		/// <summary>
		/// Creates a group and adds it to the list.
		/// </summary>
		/// <returns>
		/// The group.
		/// </returns>
		/// <param name='Name'>
		/// Name of the group.
		/// </param>
		public SettingGroup CreateGroup (string Name)
		{
			//if the group exists, return it
			if (this.Exists ((i) => i.Name == Name)) {
				return this [Name];
			} 
			//Else create one and add it
			else {
				//Create group
				SettingGroup s = new SettingGroup();
				s.Name = Name;
				this.Add(s);
				return s;
			}
		}

		/// <summary>
		/// Removes the group.
		/// </summary>
		/// <param name='Name'>
		/// Name of the group to remove.
		/// </param>
		public void RemoveGroup (string Name)
		{
			this.RemoveAll((ii) => ii.Name == Name);
		}

		/// <summary>
		/// Gets the <see cref="MCM.Core.Settings.SettingGroup"/> with the specified GroupName.
		/// </summary>
		/// <param name='GroupName'>
		/// Group name.
		/// </param>
		public SettingGroup this[string GroupName]
		{
			get
			{
				return GetGroup(GroupName);
			}
		}

		/// <summary>
		/// Generates the XML.
		/// </summary>
		/// <param name='xmlBuilder'>
		/// Xml builder.
		/// </param>
		public void GenerateXML (StringBuilder xmlBuilder)
		{
			xmlBuilder.AppendLine("<settings>");
			this.ForEach((g) => g.GenerateXML(xmlBuilder));
			xmlBuilder.AppendLine("</settings>");
		}

		/// <summary>
		/// Loads from XML.
		/// </summary>
		/// <returns>
		/// The loaded settings.
		/// </returns>
		/// <param name='xmlReader'>
		/// Xml reader with the xml.
		/// </param>
		public static Settings LoadFromXML(XmlReader xmlReader) {
			//Create new settings
			Settings settings = new Settings();

			//Create tmp variables
			SettingGroup currentgroup = null;
			Setting currentsetting = null;

			//Start read loop
			while (xmlReader.Read()) {
				if (xmlReader.Name == "group" && xmlReader.NodeType == XmlNodeType.Element) {
					string gname = xmlReader.GetAttribute("name") as string;
					currentgroup = settings.CreateGroup(gname);
				}
				else if (xmlReader.Name == "setting" && xmlReader.NodeType == XmlNodeType.Element) {
					string sname = xmlReader.GetAttribute("key") as string;
					currentsetting = currentgroup.CreateSetting(sname);
					string type = xmlReader.GetAttribute("type") as string;
					xmlReader.Read();
					object value = xmlReader.ReadContentAs(Type.GetType(type), null);
					currentsetting.Value = value;
				}
			}

			//return the settings
			return settings;
		}
	}
}

