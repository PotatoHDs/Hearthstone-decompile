using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E7 RID: 999
public class GIL_Dungeon_Boss_30h : GIL_Dungeon
{
	// Token: 0x060037CF RID: 14287 RVA: 0x0011A75C File Offset: 0x0011895C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_30h_Female_Gnome_Intro_01.prefab:8299ef8185f5d9545af06156ce8b6910",
			"VO_GILA_BOSS_30h_Female_Gnome_EmoteResponse_01.prefab:ddb12b11b028f9a478e49fde4d26b989",
			"VO_GILA_BOSS_30h_Female_Gnome_Death_01.prefab:e5758ee593be0dd428b09d57d53b6e16",
			"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_01.prefab:c9780cba16cd06445bd6e7e2e0efa1e8",
			"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_02.prefab:e1de4eca234ede04abc7dceb210b2862",
			"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_04.prefab:7743ea933ef269646a76c8890e2f6bea",
			"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_05.prefab:abb18a13f759d1f469c411f336f8745f",
			"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_06.prefab:7229b20970f6a404c915407bec978e01"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037D0 RID: 14288 RVA: 0x0011A80C File Offset: 0x00118A0C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_30h_Female_Gnome_Intro_01.prefab:8299ef8185f5d9545af06156ce8b6910", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_30h_Female_Gnome_EmoteResponse_01.prefab:ddb12b11b028f9a478e49fde4d26b989", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037D1 RID: 14289 RVA: 0x0011A893 File Offset: 0x00118A93
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_30h_Female_Gnome_Death_01.prefab:e5758ee593be0dd428b09d57d53b6e16";
	}

	// Token: 0x060037D2 RID: 14290 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060037D3 RID: 14291 RVA: 0x0011A89A File Offset: 0x00118A9A
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
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (entity.IsSpell())
		{
			string text = base.PopRandomLineWithChance(this.m_RandomHeroLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060037D4 RID: 14292 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060037D5 RID: 14293 RVA: 0x0011A8B0 File Offset: 0x00118AB0
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
		if (entity.IsSpell())
		{
			string text = base.PopRandomLineWithChance(this.m_RandomPlayerLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001D96 RID: 7574
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D97 RID: 7575
	private List<string> m_RandomPlayerLines = new List<string>
	{
		"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_01.prefab:c9780cba16cd06445bd6e7e2e0efa1e8",
		"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_02.prefab:e1de4eca234ede04abc7dceb210b2862",
		"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_06.prefab:7229b20970f6a404c915407bec978e01"
	};

	// Token: 0x04001D98 RID: 7576
	private List<string> m_RandomHeroLines = new List<string>
	{
		"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_04.prefab:7743ea933ef269646a76c8890e2f6bea",
		"VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_05.prefab:abb18a13f759d1f469c411f336f8745f"
	};
}
