using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200040B RID: 1035
public class GIL_Dungeon_Boss_68h : GIL_Dungeon
{
	// Token: 0x0600391A RID: 14618 RVA: 0x0011F098 File Offset: 0x0011D298
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_68h_Male_Undead_Intro_01.prefab:d41182657f7477847966469d4eb6fc08",
			"VO_GILA_BOSS_68h_Male_Undead_EmoteResponse_01.prefab:e1dfb4ab0b0331a4abcb536c5896186a",
			"VO_GILA_BOSS_68h_Male_Undead_Death_01.prefab:75f7dabf0922e044aac6a4f8a7315238",
			"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_01.prefab:07927380484b26541be4b49e7a8aad33",
			"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_02.prefab:ef7a4e3aea34db34caa72aa721f6ee45",
			"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_03.prefab:dc750561f86b04b48bdc5e3516c6b41a",
			"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_04.prefab:057102807e5fe1943b8b8780ba1c37a3"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600391B RID: 14619 RVA: 0x0011F140 File Offset: 0x0011D340
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600391C RID: 14620 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600391D RID: 14621 RVA: 0x0011F156 File Offset: 0x0011D356
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_68h_Male_Undead_Death_01.prefab:75f7dabf0922e044aac6a4f8a7315238";
	}

	// Token: 0x0600391E RID: 14622 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600391F RID: 14623 RVA: 0x0011F160 File Offset: 0x0011D360
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_68h_Male_Undead_Intro_01.prefab:d41182657f7477847966469d4eb6fc08", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_68h_Male_Undead_EmoteResponse_01.prefab:e1dfb4ab0b0331a4abcb536c5896186a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003920 RID: 14624 RVA: 0x0011F1E7 File Offset: 0x0011D3E7
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
		if (entity.HasTag(GAME_TAG.POISONOUS))
		{
			string text = base.PopRandomLineWithChance(this.m_PoisonMinionLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DE8 RID: 7656
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DE9 RID: 7657
	private List<string> m_PoisonMinionLines = new List<string>
	{
		"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_01.prefab:07927380484b26541be4b49e7a8aad33",
		"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_02.prefab:ef7a4e3aea34db34caa72aa721f6ee45",
		"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_03.prefab:dc750561f86b04b48bdc5e3516c6b41a",
		"VO_GILA_BOSS_68h_Male_Undead_EventPlayPoison_04.prefab:057102807e5fe1943b8b8780ba1c37a3"
	};
}
