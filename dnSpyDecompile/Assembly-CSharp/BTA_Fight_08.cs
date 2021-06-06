using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004F2 RID: 1266
public class BTA_Fight_08 : BTA_Dungeon
{
	// Token: 0x06004406 RID: 17414 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004407 RID: 17415 RVA: 0x001708B8 File Offset: 0x0016EAB8
	public BTA_Fight_08()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_08.s_booleanOptions);
	}

	// Token: 0x06004408 RID: 17416 RVA: 0x0017096C File Offset: 0x0016EB6C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_08.VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesA_01,
			BTA_Fight_08.VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesB_01,
			BTA_Fight_08.VO_BTA_01_Female_NightElf_Mission_Fight_08_Baduu_LeavesC_01,
			BTA_Fight_08.VO_BTA_01_Female_NightElf_Mission_Fight_08_PlayerStart_01,
			BTA_Fight_08.VO_BTA_07_Male_Orc_Mission_Fight_08_Baduu_LeavesD_01,
			BTA_Fight_08.VO_BTA_09_Female_Naga_Mission_Fight_08_Baduu_LeavesE_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Attack_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Bladestorm_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_WeaponEquip_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossDeathA_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossDeathB_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossStart_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Emote_Response_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_ScavengersCunningTrigger_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_StolenSteel_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_WeaponEquip_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_02,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_03,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_04,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleA_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleB_01,
			BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleC_01,
			BTA_Fight_08.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_08_TurnOneBrassRing_01,
			BTA_Fight_08.VO_BTA_03_Female_Broken_Mission_Fight_08_TurnOneBrassRing_02,
			BTA_Fight_08.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_Illidans_Challenge_01,
			BTA_Fight_08.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_VictoryA_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004409 RID: 17417 RVA: 0x00170B80 File Offset: 0x0016ED80
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_08h_IdleLines;
	}

	// Token: 0x0600440A RID: 17418 RVA: 0x00170B88 File Offset: 0x0016ED88
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossDeathB_01;
	}

	// Token: 0x0600440B RID: 17419 RVA: 0x00170BA0 File Offset: 0x0016EDA0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_08.VO_BTA_01_Female_NightElf_Mission_Fight_08_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600440C RID: 17420 RVA: 0x00170BB0 File Offset: 0x0016EDB0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600440D RID: 17421 RVA: 0x00170C38 File Offset: 0x0016EE38
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 500)
		{
			switch (missionEvent)
			{
			case 100:
				yield return base.PlayLineAlways(actor, BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_WeaponEquip_01, 2.5f);
				goto IL_33D;
			case 101:
				yield return base.PlayLineAlways(actor, BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_WeaponEquip_01, 2.5f);
				goto IL_33D;
			case 102:
				yield return base.PlayLineAlways(actor, BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_ScavengersCunningTrigger_01, 2.5f);
				goto IL_33D;
			case 103:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_08.VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesB_01, 2.5f);
				GameState.Get().SetBusy(false);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_08.VO_BTA_01_Female_NightElf_Mission_Fight_08_Baduu_LeavesC_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_08.VO_BTA_07_Male_Orc_Mission_Fight_08_Baduu_LeavesD_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_08.VO_BTA_09_Female_Naga_Mission_Fight_08_Baduu_LeavesE_01, 2.5f);
				goto IL_33D;
			default:
				if (missionEvent == 500)
				{
					base.PlaySound(BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Attack_01, 1f, true, false);
					goto IL_33D;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_08.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_VictoryA_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_33D;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_Lines);
				goto IL_33D;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_33D:
		yield break;
	}

	// Token: 0x0600440E RID: 17422 RVA: 0x00170C4E File Offset: 0x0016EE4E
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
		if (cardId == "TRL_156")
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_StolenSteel_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600440F RID: 17423 RVA: 0x00170C64 File Offset: 0x0016EE64
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
		if (cardId == "BT_117")
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Bladestorm_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004410 RID: 17424 RVA: 0x00170C7A File Offset: 0x0016EE7A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_08.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_Illidans_Challenge_01, 2.5f);
			break;
		case 3:
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_08.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_08_TurnOneBrassRing_01, 2.5f);
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_08.VO_BTA_03_Female_Broken_Mission_Fight_08_TurnOneBrassRing_02, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_08.VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesA_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04003646 RID: 13894
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_08.InitBooleanOptions();

	// Token: 0x04003647 RID: 13895
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesA_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesA_01.prefab:09fe292b6a1b94b428adfbe9db8809c5");

	// Token: 0x04003648 RID: 13896
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesB_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_08_Baduu_LeavesB_01.prefab:9e98b1db22ef8294792a8bb7102ae810");

	// Token: 0x04003649 RID: 13897
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_08_Baduu_LeavesC_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_08_Baduu_LeavesC_01.prefab:4a3c1e53c527a3146a9476b393e5f983");

	// Token: 0x0400364A RID: 13898
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_08_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_08_PlayerStart_01.prefab:9efa7a47b4707e44e9d6ef375738d65c");

	// Token: 0x0400364B RID: 13899
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_08_Baduu_LeavesD_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_08_Baduu_LeavesD_01.prefab:b32f4880489990646ac268acf7848bad");

	// Token: 0x0400364C RID: 13900
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_08_Baduu_LeavesE_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_08_Baduu_LeavesE_01.prefab:ef97d0921d3ecb243b90874aaaeffb66");

	// Token: 0x0400364D RID: 13901
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Attack_01.prefab:c5202afd72c29ae48b1872f7d3439c90");

	// Token: 0x0400364E RID: 13902
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Bladestorm_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_Bladestorm_01.prefab:21ac1b9cd0ea61e49b4173d194b0549c");

	// Token: 0x0400364F RID: 13903
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_WeaponEquip_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Boss_WeaponEquip_01.prefab:38a7b80155a260041aaa9c35c5c94afe");

	// Token: 0x04003650 RID: 13904
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossDeathA_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossDeathA_01.prefab:d9fc0fc33abebcb4dbd141f919d1aaf8");

	// Token: 0x04003651 RID: 13905
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossDeathB_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossDeathB_01.prefab:147fa644024309242a993347c2c40f69");

	// Token: 0x04003652 RID: 13906
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossStart_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_BossStart_01.prefab:20aa52e8516ccf840ab33041a1b6b156");

	// Token: 0x04003653 RID: 13907
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Emote_Response_01.prefab:27ac88d8e363d3d48b760fc63670547f");

	// Token: 0x04003654 RID: 13908
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_ScavengersCunningTrigger_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_ScavengersCunningTrigger_01.prefab:a590375c66d72ee4a80667cf33cd835f");

	// Token: 0x04003655 RID: 13909
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_StolenSteel_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_StolenSteel_01.prefab:f1f91f8e6ac14224989bd6aaba134f56");

	// Token: 0x04003656 RID: 13910
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_WeaponEquip_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_Hero_WeaponEquip_01.prefab:5ba775ab6ec34014886b35bd244db8fe");

	// Token: 0x04003657 RID: 13911
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_01.prefab:97d67acd04e9a2f4a9b23ce157c6319a");

	// Token: 0x04003658 RID: 13912
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_02 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_02.prefab:b81ebfcf3f854a445b79910b9036433f");

	// Token: 0x04003659 RID: 13913
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_03 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_03.prefab:a1ebf1a8089d55847b53527e8e4a9fa6");

	// Token: 0x0400365A RID: 13914
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_04 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_04.prefab:8aef94c85e692ce4882f7f28319b29d1");

	// Token: 0x0400365B RID: 13915
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleA_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleA_01.prefab:3bda4a141204bd04cb4f998fd2c1aa5f");

	// Token: 0x0400365C RID: 13916
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleB_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleB_01.prefab:cb997a4b6e649ba42b8f7eacb40e1db6");

	// Token: 0x0400365D RID: 13917
	private static readonly AssetReference VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleC_01 = new AssetReference("VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleC_01.prefab:7589621788aab454fbe8152adf9985e4");

	// Token: 0x0400365E RID: 13918
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_08_TurnOneBrassRing_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_08_TurnOneBrassRing_01.prefab:451dc64e05ca02943b20628e58626815");

	// Token: 0x0400365F RID: 13919
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_Illidans_Challenge_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_Illidans_Challenge_01.prefab:ceae815f01901664aa8eea462fec6677");

	// Token: 0x04003660 RID: 13920
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_VictoryA_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_08_VictoryA_01.prefab:a08cc7c7b46c59840b966ccd1ce5b436");

	// Token: 0x04003661 RID: 13921
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_08_TurnOneBrassRing_02 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_08_TurnOneBrassRing_02.prefab:ccbd1a293888a554b96911496dec8b11");

	// Token: 0x04003662 RID: 13922
	private List<string> m_VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_Lines = new List<string>
	{
		BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_01,
		BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_02,
		BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_03,
		BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_HeroPower_04
	};

	// Token: 0x04003663 RID: 13923
	private List<string> m_VO_BTA_BOSS_08h_IdleLines = new List<string>
	{
		BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleA_01,
		BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleB_01,
		BTA_Fight_08.VO_BTA_BOSS_08h_Female_Demon_Mission_Fight_08_IdleC_01
	};

	// Token: 0x04003664 RID: 13924
	private HashSet<string> m_playedLines = new HashSet<string>();
}
