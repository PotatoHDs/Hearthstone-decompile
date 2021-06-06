using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class LOOT_Dungeon_BOSS_50h : LOOT_Dungeon
{
	// Token: 0x06003728 RID: 14120 RVA: 0x00117058 File Offset: 0x00115258
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_50h_Male_Kobold_Intro_01.prefab:f80b5a28ce912c04db88d22e5d244240",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EmoteResponse_01.prefab:30cf49b490a303546b11d21a5dbb8a7d",
			"VO_LOOTA_BOSS_50h_Male_Kobold_Death_01.prefab:4f12276a9c8423549a6682a4780c4516",
			"VO_LOOTA_BOSS_50h_Male_Kobold_DefeatPlayer_01.prefab:ccf45106f55df1c4684277733fbd1c9c",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion1_01.prefab:5db49b0673cd3d2459fb17ca59c55209",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion2_01.prefab:b58956e82894ca94c8a573d24a7f52df",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion3_01.prefab:e7d8d43eef5ebe34f99c59ddac7ae3a0",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion1_01.prefab:428104523b25fc942ad89ae89c35d115",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion2_01.prefab:176da33d9d001744ab57ee85712cfda2",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion3_01.prefab:2cddc40945b23004cb090b4743df341c",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventKazakus_01.prefab:37b855a910c4d6941b3dc2cb272c7501"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003729 RID: 14121 RVA: 0x0011712C File Offset: 0x0011532C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string cardId = entity.GetCardId();
			if (cardId == "LOOTA_BOSS_50t" && this.m_PotionLines.Count != 0)
			{
				string randomLine = this.m_PotionLines[UnityEngine.Random.Range(0, this.m_PotionLines.Count)];
				yield return base.PlayLineOnlyOnce(actor, randomLine, 2.5f);
				this.m_PotionLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
		}
		yield break;
	}

	// Token: 0x0600372A RID: 14122 RVA: 0x00117142 File Offset: 0x00115342
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion1_01.prefab:5db49b0673cd3d2459fb17ca59c55209",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion2_01.prefab:b58956e82894ca94c8a573d24a7f52df",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion3_01.prefab:e7d8d43eef5ebe34f99c59ddac7ae3a0"
		};
	}

	// Token: 0x0600372B RID: 14123 RVA: 0x0011716A File Offset: 0x0011536A
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_50h_Male_Kobold_Death_01.prefab:4f12276a9c8423549a6682a4780c4516";
	}

	// Token: 0x0600372C RID: 14124 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600372D RID: 14125 RVA: 0x00117174 File Offset: 0x00115374
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_50h_Male_Kobold_Intro_01.prefab:f80b5a28ce912c04db88d22e5d244240", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_50h_Male_Kobold_EmoteResponse_01.prefab:30cf49b490a303546b11d21a5dbb8a7d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600372E RID: 14126 RVA: 0x001171FB File Offset: 0x001153FB
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
		if (cardId == "CFM_621")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_50h_Male_Kobold_EventKazakus_01.prefab:37b855a910c4d6941b3dc2cb272c7501", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600372F RID: 14127 RVA: 0x00117211 File Offset: 0x00115411
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D55 RID: 7509
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D56 RID: 7510
	private List<string> m_PotionLines = new List<string>
	{
		"VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion1_01.prefab:428104523b25fc942ad89ae89c35d115",
		"VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion2_01.prefab:176da33d9d001744ab57ee85712cfda2",
		"VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion3_01.prefab:2cddc40945b23004cb090b4743df341c"
	};
}
