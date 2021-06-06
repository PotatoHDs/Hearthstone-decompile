using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using PegasusGame;
using PegasusShared;
using PegasusUtil;

public class ServerLogs : IService
{
	public enum ServerLogLevel
	{
		LOG_NONE,
		LOG_DEBUG,
		LOG_INFO,
		LOG_NOTE,
		LOG_WARN,
		LOG_ERROR,
		LOG_FATAL
	}

	private enum ServiceType
	{
		UtilServer,
		GameServer
	}

	private bool m_serverLogSourcesRequested;

	private Logger m_serverLogger;

	private List<string> m_serverLogSources = new List<string>();

	private List<string> m_enabledServerLogSources = new List<string>();

	private string m_myPlayerIdAsString;

	private string m_playerIdReplacement;

	public void RequestServerLogSources()
	{
		if (!m_serverLogSourcesRequested)
		{
			m_serverLogSourcesRequested = true;
			EnableLogRelayOnUtilServer("*");
			EnableLogRelayOnGameServer("*");
		}
	}

	public IEnumerable<string> GetServerLogSources()
	{
		return m_serverLogSources;
	}

	public IEnumerable<string> GetEnabledServerLogSources()
	{
		return m_enabledServerLogSources;
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_serverLogger = new Logger("Server");
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(UtilLogRelay.PacketID.ID, OnUtilLogRelay);
		network.RegisterNetHandler(GameLogRelay.PacketID.ID, OnGameLogRelay);
		network.RegisterNetHandler(UpdateLoginComplete.PacketID.ID, OnUpdateLoginComplete);
		network.RegisterNetHandler(GameSetup.PacketID.ID, OnGameSetup);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(Network),
			typeof(CheatMgr)
		};
	}

	public void Shutdown()
	{
		if (HearthstoneServices.TryGet<Network>(out var service))
		{
			service.RemoveNetHandler(UtilLogRelay.PacketID.ID, OnUtilLogRelay);
			service.RemoveNetHandler(GameLogRelay.PacketID.ID, OnGameLogRelay);
			service.RemoveNetHandler(UpdateLoginComplete.PacketID.ID, OnUpdateLoginComplete);
			service.RemoveNetHandler(GameSetup.PacketID.ID, OnGameSetup);
		}
	}

	public static char ServerLogLevelToClientLogPrefix(ServerLogLevel level)
	{
		switch (level)
		{
		case ServerLogLevel.LOG_WARN:
			return 'W';
		case ServerLogLevel.LOG_INFO:
		case ServerLogLevel.LOG_NOTE:
			return 'I';
		case ServerLogLevel.LOG_DEBUG:
			return 'D';
		default:
			return 'E';
		}
	}

	public void EnableLogRelay(string serverLogSourceName)
	{
		if (!string.IsNullOrEmpty(serverLogSourceName))
		{
			if (!m_enabledServerLogSources.Contains(serverLogSourceName))
			{
				m_enabledServerLogSources.Add(serverLogSourceName);
				m_enabledServerLogSources.Sort();
			}
			EnableLogRelayOnUtilServer(serverLogSourceName);
			EnableLogRelayOnGameServer(serverLogSourceName);
		}
	}

	private void EnableLogRelayOnUtilServer(string logSourceName)
	{
		CheatMgr.Get()?.RunCheatInternally("util logrelay " + logSourceName);
	}

	private void EnableLogRelayOnGameServer(string logSourceName)
	{
		Network network = HearthstoneServices.Get<Network>();
		if (network != null && network.IsConnectedToGameServer())
		{
			CheatMgr.Get()?.RunCheatInternally("cheat logrelay " + logSourceName);
		}
	}

	private void OnUtilLogRelay()
	{
		UtilLogRelay utilLogRelay = Network.Get().GetUtilLogRelay();
		OnLogRelayMessages(utilLogRelay.Messages, ServiceType.UtilServer);
	}

	private void OnGameLogRelay()
	{
		GameLogRelay gameLogRelay = Network.Get().GetGameLogRelay();
		OnLogRelayMessages(gameLogRelay.Messages, ServiceType.GameServer);
	}

	private void OnLogRelayMessages(List<LogRelayMessage> messages, ServiceType serviceType)
	{
		if (m_myPlayerIdAsString == null)
		{
			SceneDebugger sceneDebugger = SceneDebugger.Get();
			if (sceneDebugger != null)
			{
				long? playerId_DebugOnly = sceneDebugger.GetPlayerId_DebugOnly();
				if (playerId_DebugOnly.HasValue)
				{
					m_myPlayerIdAsString = playerId_DebugOnly.Value.ToString(CultureInfo.InvariantCulture);
					m_playerIdReplacement = m_myPlayerIdAsString + "(me)";
				}
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (LogRelayMessage message in messages)
		{
			if (message.Event == "log_sources")
			{
				string[] array = message.Message.Split(',');
				foreach (string item in array)
				{
					if (!m_serverLogSources.Contains(item))
					{
						m_serverLogSources.Add(item);
					}
				}
				m_serverLogSources.Sort();
				continue;
			}
			if (!m_enabledServerLogSources.Contains(message.Log))
			{
				m_enabledServerLogSources.Add(message.Log);
				m_enabledServerLogSources.Sort();
			}
			stringBuilder.Append(ServerLogLevelToClientLogPrefix((ServerLogLevel)message.Severity));
			stringBuilder.Append(' ');
			stringBuilder.Append(TimeUtils.UnixTimeStampToDateTimeLocal(message.Timestamp).TimeOfDay.ToString());
			stringBuilder.Append(" [");
			stringBuilder.Append(message.Log);
			stringBuilder.Append("] [");
			stringBuilder.Append(serviceType.ToString());
			stringBuilder.Append("] ");
			string text = message.Message.Replace('|', '\n');
			if (m_myPlayerIdAsString != null)
			{
				text = text.Replace(m_myPlayerIdAsString, m_playerIdReplacement);
			}
			stringBuilder.AppendLine(text);
		}
		m_serverLogger.FilePrintRaw(stringBuilder.ToString());
	}

	private void OnUpdateLoginComplete()
	{
		if (m_serverLogSourcesRequested)
		{
			EnableLogRelayOnUtilServer("*");
		}
		foreach (string enabledServerLogSource in m_enabledServerLogSources)
		{
			EnableLogRelayOnUtilServer(enabledServerLogSource);
		}
	}

	private void OnGameSetup()
	{
		if (m_serverLogSourcesRequested)
		{
			EnableLogRelayOnGameServer("*");
		}
		foreach (string enabledServerLogSource in m_enabledServerLogSources)
		{
			EnableLogRelayOnGameServer(enabledServerLogSource);
		}
	}
}
