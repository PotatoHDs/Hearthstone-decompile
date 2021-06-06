using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003BE RID: 958
public class LOOT_Dungeon_BOSS_21h : LOOT_Dungeon
{
	// Token: 0x0600365E RID: 13918 RVA: 0x0011496C File Offset: 0x00112B6C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			LOOT_Dungeon_BOSS_21h.LOOTA_BOSS_21h_GnoshTheGreatWorm_Death,
			LOOT_Dungeon_BOSS_21h.LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro,
			LOOT_Dungeon_BOSS_21h.LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600365F RID: 13919 RVA: 0x001149F4 File Offset: 0x00112BF4
	protected override string GetBossDeathLine()
	{
		return LOOT_Dungeon_BOSS_21h.LOOTA_BOSS_21h_GnoshTheGreatWorm_Death;
	}

	// Token: 0x06003660 RID: 13920 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003661 RID: 13921 RVA: 0x00114A00 File Offset: 0x00112C00
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_21h.LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_21h.LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003662 RID: 13922 RVA: 0x00114A91 File Offset: 0x00112C91
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D32 RID: 7474
	private static readonly AssetReference LOOTA_BOSS_21h_GnoshTheGreatWorm_Death = new AssetReference("LOOTA_BOSS_21h_GnoshTheGreatWorm_Death.prefab:96326178d2fdd5c42bc4f6f05961c93b");

	// Token: 0x04001D33 RID: 7475
	private static readonly AssetReference LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro = new AssetReference("LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro.prefab:e398752e1baa4c74c9a7782056589c86");

	// Token: 0x04001D34 RID: 7476
	private static readonly AssetReference LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse = new AssetReference("LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse.prefab:fba69559e1920b4418245a9a1ef99130");
}
