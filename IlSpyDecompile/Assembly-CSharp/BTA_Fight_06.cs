using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Fight_06 : BTA_Dungeon
{
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01.prefab:358ae7b7d1e21db46979a72c62aa619f");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01.prefab:9e956cb17d1ef204ea467a03cd28b757");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01.prefab:cc9681b98a543074a811882afb1172c1");

	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01.prefab:69622c1091bdb184aa81eefbe18cb581");

	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01.prefab:c6129540143b14141a64ff2b0a8279d0");

	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01.prefab:d9d6a8bee1c10d84cb5cd7bc9e862fe8");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01.prefab:40687aba7ea96a34ea985ff218daca3a");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01.prefab:1e283f08c14dc314ab5ca8d77714ce6d");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01.prefab:c2cd59e7c1510c148ba7a5cb4a6769ed");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01.prefab:27228471690f3fe40a6f84659bb51909");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01.prefab:1f0fca7db75781145aa9485a7001293b");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01.prefab:022162e98a357df4aa3b7219f3af2533");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01.prefab:ea66fcda4798e664984bc02a820704d7");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01.prefab:ad8fe5800b0ee4741a5eb2cf5dba6ff9");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01.prefab:20c01d309c172ad4ea499a14579a6573");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01.prefab:15cb51a075d0da040bfc4652ef69048d");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01.prefab:47e987a79ff628f49b1910c4a55e3000");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01.prefab:117b2c00284d4e04d811289cc0eb641c");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01.prefab:f629efd6fbdb97b44b2e8abbb9b2f826");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01.prefab:b966e1d7fc4128248aa449c7de2c6baa");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01.prefab:6ca9a92ac6272e540804624d0dc9612f");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02.prefab:7ef624fd693d6fc41a9bfe6ab2c2f468");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03.prefab:80f5fa7bddbc124438247d0b0803b32f");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04.prefab:f74c625cdbea0024f9bd37a958b91d30");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01.prefab:b965da4684bf5bc4ba0d3d2256eb5d3c");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01.prefab:09e1e66fa0ccae34f80a3e36b9e25d35");

	private static readonly AssetReference VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01 = new AssetReference("VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01.prefab:77ad9b70a1674e849974ae59e6cb1060");

	private static readonly AssetReference VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01 = new AssetReference("VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01.prefab:ff7920cc00fef2c48a9fe0cb28374ed9");

	private static readonly AssetReference BTA_BOSS_06t_BrokenDemolisher_StartUp = new AssetReference("BTA_BOSS_06t_BrokenDemolisher_StartUp.prefab:5df6e842a3bc12946aef0be75d58cbb4");

	private List<string> m_VO_BTA_BOSS_06h_IdleLines = new List<string> { VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01 };

	private List<string> m_VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_Lines = new List<string> { VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private Notification m_turnCounter;

	private MineCartRushArt m_mineCartArt;

	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01, VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01, VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01, VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01, VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01, VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01,
			VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01, VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01,
			VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_02, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_03, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_04, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleA_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_IdleB_01, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01, BTA_BOSS_06t_BrokenDemolisher_StartUp
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_06h_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_Lines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossDeath_01;
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
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_06_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_BossStart_01);
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayHalfSpareParts_01);
			break;
		case 102:
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01);
			break;
		case 103:
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_DirtyTricksTrigger_01);
			break;
		case 500:
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_Attack_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			PlaySound(BTA_BOSS_06t_BrokenDemolisher_StartUp);
			yield return new WaitForSeconds(4.3f);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_PlayAllSpareParts_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_06_VictoryA_01);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, VO_BTA_03_Female_Broken_Mission_Fight_06_VictoryB_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 507:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_HeroPower_Lines);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BT_717":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_BurrowingScorpid_01);
				break;
			case "BT_202":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Hero_Helboar_01);
				break;
			case "BTA_03":
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_03"), BTA_Dungeon.BaduuBrassRing, VO_BTA_03_Female_Broken_Mission_Fight_06_Misc_01);
				break;
			case "BTA_05":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Sklibb_Played_01);
				break;
			case "BTA_07":
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, VO_BTA_07_Male_Orc_Mission_Fight_06_Karnuk_Played_01);
				break;
			case "BTA_09":
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, VO_BTA_09_Female_Naga_Mission_Fight_06_Shalja_Played_01);
				break;
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "GVG_083":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_ImprisonedGanarg_01);
				break;
			case "BOT_700":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_FighT_06_Boss_ScrapyardColossus_01);
				break;
			case "GVG_118":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_BonechewerBrawler_01);
				break;
			case "CFM_688":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_SpikedHogrider_01);
				break;
			case "DAL_376":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_06h_Male_Orc_Mission_Fight_06_Boss_HeavyMetal_01);
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (turn == 1)
		{
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_06_TurnOne_01);
		}
	}

	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		InitVisuals();
	}

	private void InitVisuals()
	{
		int cost = GetCost();
		InitTurnCounter(cost);
	}

	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			UpdateVisuals(change.newValue);
		}
	}

	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("BTA_Destroyer_Turn_Timer.prefab:5641f96ff71c06c4e8416952d878e3c7");
		m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = false;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = false;
		component.FsmVariables.GetFsmBool("Destroyer").Value = true;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		m_turnCounter.transform.parent = actor.gameObject.transform;
		m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		UpdateTurnCounterText(cost);
	}

	private void UpdateVisuals(int cost)
	{
		UpdateTurnCounter(cost);
	}

	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		m_mineCartArt.DoPortraitSwap(actor);
	}

	private void UpdateTurnCounter(int cost)
	{
		m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		UpdateTurnCounterText(cost);
	}

	private void UpdateTurnCounterText(int cost)
	{
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[1]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = cost
			}
		};
		string headlineString = GameStrings.FormatPlurals("MISSION_SPAREPARTSCOUNTERNAME", pluralNumbers);
		m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	public BTA_Fight_06()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.HANDLE_COIN,
				false
			},
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}
}
