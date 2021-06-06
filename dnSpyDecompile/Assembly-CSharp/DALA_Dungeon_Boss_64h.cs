using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200046D RID: 1133
public class DALA_Dungeon_Boss_64h : DALA_Dungeon
{
	// Token: 0x06003D68 RID: 15720 RVA: 0x001423B4 File Offset: 0x001405B4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Death_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_DefeatPlayer_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Exposition_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Exposition_02,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_04,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_05,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_06,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_07,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_08,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Idle_02,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Idle_03,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Idle_04,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Intro_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_IntroChu_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D69 RID: 15721 RVA: 0x00142598 File Offset: 0x00140798
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01;
	}

	// Token: 0x06003D6A RID: 15722 RVA: 0x001425D0 File Offset: 0x001407D0
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_01,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_04,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_05,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_06,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_07,
			DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_HeroPower_08
		};
	}

	// Token: 0x06003D6B RID: 15723 RVA: 0x00142642 File Offset: 0x00140842
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_64h.m_IdleLines;
	}

	// Token: 0x06003D6C RID: 15724 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D6D RID: 15725 RVA: 0x0014264C File Offset: 0x0014084C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003D6E RID: 15726 RVA: 0x00142765 File Offset: 0x00140965
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Exposition_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Exposition_02, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D6F RID: 15727 RVA: 0x0014277B File Offset: 0x0014097B
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
		if (!(cardId == "AT_033"))
		{
			if (!(cardId == "GAME_005"))
			{
				if (!(cardId == "DAL_088"))
				{
					if (cardId == "DAL_771")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D70 RID: 15728 RVA: 0x00142791 File Offset: 0x00140991
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
		if (!(cardId == "DAL_088"))
		{
			if (cardId == "CFM_940")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400284C RID: 10316
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01.prefab:b2a2a58d21a00cf4dab4d71a2f244492");

	// Token: 0x0400284D RID: 10317
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01.prefab:6a5496cc4ff8c504ab22bef0db913acc");

	// Token: 0x0400284E RID: 10318
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01.prefab:003303c1bb2f0da46887539f2cb966e1");

	// Token: 0x0400284F RID: 10319
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Death_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Death_01.prefab:a5c9bb6c02f5c6f45917b1f922f896a0");

	// Token: 0x04002850 RID: 10320
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_DefeatPlayer_01.prefab:646ce5edac52d0640b930fd3ea828943");

	// Token: 0x04002851 RID: 10321
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01.prefab:e48ccde61bd17914f998e285ce9cfa26");

	// Token: 0x04002852 RID: 10322
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Exposition_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Exposition_01.prefab:6995faf495156174689ab4f0d438641f");

	// Token: 0x04002853 RID: 10323
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Exposition_02 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Exposition_02.prefab:13057285d08166c4fa482e80427706e5");

	// Token: 0x04002854 RID: 10324
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_01.prefab:f15093db68319854a87c1ec81ee7d545");

	// Token: 0x04002855 RID: 10325
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_04 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_04.prefab:095c39082802db844b5e8a31d82391c2");

	// Token: 0x04002856 RID: 10326
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_05 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_05.prefab:10354dd7df73fa74bb9f9f13ef590079");

	// Token: 0x04002857 RID: 10327
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_06 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_06.prefab:8d3549d8cfc0904459793166752ad979");

	// Token: 0x04002858 RID: 10328
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_07 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_07.prefab:25aa90ac5b84d444493e5bb9266f3bd1");

	// Token: 0x04002859 RID: 10329
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_08 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_08.prefab:05fb49e541e6b2944a646864fcbc9526");

	// Token: 0x0400285A RID: 10330
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Idle_02 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Idle_02.prefab:a62396e00d1a4a14fbccb7f6b12fd02d");

	// Token: 0x0400285B RID: 10331
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Idle_03 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Idle_03.prefab:fb458f82444a32c43826f0ba39095357");

	// Token: 0x0400285C RID: 10332
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Idle_04 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Idle_04.prefab:123a0be21529dd949be25e52f6f0c30e");

	// Token: 0x0400285D RID: 10333
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Intro_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Intro_01.prefab:eaf0436940f1d5a4caf1458b31474545");

	// Token: 0x0400285E RID: 10334
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_IntroChu_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_IntroChu_01.prefab:0bb7f4c0bab9ef548a2c4fdd4d384286");

	// Token: 0x0400285F RID: 10335
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01.prefab:b948e4420698bfb459e7c35538d061f1");

	// Token: 0x04002860 RID: 10336
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01.prefab:f595a759222325f45bd13a25b7def66f");

	// Token: 0x04002861 RID: 10337
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01.prefab:9cb4494da4713604481a80d5a94810a4");

	// Token: 0x04002862 RID: 10338
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01.prefab:21844d3d1c16d154583a72e786263375");

	// Token: 0x04002863 RID: 10339
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01.prefab:1484edf934d4d70458d9ff65f1257010");

	// Token: 0x04002864 RID: 10340
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Idle_02,
		DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Idle_03,
		DALA_Dungeon_Boss_64h.VO_DALA_BOSS_64h_Male_Orc_Idle_04
	};

	// Token: 0x04002865 RID: 10341
	private HashSet<string> m_playedLines = new HashSet<string>();
}
