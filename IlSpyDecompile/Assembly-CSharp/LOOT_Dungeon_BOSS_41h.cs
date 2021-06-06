using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_41h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_41h_Male_Kobold_Intro_01.prefab:ce71aae59b0a07a4199833b254d82c66", "VO_LOOTA_BOSS_41h_Male_Kobold_EmoteResponse_01.prefab:09755fb2908eb1d4c8e93ae50ac2ee40", "VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower1_01.prefab:6647456515d002a41a5a75a7f0192a3d", "VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower2_01.prefab:f079bea51d0fa8d4e8c90da653fc948e", "VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower3_01.prefab:dc15ef4312d010b488aeb221f16e8c54", "VO_LOOTA_BOSS_41h_Male_Kobold_Death_01.prefab:a4488b2ad903b134d9493263fc696fc5", "VO_LOOTA_BOSS_41h_Male_Kobold_DefeatPlayer_01.prefab:30615acf7942fda49abf98714fac530e", "VO_LOOTA_BOSS_41h_Male_Kobold_EventHornOfCenarius_01.prefab:332370e137aa1144faf79fda6290edbd" })
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
		return new List<string> { "VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower1_01.prefab:6647456515d002a41a5a75a7f0192a3d", "VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower2_01.prefab:f079bea51d0fa8d4e8c90da653fc948e", "VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower3_01.prefab:dc15ef4312d010b488aeb221f16e8c54" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_41h_Male_Kobold_Death_01.prefab:a4488b2ad903b134d9493263fc696fc5";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_41h_Male_Kobold_Intro_01.prefab:ce71aae59b0a07a4199833b254d82c66", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_41h_Male_Kobold_EmoteResponse_01.prefab:09755fb2908eb1d4c8e93ae50ac2ee40", Notification.SpeechBubbleDirection.TopRight, actor));
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
			if (cardId == "LOOTA_837")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_41h_Male_Kobold_EventHornOfCenarius_01.prefab:332370e137aa1144faf79fda6290edbd");
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
	}
}
