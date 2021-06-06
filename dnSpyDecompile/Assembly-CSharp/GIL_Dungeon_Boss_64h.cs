using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000407 RID: 1031
public class GIL_Dungeon_Boss_64h : GIL_Dungeon
{
	// Token: 0x060038F7 RID: 14583 RVA: 0x0011EA0C File Offset: 0x0011CC0C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_64h_Male_Goblin_Intro_01.prefab:65aab9bfdc181c34794b8d264d546266",
			"VO_GILA_BOSS_64h_Male_Goblin_EmoteResponse_01.prefab:a0a1b9e4c2e6f9f4bafe6ccf9a265e98",
			"VO_GILA_BOSS_64h_Male_Goblin_Death_01.prefab:c4ef05fd8de6e2843b7436f68c970354",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandEmpty_01.prefab:6a88ebdeb564cf34c8a5ecc5f16f11de",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandSmall_01.prefab:5d88bcc0a05075040b7cfb83fdf592e6",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandSmall_02.prefab:28698528267e1ea4f986aecd06372f99",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandMedium_01.prefab:e6e970329d0630d44812eff64b772d04",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandMedium_02.prefab:d29b2384eabceff49809f4045f193b40",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandMedium_03.prefab:64f6e89d477969b4fbec31647e2f6b86",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandBig_01.prefab:aa92613c7b125fc499746b9111ab3f24",
			"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandBig_03.prefab:5cef405dfb09b5c49beda04ef476b9ed",
			"VO_GILA_BOSS_64h_Male_Goblin_EventMCTech_01.prefab:7918a8f38c26d5e4d878d1df3646a1aa",
			"VO_GILA_BOSS_64h_Male_Goblin_EventMindControl_01.prefab:a624eb8406e178d4da862a4de7765ab2",
			"VO_GILA_BOSS_64h_Male_Goblin_EvenPlayCursedCurio_01.prefab:335bddebe01902d479380f324eee0071"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038F8 RID: 14584 RVA: 0x0011EB00 File Offset: 0x0011CD00
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_64h_Male_Goblin_Intro_01.prefab:65aab9bfdc181c34794b8d264d546266", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_64h_Male_Goblin_EmoteResponse_01.prefab:a0a1b9e4c2e6f9f4bafe6ccf9a265e98", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038F9 RID: 14585 RVA: 0x0011EB87 File Offset: 0x0011CD87
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_64h_Male_Goblin_Death_01.prefab:c4ef05fd8de6e2843b7436f68c970354";
	}

	// Token: 0x060038FA RID: 14586 RVA: 0x0011EB8E File Offset: 0x0011CD8E
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060038FB RID: 14587 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x0011EBA4 File Offset: 0x0011CDA4
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
		if (!(cardId == "EX1_085"))
		{
			if (!(cardId == "CS1_113"))
			{
				if (cardId == "GILA_819")
				{
					yield return base.PlayBossLine(actor, "VO_GILA_BOSS_64h_Male_Goblin_EvenPlayCursedCurio_01.prefab:335bddebe01902d479380f324eee0071", 2.5f);
				}
			}
			else
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_64h_Male_Goblin_EventMindControl_01.prefab:a624eb8406e178d4da862a4de7765ab2", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_64h_Male_Goblin_EventMCTech_01.prefab:7918a8f38c26d5e4d878d1df3646a1aa", 2.5f);
		}
		yield break;
	}

	// Token: 0x060038FD RID: 14589 RVA: 0x0011EBBA File Offset: 0x0011CDBA
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
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandEmpty_01.prefab:6a88ebdeb564cf34c8a5ecc5f16f11de", 2.5f);
			break;
		case 102:
		{
			string text = base.PopRandomLineWithChance(this.m_SmallRandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
			break;
		}
		case 103:
		{
			string text = base.PopRandomLineWithChance(this.m_MediumRandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
			break;
		}
		case 104:
		{
			string text = base.PopRandomLineWithChance(this.m_BigRandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
			break;
		}
		}
		yield break;
	}

	// Token: 0x04001DE2 RID: 7650
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DE3 RID: 7651
	private List<string> m_SmallRandomLines = new List<string>
	{
		"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandSmall_01.prefab:5d88bcc0a05075040b7cfb83fdf592e6",
		"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandSmall_02.prefab:28698528267e1ea4f986aecd06372f99"
	};

	// Token: 0x04001DE4 RID: 7652
	private List<string> m_MediumRandomLines = new List<string>
	{
		"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandMedium_01.prefab:e6e970329d0630d44812eff64b772d04",
		"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandMedium_02.prefab:d29b2384eabceff49809f4045f193b40",
		"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandMedium_03.prefab:64f6e89d477969b4fbec31647e2f6b86"
	};

	// Token: 0x04001DE5 RID: 7653
	private List<string> m_BigRandomLines = new List<string>
	{
		"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandBig_01.prefab:aa92613c7b125fc499746b9111ab3f24",
		"VO_GILA_BOSS_64h_Male_Goblin_HeroPowerHandBig_03.prefab:5cef405dfb09b5c49beda04ef476b9ed"
	};
}
