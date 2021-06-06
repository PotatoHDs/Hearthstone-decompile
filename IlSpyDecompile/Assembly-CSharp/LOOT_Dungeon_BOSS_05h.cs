using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_05h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_05h_Male_Kobold_Intro_01.prefab:e1c7c45689b6591498aa76348e5a70f4", "VO_LOOTA_BOSS_05h_Male_Kobold_EmoteResponse_01.prefab:2934b191f5c48084f8ca1946cd1c12af", "VO_LOOTA_BOSS_05h_Male_Kobold_Death_01.prefab:71eccd84e3ff7c548bbdf11518c4bd77", "VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower1_01.prefab:f658526e24d9d9c4a9e69ac192e1b6da", "VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower2_01.prefab:767c3f76ae6a5404c9453bee77f002df", "VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower3_01.prefab:753ada26bfe00f6419552fc39b17395b", "VO_LOOTA_BOSS_05h_Male_Kobold_DefeatPlayer_01.prefab:43c69f56c33d36d4f85a19f69e96f21f", "VO_LOOTA_BOSS_05h_Male_Kobold_EventBoomBots_01.prefab:3c88fbc01e9f5554282c392c82596c29", "VO_LOOTA_BOSS_05h_Male_Kobold_EventMadBomberDeath_01.prefab:cd159b7aa0612634ebd47d147e729157" })
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
		return new List<string> { "VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower1_01.prefab:f658526e24d9d9c4a9e69ac192e1b6da", "VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower2_01.prefab:767c3f76ae6a5404c9453bee77f002df", "VO_LOOTA_BOSS_05h_Male_Kobold_HeroPower3_01.prefab:753ada26bfe00f6419552fc39b17395b" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_05h_Male_Kobold_Death_01.prefab:71eccd84e3ff7c548bbdf11518c4bd77";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_05h_Male_Kobold_Intro_01.prefab:e1c7c45689b6591498aa76348e5a70f4", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_05h_Male_Kobold_EmoteResponse_01.prefab:2934b191f5c48084f8ca1946cd1c12af", Notification.SpeechBubbleDirection.TopRight, actor));
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
			if (missionEvent == 102)
			{
				yield return PlayBossLine(enemyActor, "VO_LOOTA_BOSS_05h_Male_Kobold_EventMadBomberDeath_01.prefab:cd159b7aa0612634ebd47d147e729157");
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
			if (cardId == "LOOTA_838")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_05h_Male_Kobold_EventBoomBots_01.prefab:3c88fbc01e9f5554282c392c82596c29");
			}
		}
	}
}
