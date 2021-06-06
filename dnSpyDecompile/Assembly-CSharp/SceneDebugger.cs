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

// Token: 0x0200068A RID: 1674
public class SceneDebugger : IService, IHasUpdate
{
	// Token: 0x06005D9F RID: 23967 RVA: 0x001E71E8 File Offset: 0x001E53E8
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		TimeScaleMgr.Get().SetTimeScaleMultiplier(SceneDebugger.GetDevTimescaleMultiplier());
		UnityEngine.Vector2 scaledScreen = this.GetScaledScreen();
		this.m_guiWindow = new DebuggerGuiWindow("Scene Debugger", new DebuggerGui.LayoutGui(this.LayoutGuiControls), false, false);
		this.m_guiWindow.Position = new UnityEngine.Vector2(scaledScreen.x * 0.05f, scaledScreen.y * 0.125f);
		this.m_timeSection = new DebuggerGui("Time Scale", new DebuggerGui.LayoutGui(this.LayoutTimeControls), true, false);
		this.m_qualitySection = new DebuggerGui("Quality", new DebuggerGui.LayoutGui(this.LayoutQualityControls), true, false);
		this.m_statsSection = new DebuggerGui("Stats", new DebuggerGui.LayoutGui(this.LayoutStats), true, false);
		this.m_cheatsWindow = new CheatsDebugWindow(this.m_GUISize);
		this.m_cheatsWindow.Position = new UnityEngine.Vector2(scaledScreen.x * 0.5f, 0f);
		this.m_cheatsWindow.ResizeToFit(scaledScreen.x * 0.5f, scaledScreen.y * 0.5f);
		this.m_cheatsWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_messageWindow = new LoggerDebugWindow("Messages", this.m_GUISize, Enum.GetValues(typeof(Log.LogLevel)).Cast<object>());
		this.m_messageWindow.CustomLayout = new DebuggerGui.LayoutGui(this.LayoutMessages);
		this.m_messageWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_messageWindow.Position = new UnityEngine.Vector2(0f, 0.65f * scaledScreen.y - 35f);
		this.m_messageWindow.ResizeToFit(scaledScreen.x, scaledScreen.y * 0.35f);
		this.m_serverLogWindow = new LoggerDebugWindow("Server Script Log", this.m_GUISize, Enum.GetValues(typeof(ServerLogs.ServerLogLevel)).Cast<object>());
		this.m_serverLogWindow.CustomLayout = new DebuggerGui.LayoutGui(this.LayoutScriptWarnings);
		this.m_serverLogWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_serverLogWindow.Position = new UnityEngine.Vector2(0f, 0.65f * scaledScreen.y - 35f);
		this.m_serverLogWindow.ResizeToFit(scaledScreen.x, scaledScreen.y * 0.35f);
		this.m_rankWindow = new DebuggerGuiWindow("Rank", new DebuggerGui.LayoutGui(this.LayoutRankDebug), true, false);
		this.m_rankWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_rankWindow.Position = new UnityEngine.Vector2(scaledScreen.x - this.m_GUISize.x, 0.5f * scaledScreen.y);
		this.m_rankWindow.ResizeToFit(this.m_GUISize.x, this.m_GUISize.y);
		this.m_assetsWindow = new DebuggerGuiWindow("Assets", new DebuggerGui.LayoutGui(this.LayoutAssetsDebug), true, true);
		this.m_assetsWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_assetsWindow.Position = new UnityEngine.Vector2(scaledScreen.x - this.m_GUISize.x, 0.5f * scaledScreen.y);
		this.m_assetsWindow.ResizeToFit(this.m_GUISize.x, this.m_GUISize.y);
		this.m_gameplayWindow = new DebuggerGuiWindow("Gameplay", new DebuggerGui.LayoutGui(this.LayoutGameplayDebug), true, true);
		this.m_gameplayWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_gameplayWindow.Position = new UnityEngine.Vector2(scaledScreen.x - this.m_GUISize.x, 0.5f * scaledScreen.y);
		this.m_gameplayWindow.ResizeToFit(this.m_GUISize.x, this.m_GUISize.y);
		float num = this.m_GUISize.x * 2f;
		this.m_questWindow = new DebuggerGuiWindow("Quest", new DebuggerGui.LayoutGui(this.LayoutQuestDebug), true, false);
		this.m_questWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_questWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num, 0.5f * scaledScreen.y);
		this.m_questWindow.ResizeToFit(num, this.m_GUISize.y);
		float num2 = this.m_GUISize.x * 2.5f;
		this.m_achievementWindow = new DebuggerGuiWindow("Achievement", new DebuggerGui.LayoutGui(this.LayoutAchievementDebug), true, false);
		this.m_achievementWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_achievementWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num2, 0.5f * scaledScreen.y);
		this.m_achievementWindow.ResizeToFit(num2, this.m_GUISize.y);
		float num3 = this.m_GUISize.x * 2f;
		this.m_rewardTrackWindow = new DebuggerGuiWindow("Reward Track", new DebuggerGui.LayoutGui(this.LayoutRewardTrackDebug), true, false);
		this.m_rewardTrackWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_rewardTrackWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num3, 0.5f * scaledScreen.y);
		this.m_rewardTrackWindow.ResizeToFit(num3, this.m_GUISize.y);
		this.m_slushTrackerWindow = new LoggerDebugWindow("Slush Time Log", this.m_GUISize, Enum.GetValues(typeof(Log.LogLevel)).Cast<object>());
		this.m_slushTrackerWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_slushTrackerWindow.Position = new UnityEngine.Vector2(0f, 0.65f * scaledScreen.y - 35f);
		this.m_slushTrackerWindow.ResizeToFit(scaledScreen.x, scaledScreen.y * 0.35f);
		float num4 = this.m_GUISize.x * 2f;
		this.m_notepadWindow = new DebuggerGuiWindow("Notepad", new DebuggerGui.LayoutGui(this.LayoutNotepadDebug), true, true);
		this.m_notepadWindow.collapsedWidth = new float?(this.m_GUISize.x);
		this.m_notepadWindow.Position = new UnityEngine.Vector2(scaledScreen.x - num4, 0.5f * scaledScreen.y);
		this.m_notepadWindow.ResizeToFit(num4, this.m_GUISize.y);
		this.m_debuggerGui = new List<DebuggerGui>();
		this.m_debuggerGui.Add(this.m_guiWindow);
		this.m_debuggerGui.Add(this.m_cheatsWindow);
		this.m_debuggerGui.Add(this.m_messageWindow);
		this.m_debuggerGui.Add(this.m_serverLogWindow);
		this.m_debuggerGui.Add(this.m_rankWindow);
		this.m_debuggerGui.Add(this.m_assetsWindow);
		this.m_debuggerGui.Add(this.m_questWindow);
		this.m_debuggerGui.Add(this.m_achievementWindow);
		this.m_debuggerGui.Add(this.m_rewardTrackWindow);
		this.m_debuggerGui.Add(this.m_timeSection);
		this.m_debuggerGui.Add(this.m_qualitySection);
		this.m_debuggerGui.Add(this.m_statsSection);
		this.m_debuggerGui.Add(this.m_slushTrackerWindow);
		this.m_debuggerGui.Add(this.m_notepadWindow);
		this.m_debuggerGui.Add(this.m_gameplayWindow);
		foreach (DebuggerGui debuggerGui in this.m_debuggerGui)
		{
			debuggerGui.OnChanged += this.HandleGuiChanged;
		}
		this.m_guiWindow.IsShown = true;
		this.m_cheatsWindow.IsShown = false;
		this.m_messageWindow.IsShown = false;
		this.m_serverLogWindow.IsShown = false;
		this.m_rankWindow.IsShown = false;
		this.m_assetsWindow.IsShown = false;
		this.m_gameplayWindow.IsShown = false;
		this.m_questWindow.IsShown = false;
		this.m_achievementWindow.IsShown = false;
		this.m_rewardTrackWindow.IsShown = false;
		this.m_slushTrackerWindow.IsShown = false;
		this.m_notepadWindow.IsShown = false;
		DebuggerGui.LoadConfig(this.m_debuggerGui);
		Processor.RegisterOnGUIDelegate(new Action(this.OnGUI));
		yield break;
	}

	// Token: 0x06005DA0 RID: 23968 RVA: 0x001E71F7 File Offset: 0x001E53F7
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(GraphicsManager)
		};
	}

	// Token: 0x06005DA1 RID: 23969 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06005DA2 RID: 23970 RVA: 0x001E720C File Offset: 0x001E540C
	public bool IsMouseOverGui()
	{
		if (!Options.Get().GetBool(global::Option.HUD))
		{
			return false;
		}
		foreach (DebuggerGui debuggerGui in from g in this.m_debuggerGui
		where g is DebuggerGuiWindow
		select g)
		{
			DebuggerGuiWindow debuggerGuiWindow = (DebuggerGuiWindow)debuggerGui;
			if (debuggerGuiWindow != null && debuggerGui.IsShown && debuggerGuiWindow.IsMouseOver())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005DA3 RID: 23971 RVA: 0x001E72A8 File Offset: 0x001E54A8
	public void Update()
	{
		this.m_frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if ((double)realtimeSinceStartup > this.m_LastInterval + (double)this.m_UpdateInterval)
		{
			float num = (float)this.m_frames / (float)((double)realtimeSinceStartup - this.m_LastInterval);
			this.m_fpsText = string.Format("{0}  FPS: {1:f2}\n", SystemInfo.graphicsDeviceType, num);
			this.m_frames = 0;
			this.m_LastInterval = (double)Time.realtimeSinceStartup;
		}
		if (this.m_testMessaging)
		{
			string text = "abcdefghijklmnopqrstuvwxyz0123456789";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 5000; i++)
			{
				char value = text[UnityEngine.Random.Range(0, text.Length)];
				stringBuilder.Append(value);
			}
			this.AddErrorMessage(stringBuilder.ToString());
		}
	}

	// Token: 0x06005DA4 RID: 23972 RVA: 0x001E7374 File Offset: 0x001E5574
	private void OnGUI()
	{
		if (ScriptDebugDisplay.Get().m_isDisplayed)
		{
			return;
		}
		if (Options.Get().GetBool(global::Option.HUD))
		{
			float guiScaling = this.GetGuiScaling();
			GUI.matrix = Matrix4x4.Scale(new Vector3(guiScaling, guiScaling, guiScaling));
			if (GameState.Get() != null && GameState.Get().GetSlushTimeTracker().GetAccruedLostTimeInSeconds() > (float)GameplayDebug.LOST_SLUSH_TIME_ERROR_THRESHOLD_SECONDS)
			{
				this.m_gameplayWindow.IsShown = true;
			}
			this.m_guiWindow.Layout();
			this.m_cheatsWindow.Layout();
			this.m_messageWindow.Layout();
			this.m_serverLogWindow.Layout();
			this.m_rankWindow.Layout();
			this.m_assetsWindow.Layout();
			this.m_gameplayWindow.Layout();
			this.m_questWindow.Layout();
			this.m_achievementWindow.Layout();
			this.m_rewardTrackWindow.Layout();
			this.m_slushTrackerWindow.Layout();
			this.m_notepadWindow.Layout();
			this.LayoutCursorDebug();
			if (this.m_guiSaveTimer > 0)
			{
				this.m_guiSaveTimer--;
				if (this.m_guiSaveTimer == 0)
				{
					DebuggerGui.SaveConfig(this.m_debuggerGui);
				}
			}
		}
	}

	// Token: 0x06005DA5 RID: 23973 RVA: 0x001E7494 File Offset: 0x001E5694
	private float GetGuiScaling()
	{
		float b = 1f;
		GeneralUtils.TryParseFloat(Options.Get().GetOption(global::Option.HUD_SCALE).ToString(), out b);
		float num = (float)Screen.height;
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
		return Mathf.Max(0.1f, b) * Mathf.Max(1f, (float)Screen.height / num);
	}

	// Token: 0x06005DA6 RID: 23974 RVA: 0x001E7517 File Offset: 0x001E5717
	private UnityEngine.Vector2 GetScaledScreen()
	{
		return new UnityEngine.Vector2((float)Screen.width, (float)Screen.height) / this.GetGuiScaling();
	}

	// Token: 0x06005DA7 RID: 23975 RVA: 0x001E7535 File Offset: 0x001E5735
	public static SceneDebugger Get()
	{
		return HearthstoneServices.Get<SceneDebugger>();
	}

	// Token: 0x06005DA8 RID: 23976 RVA: 0x001E753C File Offset: 0x001E573C
	[ContextMenu("Test Messaging")]
	public void TestMessaging()
	{
		this.m_testMessaging = true;
	}

	// Token: 0x06005DA9 RID: 23977 RVA: 0x001E7545 File Offset: 0x001E5745
	public static float GetDevTimescaleMultiplier()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return 1f;
		}
		return Options.Get().GetFloat(global::Option.DEV_TIMESCALE, 1f);
	}

	// Token: 0x06005DAA RID: 23978 RVA: 0x001E7565 File Offset: 0x001E5765
	public static void SetDevTimescaleMultiplier(float f)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		if (f == TimeScaleMgr.Get().GetTimeScaleMultiplier())
		{
			return;
		}
		if (f == 0f)
		{
			f = 0.0001f;
		}
		Options.Get().SetFloat(global::Option.DEV_TIMESCALE, f);
		TimeScaleMgr.Get().SetTimeScaleMultiplier(f);
	}

	// Token: 0x06005DAB RID: 23979 RVA: 0x001E75A4 File Offset: 0x001E57A4
	public void SetPlayerId(long? playerId)
	{
		this.m_playerId = playerId;
	}

	// Token: 0x06005DAC RID: 23980 RVA: 0x001E75AD File Offset: 0x001E57AD
	public long? GetPlayerId_DebugOnly()
	{
		return this.m_playerId;
	}

	// Token: 0x06005DAD RID: 23981 RVA: 0x001E75B5 File Offset: 0x001E57B5
	public void AddMessage(string message)
	{
		this.AddMessage(Log.LogLevel.Info, message, false);
	}

	// Token: 0x06005DAE RID: 23982 RVA: 0x001E75C0 File Offset: 0x001E57C0
	public void AddMessage(Log.LogLevel level, string message, bool autoShow = false)
	{
		this.m_messageWindow.AddEntry(new SceneDebugger.ConsoleLogEntry(level, message), autoShow);
	}

	// Token: 0x06005DAF RID: 23983 RVA: 0x001E75D5 File Offset: 0x001E57D5
	public void AddErrorMessage(string message)
	{
		this.AddMessage(Log.LogLevel.Error, message, false);
	}

	// Token: 0x06005DB0 RID: 23984 RVA: 0x001E75E0 File Offset: 0x001E57E0
	public void AddSlushTimeEntry(int taskId, float expectedStart, float expectedEnd, float actualStart = 0f, float actualEnd = 0f, int entityId = 0)
	{
		this.m_slushTrackerWindow.AddEntry(new SceneDebugger.SlushTimeRecord(taskId, expectedStart, expectedEnd, actualStart, actualEnd, entityId), false);
	}

	// Token: 0x06005DB1 RID: 23985 RVA: 0x001E75FC File Offset: 0x001E57FC
	public void AddServerScriptLogMessage(ScriptLogMessage message)
	{
		int minSeverity = 3;
		if (message.Severity >= minSeverity && this.m_serverLogWindow.GetEntries().Count((LoggerDebugWindow.LogEntry m) => (m as SceneDebugger.ScriptWarning).Severity >= minSeverity) == 0)
		{
			this.m_serverLogWindow.IsShown = true;
			this.m_serverLogWindow.IsExpanded = true;
		}
		string text = "";
		string powerDef = "";
		string pc = "";
		string text2 = "";
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string text3 in message.Message.Split(new char[]
		{
			'|'
		}))
		{
			if (text3.Length > 0)
			{
				if (text3.StartsWith("source="))
				{
					Match match = Regex.Match(text3, ".*source=(?<source>[^\\(]+) \\(ID=(?<entityId>[0-9]+)( CardID=(?<cardId>[^\\)]*))?\\).*");
					if (match.Success)
					{
						if (match.Groups["cardId"].Length > 0)
						{
							text = string.Format("{0} ({1})", match.Groups["source"], match.Groups["cardId"]);
						}
						else
						{
							text = string.Format("{0}", match.Groups["source"]);
						}
					}
					else
					{
						text = text3.Substring(7);
					}
				}
				else
				{
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
							if (match2.Groups["cardId"].Length > 0)
							{
								text2 = string.Format("{0} ({1})", match2.Groups["source"], match2.Groups["cardId"]);
							}
							else
							{
								text2 = string.Format("{0}", match2.Groups["source"]);
							}
						}
					}
					stringBuilder.AppendFormat("{0}|", text3);
				}
			}
		}
		SceneDebugger.ScriptWarning scriptWarning = new SceneDebugger.ScriptWarning((text.Length > 0) ? text : text2, message.Event, stringBuilder.ToString());
		if (message.HasSeverity)
		{
			scriptWarning.Severity = message.Severity;
		}
		scriptWarning.SetPowerDefInfo(powerDef, pc);
		scriptWarning.ComputeIssueGUID();
		this.m_serverLogWindow.AddEntry(scriptWarning, false);
		string text4 = scriptWarning.ToString();
		Log.Gameplay.PrintWarning(text4, Array.Empty<object>());
		UnityEngine.Debug.LogWarning(text4);
	}

	// Token: 0x06005DB2 RID: 23986 RVA: 0x001E78AC File Offset: 0x001E5AAC
	private Rect LayoutGuiControls(Rect space)
	{
		space.width = this.m_GUISize.x;
		space.yMax = this.GetScaledScreen().y;
		float yMin = space.yMin;
		Rect headerRect = this.m_guiWindow.GetHeaderRect();
		if (GUI.Button(new Rect(headerRect.xMax - headerRect.height, headerRect.y, headerRect.height, headerRect.height), "☰"))
		{
			this.m_showGuiCustomization = !this.m_showGuiCustomization;
		}
		if (this.m_showGuiCustomization)
		{
			space = this.LayoutCustomizeMenu(space);
		}
		space = this.m_timeSection.Layout(space);
		space = this.m_qualitySection.Layout(space);
		space = this.m_statsSection.Layout(space);
		this.m_guiWindow.ResizeToFit(space.width, space.yMin - yMin);
		return new Rect(space.xMin, space.yMax, space.width, 0f);
	}

	// Token: 0x06005DB3 RID: 23987 RVA: 0x001E79AC File Offset: 0x001E5BAC
	private void LayoutCursorDebug()
	{
		if (Options.Get() != null && PegUI.Get() != null && Options.Get().GetBool(global::Option.DEBUG_CURSOR) && HearthstoneApplication.IsInternal())
		{
			RaycastHit raycastHit;
			PegUIElement x = PegUI.Get().FindHitElement(out raycastHit);
			string text = "none";
			UnityEngine.Object collider = raycastHit.collider;
			if (collider != null)
			{
				text = string.Format("{0}: {1}\n{2}", collider.GetType().ToString(), DebugUtils.GetHierarchyPath(collider, '/'), (x != null) ? "hasPegUI=true" : "hasPegUI=false");
			}
			UnityEngine.Vector2 scaledScreen = this.GetScaledScreen();
			GUIStyle style = new GUIStyle("box")
			{
				fontSize = GUI.skin.button.fontSize,
				fontStyle = GUI.skin.button.fontStyle,
				alignment = TextAnchor.UpperLeft,
				wordWrap = true,
				clipping = TextClipping.Overflow,
				stretchWidth = true
			};
			GUI.Box(new Rect(scaledScreen.x / 2f, 0f, scaledScreen.x / 2f, this.m_GUISize.y * 3f), text, style);
		}
	}

	// Token: 0x06005DB4 RID: 23988 RVA: 0x001E7AE4 File Offset: 0x001E5CE4
	private Rect LayoutTimeControls(Rect space)
	{
		SceneDebugger.SetDevTimescaleMultiplier(GUI.HorizontalSlider(new Rect(space.min, this.m_GUISize), TimeScaleMgr.Get().GetTimeScaleMultiplier(), 0.01f, 4f));
		space.yMin += 0.5f * this.m_GUISize.y;
		GUI.Box(new Rect(space.min, this.m_GUISize), string.Format("Time Scale: {0}", TimeScaleMgr.Get().GetTimeScaleMultiplier()));
		space.yMin += 0.75f * this.m_GUISize.y;
		if (GUI.Button(new Rect(space.min, this.m_GUISize), "Reset Time Scale"))
		{
			SceneDebugger.SetDevTimescaleMultiplier(1f);
		}
		space.yMin += 1.1f * this.m_GUISize.y;
		return space;
	}

	// Token: 0x06005DB5 RID: 23989 RVA: 0x001E7BD8 File Offset: 0x001E5DD8
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
		if (GUI.Button(new Rect(space.xMin, space.yMin, num, this.m_GUISize.y), text))
		{
			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.Low;
		}
		if (GUI.Button(new Rect(space.xMin + num, space.yMin, num, this.m_GUISize.y), text2))
		{
			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.Medium;
		}
		if (GUI.Button(new Rect(space.xMin + num * 2f, space.yMin, num, this.m_GUISize.y), text3))
		{
			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.High;
		}
		space.yMin += this.m_GUISize.y;
		return space;
	}

	// Token: 0x06005DB6 RID: 23990 RVA: 0x001E7D00 File Offset: 0x001E5F00
	private Rect LayoutStats(Rect space)
	{
		float lineHeight = GUI.skin.box.lineHeight;
		float num = (float)GUI.skin.box.border.vertical;
		float num2 = lineHeight + num;
		GUI.Box(new Rect(space.xMin, space.yMin, this.m_GUISize.x, num2), this.m_fpsText);
		space.yMin += num2;
		string text = string.Format("Build: {0}.{1}\nServer: {2}", "20.4", 84593, Network.GetVersion());
		num2 = 2f * lineHeight + num;
		IGameDownloadManager gameDownloadManager = GameDownloadManagerProvider.Get();
		if ((PlatformSettings.IsMobileRuntimeOS || (Application.isEditor && PlatformSettings.IsEmulating)) && gameDownloadManager != null)
		{
			string downloadOverrideString = this.GetDownloadOverrideString(gameDownloadManager);
			text += downloadOverrideString;
			num2 += lineHeight;
		}
		if (HearthstoneApplication.IsInternal() && this.m_playerId != null)
		{
			text += string.Format("\nPlayer Id: {0}", this.m_playerId);
			num2 += lineHeight;
		}
		if (!string.IsNullOrEmpty(Network.GetUsername()))
		{
			text += string.Format("\nAccount: {0}", Network.GetUsername().Split(new char[]
			{
				'@'
			})[0]);
			num2 += lineHeight;
		}
		GUI.Box(new Rect(space.xMin, space.yMin, this.m_GUISize.x, num2), text);
		space.yMin += num2;
		if (Application.isEditor && AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.ASSET_BUNDLES)
		{
			GUI.Box(new Rect(space.min, this.m_GUISize), "<color=red>Using Asset Bundles</color>");
			space.yMin += this.m_GUISize.y;
		}
		return space;
	}

	// Token: 0x06005DB7 RID: 23991 RVA: 0x001E7EB8 File Offset: 0x001E60B8
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
			text = string.Format("\nPatch: {0}", patchOverrideUrl);
		}
		if (!flag2)
		{
			text += string.Format("\nVersionSrv: {0}", versionOverrideUrl);
		}
		return text;
	}

	// Token: 0x06005DB8 RID: 23992 RVA: 0x001E7F28 File Offset: 0x001E6128
	private Rect LayoutMessages(Rect space)
	{
		Rect rect = new Rect(space.min, this.m_GUISize);
		if (GUI.Button(rect, string.Format("Clear ({0})", this.m_messageWindow.GetEntries().Count((LoggerDebugWindow.LogEntry m) => this.m_messageWindow.AreLogsDisplayed(m.category)))))
		{
			this.m_messageWindow.Clear();
		}
		rect.xMin = rect.xMax + 10f;
		Log.LogLevel[] array = new Log.LogLevel[]
		{
			Log.LogLevel.Debug,
			Log.LogLevel.Info,
			Log.LogLevel.Warning,
			Log.LogLevel.Error
		};
		rect.width = 40f;
		GUI.Label(new Rect(rect), "Filter:");
		rect.xMin = rect.xMax;
		rect.xMax = space.xMax - 100f * (float)array.Count<Log.LogLevel>();
		this.m_messageWindow.FilterString = GUI.TextField(rect, this.m_messageWindow.FilterString);
		foreach (Log.LogLevel logLevel in array)
		{
			rect.xMin = rect.xMax;
			rect.width = 100f;
			bool flag = this.m_messageWindow.AreLogsDisplayed(logLevel);
			int count = this.m_messageWindow.GetCount(logLevel);
			string text = string.Format("<color={0}>{1} ({2})</color>", flag ? "white" : "grey", logLevel.ToString(), count);
			if (GUI.Button(rect, text))
			{
				this.m_messageWindow.ToggleLogsDisplay(logLevel, !flag);
			}
		}
		space.yMin = rect.yMax;
		return this.m_messageWindow.LayoutLog(space);
	}

	// Token: 0x06005DB9 RID: 23993 RVA: 0x001E80D4 File Offset: 0x001E62D4
	private Rect LayoutRankDebug(Rect space)
	{
		if (NetCache.Get() == null || !NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheMedalInfo>())
		{
			return space;
		}
		NetCache.NetCacheMedalInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>();
		Map<RankDebugOption, FormatType> map = new Map<RankDebugOption, FormatType>();
		map.Add(RankDebugOption.STANDARD, FormatType.FT_STANDARD);
		map.Add(RankDebugOption.WILD, FormatType.FT_WILD);
		map.Add(RankDebugOption.CLASSIC, FormatType.FT_CLASSIC);
		RankDebugOption @enum = Options.Get().GetEnum<RankDebugOption>(global::Option.RANK_DEBUG);
		FormatType formatType;
		if (!map.TryGetValue(@enum, out formatType))
		{
			formatType = FormatType.FT_STANDARD;
		}
		MedalInfoData medalInfoData = netObject.GetMedalInfoData(formatType);
		if (medalInfoData == null)
		{
			UnityEngine.Debug.LogError("SceneDebugger.LayoutRankDebug could not get medal data for " + formatType.ToString() + ". Using default values for statStr instead");
			medalInfoData = new MedalInfoData();
			medalInfoData.FormatType = formatType;
		}
		string text;
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
		}.TryGetValue(formatType, out text))
		{
			text = "UNKNOWN FORMAT " + formatType.ToString();
		}
		if (medalInfoData.HasRatingId)
		{
			text += string.Format("\nRating ID: {0}", medalInfoData.RatingId);
		}
		if (medalInfoData.HasSeasonId)
		{
			text += string.Format("\nSeason ID: {0}", medalInfoData.SeasonId);
		}
		text += "\n";
		text += string.Format("\nLeague ID: {0}", medalInfoData.LeagueId);
		text += string.Format("\nStar Level: {0}", medalInfoData.StarLevel);
		text += string.Format("\nStars: {0}", medalInfoData.Stars);
		text += "\n";
		if (medalInfoData.HasStarsPerWin)
		{
			text += string.Format("\nStars Per Win: {0}", medalInfoData.StarsPerWin);
		}
		text += "\n";
		if (medalInfoData.HasRating)
		{
			text += string.Format("\nRating: {0}", medalInfoData.Rating);
		}
		if (medalInfoData.HasVariance)
		{
			text += string.Format("\nVariance: {0}", medalInfoData.Variance);
		}
		text += "\n";
		if (medalInfoData.HasSeasonGames)
		{
			text += string.Format("\nGames: {0}", medalInfoData.SeasonGames);
		}
		text += string.Format("\nWins: {0}", medalInfoData.SeasonWins);
		text += string.Format("\nStreak: {0}", medalInfoData.Streak);
		text += "\n";
		if (medalInfoData.HasBestStarLevel)
		{
			text += string.Format("\nBest Star Level: {0}", medalInfoData.BestStarLevel);
		}
		if (medalInfoData.HasBestStars)
		{
			text += string.Format("\nBest Stars: {0}", medalInfoData.BestStars);
		}
		text += "\n";
		if (medalInfoData.HasBestEverLeagueId)
		{
			text += string.Format("\nBest Ever League ID: {0}", medalInfoData.BestEverLeagueId);
		}
		if (medalInfoData.HasBestEverStarLevel)
		{
			text += string.Format("\nBest Ever Star Level: {0}", medalInfoData.BestEverStarLevel);
		}
		text += "\n";
		if (medalInfoData.HasBestRating)
		{
			text += string.Format("\nBest Rating: {0}", medalInfoData.BestRating);
		}
		if (medalInfoData.HasPublicRating)
		{
			text += string.Format("\nPublic Rating: {0}", medalInfoData.PublicRating);
		}
		if (medalInfoData.HasRatingAdjustment)
		{
			text += string.Format("\nRating Adjustment: {0}", medalInfoData.RatingAdjustment);
		}
		if (medalInfoData.HasRatingAdjustmentWins)
		{
			text += string.Format("\nRating Adjustment Wins: {0}", medalInfoData.RatingAdjustmentWins);
		}
		GUIStyle guistyle = new GUIStyle(GUI.skin.box);
		guistyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(text);
		space.height = guistyle.CalcHeight(content, space.width);
		GUI.Box(space, text, guistyle);
		float y = this.m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(text);
		}
		space.yMax += y;
		this.m_rankWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	// Token: 0x06005DBA RID: 23994 RVA: 0x001E854A File Offset: 0x001E674A
	private Rect LayoutAssetsDebug(Rect space)
	{
		space = AssetLoaderDebug.LayoutUI(space);
		this.m_assetsWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		return space;
	}

	// Token: 0x06005DBB RID: 23995 RVA: 0x001E8573 File Offset: 0x001E6773
	private Rect LayoutGameplayDebug(Rect space)
	{
		space = GameplayDebug.LayoutUI(space);
		this.m_gameplayWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		return space;
	}

	// Token: 0x06005DBC RID: 23996 RVA: 0x001E859C File Offset: 0x001E679C
	private Rect LayoutQuestDebug(Rect space)
	{
		QuestManager questManager = QuestManager.Get();
		string text = ((questManager != null) ? questManager.GetQuestDebugHudString() : null) ?? string.Empty;
		GUIStyle guistyle = new GUIStyle(GUI.skin.box);
		guistyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(text);
		space.height = guistyle.CalcHeight(content, space.width);
		GUI.Box(space, text, guistyle);
		float y = this.m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(text);
		}
		space.yMax += y;
		this.m_questWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	// Token: 0x06005DBD RID: 23997 RVA: 0x001E8674 File Offset: 0x001E6874
	private Rect LayoutAchievementDebug(Rect space)
	{
		AchievementManager achievementManager = AchievementManager.Get();
		string text = ((achievementManager != null) ? achievementManager.Debug_GetAchievementHudString() : null) ?? string.Empty;
		GUIStyle guistyle = new GUIStyle(GUI.skin.box);
		guistyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(text);
		space.height = guistyle.CalcHeight(content, space.width);
		GUI.Box(space, text, guistyle);
		float y = this.m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(text);
		}
		space.yMax += y;
		this.m_achievementWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	// Token: 0x06005DBE RID: 23998 RVA: 0x001E874C File Offset: 0x001E694C
	private Rect LayoutRewardTrackDebug(Rect space)
	{
		RewardTrackManager rewardTrackManager = RewardTrackManager.Get();
		string text = ((rewardTrackManager != null) ? rewardTrackManager.GetRewardTrackDebugHudString() : null) ?? string.Empty;
		GUIStyle guistyle = new GUIStyle(GUI.skin.box);
		guistyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent(text);
		space.height = guistyle.CalcHeight(content, space.width);
		GUI.Box(space, text, guistyle);
		float y = this.m_GUISize.y;
		if (GUI.Button(new Rect(space.xMin, space.yMax, space.width, y), "Copy to Clipboard"))
		{
			ClipboardUtils.CopyToClipboard(text);
		}
		space.yMax += y;
		this.m_rewardTrackWindow.ResizeToFit(new UnityEngine.Vector2(space.width, space.height));
		space.yMin = space.yMax;
		return space;
	}

	// Token: 0x06005DBF RID: 23999 RVA: 0x001E8824 File Offset: 0x001E6A24
	private Rect LayoutNotepadDebug(Rect space)
	{
		GUIStyle guistyle = new GUIStyle(GUI.skin.box);
		guistyle.alignment = TextAnchor.MiddleLeft;
		GUIContent content = new GUIContent("");
		space.height = guistyle.CalcHeight(content, space.width);
		string path = Directory.GetCurrentDirectory() + "\\notepad.txt";
		if (this.m_notepadFirstRun)
		{
			if (!File.Exists(path))
			{
				File.Create(path).Close();
			}
			else
			{
				this.m_notepadContents = File.ReadAllText(path);
			}
			this.m_notepadFirstRun = false;
		}
		GUILayout.BeginArea(new Rect(space.xMin, space.yMax, space.width, 300f));
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		this.scrollViewVector = GUILayout.BeginScrollView(this.scrollViewVector, Array.Empty<GUILayoutOption>());
		this.m_notepadContents = GUILayout.TextArea(this.m_notepadContents, new GUILayoutOption[]
		{
			GUILayout.ExpandHeight(true)
		});
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		float y = this.m_GUISize.y;
		if (GUILayout.Button("Copy to Clipboard", Array.Empty<GUILayoutOption>()))
		{
			ClipboardUtils.CopyToClipboard(this.m_notepadContents);
		}
		if (GUILayout.Button("Save Contents", Array.Empty<GUILayoutOption>()))
		{
			File.WriteAllText(path, this.m_notepadContents);
		}
		space.yMax += y;
		space.yMin = space.yMax;
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUILayout.EndArea();
		return space;
	}

	// Token: 0x06005DC0 RID: 24000 RVA: 0x001E8990 File Offset: 0x001E6B90
	[Conditional("SOUND_DUCKING_DEBUG")]
	private void DrawDuckingInformation(ref Rect space)
	{
		Dictionary<Global.SoundCategory, float> dictionary = SoundManager.Get().DuckingLevels();
		foreach (Global.SoundCategory soundCategory in (Global.SoundCategory[])Enum.GetValues(typeof(Global.SoundCategory)))
		{
			GUI.Box(new Rect(space.min, this.m_GUISize), soundCategory + ": " + dictionary[soundCategory].ToString("0.00"));
			space.yMin += 1f * this.m_GUISize.y;
		}
	}

	// Token: 0x06005DC1 RID: 24001 RVA: 0x001E8A28 File Offset: 0x001E6C28
	[Conditional("UNITY_EDITOR")]
	private void LayoutCursorControls(ref Rect space)
	{
		string text = "Force Hardware Cursor " + (Cursor.visible ? "Off" : "On");
		if (GUI.Button(new Rect(space.min, this.m_GUISize), text))
		{
			Cursor.visible = !Cursor.visible;
		}
		space.yMin += 1.5f * this.m_GUISize.y;
	}

	// Token: 0x06005DC2 RID: 24002 RVA: 0x001E8A98 File Offset: 0x001E6C98
	private Rect LayoutScriptWarnings(Rect space)
	{
		UnityEngine.Vector2 min = space.min;
		UnityEngine.Vector2 guisize = this.m_GUISize;
		if (GUI.Button(new Rect(min.x, min.y, guisize.x, guisize.y), "Clear Script Warnings"))
		{
			this.m_serverLogWindow.Clear();
		}
		min.x += guisize.x;
		if (GUI.Button(new Rect(min.x, min.y, guisize.x, guisize.y), "Search JIRA for GUID"))
		{
			SceneDebugger.ScriptWarning scriptWarning = this.m_serverLogWindow.GetEntries().LastOrDefault<LoggerDebugWindow.LogEntry>() as SceneDebugger.ScriptWarning;
			if (scriptWarning != null)
			{
				string arg = UnityWebRequest.EscapeURL(scriptWarning.IssueGUID);
				Application.OpenURL(string.Format("https://jira.blizzard.com/issues/?jql=text~%22{0}%22", arg));
			}
		}
		min.x += guisize.x;
		min.y += guisize.y;
		space.yMin = min.y;
		return this.m_serverLogWindow.LayoutLog(space);
	}

	// Token: 0x06005DC3 RID: 24003 RVA: 0x001E8B94 File Offset: 0x001E6D94
	private void LayoutButton(ref UnityEngine.Vector2 offset, float top, UnityEngine.Vector2 size, string label, Action action)
	{
		if (offset.y + size.y > this.GetScaledScreen().y)
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

	// Token: 0x06005DC4 RID: 24004 RVA: 0x001E8C1C File Offset: 0x001E6E1C
	private Rect LayoutCustomizeMenu(Rect space)
	{
		List<DebuggerGui> list = new List<DebuggerGui>();
		list.Add(this.m_cheatsWindow);
		list.Add(this.m_messageWindow);
		list.Add(this.m_serverLogWindow);
		list.Add(this.m_rankWindow);
		list.Add(this.m_assetsWindow);
		list.Add(this.m_questWindow);
		list.Add(this.m_achievementWindow);
		list.Add(this.m_rewardTrackWindow);
		list.Add(this.m_timeSection);
		list.Add(this.m_qualitySection);
		list.Add(this.m_statsSection);
		list.Add(this.m_slushTrackerWindow);
		list.Add(this.m_notepadWindow);
		list.Add(this.m_gameplayWindow);
		UnityEngine.Vector2 min = space.min;
		using (List<DebuggerGui>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DebuggerGui section = enumerator.Current;
				string label = (section.IsShown ? "☑" : "☐") + " " + section.Title;
				this.LayoutButton(ref min, 0f, this.m_GUISize, label, delegate
				{
					section.IsShown = !section.IsShown;
				});
			}
		}
		space.yMin = min.y;
		return space;
	}

	// Token: 0x06005DC5 RID: 24005 RVA: 0x001E8D80 File Offset: 0x001E6F80
	private void HandleGuiChanged()
	{
		this.m_guiSaveTimer = 3;
	}

	// Token: 0x06005DC6 RID: 24006 RVA: 0x001E8D8C File Offset: 0x001E6F8C
	public string GetLastScriptWarning()
	{
		SceneDebugger.ScriptWarning scriptWarning = this.m_serverLogWindow.GetEntries().LastOrDefault<LoggerDebugWindow.LogEntry>() as SceneDebugger.ScriptWarning;
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

	// Token: 0x04004F18 RID: 20248
	private readonly UnityEngine.Vector2 m_GUISize = new UnityEngine.Vector2(175f, 30f);

	// Token: 0x04004F19 RID: 20249
	private float m_UpdateInterval = 0.5f;

	// Token: 0x04004F1A RID: 20250
	private double m_LastInterval;

	// Token: 0x04004F1B RID: 20251
	private int m_frames;

	// Token: 0x04004F1C RID: 20252
	private string m_fpsText = string.Empty;

	// Token: 0x04004F1D RID: 20253
	private bool m_testMessaging;

	// Token: 0x04004F1E RID: 20254
	private DebuggerGuiWindow m_guiWindow;

	// Token: 0x04004F1F RID: 20255
	private DebuggerGuiWindow m_rankWindow;

	// Token: 0x04004F20 RID: 20256
	private DebuggerGuiWindow m_assetsWindow;

	// Token: 0x04004F21 RID: 20257
	private DebuggerGuiWindow m_gameplayWindow;

	// Token: 0x04004F22 RID: 20258
	private DebuggerGuiWindow m_questWindow;

	// Token: 0x04004F23 RID: 20259
	private DebuggerGuiWindow m_achievementWindow;

	// Token: 0x04004F24 RID: 20260
	private DebuggerGuiWindow m_rewardTrackWindow;

	// Token: 0x04004F25 RID: 20261
	private LoggerDebugWindow m_messageWindow;

	// Token: 0x04004F26 RID: 20262
	private LoggerDebugWindow m_serverLogWindow;

	// Token: 0x04004F27 RID: 20263
	private CheatsDebugWindow m_cheatsWindow;

	// Token: 0x04004F28 RID: 20264
	private LoggerDebugWindow m_slushTrackerWindow;

	// Token: 0x04004F29 RID: 20265
	private DebuggerGuiWindow m_notepadWindow;

	// Token: 0x04004F2A RID: 20266
	private string m_notepadContents = "";

	// Token: 0x04004F2B RID: 20267
	private bool m_notepadFirstRun = true;

	// Token: 0x04004F2C RID: 20268
	private UnityEngine.Vector2 scrollViewVector = UnityEngine.Vector2.zero;

	// Token: 0x04004F2D RID: 20269
	private DebuggerGui m_timeSection;

	// Token: 0x04004F2E RID: 20270
	private DebuggerGui m_qualitySection;

	// Token: 0x04004F2F RID: 20271
	private DebuggerGui m_statsSection;

	// Token: 0x04004F30 RID: 20272
	private bool m_showGuiCustomization;

	// Token: 0x04004F31 RID: 20273
	private int m_guiSaveTimer = -1;

	// Token: 0x04004F32 RID: 20274
	private List<DebuggerGui> m_debuggerGui;

	// Token: 0x04004F33 RID: 20275
	private long? m_playerId;

	// Token: 0x020021A5 RID: 8613
	public class ConsoleLogEntry : LoggerDebugWindow.LogEntry
	{
		// Token: 0x06012433 RID: 74803 RVA: 0x00502974 File Offset: 0x00500B74
		public ConsoleLogEntry(Log.LogLevel level, string message)
		{
			this.category = level;
			message = message.Trim();
			switch (level)
			{
			case Log.LogLevel.Debug:
				message = string.Format("<color=grey>{0}</color>", message);
				break;
			case Log.LogLevel.Warning:
				message = string.Format("<color=yellow>{0}</color>", message);
				break;
			case Log.LogLevel.Error:
				message = string.Format("<color=red>{0}</color>", message);
				break;
			}
			DateTime now = DateTime.Now;
			string arg = string.Format("<color=grey>[{0}:{1}:{2}]</color>", now.Hour.ToString().PadLeft(2, '0'), now.Minute.ToString().PadLeft(2, '0'), now.Second.ToString().PadLeft(2, '0'));
			this.text = string.Format("{0} {1}", arg, message);
		}
	}

	// Token: 0x020021A6 RID: 8614
	private class SlushTimeRecord : LoggerDebugWindow.LogEntry
	{
		// Token: 0x170028CA RID: 10442
		// (get) Token: 0x06012434 RID: 74804 RVA: 0x00502A43 File Offset: 0x00500C43
		// (set) Token: 0x06012435 RID: 74805 RVA: 0x00502A4B File Offset: 0x00500C4B
		public float ExpectedStart { get; set; }

		// Token: 0x170028CB RID: 10443
		// (get) Token: 0x06012436 RID: 74806 RVA: 0x00502A54 File Offset: 0x00500C54
		// (set) Token: 0x06012437 RID: 74807 RVA: 0x00502A5C File Offset: 0x00500C5C
		public float ExpectedEnd { get; set; }

		// Token: 0x170028CC RID: 10444
		// (get) Token: 0x06012438 RID: 74808 RVA: 0x00502A65 File Offset: 0x00500C65
		// (set) Token: 0x06012439 RID: 74809 RVA: 0x00502A6D File Offset: 0x00500C6D
		public int TaskId { get; set; }

		// Token: 0x170028CD RID: 10445
		// (get) Token: 0x0601243A RID: 74810 RVA: 0x00502A76 File Offset: 0x00500C76
		// (set) Token: 0x0601243B RID: 74811 RVA: 0x00502A7E File Offset: 0x00500C7E
		public float ActualStart { get; set; }

		// Token: 0x170028CE RID: 10446
		// (get) Token: 0x0601243C RID: 74812 RVA: 0x00502A87 File Offset: 0x00500C87
		// (set) Token: 0x0601243D RID: 74813 RVA: 0x00502A8F File Offset: 0x00500C8F
		public float ActualEnd { get; set; }

		// Token: 0x170028CF RID: 10447
		// (get) Token: 0x0601243E RID: 74814 RVA: 0x00502A98 File Offset: 0x00500C98
		// (set) Token: 0x0601243F RID: 74815 RVA: 0x00502AA0 File Offset: 0x00500CA0
		public int EntityId { get; set; }

		// Token: 0x06012440 RID: 74816 RVA: 0x00502AAC File Offset: 0x00500CAC
		public SlushTimeRecord(int taskId, float expectedStart, float expectedEnd, float actualStart = 0f, float actualEnd = 0f, int entityId = 0)
		{
			this.TaskId = taskId;
			this.ExpectedStart = expectedStart;
			this.ExpectedEnd = expectedEnd;
			this.ActualStart = actualStart;
			this.ActualEnd = actualEnd;
			this.EntityId = entityId;
			this.text = this.ToString();
		}

		// Token: 0x06012441 RID: 74817 RVA: 0x00502AF8 File Offset: 0x00500CF8
		private float GetDuration(float start, float end)
		{
			return end - start;
		}

		// Token: 0x06012442 RID: 74818 RVA: 0x00502B00 File Offset: 0x00500D00
		public override string ToString()
		{
			float duration = this.GetDuration(this.ExpectedStart, this.ExpectedEnd);
			float duration2 = this.GetDuration(this.ActualStart, this.ActualEnd);
			float num = this.ActualStart - this.ExpectedStart;
			float num2 = duration2 - duration;
			num2 += num;
			string text = (num2 > 0f) ? "+" : "";
			string text2 = "";
			if (this.EntityId != 0)
			{
				global::Entity entity = GameState.Get().GetEntity(this.EntityId);
				if (entity != null)
				{
					text2 = entity.GetName();
				}
			}
			return string.Format("TaskId: {0}, ({1}) {2}{3}", new object[]
			{
				this.TaskId,
				text2,
				text,
				num2
			});
		}
	}

	// Token: 0x020021A7 RID: 8615
	private class ScriptWarning : LoggerDebugWindow.LogEntry
	{
		// Token: 0x170028D0 RID: 10448
		// (get) Token: 0x06012443 RID: 74819 RVA: 0x00502BBA File Offset: 0x00500DBA
		// (set) Token: 0x06012444 RID: 74820 RVA: 0x00502BC2 File Offset: 0x00500DC2
		public string Source { get; private set; }

		// Token: 0x170028D1 RID: 10449
		// (get) Token: 0x06012445 RID: 74821 RVA: 0x00502BCB File Offset: 0x00500DCB
		// (set) Token: 0x06012446 RID: 74822 RVA: 0x00502BD3 File Offset: 0x00500DD3
		public string Event { get; private set; }

		// Token: 0x170028D2 RID: 10450
		// (get) Token: 0x06012447 RID: 74823 RVA: 0x00502BDC File Offset: 0x00500DDC
		// (set) Token: 0x06012448 RID: 74824 RVA: 0x00502BE4 File Offset: 0x00500DE4
		public string Message { get; private set; }

		// Token: 0x170028D3 RID: 10451
		// (get) Token: 0x06012449 RID: 74825 RVA: 0x00502BED File Offset: 0x00500DED
		// (set) Token: 0x0601244A RID: 74826 RVA: 0x00502BF5 File Offset: 0x00500DF5
		public int Severity { get; set; }

		// Token: 0x170028D4 RID: 10452
		// (get) Token: 0x0601244B RID: 74827 RVA: 0x00502BFE File Offset: 0x00500DFE
		// (set) Token: 0x0601244C RID: 74828 RVA: 0x00502C06 File Offset: 0x00500E06
		public string PowerDef { get; private set; }

		// Token: 0x170028D5 RID: 10453
		// (get) Token: 0x0601244D RID: 74829 RVA: 0x00502C0F File Offset: 0x00500E0F
		// (set) Token: 0x0601244E RID: 74830 RVA: 0x00502C17 File Offset: 0x00500E17
		public int PC { get; private set; }

		// Token: 0x170028D6 RID: 10454
		// (get) Token: 0x0601244F RID: 74831 RVA: 0x00502C20 File Offset: 0x00500E20
		// (set) Token: 0x06012450 RID: 74832 RVA: 0x00502C28 File Offset: 0x00500E28
		public string IssueGUID { get; private set; }

		// Token: 0x06012451 RID: 74833 RVA: 0x00502C34 File Offset: 0x00500E34
		public ScriptWarning(string logSource, string logEvent, string logMessage)
		{
			this.Source = logSource;
			this.Event = logEvent;
			this.Message = logMessage;
			this.Severity = -1;
			this.PowerDef = "";
			this.PC = -1;
			this.IssueGUID = "";
			this.RebuildString();
		}

		// Token: 0x06012452 RID: 74834 RVA: 0x00502C88 File Offset: 0x00500E88
		public void SetPowerDefInfo(string powerDef, string pc)
		{
			if (powerDef.Length < 0 || pc.Length < 0)
			{
				return;
			}
			int pc2;
			if (!int.TryParse(pc, out pc2))
			{
				return;
			}
			this.PowerDef = powerDef;
			this.PC = pc2;
			this.RebuildString();
		}

		// Token: 0x06012453 RID: 74835 RVA: 0x00502CC8 File Offset: 0x00500EC8
		public string ComputeIssueGUID()
		{
			string text = "";
			if (this.PowerDef.Length > 0 && this.PC >= 0)
			{
				text = string.Format("{0}|{1}|{2}", this.Event, this.PowerDef, this.PC);
			}
			else if (this.Source.Length > 0)
			{
				text = string.Format("{0}|{1}", this.Event, this.Source);
			}
			if (text.Length <= 0)
			{
				return "";
			}
			byte[] inArray = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text));
			this.IssueGUID = Convert.ToBase64String(inArray);
			this.RebuildString();
			return this.IssueGUID;
		}

		// Token: 0x06012454 RID: 74836 RVA: 0x00502D78 File Offset: 0x00500F78
		public void RebuildString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("<color=red>-> [{0}]</color>", this.Event));
			if (this.Source.Length > 0)
			{
				stringBuilder.AppendLine(string.Format("    -source={0}", this.Source));
			}
			foreach (string text in this.Message.Split(new char[]
			{
				'|'
			}))
			{
				if (text.Length > 0)
				{
					stringBuilder.AppendLine(string.Format("    -{0}", text));
				}
			}
			if (this.IssueGUID.Length > 0)
			{
				stringBuilder.AppendLine(string.Format("    -(guid: {0})", this.IssueGUID));
			}
			this.text = stringBuilder.ToString();
		}

		// Token: 0x06012455 RID: 74837 RVA: 0x00502E3C File Offset: 0x0050103C
		public override string ToString()
		{
			return string.Format("Received script warning from '{0}'!  event:[{1}]  message:\"{2}\"  guid:({3})", new object[]
			{
				this.Source,
				this.Event,
				this.Message,
				this.IssueGUID
			});
		}
	}
}
