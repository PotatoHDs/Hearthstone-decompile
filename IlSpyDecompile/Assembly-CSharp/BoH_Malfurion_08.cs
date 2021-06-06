using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Malfurion_08 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02.prefab:cef8fed293cffee4199974c22bf7f691");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02.prefab:eb84b2a6db7752a4284a1642db4dc689");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02.prefab:5b82789dba0e9a742b50283d5de2aa15");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03.prefab:ca565754cb17bc8429ba0a970c5ba233");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04.prefab:19896f6b77a8dd945b570cd62534a4b8");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_05 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_05.prefab:3aca5495e75012848b94079613816ca7");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_06 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_06.prefab:92435de6df5cab34481db3ba503520a9");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01.prefab:77fcd9344734e364493208a2aebd9e63");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01.prefab:9a8ca9200cd75064286c9fe4fb2a5c8f");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02.prefab:eaa8737fba71fa84dba3befade1ccc5b");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04.prefab:e5ec8a5ebad31554985870fab61ea596");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01.prefab:30ed87612411a9648b26b860d2312697");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01.prefab:094bf9c9302958c4980fb12795e35080");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04.prefab:b3bb664c9d96a6a4b822998b93104e1d");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05.prefab:daf576e796fe2ed4ca58e75cb5fce4c0");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01.prefab:e7de37a0ba78ca2439b1c9549db75793");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01.prefab:39fffad8b3b710a4f8d67ca1b65aec01");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01.prefab:98ad199d5fcadd446b58190ffe9e6a7a");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01.prefab:f590e241f8b5b5d4f80b739b68395a94");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04.prefab:e4b4160c0edd9c14e8be5aaa53cbc827");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01.prefab:d3772c8486312064da4b81f7eb893642");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02.prefab:fca33c26f99944e47ad463a737280368");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03.prefab:b0b842d84d15eba4ea085c74d63ab24d");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01.prefab:b3f4ceb3cbf6e0541abd1aef57905d6f");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01.prefab:b730811db48755f469e042bb01b55b89");

	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02.prefab:6997b8e2512d4f24baa2fbcb8d697e0e");

	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02.prefab:bd3d776874b3ee247bfcb2b9bdee14dc");

	private static readonly AssetReference SaurfangBrassRing = new AssetReference("Saurfang_BrassRing_Quote.prefab:727d1e09f5a40f649afa7ed2f3e70564");

	private List<string> m_EmoteResponseLines = new List<string> { VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03 };

	private new List<string> m_BossIdleLinesCopy = new List<string> { VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03 };

	private List<string> m_BossSaurfangIdleLines = new List<string> { VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05 };

	private List<string> m_BossSaurfangIdleLinesCopy = new List<string> { VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_08()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01,
			VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02,
			VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override void OnCreateGame()
	{
		m_OverrideMusicTrack = MusicPlaylistType.InGame_DRGEVILBoss;
		base.OnCreateGame();
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
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_Tyrande"), VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			yield return PlayLineAlways(SaurfangBrassRing, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02);
			yield return PlayLineAlways(SaurfangBrassRing, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_Tyrande"), VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			if (GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId() == "Story_08_Saurfang_008h2")
			{
				yield return MissionPlayVO(enemyActor, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01);
			}
			else
			{
				yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01);
			}
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	public override IEnumerator OnPlayThinkEmoteWithTiming()
	{
		if (m_enemySpeaking)
		{
			yield break;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			yield break;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		float thinkEmoteBossIdleChancePercentage = GetThinkEmoteBossIdleChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossIdleChancePercentage > num || (!m_Mission_FriendlyPlayIdleLines && m_Mission_EnemyPlayIdleLines))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (cardId == "Story_08_Saurfang_008h2")
			{
				string line = PopRandomLine(m_BossSaurfangIdleLinesCopy);
				if (m_BossSaurfangIdleLinesCopy.Count == 0)
				{
					m_BossSaurfangIdleLinesCopy = new List<string>(m_BossSaurfangIdleLines);
				}
				yield return MissionPlayVO(actor, line);
			}
			else if (cardId == "Story_08_Sylvanas_008hb")
			{
				string line2 = PopRandomLine(m_BossIdleLinesCopy);
				if (m_BossIdleLinesCopy.Count == 0)
				{
					m_BossIdleLinesCopy = new List<string>(m_BossIdleLines);
				}
				yield return MissionPlayVO(actor, line2);
			}
		}
		else if (m_Mission_FriendlyPlayIdleLines)
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType)
				.GetActiveAudioSource();
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
			yield return PlayLineAlways(actor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(actor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02);
			break;
		}
	}
}
