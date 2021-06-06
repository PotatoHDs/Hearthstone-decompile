using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000452 RID: 1106
public class DALA_Dungeon_Boss_37h : DALA_Dungeon
{
	// Token: 0x06003C17 RID: 15383 RVA: 0x00138F78 File Offset: 0x00137178
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_BossElise_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Death_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_DefeatPlayer_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Idle_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Idle_02,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Idle_03,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Intro_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_TurnStart_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C18 RID: 15384 RVA: 0x0013914C File Offset: 0x0013734C
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02,
			DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03
		};
	}

	// Token: 0x06003C19 RID: 15385 RVA: 0x00139183 File Offset: 0x00137383
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01;
	}

	// Token: 0x06003C1A RID: 15386 RVA: 0x001391BB File Offset: 0x001373BB
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_37h.m_IdleLines;
	}

	// Token: 0x06003C1B RID: 15387 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003C1C RID: 15388 RVA: 0x001391C4 File Offset: 0x001373C4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Rakanishu" && cardId != "DALA_Tekahn" && cardId != "DALA_Squeamlish" && cardId != "DALA_Eudora" && cardId != "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003C1D RID: 15389 RVA: 0x001392A3 File Offset: 0x001374A3
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_37h.m_HeroPowerDraw);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_TurnStart_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C1E RID: 15390 RVA: 0x001392B9 File Offset: 0x001374B9
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 615862462U)
		{
			if (num <= 531974367U)
			{
				if (num != 515196748U)
				{
					if (num != 531827272U)
					{
						if (num != 531974367U)
						{
							goto IL_471;
						}
						if (!(cardId == "ICC_828"))
						{
							goto IL_471;
						}
					}
					else if (!(cardId == "ICC_832"))
					{
						goto IL_471;
					}
				}
				else if (!(cardId == "ICC_829"))
				{
					goto IL_471;
				}
			}
			else if (num <= 565382510U)
			{
				if (num != 548604891U)
				{
					if (num != 565382510U)
					{
						goto IL_471;
					}
					if (!(cardId == "ICC_830"))
					{
						goto IL_471;
					}
				}
				else if (!(cardId == "ICC_833"))
				{
					goto IL_471;
				}
			}
			else if (num != 582160129U)
			{
				if (num != 615862462U)
				{
					goto IL_471;
				}
				if (!(cardId == "ICC_827"))
				{
					goto IL_471;
				}
			}
			else if (!(cardId == "ICC_831"))
			{
				goto IL_471;
			}
		}
		else if (num <= 1548918472U)
		{
			if (num != 632492986U)
			{
				if (num != 1414697520U)
				{
					if (num != 1548918472U)
					{
						goto IL_471;
					}
					if (!(cardId == "DALA_701"))
					{
						goto IL_471;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01, 2.5f);
					goto IL_471;
				}
				else
				{
					if (!(cardId == "DALA_709"))
					{
						goto IL_471;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01, 2.5f);
					goto IL_471;
				}
			}
			else if (!(cardId == "ICC_834"))
			{
				goto IL_471;
			}
		}
		else if (num <= 2577025539U)
		{
			if (num != 1999498950U)
			{
				if (num != 2577025539U)
				{
					goto IL_471;
				}
				if (!(cardId == "DS1_184"))
				{
					goto IL_471;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01, 2.5f);
				goto IL_471;
			}
			else if (!(cardId == "ICC_481"))
			{
				goto IL_471;
			}
		}
		else if (num != 2797504017U)
		{
			if (num != 3500228673U)
			{
				goto IL_471;
			}
			if (!(cardId == "LOOT_357"))
			{
				goto IL_471;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01, 2.5f);
			goto IL_471;
		}
		else
		{
			if (!(cardId == "LOOT_542"))
			{
				goto IL_471;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01, 2.5f);
			goto IL_471;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01, 2.5f);
		IL_471:
		yield break;
	}

	// Token: 0x06003C1F RID: 15391 RVA: 0x001392CF File Offset: 0x001374CF
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "LOE_079")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_BossElise_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400255F RID: 9567
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_BossElise_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_BossElise_01.prefab:5c468a8ec467bb542aceffda168ace08");

	// Token: 0x04002560 RID: 9568
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Death_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Death_01.prefab:371ee3d52a8568e4e9a70a857a735297");

	// Token: 0x04002561 RID: 9569
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_DefeatPlayer_01.prefab:73c96cf139eef5b4a8e8286a538ca678");

	// Token: 0x04002562 RID: 9570
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01.prefab:ae98256975eeaf94ebc2aad3893748a3");

	// Token: 0x04002563 RID: 9571
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01.prefab:969447fd1a358b845b45ff7e2e9efb3e");

	// Token: 0x04002564 RID: 9572
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02.prefab:fb9ae5ff67d1c73408558da24ca27d03");

	// Token: 0x04002565 RID: 9573
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03.prefab:f43fd61454f5e6f43a977a9001ff913a");

	// Token: 0x04002566 RID: 9574
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01.prefab:f6296e9981f5dbd43b9b0ee8aabf079d");

	// Token: 0x04002567 RID: 9575
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02.prefab:39d1b61811283a545ae5ef749acbd2f5");

	// Token: 0x04002568 RID: 9576
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03.prefab:04597d44342c0694ba0afd7a147b1973");

	// Token: 0x04002569 RID: 9577
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Idle_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Idle_01.prefab:46fbf502dcdfb8940893ee6d7e3390a0");

	// Token: 0x0400256A RID: 9578
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Idle_02 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Idle_02.prefab:a0409eb808894a8418675f56a1437857");

	// Token: 0x0400256B RID: 9579
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Idle_03 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Idle_03.prefab:9425a99cdaadc75459271211254bdedb");

	// Token: 0x0400256C RID: 9580
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Intro_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Intro_01.prefab:f3c1ebebc05c02b468dece4cf314197d");

	// Token: 0x0400256D RID: 9581
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01.prefab:e9638b80b0cea714ab3810c8ddbd2bda");

	// Token: 0x0400256E RID: 9582
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01.prefab:899736e4a366c1b4899d544e29f9f76c");

	// Token: 0x0400256F RID: 9583
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01.prefab:68519ad2124ec564e89962461b2d623a");

	// Token: 0x04002570 RID: 9584
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01.prefab:f9730a6e7130b1e4c81514847449c3f5");

	// Token: 0x04002571 RID: 9585
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01.prefab:fc19f07efbc879c4cbe732ab8e604388");

	// Token: 0x04002572 RID: 9586
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01.prefab:e6723d3ee8c3a3e43bd683be75993b97");

	// Token: 0x04002573 RID: 9587
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01.prefab:159ca9bd10be79d4faf6550a92815f90");

	// Token: 0x04002574 RID: 9588
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01.prefab:6dfc72f20d5a7444e8d03a27d1b30463");

	// Token: 0x04002575 RID: 9589
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_TurnStart_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_TurnStart_01.prefab:fcc8e955884693746b28a6e14302a492");

	// Token: 0x04002576 RID: 9590
	private static List<string> m_HeroPowerDraw = new List<string>
	{
		DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01,
		DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02,
		DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03
	};

	// Token: 0x04002577 RID: 9591
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Idle_01,
		DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Idle_02,
		DALA_Dungeon_Boss_37h.VO_DALA_BOSS_37h_Male_Troll_Idle_03
	};

	// Token: 0x04002578 RID: 9592
	private HashSet<string> m_playedLines = new HashSet<string>();
}
