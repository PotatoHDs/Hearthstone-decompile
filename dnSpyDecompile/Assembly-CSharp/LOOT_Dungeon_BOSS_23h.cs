using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C0 RID: 960
public class LOOT_Dungeon_BOSS_23h : LOOT_Dungeon
{
	// Token: 0x0600366E RID: 13934 RVA: 0x00114C70 File Offset: 0x00112E70
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_23h_Male_Furbolg_Intro_01.prefab:7e40d7bc17378c6449ba9f7bb40ceab3",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_EmoteResponse_01.prefab:e0068889c8f178846b15af6cb3a2fd1c",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower1_01.prefab:ca312436a00a9314c980c11cddf73557",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower2_01.prefab:f83a809c6ad84e647aacb215a6926217",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower3_01.prefab:3b588633715c44c4dad2284bbea2ddab",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower4_01.prefab:e29ff2a37e9864946a2e8b930ae9b90f",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower5_01.prefab:17298eb378d0f294c8760e8c30e9769f",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_Death_01.prefab:b513972ec6ee8d04786ce0e62bdab6e8",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_DefeatPlayer_01.prefab:f31959a0e152f1d409b875dab20c5420",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_EventPlayBigMinion1_01.prefab:19760aa76d53f9145bcac8932d5ce169",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_EventPlayBigMinion2_01.prefab:06ee1bd2fdd4bcd449f2fb73992d59ae",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_EventPlayBigMinion3_01.prefab:d20754ed0a8c3964ba30db1b14144c14"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600366F RID: 13935 RVA: 0x00114D4C File Offset: 0x00112F4C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003670 RID: 13936 RVA: 0x00114D62 File Offset: 0x00112F62
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower1_01.prefab:ca312436a00a9314c980c11cddf73557",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower2_01.prefab:f83a809c6ad84e647aacb215a6926217",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower3_01.prefab:3b588633715c44c4dad2284bbea2ddab",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower4_01.prefab:e29ff2a37e9864946a2e8b930ae9b90f",
			"VO_LOOTA_BOSS_23h_Male_Furbolg_HeroPower5_01.prefab:17298eb378d0f294c8760e8c30e9769f"
		};
	}

	// Token: 0x06003671 RID: 13937 RVA: 0x00114DA0 File Offset: 0x00112FA0
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_23h_Male_Furbolg_Death_01.prefab:b513972ec6ee8d04786ce0e62bdab6e8";
	}

	// Token: 0x06003672 RID: 13938 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003673 RID: 13939 RVA: 0x00114DA8 File Offset: 0x00112FA8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_23h_Male_Furbolg_Intro_01.prefab:7e40d7bc17378c6449ba9f7bb40ceab3", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_23h_Male_Furbolg_EmoteResponse_01.prefab:e0068889c8f178846b15af6cb3a2fd1c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003674 RID: 13940 RVA: 0x00114E2F File Offset: 0x0011302F
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
			int num = 50;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_BigMinionsLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_BigMinionsLines[UnityEngine.Random.Range(0, this.m_BigMinionsLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_BigMinionsLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
		}
		yield break;
	}

	// Token: 0x04001D36 RID: 7478
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D37 RID: 7479
	private List<string> m_BigMinionsLines = new List<string>
	{
		"VO_LOOTA_BOSS_23h_Male_Furbolg_EventPlayBigMinion1_01.prefab:19760aa76d53f9145bcac8932d5ce169",
		"VO_LOOTA_BOSS_23h_Male_Furbolg_EventPlayBigMinion2_01.prefab:06ee1bd2fdd4bcd449f2fb73992d59ae",
		"VO_LOOTA_BOSS_23h_Male_Furbolg_EventPlayBigMinion3_01.prefab:d20754ed0a8c3964ba30db1b14144c14"
	};
}
