using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Fight_14 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_14_BossStartReaverResponse_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_14_BossStartReaverResponse_01.prefab:86f5af34c713a064c91e870a2bc1eabe");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01.prefab:d373fa6a9ce5df946a0cde85932538a6");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01.prefab:89b7923056f049b4a939ab39efce5e8c");

	private static readonly AssetReference VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01 = new AssetReference("VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01.prefab:25170d86adb671e4d8e4b8c8d3c91d96");

	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01.prefab:f6375b435acf09847954a6079d072f57");

	private static readonly AssetReference VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01 = new AssetReference("VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01.prefab:b7fed89fc1054af4c94cc5159ed38678");

	private static readonly AssetReference VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01 = new AssetReference("VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01.prefab:3b6fb5f258ae57b46b780b6c4c9be4cd");

	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Death = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Death.prefab:e6a44b4fcf5c0774c859ebab4fa65461");

	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Defeat = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Defeat.prefab:246418e687442c84382afc90ea2e35c2");

	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Intro = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Intro.prefab:0dce97737b2e8464591b83dc06dfe47d");

	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Taunt = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Taunt.prefab:9329c646808cb0c41b4f065d80b2d40b");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_14()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_14_BossStartReaverResponse_01, VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01, VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01, VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01, VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01, VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01, BTA_BOSS_14h2_RustedFelReaver_Intro, BTA_BOSS_14h2_RustedFelReaver_Death, BTA_BOSS_14h2_RustedFelReaver_Defeat,
			BTA_BOSS_14h2_RustedFelReaver_Taunt
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = BTA_BOSS_14h2_RustedFelReaver_Death;
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
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01);
		GameState.Get().SetBusy(busy: false);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (cardId == "BTA_BOSS_14h")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "BTA_BOSS_14h2")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(BTA_BOSS_14h2_RustedFelReaver_Taunt, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			GameState.Get().SetBusy(busy: true);
			PlaySound(BTA_BOSS_14h2_RustedFelReaver_Intro);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_06"), BTA_Dungeon.SklibbBrassRingDemonHunter, VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01);
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
			string cardId2 = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId();
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "BTA_08" && cardId2 == "BTA_BOSS_14h2")
			{
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01);
			}
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
		if (turn == 3)
		{
			PlaySound(BTA_BOSS_14h2_RustedFelReaver_Intro);
			yield return new WaitForSeconds(2f);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01);
		}
	}
}
