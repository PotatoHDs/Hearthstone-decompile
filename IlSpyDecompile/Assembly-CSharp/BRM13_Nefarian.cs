using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM13_Nefarian : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private int m_ragLine;

	private Vector3 ragLinePosition = new Vector3(95f, NotificationManager.DEPTH, 36.8f);

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89");
		PreloadSound("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994");
		PreloadSound("VO_BRMA13_1_TURN1_PT2_03.prefab:dc72094fb84984f408d8d1ebf412d931");
		PreloadSound("VO_RAGNAROS_NEF1_71.prefab:195b5e3d6833ba54e887928cb7af3040");
		PreloadSound("VO_BRMA13_1_HP_PALADIN_07.prefab:a75bd560cd1e9b44192dc1aa2f59c489");
		PreloadSound("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb");
		PreloadSound("VO_BRMA13_1_HP_WARLOCK_10.prefab:31e6b3b0244121a4d84fd08d3e0a7edb");
		PreloadSound("VO_BRMA13_1_HP_WARRIOR_09.prefab:8e663fca53457e042bfc9ed73649a48d");
		PreloadSound("VO_BRMA13_1_HP_MAGE_11.prefab:30c699c65929cbe4595e9607be6b83a6");
		PreloadSound("VO_BRMA13_1_HP_DRUID_14.prefab:64491ad68b7d3924c879f7614c843744");
		PreloadSound("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093");
		PreloadSound("VO_BRMA13_1_HP_HUNTER_12.prefab:a36a163619384214b89421dc5f3f9b54");
		PreloadSound("VO_BRMA13_1_HP_ROGUE_15.prefab:a12234845f3520b4da26d3e575c16b9e");
		PreloadSound("VO_BRMA13_1_HP_GENERIC_18.prefab:b1f67fa18a6e9d94a974875d6abadcc8");
		PreloadSound("VO_BRMA13_1_DEATHWING_19.prefab:0306d0e9990cc6b49a890e59ed8852b5");
		PreloadSound("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b");
		PreloadSound("VO_NEFARIAN_NEF_MISSION_66.prefab:57a2f6e1548256846aa62132c8bb8a9a");
		PreloadSound("VO_RAGNAROS_NEF3_72.prefab:6667d1014b0ab3e4d80ae7ac43791160");
		PreloadSound("VO_NEFARIAN_HEROIC_BLOCK_77.prefab:3ee0a28083affa54489507ec406eccf5");
		PreloadSound("VO_RAGNAROS_NEF4_73.prefab:1cf3f059de2b2be4788cabc0b9e524d2");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89",
						m_stringTag = "VO_BRMA13_1_RESPONSE_05"
					}
				}
			}
		};
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (entity.GetCardId())
		{
		case "BRMA13_2":
		case "BRMA13_2H":
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994", Notification.SpeechBubbleDirection.TopRight, actor, 1f, 1f, parentBubbleToActor: false));
			actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_TURN1_PT2_03.prefab:dc72094fb84984f408d8d1ebf412d931", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case "BRMA13_4":
		case "BRMA13_4H":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				GameState.Get().SetBusy(busy: true);
				switch (GameState.Get().GetFriendlySidePlayer().GetHero()
					.GetClass())
				{
				case TAG_CLASS.PALADIN:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_PALADIN_07.prefab:a75bd560cd1e9b44192dc1aa2f59c489", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.PRIEST:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.WARLOCK:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_WARLOCK_10.prefab:31e6b3b0244121a4d84fd08d3e0a7edb", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.WARRIOR:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_WARRIOR_09.prefab:8e663fca53457e042bfc9ed73649a48d", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.MAGE:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_MAGE_11.prefab:30c699c65929cbe4595e9607be6b83a6", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.DRUID:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_DRUID_14.prefab:64491ad68b7d3924c879f7614c843744", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.SHAMAN:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.HUNTER:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_HUNTER_12.prefab:a36a163619384214b89421dc5f3f9b54", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				case TAG_CLASS.ROGUE:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_ROGUE_15.prefab:a12234845f3520b4da26d3e575c16b9e", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				default:
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_GENERIC_18.prefab:b1f67fa18a6e9d94a974875d6abadcc8", Notification.SpeechBubbleDirection.TopRight, actor));
					break;
				}
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 3:
			while (m_enemySpeaking)
			{
				yield return null;
			}
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA13_1_DEATHWING_19.prefab:0306d0e9990cc6b49a890e59ed8852b5", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 5:
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(4f);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			m_ragLine++;
			Gameplay.Get().StartCoroutine(UnBusyInSeconds(1f));
			switch (m_ragLine)
			{
			case 1:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF1_71"), "", allowRepeatDuringSession: true, 30f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_RAGNAROS_NEF1_71.prefab:195b5e3d6833ba54e887928cb7af3040", "", Notification.SpeechBubbleDirection.None, null));
				NotificationManager.Get().DestroyActiveQuote(0f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				break;
			case 2:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF3_72"), "", allowRepeatDuringSession: true, 30f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_RAGNAROS_NEF3_72.prefab:6667d1014b0ab3e4d80ae7ac43791160", "", Notification.SpeechBubbleDirection.None, null));
				NotificationManager.Get().DestroyActiveQuote(0f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NEFARIAN_NEF_MISSION_66.prefab:57a2f6e1548256846aa62132c8bb8a9a", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				break;
			case 3:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF4_73"), "VO_RAGNAROS_NEF4_73.prefab:1cf3f059de2b2be4788cabc0b9e524d2");
				break;
			case 4:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF5_74"), "VO_RAGNAROS_NEF5_74.prefab:604db2048e2b57248904d4e412c51215");
				break;
			case 5:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF6_75"), "VO_RAGNAROS_NEF6_75.prefab:b44707f4db7b8094a9d35374c62c5cc2");
				break;
			case 6:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF7_76"), "VO_RAGNAROS_NEF7_76.prefab:d1f1d1748445c104c82448cb61b90407");
				break;
			default:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF8_77"), "VO_RAGNAROS_NEF8_77.prefab:7c98dda922bb52b41b508a02b10b0a70");
				m_ragLine = 2;
				break;
			}
			break;
		case 6:
			NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF4_73"), "", allowRepeatDuringSession: true, 30f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_RAGNAROS_NEF4_73.prefab:1cf3f059de2b2be4788cabc0b9e524d2", "", Notification.SpeechBubbleDirection.None, null));
			NotificationManager.Get().DestroyActiveQuote(0f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NEFARIAN_HEROIC_BLOCK_77.prefab:3ee0a28083affa54489507ec406eccf5", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		}
	}

	private IEnumerator UnBusyInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GameState.Get().SetBusy(busy: false);
	}
}
