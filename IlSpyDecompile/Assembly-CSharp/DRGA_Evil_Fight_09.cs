using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Evil_Fight_09 : DRGA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01.prefab:c098f600a91794847b963864b4123d70");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01.prefab:0b633b6af9f7598419df0f5e011f3713");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01.prefab:dbceb85b58708dd4e8ee164969943b8b");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01.prefab:36eeafd146bb8c14f95d9fec522ebe5e");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01.prefab:c87f4c39020589940bc1dbb5e5d49c25");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01.prefab:4695c80cb7c22f2438e5c51b1ae05d84");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01.prefab:8b47a4fdcdcd6f4478956144fd02b25e");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01.prefab:1069f930a320d7d4ea605301b1a1b72b");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01.prefab:299a1799efb19f84eb0fbb804c26407f");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01.prefab:69f229b2265870a4d821f2f1492bc3bc");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01.prefab:ad778543779b586498c2bad181c449f9");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01.prefab:7d0f6cce02a5d8445bcec173bd5739c1");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01.prefab:83cbae8952bb1e04ea1c10838a26d242");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01.prefab:c43f2da90e28bae4bad02294371490d1");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01.prefab:6aa67d702fb162846a5cef0d1ad4bf84");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01.prefab:e9cee199fb8702a44834e6d8b749cbb4");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01.prefab:dda018f33f140154d96f0604dd6e1981");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01.prefab:9b2cdaaf97642654b91c51935e6ab243");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01.prefab:cd6946e744c16cb4ba680cc48e0efa3f");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01.prefab:67200a9c2a55ab74ab6ffca10fb89910");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01.prefab:5044542e0b9c95f40926d9b8b198872a");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01.prefab:f51e17efc85119e42b269f844ecffca8");

	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01.prefab:b3820d6b62883b44fa92c064ee67f5f8");

	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01.prefab:0dd77a6081e33d746ac6c5f3d8999260");

	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossAttack_01.prefab:64cef6b12b8548a408176108ff6c3cd8");

	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01.prefab:c885c65a55b66fc4bba39c8c460a3a8f");

	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01.prefab:e6d9064529fbdb041a29e231378d17ee");

	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01.prefab:3baa599da06ca3642b57c14281d2d56c");

	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01.prefab:1f39731113c03a847976d11bf92a9cd4");

	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01.prefab:32afd2cd65439ee47961c0b6cf918b2f");

	private List<string> m_missionEventTrigger120Lines = new List<string> { VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01 };

	private List<string> m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPowerLines = new List<string> { VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestructLines = new List<string> { VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private Notification m_turnCounter;

	private MineCartRushArt m_mineCartArt;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.HANDLE_COIN,
			false
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public DRGA_Evil_Fight_09()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01,
			VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01,
			VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossAttack_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01, VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01
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

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 100:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01);
			}
			break;
		case 101:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01);
			}
			break;
		case 103:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01);
			}
			break;
		case 105:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01);
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01);
			}
			break;
		case 107:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01);
			}
			break;
		case 108:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01);
			}
			break;
		case 109:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01);
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01);
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01);
			}
			break;
		case 112:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01);
			}
			break;
		case 113:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01);
			}
			break;
		case 114:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01);
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01);
			}
			break;
		case 116:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01);
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01);
			}
			break;
		case 120:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_missionEventTrigger120Lines);
			break;
		case 121:
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestructLines);
			}
			break;
		case 251:
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPowerLines);
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
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("DRG_Airship_Turn_Timer.prefab:d29fadcd008628c408fe1d26485ea2b9");
		m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = false;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = true;
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
		string headlineString = GameStrings.FormatPlurals("MISSION_AIRSHIPCOUNTERNAME", pluralNumbers);
		m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}
}
