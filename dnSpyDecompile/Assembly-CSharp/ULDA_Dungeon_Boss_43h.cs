using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A8 RID: 1192
public class ULDA_Dungeon_Boss_43h : ULDA_Dungeon
{
	// Token: 0x0600403B RID: 16443 RVA: 0x00156A98 File Offset: 0x00154C98
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_DeathALT_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_DefeatPlayer_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_02,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_03,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_04,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_05,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Idle1_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Idle2_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Idle3_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Intro_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_IntroElise_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01,
			ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600403C RID: 16444 RVA: 0x00156C2C File Offset: 0x00154E2C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x0600403D RID: 16445 RVA: 0x00156C34 File Offset: 0x00154E34
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01;
	}

	// Token: 0x0600403E RID: 16446 RVA: 0x00156C6C File Offset: 0x00154E6C
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (this.m_IdleLines.Count == 0)
		{
			return;
		}
		string line = this.m_IdleLines[0];
		this.m_IdleLines.RemoveAt(0);
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
	}

	// Token: 0x0600403F RID: 16447 RVA: 0x00156CF8 File Offset: 0x00154EF8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004040 RID: 16448 RVA: 0x00156DD2 File Offset: 0x00154FD2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004041 RID: 16449 RVA: 0x00156DE8 File Offset: 0x00154FE8
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 316364135U)
		{
			if (num != 232623135U)
			{
				if (num != 282955992U)
				{
					if (num != 316364135U)
					{
						goto IL_2A7;
					}
					if (!(cardId == "ULD_707"))
					{
						goto IL_2A7;
					}
				}
				else if (!(cardId == "ULD_715"))
				{
					goto IL_2A7;
				}
			}
			else if (!(cardId == "ULD_718"))
			{
				goto IL_2A7;
			}
		}
		else if (num <= 332374055U)
		{
			if (num != 316511230U)
			{
				if (num != 332374055U)
				{
					goto IL_2A7;
				}
				if (!(cardId == "GIL_548"))
				{
					goto IL_2A7;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01, 2.5f);
				goto IL_2A7;
			}
			else if (!(cardId == "ULD_717"))
			{
				goto IL_2A7;
			}
		}
		else if (num != 1777725921U)
		{
			if (num != 4136753158U)
			{
				goto IL_2A7;
			}
			if (!(cardId == "ULDA_006"))
			{
				goto IL_2A7;
			}
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01, 2.5f);
			goto IL_2A7;
		}
		else if (!(cardId == "ULD_172"))
		{
			goto IL_2A7;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01, 2.5f);
		IL_2A7:
		yield break;
	}

	// Token: 0x06004042 RID: 16450 RVA: 0x00156DFE File Offset: 0x00154FFE
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
		if (!(cardId == "ICC_407"))
		{
			if (!(cardId == "EX1_309"))
			{
				if (cardId == "BOT_263")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002E29 RID: 11817
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01.prefab:1f50840d32b924346a1b2d7618bd6c1a");

	// Token: 0x04002E2A RID: 11818
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01.prefab:236fe058a193aa04ea2ebc32529f2e08");

	// Token: 0x04002E2B RID: 11819
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01.prefab:62771f4e154f7ce43b0d437ec5a09b63");

	// Token: 0x04002E2C RID: 11820
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_DeathALT_01.prefab:fd1b466e8a781df4d90ccb953afe6f64");

	// Token: 0x04002E2D RID: 11821
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_DefeatPlayer_01.prefab:438ff5a226b67b544889dd690db95ee8");

	// Token: 0x04002E2E RID: 11822
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01.prefab:ad8f8039cd80e6f4ebdaa473e44c155e");

	// Token: 0x04002E2F RID: 11823
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_01.prefab:1c3729a4f9b71ff42bcec0d0bc6b50aa");

	// Token: 0x04002E30 RID: 11824
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_02.prefab:bd1f00ab53b348b438fe3acfb68b2c81");

	// Token: 0x04002E31 RID: 11825
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_03.prefab:15625ab2f64ad2d4887720071fd0b197");

	// Token: 0x04002E32 RID: 11826
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_04.prefab:f5c56522df6dbf04496484ee0b7a607b");

	// Token: 0x04002E33 RID: 11827
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_05.prefab:941daf7a6b214794b89fc110b8bbef24");

	// Token: 0x04002E34 RID: 11828
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Idle1_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Idle1_01.prefab:31150cedd029ba54989300d32c484d1c");

	// Token: 0x04002E35 RID: 11829
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Idle2_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Idle2_01.prefab:d6c17c65068a0504ab426314cb49f99e");

	// Token: 0x04002E36 RID: 11830
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Idle3_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Idle3_01.prefab:1ab4758de3265f44ab6d04ac0b3521d1");

	// Token: 0x04002E37 RID: 11831
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Intro_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Intro_01.prefab:fe99cd47663465d42a597a8eb48be752");

	// Token: 0x04002E38 RID: 11832
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_IntroElise_01.prefab:fbe3c5d185103724abd1eeef16b85e81");

	// Token: 0x04002E39 RID: 11833
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01.prefab:ffaa292aa121ef24cb388e5547146c6f");

	// Token: 0x04002E3A RID: 11834
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01.prefab:57f3417a58cebf84d8c1a9d195c0840f");

	// Token: 0x04002E3B RID: 11835
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01.prefab:d3a6bc6ac0f714a4c969c017fedc34af");

	// Token: 0x04002E3C RID: 11836
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_01,
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_02,
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_03,
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_04,
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_HeroPower_05
	};

	// Token: 0x04002E3D RID: 11837
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Idle1_01,
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Idle2_01,
		ULDA_Dungeon_Boss_43h.VO_ULDA_BOSS_43h_Female_Human_Idle3_01
	};

	// Token: 0x04002E3E RID: 11838
	private HashSet<string> m_playedLines = new HashSet<string>();
}
