using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003C1 RID: 961
public class LOOT_Dungeon_BOSS_24h : LOOT_Dungeon
{
	// Token: 0x06003677 RID: 13943 RVA: 0x00114E84 File Offset: 0x00113084
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			LOOT_Dungeon_BOSS_24h.TheMothergloop_LOOTA_BOSS_24h_Death,
			LOOT_Dungeon_BOSS_24h.TheMothergloop_LOOTA_BOSS_24h_EmoteResponse,
			LOOT_Dungeon_BOSS_24h.TheMothergloop_LOOTA_BOSS_24h_Intro
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003678 RID: 13944 RVA: 0x00114F0C File Offset: 0x0011310C
	protected override string GetBossDeathLine()
	{
		return LOOT_Dungeon_BOSS_24h.TheMothergloop_LOOTA_BOSS_24h_Death;
	}

	// Token: 0x06003679 RID: 13945 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600367A RID: 13946 RVA: 0x00114F18 File Offset: 0x00113118
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_24h.TheMothergloop_LOOTA_BOSS_24h_Intro, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_24h.TheMothergloop_LOOTA_BOSS_24h_EmoteResponse, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600367B RID: 13947 RVA: 0x00114FA9 File Offset: 0x001131A9
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D38 RID: 7480
	private static readonly AssetReference TheMothergloop_LOOTA_BOSS_24h_Death = new AssetReference("TheMothergloop_LOOTA_BOSS_24h_Death.prefab:b6895bc5ad7734ebd92fbaa3c2f85743");

	// Token: 0x04001D39 RID: 7481
	private static readonly AssetReference TheMothergloop_LOOTA_BOSS_24h_EmoteResponse = new AssetReference("TheMothergloop_LOOTA_BOSS_24h_EmoteResponse.prefab:4ca93dcbf4ae74178bf3f5e74d667045");

	// Token: 0x04001D3A RID: 7482
	private static readonly AssetReference TheMothergloop_LOOTA_BOSS_24h_Intro = new AssetReference("TheMothergloop_LOOTA_BOSS_24h_Intro.prefab:b2cb09bd0fcd84227848236790d3d9ae");
}
