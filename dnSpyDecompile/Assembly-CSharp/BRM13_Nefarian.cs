using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000382 RID: 898
public class BRM13_Nefarian : BRM_MissionEntity
{
	// Token: 0x06003458 RID: 13400 RVA: 0x0010BD3C File Offset: 0x00109F3C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89");
		base.PreloadSound("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994");
		base.PreloadSound("VO_BRMA13_1_TURN1_PT2_03.prefab:dc72094fb84984f408d8d1ebf412d931");
		base.PreloadSound("VO_RAGNAROS_NEF1_71.prefab:195b5e3d6833ba54e887928cb7af3040");
		base.PreloadSound("VO_BRMA13_1_HP_PALADIN_07.prefab:a75bd560cd1e9b44192dc1aa2f59c489");
		base.PreloadSound("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb");
		base.PreloadSound("VO_BRMA13_1_HP_WARLOCK_10.prefab:31e6b3b0244121a4d84fd08d3e0a7edb");
		base.PreloadSound("VO_BRMA13_1_HP_WARRIOR_09.prefab:8e663fca53457e042bfc9ed73649a48d");
		base.PreloadSound("VO_BRMA13_1_HP_MAGE_11.prefab:30c699c65929cbe4595e9607be6b83a6");
		base.PreloadSound("VO_BRMA13_1_HP_DRUID_14.prefab:64491ad68b7d3924c879f7614c843744");
		base.PreloadSound("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093");
		base.PreloadSound("VO_BRMA13_1_HP_HUNTER_12.prefab:a36a163619384214b89421dc5f3f9b54");
		base.PreloadSound("VO_BRMA13_1_HP_ROGUE_15.prefab:a12234845f3520b4da26d3e575c16b9e");
		base.PreloadSound("VO_BRMA13_1_HP_GENERIC_18.prefab:b1f67fa18a6e9d94a974875d6abadcc8");
		base.PreloadSound("VO_BRMA13_1_DEATHWING_19.prefab:0306d0e9990cc6b49a890e59ed8852b5");
		base.PreloadSound("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b");
		base.PreloadSound("VO_NEFARIAN_NEF_MISSION_66.prefab:57a2f6e1548256846aa62132c8bb8a9a");
		base.PreloadSound("VO_RAGNAROS_NEF3_72.prefab:6667d1014b0ab3e4d80ae7ac43791160");
		base.PreloadSound("VO_NEFARIAN_HEROIC_BLOCK_77.prefab:3ee0a28083affa54489507ec406eccf5");
		base.PreloadSound("VO_RAGNAROS_NEF4_73.prefab:1cf3f059de2b2be4788cabc0b9e524d2");
	}

	// Token: 0x06003459 RID: 13401 RVA: 0x0010BE28 File Offset: 0x0010A028
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
						m_soundName = "VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89",
						m_stringTag = "VO_BRMA13_1_RESPONSE_05"
					}
				}
			}
		};
	}

	// Token: 0x0600345A RID: 13402 RVA: 0x0010BE87 File Offset: 0x0010A087
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
		if (!(cardId == "BRMA13_2") && !(cardId == "BRMA13_2H"))
		{
			if (cardId == "BRMA13_4" || cardId == "BRMA13_4H")
			{
				if (this.m_heroPowerLinePlayed)
				{
					yield break;
				}
				this.m_heroPowerLinePlayed = true;
				GameState.Get().SetBusy(true);
				switch (GameState.Get().GetFriendlySidePlayer().GetHero().GetClass())
				{
				case TAG_CLASS.DRUID:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_DRUID_14.prefab:64491ad68b7d3924c879f7614c843744", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.HUNTER:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_HUNTER_12.prefab:a36a163619384214b89421dc5f3f9b54", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.MAGE:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_MAGE_11.prefab:30c699c65929cbe4595e9607be6b83a6", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.PALADIN:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_PALADIN_07.prefab:a75bd560cd1e9b44192dc1aa2f59c489", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.PRIEST:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.ROGUE:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_ROGUE_15.prefab:a12234845f3520b4da26d3e575c16b9e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.SHAMAN:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.WARLOCK:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_WARLOCK_10.prefab:31e6b3b0244121a4d84fd08d3e0a7edb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case TAG_CLASS.WARRIOR:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_WARRIOR_09.prefab:8e663fca53457e042bfc9ed73649a48d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				default:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_HP_GENERIC_18.prefab:b1f67fa18a6e9d94a974875d6abadcc8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				}
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994", Notification.SpeechBubbleDirection.TopRight, actor, 1f, 1f, false, false, 0f));
			actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_TURN1_PT2_03.prefab:dc72094fb84984f408d8d1ebf412d931", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600345B RID: 13403 RVA: 0x0010BE9D File Offset: 0x0010A09D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 3:
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA13_1_DEATHWING_19.prefab:0306d0e9990cc6b49a890e59ed8852b5", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			break;
		case 5:
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(4f);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			this.m_ragLine++;
			Gameplay.Get().StartCoroutine(this.UnBusyInSeconds(1f));
			switch (this.m_ragLine)
			{
			case 1:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF1_71"), "", true, 30f, null, CanvasAnchor.BOTTOM_LEFT, false);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_RAGNAROS_NEF1_71.prefab:195b5e3d6833ba54e887928cb7af3040", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
				NotificationManager.Get().DestroyActiveQuote(0f, false);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
				break;
			case 2:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF3_72"), "", true, 30f, null, CanvasAnchor.BOTTOM_LEFT, false);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_RAGNAROS_NEF3_72.prefab:6667d1014b0ab3e4d80ae7ac43791160", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
				NotificationManager.Get().DestroyActiveQuote(0f, false);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NEFARIAN_NEF_MISSION_66.prefab:57a2f6e1548256846aa62132c8bb8a9a", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
				break;
			case 3:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF4_73"), "VO_RAGNAROS_NEF4_73.prefab:1cf3f059de2b2be4788cabc0b9e524d2", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
				break;
			case 4:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF5_74"), "VO_RAGNAROS_NEF5_74.prefab:604db2048e2b57248904d4e412c51215", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
				break;
			case 5:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF6_75"), "VO_RAGNAROS_NEF6_75.prefab:b44707f4db7b8094a9d35374c62c5cc2", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
				break;
			case 6:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF7_76"), "VO_RAGNAROS_NEF7_76.prefab:d1f1d1748445c104c82448cb61b90407", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
				break;
			default:
				NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF8_77"), "VO_RAGNAROS_NEF8_77.prefab:7c98dda922bb52b41b508a02b10b0a70", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
				this.m_ragLine = 2;
				break;
			}
			break;
		case 6:
			NotificationManager.Get().CreateCharacterQuote("Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51", this.ragLinePosition, GameStrings.Get("VO_RAGNAROS_NEF4_73"), "", true, 30f, null, CanvasAnchor.BOTTOM_LEFT, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_RAGNAROS_NEF4_73.prefab:1cf3f059de2b2be4788cabc0b9e524d2", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			NotificationManager.Get().DestroyActiveQuote(0f, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NEFARIAN_HEROIC_BLOCK_77.prefab:3ee0a28083affa54489507ec406eccf5", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			break;
		}
		yield break;
	}

	// Token: 0x0600345C RID: 13404 RVA: 0x0010BEB3 File Offset: 0x0010A0B3
	private IEnumerator UnBusyInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x04001C9B RID: 7323
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C9C RID: 7324
	private int m_ragLine;

	// Token: 0x04001C9D RID: 7325
	private Vector3 ragLinePosition = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
}
