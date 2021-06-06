using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_13h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_13h_Female_Undead_EmoteResponse_01.prefab:99ea4fd615762c644b3ae60af1e4b243", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower_01.prefab:e8b84e5da14b8ed47b94319676eeefcb", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower1_01.prefab:26727f391e0a1b14dbe6001643171ba1", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower2_01.prefab:50b8e0d1b1009dd4298db0f84c345d41", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower3_01.prefab:df9624ba08ec5674c93d63a2c8fa1d5e", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower4_01.prefab:94843eb0d7581ef4cb38e5470e8ee0bf", "VO_LOOTA_BOSS_13h_Female_Undead_Death_01.prefab:f8adf94220a0f154c9e4ba5167b1d1d8", "VO_LOOTA_BOSS_13h_Female_Undead_DefeatPlayer_01.prefab:e9c4e87f8ad202148a3a94104bf0908c", "VO_LOOTA_BOSS_13h_Female_Undead_EventStealQuest_01.prefab:8c513562bebfa8f4986eafc9f6d224de", "VO_LOOTA_BOSS_13h_Female_Undead_EventBagOfCoins_01.prefab:1b354b2b9661b3b439da138ba82f1f68" })
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
		return new List<string> { "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower_01.prefab:e8b84e5da14b8ed47b94319676eeefcb", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower1_01.prefab:26727f391e0a1b14dbe6001643171ba1", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower2_01.prefab:50b8e0d1b1009dd4298db0f84c345d41", "VO_LOOTA_BOSS_13h_Female_Undead_HeroPower4_01.prefab:94843eb0d7581ef4cb38e5470e8ee0bf" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_13h_Female_Undead_Death_01.prefab:f8adf94220a0f154c9e4ba5167b1d1d8";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_13h_Female_Undead_HeroPower3_01.prefab:df9624ba08ec5674c93d63a2c8fa1d5e", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_13h_Female_Undead_EmoteResponse_01.prefab:99ea4fd615762c644b3ae60af1e4b243", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			yield return PlayLoyalSideKickBetrayal(missionEvent);
			if (missionEvent == 101)
			{
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_13h_Female_Undead_EventStealQuest_01.prefab:8c513562bebfa8f4986eafc9f6d224de");
			}
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
			if (cardId == "LOOTA_836")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_13h_Female_Undead_EventBagOfCoins_01.prefab:1b354b2b9661b3b439da138ba82f1f68");
				yield return null;
			}
		}
	}
}
