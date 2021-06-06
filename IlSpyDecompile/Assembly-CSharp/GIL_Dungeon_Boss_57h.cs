using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIL_Dungeon_Boss_57h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_HeroPowerLines = new List<string> { "VO_GILA_BOSS_57h_Male_Undead_HeroPower_01.prefab:40a54cd331253874b815acc206e8daad", "VO_GILA_BOSS_57h_Male_Undead_HeroPower_02.prefab:19834beb0c9d97948a9f9349a5e9f942", "VO_GILA_BOSS_57h_Male_Undead_HeroPower_03.prefab:ac8520ece59b43640ab5d94c025db144", "VO_GILA_BOSS_57h_Male_Undead_HeroPower_04.prefab:a3574b7c6077f0d4d868f5edc25bab91" };

	private List<string> m_ShallowGraveLines = new List<string> { "VO_GILA_BOSS_57h_Male_Undead_EventShallowGraves_01.prefab:576703a596d82e24a817a32a36e05eb0", "VO_GILA_BOSS_57h_Male_Undead_EventShallowGraves_02.prefab:64e3981780a5cb0439a7ff37f88d51c8", "VO_GILA_BOSS_57h_Male_Undead_EventShallowGraves_03.prefab:a2d3cb58e75d5c24783842e4ba8683ab" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_57h_Male_Undead_Intro_01.prefab:1a6f17bfc76f9394d9c8b14be96ab554", "VO_GILA_BOSS_57h_Male_Undead_EmoteResponse_01.prefab:97d1e8fe0b6eb4442ad6dd62c05b3c42", "VO_GILA_BOSS_57h_Male_Undead_Death_01.prefab:4dbfb6318f97dac409d3cada7ed27a04", "VO_GILA_BOSS_57h_Male_Undead_HeroPower_01.prefab:40a54cd331253874b815acc206e8daad", "VO_GILA_BOSS_57h_Male_Undead_HeroPower_02.prefab:19834beb0c9d97948a9f9349a5e9f942", "VO_GILA_BOSS_57h_Male_Undead_HeroPower_03.prefab:ac8520ece59b43640ab5d94c025db144", "VO_GILA_BOSS_57h_Male_Undead_HeroPower_04.prefab:a3574b7c6077f0d4d868f5edc25bab91", "VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_01.prefab:28b7a7c0c5932194c9980c623cc01749", "VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_02.prefab:f2a501707071b9748ba4dbf8acc5e9f7", "VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_03.prefab:84180e6550c55be41b1346590343db9f",
			"VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_04.prefab:71118bc8f937ac344a206a50a50908f8", "VO_GILA_BOSS_57h_Male_Undead_EventShallowGraves_01.prefab:576703a596d82e24a817a32a36e05eb0", "VO_GILA_BOSS_57h_Male_Undead_EventShallowGraves_02.prefab:64e3981780a5cb0439a7ff37f88d51c8", "VO_GILA_BOSS_57h_Male_Undead_EventShallowGraves_03.prefab:a2d3cb58e75d5c24783842e4ba8683ab", "VO_GILA_BOSS_57h_Male_Undead_EventPlayHolyBook_01.prefab:94d128a9b83ee8a4394ceba5bd81cd24", "VO_GIL_598_Female_Human_SpecialSylvanas_01.prefab:ef6fdd5d554c2864b867d3222641786c"
		})
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "GILA_BOSS_57t"))
		{
			if (cardId == "EX1_016")
			{
				m_playedLines.Add(cardId);
				if (GameState.Get().GetFriendlySidePlayer().GetHero()
					.GetCardId() == "GILA_500h3")
				{
					yield return new WaitForSeconds(2.8f);
					yield return PlayEasterEggLine(playerActor, "VO_GIL_598_Female_Human_SpecialSylvanas_01.prefab:ef6fdd5d554c2864b867d3222641786c");
				}
			}
		}
		else
		{
			string text = PopRandomLineWithChance(m_ShallowGraveLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_57h_Male_Undead_Death_01.prefab:4dbfb6318f97dac409d3cada7ed27a04";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_57h_Male_Undead_Intro_01.prefab:1a6f17bfc76f9394d9c8b14be96ab554", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_57h_Male_Undead_EmoteResponse_01.prefab:97d1e8fe0b6eb4442ad6dd62c05b3c42", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "GILA_BOSS_57t"))
		{
			if (cardId == "GILA_804")
			{
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_57h_Male_Undead_EventPlayHolyBook_01.prefab:94d128a9b83ee8a4394ceba5bd81cd24");
			}
			yield break;
		}
		string text = PopRandomLineWithChance(m_ShallowGraveLines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
		{
			string text = PopRandomLineWithChance(m_HeroPowerLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		case 102:
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_01.prefab:28b7a7c0c5932194c9980c623cc01749");
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_02.prefab:f2a501707071b9748ba4dbf8acc5e9f7");
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_03.prefab:84180e6550c55be41b1346590343db9f");
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_57h_Male_Undead_EventDesecrate_04.prefab:71118bc8f937ac344a206a50a50908f8");
			break;
		}
	}
}
