using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_44h : LOOT_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_44h_Female_Dragon_Intro_01.prefab:336704b53c1d9ea48a9f0d6e1e6356d5", "VO_LOOTA_BOSS_44h_Female_Dragon_EmoteResponse_01.prefab:a710ab86ebc0c1040af7104fae088d07", "VO_LOOTA_BOSS_44h_Female_Dragon_Death_01.prefab:a94e33d6a82d725469a2255d2320090f", "VO_LOOTA_BOSS_44h_Female_Dragon_DefeatPlayer_01.prefab:66671023c97390245b92c577396d0975" })
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
		return "VO_LOOTA_BOSS_44h_Female_Dragon_Death_01.prefab:a94e33d6a82d725469a2255d2320090f";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_44h_Female_Dragon_Intro_01.prefab:336704b53c1d9ea48a9f0d6e1e6356d5", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_44h_Female_Dragon_EmoteResponse_01.prefab:a710ab86ebc0c1040af7104fae088d07", Notification.SpeechBubbleDirection.TopRight, actor));
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
