using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200004C RID: 76
[CustomEditClass]
public class AdventureScene : PegasusScene
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000442 RID: 1090 RVA: 0x00019C70 File Offset: 0x00017E70
	// (set) Token: 0x06000443 RID: 1091 RVA: 0x00019C78 File Offset: 0x00017E78
	public bool IsDevMode { get; set; }

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000444 RID: 1092 RVA: 0x00019C81 File Offset: 0x00017E81
	// (set) Token: 0x06000445 RID: 1093 RVA: 0x00019C89 File Offset: 0x00017E89
	public int DevModeSetting { get; set; }

	// Token: 0x06000446 RID: 1094 RVA: 0x00019C94 File Offset: 0x00017E94
	protected override void Awake()
	{
		base.Awake();
		AdventureScene.s_instance = this;
		this.m_CurrentSubScene = null;
		this.m_TransitionOutSubScene = null;
		this.m_CurrentTransitionDirection = this.m_TransitionDirection;
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.OnAdventureSceneAwake();
		adventureConfig.AddSubSceneChangeListener(new AdventureConfig.SubSceneChange(this.OnSubSceneChange));
		adventureConfig.AddSelectedModeChangeListener(new AdventureConfig.SelectedModeChange(this.OnSelectedModeChanged));
		adventureConfig.AddAdventureModeChangeListener(new AdventureConfig.AdventureModeChange(this.OnAdventureModeChanged));
		adventureConfig.AddAdventureMissionSetListener(new AdventureConfig.AdventureMissionSet(this.OnAdventureMissionChanged));
		this.m_StartupAssetLoads++;
		this.SetCurrentTransitionDirection();
		Options.Get().SetBool(Option.BUNDLE_JUST_PURCHASE_IN_HUB, false);
		if (HearthstoneApplication.IsInternal())
		{
			CheatMgr.Get().RegisterCategory("adventure");
			CheatMgr.Get().RegisterCheatHandler("advdev", new CheatMgr.ProcessCheatCallback(this.OnDevCheat), null, null, null);
			CheatMgr.Get().DefaultCategory();
		}
		this.m_adventureDefCache = new AdventureDefCache(true);
		this.m_adventureWingDefCache = new AdventureWingDefCache(true);
		this.NotifyAchieveManagerOfAdventureSceneLoaded();
		this.LoadSubScene(adventureConfig.CurrentSubScene, new AssetLoader.GameObjectCallback(this.OnFirstSubSceneLoaded), new Action(this.OnStartupAssetLoaded));
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00019DBF File Offset: 0x00017FBF
	private void Start()
	{
		AdventureConfig.Get().UpdatePresence();
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00019DCB File Offset: 0x00017FCB
	private void OnDestroy()
	{
		AdventureScene.s_instance = null;
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00019DDF File Offset: 0x00017FDF
	public static AdventureScene Get()
	{
		return AdventureScene.s_instance;
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00019DE6 File Offset: 0x00017FE6
	public override bool IsUnloading()
	{
		return this.m_Unloading;
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00019DF0 File Offset: 0x00017FF0
	public override void Unload()
	{
		this.m_Unloading = true;
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.ClearBossDefs();
		DeckPickerTray.Get().Unload();
		adventureConfig.RemoveAdventureModeChangeListener(new AdventureConfig.AdventureModeChange(this.OnAdventureModeChanged));
		adventureConfig.RemoveSelectedModeChangeListener(new AdventureConfig.SelectedModeChange(this.OnSelectedModeChanged));
		adventureConfig.RemoveSubSceneChangeListener(new AdventureConfig.SubSceneChange(this.OnSubSceneChange));
		adventureConfig.OnAdventureSceneUnload();
		CheatMgr.Get().UnregisterCheatHandler("advdev", new CheatMgr.ProcessCheatCallback(this.OnDevCheat));
		this.m_Unloading = false;
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00019E76 File Offset: 0x00018076
	public override bool IsTransitioning()
	{
		return this.m_isTransitioning;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00019E7E File Offset: 0x0001807E
	public bool IsInitialScreen()
	{
		return this.m_SubScenesLoaded <= 1;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00019E8C File Offset: 0x0001808C
	public AdventureDef GetAdventureDef(AdventureDbId advId)
	{
		return this.m_adventureDefCache.GetDef(advId);
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00019E9A File Offset: 0x0001809A
	public List<AdventureDef> GetSortedAdventureDefs()
	{
		List<AdventureDef> list = new List<AdventureDef>(this.m_adventureDefCache.Values);
		list.Sort((AdventureDef l, AdventureDef r) => r.GetSortOrder() - l.GetSortOrder());
		return list;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x00019ED1 File Offset: 0x000180D1
	public AdventureWingDef GetWingDef(WingDbId wingId)
	{
		return this.m_adventureWingDefCache.GetDef(wingId);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00019EE0 File Offset: 0x000180E0
	private void UpdateAdventureModeMusic()
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureData.Adventuresubscene currentSubScene = AdventureConfig.Get().CurrentSubScene;
		MusicPlaylistType? musicPlaylistType = null;
		foreach (AdventureScene.AdventureModeMusic adventureModeMusic in this.m_AdventureModeMusic)
		{
			if (adventureModeMusic.m_subsceneId == currentSubScene && adventureModeMusic.m_adventureId == selectedAdventure)
			{
				MusicPlaylistType? musicPlaylistType2;
				if (AdventureScene.GetAdventureModeMusicWingOverride(adventureModeMusic, out musicPlaylistType2))
				{
					musicPlaylistType = musicPlaylistType2;
					break;
				}
				musicPlaylistType = new MusicPlaylistType?(adventureModeMusic.m_playlist);
				break;
			}
			else if (adventureModeMusic.m_subsceneId == currentSubScene && adventureModeMusic.m_adventureId == AdventureDbId.INVALID)
			{
				musicPlaylistType = new MusicPlaylistType?(adventureModeMusic.m_playlist);
			}
		}
		if (musicPlaylistType != null)
		{
			MusicManager.Get().StartPlaylist(musicPlaylistType.Value);
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00019FBC File Offset: 0x000181BC
	private static bool GetAdventureModeMusicWingOverride(AdventureScene.AdventureModeMusic music, out MusicPlaylistType? playlist)
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
		WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)lastSelectedMission);
		WingDbId wingDbId = (WingDbId)((wingRecordFromMissionId != null) ? wingRecordFromMissionId.ID : 0);
		if (wingDbId == WingDbId.INVALID)
		{
			return false;
		}
		foreach (AdventureScene.AdventureModeMusicWingOverride adventureModeMusicWingOverride in music.m_wingOverrides)
		{
			if (adventureModeMusicWingOverride.m_wingId == wingDbId)
			{
				playlist = new MusicPlaylistType?(adventureModeMusicWingOverride.m_playlist);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0001A070 File Offset: 0x00018270
	private void OnStartupAssetLoaded()
	{
		this.m_StartupAssetLoads--;
		if (this.m_StartupAssetLoads > 0)
		{
			return;
		}
		this.UpdateAdventureModeMusic();
		SceneMgr.Get().NotifySceneLoaded();
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0001A09A File Offset: 0x0001829A
	private void LoadSubScene(AdventureData.Adventuresubscene subscene)
	{
		this.LoadSubScene(subscene, new AssetLoader.GameObjectCallback(this.OnSubSceneLoaded), null);
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001A0B0 File Offset: 0x000182B0
	private void LoadSubScene(AdventureData.Adventuresubscene subscene, AssetLoader.GameObjectCallback callback, object callbackData = null)
	{
		AdventureScene.AdventureSubSceneDef subSceneDef = this.m_SubSceneDefs.Find((AdventureScene.AdventureSubSceneDef item) => item.m_SubScene == subscene);
		if (subSceneDef == null)
		{
			Debug.LogErrorFormat("Subscene {0} prefab not defined in m_SubSceneDefs", new object[]
			{
				subscene
			});
			return;
		}
		if (this.m_isLoading)
		{
			Debug.LogErrorFormat("Attempting to load subscene {0}, but another subscene is already loading! This is a bad idea!", new object[]
			{
				subscene
			});
		}
		this.m_isTransitioning = true;
		this.m_isLoading = true;
		this.EnableTransitionBlocker(true);
		if (this.m_waitForSubSceneToLoadCoroutine != null)
		{
			base.StopCoroutine(this.m_waitForSubSceneToLoadCoroutine);
		}
		AssetLoader.GameObjectCallback runCallback = callback;
		if (subSceneDef.isWidget)
		{
			WidgetInstance widgetInstance = WidgetInstance.Create(subSceneDef.m_Prefab, false);
			widgetInstance.RegisterReadyListener(delegate(object _)
			{
				this.SetUpSubSceneParent(widgetInstance.gameObject);
				if (runCallback != null)
				{
					runCallback(subSceneDef.m_Prefab, widgetInstance.Widget.gameObject, callbackData);
				}
				this.UpdateAdventureModeMusic();
				this.m_isLoading = false;
			}, null, true);
			return;
		}
		AssetLoader.Get().InstantiatePrefab(subSceneDef.m_Prefab, delegate(AssetReference assetRef, GameObject go, object data)
		{
			this.SetUpSubSceneParent(go);
			if (runCallback != null)
			{
				runCallback(assetRef, go, data);
			}
			this.UpdateAdventureModeMusic();
			this.m_isLoading = false;
		}, callbackData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0001A1FA File Offset: 0x000183FA
	private void OnSubSceneChange(AdventureData.Adventuresubscene newscene, bool forward)
	{
		this.m_transitionIsGoingBack = !forward;
		this.LoadSubScene(newscene);
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001A210 File Offset: 0x00018410
	private Vector3 GetMoveDirection()
	{
		float num = 1f;
		if (this.m_CurrentTransitionDirection >= AdventureScene.TransitionDirection.NX)
		{
			num *= -1f;
		}
		Vector3 zero = Vector3.zero;
		zero[(int)(this.m_CurrentTransitionDirection % AdventureScene.TransitionDirection.NX)] = num;
		return zero;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0001A24B File Offset: 0x0001844B
	private void OnFirstSubSceneLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.ShowExpertAIUnlockTip();
		this.OnSubSceneLoaded(assetRef, go, callbackData);
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0001A25C File Offset: 0x0001845C
	private void SetUpSubSceneParent(GameObject parent)
	{
		this.m_TransitionOutSubSceneParent = this.m_CurrentSubSceneParent;
		this.m_CurrentSubSceneParent = parent;
		GameUtils.SetParent(this.m_CurrentSubSceneParent, base.transform, false);
		this.m_CurrentSubSceneParent.transform.position = new Vector3(-500f, 0f, 0f);
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0001A2B4 File Offset: 0x000184B4
	private void OnSubSceneLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_TransitionOutSubScene = this.m_CurrentSubScene;
		this.m_CurrentSubScene = go;
		this.m_SubScenesLoaded++;
		AdventureSubScene component = this.m_CurrentSubScene.GetComponent<AdventureSubScene>();
		Action action = (Action)callbackData;
		if (component == null)
		{
			this.DoSubSceneTransition(component);
			if (action != null)
			{
				action();
				return;
			}
		}
		else
		{
			this.m_waitForSubSceneToLoadCoroutine = base.StartCoroutine(this.WaitForSubSceneToLoad(action));
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0001A324 File Offset: 0x00018524
	private void DoSubSceneTransition(AdventureSubScene subscene)
	{
		this.m_CurrentSubSceneParent.transform.localPosition = this.m_SubScenePosition;
		if (this.m_TransitionOutSubSceneParent == null)
		{
			this.CompleteTransition();
			return;
		}
		float num = (subscene == null) ? this.m_DefaultTransitionAnimationTime : subscene.m_TransitionAnimationTime;
		Vector3 moveDirection = this.GetMoveDirection();
		GameObject delobj = this.m_TransitionOutSubSceneParent;
		AdventureSubScene component = this.m_TransitionOutSubScene.GetComponent<AdventureSubScene>();
		bool flag = this.m_transitionIsGoingBack;
		bool flag2 = component != null && component.m_reverseTransitionAfterThisSubscene && !this.m_transitionIsGoingBack;
		bool flag3 = subscene != null && subscene.m_reverseTransitionAfterThisSubscene && this.m_transitionIsGoingBack;
		bool flag4 = subscene != null && subscene.m_reverseTransitionBeforeThisSubscene && !this.m_transitionIsGoingBack;
		bool flag5 = component != null && component.m_reverseTransitionBeforeThisSubscene && this.m_transitionIsGoingBack;
		if (flag2 || flag3 || flag4 || flag5)
		{
			flag = !flag;
		}
		if (flag)
		{
			AdventureSubScene adventureSubScene = component;
			Vector3 vector = (adventureSubScene == null) ? TransformUtil.GetBoundsOfChildren(this.m_TransitionOutSubScene).size : adventureSubScene.m_SubSceneBounds;
			Vector3 localPosition = this.m_TransitionOutSubSceneParent.transform.localPosition;
			localPosition.x -= vector.x * moveDirection.x;
			localPosition.y -= vector.y * moveDirection.y;
			localPosition.z -= vector.z * moveDirection.z;
			Hashtable args = iTween.Hash(new object[]
			{
				"islocal",
				true,
				"position",
				localPosition,
				"time",
				num,
				"easeType",
				this.m_TransitionEaseType,
				"oncomplete",
				new Action<object>(delegate(object e)
				{
					this.DestroyTransitioningSubScene(delobj);
					this.CompleteTransition();
				}),
				"oncompletetarget",
				base.gameObject
			});
			iTween.MoveTo(this.m_TransitionOutSubScene, args);
			if (!string.IsNullOrEmpty(this.m_SlideOutSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_SlideOutSound);
			}
		}
		else
		{
			AdventureSubScene component2 = this.m_CurrentSubScene.GetComponent<AdventureSubScene>();
			Vector3 vector2 = (component2 == null) ? TransformUtil.GetBoundsOfChildren(this.m_CurrentSubScene).size : component2.m_SubSceneBounds;
			Vector3 localPosition2 = this.m_CurrentSubSceneParent.transform.localPosition;
			Vector3 localPosition3 = this.m_CurrentSubSceneParent.transform.localPosition;
			localPosition3.x -= vector2.x * moveDirection.x;
			localPosition3.y -= vector2.y * moveDirection.y;
			localPosition3.z -= vector2.z * moveDirection.z;
			this.m_CurrentSubScene.transform.localPosition = localPosition3;
			Hashtable args2 = iTween.Hash(new object[]
			{
				"islocal",
				true,
				"position",
				localPosition2,
				"time",
				num,
				"easeType",
				this.m_TransitionEaseType,
				"oncomplete",
				new Action<object>(delegate(object e)
				{
					this.DestroyTransitioningSubScene(delobj);
					this.CompleteTransition();
				}),
				"oncompletetarget",
				base.gameObject
			});
			iTween.MoveTo(this.m_CurrentSubScene, args2);
			if (!string.IsNullOrEmpty(this.m_SlideInSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_SlideInSound);
			}
		}
		this.m_TransitionOutSubScene = null;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0001A6F2 File Offset: 0x000188F2
	private void DestroyTransitioningSubScene(GameObject destroysubscene)
	{
		if (destroysubscene != null)
		{
			UnityEngine.Object.Destroy(destroysubscene);
		}
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0001A704 File Offset: 0x00018904
	private void CompleteTransition()
	{
		this.m_isTransitioning = false;
		AdventureSubScene component = this.m_CurrentSubScene.GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.NotifyTransitionComplete();
			this.UpdateAdventureModeMusic();
		}
		this.EnableTransitionBlocker(false);
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0001A740 File Offset: 0x00018940
	private IEnumerator WaitForSubSceneToLoad(Action callback = null)
	{
		AdventureSubScene subscene = this.m_CurrentSubScene.GetComponent<AdventureSubScene>();
		while (!subscene.IsLoaded())
		{
			yield return null;
		}
		this.DoSubSceneTransition(subscene);
		if (callback != null)
		{
			callback();
		}
		yield break;
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0001A758 File Offset: 0x00018958
	private void OnSelectedModeChanged(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		this.UpdateAdventureModeMusic();
		if (!AdventureConfig.CanPlayMode(adventureId, modeId, true))
		{
			return;
		}
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		this.SetCurrentTransitionDirection();
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		if (gameSaveDataClientKey != (GameSaveKeyId)0)
		{
			bool flag = GameSaveDataManager.Get().IsDataReady(gameSaveDataClientKey);
			if (!flag && !this.m_adventuresThatRequestedGameSaveData.Contains(adventureId))
			{
				this.m_adventuresThatRequestedGameSaveData.Add(adventureId);
				GameSaveDataManager.Get().Request(gameSaveDataClientKey, new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnRequestGameSaveDataClientResponse_CreateIntroConversation));
				return;
			}
			if (flag)
			{
				this.OnRequestGameSaveDataClientResponse_CreateIntroConversation(true);
			}
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0001A7D8 File Offset: 0x000189D8
	private void OnRequestGameSaveDataClientResponse_CreateIntroConversation(bool success)
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		if (!success)
		{
			Log.Adventures.PrintWarning(string.Format("Unable to request game save data key for adventure: {0}.", selectedAdventure), Array.Empty<object>());
		}
		AdventureDef adventureDef = this.GetAdventureDef(selectedAdventure);
		if (adventureDef == null)
		{
			Log.Adventures.PrintError(string.Format("Unable to get adventure def for adventure: {0}.", selectedAdventure), Array.Empty<object>());
			return;
		}
		List<AdventureDef.IntroConversationLine> introConversationLines = adventureDef.m_IntroConversationLines;
		bool shouldOnlyPlayIntroOnFirstSeen = adventureDef.m_ShouldOnlyPlayIntroOnFirstSeen;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
		if (adventureDataRecord == null)
		{
			Log.Adventures.PrintError(string.Format("Unable to get adventure data record for adventure = {0}, mode = {1}.", selectedAdventure, selectedMode), Array.Empty<object>());
			return;
		}
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		long num = 0L;
		if (gameSaveDataClientKey != GameSaveKeyId.INVALID)
		{
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, out num);
		}
		if (!shouldOnlyPlayIntroOnFirstSeen || num == 0L || this.IsDevMode)
		{
			this.OnSelectedModeChanged_CreateIntroConversation(0, introConversationLines, gameSaveDataClientKey);
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001A8D0 File Offset: 0x00018AD0
	private void OnSelectedModeChanged_CreateIntroConversation(int index, List<AdventureDef.IntroConversationLine> convoLines, GameSaveKeyId gameSaveClientKey)
	{
		Action<int> finishCallback = null;
		if (index < convoLines.Count - 1)
		{
			finishCallback = delegate(int groupId)
			{
				if (SceneMgr.Get() == null || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
				{
					return;
				}
				this.OnSelectedModeChanged_CreateIntroConversation(index + 1, convoLines, gameSaveClientKey);
			};
		}
		bool flag = AdventureScene.Get() != null && AdventureScene.Get().IsDevMode;
		if (index >= convoLines.Count - 1 && !flag && gameSaveClientKey != (GameSaveKeyId)0)
		{
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveClientKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, new long[]
			{
				1L
			}), null);
		}
		if (index >= convoLines.Count)
		{
			return;
		}
		string text = GameStrings.Get(new AssetReference(convoLines[index].VoLinePrefab).GetLegacyAssetName());
		bool allowRepeatDuringSession = flag;
		NotificationManager.Get().CreateCharacterQuote(convoLines[index].CharacterPrefab, NotificationManager.DEFAULT_CHARACTER_POS, text, convoLines[index].VoLinePrefab, allowRepeatDuringSession, 0f, finishCallback, CanvasAnchor.BOTTOM_LEFT, false);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0001AA0C File Offset: 0x00018C0C
	private void OnAdventureModeChanged(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (GameUtils.IsModeHeroic(modeId))
		{
			this.ShowHeroicWarning();
		}
		if (adventureId == AdventureDbId.NAXXRAMAS && !Options.Get().GetBool(Option.HAS_ENTERED_NAXX))
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_INTRO2_40", "VO_KT_INTRO2_40.prefab:5615c7daf91a7ea4e8a4127b70a09682", true);
			Options.Get().SetBool(Option.HAS_ENTERED_NAXX, true);
		}
		this.UpdateAdventureModeMusic();
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0001AA68 File Offset: 0x00018C68
	private void OnAdventureMissionChanged(ScenarioDbId mission, bool showDetails)
	{
		this.UpdateAdventureModeMusic();
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0001AA70 File Offset: 0x00018C70
	private void ShowHeroicWarning()
	{
		if (Options.Get().GetBool(Option.HAS_SEEN_HEROIC_WARNING))
		{
			return;
		}
		Options.Get().SetBool(Option.HAS_SEEN_HEROIC_WARNING, true);
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_WARNING_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_WARNING");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0001AAE0 File Offset: 0x00018CE0
	private void ShowExpertAIUnlockTip()
	{
		if (AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, false).Count > 0)
		{
			return;
		}
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && !Options.Get().GetBool(Option.HAS_SEEN_UNLOCK_ALL_HEROES_TRANSITION))
		{
			return;
		}
		if (!ReturningPlayerMgr.Get().IsInReturningPlayerMode && !Options.Get().GetBool(Option.HAS_SEEN_EXPERT_AI_UNLOCK, false) && UserAttentionManager.CanShowAttentionGrabber("AdventureScene.ShowExpertAIUnlockTip"))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_EXPERT_AI_10"), "VO_INNKEEPER_EXPERT_AI_10.prefab:7979b1ca6d60f7b448686a248246542d", 0f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_EXPERT_AI_UNLOCK, true);
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0001AB80 File Offset: 0x00018D80
	private bool OnDevCheat(string func, string[] args, string rawArgs)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return true;
		}
		this.IsDevMode = true;
		if (args.Length != 0)
		{
			int num = 1;
			if (int.TryParse(args[0], out num))
			{
				if (num > 0)
				{
					this.IsDevMode = true;
					this.DevModeSetting = num;
				}
				else
				{
					this.IsDevMode = false;
					this.DevModeSetting = 0;
				}
			}
		}
		if (UIStatus.Get() != null)
		{
			UIStatus.Get().AddInfo(string.Format("{0}: IsDevMode={1} DevModeSetting={2}", func, this.IsDevMode, this.DevModeSetting));
		}
		return true;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0001AC0A File Offset: 0x00018E0A
	private void EnableTransitionBlocker(bool block)
	{
		if (this.m_transitionClickBlocker != null)
		{
			this.m_transitionClickBlocker.SetActive(block);
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0001AC26 File Offset: 0x00018E26
	private void NotifyAchieveManagerOfAdventureSceneLoaded()
	{
		AchieveManager.Get().NotifyOfClick(global::Achievement.ClickTriggerType.BUTTON_ADVENTURE);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001AC34 File Offset: 0x00018E34
	private void SetCurrentTransitionDirection()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		if (selectedAdventureDataRecord == null)
		{
			this.m_CurrentTransitionDirection = this.m_TransitionDirection;
			return;
		}
		AdventureScene.TransitionDirection transitionDirection = EnumUtils.SafeParse<AdventureScene.TransitionDirection>(selectedAdventureDataRecord.SubsceneTransitionDirection, AdventureScene.TransitionDirection.INVALID, true);
		if (transitionDirection != AdventureScene.TransitionDirection.INVALID)
		{
			this.m_CurrentTransitionDirection = transitionDirection;
			return;
		}
		this.m_CurrentTransitionDirection = this.m_TransitionDirection;
	}

	// Token: 0x040002F6 RID: 758
	private static AdventureScene s_instance;

	// Token: 0x040002F7 RID: 759
	[CustomEditField(Sections = "Transition Blocker")]
	public GameObject m_transitionClickBlocker;

	// Token: 0x040002F8 RID: 760
	[CustomEditField(Sections = "Transition Motions")]
	public Vector3 m_SubScenePosition = Vector3.zero;

	// Token: 0x040002F9 RID: 761
	[CustomEditField(Sections = "Transition Motions")]
	public float m_DefaultTransitionAnimationTime = 1f;

	// Token: 0x040002FA RID: 762
	[CustomEditField(Sections = "Transition Motions")]
	public iTween.EaseType m_TransitionEaseType = iTween.EaseType.easeInOutSine;

	// Token: 0x040002FB RID: 763
	[CustomEditField(Sections = "Transition Motions")]
	public AdventureScene.TransitionDirection m_TransitionDirection;

	// Token: 0x040002FC RID: 764
	[CustomEditField(Sections = "Transition Sounds", T = EditType.SOUND_PREFAB)]
	public string m_SlideInSound;

	// Token: 0x040002FD RID: 765
	[CustomEditField(Sections = "Transition Sounds", T = EditType.SOUND_PREFAB)]
	public string m_SlideOutSound;

	// Token: 0x040002FE RID: 766
	[CustomEditField(Sections = "Adventure Subscene Prefabs")]
	public List<AdventureScene.AdventureSubSceneDef> m_SubSceneDefs = new List<AdventureScene.AdventureSubSceneDef>();

	// Token: 0x040002FF RID: 767
	[CustomEditField(Sections = "Music Settings")]
	public List<AdventureScene.AdventureModeMusic> m_AdventureModeMusic = new List<AdventureScene.AdventureModeMusic>();

	// Token: 0x04000302 RID: 770
	private GameObject m_TransitionOutSubSceneParent;

	// Token: 0x04000303 RID: 771
	private GameObject m_CurrentSubSceneParent;

	// Token: 0x04000304 RID: 772
	private GameObject m_TransitionOutSubScene;

	// Token: 0x04000305 RID: 773
	private GameObject m_CurrentSubScene;

	// Token: 0x04000306 RID: 774
	private bool m_transitionIsGoingBack;

	// Token: 0x04000307 RID: 775
	private int m_StartupAssetLoads;

	// Token: 0x04000308 RID: 776
	private int m_SubScenesLoaded;

	// Token: 0x04000309 RID: 777
	private bool m_MusicStopped;

	// Token: 0x0400030A RID: 778
	private bool m_Unloading;

	// Token: 0x0400030B RID: 779
	private AdventureScene.TransitionDirection m_CurrentTransitionDirection;

	// Token: 0x0400030C RID: 780
	private bool m_isTransitioning;

	// Token: 0x0400030D RID: 781
	private bool m_isLoading;

	// Token: 0x0400030E RID: 782
	private Coroutine m_waitForSubSceneToLoadCoroutine;

	// Token: 0x0400030F RID: 783
	private const AdventureData.Adventuresubscene s_StartMode = AdventureData.Adventuresubscene.CHOOSER;

	// Token: 0x04000310 RID: 784
	private List<AdventureDbId> m_adventuresThatRequestedGameSaveData = new List<AdventureDbId>();

	// Token: 0x04000311 RID: 785
	private AdventureDefCache m_adventureDefCache;

	// Token: 0x04000312 RID: 786
	private AdventureWingDefCache m_adventureWingDefCache;

	// Token: 0x02001319 RID: 4889
	public enum TransitionDirection
	{
		// Token: 0x0400A58E RID: 42382
		INVALID = -1,
		// Token: 0x0400A58F RID: 42383
		X,
		// Token: 0x0400A590 RID: 42384
		Y,
		// Token: 0x0400A591 RID: 42385
		Z,
		// Token: 0x0400A592 RID: 42386
		NX,
		// Token: 0x0400A593 RID: 42387
		NY,
		// Token: 0x0400A594 RID: 42388
		NZ
	}

	// Token: 0x0200131A RID: 4890
	[Serializable]
	public class AdventureModeMusicWingOverride
	{
		// Token: 0x0400A595 RID: 42389
		public WingDbId m_wingId;

		// Token: 0x0400A596 RID: 42390
		public MusicPlaylistType m_playlist;
	}

	// Token: 0x0200131B RID: 4891
	[Serializable]
	public class AdventureModeMusic
	{
		// Token: 0x0400A597 RID: 42391
		public AdventureData.Adventuresubscene m_subsceneId;

		// Token: 0x0400A598 RID: 42392
		public AdventureDbId m_adventureId;

		// Token: 0x0400A599 RID: 42393
		public MusicPlaylistType m_playlist;

		// Token: 0x0400A59A RID: 42394
		[CustomEditField(ListSortable = true)]
		public List<AdventureScene.AdventureModeMusicWingOverride> m_wingOverrides;
	}

	// Token: 0x0200131C RID: 4892
	[Serializable]
	public class AdventureSubSceneDef
	{
		// Token: 0x0400A59B RID: 42395
		[CustomEditField(ListSortable = true)]
		public AdventureData.Adventuresubscene m_SubScene;

		// Token: 0x0400A59C RID: 42396
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public String_MobileOverride m_Prefab;

		// Token: 0x0400A59D RID: 42397
		public bool isWidget;
	}
}
