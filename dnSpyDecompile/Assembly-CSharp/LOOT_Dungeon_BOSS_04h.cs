using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003B0 RID: 944
public class LOOT_Dungeon_BOSS_04h : LOOT_Dungeon
{
	// Token: 0x060035DF RID: 13791 RVA: 0x00112EB0 File Offset: 0x001110B0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_04h_Female_Kobold_Intro1_01.prefab:1d9348a07de8c03449e6341941f594d3",
			"VO_LOOTA_BOSS_04h_Female_Kobold_Intro2_01.prefab:d7f7f1554e369f84889996b2859d90cc",
			"VO_LOOTA_BOSS_04h_Female_Kobold_EmoteResponse_01.prefab:f0813696aafb10c42a8d9977c77fde0e",
			"VO_LOOTA_BOSS_04h_Female_Kobold_Death_01.prefab:987462ddce876de4e8a90e7ae25db3b7",
			"VO_LOOTA_BOSS_04h_Female_Kobold_DefeatPlayer_01.prefab:0fc54a8945a6f734eaacd5fbcaadc032",
			"VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOn_01.prefab:339d354892daf9c47bebb4a937270177",
			"VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOff_01.prefab:f1b8d85236e3980459605d65dc0f5dbf",
			"VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerPlay_01.prefab:a7c97e6d06c29c1499c3d3efc8d649aa",
			"VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerRespawn_01.prefab:a1c84563c379d1445b71bb85d63505ec"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060035E0 RID: 13792 RVA: 0x00112F6C File Offset: 0x0011116C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060035E1 RID: 13793 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060035E2 RID: 13794 RVA: 0x00112F82 File Offset: 0x00111182
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_04h_Female_Kobold_Death_01.prefab:987462ddce876de4e8a90e7ae25db3b7";
	}

	// Token: 0x060035E3 RID: 13795 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060035E4 RID: 13796 RVA: 0x00112F8C File Offset: 0x0011118C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
			if (cardId == "LOOTA_BOSS_04h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_04h_Female_Kobold_Intro2_01.prefab:d7f7f1554e369f84889996b2859d90cc", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			if (cardId == "LOOTA_BOSS_27h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_04h_Female_Kobold_Intro1_01.prefab:1d9348a07de8c03449e6341941f594d3", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_04h_Female_Kobold_EmoteResponse_01.prefab:f0813696aafb10c42a8d9977c77fde0e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x0011306C File Offset: 0x0011126C
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
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOn_01.prefab:339d354892daf9c47bebb4a937270177", 2.5f);
			break;
		case 103:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOff_01.prefab:f1b8d85236e3980459605d65dc0f5dbf", 2.5f);
			break;
		case 104:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerRespawn_01.prefab:a1c84563c379d1445b71bb85d63505ec", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x060035E6 RID: 13798 RVA: 0x00113082 File Offset: 0x00111282
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "LOOTA_840")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerPlay_01.prefab:a7c97e6d06c29c1499c3d3efc8d649aa", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D1E RID: 7454
	private HashSet<string> m_playedLines = new HashSet<string>();
}
