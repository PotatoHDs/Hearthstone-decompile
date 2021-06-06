using System.Collections;
using System.Collections.Generic;

public class BTA_Prologue_Fight_01 : BTA_Prologue_Dungeon
{
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01.prefab:b116d4fe7f1d2aa41a293473461f3e36");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01.prefab:18612a4888476a7458e21cc8cfec740a");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01.prefab:7ef550bfd0dcbfd468f7db8c81e35be0");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01.prefab:082aa46eeed9c4d41b8f2b5036350b09");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01.prefab:e5ba2f11c0263754aa8471fb979c4e36");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01.prefab:c8e06e81e3cb3914e9bbb7ec915f88d7");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01.prefab:5cdfc42bc4a478346ba84da891736536");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01.prefab:3b90ad121c4598846a05db76b9be6d42");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01.prefab:6a80e52701a77564694bc6eb45b2a134");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01.prefab:665469cd255111c43bbe8e7b36426483");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Intro_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Intro_01_01.prefab:8935d821332350e43a354c33a69ceec3");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01.prefab:962b6f348de84524e84ce0f67d6383a0");

	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01.prefab:7c92ea0283e6be04593365633abdfdf6");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01.prefab:131dfa98a79df9d43abf851b7b7c1028");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01.prefab:2e77604e8c0d0b943aac30086296eb81");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01.prefab:65852e84660aadc4cadd8f214411102e");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01.prefab:dcfa6e38dc5bc664f887779149def147");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01.prefab:7a77c84195ba7d44b81515da355631fe");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01.prefab:4d8c91e0dd96d9d43a33cf245ebfc83e");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Intro_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Intro_02_01.prefab:ebb76883e6ac52047b1029f9ed3dbf97");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01.prefab:4cc2dd12fa63f9c4c93a937bbcf6e1eb");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01.prefab:544f9254dde3b5540b56c0a737eebb66");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01.prefab:277b830d6d8ac234da940152151e5678");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01.prefab:fe25c39b7c29e854f8f2825afc5ca366");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01.prefab:09ee5546a47f33243bfb84de80de2bf1");

	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	private List<string> m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_Lines = new List<string> { VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01 };

	private List<string> m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_Lines = new List<string> { VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01,
			VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Intro_01_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Intro_02_01,
			VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01
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

	public override List<string> GetIdleLines()
	{
		return m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_Lines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_Lines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "Prologue_ChaosNova"))
		{
			if (cardId == "Prologue_ManaBurn")
			{
				yield return PlayLineAlways(actor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01);
			}
		}
		else
		{
			yield return PlayLineAlways(actor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01);
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
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01);
			break;
		case 2:
			yield return PlayLineAlways(actor, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01);
			break;
		case 4:
			yield return PlayLineAlways(actor, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01);
			break;
		case 6:
			yield return PlayLineAlways(actor, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01);
			break;
		case 7:
			yield return PlayLineAlways(TyrandeBrassRing, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01);
			break;
		case 11:
			yield return PlayLineAlways(actor, VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01);
			break;
		}
	}
}
