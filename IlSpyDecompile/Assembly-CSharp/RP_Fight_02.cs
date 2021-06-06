using System.Collections;
using System.Collections.Generic;

public class RP_Fight_02 : RP_Dungeon
{
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01.prefab:f92d9bdfed7a3be4db70f1ff18c9979a");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01.prefab:e1861c2ee00fdbe4391f2c0c12328cb9");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01.prefab:c0adf84ac3175a149bdadf1fd310e7d5");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01.prefab:6192eff89b44d0a4092918c3157c8d7a");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01.prefab:a6366b765125c4e4089263d512eeb732");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01.prefab:8af7cef14d53b8349a5faa2085ae58ce");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01.prefab:36e898b055d997944be789464952985f");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01.prefab:d240bd5183f961b488b41c92171e7e7b");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01.prefab:19f631e11326bca4f9ecbc4050ede113");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01.prefab:24b785422e9c85f42a435d5ba3022d7d");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Intro01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Intro01_01.prefab:e284c48590589ce46b82999f59c80369");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01.prefab:634f4a13b95399c4d87cdaa39542c3c1");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01.prefab:b0120a614688b6e48ae8404190c4199d");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01.prefab:bfb880c645aca7c44a0333399e368d4c");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01.prefab:a95245d1ba9688b4585fe037f4739170");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01.prefab:4bbf74b2904c05b4a8fd550a45c4aa6f");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01.prefab:727b904e47db35a449cfd30509ce098e");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01.prefab:09925a9d93b0000429ee562d29bb9606");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle01_01.prefab:a55b7fb2790954a41ba2548d019d4880");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle02_01.prefab:569771892749dc348b69a9e98c485ba6");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle03_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle03_01.prefab:502a7c83181dcc840a0a0083d95da5ce");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01.prefab:6e01829211e285b4387383f483713b77");

	private List<string> m_VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_Lines = new List<string> { VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01,
			VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Intro01_01, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle01_01, VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle02_01,
			VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle03_01, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_Lines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01, Notification.SpeechBubbleDirection.BottomLeft, actor2));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01);
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
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01);
			break;
		case 3:
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01);
			break;
		case 6:
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01);
			break;
		case 11:
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01);
			break;
		case 15:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01);
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01);
			break;
		}
	}
}
