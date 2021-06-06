using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Uther_08 : BoH_Uther_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01.prefab:1c863967bbf51124b91a032af52bc611");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeA_01.prefab:5228d5240d7332b499320186d0c22a08");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01.prefab:17a4c7b01980e0643981c80486d6b203");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01.prefab:d5d9b45756b579647980236ed457695c");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01.prefab:a1f4a52d68e2f1b42a051fac5f244663");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02.prefab:b6b6233ceeb482147a88ac462f7c0b4c");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_03.prefab:e920578aa0df881489de3cd1c7ecd1a1");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01.prefab:f9365764ca569b1458f61865e91d0b8f");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02.prefab:b2809a7f35d86604e9e8371f58b19749");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03.prefab:e0414ff298743f34296b99c08bd63703");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01.prefab:fa3924befd4c9044999c773b893437cf");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeA_01.prefab:5ae0bef312acadf4b9728831e17f5f63");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01.prefab:482d703e8500cd54a9a42cd579e87525");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01.prefab:d0e1fba54e9156446bdd8e9f72578615");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01.prefab:16e9a108765122f4e88b7f5dec76918a");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01.prefab:0e2fd2dbe0efe79429e611b614c92d06");

	private static readonly AssetReference VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01 = new AssetReference("VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01.prefab:2c5b3ea5536f97f499f1d5edc20a6a25");

	private List<string> m_HeroPowerLines = new List<string> { VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02, VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01 };

	private List<string> m_IdleLines = new List<string> { VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private Notification m_turnCounter;

	private MineCartRushArt m_mineCartArt;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeA_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_03, VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02,
			VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeA_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType != EmoteType.START && MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 504)
		{
			yield return PlayLineAlways(actor, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 5:
			yield return PlayLineAlways(actor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01);
			break;
		case 13:
			yield return PlayLineAlways(actor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01);
			break;
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
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22");
		m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = true;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = false;
		component.FsmVariables.GetFsmBool("Destroyer").Value = false;
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
		if (cost <= 0)
		{
			Object.Destroy(m_turnCounter.gameObject);
		}
		else
		{
			UpdateTurnCounterText(cost);
		}
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
		string headlineString = GameStrings.FormatPlurals("BOH_UTHER_08", pluralNumbers);
		m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}
}
