using System.Collections;
using System.Collections.Generic;

public class LOE16_Boss2 : LOE_MissionEntity
{
	private bool m_artifactLinePlayed;

	private bool m_firstExplorerHelp;

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Wing4Mission4);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_16_TURN_2.prefab:3925784abde16ee4db84d353f35d7f43");
		PreloadSound("VO_ELISE_LOE16_ALT_1_FIRST_HALF_02.prefab:40144146aa781184cb60dc29c43d6b94");
		PreloadSound("VO_ELISE_LOE16_ALT_1_SECOND_HALF_03.prefab:72c81552bbf06de469c2276ad41625f2");
		PreloadSound("VO_LOE_16_TURN_2_2.prefab:f159587a3a381614f9d6c594e249e67b");
		PreloadSound("VO_LOE_16_TURN_2_3.prefab:95d4dc8d6753d1947932230ae8d4c354");
		PreloadSound("VO_LOE_16_TURN_3.prefab:a98c80fc30955dd4887534ac067e1780");
		PreloadSound("VO_LOE_16_TURN_3_2.prefab:cb978afd731bc2840a5295a88a83090e");
		PreloadSound("VO_LOE_16_TURN_4.prefab:7123090b33ee4a744a846eee706ae8e3");
		PreloadSound("VO_LOE_16_TURN_5.prefab:0b8f31bfdc7aa5942b9151d1a910f33c");
		PreloadSound("VO_LOE_16_TURN_5_2.prefab:f357c43289f9c674ba9155ca1f129997");
		PreloadSound("VO_LOE_16_TURN_6.prefab:7bdf1c0deae1c5b49b801c3437181625");
		PreloadSound("VO_LOE_16_FIRST_ITEM.prefab:0ece4d21868b0f241a4ff31cc1bfbbf8");
		PreloadSound("VO_LOE_092_Attack_02.prefab:299846b35a2cad243a43b819cfa34534");
		PreloadSound("VO_LOE_16_GOBLET.prefab:abdf379a3a048cd43b0e40503ee86c13");
		PreloadSound("VO_LOE_16_CROWN.prefab:b8a5687781809eb42a421dc9678f8f9f");
		PreloadSound("VO_LOE_16_EYE.prefab:ece885c70c9857f4098148b326609ec9");
		PreloadSound("VO_LOE_16_PIPE.prefab:719013ca357df6b4f9084d187720a06c");
		PreloadSound("VO_LOE_16_TEAR.prefab:a6ebaaadde4a4e244b51f02e50382bba");
		PreloadSound("VO_LOE_16_SHARD.prefab:95338a53c3e2c144eb23e21611e77e55");
		PreloadSound("VO_LOE_16_LOCKET.prefab:b73c5bae4ab644741b0c8422dfcd5ec5");
		PreloadSound("VO_LOE_16_SPLINTER.prefab:529331f284c8a1446805ad3e9ff54e92");
		PreloadSound("VO_LOE_16_VIAL.prefab:a2e27b473d1aa1248815b00e4b4dfc0e");
		PreloadSound("VO_LOE_16_GREAVE.prefab:1cff9e5cac8311747847372619d249d7");
		PreloadSound("VO_LOE_16_BOOM_BOT.prefab:790707c8113245f4cbaa50c279ad7c39");
		PreloadSound("VO_LOEA16_1_CARD_04.prefab:c929045c2c500af41ae4326197c58e1c");
		PreloadSound("VO_LOEA16_1_RESPONSE_03.prefab:c7e2ccd2d1fbd0b419645a7f8d8c4a40");
		PreloadSound("VO_LOEA16_1_TURN1_02.prefab:3effaa9f542d9bb4e86bbc3d460c2342");
	}

	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		Gameplay.Get().StartCoroutine(OnTagChangedHandler(change));
	}

	public override string UpdateCardText(Card card, Actor bigCardActor, string text)
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer.GetHeroPowerCard() != card)
		{
			return text;
		}
		int num = opposingSidePlayer.GetHeroPower().GetTag(GAME_TAG.ELECTRIC_CHARGE_LEVEL);
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.TURN) < 2)
		{
			num = 3;
		}
		string key = "";
		switch (num)
		{
		case 0:
			key = "LOEA16_2_STAFF_TEXT_CHARGE_EXPLODED";
			break;
		case 1:
			key = "LOEA16_2_STAFF_TEXT_CHARGE_1";
			break;
		case 2:
			key = "LOEA16_2_STAFF_TEXT_CHARGE_2";
			break;
		case 3:
			key = "LOEA16_2_STAFF_TEXT_CHARGE_0";
			break;
		}
		return GameStrings.Get(key);
	}

	private IEnumerator OnTagChangedHandler(TagDelta change)
	{
		if (change.tag != 3)
		{
			yield break;
		}
		int newValue = change.newValue;
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		int count2 = newValue;
		if (count2 > 14)
		{
			count2 = (count2 - 14) % 6;
			if (count2 == 0)
			{
				count2 = 6;
			}
			count2 = 8 + count2;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (count2)
		{
		case 1:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA16_1_TURN1_02.prefab:3effaa9f542d9bb4e86bbc3d460c2342", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_ELISE_LOE16_ALT_1_FIRST_HALF_02.prefab:40144146aa781184cb60dc29c43d6b94"));
			if (GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.ELECTRIC_CHARGE_LEVEL) <= 0)
			{
				GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard()
					.ActivateActorSpell(SpellType.ELECTRIC_CHARGE_LEVEL_SMALL);
			}
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_ELISE_LOE16_ALT_1_SECOND_HALF_03.prefab:72c81552bbf06de469c2276ad41625f2"));
			GameState.Get().SetBusy(busy: false);
			break;
		case 5:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_2.prefab:3925784abde16ee4db84d353f35d7f43"));
			break;
		case 6:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOE_16_TURN_2_2.prefab:f159587a3a381614f9d6c594e249e67b", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 7:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_2_3.prefab:95d4dc8d6753d1947932230ae8d4c354"));
			break;
		case 8:
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_16_TURN_3.prefab:a98c80fc30955dd4887534ac067e1780", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_3_2.prefab:cb978afd731bc2840a5295a88a83090e"));
			break;
		case 11:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_4.prefab:7123090b33ee4a744a846eee706ae8e3"));
			break;
		case 12:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_16_TURN_5.prefab:0b8f31bfdc7aa5942b9151d1a910f33c", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 13:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_5_2.prefab:f357c43289f9c674ba9155ca1f129997"));
			break;
		case 14:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_6.prefab:7bdf1c0deae1c5b49b801c3437181625"));
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		yield break;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent >= 10 && !m_firstExplorerHelp)
		{
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_FIRST_ITEM.prefab:0ece4d21868b0f241a4ff31cc1bfbbf8"));
			m_firstExplorerHelp = true;
			yield break;
		}
		switch (missionEvent)
		{
		case 2:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOE_092_Attack_02.prefab:299846b35a2cad243a43b819cfa34534", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 25400:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_GOBLET.prefab:abdf379a3a048cd43b0e40503ee86c13"));
			break;
		case 25401:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_16_CROWN.prefab:b8a5687781809eb42a421dc9678f8f9f"));
			break;
		case 25402:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_16_LOCKET.prefab:b73c5bae4ab644741b0c8422dfcd5ec5"));
			break;
		case 25393:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_EYE.prefab:ece885c70c9857f4098148b326609ec9"));
			break;
		case 25394:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_PIPE.prefab:719013ca357df6b4f9084d187720a06c"));
			break;
		case 25395:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TEAR.prefab:a6ebaaadde4a4e244b51f02e50382bba"));
			break;
		case 25396:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_SHARD.prefab:95338a53c3e2c144eb23e21611e77e55"));
			break;
		case 25397:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_16_SPLINTER.prefab:529331f284c8a1446805ad3e9ff54e92"));
			break;
		case 25398:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_VIAL.prefab:a2e27b473d1aa1248815b00e4b4dfc0e"));
			break;
		case 25399:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_16_GREAVE.prefab:1cff9e5cac8311747847372619d249d7"));
			break;
		case 2235:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_BOOM_BOT.prefab:790707c8113245f4cbaa50c279ad7c39"));
			break;
		}
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
		case "LOEA16_3":
		case "LOEA16_4":
		case "LOEA16_5":
			if (!m_artifactLinePlayed)
			{
				m_artifactLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA16_1_CARD_04.prefab:c929045c2c500af41ae4326197c58e1c", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		}
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
						m_soundName = "VO_LOEA16_1_RESPONSE_03.prefab:c7e2ccd2d1fbd0b419645a7f8d8c4a40",
						m_stringTag = "VO_LOEA16_1_RESPONSE_03"
					}
				}
			}
		};
	}
}
