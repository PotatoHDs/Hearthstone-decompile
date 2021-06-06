using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class PracticePickerTrayDisplay : MonoBehaviour
{
	public delegate void TrayLoaded();

	[CustomEditField(Sections = "UI")]
	public UberText m_trayLabel;

	[CustomEditField(Sections = "UI")]
	public StandardPegButtonNew m_backButton;

	[CustomEditField(Sections = "UI")]
	public PlayButton m_playButton;

	[CustomEditField(Sections = "AI Button Settings")]
	public PracticeAIButton m_AIButtonPrefab;

	[CustomEditField(Sections = "AI Button Settings")]
	public GameObject m_AIButtonsContainer;

	[SerializeField]
	private float m_AIButtonHeight = 5f;

	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayAnimationTime = 0.5f;

	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayInEaseType = iTween.EaseType.easeOutBounce;

	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayOutEaseType = iTween.EaseType.easeOutCubic;

	private static PracticePickerTrayDisplay s_instance;

	private List<ScenarioDbfRecord> m_sortedMissionRecords = new List<ScenarioDbfRecord>();

	private List<PracticeAIButton> m_practiceAIButtons = new List<PracticeAIButton>();

	private List<Achievement> m_lockedHeroes = new List<Achievement>();

	private PracticeAIButton m_selectedPracticeAIButton;

	private Map<string, DefLoader.DisposableFullDef> m_heroDefs = new Map<string, DefLoader.DisposableFullDef>();

	private int m_heroDefsToLoad;

	private List<TrayLoaded> m_TrayLoadedListeners = new List<TrayLoaded>();

	private bool m_buttonsCreated;

	private bool m_buttonsReady;

	private bool m_heroesLoaded;

	private bool m_shown;

	private const float PRACTICE_TRAY_MATERIAL_Y_OFFSET = -0.045f;

	[CustomEditField(Sections = "AI Button Settings")]
	public float AIButtonHeight
	{
		get
		{
			return m_AIButtonHeight;
		}
		set
		{
			m_AIButtonHeight = value;
			UpdateAIButtonPositions();
		}
	}

	private void Awake()
	{
		s_instance = this;
		InitMissionRecords();
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(value: false);
		}
		base.gameObject.SetActive(value: true);
		if (m_backButton != null)
		{
			m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
			m_backButton.AddEventListener(UIEventType.RELEASE, BackButtonReleased);
		}
		m_trayLabel.Text = GameStrings.Get("GLUE_CHOOSE_OPPONENT");
		m_playButton.AddEventListener(UIEventType.RELEASE, PlayGameButtonRelease);
		m_heroDefsToLoad = m_sortedMissionRecords.Count;
		foreach (ScenarioDbfRecord sortedMissionRecord in m_sortedMissionRecords)
		{
			string missionHeroCardId = GameUtils.GetMissionHeroCardId(sortedMissionRecord.ID);
			DefLoader.Get().LoadFullDef(missionHeroCardId, OnFullDefLoaded);
		}
		SoundManager.Get().Load("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		SoundManager.Get().Load("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
		SetupHeroAchieves();
		StartCoroutine(NotifyWhenTrayLoaded());
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
	}

	private void OnDestroy()
	{
		GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
		m_heroDefs.DisposeValuesAndClear();
		s_instance = null;
	}

	private void Start()
	{
		m_playButton.SetText(GameStrings.Get("GLOBAL_PLAY"));
		m_playButton.SetOriginalLocalPosition();
		m_playButton.Disable();
	}

	public static PracticePickerTrayDisplay Get()
	{
		return s_instance;
	}

	public void Init()
	{
		int count = m_sortedMissionRecords.Count;
		for (int i = 0; i < count; i++)
		{
			PracticeAIButton practiceAIButton = (PracticeAIButton)GameUtils.Instantiate(m_AIButtonPrefab, m_AIButtonsContainer);
			SceneUtils.SetLayer(practiceAIButton, m_AIButtonsContainer.gameObject.layer);
			m_practiceAIButtons.Add(practiceAIButton);
		}
		UpdateAIButtonPositions();
		foreach (PracticeAIButton practiceAIButton2 in m_practiceAIButtons)
		{
			practiceAIButton2.SetOriginalLocalPosition();
			practiceAIButton2.AddEventListener(UIEventType.RELEASE, AIButtonPressed);
		}
		m_buttonsCreated = true;
	}

	public void Show()
	{
		m_shown = true;
		iTween.Stop(base.gameObject);
		Transform[] components = base.gameObject.GetComponents<Transform>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].gameObject.SetActive(value: true);
		}
		base.gameObject.SetActive(value: true);
		Hashtable args = iTween.Hash("position", PracticeDisplay.Get().GetPracticePickerShowPosition(), "isLocal", true, "time", m_trayAnimationTime, "easetype", m_trayInEaseType, "delay", 0.001f);
		iTween.MoveTo(base.gameObject, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
		if (!Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_TRAY, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("PracticePickerTrayDisplay.Show:" + Option.HAS_SEEN_PRACTICE_TRAY))
		{
			Options.Get().SetBool(Option.HAS_SEEN_PRACTICE_TRAY, val: true);
			StartCoroutine(DoPickHeroLines());
		}
		if (m_selectedPracticeAIButton != null)
		{
			m_playButton.Enable();
		}
		Navigation.Push(OnNavigateBack);
	}

	private IEnumerator DoPickHeroLines()
	{
		Notification firstPart = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_PRACTICE_INST1_07"), "VO_INNKEEPER_UNLOCK_HEROES.prefab:9a11f2d877b018043a6c85883cdd1761");
		while (firstPart.GetAudio() == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(firstPart.GetAudio().clip.length);
		yield return new WaitForSeconds(6f);
		if (!m_playButton.IsEnabled() && !GameMgr.Get().IsTransitionPopupShown())
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_PRACTICE_INST2_08"), "VO_INNKEEPER_PRACTICE_INST2_08.prefab:7f8a9981df8853d44b3cc423d4f44f52", 2f);
		}
	}

	public void Hide()
	{
		m_shown = false;
		iTween.Stop(base.gameObject);
		Hashtable args = iTween.Hash("position", PracticeDisplay.Get().GetPracticePickerHidePosition(), "isLocal", true, "time", m_trayAnimationTime, "easetype", m_trayOutEaseType, "oncomplete", (Action<object>)delegate
		{
			base.gameObject.SetActive(value: false);
		}, "delay", 0.001f);
		iTween.MoveTo(base.gameObject, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
	}

	public void OnGameDenied()
	{
		UpdateAIButtons();
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public void AddTrayLoadedListener(TrayLoaded dlg)
	{
		m_TrayLoadedListeners.Add(dlg);
	}

	public void RemoveTrayLoadedListener(TrayLoaded dlg)
	{
		m_TrayLoadedListeners.Remove(dlg);
	}

	public bool IsLoaded()
	{
		return m_buttonsReady;
	}

	private void InitMissionRecords()
	{
		int practiceDbId = 2;
		int modeDbId = (int)AdventureConfig.Get().GetSelectedMode();
		m_sortedMissionRecords = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == practiceDbId && r.ModeId == modeDbId);
		m_sortedMissionRecords.Sort(GameUtils.MissionSortComparison);
	}

	private void SetupHeroAchieves()
	{
		m_lockedHeroes = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, isComplete: false);
		m_lockedHeroes.RemoveAll((Achievement a) => a.AchieveTrigger != Achieve.Trigger.WIN || a.EnemyHeroClassId == 0);
		if (m_lockedHeroes.Count <= 7 && !Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, defaultVal: false))
		{
			Options.Get().SetBool(Option.HAS_SEEN_PRACTICE_MODE, val: true);
		}
		StartCoroutine(InitButtonsWhenReady());
	}

	private IEnumerator InitButtonsWhenReady()
	{
		while (!m_buttonsCreated)
		{
			yield return null;
		}
		while (!m_heroesLoaded)
		{
			yield return null;
		}
		UpdateAIButtons();
		m_buttonsReady = true;
	}

	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		m_heroDefs.SetOrReplaceDisposable(cardId, def);
		m_heroDefsToLoad--;
		if (m_heroDefsToLoad <= 0)
		{
			m_heroesLoaded = true;
		}
	}

	private void SetSelectedButton(PracticeAIButton button)
	{
		if (m_selectedPracticeAIButton != null)
		{
			m_selectedPracticeAIButton.Deselect();
		}
		m_selectedPracticeAIButton = button;
	}

	private void DisableAIButtons()
	{
		for (int i = 0; i < m_practiceAIButtons.Count; i++)
		{
			m_practiceAIButtons[i].SetEnabled(enabled: false);
		}
	}

	private void EnableAIButtons()
	{
		for (int i = 0; i < m_practiceAIButtons.Count; i++)
		{
			m_practiceAIButtons[i].SetEnabled(enabled: true);
		}
	}

	private bool OnNavigateBack()
	{
		Hide();
		if (DeckPickerTray.GetTray() != null)
		{
			DeckPickerTray.GetTray().ResetCurrentMode();
		}
		return true;
	}

	private void BackButtonReleased(UIEvent e)
	{
		Navigation.GoBack();
	}

	private void PlayGameButtonRelease(UIEvent e)
	{
		SceneUtils.SetLayer(PracticeDisplay.Get().gameObject, GameLayer.Default);
		long selectedDeckID = DeckPickerTrayDisplay.Get().GetSelectedDeckID();
		if (selectedDeckID == 0L)
		{
			Debug.LogError("Trying to play practice game with deck ID 0!");
			return;
		}
		e.GetElement().SetEnabled(enabled: false);
		DisableAIButtons();
		if (AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.EXPERT && !Options.Get().GetBool(Option.HAS_PLAYED_EXPERT_AI, defaultVal: false))
		{
			Options.Get().SetBool(Option.HAS_PLAYED_EXPERT_AI, val: true);
		}
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, m_selectedPracticeAIButton.GetMissionID(), 0, selectedDeckID);
	}

	private void AIButtonPressed(UIEvent e)
	{
		PracticeAIButton practiceAIButton = (PracticeAIButton)e.GetElement();
		SetSelectedButton(practiceAIButton);
		m_playButton.Enable();
		practiceAIButton.Select();
	}

	private void UpdateAIButtons()
	{
		UpdateAIDeckButtons();
		if (m_selectedPracticeAIButton == null)
		{
			m_playButton.Disable();
		}
		else
		{
			m_playButton.Enable();
		}
	}

	private void UpdateAIButtonPositions()
	{
		int num = 0;
		foreach (PracticeAIButton practiceAIButton in m_practiceAIButtons)
		{
			TransformUtil.SetLocalPosZ(practiceAIButton, (0f - m_AIButtonHeight) * (float)num++);
		}
	}

	private void UpdateAIDeckButtons()
	{
		for (int i = 0; i < m_sortedMissionRecords.Count; i++)
		{
			ScenarioDbfRecord scenarioDbfRecord = m_sortedMissionRecords[i];
			int iD = scenarioDbfRecord.ID;
			string missionHeroCardId = GameUtils.GetMissionHeroCardId(iD);
			DefLoader.DisposableFullDef disposableFullDef = m_heroDefs[missionHeroCardId];
			TAG_CLASS @class = disposableFullDef.EntityDef.GetClass();
			string text = scenarioDbfRecord.ShortName;
			PracticeAIButton practiceAIButton = m_practiceAIButtons[i];
			practiceAIButton.SetInfo(text, @class, disposableFullDef.DisposableCardDef, iD, flip: false);
			bool shown = false;
			foreach (Achievement lockedHero in m_lockedHeroes)
			{
				if (lockedHero.ClassReward.Value == @class)
				{
					shown = true;
					break;
				}
			}
			practiceAIButton.ShowQuestBang(shown);
			if (practiceAIButton == m_selectedPracticeAIButton)
			{
				practiceAIButton.Select();
			}
			else
			{
				practiceAIButton.Deselect();
			}
		}
		bool num = AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.EXPERT;
		bool @bool = Options.Get().GetBool(Option.HAS_SEEN_EXPERT_AI, defaultVal: false);
		if (num && !@bool)
		{
			Options.Get().SetBool(Option.HAS_SEEN_EXPERT_AI, val: true);
			@bool = true;
		}
	}

	private IEnumerator NotifyWhenTrayLoaded()
	{
		while (!m_buttonsReady)
		{
			yield return null;
		}
		FireTrayLoadedEvent();
	}

	private void FireTrayLoadedEvent()
	{
		TrayLoaded[] array = m_TrayLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (eventData.m_state == FindGameState.INVALID)
		{
			EnableAIButtons();
		}
		return false;
	}
}
