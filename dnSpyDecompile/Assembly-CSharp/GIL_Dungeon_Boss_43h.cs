using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F4 RID: 1012
public class GIL_Dungeon_Boss_43h : GIL_Dungeon
{
	// Token: 0x06003842 RID: 14402 RVA: 0x0011C068 File Offset: 0x0011A268
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_43h_Male_HumanGhost_Intro_01.prefab:cc1f4c2e247ff014986c89a0edf75101",
			"VO_GILA_BOSS_43h_Male_HumanGhost_EmoteResponse_01.prefab:33914b71d09661e4eb62aa201dc8c4a1",
			"VO_GILA_BOSS_43h_Male_HumanGhost_Death_01.prefab:7efb42380fd966048857eb57a899eea1",
			"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_01.prefab:d273a733217d81e4d95597c70755a893",
			"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_02.prefab:3b08e00d2aea68747a83fd9a0c2d2ecc",
			"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_03.prefab:624016a4f79e249499dff4ae16d1b28b",
			"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_04.prefab:617ed484b0a362e4ab35ba1de9bf310e",
			"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_05.prefab:7a885df5d3001b144aaeba324620d575",
			"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlayStrokeOfMidnight_01.prefab:b92bec975abfc624d9436bbf8702e6c4"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003843 RID: 14403 RVA: 0x0011C124 File Offset: 0x0011A324
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_43h_Male_HumanGhost_Intro_01.prefab:cc1f4c2e247ff014986c89a0edf75101", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_43h_Male_HumanGhost_EmoteResponse_01.prefab:33914b71d09661e4eb62aa201dc8c4a1", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003844 RID: 14404 RVA: 0x0011C1AB File Offset: 0x0011A3AB
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_43h_Male_HumanGhost_Death_01.prefab:7efb42380fd966048857eb57a899eea1";
	}

	// Token: 0x06003845 RID: 14405 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003846 RID: 14406 RVA: 0x0011C1B2 File Offset: 0x0011A3B2
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003847 RID: 14407 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003848 RID: 14408 RVA: 0x0011C1C8 File Offset: 0x0011A3C8
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "GILA_904")
		{
			yield return base.PlayBossLine(enemyActor, "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlayStrokeOfMidnight_01.prefab:b92bec975abfc624d9436bbf8702e6c4", 2.5f);
		}
		if (entity.HasTag(GAME_TAG.DEATHRATTLE))
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DB0 RID: 7600
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DB1 RID: 7601
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_01.prefab:d273a733217d81e4d95597c70755a893",
		"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_02.prefab:3b08e00d2aea68747a83fd9a0c2d2ecc",
		"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_03.prefab:624016a4f79e249499dff4ae16d1b28b",
		"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_04.prefab:617ed484b0a362e4ab35ba1de9bf310e",
		"VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_05.prefab:7a885df5d3001b144aaeba324620d575"
	};
}
