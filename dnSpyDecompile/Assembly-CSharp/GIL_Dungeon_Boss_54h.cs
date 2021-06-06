using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003FE RID: 1022
public class GIL_Dungeon_Boss_54h : GIL_Dungeon
{
	// Token: 0x060038A4 RID: 14500 RVA: 0x0011D804 File Offset: 0x0011BA04
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_54h_Male_Golem_Intro_01.prefab:a7eed47f4791e1043841fdd3da622045",
			"VO_GILA_BOSS_54h_Male_Golem_EmoteResponse_01.prefab:a5682fcdb8af9a746b62a25402984208",
			"VO_GILA_BOSS_54h_Male_Golem_Death_02.prefab:e817d284956060a4b9d8e8f9355ab6a4",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_01.prefab:e00ce3897c75afb418986400bddb0906",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_02.prefab:467ad5a3ac825aa459411cba89f0207c",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_03.prefab:b5093ba875e0dc042b07a95170ea15cb",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_04.prefab:dd023ae7f6667fa43af615512cc17d57",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_05.prefab:375e868645ac4ec409a980658f7ffb5e"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038A5 RID: 14501 RVA: 0x0011D8B4 File Offset: 0x0011BAB4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_54h_Male_Golem_Intro_01.prefab:a7eed47f4791e1043841fdd3da622045", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_54h_Male_Golem_EmoteResponse_01.prefab:a5682fcdb8af9a746b62a25402984208", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038A6 RID: 14502 RVA: 0x0011D93B File Offset: 0x0011BB3B
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_54h_Male_Golem_Death_02.prefab:e817d284956060a4b9d8e8f9355ab6a4";
	}

	// Token: 0x060038A7 RID: 14503 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060038A8 RID: 14504 RVA: 0x0011D942 File Offset: 0x0011BB42
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060038A9 RID: 14505 RVA: 0x0011D958 File Offset: 0x0011BB58
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_01.prefab:e00ce3897c75afb418986400bddb0906",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_02.prefab:467ad5a3ac825aa459411cba89f0207c",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_03.prefab:b5093ba875e0dc042b07a95170ea15cb",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_04.prefab:dd023ae7f6667fa43af615512cc17d57",
			"VO_GILA_BOSS_54h_Male_Golem_HeroPower_05.prefab:375e868645ac4ec409a980658f7ffb5e"
		};
	}
}
