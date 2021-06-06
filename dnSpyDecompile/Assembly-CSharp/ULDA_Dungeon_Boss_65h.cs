using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004BD RID: 1213
public class ULDA_Dungeon_Boss_65h : ULDA_Dungeon
{
	// Token: 0x0600410E RID: 16654 RVA: 0x0015B4B8 File Offset: 0x001596B8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_DefeatPlayer_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_Idle_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_Idle_02,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_Intro_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_IntroSpecial_Reno_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01,
			ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600410F RID: 16655 RVA: 0x0015B64C File Offset: 0x0015984C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004110 RID: 16656 RVA: 0x0015B654 File Offset: 0x00159854
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004111 RID: 16657 RVA: 0x0015B65C File Offset: 0x0015985C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01;
	}

	// Token: 0x06004112 RID: 16658 RVA: 0x0015B694 File Offset: 0x00159894
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START && cardId != "ULDA_Reno")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004113 RID: 16659 RVA: 0x0015B73F File Offset: 0x0015993F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06004114 RID: 16660 RVA: 0x0015B755 File Offset: 0x00159955
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
		if (!(cardId == "ULD_214"))
		{
			if (!(cardId == "ULD_133"))
			{
				if (cardId == "ULD_163")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004115 RID: 16661 RVA: 0x0015B76B File Offset: 0x0015996B
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
		if (!(cardId == "ULD_328"))
		{
			if (!(cardId == "ULD_229"))
			{
				if (cardId == "DAL_714")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002F75 RID: 12149
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerCleverDisguise_01.prefab:c079a30c3920f9b46ba88bc4e0eaa7ed");

	// Token: 0x04002F76 RID: 12150
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerMischiefMaker_01.prefab:7d2e2be11d4f00d47a16f7ab1508db45");

	// Token: 0x04002F77 RID: 12151
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_BossTriggerUnderbellyFence_01.prefab:f946894bfb47d1e4196e94c7c9418d34");

	// Token: 0x04002F78 RID: 12152
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_DeathALT_01.prefab:c79c62a4416368846b0302ee2bcce7b4");

	// Token: 0x04002F79 RID: 12153
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_DefeatPlayer_01.prefab:15c95fa5e23c219419888025f800b9b8");

	// Token: 0x04002F7A RID: 12154
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_EmoteResponse_01.prefab:4373ea7232369e14582be39fff1b343c");

	// Token: 0x04002F7B RID: 12155
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01.prefab:6b585040d8ebccd42b88c4d8fa743c3a");

	// Token: 0x04002F7C RID: 12156
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02.prefab:27d7ab56ae04df143990810b8b2ae39b");

	// Token: 0x04002F7D RID: 12157
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03.prefab:bd6da2634009c21419eff71b2b0c0a74");

	// Token: 0x04002F7E RID: 12158
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04.prefab:345c121d1b494f941a4c5fcf03f341b6");

	// Token: 0x04002F7F RID: 12159
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05.prefab:50d90bb20d0840c41a3b17ff131abff6");

	// Token: 0x04002F80 RID: 12160
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_HeroPowerPlayStolenLegendary_01.prefab:7cd7e9d0c1f7e1348a605e1c5674c41e");

	// Token: 0x04002F81 RID: 12161
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_Idle_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_Idle_01.prefab:48bf2a053e38b2848840a4de30786d70");

	// Token: 0x04002F82 RID: 12162
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_Idle_02 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_Idle_02.prefab:01f2cea5275006c44adecb6d4b48ce80");

	// Token: 0x04002F83 RID: 12163
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_Intro_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_Intro_01.prefab:65dc909e58a43964d99c99a729d36fb3");

	// Token: 0x04002F84 RID: 12164
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_IntroSpecial_Reno_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_IntroSpecial_Reno_01.prefab:22d9f1e8364eb354b9fb4c431eeb3401");

	// Token: 0x04002F85 RID: 12165
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Crystal_Merchant_01.prefab:56ae90ddf360c2d4b8a9e614b97278d8");

	// Token: 0x04002F86 RID: 12166
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Expired_Merchant_01.prefab:3bde0e046b335e741b32e4b3905a6a2b");

	// Token: 0x04002F87 RID: 12167
	private static readonly AssetReference VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01 = new AssetReference("VO_ULDA_BOSS_65h_Male_Ogre_PlayerTrigger_Freelanthropist_01.prefab:682004f7068986c42ae87839d9571b82");

	// Token: 0x04002F88 RID: 12168
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_01,
		ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_02,
		ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_03,
		ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_04,
		ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_HeroPower_05
	};

	// Token: 0x04002F89 RID: 12169
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_Idle_01,
		ULDA_Dungeon_Boss_65h.VO_ULDA_BOSS_65h_Male_Ogre_Idle_02
	};

	// Token: 0x04002F8A RID: 12170
	private HashSet<string> m_playedLines = new HashSet<string>();
}
