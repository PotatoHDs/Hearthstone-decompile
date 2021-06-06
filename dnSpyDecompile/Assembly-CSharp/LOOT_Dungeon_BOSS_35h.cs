using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C6 RID: 966
public class LOOT_Dungeon_BOSS_35h : LOOT_Dungeon
{
	// Token: 0x060036A6 RID: 13990 RVA: 0x00115768 File Offset: 0x00113968
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_35h_Male_Furbolg_Intro_01.prefab:f6f44292c06355449ab33c49b87fd051",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_EmoteResponse_01.prefab:c01bc9067117b8142b96c8b60fe96b9d",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered1_01.prefab:25a7fb7a7b8b3814daf55fb23ea9413e",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered2_01.prefab:8b79ec33b480b80449e68711c8d26a03",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered3_01.prefab:347ceccfb0763f944998fef97df721c7",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered4_01.prefab:ec23b2e7ba01cb441a3ce3add0361e72",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered5_01.prefab:c39f53ea68577a74099dace6e5fb7f69",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_Death_01.prefab:044f0aa3de369464db7a3577780cf268",
			"VO_LOOTA_BOSS_35h_Male_Furbolg_DefeatPlayer_01.prefab:3ceb3d9d755224f4e917d571e6a93943"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036A7 RID: 13991 RVA: 0x00115824 File Offset: 0x00113A24
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036A8 RID: 13992 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060036A9 RID: 13993 RVA: 0x0011583A File Offset: 0x00113A3A
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_35h_Male_Furbolg_Death_01.prefab:044f0aa3de369464db7a3577780cf268";
	}

	// Token: 0x060036AA RID: 13994 RVA: 0x00115844 File Offset: 0x00113A44
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_35h_Male_Furbolg_Intro_01.prefab:f6f44292c06355449ab33c49b87fd051", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_35h_Male_Furbolg_EmoteResponse_01.prefab:c01bc9067117b8142b96c8b60fe96b9d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036AB RID: 13995 RVA: 0x001158CB File Offset: 0x00113ACB
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
			int num = 30;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_CounteredLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_CounteredLines[UnityEngine.Random.Range(0, this.m_CounteredLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_CounteredLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
		}
		yield break;
	}

	// Token: 0x04001D40 RID: 7488
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D41 RID: 7489
	private List<string> m_CounteredLines = new List<string>
	{
		"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered1_01.prefab:25a7fb7a7b8b3814daf55fb23ea9413e",
		"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered2_01.prefab:8b79ec33b480b80449e68711c8d26a03",
		"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered3_01.prefab:347ceccfb0763f944998fef97df721c7",
		"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered4_01.prefab:ec23b2e7ba01cb441a3ce3add0361e72",
		"VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered5_01.prefab:c39f53ea68577a74099dace6e5fb7f69"
	};
}
