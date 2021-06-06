using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200049F RID: 1183
public class ULDA_Dungeon_Boss_34h : ULDA_Dungeon
{
	// Token: 0x06003FC7 RID: 16327 RVA: 0x001522E4 File Offset: 0x001504E4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_DefeatPlayer_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Intro_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01,
			ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FC8 RID: 16328 RVA: 0x00152478 File Offset: 0x00150678
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003FC9 RID: 16329 RVA: 0x00152480 File Offset: 0x00150680
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01;
	}

	// Token: 0x06003FCA RID: 16330 RVA: 0x001524B8 File Offset: 0x001506B8
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

	// Token: 0x06003FCB RID: 16331 RVA: 0x00152544 File Offset: 0x00150744
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003FCC RID: 16332 RVA: 0x0015261E File Offset: 0x0015081E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003FCD RID: 16333 RVA: 0x00152634 File Offset: 0x00150834
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
			if (num <= 232623135U)
			{
				if (num != 110374861U)
				{
					if (num != 232623135U)
					{
						goto IL_2AB;
					}
					if (!(cardId == "ULD_718"))
					{
						goto IL_2AB;
					}
					goto IL_27F;
				}
				else if (!(cardId == "ULDA_018"))
				{
					goto IL_2AB;
				}
			}
			else if (num != 282955992U)
			{
				if (num != 316364135U)
				{
					goto IL_2AB;
				}
				if (!(cardId == "ULD_707"))
				{
					goto IL_2AB;
				}
				goto IL_27F;
			}
			else
			{
				if (!(cardId == "ULD_715"))
				{
					goto IL_2AB;
				}
				goto IL_27F;
			}
		}
		else if (num <= 1777725921U)
		{
			if (num != 316511230U)
			{
				if (num != 1777725921U)
				{
					goto IL_2AB;
				}
				if (!(cardId == "ULD_172"))
				{
					goto IL_2AB;
				}
				goto IL_27F;
			}
			else
			{
				if (!(cardId == "ULD_717"))
				{
					goto IL_2AB;
				}
				goto IL_27F;
			}
		}
		else if (num != 4153677872U)
		{
			if (num != 4170455491U)
			{
				if (num != 4187233110U)
				{
					goto IL_2AB;
				}
				if (!(cardId == "ULDA_015"))
				{
					goto IL_2AB;
				}
			}
			else if (!(cardId == "ULDA_016"))
			{
				goto IL_2AB;
			}
		}
		else if (!(cardId == "ULDA_017"))
		{
			goto IL_2AB;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01, 2.5f);
		goto IL_2AB;
		IL_27F:
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01, 2.5f);
		IL_2AB:
		yield break;
	}

	// Token: 0x06003FCE RID: 16334 RVA: 0x0015264A File Offset: 0x0015084A
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
		if (!(cardId == "DAL_060"))
		{
			if (!(cardId == "BOT_031"))
			{
				if (cardId == "DAL_615" || cardId == "DAL_614" || cardId == "DAL_739" || cardId == "ULD_616")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002CBD RID: 11453
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01.prefab:91bc077876ba53a448a96242ae45328c");

	// Token: 0x04002CBE RID: 11454
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01.prefab:15404e1dcfec4624880bf09727297f4a");

	// Token: 0x04002CBF RID: 11455
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01.prefab:97b045826fe5d0c488dc8982e4253d51");

	// Token: 0x04002CC0 RID: 11456
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01.prefab:9db63e2241b5b944cb5be6c487b06c58");

	// Token: 0x04002CC1 RID: 11457
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_DefeatPlayer_01.prefab:90b22ed56abb8334bb7302f1a239ee50");

	// Token: 0x04002CC2 RID: 11458
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01.prefab:990eaba8ea6aa3747ac4822cbfd25385");

	// Token: 0x04002CC3 RID: 11459
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01.prefab:c32ee29ec4d5a934ea42ac6d087538c2");

	// Token: 0x04002CC4 RID: 11460
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02.prefab:dcdb14588146c43478ea87777ce174c0");

	// Token: 0x04002CC5 RID: 11461
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03.prefab:2aab27e0292f9804c820d9071c3c331d");

	// Token: 0x04002CC6 RID: 11462
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04.prefab:3033bc3518b12584dbac8eb01a012b79");

	// Token: 0x04002CC7 RID: 11463
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05.prefab:3d6ae0b1c3976b545a4837aec2868980");

	// Token: 0x04002CC8 RID: 11464
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01.prefab:c3cd3f19913c8e8439a52abf00409170");

	// Token: 0x04002CC9 RID: 11465
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01.prefab:0cddde1666c4330408cdac1cf647965c");

	// Token: 0x04002CCA RID: 11466
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01.prefab:fcaf02acb0ef21f42af93bdba2c5c937");

	// Token: 0x04002CCB RID: 11467
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Intro_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Intro_01.prefab:22094abad3abdcf40abf5576dcfa51da");

	// Token: 0x04002CCC RID: 11468
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01.prefab:d4eeb747a0cbbea4683fa855c2c1883b");

	// Token: 0x04002CCD RID: 11469
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01.prefab:3b734ce79b6f68c4cb9d16441f9f7726");

	// Token: 0x04002CCE RID: 11470
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01.prefab:4912da1a58558b64d85e6e68c85a962b");

	// Token: 0x04002CCF RID: 11471
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01.prefab:dd1e35917d4245a49b6241f4c4bb7e35");

	// Token: 0x04002CD0 RID: 11472
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01,
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02,
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03,
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04,
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05
	};

	// Token: 0x04002CD1 RID: 11473
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01,
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01,
		ULDA_Dungeon_Boss_34h.VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01
	};

	// Token: 0x04002CD2 RID: 11474
	private HashSet<string> m_playedLines = new HashSet<string>();
}
