using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000468 RID: 1128
public class DALA_Dungeon_Boss_59h : DALA_Dungeon
{
	// Token: 0x06003D2A RID: 15658 RVA: 0x00140798 File Offset: 0x0013E998
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Death_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_DefeatPlayer_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Idle_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Idle_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Idle_03,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Intro_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_IntroChu_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Misc_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Misc_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01,
			DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D2B RID: 15659 RVA: 0x00140A3C File Offset: 0x0013EC3C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_59h.m_IdleLines;
	}

	// Token: 0x06003D2C RID: 15660 RVA: 0x00140A43 File Offset: 0x0013EC43
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01;
	}

	// Token: 0x06003D2D RID: 15661 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003D2E RID: 15662 RVA: 0x00140A7C File Offset: 0x0013EC7C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Chu" && cardId != "DALA_Vessina" && cardId != "DALA_Squeamlish")
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

	// Token: 0x06003D2F RID: 15663 RVA: 0x00140B80 File Offset: 0x0013ED80
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Misc_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Misc_02, 2.5f);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_59h.m_HeroPower);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_59h.m_HeroPowerTreasure);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D30 RID: 15664 RVA: 0x00140B96 File Offset: 0x0013ED96
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
		if (num <= 885120788U)
		{
			if (num <= 497808528U)
			{
				if (num != 457199889U)
				{
					if (num != 490755127U)
					{
						if (num != 497808528U)
						{
							goto IL_460;
						}
						if (!(cardId == "CFM_602"))
						{
							goto IL_460;
						}
					}
					else if (!(cardId == "CFM_715"))
					{
						goto IL_460;
					}
				}
				else if (!(cardId == "CFM_713"))
				{
					goto IL_460;
				}
			}
			else if (num <= 530525123U)
			{
				if (num != 524310365U)
				{
					if (num != 530525123U)
					{
						goto IL_460;
					}
					if (!(cardId == "CFM_672"))
					{
						goto IL_460;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01, 2.5f);
					goto IL_460;
				}
				else if (!(cardId == "CFM_717"))
				{
					goto IL_460;
				}
			}
			else if (num != 884973693U)
			{
				if (num != 885120788U)
				{
					goto IL_460;
				}
				if (!(cardId == "CFM_691"))
				{
					goto IL_460;
				}
			}
			else
			{
				if (!(cardId == "CFM_685"))
				{
					goto IL_460;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01, 2.5f);
				goto IL_460;
			}
		}
		else if (num <= 1435225994U)
		{
			if (num != 901898407U)
			{
				if (num != 1048899472U)
				{
					if (num != 1435225994U)
					{
						goto IL_460;
					}
					if (!(cardId == "CFM_312"))
					{
						goto IL_460;
					}
				}
				else if (!(cardId == "CFM_343"))
				{
					goto IL_460;
				}
			}
			else if (!(cardId == "CFM_690"))
			{
				goto IL_460;
			}
		}
		else if (num <= 3030831639U)
		{
			if (num != 2671992692U)
			{
				if (num != 3030831639U)
				{
					goto IL_460;
				}
				if (!(cardId == "CFM_621"))
				{
					goto IL_460;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01, 2.5f);
				goto IL_460;
			}
			else if (!(cardId == "CFM_707"))
			{
				goto IL_460;
			}
		}
		else if (num != 3158094027U)
		{
			if (num != 3578133813U)
			{
				goto IL_460;
			}
			if (!(cardId == "CFM_902"))
			{
				goto IL_460;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01, 2.5f);
			goto IL_460;
		}
		else
		{
			if (!(cardId == "DALA_BOSS_59t3"))
			{
				goto IL_460;
			}
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_59h.m_PlayerExtortion);
			goto IL_460;
		}
		yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_59h.m_PlayerJade);
		IL_460:
		yield break;
	}

	// Token: 0x06003D31 RID: 15665 RVA: 0x00140BAC File Offset: 0x0013EDAC
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
		if (!(cardId == "DALA_BOSS_59t"))
		{
			if (cardId == "DALA_BOSS_59t2")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_59h.m_BossLotusBruiser);
		}
		yield break;
	}

	// Token: 0x040027B9 RID: 10169
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01.prefab:ec31e5279366354429d30b5c681db473");

	// Token: 0x040027BA RID: 10170
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01.prefab:3710a50596b794944abef0f421b58912");

	// Token: 0x040027BB RID: 10171
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02.prefab:35dfcc9737ca6ee43bcd40626ac217d2");

	// Token: 0x040027BC RID: 10172
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Death_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Death_02.prefab:1badf88f612b34a45a2abc64838432f7");

	// Token: 0x040027BD RID: 10173
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_DefeatPlayer_01.prefab:a9e9b08790d28624e9bed2efb5e27462");

	// Token: 0x040027BE RID: 10174
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01.prefab:b78bd5ebf93396e4ab2e77bd02a4ac7b");

	// Token: 0x040027BF RID: 10175
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01.prefab:4ece487155b662d44a25041789151e42");

	// Token: 0x040027C0 RID: 10176
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02.prefab:7b09b2dd30821d241a7604798b3f438b");

	// Token: 0x040027C1 RID: 10177
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03.prefab:97cf1bf704850f84fa7456bda3345f0b");

	// Token: 0x040027C2 RID: 10178
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04.prefab:503b1106a44c6374faffaa6cd08ebd31");

	// Token: 0x040027C3 RID: 10179
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05.prefab:b9d8419a56e1df04487df03564ad4bc3");

	// Token: 0x040027C4 RID: 10180
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06.prefab:e4224acf79137b340893925abf78d893");

	// Token: 0x040027C5 RID: 10181
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01.prefab:fcda977d73ea6204e90660e8dabee4ff");

	// Token: 0x040027C6 RID: 10182
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02.prefab:54e068b6bb83dd5418756f2cd83a3a66");

	// Token: 0x040027C7 RID: 10183
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Idle_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Idle_01.prefab:20ccde4f26275d547b4f3e775f3675a0");

	// Token: 0x040027C8 RID: 10184
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Idle_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Idle_02.prefab:056214cd3bd86f549839a6b8bb381afb");

	// Token: 0x040027C9 RID: 10185
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Idle_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Idle_03.prefab:f329d6771e55b6e45bb4da1fecedb945");

	// Token: 0x040027CA RID: 10186
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Intro_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Intro_01.prefab:90150f5850f1c70458b60a3a5a692a69");

	// Token: 0x040027CB RID: 10187
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_IntroChu_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_IntroChu_01.prefab:07aa21ca3d677234f908e2fc35c1f444");

	// Token: 0x040027CC RID: 10188
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01.prefab:0e6678e82cad4344e86dc5c2a2d197ea");

	// Token: 0x040027CD RID: 10189
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Misc_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Misc_01.prefab:fe337316aa4d8c44d96da196f0377c56");

	// Token: 0x040027CE RID: 10190
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Misc_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Misc_02.prefab:9a101189f342fe645aa8f453ecde1667");

	// Token: 0x040027CF RID: 10191
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01.prefab:3067d9d4b64e6ea419f26b1ceb393ccc");

	// Token: 0x040027D0 RID: 10192
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01.prefab:b1e4825c1303b8341b155ae81bc2bfc8");

	// Token: 0x040027D1 RID: 10193
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01.prefab:b854c9b3b5df2e342b132330b584956b");

	// Token: 0x040027D2 RID: 10194
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02.prefab:d7454efdb0e8c404da24f60c6ffd8617");

	// Token: 0x040027D3 RID: 10195
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03.prefab:cdf03091c740d6f4cbdaccef7f0af6a0");

	// Token: 0x040027D4 RID: 10196
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04.prefab:9ccafb707f361dd40baa56b74d0ba985");

	// Token: 0x040027D5 RID: 10197
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05.prefab:36156b8904570c94db7e26748da6a0f1");

	// Token: 0x040027D6 RID: 10198
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06.prefab:c38ec7773586e8b4490f58c9f5cec55a");

	// Token: 0x040027D7 RID: 10199
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01.prefab:9b851b64fecc6294ba29d137613e5b13");

	// Token: 0x040027D8 RID: 10200
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02.prefab:71d078a963d36ab4eb67be2e52b8fd32");

	// Token: 0x040027D9 RID: 10201
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03.prefab:298011133d7a1a542829d08ec7f1c10e");

	// Token: 0x040027DA RID: 10202
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01.prefab:9e4e3076b4448e147b6800096e892f2e");

	// Token: 0x040027DB RID: 10203
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01.prefab:e5b91d6a14110d148a72dd9aadaa82ab");

	// Token: 0x040027DC RID: 10204
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01.prefab:4bfc8fa5b7288bb4c8e932aa21e1d223");

	// Token: 0x040027DD RID: 10205
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Idle_01,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Idle_02,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_Idle_03
	};

	// Token: 0x040027DE RID: 10206
	private static List<string> m_BossLotusBruiser = new List<string>
	{
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02
	};

	// Token: 0x040027DF RID: 10207
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06
	};

	// Token: 0x040027E0 RID: 10208
	private static List<string> m_HeroPowerTreasure = new List<string>
	{
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02
	};

	// Token: 0x040027E1 RID: 10209
	private static List<string> m_PlayerExtortion = new List<string>
	{
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06
	};

	// Token: 0x040027E2 RID: 10210
	private static List<string> m_PlayerJade = new List<string>
	{
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02,
		DALA_Dungeon_Boss_59h.VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03
	};

	// Token: 0x040027E3 RID: 10211
	private HashSet<string> m_playedLines = new HashSet<string>();
}
