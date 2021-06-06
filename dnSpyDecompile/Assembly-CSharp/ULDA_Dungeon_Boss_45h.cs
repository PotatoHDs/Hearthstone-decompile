using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004AA RID: 1194
public class ULDA_Dungeon_Boss_45h : ULDA_Dungeon
{
	// Token: 0x0600404D RID: 16461 RVA: 0x00157110 File Offset: 0x00155310
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_DefeatPlayer_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01,
			ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600404E RID: 16462 RVA: 0x001572E4 File Offset: 0x001554E4
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600404F RID: 16463 RVA: 0x001572EC File Offset: 0x001554EC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01;
	}

	// Token: 0x06004050 RID: 16464 RVA: 0x00157324 File Offset: 0x00155524
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004051 RID: 16465 RVA: 0x001574B5 File Offset: 0x001556B5
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerTreasureLines);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		yield break;
	}

	// Token: 0x06004052 RID: 16466 RVA: 0x001574CB File Offset: 0x001556CB
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
		if (!(cardId == "AT_033"))
		{
			if (cardId == "ULD_172" || cardId == "ULD_707" || cardId == "ULD_715" || cardId == "ULD_717" || cardId == "ULD_718")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004053 RID: 16467 RVA: 0x001574E1 File Offset: 0x001556E1
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
		if (!(cardId == "EX1_590"))
		{
			if (!(cardId == "ULD_145"))
			{
				if (cardId == "LOE_116")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002E43 RID: 11843
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01.prefab:dff001d7e7f662b44ae169a2143123c4");

	// Token: 0x04002E44 RID: 11844
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01.prefab:8aea94f72b105934bb226ff0bd2d9908");

	// Token: 0x04002E45 RID: 11845
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01.prefab:0ff5fb95711d2244f9c22f50fe618571");

	// Token: 0x04002E46 RID: 11846
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01.prefab:89d0a59f276fe774e824e6e2358e8486");

	// Token: 0x04002E47 RID: 11847
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_DefeatPlayer_01.prefab:d4ca6d27238242f4b8dffaca9df0324f");

	// Token: 0x04002E48 RID: 11848
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01.prefab:0108c06139d601e4394686211c2f76b4");

	// Token: 0x04002E49 RID: 11849
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01.prefab:f6ea52763d07084478095e57f63bd74b");

	// Token: 0x04002E4A RID: 11850
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02.prefab:5f886a597e174964289e04deb6d4145b");

	// Token: 0x04002E4B RID: 11851
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03.prefab:3c597914ab6be4c43a6534d2d5e23d42");

	// Token: 0x04002E4C RID: 11852
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04.prefab:8267c8f61a94f264482bdf47501e21cd");

	// Token: 0x04002E4D RID: 11853
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01.prefab:f477c776226a69d449301e476ed9fcf0");

	// Token: 0x04002E4E RID: 11854
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02.prefab:50e82ccc9e84b844b8c15834420b8029");

	// Token: 0x04002E4F RID: 11855
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01.prefab:3e2ceb3badbb1504c8c13a3484767d69");

	// Token: 0x04002E50 RID: 11856
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02.prefab:18bc370fe10fab5489d37f30c795cfb1");

	// Token: 0x04002E51 RID: 11857
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03.prefab:70002a8c8817a5d47b1c5c46d012ee77");

	// Token: 0x04002E52 RID: 11858
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04.prefab:b2335794ca49968458d75a7dcd9b8157");

	// Token: 0x04002E53 RID: 11859
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01.prefab:a97a1d17445e2ff4bbe9fb512b6c6489");

	// Token: 0x04002E54 RID: 11860
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01.prefab:bac0437383f783343a47a995a0cfe861");

	// Token: 0x04002E55 RID: 11861
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01.prefab:331e705c15eb171428f5a0ae9454dcbc");

	// Token: 0x04002E56 RID: 11862
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01.prefab:d08cf2b6ced6ba34b9c8a6cc4f4fa202");

	// Token: 0x04002E57 RID: 11863
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01.prefab:7e4af84ed70d03c4cb4c13091457e167");

	// Token: 0x04002E58 RID: 11864
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01.prefab:bcae8eb60177c3c4985cc24d242063b9");

	// Token: 0x04002E59 RID: 11865
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01.prefab:75799adf78bc5e54880a548ea9e6de45");

	// Token: 0x04002E5A RID: 11866
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01,
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02,
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03,
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04
	};

	// Token: 0x04002E5B RID: 11867
	private List<string> m_HeroPowerTreasureLines = new List<string>
	{
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01,
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02
	};

	// Token: 0x04002E5C RID: 11868
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01,
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02,
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03,
		ULDA_Dungeon_Boss_45h.VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04
	};

	// Token: 0x04002E5D RID: 11869
	private HashSet<string> m_playedLines = new HashSet<string>();
}
