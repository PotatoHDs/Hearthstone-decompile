using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_33h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_TriggerPowerLines = new List<string> { "VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard1_01.prefab:dfd28e8d857457e44a1bedce379ee0b1", "VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard2_01.prefab:90a3fc7d66f2c1f41a7322f56d3aad21", "VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard3_01.prefab:68a1fb41b7d14e446bebc1489278086b" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_19h_Male_Trogg_Intro_01.prefab:00df9f15d69d8ce4e8553e579e3ff728", "VO_LOOTA_BOSS_19h_Male_Trogg_EmoteResponse_01.prefab:4385254ca60d5d64eb86d9341372d69f", "VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard1_01.prefab:dfd28e8d857457e44a1bedce379ee0b1", "VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard2_01.prefab:90a3fc7d66f2c1f41a7322f56d3aad21", "VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard3_01.prefab:68a1fb41b7d14e446bebc1489278086b", "VO_LOOTA_BOSS_19h_Male_Trogg_Death_01.prefab:d0fa743934bc7a24db09df3af3ce0b77", "VO_LOOTA_BOSS_19h_Male_Trogg_DefeatPlayer_01.prefab:0a0997eeb9130dc4382df8e2f6c23b2d", "VO_LOOTA_BOSS_19h_Male_Trogg_EventHandFull_01.prefab:0cf7309fe898f1b4cb9def67e388d19e" })
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
		return "VO_LOOTA_BOSS_19h_Male_Trogg_Death_01.prefab:d0fa743934bc7a24db09df3af3ce0b77";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_19h_Male_Trogg_Intro_01.prefab:00df9f15d69d8ce4e8553e579e3ff728", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_19h_Male_Trogg_EmoteResponse_01.prefab:4385254ca60d5d64eb86d9341372d69f", Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 102:
			if (m_TriggerPowerLines.Count != 0)
			{
				string randomLine = m_TriggerPowerLines[Random.Range(0, m_TriggerPowerLines.Count)];
				yield return PlayLineOnlyOnce(enemyActor, randomLine);
				m_TriggerPowerLines.Remove(randomLine);
			}
			break;
		case 103:
			yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_19h_Male_Trogg_EventHandFull_01.prefab:0cf7309fe898f1b4cb9def67e388d19e");
			break;
		}
	}
}
