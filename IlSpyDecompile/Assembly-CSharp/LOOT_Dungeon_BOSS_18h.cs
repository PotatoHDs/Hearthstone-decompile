using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_18h : LOOT_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "GiantRat_LOOTA_BOSS_18h_Intro.prefab:6768118374ac44e46851b6f52a8891d3", "GiantRat_LOOTA_BOSS_18h_EmoteResponse.prefab:323ab0c47034e8043b688bb368fa912c", "GiantRat_LOOTA_BOSS_18h_Death.prefab:73d22864d9f5c5846ba48c8c08febd79" })
		{
			PreloadSound(item);
		}
	}

	protected override string GetBossDeathLine()
	{
		return "GiantRat_LOOTA_BOSS_18h_Death.prefab:73d22864d9f5c5846ba48c8c08febd79";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("GiantRat_LOOTA_BOSS_18h_Intro.prefab:6768118374ac44e46851b6f52a8891d3", Notification.SpeechBubbleDirection.None, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("GiantRat_LOOTA_BOSS_18h_EmoteResponse.prefab:323ab0c47034e8043b688bb368fa912c", Notification.SpeechBubbleDirection.TopRight, actor));
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
