using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_54h : GIL_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_54h_Male_Golem_Intro_01.prefab:a7eed47f4791e1043841fdd3da622045", "VO_GILA_BOSS_54h_Male_Golem_EmoteResponse_01.prefab:a5682fcdb8af9a746b62a25402984208", "VO_GILA_BOSS_54h_Male_Golem_Death_02.prefab:e817d284956060a4b9d8e8f9355ab6a4", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_01.prefab:e00ce3897c75afb418986400bddb0906", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_02.prefab:467ad5a3ac825aa459411cba89f0207c", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_03.prefab:b5093ba875e0dc042b07a95170ea15cb", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_04.prefab:dd023ae7f6667fa43af615512cc17d57", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_05.prefab:375e868645ac4ec409a980658f7ffb5e" })
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_54h_Male_Golem_Intro_01.prefab:a7eed47f4791e1043841fdd3da622045", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_54h_Male_Golem_EmoteResponse_01.prefab:a5682fcdb8af9a746b62a25402984208", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_54h_Male_Golem_Death_02.prefab:e817d284956060a4b9d8e8f9355ab6a4";
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
		return new List<string> { "VO_GILA_BOSS_54h_Male_Golem_HeroPower_01.prefab:e00ce3897c75afb418986400bddb0906", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_02.prefab:467ad5a3ac825aa459411cba89f0207c", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_03.prefab:b5093ba875e0dc042b07a95170ea15cb", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_04.prefab:dd023ae7f6667fa43af615512cc17d57", "VO_GILA_BOSS_54h_Male_Golem_HeroPower_05.prefab:375e868645ac4ec409a980658f7ffb5e" };
	}
}
