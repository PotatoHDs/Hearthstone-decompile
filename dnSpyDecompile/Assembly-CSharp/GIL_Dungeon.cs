using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class GIL_Dungeon : GIL_MissionEntity
{
	// Token: 0x06003761 RID: 14177 RVA: 0x001181BC File Offset: 0x001163BC
	public GIL_Dungeon()
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == 423);
		this.m_gameSaveDataClientKey = (GameSaveKeyId)record.GameSaveDataClientKey;
		GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, out this.m_hasSeenInGameLoseVO);
		GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO, out this.m_hasSeenInGameMulliganVO);
	}

	// Token: 0x06003762 RID: 14178 RVA: 0x00118248 File Offset: 0x00116448
	public override void PreloadAssets()
	{
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_DARIUS))
		{
			base.PreloadSound(GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TESS))
		{
			base.PreloadSound(GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01);
			base.PreloadSound(GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_SHAW))
		{
			base.PreloadSound(GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TOKI))
		{
			base.PreloadSound(GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01);
		}
		if (this.m_hasSeenInGameMulliganVO == 0L)
		{
			base.PreloadSound(GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin2_01);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_LOOT_IN_GAME_LOSE_VO))
		{
			base.PreloadSound(GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_Defeat1_01);
		}
	}

	// Token: 0x06003763 RID: 14179 RVA: 0x00118322 File Offset: 0x00116522
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILMulligan);
		}
	}

	// Token: 0x06003764 RID: 14180 RVA: 0x00118338 File Offset: 0x00116538
	public static GIL_Dungeon InstantiateGilDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(opposingHeroCardID);
		if (num <= 2354621193U)
		{
			if (num <= 1283254058U)
			{
				if (num <= 634021122U)
				{
					if (num <= 465112099U)
					{
						if (num != 30424196U)
						{
							if (num != 432395504U)
							{
								if (num == 465112099U)
								{
									if (opposingHeroCardID == "GILA_BOSS_68h")
									{
										return new GIL_Dungeon_Boss_68h();
									}
								}
							}
							else if (opposingHeroCardID == "GILA_BOSS_63h")
							{
								return new GIL_Dungeon_Boss_63h();
							}
						}
						else if (opposingHeroCardID == "GILA_BOSS_67h")
						{
							return new GIL_Dungeon_Boss_67h();
						}
					}
					else if (num != 466392027U)
					{
						if (num != 601304527U)
						{
							if (num == 634021122U)
							{
								if (opposingHeroCardID == "GILA_BOSS_61h")
								{
									return new GIL_Dungeon_Bonus_Boss_61h();
								}
							}
						}
						else if (opposingHeroCardID == "GILA_BOSS_64h")
						{
							return new GIL_Dungeon_Boss_64h();
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_60h")
					{
						return new GIL_Dungeon_Boss_60h();
					}
				}
				else if (num <= 1216437772U)
				{
					if (num != 668120813U)
					{
						if (num != 1081525272U)
						{
							if (num == 1216437772U)
							{
								if (opposingHeroCardID == "GILA_BOSS_30h")
								{
									return new GIL_Dungeon_Boss_30h();
								}
							}
						}
						else if (opposingHeroCardID == "GILA_BOSS_34h")
						{
							return new GIL_Dungeon_Boss_34h();
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_66h")
					{
						return new GIL_Dungeon_Boss_66h();
					}
				}
				else if (num != 1217717700U)
				{
					if (num != 1250434295U)
					{
						if (num == 1283254058U)
						{
							if (opposingHeroCardID == "GILA_BOSS_36h")
							{
								return new GIL_Dungeon_Boss_36h();
							}
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_33h")
					{
						return new GIL_Dungeon_Boss_33h();
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_38h")
				{
					return new GIL_Dungeon_Boss_38h();
				}
			}
			else if (num <= 1783637694U)
			{
				if (num <= 1716924576U)
				{
					if (num != 1652508771U)
					{
						if (num != 1686505294U)
						{
							if (num == 1716924576U)
							{
								if (opposingHeroCardID == "GILA_BOSS_56h")
								{
									return new GIL_Dungeon_Boss_56h();
								}
							}
						}
						else if (opposingHeroCardID == "GILA_BOSS_32h")
						{
							return new GIL_Dungeon_Boss_32h();
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_37h")
					{
						return new GIL_Dungeon_Boss_37h();
					}
				}
				else if (num != 1719221889U)
				{
					if (num != 1750921099U)
					{
						if (num == 1783637694U)
						{
							if (opposingHeroCardID == "GILA_BOSS_58h")
							{
								return new GIL_Dungeon_Boss_58h();
							}
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_55h")
					{
						return new GIL_Dungeon_Boss_55h();
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_35h")
				{
					return new GIL_Dungeon_Boss_35h();
				}
			}
			else if (num <= 1885833599U)
			{
				if (num != 1854134389U)
				{
					if (num != 1855414317U)
					{
						if (num == 1885833599U)
						{
							if (opposingHeroCardID == "GILA_BOSS_51h")
							{
								return new GIL_Dungeon_Boss_51h();
							}
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_39h")
					{
						return new GIL_Dungeon_Boss_39h();
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_31h")
				{
					return new GIL_Dungeon_Boss_31h();
				}
			}
			else if (num != 1918550194U)
			{
				if (num != 2321801430U)
				{
					if (num == 2354621193U)
					{
						if (opposingHeroCardID == "GILA_BOSS_57h")
						{
							return new GIL_Dungeon_Boss_57h();
						}
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_50h")
				{
					return new GIL_Dungeon_Boss_50h();
				}
			}
			else if (opposingHeroCardID == "GILA_BOSS_54h")
			{
				return new GIL_Dungeon_Boss_54h();
			}
		}
		else if (num <= 3608730983U)
		{
			if (num <= 3138663461U)
			{
				if (num <= 2936934675U)
				{
					if (num != 2388617716U)
					{
						if (num != 2421334311U)
						{
							if (num == 2936934675U)
							{
								if (opposingHeroCardID == "GILA_BOSS_24h")
								{
									return new GIL_Dungeon_Boss_24h();
								}
							}
						}
						else if (opposingHeroCardID == "GILA_BOSS_59h")
						{
							return new GIL_Dungeon_Boss_59h();
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_52h")
					{
						return new GIL_Dungeon_Boss_52h();
					}
				}
				else if (num != 2971034366U)
				{
					if (num != 3003750961U)
					{
						if (num == 3138663461U)
						{
							if (opposingHeroCardID == "GILA_BOSS_22h")
							{
								return new GIL_Dungeon_Boss_22h();
							}
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_26h")
					{
						return new GIL_Dungeon_Boss_26h();
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_21h")
				{
					return new GIL_Dungeon_Boss_21h();
				}
			}
			else if (num <= 3556462784U)
			{
				if (num != 3439821960U)
				{
					if (num != 3509198102U)
					{
						if (num == 3556462784U)
						{
							if (opposingHeroCardID == "GILA_BOSS_49h")
							{
								return new GIL_Dungeon_Boss_49h();
							}
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_29h")
					{
						return new GIL_Dungeon_Boss_29h();
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_27h")
				{
					return new GIL_Dungeon_Boss_27h();
				}
			}
			else if (num != 3574631292U)
			{
				if (num != 3589179379U)
				{
					if (num == 3608730983U)
					{
						if (opposingHeroCardID == "GILA_BOSS_20h")
						{
							return new GIL_Dungeon_Boss_20h();
						}
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_42h")
				{
					return new GIL_Dungeon_Boss_42h();
				}
			}
			else if (opposingHeroCardID == "GILA_BOSS_23h")
			{
				return new GIL_Dungeon_Boss_23h();
			}
		}
		else if (num <= 4092066664U)
		{
			if (num <= 3655995665U)
			{
				if (num != 3623279070U)
				{
					if (num != 3641447578U)
					{
						if (num == 3655995665U)
						{
							if (opposingHeroCardID == "GILA_BOSS_40h")
							{
								return new GIL_Dungeon_Boss_40h();
							}
						}
					}
					else if (opposingHeroCardID == "GILA_BOSS_25h")
					{
						return new GIL_Dungeon_Boss_25h();
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_47h")
				{
					return new GIL_Dungeon_Boss_47h();
				}
			}
			else if (num != 3790804997U)
			{
				if (num != 3862135474U)
				{
					if (num == 4092066664U)
					{
						if (opposingHeroCardID == "GILA_BOSS_41h")
						{
							return new GIL_Dungeon_Boss_41h();
						}
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_52h2")
				{
					return new GIL_Dungeon_Boss_52h();
				}
			}
			else if (opposingHeroCardID == "GILA_BOSS_44h")
			{
				return new GIL_Dungeon_Boss_44h();
			}
		}
		else if (num <= 4258575206U)
		{
			if (num != 4194159401U)
			{
				if (num != 4226875996U)
				{
					if (num == 4258575206U)
					{
						if (opposingHeroCardID == "GILA_BOSS_65h")
						{
							return new GIL_Dungeon_Boss_65h();
						}
					}
				}
				else if (opposingHeroCardID == "GILA_BOSS_45h")
				{
					return new GIL_Dungeon_Boss_45h();
				}
			}
			else if (opposingHeroCardID == "GILA_BOSS_48h")
			{
				return new GIL_Dungeon_Boss_48h();
			}
		}
		else if (num != 4260975687U)
		{
			if (num != 4291291801U)
			{
				if (num == 4293692282U)
				{
					if (opposingHeroCardID == "GILA_BOSS_43h")
					{
						return new GIL_Dungeon_Boss_43h();
					}
				}
			}
			else if (opposingHeroCardID == "GILA_BOSS_62h")
			{
				return new GIL_Dungeon_Boss_62h();
			}
		}
		else if (opposingHeroCardID == "GILA_BOSS_46h")
		{
			return new GIL_Dungeon_Boss_46h();
		}
		Log.All.PrintError("GIL_Dungeon.InstantiateGILDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new GIL_Dungeon();
	}

	// Token: 0x06003765 RID: 14181 RVA: 0x00118B64 File Offset: 0x00116D64
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

	// Token: 0x06003766 RID: 14182 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003767 RID: 14183 RVA: 0x00090064 File Offset: 0x0008E264
	protected virtual string GetBossDeathLine()
	{
		return null;
	}

	// Token: 0x06003768 RID: 14184 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003769 RID: 14185 RVA: 0x00112BA9 File Offset: 0x00110DA9
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 0.5f;
	}

	// Token: 0x0600376A RID: 14186 RVA: 0x00112BA9 File Offset: 0x00110DA9
	protected override float ChanceToPlayRandomVOLine()
	{
		return 0.5f;
	}

	// Token: 0x0600376B RID: 14187 RVA: 0x00118B9C File Offset: 0x00116D9C
	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
		{
			return;
		}
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

	// Token: 0x0600376C RID: 14188 RVA: 0x00118C70 File Offset: 0x00116E70
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

	// Token: 0x0600376D RID: 14189 RVA: 0x00118C86 File Offset: 0x00116E86
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1 && GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null && GameState.Get().GetFriendlySidePlayer().GetHero() != null)
		{
			bool hasPlayedLineThisGame = false;
			string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
			if (!(cardId == "GILA_500h3"))
			{
				if (!(cardId == "GILA_600h"))
				{
					if (!(cardId == "GILA_400h"))
					{
						if (cardId == "GILA_900h")
						{
							if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TOKI))
							{
								Options.Get().SetBool(Option.HAS_SEEN_PLAYED_TOKI, true);
								hasPlayedLineThisGame = true;
								yield return base.PlayBossLine(GIL_Dungeon.m_GennGreymane_BigQuote, GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01, 2.5f);
							}
						}
					}
					else if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_SHAW))
					{
						Options.Get().SetBool(Option.HAS_SEEN_PLAYED_SHAW, true);
						hasPlayedLineThisGame = true;
						yield return base.PlayBossLine(GIL_Dungeon.m_GennGreymane_BigQuote, GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01, 2.5f);
					}
				}
				else if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_DARIUS))
				{
					Options.Get().SetBool(Option.HAS_SEEN_PLAYED_DARIUS, true);
					hasPlayedLineThisGame = true;
					yield return base.PlayBossLine(GIL_Dungeon.m_GennGreymane_BigQuote, GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01, 2.5f);
				}
			}
			else if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TESS))
			{
				Options.Get().SetBool(Option.HAS_SEEN_PLAYED_TESS, true);
				hasPlayedLineThisGame = true;
				yield return base.PlayBossLine(GIL_Dungeon.m_GennGreymane_BigQuote, GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01, 2.5f);
				yield return base.PlayBossLine(GIL_Dungeon.m_GennGreymane_BigQuote, GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02, 2.5f);
			}
			if (this.m_hasSeenInGameMulliganVO == 0L && !hasPlayedLineThisGame)
			{
				this.m_inGameSubkeysToSave.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO);
				yield return base.PlayBossLine(GIL_Dungeon.m_GennGreymane_BigQuote, GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_FightBegin2_01, 2.5f);
				yield break;
			}
		}
		yield break;
	}

	// Token: 0x0600376E RID: 14190 RVA: 0x00118C9C File Offset: 0x00116E9C
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
		if (gameResult == TAG_PLAYSTATE.LOST && this.m_hasSeenInGameLoseVO == 0L)
		{
			yield return new WaitForSeconds(5f);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(this.m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, new long[]
			{
				1L
			}), null);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait(GIL_Dungeon.m_GennGreymane_Quote, GIL_Dungeon.VO_GIL_692_Male_Worgen_TUT_Defeat1_01, 0f, true, false));
			yield break;
		}
		yield break;
	}

	// Token: 0x0600376F RID: 14191 RVA: 0x00118CB4 File Offset: 0x00116EB4
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string bossDeathLine = this.GetBossDeathLine();
		if ((!this.m_enemySpeaking || string.IsNullOrEmpty(bossDeathLine)) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (this.GetShouldSupressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x06003770 RID: 14192 RVA: 0x00118D4C File Offset: 0x00116F4C
	public override void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
		base.NotifyOfResetGameFinished(source, oldGameEntity);
		GIL_Dungeon gil_Dungeon = oldGameEntity as GIL_Dungeon;
		if (gil_Dungeon != null)
		{
			this.m_inGameSubkeysToSave = new List<GameSaveKeySubkeyId>(gil_Dungeon.m_inGameSubkeysToSave);
		}
	}

	// Token: 0x04001D69 RID: 7529
	private static readonly string m_GennGreymane_BigQuote = "Greymane_BrassRing_Quote.prefab:3e16b31a3b009ad468fa76462c5eda3b";

	// Token: 0x04001D6A RID: 7530
	private static readonly string m_GennGreymane_Quote = "Greymane_Banner_Quote.prefab:cee4fc7a3f6bdd1439db34d534f85d5c";

	// Token: 0x04001D6B RID: 7531
	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01.prefab:3352fbe060719c646a4b143495a2d04e");

	// Token: 0x04001D6C RID: 7532
	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01.prefab:7f2afa4db1da44549b15a22e58b3d1c0");

	// Token: 0x04001D6D RID: 7533
	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01.prefab:ce366f4649081ed42aec86a6291f14b4");

	// Token: 0x04001D6E RID: 7534
	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02.prefab:81cc5d4027d42c04aa6e95ebca7d858a");

	// Token: 0x04001D6F RID: 7535
	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01.prefab:7514dbda27d99a0418a8f764d0c07d26");

	// Token: 0x04001D70 RID: 7536
	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin2_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin2_01.prefab:1f5d9fa8502dfdc46ad78744f2f9ea57");

	// Token: 0x04001D71 RID: 7537
	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_Defeat1_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_Defeat1_01.prefab:6e4828354338f134fa115aff3c02fb85");

	// Token: 0x04001D72 RID: 7538
	private const AdventureDbId AdventureId = AdventureDbId.GIL;

	// Token: 0x04001D73 RID: 7539
	private GameSaveKeyId m_gameSaveDataClientKey;

	// Token: 0x04001D74 RID: 7540
	private long m_hasSeenInGameLoseVO;

	// Token: 0x04001D75 RID: 7541
	private long m_hasSeenInGameMulliganVO;

	// Token: 0x04001D76 RID: 7542
	private List<GameSaveKeySubkeyId> m_inGameSubkeysToSave = new List<GameSaveKeySubkeyId>();
}
