using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000434 RID: 1076
public class DALA_Dungeon_Boss_07h : DALA_Dungeon
{
	// Token: 0x06003AA2 RID: 15010 RVA: 0x0012F888 File Offset: 0x0012DA88
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Death_02,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Idle_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Idle_02,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Idle_03,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Intro_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_IntroKriziki_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01,
			DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003AA3 RID: 15011 RVA: 0x0012FA0C File Offset: 0x0012DC0C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_07h.m_IdleLines;
	}

	// Token: 0x06003AA4 RID: 15012 RVA: 0x0012FA13 File Offset: 0x0012DC13
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01;
	}

	// Token: 0x06003AA5 RID: 15013 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003AA6 RID: 15014 RVA: 0x0012FA4B File Offset: 0x0012DC4B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01, 2.5f);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_07h.m_HeroPowerTrigger);
			break;
		case 103:
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003AA7 RID: 15015 RVA: 0x0012FA64 File Offset: 0x0012DC64
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			else if (cardId != "DALA_Kriziki" && cardId != "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003AA8 RID: 15016 RVA: 0x0012FB5B File Offset: 0x0012DD5B
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
		if (!(cardId == "DAL_747"))
		{
			if (!(cardId == "LOEA02_10a") && !(cardId == "NEW1_033"))
			{
				if (cardId == "DAL_378" || cardId == "DAL_378ts")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003AA9 RID: 15017 RVA: 0x0012FB71 File Offset: 0x0012DD71
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x040022A4 RID: 8868
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01.prefab:ce6517223437e3e44aef1c095de05e1b");

	// Token: 0x040022A5 RID: 8869
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Death_02 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Death_02.prefab:1f7c09a57b990764c955a16306d323af");

	// Token: 0x040022A6 RID: 8870
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_DefeatPlayer_01.prefab:4760d90d196631947a684013b4694426");

	// Token: 0x040022A7 RID: 8871
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01.prefab:d8937766bd4c9754095f5a69f7da6bb5");

	// Token: 0x040022A8 RID: 8872
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02.prefab:075b1d9d88e41824d84229356e946d45");

	// Token: 0x040022A9 RID: 8873
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03.prefab:6768cd1b074e1e34f9999934ae695fa2");

	// Token: 0x040022AA RID: 8874
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04.prefab:73676e2c215793c429e8c1c0bd92b8c1");

	// Token: 0x040022AB RID: 8875
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06.prefab:7e33cf311899928429f9764676e28267");

	// Token: 0x040022AC RID: 8876
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07.prefab:46c3bee69c6f3e84dbb29fc9b0657bbd");

	// Token: 0x040022AD RID: 8877
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Idle_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Idle_01.prefab:52367dffc466a154eb4bdb2e5ae0e2f8");

	// Token: 0x040022AE RID: 8878
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Idle_02 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Idle_02.prefab:23f4d1a30f688434d840b749a945edfb");

	// Token: 0x040022AF RID: 8879
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Idle_03 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Idle_03.prefab:b4e2dea35ecb43442a5cedcd6cacea51");

	// Token: 0x040022B0 RID: 8880
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Intro_01.prefab:52267af1f01386944928ccf738965d30");

	// Token: 0x040022B1 RID: 8881
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_IntroKriziki_01.prefab:6a88949089a597b47832653c84573978");

	// Token: 0x040022B2 RID: 8882
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01.prefab:e96b9344c597e3d439a8317c2a72e323");

	// Token: 0x040022B3 RID: 8883
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01.prefab:12dd0ef2a32772b4483ab2cfaf9aa2bc");

	// Token: 0x040022B4 RID: 8884
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01.prefab:c40357e8bfe39254daba3fa8ee9e5f8e");

	// Token: 0x040022B5 RID: 8885
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01.prefab:15fba6062f731e948951f39561625bd2");

	// Token: 0x040022B6 RID: 8886
	private static List<string> m_HeroPowerTrigger = new List<string>
	{
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02,
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03,
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04,
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06,
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07
	};

	// Token: 0x040022B7 RID: 8887
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Idle_01,
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Idle_02,
		DALA_Dungeon_Boss_07h.VO_DALA_BOSS_07h_Male_BloodElf_Idle_03
	};

	// Token: 0x040022B8 RID: 8888
	private HashSet<string> m_playedLines = new HashSet<string>();
}
