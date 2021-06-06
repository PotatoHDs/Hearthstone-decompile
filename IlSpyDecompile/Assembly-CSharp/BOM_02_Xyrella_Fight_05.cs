using System.Collections;
using System.Collections.Generic;

public class BOM_02_Xyrella_Fight_05 : BOM_02_Xyrella_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01.prefab:5cb01fd878f58254094c172c34c20a5e");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01.prefab:d510827255cd01d4bb98a1546c7a52d3");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01.prefab:6932cac834076ae41a6ea20ad360ba40");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01.prefab:c380c6e983f083b448d8ea298b5a27ea");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04.prefab:a04e1c633b15ce9428cd2c1face02304");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02.prefab:d6780a350f3fabd439e6fb5a2fa8a464");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01.prefab:1f58765e81ee4f744bd32a63099fb6c7");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01.prefab:0de7906fec741ea488d4d36b01862c45");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02.prefab:a90438929cfcded4d85726c665da6d57");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03.prefab:0f6c3ac3ac678d8459d6f199b69ee9ea");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01.prefab:c60bda170d386dc43ad3f5710e2defd3");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02.prefab:9cf5237501fd07345b1a766c3ab751f2");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03.prefab:95d19b58a884a9249bf2946e6cc3a473");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02.prefab:e598517e555b83e4092d2d996cf9d50f");

	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01.prefab:03c0e7a1d0a3f654ebf2adfc8b4dc8f3");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02.prefab:fbfc4fa99208be440962e10bd592ac5e");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_01.prefab:4b7acc29e927e9a4d8bf496ccdfeb189");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_02.prefab:213536285a7d20442b4d3f3350dace87");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03.prefab:cd01ba250041c1241bc408c35c0d6b73");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02.prefab:69a736ed759eaec41af9d6ca9d53f7e4");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01.prefab:468c06e51078f81408f3dad5b93147fd");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02.prefab:07e8412050bbc344abdf35c50ddc2332");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01.prefab:6b942914f84dfea42b371cde18b5ec62");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03 };

	private List<string> m_InGameBossIdleLines = new List<string> { VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03,
			VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02,
			VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetBossIdleLines()
	{
		return m_InGameBossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BOT;
		base.OnCreateGame();
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
		case 514:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(Tavish_BrassRing, VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01);
			break;
		case 517:
			yield return MissionPlayVO(enemyActor, m_InGameBossIdleLines);
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
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
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04);
			break;
		case 13:
			yield return MissionPlayVO(Tavish_BrassRing, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02);
			break;
		case 17:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02);
			break;
		}
	}
}
