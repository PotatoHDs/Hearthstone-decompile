using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RP_Fight_03 : RP_Dungeon
{
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01.prefab:864ef6f49a406a843a710af7d097b76d");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01.prefab:e2f719220d6340c4f9bd920d88ead4ac");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01.prefab:da08bc587092cb14780174b64e830ef9");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01.prefab:ba8d6be861f6e9e4f80ef7817b5883b3");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01.prefab:75de0e2fe759a214c9653c305353207b");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01.prefab:6bbad5573a5b4e4458f38c2c457b7663");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01.prefab:86ee6faadc0e2b642bf2e14d4cb2c39e");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01.prefab:103677f2477b355499d45c9c2196e3cb");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01.prefab:4b25ab45deac7654a8ce0c57c1970c9f");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01.prefab:c8fc5b1878323cb4f912085b640e10b4");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01.prefab:82462f2062fad824584dbfb424029179");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01.prefab:b151abfab37932a4c81cdd03797febf7");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01.prefab:bf5e1e8c0c3751a439ad492a386e137a");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01.prefab:90ac6117d2587dc45b2ea5db27a3ad47");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01.prefab:ea566184267863141969ade661c7ad46");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01.prefab:29891c2b1e96fc744b46c350f9322cf5");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01.prefab:49140cd1ef5700e47912e12c3a8fc256");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01.prefab:38235784bde88da4389f99c72727e087");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01.prefab:58ee5e57c59dbf342a42bfdc9d08028d");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01.prefab:309f10957d14c434aa90ab69cfb672ce");

	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01.prefab:c63dd330438410c42845403a3b755dfa");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01.prefab:cd1be84f2f62d8043ad896a21db67742");

	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	private List<string> m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_Lines = new List<string> { VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01 };

	private List<string> m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_Lines = new List<string> { VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01,
			VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01,
			VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01
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
		return m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_Lines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_Lines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType != EmoteType.START && MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		yield return PlayLineAlways(IllidanBrassRing, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01);
		yield return PlayLineAlways(enemyActor, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01);
		yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01);
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
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(IllidanBrassRing, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 503:
			yield return new WaitForSeconds(7f);
			yield return PlayLineAlways(IllidanBrassRing, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01);
			yield return PlayLineAlways(enemyActor, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01);
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
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01);
			yield return PlayLineAlways(enemyActor, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01);
			break;
		case 6:
			yield return PlayLineAlways(enemyActor, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01);
			break;
		case 11:
			yield return PlayLineAlways(enemyActor, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01);
			break;
		case 15:
			yield return PlayLineAlways(enemyActor, VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologueBoss);
	}
}
