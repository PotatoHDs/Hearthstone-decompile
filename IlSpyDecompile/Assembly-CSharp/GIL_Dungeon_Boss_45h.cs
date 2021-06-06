using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIL_Dungeon_Boss_45h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_PlayerDraw = new List<string> { "VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_01.prefab:59b04c5fc78d998419e9031e5724ac21", "VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_02.prefab:f1ced766b713bcd4bab20b5df385b375", "VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_03.prefab:92c52e500b3a5a049b2d3d44db263ec8" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_45h_Female_Human_Intro_01.prefab:5cbaf2dc51852e24dbd17bbbdf87cf8f", "VO_GILA_BOSS_45h_Female_Human_EmoteResponse_01.prefab:ec9bd3abc0eb6d243b3fc7a34fa43406", "VO_GILA_BOSS_45h_Female_Human_Death_02.prefab:a281121fabb777e49a73fa3abde185d9", "VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_01.prefab:59b04c5fc78d998419e9031e5724ac21", "VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_02.prefab:f1ced766b713bcd4bab20b5df385b375", "VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_03.prefab:92c52e500b3a5a049b2d3d44db263ec8", "VO_GILA_BOSS_45h_Female_Human_EventPlaysAcolyte_01.prefab:6cd648f8d77f0c944b9d82978c4ed975", "VO_GILA_BOSS_45h_Female_Human_EventPlaysNovice_01.prefab:c54565566da737d4f8a8aa92cf1648da", "VO_GILA_BOSS_45h_Female_Human_EventPlaysHoarder_01.prefab:2a99af8e9559db74a88b2f53f41ceb2d", "VO_GILA_BOSS_45h_Female_Human_EventFirstDamage_01.prefab:2aae7467c86b7624085aa0e2bf6beaef",
			"VO_GILA_BOSS_45h_Female_Human_EventPlayHallowedWater_01.prefab:a0de02488030e9e4fb0caa5deced73d6"
		})
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_45h_Female_Human_Death_02.prefab:a281121fabb777e49a73fa3abde185d9";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_45h_Female_Human_Intro_01.prefab:5cbaf2dc51852e24dbd17bbbdf87cf8f", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_45h_Female_Human_EmoteResponse_01.prefab:ec9bd3abc0eb6d243b3fc7a34fa43406", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			if (m_PlayerDraw.Count > 0)
			{
				string text = m_PlayerDraw[Random.Range(0, m_PlayerDraw.Count)];
				m_PlayerDraw.Remove(text);
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		case 102:
			yield return PlayBossLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventFirstDamage_01.prefab:2aae7467c86b7624085aa0e2bf6beaef");
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardID = entity.GetCardId();
			m_playedLines.Add(cardID);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardID)
			{
			case "EX1_007":
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlaysAcolyte_01.prefab:6cd648f8d77f0c944b9d82978c4ed975");
				break;
			case "EX1_015":
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlaysNovice_01.prefab:c54565566da737d4f8a8aa92cf1648da");
				break;
			case "EX1_096":
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlaysHoarder_01.prefab:2a99af8e9559db74a88b2f53f41ceb2d");
				break;
			case "GILA_850b":
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlayHallowedWater_01.prefab:a0de02488030e9e4fb0caa5deced73d6");
				break;
			}
		}
	}
}
