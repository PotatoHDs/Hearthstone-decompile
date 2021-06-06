using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004AE RID: 1198
public class ULDA_Dungeon_Boss_49h : ULDA_Dungeon
{
	// Token: 0x06004070 RID: 16496 RVA: 0x00157DF0 File Offset: 0x00155FF0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_DefeatPlayer_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01,
			ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004071 RID: 16497 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004072 RID: 16498 RVA: 0x00157F84 File Offset: 0x00156184
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004073 RID: 16499 RVA: 0x00157F8C File Offset: 0x0015618C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004074 RID: 16500 RVA: 0x00157F94 File Offset: 0x00156194
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01;
	}

	// Token: 0x06004075 RID: 16501 RVA: 0x00157FCC File Offset: 0x001561CC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004076 RID: 16502 RVA: 0x001580A6 File Offset: 0x001562A6
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004077 RID: 16503 RVA: 0x001580BC File Offset: 0x001562BC
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
		if (!(cardId == "ULDA_040"))
		{
			if (!(cardId == "ULD_188"))
			{
				if (cardId == "ULD_450")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004078 RID: 16504 RVA: 0x001580D2 File Offset: 0x001562D2
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 3615501629U)
		{
			if (num <= 3514835915U)
			{
				if (num != 845142957U)
				{
					if (num != 3514835915U)
					{
						goto IL_2F7;
					}
					if (!(cardId == "DAL_613"))
					{
						goto IL_2F7;
					}
				}
				else if (!(cardId == "DAL_413"))
				{
					goto IL_2F7;
				}
			}
			else if (num != 3598724010U)
			{
				if (num != 3615501629U)
				{
					goto IL_2F7;
				}
				if (!(cardId == "DAL_615"))
				{
					goto IL_2F7;
				}
			}
			else if (!(cardId == "DAL_614"))
			{
				goto IL_2F7;
			}
		}
		else if (num <= 3757234700U)
		{
			if (num != 3740457081U)
			{
				if (num != 3757234700U)
				{
					goto IL_2F7;
				}
				if (!(cardId == "ULD_162"))
				{
					goto IL_2F7;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01, 2.5f);
				goto IL_2F7;
			}
			else
			{
				if (!(cardId == "ULD_165"))
				{
					goto IL_2F7;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01, 2.5f);
				goto IL_2F7;
			}
		}
		else if (num != 3785628939U)
		{
			if (num != 3786761772U)
			{
				if (num != 3995164034U)
				{
					goto IL_2F7;
				}
				if (!(cardId == "ULD_616"))
				{
					goto IL_2F7;
				}
			}
			else if (!(cardId == "DAL_739"))
			{
				goto IL_2F7;
			}
		}
		else if (!(cardId == "DAL_741"))
		{
			goto IL_2F7;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01, 2.5f);
		IL_2F7:
		yield break;
	}

	// Token: 0x04002E7A RID: 11898
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01.prefab:21f99f609007e6548b8fc12e98083aea");

	// Token: 0x04002E7B RID: 11899
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01.prefab:e5d705e8a24b0824e85f98750ee2ce35");

	// Token: 0x04002E7C RID: 11900
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01.prefab:bb1c5092a9fc0aa46bce66ff0676a384");

	// Token: 0x04002E7D RID: 11901
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01.prefab:bb40cacc9d30f774aaf352dd9bd33305");

	// Token: 0x04002E7E RID: 11902
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_DefeatPlayer_01.prefab:43188fc4e15678a459c842f28a30b6c5");

	// Token: 0x04002E7F RID: 11903
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01.prefab:bcd2ac1cf3a3ba54986b7b11ffc73be5");

	// Token: 0x04002E80 RID: 11904
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01.prefab:57ea8d1ad71085844a362352f137d4a1");

	// Token: 0x04002E81 RID: 11905
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02.prefab:36491b19f6be8af488dc9f8f8c22dbe1");

	// Token: 0x04002E82 RID: 11906
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03.prefab:f1ca1e97b6e5c7746b737c1918f56bfe");

	// Token: 0x04002E83 RID: 11907
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04.prefab:8f0dbe3a39e3300489baf5bda9edac3b");

	// Token: 0x04002E84 RID: 11908
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05.prefab:fa344fe57bdb4234590881301f2c62b2");

	// Token: 0x04002E85 RID: 11909
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01.prefab:d6cbf671596a90148a1694b45995841a");

	// Token: 0x04002E86 RID: 11910
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02.prefab:4714919e5e5762346a295638ed7ecc9d");

	// Token: 0x04002E87 RID: 11911
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03.prefab:c42c3adfb3a41384da298a85f557935d");

	// Token: 0x04002E88 RID: 11912
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01.prefab:359a118384be6294e93ecdf9c79235e5");

	// Token: 0x04002E89 RID: 11913
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01.prefab:e66e97b6a2fa11449b689d1575c97b16");

	// Token: 0x04002E8A RID: 11914
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01.prefab:bd6dd8268e630984c81089cba724fdb7");

	// Token: 0x04002E8B RID: 11915
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01.prefab:a255d1eb5c4d4c04a9aefc36e365ba9a");

	// Token: 0x04002E8C RID: 11916
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01.prefab:56336d6ac94c70a46903d13a417e23c0");

	// Token: 0x04002E8D RID: 11917
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01,
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02,
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03,
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04,
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05
	};

	// Token: 0x04002E8E RID: 11918
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01,
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02,
		ULDA_Dungeon_Boss_49h.VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03
	};

	// Token: 0x04002E8F RID: 11919
	private HashSet<string> m_playedLines = new HashSet<string>();
}
