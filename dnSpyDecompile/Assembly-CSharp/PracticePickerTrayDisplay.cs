using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using PegasusShared;
using UnityEngine;

// Token: 0x0200061F RID: 1567
[CustomEditClass]
public class PracticePickerTrayDisplay : MonoBehaviour
{
	// Token: 0x17000525 RID: 1317
	// (get) Token: 0x060057DF RID: 22495 RVA: 0x001CBABA File Offset: 0x001C9CBA
	// (set) Token: 0x060057E0 RID: 22496 RVA: 0x001CBAC2 File Offset: 0x001C9CC2
	[CustomEditField(Sections = "AI Button Settings")]
	public float AIButtonHeight
	{
		get
		{
			return this.m_AIButtonHeight;
		}
		set
		{
			this.m_AIButtonHeight = value;
			this.UpdateAIButtonPositions();
		}
	}

	// Token: 0x060057E1 RID: 22497 RVA: 0x001CBAD4 File Offset: 0x001C9CD4
	private void Awake()
	{
		PracticePickerTrayDisplay.s_instance = this;
		this.InitMissionRecords();
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(false);
		}
		base.gameObject.SetActive(true);
		if (this.m_backButton != null)
		{
			this.m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
			this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonReleased));
		}
		this.m_trayLabel.Text = GameStrings.Get("GLUE_CHOOSE_OPPONENT");
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayGameButtonRelease));
		this.m_heroDefsToLoad = this.m_sortedMissionRecords.Count;
		foreach (ScenarioDbfRecord scenarioDbfRecord in this.m_sortedMissionRecords)
		{
			string missionHeroCardId = GameUtils.GetMissionHeroCardId(scenarioDbfRecord.ID);
			DefLoader.Get().LoadFullDef(missionHeroCardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
		}
		SoundManager.Get().Load("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		SoundManager.Get().Load("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
		this.SetupHeroAchieves();
		base.StartCoroutine(this.NotifyWhenTrayLoaded());
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
	}

	// Token: 0x060057E2 RID: 22498 RVA: 0x001CBC54 File Offset: 0x001C9E54
	private void OnDestroy()
	{
		GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		this.m_heroDefs.DisposeValuesAndClear<string, DefLoader.DisposableFullDef>();
		PracticePickerTrayDisplay.s_instance = null;
	}

	// Token: 0x060057E3 RID: 22499 RVA: 0x001CBC7E File Offset: 0x001C9E7E
	private void Start()
	{
		this.m_playButton.SetText(GameStrings.Get("GLOBAL_PLAY"));
		this.m_playButton.SetOriginalLocalPosition();
		this.m_playButton.Disable(false);
	}

	// Token: 0x060057E4 RID: 22500 RVA: 0x001CBCAC File Offset: 0x001C9EAC
	public static PracticePickerTrayDisplay Get()
	{
		return PracticePickerTrayDisplay.s_instance;
	}

	// Token: 0x060057E5 RID: 22501 RVA: 0x001CBCB4 File Offset: 0x001C9EB4
	public void Init()
	{
		int count = this.m_sortedMissionRecords.Count;
		for (int i = 0; i < count; i++)
		{
			PracticeAIButton practiceAIButton = (PracticeAIButton)GameUtils.Instantiate(this.m_AIButtonPrefab, this.m_AIButtonsContainer, false);
			SceneUtils.SetLayer(practiceAIButton, this.m_AIButtonsContainer.gameObject.layer);
			this.m_practiceAIButtons.Add(practiceAIButton);
		}
		this.UpdateAIButtonPositions();
		foreach (PracticeAIButton practiceAIButton2 in this.m_practiceAIButtons)
		{
			practiceAIButton2.SetOriginalLocalPosition();
			practiceAIButton2.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.AIButtonPressed));
		}
		this.m_buttonsCreated = true;
	}

	// Token: 0x060057E6 RID: 22502 RVA: 0x001CBD78 File Offset: 0x001C9F78
	public void Show()
	{
		this.m_shown = true;
		iTween.Stop(base.gameObject);
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(true);
		}
		base.gameObject.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			PracticeDisplay.Get().GetPracticePickerShowPosition(),
			"isLocal",
			true,
			"time",
			this.m_trayAnimationTime,
			"easetype",
			this.m_trayInEaseType,
			"delay",
			0.001f
		});
		iTween.MoveTo(base.gameObject, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		if (!Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_TRAY, false) && UserAttentionManager.CanShowAttentionGrabber("PracticePickerTrayDisplay.Show:" + Option.HAS_SEEN_PRACTICE_TRAY))
		{
			Options.Get().SetBool(Option.HAS_SEEN_PRACTICE_TRAY, true);
			base.StartCoroutine(this.DoPickHeroLines());
		}
		if (this.m_selectedPracticeAIButton != null)
		{
			this.m_playButton.Enable();
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
	}

	// Token: 0x060057E7 RID: 22503 RVA: 0x001CBED9 File Offset: 0x001CA0D9
	private IEnumerator DoPickHeroLines()
	{
		Notification firstPart = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_PRACTICE_INST1_07"), "VO_INNKEEPER_UNLOCK_HEROES.prefab:9a11f2d877b018043a6c85883cdd1761", 0f, null, false);
		while (firstPart.GetAudio() == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(firstPart.GetAudio().clip.length);
		yield return new WaitForSeconds(6f);
		if (this.m_playButton.IsEnabled() || GameMgr.Get().IsTransitionPopupShown())
		{
			yield break;
		}
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_PRACTICE_INST2_08"), "VO_INNKEEPER_PRACTICE_INST2_08.prefab:7f8a9981df8853d44b3cc423d4f44f52", 2f, null, false);
		yield break;
	}

	// Token: 0x060057E8 RID: 22504 RVA: 0x001CBEE8 File Offset: 0x001CA0E8
	public void Hide()
	{
		this.m_shown = false;
		iTween.Stop(base.gameObject);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			PracticeDisplay.Get().GetPracticePickerHidePosition(),
			"isLocal",
			true,
			"time",
			this.m_trayAnimationTime,
			"easetype",
			this.m_trayOutEaseType,
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				base.gameObject.SetActive(false);
			}),
			"delay",
			0.001f
		});
		iTween.MoveTo(base.gameObject, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
	}

	// Token: 0x060057E9 RID: 22505 RVA: 0x001CBFBA File Offset: 0x001CA1BA
	public void OnGameDenied()
	{
		this.UpdateAIButtons();
	}

	// Token: 0x060057EA RID: 22506 RVA: 0x001CBFC2 File Offset: 0x001CA1C2
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x060057EB RID: 22507 RVA: 0x001CBFCA File Offset: 0x001CA1CA
	public void AddTrayLoadedListener(PracticePickerTrayDisplay.TrayLoaded dlg)
	{
		this.m_TrayLoadedListeners.Add(dlg);
	}

	// Token: 0x060057EC RID: 22508 RVA: 0x001CBFD8 File Offset: 0x001CA1D8
	public void RemoveTrayLoadedListener(PracticePickerTrayDisplay.TrayLoaded dlg)
	{
		this.m_TrayLoadedListeners.Remove(dlg);
	}

	// Token: 0x060057ED RID: 22509 RVA: 0x001CBFE7 File Offset: 0x001CA1E7
	public bool IsLoaded()
	{
		return this.m_buttonsReady;
	}

	// Token: 0x060057EE RID: 22510 RVA: 0x001CBFF0 File Offset: 0x001CA1F0
	private void InitMissionRecords()
	{
		int practiceDbId = 2;
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		int modeDbId = (int)selectedMode;
		this.m_sortedMissionRecords = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == practiceDbId && r.ModeId == modeDbId, -1);
		this.m_sortedMissionRecords.Sort(new Comparison<ScenarioDbfRecord>(GameUtils.MissionSortComparison));
	}

	// Token: 0x060057EF RID: 22511 RVA: 0x001CC050 File Offset: 0x001CA250
	private void SetupHeroAchieves()
	{
		this.m_lockedHeroes = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, false);
		this.m_lockedHeroes.RemoveAll((global::Achievement a) => a.AchieveTrigger != Achieve.Trigger.WIN || a.EnemyHeroClassId == 0);
		if (this.m_lockedHeroes.Count <= 7 && !Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, false))
		{
			Options.Get().SetBool(Option.HAS_SEEN_PRACTICE_MODE, true);
		}
		base.StartCoroutine(this.InitButtonsWhenReady());
	}

	// Token: 0x060057F0 RID: 22512 RVA: 0x001CC0D7 File Offset: 0x001CA2D7
	private IEnumerator InitButtonsWhenReady()
	{
		while (!this.m_buttonsCreated)
		{
			yield return null;
		}
		while (!this.m_heroesLoaded)
		{
			yield return null;
		}
		this.UpdateAIButtons();
		this.m_buttonsReady = true;
		yield break;
	}

	// Token: 0x060057F1 RID: 22513 RVA: 0x001CC0E6 File Offset: 0x001CA2E6
	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		this.m_heroDefs.SetOrReplaceDisposable(cardId, def);
		this.m_heroDefsToLoad--;
		if (this.m_heroDefsToLoad > 0)
		{
			return;
		}
		this.m_heroesLoaded = true;
	}

	// Token: 0x060057F2 RID: 22514 RVA: 0x001CC114 File Offset: 0x001CA314
	private void SetSelectedButton(PracticeAIButton button)
	{
		if (this.m_selectedPracticeAIButton != null)
		{
			this.m_selectedPracticeAIButton.Deselect();
		}
		this.m_selectedPracticeAIButton = button;
	}

	// Token: 0x060057F3 RID: 22515 RVA: 0x001CC138 File Offset: 0x001CA338
	private void DisableAIButtons()
	{
		for (int i = 0; i < this.m_practiceAIButtons.Count; i++)
		{
			this.m_practiceAIButtons[i].SetEnabled(false, false);
		}
	}

	// Token: 0x060057F4 RID: 22516 RVA: 0x001CC170 File Offset: 0x001CA370
	private void EnableAIButtons()
	{
		for (int i = 0; i < this.m_practiceAIButtons.Count; i++)
		{
			this.m_practiceAIButtons[i].SetEnabled(true, false);
		}
	}

	// Token: 0x060057F5 RID: 22517 RVA: 0x001CC1A6 File Offset: 0x001CA3A6
	private bool OnNavigateBack()
	{
		this.Hide();
		if (DeckPickerTray.GetTray() != null)
		{
			DeckPickerTray.GetTray().ResetCurrentMode();
		}
		return true;
	}

	// Token: 0x060057F6 RID: 22518 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void BackButtonReleased(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x060057F7 RID: 22519 RVA: 0x001CC1C8 File Offset: 0x001CA3C8
	private void PlayGameButtonRelease(UIEvent e)
	{
		SceneUtils.SetLayer(PracticeDisplay.Get().gameObject, GameLayer.Default);
		long selectedDeckID = DeckPickerTrayDisplay.Get().GetSelectedDeckID();
		if (selectedDeckID == 0L)
		{
			Debug.LogError("Trying to play practice game with deck ID 0!");
			return;
		}
		e.GetElement().SetEnabled(false, false);
		this.DisableAIButtons();
		if (AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.EXPERT && !Options.Get().GetBool(Option.HAS_PLAYED_EXPERT_AI, false))
		{
			Options.Get().SetBool(Option.HAS_PLAYED_EXPERT_AI, true);
		}
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, this.m_selectedPracticeAIButton.GetMissionID(), 0, selectedDeckID, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x060057F8 RID: 22520 RVA: 0x001CC268 File Offset: 0x001CA468
	private void AIButtonPressed(UIEvent e)
	{
		PracticeAIButton practiceAIButton = (PracticeAIButton)e.GetElement();
		this.SetSelectedButton(practiceAIButton);
		this.m_playButton.Enable();
		practiceAIButton.Select();
	}

	// Token: 0x060057F9 RID: 22521 RVA: 0x001CC299 File Offset: 0x001CA499
	private void UpdateAIButtons()
	{
		this.UpdateAIDeckButtons();
		if (this.m_selectedPracticeAIButton == null)
		{
			this.m_playButton.Disable(false);
			return;
		}
		this.m_playButton.Enable();
	}

	// Token: 0x060057FA RID: 22522 RVA: 0x001CC2C8 File Offset: 0x001CA4C8
	private void UpdateAIButtonPositions()
	{
		int num = 0;
		foreach (PracticeAIButton component in this.m_practiceAIButtons)
		{
			TransformUtil.SetLocalPosZ(component, -this.m_AIButtonHeight * (float)num++);
		}
	}

	// Token: 0x060057FB RID: 22523 RVA: 0x001CC328 File Offset: 0x001CA528
	private void UpdateAIDeckButtons()
	{
		for (int i = 0; i < this.m_sortedMissionRecords.Count; i++)
		{
			ScenarioDbfRecord scenarioDbfRecord = this.m_sortedMissionRecords[i];
			int id = scenarioDbfRecord.ID;
			string missionHeroCardId = GameUtils.GetMissionHeroCardId(id);
			DefLoader.DisposableFullDef disposableFullDef = this.m_heroDefs[missionHeroCardId];
			TAG_CLASS @class = disposableFullDef.EntityDef.GetClass();
			string name = scenarioDbfRecord.ShortName;
			PracticeAIButton practiceAIButton = this.m_practiceAIButtons[i];
			practiceAIButton.SetInfo(name, @class, disposableFullDef.DisposableCardDef, id, false);
			bool shown = false;
			using (List<global::Achievement>.Enumerator enumerator = this.m_lockedHeroes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ClassReward.Value == @class)
					{
						shown = true;
						break;
					}
				}
			}
			practiceAIButton.ShowQuestBang(shown);
			if (practiceAIButton == this.m_selectedPracticeAIButton)
			{
				practiceAIButton.Select();
			}
			else
			{
				practiceAIButton.Deselect();
			}
		}
		bool flag = AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.EXPERT;
		bool @bool = Options.Get().GetBool(Option.HAS_SEEN_EXPERT_AI, false);
		if (flag && !@bool)
		{
			Options.Get().SetBool(Option.HAS_SEEN_EXPERT_AI, true);
		}
	}

	// Token: 0x060057FC RID: 22524 RVA: 0x001CC46C File Offset: 0x001CA66C
	private IEnumerator NotifyWhenTrayLoaded()
	{
		while (!this.m_buttonsReady)
		{
			yield return null;
		}
		this.FireTrayLoadedEvent();
		yield break;
	}

	// Token: 0x060057FD RID: 22525 RVA: 0x001CC47C File Offset: 0x001CA67C
	private void FireTrayLoadedEvent()
	{
		PracticePickerTrayDisplay.TrayLoaded[] array = this.m_TrayLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x060057FE RID: 22526 RVA: 0x001CC4AC File Offset: 0x001CA6AC
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (eventData.m_state == FindGameState.INVALID)
		{
			this.EnableAIButtons();
		}
		return false;
	}

	// Token: 0x04004B61 RID: 19297
	[CustomEditField(Sections = "UI")]
	public UberText m_trayLabel;

	// Token: 0x04004B62 RID: 19298
	[CustomEditField(Sections = "UI")]
	public StandardPegButtonNew m_backButton;

	// Token: 0x04004B63 RID: 19299
	[CustomEditField(Sections = "UI")]
	public PlayButton m_playButton;

	// Token: 0x04004B64 RID: 19300
	[CustomEditField(Sections = "AI Button Settings")]
	public PracticeAIButton m_AIButtonPrefab;

	// Token: 0x04004B65 RID: 19301
	[CustomEditField(Sections = "AI Button Settings")]
	public GameObject m_AIButtonsContainer;

	// Token: 0x04004B66 RID: 19302
	[SerializeField]
	private float m_AIButtonHeight = 5f;

	// Token: 0x04004B67 RID: 19303
	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayAnimationTime = 0.5f;

	// Token: 0x04004B68 RID: 19304
	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayInEaseType = iTween.EaseType.easeOutBounce;

	// Token: 0x04004B69 RID: 19305
	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayOutEaseType = iTween.EaseType.easeOutCubic;

	// Token: 0x04004B6A RID: 19306
	private static PracticePickerTrayDisplay s_instance;

	// Token: 0x04004B6B RID: 19307
	private List<ScenarioDbfRecord> m_sortedMissionRecords = new List<ScenarioDbfRecord>();

	// Token: 0x04004B6C RID: 19308
	private List<PracticeAIButton> m_practiceAIButtons = new List<PracticeAIButton>();

	// Token: 0x04004B6D RID: 19309
	private List<global::Achievement> m_lockedHeroes = new List<global::Achievement>();

	// Token: 0x04004B6E RID: 19310
	private PracticeAIButton m_selectedPracticeAIButton;

	// Token: 0x04004B6F RID: 19311
	private Map<string, DefLoader.DisposableFullDef> m_heroDefs = new Map<string, DefLoader.DisposableFullDef>();

	// Token: 0x04004B70 RID: 19312
	private int m_heroDefsToLoad;

	// Token: 0x04004B71 RID: 19313
	private List<PracticePickerTrayDisplay.TrayLoaded> m_TrayLoadedListeners = new List<PracticePickerTrayDisplay.TrayLoaded>();

	// Token: 0x04004B72 RID: 19314
	private bool m_buttonsCreated;

	// Token: 0x04004B73 RID: 19315
	private bool m_buttonsReady;

	// Token: 0x04004B74 RID: 19316
	private bool m_heroesLoaded;

	// Token: 0x04004B75 RID: 19317
	private bool m_shown;

	// Token: 0x04004B76 RID: 19318
	private const float PRACTICE_TRAY_MATERIAL_Y_OFFSET = -0.045f;

	// Token: 0x02002123 RID: 8483
	// (Invoke) Token: 0x0601226C RID: 74348
	public delegate void TrayLoaded();
}
