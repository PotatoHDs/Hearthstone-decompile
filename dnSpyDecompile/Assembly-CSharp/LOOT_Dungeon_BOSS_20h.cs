using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003BD RID: 957
public class LOOT_Dungeon_BOSS_20h : LOOT_Dungeon
{
	// Token: 0x06003656 RID: 13910 RVA: 0x00114778 File Offset: 0x00112978
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_20h_Male_Earthen_Intro_01.prefab:b1712d4e38816874f82613e4fa8060e2",
			"VO_LOOTA_BOSS_20h_Male_Earthen_EmoteResponse_01.prefab:b77df03627d7c2f449aa626db3255011",
			"VO_LOOTA_BOSS_20h_Male_Earthen_HeroPower_01.prefab:e37c0e7e80d2a684ebff084ab31d4555",
			"VO_LOOTA_BOSS_20h_Male_Earthen_HeroPowerNoStatues_01.prefab:dfec071a415b9f6498924e7fcda87900",
			"VO_LOOTA_BOSS_20h_Male_Earthen_Death_01.prefab:9c60adabd0d578e42a982f43d9e38395",
			"VO_LOOTA_BOSS_20h_Male_Earthen_DefeatPlayer_01.prefab:5283495640bba424ebb09ad1b79bc5d0",
			"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed1_01.prefab:ef7a58c5d160ca541958ec50bdfb356c",
			"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed2_01.prefab:0432d18c4a89d3c449e38372253cecd7",
			"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed3_01.prefab:6b618e4b53aaceb4598f071f0a1566ec",
			"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed4_01.prefab:c8e2a6ed7c8b5584fb3b02cf29c82ed0",
			"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed5_01.prefab:0294fb2a21551f844b543bbd163cb506"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003657 RID: 13911 RVA: 0x0011484C File Offset: 0x00112A4C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003658 RID: 13912 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003659 RID: 13913 RVA: 0x00114862 File Offset: 0x00112A62
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_20h_Male_Earthen_Death_01.prefab:9c60adabd0d578e42a982f43d9e38395";
	}

	// Token: 0x0600365A RID: 13914 RVA: 0x0011486C File Offset: 0x00112A6C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_20h_Male_Earthen_Intro_01.prefab:b1712d4e38816874f82613e4fa8060e2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_20h_Male_Earthen_EmoteResponse_01.prefab:b77df03627d7c2f449aa626db3255011", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600365B RID: 13915 RVA: 0x001148F3 File Offset: 0x00112AF3
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
		case 101:
		{
			int num = 50;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_StatueDestroyedLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_StatueDestroyedLines[UnityEngine.Random.Range(0, this.m_StatueDestroyedLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_StatueDestroyedLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
			break;
		}
		case 102:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_20h_Male_Earthen_HeroPower_01.prefab:e37c0e7e80d2a684ebff084ab31d4555", 2.5f);
			yield return null;
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_20h_Male_Earthen_HeroPowerNoStatues_01.prefab:dfec071a415b9f6498924e7fcda87900", 2.5f);
			yield return null;
			break;
		}
		yield break;
	}

	// Token: 0x04001D30 RID: 7472
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D31 RID: 7473
	private List<string> m_StatueDestroyedLines = new List<string>
	{
		"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed1_01.prefab:ef7a58c5d160ca541958ec50bdfb356c",
		"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed2_01.prefab:0432d18c4a89d3c449e38372253cecd7",
		"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed3_01.prefab:6b618e4b53aaceb4598f071f0a1566ec",
		"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed4_01.prefab:c8e2a6ed7c8b5584fb3b02cf29c82ed0",
		"VO_LOOTA_BOSS_20h_Male_Earthen_EventStatueDestroyed5_01.prefab:0294fb2a21551f844b543bbd163cb506"
	};
}
