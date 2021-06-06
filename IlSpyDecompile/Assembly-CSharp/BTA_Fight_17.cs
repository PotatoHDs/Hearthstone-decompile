using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Fight_17 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01.prefab:b981744b125d02e41a2bacb71120550b");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01.prefab:dfbcd7e9af0181d4db070eaca0e0c3f0");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01.prefab:f9182f0644b9773459269f5707b1242f");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01.prefab:05fc5131c55cd7542b2991622a6f0e51");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01.prefab:23b40f5f91306d6458a21956a1e3c8e5");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01.prefab:af8cdb376b56c5c4c8b8f3732df6767e");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01.prefab:6afb84689f8dc814e90db225dd10a511");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01.prefab:1a74dd1f109b4b5498f4b08aa11e31d9");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01.prefab:cebb08a64d2c1a24bbef8d8cefd223bf");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossDeath_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossDeath_01.prefab:c1f4a6474b7647c4e95b21a42b010610");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01.prefab:5fcbef2241013ae458698f7632b124df");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01.prefab:4af05b8c4a932dd4eb607d57c1710684");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01.prefab:1102a41180e6a194f9a08d51f4595bc4");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01.prefab:62ef80ef3008bca49a031afe15af2028");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01.prefab:f5ac5cb19cb255947801a48f02cbc076");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01.prefab:0b5455531d74bba4796fa251229623e2");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01.prefab:3b5d55db59c3c71499c3f9d6073ec53d");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01.prefab:3eed315e9279e004ea65d7f193380b44");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01.prefab:42d200acac8d10643b0aa17c7b746024");

	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01.prefab:797919cc75f886747bbffa12f0f959d7");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_17()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossDeath_01,
			VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01
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

	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
		yield return PlayLineAlways(actor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01);
		yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01);
		GameState.Get().SetBusy(busy: false);
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected IEnumerator PlayFollowupQuote()
	{
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01);
		GameState.Get().SetBusy(busy: false);
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
		switch (missionEvent)
		{
		case 101:
			PlayFollowupQuote();
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01);
			GameState.Get().SetBusy(busy: false);
			yield return new WaitForSeconds(0.1f);
			yield return PlayFollowupQuote();
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 104:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01);
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
			yield return PlayLineAlways(actor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01);
			break;
		case 3:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01);
			break;
		case 5:
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01);
			break;
		case 7:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01);
			break;
		case 9:
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01);
			break;
		case 11:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01);
			break;
		case 13:
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01);
			break;
		case 15:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01);
			break;
		case 18:
			m_DisableIdle = true;
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologueBoss);
	}
}
