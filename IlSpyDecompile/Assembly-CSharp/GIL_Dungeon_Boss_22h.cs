using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_22h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_22h_Male_Undead_Intro_01.prefab:e2f409541d775204ea830c91c59b0fb8", "VO_GILA_BOSS_22h_Male_Undead_IntroTess_01.prefab:8c903084a02c75e47aa40f564b8f536a", "VO_GILA_BOSS_22h_Male_Undead_IntroCrowley_01.prefab:dacced81dd2ca414c89fe4e1700ede67", "VO_GILA_BOSS_22h_Male_Undead_EmoteResponse_01.prefab:35d13b301743d6b41b010da858bb9100", "VO_GILA_BOSS_22h_Male_Undead_EmoteResponseTess_01.prefab:54dd324d38e93764a8370741a5faf644", "VO_GILA_BOSS_22h_Male_Undead_Death_01.prefab:61c5f65e28a99f84d9d6219edf4ba769", "VO_GILA_BOSS_22h_Male_Undead_DefeatPlayer_01.prefab:54ef28245d1c78d4f988e652b352ccf1", "VO_GILA_BOSS_22h_Male_Undead_EventPlaysShiv_01.prefab:3b8e6220e4f4dee4dade6ae967cdd8f5", "VO_GILA_BOSS_22h_Male_Undead_EventPlaysCoin_01.prefab:2cf7e803e0a855149b7152e53e84ffc6" })
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_22h_Male_Undead_Death_01.prefab:61c5f65e28a99f84d9d6219edf4ba769";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCardId();
			if (!(cardId == "GILA_500h3"))
			{
				if (cardId == "GILA_600h")
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_IntroCrowley_01.prefab:dacced81dd2ca414c89fe4e1700ede67", Notification.SpeechBubbleDirection.TopRight, actor));
				}
				else
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_Intro_01.prefab:e2f409541d775204ea830c91c59b0fb8", Notification.SpeechBubbleDirection.TopRight, actor));
				}
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_IntroTess_01.prefab:8c903084a02c75e47aa40f564b8f536a", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			string cardId2 = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCardId();
			if (cardId2 == "GILA_500h3")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_EmoteResponseTess_01.prefab:54dd324d38e93764a8370741a5faf644", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_EmoteResponse_01.prefab:35d13b301743d6b41b010da858bb9100", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "GAME_005":
			case "CFM_630":
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_22h_Male_Undead_EventPlaysCoin_01.prefab:2cf7e803e0a855149b7152e53e84ffc6");
				break;
			case "EX1_278":
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_22h_Male_Undead_EventPlaysShiv_01.prefab:3b8e6220e4f4dee4dade6ae967cdd8f5");
				break;
			}
		}
	}
}
