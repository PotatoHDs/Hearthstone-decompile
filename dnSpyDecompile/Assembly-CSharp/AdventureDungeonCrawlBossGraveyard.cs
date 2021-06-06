using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DungeonCrawl;
using UnityEngine;

// Token: 0x02000036 RID: 54
[CustomEditClass]
public class AdventureDungeonCrawlBossGraveyard : MonoBehaviour
{
	// Token: 0x060001E6 RID: 486 RVA: 0x0000AADB File Offset: 0x00008CDB
	private void Start()
	{
		this.m_bossArches.Add(this.m_bossArchNestedPrefab.PrefabGameObject(false).GetComponent<AdventureDungeonCrawlBossGraveyardActor>());
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000AAF9 File Offset: 0x00008CF9
	private void Update()
	{
		if (Application.isEditor)
		{
			this.UpdateLayout();
		}
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000AB08 File Offset: 0x00008D08
	private void OnDestroy()
	{
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().StopVignette();
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000AB1B File Offset: 0x00008D1B
	private void OnBonusChallengeUnlockObjectLoaded(Reward reward, object callbackData)
	{
		GameUtils.SetParent(reward, this.m_rewardPopupContainer, false);
		reward.Show(false);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000AB31 File Offset: 0x00008D31
	private IEnumerator PlayBossFlippingSequence(int numBossesToShow, bool defeatedLastBoss)
	{
		float flipDelayTime = this.m_delayPerBossFlip;
		int num;
		for (int i = 0; i < numBossesToShow; i = num + 1)
		{
			bool flag = i == numBossesToShow - 1;
			Animator component = this.m_bossArches[i].GetComponent<Animator>();
			if (component != null)
			{
				string stateName;
				if (flag)
				{
					stateName = (defeatedLastBoss ? this.m_bossFlipLargeAnimName : this.m_bossFlipNoDesaturateAnimName);
				}
				else
				{
					stateName = this.m_bossFlipSmallAnimName;
				}
				component.Play(stateName);
				SoundManager.Get().LoadAndPlay(flag ? this.m_bossFlipLargeSFX : this.m_bossFlipSmallSFX);
				yield return new WaitForSeconds(flipDelayTime);
				flipDelayTime *= 0.9f;
				if (flipDelayTime < 0.1f)
				{
					flipDelayTime = 0.1f;
				}
			}
			num = i;
		}
		yield return new WaitForSeconds(this.m_delayAfterBossFlips);
		yield break;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000AB50 File Offset: 0x00008D50
	private IEnumerator PlayDefeatSequence(int numBossesToShow, int numDefeatedBosses, int numTotalBosses, int bossWhoDefeatedMeId, int heroDbId, GameSaveKeyId adventureServerKeyId, AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback completedCallback)
	{
		AdventureDungeonCrawlBossGraveyard.<>c__DisplayClass36_0 CS$<>8__locals1 = new AdventureDungeonCrawlBossGraveyard.<>c__DisplayClass36_0();
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.adventureServerKeyId = adventureServerKeyId;
		CS$<>8__locals1.completedCallback = completedCallback;
		if (this.m_bossLostToActor == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlBossGraveyard.PlayDefeatSequence() - Can't PlayDefeatSequence() without a m_bossLostToActor!", Array.Empty<object>());
			yield break;
		}
		bool bossHasEmote = this.LoadBossLostToEmote();
		if (!bossHasEmote)
		{
			Log.Adventures.Print("No EmoteDef set for DUNGEON_CRAWL_DEFEAT_TAUNT for boss {0}.", new object[]
			{
				this.m_bossLostToActor.CardDefName
			});
		}
		while (!this.m_subsceneTransitionComplete || GameUtils.IsAnyTransitionActive())
		{
			yield return null;
		}
		FullScreenFXMgr.Get().Vignette();
		if (!string.IsNullOrEmpty(this.m_defeatSequenceStartSFX))
		{
			SoundManager.Get().LoadAndPlay(this.m_defeatSequenceStartSFX);
		}
		yield return base.StartCoroutine(this.PlayBossFlippingSequence(numBossesToShow, false));
		if (bossHasEmote)
		{
			AdventureDungeonCrawlBossGraveyard.<>c__DisplayClass36_1 CS$<>8__locals2 = new AdventureDungeonCrawlBossGraveyard.<>c__DisplayClass36_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			while (!this.m_emoteLoadingComplete)
			{
				yield return null;
			}
			CS$<>8__locals2.notification = NotificationManager.Get().CreateSpeechBubble(GameStrings.Get(this.m_bossLostToEmoteDef.m_emoteGameStringKey), this.m_bossLostToActor);
			if (this.m_bossLostToEmoteSoundSpell == null)
			{
				NotificationManager.Get().DestroyNotification(CS$<>8__locals2.notification, 5f);
			}
			else
			{
				this.m_bossLostToEmoteSoundSpell.AddFinishedCallback(delegate(Spell spell, object data)
				{
					NotificationManager.Get().DestroyNotification(CS$<>8__locals2.notification, 0f);
					UnityEngine.Object.Destroy(CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_bossLostToEmoteSoundSpell.gameObject);
				});
				this.m_bossLostToEmoteSoundSpell.Reactivate();
			}
			while (CS$<>8__locals2.notification != null)
			{
				yield return null;
			}
			CS$<>8__locals2 = null;
		}
		bool flag = false;
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
		if (numDefeatedBosses >= numTotalBosses - 1)
		{
			flag = DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.FINAL_BOSS_LOSS_EVENTS, bossWhoDefeatedMeId, true);
		}
		if (!flag)
		{
			flag = DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.BOSS_LOSS_1, bossWhoDefeatedMeId, true);
		}
		FullScreenFXMgr.Get().StopVignette();
		if (this.HasNewlyUnlockedGSDRewardsToShow())
		{
			PopupDisplayManager.Get().ShowRewardsForAdventureUnlocks(this.m_justUnlockedHeroPowers, this.m_justUnlockedDecks, this.m_justUnlockedLoadoutTreasures, this.m_justUpgradedLoadoutTreasures, delegate
			{
				if (!CS$<>8__locals1.<>4__this.m_runCompleteSequenceSeen)
				{
					CS$<>8__locals1.<>4__this.MarkRunCompleteSequenceAsSeen(CS$<>8__locals1.adventureServerKeyId, CS$<>8__locals1.completedCallback);
				}
			});
		}
		yield break;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000ABA0 File Offset: 0x00008DA0
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
				flag = (flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_CLASSES_FIRST_TIME, 0, true));
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO, new long[]
				{
					1L
				}), null);
			}
			else
			{
				flag = (flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_CLASSES, 0, false));
			}
		}
		if (!flag && firstTimeCompletedAsClass && numClassesCompleted > 0)
		{
			int num = numClassesCompleted - 1;
			if (num < DungeonCrawlSubDef_VOLines.CLASS_COMPLETE_EVENTS.Length)
			{
				flag = (flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.CLASS_COMPLETE_EVENTS[num], 0, false));
			}
		}
		if (!flag)
		{
			List<long> list;
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WING_COMPLETE_VO, out list))
			{
				list = new List<long>();
			}
			bool flag2 = list.Contains((long)wingIdFromMissionId);
			int count = list.Count;
			if (!flag2 && count < DungeonCrawlSubDef_VOLines.WING_COMPLETE_EVENTS.Length)
			{
				flag = (flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.WING_COMPLETE_EVENTS[count], 0, false));
				if (flag)
				{
					list.Add((long)wingIdFromMissionId);
					GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WING_COMPLETE_VO, list.ToArray()), null);
				}
			}
		}
		if (!flag)
		{
			flag = (flag || DungeonCrawlSubDef_VOLines.PlayVOLine(selectedAdventure, wingIdFromMissionId, heroDbId, DungeonCrawlSubDef_VOLines.VOEventType.WING_COMPLETE_GENERAL, 0, false));
		}
		if (!flag)
		{
			AdventureWingDef wingDef = dungeonCrawlData.GetWingDef(wingIdFromMissionId);
			if (AdventureUtils.CanPlayWingCompleteQuote(wingDef))
			{
				string legacyAssetName = new AssetReference(wingDef.m_CompleteQuoteVOLine).GetLegacyAssetName();
				NotificationManager.Get().CreateCharacterQuote(wingDef.m_CompleteQuotePrefab, GameStrings.Get(legacyAssetName), wingDef.m_CompleteQuoteVOLine, false, 0f, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000AD3B File Offset: 0x00008F3B
	private IEnumerator PlayVictorySequence(int numBossesToShow, bool hasCompletedAdventureWithAllClasses, bool firstTimeCompletedAsClass, int numClassesCompleted, int heroDbId, AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback completedCallback)
	{
		while (!this.m_subsceneTransitionComplete || GameUtils.IsAnyTransitionActive())
		{
			yield return null;
		}
		FullScreenFXMgr.Get().Vignette();
		if (!string.IsNullOrEmpty(this.m_victorySequenceStartSFX))
		{
			SoundManager.Get().LoadAndPlay(this.m_victorySequenceStartSFX);
		}
		yield return base.StartCoroutine(this.PlayBossFlippingSequence(numBossesToShow, true));
		AdventureDbId adventureDbId = this.m_dungeonCrawlData.GetSelectedAdventure();
		AdventureModeDbId adventureModeDbId = this.m_dungeonCrawlData.GetSelectedMode();
		AdventureDef adventureDef = this.m_dungeonCrawlData.GetAdventureDef();
		AdventureSubDef adventureSubDef = (adventureDef == null) ? null : adventureDef.GetSubDef(adventureModeDbId);
		if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_BannerRewardPrefab))
		{
			TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(heroDbId);
			string text = GameStrings.FormatLocalizedString(adventureSubDef.GetCompleteBannerText(), new object[]
			{
				this.GetClassNameFromTagClass(tagClassFromCardDbId)
			});
			BannerManager.Get().ShowBanner(adventureDef.m_BannerRewardPrefab, null, text, null, null);
		}
		yield return new WaitForSeconds(this.m_delayBeforeRunWinVO);
		GameSaveKeyId gameSaveClientKey = this.m_dungeonCrawlData.GetGameSaveClientKey();
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO, out num);
		bool hasSeenCompleteWithAllClassesFirstTime = num == 1L;
		AdventureDungeonCrawlBossGraveyard.PlayVictoryVO(this.m_dungeonCrawlData, hasCompletedAdventureWithAllClasses, hasSeenCompleteWithAllClassesFirstTime, firstTimeCompletedAsClass, numClassesCompleted, heroDbId);
		FullScreenFXMgr.Get().StopVignette();
		PopupDisplayManager.Get().ShowRewardsForAdventureUnlocks(this.m_justUnlockedHeroPowers, this.m_justUnlockedDecks, this.m_justUnlockedLoadoutTreasures, this.m_justUpgradedLoadoutTreasures, delegate
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureDbId, (int)adventureModeDbId);
			this.ShowAdditionalPopupsAfterOutstandingPopups(hasCompletedAdventureWithAllClasses, hasSeenCompleteWithAllClassesFirstTime, adventureDataRecord, completedCallback);
		});
		yield break;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000AD77 File Offset: 0x00008F77
	private void MarkRunCompleteSequenceAsSeen(GameSaveKeyId adventureServerKeyId, AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback completedCallback)
	{
		this.m_runCompleteSequenceSeen = true;
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(adventureServerKeyId, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, new long[]
		{
			1L
		}), null);
		completedCallback();
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000ADA8 File Offset: 0x00008FA8
	private void ShowAdditionalPopupsAfterOutstandingPopups(bool hasCompletedAdventureWithAllClasses, bool hasSeenCompleteWithAllClassesFirstTime, AdventureDataDbfRecord adventureDataRecord, AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback completedCallback)
	{
		if (hasCompletedAdventureWithAllClasses && !hasSeenCompleteWithAllClassesFirstTime)
		{
			string prefabShownOnComplete = adventureDataRecord.PrefabShownOnComplete;
			if (!string.IsNullOrEmpty(prefabShownOnComplete))
			{
				Reward.OnHideCallback <>9__1;
				new BonusChallengeUnlockData(prefabShownOnComplete, adventureDataRecord.DungeonCrawlBossCardPrefab).LoadRewardObject(delegate(Reward reward, object data)
				{
					Reward.OnHideCallback callback;
					if ((callback = <>9__1) == null)
					{
						callback = (<>9__1 = delegate(object userData)
						{
							this.MarkRunCompleteSequenceAsSeen((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, completedCallback);
							Navigation.GoBack();
							this.m_dungeonCrawlData.SetSelectedAdventureMode((AdventureDbId)adventureDataRecord.AdventureId, AdventureModeDbId.BONUS_CHALLENGE);
						});
					}
					reward.RegisterHideListener(callback);
					this.OnBonusChallengeUnlockObjectLoaded(reward, data);
				});
				return;
			}
		}
		this.MarkRunCompleteSequenceAsSeen((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, completedCallback);
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000AE28 File Offset: 0x00009028
	private string GetClassNameFromTagClass(TAG_CLASS deckClass)
	{
		List<int> guestHeroesForCurrentAdventure = this.m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		if (guestHeroesForCurrentAdventure.Count > 0)
		{
			using (List<int>.Enumerator enumerator = guestHeroesForCurrentAdventure.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int cardDbId = enumerator.Current;
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
		}
		return GameStrings.GetClassName(deckClass);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000AED0 File Offset: 0x000090D0
	private void DisableBoss(Actor boss)
	{
		boss.transform.Rotate(new Vector3(0f, 0f, 180f));
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000AEF4 File Offset: 0x000090F4
	private bool LoadBossLostToEmote()
	{
		if (!this.m_bossLostToActor.HasCardDef || this.m_bossLostToActor.EmoteDefs == null)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlBossGraveyard.PlayDefeatSequence() - No cardDef found for the boss you lost to!", Array.Empty<object>());
			return false;
		}
		this.m_bossLostToEmoteDef = this.m_bossLostToActor.EmoteDefs.Find((EmoteEntryDef e) => e.m_emoteType == EmoteType.DUNGEON_CRAWL_DEFEAT_TAUNT);
		if (this.m_bossLostToEmoteDef == null)
		{
			return false;
		}
		EmoteEntryDef emoteEntryDef = this.m_bossLostToActor.EmoteDefs.Find((EmoteEntryDef e) => e.m_emoteType == EmoteType.DUNGEON_CRAWL_DEFEAT_TAUNT_SUPER_RARE);
		if (emoteEntryDef != null && UnityEngine.Random.Range(0f, 1f) < 0.2f)
		{
			this.m_bossLostToEmoteDef = emoteEntryDef;
		}
		AssetLoader.Get().InstantiatePrefab(this.m_bossLostToEmoteDef.m_emoteSoundSpellPath, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			this.m_emoteLoadingComplete = true;
			if (go == null)
			{
				Log.Adventures.PrintError("AdventureDungeonCrawlBossGraveyard.PlayDefeatSequence() - Failed to load CardSoundSpell at path {0}!", new object[]
				{
					this.m_bossLostToEmoteDef.m_emoteSoundSpellPath
				});
				return;
			}
			this.m_bossLostToEmoteSoundSpell = go.GetComponent<CardSoundSpell>();
		}, null, AssetLoadingOptions.None);
		return true;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000AFEC File Offset: 0x000091EC
	private void UpdateLayout()
	{
		Vector3 localPosition = this.m_bossArches[0].transform.localPosition;
		for (int i = 1; i < this.m_bossArches.Count; i++)
		{
			this.m_bossArches[i].transform.localPosition = new Vector3(localPosition.x + this.m_bossArchSpacingHorizontal * (float)(i % this.m_bossesPerRow), localPosition.y, localPosition.z + this.m_bossArchSpacingVertical * (float)(i / this.m_bossesPerRow));
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000B078 File Offset: 0x00009278
	private void CheckForNewlyUnlockedAdventureRewards(GameSaveKeyId gameSaveServerKey, GameSaveKeyId gameSaveClientKey, TAG_CLASS heroClass)
	{
		List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
		List<AdventureHeroPowerDbfRecord> heroPowersForClass = this.m_dungeonCrawlData.GetHeroPowersForClass(heroClass);
		this.m_justUnlockedHeroPowers = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(heroPowersForClass.Cast<DbfRecord>(), this.m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_HERO_POWERS, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_HERO_POWERS, list, false).Cast<AdventureHeroPowerDbfRecord>().ToList<AdventureHeroPowerDbfRecord>();
		List<AdventureDeckDbfRecord> decksForClass = this.m_dungeonCrawlData.GetDecksForClass(heroClass);
		this.m_justUnlockedDecks = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(decksForClass.Cast<DbfRecord>(), this.m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_DECKS, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_DECKS, list, false).Cast<AdventureDeckDbfRecord>().ToList<AdventureDeckDbfRecord>();
		List<AdventureLoadoutTreasuresDbfRecord> loadoutTreasuresForClass = this.m_dungeonCrawlData.GetLoadoutTreasuresForClass(heroClass);
		this.m_justUnlockedLoadoutTreasures = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(loadoutTreasuresForClass.Cast<DbfRecord>(), this.m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_LOADOUT_TREASURES, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_LOADOUT_TREASURES, list, false).Cast<AdventureLoadoutTreasuresDbfRecord>().ToList<AdventureLoadoutTreasuresDbfRecord>();
		this.m_justUpgradedLoadoutTreasures = DungeonCrawlUtil.CheckForNewlyUnlockedGSDRewardsOfSpecificType(loadoutTreasuresForClass.Cast<DbfRecord>(), this.m_dungeonCrawlData, gameSaveServerKey, gameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_LOADOUT_TREASURES, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_LOADOUT_TREASURES, list, true).Cast<AdventureLoadoutTreasuresDbfRecord>().ToList<AdventureLoadoutTreasuresDbfRecord>();
		if (list.Count > 0)
		{
			GameSaveDataManager.Get().SaveSubkeys(list, null);
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000B184 File Offset: 0x00009384
	private bool HasNewlyUnlockedGSDRewardsToShow()
	{
		return (this.m_justUnlockedHeroPowers != null && this.m_justUnlockedHeroPowers.Count > 0) || (this.m_justUnlockedDecks != null && this.m_justUnlockedDecks.Count > 0) || (this.m_justUnlockedLoadoutTreasures != null && this.m_justUnlockedLoadoutTreasures.Count > 0) || (this.m_justUpgradedLoadoutTreasures != null && this.m_justUpgradedLoadoutTreasures.Count > 0);
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000B1ED File Offset: 0x000093ED
	public void OnSubSceneTransitionComplete()
	{
		this.m_subsceneTransitionComplete = true;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000B1F8 File Offset: 0x000093F8
	public void ShowRunEnd(IDungeonCrawlData dungeonCrawlData, List<long> defeatedBossIds, long bossWhoDefeatedMeId, int numTotalBosses, bool hasCompletedAdventureWithAllClasses, bool firstTimeCompletedAsClass, int numClassesCompleted, int heroDbId, GameSaveKeyId adventureGameSaveServerKey, GameSaveKeyId adventureGameSaveClientKey, AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback loadCompletedCallback, AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback completedCallback)
	{
		if (dungeonCrawlData == null)
		{
			Log.Adventures.PrintError("Error!  AdventureDungeonCrawlBossGraveyard.ShowRunEnd() called with null dungeonCrawlData!)", Array.Empty<object>());
			return;
		}
		this.m_dungeonCrawlData = dungeonCrawlData;
		if (this.m_bossArches.Count < 1)
		{
			Log.Adventures.PrintError("Error!  AdventureDungeonCrawlBossGraveyard.ShowRunEnd() called when m_bossArches is empty! (Probably because Start() has not yet executed.)", Array.Empty<object>());
			return;
		}
		TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(heroDbId);
		this.CheckForNewlyUnlockedAdventureRewards(adventureGameSaveServerKey, adventureGameSaveClientKey, tagClassFromCardDbId);
		int num = (defeatedBossIds == null) ? 0 : defeatedBossIds.Count;
		bool flag = num < numTotalBosses;
		int num2 = Mathf.Min(8, num + (flag ? 1 : 0));
		int num3 = Mathf.Max(0, num - num2 + (flag ? 1 : 0));
		this.m_defeatedCount.Text = GameStrings.Format("GLUE_ADVENTURE_DUNGEON_CRAWL_DEFEATED_COUNT", new object[]
		{
			num,
			numTotalBosses
		});
		Actor actor = this.m_bossArches[0];
		while (this.m_bossArches.Count < 8)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(actor.gameObject);
			gameObject.transform.parent = actor.transform.parent;
			gameObject.transform.localScale = actor.transform.localScale;
			AdventureDungeonCrawlBossGraveyardActor component = gameObject.GetComponent<AdventureDungeonCrawlBossGraveyardActor>();
			this.m_bossArches.Add(component);
		}
		for (int i = 0; i < this.m_bossArches.Count; i++)
		{
			AdventureDungeonCrawlBossGraveyardActor adventureDungeonCrawlBossGraveyardActor = this.m_bossArches[i];
			adventureDungeonCrawlBossGraveyardActor.SetStyle(this.m_dungeonCrawlData);
			this.DisableBoss(adventureDungeonCrawlBossGraveyardActor);
		}
		for (int j = 0; j < num2; j++)
		{
			int num4 = j + num3;
			bool flag2 = num4 == num;
			long num5 = flag2 ? bossWhoDefeatedMeId : defeatedBossIds[num4];
			string text = GameUtils.TranslateDbIdToCardId((int)num5, false);
			if (text == null)
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlBossGraveyard.SetBossDbIds() - No cardId for boss dbId {0}, in arch number {1}!", new object[]
				{
					num5,
					j
				});
			}
			else
			{
				using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(text, null))
				{
					this.m_bossArches[j].SetFullDef(fullDef);
					this.m_bossArches[j].SetPremium(TAG_PREMIUM.NORMAL);
					this.m_bossArches[j].UpdateAllComponents();
					this.m_bossArches[j].Show();
				}
				if (flag2)
				{
					this.m_bossLostToActor = this.m_bossArches[j];
				}
				else
				{
					Flipbook componentInChildren = this.m_bossArches[j].GetComponentInChildren<Flipbook>(true);
					if (componentInChildren != null)
					{
						componentInChildren.gameObject.SetActive(true);
					}
				}
			}
		}
		this.UpdateLayout();
		loadCompletedCallback();
		base.StopAllCoroutines();
		if (flag)
		{
			if (!this.HasNewlyUnlockedGSDRewardsToShow())
			{
				this.MarkRunCompleteSequenceAsSeen(adventureGameSaveServerKey, completedCallback);
			}
			base.StartCoroutine(this.PlayDefeatSequence(num2, num, numTotalBosses, (int)bossWhoDefeatedMeId, heroDbId, adventureGameSaveServerKey, completedCallback));
			return;
		}
		base.StartCoroutine(this.PlayVictorySequence(num2, hasCompletedAdventureWithAllClasses, firstTimeCompletedAsClass, numClassesCompleted, heroDbId, completedCallback));
	}

	// Token: 0x04000152 RID: 338
	private const int MAX_GRAVEYARD_BOSSES_TO_SHOW = 8;

	// Token: 0x04000153 RID: 339
	private const float CHANCE_TO_PLAY_RARE_DEFEAT_LINE = 0.2f;

	// Token: 0x04000154 RID: 340
	[CustomEditField(Sections = "UI")]
	public NestedPrefab m_bossArchNestedPrefab;

	// Token: 0x04000155 RID: 341
	[CustomEditField(Sections = "UI")]
	public float m_bossArchSpacingHorizontal;

	// Token: 0x04000156 RID: 342
	[CustomEditField(Sections = "UI")]
	public float m_bossArchSpacingVertical;

	// Token: 0x04000157 RID: 343
	[CustomEditField(Sections = "UI")]
	public int m_bossesPerRow = 4;

	// Token: 0x04000158 RID: 344
	[CustomEditField(Sections = "UI")]
	public UberText m_defeatedCount;

	// Token: 0x04000159 RID: 345
	[CustomEditField(Sections = "Animations")]
	public string m_bossFlipSmallAnimName;

	// Token: 0x0400015A RID: 346
	[CustomEditField(Sections = "Animations")]
	public string m_bossFlipLargeAnimName;

	// Token: 0x0400015B RID: 347
	[CustomEditField(Sections = "Animations")]
	public string m_bossFlipNoDesaturateAnimName;

	// Token: 0x0400015C RID: 348
	[CustomEditField(Sections = "Animations")]
	public float m_delayPerBossFlip = 0.63f;

	// Token: 0x0400015D RID: 349
	[CustomEditField(Sections = "Animations")]
	public float m_delayAfterBossFlips = 1.5f;

	// Token: 0x0400015E RID: 350
	[CustomEditField(Sections = "Animations")]
	public float m_delayBeforeRunWinVO;

	// Token: 0x0400015F RID: 351
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_bossFlipSmallSFX;

	// Token: 0x04000160 RID: 352
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_bossFlipLargeSFX;

	// Token: 0x04000161 RID: 353
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_victorySequenceStartSFX;

	// Token: 0x04000162 RID: 354
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_defeatSequenceStartSFX;

	// Token: 0x04000163 RID: 355
	[CustomEditField(Sections = "Rewards")]
	public GameObject m_rewardPopupContainer;

	// Token: 0x04000164 RID: 356
	private List<AdventureDungeonCrawlBossGraveyardActor> m_bossArches = new List<AdventureDungeonCrawlBossGraveyardActor>();

	// Token: 0x04000165 RID: 357
	private Actor m_bossLostToActor;

	// Token: 0x04000166 RID: 358
	private bool m_subsceneTransitionComplete;

	// Token: 0x04000167 RID: 359
	private bool m_emoteLoadingComplete;

	// Token: 0x04000168 RID: 360
	private EmoteEntryDef m_bossLostToEmoteDef;

	// Token: 0x04000169 RID: 361
	private CardSoundSpell m_bossLostToEmoteSoundSpell;

	// Token: 0x0400016A RID: 362
	private bool m_runCompleteSequenceSeen;

	// Token: 0x0400016B RID: 363
	private List<AdventureHeroPowerDbfRecord> m_justUnlockedHeroPowers;

	// Token: 0x0400016C RID: 364
	private List<AdventureDeckDbfRecord> m_justUnlockedDecks;

	// Token: 0x0400016D RID: 365
	private List<AdventureLoadoutTreasuresDbfRecord> m_justUnlockedLoadoutTreasures;

	// Token: 0x0400016E RID: 366
	private List<AdventureLoadoutTreasuresDbfRecord> m_justUpgradedLoadoutTreasures;

	// Token: 0x0400016F RID: 367
	private IDungeonCrawlData m_dungeonCrawlData;

	// Token: 0x020012A0 RID: 4768
	// (Invoke) Token: 0x0600D4C9 RID: 54473
	public delegate void RunEndSequenceCompletedCallback();
}
