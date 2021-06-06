using System.Collections;
using System.Collections.Generic;

public class BoH_Anduin_03 : BoH_Anduin_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01.prefab:4908545736995b744a407e7ee4134c89");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01.prefab:476b43efdd7a0d84699c1eef68f5f051");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01.prefab:46d8c2706e2cf8f42820fc5330980349");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02.prefab:d7d978e724f7e1844a0d0a792c91d038");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02.prefab:ab167d1a6079cd240abc998b7b238709");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01.prefab:0d9139c8c4f2cfd4aad6707858662219");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02.prefab:410a029f015a0624a95f499d19a61c78");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02.prefab:0a20e6d4332de6840b1353997da1ebf6");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01.prefab:e77dd6d44479acb41916ca00daea4d79");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02.prefab:42146e3371c173f4cb3c53a82571cb94");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03.prefab:66d7c92c1b2eff445aa3c086b3473225");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01.prefab:4d12cb3fda0eb284c9474679ba155c0d");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02.prefab:7b6736da40b60ff488e71a35dcb7e690");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03.prefab:a903d101ed0956c499d8d89192c2eb9f");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01.prefab:24a1a5ea1d4d4ce428d037986c6bb8fd");

	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01.prefab:8d4c62aafa6bcbb41acd800b88488085");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01.prefab:0849f6c433b00c847bf63d61d48e6f71");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01.prefab:f3b7e17724564814baaed6f0c23ad079");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02.prefab:975d367e47bef8c4b96b50ad3d6eab65");

	private static readonly AssetReference VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01 = new AssetReference("VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01.prefab:5907285304104d1449743d81bc017fed");

	private static readonly AssetReference VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01 = new AssetReference("VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01.prefab:ce942982cab8acc49a96981778b71e56");

	private static readonly AssetReference VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01 = new AssetReference("VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01.prefab:f0b4780cc9dc72848b2053f254892a7f");

	public static readonly AssetReference VarianBrassRing = new AssetReference("Varian_BrassRing_Quote.prefab:b192b80fcc22d1145bfa81b476cecc09");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Anduin_03()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02,
			VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02, VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01,
			VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01, VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		m_standardEmoteResponseLine = VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
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
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02);
			yield return MissionPlayVO(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			yield return MissionPlayVOOnce(GetFriendlyActorByCardId("Story_05_HighPriestRohan"), VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01);
			break;
		case 103:
			yield return MissionPlayVOOnce(GetFriendlyActorByCardId("Story_05_HighPriestRohan"), VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01);
			break;
		case 105:
			yield return MissionPlayVOOnce(GetFriendlyActorByCardId("Story_05_HighPriestRohan"), VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01);
			break;
		case 110:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
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
		case 1:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01);
			break;
		case 3:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01);
			break;
		case 5:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 7:
			yield return MissionPlayVO(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02);
			break;
		}
	}
}
