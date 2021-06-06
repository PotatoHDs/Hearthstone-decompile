using System.Collections;
using System.Collections.Generic;

public class BoH_Jaina_08 : BoH_Jaina_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01.prefab:fbc72de6710c2364c81a98e37280b5f0");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01.prefab:49250351557f61440aaa16548117f595");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01.prefab:17d70f81add992e40a9f0f4ac5a70fc5");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01.prefab:2a229c03821ff9b43a4643fb3397a13c");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01.prefab:20161037a0b160c4dab3a4092cabc7ea");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01.prefab:474d4054251041d44bae684265b8bdaf");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02.prefab:f24767464968cfe4ba9049b6f612b34a");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03.prefab:e6e603b63904d7848b4053f8bfd2ef9a");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01.prefab:baf05bfec7a95ef40800fc39814ac330");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02.prefab:ec00245c1a07bef4fbb62bb38dcdebea");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03.prefab:1d0fb7287ab7dff4da9898d0a98f3fbe");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01.prefab:87d63628f43897744b211feb7e766ac3");

	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01.prefab:ce1ac52480e326d4787ddee42735507f");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01.prefab:1b276a3e89a5c4c469b09f738f32deda");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01.prefab:0d3348dd7ec33474fa7b158365fcc8e6");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01.prefab:8b7754c3a70a79a4f94ce2ec283a5086");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01.prefab:c1f26293e6483e74eb4a97b295bb7eca");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01.prefab:898fb49c280bfb644acd6fe7c9249114");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_01.prefab:e8c982df62483924f871940dba3a6dab");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_02.prefab:04b4f7f1a0c948e4cb00c506bfc33395");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_03 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_03.prefab:5cb58b8880cde904cb7ff6aa03145202");

	private List<string> m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPowerLines = new List<string> { VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03 };

	private List<string> m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8IdleLines = new List<string> { VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Jaina_08()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02,
			VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_02,
			VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_03
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

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01;
		m_standardEmoteResponseLine = VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01;
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 504)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01);
			GameState.Get().SetBusy(busy: false);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
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
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(enemyActor, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01);
			break;
		case 7:
			yield return PlayLineAlways(enemyActor, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01);
			break;
		case 9:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01);
			yield return PlayLineAlways(enemyActor, VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGEVILBoss);
	}
}
