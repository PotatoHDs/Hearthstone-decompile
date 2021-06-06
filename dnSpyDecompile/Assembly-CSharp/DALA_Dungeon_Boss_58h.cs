using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000467 RID: 1127
public class DALA_Dungeon_Boss_58h : DALA_Dungeon
{
	// Token: 0x06003D1C RID: 15644 RVA: 0x0013FF78 File Offset: 0x0013E178
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Death_02,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_DefeatPlayer_02,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_02,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_03,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_04,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Intro_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D1D RID: 15645 RVA: 0x0014023C File Offset: 0x0013E43C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01;
	}

	// Token: 0x06003D1E RID: 15646 RVA: 0x00140274 File Offset: 0x0013E474
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_58h.m_IdleLines;
	}

	// Token: 0x06003D1F RID: 15647 RVA: 0x0014027C File Offset: 0x0013E47C
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05,
			DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06
		};
	}

	// Token: 0x06003D20 RID: 15648 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003D21 RID: 15649 RVA: 0x001402F0 File Offset: 0x0013E4F0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_RAkanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003D22 RID: 15650 RVA: 0x00140409 File Offset: 0x0013E609
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.m_TurnOne);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01, 2.5f);
			break;
		case 107:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.m_WiresTrigger);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D23 RID: 15651 RVA: 0x0014041F File Offset: 0x0013E61F
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
		if (!(cardId == "LOOTA_838"))
		{
			if (!(cardId == "BOT_034"))
			{
				if (!(cardId == "BOT_429"))
				{
					if (!(cardId == "GVG_110"))
					{
						if (!(cardId == "DALA_716"))
						{
							if (cardId == "BOT_031")
							{
								yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D24 RID: 15652 RVA: 0x00140435 File Offset: 0x0013E635
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
		if (!(cardId == "BOT_031"))
		{
			if (cardId == "BOT_511" || cardId == "DAL_060" || cardId == "GVG_056t")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400278F RID: 10127
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01.prefab:c018edf5d81376645bae333230a0a84b");

	// Token: 0x04002790 RID: 10128
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01.prefab:1e1e25e3e7ffa404d9d0fe5b0e11d820");

	// Token: 0x04002791 RID: 10129
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01.prefab:5349b2330a4594f4e9372bcf06f73138");

	// Token: 0x04002792 RID: 10130
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Death_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Death_02.prefab:6727de2312a8965458798bf2fd5fb78d");

	// Token: 0x04002793 RID: 10131
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_DefeatPlayer_02.prefab:b45759b23450ad041ae25605fb05ac12");

	// Token: 0x04002794 RID: 10132
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01.prefab:39fc19e5c6f5dcb49b9b22fc72876d01");

	// Token: 0x04002795 RID: 10133
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01.prefab:7716e39859a4f90468597e461b956413");

	// Token: 0x04002796 RID: 10134
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02.prefab:446254a13756c4b48be1e54ebc16fe89");

	// Token: 0x04002797 RID: 10135
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03.prefab:99e665ea05a93ad47930687edbe49124");

	// Token: 0x04002798 RID: 10136
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04.prefab:15765d15daaf2434f91cf054ea69eb61");

	// Token: 0x04002799 RID: 10137
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05.prefab:662cf91675ef18e4c961f042adc1018e");

	// Token: 0x0400279A RID: 10138
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06.prefab:2f1ba1da1c417914f9c6981f805e8091");

	// Token: 0x0400279B RID: 10139
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01.prefab:0216a6aac9cb52a4291f432aeaefdc97");

	// Token: 0x0400279C RID: 10140
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_01.prefab:d72ec50b5af6bfe41ac4ac1fb77c6afa");

	// Token: 0x0400279D RID: 10141
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_02.prefab:014362af9edf01c439dea0cd9be8b423");

	// Token: 0x0400279E RID: 10142
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_03.prefab:68f4e1db56e257b48a92fa185dee6f5d");

	// Token: 0x0400279F RID: 10143
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_04.prefab:210958be480c3e04ba84f2ba1932d8d7");

	// Token: 0x040027A0 RID: 10144
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Intro_01.prefab:19a499d9201d8e048972a0a3bd1963ad");

	// Token: 0x040027A1 RID: 10145
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01.prefab:ee8299fe7a2fda7419575752a0c6d340");

	// Token: 0x040027A2 RID: 10146
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01.prefab:4368deef56bdf014c9fb54c686cb6eea");

	// Token: 0x040027A3 RID: 10147
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01.prefab:1d1594903e88b6940a20d15cac6bd7ae");

	// Token: 0x040027A4 RID: 10148
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01.prefab:76b2c7b6d4892124fa1013a52e8b1106");

	// Token: 0x040027A5 RID: 10149
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01.prefab:5f0002ac7dd14d349a714be0066a4fc7");

	// Token: 0x040027A6 RID: 10150
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01.prefab:d4c69fc26993b2e48af11ba8d6b66c41");

	// Token: 0x040027A7 RID: 10151
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01.prefab:65ee7c7a61783cf4d8082efabad07474");

	// Token: 0x040027A8 RID: 10152
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01.prefab:4781d4c1863aaa0439e311246a28bd56");

	// Token: 0x040027A9 RID: 10153
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01.prefab:38ac059bf6795464fa02666f1a1ceb3f");

	// Token: 0x040027AA RID: 10154
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01.prefab:a93153188d08dbb439d78e055aaca36d");

	// Token: 0x040027AB RID: 10155
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02.prefab:dce4bf49dc723294cb2e38b92ccc1184");

	// Token: 0x040027AC RID: 10156
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01.prefab:b16fcec3bc9d32c45921ddf858172d48");

	// Token: 0x040027AD RID: 10157
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01.prefab:30ebb6945e952044e8db8480d9ffd79a");

	// Token: 0x040027AE RID: 10158
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01.prefab:4840364699660be40af966effa48ae79");

	// Token: 0x040027AF RID: 10159
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02.prefab:678a22ea088f77240b824d2d1adfd706");

	// Token: 0x040027B0 RID: 10160
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03.prefab:78f65f0bdbcdd864783f51d900641ee5");

	// Token: 0x040027B1 RID: 10161
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04.prefab:51d6db609bff30a4686b2c966c49a244");

	// Token: 0x040027B2 RID: 10162
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05.prefab:0972d7dddb7ec394e97b564429883d9b");

	// Token: 0x040027B3 RID: 10163
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08.prefab:819cdfc24f908d94a9e81f9f713dd378");

	// Token: 0x040027B4 RID: 10164
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09.prefab:a4ba172c63a39244e8e81fa3c21d120b");

	// Token: 0x040027B5 RID: 10165
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_01,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_02,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_03,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_Idle_04
	};

	// Token: 0x040027B6 RID: 10166
	private static List<string> m_WiresTrigger = new List<string>
	{
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09
	};

	// Token: 0x040027B7 RID: 10167
	private static List<string> m_TurnOne = new List<string>
	{
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01,
		DALA_Dungeon_Boss_58h.VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02
	};

	// Token: 0x040027B8 RID: 10168
	private HashSet<string> m_playedLines = new HashSet<string>();
}
