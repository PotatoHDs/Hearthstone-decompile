using System.Collections;
using System.Collections.Generic;

public class BoH_Malfurion_05 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02.prefab:baa16b2933ca0b24eb3a568dfba5eaae");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03.prefab:4ac0355b281527b479cb53ae2866e4cd");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01.prefab:f4b3f9005369ae149a6e2f2c9aeba8aa");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01.prefab:be68fd4770d4fc54c9e6e166c36240c2");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03.prefab:7e79f922d4688ef40821adbb6e0d1e38");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04.prefab:b084f69490e5c3b49a4cb4aa312d4264");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01.prefab:83f2bcbc02944e94091739d2bd5b04b8");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01.prefab:7a12755030c032b4f981a51f9693c755");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02.prefab:51f3204ff69bbd343970a5d7dc33c952");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03.prefab:7fa22da573b746547aafdb82eed5b60f");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01.prefab:ec582d94ab2d40a4a9c8e6f765795c95");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02.prefab:bbb6fcd38143b4846b6f8515e3ab7761");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03.prefab:43b670310ca68374fa69076f7a490948");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01.prefab:4c7b9ebafd81a3644b03a997f7ae85aa");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01.prefab:2cdcfabacb3976941bf27b533f063283");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02.prefab:21bc5da3d9486f546a26c5775282fa42");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01.prefab:bb73c8acd0807e7419a913dbd0688f40");

	private static readonly AssetReference VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01 = new AssetReference("VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01.prefab:35c46dfe3a887ed44832ef4113aa1921");

	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02.prefab:e08c6437e199cc342bb3d20a3d24e060");

	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02.prefab:9cc110946978bc64db3797a8e746d649");

	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	public static readonly AssetReference MaievBrassRing = new AssetReference("Maiev_BrassRing_Quote.prefab:32a15dc6f5ca637499225d598df88188");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_05()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03,
			VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01, VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologueBoss;
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01;
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03);
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(MaievBrassRing, VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01);
			yield return PlayLineAlways(TyrandeBrassRing, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02);
			break;
		case 3:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01);
			yield return PlayLineAlways(TyrandeBrassRing, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01);
			break;
		}
	}
}
