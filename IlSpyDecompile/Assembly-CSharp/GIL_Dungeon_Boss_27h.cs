using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_27h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_27h_Male_Construct_Intro_01.prefab:a3a9f619f8cd367498818154dc079a8a", "VO_GILA_BOSS_27h_Male_Construct_EmoteResponse_01.prefab:ff9964ab0269a7949bab4870aa594461", "VO_GILA_BOSS_27h_Male_Construct_Death_02.prefab:f4beb9b551ca2404484dff794b03a721", "VO_GILA_BOSS_27h_Male_Construct_HeroPowerMurloc_01.prefab:d9ccf54e892933140af3e09ce5459e76", "VO_GILA_BOSS_27h_Male_Construct_HeroPowerBeast_02.prefab:5be0fa4bf8907da4394ac2f633e37c6e", "VO_GILA_BOSS_27h_Male_Construct_HeroPowerPirate_01.prefab:8abc985b55353984083cf2d6edd9941a", "VO_GILA_BOSS_27h_Male_Construct_HeroPowerDragon_01.prefab:2486bd5a23450414fbc57eb09fd295d7", "VO_GILA_BOSS_27h_Male_Construct_HeroPowerMech_02.prefab:ea1f110a627b5e34698b87920d05a81a", "VO_GILA_BOSS_27h_Male_Construct_HeroPowerDemon_01.prefab:d09533588ca279b40935bb7fe9e8c409", "VO_GILA_BOSS_27h_Male_Construct_HeroPowerGeneric_01.prefab:f18f38dbd19b0244d9c57f492611951f",
			"VO_GILA_BOSS_27h_Male_Construct_EventPlayDrBoom_02.prefab:29aa1d78cc65d374ba457dff5b7014da", "VO_GILA_BOSS_27h_Male_Construct_EventPlayUnstablePortal_01.prefab:fc812125bffa13946a8b63b675dfbb72", "VO_GILA_BOSS_27h_Male_Construct_EventPlaySparePart_01.prefab:be6a9d5ca3fd4ff41af8499cb4c8c46a"
		})
		{
			PreloadSound(item);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_27h_Male_Construct_Intro_01.prefab:a3a9f619f8cd367498818154dc079a8a", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_27h_Male_Construct_EmoteResponse_01.prefab:ff9964ab0269a7949bab4870aa594461", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_27h_Male_Construct_Death_02.prefab:f4beb9b551ca2404484dff794b03a721";
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
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
			case "GVG_110":
				yield return PlayBossLine(actor, "VO_GILA_BOSS_27h_Male_Construct_EventPlayDrBoom_02.prefab:29aa1d78cc65d374ba457dff5b7014da");
				break;
			case "GVG_003":
				yield return PlayBossLine(actor, "VO_GILA_BOSS_27h_Male_Construct_EventPlayUnstablePortal_01.prefab:fc812125bffa13946a8b63b675dfbb72");
				break;
			case "PART_001":
			case "PART_002":
			case "PART_003":
			case "PART_004":
			case "PART_005":
			case "PART_006":
			case "PART_007":
				yield return PlayBossLine(actor, "VO_GILA_BOSS_27h_Male_Construct_EventPlaySparePart_01.prefab:be6a9d5ca3fd4ff41af8499cb4c8c46a");
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			switch (missionEvent)
			{
			case 101:
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerMurloc_01.prefab:d9ccf54e892933140af3e09ce5459e76");
				break;
			case 102:
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerBeast_02.prefab:5be0fa4bf8907da4394ac2f633e37c6e");
				break;
			case 103:
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerPirate_01.prefab:8abc985b55353984083cf2d6edd9941a");
				break;
			case 104:
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerDragon_01.prefab:2486bd5a23450414fbc57eb09fd295d7");
				break;
			case 105:
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerMech_02.prefab:ea1f110a627b5e34698b87920d05a81a");
				break;
			case 106:
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerDemon_01.prefab:d09533588ca279b40935bb7fe9e8c409");
				break;
			case 107:
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_27h_Male_Construct_HeroPowerGeneric_01.prefab:f18f38dbd19b0244d9c57f492611951f");
				break;
			}
		}
	}
}
