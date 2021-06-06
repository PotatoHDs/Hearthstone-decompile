using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000442 RID: 1090
public class DALA_Dungeon_Boss_21h : DALA_Dungeon
{
	// Token: 0x06003B52 RID: 15186 RVA: 0x001340B4 File Offset: 0x001322B4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_03,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_04,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Death_02,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_EmoteResponse_02,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Idle_02,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Intro_02,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerCairne_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerETC_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerSecret_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerSecret_02,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_02,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_03,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_WrongPrediction_01,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_WrongPrediction_03,
			DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_WrongPrediction_04
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B53 RID: 15187 RVA: 0x00134288 File Offset: 0x00132488
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Intro_02;
		this.m_deathLine = DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_EmoteResponse_02;
	}

	// Token: 0x06003B54 RID: 15188 RVA: 0x001342C0 File Offset: 0x001324C0
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_21h.m_IdleLines;
	}

	// Token: 0x06003B55 RID: 15189 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003B56 RID: 15190 RVA: 0x001342C8 File Offset: 0x001324C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_George" && cardId != "DALA_Eudora")
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

	// Token: 0x06003B57 RID: 15191 RVA: 0x00134380 File Offset: 0x00132580
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (this.m_enemySpeaking)
		{
			switch (missionEvent)
			{
			case 101:
				this.m_PredictionSpoken = false;
				yield break;
			case 102:
				this.m_PredictionSpoken = false;
				yield break;
			case 103:
				this.m_PredictionSpoken = false;
				yield break;
			}
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			this.m_PredictionSpoken = true;
			yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_01, 2.5f);
			goto IL_3D7;
		case 102:
			this.m_PredictionSpoken = true;
			yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_02, 2.5f);
			goto IL_3D7;
		case 103:
			this.m_PredictionSpoken = true;
			yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_03, 2.5f);
			goto IL_3D7;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerSecret);
			goto IL_3D7;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_21h.m_BossHeroPower);
			goto IL_3D7;
		case 106:
		case 107:
		case 108:
		case 109:
		case 110:
			break;
		case 111:
			if (this.m_PredictionSpoken)
			{
				this.m_PredictionSpoken = false;
				yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_WrongPrediction_01, 2.5f);
				goto IL_3D7;
			}
			goto IL_3D7;
		case 112:
			if (this.m_PredictionSpoken)
			{
				this.m_PredictionSpoken = false;
				yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_WrongPrediction_03, 2.5f);
				goto IL_3D7;
			}
			goto IL_3D7;
		case 113:
			if (this.m_PredictionSpoken)
			{
				this.m_PredictionSpoken = false;
				yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_WrongPrediction_04, 2.5f);
				goto IL_3D7;
			}
			goto IL_3D7;
		default:
			switch (missionEvent)
			{
			case 131:
				if (this.m_PredictionSpoken)
				{
					this.m_PredictionSpoken = false;
					yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_01, 2.5f);
					goto IL_3D7;
				}
				goto IL_3D7;
			case 132:
				if (this.m_PredictionSpoken)
				{
					this.m_PredictionSpoken = false;
					yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_03, 2.5f);
					goto IL_3D7;
				}
				goto IL_3D7;
			case 133:
				if (this.m_PredictionSpoken)
				{
					this.m_PredictionSpoken = false;
					yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_04, 2.5f);
					goto IL_3D7;
				}
				goto IL_3D7;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3D7:
		yield break;
	}

	// Token: 0x06003B58 RID: 15192 RVA: 0x00134396 File Offset: 0x00132596
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
		if (!(cardId == "PRO_001"))
		{
			if (cardId == "EX1_110")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerCairne_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerETC_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003B59 RID: 15193 RVA: 0x001343AC File Offset: 0x001325AC
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

	// Token: 0x040023EC RID: 9196
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_01.prefab:99d0ed6d1c54ec4448ae857bbae61e9b");

	// Token: 0x040023ED RID: 9197
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_03 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_03.prefab:e12f7cb8444f0ae4388785973cf2fee7");

	// Token: 0x040023EE RID: 9198
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_04 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_CorrectPrediction_04.prefab:733a8602cad771b4aa3ae34cb4e069dd");

	// Token: 0x040023EF RID: 9199
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_Death_02 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_Death_02.prefab:62d36b042e742b8409bde9ea0c929ae6");

	// Token: 0x040023F0 RID: 9200
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_DefeatPlayer_01.prefab:a88c48cc7be215e4c9f3a236675aae40");

	// Token: 0x040023F1 RID: 9201
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_EmoteResponse_02 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_EmoteResponse_02.prefab:a6d855255b1e95f42a96c365119d0a74");

	// Token: 0x040023F2 RID: 9202
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_HeroPower_01.prefab:65d44008864b3024cb00581af8874746");

	// Token: 0x040023F3 RID: 9203
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_HeroPower_03.prefab:bf8ae6909691ebf4b81ee01459780941");

	// Token: 0x040023F4 RID: 9204
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_HeroPower_04.prefab:3b3b4498ea12c9a4f877fb83225c3cea");

	// Token: 0x040023F5 RID: 9205
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_Idle_01.prefab:3689bdca2b6bf2c458a399dfa4a06f68");

	// Token: 0x040023F6 RID: 9206
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_Idle_02.prefab:0357bdf1f24ae164f94a614df5a49dda");

	// Token: 0x040023F7 RID: 9207
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_Idle_03.prefab:721ba6a06db3fa34ab5a7896efc8550e");

	// Token: 0x040023F8 RID: 9208
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_Intro_02 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_Intro_02.prefab:4ba21e06012280847850f37f2d6f11d3");

	// Token: 0x040023F9 RID: 9209
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_PlayerCairne_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_PlayerCairne_01.prefab:f5eed11a03020c144bc145ec42acb202");

	// Token: 0x040023FA RID: 9210
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_PlayerETC_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_PlayerETC_01.prefab:14361c1273dc26e48837e9da265a5c51");

	// Token: 0x040023FB RID: 9211
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_PlayerSecret_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_PlayerSecret_01.prefab:3b17e725dd858c844a7e007f70b459fd");

	// Token: 0x040023FC RID: 9212
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_PlayerSecret_02 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_PlayerSecret_02.prefab:ee73d80fec75e4144b9d05a87d9157bf");

	// Token: 0x040023FD RID: 9213
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_01.prefab:c1980252cb29bfc42ba413a8ee6c1fe9");

	// Token: 0x040023FE RID: 9214
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_02 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_02.prefab:8aec5aa686a78504da924873dda3fe3f");

	// Token: 0x040023FF RID: 9215
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_03 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_StartOfTurnPrediction_03.prefab:e71188bfb1ce70e4f9ea2b01c5421b5c");

	// Token: 0x04002400 RID: 9216
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_WrongPrediction_01 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_WrongPrediction_01.prefab:721173acc263c044d814dcaccb5c129d");

	// Token: 0x04002401 RID: 9217
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_WrongPrediction_03 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_WrongPrediction_03.prefab:d0bd76ef2efa59548815cd669a930511");

	// Token: 0x04002402 RID: 9218
	private static readonly AssetReference VO_DALA_BOSS_21h_Male_Human_WrongPrediction_04 = new AssetReference("VO_DALA_BOSS_21h_Male_Human_WrongPrediction_04.prefab:d896b394c7b38ef4999c2dfa760bd528");

	// Token: 0x04002403 RID: 9219
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04002404 RID: 9220
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_HeroPower_01,
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_HeroPower_03,
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_HeroPower_04
	};

	// Token: 0x04002405 RID: 9221
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Idle_02,
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_Idle_03
	};

	// Token: 0x04002406 RID: 9222
	private List<string> m_PlayerSecret = new List<string>
	{
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerSecret_01,
		DALA_Dungeon_Boss_21h.VO_DALA_BOSS_21h_Male_Human_PlayerSecret_02
	};

	// Token: 0x04002407 RID: 9223
	private bool m_PredictionSpoken;
}
