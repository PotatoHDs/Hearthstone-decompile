using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004F6 RID: 1270
public class BTA_Fight_12 : BTA_Dungeon
{
	// Token: 0x06004448 RID: 17480 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x06004449 RID: 17481 RVA: 0x00171EF0 File Offset: 0x001700F0
	public BTA_Fight_12()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_12.s_booleanOptions);
	}

	// Token: 0x0600444A RID: 17482 RVA: 0x00171FA4 File Offset: 0x001701A4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_12.VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01,
			BTA_Fight_12.VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01,
			BTA_Fight_12.VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathA_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01,
			BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600444B RID: 17483 RVA: 0x00172178 File Offset: 0x00170378
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_12h_IdleLines;
	}

	// Token: 0x0600444C RID: 17484 RVA: 0x00172180 File Offset: 0x00170380
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01;
	}

	// Token: 0x0600444D RID: 17485 RVA: 0x00172198 File Offset: 0x00170398
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_12.VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600444E RID: 17486 RVA: 0x001721A8 File Offset: 0x001703A8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600444F RID: 17487 RVA: 0x00172230 File Offset: 0x00170430
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 500)
		{
			if (missionEvent != 501)
			{
				if (missionEvent != 507)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_Lines);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor2, BTA_Fight_12.VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			base.PlaySound(BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01, 1f, true, false);
		}
		yield break;
	}

	// Token: 0x06004450 RID: 17488 RVA: 0x00172246 File Offset: 0x00170446
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_235"))
		{
			if (!(cardId == "BT_801"))
			{
				if (!(cardId == "BT_850"))
				{
					if (cardId == "BTA_08")
					{
						yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, BTA_Fight_12.VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01, 2.5f);
						yield return base.PlayLineAlways(enemyActor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(enemyActor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004451 RID: 17489 RVA: 0x0017225C File Offset: 0x0017045C
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
		if (!(cardId == "BTA_15"))
		{
			if (!(cardId == "EX1_308"))
			{
				if (!(cardId == "GVG_052"))
				{
					if (cardId == "OG_276")
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004452 RID: 17490 RVA: 0x00172272 File Offset: 0x00170472
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x040036C5 RID: 14021
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_12.InitBooleanOptions();

	// Token: 0x040036C6 RID: 14022
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01.prefab:6207b28dfe2d2dd489bddb216c9598bc");

	// Token: 0x040036C7 RID: 14023
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01.prefab:d5c8917a4cb3401499ecd86c42801133");

	// Token: 0x040036C8 RID: 14024
	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01.prefab:9ee0d92e24cef7840b5bf44e3dd19b09");

	// Token: 0x040036C9 RID: 14025
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01.prefab:dbd37b82d84594e499da15674822603e");

	// Token: 0x040036CA RID: 14026
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01.prefab:7c97c474478831c479dafff631800ca3");

	// Token: 0x040036CB RID: 14027
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01.prefab:8679ea5398ff9bd4099f5a620571ea31");

	// Token: 0x040036CC RID: 14028
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01.prefab:2961d0b9b354e684a9f522f100b12494");

	// Token: 0x040036CD RID: 14029
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01.prefab:aa316ccb5f534e546b70dd1124c96ed4");

	// Token: 0x040036CE RID: 14030
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathA_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathA_01.prefab:2de2f95d4acf4f94d91022f3c956bd40");

	// Token: 0x040036CF RID: 14031
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01.prefab:567321497cafb6b4ba8cbba14d7ca737");

	// Token: 0x040036D0 RID: 14032
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01.prefab:bb154eb15195c304db7ce2c69f43c2ac");

	// Token: 0x040036D1 RID: 14033
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01.prefab:0b372717b0e66d64f9015a3e6ddd4cab");

	// Token: 0x040036D2 RID: 14034
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01.prefab:59c3aa5627b9e804ca17b67f4c4c5cba");

	// Token: 0x040036D3 RID: 14035
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01.prefab:98d6cdd652a551b4f9d2a854c790630d");

	// Token: 0x040036D4 RID: 14036
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01.prefab:f62ffa26019811a4eaa176e993130198");

	// Token: 0x040036D5 RID: 14037
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01.prefab:4e0d3012425089f49b88e6fce4eaa82d");

	// Token: 0x040036D6 RID: 14038
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02.prefab:2003ba4e664e63549983cfc1d8bb7936");

	// Token: 0x040036D7 RID: 14039
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03.prefab:bf3ecd2fbc8e26840a45a12a378e4d9c");

	// Token: 0x040036D8 RID: 14040
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04.prefab:346f319a2dd82e54a88897feee4ad529");

	// Token: 0x040036D9 RID: 14041
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01.prefab:8012dbfe8fce27947a8cfe559c91f909");

	// Token: 0x040036DA RID: 14042
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01.prefab:b969da525c961e943be2fd9e0b2f7ca9");

	// Token: 0x040036DB RID: 14043
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01.prefab:4be27b6c5038b534f955d6c048843293");

	// Token: 0x040036DC RID: 14044
	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01.prefab:58583e48a54a77346bb7115ba47638b0");

	// Token: 0x040036DD RID: 14045
	private List<string> m_VO_BTA_BOSS_12h_IdleLines = new List<string>
	{
		BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01,
		BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01,
		BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01
	};

	// Token: 0x040036DE RID: 14046
	private List<string> m_VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_Lines = new List<string>
	{
		BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01,
		BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02,
		BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03,
		BTA_Fight_12.VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04
	};

	// Token: 0x040036DF RID: 14047
	private HashSet<string> m_playedLines = new HashSet<string>();
}
