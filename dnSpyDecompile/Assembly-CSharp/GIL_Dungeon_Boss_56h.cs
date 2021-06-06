using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000400 RID: 1024
public class GIL_Dungeon_Boss_56h : GIL_Dungeon
{
	// Token: 0x060038B5 RID: 14517 RVA: 0x0011DBFC File Offset: 0x0011BDFC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_56h_Male_Elemental_IntroALT_02.prefab:685ce5a72c2c9b143ae449798cd25504",
			"VO_GILA_BOSS_56h_Male_Elemental_EmoteResponse_01.prefab:b6e33ab8bedec2f45880bc0814a6244e",
			"VO_GILA_BOSS_56h_Male_Elemental_Death_01.prefab:106ca4785aecf2841bb4480f0faffc55",
			"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_01.prefab:df2dfa8b69de19343b42692930e430bb",
			"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_02.prefab:fb562cb1d85c1fc448d2e95add40c48f",
			"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_03.prefab:dd5ce8bfe6318fb4394c85689dd2a775",
			"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_05.prefab:49fa7560ef80a1244a7b53882c9ced80",
			"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_07.prefab:9f7a98727087e5c409840e4d54730bd8",
			"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_09.prefab:f9dae16439fc87a4fa49fd29c9970e79",
			"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_10.prefab:98e67b5d19c2dcd4b8165a280f3955fb",
			"VO_GILA_BOSS_56h_Male_Elemental_EventTransformFace_02.prefab:cc00cd508d1eca745a9fd7360b2a9bea"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038B6 RID: 14518 RVA: 0x0011DCD0 File Offset: 0x0011BED0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_56h_Male_Elemental_IntroALT_02.prefab:685ce5a72c2c9b143ae449798cd25504", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_56h_Male_Elemental_EmoteResponse_01.prefab:b6e33ab8bedec2f45880bc0814a6244e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038B7 RID: 14519 RVA: 0x0011DD57 File Offset: 0x0011BF57
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_56h_Male_Elemental_Death_01.prefab:106ca4785aecf2841bb4480f0faffc55";
	}

	// Token: 0x060038B8 RID: 14520 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060038B9 RID: 14521 RVA: 0x0011DD5E File Offset: 0x0011BF5E
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060038BA RID: 14522 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060038BB RID: 14523 RVA: 0x0011DD74 File Offset: 0x0011BF74
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_56h_Male_Elemental_EventTransformFace_02.prefab:cc00cd508d1eca745a9fd7360b2a9bea", 2.5f);
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DD3 RID: 7635
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DD4 RID: 7636
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_01.prefab:df2dfa8b69de19343b42692930e430bb",
		"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_02.prefab:fb562cb1d85c1fc448d2e95add40c48f",
		"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_03.prefab:dd5ce8bfe6318fb4394c85689dd2a775",
		"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_05.prefab:49fa7560ef80a1244a7b53882c9ced80",
		"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_07.prefab:9f7a98727087e5c409840e4d54730bd8",
		"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_09.prefab:f9dae16439fc87a4fa49fd29c9970e79",
		"VO_GILA_BOSS_56h_Male_Elemental_HeroPower_10.prefab:98e67b5d19c2dcd4b8165a280f3955fb"
	};
}
