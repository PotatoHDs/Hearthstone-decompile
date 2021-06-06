using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004EF RID: 1263
public class BTA_Fight_05 : BTA_Dungeon
{
	// Token: 0x060043C5 RID: 17349 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060043C6 RID: 17350 RVA: 0x0016F188 File Offset: 0x0016D388
	public BTA_Fight_05()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_05.s_booleanOptions);
	}

	// Token: 0x060043C7 RID: 17351 RVA: 0x0016F2C4 File Offset: 0x0016D4C4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Minion_BaduuArrivesA_01,
			BTA_Fight_05.VO_BTA_07_Male_Orc_Mission_Fight_05_Minion_BaduuArrivesB_01,
			BTA_Fight_05.VO_BTA_05_Male_Sporelok_Mission_Fight_05_Minion_BaduuArrivesC_01,
			BTA_Fight_05.VO_BTA_09_Female_Naga_Mission_Fight_05_Minion_BaduuArrivesD_01,
			BTA_Fight_05.VO_BTA_01_Female_NightElf_Mission_Fight_05_Minion_BaduuArrivesE_01,
			BTA_Fight_05.VO_BTA_01_Female_NightElf_Mission_Fight_05_Hero_HalfHealth_01,
			BTA_Fight_05.VO_BTA_01_Female_NightElf_Mission_Fight_05_PlayerStart_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Attack_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Hellfire_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_MortalCoilKill_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_RustswornChampion_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossDeath_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossStart_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_02,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_03,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Emote_Response_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_02,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_03,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_04,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleA_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleB_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleC_01,
			BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_TurnOne_01,
			BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleA_01,
			BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleB_01,
			BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleC_01,
			BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Minion_BaduuHand_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060043C8 RID: 17352 RVA: 0x0016F4F8 File Offset: 0x0016D6F8
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_05h_dleLines;
	}

	// Token: 0x060043C9 RID: 17353 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060043CA RID: 17354 RVA: 0x0016F500 File Offset: 0x0016D700
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossDeath_01;
	}

	// Token: 0x060043CB RID: 17355 RVA: 0x0016F518 File Offset: 0x0016D718
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_05.VO_BTA_01_Female_NightElf_Mission_Fight_05_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060043CC RID: 17356 RVA: 0x0016F528 File Offset: 0x0016D728
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060043CD RID: 17357 RVA: 0x0016F5B0 File Offset: 0x0016D7B0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 101)
		{
			if (missionEvent == 100)
			{
				yield return new WaitForSeconds(2f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_05.VO_BTA_07_Male_Orc_Mission_Fight_05_Minion_BaduuArrivesB_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_05"), BTA_Dungeon.SklibbBrassRing, BTA_Fight_05.VO_BTA_05_Male_Sporelok_Mission_Fight_05_Minion_BaduuArrivesC_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_05.VO_BTA_09_Female_Naga_Mission_Fight_05_Minion_BaduuArrivesD_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_05.VO_BTA_01_Female_NightElf_Mission_Fight_05_Minion_BaduuArrivesE_01, 2.5f);
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_Lines);
				goto IL_393;
			}
			if (missionEvent == 101)
			{
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_05.VO_BTA_01_Female_NightElf_Mission_Fight_05_Hero_HalfHealth_01, 2.5f);
				goto IL_393;
			}
		}
		else
		{
			if (missionEvent == 500)
			{
				base.PlaySound(BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Attack_01, 1f, true, false);
				goto IL_393;
			}
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleA_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleB_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleC_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_393;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_Lines);
				goto IL_393;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_393:
		yield break;
	}

	// Token: 0x060043CE RID: 17358 RVA: 0x0016F5C6 File Offset: 0x0016D7C6
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
		if (!(cardId == "BT_707t"))
		{
			if (cardId == "BTA_03")
			{
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_05.VO_BTA_03_Female_Broken_Mission_Fight_05_Minion_BaduuHand_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayRandomLineAlways(actor, this.m_VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_Lines);
		}
		yield break;
	}

	// Token: 0x060043CF RID: 17359 RVA: 0x0016F5DC File Offset: 0x0016D7DC
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
		if (!(cardId == "BTA_11"))
		{
			if (!(cardId == "CS2_062"))
			{
				if (cardId == "EX1_302")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_MortalCoilKill_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Hellfire_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_RustswornChampion_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043D0 RID: 17360 RVA: 0x0016F5F2 File Offset: 0x0016D7F2
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1)
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_TurnOne_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040035D4 RID: 13780
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_05.InitBooleanOptions();

	// Token: 0x040035D5 RID: 13781
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_05_Hero_HalfHealth_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_05_Hero_HalfHealth_01.prefab:c5c04c5dee72be34e95fdc69a817b641");

	// Token: 0x040035D6 RID: 13782
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_05_Minion_BaduuArrivesE_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_05_Minion_BaduuArrivesE_01.prefab:fe4f991f6d2a1b14690e1b6e14f5ee32");

	// Token: 0x040035D7 RID: 13783
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_05_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_05_PlayerStart_01.prefab:cbbdef9bb61fd1f478451af8463cae8e");

	// Token: 0x040035D8 RID: 13784
	private static readonly AssetReference VO_BTA_05_Male_Sporelok_Mission_Fight_05_Minion_BaduuArrivesC_01 = new AssetReference("VO_BTA_05_Male_Sporelok_Mission_Fight_05_Minion_BaduuArrivesC_01.prefab:4dda4e29c4c540f4e8a52e6e40789ebb");

	// Token: 0x040035D9 RID: 13785
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_05_Minion_BaduuArrivesB_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_05_Minion_BaduuArrivesB_01.prefab:343ebc20ee2d71647820df3efaea5821");

	// Token: 0x040035DA RID: 13786
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_05_Minion_BaduuArrivesD_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_05_Minion_BaduuArrivesD_01.prefab:c85a8bc5e26272d4dba740b640e15394");

	// Token: 0x040035DB RID: 13787
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Attack_01.prefab:5fd92fe65b14de7428032eaecf56c145");

	// Token: 0x040035DC RID: 13788
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Hellfire_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_Hellfire_01.prefab:78440e87696d2d74baebc54162e68f93");

	// Token: 0x040035DD RID: 13789
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_MortalCoilKill_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_MortalCoilKill_01.prefab:250c96848ac395d4c93963a4132ce306");

	// Token: 0x040035DE RID: 13790
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_RustswornChampion_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Boss_RustswornChampion_01.prefab:42ea5291394b26f47bd7a0fcc991ad6c");

	// Token: 0x040035DF RID: 13791
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossDeath_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossDeath_01.prefab:9f833c29dddfb4442b7bde1d8f8a2588");

	// Token: 0x040035E0 RID: 13792
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossStart_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BossStart_01.prefab:34b68752ecbb8574694f1e9c84499991");

	// Token: 0x040035E1 RID: 13793
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_01.prefab:aff4fe69dcd180b40a99dd6b016c7ff7");

	// Token: 0x040035E2 RID: 13794
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_02 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_02.prefab:7f7e8ae3202d18842b9480783b30713d");

	// Token: 0x040035E3 RID: 13795
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_03 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_03.prefab:dec355da407f82e499c5a15cf3b9250e");

	// Token: 0x040035E4 RID: 13796
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_Emote_Response_01.prefab:5e88889a325d4eb438ff5ce34d680ee7");

	// Token: 0x040035E5 RID: 13797
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_01.prefab:08d2b599be3e16e42ac16524bad02a75");

	// Token: 0x040035E6 RID: 13798
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_02 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_02.prefab:ad3757e9fb5957f40aacabab91dd1877");

	// Token: 0x040035E7 RID: 13799
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_03 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_03.prefab:c7703ba0f79108743a87d0c10ed29735");

	// Token: 0x040035E8 RID: 13800
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_04 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_04.prefab:c78358a33ef9f054f935bbc3d3dd5e8b");

	// Token: 0x040035E9 RID: 13801
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleA_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleA_01.prefab:d1727cf00bf37d34b997ed928b66425b");

	// Token: 0x040035EA RID: 13802
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleB_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleB_01.prefab:a2469495e2eb61f4bb6505316dd0ac5e");

	// Token: 0x040035EB RID: 13803
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleC_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleC_01.prefab:f14c7d5670f249f489994e70f6762f1f");

	// Token: 0x040035EC RID: 13804
	private static readonly AssetReference VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_TurnOne_01 = new AssetReference("VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_TurnOne_01.prefab:3f517a03417e2aa4c925ebeae1aaa1de");

	// Token: 0x040035ED RID: 13805
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleA_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleA_01.prefab:5c77cbbfc70d21b4ea45b086bb27f156");

	// Token: 0x040035EE RID: 13806
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleB_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleB_01.prefab:444a752a58748b5448db17badf5d56d4");

	// Token: 0x040035EF RID: 13807
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleC_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_05_Baduu_BlackTempleC_01.prefab:e1ad308612baeba49887f8f93ce6318c");

	// Token: 0x040035F0 RID: 13808
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_05_Minion_BaduuArrivesA_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_05_Minion_BaduuArrivesA_01.prefab:5b689f915f277f345ace5509a742f6d7");

	// Token: 0x040035F1 RID: 13809
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_05_Minion_BaduuHand_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_05_Minion_BaduuHand_01.prefab:5ffe3c59c188c924295b977333012ee0");

	// Token: 0x040035F2 RID: 13810
	private List<string> m_VO_BTA_BOSS_05h_dleLines = new List<string>
	{
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleA_01,
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleB_01,
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_IdleC_01
	};

	// Token: 0x040035F3 RID: 13811
	private List<string> m_missionEventTrigger100_Lines = new List<string>
	{
		BTA_Fight_05.VO_BTA_01_Female_NightElf_Mission_Fight_05_Minion_BaduuArrivesE_01,
		BTA_Fight_05.VO_BTA_05_Male_Sporelok_Mission_Fight_05_Minion_BaduuArrivesC_01,
		BTA_Fight_05.VO_BTA_07_Male_Orc_Mission_Fight_05_Minion_BaduuArrivesB_01,
		BTA_Fight_05.VO_BTA_09_Female_Naga_Mission_Fight_05_Minion_BaduuArrivesD_01
	};

	// Token: 0x040035F4 RID: 13812
	private List<string> m_VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_Lines = new List<string>
	{
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_01,
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_02,
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_BrokenAppear_03
	};

	// Token: 0x040035F5 RID: 13813
	private List<string> m_VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_Lines = new List<string>
	{
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_01,
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_02,
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_03,
		BTA_Fight_05.VO_BTA_BOSS_05h_Male_Human_Mission_Fight_05_HeroPower_04
	};

	// Token: 0x040035F6 RID: 13814
	private HashSet<string> m_playedLines = new HashSet<string>();
}
