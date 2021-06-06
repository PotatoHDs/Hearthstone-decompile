using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using PegasusGame;
using PegasusShared;
using PegasusUtil;

// Token: 0x020009F2 RID: 2546
public class ServerLogs : IService
{
	// Token: 0x060089DA RID: 35290 RVA: 0x002C326A File Offset: 0x002C146A
	public void RequestServerLogSources()
	{
		if (!this.m_serverLogSourcesRequested)
		{
			this.m_serverLogSourcesRequested = true;
			this.EnableLogRelayOnUtilServer("*");
			this.EnableLogRelayOnGameServer("*");
		}
	}

	// Token: 0x060089DB RID: 35291 RVA: 0x002C3291 File Offset: 0x002C1491
	public IEnumerable<string> GetServerLogSources()
	{
		return this.m_serverLogSources;
	}

	// Token: 0x060089DC RID: 35292 RVA: 0x002C3299 File Offset: 0x002C1499
	public IEnumerable<string> GetEnabledServerLogSources()
	{
		return this.m_enabledServerLogSources;
	}

	// Token: 0x060089DD RID: 35293 RVA: 0x002C32A1 File Offset: 0x002C14A1
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_serverLogger = new Logger("Server");
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(UtilLogRelay.PacketID.ID, new Network.NetHandler(this.OnUtilLogRelay), null);
		network.RegisterNetHandler(GameLogRelay.PacketID.ID, new Network.NetHandler(this.OnGameLogRelay), null);
		network.RegisterNetHandler(UpdateLoginComplete.PacketID.ID, new Network.NetHandler(this.OnUpdateLoginComplete), null);
		network.RegisterNetHandler(GameSetup.PacketID.ID, new Network.NetHandler(this.OnGameSetup), null);
		yield break;
	}

	// Token: 0x060089DE RID: 35294 RVA: 0x002C32B7 File Offset: 0x002C14B7
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(CheatMgr)
		};
	}

	// Token: 0x060089DF RID: 35295 RVA: 0x002C32DC File Offset: 0x002C14DC
	public void Shutdown()
	{
		Network network;
		if (HearthstoneServices.TryGet<Network>(out network))
		{
			network.RemoveNetHandler(UtilLogRelay.PacketID.ID, new Network.NetHandler(this.OnUtilLogRelay));
			network.RemoveNetHandler(GameLogRelay.PacketID.ID, new Network.NetHandler(this.OnGameLogRelay));
			network.RemoveNetHandler(UpdateLoginComplete.PacketID.ID, new Network.NetHandler(this.OnUpdateLoginComplete));
			network.RemoveNetHandler(GameSetup.PacketID.ID, new Network.NetHandler(this.OnGameSetup));
		}
	}

	// Token: 0x060089E0 RID: 35296 RVA: 0x002C3360 File Offset: 0x002C1560
	public static char ServerLogLevelToClientLogPrefix(ServerLogs.ServerLogLevel level)
	{
		switch (level)
		{
		case ServerLogs.ServerLogLevel.LOG_DEBUG:
			return 'D';
		case ServerLogs.ServerLogLevel.LOG_INFO:
		case ServerLogs.ServerLogLevel.LOG_NOTE:
			return 'I';
		case ServerLogs.ServerLogLevel.LOG_WARN:
			return 'W';
		}
		return 'E';
	}

	// Token: 0x060089E1 RID: 35297 RVA: 0x002C338F File Offset: 0x002C158F
	public void EnableLogRelay(string serverLogSourceName)
	{
		if (string.IsNullOrEmpty(serverLogSourceName))
		{
			return;
		}
		if (!this.m_enabledServerLogSources.Contains(serverLogSourceName))
		{
			this.m_enabledServerLogSources.Add(serverLogSourceName);
			this.m_enabledServerLogSources.Sort();
		}
		this.EnableLogRelayOnUtilServer(serverLogSourceName);
		this.EnableLogRelayOnGameServer(serverLogSourceName);
	}

	// Token: 0x060089E2 RID: 35298 RVA: 0x002C33D0 File Offset: 0x002C15D0
	private void EnableLogRelayOnUtilServer(string logSourceName)
	{
		CheatMgr cheatMgr = CheatMgr.Get();
		if (cheatMgr != null)
		{
			cheatMgr.RunCheatInternally("util logrelay " + logSourceName);
		}
	}

	// Token: 0x060089E3 RID: 35299 RVA: 0x002C33F8 File Offset: 0x002C15F8
	private void EnableLogRelayOnGameServer(string logSourceName)
	{
		Network network = HearthstoneServices.Get<Network>();
		if (network == null)
		{
			return;
		}
		if (!network.IsConnectedToGameServer())
		{
			return;
		}
		CheatMgr cheatMgr = CheatMgr.Get();
		if (cheatMgr != null)
		{
			cheatMgr.RunCheatInternally("cheat logrelay " + logSourceName);
		}
	}

	// Token: 0x060089E4 RID: 35300 RVA: 0x002C3434 File Offset: 0x002C1634
	private void OnUtilLogRelay()
	{
		UtilLogRelay utilLogRelay = Network.Get().GetUtilLogRelay();
		this.OnLogRelayMessages(utilLogRelay.Messages, ServerLogs.ServiceType.UtilServer);
	}

	// Token: 0x060089E5 RID: 35301 RVA: 0x002C345C File Offset: 0x002C165C
	private void OnGameLogRelay()
	{
		GameLogRelay gameLogRelay = Network.Get().GetGameLogRelay();
		this.OnLogRelayMessages(gameLogRelay.Messages, ServerLogs.ServiceType.GameServer);
	}

	// Token: 0x060089E6 RID: 35302 RVA: 0x002C3484 File Offset: 0x002C1684
	private void OnLogRelayMessages(List<LogRelayMessage> messages, ServerLogs.ServiceType serviceType)
	{
		if (this.m_myPlayerIdAsString == null)
		{
			SceneDebugger sceneDebugger = SceneDebugger.Get();
			if (sceneDebugger != null)
			{
				long? playerId_DebugOnly = sceneDebugger.GetPlayerId_DebugOnly();
				if (playerId_DebugOnly != null)
				{
					this.m_myPlayerIdAsString = playerId_DebugOnly.Value.ToString(CultureInfo.InvariantCulture);
					this.m_playerIdReplacement = this.m_myPlayerIdAsString + "(me)";
				}
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (LogRelayMessage logRelayMessage in messages)
		{
			if (logRelayMessage.Event == "log_sources")
			{
				foreach (string item in logRelayMessage.Message.Split(new char[]
				{
					','
				}))
				{
					if (!this.m_serverLogSources.Contains(item))
					{
						this.m_serverLogSources.Add(item);
					}
				}
				this.m_serverLogSources.Sort();
			}
			else
			{
				if (!this.m_enabledServerLogSources.Contains(logRelayMessage.Log))
				{
					this.m_enabledServerLogSources.Add(logRelayMessage.Log);
					this.m_enabledServerLogSources.Sort();
				}
				stringBuilder.Append(ServerLogs.ServerLogLevelToClientLogPrefix((ServerLogs.ServerLogLevel)logRelayMessage.Severity));
				stringBuilder.Append(' ');
				stringBuilder.Append(TimeUtils.UnixTimeStampToDateTimeLocal(logRelayMessage.Timestamp).TimeOfDay.ToString());
				stringBuilder.Append(" [");
				stringBuilder.Append(logRelayMessage.Log);
				stringBuilder.Append("] [");
				stringBuilder.Append(serviceType.ToString());
				stringBuilder.Append("] ");
				string text = logRelayMessage.Message.Replace('|', '\n');
				if (this.m_myPlayerIdAsString != null)
				{
					text = text.Replace(this.m_myPlayerIdAsString, this.m_playerIdReplacement);
				}
				stringBuilder.AppendLine(text);
			}
		}
		this.m_serverLogger.FilePrintRaw(stringBuilder.ToString());
	}

	// Token: 0x060089E7 RID: 35303 RVA: 0x002C36B0 File Offset: 0x002C18B0
	private void OnUpdateLoginComplete()
	{
		if (this.m_serverLogSourcesRequested)
		{
			this.EnableLogRelayOnUtilServer("*");
		}
		foreach (string logSourceName in this.m_enabledServerLogSources)
		{
			this.EnableLogRelayOnUtilServer(logSourceName);
		}
	}

	// Token: 0x060089E8 RID: 35304 RVA: 0x002C3718 File Offset: 0x002C1918
	private void OnGameSetup()
	{
		if (this.m_serverLogSourcesRequested)
		{
			this.EnableLogRelayOnGameServer("*");
		}
		foreach (string logSourceName in this.m_enabledServerLogSources)
		{
			this.EnableLogRelayOnGameServer(logSourceName);
		}
	}

	// Token: 0x04007344 RID: 29508
	private bool m_serverLogSourcesRequested;

	// Token: 0x04007345 RID: 29509
	private Logger m_serverLogger;

	// Token: 0x04007346 RID: 29510
	private List<string> m_serverLogSources = new List<string>();

	// Token: 0x04007347 RID: 29511
	private List<string> m_enabledServerLogSources = new List<string>();

	// Token: 0x04007348 RID: 29512
	private string m_myPlayerIdAsString;

	// Token: 0x04007349 RID: 29513
	private string m_playerIdReplacement;

	// Token: 0x02002682 RID: 9858
	public enum ServerLogLevel
	{
		// Token: 0x0400F0CB RID: 61643
		LOG_NONE,
		// Token: 0x0400F0CC RID: 61644
		LOG_DEBUG,
		// Token: 0x0400F0CD RID: 61645
		LOG_INFO,
		// Token: 0x0400F0CE RID: 61646
		LOG_NOTE,
		// Token: 0x0400F0CF RID: 61647
		LOG_WARN,
		// Token: 0x0400F0D0 RID: 61648
		LOG_ERROR,
		// Token: 0x0400F0D1 RID: 61649
		LOG_FATAL
	}

	// Token: 0x02002683 RID: 9859
	private enum ServiceType
	{
		// Token: 0x0400F0D3 RID: 61651
		UtilServer,
		// Token: 0x0400F0D4 RID: 61652
		GameServer
	}
}
