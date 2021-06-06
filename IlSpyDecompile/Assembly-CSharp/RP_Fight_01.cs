using System.Collections;
using System.Collections.Generic;

public class RP_Fight_01 : RP_Dungeon
{
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01.prefab:a5b20eec3ba5adc4093b9005a1663244");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01.prefab:284f8062fddb2484e9c1e60c4e450680");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01.prefab:7860c64a2106ec8498b3052a945f4de8");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01.prefab:424406dd090e8274192c8be6f9a3815c");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01.prefab:e2f258f89e2a59a45ab47a967a244920");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01.prefab:d3d7e71ad1e60f645a8a653b06f35998");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01.prefab:171c013ea75af8a4fbadb9e6274f354c");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01.prefab:8e2488eef3bac7f44b78761b488e5588");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01.prefab:bff90af9a86e46546a7288f5a93b4272");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Intro01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Intro01_01.prefab:3cbaf7029a3940d4c806633d099443c4");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01.prefab:a576a2f45112ab64b995f4cfed9cf08b");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01.prefab:d1b0b0dccedbaca49a2e3ee7fd23d6cb");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01.prefab:bf01a3c975acddd428a09a320f6f0091");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01.prefab:1ef60c386c190b445aa16997c878bec3");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01.prefab:49a123fd372abb246b60ffabf35a625c");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01.prefab:acef5ea7ccea5b54c8a3a6ed1da1f32f");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01.prefab:f0115342f8307ca43a3132e7c7c30235");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01.prefab:14e58cb84c672084ca79100a523cdfac");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01.prefab:3e3429f2ada367c4f9fa4eb9aa110549");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01.prefab:5e40fc7764481ad4db6e107f4b9c1386");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01.prefab:8b115fc72844cb34a820ba1aa0814228");

	private List<string> m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_Lines = new List<string> { VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01 };

	private List<string> m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_Lines = new List<string> { VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Intro01_01,
			VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01, VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01,
			VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01
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

	public override List<string> GetIdleLines()
	{
		return m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_Lines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_Lines;
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01, Notification.SpeechBubbleDirection.BottomLeft, actor2));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineAlways(actor, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01);
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
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01);
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01);
			break;
		case 6:
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01);
			break;
		case 11:
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01);
			break;
		case 15:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01);
			break;
		}
	}
}
