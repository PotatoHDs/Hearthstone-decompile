using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003C3 RID: 963
public class LOOT_Dungeon_BOSS_26h : LOOT_Dungeon
{
	// Token: 0x06003689 RID: 13961 RVA: 0x00115274 File Offset: 0x00113474
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_26h_Female_Demon_Intro_01.prefab:c703633103d735549b99de1f634199ca",
			"VO_LOOTA_BOSS_26h_Female_Demon_EmoteResponse_01.prefab:4e149016fdfa580488c77b1a96a3b591",
			"VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkFrost_01.prefab:993f89ae525d2d04aa9feafca8a81c28",
			"VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkDeath_01.prefab:6961bc705a859874693e7954322b011e",
			"VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkConfuse_01.prefab:2c97b65ba728edc4c987d06fcdbccca3",
			"VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkFear_01.prefab:7606ae781cbd88e49a460898e82c4ad7",
			"VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkFire_01.prefab:d5b30fd8422782d428f4a77e6384e808",
			"VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkDevitalize_01.prefab:0f45daf1f2aee9a489d3f5553bc440c1",
			"VO_LOOTA_BOSS_26h_Female_Demon_Death_01.prefab:bdf3d6fb4e212144b8d02428f5c09520",
			"VO_LOOTA_BOSS_26h_Female_Demon_DefeatPlayer_01.prefab:50208e375f2b0804b814051af470f108"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600368A RID: 13962 RVA: 0x001150F0 File Offset: 0x001132F0
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTFinalBoss);
	}

	// Token: 0x0600368B RID: 13963 RVA: 0x0011533C File Offset: 0x0011353C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600368C RID: 13964 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600368D RID: 13965 RVA: 0x00115352 File Offset: 0x00113552
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_26h_Female_Demon_Death_01.prefab:bdf3d6fb4e212144b8d02428f5c09520";
	}

	// Token: 0x0600368E RID: 13966 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600368F RID: 13967 RVA: 0x0011535C File Offset: 0x0011355C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_26h_Female_Demon_Intro_01.prefab:c703633103d735549b99de1f634199ca", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_26h_Female_Demon_EmoteResponse_01.prefab:4e149016fdfa580488c77b1a96a3b591", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003690 RID: 13968 RVA: 0x001153E3 File Offset: 0x001135E3
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
		case 102:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkFrost_01.prefab:993f89ae525d2d04aa9feafca8a81c28", 2.5f);
			break;
		case 103:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkDeath_01.prefab:6961bc705a859874693e7954322b011e", 2.5f);
			break;
		case 104:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkConfuse_01.prefab:2c97b65ba728edc4c987d06fcdbccca3", 2.5f);
			break;
		case 105:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkFear_01.prefab:7606ae781cbd88e49a460898e82c4ad7", 2.5f);
			break;
		case 106:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkFire_01.prefab:d5b30fd8422782d428f4a77e6384e808", 2.5f);
			break;
		case 107:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_26h_Female_Demon_EyeStalkDevitalize_01.prefab:0f45daf1f2aee9a489d3f5553bc440c1", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04001D3D RID: 7485
	private HashSet<string> m_playedLines = new HashSet<string>();
}
