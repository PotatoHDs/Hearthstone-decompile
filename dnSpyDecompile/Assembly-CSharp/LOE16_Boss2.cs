using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000396 RID: 918
public class LOE16_Boss2 : LOE_MissionEntity
{
	// Token: 0x060034EE RID: 13550 RVA: 0x0010DA88 File Offset: 0x0010BC88
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Wing4Mission4);
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x0010DA9C File Offset: 0x0010BC9C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_16_TURN_2.prefab:3925784abde16ee4db84d353f35d7f43");
		base.PreloadSound("VO_ELISE_LOE16_ALT_1_FIRST_HALF_02.prefab:40144146aa781184cb60dc29c43d6b94");
		base.PreloadSound("VO_ELISE_LOE16_ALT_1_SECOND_HALF_03.prefab:72c81552bbf06de469c2276ad41625f2");
		base.PreloadSound("VO_LOE_16_TURN_2_2.prefab:f159587a3a381614f9d6c594e249e67b");
		base.PreloadSound("VO_LOE_16_TURN_2_3.prefab:95d4dc8d6753d1947932230ae8d4c354");
		base.PreloadSound("VO_LOE_16_TURN_3.prefab:a98c80fc30955dd4887534ac067e1780");
		base.PreloadSound("VO_LOE_16_TURN_3_2.prefab:cb978afd731bc2840a5295a88a83090e");
		base.PreloadSound("VO_LOE_16_TURN_4.prefab:7123090b33ee4a744a846eee706ae8e3");
		base.PreloadSound("VO_LOE_16_TURN_5.prefab:0b8f31bfdc7aa5942b9151d1a910f33c");
		base.PreloadSound("VO_LOE_16_TURN_5_2.prefab:f357c43289f9c674ba9155ca1f129997");
		base.PreloadSound("VO_LOE_16_TURN_6.prefab:7bdf1c0deae1c5b49b801c3437181625");
		base.PreloadSound("VO_LOE_16_FIRST_ITEM.prefab:0ece4d21868b0f241a4ff31cc1bfbbf8");
		base.PreloadSound("VO_LOE_092_Attack_02.prefab:299846b35a2cad243a43b819cfa34534");
		base.PreloadSound("VO_LOE_16_GOBLET.prefab:abdf379a3a048cd43b0e40503ee86c13");
		base.PreloadSound("VO_LOE_16_CROWN.prefab:b8a5687781809eb42a421dc9678f8f9f");
		base.PreloadSound("VO_LOE_16_EYE.prefab:ece885c70c9857f4098148b326609ec9");
		base.PreloadSound("VO_LOE_16_PIPE.prefab:719013ca357df6b4f9084d187720a06c");
		base.PreloadSound("VO_LOE_16_TEAR.prefab:a6ebaaadde4a4e244b51f02e50382bba");
		base.PreloadSound("VO_LOE_16_SHARD.prefab:95338a53c3e2c144eb23e21611e77e55");
		base.PreloadSound("VO_LOE_16_LOCKET.prefab:b73c5bae4ab644741b0c8422dfcd5ec5");
		base.PreloadSound("VO_LOE_16_SPLINTER.prefab:529331f284c8a1446805ad3e9ff54e92");
		base.PreloadSound("VO_LOE_16_VIAL.prefab:a2e27b473d1aa1248815b00e4b4dfc0e");
		base.PreloadSound("VO_LOE_16_GREAVE.prefab:1cff9e5cac8311747847372619d249d7");
		base.PreloadSound("VO_LOE_16_BOOM_BOT.prefab:790707c8113245f4cbaa50c279ad7c39");
		base.PreloadSound("VO_LOEA16_1_CARD_04.prefab:c929045c2c500af41ae4326197c58e1c");
		base.PreloadSound("VO_LOEA16_1_RESPONSE_03.prefab:c7e2ccd2d1fbd0b419645a7f8d8c4a40");
		base.PreloadSound("VO_LOEA16_1_TURN1_02.prefab:3effaa9f542d9bb4e86bbc3d460c2342");
	}

	// Token: 0x060034F0 RID: 13552 RVA: 0x0010DBD2 File Offset: 0x0010BDD2
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		Gameplay.Get().StartCoroutine(this.OnTagChangedHandler(change));
	}

	// Token: 0x060034F1 RID: 13553 RVA: 0x0010DBF0 File Offset: 0x0010BDF0
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

	// Token: 0x060034F2 RID: 13554 RVA: 0x0010DC81 File Offset: 0x0010BE81
	private IEnumerator OnTagChangedHandler(TagDelta change)
	{
		if (change.tag != 3)
		{
			yield break;
		}
		int newValue = change.newValue;
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		int count = newValue;
		if (count > 14)
		{
			count = (count - 14) % 6;
			if (count == 0)
			{
				count = 6;
			}
			count = 8 + count;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		switch (count)
		{
		case 1:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA16_1_TURN1_02.prefab:3effaa9f542d9bb4e86bbc3d460c2342", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_ELISE_LOE16_ALT_1_FIRST_HALF_02.prefab:40144146aa781184cb60dc29c43d6b94", 3f, 1f, true, false));
			if (GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.ELECTRIC_CHARGE_LEVEL) <= 0)
			{
				GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard().ActivateActorSpell(SpellType.ELECTRIC_CHARGE_LEVEL_SMALL);
			}
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_ELISE_LOE16_ALT_1_SECOND_HALF_03.prefab:72c81552bbf06de469c2276ad41625f2", 3f, 1f, true, false));
			GameState.Get().SetBusy(false);
			break;
		case 5:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_2.prefab:3925784abde16ee4db84d353f35d7f43", 3f, 1f, true, false));
			break;
		case 6:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOE_16_TURN_2_2.prefab:f159587a3a381614f9d6c594e249e67b", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 7:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_2_3.prefab:95d4dc8d6753d1947932230ae8d4c354", 3f, 1f, true, false));
			break;
		case 8:
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_16_TURN_3.prefab:a98c80fc30955dd4887534ac067e1780", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_3_2.prefab:cb978afd731bc2840a5295a88a83090e", 3f, 1f, true, false));
			break;
		case 11:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_4.prefab:7123090b33ee4a744a846eee706ae8e3", 3f, 1f, true, false));
			break;
		case 12:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_16_TURN_5.prefab:0b8f31bfdc7aa5942b9151d1a910f33c", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 13:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_5_2.prefab:f357c43289f9c674ba9155ca1f129997", 3f, 1f, false, false));
			break;
		case 14:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TURN_6.prefab:7bdf1c0deae1c5b49b801c3437181625", 3f, 1f, false, false));
			break;
		}
		yield break;
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x0010DC97 File Offset: 0x0010BE97
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		yield break;
	}

	// Token: 0x060034F4 RID: 13556 RVA: 0x0010DC9F File Offset: 0x0010BE9F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent >= 10 && !this.m_firstExplorerHelp)
		{
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_FIRST_ITEM.prefab:0ece4d21868b0f241a4ff31cc1bfbbf8", 3f, 1f, true, false));
			this.m_firstExplorerHelp = true;
		}
		else if (missionEvent != 2)
		{
			if (missionEvent != 2235)
			{
				switch (missionEvent)
				{
				case 25393:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_EYE.prefab:ece885c70c9857f4098148b326609ec9", 3f, 1f, false, false));
					break;
				case 25394:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_PIPE.prefab:719013ca357df6b4f9084d187720a06c", 3f, 1f, false, false));
					break;
				case 25395:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_TEAR.prefab:a6ebaaadde4a4e244b51f02e50382bba", 3f, 1f, false, false));
					break;
				case 25396:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_SHARD.prefab:95338a53c3e2c144eb23e21611e77e55", 3f, 1f, false, false));
					break;
				case 25397:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_16_SPLINTER.prefab:529331f284c8a1446805ad3e9ff54e92", 3f, 1f, false, false));
					break;
				case 25398:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_16_VIAL.prefab:a2e27b473d1aa1248815b00e4b4dfc0e", 3f, 1f, false, false));
					break;
				case 25399:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_16_GREAVE.prefab:1cff9e5cac8311747847372619d249d7", 3f, 1f, false, false));
					break;
				case 25400:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_GOBLET.prefab:abdf379a3a048cd43b0e40503ee86c13", 3f, 1f, false, false));
					break;
				case 25401:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_16_CROWN.prefab:b8a5687781809eb42a421dc9678f8f9f", 3f, 1f, false, false));
					break;
				case 25402:
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_16_LOCKET.prefab:b73c5bae4ab644741b0c8422dfcd5ec5", 3f, 1f, false, false));
					break;
				}
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_16_BOOM_BOT.prefab:790707c8113245f4cbaa50c279ad7c39", 3f, 1f, false, false));
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOE_092_Attack_02.prefab:299846b35a2cad243a43b819cfa34534", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x0010DCB5 File Offset: 0x0010BEB5
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (cardId == "LOEA16_3" || cardId == "LOEA16_4" || cardId == "LOEA16_5")
		{
			if (this.m_artifactLinePlayed)
			{
				yield break;
			}
			this.m_artifactLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA16_1_CARD_04.prefab:c929045c2c500af41ae4326197c58e1c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x060034F6 RID: 13558 RVA: 0x0010DCCC File Offset: 0x0010BECC
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_LOEA16_1_RESPONSE_03.prefab:c7e2ccd2d1fbd0b419645a7f8d8c4a40",
						m_stringTag = "VO_LOEA16_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x04001CD1 RID: 7377
	private bool m_artifactLinePlayed;

	// Token: 0x04001CD2 RID: 7378
	private bool m_firstExplorerHelp;
}
