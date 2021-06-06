using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B3 RID: 947
public class LOOT_Dungeon_BOSS_09h : LOOT_Dungeon
{
	// Token: 0x060035FC RID: 13820 RVA: 0x0011346C File Offset: 0x0011166C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_09h_Female_Furbolg_Intro_01.prefab:29cd999b72b06e0468d7bf599c86bd0b",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_EmoteResponse_01.prefab:21a1c7420686dbd4789868f49a5949e7",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower1_01.prefab:7ecdadc75755ce84bb13cdeaead21310",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower2_01.prefab:500be17bf66199247a02ab77b8798eff",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower3_01.prefab:45f7242c90e66c148946f4225399f143",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_Death_01.prefab:d7d94e2e925474b4bbcb7a61c0c6fdaf",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_DefeatPlayer_01.prefab:6391d6c1a88c90f4ab7fee2c553e700f",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic1_01.prefab:9173fbf2da9c2954e86a889ddfb1ee83",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic2_01.prefab:aff1ca3d359706243b7a2c678e4ddbca",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic3_01.prefab:f9ca28db284511b40ad78ed168e90d03"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x00113534 File Offset: 0x00111734
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060035FE RID: 13822 RVA: 0x0011354A File Offset: 0x0011174A
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower1_01.prefab:7ecdadc75755ce84bb13cdeaead21310",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower2_01.prefab:500be17bf66199247a02ab77b8798eff",
			"VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower3_01.prefab:45f7242c90e66c148946f4225399f143"
		};
	}

	// Token: 0x060035FF RID: 13823 RVA: 0x00113572 File Offset: 0x00111772
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_09h_Female_Furbolg_Death_01.prefab:d7d94e2e925474b4bbcb7a61c0c6fdaf";
	}

	// Token: 0x06003600 RID: 13824 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003601 RID: 13825 RVA: 0x0011357C File Offset: 0x0011177C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_09h_Female_Furbolg_Intro_01.prefab:29cd999b72b06e0468d7bf599c86bd0b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_09h_Female_Furbolg_EmoteResponse_01.prefab:21a1c7420686dbd4789868f49a5949e7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003602 RID: 13826 RVA: 0x00113603 File Offset: 0x00111803
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
		if (num <= 1235354129U)
		{
			if (num <= 598937748U)
			{
				if (num != 165618513U)
				{
					if (num != 598937748U)
					{
						goto IL_2D0;
					}
					if (!(cardId == "ICC_836"))
					{
						goto IL_2D0;
					}
				}
				else if (!(cardId == "CFM_021"))
				{
					goto IL_2D0;
				}
			}
			else if (num != 750656914U)
			{
				if (num != 985837770U)
				{
					if (num != 1235354129U)
					{
						goto IL_2D0;
					}
					if (!(cardId == "LOOTA_840"))
					{
						goto IL_2D0;
					}
				}
				else if (!(cardId == "ICC_078"))
				{
					goto IL_2D0;
				}
			}
			else if (!(cardId == "ICC_056"))
			{
				goto IL_2D0;
			}
		}
		else if (num <= 4056078886U)
		{
			if (num != 3928403510U)
			{
				if (num != 4056078886U)
				{
					goto IL_2D0;
				}
				if (!(cardId == "EX1_275"))
				{
					goto IL_2D0;
				}
			}
			else if (!(cardId == "CS2_028"))
			{
				goto IL_2D0;
			}
		}
		else if (num != 4096179700U)
		{
			if (num != 4113104414U)
			{
				if (num != 4129734938U)
				{
					goto IL_2D0;
				}
				if (!(cardId == "CS2_024"))
				{
					goto IL_2D0;
				}
			}
			else if (!(cardId == "CS2_037"))
			{
				goto IL_2D0;
			}
		}
		else if (!(cardId == "CS2_026"))
		{
			goto IL_2D0;
		}
		if (this.m_FrostLines.Count != 0)
		{
			string randomLine = this.m_FrostLines[UnityEngine.Random.Range(0, this.m_FrostLines.Count)];
			yield return base.PlayEasterEggLine(actor, randomLine, 2.5f);
			this.m_FrostLines.Remove(randomLine);
			yield return null;
			randomLine = null;
		}
		IL_2D0:
		yield break;
	}

	// Token: 0x06003603 RID: 13827 RVA: 0x00113619 File Offset: 0x00111819
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D21 RID: 7457
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D22 RID: 7458
	private List<string> m_FrostLines = new List<string>
	{
		"VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic1_01.prefab:9173fbf2da9c2954e86a889ddfb1ee83",
		"VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic2_01.prefab:aff1ca3d359706243b7a2c678e4ddbca",
		"VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic3_01.prefab:f9ca28db284511b40ad78ed168e90d03"
	};
}
