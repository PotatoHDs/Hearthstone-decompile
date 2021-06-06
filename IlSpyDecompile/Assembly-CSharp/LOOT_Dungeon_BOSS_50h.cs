using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_50h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_PotionLines = new List<string> { "VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion1_01.prefab:428104523b25fc942ad89ae89c35d115", "VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion2_01.prefab:176da33d9d001744ab57ee85712cfda2", "VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion3_01.prefab:2cddc40945b23004cb090b4743df341c" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_LOOTA_BOSS_50h_Male_Kobold_Intro_01.prefab:f80b5a28ce912c04db88d22e5d244240", "VO_LOOTA_BOSS_50h_Male_Kobold_EmoteResponse_01.prefab:30cf49b490a303546b11d21a5dbb8a7d", "VO_LOOTA_BOSS_50h_Male_Kobold_Death_01.prefab:4f12276a9c8423549a6682a4780c4516", "VO_LOOTA_BOSS_50h_Male_Kobold_DefeatPlayer_01.prefab:ccf45106f55df1c4684277733fbd1c9c", "VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion1_01.prefab:5db49b0673cd3d2459fb17ca59c55209", "VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion2_01.prefab:b58956e82894ca94c8a573d24a7f52df", "VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion3_01.prefab:e7d8d43eef5ebe34f99c59ddac7ae3a0", "VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion1_01.prefab:428104523b25fc942ad89ae89c35d115", "VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion2_01.prefab:176da33d9d001744ab57ee85712cfda2", "VO_LOOTA_BOSS_50h_Male_Kobold_EventUsePotion3_01.prefab:2cddc40945b23004cb090b4743df341c",
			"VO_LOOTA_BOSS_50h_Male_Kobold_EventKazakus_01.prefab:37b855a910c4d6941b3dc2cb272c7501"
		})
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			string cardId = entity.GetCardId();
			if (cardId == "LOOTA_BOSS_50t" && m_PotionLines.Count != 0)
			{
				string randomLine = m_PotionLines[Random.Range(0, m_PotionLines.Count)];
				yield return PlayLineOnlyOnce(actor, randomLine);
				m_PotionLines.Remove(randomLine);
				yield return null;
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion1_01.prefab:5db49b0673cd3d2459fb17ca59c55209", "VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion2_01.prefab:b58956e82894ca94c8a573d24a7f52df", "VO_LOOTA_BOSS_50h_Male_Kobold_EventMakePotion3_01.prefab:e7d8d43eef5ebe34f99c59ddac7ae3a0" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_50h_Male_Kobold_Death_01.prefab:4f12276a9c8423549a6682a4780c4516";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_50h_Male_Kobold_Intro_01.prefab:f80b5a28ce912c04db88d22e5d244240", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_50h_Male_Kobold_EmoteResponse_01.prefab:30cf49b490a303546b11d21a5dbb8a7d", Notification.SpeechBubbleDirection.TopRight, actor));
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
			if (cardId == "CFM_621")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_50h_Male_Kobold_EventKazakus_01.prefab:37b855a910c4d6941b3dc2cb272c7501");
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
