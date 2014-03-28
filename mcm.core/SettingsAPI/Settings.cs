using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MCM.Core.Utils;

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
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Creating group: {0}".format(Name));
			//if the group exists, return it
			if (this.Exists ((i) => i.Name == Name)) {
				sb.AppendLine("Group allready in settings, returning default");
				Logger.Write(sb.ToString());
				return this [Name];
			} 
			//Else create one and add it
			else {
				//Create group
				SettingGroup s = new SettingGroup(Name);
				this.Add(s);
				sb.AppendLine("Created group {0} and added to settings".format(Name));
				Logger.Write(sb.ToString());
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
			Logger.Write("Removed group {0}".format(Name));
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
			Logger.Write("Saved settings to xml");
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
					string sname = xmlReader.GetAttribute("name") as string;
					currentsetting = currentgroup.CreateSetting(sname);
					string type = xmlReader.GetAttribute("type") as string;
					xmlReader.Read();
					string strval = xmlReader.ReadContentAsString();
					object value = Convert.ChangeType(strval, Type.GetType(type));
					currentsetting.Value = value;
				}
			}

			Logger.Write("Loaded settings from xml");

			//return the settings
			return settings;
		}
	}
}

