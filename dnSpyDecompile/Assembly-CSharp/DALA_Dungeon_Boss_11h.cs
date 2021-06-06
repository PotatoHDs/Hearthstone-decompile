using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000438 RID: 1080
public class DALA_Dungeon_Boss_11h : DALA_Dungeon
{
	// Token: 0x06003AD5 RID: 15061 RVA: 0x00130D44 File Offset: 0x0012EF44
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Death_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_DefeatPlayer_02,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_02,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_03,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_04,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_05,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Intro_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_IntroEudora_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_IntroSqueamlish_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01,
			DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003AD6 RID: 15062 RVA: 0x00130F08 File Offset: 0x0012F108
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_11h.m_IdleLines;
	}

	// Token: 0x06003AD7 RID: 15063 RVA: 0x00130F0F File Offset: 0x0012F10F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01;
	}

	// Token: 0x06003AD8 RID: 15064 RVA: 0x00130F48 File Offset: 0x0012F148
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Eudora" && cardId != "DALA_Squeamlish")
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

	// Token: 0x06003AD9 RID: 15065 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003ADA RID: 15066 RVA: 0x0013103F File Offset: 0x0012F23F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_11h.m_PlayerMinionDies);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_11h.m_PlayerAOE);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_11h.m_PlayerPirate);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003ADB RID: 15067 RVA: 0x00131055 File Offset: 0x0012F255
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "LOOT_541"))
		{
			if (cardId == "NEW1_004")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003ADC RID: 15068 RVA: 0x0013106B File Offset: 0x0012F26B
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

	// Token: 0x04002302 RID: 8962
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Death_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Death_01.prefab:91c27ea464e733a479831e55f0e10d94");

	// Token: 0x04002303 RID: 8963
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_DefeatPlayer_02.prefab:b0adb823eb8cc7c408c7f3a651dcf44e");

	// Token: 0x04002304 RID: 8964
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01.prefab:3b9e1c829fbdb1d4db2c267db716ad46");

	// Token: 0x04002305 RID: 8965
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_01.prefab:45a2f62281c4c7d44b9aaa7b758de255");

	// Token: 0x04002306 RID: 8966
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_02.prefab:9a2eb335a1e90164eb829c4aec0c8949");

	// Token: 0x04002307 RID: 8967
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_03 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_03.prefab:2596c8a227f61584a9be27aeccc264d9");

	// Token: 0x04002308 RID: 8968
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_04 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_04.prefab:ad800a6f15ebec2489e757d69d627133");

	// Token: 0x04002309 RID: 8969
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_05 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_05.prefab:607e7297eb0cf7d40ae1ff028019f0c6");

	// Token: 0x0400230A RID: 8970
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Intro_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Intro_01.prefab:be53259682365ee42a19fde978a89ed4");

	// Token: 0x0400230B RID: 8971
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_IntroEudora_01.prefab:d5d95e5fda7479144b839b4678467a62");

	// Token: 0x0400230C RID: 8972
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01.prefab:3c2e3913a836c594686eea862f63ee89");

	// Token: 0x0400230D RID: 8973
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_IntroSqueamlish_01.prefab:e74828d8ab059744596941c49237bcf8");

	// Token: 0x0400230E RID: 8974
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01.prefab:815aa996010aa3e4c93d500f799a791c");

	// Token: 0x0400230F RID: 8975
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02.prefab:e254e19b1dd157b48a36351baa4c38c7");

	// Token: 0x04002310 RID: 8976
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03.prefab:b8b6471a4898b3c4082a54382e059f2b");

	// Token: 0x04002311 RID: 8977
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01.prefab:7854bb59dfa41df4c855705af84062aa");

	// Token: 0x04002312 RID: 8978
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01.prefab:ab01ceedf80d1e34291a19662d0dc15a");

	// Token: 0x04002313 RID: 8979
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01.prefab:7d9e124b25400b54bb0453326c9e758b");

	// Token: 0x04002314 RID: 8980
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02.prefab:cba80c7914deb5547b3ba75277ddde21");

	// Token: 0x04002315 RID: 8981
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04.prefab:c10e67261473f4e4a873960abc4aa224");

	// Token: 0x04002316 RID: 8982
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01.prefab:406b5b0fbb732ef4c859f6308a86150b");

	// Token: 0x04002317 RID: 8983
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01.prefab:9121f92817c98fe4a819d63eb039937e");

	// Token: 0x04002318 RID: 8984
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_01,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_02,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_03,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_04,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_Idle_05
	};

	// Token: 0x04002319 RID: 8985
	private static List<string> m_PlayerMinionDies = new List<string>
	{
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04
	};

	// Token: 0x0400231A RID: 8986
	private static List<string> m_PlayerAOE = new List<string>
	{
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03
	};

	// Token: 0x0400231B RID: 8987
	private static List<string> m_PlayerPirate = new List<string>
	{
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01,
		DALA_Dungeon_Boss_11h.VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01
	};

	// Token: 0x0400231C RID: 8988
	private HashSet<string> m_playedLines = new HashSet<string>();
}
