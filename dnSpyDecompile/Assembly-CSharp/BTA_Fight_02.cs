using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004EC RID: 1260
public class BTA_Fight_02 : BTA_Dungeon
{
	// Token: 0x06004394 RID: 17300 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004395 RID: 17301 RVA: 0x0016E408 File Offset: 0x0016C608
	public BTA_Fight_02()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_02.s_booleanOptions);
	}

	// Token: 0x06004396 RID: 17302 RVA: 0x0016E4AC File Offset: 0x0016C6AC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_02.VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01,
			BTA_Fight_02.VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01,
			BTA_Fight_02.VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01,
			BTA_Fight_02.VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01,
			BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01,
			BTA_Fight_02.VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004397 RID: 17303 RVA: 0x0016E680 File Offset: 0x0016C880
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_02h_IdleLines;
	}

	// Token: 0x06004398 RID: 17304 RVA: 0x0016E688 File Offset: 0x0016C888
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01;
	}

	// Token: 0x06004399 RID: 17305 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600439A RID: 17306 RVA: 0x0016E6A0 File Offset: 0x0016C8A0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600439B RID: 17307 RVA: 0x0016E6B0 File Offset: 0x0016C8B0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600439C RID: 17308 RVA: 0x0016E738 File Offset: 0x0016C938
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01, 2.5f);
			goto IL_266;
		case 101:
			yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01, 2.5f);
			goto IL_266;
		case 102:
			break;
		case 103:
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_02.VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01, 2.5f);
			goto IL_266;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_Lines);
			goto IL_266;
		default:
			if (missionEvent == 500)
			{
				base.PlaySound(BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01, 1f, true, false);
				goto IL_266;
			}
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_02.VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_02.VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_266;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_266:
		yield break;
	}

	// Token: 0x0600439D RID: 17309 RVA: 0x0016E74E File Offset: 0x0016C94E
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
		if (!(cardId == "BT_205"))
		{
			if (cardId == "BT_213")
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600439E RID: 17310 RVA: 0x0016E764 File Offset: 0x0016C964
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
		if (!(cardId == "BT_008"))
		{
			if (!(cardId == "BTA_13"))
			{
				if (cardId == "BT_156")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600439F RID: 17311 RVA: 0x0016E77A File Offset: 0x0016C97A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1)
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_02.VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400358B RID: 13707
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_02.InitBooleanOptions();

	// Token: 0x0400358C RID: 13708
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01.prefab:e898b0501f4775946b45e36df0866d2c");

	// Token: 0x0400358D RID: 13709
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01.prefab:b138bd177c46bff408d35429f20225b1");

	// Token: 0x0400358E RID: 13710
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01.prefab:ebf5ee5fa99b59346a22f06c02259d59");

	// Token: 0x0400358F RID: 13711
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01.prefab:2216e4de765f8934d828057e33368a80");

	// Token: 0x04003590 RID: 13712
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01.prefab:f7036b4d57a6ab841a3f7c435ae9aa71");

	// Token: 0x04003591 RID: 13713
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01.prefab:e20013fc04519e247a6565196df2a807");

	// Token: 0x04003592 RID: 13714
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01.prefab:c5cce735e02941443b4bba89e4b6ca41");

	// Token: 0x04003593 RID: 13715
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01.prefab:51455cd023371ba4cbef5b1d7b6922be");

	// Token: 0x04003594 RID: 13716
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01.prefab:e1be88dd306bb62428226b4036abd8f8");

	// Token: 0x04003595 RID: 13717
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01.prefab:32f3fccae85cdda439d8c51176e0d56b");

	// Token: 0x04003596 RID: 13718
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01.prefab:d7025c683be7ad846938c3064d76d94c");

	// Token: 0x04003597 RID: 13719
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01.prefab:290f1670e64f3bf43b9747159fb87dce");

	// Token: 0x04003598 RID: 13720
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01.prefab:de3d15bd885801a43bf61272476c90b3");

	// Token: 0x04003599 RID: 13721
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01.prefab:03210da823f75eb47b94594386315846");

	// Token: 0x0400359A RID: 13722
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01.prefab:35e8ecb028ff8ac45b53de4c71df333c");

	// Token: 0x0400359B RID: 13723
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02.prefab:3dafb16092279f54e972b371de877630");

	// Token: 0x0400359C RID: 13724
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03.prefab:121cc3da7ebfd20418590b6fae0c82aa");

	// Token: 0x0400359D RID: 13725
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04.prefab:95cb66ddfad77604db12084d6e354cf0");

	// Token: 0x0400359E RID: 13726
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01.prefab:f9cc9fd6f2f032945a9a967611bcfbbd");

	// Token: 0x0400359F RID: 13727
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01.prefab:4a195d306e1d1f448a2930fdfef61a68");

	// Token: 0x040035A0 RID: 13728
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01.prefab:5bcc1a551dfcd1f4f9ef580b66ae9422");

	// Token: 0x040035A1 RID: 13729
	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01.prefab:868ebb9c850bd6b4cb8f6b01cbb11f71");

	// Token: 0x040035A2 RID: 13730
	private static readonly AssetReference VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01 = new AssetReference("VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01.prefab:bbdf5fec759200b40a01c3a1e8da80cd");

	// Token: 0x040035A3 RID: 13731
	private List<string> m_VO_BTA_BOSS_02h_IdleLines = new List<string>
	{
		BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01,
		BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01,
		BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01
	};

	// Token: 0x040035A4 RID: 13732
	private List<string> m_VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_Lines = new List<string>
	{
		BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02,
		BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03,
		BTA_Fight_02.VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04
	};

	// Token: 0x040035A5 RID: 13733
	private HashSet<string> m_playedLines = new HashSet<string>();
}
