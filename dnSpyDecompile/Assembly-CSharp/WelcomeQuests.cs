using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x0200063A RID: 1594
public class WelcomeQuests : MonoBehaviour
{
	// Token: 0x060059C0 RID: 22976 RVA: 0x001D47A0 File Offset: 0x001D29A0
	public static bool Show(UserAttentionBlocker blocker, bool fromLogin, WelcomeQuests.DelOnWelcomeQuestsClosed onCloseCallback = null, bool keepRichPresence = false)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "WelcomeQuests.Show:" + fromLogin.ToString()))
		{
			if (onCloseCallback != null)
			{
				onCloseCallback();
			}
			return false;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.WELCOMEQUESTS
		});
		WelcomeQuests.ShowRequestData showRequestData = new WelcomeQuests.ShowRequestData
		{
			m_fromLogin = fromLogin,
			m_onCloseCallback = onCloseCallback,
			m_keepRichPresence = keepRichPresence,
			m_achievement = null
		};
		if (WelcomeQuests.s_instance != null)
		{
			Debug.Log("WelcomeQuests.Show(): requested to show welcome quests while it was already active!");
			WelcomeQuests.s_instance.ReinitAndShow(showRequestData);
			return true;
		}
		AssetLoader.Get().InstantiatePrefab("WelcomeQuests.prefab:c1b288441ca1a05419dcb2bd498b8830", new PrefabCallback<GameObject>(WelcomeQuests.OnWelcomeQuestsLoaded), showRequestData, AssetLoadingOptions.None);
		return true;
	}

	// Token: 0x060059C1 RID: 22977 RVA: 0x001D4858 File Offset: 0x001D2A58
	public static void ShowSpecialQuest(UserAttentionBlocker blocker, global::Achievement achievement, WelcomeQuests.DelOnWelcomeQuestsClosed onCloseCallback = null, bool keepRichPresence = false)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "WelcomeQuests.ShowSpecialQuest:" + ((achievement == null) ? "null" : achievement.ID.ToString())))
		{
			if (onCloseCallback != null)
			{
				onCloseCallback();
			}
			return;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.WELCOMEQUESTS
		});
		WelcomeQuests.ShowRequestData showRequestData = new WelcomeQuests.ShowRequestData
		{
			m_fromLogin = false,
			m_onCloseCallback = onCloseCallback,
			m_keepRichPresence = keepRichPresence,
			m_achievement = achievement
		};
		if (WelcomeQuests.s_instance != null)
		{
			Debug.Log("WelcomeQuests.Show(): requested to show welcome quests while it was already active!");
			WelcomeQuests.s_instance.ReinitAndShow(showRequestData);
			return;
		}
		AssetLoader.Get().InstantiatePrefab("WelcomeQuests.prefab:c1b288441ca1a05419dcb2bd498b8830", new PrefabCallback<GameObject>(WelcomeQuests.OnWelcomeQuestsLoaded), showRequestData, AssetLoadingOptions.None);
	}

	// Token: 0x060059C2 RID: 22978 RVA: 0x001D491B File Offset: 0x001D2B1B
	public static void Hide()
	{
		if (WelcomeQuests.s_instance == null)
		{
			return;
		}
		WelcomeQuests.s_instance.Close();
	}

	// Token: 0x060059C3 RID: 22979 RVA: 0x001D4935 File Offset: 0x001D2B35
	public static WelcomeQuests Get()
	{
		return WelcomeQuests.s_instance;
	}

	// Token: 0x060059C4 RID: 22980 RVA: 0x001D493C File Offset: 0x001D2B3C
	public QuestTile GetFirstQuestTile()
	{
		return this.m_currentQuests[0];
	}

	// Token: 0x060059C5 RID: 22981 RVA: 0x001D494C File Offset: 0x001D2B4C
	public int CompleteAndReplaceAutoDestroyQuestTile(int achieveId)
	{
		foreach (QuestTile questTile in this.m_currentQuests)
		{
			if (questTile.GetQuestID() == achieveId)
			{
				questTile.CompleteAndAutoDestroyQuest();
				return AchieveManager.Get().GetAchievement(achieveId).LinkToId;
			}
		}
		return 0;
	}

	// Token: 0x060059C6 RID: 22982 RVA: 0x001D49C0 File Offset: 0x001D2BC0
	public void ActivateClickCatcher()
	{
		this.m_clickCatcher.gameObject.SetActive(true);
		this.RegisterClickCatcher();
	}

	// Token: 0x060059C7 RID: 22983 RVA: 0x001D49DC File Offset: 0x001D2BDC
	private void Awake()
	{
		this.m_originalScale = base.transform.localScale;
		this.m_headlineBanner.gameObject.SetActive(false);
		this.m_friendWeekReminderContainer.SetActive(false);
		this.m_questCaption.gameObject.SetActive(false);
		this.m_clickCatcher.gameObject.SetActive(false);
		this.m_allCompletedCaption.gameObject.SetActive(false);
		SoundManager.Get().Load("new_quest_pop_up.prefab:5ef0d42842220a648bdebd874ba716e4");
		SoundManager.Get().Load("existing_quest_pop_up.prefab:9b4dcb4e8233104409605a8bd5f3095d");
		SoundManager.Get().Load("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
		SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadScene));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x060059C8 RID: 22984 RVA: 0x001D4AB8 File Offset: 0x001D2CB8
	private void OnDestroy()
	{
		WelcomeQuests.FadeEffectsOut();
		if (WelcomeQuests.s_instance != null)
		{
			this.CleanUpEventListeners();
			WelcomeQuests.s_instance = null;
			this.UnlockBnetButtons();
			if (DeckPickerTrayDisplay.Get() != null && UniversalInputManager.UsePhoneUI)
			{
				DeckPickerTrayDisplay.Get().SetHeroDetailsTrayToIgnoreFullScreenEffects(true);
			}
			InnKeepersSpecial.Close();
		}
	}

	// Token: 0x060059C9 RID: 22985 RVA: 0x001D4B14 File Offset: 0x001D2D14
	private static void OnWelcomeQuestsLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (SceneMgr.Get() != null && SceneMgr.Get().IsInGame())
		{
			if (WelcomeQuests.s_instance != null)
			{
				WelcomeQuests.s_instance.Close();
			}
			return;
		}
		if (go == null)
		{
			Debug.LogError(string.Format("WelcomeQuests.OnWelcomeQuestsLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		WelcomeQuests.s_instance = go.GetComponent<WelcomeQuests>();
		if (WelcomeQuests.s_instance == null)
		{
			Debug.LogError(string.Format("WelcomeQuests.OnWelcomeQuestsLoaded() - ERROR object \"{0}\" has no WelcomeQuests component", assetRef));
			return;
		}
		WelcomeQuests.ShowRequestData showRequestData = callbackData as WelcomeQuests.ShowRequestData;
		WelcomeQuests.s_instance.InitAndShow(showRequestData);
	}

	// Token: 0x060059CA RID: 22986 RVA: 0x001D4BA4 File Offset: 0x001D2DA4
	private List<global::Achievement> GetQuestsToShow(WelcomeQuests.ShowRequestData showRequestData)
	{
		List<global::Achievement> list;
		if (showRequestData.m_achievement == null)
		{
			list = AchieveManager.Get().GetActiveQuests(false);
		}
		else
		{
			list = new List<global::Achievement>();
			list.Add(showRequestData.m_achievement);
		}
		return list;
	}

	// Token: 0x060059CB RID: 22987 RVA: 0x001D4BDC File Offset: 0x001D2DDC
	private void InitAndShow(WelcomeQuests.ShowRequestData showRequestData)
	{
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		this.m_showRequestData = showRequestData;
		this.LockBnetButtons();
		List<global::Achievement> questsToShow = this.GetQuestsToShow(this.m_showRequestData);
		if (questsToShow.Count < 1 && !InnKeepersSpecial.Get().LoadedSuccessfully())
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! loadedSucsesfully={0}", new object[]
			{
				InnKeepersSpecial.Get().LoadedSuccessfully()
			});
			this.Close();
			return;
		}
		List<global::Achievement> list = new List<global::Achievement>();
		foreach (global::Achievement achievement in questsToShow)
		{
			if (achievement.IsNewlyActive())
			{
				list.Add(achievement);
			}
		}
		this.m_clickCatcher.gameObject.SetActive(true);
		if (this.m_showRequestData.IsSpecialQuestRequest())
		{
			base.Invoke("RegisterClickCatcher", 2.5f);
		}
		else if (!AchieveManager.Get().HasActiveAutoDestroyQuests() && !AchieveManager.Get().HasActiveUnseenWelcomeQuestDialog())
		{
			this.RegisterClickCatcher();
		}
		this.CheckShowInnkeepersSpecial();
		this.ShowQuests();
		WelcomeQuests.FadeEffectsIn();
		if (DeckPickerTrayDisplay.Get() != null && UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().SetHeroDetailsTrayToIgnoreFullScreenEffects(false);
		}
		base.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		iTween.ScaleTo(base.gameObject, this.m_originalScale, 0.5f);
		Navigation.PushUnique(new Navigation.NavigateBackHandler(WelcomeQuests.OnNavigateBack));
		NarrativeManager.Get().OnWelcomeQuestsShown(questsToShow, list);
	}

	// Token: 0x060059CC RID: 22988 RVA: 0x001D4D80 File Offset: 0x001D2F80
	private void ReinitAndShow(WelcomeQuests.ShowRequestData showRequestData)
	{
		WelcomeQuests.FadeEffectsOut();
		this.UnlockBnetButtons();
		this.InitAndShow(showRequestData);
	}

	// Token: 0x060059CD RID: 22989 RVA: 0x001D4D94 File Offset: 0x001D2F94
	private void RegisterClickCatcher()
	{
		if (WelcomeQuests.s_instance != null)
		{
			this.m_clickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClickCatcherClicked));
		}
	}

	// Token: 0x060059CE RID: 22990 RVA: 0x001D4DBC File Offset: 0x001D2FBC
	private void ShowQuests()
	{
		List<global::Achievement> questsToShow = this.GetQuestsToShow(this.m_showRequestData);
		if (questsToShow.Count < 1)
		{
			this.m_allCompletedCaption.gameObject.SetActive(true);
			return;
		}
		this.m_headlineBanner.gameObject.SetActive(true);
		if (this.m_showRequestData.IsSpecialQuestRequest())
		{
			this.m_headlineBanner.SetText(GameStrings.Get("GLUE_SPECIAL_QUEST_NOTIFICATION_HEADER"));
		}
		else if (this.m_showRequestData.m_fromLogin)
		{
			this.m_headlineBanner.SetText(GameStrings.Get("GLUE_QUEST_NOTIFICATION_HEADER"));
		}
		else
		{
			this.m_headlineBanner.SetText(GameStrings.Get("GLUE_QUEST_NOTIFICATION_HEADER_NEW_ONLY"));
		}
		bool flag = SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, false) && !string.IsNullOrEmpty(GameStrings.Get("GLUE_QUEST_NOTIFICATION_CAPTION_FRIEND_WEEK"));
		bool active = !flag;
		if (!AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY) || this.m_showRequestData.IsSpecialQuestRequest() || ReturningPlayerMgr.Get().IsInReturningPlayerMode)
		{
			flag = false;
			active = false;
		}
		this.m_friendWeekReminderContainer.SetActive(flag);
		this.m_questCaption.gameObject.SetActive(active);
		if (flag)
		{
			Vector3 localScale = this.m_friendWeekReminderGlow.transform.localScale;
			localScale.x = this.m_friendWeekReminderCaption.GetTextBounds().extents.x * 2f;
			this.m_friendWeekReminderGlow.transform.localScale = localScale;
		}
		bool flag2 = true;
		using (List<global::Achievement>.Enumerator enumerator = questsToShow.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsLegendary)
				{
					flag2 = false;
					break;
				}
			}
		}
		GameObject[] array = this.m_normalFXs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(!flag2);
		}
		array = this.m_legendaryFXs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(flag2);
		}
		this.m_currentQuests = new List<QuestTile>();
		GameObject gameObject = this.m_placementCollider.gameObject;
		PlatformDependentValue<float> val = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.4408684f,
			Phone = 0.4208684f
		};
		if (!InnKeepersSpecial.Get().IsShown)
		{
			val = new PlatformDependentValue<float>(PlatformCategory.Screen)
			{
				PC = 0.4408684f,
				Phone = 0.4408684f
			};
			if (UniversalInputManager.UsePhoneUI)
			{
				if (this.m_placementColliderPhoneNoIksBone != null)
				{
					gameObject = this.m_placementColliderPhoneNoIksBone;
				}
				if (this.m_phoneNoIksCaptionBone != null)
				{
					this.m_friendWeekReminderContainer.transform.position = this.m_phoneNoIksCaptionBone.transform.position;
					this.m_questCaption.transform.position = this.m_phoneNoIksCaptionBone.transform.position;
				}
			}
		}
		float num = this.m_placementCollider.transform.position.x - this.m_placementCollider.GetComponent<Collider>().bounds.extents.x;
		float num2 = this.m_placementCollider.bounds.size.x / (float)questsToShow.Count;
		float num3 = num2 / 2f;
		for (int j = 0; j < questsToShow.Count; j++)
		{
			global::Achievement achievement = questsToShow[j];
			bool flag3 = achievement.IsNewlyActive();
			if (flag3)
			{
				this.DoInnkeeperLine(achievement);
			}
			GameObject gameObject2;
			if (achievement.AutoDestroy && !string.IsNullOrEmpty(achievement.QuestTilePrefabName))
			{
				gameObject2 = GameUtils.LoadGameObjectWithComponent<QuestTile>(achievement.QuestTilePrefabName).gameObject;
				if (gameObject2 == null)
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_questTilePrefab.gameObject);
				}
			}
			else
			{
				gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_questTilePrefab.gameObject);
			}
			SceneUtils.SetLayer(gameObject2, GameLayer.UI);
			gameObject2.transform.position = new Vector3(num + num3, gameObject.transform.position.y, gameObject.transform.position.z);
			gameObject2.transform.parent = base.transform;
			gameObject2.transform.localEulerAngles = new Vector3(90f, 180f, 0f);
			gameObject2.transform.localScale = new Vector3(val, val, val);
			QuestTile component = gameObject2.GetComponent<QuestTile>();
			QuestTile.FsmEvent fsmEventToPlay = flag3 ? QuestTile.FsmEvent.QuestGranted : QuestTile.FsmEvent.QuestShownInQuestAlert;
			component.SetupTile(achievement, fsmEventToPlay);
			this.m_currentQuests.Add(component);
			num3 += num2;
		}
		if (this.m_showRequestData.m_fromLogin)
		{
			this.m_loginQuestShownTime = Time.realtimeSinceStartup;
			this.m_clickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.SendTelemetry));
		}
	}

	// Token: 0x060059CF RID: 22991 RVA: 0x001D5278 File Offset: 0x001D3478
	private void CheckShowInnkeepersSpecial()
	{
		int num = Options.Get().GetInt(Option.IKS_VIEW_ATTEMPTS, 0);
		num++;
		Options.Get().SetInt(Option.IKS_VIEW_ATTEMPTS, num);
		bool flag = num > 3;
		int num2 = 0;
		bool @bool = Options.Get().GetBool(Option.FORCE_SHOW_IKS);
		if (!this.m_showRequestData.m_fromLogin || ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! login={0}, ReturningPlayerMgr.Get().SuppressOldPopups={1}!", new object[]
			{
				this.m_showRequestData.m_fromLogin,
				ReturningPlayerMgr.Get().SuppressOldPopups
			});
			return;
		}
		if (flag || @bool)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				Vector3 localPosition = base.transform.localPosition;
				localPosition.y += 2f;
				base.transform.localPosition = localPosition;
			}
			Log.InnKeepersSpecial.Print("Showing IKS!", Array.Empty<object>());
			InnKeepersSpecial.Get().ShowAdAndIncrementViewCountWhenReady();
			return;
		}
		Log.InnKeepersSpecial.Print("Skipping IKS! views={0} lastShownViews={1}", new object[]
		{
			num,
			num2
		});
	}

	// Token: 0x060059D0 RID: 22992 RVA: 0x001D5393 File Offset: 0x001D3593
	private void DoInnkeeperLine(global::Achievement quest)
	{
		if (quest.ID != 11 && quest.ID == 568)
		{
			NotificationManager.Get().CreateCharacterQuote("DemonHunter_Illidan_Popup_Banner.prefab:c2b08a2b89af02e4bb9e80b08526df7a", GameStrings.Get("VO_ILLIDAN_RETURNING_PLAYER_QUEST1"), "VO_TB_Hero_Illidan2_Male_NightElf_RP_Intro02_01.prefab:85586365c070ded4bb713703951d6bd5", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
	}

	// Token: 0x060059D1 RID: 22993 RVA: 0x001D53D4 File Offset: 0x001D35D4
	private static void FadeEffectsIn()
	{
		if (WelcomeQuests.s_fullScreenFXActive)
		{
			return;
		}
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			return;
		}
		WelcomeQuests.s_fullScreenFXActive = true;
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x060059D2 RID: 22994 RVA: 0x001D5428 File Offset: 0x001D3628
	private static void FadeEffectsOut()
	{
		if (!WelcomeQuests.s_fullScreenFXActive)
		{
			return;
		}
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			return;
		}
		WelcomeQuests.s_fullScreenFXActive = false;
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}

	// Token: 0x060059D3 RID: 22995 RVA: 0x001D5459 File Offset: 0x001D3659
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.Close();
	}

	// Token: 0x060059D4 RID: 22996 RVA: 0x001D5464 File Offset: 0x001D3664
	private void Close()
	{
		this.CleanUpEventListeners();
		this.UnlockBnetButtons();
		WelcomeQuests.s_instance = null;
		WelcomeQuests.FadeEffectsOut();
		if (DeckPickerTrayDisplay.Get() != null && UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().SetHeroDetailsTrayToIgnoreFullScreenEffects(true);
		}
		if (this.m_currentQuests != null)
		{
			foreach (QuestTile questTile in this.m_currentQuests)
			{
				questTile.OnClose();
			}
		}
		if (base.gameObject != null)
		{
			iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
			{
				"scale",
				Vector3.zero,
				"time",
				0.5f,
				"oncompletetarget",
				base.gameObject,
				"oncomplete",
				"DestroyWelcomeQuests"
			}));
			SoundManager.Get().LoadAndPlay("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
			this.m_bannerFX.Play("BannerClose");
		}
		if (this.m_showRequestData != null)
		{
			if (!this.m_showRequestData.m_keepRichPresence)
			{
				PresenceMgr.Get().SetPrevStatus();
			}
			if (this.m_showRequestData.m_onCloseCallback != null)
			{
				this.m_showRequestData.m_onCloseCallback();
			}
		}
		InnKeepersSpecial.Close();
	}

	// Token: 0x060059D5 RID: 22997 RVA: 0x001D55D0 File Offset: 0x001D37D0
	public static bool OnNavigateBack()
	{
		if (WelcomeQuests.s_instance != null)
		{
			WelcomeQuests.s_instance.Close();
		}
		return true;
	}

	// Token: 0x060059D6 RID: 22998 RVA: 0x001D5459 File Offset: 0x001D3659
	private void OnClickCatcherClicked(UIEvent e)
	{
		this.Close();
	}

	// Token: 0x060059D7 RID: 22999 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void DestroyWelcomeQuests()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060059D8 RID: 23000 RVA: 0x001D55EA File Offset: 0x001D37EA
	private void OnPreLoadScene(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		if (nextMode == SceneMgr.Mode.GAMEPLAY)
		{
			this.Close();
		}
	}

	// Token: 0x060059D9 RID: 23001 RVA: 0x001D55F8 File Offset: 0x001D37F8
	private void SendTelemetry(UIEvent e)
	{
		float questAckDuration = Time.realtimeSinceStartup - this.m_loginQuestShownTime;
		TelemetryManager.Client().SendWelcomeQuestsAcknowledged(questAckDuration);
		this.m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.SendTelemetry));
	}

	// Token: 0x060059DA RID: 23002 RVA: 0x001D5638 File Offset: 0x001D3838
	private void CleanUpEventListeners()
	{
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(WelcomeQuests.OnNavigateBack));
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadScene));
		}
		this.m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClickCatcherClicked));
		this.m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.SendTelemetry));
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x060059DB RID: 23003 RVA: 0x001D56BE File Offset: 0x001D38BE
	private void LockBnetButtons()
	{
		if (BaseUI.Get() == null || this.m_bnetButtonsLocked)
		{
			return;
		}
		BaseUI.Get().m_BnetBar.RequestDisableButtons();
		this.m_bnetButtonsLocked = true;
	}

	// Token: 0x060059DC RID: 23004 RVA: 0x001D56EC File Offset: 0x001D38EC
	private void UnlockBnetButtons()
	{
		if (BaseUI.Get() == null || !this.m_bnetButtonsLocked)
		{
			return;
		}
		BaseUI.Get().m_BnetBar.CancelRequestToDisableButtons();
		this.m_bnetButtonsLocked = false;
	}

	// Token: 0x04004CD4 RID: 19668
	public QuestTile m_questTilePrefab;

	// Token: 0x04004CD5 RID: 19669
	public Collider m_placementCollider;

	// Token: 0x04004CD6 RID: 19670
	public GameObject m_placementColliderPhoneNoIksBone;

	// Token: 0x04004CD7 RID: 19671
	public Banner m_headlineBanner;

	// Token: 0x04004CD8 RID: 19672
	public PegUIElement m_clickCatcher;

	// Token: 0x04004CD9 RID: 19673
	public UberText m_questCaption;

	// Token: 0x04004CDA RID: 19674
	public UberText m_allCompletedCaption;

	// Token: 0x04004CDB RID: 19675
	public GameObject m_friendWeekReminderContainer;

	// Token: 0x04004CDC RID: 19676
	public UberText m_friendWeekReminderCaption;

	// Token: 0x04004CDD RID: 19677
	public GameObject m_friendWeekReminderGlow;

	// Token: 0x04004CDE RID: 19678
	public Transform m_phoneNoIksCaptionBone;

	// Token: 0x04004CDF RID: 19679
	public Animation m_bannerFX;

	// Token: 0x04004CE0 RID: 19680
	public GameObject m_Root;

	// Token: 0x04004CE1 RID: 19681
	public GameObject[] m_normalFXs;

	// Token: 0x04004CE2 RID: 19682
	public GameObject[] m_legendaryFXs;

	// Token: 0x04004CE3 RID: 19683
	private static WelcomeQuests s_instance;

	// Token: 0x04004CE4 RID: 19684
	private static bool s_fullScreenFXActive;

	// Token: 0x04004CE5 RID: 19685
	private WelcomeQuests.ShowRequestData m_showRequestData;

	// Token: 0x04004CE6 RID: 19686
	private List<QuestTile> m_currentQuests;

	// Token: 0x04004CE7 RID: 19687
	private Vector3 m_originalScale;

	// Token: 0x04004CE8 RID: 19688
	private float m_loginQuestShownTime;

	// Token: 0x04004CE9 RID: 19689
	private bool m_bnetButtonsLocked;

	// Token: 0x04004CEA RID: 19690
	private const float SPECIAL_QUEST_DISMISS_DELAY = 2.5f;

	// Token: 0x02002141 RID: 8513
	// (Invoke) Token: 0x060122C4 RID: 74436
	public delegate void DelOnWelcomeQuestsClosed();

	// Token: 0x02002142 RID: 8514
	private class ShowRequestData
	{
		// Token: 0x060122C7 RID: 74439 RVA: 0x004FFCBC File Offset: 0x004FDEBC
		public bool IsSpecialQuestRequest()
		{
			return this.m_achievement != null;
		}

		// Token: 0x0400DFB6 RID: 57270
		public bool m_fromLogin;

		// Token: 0x0400DFB7 RID: 57271
		public WelcomeQuests.DelOnWelcomeQuestsClosed m_onCloseCallback;

		// Token: 0x0400DFB8 RID: 57272
		public bool m_keepRichPresence;

		// Token: 0x0400DFB9 RID: 57273
		public global::Achievement m_achievement;
	}
}
