using System;
using System.IO;

namespace MCM.Core
{
	public static class Logger
	{
	    static readonly TextWriter tw;

		static string Path { get; private set; }

	    static void CreateLogger(string Path)
	    {
			Logger.Path = Path;
	        tw = TextWriter.Synchronized(File.AppendText(Path));
	    }

	    public static void Write(string logMessage)
	    {
	        try
	        {
	            Log(logMessage, tw);
	        }
	        catch (IOException e)
	        {
	            tw.Close();
	        }
	    }

	    private static readonly object _syncObject = new object();

		public static void Log(string logMessage, TextWriter w)    {
		   // only one thread can own this lock, so other threads
		   // entering this method will wait here until lock is
		   // available.
		   lock(_syncObject) {
		      w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
		      w.WriteLine("  :");
		      w.WriteLine("  :{0}", logMessage);
		      w.WriteLine("-------------------------------");
		      // Update the underlying file.
		      w.Flush();
		   }
		}
	}
}

