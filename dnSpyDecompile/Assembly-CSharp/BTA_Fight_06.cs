using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F0 RID: 1264
public class BTA_Fight_06 : BTA_Dungeon
{
	// Token: 0x060043D5 RID: 17365 RVA: 0x0016F7D4 File Offset: 0x0016D9D4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_06.VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01,
			BTA_Fight_06.VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01,
			BTA_Fight_06.VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01,
			BTA_Fight_06.VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01,
			BTA_Fight_06.VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01,
			BTA_Fight_06.VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01,
			BTA_Fight_06.VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01,
			BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01,
			BTA_Fight_06.BTA_BOSS_06t_BrokenDemolisher_StartUp
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060043D6 RID: 17366 RVA: 0x0016FA08 File Offset: 0x0016DC08
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_06h_IdleLines;
	}

	// Token: 0x060043D7 RID: 17367 RVA: 0x0016FA10 File Offset: 0x0016DC10
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_Lines;
	}

	// Token: 0x060043D8 RID: 17368 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060043D9 RID: 17369 RVA: 0x0016FA18 File Offset: 0x0016DC18
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01;
	}

	// Token: 0x060043DA RID: 17370 RVA: 0x0016FA30 File Offset: 0x0016DC30
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060043DB RID: 17371 RVA: 0x0016FA40 File Offset: 0x0016DC40
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060043DC RID: 17372 RVA: 0x0016FAC8 File Offset: 0x0016DCC8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 500)
		{
			switch (missionEvent)
			{
			case 101:
				yield return base.PlayLineAlways(enemyActor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01, 2.5f);
				goto IL_30E;
			case 102:
				yield return base.PlayLineAlways(enemyActor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01, 2.5f);
				goto IL_30E;
			case 103:
				yield return base.PlayLineAlways(enemyActor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01, 2.5f);
				goto IL_30E;
			default:
				if (missionEvent == 500)
				{
					yield return base.PlayLineAlways(enemyActor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01, 2.5f);
					goto IL_30E;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				base.PlaySound(BTA_Fight_06.BTA_BOSS_06t_BrokenDemolisher_StartUp, 1f, true, false);
				yield return new WaitForSeconds(4.3f);
				yield return base.PlayLineAlways(enemyActor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_06.VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_06.VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_30E;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_Lines);
				goto IL_30E;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_30E:
		yield break;
	}

	// Token: 0x060043DD RID: 17373 RVA: 0x0016FADE File Offset: 0x0016DCDE
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
		if (!(cardId == "BT_717"))
		{
			if (!(cardId == "BT_202"))
			{
				if (!(cardId == "BTA_03"))
				{
					if (!(cardId == "BTA_05"))
					{
						if (!(cardId == "BTA_07"))
						{
							if (cardId == "BTA_09")
							{
								yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, BTA_Fight_06.VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_06.VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, BTA_Fight_06.VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043DE RID: 17374 RVA: 0x0016FAF4 File Offset: 0x0016DCF4
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
		if (!(cardId == "GVG_083"))
		{
			if (!(cardId == "BOT_700"))
			{
				if (!(cardId == "GVG_118"))
				{
					if (!(cardId == "CFM_688"))
					{
						if (cardId == "DAL_376")
						{
							yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043DF RID: 17375 RVA: 0x0016FB0A File Offset: 0x0016DD0A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1)
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_06.VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043E0 RID: 17376 RVA: 0x0016FB20 File Offset: 0x0016DD20
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		this.InitVisuals();
	}

	// Token: 0x060043E1 RID: 17377 RVA: 0x0016FB30 File Offset: 0x0016DD30
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitTurnCounter(cost);
	}

	// Token: 0x060043E2 RID: 17378 RVA: 0x0016FB4C File Offset: 0x0016DD4C
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x060043E3 RID: 17379 RVA: 0x0016FB88 File Offset: 0x0016DD88
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

	// Token: 0x060043E4 RID: 17380 RVA: 0x0016FC9E File Offset: 0x0016DE9E
	private void UpdateVisuals(int cost)
	{
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x060043E5 RID: 17381 RVA: 0x0016FCA8 File Offset: 0x0016DEA8
	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_mineCartArt.DoPortraitSwap(actor);
	}

	// Token: 0x060043E6 RID: 17382 RVA: 0x0016FCD6 File Offset: 0x0016DED6
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x060043E7 RID: 17383 RVA: 0x0016FCF4 File Offset: 0x0016DEF4
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
		string headlineString = GameStrings.FormatPlurals("MISSION_SPAREPARTSCOUNTERNAME", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x060043E8 RID: 17384 RVA: 0x0016FD4C File Offset: 0x0016DF4C
	public BTA_Fight_06()
	{
		this.m_gameOptions.AddOptions(BTA_Fight_06.s_booleanOptions, BTA_Fight_06.s_stringOptions);
	}

	// Token: 0x060043E9 RID: 17385 RVA: 0x0016FDF5 File Offset: 0x0016DFF5
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.HANDLE_COIN,
				false
			},
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x060043EA RID: 17386 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x040035F7 RID: 13815
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01.prefab:358ae7b7d1e21db46979a72c62aa619f");

	// Token: 0x040035F8 RID: 13816
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01.prefab:9e956cb17d1ef204ea467a03cd28b757");

	// Token: 0x040035F9 RID: 13817
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01.prefab:cc9681b98a543074a811882afb1172c1");

	// Token: 0x040035FA RID: 13818
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01.prefab:69622c1091bdb184aa81eefbe18cb581");

	// Token: 0x040035FB RID: 13819
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01.prefab:c6129540143b14141a64ff2b0a8279d0");

	// Token: 0x040035FC RID: 13820
	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01.prefab:d9d6a8bee1c10d84cb5cd7bc9e862fe8");

	// Token: 0x040035FD RID: 13821
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01.prefab:40687aba7ea96a34ea985ff218daca3a");

	// Token: 0x040035FE RID: 13822
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01.prefab:1e283f08c14dc314ab5ca8d77714ce6d");

	// Token: 0x040035FF RID: 13823
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01.prefab:c2cd59e7c1510c148ba7a5cb4a6769ed");

	// Token: 0x04003600 RID: 13824
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01.prefab:27228471690f3fe40a6f84659bb51909");

	// Token: 0x04003601 RID: 13825
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01.prefab:1f0fca7db75781145aa9485a7001293b");

	// Token: 0x04003602 RID: 13826
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01.prefab:022162e98a357df4aa3b7219f3af2533");

	// Token: 0x04003603 RID: 13827
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01.prefab:ea66fcda4798e664984bc02a820704d7");

	// Token: 0x04003604 RID: 13828
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01.prefab:ad8fe5800b0ee4741a5eb2cf5dba6ff9");

	// Token: 0x04003605 RID: 13829
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01.prefab:20c01d309c172ad4ea499a14579a6573");

	// Token: 0x04003606 RID: 13830
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01.prefab:15cb51a075d0da040bfc4652ef69048d");

	// Token: 0x04003607 RID: 13831
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01.prefab:47e987a79ff628f49b1910c4a55e3000");

	// Token: 0x04003608 RID: 13832
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01.prefab:117b2c00284d4e04d811289cc0eb641c");

	// Token: 0x04003609 RID: 13833
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01.prefab:f629efd6fbdb97b44b2e8abbb9b2f826");

	// Token: 0x0400360A RID: 13834
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01.prefab:b966e1d7fc4128248aa449c7de2c6baa");

	// Token: 0x0400360B RID: 13835
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01.prefab:6ca9a92ac6272e540804624d0dc9612f");

	// Token: 0x0400360C RID: 13836
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02.prefab:7ef624fd693d6fc41a9bfe6ab2c2f468");

	// Token: 0x0400360D RID: 13837
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03.prefab:80f5fa7bddbc124438247d0b0803b32f");

	// Token: 0x0400360E RID: 13838
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04.prefab:f74c625cdbea0024f9bd37a958b91d30");

	// Token: 0x0400360F RID: 13839
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01.prefab:b965da4684bf5bc4ba0d3d2256eb5d3c");

	// Token: 0x04003610 RID: 13840
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01.prefab:09e1e66fa0ccae34f80a3e36b9e25d35");

	// Token: 0x04003611 RID: 13841
	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01.prefab:77ad9b70a1674e849974ae59e6cb1060");

	// Token: 0x04003612 RID: 13842
	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01.prefab:ff7920cc00fef2c48a9fe0cb28374ed9");

	// Token: 0x04003613 RID: 13843
	private static readonly AssetReference BTA_BOSS_06t_BrokenDemolisher_StartUp = new AssetReference("BTA_BOSS_06t_BrokenDemolisher_StartUp.prefab:5df6e842a3bc12946aef0be75d58cbb4");

	// Token: 0x04003614 RID: 13844
	private List<string> m_VO_BTA_BOSS_06h_IdleLines = new List<string>
	{
		BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01,
		BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01
	};

	// Token: 0x04003615 RID: 13845
	private List<string> m_VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_Lines = new List<string>
	{
		BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01,
		BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02,
		BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03,
		BTA_Fight_06.VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04
	};

	// Token: 0x04003616 RID: 13846
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04003617 RID: 13847
	private Notification m_turnCounter;

	// Token: 0x04003618 RID: 13848
	private MineCartRushArt m_mineCartArt;

	// Token: 0x04003619 RID: 13849
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_06.InitBooleanOptions();

	// Token: 0x0400361A RID: 13850
	private static Map<GameEntityOption, string> s_stringOptions = BTA_Fight_06.InitStringOptions();
}
