using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003C8 RID: 968
public class LOOT_Dungeon_BOSS_37h : LOOT_Dungeon
{
	// Token: 0x060036B5 RID: 14005 RVA: 0x00115AB0 File Offset: 0x00113CB0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_37h_Female_BloodElf_Intro_01.prefab:787f8e4723194de4bba60f2d80035ad0",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_EmoteResponse_01.prefab:636239635d202b042a286ad844eda901",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower1_01.prefab:d957c04a8421ee64fbc9582e3ae989d2",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower2_01.prefab:e88460c151b62a643845ede7fb25a474",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower3_01.prefab:160676fd299d6154297898fbe0aa1993",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_Flamewakers_01.prefab:f14f69393caf7f0458dfa3e3d3539e10",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_Death_01.prefab:8b8ca1919aee09e4f8c64d358e019808",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_DefeatPlayer_01.prefab:21c8ee36b60e4c84f8664d3303427dc6",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_EventTheCandle_01.prefab:2674d9632f2b16245b8febb2a27e6551",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_EventPyroblast_01.prefab:3104b792fda960c43a9ae857d8afeaaf",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_EventFireball_01.prefab:51c0b0ac775aee44b9c3d9bcf321e35e",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_EventRagnaros_01.prefab:52e35fb26e37aec4084d826f2a12e95c"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036B6 RID: 14006 RVA: 0x00115B8C File Offset: 0x00113D8C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "BRM_002")
		{
			yield return base.PlayLineOnlyOnce(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_Flamewakers_01.prefab:f14f69393caf7f0458dfa3e3d3539e10", 2.5f);
		}
		yield break;
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x00115BA2 File Offset: 0x00113DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower1_01.prefab:d957c04a8421ee64fbc9582e3ae989d2",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower2_01.prefab:e88460c151b62a643845ede7fb25a474",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower3_01.prefab:160676fd299d6154297898fbe0aa1993"
		};
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x00115BCA File Offset: 0x00113DCA
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_37h_Female_BloodElf_Death_01.prefab:8b8ca1919aee09e4f8c64d358e019808";
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x00115BD4 File Offset: 0x00113DD4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_37h_Female_BloodElf_Intro_01.prefab:787f8e4723194de4bba60f2d80035ad0", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_37h_Female_BloodElf_EmoteResponse_01.prefab:636239635d202b042a286ad844eda901", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x00115C5B File Offset: 0x00113E5B
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
		if (!(cardId == "LOOTA_843"))
		{
			if (!(cardId == "EX1_279"))
			{
				if (!(cardId == "CS2_029"))
				{
					if (cardId == "EX1_298")
					{
						yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventRagnaros_01.prefab:52e35fb26e37aec4084d826f2a12e95c", 2.5f);
					}
				}
				else
				{
					yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventFireball_01.prefab:51c0b0ac775aee44b9c3d9bcf321e35e", 2.5f);
				}
			}
			else
			{
				yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventPyroblast_01.prefab:3104b792fda960c43a9ae857d8afeaaf", 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventTheCandle_01.prefab:2674d9632f2b16245b8febb2a27e6551", 2.5f);
		}
		yield break;
	}

	// Token: 0x060036BB RID: 14011 RVA: 0x00115C71 File Offset: 0x00113E71
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D45 RID: 7493
	private HashSet<string> m_playedLines = new HashSet<string>();
}
