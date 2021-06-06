using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_04h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_04h_Female_Kobold_Intro1_01.prefab:1d9348a07de8c03449e6341941f594d3", "VO_LOOTA_BOSS_04h_Female_Kobold_Intro2_01.prefab:d7f7f1554e369f84889996b2859d90cc", "VO_LOOTA_BOSS_04h_Female_Kobold_EmoteResponse_01.prefab:f0813696aafb10c42a8d9977c77fde0e", "VO_LOOTA_BOSS_04h_Female_Kobold_Death_01.prefab:987462ddce876de4e8a90e7ae25db3b7", "VO_LOOTA_BOSS_04h_Female_Kobold_DefeatPlayer_01.prefab:0fc54a8945a6f734eaacd5fbcaadc032", "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOn_01.prefab:339d354892daf9c47bebb4a937270177", "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOff_01.prefab:f1b8d85236e3980459605d65dc0f5dbf", "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerPlay_01.prefab:a7c97e6d06c29c1499c3d3efc8d649aa", "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerRespawn_01.prefab:a1c84563c379d1445b71bb85d63505ec" })
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
		return "VO_LOOTA_BOSS_04h_Female_Kobold_Death_01.prefab:987462ddce876de4e8a90e7ae25db3b7";
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
			string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId();
			if (cardId == "LOOTA_BOSS_04h")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_04h_Female_Kobold_Intro2_01.prefab:d7f7f1554e369f84889996b2859d90cc", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			if (cardId == "LOOTA_BOSS_27h")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_04h_Female_Kobold_Intro1_01.prefab:1d9348a07de8c03449e6341941f594d3", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_04h_Female_Kobold_EmoteResponse_01.prefab:f0813696aafb10c42a8d9977c77fde0e", Notification.SpeechBubbleDirection.TopRight, actor));
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
			switch (missionEvent)
			{
			case 102:
				yield return PlayBossLine(enemyActor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOn_01.prefab:339d354892daf9c47bebb4a937270177");
				break;
			case 103:
				yield return PlayBossLine(enemyActor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxOff_01.prefab:f1b8d85236e3980459605d65dc0f5dbf");
				break;
			case 104:
				yield return PlayBossLine(enemyActor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerRespawn_01.prefab:a1c84563c379d1445b71bb85d63505ec");
				break;
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
			if (cardId == "LOOTA_840")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_04h_Female_Kobold_EventWaxRagerPlay_01.prefab:a7c97e6d06c29c1499c3d3efc8d649aa");
			}
		}
	}
}
