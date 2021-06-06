using System;
using System.Diagnostics;
using System.IO;
using Hearthstone;

// Token: 0x02000862 RID: 2146
public class BattleNetClient
{
	// Token: 0x170006DD RID: 1757
	// (get) Token: 0x060073D7 RID: 29655 RVA: 0x00252B42 File Offset: 0x00250D42
	public static bool needsToRun
	{
		get
		{
			return BattleNetClient.usedOnThisPlatform && !BattleNetClient.launchedHearthstone;
		}
	}

	// Token: 0x060073D8 RID: 29656 RVA: 0x00252B58 File Offset: 0x00250D58
	public static void quitHearthstoneAndRun()
	{
		Log.All.PrintWarning("Hearthstone was not run from Battle.net Client", Array.Empty<object>());
		if (!BattleNetClient.bootstrapper.Exists)
		{
			Log.All.PrintWarning("Hearthstone could not find Battle.net client", Array.Empty<object>());
			Error.AddFatal(FatalErrorReason.NO_BNET_CLIENT, "GLUE_CANNOT_FIND_BATTLENET_CLIENT", Array.Empty<object>());
			return;
		}
		try
		{
			new Process
			{
				StartInfo = 
				{
					UseShellExecute = false,
					FileName = BattleNetClient.bootstrapper.FullName,
					Arguments = "-uid hs_beta"
				},
				EnableRaisingEvents = true
			}.Start();
			Log.All.PrintWarning("Hearthstone ran Battle.net Client.  Exiting.", Array.Empty<object>());
			HearthstoneApplication.Get().Exit();
		}
		catch (Exception ex)
		{
			Error.AddFatal(FatalErrorReason.FAIL_BNET_CLIENT, "GLUE_CANNOT_RUN_BATTLENET_CLIENT", Array.Empty<object>());
			Log.All.PrintWarning("Hearthstone could not launch Battle.net client: {0}", new object[]
			{
				ex.Message
			});
		}
	}

	// Token: 0x170006DE RID: 1758
	// (get) Token: 0x060073D9 RID: 29657 RVA: 0x000052EC File Offset: 0x000034EC
	private static bool usedOnThisPlatform
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170006DF RID: 1759
	// (get) Token: 0x060073DA RID: 29658 RVA: 0x00252C54 File Offset: 0x00250E54
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

	// Token: 0x170006E0 RID: 1760
	// (get) Token: 0x060073DB RID: 29659 RVA: 0x00252C88 File Offset: 0x00250E88
	private static FileInfo bootstrapper
	{
		get
		{
			return new FileInfo("Hearthstone Beta Launcher.exe");
		}
	}
}
