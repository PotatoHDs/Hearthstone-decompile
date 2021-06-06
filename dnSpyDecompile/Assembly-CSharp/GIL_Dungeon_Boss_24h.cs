using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E2 RID: 994
public class GIL_Dungeon_Boss_24h : GIL_Dungeon
{
	// Token: 0x0600379E RID: 14238 RVA: 0x00119A28 File Offset: 0x00117C28
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_24h_Male_Bogbeast_Intro_01.prefab:9a2ea2d619351344dbf3193778af9c76",
			"VO_GILA_BOSS_24h_Male_Bogbeast_EmoteResponse_01.prefab:fba37407e0659ed438a33f8123ae4cf7",
			"VO_GILA_BOSS_24h_Male_Bogbeast_Death_01.prefab:3a813a650b1526e48bb50c4d3859ffe2",
			"VO_GILA_BOSS_24h_Male_Bogbeast_DefeatPlayer_01.prefab:78e984ed197b3024588f7d47bd76bd7a",
			"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_01.prefab:2fa04f3ef4d736c479204e71bdd26999",
			"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_02.prefab:0e5a979ec0902f046a1721e40fe035fe",
			"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_03.prefab:45a145dbb9dfc0841ab7d534229729ca",
			"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_04.prefab:3d6f6e82106914044b728e6311bfe1fc",
			"VO_GILA_BOSS_24h_Male_Bogbeast_HeroPower_01.prefab:68e1cf7b4e5d11247aba64f927a7e1d6",
			"VO_GILA_BOSS_24h_Male_Bogbeast_HeroPower_02.prefab:c7cde8cf1bff3ae4a9bb1f435a7eb176",
			"VO_GILA_BOSS_24h_Male_Bogbeast_HeroPower_04.prefab:ce0eb5b3f74d4dd4db6d6cbf766724b2"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600379F RID: 14239 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060037A0 RID: 14240 RVA: 0x00119AFC File Offset: 0x00117CFC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_24h_Male_Bogbeast_Intro_01.prefab:9a2ea2d619351344dbf3193778af9c76", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_24h_Male_Bogbeast_EmoteResponse_01.prefab:fba37407e0659ed438a33f8123ae4cf7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037A1 RID: 14241 RVA: 0x00119B83 File Offset: 0x00117D83
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_24h_Male_Bogbeast_Death_01.prefab:3a813a650b1526e48bb50c4d3859ffe2";
	}

	// Token: 0x060037A2 RID: 14242 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060037A3 RID: 14243 RVA: 0x00119B8A File Offset: 0x00117D8A
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060037A4 RID: 14244 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060037A5 RID: 14245 RVA: 0x00119BA0 File Offset: 0x00117DA0
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
		if (entity.GetHealth() == 1 && entity.IsMinion())
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060037A6 RID: 14246 RVA: 0x00119BB6 File Offset: 0x00117DB6
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
		if (missionEvent == 101)
		{
			string text = base.PopRandomLineWithChance(this.m_ZombieLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001D8A RID: 7562
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D8B RID: 7563
	private List<string> m_ZombieLines = new List<string>
	{
		"VO_GILA_BOSS_24h_Male_Bogbeast_HeroPower_01.prefab:68e1cf7b4e5d11247aba64f927a7e1d6",
		"VO_GILA_BOSS_24h_Male_Bogbeast_HeroPower_02.prefab:c7cde8cf1bff3ae4a9bb1f435a7eb176",
		"VO_GILA_BOSS_24h_Male_Bogbeast_HeroPower_04.prefab:ce0eb5b3f74d4dd4db6d6cbf766724b2"
	};

	// Token: 0x04001D8C RID: 7564
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_01.prefab:2fa04f3ef4d736c479204e71bdd26999",
		"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_02.prefab:0e5a979ec0902f046a1721e40fe035fe",
		"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_03.prefab:45a145dbb9dfc0841ab7d534229729ca",
		"VO_GILA_BOSS_24h_Male_Bogbeast_EventPlaysSmallMinion_04.prefab:3d6f6e82106914044b728e6311bfe1fc"
	};
}
