using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Assets;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Progression;
using Hearthstone.Streaming;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using UnityEngine;
using UnityEngine.Networking;

public class SceneDebugger : IService, IHasUpdate
{
	public class ConsoleLogEntry : LoggerDebugWindow.LogEntry
	{
		public ConsoleLogEntry(Log.LogLevel level, string message)
		{
			category = level;
			message = message.Trim();
			switch (level)
			{
			case Log.LogLevel.Debug:
				message = $"<color=grey>{message}</color>";
				break;
			case Log.LogLevel.Warning:
				message = $"<color=yellow>{message}</color>";
				break;
			case Log.LogLevel.Error:
				message = $"<color=red>{message}</color>";
				break;
			}
			DateTime now = DateTime.Now;
			string arg = $"<color=grey>[{now.Hour.ToString().PadLeft(2, '0')}:{now.Minute.ToString().PadLeft(2, '0')}:{now.Second.ToString().PadLeft(2, '0')}]</color>";
			text = $"{arg} {message}";
		}
	}

	private class SlushTimeRecord : LoggerDebugWindow.LogEntry
	{
		public float ExpectedStart { get; set; }

		public float ExpectedEnd { get; set; }

		public int TaskId { get; set; }

		public float ActualStart { get; set; }

		public float ActualEnd { get; set; }

		public int EntityId { get; set; }

		public SlushTimeRecord(int taskId, float expectedStart, float expectedEnd, float actualStart = 0f, float actualEnd = 0f, int entityId = 0)
		{
			TaskId = taskId;
			ExpectedStart = expectedStart;
			ExpectedEnd = expectedEnd;
			ActualStart = actualStart;
			ActualEnd = actualEnd;
			EntityId = entityId;
			text = ToString();
		}

		private float GetDuration(float start, float end)
		{
			return end - start;
		}

		public override string ToString()
		{
			float duration = GetDuration(ExpectedStart, ExpectedEnd);
			float duration2 = GetDuration(ActualStart, ActualEnd);
			float num = ActualStart - ExpectedStart;
			float num2 = duration2 - duration;
			num2 += num;
			string text = ((num2 > 0f) ? "+" : "");
			string text2 = "";
			if (EntityId != 0)
			{
				Entity entity = GameState.Get().GetEntity(EntityId);
				if (entity != null)
				{
					text2 = entity.GetName();
				}
			}
			return $"TaskId: {TaskId}, ({text2}) {text}{num2}";
		}
	}

	private class ScriptWarning : LoggerDebugWindow.LogEntry
	{
		public string Source { get; private set; }

		public string Event { get; private set; }

		public string Message { get; private set; }

		public int Severity { get; set; }

		public string PowerDef { get; private set; }

		public int PC { get; private set; }

		public string IssueGUID { get; private set; }

		public ScriptWarning(string logSource, string logEvent, string logMessage)
		{
			Source = logSource;
			Event = logEvent;
			Message = logMessage;
			Severity = -1;
			PowerDef = "";
			PC = -1;
			IssueGUID = "";
			RebuildString();
		}

		public void SetPowerDefInfo(string powerDef, string pc)
		{
			if (powerDef.Length >= 0 && pc.Length >= 0 && int.TryParse(pc, out var result))
			{
				PowerDef = powerDef;
				PC = result;
				RebuildString();
			}
		}

