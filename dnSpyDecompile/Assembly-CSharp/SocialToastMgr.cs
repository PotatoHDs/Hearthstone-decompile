using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using bgs;
using bgs.types;
using Hearthstone.Core;
using Hearthstone.Progression;
using PegasusClient;
using PegasusShared;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class SocialToastMgr : MonoBehaviour
{
	// Token: 0x06000A96 RID: 2710 RVA: 0x0003E6E4 File Offset: 0x0003C8E4
	private void Awake()
	{
		SocialToastMgr.s_instance = this;
		this.CreateSocialToastObjects();
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += this.OnPresenceChanged;
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		Network.Get().SetShutdownHandler(new Network.ShutdownHandler(this.ShutdownHandler));
		SoundManager.Get().Load("UI_BnetToast.prefab:b869739323d1fc241984f9f480fff8ef");
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0003E76C File Offset: 0x0003C96C
	private void CreateSocialToastObjects()
	{
		if (BnetBar.Get() == null || BnetBar.Get().m_socialToastBone == null)
		{
			return;
		}
		if (this.m_defaultToast == null || this.m_firesideGatheringToast == null)
		{
			this.CreateSocialToastObject(this.m_defaultSocialToastPrefab, ref this.m_defaultToast, null);
			this.CreateSocialToastObject(this.m_firesideGatheringSocialToastPrefab, ref this.m_firesideGatheringToast, null);
			this.m_currentToast = this.m_defaultToast;
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0003E7E8 File Offset: 0x0003C9E8
	private void OnDestroy()
	{
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= this.OnPresenceChanged;
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		BnetFriendMgr.Get().RemoveChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		this.m_lastKnownMedals.Clear();
		SocialToastMgr.s_instance = null;
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0003E84A File Offset: 0x0003CA4A
	public static SocialToastMgr Get()
	{
		return SocialToastMgr.s_instance;
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0003E854 File Offset: 0x0003CA54
	public void Reset()
	{
		if (this.m_currentToast == null)
		{
			return;
		}
		iTween.Stop(this.m_currentToast.gameObject, true);
		iTween.Stop(base.gameObject, true);
		RenderUtils.SetAlpha(this.m_currentToast.gameObject, 0f);
		this.m_toastQueue.Clear();
		this.DeactivateToast();
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0003E8B3 File Offset: 0x0003CAB3
	public void AddToast(UserAttentionBlocker blocker, string textArg)
	{
		this.AddToast(blocker, textArg, SocialToastMgr.TOAST_TYPE.DEFAULT, 2f, true);
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0003E8C4 File Offset: 0x0003CAC4
	public void AddToast(UserAttentionBlocker blocker, string textArg, SocialToastMgr.TOAST_TYPE toastType)
	{
		this.AddToast(blocker, textArg, toastType, 2f, true);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0003E8D5 File Offset: 0x0003CAD5
	public void AddToast(UserAttentionBlocker blocker, string textArg, SocialToastMgr.TOAST_TYPE toastType, bool playSound)
	{
		this.AddToast(blocker, textArg, toastType, 2f, playSound);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0003E8E7 File Offset: 0x0003CAE7
	public void AddToast(UserAttentionBlocker blocker, string textArg, SocialToastMgr.TOAST_TYPE toastType, float displayTime)
	{
		this.AddToast(blocker, textArg, toastType, displayTime, true);
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
	public void AddToast(UserAttentionBlocker blocker, string textArg, SocialToastMgr.TOAST_TYPE toastType, float displayTime, bool playSound)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "SocialToastMgr.AddToast:" + toastType))
		{
			return;
		}
		SocialToastDesign design = SocialToastDesign.Default;
		string message;
		switch (toastType)
		{
		case SocialToastMgr.TOAST_TYPE.DEFAULT:
			message = textArg;
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.FRIEND_ONLINE:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_ONLINE", new object[]
			{
				"5ecaf0ff",
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.FRIEND_OFFLINE:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_OFFLINE", new object[]
			{
				"999999ff",
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.FRIEND_INVITE:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_REQUEST", new object[]
			{
				"5ecaf0ff",
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.HEALTHY_GAMING:
			message = GameStrings.Format("GLOBAL_HEALTHY_GAMING_TOAST", new object[]
			{
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.HEALTHY_GAMING_OVER_THRESHOLD:
			message = GameStrings.Format("GLOBAL_HEALTHY_GAMING_TOAST_OVER_THRESHOLD", new object[]
			{
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_SENT:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_INVITE_SENT", new object[]
			{
				"5ecaf0ff",
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_RECEIVED:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_INVITE_RECEIVED", new object[]
			{
				"5ecaf0ff",
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.SPECTATOR_ADDED:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_ADDED", new object[]
			{
				"5ecaf0ff",
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.SPECTATOR_REMOVED:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_REMOVED", new object[]
			{
				"5ecaf0ff",
				textArg
			});
			goto IL_19A;
		case SocialToastMgr.TOAST_TYPE.FIRESIDE_GATHERING_IS_HERE_REMINDER:
			design = SocialToastDesign.FiresideGathering;
			message = string.Format("<color=#{0}>{1}</color>", "ffd200", GameStrings.Get("GLOBAL_FIRESIDE_GATHERING"));
			goto IL_19A;
		}
		message = "";
		IL_19A:
		this.m_currentToast = this.GetSocialToastFromDesign(design);
		if (this.m_currentToast == null)
		{
			global::Log.All.PrintWarning("Toast design is not created yet", Array.Empty<object>());
			return;
		}
		if (this.m_toastQueue.Count <= 5)
		{
			SocialToastMgr.ToastArgs item = new SocialToastMgr.ToastArgs(message, displayTime, playSound);
			this.m_toastQueue.Enqueue(item);
			if (!this.m_toastIsShown && !AchievementManager.Get().IsShowingToast())
			{
				this.FadeInToast();
			}
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0003EB10 File Offset: 0x0003CD10
	public SocialToast CreateDefaultSocialToast(Transform parent)
	{
		SocialToast result = null;
		this.CreateSocialToastObject(this.m_defaultSocialToastPrefab, ref result, parent);
		return result;
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0003EB30 File Offset: 0x0003CD30
	private void CreateSocialToastObject(SocialToast prefab, ref SocialToast newToastReference, Transform parent = null)
	{
		newToastReference = UnityEngine.Object.Instantiate<SocialToast>(prefab);
		RenderUtils.SetAlpha(newToastReference.gameObject, 0f);
		newToastReference.gameObject.SetActive(false);
		newToastReference.transform.parent = ((parent != null) ? parent : BnetBar.Get().m_socialToastBone.transform);
		newToastReference.transform.localRotation = Quaternion.Euler(new Vector3(90f, 180f, 0f));
		newToastReference.transform.localScale = this.TOAST_SCALE;
		newToastReference.transform.position = BnetBar.Get().m_socialToastBone.transform.position;
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0003EBE8 File Offset: 0x0003CDE8
	private void AssignParentAndPosition(ref SocialToast newToastReference, Transform parent = null)
	{
		if (BnetBar.Get() != null && BnetBar.Get().m_socialToastBone != null)
		{
			newToastReference.transform.position = BnetBar.Get().m_socialToastBone.transform.position;
			if (parent == null)
			{
				newToastReference.transform.parent = BnetBar.Get().m_socialToastBone.transform;
			}
		}
		if (parent != null)
		{
			newToastReference.transform.parent = parent;
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0003EC6E File Offset: 0x0003CE6E
	private SocialToast GetSocialToastFromDesign(SocialToastDesign design)
	{
		this.CreateSocialToastObjects();
		if (design == SocialToastDesign.FiresideGathering)
		{
			return this.m_firesideGatheringToast;
		}
		return this.m_defaultToast;
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0003EC88 File Offset: 0x0003CE88
	private void FadeInToast()
	{
		this.m_toastIsShown = true;
		SocialToastMgr.ToastArgs toastArgs = this.m_toastQueue.Dequeue();
		this.m_currentToast.gameObject.SetActive(true);
		this.m_currentToast.SetText(toastArgs.m_message);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"FadeOutToast",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			toastArgs.m_displayTime,
			"name",
			"fade"
		});
		iTween.StopByName(base.gameObject, "fade");
		iTween.FadeTo(this.m_currentToast.gameObject, args);
		RenderUtils.SetAlpha(this.m_currentToast.gameObject, 1f);
		if (toastArgs.m_playSound)
		{
			this.PlayToastSound();
		}
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0003EDA2 File Offset: 0x0003CFA2
	public void PlayToastSound()
	{
		SoundManager.Get().LoadAndPlay("UI_BnetToast.prefab:b869739323d1fc241984f9f480fff8ef");
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0003EDB8 File Offset: 0x0003CFB8
	private void FadeOutToast(float displayTime)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			displayTime,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"DeactivateToast",
			"oncompletetarget",
			base.gameObject,
			"name",
			"fade"
		});
		iTween.FadeTo(this.m_currentToast.gameObject, args);
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0003EE65 File Offset: 0x0003D065
	private void DeactivateToast()
	{
		this.m_currentToast.gameObject.SetActive(false);
		this.m_toastIsShown = false;
		this.CheckToastQueue();
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0003EE88 File Offset: 0x0003D088
	public void CheckToastQueue()
	{
		AchievementManager achievementManager = AchievementManager.Get();
		if (achievementManager != null)
		{
			achievementManager.CheckToastQueue();
		}
		if (this.m_toastQueue.Count != 0 && (achievementManager == null || !achievementManager.IsShowingToast()))
		{
			this.FadeInToast();
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0003EEC3 File Offset: 0x0003D0C3
	public bool IsShowingToast()
	{
		return this.m_toastIsShown;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0003EECC File Offset: 0x0003D0CC
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (!DemoMgr.Get().IsSocialEnabled())
		{
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		foreach (BnetPlayerChange bnetPlayerChange in changelist.GetChanges())
		{
			if (bnetPlayerChange.GetPlayer() != null && bnetPlayerChange.GetNewPlayer() != null && bnetPlayerChange != null && bnetPlayerChange.GetPlayer().IsDisplayable() && bnetPlayerChange.GetPlayer() != myPlayer && BnetFriendMgr.Get().IsFriend(bnetPlayerChange.GetPlayer()))
			{
				BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
				BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
				this.CheckForOnlineStatusChanged(oldPlayer, newPlayer);
				if (oldPlayer != null)
				{
					BnetGameAccount hearthstoneGameAccount = newPlayer.GetHearthstoneGameAccount();
					BnetGameAccount hearthstoneGameAccount2 = oldPlayer.GetHearthstoneGameAccount();
					if (!(hearthstoneGameAccount2 == null) && !(hearthstoneGameAccount == null))
					{
						this.CheckForCardOpened(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForDruidLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForHunterLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForMageLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForPaladinLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForPriestLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForRogueLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForShamanLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForWarlockLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForWarriorLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
						this.CheckForMissionComplete(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					}
				}
			}
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0003F05C File Offset: 0x0003D25C
	private void OnPresenceChanged(PresenceUpdate[] updates)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		foreach (PresenceUpdate presenceUpdate in updates)
		{
			if (!(presenceUpdate.programId != BnetProgramId.HEARTHSTONE))
			{
				BnetPlayer player = BnetUtils.GetPlayer(BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId));
				if (player != null && player != myPlayer && player.IsDisplayable() && BnetFriendMgr.Get().IsFriend(player))
				{
					uint fieldId = presenceUpdate.fieldId;
					if (fieldId != 17U)
					{
						if (fieldId != 18U)
						{
							if (fieldId == 22U)
							{
								this.CheckSessionRecordChanged(player);
							}
						}
						else
						{
							this.CheckForNewRank(player);
						}
					}
					else
					{
						this.CheckSessionGameStarted(player);
					}
				}
			}
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0003F110 File Offset: 0x0003D310
	private void CheckForOnlineStatusChanged(BnetPlayer oldPlayer, BnetPlayer newPlayer)
	{
		if (oldPlayer != null && oldPlayer.IsOnline() == newPlayer.IsOnline())
		{
			return;
		}
		long bestLastOnlineMicrosec = newPlayer.GetBestLastOnlineMicrosec();
		long bestLastOnlineMicrosec2 = BnetPresenceMgr.Get().GetMyPlayer().GetBestLastOnlineMicrosec();
		if (bestLastOnlineMicrosec == 0L || bestLastOnlineMicrosec2 == 0L || bestLastOnlineMicrosec2 > bestLastOnlineMicrosec)
		{
			return;
		}
		SocialToastMgr.LastOnlineTracker lastOnlineTracker = null;
		float fixedTime = Time.fixedTime;
		int hashCode = newPlayer.GetAccountId().GetHashCode();
		if (!this.m_lastOnlineTracker.TryGetValue(hashCode, out lastOnlineTracker))
		{
			lastOnlineTracker = new SocialToastMgr.LastOnlineTracker();
			this.m_lastOnlineTracker[hashCode] = lastOnlineTracker;
		}
		if (newPlayer.IsOnline())
		{
			if (lastOnlineTracker.m_callback != null)
			{
				Processor.CancelScheduledCallback(lastOnlineTracker.m_callback, null);
			}
			lastOnlineTracker.m_callback = null;
			if (fixedTime - lastOnlineTracker.m_localLastOnlineTime >= 5f)
			{
				this.AddToast(UserAttentionBlocker.NONE, newPlayer.GetBestName(), SocialToastMgr.TOAST_TYPE.FRIEND_ONLINE);
				return;
			}
		}
		else
		{
			lastOnlineTracker.m_localLastOnlineTime = fixedTime;
			lastOnlineTracker.m_callback = delegate(object data)
			{
				if (newPlayer != null && !newPlayer.IsOnline())
				{
					this.AddToast(UserAttentionBlocker.NONE, newPlayer.GetBestName(), SocialToastMgr.TOAST_TYPE.FRIEND_OFFLINE, false);
				}
			};
			Processor.ScheduleCallback(5f, false, lastOnlineTracker.m_callback, null);
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0003F22C File Offset: 0x0003D42C
	private void CheckSessionGameStarted(BnetPlayer player)
	{
		if (PresenceMgr.Get().GetStatus(player) == Global.PresenceStatus.TAVERN_BRAWL_GAME)
		{
			if (!TavernBrawlManager.Get().IsCurrentSeasonSessionBased)
			{
				return;
			}
		}
		else if (PresenceMgr.Get().GetStatus(player) != Global.PresenceStatus.ARENA_GAME)
		{
			return;
		}
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null)
		{
			return;
		}
		SessionRecord sessionRecord = hearthstoneGameAccount.GetSessionRecord();
		if (sessionRecord == null)
		{
			return;
		}
		if (sessionRecord.Wins >= 8U && !sessionRecord.RunFinished)
		{
			string key = string.Empty;
			switch (sessionRecord.SessionRecordType)
			{
			case SessionRecordType.ARENA:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_ARENA_START_WITH_MANY_WINS";
				break;
			case SessionRecordType.HEROIC_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_HEROIC_BRAWL_START_WITH_MANY_WINS";
				break;
			case SessionRecordType.TAVERN_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_BRAWLISEUM_START_WITH_MANY_WINS";
				break;
			}
			this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format(key, new object[]
			{
				"5ecaf0ff",
				player.GetBestName(),
				sessionRecord.Wins
			}));
		}
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0003F2FC File Offset: 0x0003D4FC
	private void CheckSessionRecordChanged(BnetPlayer player)
	{
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null)
		{
			return;
		}
		SessionRecord sessionRecord = hearthstoneGameAccount.GetSessionRecord();
		if (sessionRecord == null)
		{
			return;
		}
		string key = string.Empty;
		if (sessionRecord.RunFinished)
		{
			if (sessionRecord.Wins >= 3U)
			{
				switch (sessionRecord.SessionRecordType)
				{
				case SessionRecordType.ARENA:
					key = "GLOBAL_SOCIAL_TOAST_FRIEND_ARENA_COMPLETE";
					break;
				case SessionRecordType.HEROIC_BRAWL:
					key = "GLOBAL_SOCIAL_TOAST_FRIEND_HEROIC_BRAWL_COMPLETE";
					break;
				case SessionRecordType.TAVERN_BRAWL:
					key = "GLOBAL_SOCIAL_TOAST_FRIEND_BRAWLISEUM_COMPLETE";
					break;
				}
				this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format(key, new object[]
				{
					"5ecaf0ff",
					player.GetBestName(),
					sessionRecord.Wins,
					sessionRecord.Losses
				}));
				return;
			}
		}
		else if (sessionRecord.Wins == 0U && sessionRecord.Losses == 0U)
		{
			switch (sessionRecord.SessionRecordType)
			{
			case SessionRecordType.ARENA:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_ARENA_START";
				break;
			case SessionRecordType.HEROIC_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_HEROIC_BRAWL_START";
				break;
			case SessionRecordType.TAVERN_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_BRAWLISEUM_START";
				break;
			case SessionRecordType.DUELS:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_DUEL_START";
				break;
			}
			this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format(key, new object[]
			{
				"5ecaf0ff",
				player.GetBestName()
			}));
		}
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0003F424 File Offset: 0x0003D624
	private void CheckForCardOpened(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (newPlayerAccount.GetCardsOpened() == oldPlayerAccount.GetCardsOpened())
		{
			return;
		}
		string cardsOpened = newPlayerAccount.GetCardsOpened();
		if (string.IsNullOrEmpty(cardsOpened))
		{
			return;
		}
		string[] array = cardsOpened.Split(new char[]
		{
			','
		});
		if (array.Length != 2)
		{
			return;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(array[0]);
		if (entityDef == null)
		{
			return;
		}
		if (array[1] == "1")
		{
			this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_GOLDEN_LEGENDARY", new object[]
			{
				"5ecaf0ff",
				newPlayer.GetBestName(),
				entityDef.GetName(),
				"ffd200"
			}));
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_LEGENDARY", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			entityDef.GetName(),
			"ff9c00"
		}));
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0003F504 File Offset: 0x0003D704
	private bool CheckForHigherRankForFormat(FormatType format, MedalInfoTranslator currentMedalInfo, MedalInfoTranslator lastKnownMedalInfo, out TranslatedMedalInfo rankToShowToastFor)
	{
		rankToShowToastFor = null;
		TranslatedMedalInfo currentMedal = currentMedalInfo.GetCurrentMedal(format);
		TranslatedMedalInfo currentMedal2 = lastKnownMedalInfo.GetCurrentMedal(format);
		if (currentMedal.LeagueConfig.LeagueLevel < currentMedal2.LeagueConfig.LeagueLevel)
		{
			return false;
		}
		int num = 1;
		if (currentMedal.LeagueConfig.LeagueLevel == currentMedal2.LeagueConfig.LeagueLevel)
		{
			if (currentMedal.starLevel <= currentMedal2.starLevel)
			{
				return false;
			}
			num = currentMedal2.starLevel + 1;
		}
		for (int i = currentMedal.starLevel; i >= num; i--)
		{
			LeagueRankDbfRecord leagueRankRecord = RankMgr.Get().GetLeagueRankRecord(currentMedal.leagueId, i);
			if (leagueRankRecord == null)
			{
				return false;
			}
			if (leagueRankRecord.ShowToastOnAttained)
			{
				rankToShowToastFor = MedalInfoTranslator.CreateTranslatedMedalInfo(format, leagueRankRecord.LeagueId, leagueRankRecord.StarLevel, 0);
				break;
			}
		}
		return true;
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0003F5C0 File Offset: 0x0003D7C0
	private void CheckForNewRank(BnetPlayer player)
	{
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(player);
		if (rankPresenceField == null || !rankPresenceField.IsDisplayable())
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		if (!this.m_lastKnownMedals.ContainsKey(hearthstoneGameAccountId))
		{
			this.m_lastKnownMedals[hearthstoneGameAccountId] = rankPresenceField;
			return;
		}
		MedalInfoTranslator lastKnownMedalInfo = this.m_lastKnownMedals[hearthstoneGameAccountId];
		TranslatedMedalInfo translatedMedalInfo;
		bool flag = this.CheckForHigherRankForFormat(FormatType.FT_STANDARD, rankPresenceField, lastKnownMedalInfo, out translatedMedalInfo);
		TranslatedMedalInfo translatedMedalInfo2;
		bool flag2 = this.CheckForHigherRankForFormat(FormatType.FT_WILD, rankPresenceField, lastKnownMedalInfo, out translatedMedalInfo2);
		TranslatedMedalInfo translatedMedalInfo3;
		bool flag3 = this.CheckForHigherRankForFormat(FormatType.FT_CLASSIC, rankPresenceField, lastKnownMedalInfo, out translatedMedalInfo3);
		if (flag || flag2 || flag3)
		{
			this.m_lastKnownMedals[hearthstoneGameAccountId] = rankPresenceField;
		}
		if (translatedMedalInfo3 != null)
		{
			if (translatedMedalInfo3.IsLegendRank())
			{
				this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_LEGEND_CLASSIC", new object[]
				{
					"5ecaf0ff",
					player.GetBestName()
				}));
				return;
			}
			this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_EARNED_CLASSIC", new object[]
			{
				"5ecaf0ff",
				player.GetBestName(),
				translatedMedalInfo3.GetRankName()
			}));
			return;
		}
		else
		{
			if (translatedMedalInfo == null)
			{
				if (translatedMedalInfo2 != null)
				{
					if (translatedMedalInfo2.IsLegendRank())
					{
						this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_LEGEND_WILD", new object[]
						{
							"5ecaf0ff",
							player.GetBestName()
						}));
						return;
					}
					this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_EARNED_WILD", new object[]
					{
						"5ecaf0ff",
						player.GetBestName(),
						translatedMedalInfo2.GetRankName()
					}));
				}
				return;
			}
			if (translatedMedalInfo.IsLegendRank())
			{
				this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_LEGEND", new object[]
				{
					"5ecaf0ff",
					player.GetBestName()
				}));
				return;
			}
			this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_EARNED", new object[]
			{
				"5ecaf0ff",
				player.GetBestName(),
				translatedMedalInfo.GetRankName()
			}));
			return;
		}
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0003F788 File Offset: 0x0003D988
	private void CheckForMissionComplete(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (newPlayerAccount.GetTutorialBeaten() == oldPlayerAccount.GetTutorialBeaten())
		{
			return;
		}
		if (newPlayerAccount.GetTutorialBeaten() != 1)
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_ILLIDAN_COMPLETE", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName()
		}));
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0003F7D8 File Offset: 0x0003D9D8
	private void CheckForMageLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetMageLevel(), newPlayerAccount.GetMageLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_MAGE_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetMageLevel()
		}));
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0003F830 File Offset: 0x0003DA30
	private void CheckForPaladinLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetPaladinLevel(), newPlayerAccount.GetPaladinLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_PALADIN_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetPaladinLevel()
		}));
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0003F888 File Offset: 0x0003DA88
	private void CheckForDruidLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetDruidLevel(), newPlayerAccount.GetDruidLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_DRUID_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetDruidLevel()
		}));
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0003F8E0 File Offset: 0x0003DAE0
	private void CheckForRogueLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetRogueLevel(), newPlayerAccount.GetRogueLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_ROGUE_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetRogueLevel()
		}));
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0003F938 File Offset: 0x0003DB38
	private void CheckForHunterLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetHunterLevel(), newPlayerAccount.GetHunterLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_HUNTER_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetHunterLevel()
		}));
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0003F990 File Offset: 0x0003DB90
	private void CheckForShamanLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetShamanLevel(), newPlayerAccount.GetShamanLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_SHAMAN_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetShamanLevel()
		}));
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
	private void CheckForWarriorLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetWarriorLevel(), newPlayerAccount.GetWarriorLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_WARRIOR_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetWarriorLevel()
		}));
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0003FA40 File Offset: 0x0003DC40
	private void CheckForWarlockLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetWarlockLevel(), newPlayerAccount.GetWarlockLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_WARLOCK_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetWarlockLevel()
		}));
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0003FA98 File Offset: 0x0003DC98
	private void CheckForPriestLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (!this.ShouldToastThisLevel(oldPlayerAccount.GetPriestLevel(), newPlayerAccount.GetPriestLevel()))
		{
			return;
		}
		this.AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_PRIEST_LEVEL", new object[]
		{
			"5ecaf0ff",
			newPlayer.GetBestName(),
			newPlayerAccount.GetPriestLevel()
		}));
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0003FAF0 File Offset: 0x0003DCF0
	private bool ShouldToastThisLevel(int oldLevel, int newLevel)
	{
		return oldLevel != newLevel && (newLevel == 20 || newLevel == 30 || newLevel == 40 || newLevel == 50 || newLevel == 60);
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0003FB14 File Offset: 0x0003DD14
	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		if (!DemoMgr.Get().IsSocialEnabled())
		{
			return;
		}
		List<BnetInvitation> addedReceivedInvites = changelist.GetAddedReceivedInvites();
		if (addedReceivedInvites == null)
		{
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null && myPlayer.IsBusy())
		{
			return;
		}
		foreach (BnetInvitation bnetInvitation in addedReceivedInvites)
		{
			BnetPlayer recentOpponent = FriendMgr.Get().GetRecentOpponent();
			if (recentOpponent != null && recentOpponent.HasAccount(bnetInvitation.GetInviterId()))
			{
				this.AddToast(UserAttentionBlocker.NONE, GameStrings.Get("GLOBAL_SOCIAL_TOAST_RECENT_OPPONENT_FRIEND_REQUEST"));
			}
			else
			{
				this.AddToast(UserAttentionBlocker.NONE, bnetInvitation.GetInviterName(), SocialToastMgr.TOAST_TYPE.FRIEND_INVITE);
			}
		}
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0003FBCC File Offset: 0x0003DDCC
	private void ShutdownHandler(int minutes)
	{
		this.AddToast(UserAttentionBlocker.ALL, GameStrings.Format("GLOBAL_SHUTDOWN_TOAST", new object[]
		{
			"f61f1fff",
			minutes
		}), SocialToastMgr.TOAST_TYPE.DEFAULT, 3.5f);
	}

	// Token: 0x040006C1 RID: 1729
	private const float FADE_IN_TIME = 0.25f;

	// Token: 0x040006C2 RID: 1730
	private const float FADE_OUT_TIME = 0.5f;

	// Token: 0x040006C3 RID: 1731
	private const float HOLD_TIME = 2f;

	// Token: 0x040006C4 RID: 1732
	private const float SHUTDOWN_MESSAGE_TIME = 3.5f;

	// Token: 0x040006C5 RID: 1733
	private const float OFFLINE_TOAST_DELAY = 5f;

	// Token: 0x040006C6 RID: 1734
	private const int MAX_QUEUE_CAPACITY = 5;

	// Token: 0x040006C7 RID: 1735
	private const string BNET_TOAST_SOUND = "UI_BnetToast.prefab:b869739323d1fc241984f9f480fff8ef";

	// Token: 0x040006C8 RID: 1736
	public SocialToast m_defaultSocialToastPrefab;

	// Token: 0x040006C9 RID: 1737
	public SocialToast m_firesideGatheringSocialToastPrefab;

	// Token: 0x040006CA RID: 1738
	private static SocialToastMgr s_instance;

	// Token: 0x040006CB RID: 1739
	private SocialToast m_defaultToast;

	// Token: 0x040006CC RID: 1740
	private SocialToast m_firesideGatheringToast;

	// Token: 0x040006CD RID: 1741
	private SocialToast m_currentToast;

	// Token: 0x040006CE RID: 1742
	private Queue<SocialToastMgr.ToastArgs> m_toastQueue = new Queue<SocialToastMgr.ToastArgs>();

	// Token: 0x040006CF RID: 1743
	private bool m_toastIsShown;

	// Token: 0x040006D0 RID: 1744
	private PlatformDependentValue<Vector3> TOAST_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(235f, 1f, 235f),
		Phone = new Vector3(470f, 1f, 470f)
	};

	// Token: 0x040006D1 RID: 1745
	private global::Map<BnetGameAccountId, MedalInfoTranslator> m_lastKnownMedals = new global::Map<BnetGameAccountId, MedalInfoTranslator>();

	// Token: 0x040006D2 RID: 1746
	private global::Map<int, SocialToastMgr.LastOnlineTracker> m_lastOnlineTracker = new global::Map<int, SocialToastMgr.LastOnlineTracker>();

	// Token: 0x020013B0 RID: 5040
	public enum TOAST_TYPE
	{
		// Token: 0x0400A77A RID: 42874
		DEFAULT,
		// Token: 0x0400A77B RID: 42875
		FRIEND_ONLINE,
		// Token: 0x0400A77C RID: 42876
		FRIEND_OFFLINE,
		// Token: 0x0400A77D RID: 42877
		FRIEND_INVITE,
		// Token: 0x0400A77E RID: 42878
		HEALTHY_GAMING,
		// Token: 0x0400A77F RID: 42879
		HEALTHY_GAMING_OVER_THRESHOLD,
		// Token: 0x0400A780 RID: 42880
		FRIEND_ARENA_COMPLETE,
		// Token: 0x0400A781 RID: 42881
		SPECTATOR_INVITE_SENT,
		// Token: 0x0400A782 RID: 42882
		SPECTATOR_INVITE_RECEIVED,
		// Token: 0x0400A783 RID: 42883
		SPECTATOR_ADDED,
		// Token: 0x0400A784 RID: 42884
		SPECTATOR_REMOVED,
		// Token: 0x0400A785 RID: 42885
		FIRESIDE_GATHERING_IS_HERE_REMINDER
	}

	// Token: 0x020013B1 RID: 5041
	private class ToastArgs
	{
		// Token: 0x0600D838 RID: 55352 RVA: 0x003EDBD2 File Offset: 0x003EBDD2
		public ToastArgs(string message, float displayTime, bool playSound)
		{
			this.m_message = message;
			this.m_displayTime = displayTime;
			this.m_playSound = playSound;
		}

		// Token: 0x0400A786 RID: 42886
		public string m_message;

		// Token: 0x0400A787 RID: 42887
		public float m_displayTime;

		// Token: 0x0400A788 RID: 42888
		public bool m_playSound;
	}

	// Token: 0x020013B2 RID: 5042
	private class LastOnlineTracker
	{
		// Token: 0x0400A789 RID: 42889
		public float m_localLastOnlineTime;

		// Token: 0x0400A78A RID: 42890
		public Processor.ScheduledCallback m_callback;
	}
}
