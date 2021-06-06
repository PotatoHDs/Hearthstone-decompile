using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class AdventureScene : PegasusScene
{
	public enum TransitionDirection
	{
		INVALID = -1,
		X,
		Y,
		Z,
		NX,
		NY,
		NZ
	}

	[Serializable]
	public class AdventureModeMusicWingOverride
	{
		public WingDbId m_wingId;

		public MusicPlaylistType m_playlist;
	}

	[Serializable]
	public class AdventureModeMusic
	{
		public AdventureData.Adventuresubscene m_subsceneId;

		public AdventureDbId m_adventureId;

		public MusicPlaylistType m_playlist;

		[CustomEditField(ListSortable = true)]
		public List<AdventureModeMusicWingOverride> m_wingOverrides;
	}

	[Serializable]
	public class AdventureSubSceneDef
	{
		[CustomEditField(ListSortable = true)]
		public AdventureData.Adventuresubscene m_SubScene;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public String_MobileOverride m_Prefab;

		public bool isWidget;
	}

	private static AdventureScene s_instance;

	[CustomEditField(Sections = "Transition Blocker")]
	public GameObject m_transitionClickBlocker;

	[CustomEditField(Sections = "Transition Motions")]
	public Vector3 m_SubScenePosition = Vector3.zero;

	[CustomEditField(Sections = "Transition Motions")]
	public float m_DefaultTransitionAnimationTime = 1f;

	[CustomEditField(Sections = "Transition Motions")]
	public iTween.EaseType m_TransitionEaseType = iTween.EaseType.easeInOutSine;

	[CustomEditField(Sections = "Transition Motions")]
	public TransitionDirection m_TransitionDirection;

	[CustomEditField(Sections = "Transition Sounds", T = EditType.SOUND_PREFAB)]
	public string m_SlideInSound;

	[CustomEditField(Sections = "Transition Sounds", T = EditType.SOUND_PREFAB)]
	public string m_SlideOutSound;

	[CustomEditField(Sections = "Adventure Subscene Prefabs")]
	public List<AdventureSubSceneDef> m_SubSceneDefs = new List<AdventureSubSceneDef>();

	[CustomEditField(Sections = "Music Settings")]
	public List<AdventureModeMusic> m_AdventureModeMusic = new List<AdventureModeMusic>();

	private GameObject m_TransitionOutSubSceneParent;

	private GameObject m_CurrentSubSceneParent;

	private GameObject m_TransitionOutSubScene;

	private GameObject m_CurrentSubScene;

	private bool m_transitionIsGoingBack;

	private int m_StartupAssetLoads;

	private int m_SubScenesLoaded;

	private bool m_MusicStopped;

	private bool m_Unloading;

	private TransitionDirection m_CurrentTransitionDirection;

	private bool m_isTransitioning;

	private bool m_isLoading;

	private Coroutine m_waitForSubSceneToLoadCoroutine;

	private const AdventureData.Adventuresubscene s_StartMode = AdventureData.Adventuresubscene.CHOOSER;

	private List<AdventureDbId> m_adventuresThatRequestedGameSaveData = new List<AdventureDbId>();

	private AdventureDefCache m_adventureDefCache;

	private AdventureWingDefCache m_adventureWingDefCache;

	public bool IsDevMode { get; set; }

	public int DevModeSetting { get; set; }

	protected override void Awake()
	{
		base.Awake();
		s_instance = this;
		m_CurrentSubScene = null;
		m_TransitionOutSubScene = null;
		m_CurrentTransitionDirection = m_TransitionDirection;
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.OnAdventureSceneAwake();
		adventureConfig.AddSubSceneChangeListener(OnSubSceneChange);
		adventureConfig.AddSelectedModeChangeListener(OnSelectedModeChanged);
		adventureConfig.AddAdventureModeChangeListener(OnAdventureModeChanged);
		adventureConfig.AddAdventureMissionSetListener(OnAdventureMissionChanged);
		m_StartupAssetLoads++;
		SetCurrentTransitionDirection();
		Options.Get().SetBool(Option.BUNDLE_JUST_PURCHASE_IN_HUB, val: false);
		if (HearthstoneApplication.IsInternal())
		{
			CheatMgr.Get().RegisterCategory("adventure");
			CheatMgr.Get().RegisterCheatHandler("advdev", OnDevCheat);
			CheatMgr.Get().DefaultCategory();
		}
		m_adventureDefCache = new AdventureDefCache(preloadRecords: true);
		m_adventureWingDefCache = new AdventureWingDefCache(preloadRecords: true);
		NotifyAchieveManagerOfAdventureSceneLoaded();
		LoadSubScene(adventureConfig.CurrentSubScene, OnFirstSubSceneLoaded, new Action(OnStartupAssetLoaded));
	}

	private void Start()
	{
		AdventureConfig.Get().UpdatePresence();
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public static AdventureScene Get()
	{
		return s_instance;
	}

	public override bool IsUnloading()
	{
		return m_Unloading;
	}

	public override void Unload()
	{
		m_Unloading = true;
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.ClearBossDefs();
		DeckPickerTray.Get().Unload();
		adventureConfig.RemoveAdventureModeChangeListener(OnAdventureModeChanged);
		adventureConfig.RemoveSelectedModeChangeListener(OnSelectedModeChanged);
		adventureConfig.RemoveSubSceneChangeListener(OnSubSceneChange);
		adventureConfig.OnAdventureSceneUnload();
		CheatMgr.Get().UnregisterCheatHandler("advdev", OnDevCheat);
		m_Unloading = false;
	}

	public override bool IsTransitioning()
	{
		return m_isTransitioning;
	}

	public bool IsInitialScreen()
	{
		return m_SubScenesLoaded <= 1;
	}

	public AdventureDef GetAdventureDef(AdventureDbId advId)
	{
		return m_adventureDefCache.GetDef(advId);
	}

	public List<AdventureDef> GetSortedAdventureDefs()
	{
		List<AdventureDef> list = new List<AdventureDef>(m_adventureDefCache.Values);
		list.Sort((AdventureDef l, AdventureDef r) => r.GetSortOrder() - l.GetSortOrder());
		return list;
	}

	public AdventureWingDef GetWingDef(WingDbId wingId)
	{
		return m_adventureWingDefCache.GetDef(wingId);
	}

	private void UpdateAdventureModeMusic()
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureData.Adventuresubscene currentSubScene = AdventureConfig.Get().CurrentSubScene;
		MusicPlaylistType? musicPlaylistType = null;
		foreach (AdventureModeMusic item in m_AdventureModeMusic)
		{
			if (item.m_subsceneId == currentSubScene && item.m_adventureId == selectedAdventure)
			{
				musicPlaylistType = ((!GetAdventureModeMusicWingOverride(item, out var playlist)) ? new MusicPlaylistType?(item.m_playlist) : playlist);
				break;
			}
			if (item.m_subsceneId == currentSubScene && item.m_adventureId == AdventureDbId.INVALID)
			{
				musicPlaylistType = item.m_playlist;
			}
		}
		if (musicPlaylistType.HasValue)
		{
			MusicManager.Get().StartPlaylist(musicPlaylistType.Value);
		}
	}

	private static bool GetAdventureModeMusicWingOverride(AdventureModeMusic music, out MusicPlaylistType? playlist)
	{
		playlist = null;
		if (music == null || music.m_wingOverrides.Count == 0)
		{
			return false;
		}
		ScenarioDbId lastSelectedMission = AdventureConfig.Get().GetLastSelectedMission();
		if (lastSelectedMission == ScenarioDbId.INVALID)
		{
			return false;
		}
		WingDbId wingDbId = (WingDbId)(GameUtils.GetWingRecordFromMissionId((int)lastSelectedMission)?.ID ?? 0);
		if (wingDbId == WingDbId.INVALID)
		{
			return false;
		}
		foreach (AdventureModeMusicWingOverride wingOverride in music.m_wingOverrides)
		{
			if (wingOverride.m_wingId == wingDbId)
			{
				playlist = wingOverride.m_playlist;
				return true;
			}
		}
		return false;
	}

	private void OnStartupAssetLoaded()
	{
		m_StartupAssetLoads--;
		if (m_StartupAssetLoads <= 0)
		{
			UpdateAdventureModeMusic();
			SceneMgr.Get().NotifySceneLoaded();
		}
	}

	private void LoadSubScene(AdventureData.Adventuresubscene subscene)
	{
		LoadSubScene(subscene, OnSubSceneLoaded);
	}

	private void LoadSubScene(AdventureData.Adventuresubscene subscene, AssetLoader.GameObjectCallback callback, object callbackData = null)
	{
		AdventureSubSceneDef subSceneDef = m_SubSceneDefs.Find((AdventureSubSceneDef item) => item.m_SubScene == subscene);
		if (subSceneDef == null)
		{
			Debug.LogErrorFormat("Subscene {0} prefab not defined in m_SubSceneDefs", subscene);
			return;
		}
		if (m_isLoading)
		{
			Debug.LogErrorFormat("Attempting to load subscene {0}, but another subscene is already loading! This is a bad idea!", subscene);
		}
		m_isTransitioning = true;
		m_isLoading = true;
		EnableTransitionBlocker(block: true);
		if (m_waitForSubSceneToLoadCoroutine != null)
		{
			StopCoroutine(m_waitForSubSceneToLoadCoroutine);
		}
		AssetLoader.GameObjectCallback runCallback = callback;
		if (subSceneDef.isWidget)
		{
			WidgetInstance widgetInstance = WidgetInstance.Create(subSceneDef.m_Prefab);
			widgetInstance.RegisterReadyListener(delegate
			{
				SetUpSubSceneParent(widgetInstance.gameObject);
				if (runCallback != null)
				{
					runCallback((string)subSceneDef.m_Prefab, widgetInstance.Widget.gameObject, callbackData);
				}
				UpdateAdventureModeMusic();
				m_isLoading = false;
			});
			return;
		}
		AssetLoader.Get().InstantiatePrefab((string)subSceneDef.m_Prefab, delegate(AssetReference assetRef, GameObject go, object data)
		{
			SetUpSubSceneParent(go);
			if (runCallback != null)
			{
				runCallback(assetRef, go, data);
			}
			UpdateAdventureModeMusic();
			m_isLoading = false;
		}, callbackData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnSubSceneChange(AdventureData.Adventuresubscene newscene, bool forward)
	{
		m_transitionIsGoingBack = !forward;
		LoadSubScene(newscene);
	}

	private Vector3 GetMoveDirection()
	{
		float num = 1f;
		if (m_CurrentTransitionDirection >= TransitionDirection.NX)
		{
			num *= -1f;
		}
		Vector3 zero = Vector3.zero;
		zero[(int)m_CurrentTransitionDirection % 3] = num;
		return zero;
	}

	private void OnFirstSubSceneLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		ShowExpertAIUnlockTip();
		OnSubSceneLoaded(assetRef, go, callbackData);
	}

	private void SetUpSubSceneParent(GameObject parent)
	{
		m_TransitionOutSubSceneParent = m_CurrentSubSceneParent;
		m_CurrentSubSceneParent = parent;
		GameUtils.SetParent(m_CurrentSubSceneParent, base.transform);
		m_CurrentSubSceneParent.transform.position = new Vector3(-500f, 0f, 0f);
	}

	private void OnSubSceneLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_TransitionOutSubScene = m_CurrentSubScene;
		m_CurrentSubScene = go;
		m_SubScenesLoaded++;
		AdventureSubScene component = m_CurrentSubScene.GetComponent<AdventureSubScene>();
		Action action = (Action)callbackData;
		if (component == null)
		{
			DoSubSceneTransition(component);
			action?.Invoke();
		}
		else
		{
			m_waitForSubSceneToLoadCoroutine = StartCoroutine(WaitForSubSceneToLoad(action));
		}
	}

	private void DoSubSceneTransition(AdventureSubScene subscene)
	{
		m_CurrentSubSceneParent.transform.localPosition = m_SubScenePosition;
		if (m_TransitionOutSubSceneParent == null)
		{
			CompleteTransition();
			return;
		}
		float num = ((subscene == null) ? m_DefaultTransitionAnimationTime : subscene.m_TransitionAnimationTime);
		Vector3 moveDirection = GetMoveDirection();
		GameObject delobj = m_TransitionOutSubSceneParent;
		AdventureSubScene component = m_TransitionOutSubScene.GetComponent<AdventureSubScene>();
		bool flag = m_transitionIsGoingBack;
		bool num2 = component != null && component.m_reverseTransitionAfterThisSubscene && !m_transitionIsGoingBack;
		bool flag2 = subscene != null && subscene.m_reverseTransitionAfterThisSubscene && m_transitionIsGoingBack;
		bool flag3 = subscene != null && subscene.m_reverseTransitionBeforeThisSubscene && !m_transitionIsGoingBack;
		bool flag4 = component != null && component.m_reverseTransitionBeforeThisSubscene && m_transitionIsGoingBack;
		if (num2 || flag2 || flag3 || flag4)
		{
			flag = !flag;
		}
		if (flag)
		{
			AdventureSubScene adventureSubScene = component;
			Vector3 vector = ((adventureSubScene == null) ? TransformUtil.GetBoundsOfChildren(m_TransitionOutSubScene).size : ((Vector3)adventureSubScene.m_SubSceneBounds));
			Vector3 localPosition = m_TransitionOutSubSceneParent.transform.localPosition;
			localPosition.x -= vector.x * moveDirection.x;
			localPosition.y -= vector.y * moveDirection.y;
			localPosition.z -= vector.z * moveDirection.z;
			Hashtable args = iTween.Hash("islocal", true, "position", localPosition, "time", num, "easeType", m_TransitionEaseType, "oncomplete", (Action<object>)delegate
			{
				DestroyTransitioningSubScene(delobj);
				CompleteTransition();
			}, "oncompletetarget", base.gameObject);
			iTween.MoveTo(m_TransitionOutSubScene, args);
			if (!string.IsNullOrEmpty(m_SlideOutSound))
			{
				SoundManager.Get().LoadAndPlay(m_SlideOutSound);
			}
		}
		else
		{
			AdventureSubScene component2 = m_CurrentSubScene.GetComponent<AdventureSubScene>();
			Vector3 vector2 = ((component2 == null) ? TransformUtil.GetBoundsOfChildren(m_CurrentSubScene).size : ((Vector3)component2.m_SubSceneBounds));
			Vector3 localPosition2 = m_CurrentSubSceneParent.transform.localPosition;
			Vector3 localPosition3 = m_CurrentSubSceneParent.transform.localPosition;
			localPosition3.x -= vector2.x * moveDirection.x;
			localPosition3.y -= vector2.y * moveDirection.y;
			localPosition3.z -= vector2.z * moveDirection.z;
			m_CurrentSubScene.transform.localPosition = localPosition3;
			Hashtable args2 = iTween.Hash("islocal", true, "position", localPosition2, "time", num, "easeType", m_TransitionEaseType, "oncomplete", (Action<object>)delegate
			{
				DestroyTransitioningSubScene(delobj);
				CompleteTransition();
			}, "oncompletetarget", base.gameObject);
			iTween.MoveTo(m_CurrentSubScene, args2);
			if (!string.IsNullOrEmpty(m_SlideInSound))
			{
				SoundManager.Get().LoadAndPlay(m_SlideInSound);
			}
		}
		m_TransitionOutSubScene = null;
	}

	private void DestroyTransitioningSubScene(GameObject destroysubscene)
	{
		if (destroysubscene != null)
		{
			UnityEngine.Object.Destroy(destroysubscene);
		}
	}

	private void CompleteTransition()
	{
		m_isTransitioning = false;
		AdventureSubScene component = m_CurrentSubScene.GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.NotifyTransitionComplete();
			UpdateAdventureModeMusic();
		}
		EnableTransitionBlocker(block: false);
	}

	private IEnumerator WaitForSubSceneToLoad(Action callback = null)
	{
		AdventureSubScene subscene = m_CurrentSubScene.GetComponent<AdventureSubScene>();
		while (!subscene.IsLoaded())
		{
			yield return null;
		}
		DoSubSceneTransition(subscene);
		callback?.Invoke();
	}

	private void OnSelectedModeChanged(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		UpdateAdventureModeMusic();
		if (!AdventureConfig.CanPlayMode(adventureId, modeId))
		{
			return;
		}
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		SetCurrentTransitionDirection();
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		if (gameSaveDataClientKey != 0)
		{
			bool flag = GameSaveDataManager.Get().IsDataReady(gameSaveDataClientKey);
			if (!flag && !m_adventuresThatRequestedGameSaveData.Contains(adventureId))
			{
				m_adventuresThatRequestedGameSaveData.Add(adventureId);
				GameSaveDataManager.Get().Request(gameSaveDataClientKey, OnRequestGameSaveDataClientResponse_CreateIntroConversation);
			}
			else if (flag)
			{
				OnRequestGameSaveDataClientResponse_CreateIntroConversation(success: true);
			}
		}
	}

	private void OnRequestGameSaveDataClientResponse_CreateIntroConversation(bool success)
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		if (!success)
		{
			Log.Adventures.PrintWarning($"Unable to request game save data key for adventure: {selectedAdventure}.");
		}
		AdventureDef adventureDef = GetAdventureDef(selectedAdventure);
		if (adventureDef == null)
		{
			Log.Adventures.PrintError($"Unable to get adventure def for adventure: {selectedAdventure}.");
			return;
		}
		List<AdventureDef.IntroConversationLine> introConversationLines = adventureDef.m_IntroConversationLines;
		bool shouldOnlyPlayIntroOnFirstSeen = adventureDef.m_ShouldOnlyPlayIntroOnFirstSeen;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
		if (adventureDataRecord == null)
		{
			Log.Adventures.PrintError($"Unable to get adventure data record for adventure = {selectedAdventure}, mode = {selectedMode}.");
			return;
		}
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		long value = 0L;
		if (gameSaveDataClientKey != GameSaveKeyId.INVALID)
		{
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, out value);
		}
		if (!shouldOnlyPlayIntroOnFirstSeen || value == 0L || IsDevMode)
		{
			OnSelectedModeChanged_CreateIntroConversation(0, introConversationLines, gameSaveDataClientKey);
		}
	}

	private void OnSelectedModeChanged_CreateIntroConversation(int index, List<AdventureDef.IntroConversationLine> convoLines, GameSaveKeyId gameSaveClientKey)
	{
		Action<int> finishCallback = null;
		if (index < convoLines.Count - 1)
		{
			finishCallback = delegate
			{
				if (SceneMgr.Get() != null && SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE)
				{
					OnSelectedModeChanged_CreateIntroConversation(index + 1, convoLines, gameSaveClientKey);
				}
			};
		}
		bool flag = Get() != null && Get().IsDevMode;
		if (index >= convoLines.Count - 1 && !flag && gameSaveClientKey != 0)
		{
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, 1L));
		}
		if (index < convoLines.Count)
		{
			string text = GameStrings.Get(new AssetReference(convoLines[index].VoLinePrefab).GetLegacyAssetName());
			bool allowRepeatDuringSession = flag;
			NotificationManager.Get().CreateCharacterQuote(convoLines[index].CharacterPrefab, NotificationManager.DEFAULT_CHARACTER_POS, text, convoLines[index].VoLinePrefab, allowRepeatDuringSession, 0f, finishCallback);
		}
	}

	private void OnAdventureModeChanged(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (GameUtils.IsModeHeroic(modeId))
		{
			ShowHeroicWarning();
		}
		if (adventureId == AdventureDbId.NAXXRAMAS && !Options.Get().GetBool(Option.HAS_ENTERED_NAXX))
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_INTRO2_40", "VO_KT_INTRO2_40.prefab:5615c7daf91a7ea4e8a4127b70a09682");
			Options.Get().SetBool(Option.HAS_ENTERED_NAXX, val: true);
		}
		UpdateAdventureModeMusic();
	}

	private void OnAdventureMissionChanged(ScenarioDbId mission, bool showDetails)
	{
		UpdateAdventureModeMusic();
	}

	private void ShowHeroicWarning()
	{
		if (!Options.Get().GetBool(Option.HAS_SEEN_HEROIC_WARNING))
		{
			Options.Get().SetBool(Option.HAS_SEEN_HEROIC_WARNING, val: true);
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_WARNING_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_WARNING");
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	private void ShowExpertAIUnlockTip()
	{
		if (AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, isComplete: false).Count <= 0 && (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY || Options.Get().GetBool(Option.HAS_SEEN_UNLOCK_ALL_HEROES_TRANSITION)) && !ReturningPlayerMgr.Get().IsInReturningPlayerMode && !Options.Get().GetBool(Option.HAS_SEEN_EXPERT_AI_UNLOCK, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("AdventureScene.ShowExpertAIUnlockTip"))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_EXPERT_AI_10"), "VO_INNKEEPER_EXPERT_AI_10.prefab:7979b1ca6d60f7b448686a248246542d");
			Options.Get().SetBool(Option.HAS_SEEN_EXPERT_AI_UNLOCK, val: true);
		}
	}

	private bool OnDevCheat(string func, string[] args, string rawArgs)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return true;
		}
		IsDevMode = true;
		if (args.Length != 0)
		{
			int result = 1;
			if (int.TryParse(args[0], out result))
			{
				if (result > 0)
				{
					IsDevMode = true;
					DevModeSetting = result;
				}
				else
				{
					IsDevMode = false;
					DevModeSetting = 0;
				}
			}
		}
		if (UIStatus.Get() != null)
		{
			UIStatus.Get().AddInfo($"{func}: IsDevMode={IsDevMode} DevModeSetting={DevModeSetting}");
		}
		return true;
	}

	private void EnableTransitionBlocker(bool block)
	{
		if (m_transitionClickBlocker != null)
		{
			m_transitionClickBlocker.SetActive(block);
		}
	}

	private void NotifyAchieveManagerOfAdventureSceneLoaded()
	{
		AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ADVENTURE);
	}

	private void SetCurrentTransitionDirection()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		if (selectedAdventureDataRecord == null)
		{
			m_CurrentTransitionDirection = m_TransitionDirection;
			return;
		}
		TransitionDirection transitionDirection = EnumUtils.SafeParse(selectedAdventureDataRecord.SubsceneTransitionDirection, TransitionDirection.INVALID, ignoreCase: true);
		if (transitionDirection != TransitionDirection.INVALID)
		{
			m_CurrentTransitionDirection = transitionDirection;
		}
		else
		{
			m_CurrentTransitionDirection = m_TransitionDirection;
		}
	}
}
