using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_37h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_LOOTA_BOSS_37h_Female_BloodElf_Intro_01.prefab:787f8e4723194de4bba60f2d80035ad0", "VO_LOOTA_BOSS_37h_Female_BloodElf_EmoteResponse_01.prefab:636239635d202b042a286ad844eda901", "VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower1_01.prefab:d957c04a8421ee64fbc9582e3ae989d2", "VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower2_01.prefab:e88460c151b62a643845ede7fb25a474", "VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower3_01.prefab:160676fd299d6154297898fbe0aa1993", "VO_LOOTA_BOSS_37h_Female_BloodElf_Flamewakers_01.prefab:f14f69393caf7f0458dfa3e3d3539e10", "VO_LOOTA_BOSS_37h_Female_BloodElf_Death_01.prefab:8b8ca1919aee09e4f8c64d358e019808", "VO_LOOTA_BOSS_37h_Female_BloodElf_DefeatPlayer_01.prefab:21c8ee36b60e4c84f8664d3303427dc6", "VO_LOOTA_BOSS_37h_Female_BloodElf_EventTheCandle_01.prefab:2674d9632f2b16245b8febb2a27e6551", "VO_LOOTA_BOSS_37h_Female_BloodElf_EventPyroblast_01.prefab:3104b792fda960c43a9ae857d8afeaaf",
			"VO_LOOTA_BOSS_37h_Female_BloodElf_EventFireball_01.prefab:51c0b0ac775aee44b9c3d9bcf321e35e", "VO_LOOTA_BOSS_37h_Female_BloodElf_EventRagnaros_01.prefab:52e35fb26e37aec4084d826f2a12e95c"
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "BRM_002")
			{
				yield return PlayLineOnlyOnce(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_Flamewakers_01.prefab:f14f69393caf7f0458dfa3e3d3539e10");
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower1_01.prefab:d957c04a8421ee64fbc9582e3ae989d2", "VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower2_01.prefab:e88460c151b62a643845ede7fb25a474", "VO_LOOTA_BOSS_37h_Female_BloodElf_HeroPower3_01.prefab:160676fd299d6154297898fbe0aa1993" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_37h_Female_BloodElf_Death_01.prefab:8b8ca1919aee09e4f8c64d358e019808";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_37h_Female_BloodElf_Intro_01.prefab:787f8e4723194de4bba60f2d80035ad0", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_37h_Female_BloodElf_EmoteResponse_01.prefab:636239635d202b042a286ad844eda901", Notification.SpeechBubbleDirection.TopRight, actor));
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
			case "LOOTA_843":
				yield return PlayLineOnlyOnce(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventTheCandle_01.prefab:2674d9632f2b16245b8febb2a27e6551");
				break;
			case "EX1_279":
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventPyroblast_01.prefab:3104b792fda960c43a9ae857d8afeaaf");
				break;
			case "CS2_029":
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventFireball_01.prefab:51c0b0ac775aee44b9c3d9bcf321e35e");
				break;
			case "EX1_298":
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_37h_Female_BloodElf_EventRagnaros_01.prefab:52e35fb26e37aec4084d826f2a12e95c");
				break;
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
