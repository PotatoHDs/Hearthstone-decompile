using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_11h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_11h_Male_Trogg_Intro_01.prefab:54f6017f84e6d824e93a04abd27f6f01", "VO_LOOTA_BOSS_11h_Male_Trogg_EmoteResponse_01.prefab:ed6cf5a2a048fea4e98c50eff13cd707", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower1_01.prefab:dcb9b31148865ad428982e408330d33c", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower2_01.prefab:c07216b69bb2611419c50914f76f44c6", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower3_01.prefab:e679dfe45da0bb74891a11037790eb93", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower4_01.prefab:5856b86e997271a4fa2b1118dd2ef534", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower5_01.prefab:b300a890ae8c7d3458cf37bf1723357e", "VO_LOOTA_BOSS_11h_Male_Trogg_Death_01.prefab:5563abda7ad8407499b671cd4b63b3f8", "VO_LOOTA_BOSS_11h_Male_Trogg_DefeatPlayer_01.prefab:b09d58b2cb5875a4eba888ff6bcb6da5", "VO_LOOTA_BOSS_11h_Male_Trogg_EventEquality_01.prefab:6b570bed497bd2e458e70d929695c99c" })
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
		return new List<string> { "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower1_01.prefab:dcb9b31148865ad428982e408330d33c", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower2_01.prefab:c07216b69bb2611419c50914f76f44c6", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower3_01.prefab:e679dfe45da0bb74891a11037790eb93", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower4_01.prefab:5856b86e997271a4fa2b1118dd2ef534", "VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower5_01.prefab:b300a890ae8c7d3458cf37bf1723357e" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_11h_Male_Trogg_Death_01.prefab:5563abda7ad8407499b671cd4b63b3f8";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_11h_Male_Trogg_Intro_01.prefab:54f6017f84e6d824e93a04abd27f6f01", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_11h_Male_Trogg_EmoteResponse_01.prefab:ed6cf5a2a048fea4e98c50eff13cd707", Notification.SpeechBubbleDirection.TopRight, actor));
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
			if (cardId == "EX1_619")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_11h_Male_Trogg_EventEquality_01.prefab:6b570bed497bd2e458e70d929695c99c");
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
	}
}
