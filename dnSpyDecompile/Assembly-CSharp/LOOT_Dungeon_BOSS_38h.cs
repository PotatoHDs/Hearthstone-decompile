using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C9 RID: 969
public class LOOT_Dungeon_BOSS_38h : LOOT_Dungeon
{
	// Token: 0x060036BE RID: 14014 RVA: 0x00115C9C File Offset: 0x00113E9C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_38h_Male_Kobold_Intro_01.prefab:4949580433bcd1e48bd351d8ff68cfba",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EmoteResponse_01.prefab:c7c2f50bb6da6bd4b89ad39673ae5b86",
			"VO_LOOTA_BOSS_38h_Male_Kobold_Death_01.prefab:773c7f8ebca84d9458c47a951283da8b",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EventBrann_01.prefab:8529209f43f99c44dbd9227432602ee1",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EventRivendare_01.prefab:1555767ca07a51840aada9900666633d",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerBattlecries1_01.prefab:7f36a8303b69b7942a3b4b65165bef87",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerBattlecries2_01.prefab:4bdd4e42d0330a74b807778043a0b531",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerBattlecries3_01.prefab:36764c0f7268d0144b813095fb699cdf",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerDeathrattles1_01.prefab:5876e2505aebd13499f22af506e2fe5e",
			"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerDeathrattles3_01.prefab:28b4a262be6ba4a4b93267be10fb3110"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036BF RID: 14015 RVA: 0x00115D64 File Offset: 0x00113F64
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036C0 RID: 14016 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060036C1 RID: 14017 RVA: 0x00115D7A File Offset: 0x00113F7A
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_38h_Male_Kobold_Death_01.prefab:773c7f8ebca84d9458c47a951283da8b";
	}

	// Token: 0x060036C2 RID: 14018 RVA: 0x00115D84 File Offset: 0x00113F84
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_38h_Male_Kobold_Intro_01.prefab:4949580433bcd1e48bd351d8ff68cfba", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_38h_Male_Kobold_EmoteResponse_01.prefab:c7c2f50bb6da6bd4b89ad39673ae5b86", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036C3 RID: 14019 RVA: 0x00115E0B File Offset: 0x0011400B
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
		if (!(cardId == "FP1_031"))
		{
			if (cardId == "LOE_077")
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_LOOTA_BOSS_38h_Male_Kobold_EventBrann_01.prefab:8529209f43f99c44dbd9227432602ee1", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(enemyActor, "VO_LOOTA_BOSS_38h_Male_Kobold_EventRivendare_01.prefab:1555767ca07a51840aada9900666633d", 2.5f);
		}
		int chanceVO = 50;
		int randomNum = UnityEngine.Random.Range(0, 100);
		if (entity.HasTag(GAME_TAG.BATTLECRY) && chanceVO >= randomNum && this.m_BattlecryLines.Count != 0)
		{
			string randomLine = this.m_BattlecryLines[UnityEngine.Random.Range(0, this.m_BattlecryLines.Count)];
			yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
			this.m_BattlecryLines.Remove(randomLine);
			yield return null;
			randomLine = null;
		}
		if (entity.HasTag(GAME_TAG.DEATHRATTLE) && chanceVO >= randomNum && this.m_DeathrattleLines.Count != 0)
		{
			string randomLine = this.m_DeathrattleLines[UnityEngine.Random.Range(0, this.m_DeathrattleLines.Count)];
			yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
			this.m_DeathrattleLines.Remove(randomLine);
			yield return null;
			randomLine = null;
		}
		yield break;
	}

	// Token: 0x060036C4 RID: 14020 RVA: 0x00115E21 File Offset: 0x00114021
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D46 RID: 7494
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D47 RID: 7495
	private List<string> m_DeathrattleLines = new List<string>
	{
		"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerDeathrattles1_01.prefab:5876e2505aebd13499f22af506e2fe5e",
		"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerDeathrattles3_01.prefab:28b4a262be6ba4a4b93267be10fb3110"
	};

	// Token: 0x04001D48 RID: 7496
	private List<string> m_BattlecryLines = new List<string>
	{
		"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerBattlecries1_01.prefab:7f36a8303b69b7942a3b4b65165bef87",
		"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerBattlecries2_01.prefab:4bdd4e42d0330a74b807778043a0b531",
		"VO_LOOTA_BOSS_38h_Male_Kobold_EventPlayerBattlecries3_01.prefab:36764c0f7268d0144b813095fb699cdf"
	};
}
