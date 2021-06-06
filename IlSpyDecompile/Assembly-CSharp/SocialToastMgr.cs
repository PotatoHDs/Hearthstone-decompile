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

public class SocialToastMgr : MonoBehaviour
{
	public enum TOAST_TYPE
	{
		DEFAULT,
		FRIEND_ONLINE,
		FRIEND_OFFLINE,
		FRIEND_INVITE,
		HEALTHY_GAMING,
		HEALTHY_GAMING_OVER_THRESHOLD,
		FRIEND_ARENA_COMPLETE,
		SPECTATOR_INVITE_SENT,
		SPECTATOR_INVITE_RECEIVED,
		SPECTATOR_ADDED,
		SPECTATOR_REMOVED,
		FIRESIDE_GATHERING_IS_HERE_REMINDER
	}

	private class ToastArgs
	{
		public string m_message;

		public float m_displayTime;

		public bool m_playSound;

		public ToastArgs(string message, float displayTime, bool playSound)
		{
			m_message = message;
			m_displayTime = displayTime;
			m_playSound = playSound;
		}
	}

	private class LastOnlineTracker
	{
		public float m_localLastOnlineTime;

		public Processor.ScheduledCallback m_callback;
	}

	private const float FADE_IN_TIME = 0.25f;

	private const float FADE_OUT_TIME = 0.5f;

	private const float HOLD_TIME = 2f;

	private const float SHUTDOWN_MESSAGE_TIME = 3.5f;

	private const float OFFLINE_TOAST_DELAY = 5f;

	private const int MAX_QUEUE_CAPACITY = 5;

	private const string BNET_TOAST_SOUND = "UI_BnetToast.prefab:b869739323d1fc241984f9f480fff8ef";

	public SocialToast m_defaultSocialToastPrefab;

	public SocialToast m_firesideGatheringSocialToastPrefab;

	private static SocialToastMgr s_instance;

	private SocialToast m_defaultToast;

	private SocialToast m_firesideGatheringToast;

	private SocialToast m_currentToast;

	private Queue<ToastArgs> m_toastQueue = new Queue<ToastArgs>();

	private bool m_toastIsShown;

