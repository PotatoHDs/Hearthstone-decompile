using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A1 RID: 1185
public class ULDA_Dungeon_Boss_36h : ULDA_Dungeon
{
	// Token: 0x06003FE2 RID: 16354 RVA: 0x00152DB8 File Offset: 0x00150FB8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_BossArcaniteReaper_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_BossPlagueofMadness_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_BossWhirlwind_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_DeathALT_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_DefeatPlayer_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_EmoteResponse_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_02,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_03,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_04,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_05,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Idle_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Idle_02,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Idle_03,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Intro_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_IntroFinley_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_PlayerPlagueofMadness_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_PlayerSurrendertoMadness_01,
			ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_PlayerWeapon_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FE3 RID: 16355 RVA: 0x00152F4C File Offset: 0x0015114C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003FE4 RID: 16356 RVA: 0x00152F54 File Offset: 0x00151154
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003FE5 RID: 16357 RVA: 0x00152F5C File Offset: 0x0015115C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_EmoteResponse_01;
	}

	// Token: 0x06003FE6 RID: 16358 RVA: 0x00152F94 File Offset: 0x00151194
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003FE7 RID: 16359 RVA: 0x0015306E File Offset: 0x0015126E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_PlayerWeapon_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003FE8 RID: 16360 RVA: 0x00153084 File Offset: 0x00151284
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
		if (!(cardId == "ULD_715"))
		{
			if (cardId == "TRL_500")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_PlayerSurrendertoMadness_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_PlayerPlagueofMadness_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003FE9 RID: 16361 RVA: 0x0015309A File Offset: 0x0015129A
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
		if (!(cardId == "CS2_112"))
		{
			if (!(cardId == "ULD_715"))
			{
				if (cardId == "EX1_400")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_BossWhirlwind_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_BossPlagueofMadness_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_BossArcaniteReaper_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002CE9 RID: 11497
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_BossArcaniteReaper_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_BossArcaniteReaper_01.prefab:7fa33d2ac741de44389bf1fb3fde4f46");

	// Token: 0x04002CEA RID: 11498
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_BossPlagueofMadness_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_BossPlagueofMadness_01.prefab:af9893496aab0494b89e03d3dd97e8fb");

	// Token: 0x04002CEB RID: 11499
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_BossWhirlwind_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_BossWhirlwind_01.prefab:bc9564972c7c56b4a87bdf04f0cc09a2");

	// Token: 0x04002CEC RID: 11500
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_DeathALT_01.prefab:37dcc5cdaef41d14ea2e6a7ab2ab7b23");

	// Token: 0x04002CED RID: 11501
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_DefeatPlayer_01.prefab:c2f2811751473b44a8d658e52aaa8a99");

	// Token: 0x04002CEE RID: 11502
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_EmoteResponse_01.prefab:a82d1d8269abcf64289e3397ee9b3fe4");

	// Token: 0x04002CEF RID: 11503
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_01.prefab:3a9ebdb7977b9534898c3b91be5ba31a");

	// Token: 0x04002CF0 RID: 11504
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_02.prefab:c7a055caeb6454f49b9255996de9764e");

	// Token: 0x04002CF1 RID: 11505
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_03.prefab:ad8d27dcc440d2c4dba72fed6ebd0065");

	// Token: 0x04002CF2 RID: 11506
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_04.prefab:3cc2612b677eca746aaa2c774d57cd8a");

	// Token: 0x04002CF3 RID: 11507
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_05.prefab:0081e247d139daf419475f2297606ee9");

	// Token: 0x04002CF4 RID: 11508
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_Idle_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_Idle_01.prefab:cf9d73226a4ad8149b63f800f1587fc2");

	// Token: 0x04002CF5 RID: 11509
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_Idle_02 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_Idle_02.prefab:2948f69aa2a26a14ca7f89aac46676b2");

	// Token: 0x04002CF6 RID: 11510
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_Idle_03 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_Idle_03.prefab:b2c60d4cc959dec4fb022c8c9c16c8b9");

	// Token: 0x04002CF7 RID: 11511
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_Intro_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_Intro_01.prefab:1c1e7a9af96c7b445a4ad9baf31afe58");

	// Token: 0x04002CF8 RID: 11512
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_IntroFinley_01.prefab:2af5a139e6863034e87f5d457358cd35");

	// Token: 0x04002CF9 RID: 11513
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_PlayerPlagueofMadness_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_PlayerPlagueofMadness_01.prefab:b55a4dc0d7e356e48b95995d97abee6a");

	// Token: 0x04002CFA RID: 11514
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_PlayerSurrendertoMadness_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_PlayerSurrendertoMadness_01.prefab:a2f5e0bc65718e848a3bb1b71ef0eb19");

	// Token: 0x04002CFB RID: 11515
	private static readonly AssetReference VO_ULDA_BOSS_36h_Female_Tauren_PlayerWeapon_01 = new AssetReference("VO_ULDA_BOSS_36h_Female_Tauren_PlayerWeapon_01.prefab:6b975740d84034846bf7977f0f6efc5d");

	// Token: 0x04002CFC RID: 11516
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_01,
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_02,
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_03,
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_04,
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_HeroPower_05
	};

	// Token: 0x04002CFD RID: 11517
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Idle_01,
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Idle_02,
		ULDA_Dungeon_Boss_36h.VO_ULDA_BOSS_36h_Female_Tauren_Idle_03
	};

	// Token: 0x04002CFE RID: 11518
	private HashSet<string> m_playedLines = new HashSet<string>();
}
