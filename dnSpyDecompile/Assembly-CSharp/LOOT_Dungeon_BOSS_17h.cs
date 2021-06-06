using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003BA RID: 954
public class LOOT_Dungeon_BOSS_17h : LOOT_Dungeon
{
	// Token: 0x0600363D RID: 13885 RVA: 0x001142F8 File Offset: 0x001124F8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_17h_Male_Troll_Intro_01.prefab:dc1c1e6dcdd83a54a8ff082a741b6d46",
			"VO_LOOTA_BOSS_17h_Male_Troll_EmoteResponse_01.prefab:5f2ffdacd3c4d974787263b2708ac237",
			"VO_LOOTA_BOSS_17h_Male_Troll_Death_01.prefab:5cc9961752e42494fa049e753a79ec20",
			"VO_LOOTA_BOSS_17h_Male_Troll_DefeatPlayer_01.prefab:38b3cb0dcd08da041befcf72f892c3a1",
			"VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBrann_01.prefab:a2dcc57d60e941c4ca6d8045a8268671",
			"VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBattlecry1_01.prefab:e88d4148f01b87b49a0b3fab1270219e",
			"VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBattlecry2_01.prefab:5109f4d775235bb4c9d2736fe5d9860d",
			"VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBattlecry3_01.prefab:0a43c143114985640b7585405406eef2"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x001143A8 File Offset: 0x001125A8
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600363F RID: 13887 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003640 RID: 13888 RVA: 0x001143BE File Offset: 0x001125BE
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_17h_Male_Troll_Death_01.prefab:5cc9961752e42494fa049e753a79ec20";
	}

	// Token: 0x06003641 RID: 13889 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003642 RID: 13890 RVA: 0x001143C8 File Offset: 0x001125C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_17h_Male_Troll_Intro_01.prefab:dc1c1e6dcdd83a54a8ff082a741b6d46", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_17h_Male_Troll_EmoteResponse_01.prefab:5f2ffdacd3c4d974787263b2708ac237", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003643 RID: 13891 RVA: 0x0011444F File Offset: 0x0011264F
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "LOE_077")
		{
			yield return base.PlayEasterEggLine(enemyActor, "VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBrann_01.prefab:a2dcc57d60e941c4ca6d8045a8268671", 2.5f);
		}
		if (entity.HasTag(GAME_TAG.BATTLECRY) && this.m_BattlecryLines.Count != 0)
		{
			string randomLine = this.m_BattlecryLines[UnityEngine.Random.Range(0, this.m_BattlecryLines.Count)];
			yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
			this.m_BattlecryLines.Remove(randomLine);
			yield return null;
			randomLine = null;
		}
		yield break;
	}

	// Token: 0x06003644 RID: 13892 RVA: 0x00114465 File Offset: 0x00112665
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D2E RID: 7470
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D2F RID: 7471
	private List<string> m_BattlecryLines = new List<string>
	{
		"VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBattlecry1_01.prefab:e88d4148f01b87b49a0b3fab1270219e",
		"VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBattlecry2_01.prefab:5109f4d775235bb4c9d2736fe5d9860d",
		"VO_LOOTA_BOSS_17h_Male_Troll_EventPlayerBattlecry3_01.prefab:0a43c143114985640b7585405406eef2"
	};
}
