using System.Collections;
using System.Collections.Generic;

public class BoH_Valeera_04 : BoH_Valeera_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02.prefab:dd2109fe438749c4599c34d7243532d2");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02.prefab:1ca6cc69fe12fe14b967ae296ebe4ea1");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04.prefab:cc562277732f2f6498fd567898469d1b");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01.prefab:f9e8f04ef7aae384bbe30e77df4b9fdf");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01.prefab:748c18b7b52d09b49a1536bfcc30297b");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01.prefab:157cff44b3f14a64593d1f30a78c0393");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01.prefab:41d0a1f9a407add43bcda581166286e5");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02.prefab:792f6aa0ff77c85479a412042ae215f0");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03.prefab:e5679e2be5dfd6b40a915e6a4d5e0379");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01.prefab:fb1aca4772cf0064398153c0ea1e8bff");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02.prefab:94c65fe0345b00d4faa1ed02a194a0de");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03.prefab:bf5d0de953abd3d4fa1cd5408a2d27e3");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01.prefab:cf87ece7f8cf3234a8d1588c55ff8e08");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01.prefab:c163ec3c03658af4eb19f1036e4eef87");

	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01.prefab:fe4234dfdd5be7b4a8f6568c843dee50");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01.prefab:98bd91f390743bf4aba9425254c647ab");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03.prefab:8b53af1c3bbad7e4cb275990db51a086");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02.prefab:2bca6024fcf6c8a46856c26f432b718e");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03.prefab:654c0f5dddd28df4cbadbedad0d285a1");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02.prefab:58ad4e7fe7f56ec40a130abc4cff96e2");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01.prefab:1de707331d28620448c15efd9e20ce71");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_03.prefab:c0e05ff08be572940a1893086ead4daf");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03.prefab:7863afee8b3a15d4e9b2a606917b33e3");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02.prefab:0ccb86e6efd0efd449633932bfe9baef");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05.prefab:94d1130ab18533145a0805b198a56fcb");

	public static readonly AssetReference VarianBrassRing = new AssetReference("Varian_BrassRing_Quote.prefab:b192b80fcc22d1145bfa81b476cecc09");

	public static readonly AssetReference BrollBrassRing = new AssetReference("Broll_BrassRing_Quote.prefab:1bfe5acde48846249b4b7716c3ff0d8c");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01,
			VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02,
			VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_03, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01);
		yield return MissionPlayVO(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03);
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
		m_deathLine = VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01;
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BT;
		m_standardEmoteResponseLine = VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01;
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			yield return PlayLineAlways(actor, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02);
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03);
			yield return PlayLineAlways(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(actor, VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02);
			break;
		case 7:
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01);
			yield return PlayLineAlways(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02);
			break;
		case 11:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01);
			yield return PlayLineAlways(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02);
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03);
			break;
		}
	}
}
