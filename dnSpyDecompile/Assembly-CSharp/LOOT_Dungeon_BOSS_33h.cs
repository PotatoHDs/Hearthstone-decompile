using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C4 RID: 964
public class LOOT_Dungeon_BOSS_33h : LOOT_Dungeon
{
	// Token: 0x06003693 RID: 13971 RVA: 0x0011540C File Offset: 0x0011360C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_19h_Male_Trogg_Intro_01.prefab:00df9f15d69d8ce4e8553e579e3ff728",
			"VO_LOOTA_BOSS_19h_Male_Trogg_EmoteResponse_01.prefab:4385254ca60d5d64eb86d9341372d69f",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard1_01.prefab:dfd28e8d857457e44a1bedce379ee0b1",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard2_01.prefab:90a3fc7d66f2c1f41a7322f56d3aad21",
			"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard3_01.prefab:68a1fb41b7d14e446bebc1489278086b",
			"VO_LOOTA_BOSS_19h_Male_Trogg_Death_01.prefab:d0fa743934bc7a24db09df3af3ce0b77",
			"VO_LOOTA_BOSS_19h_Male_Trogg_DefeatPlayer_01.prefab:0a0997eeb9130dc4382df8e2f6c23b2d",
			"VO_LOOTA_BOSS_19h_Male_Trogg_EventHandFull_01.prefab:0cf7309fe898f1b4cb9def67e388d19e"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003694 RID: 13972 RVA: 0x001154BC File Offset: 0x001136BC
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003695 RID: 13973 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003696 RID: 13974 RVA: 0x001146CE File Offset: 0x001128CE
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_19h_Male_Trogg_Death_01.prefab:d0fa743934bc7a24db09df3af3ce0b77";
	}

	// Token: 0x06003697 RID: 13975 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003698 RID: 13976 RVA: 0x001154D4 File Offset: 0x001136D4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_19h_Male_Trogg_Intro_01.prefab:00df9f15d69d8ce4e8553e579e3ff728", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_19h_Male_Trogg_EmoteResponse_01.prefab:4385254ca60d5d64eb86d9341372d69f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003699 RID: 13977 RVA: 0x0011555B File Offset: 0x0011375B
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
		if (missionEvent != 102)
		{
			if (missionEvent == 103)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_19h_Male_Trogg_EventHandFull_01.prefab:0cf7309fe898f1b4cb9def67e388d19e", 2.5f);
			}
		}
		else if (this.m_TriggerPowerLines.Count != 0)
		{
			string randomLine = this.m_TriggerPowerLines[UnityEngine.Random.Range(0, this.m_TriggerPowerLines.Count)];
			yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
			this.m_TriggerPowerLines.Remove(randomLine);
			randomLine = null;
		}
		yield break;
	}

	// Token: 0x04001D3E RID: 7486
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D3F RID: 7487
	private List<string> m_TriggerPowerLines = new List<string>
	{
		"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard1_01.prefab:dfd28e8d857457e44a1bedce379ee0b1",
		"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard2_01.prefab:90a3fc7d66f2c1f41a7322f56d3aad21",
		"VO_LOOTA_BOSS_19h_Male_Trogg_HeroPowerHard3_01.prefab:68a1fb41b7d14e446bebc1489278086b"
	};
}
