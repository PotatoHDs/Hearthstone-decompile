using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DungeonCrawl;
using UnityEngine;

[CustomEditClass]
public class AdventureDungeonCrawlBossGraveyard : MonoBehaviour
{
	public delegate void RunEndSequenceCompletedCallback();

	private const int MAX_GRAVEYARD_BOSSES_TO_SHOW = 8;

	private const float CHANCE_TO_PLAY_RARE_DEFEAT_LINE = 0.2f;

	[CustomEditField(Sections = "UI")]
	public NestedPrefab m_bossArchNestedPrefab;

	[CustomEditField(Sections = "UI")]
	public float m_bossArchSpacingHorizontal;

	[CustomEditField(Sections = "UI")]
	public float m_bossArchSpacingVertical;

	[CustomEditField(Sections = "UI")]
	public int m_bossesPerRow = 4;

	[CustomEditField(Sections = "UI")]
	public UberText m_defeatedCount;

	[CustomEditField(Sections = "Animations")]
	public string m_bossFlipSmallAnimName;

	[CustomEditField(Sections = "Animations")]
	public string m_bossFlipLargeAnimName;

	[CustomEditField(Sections = "Animations")]
	public string m_bossFlipNoDesaturateAnimName;

	[CustomEditField(Sections = "Animations")]
	public float m_delayPerBossFlip = 0.63f;

	[CustomEditField(Sections = "Animations")]
	public float m_delayAfterBossFlips = 1.5f;

	[CustomEditField(Sections = "Animations")]
	public float m_delayBeforeRunWinVO;

	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_bossFlipSmallSFX;

	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_bossFlipLargeSFX;

	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_victorySequenceStartSFX;

	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_defeatSequenceStartSFX;

	[CustomEditField(Sections = "Rewards")]
	public GameObject m_rewardPopupContainer;

	private List<AdventureDungeonCrawlBossGraveyardActor> m_bossArches = new List<AdventureDungeonCrawlBossGraveyardActor>();

	private Actor m_bossLostToActor;

	private bool m_subsceneTransitionComplete;

	private bool m_emoteLoadingComplete;

	private EmoteEntryDef m_bossLostToEmoteDef;

	private CardSoundSpell m_bossLostToEmoteSoundSpell;

	private bool m_runCompleteSequenceSeen;

	private List<AdventureHeroPowerDbfRecord> m_justUnlockedHeroPowers;

	private List<AdventureDeckDbfRecord> m_justUnlockedDecks;

	private List<AdventureLoadoutTreasuresDbfRecord> m_justUnlockedLoadoutTreasures;

	private List<AdventureLoadoutTreasuresDbfRecord> m_justUpgradedLoadoutTreasures;

	private IDungeonCrawlData m_dungeonCrawlData;

	private void Start()
	{
		m_bossArches.Add(m_bossArchNestedPrefab.PrefabGameObject().GetComponent<AdventureDungeonCrawlBossGraveyardActor>());
	}

	private void Update()
	{
		if (Application.isEditor)
		{
			UpdateLayout();
		}
	}

