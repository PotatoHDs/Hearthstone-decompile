using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004BC RID: 1212
public class ULDA_Dungeon_Boss_63h : ULDA_Dungeon
{
	// Token: 0x06004103 RID: 16643 RVA: 0x0015AFDC File Offset: 0x001591DC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_DefeatPlayer_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_IntroElise_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_PlayerRamkahenAlly_01,
			ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004104 RID: 16644 RVA: 0x0015B190 File Offset: 0x00159390
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01;
	}

	// Token: 0x06004105 RID: 16645 RVA: 0x0015B1C8 File Offset: 0x001593C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "ULDA_Elise")
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

	// Token: 0x06004106 RID: 16646 RVA: 0x0015B2AF File Offset: 0x001594AF
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerTriggerLines);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01, 2.5f);
			break;
		case 103:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01, 2.5f);
			break;
		case 104:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01, 2.5f);
			break;
		case 105:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06004107 RID: 16647 RVA: 0x0015B2C5 File Offset: 0x001594C5
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
		if (cardId == "ULDA_036")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004108 RID: 16648 RVA: 0x0015B2DB File Offset: 0x001594DB
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
		if (!(cardId == "ULD_181"))
		{
			if (!(cardId == "ULD_276"))
			{
				if (cardId == "TRL_351")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002F5E RID: 12126
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01.prefab:530b8dddac9eb044aaf60d1ad8cba961");

	// Token: 0x04002F5F RID: 12127
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01.prefab:6203deb0428d5664982f2b7ae71ef565");

	// Token: 0x04002F60 RID: 12128
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01.prefab:1799a5976ac96e94992cae474a6d1a6e");

	// Token: 0x04002F61 RID: 12129
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01.prefab:52ea4f6e5cff0334bb18839ad70263f0");

	// Token: 0x04002F62 RID: 12130
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_DefeatPlayer_01.prefab:9345cecf23a8cdc4cbd24b4510493f5f");

	// Token: 0x04002F63 RID: 12131
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01.prefab:3eb46d22cdecb21458d15723d5193ad6");

	// Token: 0x04002F64 RID: 12132
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01.prefab:1767d59f4aeea99449281a9d9ad3ce5e");

	// Token: 0x04002F65 RID: 12133
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02.prefab:abf3ff922e98d0649b17f988f9c3dedf");

	// Token: 0x04002F66 RID: 12134
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03.prefab:110a9e6222b89ea48b6a262d92a80a2c");

	// Token: 0x04002F67 RID: 12135
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04.prefab:db8fe6a05423c1248b05f4230ea7a18e");

	// Token: 0x04002F68 RID: 12136
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05.prefab:0fc0189270761f14b912b17ec0045a3f");

	// Token: 0x04002F69 RID: 12137
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01.prefab:767b8135cbddfd643b11a0a0e2aebc1c");

	// Token: 0x04002F6A RID: 12138
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01.prefab:5d04e94cc77827945b305fa3bd624563");

	// Token: 0x04002F6B RID: 12139
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01.prefab:94aeec70cd130704b95d152fc8c83877");

	// Token: 0x04002F6C RID: 12140
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01.prefab:a68cd9845137e9244ad4e960161ca730");

	// Token: 0x04002F6D RID: 12141
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_IntroElise_01.prefab:12b087cd156d772488c5c6f6be6f84a7");

	// Token: 0x04002F6E RID: 12142
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01.prefab:37dc56526663fcf4396e403b1ba9de2c");

	// Token: 0x04002F6F RID: 12143
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01.prefab:932d49c94b03bb848a8245c67202436a");

	// Token: 0x04002F70 RID: 12144
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01.prefab:797dfb3672013504bafce8ad913ca8c2");

	// Token: 0x04002F71 RID: 12145
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_PlayerRamkahenAlly_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_PlayerRamkahenAlly_01.prefab:cd61ce439bbf9bc47814336adeb3560d");

	// Token: 0x04002F72 RID: 12146
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01.prefab:73386dc0dec033d42bc2991cee453823");

	// Token: 0x04002F73 RID: 12147
	private List<string> m_HeroPowerTriggerLines = new List<string>
	{
		ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01,
		ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02,
		ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03,
		ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04,
		ULDA_Dungeon_Boss_63h.VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05
	};

	// Token: 0x04002F74 RID: 12148
	private HashSet<string> m_playedLines = new HashSet<string>();
}
