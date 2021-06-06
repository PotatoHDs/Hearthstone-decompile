using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003B7 RID: 951
public class LOOT_Dungeon_BOSS_13h : LOOT_Dungeon
{
	// Token: 0x06003621 RID: 13857 RVA: 0x00113C60 File Offset: 0x00111E60
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_13h_Female_Undead_EmoteResponse_01.prefab:99ea4fd615762c644b3ae60af1e4b243",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower_01.prefab:e8b84e5da14b8ed47b94319676eeefcb",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower1_01.prefab:26727f391e0a1b14dbe6001643171ba1",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower2_01.prefab:50b8e0d1b1009dd4298db0f84c345d41",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower3_01.prefab:df9624ba08ec5674c93d63a2c8fa1d5e",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower4_01.prefab:94843eb0d7581ef4cb38e5470e8ee0bf",
			"VO_LOOTA_BOSS_13h_Female_Undead_Death_01.prefab:f8adf94220a0f154c9e4ba5167b1d1d8",
			"VO_LOOTA_BOSS_13h_Female_Undead_DefeatPlayer_01.prefab:e9c4e87f8ad202148a3a94104bf0908c",
			"VO_LOOTA_BOSS_13h_Female_Undead_EventStealQuest_01.prefab:8c513562bebfa8f4986eafc9f6d224de",
			"VO_LOOTA_BOSS_13h_Female_Undead_EventBagOfCoins_01.prefab:1b354b2b9661b3b439da138ba82f1f68"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003622 RID: 13858 RVA: 0x00113D28 File Offset: 0x00111F28
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003623 RID: 13859 RVA: 0x00113D3E File Offset: 0x00111F3E
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower_01.prefab:e8b84e5da14b8ed47b94319676eeefcb",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower1_01.prefab:26727f391e0a1b14dbe6001643171ba1",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower2_01.prefab:50b8e0d1b1009dd4298db0f84c345d41",
			"VO_LOOTA_BOSS_13h_Female_Undead_HeroPower4_01.prefab:94843eb0d7581ef4cb38e5470e8ee0bf"
		};
	}

	// Token: 0x06003624 RID: 13860 RVA: 0x00113D71 File Offset: 0x00111F71
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_13h_Female_Undead_Death_01.prefab:f8adf94220a0f154c9e4ba5167b1d1d8";
	}

	// Token: 0x06003625 RID: 13861 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003626 RID: 13862 RVA: 0x00113D78 File Offset: 0x00111F78
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_13h_Female_Undead_HeroPower3_01.prefab:df9624ba08ec5674c93d63a2c8fa1d5e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_13h_Female_Undead_EmoteResponse_01.prefab:99ea4fd615762c644b3ae60af1e4b243", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003627 RID: 13863 RVA: 0x00113DFF File Offset: 0x00111FFF
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
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_13h_Female_Undead_EventStealQuest_01.prefab:8c513562bebfa8f4986eafc9f6d224de", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003628 RID: 13864 RVA: 0x00113E15 File Offset: 0x00112015
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
		if (cardId == "LOOTA_836")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_13h_Female_Undead_EventBagOfCoins_01.prefab:1b354b2b9661b3b439da138ba82f1f68", 2.5f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001D28 RID: 7464
	private HashSet<string> m_playedLines = new HashSet<string>();
}
