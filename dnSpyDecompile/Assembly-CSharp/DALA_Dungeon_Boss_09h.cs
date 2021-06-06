using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000436 RID: 1078
public class DALA_Dungeon_Boss_09h : DALA_Dungeon
{
	// Token: 0x06003ABC RID: 15036 RVA: 0x00130370 File Offset: 0x0012E570
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Death_02,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_DefeatPlayer_02,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Idle_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Idle_02,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Idle_03,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Intro_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01,
			DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003ABD RID: 15037 RVA: 0x00130504 File Offset: 0x0012E704
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_09h.m_IdleLines;
	}

	// Token: 0x06003ABE RID: 15038 RVA: 0x0013050B File Offset: 0x0012E70B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01;
	}

	// Token: 0x06003ABF RID: 15039 RVA: 0x00130544 File Offset: 0x0012E744
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Rakanishu")
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

	// Token: 0x06003AC0 RID: 15040 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003AC1 RID: 15041 RVA: 0x0013066A File Offset: 0x0012E86A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_HeroPowerBigLines);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_HeroPowerLines);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003AC2 RID: 15042 RVA: 0x00130680 File Offset: 0x0012E880
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
		if (!(cardId == "DALA_700"))
		{
			if (!(cardId == "FP1_013"))
			{
				if (cardId == "UNG_028t")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003AC3 RID: 15043 RVA: 0x00130696 File Offset: 0x0012E896
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

	// Token: 0x040022D6 RID: 8918
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01.prefab:2d3a0eca03e693048935dedaf40b5950");

	// Token: 0x040022D7 RID: 8919
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Death_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Death_02.prefab:a521e874798c5f94e925cc7c38c92395");

	// Token: 0x040022D8 RID: 8920
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_DefeatPlayer_02.prefab:6d0611de6e05d634995a4e5f9cf996ae");

	// Token: 0x040022D9 RID: 8921
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01.prefab:5a3899f8e090a284ca9306c5133eda33");

	// Token: 0x040022DA RID: 8922
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01.prefab:b0f5526fd41939a42a7da237bf2f3b67");

	// Token: 0x040022DB RID: 8923
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02.prefab:b05b69160a5a2234a8c7276b6bb82418");

	// Token: 0x040022DC RID: 8924
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03.prefab:fd72d1583ebd0a748b126e08044f4181");

	// Token: 0x040022DD RID: 8925
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04.prefab:6342c54e4556d124fa6eb64060907fc2");

	// Token: 0x040022DE RID: 8926
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01.prefab:5347ca8e73d49e3458ad43b273a8b22d");

	// Token: 0x040022DF RID: 8927
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02.prefab:494387459013c6146a4497ecb65c0ab8");

	// Token: 0x040022E0 RID: 8928
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Idle_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Idle_01.prefab:abe046261c6acca4cb3ee65b7caa0a6d");

	// Token: 0x040022E1 RID: 8929
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Idle_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Idle_02.prefab:829eae7d0040b844c8384a07b1560871");

	// Token: 0x040022E2 RID: 8930
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Idle_03 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Idle_03.prefab:e12237ce71051d74ca1c14fa312fef2a");

	// Token: 0x040022E3 RID: 8931
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Intro_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Intro_01.prefab:a95b2f05fa1296d4fb3d7224c1784944");

	// Token: 0x040022E4 RID: 8932
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01.prefab:00769729f73611444a4c0c57c66f26d7");

	// Token: 0x040022E5 RID: 8933
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01.prefab:783cd7dcdb78d67469bebb203aec84d9");

	// Token: 0x040022E6 RID: 8934
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01.prefab:685451e94347e1344a80bb2ca006eb8e");

	// Token: 0x040022E7 RID: 8935
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01.prefab:69b0a0dacde67664ea064cbb354a5535");

	// Token: 0x040022E8 RID: 8936
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01.prefab:15acdf0666a18a243a578d76b97dd4da");

	// Token: 0x040022E9 RID: 8937
	private List<string> m_HeroPowerLines = new List<string>
	{
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01,
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02,
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03,
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04
	};

	// Token: 0x040022EA RID: 8938
	private List<string> m_HeroPowerBigLines = new List<string>
	{
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01,
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02
	};

	// Token: 0x040022EB RID: 8939
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Idle_01,
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Idle_02,
		DALA_Dungeon_Boss_09h.VO_DALA_BOSS_09h_Female_Draenei_Idle_03
	};

	// Token: 0x040022EC RID: 8940
	private HashSet<string> m_playedLines = new HashSet<string>();
}
