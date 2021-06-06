using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003FA RID: 1018
public class GIL_Dungeon_Boss_49h : GIL_Dungeon
{
	// Token: 0x0600387C RID: 14460 RVA: 0x0011CE14 File Offset: 0x0011B014
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_49h_Female_Undead_Intro_02.prefab:2f0acde3738fe4446865336c303d12a8",
			"VO_GILA_BOSS_49h_Female_Undead_EmoteResponse_01.prefab:bc5c2c4ceee01194fa3a2008208b7bd4",
			"VO_GILA_BOSS_49h_Female_Undead_Death_01.prefab:ca4b279f54ee8db4f9ec1907a85fd73e",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_01.prefab:d9d094e9e67fac3458521751d4feb5b2",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_02.prefab:0289abf9ce7d95949ae8b9c0c18412bb",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_03.prefab:5fda1ce79f9a98442bcfb238e214937e",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_04.prefab:0b1588ab70f395f4fb37eae0b35aec0d",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_05.prefab:34d5190a3edea1745bec766025bee71e",
			"VO_GILA_BOSS_49h_Female_Undead_EventPact_01.prefab:ccdcf80420c1f234bbf1ce63c976ea7e",
			"VO_GILA_BOSS_49h_Female_Undead_EventPact_02.prefab:31395c54f2db7504e8843c6dfa385a96",
			"VO_GILA_BOSS_49h_Female_Undead_EventPact_03.prefab:22836152465e0b240aad0e8121f2d88f",
			"VO_GILA_BOSS_49h_Female_Undead_EventPact_05.prefab:9ce174ea9bda3044994468a83d6b0e04",
			"VO_GILA_BOSS_49h_Female_Undead_EventFaithfulDies_01.prefab:bbda8addb74a4d64e88928a14faec44e",
			"VO_GILA_BOSS_49h_Female_Undead_EventFaithfulDies_02.prefab:9bd5404fb376f8f4d93325153663c8ae",
			"VO_GILA_BOSS_49h_Female_Undead_EventFaithfulDies_03.prefab:988dd4ecb5ffd534d9d5ba128736c330",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayTreant_01.prefab:e92c0d40ff6129a47abcfc29f57b6e7b",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayBird_01.prefab:82defd64034acef4ab92df6b337f4562",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayCauldron_01.prefab:6a62fc2214d9c9c46acd777d8965cba2",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayHex_01.prefab:62c66cae6890a5a448a7f2840c80290a",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlaySnake_01.prefab:2d3b11843f5f807488ef0167c5dd158d",
			"VO_GILA_BOSS_49h_Female_Undead_EventPlayDragon_01.prefab:4e0a2fcb4940e5248acf3f7c6cde7b72",
			"VO_GILA_BOSS_49h_Female_Undead_EventTurn02_01.prefab:a52e60d47c36dac4bba54bde6e75c7c8",
			"VO_GILA_400h_Male_Human_EVENT_NEMESIS_TURN2_01.prefab:aa59fb8f1a646c042b1b22355393a9a3"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600387D RID: 14461 RVA: 0x00118E06 File Offset: 0x00117006
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	// Token: 0x0600387E RID: 14462 RVA: 0x0011CF6C File Offset: 0x0011B16C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_49h_Female_Undead_Intro_02.prefab:2f0acde3738fe4446865336c303d12a8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_49h_Female_Undead_EmoteResponse_01.prefab:bc5c2c4ceee01194fa3a2008208b7bd4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600387F RID: 14463 RVA: 0x0011CFF3 File Offset: 0x0011B1F3
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_49h_Female_Undead_Death_01.prefab:ca4b279f54ee8db4f9ec1907a85fd73e";
	}

	// Token: 0x06003880 RID: 14464 RVA: 0x0011CFFA File Offset: 0x0011B1FA
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
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "GILA_BOSS_49t"))
		{
			if (cardId == "GILA_BOSS_49t2")
			{
				string text = base.PopRandomLineWithChance(this.m_RandomPlayPactLines);
				if (text != null)
				{
					yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
				}
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_RandomPlayFaithfulLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003882 RID: 14466 RVA: 0x0011D010 File Offset: 0x0011B210
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 2398220810U)
		{
			if (num <= 1736428460U)
			{
				if (num <= 835677842U)
				{
					if (num != 693742830U)
					{
						if (num != 717358432U)
						{
							if (num != 835677842U)
							{
								goto IL_554;
							}
							if (!(cardId == "ICC_023"))
							{
								goto IL_554;
							}
							goto IL_49B;
						}
						else if (!(cardId == "EX1_tk9"))
						{
							goto IL_554;
						}
					}
					else if (!(cardId == "UNG_111t1"))
					{
						goto IL_554;
					}
				}
				else if (num != 1191726123U)
				{
					if (num != 1449416540U)
					{
						if (num != 1736428460U)
						{
							goto IL_554;
						}
						if (!(cardId == "LOOT_170"))
						{
							goto IL_554;
						}
						goto IL_49B;
					}
					else
					{
						if (!(cardId == "NEW1_016"))
						{
							goto IL_554;
						}
						goto IL_49B;
					}
				}
				else
				{
					if (!(cardId == "KAR_300"))
					{
						goto IL_554;
					}
					goto IL_49B;
				}
			}
			else if (num <= 1945129914U)
			{
				if (num != 1791989495U)
				{
					if (num != 1855769270U)
					{
						if (num != 1945129914U)
						{
							goto IL_554;
						}
						if (!(cardId == "EX1_158t"))
						{
							goto IL_554;
						}
					}
					else if (!(cardId == "FB_Champs_EX1_tk9"))
					{
						goto IL_554;
					}
				}
				else if (!(cardId == "GIL_663t"))
				{
					goto IL_554;
				}
			}
			else if (num != 2126902964U)
			{
				if (num != 2212797563U)
				{
					if (num != 2398220810U)
					{
						goto IL_554;
					}
					if (!(cardId == "EX1_554t"))
					{
						goto IL_554;
					}
					goto IL_528;
				}
				else
				{
					if (!(cardId == "UNG_027"))
					{
						goto IL_554;
					}
					goto IL_49B;
				}
			}
			else
			{
				if (!(cardId == "EX1_246"))
				{
					goto IL_554;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_49h_Female_Undead_EventPlayHex_01.prefab:62c66cae6890a5a448a7f2840c80290a", 2.5f);
				goto IL_554;
			}
		}
		else if (num <= 3275638207U)
		{
			if (num <= 3118109640U)
			{
				if (num != 2693582287U)
				{
					if (num != 2747335539U)
					{
						if (num != 3118109640U)
						{
							goto IL_554;
						}
						if (!(cardId == "KAR_037"))
						{
							goto IL_554;
						}
						goto IL_49B;
					}
					else if (!(cardId == "EX1_573t"))
					{
						goto IL_554;
					}
				}
				else
				{
					if (!(cardId == "EX1_009"))
					{
						goto IL_554;
					}
					goto IL_49B;
				}
			}
			else if (num != 3132923117U)
			{
				if (num != 3208380636U)
				{
					if (num != 3275638207U)
					{
						goto IL_554;
					}
					if (!(cardId == "CS2_203"))
					{
						goto IL_554;
					}
					goto IL_49B;
				}
				else
				{
					if (!(cardId == "CS2_237"))
					{
						goto IL_554;
					}
					goto IL_49B;
				}
			}
			else
			{
				if (!(cardId == "LOE_010"))
				{
					goto IL_554;
				}
				goto IL_528;
			}
		}
		else if (num <= 3639008486U)
		{
			if (num != 3294814722U)
			{
				if (num != 3368492501U)
				{
					if (num != 3639008486U)
					{
						goto IL_554;
					}
					if (!(cardId == "CS2_169"))
					{
						goto IL_554;
					}
					goto IL_49B;
				}
				else if (!(cardId == "FP1_019t"))
				{
					goto IL_554;
				}
			}
			else
			{
				if (!(cardId == "GIL_819"))
				{
					goto IL_554;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_49h_Female_Undead_EventPlayCauldron_01.prefab:6a62fc2214d9c9c46acd777d8965cba2", 2.5f);
				goto IL_554;
			}
		}
		else if (num != 4065199500U)
		{
			if (num != 4229084096U)
			{
				if (num != 4237145036U)
				{
					goto IL_554;
				}
				if (!(cardId == "UNG_912"))
				{
					goto IL_554;
				}
				goto IL_49B;
			}
			else
			{
				if (!(cardId == "EX1_170"))
				{
					goto IL_554;
				}
				goto IL_528;
			}
		}
		else
		{
			if (!(cardId == "GIL_664"))
			{
				goto IL_554;
			}
			goto IL_49B;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_49h_Female_Undead_EventPlayTreant_01.prefab:e92c0d40ff6129a47abcfc29f57b6e7b", 2.5f);
		goto IL_554;
		IL_49B:
		yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_49h_Female_Undead_EventPlayBird_01.prefab:82defd64034acef4ab92df6b337f4562", 2.5f);
		goto IL_554;
		IL_528:
		yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_49h_Female_Undead_EventPlaySnake_01.prefab:2d3b11843f5f807488ef0167c5dd158d", 2.5f);
		IL_554:
		if (entity.HasRace(TAG_RACE.DRAGON))
		{
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_49h_Female_Undead_EventPlayDragon_01.prefab:4e0a2fcb4940e5248acf3f7c6cde7b72", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003883 RID: 14467 RVA: 0x0011D026 File Offset: 0x0011B226
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		if (missionEvent == 101)
		{
			string text = base.PopRandomLineWithChance(this.m_RandomPlayFaithfulDeathLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003884 RID: 14468 RVA: 0x0011D03C File Offset: 0x0011B23C
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (turn == 2)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_400h_Male_Human_EVENT_NEMESIS_TURN2_01.prefab:aa59fb8f1a646c042b1b22355393a9a3", 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_49h_Female_Undead_EventTurn02_01.prefab:a52e60d47c36dac4bba54bde6e75c7c8", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001DC2 RID: 7618
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DC3 RID: 7619
	private List<string> m_RandomPlayFaithfulLines = new List<string>
	{
		"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_01.prefab:d9d094e9e67fac3458521751d4feb5b2",
		"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_02.prefab:0289abf9ce7d95949ae8b9c0c18412bb",
		"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_03.prefab:5fda1ce79f9a98442bcfb238e214937e",
		"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_04.prefab:0b1588ab70f395f4fb37eae0b35aec0d",
		"VO_GILA_BOSS_49h_Female_Undead_EventPlayFaithful_05.prefab:34d5190a3edea1745bec766025bee71e"
	};

	// Token: 0x04001DC4 RID: 7620
	private List<string> m_RandomPlayPactLines = new List<string>
	{
		"VO_GILA_BOSS_49h_Female_Undead_EventPact_01.prefab:ccdcf80420c1f234bbf1ce63c976ea7e",
		"VO_GILA_BOSS_49h_Female_Undead_EventPact_02.prefab:31395c54f2db7504e8843c6dfa385a96",
		"VO_GILA_BOSS_49h_Female_Undead_EventPact_03.prefab:22836152465e0b240aad0e8121f2d88f",
		"VO_GILA_BOSS_49h_Female_Undead_EventPact_05.prefab:9ce174ea9bda3044994468a83d6b0e04"
	};

	// Token: 0x04001DC5 RID: 7621
	private List<string> m_RandomPlayFaithfulDeathLines = new List<string>
	{
		"VO_GILA_BOSS_49h_Female_Undead_EventFaithfulDies_01.prefab:bbda8addb74a4d64e88928a14faec44e",
		"VO_GILA_BOSS_49h_Female_Undead_EventFaithfulDies_02.prefab:9bd5404fb376f8f4d93325153663c8ae",
		"VO_GILA_BOSS_49h_Female_Undead_EventFaithfulDies_03.prefab:988dd4ecb5ffd534d9d5ba128736c330"
	};
}
