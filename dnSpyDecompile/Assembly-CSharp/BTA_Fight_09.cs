using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004F3 RID: 1267
public class BTA_Fight_09 : BTA_Dungeon
{
	// Token: 0x06004415 RID: 17429 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004416 RID: 17430 RVA: 0x00170E3C File Offset: 0x0016F03C
	public BTA_Fight_09()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_09.s_booleanOptions);
	}

	// Token: 0x06004417 RID: 17431 RVA: 0x00170F0C File Offset: 0x0016F10C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_09_Hero_HeroPower_02,
			BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_09_PlayerStart_01,
			BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_09_VictoryB_01,
			BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_09_VictoryC_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Attack_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_ConchguardWarlord_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_SerpentShrinePortal_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Torrent_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_WrathscaleNaga_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_BossDeath_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_BossStart_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Emote_Response_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_Blur_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_CoordinatedStrike_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_TwinSlice_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_02,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_03,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_04,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleA_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleB_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleC_01,
			BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_VictoryA_01,
			BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_A_01,
			BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_B_01,
			BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_C_01,
			BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_01A_01,
			BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_01B_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004418 RID: 17432 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004419 RID: 17433 RVA: 0x00171130 File Offset: 0x0016F330
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_09h_IdleLines;
	}

	// Token: 0x0600441A RID: 17434 RVA: 0x00171138 File Offset: 0x0016F338
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_Lines;
	}

	// Token: 0x0600441B RID: 17435 RVA: 0x0016D56A File Offset: 0x0016B76A
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x0600441C RID: 17436 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600441D RID: 17437 RVA: 0x00171140 File Offset: 0x0016F340
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_09_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600441E RID: 17438 RVA: 0x00171150 File Offset: 0x0016F350
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600441F RID: 17439 RVA: 0x001711D8 File Offset: 0x0016F3D8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 500)
		{
			if (missionEvent == 100)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_VictoryA_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_1EE;
			}
			if (missionEvent == 101)
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_ConchguardWarlord_01, 2.5f);
				goto IL_1EE;
			}
			if (missionEvent == 500)
			{
				base.PlaySound(BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Attack_01, 1f, true, false);
				goto IL_1EE;
			}
		}
		else
		{
			if (missionEvent == 501)
			{
				this.m_DisableIdle = true;
				goto IL_1EE;
			}
			if (missionEvent == 505)
			{
				yield return base.PlayRandomLineAlways(actor2, this.m_missionEventTrigger101_Lines);
				goto IL_1EE;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayRandomLineAlways(actor, this.m_VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_Lines);
				goto IL_1EE;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1EE:
		yield break;
	}

	// Token: 0x06004420 RID: 17440 RVA: 0x001711EE File Offset: 0x0016F3EE
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
		if (!(cardId == "BT_036"))
		{
			if (!(cardId == "BT_175"))
			{
				if (cardId == "BT_752")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_Blur_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_TwinSlice_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_CoordinatedStrike_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004421 RID: 17441 RVA: 0x00171204 File Offset: 0x0016F404
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
		if (!(cardId == "BT_100"))
		{
			if (!(cardId == "BT_110"))
			{
				if (!(cardId == "BT_355"))
				{
					if (cardId == "BT_761t")
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_ConchguardWarlord_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_WrathscaleNaga_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Torrent_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_SerpentShrinePortal_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004422 RID: 17442 RVA: 0x0017121A File Offset: 0x0016F41A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 5)
		{
			if (turn != 1)
			{
				if (turn == 5)
				{
					yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_A_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_01A_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_01B_01, 2.5f);
			}
		}
		else if (turn != 9)
		{
			if (turn == 13)
			{
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_C_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_09.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_B_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003665 RID: 13925
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_09.InitBooleanOptions();

	// Token: 0x04003666 RID: 13926
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_09_Hero_HeroPower_02 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_09_Hero_HeroPower_02.prefab:c82d3b7dbe014c645be61ffcd2c1d3a1");

	// Token: 0x04003667 RID: 13927
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_09_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_09_PlayerStart_01.prefab:1c96ac5f4a472254d89c5f5458c156d2");

	// Token: 0x04003668 RID: 13928
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_09_VictoryB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_09_VictoryB_01.prefab:256ee2d4247107c49ac0ad8ea19433ec");

	// Token: 0x04003669 RID: 13929
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_09_VictoryC_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_09_VictoryC_01.prefab:5cafe1b87d62a0347911a17d8f95630b");

	// Token: 0x0400366A RID: 13930
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Attack_01.prefab:dc3d7c77c3b005841ad0df25740b93e3");

	// Token: 0x0400366B RID: 13931
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_ConchguardWarlord_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_ConchguardWarlord_01.prefab:3267260319c4caf4d9cb241acf9a1de9");

	// Token: 0x0400366C RID: 13932
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_SerpentShrinePortal_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_SerpentShrinePortal_01.prefab:7dd9fae21b7c68146843a3ec35063bf6");

	// Token: 0x0400366D RID: 13933
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Torrent_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_Torrent_01.prefab:c7a9c52a826a3534093d2d9503e88580");

	// Token: 0x0400366E RID: 13934
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_WrathscaleNaga_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Boss_WrathscaleNaga_01.prefab:4d5ed87abba2dde41a7e0dec2cae7861");

	// Token: 0x0400366F RID: 13935
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_BossDeath_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_BossDeath_01.prefab:3f5731086b9af044797663a86ff45608");

	// Token: 0x04003670 RID: 13936
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_BossStart_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_BossStart_01.prefab:2bd3c4cbead065444ba9bee77bc4897a");

	// Token: 0x04003671 RID: 13937
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Emote_Response_01.prefab:0a1297a92926faf4b8be06fdb1548cfc");

	// Token: 0x04003672 RID: 13938
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_Blur_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_Blur_01.prefab:8e6a29c38451174448e3813ca771c499");

	// Token: 0x04003673 RID: 13939
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_CoordinatedStrike_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_CoordinatedStrike_01.prefab:b59d0cf2e7fda1a499f5e0022acb6823");

	// Token: 0x04003674 RID: 13940
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_TwinSlice_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_Hero_TwinSlice_01.prefab:6c11f1ac9f43e544eb9935ec9ce46de3");

	// Token: 0x04003675 RID: 13941
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_01.prefab:ba5088602273fc645813ee38a14e0640");

	// Token: 0x04003676 RID: 13942
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_02 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_02.prefab:3a8618191e6cae14b89343d8ea437cd3");

	// Token: 0x04003677 RID: 13943
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_03 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_03.prefab:11757f385d542bb4a85a38a9d42b3f83");

	// Token: 0x04003678 RID: 13944
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_04 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_04.prefab:acc821a837c199848b732cc6c5a436d5");

	// Token: 0x04003679 RID: 13945
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleA_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleA_01.prefab:d70407fa52d318b458c1b8c5ba323b31");

	// Token: 0x0400367A RID: 13946
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleB_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleB_01.prefab:d256b1b5035d76848a3ecee818b7a3db");

	// Token: 0x0400367B RID: 13947
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleC_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleC_01.prefab:c4ac31fbad6745848a793343717d030b");

	// Token: 0x0400367C RID: 13948
	private static readonly AssetReference VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_VictoryA_01 = new AssetReference("VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_VictoryA_01.prefab:9d9d95cee3244d0489683f2c567eba95");

	// Token: 0x0400367D RID: 13949
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_A_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_A_01.prefab:da13367489516a84f975bcf2a83ec610");

	// Token: 0x0400367E RID: 13950
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_B_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_B_01.prefab:0618987063f9b19498f892ea34e6431c");

	// Token: 0x0400367F RID: 13951
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_C_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_09_Illidan_Coaching_C_01.prefab:2a533cc710278b94895fb3ebec0d619c");

	// Token: 0x04003680 RID: 13952
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_01B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_01B_01.prefab:92fbd307bb421b242b08ea8bf9f282f2");

	// Token: 0x04003681 RID: 13953
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_01A_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_01A_01.prefab:a79ed8c6862640c49ab65db8455e3dbb");

	// Token: 0x04003682 RID: 13954
	private List<string> m_VO_BTA_BOSS_09h_IdleLines = new List<string>
	{
		BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleA_01,
		BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleB_01,
		BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_IdleC_01
	};

	// Token: 0x04003683 RID: 13955
	private List<string> m_missionEventTrigger101_Lines = new List<string>
	{
		BTA_Fight_09.VO_BTA_01_Female_NightElf_Mission_Fight_09_Hero_HeroPower_02
	};

	// Token: 0x04003684 RID: 13956
	private List<string> m_VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_Lines = new List<string>
	{
		BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_01,
		BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_02,
		BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_03,
		BTA_Fight_09.VO_BTA_BOSS_09h_Female_Naga_Mission_Fight_09_HeroPower_04
	};

	// Token: 0x04003685 RID: 13957
	private HashSet<string> m_playedLines = new HashSet<string>();
}
