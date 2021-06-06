using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public class LOOT_Dungeon_BOSS_53h : LOOT_Dungeon
{
	// Token: 0x06003743 RID: 14147 RVA: 0x001178C0 File Offset: 0x00115AC0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_53h2_Female_Undead_Intro_01.prefab:1b2a36b185a12124d93aa365ea6e09da",
			"VO_LOOTA_BOSS_53h2_Female_Undead_EmoteResponse_01.prefab:bbf07041f5574b248a637c1f32854c1a",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart1_01.prefab:7a7d593a0fda38b499357f41523ffcfe",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart2_01.prefab:a3bf00b1d5df19547906ea76d234b357",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart3_01.prefab:5062a4a6bd097af41afe42c1fd92ff6e",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart4_01.prefab:25d00fd30e682cd46b321ca0bbb5f359",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard1_01.prefab:0fc1a8ac09c4dff4e9c77d0e585b1e09",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard2_01.prefab:bc8b1cdb3e468714a9bfeab0889d295e",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard3_01.prefab:17cdebaed9270e9428a4f2c4bdff4e34",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard4_01.prefab:da2babef59fc30540a9c8c2a94fcdd7d",
			"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard5_01.prefab:2899253c601f7794f9d8e238fb299a13",
			"VO_LOOTA_BOSS_53h2_Female_Undead_RitualPrep1_01.prefab:1139299c1105a2d449b43be7945a856d",
			"VO_LOOTA_BOSS_53h2_Female_Undead_RitualPrep2_01.prefab:87e4e1d01cd798844958022b73bf7d75",
			"VO_LOOTA_BOSS_53h2_Female_Undead_RitualPrep3_01.prefab:7294b86c21074c049a201377991cac84",
			"VO_LOOTA_BOSS_53h2_Female_Undead_Death_01.prefab:47609d8c37e31cc42b9201af07985ed1",
			"VO_LOOTA_BOSS_53h2_Female_Undead_DefeatPlayer_01.prefab:90c1f9a85ee256d4b96c1e33ce606ca7",
			"VO_LOOTA_BOSS_53h2_Female_Undead_EventPlayerTimeWalk_01.prefab:670d03d507a53de41b3ad656aeb5a568",
			"VO_LOOTA_BOSS_53h2_Female_Undead_EventManaDestroyed_01.prefab:9380576598589cb4b8d813599e80a5a1"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003744 RID: 14148 RVA: 0x001179E0 File Offset: 0x00115BE0
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (this.m_IdleLines.Count == 0)
		{
			return;
		}
		string line = this.m_IdleLines[0];
		this.m_IdleLines.RemoveAt(0);
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
	}

	// Token: 0x06003745 RID: 14149 RVA: 0x00117A6C File Offset: 0x00115C6C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003746 RID: 14150 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003747 RID: 14151 RVA: 0x00117A82 File Offset: 0x00115C82
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_53h2_Female_Undead_Death_01.prefab:47609d8c37e31cc42b9201af07985ed1";
	}

	// Token: 0x06003748 RID: 14152 RVA: 0x00117A8C File Offset: 0x00115C8C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_53h2_Female_Undead_Intro_01.prefab:1b2a36b185a12124d93aa365ea6e09da", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
			if (cardId == "LOOTA_BOSS_53h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_53h2_Female_Undead_EmoteResponse_01.prefab:bbf07041f5574b248a637c1f32854c1a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			if (cardId == "LOOTA_BOSS_53h2")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard1_01.prefab:0fc1a8ac09c4dff4e9c77d0e585b1e09", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
	}

	// Token: 0x06003749 RID: 14153 RVA: 0x00117B6C File Offset: 0x00115D6C
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
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
			}
		}
		else
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
			int num = 50;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_ExtraTurnLines.Count != 0 && num >= num2)
			{
				GameState.Get().SetBusy(true);
				string randomLine = this.m_ExtraTurnLines[UnityEngine.Random.Range(0, this.m_ExtraTurnLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				GameState.Get().SetBusy(false);
				this.m_ExtraTurnLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
		}
		yield break;
	}

	// Token: 0x0600374A RID: 14154 RVA: 0x00117B82 File Offset: 0x00115D82
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
		if (!(cardId == "UNG_028t") && !(cardId == "LOOTA_830"))
		{
			if (cardId == "LOOTA_811")
			{
				yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_53h2_Female_Undead_EventManaDestroyed_01.prefab:9380576598589cb4b8d813599e80a5a1", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_53h2_Female_Undead_EventPlayerTimeWalk_01.prefab:670d03d507a53de41b3ad656aeb5a568", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D5E RID: 7518
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D5F RID: 7519
	private List<string> m_ExtraTurnLines = new List<string>
	{
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart1_01.prefab:7a7d593a0fda38b499357f41523ffcfe",
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart2_01.prefab:a3bf00b1d5df19547906ea76d234b357",
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart3_01.prefab:5062a4a6bd097af41afe42c1fd92ff6e",
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindStart4_01.prefab:25d00fd30e682cd46b321ca0bbb5f359",
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard2_01.prefab:bc8b1cdb3e468714a9bfeab0889d295e",
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard3_01.prefab:17cdebaed9270e9428a4f2c4bdff4e34",
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard4_01.prefab:da2babef59fc30540a9c8c2a94fcdd7d",
		"VO_LOOTA_BOSS_53h2_Female_Undead_TimeRewindPlayCard5_01.prefab:2899253c601f7794f9d8e238fb299a13"
	};

	// Token: 0x04001D60 RID: 7520
	private List<string> m_IdleLines = new List<string>
	{
		"VO_LOOTA_BOSS_53h2_Female_Undead_RitualPrep1_01.prefab:1139299c1105a2d449b43be7945a856d",
		"VO_LOOTA_BOSS_53h2_Female_Undead_RitualPrep2_01.prefab:87e4e1d01cd798844958022b73bf7d75",
		"VO_LOOTA_BOSS_53h2_Female_Undead_RitualPrep3_01.prefab:7294b86c21074c049a201377991cac84"
	};
}
