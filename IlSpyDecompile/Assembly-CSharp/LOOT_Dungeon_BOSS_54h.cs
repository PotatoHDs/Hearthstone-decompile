using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_54h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_54h_Female_Human_Intro_01.prefab:fc331202e4398374c87ba5e467990a7a", "VO_LOOTA_BOSS_54h_Female_Human_EmoteResponse_01.prefab:30a58ef54493277458e6f40e10808e73", "VO_LOOTA_BOSS_54h_Female_Human_WakeUp_01.prefab:3459b094d47361849a38d1c11d100d07", "VO_LOOTA_BOSS_54h_Female_Human_Death_01.prefab:0b30aff6d8b02bf44aa25146198146c9", "VO_LOOTA_BOSS_54h_Female_Human_DefeatPlayer_01.prefab:f710812544c073044b59f063537fe205", "VO_LOOTA_BOSS_54h_Female_Human_Intro_02.prefab:138fa139ba8594e47aabf7064ef0a62a", "VO_LOOTA_BOSS_54h_Female_Human_Intro_03.prefab:0c95e3240fb81a34b99ffcb977dee49a", "VO_LOOTA_BOSS_54h_Female_Human_Intro_04.prefab:5547e3a11ad9c6c4eb7fe18a7fda090f", "VO_LOOTA_BOSS_54h_Female_Human_Intro_07.prefab:2740bd918d6591643a076e2b0804021b" })
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
		return new List<string> { "VO_LOOTA_BOSS_54h_Female_Human_Intro_02.prefab:138fa139ba8594e47aabf7064ef0a62a", "VO_LOOTA_BOSS_54h_Female_Human_Intro_03.prefab:0c95e3240fb81a34b99ffcb977dee49a", "VO_LOOTA_BOSS_54h_Female_Human_Intro_04.prefab:5547e3a11ad9c6c4eb7fe18a7fda090f", "VO_LOOTA_BOSS_54h_Female_Human_Intro_07.prefab:2740bd918d6591643a076e2b0804021b" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_54h_Female_Human_Death_01.prefab:0b30aff6d8b02bf44aa25146198146c9";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_54h_Female_Human_Intro_01.prefab:fc331202e4398374c87ba5e467990a7a", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (GameState.Get().GetTurn() >= 12)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_54h_Female_Human_DefeatPlayer_01.prefab:f710812544c073044b59f063537fe205", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_54h_Female_Human_EmoteResponse_01.prefab:30a58ef54493277458e6f40e10808e73", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			yield return PlayLoyalSideKickBetrayal(missionEvent);
			if (missionEvent == 101)
			{
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_54h_Female_Human_WakeUp_01.prefab:3459b094d47361849a38d1c11d100d07");
			}
		}
	}
}
