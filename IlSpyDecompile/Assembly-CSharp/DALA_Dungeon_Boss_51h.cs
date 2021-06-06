using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_51h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01.prefab:902c69955c35bf944a6cd10191920c38");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Death_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Death_02.prefab:d116280bede237d4dba75d1bf742d017");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_DefeatPlayer_01.prefab:b7b527b716e18f045979213e1f5a3da3");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01.prefab:71cb02d9bfdcbf04bba9c66a6b3c4d1b");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01.prefab:61529b1bf9c6da44f84826833d7f81f6");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02.prefab:774bf5d0eea789e41bbb2ba817774e86");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03.prefab:da1321e86d64a4049bb54657c1a9a484");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04.prefab:c7ca85e3db2e8844aac7fa22ff551b80");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05.prefab:cb4bdbb872d53454296851cdc648e1c3");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06.prefab:5aef9ce42c7f31a45a5ab40f45344eec");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Idle_01.prefab:b12153bc9aaa2874a9bf9ea633a2fa07");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Idle_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Idle_02.prefab:1df5dd1fe1c55b945ba9797e11e999eb");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Idle_03.prefab:370fdfe6c0f8d7f4c9599a43f1203c89");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Intro_01.prefab:35e0c050d31cc934ab0bd8ad0f36a651");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01.prefab:8c5c335b826eeed4abe785234492cc04");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01.prefab:863a24787ab908944af66b322345aec0");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01.prefab:43a58b271a6a1d34a99d6f48d33a8f0d");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01.prefab:cd954e939fbb7ba4e8a75069bd4137a3");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02.prefab:24693a1f0214d6a4b90143ce6bab52be");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03.prefab:ed85e2850d5fba2499cc106dfe513b7b");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01.prefab:c6c8d4a827188c04290e8de2ada52dbd");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01.prefab:48409dd673a24c043a1d9edda49b157f");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01.prefab:abb42e2131175cd40b99fe83aea3c5a4");

	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01.prefab:cad1cbdead731ed4dbf6b8db4758e8ab");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01.prefab:424ae2d26e9c0e8449ea1dbec73cea84");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_02.prefab:3fcf163fdacf69f4c820550e9198ae67");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01.prefab:be513a2b0e9972e40aae962d10f34f77");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02.prefab:b5f65c26ea96cd749b81613e0fc1155e");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03.prefab:bda675ba2aa888c4b87d22a0e403fae5");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_IntroA_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_IntroA_01.prefab:b32aa81844e93e045b0194e8b703e67a");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_IntroA_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_IntroA_02.prefab:91746117746f78049a9618a845756456");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_IntroB_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_IntroB_01.prefab:6426df8a683c16942a3e77f7cdb2d69c");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01.prefab:9b2f090f78249d949845e2525c59f0f9");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02.prefab:b8f5dc5567747f146a775ae3d080c12b");

	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03.prefab:68f7eaa589a430948ab215bdc3fdcd60");

	private static readonly AssetReference Millhouse_BrassRing_Quote = new AssetReference("Millhouse_BrassRing_Quote.prefab:8e3a3b2cb7811ba42b6dbccaafd61fe3");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_51h_Female_Gnome_Idle_01, VO_DALA_BOSS_51h_Female_Gnome_Idle_02, VO_DALA_BOSS_51h_Female_Gnome_Idle_03 };

	private List<string> m_IdleLinesCopy = new List<string>(m_IdleLines);

	private static List<string> m_PlaysMillhouse = new List<string> { VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01, VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02, VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03 };

	private static List<string> m_MillhouseIntroA = new List<string> { VO_DALA_BOSS_51h_Male_Gnome_IntroA_01, VO_DALA_BOSS_51h_Male_Gnome_IntroA_02 };

	private static List<string> m_PlayerMillhouseManaStorm = new List<string> { VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01, VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02, VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03 };

	private static List<string> m_PlayerDrawMillhouse = new List<string> { VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01, VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02, VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03 };

	private static List<string> m_DefeatWithMilhouseInPlay = new List<string> { VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01, VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private bool m_introLinesPlaying;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01, VO_DALA_BOSS_51h_Female_Gnome_Death_02, VO_DALA_BOSS_51h_Female_Gnome_DefeatPlayer_01, VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06,
			VO_DALA_BOSS_51h_Female_Gnome_Idle_01, VO_DALA_BOSS_51h_Female_Gnome_Idle_02, VO_DALA_BOSS_51h_Female_Gnome_Idle_03, VO_DALA_BOSS_51h_Female_Gnome_Intro_01, VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01, VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01, VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01, VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01, VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02, VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03,
			VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01, VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01, VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01, VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01, VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01, VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_02, VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01, VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02, VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03, VO_DALA_BOSS_51h_Male_Gnome_IntroA_01,
			VO_DALA_BOSS_51h_Male_Gnome_IntroA_02, VO_DALA_BOSS_51h_Male_Gnome_IntroB_01, VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01, VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02, VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05, VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06 };
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_51h_Female_Gnome_Intro_01;
		m_deathLine = VO_DALA_BOSS_51h_Female_Gnome_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
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
		switch (missionEvent)
		{
		case 101:
			GameState.Get().SetBusy(busy: true);
			m_introLinesPlaying = true;
			ZoneMgr.Get().FindZoneOfType<ZonePlay>(Player.Side.FRIENDLY).UpdateLayout();
			yield return PlayRandomLineAlways(Millhouse_BrassRing_Quote, m_MillhouseIntroA);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(Millhouse_BrassRing_Quote, VO_DALA_BOSS_51h_Male_Gnome_IntroB_01);
			GameState.Get().SetBusy(busy: false);
			m_introLinesPlaying = false;
			break;
		case 104:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 106:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 107:
			GameState.Get().SetBusy(busy: true);
			yield return PlayRandomLineAlways(Millhouse_BrassRing_Quote, m_PlaysMillhouse);
			GameState.Get().SetBusy(busy: false);
			break;
		case 115:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 108:
			yield return PlayAndRemoveRandomLineOnlyOnce(Millhouse_BrassRing_Quote, m_PlayerDrawMillhouse);
			break;
		case 109:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01);
			break;
		case 110:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01);
			break;
		case 111:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01);
			break;
		case 112:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerMillhouseManaStorm);
			break;
		case 113:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01);
			break;
		case 114:
			GameState.Get().SetBusy(busy: true);
			yield return PlayRandomLineAlways(Millhouse_BrassRing_Quote, m_DefeatWithMilhouseInPlay);
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
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking || m_introLinesPlaying)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			string line = PopRandomLine(m_IdleLinesCopy);
			if (m_IdleLinesCopy.Count == 0)
			{
				m_IdleLinesCopy = new List<string>(m_IdleLines);
			}
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
		}
	}
}
