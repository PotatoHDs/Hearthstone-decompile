using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Fight_13 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01.prefab:bae570e3f5e92504dafd2ec014f95469");

	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02.prefab:db1012ddaade0eb4c80841a7979488a9");

	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01.prefab:3f7c607714785d14a90a5d6f9e9942df");

	private static readonly AssetReference VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01 = new AssetReference("VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01.prefab:04ab7d9bb0134fb43b507218e50093c9");

	private static readonly AssetReference VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01 = new AssetReference("VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01.prefab:832f26ddd691333499c84771674e9729");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01.prefab:1ea54479130e6e74b8486ceacca9e138");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01.prefab:45d04acd3518701488e6ecc12f075a01");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01.prefab:a4884c283694ef9439867c3d95f8fe0a");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01.prefab:5d7d05fa5f5e7f840a75b4997f715254");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01.prefab:393f6dd3c7f07704b8c09513b8fede3b");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02.prefab:6365b6327d264b940a47b433328f16d7");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01.prefab:9b94ae21a9aba4446b87d1bf7997e806");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01.prefab:300df50b2d1aa594e911513834cfd40e");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01.prefab:dd4999b21305f404c994e01585b277bd");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01.prefab:53c16de44f503914995c745164c42360");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01.prefab:f6bcaa55d4a8ddd46b51ed06755dacdc");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02.prefab:06affe6dba6315a4994e4cffcbfbedf9");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03.prefab:3f006288048bea248b9116041f7600ff");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01.prefab:815d3d852c6f7484896ca5aa7ce0408a");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01.prefab:01620e4baee2a4c4d81940a48586365b");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01.prefab:0903eb35b0e73794aad8171c1eb0c72b");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01.prefab:bdc5f5b29b1db2d408bdd8ce273ba905");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01.prefab:766c798064cdb204f8591f2698884b8e");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02.prefab:f0f69149f5582114e818dfc7cb485f6f");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01.prefab:5a4a77be23df1e647b603812a049dbe8");

	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01.prefab:42ddb238f95e9c04e9c9615e3133008c");

	private Notification.SpeechBubbleDirection m_OgreMechSpeechBubbleDirection = Notification.SpeechBubbleDirection.TopLeft;

	private List<string> m_VO_BTA_BOSS_13h_IdleLines = new List<string> { VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01 };

	private List<string> m_missionEventTrigger501_Lines = new List<string> { VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01, VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01 };

	private List<string> m_VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_Lines = new List<string> { VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02 };

	private List<string> m_VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_Lines = new List<string> { VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03 };

	private List<string> m_VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_Lines = new List<string> { VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02 };

	private List<string> m_VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_Lines_Copy = new List<string> { VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_13()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01, VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02, VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01, VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01, VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01,
			VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01,
			VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_13h_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_Lines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01;
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
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02, m_OgreMechSpeechBubbleDirection);
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_10"), BTA_Dungeon.ShaljaBrassRingDemonHunter, VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 507:
			switch (Random.Range(1, 3))
			{
			case 1:
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02);
				break;
			case 2:
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03);
				break;
			}
			break;
		case 508:
			switch (Random.Range(1, 3))
			{
			case 1:
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02);
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02, m_OgreMechSpeechBubbleDirection);
				break;
			case 2:
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01);
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01, m_OgreMechSpeechBubbleDirection);
				break;
			}
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "BT_495":
			yield return PlayLineAlways(actor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01);
			break;
		case "BT_509":
			yield return PlayLineAlways(actor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01);
			break;
		case "BT_510":
			yield return PlayLineAlways(actor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01);
			break;
		case "BT_514":
			yield return PlayLineAlways(actor, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01, m_OgreMechSpeechBubbleDirection);
			break;
		case "BTA_10":
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_10"), BTA_Dungeon.ShaljaBrassRingDemonHunter, VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01);
			break;
		case "BTA_08":
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02);
			break;
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BTA_14":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01);
				break;
			case "BTA_15":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01, m_OgreMechSpeechBubbleDirection);
				break;
			case "BTA_16":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01);
				break;
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		_ = turn;
	}

	public override float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.5f;
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = GetThinkEmoteBossThinkChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && m_BossIdleLines != null && m_BossIdleLines.Count != 0)
		{
			GameEntity.Coroutines.StartCoroutine(PlayPairedBossIdleLines());
			return;
		}
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
			.PlayEmote(emoteType);
	}

	protected IEnumerator PlayPairedBossIdleLines()
	{
		int num = Random.Range(1, 3);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (num == 1)
		{
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01, m_OgreMechSpeechBubbleDirection);
		}
		else
		{
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01, m_OgreMechSpeechBubbleDirection);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01);
		}
	}
}
