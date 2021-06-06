using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class LOOT_Dungeon : LOOT_MissionEntity
{
	// Token: 0x060035CE RID: 13774 RVA: 0x00112154 File Offset: 0x00110354
	public LOOT_Dungeon()
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == 414);
		this.m_gameSaveDataClientKey = (GameSaveKeyId)record.GameSaveDataClientKey;
		GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO, out this.m_hasSeenInGameWinVO);
		GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, out this.m_hasSeenInGameLoseVO);
		GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO, out this.m_hasSeenInGameLose2VO);
		GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO, out this.m_hasSeenInGameMulliganVO);
		GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO, out this.m_hasSeenInGameMulligan2VO);
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x00112234 File Offset: 0x00110434
	public override void PreloadAssets()
	{
		base.PreloadSound(LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01);
		base.PreloadSound(LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01);
		base.PreloadSound(LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01);
		base.PreloadSound(LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01);
		base.PreloadSound(LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01);
		base.PreloadSound(LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01);
		base.PreloadSound(LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01);
		base.PreloadSound(LOOT_Dungeon.VO_LOOTA_829_Male_Human_Event_01);
	}

	// Token: 0x060035D0 RID: 13776 RVA: 0x001122C1 File Offset: 0x001104C1
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTMulligan);
		}
	}

	// Token: 0x060035D1 RID: 13777 RVA: 0x001122D8 File Offset: 0x001104D8
	public static LOOT_Dungeon InstantiateLootDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(opposingHeroCardID);
		if (num <= 1937970604U)
		{
			if (num <= 558520244U)
			{
				if (num <= 121169317U)
				{
					if (num <= 49844606U)
					{
						if (num != 15744915U)
						{
							if (num != 22088348U)
							{
								if (num == 49844606U)
								{
									if (opposingHeroCardID == "LOOTA_BOSS_36h")
									{
										return new LOOT_Dungeon_BOSS_36h();
									}
								}
							}
							else if (opposingHeroCardID == "LOOTA_BOSS_12h")
							{
								return new LOOT_Dungeon_BOSS_12h();
							}
						}
						else if (opposingHeroCardID == "LOOTA_BOSS_33h")
						{
							return new LOOT_Dungeon_BOSS_33h();
						}
					}
					else if (num != 56084871U)
					{
						if (num != 82561201U)
						{
							if (num == 121169317U)
							{
								if (opposingHeroCardID == "LOOTA_BOSS_40h")
								{
									return new LOOT_Dungeon_BOSS_40h();
								}
							}
						}
						else if (opposingHeroCardID == "LOOTA_BOSS_31h")
						{
							return new LOOT_Dungeon_BOSS_12h();
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_11h")
					{
						return new LOOT_Dungeon_BOSS_11h();
					}
				}
				else if (num <= 422327816U)
				{
					if (num != 122449245U)
					{
						if (num != 217473701U)
						{
							if (num == 422327816U)
							{
								if (opposingHeroCardID == "LOOTA_BOSS_45h")
								{
									return new LOOT_Dungeon_BOSS_45h();
								}
							}
						}
						else if (opposingHeroCardID == "LOOTA_BOSS_35h")
						{
							return new LOOT_Dungeon_BOSS_35h();
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_48h")
					{
						return new LOOT_Dungeon_BOSS_48h();
					}
				}
				else if (num != 518632200U)
				{
					if (num != 557137148U)
					{
						if (num == 558520244U)
						{
							if (opposingHeroCardID == "LOOTA_BOSS_49h")
							{
								return new LOOT_Dungeon_BOSS_49h();
							}
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_41h")
					{
						return new LOOT_Dungeon_BOSS_41h();
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_30h")
				{
					return new LOOT_Dungeon_BOSS_11h();
				}
			}
			else if (num <= 720257818U)
			{
				if (num <= 623953434U)
				{
					if (num != 591236839U)
					{
						if (num != 620724937U)
						{
							if (num == 623953434U)
							{
								if (opposingHeroCardID == "LOOTA_BOSS_47h")
								{
									return new LOOT_Dungeon_BOSS_47h();
								}
							}
						}
						else if (opposingHeroCardID == "LOOTA_BOSS_39h")
						{
							return new LOOT_Dungeon_BOSS_39h();
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_42h")
					{
						return new LOOT_Dungeon_BOSS_42h();
					}
				}
				else if (num != 653441532U)
				{
					if (num != 687541223U)
					{
						if (num == 720257818U)
						{
							if (opposingHeroCardID == "LOOTA_BOSS_32h")
							{
								return new LOOT_Dungeon_BOSS_15h();
							}
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_37h")
					{
						return new LOOT_Dungeon_BOSS_37h();
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_34h")
				{
					return new LOOT_Dungeon_BOSS_34h();
				}
			}
			else if (num <= 1307897348U)
			{
				if (num != 1171704920U)
				{
					if (num != 1244309559U)
					{
						if (num == 1307897348U)
						{
							if (opposingHeroCardID == "LOOTA_BOSS_05h")
							{
								return new LOOT_Dungeon_BOSS_05h();
							}
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_99h")
					{
						return new LOOT_Dungeon_BOSS_99h();
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_09h")
				{
					return new LOOT_Dungeon_BOSS_09h();
				}
			}
			else if (num <= 1803161272U)
			{
				if (num != 1637949513U)
				{
					if (num == 1803161272U)
					{
						if (opposingHeroCardID == "LOOTA_BOSS_23h")
						{
							return new LOOT_Dungeon_BOSS_23h();
						}
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_53h2")
				{
					return new LOOT_Dungeon_BOSS_53h();
				}
			}
			else if (num != 1878777679U)
			{
				if (num == 1937970604U)
				{
					if (opposingHeroCardID == "LOOTA_BOSS_27h")
					{
						return new LOOT_Dungeon_BOSS_04h();
					}
				}
			}
			else if (opposingHeroCardID == "LOOTA_BOSS_06h")
			{
				return new LOOT_Dungeon_BOSS_06h();
			}
		}
		else if (num <= 2929981923U)
		{
			if (num <= 2375321531U)
			{
				if (num <= 2004786890U)
				{
					if (num != 1945593965U)
					{
						if (num != 1972070295U)
						{
							if (num == 2004786890U)
							{
								if (opposingHeroCardID == "LOOTA_BOSS_21h")
								{
									return new LOOT_Dungeon_BOSS_21h();
								}
							}
						}
						else if (opposingHeroCardID == "LOOTA_BOSS_24h")
						{
							return new LOOT_Dungeon_BOSS_24h();
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_04h")
					{
						return new LOOT_Dungeon_BOSS_04h();
					}
				}
				else if (num != 2358998424U)
				{
					if (num != 2374041603U)
					{
						if (num == 2375321531U)
						{
							if (opposingHeroCardID == "LOOTA_BOSS_28h")
							{
								return new LOOT_Dungeon_BOSS_05h();
							}
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_20h")
					{
						return new LOOT_Dungeon_BOSS_20h();
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_52h")
				{
					return new LOOT_Dungeon_BOSS_52h();
				}
			}
			else if (num <= 2542950626U)
			{
				if (num != 2408038126U)
				{
					if (num != 2440857889U)
					{
						if (num == 2542950626U)
						{
							if (opposingHeroCardID == "LOOTA_BOSS_29h")
							{
								return new LOOT_Dungeon_BOSS_06h();
							}
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_22h")
					{
						return new LOOT_Dungeon_BOSS_22h();
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_25h")
				{
					return new LOOT_Dungeon_BOSS_25h();
				}
			}
			else if (num != 2560727210U)
			{
				if (num != 2575667221U)
				{
					if (num == 2929981923U)
					{
						if (opposingHeroCardID == "LOOTA_BOSS_51h")
						{
							return new LOOT_Dungeon_BOSS_51h();
						}
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_26h")
				{
					return new LOOT_Dungeon_BOSS_26h();
				}
			}
			else if (opposingHeroCardID == "LOOTA_BOSS_50h")
			{
				return new LOOT_Dungeon_BOSS_50h();
			}
		}
		else if (num <= 3815448287U)
		{
			if (num <= 3679359027U)
			{
				if (num != 2963978446U)
				{
					if (num != 2996695041U)
					{
						if (num == 3679359027U)
						{
							if (opposingHeroCardID == "LOOTA_BOSS_15h")
							{
								return new LOOT_Dungeon_BOSS_15h();
							}
						}
					}
					else if (opposingHeroCardID == "LOOTA_BOSS_53h")
					{
						return new LOOT_Dungeon_BOSS_53h();
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_54h")
				{
					return new LOOT_Dungeon_BOSS_54h();
				}
			}
			else if (num != 3713355550U)
			{
				if (num != 3746072145U)
				{
					if (num == 3815448287U)
					{
						if (opposingHeroCardID == "LOOTA_BOSS_19h")
						{
							return new LOOT_Dungeon_BOSS_19h();
						}
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_17h")
				{
					return new LOOT_Dungeon_BOSS_17h();
				}
			}
			else if (opposingHeroCardID == "LOOTA_BOSS_10h")
			{
				return new LOOT_Dungeon_BOSS_10h();
			}
		}
		else if (num <= 4214407827U)
		{
			if (num != 3880984645U)
			{
				if (num != 4182143144U)
				{
					if (num == 4214407827U)
					{
						if (opposingHeroCardID == "LOOTA_BOSS_46h")
						{
							return new LOOT_Dungeon_BOSS_46h();
						}
					}
				}
				else if (opposingHeroCardID == "LOOTA_BOSS_16h")
				{
					return new LOOT_Dungeon_BOSS_16h();
				}
			}
			else if (opposingHeroCardID == "LOOTA_BOSS_13h")
			{
				return new LOOT_Dungeon_BOSS_13h();
			}
		}
		else if (num <= 4251519286U)
		{
			if (num != 4248507518U)
			{
				if (num == 4251519286U)
				{
					if (opposingHeroCardID == "LOOTA_BOSS_18h")
					{
						return new LOOT_Dungeon_BOSS_18h();
					}
				}
			}
			else if (opposingHeroCardID == "LOOTA_BOSS_43h")
			{
				return new LOOT_Dungeon_BOSS_43h();
			}
		}
		else if (num != 4277995616U)
		{
			if (num == 4281224113U)
			{
				if (opposingHeroCardID == "LOOTA_BOSS_44h")
				{
					return new LOOT_Dungeon_BOSS_44h();
				}
			}
		}
		else if (opposingHeroCardID == "LOOTA_BOSS_38h")
		{
			return new LOOT_Dungeon_BOSS_38h();
		}
		Log.All.PrintError("LOOT_Dungeon.InstantiateLootDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new LOOT_Dungeon();
	}

	// Token: 0x060035D2 RID: 13778 RVA: 0x00112B6C File Offset: 0x00110D6C
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		currentPlayer.GetHeroCard().HasActiveEmoteSound();
	}

	// Token: 0x060035D3 RID: 13779 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x00090064 File Offset: 0x0008E264
	protected virtual string GetBossDeathLine()
	{
		return null;
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x060035D6 RID: 13782 RVA: 0x00112BA9 File Offset: 0x00110DA9
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 0.5f;
	}

	// Token: 0x060035D7 RID: 13783 RVA: 0x00112BB0 File Offset: 0x00110DB0
	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> bossHeroPowerRandomLines = this.GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (text == "")
		{
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x060035D8 RID: 13784 RVA: 0x00112C72 File Offset: 0x00110E72
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		if (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield break;
		}
		if (entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x060035D9 RID: 13785 RVA: 0x00112C88 File Offset: 0x00110E88
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1)
		{
			if (this.m_hasSeenInGameMulliganVO == 0L && this.m_hasSeenInGameMulligan2VO == 0L)
			{
				this.m_inGameSubkeysToSave.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO);
				yield return base.PlayBigCharacterQuoteAndWait(LOOT_Dungeon.m_KingTogwaggle_BigQuote, LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01, 3f, 1f, true, false);
			}
			else if (this.m_hasSeenInGameMulliganVO > 0L && this.m_hasSeenInGameMulligan2VO == 0L)
			{
				this.m_inGameSubkeysToSave.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO);
				yield return base.PlayBigCharacterQuoteAndWait(LOOT_Dungeon.m_KingTogwaggle_BigQuote, LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01, 3f, 1f, true, false);
				yield return base.PlayBigCharacterQuoteAndWait(LOOT_Dungeon.m_KingTogwaggle_BigQuote, LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01, 3f, 1f, true, false);
			}
		}
		yield break;
	}

	// Token: 0x060035DA RID: 13786 RVA: 0x00112C9E File Offset: 0x00110E9E
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
		foreach (GameSaveKeySubkeyId subkey in this.m_inGameSubkeysToSave)
		{
			list.Add(new GameSaveDataManager.SubkeySaveRequest(this.m_gameSaveDataClientKey, subkey, new long[]
			{
				1L
			}));
		}
		if (list.Count > 0)
		{
			GameSaveDataManager.Get().SaveSubkeys(list, null);
		}
		if (gameResult == TAG_PLAYSTATE.WON && this.m_hasSeenInGameWinVO == 0L)
		{
			yield return new WaitForSeconds(5f);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO, new long[]
			{
				1L
			}), null);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait(LOOT_Dungeon.m_KingTogwaggle_Quote, LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01, 0f, true, false));
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			if (this.m_hasSeenInGameLoseVO == 0L && this.m_hasSeenInGameLose2VO == 0L)
			{
				yield return new WaitForSeconds(5f);
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, new long[]
				{
					1L
				}), null);
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait(LOOT_Dungeon.m_KingTogwaggle_Quote, LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01, 0f, true, false));
				yield break;
			}
			if (this.m_hasSeenInGameLoseVO > 0L && this.m_hasSeenInGameLose2VO == 0L)
			{
				yield return new WaitForSeconds(5f);
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO, new long[]
				{
					1L
				}), null);
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait(LOOT_Dungeon.m_KingTogwaggle_Quote, LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01, 0f, true, false));
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait(LOOT_Dungeon.m_KingTogwaggle_Quote, LOOT_Dungeon.VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01, 0f, true, false));
				yield break;
			}
		}
		yield break;
	}

	// Token: 0x060035DB RID: 13787 RVA: 0x00112CB4 File Offset: 0x00110EB4
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string bossDeathLine = this.GetBossDeathLine();
		if ((!this.m_enemySpeaking || string.IsNullOrEmpty(bossDeathLine)) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (bossDeathLine == "VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01.prefab:e5c8b619095374542bac028ed3654007")
			{
				base.PlaySound("RussellTheBard_Death_Underlay.prefab:8d76a143441379e40a36cb5b7c84b9b9", 1f, true, false);
			}
			if (this.GetShouldSupressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x060035DC RID: 13788 RVA: 0x00112D6C File Offset: 0x00110F6C
	private Actor GetEnemyLoyalSidekickActor()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) == 1000 && entity.GetCardId() == "LOOTA_829")
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x060035DD RID: 13789 RVA: 0x00112DFC File Offset: 0x00110FFC
	public IEnumerator PlayLoyalSideKickBetrayal(int missionEvent)
	{
		if (missionEvent == 1000)
		{
			Actor loyalSideKick = this.GetEnemyLoyalSidekickActor();
			yield return base.WaitForEntitySoundToFinish(loyalSideKick.GetEntity());
			yield return base.PlayLineOnlyOnce(loyalSideKick, LOOT_Dungeon.VO_LOOTA_829_Male_Human_Event_01, 2.5f);
			loyalSideKick = null;
		}
		yield break;
	}

	// Token: 0x04001D0C RID: 7436
	private static readonly string m_KingTogwaggle_BigQuote = "KingTogwaggle_BigQuote.prefab:9416c71ab37ae184b8c4bfaaf3233882";

	// Token: 0x04001D0D RID: 7437
	private static readonly string m_KingTogwaggle_Quote = "KingTogwaggle_Quote.prefab:b20f7b1314c0a2d46a9de0e48e7ae6f5";

	// Token: 0x04001D0E RID: 7438
	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01.prefab:4eb8b422c051369409d10b42a777ee1d");

	// Token: 0x04001D0F RID: 7439
	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01.prefab:c16731bb4687d154a9a22c8e01eeabb2");

	// Token: 0x04001D10 RID: 7440
	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01.prefab:bf40d91ec399f7e4c88b3a4c8c71bda5");

	// Token: 0x04001D11 RID: 7441
	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01.prefab:7fcfd8f32efb5374aae312892eac84ff");

	// Token: 0x04001D12 RID: 7442
	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01.prefab:90b80890ad0f972478294383cc02e233");

	// Token: 0x04001D13 RID: 7443
	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01.prefab:9bb4ec22f68d90342a84fba2f3d7a100");

	// Token: 0x04001D14 RID: 7444
	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01.prefab:6dfdf8edb59d4c14380b38e874769662");

	// Token: 0x04001D15 RID: 7445
	private static readonly AssetReference VO_LOOTA_829_Male_Human_Event_01 = new AssetReference("VO_LOOTA_829_Male_Human_Event_01.prefab:beb8a7cd19bc24f46a617b0c1774da48");

	// Token: 0x04001D16 RID: 7446
	private const AdventureDbId AdventureId = AdventureDbId.LOOT;

	// Token: 0x04001D17 RID: 7447
	private GameSaveKeyId m_gameSaveDataClientKey;

	// Token: 0x04001D18 RID: 7448
	private long m_hasSeenInGameWinVO;

	// Token: 0x04001D19 RID: 7449
	private long m_hasSeenInGameLoseVO;

	// Token: 0x04001D1A RID: 7450
	private long m_hasSeenInGameLose2VO;

	// Token: 0x04001D1B RID: 7451
	private long m_hasSeenInGameMulliganVO;

	// Token: 0x04001D1C RID: 7452
	private long m_hasSeenInGameMulligan2VO;

	// Token: 0x04001D1D RID: 7453
	private List<GameSaveKeySubkeyId> m_inGameSubkeysToSave = new List<GameSaveKeySubkeyId>();
}
