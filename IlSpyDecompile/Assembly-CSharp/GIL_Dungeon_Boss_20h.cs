using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_20h : GIL_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_20h_MangyWolf_Intro.prefab:a2fc1112ddf13f341aa993e782e2a20b", "VO_GILA_BOSS_20h_MangyWolf_Emote.prefab:a4a8067fb5363474b939750a725cd8d4", "VO_GILA_BOSS_20h_MangyWolf_Death.prefab:24b687c08c113c041b65a387fe95c1cb" })
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
		return "VO_GILA_BOSS_20h_MangyWolf_Death.prefab:24b687c08c113c041b65a387fe95c1cb";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_20h_MangyWolf_Intro.prefab:a2fc1112ddf13f341aa993e782e2a20b", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_20h_MangyWolf_Emote.prefab:a4a8067fb5363474b939750a725cd8d4", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
