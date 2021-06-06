using System.Collections;
using System.Collections.Generic;

public class BoH_Anduin_02 : BoH_Anduin_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01.prefab:0551229c227e1b449a624ac9a65700c2");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01.prefab:a818ca367aee3414183616d8efb6b61a");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01.prefab:f25ea827c4bc4ce291766cad764e0f94");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03.prefab:e7b3f58332e299146ab64219eee6bdc8");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02.prefab:4d74e9fa7670ad9478148b2a35691ebf");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02.prefab:fd97382211c9ab445bab5d26b98397af");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01.prefab:ff0e37310038d68438b8335d8512f633");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02.prefab:81da409b6daf7214eb601d93ee5354ea");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02.prefab:0d3e7d6e1b634924bbc27715396abbc8");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02.prefab:50cb5e6205747fb4d8787c54ea4e3ead");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01.prefab:4e000bc14de100d4a9763f131ee9ce90");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02.prefab:54fc90dae5aaa25459fb98d4faff7610");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03.prefab:f9c3c2fa810126042adefe7c5bd7fa84");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01.prefab:da2b7baf7f818e343b54d284732c7166");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02.prefab:7878d4e85944e1648bcc90f7c95e6ef7");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03.prefab:92ac8ac41c8b938469440eb14220ade4");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01.prefab:36cd16043cea5054dbd0995bc4b4a68c");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03.prefab:8da41afa8342dd045baf4b38b097b27f");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01.prefab:afd0be6876e8e894a81ea9e051f2afe0");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01.prefab:182b42cb9887c9940997e67d8214d98b");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02.prefab:4d023795b20ab504c8c5d27f1c6f681a");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Anduin_02()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02,
			VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01,
			VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03);
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
		m_standardEmoteResponseLine = VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01;
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
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01);
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
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02);
			break;
		case 11:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03);
			break;
		}
	}
}
