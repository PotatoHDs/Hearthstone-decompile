using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003EF RID: 1007
public class GIL_Dungeon_Boss_38h : GIL_Dungeon
{
	// Token: 0x06003815 RID: 14357 RVA: 0x0011B57C File Offset: 0x0011977C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_38h_Male_Goblin_Intro_01.prefab:952991d4799b8014086abf0a8eb3a4ba",
			"VO_GILA_BOSS_38h_Male_Goblin_EmoteResponse_01.prefab:31f5ac41aa6b846419f4080611dfaf79",
			"VO_GILA_BOSS_38h_Male_Goblin_Death_01.prefab:cc33cd36cbe954e40b849f9e5123a58d",
			"VO_GILA_BOSS_38h_Male_Goblin_DefeatPlayer_01.prefab:c16741a44637661469202e067ca63c6b",
			"VO_GILA_BOSS_38h_Male_Goblin_HeroPower_01.prefab:037e6d75fb60c3c4bb81645ced779df7",
			"VO_GILA_BOSS_38h_Male_Goblin_HeroPower_02.prefab:d2d33eeb557d01649b17fc85b6c25811",
			"VO_GILA_BOSS_38h_Male_Goblin_HeroPower_03.prefab:65762f713cbb9a243b3ab042da2b4176",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayNestingRoc_01.prefab:1e538ce6838c7cb44bb42fbe7b5e4f94",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayNestingRoc_02.prefab:08ff0ef9bfafb3c4eafcc03bb4b2a2dd",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_01.prefab:87fb7cb30ce446c42b2f94c7550f507f",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_02.prefab:9e41bc5cc0f12774bbb04ad8766d91a1",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_03.prefab:8cb03c80c4dc16f40a1bd685465fcfbb",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_04.prefab:83eb53ae594d4814d9ee125c175f7a24",
			"VO_GILA_BOSS_38h_Male_Goblin_EventBirdDead_01.prefab:77653c50d328f8f4fb86dca2670da780",
			"VO_GILA_BOSS_38h_Male_Goblin_EventBirdDead_02.prefab:2f0df9f435f01ca4d860834b69b1fc18",
			"VO_GILA_BOSS_38h_Male_Goblin_EventBirdDead_03.prefab:cb381c7fe1966e84fa08f512e92d9122",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayerPlayBird_01.prefab:c4ee445c7f3a05543b3d0a52a153fc4c",
			"VO_GILA_BOSS_38h_Male_Goblin_EventPlayerPlayBird_02.prefab:b74522c99d50c7743a2018b4339a4e43"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003816 RID: 14358 RVA: 0x0011B69C File Offset: 0x0011989C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string cardId = entity.GetCardId();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
			string text;
			if (num <= 2212797563U)
			{
				if (num <= 1191726123U)
				{
					if (num != 96218979U)
					{
						if (num != 835677842U)
						{
							if (num != 1191726123U)
							{
								goto IL_355;
							}
							if (!(cardId == "KAR_300"))
							{
								goto IL_355;
							}
						}
						else if (!(cardId == "ICC_023"))
						{
							goto IL_355;
						}
					}
					else
					{
						if (!(cardId == "UNG_801"))
						{
							goto IL_355;
						}
						text = base.PopRandomLineWithChance(this.m_NestingRoc);
						if (text != null)
						{
							yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
							goto IL_355;
						}
						goto IL_355;
					}
				}
				else if (num != 1449416540U)
				{
					if (num != 1736428460U)
					{
						if (num != 2212797563U)
						{
							goto IL_355;
						}
						if (!(cardId == "UNG_027"))
						{
							goto IL_355;
						}
					}
					else if (!(cardId == "LOOT_170"))
					{
						goto IL_355;
					}
				}
				else if (!(cardId == "NEW1_016"))
				{
					goto IL_355;
				}
			}
			else if (num <= 3208380636U)
			{
				if (num != 2693582287U)
				{
					if (num != 3118109640U)
					{
						if (num != 3208380636U)
						{
							goto IL_355;
						}
						if (!(cardId == "CS2_237"))
						{
							goto IL_355;
						}
					}
					else if (!(cardId == "KAR_037"))
					{
						goto IL_355;
					}
				}
				else if (!(cardId == "EX1_009"))
				{
					goto IL_355;
				}
			}
			else if (num <= 3639008486U)
			{
				if (num != 3275638207U)
				{
					if (num != 3639008486U)
					{
						goto IL_355;
					}
					if (!(cardId == "CS2_169"))
					{
						goto IL_355;
					}
				}
				else if (!(cardId == "CS2_203"))
				{
					goto IL_355;
				}
			}
			else if (num != 4065199500U)
			{
				if (num != 4237145036U)
				{
					goto IL_355;
				}
				if (!(cardId == "UNG_912"))
				{
					goto IL_355;
				}
			}
			else if (!(cardId == "GIL_664"))
			{
				goto IL_355;
			}
			text = base.PopRandomLineWithChance(this.m_BossBirds);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		IL_355:
		yield break;
	}

	// Token: 0x06003817 RID: 14359 RVA: 0x0011B6B2 File Offset: 0x001198B2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_38h_Male_Goblin_HeroPower_01.prefab:037e6d75fb60c3c4bb81645ced779df7",
			"VO_GILA_BOSS_38h_Male_Goblin_HeroPower_02.prefab:d2d33eeb557d01649b17fc85b6c25811",
			"VO_GILA_BOSS_38h_Male_Goblin_HeroPower_03.prefab:65762f713cbb9a243b3ab042da2b4176"
		};
	}

	// Token: 0x06003818 RID: 14360 RVA: 0x0011B6DA File Offset: 0x001198DA
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_38h_Male_Goblin_Death_01.prefab:cc33cd36cbe954e40b849f9e5123a58d";
	}

	// Token: 0x06003819 RID: 14361 RVA: 0x0011B6E4 File Offset: 0x001198E4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_38h_Male_Goblin_Intro_01.prefab:952991d4799b8014086abf0a8eb3a4ba", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_38h_Male_Goblin_EmoteResponse_01.prefab:31f5ac41aa6b846419f4080611dfaf79", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600381A RID: 14362 RVA: 0x0011B76B File Offset: 0x0011996B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 101)
		{
			string text = base.PopRandomLineWithChance(this.m_DeadBirds);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x0600381B RID: 14363 RVA: 0x0011B781 File Offset: 0x00119981
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
		if (num <= 2693582287U)
		{
			if (num <= 1449416540U)
			{
				if (num != 835677842U)
				{
					if (num != 1191726123U)
					{
						if (num != 1449416540U)
						{
							goto IL_2BE;
						}
						if (!(cardId == "NEW1_016"))
						{
							goto IL_2BE;
						}
					}
					else if (!(cardId == "KAR_300"))
					{
						goto IL_2BE;
					}
				}
				else if (!(cardId == "ICC_023"))
				{
					goto IL_2BE;
				}
			}
			else if (num != 1736428460U)
			{
				if (num != 2212797563U)
				{
					if (num != 2693582287U)
					{
						goto IL_2BE;
					}
					if (!(cardId == "EX1_009"))
					{
						goto IL_2BE;
					}
				}
				else if (!(cardId == "UNG_027"))
				{
					goto IL_2BE;
				}
			}
			else if (!(cardId == "LOOT_170"))
			{
				goto IL_2BE;
			}
		}
		else if (num <= 3275638207U)
		{
			if (num != 3118109640U)
			{
				if (num != 3208380636U)
				{
					if (num != 3275638207U)
					{
						goto IL_2BE;
					}
					if (!(cardId == "CS2_203"))
					{
						goto IL_2BE;
					}
				}
				else if (!(cardId == "CS2_237"))
				{
					goto IL_2BE;
				}
			}
			else if (!(cardId == "KAR_037"))
			{
				goto IL_2BE;
			}
		}
		else if (num != 3639008486U)
		{
			if (num != 4065199500U)
			{
				if (num != 4237145036U)
				{
					goto IL_2BE;
				}
				if (!(cardId == "UNG_912"))
				{
					goto IL_2BE;
				}
			}
			else if (!(cardId == "GIL_664"))
			{
				goto IL_2BE;
			}
		}
		else if (!(cardId == "CS2_169"))
		{
			goto IL_2BE;
		}
		if (this.m_PlayerBirds.Count != 0)
		{
			string text = base.PopRandomLineWithChance(this.m_PlayerBirds);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		IL_2BE:
		yield break;
	}

	// Token: 0x04001DA1 RID: 7585
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DA2 RID: 7586
	private List<string> m_PlayerBirds = new List<string>
	{
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayerPlayBird_01.prefab:c4ee445c7f3a05543b3d0a52a153fc4c",
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayerPlayBird_02.prefab:b74522c99d50c7743a2018b4339a4e43"
	};

	// Token: 0x04001DA3 RID: 7587
	private List<string> m_BossBirds = new List<string>
	{
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_01.prefab:87fb7cb30ce446c42b2f94c7550f507f",
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_02.prefab:9e41bc5cc0f12774bbb04ad8766d91a1",
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_03.prefab:8cb03c80c4dc16f40a1bd685465fcfbb",
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayBird_04.prefab:83eb53ae594d4814d9ee125c175f7a24"
	};

	// Token: 0x04001DA4 RID: 7588
	private List<string> m_NestingRoc = new List<string>
	{
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayNestingRoc_01.prefab:1e538ce6838c7cb44bb42fbe7b5e4f94",
		"VO_GILA_BOSS_38h_Male_Goblin_EventPlayNestingRoc_02.prefab:08ff0ef9bfafb3c4eafcc03bb4b2a2dd"
	};

	// Token: 0x04001DA5 RID: 7589
	private List<string> m_DeadBirds = new List<string>
	{
		"VO_GILA_BOSS_38h_Male_Goblin_EventBirdDead_01.prefab:77653c50d328f8f4fb86dca2670da780",
		"VO_GILA_BOSS_38h_Male_Goblin_EventBirdDead_02.prefab:2f0df9f435f01ca4d860834b69b1fc18",
		"VO_GILA_BOSS_38h_Male_Goblin_EventBirdDead_03.prefab:cb381c7fe1966e84fa08f512e92d9122"
	};
}
