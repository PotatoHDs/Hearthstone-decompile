using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_67h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_67h_Male_Undead_Intro_01.prefab:77315cc1645828346beace1a75cad6eb", "VO_GILA_BOSS_67h_Male_Undead_EmoteResponse_01.prefab:2916a45adc35de04eb7c232e050bfaf9", "VO_GILA_BOSS_67h_Male_Undead_Death_01.prefab:ec969a349b58f10419abaccd73d282dd", "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_01.prefab:33a73764b8071f94eaf228ea7be402c9", "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_02.prefab:f28d93840a2669543aa292b9de1c594f", "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_03.prefab:226b571d460839248aaae37de16b2d95", "VO_GILA_BOSS_67h_Male_Undead_EventPlayRatTrap_01.prefab:3c24f336ee2317746be5b165d1409345" })
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_67h_Male_Undead_Death_01.prefab:ec969a349b58f10419abaccd73d282dd";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_67h_Male_Undead_Intro_01.prefab:77315cc1645828346beace1a75cad6eb", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_67h_Male_Undead_EmoteResponse_01.prefab:2916a45adc35de04eb7c232e050bfaf9", Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "CFM_316":
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_02.prefab:f28d93840a2669543aa292b9de1c594f");
				break;
			case "CFM_790":
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_03.prefab:226b571d460839248aaae37de16b2d95");
				break;
			case "LOOT_069":
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRat_01.prefab:33a73764b8071f94eaf228ea7be402c9");
				break;
			case "GIL_577":
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_67h_Male_Undead_EventPlayRatTrap_01.prefab:3c24f336ee2317746be5b165d1409345");
				break;
			}
		}
	}
}
