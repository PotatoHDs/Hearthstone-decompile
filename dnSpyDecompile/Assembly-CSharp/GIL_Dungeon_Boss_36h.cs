using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003ED RID: 1005
public class GIL_Dungeon_Boss_36h : GIL_Dungeon
{
	// Token: 0x06003802 RID: 14338 RVA: 0x0011B1B0 File Offset: 0x001193B0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_36h_Female_Human_Intro_01.prefab:57aad793ef959bc4e8fb0d5bd541240e",
			"VO_GILA_BOSS_36h_Female_Human_Emote Response_01:10f920edf9f9dc84595c32d34bc30106",
			"VO_GILA_BOSS_36h_Female_Human_Death_01.prefab:2f9429b7a50339340991733a35640edf",
			"VO_GILA_BOSS_36h_Female_Human_HeroPower_01.prefab:cee27fa7fae7801449ea6d9093449aa3",
			"VO_GILA_BOSS_36h_Female_Human_HeroPower_02.prefab:92888a961bb3af34f9e715d3bd83368c",
			"VO_GILA_BOSS_36h_Female_Human_HeroPower_03.prefab:dc8ea40d25fbe344d8900d53fbffbb5c",
			"VO_GILA_BOSS_36h_Female_Human_EventHex_01.prefab:3ac21887b5d04084cba245f59cdf08e2",
			"VO_GILA_BOSS_36h_Female_Human_EventHex_02.prefab:9213365e2510a7e488c2291eae467da5"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003803 RID: 14339 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003804 RID: 14340 RVA: 0x0011B260 File Offset: 0x00119460
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003805 RID: 14341 RVA: 0x0011B276 File Offset: 0x00119476
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_36h_Female_Human_HeroPower_01.prefab:cee27fa7fae7801449ea6d9093449aa3",
			"VO_GILA_BOSS_36h_Female_Human_HeroPower_02.prefab:92888a961bb3af34f9e715d3bd83368c",
			"VO_GILA_BOSS_36h_Female_Human_HeroPower_03.prefab:dc8ea40d25fbe344d8900d53fbffbb5c"
		};
	}

	// Token: 0x06003806 RID: 14342 RVA: 0x0011B29E File Offset: 0x0011949E
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_36h_Female_Human_Death_01.prefab:2f9429b7a50339340991733a35640edf";
	}

	// Token: 0x06003807 RID: 14343 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003808 RID: 14344 RVA: 0x0011B2A8 File Offset: 0x001194A8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_36h_Female_Human_Intro_01.prefab:57aad793ef959bc4e8fb0d5bd541240e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_36h_Female_Human_Emote Response_01:10f920edf9f9dc84595c32d34bc30106", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003809 RID: 14345 RVA: 0x0011B32F File Offset: 0x0011952F
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
		if (cardId == "EX1_246")
		{
			string text = base.PopRandomLineWithChance(this.m_PlayerHex);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001D9D RID: 7581
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D9E RID: 7582
	private List<string> m_PlayerHex = new List<string>
	{
		"VO_GILA_BOSS_36h_Female_Human_EventHex_01.prefab:3ac21887b5d04084cba245f59cdf08e2",
		"VO_GILA_BOSS_36h_Female_Human_EventHex_02.prefab:9213365e2510a7e488c2291eae467da5"
	};
}
