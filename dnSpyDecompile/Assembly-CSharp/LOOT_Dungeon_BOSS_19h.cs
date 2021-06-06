using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003BC RID: 956
public class LOOT_Dungeon_BOSS_19h : LOOT_Dungeon
{
	// Token: 0x0600364D RID: 13901 RVA: 0x001145E8 File Offset: 0x001127E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_19h_Male_Trogg_Intro_01.prefab:00df9f15d69d8ce4e8553e579e3ff728",
			"VO_LOOTA_BOSS_19h_Male_Trogg_EmoteResponse_01.prefab:4385254ca60d5d64eb86d9341372d69f",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPower1_01.prefab:5329b61147fc6f94a849ed713935994d",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPower2_01.prefab:a0a22baa0aa03b54b8b9da60451603fa",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPower3_01.prefab:59c9152915ad0d246a3033403363ab4c",
			"VO_LOOTA_BOSS_19h_Male_Trogg_Death_01.prefab:d0fa743934bc7a24db09df3af3ce0b77",
			"VO_LOOTA_BOSS_19h_Male_Trogg_DefeatPlayer_01.prefab:0a0997eeb9130dc4382df8e2f6c23b2d"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600364E RID: 13902 RVA: 0x00114690 File Offset: 0x00112890
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600364F RID: 13903 RVA: 0x001146A6 File Offset: 0x001128A6
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPower1_01.prefab:5329b61147fc6f94a849ed713935994d",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPower2_01.prefab:a0a22baa0aa03b54b8b9da60451603fa",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPower3_01.prefab:59c9152915ad0d246a3033403363ab4c"
		};
	}

	// Token: 0x06003650 RID: 13904 RVA: 0x001146CE File Offset: 0x001128CE
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_19h_Male_Trogg_Death_01.prefab:d0fa743934bc7a24db09df3af3ce0b77";
	}

	// Token: 0x06003651 RID: 13905 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003652 RID: 13906 RVA: 0x001146D8 File Offset: 0x001128D8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_19h_Male_Trogg_Intro_01.prefab:00df9f15d69d8ce4e8553e579e3ff728", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_19h_Male_Trogg_EmoteResponse_01.prefab:4385254ca60d5d64eb86d9341372d69f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003653 RID: 13907 RVA: 0x0011475F File Offset: 0x0011295F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}
}
