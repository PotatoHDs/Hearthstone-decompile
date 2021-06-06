using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E4 RID: 996
public class GIL_Dungeon_Boss_26h : GIL_Dungeon
{
	// Token: 0x060037B4 RID: 14260 RVA: 0x00119FC4 File Offset: 0x001181C4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_26h_Male_Tauren_Intro_02.prefab:356bedd73c7f2d4429b9a27d2bba1c9c",
			"VO_GILA_BOSS_26h_Male_Tauren_IntroToki_02.prefab:a2170e88fa5ee854bbfdb0ef56c85c47",
			"VO_GILA_BOSS_26h_Male_Tauren_EmoteResponse_01.prefab:64aa7106bd06b1d4696335e0fe16cc60",
			"VO_GILA_BOSS_26h_Male_Tauren_EmoteResponseToki_01.prefab:692123ffe8ef90d43940f4e859ca22da",
			"VO_GILA_BOSS_26h_Male_Tauren_Death_01.prefab:665b491f22b62f44a8e0e3ba22a4f4ab",
			"VO_GILA_BOSS_26h_Male_Tauren_DeathToki_01.prefab:8b04253021cfa854ca2466f8cb560573",
			"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_01.prefab:54042ee9fcdd8f3468ebb79d05a9c8f6",
			"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_03.prefab:1c02f7141d30b774db1fa3c07b500436",
			"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_04.prefab:fcf56afd75e1ad140a86a3f6340e5d6a",
			"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_05.prefab:ae3a8f60d1eace9498f9ee7f63dd1000",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_01.prefab:89b7095a80b401d4f901b70832d3af8b",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_02.prefab:59764dcebfb7ed94db6d0749bf458388",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_05.prefab:223ce0eff623f1d498bc499f774a0396",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_06.prefab:9b3de161076f2de4cb07b07097a048ad",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysLeper_01.prefab:87b119c33758df5488c79e39f5726583",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysClockwork_02.prefab:750d4d813e1c28e478bf0bd1e07d53e8",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysMillhouse_02.prefab:2f68b6ac173275f4b888f6aed4682b08",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysThermaplugg_02.prefab:3296c7c83da25224eadb03712274952e",
			"VO_GILA_BOSS_26h_Male_Tauren_EventSilence_03.prefab:606503d7cd29980419bfc88a0b0d9c6c",
			"VO_GILA_BOSS_26h_Male_Tauren_EventBurgle_01.prefab:38c95405acb3f03448203a4e342bc93c",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlayGattlingGunner_01.prefab:d15061c1491699245be500c1e639bbb5",
			"VO_GILA_BOSS_26h_Male_Tauren_EventPlayClockworkAssistant_01.prefab:edeb9459e433a6b41bcb627a135255fb"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037B5 RID: 14261 RVA: 0x0011A110 File Offset: 0x00118310
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "GILA_900h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_26h_Male_Tauren_IntroToki_02.prefab:a2170e88fa5ee854bbfdb0ef56c85c47", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_26h_Male_Tauren_Intro_02.prefab:356bedd73c7f2d4429b9a27d2bba1c9c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				return;
			}
			if (cardId == "GILA_900h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_26h_Male_Tauren_EmoteResponseToki_01.prefab:692123ffe8ef90d43940f4e859ca22da", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_26h_Male_Tauren_EmoteResponse_01.prefab:64aa7106bd06b1d4696335e0fe16cc60", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037B6 RID: 14262 RVA: 0x0011A21A File Offset: 0x0011841A
	protected override string GetBossDeathLine()
	{
		if (GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId() == "GILA_900h")
		{
			return "VO_GILA_BOSS_26h_Male_Tauren_DeathToki_01.prefab:8b04253021cfa854ca2466f8cb560573";
		}
		return "VO_GILA_BOSS_26h_Male_Tauren_Death_01.prefab:665b491f22b62f44a8e0e3ba22a4f4ab";
	}

	// Token: 0x060037B7 RID: 14263 RVA: 0x0011A247 File Offset: 0x00118447
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
		if (!(cardId == "GILA_BOSS_26t2"))
		{
			if (!(cardId == "EX1_029"))
			{
				if (!(cardId == "GVG_082"))
				{
					if (!(cardId == "NEW1_029"))
					{
						if (!(cardId == "GVG_116"))
						{
							if (cardId == "AT_033")
							{
								yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventBurgle_01.prefab:38c95405acb3f03448203a4e342bc93c", 2.5f);
							}
						}
						else
						{
							yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventPlaysThermaplugg_02.prefab:3296c7c83da25224eadb03712274952e", 2.5f);
						}
					}
					else
					{
						yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventPlaysMillhouse_02.prefab:2f68b6ac173275f4b888f6aed4682b08", 2.5f);
					}
				}
				else
				{
					yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventPlaysClockwork_02.prefab:750d4d813e1c28e478bf0bd1e07d53e8", 2.5f);
				}
			}
			else
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventPlaysLeper_01.prefab:87b119c33758df5488c79e39f5726583", 2.5f);
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_SackOfGnomesRandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060037B8 RID: 14264 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060037B9 RID: 14265 RVA: 0x0011A25D File Offset: 0x0011845D
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
		if (!(cardId == "GILA_801"))
		{
			if (cardId == "GILA_907")
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventPlayClockworkAssistant_01.prefab:edeb9459e433a6b41bcb627a135255fb", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventPlayGattlingGunner_01.prefab:d15061c1491699245be500c1e639bbb5", 2.5f);
		}
		yield break;
	}

	// Token: 0x060037BA RID: 14266 RVA: 0x0011A273 File Offset: 0x00118473
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
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_26h_Male_Tauren_EventSilence_03.prefab:606503d7cd29980419bfc88a0b0d9c6c", 2.5f);
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_PlayGnomesRandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001D8F RID: 7567
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D90 RID: 7568
	private List<string> m_SackOfGnomesRandomLines = new List<string>
	{
		"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_01.prefab:54042ee9fcdd8f3468ebb79d05a9c8f6",
		"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_03.prefab:1c02f7141d30b774db1fa3c07b500436",
		"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_04.prefab:fcf56afd75e1ad140a86a3f6340e5d6a",
		"VO_GILA_BOSS_26h_Male_Tauren_EventSackOfGnomes_05.prefab:ae3a8f60d1eace9498f9ee7f63dd1000"
	};

	// Token: 0x04001D91 RID: 7569
	private List<string> m_PlayGnomesRandomLines = new List<string>
	{
		"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_01.prefab:89b7095a80b401d4f901b70832d3af8b",
		"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_02.prefab:59764dcebfb7ed94db6d0749bf458388",
		"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_05.prefab:223ce0eff623f1d498bc499f774a0396",
		"VO_GILA_BOSS_26h_Male_Tauren_EventPlaysGnome_06.prefab:9b3de161076f2de4cb07b07097a048ad"
	};
}
