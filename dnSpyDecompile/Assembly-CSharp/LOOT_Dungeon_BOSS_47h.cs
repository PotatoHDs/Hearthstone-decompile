using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003D2 RID: 978
public class LOOT_Dungeon_BOSS_47h : LOOT_Dungeon
{
	// Token: 0x0600370F RID: 14095 RVA: 0x00116BF0 File Offset: 0x00114DF0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			LOOT_Dungeon_BOSS_47h.LOOTA_BOSS_47h_Lava_FilledRoom_Death,
			LOOT_Dungeon_BOSS_47h.LOOTA_BOSS_47h_Lava_FilledRoom_Emote,
			LOOT_Dungeon_BOSS_47h.LOOTA_BOSS_47h_Lava_FilledRoom_Intro
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003710 RID: 14096 RVA: 0x00116C78 File Offset: 0x00114E78
	protected override string GetBossDeathLine()
	{
		return LOOT_Dungeon_BOSS_47h.LOOTA_BOSS_47h_Lava_FilledRoom_Death;
	}

	// Token: 0x06003711 RID: 14097 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003712 RID: 14098 RVA: 0x00116C84 File Offset: 0x00114E84
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_47h.LOOTA_BOSS_47h_Lava_FilledRoom_Intro, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_47h.LOOTA_BOSS_47h_Lava_FilledRoom_Emote, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003713 RID: 14099 RVA: 0x00116D15 File Offset: 0x00114F15
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D4F RID: 7503
	private static readonly AssetReference LOOTA_BOSS_47h_Lava_FilledRoom_Death = new AssetReference("LOOTA_BOSS_47h_Lava_FilledRoom_Death.prefab:2753d2ebd9bd40b458a33c552832df00");

	// Token: 0x04001D50 RID: 7504
	private static readonly AssetReference LOOTA_BOSS_47h_Lava_FilledRoom_Emote = new AssetReference("LOOTA_BOSS_47h_Lava_FilledRoom_Emote.prefab:ceb20cb5ceec4a549886c63442ac8b93");

	// Token: 0x04001D51 RID: 7505
	private static readonly AssetReference LOOTA_BOSS_47h_Lava_FilledRoom_Intro = new AssetReference("LOOTA_BOSS_47h_Lava_FilledRoom_Intro.prefab:79a4d695d025efc4a82d056a338932e5");
}
