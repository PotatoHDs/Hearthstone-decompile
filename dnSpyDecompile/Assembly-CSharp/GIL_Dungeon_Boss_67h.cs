using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200040A RID: 1034
public class GIL_Dungeon_Boss_67h : GIL_Dungeon
{
	// Token: 0x06003911 RID: 14609 RVA: 0x0011EF20 File Offset: 0x0011D120
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_67h_Male_Undead_Intro_01.prefab:77315cc1645828346beace1a75cad6eb",
			"VO_GILA_BOSS_67h_Male_Undead_EmoteResponse_01.prefab:2916a45adc35de04eb7c232e050bfaf9",
			"VO_GILA_BOSS_67h_Male_Undead_Death_01.prefab:ec969a349b58f10419abaccd73d282dd",
			"VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_01.prefab:33a73764b8071f94eaf228ea7be402c9",
			"VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_02.prefab:f28d93840a2669543aa292b9de1c594f",
			"VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_03.prefab:226b571d460839248aaae37de16b2d95",
			"VO_GILA_BOSS_67h_Male_Undead_EventPlayRatTrap_01.prefab:3c24f336ee2317746be5b165d1409345"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003912 RID: 14610 RVA: 0x0011EFC8 File Offset: 0x0011D1C8
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003913 RID: 14611 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003914 RID: 14612 RVA: 0x0011EFDE File Offset: 0x0011D1DE
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_67h_Male_Undead_Death_01.prefab:ec969a349b58f10419abaccd73d282dd";
	}

	// Token: 0x06003915 RID: 14613 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003916 RID: 14614 RVA: 0x0011EFE8 File Offset: 0x0011D1E8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_67h_Male_Undead_Intro_01.prefab:77315cc1645828346beace1a75cad6eb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_67h_Male_Undead_EmoteResponse_01.prefab:2916a45adc35de04eb7c232e050bfaf9", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003917 RID: 14615 RVA: 0x0011F06F File Offset: 0x0011D26F
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
		if (!(cardId == "CFM_316"))
		{
			if (!(cardId == "CFM_790"))
			{
				if (!(cardId == "LOOT_069"))
				{
					if (cardId == "GIL_577")
					{
						yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRatTrap_01.prefab:3c24f336ee2317746be5b165d1409345", 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_01.prefab:33a73764b8071f94eaf228ea7be402c9", 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_03.prefab:226b571d460839248aaae37de16b2d95", 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_02.prefab:f28d93840a2669543aa292b9de1c594f", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001DE7 RID: 7655
	private HashSet<string> m_playedLines = new HashSet<string>();
}
