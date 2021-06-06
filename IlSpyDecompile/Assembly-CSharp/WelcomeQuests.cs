using System.Collections.Generic;
using Assets;
using UnityEngine;

public class WelcomeQuests : MonoBehaviour
{
	public delegate void DelOnWelcomeQuestsClosed();

	private class ShowRequestData
	{
		public bool m_fromLogin;

		public DelOnWelcomeQuestsClosed m_onCloseCallback;

		public bool m_keepRichPresence;

		public Achievement m_achievement;

		public bool IsSpecialQuestRequest()
		{
			return m_achievement != null;
		}
	}

	public QuestTile m_questTilePrefab;

	public Collider m_placementCollider;

	public GameObject m_placementColliderPhoneNoIksBone;

	public Banner m_headlineBanner;

	public PegUIElement m_clickCatcher;

	public UberText m_questCaption;

	public UberText m_allCompletedCaption;

	public GameObject m_friendWeekReminderContainer;

	public UberText m_friendWeekReminderCaption;

	public GameObject m_friendWeekReminderGlow;

	public Transform m_phoneNoIksCaptionBone;

	public Animation m_bannerFX;

	public GameObject m_Root;

	public GameObject[] m_normalFXs;

	public GameObject[] m_legendaryFXs;

	private static WelcomeQuests s_instance;

	private static bool s_fullScreenFXActive;

	private ShowRequestData m_showRequestData;

	private List<QuestTile> m_currentQuests;

	private Vector3 m_originalScale;

	private float m_loginQuestShownTime;

	private bool m_bnetButtonsLocked;

	private const float SPECIAL_QUEST_DISMISS_DELAY = 2.5f;

