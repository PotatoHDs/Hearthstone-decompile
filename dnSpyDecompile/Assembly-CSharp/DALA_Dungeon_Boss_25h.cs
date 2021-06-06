using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000446 RID: 1094
public class DALA_Dungeon_Boss_25h : DALA_Dungeon
{
	// Token: 0x06003B84 RID: 15236 RVA: 0x00135388 File Offset: 0x00133588
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Death_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_DefeatPlayer_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Idle_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Idle_03,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Idle_04,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Intro_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01,
			DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B85 RID: 15237 RVA: 0x0013561C File Offset: 0x0013381C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003B86 RID: 15238 RVA: 0x00135654 File Offset: 0x00133854
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_25h.m_IdleLines;
	}

	// Token: 0x06003B87 RID: 15239 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B88 RID: 15240 RVA: 0x0013565C File Offset: 0x0013385C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish" && cardId != "DALA_Chu")
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

	// Token: 0x06003B89 RID: 15241 RVA: 0x00135753 File Offset: 0x00133953
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01, 2.5f);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.m_HeroPowerPlayerTrigger);
			break;
		case 107:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.m_HeroPowerBossTrigger);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003B8A RID: 15242 RVA: 0x00135769 File Offset: 0x00133969
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
		if (num <= 3463079251U)
		{
			if (num <= 1062973017U)
			{
				if (num <= 286250261U)
				{
					if (num != 167543092U)
					{
						if (num == 286250261U)
						{
							if (cardId == "AT_033")
							{
								yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02, 2.5f);
							}
						}
					}
					else if (cardId == "BOT_270")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01, 2.5f);
					}
				}
				else if (num != 1046195398U)
				{
					if (num == 1062973017U)
					{
						if (cardId == "GIL_124")
						{
							yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01, 2.5f);
						}
					}
				}
				else if (cardId == "GIL_125")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02, 2.5f);
				}
			}
			else if (num <= 1638365393U)
			{
				if (num != 1114593127U)
				{
					if (num == 1638365393U)
					{
						if (cardId == "HERO_05b")
						{
							yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01, 2.5f);
						}
					}
				}
				else if (cardId == "GVG_119")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02, 2.5f);
				}
			}
			else if (num != 2174476927U)
			{
				if (num == 3463079251U)
				{
					if (cardId == "BOT_020")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01, 2.5f);
					}
				}
			}
			else if (cardId == "DALA_BOSS_25t")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01, 2.5f);
			}
		}
		else
		{
			if (num <= 3785628939U)
			{
				if (num <= 3598724010U)
				{
					if (num != 3514835915U)
					{
						if (num != 3598724010U)
						{
							goto IL_694;
						}
						if (!(cardId == "DAL_614"))
						{
							goto IL_694;
						}
					}
					else if (!(cardId == "DAL_613"))
					{
						goto IL_694;
					}
				}
				else if (num != 3615501629U)
				{
					if (num != 3785628939U)
					{
						goto IL_694;
					}
					if (!(cardId == "DAL_741"))
					{
						goto IL_694;
					}
				}
				else if (!(cardId == "DAL_615"))
				{
					goto IL_694;
				}
			}
			else if (num <= 3836108891U)
			{
				if (num != 3786761772U)
				{
					if (num != 3836108891U)
					{
						goto IL_694;
					}
					if (!(cardId == "DAL_752"))
					{
						goto IL_694;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01, 2.5f);
					goto IL_694;
				}
				else if (!(cardId == "DAL_739"))
				{
					goto IL_694;
				}
			}
			else if (num != 4099887571U)
			{
				if (num != 4111313222U)
				{
					if (num != 4280660314U)
					{
						goto IL_694;
					}
					if (!(cardId == "DAL_063"))
					{
						goto IL_694;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01, 2.5f);
					goto IL_694;
				}
				else
				{
					if (!(cardId == "BOT_283"))
					{
						goto IL_694;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01, 2.5f);
					goto IL_694;
				}
			}
			else
			{
				if (!(cardId == "GIL_614"))
				{
					goto IL_694;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01, 2.5f);
				goto IL_694;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01, 2.5f);
		}
		IL_694:
		yield break;
	}

	// Token: 0x06003B8B RID: 15243 RVA: 0x0013577F File Offset: 0x0013397F
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
		if (cardId == "DALA_BOSS_25t")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_25h.m_BossHordeOfToys);
		}
		yield break;
	}

	// Token: 0x04002449 RID: 9289
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01.prefab:642b197412703f943a7bd6bae7fce20a");

	// Token: 0x0400244A RID: 9290
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03.prefab:4965501287045dc499b89deea01e4516");

	// Token: 0x0400244B RID: 9291
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04.prefab:ec1896aa4bde2e846aa4d9772eb30bf0");

	// Token: 0x0400244C RID: 9292
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Death_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Death_01.prefab:c489792aa9ab2c84f88e28236442f0d0");

	// Token: 0x0400244D RID: 9293
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_DefeatPlayer_01.prefab:71c9b741070ea484297de555cfa46dcd");

	// Token: 0x0400244E RID: 9294
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01.prefab:35451f0b9b2cc604ca4fff8a4cd47465");

	// Token: 0x0400244F RID: 9295
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01.prefab:7f057d778d09ab14bae2898fd86bf278");

	// Token: 0x04002450 RID: 9296
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02.prefab:2a043615cf4c53c4389e3cb15ba23045");

	// Token: 0x04002451 RID: 9297
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01.prefab:54ace46e444df87499b62107f27175e9");

	// Token: 0x04002452 RID: 9298
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02.prefab:72d75b01b6cbd704faa2de79aa272986");

	// Token: 0x04002453 RID: 9299
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03.prefab:3cb36417937c6e04aac616e148960e17");

	// Token: 0x04002454 RID: 9300
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01.prefab:02fdba25bea00be478a5e912a5a95f9f");

	// Token: 0x04002455 RID: 9301
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Idle_01.prefab:6590bda1aea1f534f987e95bc906b372");

	// Token: 0x04002456 RID: 9302
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Idle_03.prefab:b7f30f385ea28cb4e880d5795eb78170");

	// Token: 0x04002457 RID: 9303
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Idle_04 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Idle_04.prefab:9bcc7c80ab32dd140aca06cf86646ea9");

	// Token: 0x04002458 RID: 9304
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Intro_01.prefab:f2426e0cb62783f448f47c5ee6dcc91e");

	// Token: 0x04002459 RID: 9305
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01.prefab:256912597be247a4390af888c7741a59");

	// Token: 0x0400245A RID: 9306
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02.prefab:4c98caddb50c6164bb73f3e7408603a2");

	// Token: 0x0400245B RID: 9307
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02.prefab:05c4a1ff34a46ba4cb53cf6855a16b50");

	// Token: 0x0400245C RID: 9308
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01.prefab:33a4a93c38ec5d44189132f52b09dfe5");

	// Token: 0x0400245D RID: 9309
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01.prefab:62c8ebeb4bbcb104fb2a0a3d4c09568a");

	// Token: 0x0400245E RID: 9310
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01.prefab:dc8ae52ee7b83bf478509adc3be8e803");

	// Token: 0x0400245F RID: 9311
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01.prefab:c0e950486a2c24549911b26b11514ce0");

	// Token: 0x04002460 RID: 9312
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01.prefab:004d3f76f4b38704795b28fa4e8ff8a8");

	// Token: 0x04002461 RID: 9313
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02.prefab:9ffaea17a417d414698f72aae7969d22");

	// Token: 0x04002462 RID: 9314
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01.prefab:2322868af9f4f2b46bff916aa4d3c7b0");

	// Token: 0x04002463 RID: 9315
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01.prefab:3e1c4653d620c8144b92ea7b47b6ee63");

	// Token: 0x04002464 RID: 9316
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01.prefab:c290c7a4e6148554c88123750e1e4928");

	// Token: 0x04002465 RID: 9317
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01.prefab:5dfbf8d0c5dabbd4a9452ca9ea7e4e35");

	// Token: 0x04002466 RID: 9318
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01.prefab:fbc2098a05c78314f96a7419a08d1431");

	// Token: 0x04002467 RID: 9319
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01.prefab:af1824e515f100e48a412fa5319ba959");

	// Token: 0x04002468 RID: 9320
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01.prefab:08d019a47a171c845a30b8574b7de99a");

	// Token: 0x04002469 RID: 9321
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01.prefab:14e0d01e64e9f4d46a2ca802e69257b0");

	// Token: 0x0400246A RID: 9322
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01.prefab:dfc3ed17136b0b04795a33e0571b3c1a");

	// Token: 0x0400246B RID: 9323
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01.prefab:58c2d75aa76c72f408572597ebea04b8");

	// Token: 0x0400246C RID: 9324
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Idle_01,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Idle_03,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_Idle_04
	};

	// Token: 0x0400246D RID: 9325
	private static List<string> m_BossHordeOfToys = new List<string>
	{
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04
	};

	// Token: 0x0400246E RID: 9326
	private static List<string> m_HeroPowerPlayerTrigger = new List<string>
	{
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03
	};

	// Token: 0x0400246F RID: 9327
	private static List<string> m_HeroPowerBossTrigger = new List<string>
	{
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01,
		DALA_Dungeon_Boss_25h.VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02
	};

	// Token: 0x04002470 RID: 9328
	private HashSet<string> m_playedLines = new HashSet<string>();
}
