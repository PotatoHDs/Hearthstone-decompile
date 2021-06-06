using System;
using System.Diagnostics;
using System.IO;
using Hearthstone;

public class BattleNetClient
{
	public static bool needsToRun
	{
		get
		{
			if (usedOnThisPlatform)
			{
				return !launchedHearthstone;
			}
			return false;
		}
	}

	private static bool usedOnThisPlatform => true;

	private static bool launchedHearthstone
	{
		get
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				if (commandLineArgs[i].Equals("-launch", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
	}

	private static FileInfo bootstrapper => new FileInfo("Hearthstone Beta Launcher.exe");

	public static void quitHearthstoneAndRun()
	{
		Log.All.PrintWarning("Hearthstone was not run from Battle.net Client");
		if (!bootstrapper.Exists)
		{
			Log.All.PrintWarning("Hearthstone could not find Battle.net client");
			Error.AddFatal(FatalErrorReason.NO_BNET_CLIENT, "GLUE_CANNOT_FIND_BATTLENET_CLIENT");
			return;
		}
		try
		{
			Process process = new Process();
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.FileName = bootstrapper.FullName;
			process.StartInfo.Arguments = "-uid hs_beta";
			process.EnableRaisingEvents = true;
			process.Start();
			Log.All.PrintWarning("Hearthstone ran Battle.net Client.  Exiting.");
			HearthstoneApplication.Get().Exit();
		}
		catch (Exception ex)
		{
			Error.AddFatal(FatalErrorReason.FAIL_BNET_CLIENT, "GLUE_CANNOT_RUN_BATTLENET_CLIENT");
			Log.All.PrintWarning("Hearthstone could not launch Battle.net client: {0}", ex.Message);
		}
	}
}
