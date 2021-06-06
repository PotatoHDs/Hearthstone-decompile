using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000502 RID: 1282
public class BTA_Fight_24 : BTA_Dungeon_Heroic
{
	// Token: 0x06004500 RID: 17664 RVA: 0x00175A2C File Offset: 0x00173C2C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01,
			BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_UI_Mission_Fight_24_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004501 RID: 17665 RVA: 0x00175BC0 File Offset: 0x00173DC0
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_24h_IdleLines;
	}

	// Token: 0x06004502 RID: 17666 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004503 RID: 17667 RVA: 0x00175BC8 File Offset: 0x00173DC8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01;
		this.m_standardEmoteResponseLine = BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01;
	}

	// Token: 0x06004504 RID: 17668 RVA: 0x00175BF0 File Offset: 0x00173DF0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_10" || cardId == "HERO_10a")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004505 RID: 17669 RVA: 0x00175CF0 File Offset: 0x00173EF0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 500)
		{
			if (missionEvent != 507)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger507Lines);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004506 RID: 17670 RVA: 0x00175D06 File Offset: 0x00173F06
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "BT_109")
		{
			yield return base.PlayLineOnlyOnce(actor, BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004507 RID: 17671 RVA: 0x00175D1C File Offset: 0x00173F1C
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BTA_BOSS_24t"))
		{
			if (!(cardId == "BT_110"))
			{
				if (!(cardId == "BT_230"))
				{
					if (cardId == "EX1_238")
					{
						yield return base.PlayLineOnlyOnce(actor, BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangEliteLines);
		}
		yield break;
	}

	// Token: 0x06004508 RID: 17672 RVA: 0x00175D32 File Offset: 0x00173F32
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x040037E1 RID: 14305
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01.prefab:e3d63675b22f79e4c8b479023969a0bc");

	// Token: 0x040037E2 RID: 14306
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01.prefab:2e67226b64f44a847bde99c46719a105");

	// Token: 0x040037E3 RID: 14307
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01.prefab:48db091dfe3a80f4694150e900f0330c");

	// Token: 0x040037E4 RID: 14308
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02.prefab:0339aab82e9580d4296dc6ec75e2e1b0");

	// Token: 0x040037E5 RID: 14309
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01.prefab:53006bfee7cdaf04bb87b85ce1d6b516");

	// Token: 0x040037E6 RID: 14310
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01.prefab:9297d3bddef53524eb26bc31b73bd2f1");

	// Token: 0x040037E7 RID: 14311
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01.prefab:46233fcd1e7d4914d9312c9379b7a9e6");

	// Token: 0x040037E8 RID: 14312
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01.prefab:e81575e9996a764498922f6e29c78e01");

	// Token: 0x040037E9 RID: 14313
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01.prefab:536317df38e083046845246a41109d6b");

	// Token: 0x040037EA RID: 14314
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01.prefab:2d32c35040548d547ad9fc19b5b8b864");

	// Token: 0x040037EB RID: 14315
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01.prefab:c3f902a050d2a07488944749521f06a6");

	// Token: 0x040037EC RID: 14316
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01.prefab:c12ab414324a5564da2632ebce568b36");

	// Token: 0x040037ED RID: 14317
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02.prefab:a89fb81b96e5cb843953afaeb436c4c4");

	// Token: 0x040037EE RID: 14318
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03.prefab:507050813e0a8ef43b2cdb5fa881ace3");

	// Token: 0x040037EF RID: 14319
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04.prefab:8820cce8136fa6e46bf555d29dbb01d0");

	// Token: 0x040037F0 RID: 14320
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01.prefab:08d3b3877a3f9d246851bab5420c00a0");

	// Token: 0x040037F1 RID: 14321
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01.prefab:f3ec2682d3af4464ebbe35bf81559f79");

	// Token: 0x040037F2 RID: 14322
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01.prefab:e0fce4f07262b234289bcf55676aef12");

	// Token: 0x040037F3 RID: 14323
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_UI_Mission_Fight_24_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_UI_Mission_Fight_24_CoinSelect_01.prefab:253dabbfd2c19f8488345380a6953f76");

	// Token: 0x040037F4 RID: 14324
	private List<string> m_VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangEliteLines = new List<string>
	{
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01,
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02
	};

	// Token: 0x040037F5 RID: 14325
	private List<string> m_missionEventTrigger507Lines = new List<string>
	{
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01,
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02,
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03,
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04
	};

	// Token: 0x040037F6 RID: 14326
	private List<string> m_VO_BTA_BOSS_24h_IdleLines = new List<string>
	{
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01,
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01,
		BTA_Fight_24.VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01
	};

	// Token: 0x040037F7 RID: 14327
	private HashSet<string> m_playedLines = new HashSet<string>();
}
