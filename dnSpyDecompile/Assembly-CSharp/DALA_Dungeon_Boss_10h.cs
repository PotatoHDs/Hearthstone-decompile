using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000437 RID: 1079
public class DALA_Dungeon_Boss_10h : DALA_Dungeon
{
	// Token: 0x06003AC8 RID: 15048 RVA: 0x001308A4 File Offset: 0x0012EAA4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Death_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_DefeatPlayer_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Idle_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Idle_02,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Idle_04,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Intro_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_IntroSqueamlish_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01,
			DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003AC9 RID: 15049 RVA: 0x00130A28 File Offset: 0x0012EC28
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_10h.m_IdleLines;
	}

	// Token: 0x06003ACA RID: 15050 RVA: 0x00130A2F File Offset: 0x0012EC2F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01;
	}

	// Token: 0x06003ACB RID: 15051 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003ACC RID: 15052 RVA: 0x00130A67 File Offset: 0x0012EC67
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_10h.m_HeroPowerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003ACD RID: 15053 RVA: 0x00130A80 File Offset: 0x0012EC80
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Squeamlish" && cardId != "DALA_Eudora" && cardId != "DALA_Rakanishu" && cardId != "DALA_Chu")
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

	// Token: 0x06003ACE RID: 15054 RVA: 0x00130B52 File Offset: 0x0012ED52
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 2079328431U)
		{
			if (num <= 1084795990U)
			{
				if (num <= 734107143U)
				{
					if (num != 216571437U)
					{
						if (num != 734107143U)
						{
							goto IL_41B;
						}
						if (!(cardId == "LOOT_531"))
						{
							goto IL_41B;
						}
					}
					else if (!(cardId == "LOOT_014"))
					{
						goto IL_41B;
					}
				}
				else if (num != 888934466U)
				{
					if (num != 1084795990U)
					{
						goto IL_41B;
					}
					if (!(cardId == "LOOT_367"))
					{
						goto IL_41B;
					}
				}
				else if (!(cardId == "OG_082"))
				{
					goto IL_41B;
				}
			}
			else if (num <= 1340872041U)
			{
				if (num != 1110709447U)
				{
					if (num != 1340872041U)
					{
						goto IL_41B;
					}
					if (!(cardId == "CS2_142"))
					{
						goto IL_41B;
					}
				}
				else if (!(cardId == "LOOT_412"))
				{
					goto IL_41B;
				}
			}
			else if (num != 1370196256U)
			{
				if (num != 2079328431U)
				{
					goto IL_41B;
				}
				if (!(cardId == "LOOT_041"))
				{
					goto IL_41B;
				}
			}
			else
			{
				if (!(cardId == "DAL_771"))
				{
					goto IL_41B;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01, 2.5f);
				goto IL_41B;
			}
		}
		else if (num <= 2747171160U)
		{
			if (num <= 2230621192U)
			{
				if (num != 2096106050U)
				{
					if (num != 2230621192U)
					{
						goto IL_41B;
					}
					if (!(cardId == "LOOT_062"))
					{
						goto IL_41B;
					}
				}
				else if (!(cardId == "LOOT_042"))
				{
					goto IL_41B;
				}
			}
			else if (num != 2664405965U)
			{
				if (num != 2747171160U)
				{
					goto IL_41B;
				}
				if (!(cardId == "LOOT_541"))
				{
					goto IL_41B;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01, 2.5f);
				goto IL_41B;
			}
			else if (!(cardId == "TOT_033"))
			{
				goto IL_41B;
			}
		}
		else if (num <= 3450734459U)
		{
			if (num != 3333291126U)
			{
				if (num != 3450734459U)
				{
					goto IL_41B;
				}
				if (!(cardId == "LOOT_382"))
				{
					goto IL_41B;
				}
			}
			else if (!(cardId == "LOOT_389"))
			{
				goto IL_41B;
			}
		}
		else if (num != 3500375768U)
		{
			if (num != 3598724010U)
			{
				goto IL_41B;
			}
			if (!(cardId == "DAL_614"))
			{
				goto IL_41B;
			}
		}
		else if (!(cardId == "LOOT_347"))
		{
			goto IL_41B;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01, 2.5f);
		IL_41B:
		yield break;
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x00130B68 File Offset: 0x0012ED68
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
		if (!(cardId == "AT_031"))
		{
			if (cardId == "CFM_669")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040022ED RID: 8941
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01.prefab:cdcb64b82cfe35e4a8c58b6dc785472c");

	// Token: 0x040022EE RID: 8942
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01.prefab:efc3fe713f5dab1469dc8f824bf8a1d5");

	// Token: 0x040022EF RID: 8943
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Death_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Death_01.prefab:16293dd1aed2d2144bf41bae0a8d041b");

	// Token: 0x040022F0 RID: 8944
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_DefeatPlayer_01.prefab:8dcf1dfbddb0a2f47b911882aabe7d14");

	// Token: 0x040022F1 RID: 8945
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01.prefab:3e0b5bc9484bdad4ca2d17790882871f");

	// Token: 0x040022F2 RID: 8946
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01.prefab:73802bf2bf77cb1468923fdf87dcfe79");

	// Token: 0x040022F3 RID: 8947
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02.prefab:a20f211350f961845912d9521b06b8ce");

	// Token: 0x040022F4 RID: 8948
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03.prefab:5de59bf0bd060a543b753f9882acb5f3");

	// Token: 0x040022F5 RID: 8949
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04.prefab:ac6af50bd75b88c4cb53bdb3927551e7");

	// Token: 0x040022F6 RID: 8950
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05.prefab:28c58ee87c5cae84c9c8d97f9685babf");

	// Token: 0x040022F7 RID: 8951
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Idle_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Idle_01.prefab:f660bf4d2ac975348b161b0b64bc7fef");

	// Token: 0x040022F8 RID: 8952
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Idle_02 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Idle_02.prefab:5a770e18424209e428b3e4f942daa9bd");

	// Token: 0x040022F9 RID: 8953
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Idle_04 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Idle_04.prefab:c8c1bcd6f607d2f41a18f52e808e7c33");

	// Token: 0x040022FA RID: 8954
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Intro_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Intro_01.prefab:98ab83ba93e213f429b2d5fcd8dbc174");

	// Token: 0x040022FB RID: 8955
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_IntroSqueamlish_01.prefab:04e7f88bca0568b42bc978eeb265f2bc");

	// Token: 0x040022FC RID: 8956
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01.prefab:57ed1b7fa1b145146af66216263d2c9d");

	// Token: 0x040022FD RID: 8957
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01.prefab:484bad869ef760648bba09c0cf6d48de");

	// Token: 0x040022FE RID: 8958
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01.prefab:6c7497e1b64e59945a96dcda503382ed");

	// Token: 0x040022FF RID: 8959
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Idle_01,
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Idle_02,
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_Idle_04
	};

	// Token: 0x04002300 RID: 8960
	private static List<string> m_HeroPowerLines = new List<string>
	{
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03,
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04,
		DALA_Dungeon_Boss_10h.VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05
	};

	// Token: 0x04002301 RID: 8961
	private HashSet<string> m_playedLines = new HashSet<string>();
}
