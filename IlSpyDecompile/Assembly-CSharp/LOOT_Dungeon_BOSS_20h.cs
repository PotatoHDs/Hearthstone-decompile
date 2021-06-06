using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_20h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_StatueDestroyedLines = new List<string> { "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed1_01.prefab:ef7a58c5d160ca541958ec50bdfb356c", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed2_01.prefab:0432d18c4a89d3c449e38372253cecd7", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed3_01.prefab:6b618e4b53aaceb4598f071f0a1566ec", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed4_01.prefab:c8e2a6ed7c8b5584fb3b02cf29c82ed0", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed5_01.prefab:0294fb2a21551f844b543bbd163cb506" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_LOOTA_BOSS_20h_Male_Earthen_Intro_01.prefab:b1712d4e38816874f82613e4fa8060e2", "VO_LOOTA_BOSS_20h_Male_Earthen_EmoteResponse_01.prefab:b77df03627d7c2f449aa626db3255011", "VO_LOOTA_BOSS_20h_Male_Earthen_HeroPower_01.prefab:e37c0e7e80d2a684ebff084ab31d4555", "VO_LOOTA_BOSS_20h_Male_Earthen_HeroPowerNoStatues_01.prefab:dfec071a415b9f6498924e7fcda87900", "VO_LOOTA_BOSS_20h_Male_Earthen_Death_01.prefab:9c60adabd0d578e42a982f43d9e38395", "VO_LOOTA_BOSS_20h_Male_Earthen_DefeatPlayer_01.prefab:5283495640bba424ebb09ad1b79bc5d0", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed1_01.prefab:ef7a58c5d160ca541958ec50bdfb356c", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed2_01.prefab:0432d18c4a89d3c449e38372253cecd7", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed3_01.prefab:6b618e4b53aaceb4598f071f0a1566ec", "VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed4_01.prefab:c8e2a6ed7c8b5584fb3b02cf29c82ed0",
			"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed5_01.prefab:0294fb2a21551f844b543bbd163cb506"
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
		return "VO_LOOTA_BOSS_20h_Male_Earthen_Death_01.prefab:9c60adabd0d578e42a982f43d9e38395";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_20h_Male_Earthen_Intro_01.prefab:b1712d4e38816874f82613e4fa8060e2", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_20h_Male_Earthen_EmoteResponse_01.prefab:b77df03627d7c2f449aa626db3255011", Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
		switch (missionEvent)
		{
		case 101:
		{
			int num = 50;
			int num2 = Random.Range(0, 100);
			if (m_StatueDestroyedLines.Count != 0 && num >= num2)
			{
				string randomLine = m_StatueDestroyedLines[Random.Range(0, m_StatueDestroyedLines.Count)];
				yield return PlayLineOnlyOnce(enemyActor, randomLine);
				m_StatueDestroyedLines.Remove(randomLine);
				yield return null;
			}
			break;
		}
		case 102:
			yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_20h_Male_Earthen_HeroPower_01.prefab:e37c0e7e80d2a684ebff084ab31d4555");
			yield return null;
			break;
		case 103:
			yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_20h_Male_Earthen_HeroPowerNoStatues_01.prefab:dfec071a415b9f6498924e7fcda87900");
			yield return null;
			break;
		}
	}
}
