using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003CE RID: 974
public class LOOT_Dungeon_BOSS_43h : LOOT_Dungeon
{
	// Token: 0x060036EB RID: 14059 RVA: 0x0011657C File Offset: 0x0011477C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_43h_Female_Djinn_Intro_01.prefab:d807d173b15d7a349ac7633f4d6d49cb",
			"VO_LOOTA_BOSS_43h_Female_Djinn_EmoteResponse_01.prefab:b0b146264452748448f38ba8653c4152",
			"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility1_01.prefab:d67ddbe9d86c15448bbd704d6b1d9ea8",
			"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility2_01.prefab:dbf5167e9736f864791569eecc2d9ea4",
			"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility3_01.prefab:13705bf4d60d9344a9245bad79dda6fc",
			"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull1_01.prefab:49448716e2caa7848b6b36fdb510d857",
			"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull2_01.prefab:a25a5ea2ac1395747b5a0a99b5976974",
			"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull3_01.prefab:75e111a15c0fbc141b4c44c04972f981",
			"VO_LOOTA_BOSS_43h_Female_Djinn_Death_01.prefab:2947452bb2cf9a644983baf0e5d720e7",
			"VO_LOOTA_BOSS_43h_Female_Djinn_DefeatPlayer_01.prefab:13966eb897e19934bb20d494b873a945"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036EC RID: 14060 RVA: 0x00116644 File Offset: 0x00114844
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036ED RID: 14061 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060036EE RID: 14062 RVA: 0x0011665A File Offset: 0x0011485A
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_43h_Female_Djinn_Death_01.prefab:2947452bb2cf9a644983baf0e5d720e7";
	}

	// Token: 0x060036EF RID: 14063 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060036F0 RID: 14064 RVA: 0x00116664 File Offset: 0x00114864
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_43h_Female_Djinn_Intro_01.prefab:d807d173b15d7a349ac7633f4d6d49cb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_43h_Female_Djinn_EmoteResponse_01.prefab:b0b146264452748448f38ba8653c4152", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036F1 RID: 14065 RVA: 0x001166EB File Offset: 0x001148EB
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
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				int num = 20;
				int num2 = UnityEngine.Random.Range(0, 100);
				if (this.m_BurnedHeroLines.Count != 0 && num >= num2)
				{
					string randomLine = this.m_BurnedHeroLines[UnityEngine.Random.Range(0, this.m_BurnedHeroLines.Count)];
					yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
					this.m_BurnedHeroLines.Remove(randomLine);
					yield return null;
					randomLine = null;
				}
			}
		}
		else
		{
			int num = 30;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_NormalHeroLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_NormalHeroLines[UnityEngine.Random.Range(0, this.m_NormalHeroLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_NormalHeroLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
		}
		yield break;
	}

	// Token: 0x04001D4C RID: 7500
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D4D RID: 7501
	private List<string> m_NormalHeroLines = new List<string>
	{
		"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility1_01.prefab:d67ddbe9d86c15448bbd704d6b1d9ea8",
		"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility2_01.prefab:dbf5167e9736f864791569eecc2d9ea4",
		"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbility3_01.prefab:13705bf4d60d9344a9245bad79dda6fc"
	};

	// Token: 0x04001D4E RID: 7502
	private List<string> m_BurnedHeroLines = new List<string>
	{
		"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull1_01.prefab:49448716e2caa7848b6b36fdb510d857",
		"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull2_01.prefab:a25a5ea2ac1395747b5a0a99b5976974",
		"VO_LOOTA_BOSS_43h_Female_Djinn_HeroAbilityHandFull3_01.prefab:75e111a15c0fbc141b4c44c04972f981"
	};
}
