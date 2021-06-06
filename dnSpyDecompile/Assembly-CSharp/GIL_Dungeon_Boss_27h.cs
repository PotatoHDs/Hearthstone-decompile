using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E5 RID: 997
public class GIL_Dungeon_Boss_27h : GIL_Dungeon
{
	// Token: 0x060037BD RID: 14269 RVA: 0x0011A318 File Offset: 0x00118518
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_27h_Male_Construct_Intro_01.prefab:a3a9f619f8cd367498818154dc079a8a",
			"VO_GILA_BOSS_27h_Male_Construct_EmoteResponse_01.prefab:ff9964ab0269a7949bab4870aa594461",
			"VO_GILA_BOSS_27h_Male_Construct_Death_02.prefab:f4beb9b551ca2404484dff794b03a721",
			"VO_GILA_BOSS_27h_Male_Construct_HeroPowerMurloc_01.prefab:d9ccf54e892933140af3e09ce5459e76",
			"VO_GILA_BOSS_27h_Male_Construct_HeroPowerBeast_02.prefab:5be0fa4bf8907da4394ac2f633e37c6e",
			"VO_GILA_BOSS_27h_Male_Construct_HeroPowerPirate_01.prefab:8abc985b55353984083cf2d6edd9941a",
			"VO_GILA_BOSS_27h_Male_Construct_HeroPowerDragon_01.prefab:2486bd5a23450414fbc57eb09fd295d7",
			"VO_GILA_BOSS_27h_Male_Construct_HeroPowerMech_02.prefab:ea1f110a627b5e34698b87920d05a81a",
			"VO_GILA_BOSS_27h_Male_Construct_HeroPowerDemon_01.prefab:d09533588ca279b40935bb7fe9e8c409",
			"VO_GILA_BOSS_27h_Male_Construct_HeroPowerGeneric_01.prefab:f18f38dbd19b0244d9c57f492611951f",
			"VO_GILA_BOSS_27h_Male_Construct_EventPlayDrBoom_02.prefab:29aa1d78cc65d374ba457dff5b7014da",
			"VO_GILA_BOSS_27h_Male_Construct_EventPlayUnstablePortal_01.prefab:fc812125bffa13946a8b63b675dfbb72",
			"VO_GILA_BOSS_27h_Male_Construct_EventPlaySparePart_01.prefab:be6a9d5ca3fd4ff41af8499cb4c8c46a"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037BE RID: 14270 RVA: 0x0011A400 File Offset: 0x00118600
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_27h_Male_Construct_Intro_01.prefab:a3a9f619f8cd367498818154dc079a8a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_27h_Male_Construct_EmoteResponse_01.prefab:ff9964ab0269a7949bab4870aa594461", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037BF RID: 14271 RVA: 0x0011A487 File Offset: 0x00118687
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_27h_Male_Construct_Death_02.prefab:f4beb9b551ca2404484dff794b03a721";
	}

	// Token: 0x060037C0 RID: 14272 RVA: 0x0011A48E File Offset: 0x0011868E
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060037C1 RID: 14273 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060037C2 RID: 14274 RVA: 0x0011A4A4 File Offset: 0x001186A4
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1720466961U)
		{
			if (num <= 1670134104U)
			{
				if (num != 963594556U)
				{
					if (num != 1670134104U)
					{
						goto IL_2B6;
					}
					if (!(cardId == "PART_001"))
					{
						goto IL_2B6;
					}
				}
				else
				{
					if (!(cardId == "GVG_110"))
					{
						goto IL_2B6;
					}
					yield return base.PlayBossLine(actor, "VO_GILA_BOSS_27h_Male_Construct_EventPlayDrBoom_02.prefab:29aa1d78cc65d374ba457dff5b7014da", 2.5f);
					goto IL_2B6;
				}
			}
			else if (num != 1703689342U)
			{
				if (num != 1720466961U)
				{
					goto IL_2B6;
				}
				if (!(cardId == "PART_002"))
				{
					goto IL_2B6;
				}
			}
			else if (!(cardId == "PART_003"))
			{
				goto IL_2B6;
			}
		}
		else if (num <= 1754022199U)
		{
			if (num != 1737244580U)
			{
				if (num != 1754022199U)
				{
					goto IL_2B6;
				}
				if (!(cardId == "PART_004"))
				{
					goto IL_2B6;
				}
			}
			else if (!(cardId == "PART_005"))
			{
				goto IL_2B6;
			}
		}
		else if (num != 1770799818U)
		{
			if (num != 1787577437U)
			{
				if (num != 3755629543U)
				{
					goto IL_2B6;
				}
				if (!(cardId == "GVG_003"))
				{
					goto IL_2B6;
				}
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_27h_Male_Construct_EventPlayUnstablePortal_01.prefab:fc812125bffa13946a8b63b675dfbb72", 2.5f);
				goto IL_2B6;
			}
			else if (!(cardId == "PART_006"))
			{
				goto IL_2B6;
			}
		}
		else if (!(cardId == "PART_007"))
		{
			goto IL_2B6;
		}
		yield return base.PlayBossLine(actor, "VO_GILA_BOSS_27h_Male_Construct_EventPlaySparePart_01.prefab:be6a9d5ca3fd4ff41af8499cb4c8c46a", 2.5f);
		IL_2B6:
		yield break;
	}

	// Token: 0x060037C3 RID: 14275 RVA: 0x0011A4BA File Offset: 0x001186BA
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
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerMurloc_01.prefab:d9ccf54e892933140af3e09ce5459e76", 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerBeast_02.prefab:5be0fa4bf8907da4394ac2f633e37c6e", 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerPirate_01.prefab:8abc985b55353984083cf2d6edd9941a", 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerDragon_01.prefab:2486bd5a23450414fbc57eb09fd295d7", 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerMech_02.prefab:ea1f110a627b5e34698b87920d05a81a", 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerDemon_01.prefab:d09533588ca279b40935bb7fe9e8c409", 2.5f);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerGeneric_01.prefab:f18f38dbd19b0244d9c57f492611951f", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04001D92 RID: 7570
	private HashSet<string> m_playedLines = new HashSet<string>();
}
