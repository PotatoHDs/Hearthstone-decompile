using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F5 RID: 1013
public class GIL_Dungeon_Boss_44h : GIL_Dungeon
{
	// Token: 0x0600384B RID: 14411 RVA: 0x0011C240 File Offset: 0x0011A440
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_44h_Male_Troll_Intro_01.prefab:9dd32ba7833dcfd44840ca37fba0b27b",
			"VO_GILA_BOSS_44h_Male_Troll_EmoteResponse_01.prefab:058769cea8ad45d4da1351f69ff6a0dd",
			"VO_GILA_BOSS_44h_Male_Troll_Death_01.prefab:a2acdebd26eb1374dab8e62b6677b3de",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_01.prefab:7b359f4c22676d147b26675bf420498b",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_02.prefab:2b67d15a14195374b8eba98db631eb66",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_03.prefab:ef80eeff5436caa4aa68f3c5f7958b2a",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_04.prefab:7f38acc3c246298458f4cb7a22908c98",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_05.prefab:92aa0d012ac47c844844b285d627cdd3",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_06.prefab:a78930d455cb72a46b71d1698f2a183b",
			"VO_GILA_BOSS_44h_Male_Troll_EventHeals_01.prefab:4ae1c7b1886eb7146afd565bd657a8f1",
			"VO_GILA_BOSS_44h_Male_Troll_EventHeals_02.prefab:426edecc3738b0141aaa43c0917cc5de",
			"VO_GILA_BOSS_44h_Male_Troll_EventHeals_04.prefab:aefa2dfc7947fc74595541542d4ad8c4",
			"VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_03.prefab:a1e7fa7c42a47d642a1cce3d848dcd89",
			"VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_04.prefab:de44a20eabddf8a47b8b7155505e7009",
			"VO_GILA_BOSS_44h_Male_Troll_EventPlaysBandages_01.prefab:a37d44a72287bbb48abe47ac1092dc61"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600384C RID: 14412 RVA: 0x0011C340 File Offset: 0x0011A540
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600384D RID: 14413 RVA: 0x0011C358 File Offset: 0x0011A558
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_01.prefab:7b359f4c22676d147b26675bf420498b",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_02.prefab:2b67d15a14195374b8eba98db631eb66",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_03.prefab:ef80eeff5436caa4aa68f3c5f7958b2a",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_04.prefab:7f38acc3c246298458f4cb7a22908c98",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_05.prefab:92aa0d012ac47c844844b285d627cdd3",
			"VO_GILA_BOSS_44h_Male_Troll_HeroPower_06.prefab:a78930d455cb72a46b71d1698f2a183b"
		};
	}

	// Token: 0x0600384E RID: 14414 RVA: 0x0011C3AC File Offset: 0x0011A5AC
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_44h_Male_Troll_Death_01.prefab:a2acdebd26eb1374dab8e62b6677b3de";
	}

	// Token: 0x0600384F RID: 14415 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003850 RID: 14416 RVA: 0x0011C3B4 File Offset: 0x0011A5B4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_44h_Male_Troll_Intro_01.prefab:9dd32ba7833dcfd44840ca37fba0b27b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_44h_Male_Troll_EmoteResponse_01.prefab:058769cea8ad45d4da1351f69ff6a0dd", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003851 RID: 14417 RVA: 0x0011C43B File Offset: 0x0011A63B
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
				string text = base.PopRandomLineWithChance(this.m_PlayerHeal);
				if (text != null)
				{
					yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
				}
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_BossHeal);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003852 RID: 14418 RVA: 0x0011C451 File Offset: 0x0011A651
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
		if (cardId == "GILA_506t")
		{
			yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_44h_Male_Troll_EventPlaysBandages_01.prefab:a37d44a72287bbb48abe47ac1092dc61", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001DB2 RID: 7602
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DB3 RID: 7603
	private List<string> m_BossHeal = new List<string>
	{
		"VO_GILA_BOSS_44h_Male_Troll_EventHeals_01.prefab:4ae1c7b1886eb7146afd565bd657a8f1",
		"VO_GILA_BOSS_44h_Male_Troll_EventHeals_02.prefab:426edecc3738b0141aaa43c0917cc5de",
		"VO_GILA_BOSS_44h_Male_Troll_EventHeals_04.prefab:aefa2dfc7947fc74595541542d4ad8c4"
	};

	// Token: 0x04001DB4 RID: 7604
	private List<string> m_PlayerHeal = new List<string>
	{
		"VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_03.prefab:a1e7fa7c42a47d642a1cce3d848dcd89",
		"VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_04.prefab:de44a20eabddf8a47b8b7155505e7009"
	};
}
