using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003EA RID: 1002
public class GIL_Dungeon_Boss_33h : GIL_Dungeon
{
	// Token: 0x060037E8 RID: 14312 RVA: 0x0011ACD8 File Offset: 0x00118ED8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_33h_Male_Mech_Intro_01.prefab:0511c22148db56a448c2e55a8930a644",
			"VO_GILA_BOSS_33h_Male_Mech_EmoteResponse_01.prefab:e76887ef139b3454f952a8d666c627ef",
			"VO_GILA_BOSS_33h_Male_Mech_Death_01.prefab:88c829f9fdf7791458a9ff6540f55b8a",
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_01.prefab:82aaf3e22b386c14b97f9ce229d06df1",
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_02.prefab:f8a6c89b84d011044b93570ca4e706b7",
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_03.prefab:ea6cc1b345bc2b54196d189c4923d4eb",
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_04.prefab:72bfd202931ce5346bec2e2e18460aa8",
			"VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_01.prefab:37a31f0f76dfdf64ea7758fe65286dcf",
			"VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_02.prefab:f031f0ad30432914faebc7910e4b8dfc",
			"VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_03.prefab:088df2cc43594914f978f3cd6ffe4936",
			"VO_GILA_BOSS_33h_Male_Mech_Attack_01.prefab:004b23d48756f1c4faf510e064804b5a",
			"VO_GILA_BOSS_33h_Male_Mech_EventPlayHarvestGolem_01.prefab:2ffa7bb43bb5bc34ca7b76f0a8f05e8f"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037E9 RID: 14313 RVA: 0x0011ADB4 File Offset: 0x00118FB4
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060037EA RID: 14314 RVA: 0x0011ADCA File Offset: 0x00118FCA
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_01.prefab:82aaf3e22b386c14b97f9ce229d06df1",
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_02.prefab:f8a6c89b84d011044b93570ca4e706b7",
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_03.prefab:ea6cc1b345bc2b54196d189c4923d4eb",
			"VO_GILA_BOSS_33h_Male_Mech_HeroPower_04.prefab:72bfd202931ce5346bec2e2e18460aa8"
		};
	}

	// Token: 0x060037EB RID: 14315 RVA: 0x0011ADFD File Offset: 0x00118FFD
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_33h_Male_Mech_Death_01.prefab:88c829f9fdf7791458a9ff6540f55b8a";
	}

	// Token: 0x060037EC RID: 14316 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x060037ED RID: 14317 RVA: 0x0011AE04 File Offset: 0x00119004
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_33h_Male_Mech_Intro_01.prefab:0511c22148db56a448c2e55a8930a644", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_33h_Male_Mech_EmoteResponse_01.prefab:e76887ef139b3454f952a8d666c627ef", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037EE RID: 14318 RVA: 0x0011AE8B File Offset: 0x0011908B
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
		if (cardId == "EX1_556")
		{
			yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_33h_Male_Mech_EventPlayHarvestGolem_01.prefab:2ffa7bb43bb5bc34ca7b76f0a8f05e8f", 2.5f);
		}
		yield break;
	}

	// Token: 0x060037EF RID: 14319 RVA: 0x0011AEA1 File Offset: 0x001190A1
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
				string text = base.PopRandomLineWithChance(this.m_WeaponKillLines);
				if (text != null)
				{
					yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
				}
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_33h_Male_Mech_Attack_01.prefab:004b23d48756f1c4faf510e064804b5a", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D9B RID: 7579
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D9C RID: 7580
	private List<string> m_WeaponKillLines = new List<string>
	{
		"VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_01.prefab:37a31f0f76dfdf64ea7758fe65286dcf",
		"VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_02.prefab:f031f0ad30432914faebc7910e4b8dfc",
		"VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_03.prefab:088df2cc43594914f978f3cd6ffe4936"
	};
}
