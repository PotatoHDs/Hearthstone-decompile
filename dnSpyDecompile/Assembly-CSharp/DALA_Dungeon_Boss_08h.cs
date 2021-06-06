using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000435 RID: 1077
public class DALA_Dungeon_Boss_08h : DALA_Dungeon
{
	// Token: 0x06003AAE RID: 15022 RVA: 0x0012FD4C File Offset: 0x0012DF4C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_BossTreant_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_BossTreeSpeaker_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_BossWitchwoodApple_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Death_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_DefeatPlayer_02,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_EmoteResponse_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_02,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_03,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_04,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_05,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_02,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_03,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_04,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_05,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Intro_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_IntroGeorge_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_IntroRakanishu_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_IntroSqueamlish_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerDruidSpell_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerFireMageSpell_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_02,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerTreeOfLife_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003AAF RID: 15023 RVA: 0x0012FF50 File Offset: 0x0012E150
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_EmoteResponse_01;
	}

	// Token: 0x06003AB0 RID: 15024 RVA: 0x0012FF88 File Offset: 0x0012E188
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_08h.m_IdleLines;
	}

	// Token: 0x06003AB1 RID: 15025 RVA: 0x0012FF90 File Offset: 0x0012E190
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_01,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_02,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_03,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_04,
			DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_HeroPower_05
		};
	}

	// Token: 0x06003AB2 RID: 15026 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003AB3 RID: 15027 RVA: 0x0012FFF2 File Offset: 0x0012E1F2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerDruidSpell_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003AB4 RID: 15028 RVA: 0x00130008 File Offset: 0x0012E208
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish" && cardId != "DALA_Barkeye" && cardId != "DALA_Rakanishu" && cardId != "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
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

	// Token: 0x06003AB5 RID: 15029 RVA: 0x00130119 File Offset: 0x0012E319
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1956011339U)
		{
			if (num <= 918130314U)
			{
				if (num <= 605821402U)
				{
					if (num != 598937748U)
					{
						if (num != 605821402U)
						{
							goto IL_448;
						}
						if (!(cardId == "LOE_002t"))
						{
							goto IL_448;
						}
					}
					else if (!(cardId == "ICC_836"))
					{
						goto IL_448;
					}
				}
				else if (num != 718321743U)
				{
					if (num != 918130314U)
					{
						goto IL_448;
					}
					if (!(cardId == "LOE_002"))
					{
						goto IL_448;
					}
				}
				else if (!(cardId == "KAR_076"))
				{
					goto IL_448;
				}
			}
			else if (num <= 1769983698U)
			{
				if (num != 1095804670U)
				{
					if (num != 1769983698U)
					{
						goto IL_448;
					}
					if (!(cardId == "LOOT_172"))
					{
						goto IL_448;
					}
				}
				else
				{
					if (!(cardId == "DAL_256t2"))
					{
						goto IL_448;
					}
					goto IL_421;
				}
			}
			else if (num != 1791989495U)
			{
				if (num != 1945129914U)
				{
					if (num != 1956011339U)
					{
						goto IL_448;
					}
					if (!(cardId == "TRL_313"))
					{
						goto IL_448;
					}
				}
				else
				{
					if (!(cardId == "EX1_158t"))
					{
						goto IL_448;
					}
					goto IL_421;
				}
			}
			else
			{
				if (!(cardId == "GIL_663t"))
				{
					goto IL_448;
				}
				goto IL_421;
			}
		}
		else if (num <= 3368492501U)
		{
			if (num <= 2379572673U)
			{
				if (num != 2023121815U)
				{
					if (num != 2379572673U)
					{
						goto IL_448;
					}
					if (!(cardId == "CFM_065"))
					{
						goto IL_448;
					}
				}
				else if (!(cardId == "TRL_317"))
				{
					goto IL_448;
				}
			}
			else if (num != 3159778034U)
			{
				if (num != 3368492501U)
				{
					goto IL_448;
				}
				if (!(cardId == "FP1_019t"))
				{
					goto IL_448;
				}
				goto IL_421;
			}
			else if (!(cardId == "GIL_147"))
			{
				goto IL_448;
			}
		}
		else if (num <= 3945181129U)
		{
			if (num != 3621261496U)
			{
				if (num != 3945181129U)
				{
					goto IL_448;
				}
				if (!(cardId == "CS2_029"))
				{
					goto IL_448;
				}
			}
			else
			{
				if (!(cardId == "GVG_033"))
				{
					goto IL_448;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerTreeOfLife_01, 2.5f);
				goto IL_448;
			}
		}
		else if (num != 4196992509U)
		{
			if (num != 4221058965U)
			{
				if (num != 4257410314U)
				{
					goto IL_448;
				}
				if (!(cardId == "EX1_279"))
				{
					goto IL_448;
				}
			}
			else if (!(cardId == "UNG_955"))
			{
				goto IL_448;
			}
		}
		else if (!(cardId == "CS2_032"))
		{
			goto IL_448;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerFireMageSpell_01, 2.5f);
		goto IL_448;
		IL_421:
		yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_08h.m_PlayerTreant);
		IL_448:
		yield break;
	}

	// Token: 0x06003AB6 RID: 15030 RVA: 0x0013012F File Offset: 0x0012E32F
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
		if (!(cardId == "GIL_663t") && !(cardId == "FP1_019t") && !(cardId == "EX1_158t") && !(cardId == "DAL_256t2"))
		{
			if (!(cardId == "TRL_341"))
			{
				if (cardId == "GIL_663")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_BossWitchwoodApple_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_BossTreeSpeaker_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_BossTreant_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040022B9 RID: 8889
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_BossTreant_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_BossTreant_01.prefab:6ed67142b9e36174cb5ed1ea0f9833ec");

	// Token: 0x040022BA RID: 8890
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_BossTreeSpeaker_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_BossTreeSpeaker_01.prefab:49b12f96b9d97954b9de643915195a67");

	// Token: 0x040022BB RID: 8891
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_BossWitchwoodApple_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_BossWitchwoodApple_01.prefab:adc5a6e72aa03114a911d5fb2e18c943");

	// Token: 0x040022BC RID: 8892
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_Death_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_Death_01.prefab:736bbdcfae947564f98ff96de06f0c77");

	// Token: 0x040022BD RID: 8893
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_DefeatPlayer_02.prefab:f5d38607acec70a47bc28ae0b3d64219");

	// Token: 0x040022BE RID: 8894
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_EmoteResponse_01.prefab:fb9b7860d65a635478c4bd16b475a2ef");

	// Token: 0x040022BF RID: 8895
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_HeroPower_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_HeroPower_01.prefab:f562d83ab8499af4b8e7370b52bf558f");

	// Token: 0x040022C0 RID: 8896
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_HeroPower_02 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_HeroPower_02.prefab:a9856797b8e979f4eb35a97c58332c51");

	// Token: 0x040022C1 RID: 8897
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_HeroPower_03 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_HeroPower_03.prefab:78032eb399938b04fac5bfb6969a8f46");

	// Token: 0x040022C2 RID: 8898
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_HeroPower_04 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_HeroPower_04.prefab:13819203da5d83f48826966a14d2bc77");

	// Token: 0x040022C3 RID: 8899
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_HeroPower_05 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_HeroPower_05.prefab:b701bf8041d416b4a81847d217523b63");

	// Token: 0x040022C4 RID: 8900
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_Idle_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_Idle_01.prefab:bb8e872490da7434cb42a13b8f00dd6b");

	// Token: 0x040022C5 RID: 8901
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_Idle_02 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_Idle_02.prefab:2b5a5c1f3451b074e906f705de20ad94");

	// Token: 0x040022C6 RID: 8902
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_Idle_03 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_Idle_03.prefab:20965f6bf0c7c024bbeb0acebc70a4d3");

	// Token: 0x040022C7 RID: 8903
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_Idle_04 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_Idle_04.prefab:a2fd012251a56ed4aba40be68b8f86bc");

	// Token: 0x040022C8 RID: 8904
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_Idle_05 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_Idle_05.prefab:7807ff6818a3dd64fa3745aad25e50d2");

	// Token: 0x040022C9 RID: 8905
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_Intro_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_Intro_01.prefab:1ae0678c9ea0dc8429c920d1612589a7");

	// Token: 0x040022CA RID: 8906
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_IntroGeorge_01.prefab:e898a2707ad42ba43bdc1c3eeae1f6e8");

	// Token: 0x040022CB RID: 8907
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_IntroOlBarkeye_01.prefab:736fe86dad6e19a4494a39e5f456bab9");

	// Token: 0x040022CC RID: 8908
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_IntroRakanishu_01.prefab:7ed31126427cf2b44aab5160a58acb94");

	// Token: 0x040022CD RID: 8909
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_IntroSqueamlish_01.prefab:4dd5c5ae31b454b4f9664679eaec2ac5");

	// Token: 0x040022CE RID: 8910
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_PlayerDruidSpell_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_PlayerDruidSpell_01.prefab:671722509645e5640a5cd6689d234348");

	// Token: 0x040022CF RID: 8911
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_PlayerFireMageSpell_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_PlayerFireMageSpell_01.prefab:97365f771ae3c0f4f9aca6c5bc019421");

	// Token: 0x040022D0 RID: 8912
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_01.prefab:d76ce6051fbb562489e6e41fd24431cb");

	// Token: 0x040022D1 RID: 8913
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_02 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_02.prefab:0f4e9f88aef1b9442a9d940ac5d5b3e2");

	// Token: 0x040022D2 RID: 8914
	private static readonly AssetReference VO_DALA_BOSS_08h_Female_Treant_PlayerTreeOfLife_01 = new AssetReference("VO_DALA_BOSS_08h_Female_Treant_PlayerTreeOfLife_01.prefab:e8b6206b7df036b4e8dadf18f7a4c33b");

	// Token: 0x040022D3 RID: 8915
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_01,
		DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_02,
		DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_03,
		DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_04,
		DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_Idle_05
	};

	// Token: 0x040022D4 RID: 8916
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x040022D5 RID: 8917
	private static List<string> m_PlayerTreant = new List<string>
	{
		DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_01,
		DALA_Dungeon_Boss_08h.VO_DALA_BOSS_08h_Female_Treant_PlayerTreant_02
	};
}
