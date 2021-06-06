using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_16 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01.prefab:f64b46a1aff11e843b96aa93b21ede1d");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01.prefab:e2dbfb9e1dc9c1b41ac180cbf545b59b");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01.prefab:efd902b4f0990ca499a148afc18c7f44");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01.prefab:bbaee13c2a2b7344e89dc6b03f7cae44");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01.prefab:05e1d5d9a8d854641b15ebd3ef332b0a");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01.prefab:9f4fdf57b47edc44fbd30866fe3b1dc3");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01.prefab:7f6f762e90266d34e909f2d95afaefe3");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01.prefab:5149d2b4dfd48e549a4a1ddfdb97041e");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01.prefab:21d459a18887fb5429e5f31803f2737a");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01.prefab:ab46822842175544c9042eb73a2af8d7");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01.prefab:706539eed8f118646a8c6d49a3f706c8");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01.prefab:b31b801186a95ec47a8dfa5dc5073f5f");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01.prefab:5cd807b17b5029c4782d0459ceef1428");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01.prefab:85b57a6352934464eb4bdcc2b75f174b");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01.prefab:62c2fd4daf05fb243a4315a1fb60f6b6");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01.prefab:e8f593ffa739dee498d0227ba5df56ae");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01.prefab:e57bf87b57088044a9f5aacae1cc24c7");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01.prefab:258a9924e626b884e9297bdc1a171013");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01.prefab:0fc4409a36b989f41b161ab6bde3faab");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01.prefab:deb0bf29ac4f84e4399e8dbed4a31296");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02.prefab:eacb7147dd317774480528c83a68ce69");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03.prefab:36f6d2629f891b14b8b772f1e8075f87");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01.prefab:2c1c56acc0bd5884db2f6e50c2553bd1");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02.prefab:f668fb355f4c178428ccd7a4f011cdda");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03.prefab:98d83b8ae4554e9459345385a50e7b62");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01.prefab:7100b25017ab8ee4b8fe844f64470f7f");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01.prefab:bcec9f995795fe3439132fe644f6da0a");

	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01.prefab:b6d448c4b845ddc4da9ccb2f07f33739");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Misc_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Misc_01.prefab:1aa5cefd36c6a0440831c1307d997c34");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01.prefab:59cc24da20e79ef40b1d1727b6b28738");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01.prefab:b13583b14cfb088458c76ffcef37184d");

	private static readonly AssetReference VO_BTA_10_Female_Naga_Misc_02 = new AssetReference("VO_BTA_10_Female_Naga_Misc_02.prefab:47b149080b9211f4a9d561372fc3ed57");

	private static readonly AssetReference VO_BTA_08_Male_Orc_Misc_03 = new AssetReference("VO_BTA_08_Male_Orc_Misc_03.prefab:0beffd1b2e61ee24ea6ae52d8ea26fba");

	private List<string> m_VO_BTA_BOSS_16h_IdleLines = new List<string> { VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01 };

	private List<string> m_missionEventTrigger501_Lines = new List<string> { VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01, VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01 };

	private List<string> m_missionEventTrigger100_Lines = new List<string> { VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03 };

	private List<string> m_missionEventTrigger101_Lines = new List<string> { VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_16()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01, VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01, VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01, VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01,
			VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01,
			VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01, VO_BTA_BOSS_17h_Male_NightElf_Misc_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01,
			VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01, VO_BTA_08_Male_Orc_Misc_03, VO_BTA_10_Female_Naga_Misc_02
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_16h_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01);
		GameState.Get().SetBusy(busy: false);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayRandomLineAlways(actor, m_missionEventTrigger100_Lines);
			break;
		case 101:
			yield return PlayRandomLineAlways(actor, m_missionEventTrigger101_Lines);
			break;
		case 102:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01);
			break;
		case 103:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01);
			break;
		case 104:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01);
			break;
		case 105:
			yield return PlayLineAlways(BTA_Dungeon.IllidanBrassRing, VO_BTA_BOSS_17h_Male_NightElf_Misc_01);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(BTA_Dungeon.IllidanBrassRing, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01);
			yield return PlayLineAlways(BTA_Dungeon.IllidanBrassRing, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01);
			GameState.Get().SetBusy(busy: false);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BT_491":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01);
				break;
			case "BT_512":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01);
				break;
			case "BT_752":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01);
				break;
			case "BTA_05":
			case "BTA_06":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01);
				break;
			case "BTA_BOSS_15s":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01);
				break;
			}
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
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BT_302":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01);
				break;
			case "BTA_13":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01);
				break;
			case "BTA_15":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01);
				break;
			case "BTA_16":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01);
				break;
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRingDemonHunter, VO_BTA_08_Male_Orc_Misc_03);
			yield return PlayLineAlways(GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRingDemonHunter, VO_BTA_10_Female_Naga_Misc_02);
			break;
		case 5:
			yield return PlayLineAlways(BTA_Dungeon.IllidanBrassRing, VO_BTA_BOSS_17h_Male_NightElf_Misc_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT_FinalBoss);
	}
}
