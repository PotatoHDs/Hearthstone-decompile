using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000455 RID: 1109
public class DALA_Dungeon_Boss_40h : DALA_Dungeon
{
	// Token: 0x06003C3E RID: 15422 RVA: 0x00139E34 File Offset: 0x00138034
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_BossHellfire_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_BossSoulfire_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Death_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_EmoteResponse_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_04,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_05,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_07,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Idle_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Idle_02,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Idle_03,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Intro_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_IntroGeorge_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_IntroTekahn_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_01,
			DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C3F RID: 15423 RVA: 0x00139FC8 File Offset: 0x001381C8
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_40h.m_IdleLines;
	}

	// Token: 0x06003C40 RID: 15424 RVA: 0x00139FCF File Offset: 0x001381CF
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_EmoteResponse_01;
	}

	// Token: 0x06003C41 RID: 15425 RVA: 0x0013A008 File Offset: 0x00138208
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003C42 RID: 15426 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C43 RID: 15427 RVA: 0x0013A12E File Offset: 0x0013832E
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
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_40h.m_PlayerDemon);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_40h.m_HeroPowerTrigger);
		}
		yield break;
	}

	// Token: 0x06003C44 RID: 15428 RVA: 0x0013A144 File Offset: 0x00138344
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
		yield break;
	}

	// Token: 0x06003C45 RID: 15429 RVA: 0x0013A15A File Offset: 0x0013835A
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "CS2_062"))
		{
			if (cardId == "EX1_308")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_BossSoulfire_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_BossHellfire_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040025A8 RID: 9640
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_BossHellfire_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_BossHellfire_01.prefab:eab07753d5b20ff4f88c59fa65ecaded");

	// Token: 0x040025A9 RID: 9641
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_BossSoulfire_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_BossSoulfire_01.prefab:cd9ef1ba1cc9da94d864036103510664");

	// Token: 0x040025AA RID: 9642
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_Death_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_Death_01.prefab:ee93c75aa8c32f240be03d296981597b");

	// Token: 0x040025AB RID: 9643
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_DefeatPlayer_01.prefab:5b3f52e8fed509b4c99e376c259363ea");

	// Token: 0x040025AC RID: 9644
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_EmoteResponse_01.prefab:c037e2bea37831e48a0e5c784f106d60");

	// Token: 0x040025AD RID: 9645
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_01.prefab:7f99351632f125342b882f6b84f62ce3");

	// Token: 0x040025AE RID: 9646
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_02.prefab:92a2d289e65e24f4e83991381f7c90d2");

	// Token: 0x040025AF RID: 9647
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_03.prefab:62c2b85fb9a7a7b4796da98365788b8b");

	// Token: 0x040025B0 RID: 9648
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_04.prefab:7107e6ba92469a248b568b899e18560b");

	// Token: 0x040025B1 RID: 9649
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_05.prefab:7629c3883ea0ead4dab7e7c8ce5a4469");

	// Token: 0x040025B2 RID: 9650
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_07 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_07.prefab:b8f4f41f80a949d4980866798a3fbb42");

	// Token: 0x040025B3 RID: 9651
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_Idle_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_Idle_01.prefab:8c36b680bf54d8e46bc6151805f40dbf");

	// Token: 0x040025B4 RID: 9652
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_Idle_02 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_Idle_02.prefab:8fff03a9803120d49a3a1e04ded78b80");

	// Token: 0x040025B5 RID: 9653
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_Idle_03 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_Idle_03.prefab:736ec88177e65c5419c1e63d5f7a1677");

	// Token: 0x040025B6 RID: 9654
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_Intro_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_Intro_01.prefab:6c1dea20918bacd4ea534f58b39bbb0b");

	// Token: 0x040025B7 RID: 9655
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_IntroGeorge_01.prefab:0b2620bb77ce857438c21fab7a946ad6");

	// Token: 0x040025B8 RID: 9656
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_IntroTekahn_01.prefab:da25ce0d10163834a9249d899d12dd27");

	// Token: 0x040025B9 RID: 9657
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_01 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_01.prefab:85e969203fa0fa5439fd5dad5ff2c343");

	// Token: 0x040025BA RID: 9658
	private static readonly AssetReference VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_02 = new AssetReference("VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_02.prefab:a5e4d15fe4ca80747856ef1926bc45d6");

	// Token: 0x040025BB RID: 9659
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Idle_01,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Idle_02,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_Idle_03
	};

	// Token: 0x040025BC RID: 9660
	private static List<string> m_HeroPowerTrigger = new List<string>
	{
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_03,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_04,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_05,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_HeroPowerTrigger_07
	};

	// Token: 0x040025BD RID: 9661
	private static List<string> m_PlayerDemon = new List<string>
	{
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_01,
		DALA_Dungeon_Boss_40h.VO_DALA_BOSS_40h_Female_NightElf_PlayerDemon_02
	};

	// Token: 0x040025BE RID: 9662
	private HashSet<string> m_playedLines = new HashSet<string>();
}
