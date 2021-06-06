using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200044C RID: 1100
public class DALA_Dungeon_Boss_31h : DALA_Dungeon
{
	// Token: 0x06003BCA RID: 15306 RVA: 0x00136F68 File Offset: 0x00135168
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_BossBlessingOfKings_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Death_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Idle_02,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Intro_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01,
			DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BCB RID: 15307 RVA: 0x0013713C File Offset: 0x0013533C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01;
	}

	// Token: 0x06003BCC RID: 15308 RVA: 0x00137174 File Offset: 0x00135374
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_31h.m_IdleLines;
	}

	// Token: 0x06003BCD RID: 15309 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003BCE RID: 15310 RVA: 0x0013717C File Offset: 0x0013537C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06003BCF RID: 15311 RVA: 0x00137263 File Offset: 0x00135463
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_31h.m_HeroPower);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_31h.m_HeroPowerGolden);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_TurnOne_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003BD0 RID: 15312 RVA: 0x00137279 File Offset: 0x00135479
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
		if (num <= 2146586002U)
		{
			if (num <= 1114593127U)
			{
				if (num <= 300353834U)
				{
					if (num != 222912296U)
					{
						if (num != 273245153U)
						{
							if (num != 300353834U)
							{
								goto IL_66F;
							}
							if (!(cardId == "LOOT_203t3"))
							{
								goto IL_66F;
							}
						}
						else if (!(cardId == "LOOT_064t1"))
						{
							goto IL_66F;
						}
					}
					else if (!(cardId == "LOOT_064t2"))
					{
						goto IL_66F;
					}
				}
				else if (num <= 331234044U)
				{
					if (num != 317131453U)
					{
						if (num != 331234044U)
						{
							goto IL_66F;
						}
						if (!(cardId == "LOOT_507t2"))
						{
							goto IL_66F;
						}
					}
					else if (!(cardId == "LOOT_203t2"))
					{
						goto IL_66F;
					}
				}
				else if (num != 413356294U)
				{
					if (num != 1114593127U)
					{
						goto IL_66F;
					}
					if (!(cardId == "GVG_119"))
					{
						goto IL_66F;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01, 2.5f);
					goto IL_66F;
				}
				else
				{
					if (!(cardId == "EX1_095"))
					{
						goto IL_66F;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01, 2.5f);
					goto IL_66F;
				}
			}
			else if (num <= 1926358384U)
			{
				if (num <= 1458408869U)
				{
					if (num != 1408076012U)
					{
						if (num != 1458408869U)
						{
							goto IL_66F;
						}
						if (!(cardId == "LOOT_091t1"))
						{
							goto IL_66F;
						}
					}
					else if (!(cardId == "LOOT_091t2"))
					{
						goto IL_66F;
					}
				}
				else if (num != 1652393270U)
				{
					if (num != 1926358384U)
					{
						goto IL_66F;
					}
					if (!(cardId == "LOOT_080"))
					{
						goto IL_66F;
					}
				}
				else if (!(cardId == "LOOT_103"))
				{
					goto IL_66F;
				}
			}
			else if (num <= 2012389634U)
			{
				if (num != 2010393574U)
				{
					if (num != 2012389634U)
					{
						goto IL_66F;
					}
					if (!(cardId == "BOT_236"))
					{
						goto IL_66F;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01, 2.5f);
					goto IL_66F;
				}
				else if (!(cardId == "LOOT_091"))
				{
					goto IL_66F;
				}
			}
			else if (num != 2112883669U)
			{
				if (num != 2146586002U)
				{
					goto IL_66F;
				}
				if (!(cardId == "LOOT_051"))
				{
					goto IL_66F;
				}
			}
			else if (!(cardId == "LOOT_043"))
			{
				goto IL_66F;
			}
		}
		else if (num <= 3024299757U)
		{
			if (num <= 2574464002U)
			{
				if (num != 2331286906U)
				{
					if (num != 2573772454U)
					{
						if (num != 2574464002U)
						{
							goto IL_66F;
						}
						if (!(cardId == "LOOT_503t"))
						{
							goto IL_66F;
						}
					}
					else if (!(cardId == "LOOT_507t"))
					{
						goto IL_66F;
					}
				}
				else if (!(cardId == "LOOT_064"))
				{
					goto IL_66F;
				}
			}
			else if (num <= 2647196994U)
			{
				if (num != 2580086518U)
				{
					if (num != 2647196994U)
					{
						goto IL_66F;
					}
					if (!(cardId == "LOOT_503"))
					{
						goto IL_66F;
					}
				}
				else if (!(cardId == "LOOT_507"))
				{
					goto IL_66F;
				}
			}
			else if (num != 3007522138U)
			{
				if (num != 3024299757U)
				{
					goto IL_66F;
				}
				if (!(cardId == "LOOT_080t3"))
				{
					goto IL_66F;
				}
			}
			else if (!(cardId == "LOOT_080t2"))
			{
				goto IL_66F;
			}
		}
		else if (num <= 3611373248U)
		{
			if (num <= 3243091296U)
			{
				if (num != 3227199376U)
				{
					if (num != 3243091296U)
					{
						goto IL_66F;
					}
					if (!(cardId == "LOOT_051t2"))
					{
						goto IL_66F;
					}
				}
				else if (!(cardId == "LOOT_503t2"))
				{
					goto IL_66F;
				}
			}
			else if (num != 3293424153U)
			{
				if (num != 3611373248U)
				{
					goto IL_66F;
				}
				if (!(cardId == "LOOT_043t3"))
				{
					goto IL_66F;
				}
			}
			else if (!(cardId == "LOOT_051t1"))
			{
				goto IL_66F;
			}
		}
		else if (num <= 3742689339U)
		{
			if (num != 3628150867U)
			{
				if (num != 3742689339U)
				{
					goto IL_66F;
				}
				if (!(cardId == "LOOT_203"))
				{
					goto IL_66F;
				}
			}
			else if (!(cardId == "LOOT_043t2"))
			{
				goto IL_66F;
			}
		}
		else if (num != 4219535292U)
		{
			if (num != 4269868149U)
			{
				goto IL_66F;
			}
			if (!(cardId == "LOOT_103t1"))
			{
				goto IL_66F;
			}
		}
		else if (!(cardId == "LOOT_103t2"))
		{
			goto IL_66F;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01, 2.5f);
		IL_66F:
		yield break;
	}

	// Token: 0x06003BD1 RID: 15313 RVA: 0x0013728F File Offset: 0x0013548F
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		if (cardId == "BOT_236")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040024CB RID: 9419
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_BossBlessingOfKings_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_BossBlessingOfKings_01.prefab:362431c121a53e3418baab30f3337cf6");

	// Token: 0x040024CC RID: 9420
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Death_01.prefab:52478d9fe390c3f479a2d4f672cbbdc1");

	// Token: 0x040024CD RID: 9421
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_DefeatPlayer_01.prefab:c11cc13c4de085642adca08c1a70c0c6");

	// Token: 0x040024CE RID: 9422
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01.prefab:3493b456d393ef047b54699fc3dd471c");

	// Token: 0x040024CF RID: 9423
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_01.prefab:576e4e28c3a21d146a75f571e30b9783");

	// Token: 0x040024D0 RID: 9424
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_02.prefab:1c80b0707d1fc2540beabd40f7586a50");

	// Token: 0x040024D1 RID: 9425
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_03.prefab:97e90f36e963f5244902c4d6cf645fd6");

	// Token: 0x040024D2 RID: 9426
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_04.prefab:b10e21ec99878454bbb70eae121cb6e1");

	// Token: 0x040024D3 RID: 9427
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01.prefab:a3be152294e00854dbe3b94eae2d03bd");

	// Token: 0x040024D4 RID: 9428
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02.prefab:2fe52071140e7784c8c5c3259c2d21e8");

	// Token: 0x040024D5 RID: 9429
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03.prefab:f4ffaa80dc382604fbd8236a7a019659");

	// Token: 0x040024D6 RID: 9430
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04.prefab:fcabae9b8e6859848b4a7035649b893b");

	// Token: 0x040024D7 RID: 9431
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Idle_01.prefab:a54d80ffdb6341e458c257ecaa4234a0");

	// Token: 0x040024D8 RID: 9432
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Idle_02.prefab:94a0d67503c61894a89aff041e6438d6");

	// Token: 0x040024D9 RID: 9433
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Idle_03.prefab:16a643a55b1f54a4f9f724fa89e2f104");

	// Token: 0x040024DA RID: 9434
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Intro_01.prefab:f831100f9e60ff441ba8b1ae4f7bd2d9");

	// Token: 0x040024DB RID: 9435
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01.prefab:8caa17fa6d7ae9d419cb6e95e03759b8");

	// Token: 0x040024DC RID: 9436
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01.prefab:8e617c1e0daf89240a304e136d18840d");

	// Token: 0x040024DD RID: 9437
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01.prefab:22b252b7230fea145b379812d7318d09");

	// Token: 0x040024DE RID: 9438
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01.prefab:20ec9797c2cb578438ec1f484ee8f19e");

	// Token: 0x040024DF RID: 9439
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01.prefab:0b23d6106ea13ee4897b1156562c51db");

	// Token: 0x040024E0 RID: 9440
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01.prefab:cb89c9ec4aa20d943bca91196dd449e9");

	// Token: 0x040024E1 RID: 9441
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_TurnOne_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_TurnOne_01.prefab:c3bf8f01cb7a67f499e210311655aa12");

	// Token: 0x040024E2 RID: 9442
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Idle_02,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_Idle_03
	};

	// Token: 0x040024E3 RID: 9443
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_01,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_02,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_03,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPower_04
	};

	// Token: 0x040024E4 RID: 9444
	private static List<string> m_HeroPowerGolden = new List<string>
	{
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03,
		DALA_Dungeon_Boss_31h.VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04
	};

	// Token: 0x040024E5 RID: 9445
	private HashSet<string> m_playedLines = new HashSet<string>();
}
