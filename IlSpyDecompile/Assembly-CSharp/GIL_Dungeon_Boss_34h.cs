using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_34h : GIL_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_34h_Male_Demon_Intro_01.prefab:127d0d9c805d73b4798b6a933fd79e8d", "VO_GILA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:36cd563aa82e55f46a02e7a260706d03", "VO_GILA_BOSS_34h_Male_Demon_Death_01.prefab:a25d8865da590d64084a27bc4bde34e7", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_01.prefab:a37c2733cf24d564c87bb936bd319288", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_02.prefab:21ff8766f68381a43be28d779d6c968a", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_03.prefab:c0db8c5648389c14faa56642050455e4", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_04.prefab:ffdef008db8e7d14abe96b4c710c7798", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_05.prefab:f0e80111bde0e2344929324cc1708723" })
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_34h_Male_Demon_Intro_01.prefab:127d0d9c805d73b4798b6a933fd79e8d", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:36cd563aa82e55f46a02e7a260706d03", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_34h_Male_Demon_Death_01.prefab:a25d8865da590d64084a27bc4bde34e7";
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
		return new List<string> { "VO_GILA_BOSS_34h_Male_Demon_HeroPower_01.prefab:a37c2733cf24d564c87bb936bd319288", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_02.prefab:21ff8766f68381a43be28d779d6c968a", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_03.prefab:c0db8c5648389c14faa56642050455e4", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_04.prefab:ffdef008db8e7d14abe96b4c710c7798", "VO_GILA_BOSS_34h_Male_Demon_HeroPower_05.prefab:f0e80111bde0e2344929324cc1708723" };
	}
}
