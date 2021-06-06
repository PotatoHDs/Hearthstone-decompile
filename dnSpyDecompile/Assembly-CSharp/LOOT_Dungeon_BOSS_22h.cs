using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003BF RID: 959
public class LOOT_Dungeon_BOSS_22h : LOOT_Dungeon
{
	// Token: 0x06003665 RID: 13925 RVA: 0x00114AD8 File Offset: 0x00112CD8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_22h_Male_Murloc_Intro_01.prefab:0d178c00638692b4093f9b7b0511d3b5",
			"VO_LOOTA_BOSS_22h_Male_Murloc_EmoteResponse_01.prefab:c9544ca612ac6144098259cac8d014bc",
			"VO_LOOTA_BOSS_22h_Male_Murloc_FishSmall_01.prefab:60c1d4374ab18bc419b47e6212c2d69e",
			"VO_LOOTA_BOSS_22h_Male_Murloc_FishMedium_01.prefab:e496cf9664f2b334b9a9511929144f56",
			"VO_LOOTA_BOSS_22h_Male_Murloc_FishLarge_02.prefab:dced70262b5d79f4095264bb1d74814c",
			"VO_LOOTA_BOSS_22h_Male_Murloc_Death_01.prefab:dab13998aedc64649afc23ada85aae06",
			"VO_LOOTA_BOSS_22h_Male_Murloc_DeathSpecial_01.prefab:4bc5951e1876f704693c475e0689419e",
			"VO_LOOTA_BOSS_22h_Male_Murloc_DefeatPlayer_01.prefab:7ceef4781a175854d921248187b25d63",
			"VO_LOOTA_BOSS_22h_Male_Murloc_EventSummonCrab_01.prefab:ed45607f96ac8014fbb5e1d42e94ce58",
			"VO_LOOTA_BOSS_22h_Male_Murloc_EventFishUpChest_01.prefab:4383c94c10459fb46b77dde1b4b0341a"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003666 RID: 13926 RVA: 0x00114BA0 File Offset: 0x00112DA0
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003667 RID: 13927 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003668 RID: 13928 RVA: 0x00114BB6 File Offset: 0x00112DB6
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_22h_Male_Murloc_Death_01.prefab:dab13998aedc64649afc23ada85aae06";
	}

	// Token: 0x06003669 RID: 13929 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600366A RID: 13930 RVA: 0x00114BC0 File Offset: 0x00112DC0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_22h_Male_Murloc_Intro_01.prefab:0d178c00638692b4093f9b7b0511d3b5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_22h_Male_Murloc_EmoteResponse_01.prefab:c9544ca612ac6144098259cac8d014bc", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600366B RID: 13931 RVA: 0x00114C47 File Offset: 0x00112E47
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_FishSmall_01.prefab:60c1d4374ab18bc419b47e6212c2d69e", 2.5f);
			yield return null;
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_FishMedium_01.prefab:e496cf9664f2b334b9a9511929144f56", 2.5f);
			yield return null;
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_FishLarge_02.prefab:dced70262b5d79f4095264bb1d74814c", 2.5f);
			yield return null;
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_EventSummonCrab_01.prefab:ed45607f96ac8014fbb5e1d42e94ce58", 2.5f);
			yield return null;
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_EventFishUpChest_01.prefab:4383c94c10459fb46b77dde1b4b0341a", 2.5f);
			yield return null;
			break;
		}
		yield break;
	}

	// Token: 0x04001D35 RID: 7477
	private HashSet<string> m_playedLines = new HashSet<string>();
}