	public static bool Show(UserAttentionBlocker blocker, bool fromLogin, DelOnWelcomeQuestsClosed onCloseCallback = null, bool keepRichPresence = false)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "WelcomeQuests.Show:" + fromLogin))
		{
			onCloseCallback?.Invoke();
			return false;
		}
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.WELCOMEQUESTS);
		ShowRequestData showRequestData = new ShowRequestData
		{
			m_fromLogin = fromLogin,
			m_onCloseCallback = onCloseCallback,
			m_keepRichPresence = keepRichPresence,
			m_achievement = null
		};
		if (s_instance != null)
		{
			Debug.Log("WelcomeQuests.Show(): requested to show welcome quests while it was already active!");
			s_instance.ReinitAndShow(showRequestData);
			return true;
		}
		AssetLoader.Get().InstantiatePrefab("WelcomeQuests.prefab:c1b288441ca1a05419dcb2bd498b8830", OnWelcomeQuestsLoaded, showRequestData);
		return true;
	}

	public static void ShowSpecialQuest(UserAttentionBlocker blocker, Achievement achievement, DelOnWelcomeQuestsClosed onCloseCallback = null, bool keepRichPresence = false)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "WelcomeQuests.ShowSpecialQuest:" + ((achievement == null) ? "null" : achievement.ID.ToString())))
		{
			onCloseCallback?.Invoke();
			return;
		}
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.WELCOMEQUESTS);
		ShowRequestData showRequestData = new ShowRequestData
		{
			m_fromLogin = false,
			m_onCloseCallback = onCloseCallback,
			m_keepRichPresence = keepRichPresence,
			m_achievement = achievement
		};
		if (s_instance != null)
		{
			Debug.Log("WelcomeQuests.Show(): requested to show welcome quests while it was already active!");
			s_instance.ReinitAndShow(showRequestData);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab("WelcomeQuests.prefab:c1b288441ca1a05419dcb2bd498b8830", OnWelcomeQuestsLoaded, showRequestData);
		}
	}

	public static void Hide()
	{
		if (!(s_instance == null))
		{
			s_instance.Close();
		}
	}

	public static WelcomeQuests Get()
	{
		return s_instance;
	}

	public QuestTile GetFirstQuestTile()
	{
		return m_currentQuests[0];
	}

	public int CompleteAndReplaceAutoDestroyQuestTile(int achieveId)
	{
		foreach (QuestTile currentQuest in m_currentQuests)
		{
			if (currentQuest.GetQuestID() == achieveId)
			{
				currentQuest.CompleteAndAutoDestroyQuest();
				return AchieveManager.Get().GetAchievement(achieveId).LinkToId;
			}
		}
		return 0;
	}

	public void ActivateClickCatcher()
	{
		m_clickCatcher.gameObject.SetActive(value: true);
		RegisterClickCatcher();
	}

	private void Awake()
	{
		m_originalScale = base.transform.localScale;
		m_headlineBanner.gameObject.SetActive(value: false);
		m_friendWeekReminderContainer.SetActive(value: false);
		m_questCaption.gameObject.SetActive(value: false);
		m_clickCatcher.gameObject.SetActive(value: false);
		m_allCompletedCaption.gameObject.SetActive(value: false);
		SoundManager.Get().Load("new_quest_pop_up.prefab:5ef0d42842220a648bdebd874ba716e4");
		SoundManager.Get().Load("existing_quest_pop_up.prefab:9b4dcb4e8233104409605a8bd5f3095d");
		SoundManager.Get().Load("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
		SceneMgr.Get().RegisterScenePreLoadEvent(OnPreLoadScene);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	private void OnDestroy()
	{
		FadeEffectsOut();
		if (s_instance != null)
		{
			CleanUpEventListeners();
			s_instance = null;
			UnlockBnetButtons();
			if (DeckPickerTrayDisplay.Get() != null && (bool)UniversalInputManager.UsePhoneUI)
			{
				DeckPickerTrayDisplay.Get().SetHeroDetailsTrayToIgnoreFullScreenEffects(ignoreEffects: true);
			}
			InnKeepersSpecial.Close();
		}
	}

	private static void OnWelcomeQuestsLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (SceneMgr.Get() != null && SceneMgr.Get().IsInGame())
		{
			if (s_instance != null)
			{
				s_instance.Close();
			}
			return;
		}
		if (go == null)
		{
			Debug.LogError($"WelcomeQuests.OnWelcomeQuestsLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		s_instance = go.GetComponent<WelcomeQuests>();
		if (s_instance == null)
		{
			Debug.LogError($"WelcomeQuests.OnWelcomeQuestsLoaded() - ERROR object \"{assetRef}\" has no WelcomeQuests component");
			return;
		}
		ShowRequestData showRequestData = callbackData as ShowRequestData;
		s_instance.InitAndShow(showRequestData);
	}

	private List<Achievement> GetQuestsToShow(ShowRequestData showRequestData)
	{
		List<Achievement> list;
		if (showRequestData.m_achievement == null)
		{
			list = AchieveManager.Get().GetActiveQuests();
		}
		else
		{
			list = new List<Achievement>();
			list.Add(showRequestData.m_achievement);
		}
		return list;
	}

	private void InitAndShow(ShowRequestData showRequestData)
	{
		OverlayUI.Get().AddGameObject(base.gameObject);
		m_showRequestData = showRequestData;
		LockBnetButtons();
		List<Achievement> questsToShow = GetQuestsToShow(m_showRequestData);
		if (questsToShow.Count < 1 && !InnKeepersSpecial.Get().LoadedSuccessfully())
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! loadedSucsesfully={0}", InnKeepersSpecial.Get().LoadedSuccessfully());
			Close();
			return;
		}
		List<Achievement> list = new List<Achievement>();
		foreach (Achievement item in questsToShow)
		{
			if (item.IsNewlyActive())
			{
				list.Add(item);
			}
		}
		m_clickCatcher.gameObject.SetActive(value: true);
		if (m_showRequestData.IsSpecialQuestRequest())
		{
			Invoke("RegisterClickCatcher", 2.5f);
		}
		else if (!AchieveManager.Get().HasActiveAutoDestroyQuests() && !AchieveManager.Get().HasActiveUnseenWelcomeQuestDialog())
		{
			RegisterClickCatcher();
		}
		CheckShowInnkeepersSpecial();
		ShowQuests();
		FadeEffectsIn();
		if (DeckPickerTrayDisplay.Get() != null && (bool)UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().SetHeroDetailsTrayToIgnoreFullScreenEffects(ignoreEffects: false);
		}
		base.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		iTween.ScaleTo(base.gameObject, m_originalScale, 0.5f);
		Navigation.PushUnique(OnNavigateBack);
		NarrativeManager.Get().OnWelcomeQuestsShown(questsToShow, list);
	}

	private void ReinitAndShow(ShowRequestData showRequestData)
	{
		FadeEffectsOut();
		UnlockBnetButtons();
		InitAndShow(showRequestData);
	}

	private void RegisterClickCatcher()
	{
		if (s_instance != null)
		{
			m_clickCatcher.AddEventListener(UIEventType.RELEASE, OnClickCatcherClicked);
		}
	}

	private void ShowQuests()
	{
		List<Achievement> questsToShow = GetQuestsToShow(m_showRequestData);
		if (questsToShow.Count < 1)
		{
			m_allCompletedCaption.gameObject.SetActive(value: true);
			return;
		}
		m_headlineBanner.gameObject.SetActive(value: true);
		if (m_showRequestData.IsSpecialQuestRequest())
		{
			m_headlineBanner.SetText(GameStrings.Get("GLUE_SPECIAL_QUEST_NOTIFICATION_HEADER"));
		}
		else if (m_showRequestData.m_fromLogin)
		{
			m_headlineBanner.SetText(GameStrings.Get("GLUE_QUEST_NOTIFICATION_HEADER"));
		}
		else
		{
			m_headlineBanner.SetText(GameStrings.Get("GLUE_QUEST_NOTIFICATION_HEADER_NEW_ONLY"));
		}
		bool flag = SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, activeIfDoesNotExist: false) && !string.IsNullOrEmpty(GameStrings.Get("GLUE_QUEST_NOTIFICATION_CAPTION_FRIEND_WEEK"));
		bool active = !flag;
		if (!AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY) || m_showRequestData.IsSpecialQuestRequest() || ReturningPlayerMgr.Get().IsInReturningPlayerMode)
		{
			flag = false;
			active = false;
		}
		m_friendWeekReminderContainer.SetActive(flag);
		m_questCaption.gameObject.SetActive(active);
		if (flag)
		{
			Vector3 localScale = m_friendWeekReminderGlow.transform.localScale;
			localScale.x = m_friendWeekReminderCaption.GetTextBounds().extents.x * 2f;
			m_friendWeekReminderGlow.transform.localScale = localScale;
		}
		bool flag2 = true;
		foreach (Achievement item in questsToShow)
		{
			if (!item.IsLegendary)
			{
				flag2 = false;
				break;
			}
		}
		GameObject[] normalFXs = m_normalFXs;
		for (int i = 0; i < normalFXs.Length; i++)
		{
			normalFXs[i].SetActive(!flag2);
		}
		normalFXs = m_legendaryFXs;
		for (int i = 0; i < normalFXs.Length; i++)
		{
			normalFXs[i].SetActive(flag2);
		}
		m_currentQuests = new List<QuestTile>();
		GameObject placementColliderPhoneNoIksBone = m_placementCollider.gameObject;
		PlatformDependentValue<float> platformDependentValue = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.4408684f,
			Phone = 0.4208684f
		};
		if (!InnKeepersSpecial.Get().IsShown)
		{
			platformDependentValue = new PlatformDependentValue<float>(PlatformCategory.Screen)
			{
				PC = 0.4408684f,
				Phone = 0.4408684f
			};
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				if (m_placementColliderPhoneNoIksBone != null)
				{
					placementColliderPhoneNoIksBone = m_placementColliderPhoneNoIksBone;
				}
				if (m_phoneNoIksCaptionBone != null)
				{
					m_friendWeekReminderContainer.transform.position = m_phoneNoIksCaptionBone.transform.position;
					m_questCaption.transform.position = m_phoneNoIksCaptionBone.transform.position;
				}
			}
		}
		float num = m_placementCollider.transform.position.x - m_placementCollider.GetComponent<Collider>().bounds.extents.x;
		float num2 = m_placementCollider.bounds.size.x / (float)questsToShow.Count;
		float num3 = num2 / 2f;
		for (int j = 0; j < questsToShow.Count; j++)
		{
			Achievement achievement = questsToShow[j];
			bool num4 = achievement.IsNewlyActive();
			if (num4)
			{
				DoInnkeeperLine(achievement);
			}
			GameObject gameObject;
			if (achievement.AutoDestroy && !string.IsNullOrEmpty(achievement.QuestTilePrefabName))
			{
				gameObject = GameUtils.LoadGameObjectWithComponent<QuestTile>(achievement.QuestTilePrefabName).gameObject;
				if (gameObject == null)
				{
					gameObject = Object.Instantiate(m_questTilePrefab.gameObject);
				}
			}
			else
			{
				gameObject = Object.Instantiate(m_questTilePrefab.gameObject);
			}
			SceneUtils.SetLayer(gameObject, GameLayer.UI);
			gameObject.transform.position = new Vector3(num + num3, placementColliderPhoneNoIksBone.transform.position.y, placementColliderPhoneNoIksBone.transform.position.z);
			gameObject.transform.parent = base.transform;
			gameObject.transform.localEulerAngles = new Vector3(90f, 180f, 0f);
			gameObject.transform.localScale = new Vector3(platformDependentValue, platformDependentValue, platformDependentValue);
			QuestTile component = gameObject.GetComponent<QuestTile>();
			QuestTile.FsmEvent fsmEventToPlay = (num4 ? QuestTile.FsmEvent.QuestGranted : QuestTile.FsmEvent.QuestShownInQuestAlert);
			component.SetupTile(achievement, fsmEventToPlay);
			m_currentQuests.Add(component);
			num3 += num2;
		}
		if (m_showRequestData.m_fromLogin)
		{
			m_loginQuestShownTime = Time.realtimeSinceStartup;
			m_clickCatcher.AddEventListener(UIEventType.RELEASE, SendTelemetry);
		}
	}

	private void CheckShowInnkeepersSpecial()
	{
		int @int = Options.Get().GetInt(Option.IKS_VIEW_ATTEMPTS, 0);
		@int++;
		Options.Get().SetInt(Option.IKS_VIEW_ATTEMPTS, @int);
		bool flag = @int > 3;
		int num = 0;
		bool @bool = Options.Get().GetBool(Option.FORCE_SHOW_IKS);
		if (m_showRequestData.m_fromLogin && !ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			if (flag || @bool)
			{
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					Vector3 localPosition = base.transform.localPosition;
					localPosition.y += 2f;
					base.transform.localPosition = localPosition;
				}
				Log.InnKeepersSpecial.Print("Showing IKS!");
				InnKeepersSpecial.Get().ShowAdAndIncrementViewCountWhenReady();
			}
			else
			{
				Log.InnKeepersSpecial.Print("Skipping IKS! views={0} lastShownViews={1}", @int, num);
			}
		}
		else
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! login={0}, ReturningPlayerMgr.Get().SuppressOldPopups={1}!", m_showRequestData.m_fromLogin, ReturningPlayerMgr.Get().SuppressOldPopups);
		}
	}

	private void DoInnkeeperLine(Achievement quest)
	{
		if (quest.ID != 11 && quest.ID == 568)
		{
			NotificationManager.Get().CreateCharacterQuote("DemonHunter_Illidan_Popup_Banner.prefab:c2b08a2b89af02e4bb9e80b08526df7a", GameStrings.Get("VO_ILLIDAN_RETURNING_PLAYER_QUEST1"), "VO_TB_Hero_Illidan2_Male_NightElf_RP_Intro02_01.prefab:85586365c070ded4bb713703951d6bd5");
		}
	}

	private static void FadeEffectsIn()
	{
		if (!s_fullScreenFXActive)
		{
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr != null)
			{
				s_fullScreenFXActive = true;
				fullScreenFXMgr.SetBlurBrightness(1f);
				fullScreenFXMgr.SetBlurDesaturation(0f);
				fullScreenFXMgr.Vignette();
				fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
			}
		}
	}

	private static void FadeEffectsOut()
	{
		if (s_fullScreenFXActive)
		{
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr != null)
			{
				s_fullScreenFXActive = false;
				fullScreenFXMgr.StopVignette();
				fullScreenFXMgr.StopBlur();
			}
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		Close();
	}

	private void Close()
	{
		CleanUpEventListeners();
		UnlockBnetButtons();
		s_instance = null;
		FadeEffectsOut();
		if (DeckPickerTrayDisplay.Get() != null && (bool)UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().SetHeroDetailsTrayToIgnoreFullScreenEffects(ignoreEffects: true);
		}
		if (m_currentQuests != null)
		{
			foreach (QuestTile currentQuest in m_currentQuests)
			{
				currentQuest.OnClose();
			}
		}
		if (base.gameObject != null)
		{
			iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "oncompletetarget", base.gameObject, "oncomplete", "DestroyWelcomeQuests"));
			SoundManager.Get().LoadAndPlay("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
			m_bannerFX.Play("BannerClose");
		}
		if (m_showRequestData != null)
		{
			if (!m_showRequestData.m_keepRichPresence)
			{
				PresenceMgr.Get().SetPrevStatus();
			}
			if (m_showRequestData.m_onCloseCallback != null)
			{
				m_showRequestData.m_onCloseCallback();
			}
		}
		InnKeepersSpecial.Close();
	}

	public static bool OnNavigateBack()
	{
		if (s_instance != null)
		{
			s_instance.Close();
		}
		return true;
	}

	private void OnClickCatcherClicked(UIEvent e)
	{
		Close();
	}

	private void DestroyWelcomeQuests()
	{
		Object.Destroy(base.gameObject);
	}

	private void OnPreLoadScene(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		if (nextMode == SceneMgr.Mode.GAMEPLAY)
		{
			Close();
		}
	}

	private void SendTelemetry(UIEvent e)
	{
		float questAckDuration = Time.realtimeSinceStartup - m_loginQuestShownTime;
		TelemetryManager.Client().SendWelcomeQuestsAcknowledged(questAckDuration);
		m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, SendTelemetry);
	}

	private void CleanUpEventListeners()
	{
		Navigation.RemoveHandler(OnNavigateBack);
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreLoadEvent(OnPreLoadScene);
		}
		m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, OnClickCatcherClicked);
		m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, SendTelemetry);
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
	}

	private void LockBnetButtons()
	{
		if (!(BaseUI.Get() == null) && !m_bnetButtonsLocked)
		{
			BaseUI.Get().m_BnetBar.RequestDisableButtons();
			m_bnetButtonsLocked = true;
		}
	}

	private void UnlockBnetButtons()
	{
		if (!(BaseUI.Get() == null) && m_bnetButtonsLocked)
		{
			BaseUI.Get().m_BnetBar.CancelRequestToDisableButtons();
			m_bnetButtonsLocked = false;
		}
	}
}
