using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_10 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01.prefab:cb5520ea953cb7b4ca314641b40ea4d9");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01.prefab:763aead63794a6644bea4eda919afc06");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01.prefab:d48b0991caa45874f8a8ebfe3a3ae4c4");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01.prefab:ba8210b3032710a49a5f6238e1f24bb4");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01.prefab:d964e4522d5cf3440a56fef3bccb0136");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01.prefab:b9f57720ca5438d4f8fb28806edb411d");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01.prefab:b380391f0f1c5f6488843546cf3a0097");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01.prefab:ba553daa3e1084f489220fef2c3f7490");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01.prefab:7934cc668d79e1f448618b18ce626b4c");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01.prefab:845a8061f83e00040a99043d723b82b2");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01.prefab:b8f75c717b47df94bbf4c09f4313d8e9");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01.prefab:7efd376874f3dda439611f1e2dd8b100");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01.prefab:39610ca2882f8e64b84afbd20acdf28a");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02.prefab:7a4a107635a9af04dbbb7168b1484934");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03.prefab:dbdcbddbeedb50d46ad96f0da5494492");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04.prefab:a383e95229214f447a7bd4806963ba63");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01.prefab:506ac8b0409b2614aa2c8c1aee12a2db");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01.prefab:4ff2c046236f5c44fb2eb12747e7617e");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01.prefab:38414059929a0d94f81e7d315c654885");

	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01.prefab:931b0079d78c00845b2a29e96105fa55");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01.prefab:c8a7bab0d2d33114386b97230d95ca3d");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01.prefab:4ed12ea3fbae14f498812ebe5a730151");

	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01.prefab:a3cacdf37e880e54f9f1b2012b20501b");

	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01.prefab:ca7dc7abc3fa24a46a70aa193a5df3f5");

	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Start_01 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Start_01.prefab:fb33f3d48876c414489f39b68bcb3b3a");

	private List<string> m_VO_BTA_BOSS_10h_IdleLines = new List<string> { VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01 };

	private List<string> m_missionEventTrigger100_Lines = new List<string> { VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_10()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01, VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01, VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01, VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01,
			VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01,
			VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01, VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01, VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01, VO_BTA_BOSS_10h2_Male_Sporelok_Start_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_10h_IdleLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
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
		yield return PlayLineAlways(actor, VO_BTA_BOSS_10h2_Male_Sporelok_Start_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01);
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 507:
			yield return PlayRandomLineAlways(actor, m_missionEventTrigger100_Lines);
			break;
		case 508:
			yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04);
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
			case "BT_036":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01);
				break;
			case "BTA_BOSS_10t":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01);
				break;
			case "BT_491":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01);
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
			case "BT_252":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01);
				break;
			case "BT_493":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01);
				break;
			case "BT_921":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01);
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
		case 5:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01);
			yield return PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01);
			yield return PlayLineAlways(BTA_Dungeon.IllidanBrassRing, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01);
			yield return PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01);
			break;
		case 7:
			yield return PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01);
			yield return PlayLineAlways(BTA_Dungeon.IllidanBrassRing, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01);
			yield return PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01);
			break;
		}
	}
}
