using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200042F RID: 1071
public class DALA_Dungeon_Boss_02h : DALA_Dungeon
{
	// Token: 0x06003A60 RID: 14944 RVA: 0x0012D3A4 File Offset: 0x0012B5A4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Death_02,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_DefeatPlayer_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPower_04,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPower_05,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Idle_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Idle_02,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Idle_03,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Intro_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_IntroSqueamlish_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01,
			DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A61 RID: 14945 RVA: 0x0012D538 File Offset: 0x0012B738
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_02h.m_IdleLines;
	}

	// Token: 0x06003A62 RID: 14946 RVA: 0x0012D53F File Offset: 0x0012B73F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01;
	}

	// Token: 0x06003A63 RID: 14947 RVA: 0x0012D578 File Offset: 0x0012B778
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish")
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

	// Token: 0x06003A64 RID: 14948 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003A65 RID: 14949 RVA: 0x0012D65F File Offset: 0x0012B85F
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
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerFullLines);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		yield break;
	}

	// Token: 0x06003A66 RID: 14950 RVA: 0x0012D675 File Offset: 0x0012B875
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
		if (num <= 2006491291U)
		{
			if (num <= 1414403330U)
			{
				if (num <= 497286296U)
				{
					if (num != 32398641U)
					{
						if (num != 497286296U)
						{
							goto IL_4B9;
						}
						if (!(cardId == "ICC_850"))
						{
							goto IL_4B9;
						}
						goto IL_454;
					}
					else
					{
						if (!(cardId == "UNG_061"))
						{
							goto IL_4B9;
						}
						goto IL_454;
					}
				}
				else if (num != 646074946U)
				{
					if (num != 1414403330U)
					{
						goto IL_4B9;
					}
					if (!(cardId == "DALA_725"))
					{
						goto IL_4B9;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01, 2.5f);
					goto IL_4B9;
				}
				else
				{
					if (!(cardId == "AT_111"))
					{
						goto IL_4B9;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01, 2.5f);
					goto IL_4B9;
				}
			}
			else if (num <= 1954696374U)
			{
				if (num != 1900257086U)
				{
					if (num != 1954696374U)
					{
						goto IL_4B9;
					}
					if (!(cardId == "TRL_509t"))
					{
						goto IL_4B9;
					}
				}
				else
				{
					if (!(cardId == "EX1_411"))
					{
						goto IL_4B9;
					}
					goto IL_421;
				}
			}
			else if (num != 1980625588U)
			{
				if (num != 2006491291U)
				{
					goto IL_4B9;
				}
				if (!(cardId == "TRL_304"))
				{
					goto IL_4B9;
				}
				goto IL_421;
			}
			else
			{
				if (!(cardId == "CS2_080"))
				{
					goto IL_4B9;
				}
				goto IL_454;
			}
		}
		else if (num <= 2684087453U)
		{
			if (num <= 2169873006U)
			{
				if (num != 2143680583U)
				{
					if (num != 2169873006U)
					{
						goto IL_4B9;
					}
					if (!(cardId == "GIL_653"))
					{
						goto IL_4B9;
					}
					goto IL_421;
				}
				else
				{
					if (!(cardId == "EX1_247"))
					{
						goto IL_4B9;
					}
					goto IL_421;
				}
			}
			else if (num != 2535236125U)
			{
				if (num != 2684087453U)
				{
					goto IL_4B9;
				}
				if (!(cardId == "CS2_083b"))
				{
					goto IL_4B9;
				}
				goto IL_454;
			}
			else
			{
				if (!(cardId == "EX1_133"))
				{
					goto IL_4B9;
				}
				goto IL_454;
			}
		}
		else if (num <= 3511113343U)
		{
			if (num != 2797504017U)
			{
				if (num != 3511113343U)
				{
					goto IL_4B9;
				}
				if (!(cardId == "EX1_014t"))
				{
					goto IL_4B9;
				}
			}
			else
			{
				if (!(cardId == "LOOT_542"))
				{
					goto IL_4B9;
				}
				goto IL_454;
			}
		}
		else if (num != 3824548033U)
		{
			if (num != 3890777505U)
			{
				if (num != 4195201317U)
				{
					goto IL_4B9;
				}
				if (!(cardId == "BOT_286"))
				{
					goto IL_4B9;
				}
				goto IL_454;
			}
			else
			{
				if (!(cardId == "TRL_074"))
				{
					goto IL_4B9;
				}
				goto IL_454;
			}
		}
		else
		{
			if (!(cardId == "CS2_106"))
			{
				goto IL_4B9;
			}
			goto IL_421;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01, 2.5f);
		goto IL_4B9;
		IL_421:
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01, 2.5f);
		goto IL_4B9;
		IL_454:
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01, 2.5f);
		IL_4B9:
		yield break;
	}

	// Token: 0x06003A67 RID: 14951 RVA: 0x0012D68B File Offset: 0x0012B88B
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

	// Token: 0x04002206 RID: 8710
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Death_02 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Death_02.prefab:1b50b7f37adbbb947939bec415a2bd92");

	// Token: 0x04002207 RID: 8711
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_DefeatPlayer_01.prefab:3e3f9e952283cde4593ed7caaa001e44");

	// Token: 0x04002208 RID: 8712
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01.prefab:dd24979e7bcc4364da7e20844d88046c");

	// Token: 0x04002209 RID: 8713
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPower_04 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPower_04.prefab:45f67c748641b6647befbed904d8d44d");

	// Token: 0x0400220A RID: 8714
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPower_05 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPower_05.prefab:087fd15142f909844a74f5d49cff515b");

	// Token: 0x0400220B RID: 8715
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02.prefab:6102ced58599bde41bb5496e62b5770b");

	// Token: 0x0400220C RID: 8716
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04.prefab:bcdbe31471b89b743b89898822468a45");

	// Token: 0x0400220D RID: 8717
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05.prefab:164f85a40f4fb0644a76c31333e1225e");

	// Token: 0x0400220E RID: 8718
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Idle_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Idle_01.prefab:8ed2a948ac75a2b44a7b98d748065829");

	// Token: 0x0400220F RID: 8719
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Idle_02 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Idle_02.prefab:2af709dc4aa48364f92c22bfe09500aa");

	// Token: 0x04002210 RID: 8720
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Idle_03 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Idle_03.prefab:323104a83847e9b49802bf634e3d1bc5");

	// Token: 0x04002211 RID: 8721
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Intro_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Intro_01.prefab:295b44c099df1d04ab737350cd56781c");

	// Token: 0x04002212 RID: 8722
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01.prefab:4700ab3bd72c59b499d0c99a1754b981");

	// Token: 0x04002213 RID: 8723
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_IntroSqueamlish_01.prefab:85be90b1756f4ed479eb8cd6fc392df0");

	// Token: 0x04002214 RID: 8724
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01.prefab:047a9199ec799d34ea7d2c8368738125");

	// Token: 0x04002215 RID: 8725
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01.prefab:26f331e2886384942b26c24c4ec1dbbf");

	// Token: 0x04002216 RID: 8726
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01.prefab:0804876260c58cd49bc6c7f06312d007");

	// Token: 0x04002217 RID: 8727
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01.prefab:847cb52f1546b7848b947d0692765b0b");

	// Token: 0x04002218 RID: 8728
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01.prefab:723c5f77b0f8b6d45b9b527463b7203c");

	// Token: 0x04002219 RID: 8729
	private List<string> m_HeroPowerLines = new List<string>
	{
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPower_04,
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPower_05
	};

	// Token: 0x0400221A RID: 8730
	private List<string> m_HeroPowerFullLines = new List<string>
	{
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02,
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04,
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05
	};

	// Token: 0x0400221B RID: 8731
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Idle_01,
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Idle_02,
		DALA_Dungeon_Boss_02h.VO_DALA_BOSS_02h_Male_Troll_Idle_03
	};

	// Token: 0x0400221C RID: 8732
	private HashSet<string> m_playedLines = new HashSet<string>();
}
