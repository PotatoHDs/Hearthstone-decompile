using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_15 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01.prefab:14596b34ebbc4b34496f20b8e9bd490c");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01.prefab:bdb407d38aee2fc4b98ac4254ac5d822");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01.prefab:0e554cf533cc69e4ebb72c0d355a4746");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01.prefab:7d6a61670538aa2458501d48875ed511");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01.prefab:2e02ad18ce4c29c4fa2162474c59c438");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01.prefab:e56b316808fa9ef47b6abd137f7f3faf");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01.prefab:a6dfb894cf0deb242a25f3436118b26c");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01.prefab:fafd9b1edb0ee8345a1a28618d9d74c1");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01.prefab:c400b66a433ca964a921196d4d9ec45f");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01.prefab:df0c19e1b43c7d64fb99334f85184a3a");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01.prefab:89661cbc1dbddb049842fd9ef2a456c9");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02.prefab:1d53f67380b935a4999cb7b27c1c3b89");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01.prefab:40961be1503134048a868990e3419cc0");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01.prefab:3e47af720f0fd284fac9f0457f41e561");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01.prefab:53ad352b3841dda47b9c726dbfca3660");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01.prefab:2edad56215fe4f945afd73d4170de70d");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01.prefab:b34eb9c171f915b49ba541239d6c2c12");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01.prefab:6a51855be33c3734e8dd00ac0fe76243");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02.prefab:67bf5130a6208864d9696171fc5fd22a");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03.prefab:2a29e5a54cc191c478785d480b439ef7");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04.prefab:76b21a6a346cc15478eff3f9a29717de");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01.prefab:09f710e21019795498ef62f109e9ea42");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01.prefab:1765c3f6848501e46b1dce09f6611c8c");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01.prefab:d384d8fa9d5b573468125ec47132f7f6");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01.prefab:07dc71ede87d4c847961b8caa50132a4");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01.prefab:5e1be49dba03cac41a8ef63c7576a6fa");

	private static readonly AssetReference VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01 = new AssetReference("VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01.prefab:3f97487fe0b57ab4da578c3c3440f515");

	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01.prefab:f3c652cba42256c42866227f75dbd60e");

	private List<string> m_VO_BTA_BOSS_15h_IdleLines = new List<string> { VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01 };

	private List<string> m_missionEventTrigger103_Lines = new List<string> { VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02 };

	private List<string> m_missionEventTrigger100_Lines = new List<string> { VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04 };

	public bool shouldEnemyActorExplode = true;

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_15()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01, VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01,
			VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_EasterEggAmulet_02, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_02, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_03,
			VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_HeroPowerTrigger_04, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleA_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleB_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleC_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_IdleD_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01, VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_15h_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossDeath_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		if (shouldEnemyActorExplode)
		{
			return true;
		}
		return playState != TAG_PLAYSTATE.WON;
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
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_15_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_BossStart_01);
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayRandomLineAlways(enemyActor, m_missionEventTrigger100_Lines);
			break;
		case 101:
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AmbushTrigger_01);
			break;
		case 102:
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Demon_01);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			shouldEnemyActorExplode = false;
			m_deathLine = null;
			yield return PlayRandomLineAlways(enemyActor, m_missionEventTrigger103_Lines);
			yield return PlayLineAlways(enemyActor, VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 104:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointA_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_15_MidpointB_01);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_MidpointC_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Attack_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_15_VictoryA_01);
			yield return PlayLineAlways(enemyActor, VO_BTA_03_Female_Broken_Mission_Fight_15_VictoryB_01);
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
			case "BT_354":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_BladeDance_01);
				break;
			case "BT_429":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_Metamorphosis_01);
				break;
			case "BT_493":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Hero_PriestessofFury_01);
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
			case "BT_702":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_AshtongueSlayer_01);
				break;
			case "CS2_077":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Sprint_01);
				break;
			case "EX1_126":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_15h_Female_Broken_Mission_Fight_15_Boss_Betrayal_01);
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		_ = turn;
	}
}