		public string ComputeIssueGUID()
		{
			string text = "";
			if (PowerDef.Length > 0 && PC >= 0)
			{
				text = $"{Event}|{PowerDef}|{PC}";
			}
			else if (Source.Length > 0)
			{
				text = $"{Event}|{Source}";
			}
			if (text.Length <= 0)
			{
				return "";
			}
			byte[] inArray = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text));
			IssueGUID = Convert.ToBase64String(inArray);
			RebuildString();
			return IssueGUID;
		}

		public void RebuildString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"<color=red>-> [{Event}]</color>");
			if (Source.Length > 0)
			{
				stringBuilder.AppendLine($"    -source={Source}");
			}
			string[] array = Message.Split('|');
			foreach (string text in array)
			{
				if (text.Length > 0)
				{
					stringBuilder.AppendLine($"    -{text}");
				}
			}
			if (IssueGUID.Length > 0)
			{
				stringBuilder.AppendLine($"    -(guid: {IssueGUID})");
			}
			base.text = stringBuilder.ToString();
		}

		public override string ToString()
		{
			return $"Received script warning from '{Source}'!  event:[{Event}]  message:\"{Message}\"  guid:({IssueGUID})";
		}
	}

	private readonly UnityEngine.Vector2 m_GUISize = new UnityEngine.Vector2(175f, 30f);

	private float m_UpdateInterval = 0.5f;

	private double m_LastInterval;

	private int m_frames;

	private string m_fpsText = string.Empty;

	private bool m_testMessaging;

	private DebuggerGuiWindow m_guiWindow;

	private DebuggerGuiWindow m_rankWindow;

	private DebuggerGuiWindow m_assetsWindow;

	private DebuggerGuiWindow m_gameplayWindow;

	private DebuggerGuiWindow m_questWindow;

	private DebuggerGuiWindow m_achievementWindow;

	private DebuggerGuiWindow m_rewardTrackWindow;

	private LoggerDebugWindow m_messageWindow;

	private LoggerDebugWindow m_serverLogWindow;

	private CheatsDebugWindow m_cheatsWindow;

	private LoggerDebugWindow m_slushTrackerWindow;

	private DebuggerGuiWindow m_notepadWindow;

	private string m_notepadContents = "";

	private bool m_notepadFirstRun = true;

	private UnityEngine.Vector2 scrollViewVector = UnityEngine.Vector2.zero;

	private DebuggerGui m_timeSection;

	private DebuggerGui m_qualitySection;

	private DebuggerGui m_statsSection;

	private bool m_showGuiCustomization;

	private int m_guiSaveTimer = -1;

	private List<DebuggerGui> m_debuggerGui;

	private long? m_playerId;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		TimeScaleMgr.Get().SetTimeScaleMultiplier(GetDevTimescaleMultiplier());
		UnityEngine.Vector2 scaledScreen = GetScaledScreen();
		m_guiWindow = new DebuggerGuiWindow("Scene Debugger", LayoutGuiControls, canClose: false, canResize: false);
		m_guiWindow.Position = new UnityEngine.Vector2(scaledScreen.x * 0.05f, scaledScreen.y * 0.125f);
		m_timeSection = new DebuggerGui("Time Scale", LayoutTimeControls);
		m_qualitySection = new DebuggerGui("Quality", LayoutQualityControls);
		m_statsSection = new DebuggerGui("Stats", LayoutStats);
		m_cheatsWindow = new CheatsDebugWindow(m_GUISize);
		m_cheatsWindow.Position = new UnityEngine.Vector2(scaledScreen.x * 0.5f, 0f);
		m_cheatsWindow.ResizeToFit(scaledScreen.x * 0.5f, scaledScreen.y * 0.5f);
		m_cheatsWindow.collapsedWidth = m_GUISize.x;
		m_messageWindow = new LoggerDebugWindow("Messages", m_GUISize, Enum.GetValues(typeof(Log.LogLevel)).Cast<object>());
		m_messageWindow.CustomLayout = LayoutMessages;
		m_messageWindow.collapsedWidth = m_GUISize.x;
		m_messageWindow.Position = new UnityEngine.Vector2(0f, 0.65f * scaledScreen.y - 35f);
		m_messageWindow.ResizeToFit(scaledScreen.x, scaledScreen.y * 0.35f);
		m_serverLogWindow = new LoggerDebugWindow("Server Script Log", m_GUISize, Enum.GetValues(typeof(ServerLogs.ServerLogLevel)).Cast<object>());
		m_serverLogWindow.CustomLayout = LayoutScriptWarnings;
		m_serverLogWindow.collapsedWidth = m_GUISize.x;
		m_serverLogWindow.Position = new UnityEngine.Vector2(0f, 0.65f * scaledScreen.y - 35f);
		m_serverLogWindow.ResizeToFit(scaledScreen.x, scaledScreen.y * 0.35f);
		m_rankWindow = new DebuggerGuiWindow("Rank", LayoutRankDebug, canClose: true, canResize: false);
		m_rankWindow.collapsedWidth = m_GUISize.x;
		m_rankWindow.Position = new UnityEngine.Vector2(scaledScreen.x - m_GUISize.x, 0.5f * scaledScreen.y);
		m_rankWindow.ResizeToFit(m_GUISize.x, m_GUISize.y);
		m_assetsWindow = new DebuggerGuiWindow("Assets", LayoutAssetsDebug);
		m_assetsWindow.collapsedWidth = m_GUISize.x;
		m_assetsWindow.Position = new UnityEngine.Vector2(scaledScreen.x - m_GUISize.x, 0.5f * scaledScreen.y);
		m_assetsWindow.ResizeToFit(m_GUISize.x, m_GUISize.y);
		m_gameplayWindow = new DebuggerGuiWindow("Gameplay", LayoutGameplayDebug);
		m_gameplayWindow.collapsedWidth = m_GUISize.x;
		m_gameplayWindow.Position = new UnityEngine.Vector2(scaledScreen.x - m_GUISize.x, 0.5f * scaledScreen.y);
		m_gameplayWindow.ResizeToFit(m_GUISize.x, m_GUISize.y);
		float num = m_GUISize.x * 2f;
		m_questWindow = new DebuggerGuiWindow("Quest", LayoutQuestDebug, canClose: true, canResize: false);
		m_questWindow.collapsedWidth = m_GUISize.x;
		m_questWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num, 0.5f * scaledScreen.y);
		m_questWindow.ResizeToFit(num, m_GUISize.y);
		float num2 = m_GUISize.x * 2.5f;
		m_achievementWindow = new DebuggerGuiWindow("Achievement", LayoutAchievementDebug, canClose: true, canResize: false);
		m_achievementWindow.collapsedWidth = m_GUISize.x;
		m_achievementWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num2, 0.5f * scaledScreen.y);
		m_achievementWindow.ResizeToFit(num2, m_GUISize.y);
		float num3 = m_GUISize.x * 2f;
		m_rewardTrackWindow = new DebuggerGuiWindow("Reward Track", LayoutRewardTrackDebug, canClose: true, canResize: false);
		m_rewardTrackWindow.collapsedWidth = m_GUISize.x;
		m_rewardTrackWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num3, 0.5f * scaledScreen.y);
		m_rewardTrackWindow.ResizeToFit(num3, m_GUISize.y);
		m_slushTrackerWindow = new LoggerDebugWindow("Slush Time Log", m_GUISize, Enum.GetValues(typeof(Log.LogLevel)).Cast<object>());
		m_slushTrackerWindow.collapsedWidth = m_GUISize.x;
		m_slushTrackerWindow.Position = new UnityEngine.Vector2(0f, 0.65f * scaledScreen.y - 35f);
		m_slushTrackerWindow.ResizeToFit(scaledScreen.x, scaledScreen.y * 0.35f);
		float num4 = m_GUISize.x * 2f;
		m_notepadWindow = new DebuggerGuiWindow("Notepad", LayoutNotepadDebug);
		m_notepadWindow.collapsedWidth = m_GUISize.x;
		m_notepadWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num4, 0.5f * scaledScreen.y);
		m_notepadWindow.ResizeToFit(num4, m_GUISize.y);
		m_debuggerGui = new List<DebuggerGui>();
		m_debuggerGui.Add(m_guiWindow);
		m_debuggerGui.Add(m_cheatsWindow);
		m_debuggerGui.Add(m_messageWindow);
		m_debuggerGui.Add(m_serverLogWindow);
		m_debuggerGui.Add(m_rankWindow);
		m_debuggerGui.Add(m_assetsWindow);
		m_debuggerGui.Add(m_questWindow);
		m_debuggerGui.Add(m_achievementWindow);
		m_debuggerGui.Add(m_rewardTrackWindow);
		m_debuggerGui.Add(m_timeSection);
		m_debuggerGui.Add(m_qualitySection);
		m_debuggerGui.Add(m_statsSection);
		m_debuggerGui.Add(m_slushTrackerWindow);
		m_debuggerGui.Add(m_notepadWindow);
		m_debuggerGui.Add(m_gameplayWindow);
		foreach (DebuggerGui item in m_debuggerGui)
		{
			item.OnChanged += HandleGuiChanged;
		}
		m_guiWindow.IsShown = true;
		m_cheatsWindow.IsShown = false;
		m_messageWindow.IsShown = false;
		m_serverLogWindow.IsShown = false;
		m_rankWindow.IsShown = false;
		m_assetsWindow.IsShown = false;
		m_gameplayWindow.IsShown = false;
		m_questWindow.IsShown = false;
		m_achievementWindow.IsShown = false;
		m_rewardTrackWindow.IsShown = false;
		m_slushTrackerWindow.IsShown = false;
		m_notepadWindow.IsShown = false;
		DebuggerGui.LoadConfig(m_debuggerGui);
		Processor.RegisterOnGUIDelegate(OnGUI);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(GraphicsManager) };
	}

	public void Shutdown()
	{
	}

	public bool IsMouseOverGui()
	{
		if (!Options.Get().GetBool(Option.HUD))
		{
			return false;
		}
		foreach (DebuggerGui item in m_debuggerGui.Where((DebuggerGui g) => g is DebuggerGuiWindow))
		{
			DebuggerGuiWindow debuggerGuiWindow = (DebuggerGuiWindow)item;
			if (debuggerGuiWindow != null && item.IsShown && debuggerGuiWindow.IsMouseOver())
			{
				return true;
			}
		}
		return false;
	}

	public void Update()
	{
		m_frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if ((double)realtimeSinceStartup > m_LastInterval + (double)m_UpdateInterval)
		{
			float num = (float)m_frames / (float)((double)realtimeSinceStartup - m_LastInterval);
			m_fpsText = $"{SystemInfo.graphicsDeviceType}  FPS: {num:f2}\n";
			m_frames = 0;
			m_LastInterval = Time.realtimeSinceStartup;
		}
		if (m_testMessaging)
		{
			string text = "abcdefghijklmnopqrstuvwxyz0123456789";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 5000; i++)
			{
				char value = text[UnityEngine.Random.Range(0, text.Length)];
				stringBuilder.Append(value);
			}
			AddErrorMessage(stringBuilder.ToString());
		}
	}

	private void OnGUI()
	{
		if (ScriptDebugDisplay.Get().m_isDisplayed || !Options.Get().GetBool(Option.HUD))
		{
			return;
		}
		float guiScaling = GetGuiScaling();
		GUI.matrix = Matrix4x4.Scale(new Vector3(guiScaling, guiScaling, guiScaling));
		if (GameState.Get() != null && GameState.Get().GetSlushTimeTracker().GetAccruedLostTimeInSeconds() > (float)GameplayDebug.LOST_SLUSH_TIME_ERROR_THRESHOLD_SECONDS)
		{
			m_gameplayWindow.IsShown = true;
		}
		m_guiWindow.Layout();
		m_cheatsWindow.Layout();
		m_messageWindow.Layout();
		m_serverLogWindow.Layout();
		m_rankWindow.Layout();
		m_assetsWindow.Layout();
		m_gameplayWindow.Layout();
		m_questWindow.Layout();
		m_achievementWindow.Layout();
		m_rewardTrackWindow.Layout();
		m_slushTrackerWindow.Layout();
		m_notepadWindow.Layout();
		LayoutCursorDebug();
		if (m_guiSaveTimer > 0)
		{
			m_guiSaveTimer--;
			if (m_guiSaveTimer == 0)
			{
				DebuggerGui.SaveConfig(m_debuggerGui);
			}
		}
	}

	private float GetGuiScaling()
	{
		float val = 1f;
		GeneralUtils.TryParseFloat(Options.Get().GetOption(Option.HUD_SCALE).ToString(), out val);
		float num = Screen.height;
		switch (PlatformSettings.Screen)
		{
		case ScreenCategory.Phone:
			num = 480f;
			break;
		case ScreenCategory.MiniTablet:
			num = 576f;
			break;
		case ScreenCategory.Tablet:
			num = 640f;
			break;
		}
		return Mathf.Max(0.1f, val) * Mathf.Max(1f, (float)Screen.height / num);
	}

	private UnityEngine.Vector2 GetScaledScreen()
	{
		return new UnityEngine.Vector2(Screen.width, Screen.height) / GetGuiScaling();
	}

	public static SceneDebugger Get()
	{
		return HearthstoneServices.Get<SceneDebugger>();
	}

	[ContextMenu("Test Messaging")]
	public void TestMessaging()
	{
		m_testMessaging = true;
	}

	public static float GetDevTimescaleMultiplier()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return 1f;
		}
		return Options.Get().GetFloat(Option.DEV_TIMESCALE, 1f);
	}

	public static void SetDevTimescaleMultiplier(float f)
	{
		if (!HearthstoneApplication.IsPublic() && f != TimeScaleMgr.Get().GetTimeScaleMultiplier())
		{
			if (f == 0f)
			{
				f = 0.0001f;
			}
			Options.Get().SetFloat(Option.DEV_TIMESCALE, f);
			TimeScaleMgr.Get().SetTimeScaleMultiplier(f);
		}
	}

	public void SetPlayerId(long? playerId)
	{
		m_playerId = playerId;
	}

	public long? GetPlayerId_DebugOnly()
	{
		return m_playerId;
	}

	public void AddMessage(string message)
	{
		AddMessage(Log.LogLevel.Info, message);
	}

	public void AddMessage(Log.LogLevel level, string message, bool autoShow = false)
	{
		m_messageWindow.AddEntry(new ConsoleLogEntry(level, message), autoShow);
	}

	public void AddErrorMessage(string message)
	{
		AddMessage(Log.LogLevel.Error, message);
	}

	public void AddSlushTimeEntry(int taskId, float expectedStart, float expectedEnd, float actualStart = 0f, float actualEnd = 0f, int entityId = 0)
	{
		m_slushTrackerWindow.AddEntry(new SlushTimeRecord(taskId, expectedStart, expectedEnd, actualStart, actualEnd, entityId));
	}

	public void AddServerScriptLogMessage(ScriptLogMessage message)
	{
		int minSeverity = 3;
		if (message.Severity >= minSeverity && m_serverLogWindow.GetEntries().Count((LoggerDebugWindow.LogEntry m) => (m as ScriptWarning).Severity >= minSeverity) == 0)
		{
			m_serverLogWindow.IsShown = true;
			m_serverLogWindow.IsExpanded = true;
		}
		string text = "";
		string powerDef = "";
		string pc = "";
		string text2 = "";
		StringBuilder stringBuilder = new StringBuilder();
		string[] array = message.Message.Split('|');
		foreach (string text3 in array)
		{
			if (text3.Length <= 0)
			{
				continue;
			}
			if (text3.StartsWith("source="))
			{
				Match match = Regex.Match(text3, ".*source=(?<source>[^\\(]+) \\(ID=(?<entityId>[0-9]+)( CardID=(?<cardId>[^\\)]*))?\\).*");
				text = ((!match.Success) ? text3.Substring(7) : ((match.Groups["cardId"].Length <= 0) ? string.Format("{0}", match.Groups["source"]) : string.Format("{0} ({1})", match.Groups["source"], match.Groups["cardId"])));
				continue;
			}
			if (text3.StartsWith("powerDef="))
			{
				powerDef = text3.Substring(9);
			}
			else if (text3.StartsWith("pc="))
			{
				pc = text3.Substring(3);
			}
			else if (text3.StartsWith("entity="))
			{
				Match match2 = Regex.Match(text3, ".*entity=(?<source>[^\\(]+) \\(ID=(?<entityId>[0-9]+)( CardID=(?<cardId>[^\\)]*))?\\).*");
				if (match2.Success)
				{
					text2 = ((match2.Groups["cardId"].Length <= 0) ? string.Format("{0}", match2.Groups["source"]) : string.Format("{0} ({1})", match2.Groups["source"], match2.Groups["cardId"]));
				}
			}
			stringBuilder.AppendFormat("{0}|", text3);
		}
		ScriptWarning scriptWarning = new ScriptWarning((text.Length > 0) ? text : text2, message.Event, stringBuilder.ToString());
		if (message.HasSeverity)
		{
			scriptWarning.Severity = message.Severity;
		}
		scriptWarning.SetPowerDefInfo(powerDef, pc);
		scriptWarning.ComputeIssueGUID();
		m_serverLogWindow.AddEntry(scriptWarning);
		string text4 = scriptWarning.ToString();
		Log.Gameplay.PrintWarning(text4);
		UnityEngine.Debug.LogWarning(text4);
	}

	private Rect LayoutGuiControls(Rect space)
	{
		space.width = m_GUISize.x;
		space.yMax = GetScaledScreen().y;
		float yMin = space.yMin;
		Rect headerRect = m_guiWindow.GetHeaderRect();
		if (GUI.Button(new Rect(headerRect.xMax - headerRect.height, headerRect.y, headerRect.height, headerRect.height), "☰"))
		{
			m_showGuiCustomization = !m_showGuiCustomization;
		}
		if (m_showGuiCustomization)
		{
			space = LayoutCustomizeMenu(space);
		}
		space = m_timeSection.Layout(space);
		space = m_qualitySection.Layout(space);
		space = m_statsSection.Layout(space);
		m_guiWindow.ResizeToFit(space.width, space.yMin - yMin);
		return new Rect(space.xMin, space.yMax, space.width, 0f);
	}

	private void LayoutCursorDebug()
	{
		if (Options.Get() != null && PegUI.Get() != null && Options.Get().GetBool(Option.DEBUG_CURSOR) && HearthstoneApplication.IsInternal())
		{
			RaycastHit hit;
			PegUIElement pegUIElement = PegUI.Get().FindHitElement(out hit);
			string text = "none";
			UnityEngine.Object collider = hit.collider;
			if (collider != null)
			{
				text = string.Format("{0}: {1}\n{2}", collider.GetType().ToString(), DebugUtils.GetHierarchyPath(collider, '/'), (pegUIElement != null) ? "hasPegUI=true" : "hasPegUI=false");
			}
			UnityEngine.Vector2 scaledScreen = GetScaledScreen();
			GUIStyle style = new GUIStyle("box")
			{
				fontSize = GUI.skin.button.fontSize,
				fontStyle = GUI.skin.button.fontStyle,
				alignment = TextAnchor.UpperLeft,
				wordWrap = true,
				clipping = TextClipping.Overflow,
				stretchWidth = true
			};
			GUI.Box(new Rect(scaledScreen.x / 2f, 0f, scaledScreen.x / 2f, m_GUISize.y * 3f), text, style);
		}
	}

	private Rect LayoutTimeControls(Rect space)
	{
		SetDevTimescaleMultiplier(GUI.HorizontalSlider(new Rect(space.min, m_GUISize), TimeScaleMgr.Get().GetTimeScaleMultiplier(), 0.01f, 4f));
		space.yMin += 0.5f * m_GUISize.y;
		GUI.Box(new Rect(space.min, m_GUISize), $"Time Scale: {TimeScaleMgr.Get().GetTimeScaleMultiplier()}");
		space.yMin += 0.75f * m_GUISize.y;
		if (GUI.Button(new Rect(space.min, m_GUISize), "Reset Time Scale"))
		{
			SetDevTimescaleMultiplier(1f);
		}
		space.yMin += 1.1f * m_GUISize.y;
		return space;
	}

	private Rect LayoutQualityControls(Rect space)
	{
		if (GraphicsManager.Get() == null)
		{
			return space;
		}
		string text = "Low";
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			text = "<color=cyan>Low</color>";
		}
		string text2 = "Medium";
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Medium)
		{
			text2 = "<color=cyan>Medium</color>";
		}
		string text3 = "High";
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.High)
		{
			text3 = "<color=cyan>High</color>";
		}
		float num = space.width / 3f;
		if (GUI.Button(new Rect(space.xMin, space.yMin, num, m_GUISize.y), text))
		{
			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.Low;
		}
		if (GUI.Button(new Rect(space.xMin + num, space.yMin, num, m_GUISize.y), text2))
		{
			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.Medium;
		}
		if (GUI.Button(new Rect(space.xMin + num * 2f, space.yMin, num, m_GUISize.y), text3))
		{
			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.High;
		}
		space.yMin += m_GUISize.y;
		return space;
	}

	private Rect LayoutStats(Rect space)
	{
		float lineHeight = GUI.skin.box.lineHeight;
		float num = GUI.skin.box.border.vertical;
		float num2 = lineHeight + num;
		GUI.Box(new Rect(space.xMin, space.yMin, m_GUISize.x, num2), m_fpsText);
		space.yMin += num2;
		string text = string.Format("Build: {0}.{1}\nServer: {2}", "20.4", 84593, Network.GetVersion());
		num2 = 2f * lineHeight + num;
		IGameDownloadManager gameDownloadManager = GameDownloadManagerProvider.Get();
		if ((PlatformSettings.IsMobileRuntimeOS || (Application.isEditor && PlatformSettings.IsEmulating)) && gameDownloadManager != null)
		{
			string downloadOverrideString = GetDownloadOverrideString(gameDownloadManager);
			text += downloadOverrideString;
			num2 += lineHeight;
		}
		if (HearthstoneApplication.IsInternal() && m_playerId.HasValue)
		{
			text += $"\nPlayer Id: {m_playerId}";
			num2 += lineHeight;
		}
		if (!string.IsNullOrEmpty(Network.GetUsername()))
		{
			text += $"\nAccount: {Network.GetUsername().Split('@')[0]}";
			num2 += lineHeight;
		}
		GUI.Box(new Rect(space.xMin, space.yMin, m_GUISize.x, num2), text);
		space.yMin += num2;
		if (Application.isEditor && AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.ASSET_BUNDLES)
		{
			GUI.Box(new Rect(space.min, m_GUISize), "<color=red>Using Asset Bundles</color>");
			space.yMin += m_GUISize.y;
		}
		return space;
	}

	private string GetDownloadOverrideString(IGameDownloadManager downloadMgr)
	{
		string patchOverrideUrl = downloadMgr.PatchOverrideUrl;
		string versionOverrideUrl = downloadMgr.VersionOverrideUrl;
		bool flag = patchOverrideUrl.Equals("Live");
		bool flag2 = versionOverrideUrl.Equals("Live");
		if (flag && flag2)
		{
			return "\nPatch & VerSrv: Live";
		}
		string text = "";
		if (!flag)
		{
			text = $"\nPatch: {patchOverrideUrl}";
		}
		if (!flag2)
		{
			text += $"\nVersionSrv: {versionOverrideUrl}";
		}
		return text;
	}

	private Rect LayoutMessages(Rect space)
	{
		Rect rect = new Rect(space.min, m_GUISize);
		if (GUI.Button(rect, $"Clear ({m_messageWindow.GetEntries().Count((LoggerDebugWindow.LogEntry m) => m_messageWindow.AreLogsDisplayed(m.category))})"))
		{
			m_messageWindow.Clear();
		}
		rect.xMin = rect.xMax + 10f;
		Log.LogLevel[] array = new Log.LogLevel[4]
		{
			Log.LogLevel.Debug,
			Log.LogLevel.Info,
			Log.LogLevel.Warning,
			Log.LogLevel.Error
		};
		rect.width = 40f;
		GUI.Label(new Rect(rect), "Filter:");
		rect.xMin = rect.xMax;
		rect.xMax = space.xMax - 100f * (float)array.Count();
		m_messageWindow.FilterString = GUI.TextField(rect, m_messageWindow.FilterString);
		Log.LogLevel[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Log.LogLevel logLevel = array2[i];
			rect.xMin = rect.xMax;
			rect.width = 100f;
			bool flag = m_messageWindow.AreLogsDisplayed(logLevel);
			int count = m_messageWindow.GetCount(logLevel);
			string text = string.Format("<color={0}>{1} ({2})</color>", flag ? "white" : "grey", logLevel.ToString(), count);
			if (GUI.Button(rect, text))
			{
				m_messageWindow.ToggleLogsDisplay(logLevel, !flag);
			}
		}
		space.yMin = rect.yMax;
		return m_messageWindow.LayoutLog(space);
	}

	private Rect LayoutRankDebug(Rect space)
	{
		if (NetCache.Get() == null || !NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheMedalInfo>())
		{
			return space;
		}
		NetCache.NetCacheMedalInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>();
		Map<RankDebugOption, FormatType> obj = new Map<RankDebugOption, FormatType>
		{
			{
				RankDebugOption.STANDARD,
				FormatType.FT_STANDARD
			},
			{
				RankDebugOption.WILD,
				FormatType.FT_WILD
			},
			{
				RankDebugOption.CLASSIC,
				FormatType.FT_CLASSIC
			}
		};
		RankDebugOption @enum = Options.Get().GetEnum<RankDebugOption>(Option.RANK_DEBUG);
		if (!obj.TryGetValue(@enum, out var value))
		{
			value = FormatType.FT_STANDARD;
		}
		MedalInfoData medalInfoData = netObject.GetMedalInfoData(value);
		if (medalInfoData == null)
		{
			UnityEngine.Debug.LogError("SceneDebugger.LayoutRankDebug could not get medal data for " + value.ToString() + ". Using default values for statStr instead");
			medalInfoData = new MedalInfoData();
			medalInfoData.FormatType = value;
		}
		if (!new Map<FormatType, string>
		{
			{
				FormatType.FT_STANDARD,
				"STANDARD"
			},
			{
				FormatType.FT_WILD,
				"WILD"
			},
			{
				FormatType.FT_CLASSIC,
				"CLASSIC"
			}
		}.TryGetValue(value, out var value2))
		{
			value2 = "UNKNOWN FORMAT " + value;
		}
		if (medalInfoData.HasRatingId)
		{
			value2 += $"\nRating ID: {medalInfoData.RatingId}";
		}
		if (medalInfoData.HasSeasonId)
		{
			value2 += $"\nSeason ID: {medalInfoData.SeasonId}";
		}
		value2 += "\n";
		value2 += $"\nLeague ID: {medalInfoData.LeagueId}";
		value2 += $"\nStar Level: {medalInfoData.StarLevel}";
		value2 += $"\nStars: {medalInfoData.Stars}";
		value2 += "\n";
		if (medalInfoData.HasStarsPerWin)
		{
			value2 += $"\nStars Per Win: {medalInfoData.StarsPerWin}";
		}
		value2 += "\n";
		if (medalInfoData.HasRating)
		{
			value2 += $"\nRating: {medalInfoData.Rating}";
		}
		if (medalInfoData.HasVariance)
		{
			value2 += $"\nVariance: {medalInfoData.Variance}";
		}
		value2 += "\n";
		if (medalInfoData.HasSeasonGames)
		{
			value2 += $"\nGames: {medalInfoData.SeasonGames}";
		}
		value2 += $"\nWins: {medalInfoData.SeasonWins}";
		value2 += $"\nStreak: {medalInfoData.Streak}";
		value2 += "\n";
		if (medalInfoData.HasBestStarLevel)
		{
			value2 += $"\nBest Star Level: {medalInfoData.BestStarLevel}";
		}
		if (medalInfoData.HasBestStars)
		{
			value2 += $"\nBest Stars: {medalInfoData.BestStars}";
		}
		value2 += "\n";
		if (medalInfoData.HasBestEverLeagueId)
		{
			value2 += $"\nBest Ever League ID: {medalInfoData.BestEverLeagueId}";
		}
		if (medalInfoData.HasBestEverStarLevel)
		{
			value2 += $"\nBest Ever Star Level: {medalInfoData.BestEverStarLevel}";
		}
		value2 += "\n";
		if (medalInfoData.HasBestRating)
		{
			value2 += $"\nBest Rating: {medalInfoData.BestRating}";
		}
		if (medalInfoData.HasPublicRating)
		{
			value2 += $"\nPublic Rating: {medalInfoData.PublicRating}";
		}
		if (medalInfoData.HasRatingAdjustment)
		{
			value2 += $"\nRating Adjustment: {medalInfoData.RatingAdjustment}";
		}
		if (medalInfoData.HasRatingAdjustmentWins)
		{
			value2 += $"\nRating Adjustment Wins: {medalInfoData.RatingAdjustmentWins}";
		}
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.box);
		gUIStyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(value2);
		space.height = gUIStyle.CalcHeight(content, space.width);
		GUI.Box(space, value2, gUIStyle);
		float y = m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(value2);
		}
		space.yMax += y;
		m_rankWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	private Rect LayoutAssetsDebug(Rect space)
	{
		space = AssetLoaderDebug.LayoutUI(space);
		m_assetsWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		return space;
	}

	private Rect LayoutGameplayDebug(Rect space)
	{
		space = GameplayDebug.LayoutUI(space);
		m_gameplayWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		return space;
	}

	private Rect LayoutQuestDebug(Rect space)
	{
		string text = QuestManager.Get()?.GetQuestDebugHudString() ?? string.Empty;
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.box);
		gUIStyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(text);
		space.height = gUIStyle.CalcHeight(content, space.width);
		GUI.Box(space, text, gUIStyle);
		float y = m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(text);
		}
		space.yMax += y;
		m_questWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	private Rect LayoutAchievementDebug(Rect space)
	{
		string text = AchievementManager.Get()?.Debug_GetAchievementHudString() ?? string.Empty;
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.box);
		gUIStyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(text);
		space.height = gUIStyle.CalcHeight(content, space.width);
		GUI.Box(space, text, gUIStyle);
		float y = m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(text);
		}
		space.yMax += y;
		m_achievementWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	private Rect LayoutRewardTrackDebug(Rect space)
	{
		string text = RewardTrackManager.Get()?.GetRewardTrackDebugHudString() ?? string.Empty;
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.box);
		gUIStyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(text);
		space.height = gUIStyle.CalcHeight(content, space.width);
		GUI.Box(space, text, gUIStyle);
		float y = m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(text);
		}
		space.yMax += y;
		m_rewardTrackWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	private Rect LayoutNotepadDebug(Rect space)
	{
		GUIStyle gUIStyle = new GUIStyle(GUI.skin.box);
		gUIStyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent("");
		space.height = gUIStyle.CalcHeight(content, space.width);
		string path = Directory.GetCurrentDirectory() + "\\notepad.txt";
		if (m_notepadFirstRun)
		{
			if (!File.Exists(path))
			{
				File.Create(path).Close();
			}
			else
			{
				m_notepadContents = File.ReadAllText(path);
			}
			m_notepadFirstRun = false;
		}
		GUILayout.BeginArea(new Rect(space.xMin, space.yMax, space.width, 300f));
		GUILayout.BeginVertical();
		scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
		m_notepadContents = GUILayout.TextArea(m_notepadContents, GUILayout.ExpandHeight(expand: true));
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal();
		float y = m_GUISize.y;
		if (GUILayout.Button("Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(m_notepadContents);
		}
		if (GUILayout.Button("Save Contents"))
		{
			File.WriteAllText(path, m_notepadContents);
		}
		space.yMax += y;
		space.yMin = space.yMax;
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUILayout.EndArea();
		return space;
	}

	[Conditional("SOUND_DUCKING_DEBUG")]
	private void DrawDuckingInformation(ref Rect space)
	{
		Dictionary<Global.SoundCategory, float> dictionary = SoundManager.Get().DuckingLevels();
		Global.SoundCategory[] array = (Global.SoundCategory[])Enum.GetValues(typeof(Global.SoundCategory));
		foreach (Global.SoundCategory soundCategory in array)
		{
			GUI.Box(new Rect(space.min, m_GUISize), string.Concat(soundCategory, ": ", dictionary[soundCategory].ToString("0.00")));
			space.yMin += 1f * m_GUISize.y;
		}
	}

	[Conditional("UNITY_EDITOR")]
	private void LayoutCursorControls(ref Rect space)
	{
		string text = "Force Hardware Cursor " + (Cursor.visible ? "Off" : "On");
		if (GUI.Button(new Rect(space.min, m_GUISize), text))
		{
			Cursor.visible = !Cursor.visible;
		}
		space.yMin += 1.5f * m_GUISize.y;
	}

	private Rect LayoutScriptWarnings(Rect space)
	{
		UnityEngine.Vector2 min = space.min;
		UnityEngine.Vector2 gUISize = m_GUISize;
		if (GUI.Button(new Rect(min.x, min.y, gUISize.x, gUISize.y), "Clear Script Warnings"))
		{
			m_serverLogWindow.Clear();
		}
		min.x += gUISize.x;
		if (GUI.Button(new Rect(min.x, min.y, gUISize.x, gUISize.y), "Search JIRA for GUID"))
		{
			ScriptWarning scriptWarning = m_serverLogWindow.GetEntries().LastOrDefault() as ScriptWarning;
			if (scriptWarning != null)
			{
				string arg = UnityWebRequest.EscapeURL(scriptWarning.IssueGUID);
				Application.OpenURL($"https://jira.blizzard.com/issues/?jql=text~%22{arg}%22");
			}
		}
		min.x += gUISize.x;
		min.y += gUISize.y;
		space.yMin = min.y;
		return m_serverLogWindow.LayoutLog(space);
	}

	private void LayoutButton(ref UnityEngine.Vector2 offset, float top, UnityEngine.Vector2 size, string label, Action action)
	{
		if (offset.y + size.y > GetScaledScreen().y)
		{
			offset.y = top;
			offset.x += 1.1f * size.x;
		}
		if (GUI.Button(new Rect(offset.x, offset.y, size.x, size.y), label))
		{
			action();
		}
		offset.y += 1.1f * size.y;
	}

	private Rect LayoutCustomizeMenu(Rect space)
	{
		List<DebuggerGui> obj = new List<DebuggerGui>
		{
			m_cheatsWindow, m_messageWindow, m_serverLogWindow, m_rankWindow, m_assetsWindow, m_questWindow, m_achievementWindow, m_rewardTrackWindow, m_timeSection, m_qualitySection,
			m_statsSection, m_slushTrackerWindow, m_notepadWindow, m_gameplayWindow
		};
		UnityEngine.Vector2 offset = space.min;
		foreach (DebuggerGui section in obj)
		{
			string label = (section.IsShown ? "☑" : "☐") + " " + section.Title;
			LayoutButton(ref offset, 0f, m_GUISize, label, delegate
			{
				section.IsShown = !section.IsShown;
			});
		}
		space.yMin = offset.y;
		return space;
	}

	private void HandleGuiChanged()
	{
		m_guiSaveTimer = 3;
	}

	public string GetLastScriptWarning()
	{
		ScriptWarning scriptWarning = m_serverLogWindow.GetEntries().LastOrDefault() as ScriptWarning;
		if (scriptWarning == null)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("-Event: ");
		stringBuilder.AppendLine(scriptWarning.Event);
		stringBuilder.Append("-Source: ");
		stringBuilder.AppendLine(scriptWarning.Source);
		stringBuilder.Append("-PowerDef: ");
		stringBuilder.AppendLine(scriptWarning.PowerDef);
		stringBuilder.Append("-PC: ");
		stringBuilder.AppendLine(scriptWarning.PC.ToString());
		stringBuilder.Append("-Guid: ");
		stringBuilder.AppendLine(scriptWarning.IssueGUID);
		return stringBuilder.ToString();
	}
}
