using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003D3 RID: 979
public class LOOT_Dungeon_BOSS_48h : LOOT_Dungeon
{
	// Token: 0x06003716 RID: 14102 RVA: 0x00116D5C File Offset: 0x00114F5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			LOOT_Dungeon_BOSS_48h.TrappedRoom_LOOTA_BOSS_48h_Death,
			LOOT_Dungeon_BOSS_48h.TrappedRoom_LOOTA_BOSS_48h_EmoteResponse,
			LOOT_Dungeon_BOSS_48h.TrappedRoom_LOOTA_BOSS_48h_Intro
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003717 RID: 14103 RVA: 0x00116DE4 File Offset: 0x00114FE4
	protected override string GetBossDeathLine()
	{
		return LOOT_Dungeon_BOSS_48h.TrappedRoom_LOOTA_BOSS_48h_Death;
	}

	// Token: 0x06003718 RID: 14104 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003719 RID: 14105 RVA: 0x00116DF0 File Offset: 0x00114FF0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_48h.TrappedRoom_LOOTA_BOSS_48h_Intro, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_48h.TrappedRoom_LOOTA_BOSS_48h_EmoteResponse, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600371A RID: 14106 RVA: 0x00116E81 File Offset: 0x00115081
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D52 RID: 7506
	private static readonly AssetReference TrappedRoom_LOOTA_BOSS_48h_Death = new AssetReference("TrappedRoom_LOOTA_BOSS_48h_Death.prefab:a6c6e15236bcc405aafc279d56f13a3d");

	// Token: 0x04001D53 RID: 7507
	private static readonly AssetReference TrappedRoom_LOOTA_BOSS_48h_EmoteResponse = new AssetReference("TrappedRoom_LOOTA_BOSS_48h_EmoteResponse.prefab:36afbe32da4e24850860d944519508bc");

	// Token: 0x04001D54 RID: 7508
	private static readonly AssetReference TrappedRoom_LOOTA_BOSS_48h_Intro = new AssetReference("TrappedRoom_LOOTA_BOSS_48h_Intro.prefab:4d478c1f12dc2411d89e3f3b85fbcd85");
}
