using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003D7 RID: 983
public class LOOT_Dungeon_BOSS_52h : LOOT_Dungeon
{
	// Token: 0x0600373C RID: 14140 RVA: 0x00117754 File Offset: 0x00115954
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			LOOT_Dungeon_BOSS_52h.LOOTA_BOSS_52h_TreasureVault_Death,
			LOOT_Dungeon_BOSS_52h.LOOTA_BOSS_52h_TreasureVault_Emote,
			LOOT_Dungeon_BOSS_52h.LOOTA_BOSS_52h_TreasureVault_Intro
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600373D RID: 14141 RVA: 0x001177DC File Offset: 0x001159DC
	protected override string GetBossDeathLine()
	{
		return LOOT_Dungeon_BOSS_52h.LOOTA_BOSS_52h_TreasureVault_Death;
	}

	// Token: 0x0600373E RID: 14142 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600373F RID: 14143 RVA: 0x001177E8 File Offset: 0x001159E8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_52h.LOOTA_BOSS_52h_TreasureVault_Intro, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_52h.LOOTA_BOSS_52h_TreasureVault_Emote, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003740 RID: 14144 RVA: 0x00117879 File Offset: 0x00115A79
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D5B RID: 7515
	private static readonly AssetReference LOOTA_BOSS_52h_TreasureVault_Death = new AssetReference("LOOTA_BOSS_52h_TreasureVault_Death.prefab:b6852fd41796e6649b95bbfca14a45e4");

	// Token: 0x04001D5C RID: 7516
	private static readonly AssetReference LOOTA_BOSS_52h_TreasureVault_Emote = new AssetReference("LOOTA_BOSS_52h_TreasureVault_Emote.prefab:0248c411691a18a4f88409445f837035");

	// Token: 0x04001D5D RID: 7517
	private static readonly AssetReference LOOTA_BOSS_52h_TreasureVault_Intro = new AssetReference("LOOTA_BOSS_52h_TreasureVault_Intro.prefab:dd6522622d543b742a2766c78d14f3e3");
}
