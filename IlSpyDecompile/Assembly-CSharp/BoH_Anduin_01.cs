using System.Collections;
using System.Collections.Generic;

public class BoH_Anduin_01 : BoH_Anduin_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02.prefab:aa7cbf7409e919847b1ffae3db4df734");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02.prefab:77fd621472b276747b8debf68792404f");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01.prefab:24e9a1cd458be0540816b38f6d117770");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03.prefab:260037a21752be048b754d15a57615e8");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02.prefab:46b083e10f1d6a842b06ab8bc1f2b7c3");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01.prefab:f0c14cdcc45ccb842967914e2ecbe99e");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01.prefab:5882b1257c7f6ff4a8d2de154c0e42b8");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01.prefab:dcdfd8a448d20cc41aeecfbacfb0293d");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02.prefab:dd88a9e2bcfd57a498ab78a7bac4cc6e");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01.prefab:9652ba1809af8014286b16d7a4526217");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02.prefab:ca4b63ff57c78f84d9c00a2153640e00");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03.prefab:5afe7607307634d40ad7640f43bae35b");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01.prefab:225f2d73c513c7543bae3aa0847e458a");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02.prefab:91f154fec9df19e4c9f205435f347df6");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03.prefab:8009fa680afd8d64bb62c017601aaa1e");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01.prefab:134d77bcce6b763429f503ec7911c0c1");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01.prefab:aeb35ee10bcafdf43a751cb0694f4a29");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01.prefab:a3c75474456c2eb46a0d5af2ba7dde7f");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02.prefab:55f37cb0cf500bd4f92a0d798373d5a4");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Anduin_01()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01,
			VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02);
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
		m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_Default;
		m_standardEmoteResponseLine = VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01;
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02);
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
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02);
			break;
		case 5:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02);
			break;
		case 11:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03);
			break;
		}
	}
}
