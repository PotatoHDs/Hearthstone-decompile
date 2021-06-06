using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003BB RID: 955
public class LOOT_Dungeon_BOSS_18h : LOOT_Dungeon
{
	// Token: 0x06003647 RID: 13895 RVA: 0x001144BC File Offset: 0x001126BC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"GiantRat_LOOTA_BOSS_18h_Intro.prefab:6768118374ac44e46851b6f52a8891d3",
			"GiantRat_LOOTA_BOSS_18h_EmoteResponse.prefab:323ab0c47034e8043b688bb368fa912c",
			"GiantRat_LOOTA_BOSS_18h_Death.prefab:73d22864d9f5c5846ba48c8c08febd79"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003648 RID: 13896 RVA: 0x00114538 File Offset: 0x00112738
	protected override string GetBossDeathLine()
	{
		return "GiantRat_LOOTA_BOSS_18h_Death.prefab:73d22864d9f5c5846ba48c8c08febd79";
	}

	// Token: 0x06003649 RID: 13897 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600364A RID: 13898 RVA: 0x00114540 File Offset: 0x00112740
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("GiantRat_LOOTA_BOSS_18h_Intro.prefab:6768118374ac44e46851b6f52a8891d3", Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("GiantRat_LOOTA_BOSS_18h_EmoteResponse.prefab:323ab0c47034e8043b688bb368fa912c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600364B RID: 13899 RVA: 0x001145C7 File Offset: 0x001127C7
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
