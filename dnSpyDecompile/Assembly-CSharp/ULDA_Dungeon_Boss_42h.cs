using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A7 RID: 1191
public class ULDA_Dungeon_Boss_42h : ULDA_Dungeon
{
	// Token: 0x0600402D RID: 16429 RVA: 0x0015661C File Offset: 0x0015481C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Death_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_DefeatPlayer_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_IntroFinley_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01,
			ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600402E RID: 16430 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600402F RID: 16431 RVA: 0x001567A0 File Offset: 0x001549A0
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004030 RID: 16432 RVA: 0x001567A8 File Offset: 0x001549A8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004031 RID: 16433 RVA: 0x001567B0 File Offset: 0x001549B0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01;
	}

	// Token: 0x06004032 RID: 16434 RVA: 0x001567E8 File Offset: 0x001549E8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
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

	// Token: 0x06004033 RID: 16435 RVA: 0x00156894 File Offset: 0x00154A94
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004034 RID: 16436 RVA: 0x001568AA File Offset: 0x00154AAA
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
		if (!(cardId == "TRL_304"))
		{
			if (!(cardId == "GIL_581"))
			{
				if (cardId == "TRL_131")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004035 RID: 16437 RVA: 0x001568C0 File Offset: 0x00154AC0
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
		if (!(cardId == "LOOT_060"))
		{
			if (!(cardId == "EX1_243"))
			{
				if (cardId == "ULD_158")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002E14 RID: 11796
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01.prefab:cedaca41e3476114bb40ba0232879d32");

	// Token: 0x04002E15 RID: 11797
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01.prefab:3d008fc61c188a24bb6dabca39cb45c5");

	// Token: 0x04002E16 RID: 11798
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01.prefab:5959d1f06c5bf2740a86fe2c982fba03");

	// Token: 0x04002E17 RID: 11799
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Death_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Death_01.prefab:dd918dedf2b3b65408ff34e74e4bf0fe");

	// Token: 0x04002E18 RID: 11800
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_DefeatPlayer_01.prefab:536049549f204b54ebf0ae19989feff0");

	// Token: 0x04002E19 RID: 11801
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01.prefab:c56a739731416ad4796ea7b5046692ac");

	// Token: 0x04002E1A RID: 11802
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01.prefab:b6acc464be384f94a8922260c1e02935");

	// Token: 0x04002E1B RID: 11803
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02.prefab:791a3f238360a4f41924af56cf5526ae");

	// Token: 0x04002E1C RID: 11804
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03.prefab:3c0f649e5dbf05a45980e0b752fe649c");

	// Token: 0x04002E1D RID: 11805
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04.prefab:ce12123c528174243b92d52b1a0cead6");

	// Token: 0x04002E1E RID: 11806
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01.prefab:0d637de1df77bbf42b8e2c1b3b1b934a");

	// Token: 0x04002E1F RID: 11807
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02.prefab:cb30f25b73f9d8b49b1e546cf467719e");

	// Token: 0x04002E20 RID: 11808
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03.prefab:59b6a956a46a0c6479de8584bc9139c9");

	// Token: 0x04002E21 RID: 11809
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01.prefab:7e5b42817311e254daa409cb18b88bcc");

	// Token: 0x04002E22 RID: 11810
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_IntroFinley_01.prefab:36e38e92e18f1544b85c66053309018a");

	// Token: 0x04002E23 RID: 11811
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01.prefab:ff57e5b557e902e41a33107f9cc19414");

	// Token: 0x04002E24 RID: 11812
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01.prefab:5e7b52c2cabbb014587f0d9361e69065");

	// Token: 0x04002E25 RID: 11813
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01.prefab:7599c448afe801940b8514aad22de783");

	// Token: 0x04002E26 RID: 11814
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01,
		ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02,
		ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03,
		ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04
	};

	// Token: 0x04002E27 RID: 11815
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01,
		ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02,
		ULDA_Dungeon_Boss_42h.VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03
	};

	// Token: 0x04002E28 RID: 11816
	private HashSet<string> m_playedLines = new HashSet<string>();
}
