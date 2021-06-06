using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_39h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomShuffleLines = new List<string> { "VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_01.prefab:555e3e45f1d745a4f8c4a2cd6852c9e9", "VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_02.prefab:d17efc332ca26e84698da64d005bc42c", "VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_03.prefab:fbfd8102e3349e5409b55cb0bc3a9f32" };

	private List<string> m_RandomDrawLines = new List<string> { "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_01.prefab:1c885e702a6bd904fa0a2710922f33fd", "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_02.prefab:49c1b6e5ba4085e46a44babc098c51b3", "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_03.prefab:8fcebf4a48445414598e6d1d04e76526", "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_04.prefab:0c39048a12578ef428fe10828178fe18" };

	private List<string> m_RandomFatigueLines = new List<string> { "VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_01.prefab:18453d7e2b37f714a805f082bcddf01d", "VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_02.prefab:9299aad73816811429f4da6082e9fdda" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_41h_Female_HumanGhost_Intro_01.prefab:2d9a2c1744722b04487c9983f830b25f", "VO_GILA_BOSS_41h_Female_HumanGhost_EmoteResponse_01.prefab:2f02f1ad1940efc4498a56fdf9425bc1", "VO_GILA_BOSS_41h_Female_HumanGhost_Death_01.prefab:0826bc5f228a6724789cd46200d05a94", "VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_01.prefab:555e3e45f1d745a4f8c4a2cd6852c9e9", "VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_02.prefab:d17efc332ca26e84698da64d005bc42c", "VO_GILA_BOSS_41h_Female_HumanGhost_EventShuffle_03.prefab:fbfd8102e3349e5409b55cb0bc3a9f32", "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_01.prefab:1c885e702a6bd904fa0a2710922f33fd", "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_02.prefab:49c1b6e5ba4085e46a44babc098c51b3", "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_03.prefab:8fcebf4a48445414598e6d1d04e76526", "VO_GILA_BOSS_41h_Female_HumanGhost_HeroPower_04.prefab:0c39048a12578ef428fe10828178fe18",
			"VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_01.prefab:18453d7e2b37f714a805f082bcddf01d", "VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigue_02.prefab:9299aad73816811429f4da6082e9fdda", "VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigueDeath_01.prefab:0551a75f20704894ba0340fafe3eefc2", "VO_GILA_BOSS_41h_Female_HumanGhost_EventPlayRin_01.prefab:ad7bd3274cab68846ae61271015ebc01"
		})
		{
			PreloadSound(item);
		}
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Female_HumanGhost_Intro_01.prefab:2d9a2c1744722b04487c9983f830b25f", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Female_HumanGhost_EmoteResponse_01.prefab:2f02f1ad1940efc4498a56fdf9425bc1", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_41h_Female_HumanGhost_Death_01.prefab:0826bc5f228a6724789cd46200d05a94";
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "LOOT_415":
			yield return PlayBossLine(actor, "VO_GILA_BOSS_41h_Female_HumanGhost_EventPlayRin_01.prefab:ad7bd3274cab68846ae61271015ebc01");
			break;
		case "CFM_602b":
		case "GIL_828":
		case "UNG_851":
		case "LOOT_106":
		case "LOOT_026":
		case "LOE_104":
		case "LOE_079":
		case "LOE_002":
		case "ICC_091":
		case "GILA_BOSS_60t":
		case "GILA_852a":
		case "GILA_821a":
		case "GILA_817":
		case "GILA_816a":
		case "GIL_815":
		case "CFM_660":
		case "BRM_007":
		{
			string text = PopRandomLineWithChance(m_RandomShuffleLines);
			if (text != null)
			{
				yield return PlayBossLine(actor, text);
			}
			break;
		}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
		{
			string text = PopRandomLineWithChance(m_RandomDrawLines);
			if (text != null)
			{
				yield return PlayBossLine(actor, text);
			}
			break;
		}
		case 102:
		{
			string text = PopRandomLineWithChance(m_RandomFatigueLines);
			if (text != null)
			{
				yield return PlayBossLine(actor, text);
			}
			break;
		}
		case 103:
			yield return PlayBossLine(actor, "VO_GILA_BOSS_41h_Female_HumanGhost_EventFatigueDeath_01.prefab:0551a75f20704894ba0340fafe3eefc2");
			break;
		}
	}
}
