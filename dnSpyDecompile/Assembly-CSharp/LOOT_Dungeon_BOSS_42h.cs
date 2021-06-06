using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003CD RID: 973
public class LOOT_Dungeon_BOSS_42h : LOOT_Dungeon
{
	// Token: 0x060036E3 RID: 14051 RVA: 0x001163FC File Offset: 0x001145FC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_42h_Female_Furbolg_Intro_01.prefab:e6a0b52c962983c489ab786bdd7b79f4",
			"VO_LOOTA_BOSS_42h_Female_Furbolg_EmoteResponse_01.prefab:1d5578d063a77c041ba0c134c626aced",
			"VO_LOOTA_BOSS_42h_Female_Furbolg_Armor5_01.prefab:fd5a2bdaea56f7b4b883ef7334881533",
			"VO_LOOTA_BOSS_42h_Female_Furbolg_Armor10_01.prefab:6fdf043dd2afda0468139c7de643902c",
			"VO_LOOTA_BOSS_42h_Female_Furbolg_Armor15_01.prefab:df9cb4bc288d2f04fb2c31a63b98d136",
			"VO_LOOTA_BOSS_42h_Female_Furbolg_Armor20_01.prefab:5024ee61e4f4e31468d1617148af5c76",
			"VO_LOOTA_BOSS_42h_Female_Furbolg_Death_01.prefab:7a7171be3063c424bacaae1f4986d0b1",
			"VO_LOOTA_BOSS_42h_Female_Furbolg_DefeatPlayer_01.prefab:43a63a4ab1d3d8f4d9a9df823f882209"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036E4 RID: 14052 RVA: 0x001164AC File Offset: 0x001146AC
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036E5 RID: 14053 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060036E6 RID: 14054 RVA: 0x001164C2 File Offset: 0x001146C2
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_42h_Female_Furbolg_Death_01.prefab:7a7171be3063c424bacaae1f4986d0b1";
	}

	// Token: 0x060036E7 RID: 14055 RVA: 0x001164CC File Offset: 0x001146CC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_42h_Female_Furbolg_Intro_01.prefab:e6a0b52c962983c489ab786bdd7b79f4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_42h_Female_Furbolg_EmoteResponse_01.prefab:1d5578d063a77c041ba0c134c626aced", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036E8 RID: 14056 RVA: 0x00116553 File Offset: 0x00114753
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string missionEventName = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(missionEventName))
		{
			yield break;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		switch (missionEvent)
		{
		case 101:
			this.m_playedLines.Add(missionEventName);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor5_01.prefab:fd5a2bdaea56f7b4b883ef7334881533", 2.5f);
			break;
		case 102:
			this.m_playedLines.Add(missionEventName);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor10_01.prefab:6fdf043dd2afda0468139c7de643902c", 2.5f);
			break;
		case 103:
			this.m_playedLines.Add(missionEventName);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor15_01.prefab:df9cb4bc288d2f04fb2c31a63b98d136", 2.5f);
			break;
		case 104:
			this.m_playedLines.Add(missionEventName);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor20_01.prefab:5024ee61e4f4e31468d1617148af5c76", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04001D4B RID: 7499
	private HashSet<string> m_playedLines = new HashSet<string>();
}
