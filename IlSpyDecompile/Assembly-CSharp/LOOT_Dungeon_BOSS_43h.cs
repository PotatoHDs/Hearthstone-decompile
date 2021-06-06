using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_43h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_NormalHeroLines = new List<string> { "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility1_01.prefab:d67ddbe9d86c15448bbd704d6b1d9ea8", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility2_01.prefab:dbf5167e9736f864791569eecc2d9ea4", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility3_01.prefab:13705bf4d60d9344a9245bad79dda6fc" };

	private List<string> m_BurnedHeroLines = new List<string> { "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull1_01.prefab:49448716e2caa7848b6b36fdb510d857", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull2_01.prefab:a25a5ea2ac1395747b5a0a99b5976974", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull3_01.prefab:75e111a15c0fbc141b4c44c04972f981" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_43h_Female_Djinn_Intro_01.prefab:d807d173b15d7a349ac7633f4d6d49cb", "VO_LOOTA_BOSS_43h_Female_Djinn_EmoteResponse_01.prefab:b0b146264452748448f38ba8653c4152", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility1_01.prefab:d67ddbe9d86c15448bbd704d6b1d9ea8", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility2_01.prefab:dbf5167e9736f864791569eecc2d9ea4", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility3_01.prefab:13705bf4d60d9344a9245bad79dda6fc", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull1_01.prefab:49448716e2caa7848b6b36fdb510d857", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull2_01.prefab:a25a5ea2ac1395747b5a0a99b5976974", "VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull3_01.prefab:75e111a15c0fbc141b4c44c04972f981", "VO_LOOTA_BOSS_43h_Female_Djinn_Death_01.prefab:2947452bb2cf9a644983baf0e5d720e7", "VO_LOOTA_BOSS_43h_Female_Djinn_DefeatPlayer_01.prefab:13966eb897e19934bb20d494b873a945" })
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
		return "VO_LOOTA_BOSS_43h_Female_Djinn_Death_01.prefab:2947452bb2cf9a644983baf0e5d720e7";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_43h_Female_Djinn_Intro_01.prefab:d807d173b15d7a349ac7633f4d6d49cb", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_43h_Female_Djinn_EmoteResponse_01.prefab:b0b146264452748448f38ba8653c4152", Notification.SpeechBubbleDirection.TopRight, actor));
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
			int num = 30;
			int num2 = Random.Range(0, 100);
			if (m_NormalHeroLines.Count != 0 && num >= num2)
			{
				string randomLine2 = m_NormalHeroLines[Random.Range(0, m_NormalHeroLines.Count)];
				yield return PlayLineOnlyOnce(enemyActor, randomLine2);
				m_NormalHeroLines.Remove(randomLine2);
				yield return null;
			}
			break;
		}
		case 102:
		{
			int num = 20;
			int num2 = Random.Range(0, 100);
			if (m_BurnedHeroLines.Count != 0 && num >= num2)
			{
				string randomLine2 = m_BurnedHeroLines[Random.Range(0, m_BurnedHeroLines.Count)];
				yield return PlayLineOnlyOnce(enemyActor, randomLine2);
				m_BurnedHeroLines.Remove(randomLine2);
				yield return null;
			}
			break;
		}
		}
	}
}
