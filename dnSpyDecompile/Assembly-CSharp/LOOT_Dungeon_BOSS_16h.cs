using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B9 RID: 953
public class LOOT_Dungeon_BOSS_16h : LOOT_Dungeon
{
	// Token: 0x06003634 RID: 13876 RVA: 0x00114108 File Offset: 0x00112308
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_16h_Male_Troll_Intro_01.prefab:d18ce221a61804e47ad506f74a5ca73d",
			"VO_LOOTA_BOSS_16h_Male_Troll_EmoteResponse_01.prefab:aa8a544cd3c2f6943aeb6938349ed538",
			"VO_LOOTA_BOSS_16h_Male_Troll_Death_01.prefab:408bf2eabcb56f84abea5d2221843992",
			"VO_LOOTA_BOSS_16h_Male_Troll_DefeatPlayer_01.prefab:25af3e732ec6fb74494cb1ddd0fa94d7",
			"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle1_01.prefab:395d36da235b4fb4f9b0477207d50e82",
			"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle2_01.prefab:386aae58797d12f4a8ada77b12b5145c",
			"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle3_01.prefab:65fd8caf09425d445af043833cc3c5ba",
			"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle4_01.prefab:63ee8d5e7b8fd9046b82eb7a138354ee",
			"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle5_01.prefab:3a64b92250de54a42b4458b3e81ca574"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003635 RID: 13877 RVA: 0x001141C4 File Offset: 0x001123C4
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003636 RID: 13878 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003637 RID: 13879 RVA: 0x001141DA File Offset: 0x001123DA
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_16h_Male_Troll_Death_01.prefab:408bf2eabcb56f84abea5d2221843992";
	}

	// Token: 0x06003638 RID: 13880 RVA: 0x001141E4 File Offset: 0x001123E4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_16h_Male_Troll_Intro_01.prefab:d18ce221a61804e47ad506f74a5ca73d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_16h_Male_Troll_EmoteResponse_01.prefab:aa8a544cd3c2f6943aeb6938349ed538", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003639 RID: 13881 RVA: 0x0011426B File Offset: 0x0011246B
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
		if (entity.HasTag(GAME_TAG.DEATHRATTLE) && this.m_DeathrattleLines.Count != 0)
		{
			string randomLine = this.m_DeathrattleLines[UnityEngine.Random.Range(0, this.m_DeathrattleLines.Count)];
			yield return base.PlayLineOnlyOnce(actor, randomLine, 2.5f);
			this.m_DeathrattleLines.Remove(randomLine);
			yield return null;
			randomLine = null;
		}
		yield break;
	}

	// Token: 0x0600363A RID: 13882 RVA: 0x00114281 File Offset: 0x00112481
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D2C RID: 7468
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D2D RID: 7469
	private List<string> m_DeathrattleLines = new List<string>
	{
		"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle1_01.prefab:395d36da235b4fb4f9b0477207d50e82",
		"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle2_01.prefab:386aae58797d12f4a8ada77b12b5145c",
		"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle3_01.prefab:65fd8caf09425d445af043833cc3c5ba",
		"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle4_01.prefab:63ee8d5e7b8fd9046b82eb7a138354ee",
		"VO_LOOTA_BOSS_16h_Male_Troll_EventPlayerDeathrattle5_01.prefab:3a64b92250de54a42b4458b3e81ca574"
	};
}
