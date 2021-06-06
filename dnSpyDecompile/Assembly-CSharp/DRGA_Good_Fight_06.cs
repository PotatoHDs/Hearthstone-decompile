using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004E0 RID: 1248
public class DRGA_Good_Fight_06 : DRGA_Dungeon
{
	// Token: 0x060042DE RID: 17118 RVA: 0x00168D4C File Offset: 0x00166F4C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_06.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01,
			DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060042DF RID: 17119 RVA: 0x00168F60 File Offset: 0x00167160
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_IdleLines;
	}

	// Token: 0x060042E0 RID: 17120 RVA: 0x00168F68 File Offset: 0x00167168
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPowerLines;
	}

	// Token: 0x060042E1 RID: 17121 RVA: 0x00168F70 File Offset: 0x00167170
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01;
	}

	// Token: 0x060042E2 RID: 17122 RVA: 0x00168F88 File Offset: 0x00167188
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		if (!this.m_Heroic)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (this.m_Heroic)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060042E3 RID: 17123 RVA: 0x00169059 File Offset: 0x00167259
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_06.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01, 2.5f);
				goto IL_23C;
			}
			goto IL_23C;
		case 104:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01, 2.5f);
				goto IL_23C;
			}
			goto IL_23C;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01, 2.5f);
				goto IL_23C;
			}
			goto IL_23C;
		case 106:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_06.VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01, 2.5f);
				goto IL_23C;
			}
			goto IL_23C;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01, 2.5f);
			goto IL_23C;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_23C:
		yield break;
	}

	// Token: 0x060042E4 RID: 17124 RVA: 0x0016906F File Offset: 0x0016726F
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "EX1_561"))
		{
			if (!(cardId == "GIL_504"))
			{
				if (!(cardId == "DAL_009"))
				{
					if (cardId == "GIL_820")
					{
						yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060042E5 RID: 17125 RVA: 0x00169085 File Offset: 0x00167285
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
		if (!(cardId == "DRGA_BOSS_06t"))
		{
			if (cardId == "DRGA_BOSS_06t2")
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WormholeLines);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwockLines);
		}
		yield break;
	}

	// Token: 0x040033C7 RID: 13255
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01.prefab:07fb5704f92f2a141a4cebb96b2c6630");

	// Token: 0x040033C8 RID: 13256
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01.prefab:dd7acb97dad260f42b2ee530cebe6d04");

	// Token: 0x040033C9 RID: 13257
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01.prefab:cfc2c34ed3f9d114b919eda95929f512");

	// Token: 0x040033CA RID: 13258
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01.prefab:880f4643a2abaa24eb7c569120cd020c");

	// Token: 0x040033CB RID: 13259
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01.prefab:25c6ebcc533e74d40b8f5302578e1226");

	// Token: 0x040033CC RID: 13260
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01.prefab:8c4ace3d96b3b4941b2c823b807f65b6");

	// Token: 0x040033CD RID: 13261
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01.prefab:0218774043411a044a7187fde0fb1e73");

	// Token: 0x040033CE RID: 13262
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01.prefab:2bc8ec5740ed36d4992b9d15b5203f59");

	// Token: 0x040033CF RID: 13263
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01.prefab:d20f43aa81ae75340ae76958f278baf8");

	// Token: 0x040033D0 RID: 13264
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01.prefab:40b50b50b6ab9754cb9faaf5d23b1e82");

	// Token: 0x040033D1 RID: 13265
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01.prefab:5d7c7e9743d61ec47b0e1be1751d56c9");

	// Token: 0x040033D2 RID: 13266
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01.prefab:348ee603c78c3524a94edd3084cc9743");

	// Token: 0x040033D3 RID: 13267
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01.prefab:b4813b2bd518b7945990fa5e386fcb08");

	// Token: 0x040033D4 RID: 13268
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01.prefab:94937513f9fd954469ee1cf9d60df53e");

	// Token: 0x040033D5 RID: 13269
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01.prefab:02592c2e441807e4a82b828d1c733ce5");

	// Token: 0x040033D6 RID: 13270
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01.prefab:5d1c0611bd74d444492b8998fbfea7d2");

	// Token: 0x040033D7 RID: 13271
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01.prefab:e8da6e64caaae854bbaeab1478623f90");

	// Token: 0x040033D8 RID: 13272
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01.prefab:bbde722c4668ff741813ce379840a0a1");

	// Token: 0x040033D9 RID: 13273
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01.prefab:75c59d7faf8900e4fb33a89f048e2630");

	// Token: 0x040033DA RID: 13274
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01.prefab:932c5936f1e05d440867ac615d58871c");

	// Token: 0x040033DB RID: 13275
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01.prefab:6e2943d76bdfcb149af604ab42ee2d3e");

	// Token: 0x040033DC RID: 13276
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01.prefab:64b4eca11dfdbc34cb856bb5154f71ca");

	// Token: 0x040033DD RID: 13277
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01.prefab:56708b2c88649a1498566e31a1ba03b9");

	// Token: 0x040033DE RID: 13278
	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01.prefab:f585e621ed631764481e767eed92a482");

	// Token: 0x040033DF RID: 13279
	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01.prefab:ddb97a63484c90e43b9559f8bc844885");

	// Token: 0x040033E0 RID: 13280
	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01.prefab:dfc12eafb2e1bf242934273816060327");

	// Token: 0x040033E1 RID: 13281
	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01.prefab:9c189b0f8e5216e42b5d3827bc29d6ea");

	// Token: 0x040033E2 RID: 13282
	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01
	};

	// Token: 0x040033E3 RID: 13283
	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwockLines = new List<string>
	{
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01
	};

	// Token: 0x040033E4 RID: 13284
	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WormholeLines = new List<string>
	{
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01
	};

	// Token: 0x040033E5 RID: 13285
	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_IdleLines = new List<string>
	{
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01,
		DRGA_Good_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01
	};

	// Token: 0x040033E6 RID: 13286
	private HashSet<string> m_playedLines = new HashSet<string>();
}