	private PlatformDependentValue<Vector3> TOAST_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(235f, 1f, 235f),
		Phone = new Vector3(470f, 1f, 470f)
	};

	private Map<BnetGameAccountId, MedalInfoTranslator> m_lastKnownMedals = new Map<BnetGameAccountId, MedalInfoTranslator>();

	private Map<int, LastOnlineTracker> m_lastOnlineTracker = new Map<int, LastOnlineTracker>();

	private void Awake()
	{
		s_instance = this;
		CreateSocialToastObjects();
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += OnPresenceChanged;
		BnetFriendMgr.Get().AddChangeListener(OnFriendsChanged);
		Network.Get().SetShutdownHandler(ShutdownHandler);
		SoundManager.Get().Load("UI_BnetToast.prefab:b869739323d1fc241984f9f480fff8ef");
	}

	private void CreateSocialToastObjects()
	{
		if (!(BnetBar.Get() == null) && !(BnetBar.Get().m_socialToastBone == null) && (m_defaultToast == null || m_firesideGatheringToast == null))
		{
			CreateSocialToastObject(m_defaultSocialToastPrefab, ref m_defaultToast);
			CreateSocialToastObject(m_firesideGatheringSocialToastPrefab, ref m_firesideGatheringToast);
			m_currentToast = m_defaultToast;
		}
	}

	private void OnDestroy()
	{
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= OnPresenceChanged;
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
		BnetFriendMgr.Get().RemoveChangeListener(OnFriendsChanged);
		m_lastKnownMedals.Clear();
		s_instance = null;
	}

	public static SocialToastMgr Get()
	{
		return s_instance;
	}

	public void Reset()
	{
		if (!(m_currentToast == null))
		{
			iTween.Stop(m_currentToast.gameObject, includechildren: true);
			iTween.Stop(base.gameObject, includechildren: true);
			RenderUtils.SetAlpha(m_currentToast.gameObject, 0f);
			m_toastQueue.Clear();
			DeactivateToast();
		}
	}

	public void AddToast(UserAttentionBlocker blocker, string textArg)
	{
		AddToast(blocker, textArg, TOAST_TYPE.DEFAULT, 2f, playSound: true);
	}

	public void AddToast(UserAttentionBlocker blocker, string textArg, TOAST_TYPE toastType)
	{
		AddToast(blocker, textArg, toastType, 2f, playSound: true);
	}

	public void AddToast(UserAttentionBlocker blocker, string textArg, TOAST_TYPE toastType, bool playSound)
	{
		AddToast(blocker, textArg, toastType, 2f, playSound);
	}

	public void AddToast(UserAttentionBlocker blocker, string textArg, TOAST_TYPE toastType, float displayTime)
	{
		AddToast(blocker, textArg, toastType, displayTime, playSound: true);
	}

	public void AddToast(UserAttentionBlocker blocker, string textArg, TOAST_TYPE toastType, float displayTime, bool playSound)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "SocialToastMgr.AddToast:" + toastType))
		{
			return;
		}
		SocialToastDesign design = SocialToastDesign.Default;
		string message;
		switch (toastType)
		{
		case TOAST_TYPE.DEFAULT:
			message = textArg;
			break;
		case TOAST_TYPE.FRIEND_OFFLINE:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_OFFLINE", "999999ff", textArg);
			break;
		case TOAST_TYPE.FRIEND_ONLINE:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_ONLINE", "5ecaf0ff", textArg);
			break;
		case TOAST_TYPE.FRIEND_INVITE:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_REQUEST", "5ecaf0ff", textArg);
			break;
		case TOAST_TYPE.HEALTHY_GAMING:
			message = GameStrings.Format("GLOBAL_HEALTHY_GAMING_TOAST", textArg);
			break;
		case TOAST_TYPE.HEALTHY_GAMING_OVER_THRESHOLD:
			message = GameStrings.Format("GLOBAL_HEALTHY_GAMING_TOAST_OVER_THRESHOLD", textArg);
			break;
		case TOAST_TYPE.SPECTATOR_INVITE_SENT:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_INVITE_SENT", "5ecaf0ff", textArg);
			break;
		case TOAST_TYPE.SPECTATOR_INVITE_RECEIVED:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_INVITE_RECEIVED", "5ecaf0ff", textArg);
			break;
		case TOAST_TYPE.SPECTATOR_ADDED:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_ADDED", "5ecaf0ff", textArg);
			break;
		case TOAST_TYPE.SPECTATOR_REMOVED:
			message = GameStrings.Format("GLOBAL_SOCIAL_TOAST_SPECTATOR_REMOVED", "5ecaf0ff", textArg);
			break;
		case TOAST_TYPE.FIRESIDE_GATHERING_IS_HERE_REMINDER:
			design = SocialToastDesign.FiresideGathering;
			message = string.Format("<color=#{0}>{1}</color>", "ffd200", GameStrings.Get("GLOBAL_FIRESIDE_GATHERING"));
			break;
		default:
			message = "";
			break;
		}
		m_currentToast = GetSocialToastFromDesign(design);
		if (m_currentToast == null)
		{
			Log.All.PrintWarning("Toast design is not created yet");
		}
		else if (m_toastQueue.Count <= 5)
		{
			ToastArgs item = new ToastArgs(message, displayTime, playSound);
			m_toastQueue.Enqueue(item);
			if (!m_toastIsShown && !AchievementManager.Get().IsShowingToast())
			{
				FadeInToast();
			}
		}
	}

	public SocialToast CreateDefaultSocialToast(Transform parent)
	{
		SocialToast newToastReference = null;
		CreateSocialToastObject(m_defaultSocialToastPrefab, ref newToastReference, parent);
		return newToastReference;
	}

	private void CreateSocialToastObject(SocialToast prefab, ref SocialToast newToastReference, Transform parent = null)
	{
		newToastReference = Object.Instantiate(prefab);
		RenderUtils.SetAlpha(newToastReference.gameObject, 0f);
		newToastReference.gameObject.SetActive(value: false);
		newToastReference.transform.parent = ((parent != null) ? parent : BnetBar.Get().m_socialToastBone.transform);
		newToastReference.transform.localRotation = Quaternion.Euler(new Vector3(90f, 180f, 0f));
		newToastReference.transform.localScale = TOAST_SCALE;
		newToastReference.transform.position = BnetBar.Get().m_socialToastBone.transform.position;
	}

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

	private SocialToast GetSocialToastFromDesign(SocialToastDesign design)
	{
		CreateSocialToastObjects();
		if (design == SocialToastDesign.FiresideGathering)
		{
			return m_firesideGatheringToast;
		}
		return m_defaultToast;
	}

	private void FadeInToast()
	{
		m_toastIsShown = true;
		ToastArgs toastArgs = m_toastQueue.Dequeue();
		m_currentToast.gameObject.SetActive(value: true);
		m_currentToast.SetText(toastArgs.m_message);
		Hashtable args = iTween.Hash("amount", 1f, "time", 0.25f, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "FadeOutToast", "oncompletetarget", base.gameObject, "oncompleteparams", toastArgs.m_displayTime, "name", "fade");
		iTween.StopByName(base.gameObject, "fade");
		iTween.FadeTo(m_currentToast.gameObject, args);
		RenderUtils.SetAlpha(m_currentToast.gameObject, 1f);
		if (toastArgs.m_playSound)
		{
			PlayToastSound();
		}
	}

	public void PlayToastSound()
	{
		SoundManager.Get().LoadAndPlay("UI_BnetToast.prefab:b869739323d1fc241984f9f480fff8ef");
	}

	private void FadeOutToast(float displayTime)
	{
		Hashtable args = iTween.Hash("amount", 0f, "delay", displayTime, "time", 0.25f, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "DeactivateToast", "oncompletetarget", base.gameObject, "name", "fade");
		iTween.FadeTo(m_currentToast.gameObject, args);
	}

	private void DeactivateToast()
	{
		m_currentToast.gameObject.SetActive(value: false);
		m_toastIsShown = false;
		CheckToastQueue();
	}

	public void CheckToastQueue()
	{
		AchievementManager achievementManager = AchievementManager.Get();
		achievementManager?.CheckToastQueue();
		if (m_toastQueue.Count != 0 && (achievementManager == null || !achievementManager.IsShowingToast()))
		{
			FadeInToast();
		}
	}

	public bool IsShowingToast()
	{
		return m_toastIsShown;
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (!DemoMgr.Get().IsSocialEnabled())
		{
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		foreach (BnetPlayerChange change in changelist.GetChanges())
		{
			if (change.GetPlayer() == null || change.GetNewPlayer() == null || change == null || !change.GetPlayer().IsDisplayable() || change.GetPlayer() == myPlayer || !BnetFriendMgr.Get().IsFriend(change.GetPlayer()))
			{
				continue;
			}
			BnetPlayer oldPlayer = change.GetOldPlayer();
			BnetPlayer newPlayer = change.GetNewPlayer();
			CheckForOnlineStatusChanged(oldPlayer, newPlayer);
			if (oldPlayer != null)
			{
				BnetGameAccount hearthstoneGameAccount = newPlayer.GetHearthstoneGameAccount();
				BnetGameAccount hearthstoneGameAccount2 = oldPlayer.GetHearthstoneGameAccount();
				if (!(hearthstoneGameAccount2 == null) && !(hearthstoneGameAccount == null))
				{
					CheckForCardOpened(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForDruidLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForHunterLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForMageLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForPaladinLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForPriestLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForRogueLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForShamanLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForWarlockLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForWarriorLevelChanged(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
					CheckForMissionComplete(hearthstoneGameAccount2, hearthstoneGameAccount, newPlayer);
				}
			}
		}
	}

	private void OnPresenceChanged(PresenceUpdate[] updates)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		for (int i = 0; i < updates.Length; i++)
		{
			PresenceUpdate presenceUpdate = updates[i];
			if (presenceUpdate.programId != BnetProgramId.HEARTHSTONE)
			{
				continue;
			}
			BnetPlayer player = BnetUtils.GetPlayer(BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId));
			if (player != null && player != myPlayer && player.IsDisplayable() && BnetFriendMgr.Get().IsFriend(player))
			{
				switch (presenceUpdate.fieldId)
				{
				case 17u:
					CheckSessionGameStarted(player);
					break;
				case 22u:
					CheckSessionRecordChanged(player);
					break;
				case 18u:
					CheckForNewRank(player);
					break;
				}
			}
		}
	}

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
		LastOnlineTracker value = null;
		float fixedTime = Time.fixedTime;
		int hashCode = newPlayer.GetAccountId().GetHashCode();
		if (!m_lastOnlineTracker.TryGetValue(hashCode, out value))
		{
			value = new LastOnlineTracker();
			m_lastOnlineTracker[hashCode] = value;
		}
		if (newPlayer.IsOnline())
		{
			if (value.m_callback != null)
			{
				Processor.CancelScheduledCallback(value.m_callback);
			}
			value.m_callback = null;
			if (fixedTime - value.m_localLastOnlineTime >= 5f)
			{
				AddToast(UserAttentionBlocker.NONE, newPlayer.GetBestName(), TOAST_TYPE.FRIEND_ONLINE);
			}
			return;
		}
		value.m_localLastOnlineTime = fixedTime;
		value.m_callback = delegate
		{
			if (newPlayer != null && !newPlayer.IsOnline())
			{
				AddToast(UserAttentionBlocker.NONE, newPlayer.GetBestName(), TOAST_TYPE.FRIEND_OFFLINE, playSound: false);
			}
		};
		Processor.ScheduleCallback(5f, realTime: false, value.m_callback);
	}

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
		if (sessionRecord != null && sessionRecord.Wins >= 8 && !sessionRecord.RunFinished)
		{
			string key = string.Empty;
			switch (sessionRecord.SessionRecordType)
			{
			case SessionRecordType.ARENA:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_ARENA_START_WITH_MANY_WINS";
				break;
			case SessionRecordType.TAVERN_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_BRAWLISEUM_START_WITH_MANY_WINS";
				break;
			case SessionRecordType.HEROIC_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_HEROIC_BRAWL_START_WITH_MANY_WINS";
				break;
			}
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format(key, "5ecaf0ff", player.GetBestName(), sessionRecord.Wins));
		}
	}

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
			if (sessionRecord.Wins >= 3)
			{
				switch (sessionRecord.SessionRecordType)
				{
				case SessionRecordType.ARENA:
					key = "GLOBAL_SOCIAL_TOAST_FRIEND_ARENA_COMPLETE";
					break;
				case SessionRecordType.TAVERN_BRAWL:
					key = "GLOBAL_SOCIAL_TOAST_FRIEND_BRAWLISEUM_COMPLETE";
					break;
				case SessionRecordType.HEROIC_BRAWL:
					key = "GLOBAL_SOCIAL_TOAST_FRIEND_HEROIC_BRAWL_COMPLETE";
					break;
				}
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format(key, "5ecaf0ff", player.GetBestName(), sessionRecord.Wins, sessionRecord.Losses));
			}
		}
		else if (sessionRecord.Wins == 0 && sessionRecord.Losses == 0)
		{
			switch (sessionRecord.SessionRecordType)
			{
			case SessionRecordType.ARENA:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_ARENA_START";
				break;
			case SessionRecordType.TAVERN_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_BRAWLISEUM_START";
				break;
			case SessionRecordType.HEROIC_BRAWL:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_HEROIC_BRAWL_START";
				break;
			case SessionRecordType.DUELS:
				key = "GLOBAL_SOCIAL_TOAST_FRIEND_DUEL_START";
				break;
			}
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format(key, "5ecaf0ff", player.GetBestName()));
		}
	}

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
		string[] array = cardsOpened.Split(',');
		if (array.Length != 2)
		{
			return;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(array[0]);
		if (entityDef != null)
		{
			if (array[1] == "1")
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_GOLDEN_LEGENDARY", "5ecaf0ff", newPlayer.GetBestName(), entityDef.GetName(), "ffd200"));
			}
			else
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_LEGENDARY", "5ecaf0ff", newPlayer.GetBestName(), entityDef.GetName(), "ff9c00"));
			}
		}
	}

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
		for (int num2 = currentMedal.starLevel; num2 >= num; num2--)
		{
			LeagueRankDbfRecord leagueRankRecord = RankMgr.Get().GetLeagueRankRecord(currentMedal.leagueId, num2);
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

	private void CheckForNewRank(BnetPlayer player)
	{
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(player);
		if (rankPresenceField == null || !rankPresenceField.IsDisplayable())
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		if (!m_lastKnownMedals.ContainsKey(hearthstoneGameAccountId))
		{
			m_lastKnownMedals[hearthstoneGameAccountId] = rankPresenceField;
			return;
		}
		MedalInfoTranslator lastKnownMedalInfo = m_lastKnownMedals[hearthstoneGameAccountId];
		TranslatedMedalInfo rankToShowToastFor;
		bool num = CheckForHigherRankForFormat(FormatType.FT_STANDARD, rankPresenceField, lastKnownMedalInfo, out rankToShowToastFor);
		TranslatedMedalInfo rankToShowToastFor2;
		bool flag = CheckForHigherRankForFormat(FormatType.FT_WILD, rankPresenceField, lastKnownMedalInfo, out rankToShowToastFor2);
		TranslatedMedalInfo rankToShowToastFor3;
		bool flag2 = CheckForHigherRankForFormat(FormatType.FT_CLASSIC, rankPresenceField, lastKnownMedalInfo, out rankToShowToastFor3);
		if (num || flag || flag2)
		{
			m_lastKnownMedals[hearthstoneGameAccountId] = rankPresenceField;
		}
		if (rankToShowToastFor3 != null)
		{
			if (rankToShowToastFor3.IsLegendRank())
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_LEGEND_CLASSIC", "5ecaf0ff", player.GetBestName()));
			}
			else
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_EARNED_CLASSIC", "5ecaf0ff", player.GetBestName(), rankToShowToastFor3.GetRankName()));
			}
		}
		else if (rankToShowToastFor != null)
		{
			if (rankToShowToastFor.IsLegendRank())
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_LEGEND", "5ecaf0ff", player.GetBestName()));
			}
			else
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_EARNED", "5ecaf0ff", player.GetBestName(), rankToShowToastFor.GetRankName()));
			}
		}
		else if (rankToShowToastFor2 != null)
		{
			if (rankToShowToastFor2.IsLegendRank())
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_LEGEND_WILD", "5ecaf0ff", player.GetBestName()));
			}
			else
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_RANK_EARNED_WILD", "5ecaf0ff", player.GetBestName(), rankToShowToastFor2.GetRankName()));
			}
		}
	}

	private void CheckForMissionComplete(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (newPlayerAccount.GetTutorialBeaten() != oldPlayerAccount.GetTutorialBeaten() && newPlayerAccount.GetTutorialBeaten() == 1)
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_ILLIDAN_COMPLETE", "5ecaf0ff", newPlayer.GetBestName()));
		}
	}

	private void CheckForMageLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetMageLevel(), newPlayerAccount.GetMageLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_MAGE_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetMageLevel()));
		}
	}

	private void CheckForPaladinLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetPaladinLevel(), newPlayerAccount.GetPaladinLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_PALADIN_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetPaladinLevel()));
		}
	}

	private void CheckForDruidLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetDruidLevel(), newPlayerAccount.GetDruidLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_DRUID_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetDruidLevel()));
		}
	}

	private void CheckForRogueLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetRogueLevel(), newPlayerAccount.GetRogueLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_ROGUE_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetRogueLevel()));
		}
	}

	private void CheckForHunterLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetHunterLevel(), newPlayerAccount.GetHunterLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_HUNTER_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetHunterLevel()));
		}
	}

	private void CheckForShamanLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetShamanLevel(), newPlayerAccount.GetShamanLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_SHAMAN_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetShamanLevel()));
		}
	}

	private void CheckForWarriorLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetWarriorLevel(), newPlayerAccount.GetWarriorLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_WARRIOR_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetWarriorLevel()));
		}
	}

	private void CheckForWarlockLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetWarlockLevel(), newPlayerAccount.GetWarlockLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_WARLOCK_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetWarlockLevel()));
		}
	}

	private void CheckForPriestLevelChanged(BnetGameAccount oldPlayerAccount, BnetGameAccount newPlayerAccount, BnetPlayer newPlayer)
	{
		if (ShouldToastThisLevel(oldPlayerAccount.GetPriestLevel(), newPlayerAccount.GetPriestLevel()))
		{
			AddToast(UserAttentionBlocker.NONE, GameStrings.Format("GLOBAL_SOCIAL_TOAST_FRIEND_PRIEST_LEVEL", "5ecaf0ff", newPlayer.GetBestName(), newPlayerAccount.GetPriestLevel()));
		}
	}

	private bool ShouldToastThisLevel(int oldLevel, int newLevel)
	{
		if (oldLevel == newLevel)
		{
			return false;
		}
		if (newLevel == 20 || newLevel == 30 || newLevel == 40 || newLevel == 50 || newLevel == 60)
		{
			return true;
		}
		return false;
	}

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
		foreach (BnetInvitation item in addedReceivedInvites)
		{
			BnetPlayer recentOpponent = FriendMgr.Get().GetRecentOpponent();
			if (recentOpponent != null && recentOpponent.HasAccount(item.GetInviterId()))
			{
				AddToast(UserAttentionBlocker.NONE, GameStrings.Get("GLOBAL_SOCIAL_TOAST_RECENT_OPPONENT_FRIEND_REQUEST"));
			}
			else
			{
				AddToast(UserAttentionBlocker.NONE, item.GetInviterName(), TOAST_TYPE.FRIEND_INVITE);
			}
		}
	}

	private void ShutdownHandler(int minutes)
	{
		AddToast(UserAttentionBlocker.ALL, GameStrings.Format("GLOBAL_SHUTDOWN_TOAST", "f61f1fff", minutes), TOAST_TYPE.DEFAULT, 3.5f);
	}
}
