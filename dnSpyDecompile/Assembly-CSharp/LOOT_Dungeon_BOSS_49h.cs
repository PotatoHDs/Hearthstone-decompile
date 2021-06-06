using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003D4 RID: 980
public class LOOT_Dungeon_BOSS_49h : LOOT_Dungeon
{
	// Token: 0x0600371D RID: 14109 RVA: 0x00116EC8 File Offset: 0x001150C8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_49h_Male_Elemental_Intro_01.prefab:ac8ed0481b700e54ea96b415b7254a29",
			"VO_LOOTA_BOSS_49h_Male_Elemental_EmoteResponse_01.prefab:fea117c2a4e020b4d8db820b9a991c51",
			"VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower1_01.prefab:cf576d8d32edf2a4899d11c634d41c01",
			"VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower2_01.prefab:d1dd68ead56129547a000d910a498cac",
			"VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower3_01.prefab:ffda1be6d974ba747aa87df0dfc83174",
			"VO_LOOTA_BOSS_49h_Male_Elemental_Death_01.prefab:4acf15c5d3b4a8347bec1d0959a526bf",
			"VO_LOOTA_BOSS_49h_Male_Elemental_DefeatPlayer_01.prefab:8e0e0ccbc81c5e740b6463e6edf7c364"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600371E RID: 14110 RVA: 0x001150F0 File Offset: 0x001132F0
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTFinalBoss);
	}

	// Token: 0x0600371F RID: 14111 RVA: 0x00116F70 File Offset: 0x00115170
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003720 RID: 14112 RVA: 0x00116F86 File Offset: 0x00115186
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower1_01.prefab:cf576d8d32edf2a4899d11c634d41c01",
			"VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower2_01.prefab:d1dd68ead56129547a000d910a498cac",
			"VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower3_01.prefab:ffda1be6d974ba747aa87df0dfc83174"
		};
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x00116FAE File Offset: 0x001151AE
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_49h_Male_Elemental_Death_01.prefab:4acf15c5d3b4a8347bec1d0959a526bf";
	}

	// Token: 0x06003722 RID: 14114 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003723 RID: 14115 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06003724 RID: 14116 RVA: 0x00116FB8 File Offset: 0x001151B8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_49h_Male_Elemental_Intro_01.prefab:ac8ed0481b700e54ea96b415b7254a29", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_49h_Male_Elemental_EmoteResponse_01.prefab:fea117c2a4e020b4d8db820b9a991c51", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003725 RID: 14117 RVA: 0x0011703F File Offset: 0x0011523F
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
