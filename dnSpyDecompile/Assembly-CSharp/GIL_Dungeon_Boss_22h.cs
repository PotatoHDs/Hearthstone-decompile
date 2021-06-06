using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E0 RID: 992
public class GIL_Dungeon_Boss_22h : GIL_Dungeon
{
	// Token: 0x0600378F RID: 14223 RVA: 0x00119640 File Offset: 0x00117840
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_22h_Male_Undead_Intro_01.prefab:e2f409541d775204ea830c91c59b0fb8",
			"VO_GILA_BOSS_22h_Male_Undead_IntroTess_01.prefab:8c903084a02c75e47aa40f564b8f536a",
			"VO_GILA_BOSS_22h_Male_Undead_IntroCrowley_01.prefab:dacced81dd2ca414c89fe4e1700ede67",
			"VO_GILA_BOSS_22h_Male_Undead_EmoteResponse_01.prefab:35d13b301743d6b41b010da858bb9100",
			"VO_GILA_BOSS_22h_Male_Undead_EmoteResponseTess_01.prefab:54dd324d38e93764a8370741a5faf644",
			"VO_GILA_BOSS_22h_Male_Undead_Death_01.prefab:61c5f65e28a99f84d9d6219edf4ba769",
			"VO_GILA_BOSS_22h_Male_Undead_DefeatPlayer_01.prefab:54ef28245d1c78d4f988e652b352ccf1",
			"VO_GILA_BOSS_22h_Male_Undead_EventPlaysShiv_01.prefab:3b8e6220e4f4dee4dade6ae967cdd8f5",
			"VO_GILA_BOSS_22h_Male_Undead_EventPlaysCoin_01.prefab:2cf7e803e0a855149b7152e53e84ffc6"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003790 RID: 14224 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003791 RID: 14225 RVA: 0x001196FC File Offset: 0x001178FC
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_22h_Male_Undead_Death_01.prefab:61c5f65e28a99f84d9d6219edf4ba769";
	}

	// Token: 0x06003792 RID: 14226 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003793 RID: 14227 RVA: 0x00119704 File Offset: 0x00117904
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
			if (cardId == "GILA_500h3")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_IntroTess_01.prefab:8c903084a02c75e47aa40f564b8f536a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (!(cardId == "GILA_600h"))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_Intro_01.prefab:e2f409541d775204ea830c91c59b0fb8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_IntroCrowley_01.prefab:dacced81dd2ca414c89fe4e1700ede67", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				return;
			}
			string cardId2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
			if (cardId2 == "GILA_500h3")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_EmoteResponseTess_01.prefab:54dd324d38e93764a8370741a5faf644", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_22h_Male_Undead_EmoteResponse_01.prefab:35d13b301743d6b41b010da858bb9100", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003794 RID: 14228 RVA: 0x0011985F File Offset: 0x00117A5F
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
		if (!(cardId == "GAME_005") && !(cardId == "CFM_630"))
		{
			if (cardId == "EX1_278")
			{
				yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_22h_Male_Undead_EventPlaysShiv_01.prefab:3b8e6220e4f4dee4dade6ae967cdd8f5", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_22h_Male_Undead_EventPlaysCoin_01.prefab:2cf7e803e0a855149b7152e53e84ffc6", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D89 RID: 7561
	private HashSet<string> m_playedLines = new HashSet<string>();
}
