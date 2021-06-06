using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004F9 RID: 1273
public class BTA_Fight_15 : BTA_Dungeon
{
	// Token: 0x0600447A RID: 17530 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600447B RID: 17531 RVA: 0x00172E2C File Offset: 0x0017102C
	public BTA_Fight_15()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_15.s_booleanOptions);
	}

	// Token: 0x0600447C RID: 17532 RVA: 0x00172F24 File Offset: 0x00171124
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_15.VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01,
			BTA_Fight_15.VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01,
			BTA_Fight_15.VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01,
			BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01,
			BTA_Fight_15.VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600447D RID: 17533 RVA: 0x00173148 File Offset: 0x00171348
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_15h_IdleLines;
	}

	// Token: 0x0600447E RID: 17534 RVA: 0x00173150 File Offset: 0x00171350
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01;
	}

	// Token: 0x0600447F RID: 17535 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004480 RID: 17536 RVA: 0x00173168 File Offset: 0x00171368
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return this.shouldEnemyActorExplode || playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004481 RID: 17537 RVA: 0x0017317B File Offset: 0x0017137B
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_15.VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004482 RID: 17538 RVA: 0x0017318C File Offset: 0x0017138C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004483 RID: 17539 RVA: 0x00173214 File Offset: 0x00171414
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayRandomLineAlways(enemyActor, this.m_missionEventTrigger100_Lines);
			break;
		case 101:
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01, 2.5f);
			break;
		case 103:
			GameState.Get().SetBusy(true);
			this.shouldEnemyActorExplode = false;
			this.m_deathLine = null;
			yield return base.PlayRandomLineAlways(enemyActor, this.m_missionEventTrigger103_Lines);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_15.VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 104:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_15.VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		default:
			if (missionEvent != 500)
			{
				if (missionEvent != 501)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(friendlyActor, BTA_Fight_15.VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, BTA_Fight_15.VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				base.PlaySound(BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01, 1f, true, false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004484 RID: 17540 RVA: 0x0017322A File Offset: 0x0017142A
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
		if (!(cardId == "BT_354"))
		{
			if (!(cardId == "BT_429"))
			{
				if (cardId == "BT_493")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004485 RID: 17541 RVA: 0x00173240 File Offset: 0x00171440
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
		if (!(cardId == "BT_702"))
		{
			if (!(cardId == "CS2_077"))
			{
				if (cardId == "EX1_126")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004486 RID: 17542 RVA: 0x00173256 File Offset: 0x00171456
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

	// Token: 0x04003710 RID: 14096
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_15.InitBooleanOptions();

	// Token: 0x04003711 RID: 14097
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01.prefab:14596b34ebbc4b34496f20b8e9bd490c");

	// Token: 0x04003712 RID: 14098
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01.prefab:bdb407d38aee2fc4b98ac4254ac5d822");

	// Token: 0x04003713 RID: 14099
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01.prefab:0e554cf533cc69e4ebb72c0d355a4746");

	// Token: 0x04003714 RID: 14100
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01.prefab:7d6a61670538aa2458501d48875ed511");

	// Token: 0x04003715 RID: 14101
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01.prefab:2e02ad18ce4c29c4fa2162474c59c438");

	// Token: 0x04003716 RID: 14102
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01.prefab:e56b316808fa9ef47b6abd137f7f3faf");

	// Token: 0x04003717 RID: 14103
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01.prefab:a6dfb894cf0deb242a25f3436118b26c");

	// Token: 0x04003718 RID: 14104
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01.prefab:fafd9b1edb0ee8345a1a28618d9d74c1");

	// Token: 0x04003719 RID: 14105
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01.prefab:c400b66a433ca964a921196d4d9ec45f");

	// Token: 0x0400371A RID: 14106
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01.prefab:df0c19e1b43c7d64fb99334f85184a3a");

	// Token: 0x0400371B RID: 14107
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01.prefab:89661cbc1dbddb049842fd9ef2a456c9");

	// Token: 0x0400371C RID: 14108
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02.prefab:1d53f67380b935a4999cb7b27c1c3b89");

	// Token: 0x0400371D RID: 14109
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01.prefab:40961be1503134048a868990e3419cc0");

	// Token: 0x0400371E RID: 14110
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01.prefab:3e47af720f0fd284fac9f0457f41e561");

	// Token: 0x0400371F RID: 14111
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01.prefab:53ad352b3841dda47b9c726dbfca3660");

	// Token: 0x04003720 RID: 14112
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01.prefab:2edad56215fe4f945afd73d4170de70d");

	// Token: 0x04003721 RID: 14113
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01.prefab:b34eb9c171f915b49ba541239d6c2c12");

	// Token: 0x04003722 RID: 14114
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01.prefab:6a51855be33c3734e8dd00ac0fe76243");

	// Token: 0x04003723 RID: 14115
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02.prefab:67bf5130a6208864d9696171fc5fd22a");

	// Token: 0x04003724 RID: 14116
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03.prefab:2a29e5a54cc191c478785d480b439ef7");

	// Token: 0x04003725 RID: 14117
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04.prefab:76b21a6a346cc15478eff3f9a29717de");

	// Token: 0x04003726 RID: 14118
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01.prefab:09f710e21019795498ef62f109e9ea42");

	// Token: 0x04003727 RID: 14119
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01.prefab:1765c3f6848501e46b1dce09f6611c8c");

	// Token: 0x04003728 RID: 14120
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01.prefab:d384d8fa9d5b573468125ec47132f7f6");

	// Token: 0x04003729 RID: 14121
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01.prefab:07dc71ede87d4c847961b8caa50132a4");

	// Token: 0x0400372A RID: 14122
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01.prefab:5e1be49dba03cac41a8ef63c7576a6fa");

	// Token: 0x0400372B RID: 14123
	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01.prefab:3f97487fe0b57ab4da578c3c3440f515");

	// Token: 0x0400372C RID: 14124
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01.prefab:f3c652cba42256c42866227f75dbd60e");

	// Token: 0x0400372D RID: 14125
	private List<string> m_VO_BTA_BOSS_15h_IdleLines = new List<string>
	{
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01,
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01,
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01,
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01
	};

	// Token: 0x0400372E RID: 14126
	private List<string> m_missionEventTrigger103_Lines = new List<string>
	{
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01,
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02
	};

	// Token: 0x0400372F RID: 14127
	private List<string> m_missionEventTrigger100_Lines = new List<string>
	{
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01,
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02,
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03,
		BTA_Fight_15.VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04
	};

	// Token: 0x04003730 RID: 14128
	public bool shouldEnemyActorExplode = true;

	// Token: 0x04003731 RID: 14129
	private HashSet<string> m_playedLines = new HashSet<string>();
}
