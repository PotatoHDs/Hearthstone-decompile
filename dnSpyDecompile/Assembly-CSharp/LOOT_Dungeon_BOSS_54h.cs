using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003D9 RID: 985
public class LOOT_Dungeon_BOSS_54h : LOOT_Dungeon
{
	// Token: 0x0600374D RID: 14157 RVA: 0x00117C48 File Offset: 0x00115E48
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_01.prefab:fc331202e4398374c87ba5e467990a7a",
			"VO_LOOTA_BOSS_54h_Female_Human_EmoteResponse_01.prefab:30a58ef54493277458e6f40e10808e73",
			"VO_LOOTA_BOSS_54h_Female_Human_WakeUp_01.prefab:3459b094d47361849a38d1c11d100d07",
			"VO_LOOTA_BOSS_54h_Female_Human_Death_01.prefab:0b30aff6d8b02bf44aa25146198146c9",
			"VO_LOOTA_BOSS_54h_Female_Human_DefeatPlayer_01.prefab:f710812544c073044b59f063537fe205",
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_02.prefab:138fa139ba8594e47aabf7064ef0a62a",
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_03.prefab:0c95e3240fb81a34b99ffcb977dee49a",
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_04.prefab:5547e3a11ad9c6c4eb7fe18a7fda090f",
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_07.prefab:2740bd918d6591643a076e2b0804021b"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600374E RID: 14158 RVA: 0x00117D04 File Offset: 0x00115F04
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600374F RID: 14159 RVA: 0x00117D1A File Offset: 0x00115F1A
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_02.prefab:138fa139ba8594e47aabf7064ef0a62a",
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_03.prefab:0c95e3240fb81a34b99ffcb977dee49a",
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_04.prefab:5547e3a11ad9c6c4eb7fe18a7fda090f",
			"VO_LOOTA_BOSS_54h_Female_Human_Intro_07.prefab:2740bd918d6591643a076e2b0804021b"
		};
	}

	// Token: 0x06003750 RID: 14160 RVA: 0x00117D4D File Offset: 0x00115F4D
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_54h_Female_Human_Death_01.prefab:0b30aff6d8b02bf44aa25146198146c9";
	}

	// Token: 0x06003751 RID: 14161 RVA: 0x00117D54 File Offset: 0x00115F54
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_54h_Female_Human_Intro_01.prefab:fc331202e4398374c87ba5e467990a7a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		if (GameState.Get().GetTurn() >= 12)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_54h_Female_Human_DefeatPlayer_01.prefab:f710812544c073044b59f063537fe205", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_54h_Female_Human_EmoteResponse_01.prefab:30a58ef54493277458e6f40e10808e73", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06003752 RID: 14162 RVA: 0x00117E13 File Offset: 0x00116013
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
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_54h_Female_Human_WakeUp_01.prefab:3459b094d47361849a38d1c11d100d07", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D61 RID: 7521
	private HashSet<string> m_playedLines = new HashSet<string>();
}
