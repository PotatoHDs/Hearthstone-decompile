using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_36h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_PlayerHex = new List<string> { "VO_GILA_BOSS_36h_Female_Human_EventHex_01.prefab:3ac21887b5d04084cba245f59cdf08e2", "VO_GILA_BOSS_36h_Female_Human_EventHex_02.prefab:9213365e2510a7e488c2291eae467da5" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_36h_Female_Human_Intro_01.prefab:57aad793ef959bc4e8fb0d5bd541240e", "VO_GILA_BOSS_36h_Female_Human_Emote Response_01:10f920edf9f9dc84595c32d34bc30106", "VO_GILA_BOSS_36h_Female_Human_Death_01.prefab:2f9429b7a50339340991733a35640edf", "VO_GILA_BOSS_36h_Female_Human_HeroPower_01.prefab:cee27fa7fae7801449ea6d9093449aa3", "VO_GILA_BOSS_36h_Female_Human_HeroPower_02.prefab:92888a961bb3af34f9e715d3bd83368c", "VO_GILA_BOSS_36h_Female_Human_HeroPower_03.prefab:dc8ea40d25fbe344d8900d53fbffbb5c", "VO_GILA_BOSS_36h_Female_Human_EventHex_01.prefab:3ac21887b5d04084cba245f59cdf08e2", "VO_GILA_BOSS_36h_Female_Human_EventHex_02.prefab:9213365e2510a7e488c2291eae467da5" })
		{
			PreloadSound(item);
		}
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_36h_Female_Human_HeroPower_01.prefab:cee27fa7fae7801449ea6d9093449aa3", "VO_GILA_BOSS_36h_Female_Human_HeroPower_02.prefab:92888a961bb3af34f9e715d3bd83368c", "VO_GILA_BOSS_36h_Female_Human_HeroPower_03.prefab:dc8ea40d25fbe344d8900d53fbffbb5c" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_36h_Female_Human_Death_01.prefab:2f9429b7a50339340991733a35640edf";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_36h_Female_Human_Intro_01.prefab:57aad793ef959bc4e8fb0d5bd541240e", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_36h_Female_Human_Emote Response_01:10f920edf9f9dc84595c32d34bc30106", Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (cardId == "EX1_246")
		{
			string text = PopRandomLineWithChance(m_PlayerHex);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}
}
