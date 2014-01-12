using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MCM.Core.Utils;

namespace MCM.Core.SettingsAPI
{
	/// <summary>
	/// Contains all settings
	/// </summary>
	public static class Settings
	{
		static List<SettingGroup> list;

		static Settings ()
		{
			list = new List<SettingGroup>();
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
		public static SettingGroup GetGroup(string name)
		{
			return list.Find((i) => i.Name == name);
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
		public static SettingGroup CreateGroup (string Name)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Creating group: {0}".format(Name));
			//if the group exists, return it
			if (list.Exists ((i) => i.Name == Name)) {
				sb.AppendLine("Group allready in settings, returning default");
				Logger.Write(sb.ToString());
				return GetGroup(Name);
			} 
			//Else create one and add it
			else {
				//Create group
				SettingGroup s = new SettingGroup(Name);
				list.Add(s);
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
		public static void RemoveGroup (string Name)
		{
			list.RemoveAll((ii) => ii.Name == Name);
			Logger.Write("Removed group {0}".format(Name));
		}

		/// <summary>
		/// Generates the XML.
		/// </summary>
		/// <param name='xmlBuilder'>
		/// Xml builder.
		/// </param>
		public static void GenerateXML (StringBuilder xmlBuilder)
		{
			xmlBuilder.AppendLine("<settings>");
			list.ForEach((g) => g.GenerateXML(xmlBuilder));
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
		public static void LoadFromXML(XmlReader xmlReader) {
			//Create tmp variables
			SettingGroup currentgroup = null;
			Setting currentsetting = null;

			//Start read loop
			while (xmlReader.Read()) {
				if (xmlReader.Name == "group" && xmlReader.NodeType == XmlNodeType.Element) {
					string gname = xmlReader.GetAttribute("name") as string;
					currentgroup = CreateGroup(gname);
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
		}

		public static IEnumerable<SettingGroup> GetList ()
		{
			return list.AsReadOnly();
		}
	}
}

