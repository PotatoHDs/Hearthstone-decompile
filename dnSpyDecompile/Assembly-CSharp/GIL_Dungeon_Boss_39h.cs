using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F0 RID: 1008
public class GIL_Dungeon_Boss_39h : GIL_Dungeon
{
	// Token: 0x0600381E RID: 14366 RVA: 0x0011B85C File Offset: 0x00119A5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_41h_Female_HumanGhost_Intro_01.prefab:2d9a2c1744722b04487c9983f830b25f",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EmoteResponse_01.prefab:2f02f1ad1940efc4498a56fdf9425bc1",
			"VO_GILA_BOSS_41h_Female_HumanGhost_Death_01.prefab:0826bc5f228a6724789cd46200d05a94",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_01.prefab:555e3e45f1d745a4f8c4a2cd6852c9e9",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_02.prefab:d17efc332ca26e84698da64d005bc42c",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_03.prefab:fbfd8102e3349e5409b55cb0bc3a9f32",
			"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_01.prefab:1c885e702a6bd904fa0a2710922f33fd",
			"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_02.prefab:49c1b6e5ba4085e46a44babc098c51b3",
			"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_03.prefab:8fcebf4a48445414598e6d1d04e76526",
			"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_04.prefab:0c39048a12578ef428fe10828178fe18",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_01.prefab:18453d7e2b37f714a805f082bcddf01d",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_02.prefab:9299aad73816811429f4da6082e9fdda",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigueDeath_01.prefab:0551a75f20704894ba0340fafe3eefc2",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventPlayRin_01.prefab:ad7bd3274cab68846ae61271015ebc01"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600381F RID: 14367 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003820 RID: 14368 RVA: 0x0011B950 File Offset: 0x00119B50
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Female_HumanGhost_Intro_01.prefab:2d9a2c1744722b04487c9983f830b25f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Female_HumanGhost_EmoteResponse_01.prefab:2f02f1ad1940efc4498a56fdf9425bc1", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003821 RID: 14369 RVA: 0x0011B9D7 File Offset: 0x00119BD7
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_41h_Female_HumanGhost_Death_01.prefab:0826bc5f228a6724789cd46200d05a94";
	}

	// Token: 0x06003822 RID: 14370 RVA: 0x0011B9DE File Offset: 0x00119BDE
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003823 RID: 14371 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003824 RID: 14372 RVA: 0x0011B9F4 File Offset: 0x00119BF4
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 918130314U)
		{
			if (num <= 257000035U)
			{
				if (num <= 130839647U)
				{
					if (num != 14452657U)
					{
						if (num != 130839647U)
						{
							goto IL_3EA;
						}
						if (!(cardId == "GILA_BOSS_60t"))
						{
							goto IL_3EA;
						}
					}
					else if (!(cardId == "GILA_816a"))
					{
						goto IL_3EA;
					}
				}
				else if (num != 149608056U)
				{
					if (num != 257000035U)
					{
						goto IL_3EA;
					}
					if (!(cardId == "LOE_104"))
					{
						goto IL_3EA;
					}
				}
				else if (!(cardId == "LOOT_026"))
				{
					goto IL_3EA;
				}
			}
			else if (num <= 632763696U)
			{
				if (num != 463267552U)
				{
					if (num != 632763696U)
					{
						goto IL_3EA;
					}
					if (!(cardId == "LOE_079"))
					{
						goto IL_3EA;
					}
				}
				else if (!(cardId == "CFM_660"))
				{
					goto IL_3EA;
				}
			}
			else if (num != 801576798U)
			{
				if (num != 864031283U)
				{
					if (num != 918130314U)
					{
						goto IL_3EA;
					}
					if (!(cardId == "LOE_002"))
					{
						goto IL_3EA;
					}
				}
				else if (!(cardId == "GILA_821a"))
				{
					goto IL_3EA;
				}
			}
			else if (!(cardId == "BRM_007"))
			{
				goto IL_3EA;
			}
		}
		else if (num <= 2714263018U)
		{
			if (num <= 1736281365U)
			{
				if (num != 1060376590U)
				{
					if (num != 1736281365U)
					{
						goto IL_3EA;
					}
					if (!(cardId == "LOOT_106"))
					{
						goto IL_3EA;
					}
				}
				else
				{
					if (!(cardId == "LOOT_415"))
					{
						goto IL_3EA;
					}
					yield return base.PlayBossLine(actor, "VO_GILA_BOSS_41h_Female_HumanGhost_EventPlayRin_01.prefab:ad7bd3274cab68846ae61271015ebc01", 2.5f);
					goto IL_3EA;
				}
			}
			else if (num != 1869426141U)
			{
				if (num != 2714263018U)
				{
					goto IL_3EA;
				}
				if (!(cardId == "UNG_851"))
				{
					goto IL_3EA;
				}
			}
			else if (!(cardId == "GILA_817"))
			{
				goto IL_3EA;
			}
		}
		else if (num <= 2813499638U)
		{
			if (num != 2774225561U)
			{
				if (num != 2813499638U)
				{
					goto IL_3EA;
				}
				if (!(cardId == "CFM_602b"))
				{
					goto IL_3EA;
				}
			}
			else if (!(cardId == "GILA_852a"))
			{
				goto IL_3EA;
			}
		}
		else if (num != 2950393311U)
		{
			if (num != 3143963246U)
			{
				if (num != 3361925198U)
				{
					goto IL_3EA;
				}
				if (!(cardId == "GIL_815"))
				{
					goto IL_3EA;
				}
			}
			else if (!(cardId == "GIL_828"))
			{
				goto IL_3EA;
			}
		}
		else if (!(cardId == "ICC_091"))
		{
			goto IL_3EA;
		}
		string text = base.PopRandomLineWithChance(this.m_RandomShuffleLines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		IL_3EA:
		yield break;
	}

	// Token: 0x06003825 RID: 14373 RVA: 0x0011BA0A File Offset: 0x00119C0A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 101:
		{
			string text = base.PopRandomLineWithChance(this.m_RandomDrawLines);
			if (text != null)
			{
				yield return base.PlayBossLine(actor, text, 2.5f);
			}
			break;
		}
		case 102:
		{
			string text = base.PopRandomLineWithChance(this.m_RandomFatigueLines);
			if (text != null)
			{
				yield return base.PlayBossLine(actor, text, 2.5f);
			}
			break;
		}
		case 103:
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigueDeath_01.prefab:0551a75f20704894ba0340fafe3eefc2", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04001DA6 RID: 7590
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DA7 RID: 7591
	private List<string> m_RandomShuffleLines = new List<string>
	{
		"VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_01.prefab:555e3e45f1d745a4f8c4a2cd6852c9e9",
		"VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_02.prefab:d17efc332ca26e84698da64d005bc42c",
		"VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_03.prefab:fbfd8102e3349e5409b55cb0bc3a9f32"
	};

	// Token: 0x04001DA8 RID: 7592
	private List<string> m_RandomDrawLines = new List<string>
	{
		"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_01.prefab:1c885e702a6bd904fa0a2710922f33fd",
		"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_02.prefab:49c1b6e5ba4085e46a44babc098c51b3",
		"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_03.prefab:8fcebf4a48445414598e6d1d04e76526",
		"VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_04.prefab:0c39048a12578ef428fe10828178fe18"
	};

	// Token: 0x04001DA9 RID: 7593
	private List<string> m_RandomFatigueLines = new List<string>
	{
		"VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_01.prefab:18453d7e2b37f714a805f082bcddf01d",
		"VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_02.prefab:9299aad73816811429f4da6082e9fdda"
	};
}
