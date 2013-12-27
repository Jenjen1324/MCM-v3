using System;

using MCM.Core.DownloadManager;
using MCM.Core.Utils;
using System.IO;
using MCM.Core;
using MCM.Core.LoginAPI;
using MCM.Core.Settings;
using System.Text;
using System.Xml;

namespace testing
{
	public static class Program
	{
		public static void Main ()
		{
			Logger.CreateLogger("/home/andreas/testingmcm/mcmlog.log");

			/*
			Settings settings = new Settings();
			settings.CreateGroup("main");
			var set1 = settings["main"].CreateSetting("set1");
			set1.Value = true;
			StringBuilder xml = new StringBuilder();
			settings.GenerateXML(xml);
			File.WriteAllText("/home/andreas/testingmcm/settins.xml",xml.ToString());
			*/

			FileStream xmlfs = new FileStream("/home/andreas/testingmcm/settings.xml",FileMode.Open);
			XmlReader xmlr = XmlReader.Create(xmlfs);
			Settings settings = Settings.LoadFromXML(xmlr);
			foreach (SettingGroup group in settings) {
				Console.WriteLine("{0}:".format(group.Name));
				foreach (var setting in group) {
					Console.WriteLine("- {0} : {1} - {2}".format(setting.Key,setting.Value.ToString(),setting.Value.GetType().FullName));
				}
			}
			Console.ReadKey();

			LoginManager.SaveToFolder("/home/andreas/testingmcm");
		}
	}
}
