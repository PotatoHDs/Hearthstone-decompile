using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200048D RID: 1165
public class ULDA_Dungeon_Boss_16h : ULDA_Dungeon
{
	// Token: 0x06003EE3 RID: 16099 RVA: 0x0014D91C File Offset: 0x0014BB1C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_BossLivingMonument_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_BossPharoahCat_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_BossWastelandScorpid_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Death_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_DefeatPlayer_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_EmoteResponse_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_02,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_03,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_04,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Idle_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Idle_02,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Idle_03,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Intro_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_IntroEliseResponse_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_IntroFinleyResponse_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Restless_Mummy_01,
			ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Sunstruck_Henchman_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EE4 RID: 16100 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003EE5 RID: 16101 RVA: 0x0014DAA0 File Offset: 0x0014BCA0
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003EE6 RID: 16102 RVA: 0x0014DAA8 File Offset: 0x0014BCA8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_EmoteResponse_01;
	}

	// Token: 0x06003EE7 RID: 16103 RVA: 0x0014DAE0 File Offset: 0x0014BCE0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_IntroEliseResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_IntroFinleyResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003EE8 RID: 16104 RVA: 0x0014DBF9 File Offset: 0x0014BDF9
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerTriggerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003EE9 RID: 16105 RVA: 0x0014DC0F File Offset: 0x0014BE0F
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
		if (!(cardId == "ULD_180"))
		{
			if (cardId == "ULD_206")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Restless_Mummy_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Sunstruck_Henchman_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003EEA RID: 16106 RVA: 0x0014DC25 File Offset: 0x0014BE25
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
		if (!(cardId == "ULD_193"))
		{
			if (!(cardId == "ULD_186"))
			{
				if (cardId == "ULD_194")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_BossWastelandScorpid_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_BossPharoahCat_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_BossLivingMonument_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002B6F RID: 11119
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_BossLivingMonument_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_BossLivingMonument_01.prefab:e19979b124813df4ea60a858301aa8dc");

	// Token: 0x04002B70 RID: 11120
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_BossPharoahCat_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_BossPharoahCat_01.prefab:d490c54d7b5dcb7449219d7a3f4b81bf");

	// Token: 0x04002B71 RID: 11121
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_BossWastelandScorpid_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_BossWastelandScorpid_01.prefab:6e2287671c626494bae598ac1c8391e3");

	// Token: 0x04002B72 RID: 11122
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_Death_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_Death_01.prefab:8c42b4718167aeb41b94340afc8e5a99");

	// Token: 0x04002B73 RID: 11123
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_DefeatPlayer_01.prefab:3f94a44833135534bb266edb6fba29a0");

	// Token: 0x04002B74 RID: 11124
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_EmoteResponse_01.prefab:18f01fdb6e83ad14bba08f1d0201dd8c");

	// Token: 0x04002B75 RID: 11125
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_01.prefab:1035da81b396ba548a0cee2c9ac8f1be");

	// Token: 0x04002B76 RID: 11126
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_02 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_02.prefab:385556a0452255040afaee4ca69eb19a");

	// Token: 0x04002B77 RID: 11127
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_03 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_03.prefab:55415163087181643bc5c60ac1427953");

	// Token: 0x04002B78 RID: 11128
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_04 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_04.prefab:59bdf3254f14bce4893c46c94d167dfe");

	// Token: 0x04002B79 RID: 11129
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_Idle_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_Idle_01.prefab:e5574b0cc2ab90c4ea8ee06c39f4c183");

	// Token: 0x04002B7A RID: 11130
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_Idle_02 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_Idle_02.prefab:029e97e2ddc74b64c8144839a2d912e4");

	// Token: 0x04002B7B RID: 11131
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_Idle_03 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_Idle_03.prefab:19cc36bc9abbf564f9369dac62e60c4c");

	// Token: 0x04002B7C RID: 11132
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_Intro_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_Intro_01.prefab:a87391e6aca01904c9e964a5f2ae2be2");

	// Token: 0x04002B7D RID: 11133
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_IntroEliseResponse_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_IntroEliseResponse_01.prefab:2847787990484a9468f42f0148c80fb9");

	// Token: 0x04002B7E RID: 11134
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_IntroFinleyResponse_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_IntroFinleyResponse_01.prefab:fc6f5988aa3e16d41b73e5eccab16693");

	// Token: 0x04002B7F RID: 11135
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Restless_Mummy_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Restless_Mummy_01.prefab:24c85f783c3610745b90b398cad1f00a");

	// Token: 0x04002B80 RID: 11136
	private static readonly AssetReference VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Sunstruck_Henchman_01 = new AssetReference("VO_ULDA_BOSS_16h_Male_Ogre_PlayerTrigger_Sunstruck_Henchman_01.prefab:553b91fa30e96a943b3871d0e4cef235");

	// Token: 0x04002B81 RID: 11137
	private List<string> m_HeroPowerTriggerLines = new List<string>
	{
		ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_01,
		ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_02,
		ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_03,
		ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_HeroPowerTrigger_04
	};

	// Token: 0x04002B82 RID: 11138
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Idle_01,
		ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Idle_02,
		ULDA_Dungeon_Boss_16h.VO_ULDA_BOSS_16h_Male_Ogre_Idle_03
	};

	// Token: 0x04002B83 RID: 11139
	private HashSet<string> m_playedLines = new HashSet<string>();
}
