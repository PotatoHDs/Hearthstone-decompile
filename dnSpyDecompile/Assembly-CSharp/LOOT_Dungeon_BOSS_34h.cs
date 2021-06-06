using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003C5 RID: 965
public class LOOT_Dungeon_BOSS_34h : LOOT_Dungeon
{
	// Token: 0x0600369C RID: 13980 RVA: 0x001155B0 File Offset: 0x001137B0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_34h_Male_Demon_Intro_01.prefab:f920329aab2358b46acc4a4078ac1d47",
			"VO_LOOTA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:f6ab96c6db6844141868af6c3719748c",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower1_01.prefab:4050260455fd49949a326d2fd7a8de4e",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower2_01.prefab:2465f3f831f76ef4bacb23d22f75cfc3",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower3_01.prefab:3a4ee40cba1dc9b45b3e8def5bc46a6a",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower4_01.prefab:4cfa80ab6d8338e43a13d63b03d2e81d",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower5_01.prefab:94d02c154a8ee724b965c48dd11e29df",
			"VO_LOOTA_BOSS_34h_Male_Demon_Death_01.prefab:61c182fcaec41bb4e8f14fcc0f55e4f4",
			"VO_LOOTA_BOSS_34h_Male_Demon_DefeatPlayer_01.prefab:1640e62dc70ba9a4188e22d0bc32a2fa"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600369D RID: 13981 RVA: 0x001150F0 File Offset: 0x001132F0
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTFinalBoss);
	}

	// Token: 0x0600369E RID: 13982 RVA: 0x0011566C File Offset: 0x0011386C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600369F RID: 13983 RVA: 0x00115682 File Offset: 0x00113882
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower1_01.prefab:4050260455fd49949a326d2fd7a8de4e",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower2_01.prefab:2465f3f831f76ef4bacb23d22f75cfc3",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower3_01.prefab:3a4ee40cba1dc9b45b3e8def5bc46a6a",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower4_01.prefab:4cfa80ab6d8338e43a13d63b03d2e81d",
			"VO_LOOTA_BOSS_34h_Male_Demon_HeroPower5_01.prefab:94d02c154a8ee724b965c48dd11e29df"
		};
	}

	// Token: 0x060036A0 RID: 13984 RVA: 0x001156C0 File Offset: 0x001138C0
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_34h_Male_Demon_Death_01.prefab:61c182fcaec41bb4e8f14fcc0f55e4f4";
	}

	// Token: 0x060036A1 RID: 13985 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060036A2 RID: 13986 RVA: 0x001156C8 File Offset: 0x001138C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_34h_Male_Demon_Intro_01.prefab:f920329aab2358b46acc4a4078ac1d47", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:f6ab96c6db6844141868af6c3719748c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036A3 RID: 13987 RVA: 0x0011574F File Offset: 0x0011394F
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
