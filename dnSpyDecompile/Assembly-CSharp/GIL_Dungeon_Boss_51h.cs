using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003FC RID: 1020
public class GIL_Dungeon_Boss_51h : GIL_Dungeon
{
	// Token: 0x06003890 RID: 14480 RVA: 0x0011D2BC File Offset: 0x0011B4BC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_51h_Male_Undead_Intro_01.prefab:f62d6a72af299064b8e79ffae9ece1dc",
			"VO_GILA_BOSS_51h_Male_Undead_EmoteResponse_01.prefab:5a733778d0078834e8c3610dcf4c9279",
			"VO_GILA_BOSS_51h_Male_Undead_Death_01.prefab:50b65f13e26e81544b99e25ac9af8264",
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_01.prefab:e54b7a566f812674cb5a060c12c44580",
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_02.prefab:69e0f8122944f2b458578c005e70de19",
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_03.prefab:4231df7278f04304b959c2bd30b2de92",
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_04.prefab:dc48451b3ab43f24b8956ca0d99c6cfc",
			"VO_GILA_BOSS_51h_Male_Undead_EventPlayMinion_01.prefab:3ac948e98a418e445bcd6c1e1a12d190",
			"VO_GILA_BOSS_51h_Male_Undead_EventPlayMinion_03.prefab:78ec78c78bd8d5a4f9542ca810d780bb",
			"VO_GILA_BOSS_51h_Male_Undead_EventPlayMinion_04.prefab:37cccdba1fae2e24291aab3773c1e2fa",
			"VO_GILA_BOSS_51h_Male_Undead_EventPlayCoinPouch_01.prefab:7cb96fee789ac8044994fbabb6061db7"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003891 RID: 14481 RVA: 0x0011D390 File Offset: 0x0011B590
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003892 RID: 14482 RVA: 0x0011D3A6 File Offset: 0x0011B5A6
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_01.prefab:e54b7a566f812674cb5a060c12c44580",
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_02.prefab:69e0f8122944f2b458578c005e70de19",
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_03.prefab:4231df7278f04304b959c2bd30b2de92",
			"VO_GILA_BOSS_51h_Male_Undead_HeroPower_04.prefab:dc48451b3ab43f24b8956ca0d99c6cfc"
		};
	}

	// Token: 0x06003893 RID: 14483 RVA: 0x0011D3D9 File Offset: 0x0011B5D9
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_51h_Male_Undead_Death_01.prefab:50b65f13e26e81544b99e25ac9af8264";
	}

	// Token: 0x06003894 RID: 14484 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003895 RID: 14485 RVA: 0x0011D3E0 File Offset: 0x0011B5E0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_51h_Male_Undead_Intro_01.prefab:f62d6a72af299064b8e79ffae9ece1dc", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_51h_Male_Undead_EmoteResponse_01.prefab:5a733778d0078834e8c3610dcf4c9279", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003896 RID: 14486 RVA: 0x0011D467 File Offset: 0x0011B667
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 101)
		{
			string text = base.PopRandomLineWithChance(this.m_MinionWarning);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003897 RID: 14487 RVA: 0x0011D47D File Offset: 0x0011B67D
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
		if (cardId == "GILA_816a")
		{
			yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_51h_Male_Undead_EventPlayCoinPouch_01.prefab:7cb96fee789ac8044994fbabb6061db7", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001DC8 RID: 7624
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DC9 RID: 7625
	private List<string> m_MinionWarning = new List<string>
	{
		"VO_GILA_BOSS_51h_Male_Undead_EventPlayMinion_01.prefab:3ac948e98a418e445bcd6c1e1a12d190",
		"VO_GILA_BOSS_51h_Male_Undead_EventPlayMinion_03.prefab:78ec78c78bd8d5a4f9542ca810d780bb",
		"VO_GILA_BOSS_51h_Male_Undead_EventPlayMinion_04.prefab:37cccdba1fae2e24291aab3773c1e2fa"
	};
}
