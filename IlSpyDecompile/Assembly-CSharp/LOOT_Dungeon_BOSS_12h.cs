using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_12h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_IntroLines = new List<string> { "VO_LOOTA_BOSS_12h_Male_Kobold_Intro1_01.prefab:46cb2187ee29a324580ee8107c7c3c0a", "VO_LOOTA_BOSS_12h_Male_Kobold_Intro2_01.prefab:0ead7cf01e448f24aae1b25b7d633d99", "VO_LOOTA_BOSS_12h_Male_Kobold_Intro3_01.prefab:f52f03fbf2a9b4542aafb456236240ac" };

	private List<string> m_DeathLines = new List<string> { "VO_LOOTA_BOSS_12h_Male_Kobold_Death1_01.prefab:ed87ca80573e2ab46afa1b7ecd055682", "VO_LOOTA_BOSS_12h_Male_Kobold_Death2_01.prefab:ed9c247de5783ca4ba252280da1ec02b", "VO_LOOTA_BOSS_12h_Male_Kobold_Death3_01.prefab:cc139c64563141a4299d25958b9e8af4" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_LOOTA_BOSS_12h_Male_Kobold_Intro1_01.prefab:46cb2187ee29a324580ee8107c7c3c0a", "VO_LOOTA_BOSS_12h_Male_Kobold_Intro2_01.prefab:0ead7cf01e448f24aae1b25b7d633d99", "VO_LOOTA_BOSS_12h_Male_Kobold_Intro3_01.prefab:f52f03fbf2a9b4542aafb456236240ac", "VO_LOOTA_BOSS_12h_Male_Kobold_EmoteResponse_01.prefab:8114296dd0a4eb742837f7147c34e37a", "VO_LOOTA_BOSS_12h_Male_Kobold_Death1_01.prefab:ed87ca80573e2ab46afa1b7ecd055682", "VO_LOOTA_BOSS_12h_Male_Kobold_Death2_01.prefab:ed9c247de5783ca4ba252280da1ec02b", "VO_LOOTA_BOSS_12h_Male_Kobold_Death3_01.prefab:cc139c64563141a4299d25958b9e8af4", "VO_LOOTA_BOSS_12h_Male_Kobold_DefeatPlayer_01.prefab:51813afc620f6db4cafa3e8bc65aef20", "VO_LOOTA_BOSS_12h_Male_Kobold_EventChargeBigMinion_01.prefab:9029d30fc0c80ef419c1b763e46356db", "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysPatches_01.prefab:25aab388198063f47b882b311169cece",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysCrab_01.prefab:1e4208a2ac0cc584292fe3375d0145c7"
		})
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			string soundPath = m_IntroLines[Random.Range(0, m_IntroLines.Count)];
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(soundPath, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_12h_Male_Kobold_EmoteResponse_01.prefab:8114296dd0a4eb742837f7147c34e37a", Notification.SpeechBubbleDirection.TopRight, actor));
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
			case 101:
			{
				string line = m_DeathLines[Random.Range(0, m_DeathLines.Count)];
				yield return PlayBossLine(enemyActor, line);
				break;
			}
			case 102:
				yield return PlayBossLine(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventChargeBigMinion_01.prefab:9029d30fc0c80ef419c1b763e46356db");
				break;
			case 103:
				yield return new WaitForSeconds(4.5f);
				yield return PlayBossLine(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysPatches_01.prefab:25aab388198063f47b882b311169cece");
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
			if (cardId == "UNG_807")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysCrab_01.prefab:1e4208a2ac0cc584292fe3375d0145c7");
			}
		}
	}
}
