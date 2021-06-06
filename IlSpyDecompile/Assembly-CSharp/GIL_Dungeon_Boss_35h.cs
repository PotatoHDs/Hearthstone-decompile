using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_35h : GIL_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_35h_Male_Beast_Intro.prefab:7d5d462108277c54fa34c7419d6b7f34", "VO_GILA_BOSS_35h_Male_Beast_EmoteResponse.prefab:720896f4a73b2dd499faa041441413f6", "VO_GILA_BOSS_35h_Male_Beast_Death.prefab:040c07f77adc84e44b9526ec8f40c5b7" })
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
		return "VO_GILA_BOSS_35h_Male_Beast_Death.prefab:040c07f77adc84e44b9526ec8f40c5b7";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_35h_Male_Beast_Intro.prefab:7d5d462108277c54fa34c7419d6b7f34", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_35h_Male_Beast_EmoteResponse.prefab:720896f4a73b2dd499faa041441413f6", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
