using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003B1 RID: 945
public class LOOT_Dungeon_BOSS_05h : LOOT_Dungeon
{
	// Token: 0x060035E9 RID: 13801 RVA: 0x001130B4 File Offset: 0x001112B4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_05h_Male_Kobold_Intro_01.prefab:e1c7c45689b6591498aa76348e5a70f4",
			"VO_LOOTA_BOSS_05h_Male_Kobold_EmoteResponse_01.prefab:2934b191f5c48084f8ca1946cd1c12af",
			"VO_LOOTA_BOSS_05h_Male_Kobold_Death_01.prefab:71eccd84e3ff7c548bbdf11518c4bd77",
			"VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower1_01.prefab:f658526e24d9d9c4a9e69ac192e1b6da",
			"VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower2_01.prefab:767c3f76ae6a5404c9453bee77f002df",
			"VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower3_01.prefab:753ada26bfe00f6419552fc39b17395b",
			"VO_LOOTA_BOSS_05h_Male_Kobold_DefeatPlayer_01.prefab:43c69f56c33d36d4f85a19f69e96f21f",
			"VO_LOOTA_BOSS_05h_Male_Kobold_EventBoomBots_01.prefab:3c88fbc01e9f5554282c392c82596c29",
			"VO_LOOTA_BOSS_05h_Male_Kobold_EventMadBomberDeath_01.prefab:cd159b7aa0612634ebd47d147e729157"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x00113170 File Offset: 0x00111370
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x00113186 File Offset: 0x00111386
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower1_01.prefab:f658526e24d9d9c4a9e69ac192e1b6da",
			"VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower2_01.prefab:767c3f76ae6a5404c9453bee77f002df",
			"VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower3_01.prefab:753ada26bfe00f6419552fc39b17395b"
		};
	}

	// Token: 0x060035EC RID: 13804 RVA: 0x001131AE File Offset: 0x001113AE
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_05h_Male_Kobold_Death_01.prefab:71eccd84e3ff7c548bbdf11518c4bd77";
	}

	// Token: 0x060035ED RID: 13805 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060035EE RID: 13806 RVA: 0x001131B8 File Offset: 0x001113B8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_05h_Male_Kobold_Intro_01.prefab:e1c7c45689b6591498aa76348e5a70f4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_05h_Male_Kobold_EmoteResponse_01.prefab:2934b191f5c48084f8ca1946cd1c12af", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060035EF RID: 13807 RVA: 0x0011323F File Offset: 0x0011143F
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
		if (missionEvent == 102)
		{
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_05h_Male_Kobold_EventMadBomberDeath_01.prefab:cd159b7aa0612634ebd47d147e729157", 2.5f);
		}
		yield break;
	}

	// Token: 0x060035F0 RID: 13808 RVA: 0x00113255 File Offset: 0x00111455
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
		if (cardId == "LOOTA_838")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_05h_Male_Kobold_EventBoomBots_01.prefab:3c88fbc01e9f5554282c392c82596c29", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D1F RID: 7455
	private HashSet<string> m_playedLines = new HashSet<string>();
}
