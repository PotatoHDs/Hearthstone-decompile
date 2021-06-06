using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F1 RID: 1009
public class GIL_Dungeon_Boss_40h : GIL_Dungeon
{
	// Token: 0x06003828 RID: 14376 RVA: 0x0011BAC4 File Offset: 0x00119CC4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_40h_Female_Treant_Intro_01.prefab:5d14b06a592ef5640ae7acbdb74c031a",
			"VO_GILA_BOSS_40h_Female_Treant_EmoteResponse_01.prefab:1252f452e179aea4c97325973e1c3dc4",
			"VO_GILA_BOSS_40h_Female_Treant_Death_01.prefab:14e1eb2eba09ae74eafa6c80011b74bb",
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_01.prefab:3a425a60e1d4a9c4dbc092e0a82bbf20",
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_02.prefab:ba115807ed6f853428c7742cdb023f39",
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_03.prefab:3c769fa8be22e194ca01135dd8169999",
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_04.prefab:0add75f1f2992354b89863a7b0672469",
			"VO_GILA_BOSS_40h_Female_Treant_EventPlaysWoodsmansAxe_01.prefab:f765de0ef498d5f48998db8dac4ff08a",
			"VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_01.prefab:fb8468b00a5f0cc428dfcfce728fa042",
			"VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_02.prefab:0a5c52faccaf8c5489a2da55a4e3590f"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003829 RID: 14377 RVA: 0x0011BB8C File Offset: 0x00119D8C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600382A RID: 14378 RVA: 0x0011BBA2 File Offset: 0x00119DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_01.prefab:3a425a60e1d4a9c4dbc092e0a82bbf20",
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_02.prefab:ba115807ed6f853428c7742cdb023f39",
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_03.prefab:3c769fa8be22e194ca01135dd8169999",
			"VO_GILA_BOSS_40h_Female_Treant_HeroPower_04.prefab:0add75f1f2992354b89863a7b0672469"
		};
	}

	// Token: 0x0600382B RID: 14379 RVA: 0x0011BBD5 File Offset: 0x00119DD5
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_40h_Female_Treant_Death_01.prefab:14e1eb2eba09ae74eafa6c80011b74bb";
	}

	// Token: 0x0600382C RID: 14380 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600382D RID: 14381 RVA: 0x0011BBDC File Offset: 0x00119DDC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_40h_Female_Treant_Intro_01.prefab:5d14b06a592ef5640ae7acbdb74c031a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_40h_Female_Treant_EmoteResponse_01.prefab:1252f452e179aea4c97325973e1c3dc4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600382E RID: 14382 RVA: 0x0011BC63 File Offset: 0x00119E63
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
		if (num <= 2143680583U)
		{
			if (num <= 1386644598U)
			{
				if (num != 860822576U)
				{
					if (num != 1386644598U)
					{
						goto IL_286;
					}
					if (!(cardId == "ICC_236"))
					{
						goto IL_286;
					}
				}
				else if (!(cardId == "FP1_021"))
				{
					goto IL_286;
				}
			}
			else if (num != 1900257086U)
			{
				if (num != 2143680583U)
				{
					goto IL_286;
				}
				if (!(cardId == "EX1_247"))
				{
					goto IL_286;
				}
			}
			else if (!(cardId == "EX1_411"))
			{
				goto IL_286;
			}
		}
		else if (num <= 3484289697U)
		{
			if (num != 2169873006U)
			{
				if (num != 3484289697U)
				{
					goto IL_286;
				}
				if (!(cardId == "LOOT_380"))
				{
					goto IL_286;
				}
			}
			else
			{
				if (!(cardId == "GIL_653"))
				{
					goto IL_286;
				}
				yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_40h_Female_Treant_EventPlaysWoodsmansAxe_01.prefab:f765de0ef498d5f48998db8dac4ff08a", 2.5f);
				goto IL_286;
			}
		}
		else if (num != 3723735224U)
		{
			if (num != 3824548033U)
			{
				if (num != 3866761196U)
				{
					goto IL_286;
				}
				if (!(cardId == "EX1_398"))
				{
					goto IL_286;
				}
			}
			else if (!(cardId == "CS2_106"))
			{
				goto IL_286;
			}
		}
		else if (!(cardId == "CS2_112"))
		{
			goto IL_286;
		}
		string text = base.PopRandomLineWithChance(this.m_PlayerAxe);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		IL_286:
		yield break;
	}

	// Token: 0x04001DAA RID: 7594
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DAB RID: 7595
	private List<string> m_PlayerAxe = new List<string>
	{
		"VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_01.prefab:fb8468b00a5f0cc428dfcfce728fa042",
		"VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_02.prefab:0a5c52faccaf8c5489a2da55a4e3590f"
	};
}
