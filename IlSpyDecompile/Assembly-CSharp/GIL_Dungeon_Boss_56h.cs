using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_56h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomLines = new List<string> { "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_01.prefab:df2dfa8b69de19343b42692930e430bb", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_02.prefab:fb562cb1d85c1fc448d2e95add40c48f", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_03.prefab:dd5ce8bfe6318fb4394c85689dd2a775", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_05.prefab:49fa7560ef80a1244a7b53882c9ced80", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_07.prefab:9f7a98727087e5c409840e4d54730bd8", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_09.prefab:f9dae16439fc87a4fa49fd29c9970e79", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_10.prefab:98e67b5d19c2dcd4b8165a280f3955fb" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_56h_Male_Elemental_IntroALT_02.prefab:685ce5a72c2c9b143ae449798cd25504", "VO_GILA_BOSS_56h_Male_Elemental_EmoteResponse_01.prefab:b6e33ab8bedec2f45880bc0814a6244e", "VO_GILA_BOSS_56h_Male_Elemental_Death_01.prefab:106ca4785aecf2841bb4480f0faffc55", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_01.prefab:df2dfa8b69de19343b42692930e430bb", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_02.prefab:fb562cb1d85c1fc448d2e95add40c48f", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_03.prefab:dd5ce8bfe6318fb4394c85689dd2a775", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_05.prefab:49fa7560ef80a1244a7b53882c9ced80", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_07.prefab:9f7a98727087e5c409840e4d54730bd8", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_09.prefab:f9dae16439fc87a4fa49fd29c9970e79", "VO_GILA_BOSS_56h_Male_Elemental_HeroPower_10.prefab:98e67b5d19c2dcd4b8165a280f3955fb",
			"VO_GILA_BOSS_56h_Male_Elemental_EventTransformFace_02.prefab:cc00cd508d1eca745a9fd7360b2a9bea"
		})
		{
			PreloadSound(item);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_56h_Male_Elemental_IntroALT_02.prefab:685ce5a72c2c9b143ae449798cd25504", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_56h_Male_Elemental_EmoteResponse_01.prefab:b6e33ab8bedec2f45880bc0814a6244e", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_56h_Male_Elemental_Death_01.prefab:106ca4785aecf2841bb4480f0faffc55";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
		{
			string text = PopRandomLineWithChance(m_RandomLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		case 102:
			yield return PlayBossLine(actor, "VO_GILA_BOSS_56h_Male_Elemental_EventTransformFace_02.prefab:cc00cd508d1eca745a9fd7360b2a9bea");
			break;
		}
	}
}
