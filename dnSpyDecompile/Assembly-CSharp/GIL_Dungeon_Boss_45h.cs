using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class GIL_Dungeon_Boss_45h : GIL_Dungeon
{
	// Token: 0x06003855 RID: 14421 RVA: 0x0011C4D4 File Offset: 0x0011A6D4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_45h_Female_Human_Intro_01.prefab:5cbaf2dc51852e24dbd17bbbdf87cf8f",
			"VO_GILA_BOSS_45h_Female_Human_EmoteResponse_01.prefab:ec9bd3abc0eb6d243b3fc7a34fa43406",
			"VO_GILA_BOSS_45h_Female_Human_Death_02.prefab:a281121fabb777e49a73fa3abde185d9",
			"VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_01.prefab:59b04c5fc78d998419e9031e5724ac21",
			"VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_02.prefab:f1ced766b713bcd4bab20b5df385b375",
			"VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_03.prefab:92c52e500b3a5a049b2d3d44db263ec8",
			"VO_GILA_BOSS_45h_Female_Human_EventPlaysAcolyte_01.prefab:6cd648f8d77f0c944b9d82978c4ed975",
			"VO_GILA_BOSS_45h_Female_Human_EventPlaysNovice_01.prefab:c54565566da737d4f8a8aa92cf1648da",
			"VO_GILA_BOSS_45h_Female_Human_EventPlaysHoarder_01.prefab:2a99af8e9559db74a88b2f53f41ceb2d",
			"VO_GILA_BOSS_45h_Female_Human_EventFirstDamage_01.prefab:2aae7467c86b7624085aa0e2bf6beaef",
			"VO_GILA_BOSS_45h_Female_Human_EventPlayHallowedWater_01.prefab:a0de02488030e9e4fb0caa5deced73d6"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003856 RID: 14422 RVA: 0x0011C5A8 File Offset: 0x0011A7A8
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003857 RID: 14423 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003858 RID: 14424 RVA: 0x0011C5BE File Offset: 0x0011A7BE
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_45h_Female_Human_Death_02.prefab:a281121fabb777e49a73fa3abde185d9";
	}

	// Token: 0x06003859 RID: 14425 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600385A RID: 14426 RVA: 0x0011C5C8 File Offset: 0x0011A7C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_45h_Female_Human_Intro_01.prefab:5cbaf2dc51852e24dbd17bbbdf87cf8f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_45h_Female_Human_EmoteResponse_01.prefab:ec9bd3abc0eb6d243b3fc7a34fa43406", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600385B RID: 14427 RVA: 0x0011C64F File Offset: 0x0011A84F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventFirstDamage_01.prefab:2aae7467c86b7624085aa0e2bf6beaef", 2.5f);
			}
		}
		else if (this.m_PlayerDraw.Count > 0)
		{
			string text = this.m_PlayerDraw[UnityEngine.Random.Range(0, this.m_PlayerDraw.Count)];
			this.m_PlayerDraw.Remove(text);
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600385C RID: 14428 RVA: 0x0011C665 File Offset: 0x0011A865
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		this.m_playedLines.Add(cardID);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string a = cardID;
		if (!(a == "EX1_007"))
		{
			if (!(a == "EX1_015"))
			{
				if (!(a == "EX1_096"))
				{
					if (a == "GILA_850b")
					{
						yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlayHallowedWater_01.prefab:a0de02488030e9e4fb0caa5deced73d6", 2.5f);
					}
				}
				else
				{
					yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlaysHoarder_01.prefab:2a99af8e9559db74a88b2f53f41ceb2d", 2.5f);
				}
			}
			else
			{
				yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlaysNovice_01.prefab:c54565566da737d4f8a8aa92cf1648da", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_45h_Female_Human_EventPlaysAcolyte_01.prefab:6cd648f8d77f0c944b9d82978c4ed975", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001DB5 RID: 7605
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DB6 RID: 7606
	private List<string> m_PlayerDraw = new List<string>
	{
		"VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_01.prefab:59b04c5fc78d998419e9031e5724ac21",
		"VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_02.prefab:f1ced766b713bcd4bab20b5df385b375",
		"VO_GILA_BOSS_45h_Female_Human_EventDrawsExtraCards_03.prefab:92c52e500b3a5a049b2d3d44db263ec8"
	};
}
