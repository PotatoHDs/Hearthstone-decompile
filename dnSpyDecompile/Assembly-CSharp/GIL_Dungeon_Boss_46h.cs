using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F7 RID: 1015
public class GIL_Dungeon_Boss_46h : GIL_Dungeon
{
	// Token: 0x0600385F RID: 14431 RVA: 0x0011C6BC File Offset: 0x0011A8BC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_46h_Male_Worgen_Intro_01.prefab:793a3ad2cd2405741ad472479cf70932",
			"VO_GILA_BOSS_46h_Male_Worgen_EmoteResponse_01.prefab:b86300a09c3a5ab43bb21a558a2aa331",
			"VO_GILA_BOSS_46h_Male_Worgen_Death_01.prefab:f43afb982dcd460418125e4c4c91ef38",
			"VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_01.prefab:7e95c21db5f63094bb9078b4c11d3a32",
			"VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_03.prefab:d6abf1d6660959147af580da61058831",
			"VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_06.prefab:bb4338cac09ab744a8c9ce0d3822dd5a",
			"VO_GILA_BOSS_46h_Male_Worgen_EventPlayTrapper_02.prefab:0f49f185267a7144689d07bf20405e02",
			"VO_GILA_BOSS_46h_Male_Worgen_EventPlayBloodMoon_01.prefab:95888d40608ed4344a19e1f276275a59"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003860 RID: 14432 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003861 RID: 14433 RVA: 0x0011C76C File Offset: 0x0011A96C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_46h_Male_Worgen_Intro_01.prefab:793a3ad2cd2405741ad472479cf70932", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_46h_Male_Worgen_EmoteResponse_01.prefab:b86300a09c3a5ab43bb21a558a2aa331", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003862 RID: 14434 RVA: 0x0011C7F3 File Offset: 0x0011A9F3
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_46h_Male_Worgen_Death_01.prefab:f43afb982dcd460418125e4c4c91ef38";
	}

	// Token: 0x06003863 RID: 14435 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003864 RID: 14436 RVA: 0x0011C7FA File Offset: 0x0011A9FA
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003865 RID: 14437 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003866 RID: 14438 RVA: 0x0011C810 File Offset: 0x0011AA10
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
		if (num <= 1667476941U)
		{
			if (num <= 734865033U)
			{
				if (num <= 147820246U)
				{
					if (num != 7384536U)
					{
						if (num != 96354556U)
						{
							if (num != 147820246U)
							{
								goto IL_4ED;
							}
							if (!(cardId == "GIL_547"))
							{
								goto IL_4ED;
							}
						}
						else if (!(cardId == "GIL_534"))
						{
							goto IL_4ED;
						}
					}
					else if (!(cardId == "GIL_682"))
					{
						goto IL_4ED;
					}
				}
				else if (num != 547155647U)
				{
					if (num != 689193731U)
					{
						if (num != 734865033U)
						{
							goto IL_4ED;
						}
						if (!(cardId == "ICC_031"))
						{
							goto IL_4ED;
						}
					}
					else if (!(cardId == "DS1_070"))
					{
						goto IL_4ED;
					}
				}
				else if (!(cardId == "CFM_665"))
				{
					goto IL_4ED;
				}
			}
			else if (num <= 1096675350U)
			{
				if (num != 735938005U)
				{
					if (num != 878539475U)
					{
						if (num != 1096675350U)
						{
							goto IL_4ED;
						}
						if (!(cardId == "GIL_118"))
						{
							goto IL_4ED;
						}
					}
					else if (!(cardId == "GIL_201t"))
					{
						goto IL_4ED;
					}
				}
				else if (!(cardId == "KAR_005"))
				{
					goto IL_4ED;
				}
			}
			else if (num != 1112881688U)
			{
				if (num != 1214118683U)
				{
					if (num != 1667476941U)
					{
						goto IL_4ED;
					}
					if (!(cardId == "GIL_683t"))
					{
						goto IL_4ED;
					}
				}
				else if (!(cardId == "GIL_113"))
				{
					goto IL_4ED;
				}
			}
			else if (!(cardId == "GIL_202t"))
			{
				goto IL_4ED;
			}
		}
		else if (num <= 2550470993U)
		{
			if (num <= 2212055763U)
			{
				if (num != 1883479467U)
				{
					if (num != 2186503530U)
					{
						if (num != 2212055763U)
						{
							goto IL_4ED;
						}
						if (!(cardId == "GIL_580"))
						{
							goto IL_4ED;
						}
					}
					else if (!(cardId == "GIL_648"))
					{
						goto IL_4ED;
					}
				}
				else if (!(cardId == "EX1_412"))
				{
					goto IL_4ED;
				}
			}
			else if (num != 2322843053U)
			{
				if (num != 2328219168U)
				{
					if (num != 2550470993U)
					{
						goto IL_4ED;
					}
					if (!(cardId == "OG_292"))
					{
						goto IL_4ED;
					}
				}
				else if (!(cardId == "GIL_509"))
				{
					goto IL_4ED;
				}
			}
			else if (!(cardId == "GIL_692"))
			{
				goto IL_4ED;
			}
		}
		else if (num <= 2911838429U)
		{
			if (num != 2567248612U)
			{
				if (num != 2602628231U)
				{
					if (num != 2911838429U)
					{
						goto IL_4ED;
					}
					if (!(cardId == "EX1_010"))
					{
						goto IL_4ED;
					}
				}
				else if (!(cardId == "OG_247"))
				{
					goto IL_4ED;
				}
			}
			else if (!(cardId == "OG_295"))
			{
				goto IL_4ED;
			}
		}
		else if (num != 3176741322U)
		{
			if (num != 3250158172U)
			{
				if (num != 4238288290U)
				{
					goto IL_4ED;
				}
				if (!(cardId == "GILA_412"))
				{
					goto IL_4ED;
				}
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_46h_Male_Worgen_EventPlayBloodMoon_01.prefab:95888d40608ed4344a19e1f276275a59", 2.5f);
				goto IL_4ED;
			}
			else if (!(cardId == "KAR_005a"))
			{
				goto IL_4ED;
			}
		}
		else
		{
			if (!(cardId == "GILA_851a"))
			{
				goto IL_4ED;
			}
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_46h_Male_Worgen_EventPlayTrapper_02.prefab:0f49f185267a7144689d07bf20405e02", 2.5f);
			goto IL_4ED;
		}
		string text = base.PopRandomLineWithChance(this.m_RandomLines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		IL_4ED:
		yield break;
	}

	// Token: 0x04001DB7 RID: 7607
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DB8 RID: 7608
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_01.prefab:7e95c21db5f63094bb9078b4c11d3a32",
		"VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_03.prefab:d6abf1d6660959147af580da61058831",
		"VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_06.prefab:bb4338cac09ab744a8c9ce0d3822dd5a"
	};
}
