using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004ED RID: 1261
public class BTA_Fight_03 : BTA_Dungeon
{
	// Token: 0x060043A4 RID: 17316 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060043A5 RID: 17317 RVA: 0x0016E900 File Offset: 0x0016CB00
	public BTA_Fight_03()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_03.s_booleanOptions);
	}

	// Token: 0x060043A6 RID: 17318 RVA: 0x0016E95C File Offset: 0x0016CB5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Misc_01,
			BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Response_01,
			BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01,
			BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01,
			BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01,
			BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01,
			BTA_Fight_03.VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01,
			BTA_Fight_03.VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01,
			BTA_Fight_03.VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01,
			BTA_Fight_03.VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01,
			BTA_Fight_03.BTA_BOSS_03h_BossStart_01,
			BTA_Fight_03.VO_BTA_BOSS_10h2_Male_Sporelok_Death_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060043A7 RID: 17319 RVA: 0x0016D56A File Offset: 0x0016B76A
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x060043A8 RID: 17320 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060043A9 RID: 17321 RVA: 0x0016EA80 File Offset: 0x0016CC80
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_03.BTA_BOSS_03h_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060043AA RID: 17322 RVA: 0x0016EA90 File Offset: 0x0016CC90
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType);
	}

	// Token: 0x060043AB RID: 17323 RVA: 0x0016EAE8 File Offset: 0x0016CCE8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 501)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_05"), BTA_Dungeon.SklibbBrassRing, BTA_Fight_03.VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_05"), BTA_Dungeon.SklibbBrassRing, BTA_Fight_03.VO_BTA_BOSS_10h2_Male_Sporelok_Death_02, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060043AC RID: 17324 RVA: 0x0016EAFE File Offset: 0x0016CCFE
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "BTA_09")
		{
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_03.VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043AD RID: 17325 RVA: 0x0016EB14 File Offset: 0x0016CD14
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060043AE RID: 17326 RVA: 0x0016EB2A File Offset: 0x0016CD2A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1)
		{
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_03.VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040035A6 RID: 13734
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_03.InitBooleanOptions();

	// Token: 0x040035A7 RID: 13735
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Misc_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Misc_01.prefab:aa77a6190a706a9409f74424055e919d");

	// Token: 0x040035A8 RID: 13736
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Response_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Response_01.prefab:c2f61fcc26296f742b3ae0339bcf6d5f");

	// Token: 0x040035A9 RID: 13737
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01.prefab:a238ff9cbe5b04d4dbc58a04c21f2170");

	// Token: 0x040035AA RID: 13738
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01.prefab:f958d5cd88f88ec49a179c2c9e4dd5e0");

	// Token: 0x040035AB RID: 13739
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01.prefab:4502e40b6c2c6d34589f2fcec0b56d66");

	// Token: 0x040035AC RID: 13740
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01.prefab:5d9fe6f43cfefed49a6c99b3b115f94b");

	// Token: 0x040035AD RID: 13741
	private static readonly AssetReference VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01 = new AssetReference("VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01.prefab:f0250cebc03517c4c965b6651451dcea");

	// Token: 0x040035AE RID: 13742
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01.prefab:981dcbdafca1eb0408b78d03458537cc");

	// Token: 0x040035AF RID: 13743
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01.prefab:bc143e2ea74384144ae654373969927a");

	// Token: 0x040035B0 RID: 13744
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01.prefab:72073a89bec549e4f97966c19544d294");

	// Token: 0x040035B1 RID: 13745
	private static readonly AssetReference BTA_BOSS_03h_BossStart_01 = new AssetReference("BTA_BOSS_03h_BossStart_01.prefab:ed4dd64ad7f74444c86bb7c59c26e43c");

	// Token: 0x040035B2 RID: 13746
	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Death_02 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Death_02.prefab:6df02684ddf4c454d9217e9998b3f09f");

	// Token: 0x040035B3 RID: 13747
	private List<string> m_missionEventTrigger501_Lines = new List<string>
	{
		BTA_Fight_03.VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01,
		BTA_Fight_03.VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01
	};

	// Token: 0x040035B4 RID: 13748
	private HashSet<string> m_playedLines = new HashSet<string>();
}
