using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_65h : GIL_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "GILA_BOSS_65h_Gobbles_Intro.prefab:2874743bc69515b4b8dc76500e19146b", "GILA_BOSS_65h_Gobbles_EmoteResponse.prefab:9650db10cb0078644b486f3f78dfd5e2", "GILA_BOSS_65h_Gobbles_Death.prefab:70d3c25a198efa5418b249f3d38e3ba4" })
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
		return "GILA_BOSS_65h_Gobbles_Death.prefab:70d3c25a198efa5418b249f3d38e3ba4";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("GILA_BOSS_65h_Gobbles_Intro.prefab:2874743bc69515b4b8dc76500e19146b", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("GILA_BOSS_65h_Gobbles_EmoteResponse.prefab:9650db10cb0078644b486f3f78dfd5e2", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