	private void OnDestroy()
	{
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().StopVignette();
		}
	}

	private void OnBonusChallengeUnlockObjectLoaded(Reward reward, object callbackData)
	{
		GameUtils.SetParent(reward, m_rewardPopupContainer);
		reward.Show(updateCacheValues: false);
	}

	private IEnumerator PlayBossFlippingSequence(int numBossesToShow, bool defeatedLastBoss)
	{
		float flipDelayTime = m_delayPerBossFlip;
		for (int i = 0; i < numBossesToShow; i++)
		{
			bool flag = i == numBossesToShow - 1;
			Animator component = m_bossArches[i].GetComponent<Animator>();
			if (component != null)
			{
				string stateName = ((!flag) ? m_bossFlipSmallAnimName : (defeatedLastBoss ? m_bossFlipLargeAnimName : m_bossFlipNoDesaturateAnimName));
				component.Play(stateName);
				SoundManager.Get().LoadAndPlay(flag ? m_bossFlipLargeSFX : m_bossFlipSmallSFX);
				yield return new WaitForSeconds(flipDelayTime);
				flipDelayTime *= 0.9f;
				if (flipDelayTime < 0.1f)
				{
					flipDelayTime = 0.1f;
				}
			}
		}
		yield return new WaitForSeconds(m_delayAfterBossFlips);
	}

	private IEnumerator PlayDefeatSequence(int numBossesToShow, int numDefeatedBosses, int numTotalBosses, int bossWhoDefeatedMeId, int heroDbId, GameSaveKeyId adventureServerKeyId, RunEndSequenceCompletedCallback completedCallback)
	{
		if (m_bossLostToActor == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlBossGraveyard.PlayDefeatSequence() - Can't PlayDefeatSequence() without a m_bossLostToActor!");
			yield break;
		}
		bool bossHasEmote = LoadBossLostToEmote();
		if (!bossHasEmote)
		{
			Log.Adventures.Print("No EmoteDef set for DUNGEON_CRAWL_DEFEAT_TAUNT for boss {0}.", m_bossLostToActor.CardDefName);
		}
		while (!m_subsceneTransitionComplete || GameUtils.IsAnyTransitionActive())
		{
			yield return null;
		}
		FullScreenFXMgr.Get().Vignette();
		if (!string.IsNullOrEmpty(m_defeatSequenceStartSFX))
		{
			SoundManager.Get().LoadAndPlay(m_defeatSequenceStartSFX);
		}
		yield return StartCoroutine(PlayBossFlippingSequence(numBossesToShow, defeatedLastBoss: false));
		if (bossHasEmote)
		{
			while (!m_emoteLoadingComplete)
			{
				yield return null;
			}
			Notification notification = NotificationManager.Get().CreateSpeechBubble(GameStrings.Get(m_bossLostToEmoteDef.m_emoteGameStringKey), m_bossLostToActor);
			if (m_bossLostToEmoteSoundSpell == null)
			{
				NotificationManager.Get().DestroyNotification(notification, 5f);
			}
			else
			{
				m_bossLostToEmoteSoundSpell.AddFinishedCallback(delegate
				{
					NotificationManager.Get().DestroyNotification(notification, 0f);
					Object.Destroy(m_bossLostToEmoteSoundSpell.gameObject);
				});
				m_bossLostToEmoteSoundSpell.Reactivate();
			}
			while (notification != null)
			{
				yield return null;
			}
		}
		bool flag = false;
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(m_dungeonCrawlData.GetMission());
		if (numDefeatedBosses >= numTotalBosses - 1)
		{
			flag = DungeonCrawlSubDef_VOLines.PlayVOLine(m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.FINAL_BOSS_LOSS_EVENTS, bossWhoDefeatedMeId);
		}
		if (!flag)
		{
			DungeonCrawlSubDef_VOLines.PlayVOLine(m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.BOSS_LOSS_1, bossWhoDefeatedMeId);
		}
		FullScreenFXMgr.Get().StopVignette();
		if (!HasNewlyUnlockedGSDRewardsToShow())
		{
			yield break;
		}
		PopupDisplayManager.Get().ShowRewardsForAdventureUnlocks(m_justUnlockedHeroPowers, m_justUnlockedDecks, m_justUnlockedLoadoutTreasures, m_justUpgradedLoadoutTreasures, delegate
		{
			if (!m_runCompleteSequenceSeen)
			{
				MarkRunCompleteSequenceAsSeen(adventureServerKeyId, completedCallback);
			}
		});
	}

	private static void PlayVictoryVO(IDungeonCrawlData dungeonCrawlData, bool hasCompletedAdventureWithAllClasses, bool hasSeenCompleteWithAllClassesFirstTime, bool firstTimeCompletedAsClass, int numClassesCompleted, int heroDbId)
	{
		AdventureDbId selectedAdventure = dungeonCrawlData.GetSelectedAdventure();
		GameSaveKeyId gameSaveClientKey = dungeonCrawlData.GetGameSaveClientKey();
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(dungeonCrawlData.GetMission());
		bool flag = false;
		if (hasCompletedAdventureWithAllClasses)
		{
			if (!hasSeenCompleteWithAllClassesFirstTime)
			{
				flag = flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_CLASSES_FIRST_TIME);
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO, 1L));
			}
			else
			{
				flag = flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_CLASSES, 0, allowRepeatDuringSession: false);
			}
		}
		if (!flag && firstTimeCompletedAsClass && numClassesCompleted > 0)
		{
			int num = numClassesCompleted - 1;
			if (num < DungeonCrawlSubDef_VOLines.CLASS_COMPLETE_EVENTS.Length)
			{
				flag = flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.CLASS_COMPLETE_EVENTS[num], 0, allowRepeatDuringSession: false);
			}
		}
		if (!flag)
		{
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WING_COMPLETE_VO, out List<long> values))
			{
				values = new List<long>();
			}
			bool num2 = values.Contains((long)wingIdFromMissionId);
			int count = values.Count;
			if (!num2 && count < DungeonCrawlSubDef_VOLines.WING_COMPLETE_EVENTS.Length)
			{
				flag = flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.WING_COMPLETE_EVENTS[count], 0, allowRepeatDuringSession: false);
				if (flag)
				{
					values.Add((long)wingIdFromMissionId);
					GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WING_COMPLETE_VO, values.ToArray()));
				}
			}
		}
		if (!flag)
		{
			flag = flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.WING_COMPLETE_GENERAL, 0, allowRepeatDuringSession: false);
		}
		if (!flag)
		{
			AdventureWingDef wingDef = dungeonCrawlData.GetWingDef(wingIdFromMissionId);
			if (AdventureUtils.CanPlayWingCompleteQuote(wingDef))
			{
				string legacyAssetName = new AssetReference(wingDef.m_CompleteQuoteVOLine).GetLegacyAssetName();
				NotificationManager.Get().CreateCharacterQuote(wingDef.m_CompleteQuotePrefab, GameStrings.Get(legacyAssetName), wingDef.m_CompleteQuoteVOLine, allowRepeatDuringSession: false);
			}
		}
	}

	private IEnumerator PlayVictorySequence(int numBossesToShow, bool hasCompletedAdventureWithAllClasses, bool firstTimeCompletedAsClass, int numClassesCompleted, int heroDbId, RunEndSequenceCompletedCallback completedCallback)
	{
		while (!m_subsceneTransitionComplete || GameUtils.IsAnyTransitionActive())
		{
			yield return null;
		}
		FullScreenFXMgr.Get().Vignette();
		if (!string.IsNullOrEmpty(m_victorySequenceStartSFX))
		{
			SoundManager.Get().LoadAndPlay(m_victorySequenceStartSFX);
		}
		yield return StartCoroutine(PlayBossFlippingSequence(numBossesToShow, defeatedLastBoss: true));
		AdventureDbId adventureDbId = m_dungeonCrawlData.GetSelectedAdventure();
		AdventureModeDbId adventureModeDbId = m_dungeonCrawlData.GetSelectedMode();
		AdventureDef adventureDef = m_dungeonCrawlData.GetAdventureDef();
		AdventureSubDef adventureSubDef = ((adventureDef == null) ? null : adventureDef.GetSubDef(adventureModeDbId));
		if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_BannerRewardPrefab))
		{
			TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(heroDbId);
			string text = GameStrings.FormatLocalizedString(adventureSubDef.GetCompleteBannerText(), GetClassNameFromTagClass(tagClassFromCardDbId));
			BannerManager.Get().ShowBanner(adventureDef.m_BannerRewardPrefab, null, text);
		}
		yield return new WaitForSeconds(m_delayBeforeRunWinVO);
		GameSaveKeyId gameSaveClientKey = m_dungeonCrawlData.GetGameSaveClientKey();
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO, out long value);
		bool hasSeenCompleteWithAllClassesFirstTime = value == 1;
		PlayVictoryVO(m_dungeonCrawlData, hasCompletedAdventureWithAllClasses, hasSeenCompleteWithAllClassesFirstTime, firstTimeCompletedAsClass, numClassesCompleted, heroDbId);
		FullScreenFXMgr.Get().StopVignette();
		PopupDisplayManager.Get().ShowRewardsForAdventureUnlocks(m_justUnlockedHeroPowers, m_justUnlockedDecks, m_justUnlockedLoadoutTreasures, m_justUpgradedLoadoutTreasures, delegate
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureDbId, (int)adventureModeDbId);
			ShowAdditionalPopupsAfterOutstandingPopups(hasCompletedAdventureWithAllClasses, hasSeenCompleteWithAllClassesFirstTime, adventureDataRecord, completedCallback);
		});
	}

	private void MarkRunCompleteSequenceAsSeen(GameSaveKeyId adventureServerKeyId, RunEndSequenceCompletedCallback completedCallback)
	{
		m_runCompleteSequenceSeen = true;
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(adventureServerKeyId, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, 1L));
		completedCallback();
	}

	private void ShowAdditionalPopupsAfterOutstandingPopups(bool hasCompletedAdventureWithAllClasses, bool hasSeenCompleteWithAllClassesFirstTime, AdventureDataDbfRecord adventureDataRecord, RunEndSequenceCompletedCallback completedCallback)
	{
		if (hasCompletedAdventureWithAllClasses && !hasSeenCompleteWithAllClassesFirstTime)
		{
			string prefabShownOnComplete = adventureDataRecord.PrefabShownOnComplete;
			if (!string.IsNullOrEmpty(prefabShownOnComplete))
			{
				new BonusChallengeUnlockData(prefabShownOnComplete, adventureDataRecord.DungeonCrawlBossCardPrefab).LoadRewardObject(delegate(Reward reward, object data)
				{
					reward.RegisterHideListener(delegate
					{
						MarkRunCompleteSequenceAsSeen((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, completedCallback);
						Navigation.GoBack();
						m_dungeonCrawlData.SetSelectedAdventureMode((AdventureDbId)adventureDataRecord.AdventureId, AdventureModeDbId.BONUS_CHALLENGE);
					});
					OnBonusChallengeUnlockObjectLoaded(reward, data);
				});
				return;
			}
		}
		MarkRunCompleteSequenceAsSeen((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, completedCallback);
	}

	private string GetClassNameFromTagClass(TAG_CLASS deckClass)
	{
		List<int> guestHeroesForCurrentAdventure = m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		if (guestHeroesForCurrentAdventure.Count > 0)
		{
			foreach (int cardDbId in guestHeroesForCurrentAdventure)
			{
				if (GameUtils.GetTagClassFromCardDbId(cardDbId) == deckClass)
				{
					GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord((GuestHeroDbfRecord r) => r.CardId == cardDbId);
					if (record != null)
					{
						return record.Name;
					}
				}
			}
		}
		return GameStrings.GetClassName(deckClass);
	}

	private void DisableBoss(Actor boss)
	{
		boss.transform.Rotate(new Vector3(0f, 0f, 180f));
	}

	private bool LoadBossLostToEmote()
	{
		if (!m_bossLostToActor.HasCardDef || m_bossLostToActor.EmoteDefs == null)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlBossGraveyard.PlayDefeatSequence() - No cardDef found for the boss you lost to!");
			return false;
		}
		m_bossLostToEmoteDef = m_bossLostToActor.EmoteDefs.Find((EmoteEntryDef e) => e.m_emoteType == EmoteType.DUNGEON_CRAWL_DEFEAT_TAUNT);
		if (m_bossLostToEmoteDef == null)
		{
			return false;
		}
		EmoteEntryDef emoteEntryDef = m_bossLostToActor.EmoteDefs.Find((EmoteEntryDef e) => e.m_emoteType == EmoteType.DUNGEON_CRAWL_DEFEAT_TAUNT_SUPER_RARE);
		if (emoteEntryDef != null && Random.Range(0f, 1f) < 0.2f)
		{
			m_bossLostToEmoteDef = emoteEntryDef;
		}
		AssetLoader.Get().InstantiatePrefab(m_bossLostToEmoteDef.m_emoteSoundSpellPath, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			m_emoteLoadingComplete = true;
			if (go == null)
			{
				Log.Adventures.PrintError("AdventureDungeonCrawlBossGraveyard.PlayDefeatSequence() - Failed to load CardSoundSpell at path {0}!", m_bossLostToEmoteDef.m_emoteSoundSpellPath);
			}
			else
			{
				m_bossLostToEmoteSoundSpell = go.GetComponent<CardSoundSpell>();
			}
		});
		return true;
	}

	private void UpdateLayout()
	{
		Vector3 localPosition = m_bossArches[0].transform.localPosition;
		for (int i = 1; i < m_bossArches.Count; i++)
		{
			m_bossArches[i].transform.localPosition = new Vector3(localPosition.x + m_bossArchSpacingHorizontal * (float)(i % m_bossesPerRow), localPosition.y, localPosition.z + m_bossArchSpacingVertical * (float)(i / m_bossesPerRow));
		}
	}

	private void CheckForNewlyUnlockedAdventureRewards(GameSaveKeyId gameSaveServerKey, GameSaveKeyId gameSaveClientKey, TAG_CLASS heroClass)
	{
		List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
		List<AdventureHeroPowerDbfRecord> heroPowersForClass = m_dungeonCrawlData.GetHeroPowersForClass(heroClass);
		m_justUnlockedHeroPowers = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(heroPowersForClass.Cast<DbfRecord>(), m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_HERO_POWERS, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_HERO_POWERS, list).Cast<AdventureHeroPowerDbfRecord>().ToList();
		List<AdventureDeckDbfRecord> decksForClass = m_dungeonCrawlData.GetDecksForClass(heroClass);
		m_justUnlockedDecks = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(decksForClass.Cast<DbfRecord>(), m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_DECKS, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_DECKS, list).Cast<AdventureDeckDbfRecord>().ToList();
		List<AdventureLoadoutTreasuresDbfRecord> loadoutTreasuresForClass = m_dungeonCrawlData.GetLoadoutTreasuresForClass(heroClass);
		m_justUnlockedLoadoutTreasures = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(loadoutTreasuresForClass.Cast<DbfRecord>(), m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_LOADOUT_TREASURES, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_LOADOUT_TREASURES, list).Cast<AdventureLoadoutTreasuresDbfRecord>().ToList();
		m_justUpgradedLoadoutTreasures = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(loadoutTreasuresForClass.Cast<DbfRecord>(), m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_LOADOUT_TREASURES, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_LOADOUT_TREASURES, list, checkForUpgrades: true).Cast<AdventureLoadoutTreasuresDbfRecord>().ToList();
		if (list.Count > 0)
		{
			GameSaveDataManager.Get().SaveSubkeys(list);
		}
	}

	private bool HasNewlyUnlockedGSDRewardsToShow()
	{
		if ((m_justUnlockedHeroPowers == null || m_justUnlockedHeroPowers.Count <= 0) && (m_justUnlockedDecks == null || m_justUnlockedDecks.Count <= 0) && (m_justUnlockedLoadoutTreasures == null || m_justUnlockedLoadoutTreasures.Count <= 0))
		{
			if (m_justUpgradedLoadoutTreasures != null)
			{
				return m_justUpgradedLoadoutTreasures.Count > 0;
			}
			return false;
		}
		return true;
	}

	public void OnSubSceneTransitionComplete()
	{
		m_subsceneTransitionComplete = true;
	}

	public void ShowRunEnd(IDungeonCrawlData dungeonCrawlData, List<long> defeatedBossIds, long bossWhoDefeatedMeId, int numTotalBosses, bool hasCompletedAdventureWithAllClasses, bool firstTimeCompletedAsClass, int numClassesCompleted, int heroDbId, GameSaveKeyId adventureGameSaveServerKey, GameSaveKeyId adventureGameSaveClientKey, AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback loadCompletedCallback, RunEndSequenceCompletedCallback completedCallback)
	{
		if (dungeonCrawlData == null)
		{
			Log.Adventures.PrintError("Error!  AdventureDungeonCrawlBossGraveyard.ShowRunEnd() called with null dungeonCrawlData!)");
			return;
		}
		m_dungeonCrawlData = dungeonCrawlData;
		if (m_bossArches.Count < 1)
		{
			Log.Adventures.PrintError("Error!  AdventureDungeonCrawlBossGraveyard.ShowRunEnd() called when m_bossArches is empty! (Probably because Start() has not yet executed.)");
			return;
		}
		TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(heroDbId);
		CheckForNewlyUnlockedAdventureRewards(adventureGameSaveServerKey, adventureGameSaveClientKey, tagClassFromCardDbId);
		int num = defeatedBossIds?.Count ?? 0;
		bool flag = num < numTotalBosses;
		int num2 = Mathf.Min(8, num + (flag ? 1 : 0));
		int num3 = Mathf.Max(0, num - num2 + (flag ? 1 : 0));
		m_defeatedCount.Text = GameStrings.Format("GLUE_ADVENTURE_DUNGEON_CRAWL_DEFEATED_COUNT", num, numTotalBosses);
		Actor actor = m_bossArches[0];
		while (m_bossArches.Count < 8)
		{
			GameObject obj = Object.Instantiate(actor.gameObject);
			obj.transform.parent = actor.transform.parent;
			obj.transform.localScale = actor.transform.localScale;
			AdventureDungeonCrawlBossGraveyardActor component = obj.GetComponent<AdventureDungeonCrawlBossGraveyardActor>();
			m_bossArches.Add(component);
		}
		for (int i = 0; i < m_bossArches.Count; i++)
		{
			AdventureDungeonCrawlBossGraveyardActor adventureDungeonCrawlBossGraveyardActor = m_bossArches[i];
			adventureDungeonCrawlBossGraveyardActor.SetStyle(m_dungeonCrawlData);
			DisableBoss(adventureDungeonCrawlBossGraveyardActor);
		}
		for (int j = 0; j < num2; j++)
		{
			int num4 = j + num3;
			bool flag2 = num4 == num;
			long num5 = (flag2 ? bossWhoDefeatedMeId : defeatedBossIds[num4]);
			string text = GameUtils.TranslateDbIdToCardId((int)num5);
			if (text == null)
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlBossGraveyard.SetBossDbIds() - No cardId for boss dbId {0}, in arch number {1}!", num5, j);
				continue;
			}
			using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(text))
			{
				m_bossArches[j].SetFullDef(fullDef);
				m_bossArches[j].SetPremium(TAG_PREMIUM.NORMAL);
				m_bossArches[j].UpdateAllComponents();
				m_bossArches[j].Show();
			}
			if (flag2)
			{
				m_bossLostToActor = m_bossArches[j];
				continue;
			}
			Flipbook componentInChildren = m_bossArches[j].GetComponentInChildren<Flipbook>(includeInactive: true);
			if (componentInChildren != null)
			{
				componentInChildren.gameObject.SetActive(value: true);
			}
		}
		UpdateLayout();
		loadCompletedCallback();
		StopAllCoroutines();
		if (flag)
		{
			if (!HasNewlyUnlockedGSDRewardsToShow())
			{
				MarkRunCompleteSequenceAsSeen(adventureGameSaveServerKey, completedCallback);
			}
			StartCoroutine(PlayDefeatSequence(num2, num, numTotalBosses, (int)bossWhoDefeatedMeId, heroDbId, adventureGameSaveServerKey, completedCallback));
		}
		else
		{
			StartCoroutine(PlayVictorySequence(num2, hasCompletedAdventureWithAllClasses, firstTimeCompletedAsClass, numClassesCompleted, heroDbId, completedCallback));
		}
	}
}
