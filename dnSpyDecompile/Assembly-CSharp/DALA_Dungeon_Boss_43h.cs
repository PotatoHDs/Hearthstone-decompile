using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000458 RID: 1112
public class DALA_Dungeon_Boss_43h : DALA_Dungeon
{
	// Token: 0x06003C65 RID: 15461 RVA: 0x0013ACDC File Offset: 0x00138EDC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_Death_02,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_Intro_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02,
			DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C66 RID: 15462 RVA: 0x0013AE60 File Offset: 0x00139060
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01;
	}

	// Token: 0x06003C67 RID: 15463 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003C68 RID: 15464 RVA: 0x0013AE98 File Offset: 0x00139098
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003C69 RID: 15465 RVA: 0x0013AF43 File Offset: 0x00139143
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_TurnStart);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_RopeExplodes);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C6A RID: 15466 RVA: 0x0013AF59 File Offset: 0x00139159
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
		if (!(cardId == "EX1_560"))
		{
			if (!(cardId == "LOOT_538"))
			{
				if (cardId == "UNG_028t")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003C6B RID: 15467 RVA: 0x0013AF6F File Offset: 0x0013916F
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
		if (cardId == "UNG_028t")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040025E9 RID: 9705
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01.prefab:183d37915f836cf4e9cc5545136e7771");

	// Token: 0x040025EA RID: 9706
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_Death_02 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_Death_02.prefab:9f007104467bc8b47bddf8814f36a796");

	// Token: 0x040025EB RID: 9707
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_DefeatPlayer_01.prefab:03602ba877b971942b82211782b024a4");

	// Token: 0x040025EC RID: 9708
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01.prefab:fd1117caffcf287419549fd83cb9c45a");

	// Token: 0x040025ED RID: 9709
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_Intro_01.prefab:9eeced40701ab874d9a2224f876db8a3");

	// Token: 0x040025EE RID: 9710
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01.prefab:64d2445dabb900e499ccee0f17b585e7");

	// Token: 0x040025EF RID: 9711
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01.prefab:70aafb0f704a8774ca8bc4f2c1bd6024");

	// Token: 0x040025F0 RID: 9712
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01.prefab:825ee80004394b3428f00c3f67def6d9");

	// Token: 0x040025F1 RID: 9713
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01.prefab:d23bbbe5030cca14b9b6b27122ec31c2");

	// Token: 0x040025F2 RID: 9714
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02.prefab:d5e11881cc8e7da4bb5808e5c53d27fa");

	// Token: 0x040025F3 RID: 9715
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03.prefab:1bd952c1fcbb3284e8d0f6a4523f22e1");

	// Token: 0x040025F4 RID: 9716
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04.prefab:15086e2a34483e142a09f38854c2d840");

	// Token: 0x040025F5 RID: 9717
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05.prefab:06821994a69f851468ac0694e344503d");

	// Token: 0x040025F6 RID: 9718
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06.prefab:83ca55c537e3df3429ac37a67ffe25f9");

	// Token: 0x040025F7 RID: 9719
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01.prefab:7702bf67f79a89643a49d91745bc0000");

	// Token: 0x040025F8 RID: 9720
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01.prefab:7f7f9af17e3db0249ac4b62a87f5681f");

	// Token: 0x040025F9 RID: 9721
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02.prefab:da28857aa4b21e348a94d31e5397972a");

	// Token: 0x040025FA RID: 9722
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03.prefab:46f1b3f800c9f304aa55bf2599315e43");

	// Token: 0x040025FB RID: 9723
	private List<string> m_RopeExplodes = new List<string>
	{
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01,
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02,
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03,
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04,
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05,
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06
	};

	// Token: 0x040025FC RID: 9724
	private List<string> m_TurnStart = new List<string>
	{
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01,
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02,
		DALA_Dungeon_Boss_43h.VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03
	};

	// Token: 0x040025FD RID: 9725
	private HashSet<string> m_playedLines = new HashSet<string>();
}
