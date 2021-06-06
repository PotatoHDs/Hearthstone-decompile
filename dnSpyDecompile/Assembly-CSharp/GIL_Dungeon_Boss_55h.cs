using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003FF RID: 1023
public class GIL_Dungeon_Boss_55h : GIL_Dungeon
{
	// Token: 0x060038AC RID: 14508 RVA: 0x0011D998 File Offset: 0x0011BB98
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_55h_Female_NightElf_Intro_01.prefab:e3728559d0b0985429d242b1fe2b9b8b",
			"VO_GILA_BOSS_55h_Female_NightElf_EmoteResponse_01.prefab:116d3641525316e48946409c8fc0c513",
			"VO_GILA_BOSS_55h_Female_NightElf_Death_01.prefab:43f663f0d8f79e74bb961d940249124b",
			"VO_GILA_BOSS_55h_Female_NightElf_HeroPower_01.prefab:1fb7a796c7919ea4eb8de297546ee318",
			"VO_GILA_BOSS_55h_Female_NightElf_HeroPower_02.prefab:e9fc95ab8de424f4886016550f337f3c",
			"VO_GILA_BOSS_55h_Female_NightElf_HeroPower_03.prefab:a0f70912013f6384198a5572b898d4c2",
			"VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_02.prefab:99cd15bc7f01799419e95264a6001b42",
			"VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_03.prefab:6dafb4b22c71af64093d6ee4bf7eb356",
			"VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_04.prefab:a9602988cfe2c26498142ae555290ce9",
			"VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_01.prefab:a6058e565a8c41840a594ee925f4b2f1",
			"VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_02.prefab:7191f790b24aa1144a51d2b31db77777",
			"VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_03.prefab:165abae668774ed4c811ddd64640aad8",
			"VO_GILA_BOSS_55h_Female_NightElf_EventPlayCompass_01.prefab:4976a34329a9cbc48926976b879ca402",
			"VO_GILA_BOSS_55h_Female_NightElf_EventPlayCartographer_01.prefab:68105bfdb75d15444a189b7713d2eb6a"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038AD RID: 14509 RVA: 0x0011DA8C File Offset: 0x0011BC8C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_55h_Female_NightElf_Intro_01.prefab:e3728559d0b0985429d242b1fe2b9b8b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_55h_Female_NightElf_EmoteResponse_01.prefab:116d3641525316e48946409c8fc0c513", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038AE RID: 14510 RVA: 0x0011DB13 File Offset: 0x0011BD13
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_55h_Female_NightElf_Death_01.prefab:43f663f0d8f79e74bb961d940249124b";
	}

	// Token: 0x060038AF RID: 14511 RVA: 0x0011DB1A File Offset: 0x0011BD1A
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
		if (cardId == "GILA_BOSS_55t2")
		{
			string text = base.PopRandomLineWithChance(this.m_RandomAssimilationLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060038B0 RID: 14512 RVA: 0x0011DB30 File Offset: 0x0011BD30
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_55h_Female_NightElf_HeroPower_01.prefab:1fb7a796c7919ea4eb8de297546ee318",
			"VO_GILA_BOSS_55h_Female_NightElf_HeroPower_02.prefab:e9fc95ab8de424f4886016550f337f3c",
			"VO_GILA_BOSS_55h_Female_NightElf_HeroPower_03.prefab:a0f70912013f6384198a5572b898d4c2"
		};
	}

	// Token: 0x060038B1 RID: 14513 RVA: 0x0011DB58 File Offset: 0x0011BD58
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
		if (!(cardId == "GILA_853b"))
		{
			if (cardId == "GILA_802")
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_55h_Female_NightElf_EventPlayCartographer_01.prefab:68105bfdb75d15444a189b7713d2eb6a", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_55h_Female_NightElf_EventPlayCompass_01.prefab:4976a34329a9cbc48926976b879ca402", 2.5f);
		}
		yield break;
	}

	// Token: 0x060038B2 RID: 14514 RVA: 0x0011DB6E File Offset: 0x0011BD6E
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
			string text = base.PopRandomLineWithChance(this.m_RandomWispLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DD0 RID: 7632
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DD1 RID: 7633
	private List<string> m_RandomWispLines = new List<string>
	{
		"VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_02.prefab:99cd15bc7f01799419e95264a6001b42",
		"VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_03.prefab:6dafb4b22c71af64093d6ee4bf7eb356",
		"VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_04.prefab:a9602988cfe2c26498142ae555290ce9"
	};

	// Token: 0x04001DD2 RID: 7634
	private List<string> m_RandomAssimilationLines = new List<string>
	{
		"VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_01.prefab:a6058e565a8c41840a594ee925f4b2f1",
		"VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_02.prefab:7191f790b24aa1144a51d2b31db77777",
		"VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_03.prefab:165abae668774ed4c811ddd64640aad8"
	};
}
