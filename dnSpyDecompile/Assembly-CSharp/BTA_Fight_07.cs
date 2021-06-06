using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
public class BTA_Fight_07 : BTA_Dungeon
{
	// Token: 0x060043EF RID: 17391 RVA: 0x0010C851 File Offset: 0x0010AA51
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.HANDLE_COIN,
				false
			}
		};
	}

	// Token: 0x060043F0 RID: 17392 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x060043F1 RID: 17393 RVA: 0x0016FFE4 File Offset: 0x0016E1E4
	public BTA_Fight_07()
	{
		this.m_gameOptions.AddOptions(BTA_Fight_07.s_booleanOptions, BTA_Fight_07.s_stringOptions);
	}

	// Token: 0x060043F2 RID: 17394 RVA: 0x001701B0 File Offset: 0x0016E3B0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_07.VO_BTA_03_Female_Broken_Mission_Fight_07_Misc_01,
			BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_Misc_02,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Misc_03,
			BTA_Fight_07.VO_BTA_05_Male_Sporelok_Mission_Fight_07_Hero_SelfRepairsReply_01,
			BTA_Fight_07.VO_BTA_05_Male_Sporelok_Mission_Fight_07_VictoryB_01,
			BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_BrassRing_TurboBoostReply_01,
			BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannons_02,
			BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_01,
			BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_02,
			BTA_Fight_07.VO_BTA_09_Female_Naga_Mission_Fight_07_BrassRing_Refueling_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_02,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_03,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_02,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_02,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_03,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_02,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_03,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_02,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_03,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_04,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_PlayerStart_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_01,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_02,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_03,
			BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_VictoryA_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060043F3 RID: 17395 RVA: 0x0016D56A File Offset: 0x0016B76A
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x060043F4 RID: 17396 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060043F5 RID: 17397 RVA: 0x001703F4 File Offset: 0x0016E5F4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType);
	}

	// Token: 0x060043F6 RID: 17398 RVA: 0x00170451 File Offset: 0x0016E651
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayRandomLineAlways(actor, this.m_missionEventTrigger101_Lines);
			goto IL_377;
		case 102:
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_BrassRing_TurboBoostReply_01, 2.5f);
			goto IL_377;
		case 103:
			if (UnityEngine.Random.Range(1, 4) == 1)
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_03, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_07.VO_BTA_09_Female_Naga_Mission_Fight_07_BrassRing_Refueling_01, 2.5f);
				goto IL_377;
			}
			yield return base.PlayRandomLineAlways(actor, this.m_missionEventTrigger103_Lines);
			goto IL_377;
		case 104:
		case 105:
			break;
		case 106:
			yield return base.PlayLineAlways(actor, BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Misc_03, 2.5f);
			goto IL_377;
		case 107:
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_07.VO_BTA_03_Female_Broken_Mission_Fight_07_Misc_01, 2.5f);
			goto IL_377;
		case 108:
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_Misc_02, 2.5f);
			goto IL_377;
		default:
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_VictoryA_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_05"), BTA_Dungeon.SklibbBrassRing, BTA_Fight_07.VO_BTA_05_Male_Sporelok_Mission_Fight_07_VictoryB_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_377;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_Lines);
				goto IL_377;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_377:
		yield break;
	}

	// Token: 0x060043F7 RID: 17399 RVA: 0x00170467 File Offset: 0x0016E667
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BTA_BOSS_07s2"))
		{
			if (!(cardId == "BTA_BOSS_07s3"))
			{
				if (cardId == "BTA_BOSS_07s4")
				{
					if (UnityEngine.Random.Range(1, 4) == 1)
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_03, 2.5f);
						yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_05"), BTA_Dungeon.SklibbBrassRing, BTA_Fight_07.VO_BTA_05_Male_Sporelok_Mission_Fight_07_Hero_SelfRepairsReply_01, 2.5f);
					}
					else
					{
						yield return base.PlayRandomLineAlways(actor, this.m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_Lines);
					}
				}
			}
			else
			{
				yield return base.PlayRandomLineAlways(actor, this.m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_Lines);
				yield return base.PlayAndRemoveRandomLineOnlyOnceWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, this.m_VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_Lines);
			}
		}
		else
		{
			yield return base.PlayRandomLineAlways(actor, this.m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_Lines);
		}
		yield break;
	}

	// Token: 0x060043F8 RID: 17400 RVA: 0x0017047D File Offset: 0x0016E67D
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

	// Token: 0x060043F9 RID: 17401 RVA: 0x00170493 File Offset: 0x0016E693
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

	// Token: 0x060043FA RID: 17402 RVA: 0x001704A9 File Offset: 0x0016E6A9
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		this.InitVisuals();
	}

	// Token: 0x060043FB RID: 17403 RVA: 0x001704B8 File Offset: 0x0016E6B8
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitTurnCounter(cost);
	}

	// Token: 0x060043FC RID: 17404 RVA: 0x001704D4 File Offset: 0x0016E6D4
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x060043FD RID: 17405 RVA: 0x00170510 File Offset: 0x0016E710
	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("BTA_Destroyer_Turn_Timer.prefab:5641f96ff71c06c4e8416952d878e3c7", AssetLoadingOptions.None);
		this.m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = this.m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = false;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = false;
		component.FsmVariables.GetFsmBool("Destroyer").Value = true;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_turnCounter.transform.parent = actor.gameObject.transform;
		this.m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		this.m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x060043FE RID: 17406 RVA: 0x00170626 File Offset: 0x0016E826
	private void UpdateVisuals(int cost)
	{
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x060043FF RID: 17407 RVA: 0x00170630 File Offset: 0x0016E830
	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_mineCartArt.DoPortraitSwap(actor);
	}

	// Token: 0x06004400 RID: 17408 RVA: 0x0017065E File Offset: 0x0016E85E
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x06004401 RID: 17409 RVA: 0x0017067C File Offset: 0x0016E87C
	private void UpdateTurnCounterText(int cost)
	{
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = cost
			}
		};
		string headlineString = GameStrings.FormatPlurals("MISSION_DEFAULTCOUNTERNAME", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x0400361B RID: 13851
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_07.InitBooleanOptions();

	// Token: 0x0400361C RID: 13852
	private static Map<GameEntityOption, string> s_stringOptions = BTA_Fight_07.InitStringOptions();

	// Token: 0x0400361D RID: 13853
	private static readonly AssetReference VO_BTA_05_Male_Sporelok_Mission_Fight_07_Hero_SelfRepairsReply_01 = new AssetReference("VO_BTA_05_Male_Sporelok_Mission_Fight_07_Hero_SelfRepairsReply_01.prefab:4017403984d282041a161261b674aa2f");

	// Token: 0x0400361E RID: 13854
	private static readonly AssetReference VO_BTA_05_Male_Sporelok_Mission_Fight_07_VictoryB_01 = new AssetReference("VO_BTA_05_Male_Sporelok_Mission_Fight_07_VictoryB_01.prefab:c51713e191ee2184493bbab10e8ab8ba");

	// Token: 0x0400361F RID: 13855
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_07_BrassRing_TurboBoostReply_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_07_BrassRing_TurboBoostReply_01.prefab:6ab4ecb2ab304e4488143a38ac6f8dbb");

	// Token: 0x04003620 RID: 13856
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannons_02 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannons_02.prefab:c15127a0157c203439fece5e4b3a4e2f");

	// Token: 0x04003621 RID: 13857
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_01.prefab:84137aaf5131fe24a880e295f270a5aa");

	// Token: 0x04003622 RID: 13858
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_02 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_02.prefab:0a2c2580a5f0074488f82a54b9cc3a55");

	// Token: 0x04003623 RID: 13859
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_07_Misc_02 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_07_Misc_02.prefab:572d1de818e589f4f9043d9929bad47f");

	// Token: 0x04003624 RID: 13860
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_07_BrassRing_Refueling_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_07_BrassRing_Refueling_01.prefab:0a50f58e6466a3f4a832cb778dd97144");

	// Token: 0x04003625 RID: 13861
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_01.prefab:1bca6ee372b507b40a04e2c9f0b26bb7");

	// Token: 0x04003626 RID: 13862
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_02 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_02.prefab:7caab9d6af7df6a47af7570ec903202d");

	// Token: 0x04003627 RID: 13863
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_03 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_03.prefab:04873e433a5619a4ea3c26bcc6d98024");

	// Token: 0x04003628 RID: 13864
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_01.prefab:9b1c350c6ab01824ebc72aba0ffca8f6");

	// Token: 0x04003629 RID: 13865
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_02 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_02.prefab:95055fee3ad738b44a48534879b8585d");

	// Token: 0x0400362A RID: 13866
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_01.prefab:baf60ad5b99e596488e33dd0efa5c4f2");

	// Token: 0x0400362B RID: 13867
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_02 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_02.prefab:7517f5bf99c78f646b151ace0e56c7f2");

	// Token: 0x0400362C RID: 13868
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_03 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_03.prefab:c8e2df79b70087c499b3fbac46102400");

	// Token: 0x0400362D RID: 13869
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_01.prefab:6959ec06d9fce674399e88e0c27de165");

	// Token: 0x0400362E RID: 13870
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_02 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_02.prefab:3c6fc137bfe16db44b55a7d96367a36c");

	// Token: 0x0400362F RID: 13871
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_03 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_03.prefab:1fe4fed3bcf786b4789a3b98f6c4716b");

	// Token: 0x04003630 RID: 13872
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_01.prefab:7a7283343530d134eb88522f9214240d");

	// Token: 0x04003631 RID: 13873
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_02 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_02.prefab:e2f1c7e8866649e42acf265e3e6df3b7");

	// Token: 0x04003632 RID: 13874
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_03 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_03.prefab:0a1772545ff208946969653626be596e");

	// Token: 0x04003633 RID: 13875
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_04 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_04.prefab:443d4dec43022954ebf6231f0f124957");

	// Token: 0x04003634 RID: 13876
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Misc_03 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Misc_03.prefab:eff630d1ff4e877448bf249d3097165c");

	// Token: 0x04003635 RID: 13877
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_PlayerStart_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_PlayerStart_01.prefab:616c9e57bb7fce54684e26be50462d17");

	// Token: 0x04003636 RID: 13878
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_01.prefab:e7f979d7c3d0a6748aae01e75b588fc7");

	// Token: 0x04003637 RID: 13879
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_02 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_02.prefab:e56d157cb67db4e4097487bdb61f4d41");

	// Token: 0x04003638 RID: 13880
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_03 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_03.prefab:d87d224d117aa8d449e0080d7d1afeef");

	// Token: 0x04003639 RID: 13881
	private static readonly AssetReference VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_VictoryA_01 = new AssetReference("VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_VictoryA_01.prefab:06093d41aab91bf4ca4322c5189ba5a5");

	// Token: 0x0400363A RID: 13882
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_07_Misc_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_07_Misc_01.prefab:b9a7f440e929f954a90bb76c906e8346");

	// Token: 0x0400363B RID: 13883
	private List<string> m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_01,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_FelCannons_02
	};

	// Token: 0x0400363C RID: 13884
	private List<string> m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_01,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_SelfRepairs_02
	};

	// Token: 0x0400363D RID: 13885
	private List<string> m_VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_01,
		BTA_Fight_07.VO_BTA_07_Male_Orc_Mission_Fight_07_Hero_FelCannonsReply_02
	};

	// Token: 0x0400363E RID: 13886
	private List<string> m_missionEventTrigger501_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_05_Male_Sporelok_Mission_Fight_07_VictoryB_01,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_VictoryA_01
	};

	// Token: 0x0400363F RID: 13887
	private List<string> m_missionEventTrigger103_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_01,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_Refueling_02
	};

	// Token: 0x04003640 RID: 13888
	private List<string> m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_01,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_02,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_Hero_ExhaustBackfire_03
	};

	// Token: 0x04003641 RID: 13889
	private List<string> m_VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_01,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_02,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_03,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_HeroPower_04
	};

	// Token: 0x04003642 RID: 13890
	private List<string> m_missionEventTrigger101_Lines = new List<string>
	{
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_01,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_02,
		BTA_Fight_07.VO_BTA_BOSS_07h2_Female_NightElf_Mission_Fight_07_TurboBoost_03
	};

	// Token: 0x04003643 RID: 13891
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04003644 RID: 13892
	private Notification m_turnCounter;

	// Token: 0x04003645 RID: 13893
	private MineCartRushArt m_mineCartArt;
}
