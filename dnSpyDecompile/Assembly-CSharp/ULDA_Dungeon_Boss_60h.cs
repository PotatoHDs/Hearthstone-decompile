using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004B9 RID: 1209
public class ULDA_Dungeon_Boss_60h : ULDA_Dungeon
{
	// Token: 0x060040E2 RID: 16610 RVA: 0x0015A498 File Offset: 0x00158698
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Death_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_DefeatPlayer_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01,
			ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040E3 RID: 16611 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040E4 RID: 16612 RVA: 0x0015A67C File Offset: 0x0015887C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x060040E5 RID: 16613 RVA: 0x0015A684 File Offset: 0x00158884
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x060040E6 RID: 16614 RVA: 0x0015A68C File Offset: 0x0015888C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01;
	}

	// Token: 0x060040E7 RID: 16615 RVA: 0x0015A6C4 File Offset: 0x001588C4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x060040E8 RID: 16616 RVA: 0x0015A79E File Offset: 0x0015899E
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
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060040E9 RID: 16617 RVA: 0x0015A7B4 File Offset: 0x001589B4
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "ULD_143"))
		{
			if (!(cardId == "ULD_290"))
			{
				if (!(cardId == "OG_280"))
				{
					if (!(cardId == "NEW1_021"))
					{
						if (!(cardId == "CS2_234"))
						{
							if (cardId == "ULD_216")
							{
								yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerTriggerCThun);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060040EA RID: 16618 RVA: 0x0015A7CA File Offset: 0x001589CA
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1728820087U)
		{
			if (num <= 854393490U)
			{
				if (num != 460174924U)
				{
					if (num != 854393490U)
					{
						goto IL_2F1;
					}
					if (!(cardId == "OG_024"))
					{
						goto IL_2F1;
					}
				}
				else if (!(cardId == "OG_174"))
				{
					goto IL_2F1;
				}
			}
			else if (num != 1390272473U)
			{
				if (num != 1728820087U)
				{
					goto IL_2F1;
				}
				if (!(cardId == "ULD_189"))
				{
					goto IL_2F1;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01, 2.5f);
				goto IL_2F1;
			}
			else if (!(cardId == "EX1_564"))
			{
				goto IL_2F1;
			}
		}
		else if (num <= 2601936683U)
		{
			if (num != 2416102946U)
			{
				if (num != 2601936683U)
				{
					goto IL_2F1;
				}
				if (!(cardId == "OG_207"))
				{
					goto IL_2F1;
				}
			}
			else
			{
				if (!(cardId == "OG_280"))
				{
					goto IL_2F1;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01, 2.5f);
				goto IL_2F1;
			}
		}
		else if (num != 2826260488U)
		{
			if (num != 3514835915U)
			{
				if (num != 3835961796U)
				{
					goto IL_2F1;
				}
				if (!(cardId == "DAL_744"))
				{
					goto IL_2F1;
				}
			}
			else if (!(cardId == "DAL_613"))
			{
				goto IL_2F1;
			}
		}
		else if (!(cardId == "OG_141"))
		{
			goto IL_2F1;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01, 2.5f);
		IL_2F1:
		yield break;
	}

	// Token: 0x04002F29 RID: 12073
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerCthun_01.prefab:832d52d6c27ef3a41ae7c11ed17e4466");

	// Token: 0x04002F2A RID: 12074
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerFaceless_01.prefab:b0180209467fc604f8f71c331f28579a");

	// Token: 0x04002F2B RID: 12075
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_BossTriggerLineLieutenant_01.prefab:28ca15992f5c5b84f9c80726ccc37e74");

	// Token: 0x04002F2C RID: 12076
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Death_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Death_01.prefab:8b5e93c5f15d03044b5477876329df5b");

	// Token: 0x04002F2D RID: 12077
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_DefeatPlayer_01.prefab:fdbb390966fd8264d906e22a14828473");

	// Token: 0x04002F2E RID: 12078
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_EmoteResponse_01.prefab:218d3d4ccfb266644ac630880e738ccd");

	// Token: 0x04002F2F RID: 12079
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02.prefab:440880063ebdac3449f1df8df66b8871");

	// Token: 0x04002F30 RID: 12080
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03.prefab:c0e0700250a91d249870f2ce5091b303");

	// Token: 0x04002F31 RID: 12081
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04.prefab:db96b1d8e1f1ffe4791fa75534b29d66");

	// Token: 0x04002F32 RID: 12082
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05.prefab:006b499fcea589947b6848dc60fefedf");

	// Token: 0x04002F33 RID: 12083
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01.prefab:943ddd8352ce58246a5b8ce18154e56d");

	// Token: 0x04002F34 RID: 12084
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02.prefab:2a3765d56abef1143b32b60e68ad6513");

	// Token: 0x04002F35 RID: 12085
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03.prefab:6a0576ec316d5954998a27ce1707997f");

	// Token: 0x04002F36 RID: 12086
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_Intro_01.prefab:22e2fef3828561e46b83e176bacd2147");

	// Token: 0x04002F37 RID: 12087
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_IntroResponseReno_01.prefab:275ae9f9c7ac3a845ac36ade2fd10446");

	// Token: 0x04002F38 RID: 12088
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01.prefab:9696080ec36f628499cac7d24e1c5a66");

	// Token: 0x04002F39 RID: 12089
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Doomsayer_01.prefab:b6c2a25e18f840e439234c300e9a15c6");

	// Token: 0x04002F3A RID: 12090
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_History_Buff_01.prefab:3ddffbfd01e602c4d8325d8da126d37b");

	// Token: 0x04002F3B RID: 12091
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Pharaohs_Blessing_01.prefab:0bf3da4fccdb2af4084a8621a7141878");

	// Token: 0x04002F3C RID: 12092
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Puzzle_Box_of_Yogg_Saron_01.prefab:f5801d2dc607d5149b12c95ef0d7c2da");

	// Token: 0x04002F3D RID: 12093
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_ShadowWordPain_01.prefab:852ff26535557cc4986fe220f77b13d7");

	// Token: 0x04002F3E RID: 12094
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01.prefab:ae6ea80bb8f3a094fa76cc5896beb20d");

	// Token: 0x04002F3F RID: 12095
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThunDefeatBoss_01.prefab:47f5a78f2981a7d43b3bddb129d23d1f");

	// Token: 0x04002F40 RID: 12096
	private static readonly AssetReference VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01 = new AssetReference("VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerFatigue_01.prefab:37b4ca4b53526784994ef4c6f6170e0d");

	// Token: 0x04002F41 RID: 12097
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_02,
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_03,
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_04,
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_HeroPower_05
	};

	// Token: 0x04002F42 RID: 12098
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Idle_01,
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Idle_02,
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_Idle_03
	};

	// Token: 0x04002F43 RID: 12099
	private List<string> m_PlayerTriggerCThun = new List<string>
	{
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTrigger_Cthun_01,
		ULDA_Dungeon_Boss_60h.VO_ULDA_BOSS_60h_Male_Ethereal_PlayerTriggerCThun_01
	};

	// Token: 0x04002F44 RID: 12100
	private HashSet<string> m_playedLines = new HashSet<string>();
}
