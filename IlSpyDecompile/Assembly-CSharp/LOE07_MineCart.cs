using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE07_MineCart : LOE_MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification m_turnCounter;

	private HashSet<string> m_playedLines = new HashSet<string>();

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

	public LOE07_MineCart()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Minecart);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_07_START.prefab:9707703d728d10a47a13b7fb9b978c15");
		PreloadSound("VO_LOE_07_WIN.prefab:2eef218b9ec26a44cac30cbc83ea01b6");
		PreloadSound("VO_LOEA07_MINE_ARCHAEDAS.prefab:882b69209ca43844a9faa3767dc8fc84");
		PreloadSound("VO_LOEA07_MINE_DETONATE.prefab:761ba0f22ade3c64c8826d5823ba060f");
		PreloadSound("VO_LOEA07_MINE_RAMMING.prefab:a97ddf505676fc64db1bec8b236e3619");
		PreloadSound("VO_LOEA07_MINE_PARROT.prefab:101be2462fc0a964d8dc7b328be15e20");
		PreloadSound("VO_LOEA07_MINE_BOOMZOOKA.prefab:54d6205b2fe93334c8b32f302a7095e3");
		PreloadSound("VO_LOEA07_MINE_DYNAMITE.prefab:dc00acb77f796994ba54c000ab83a313");
		PreloadSound("VO_LOEA07_MINE_BOOM.prefab:a60ee4fd18095fc4795b96e72a3d30d9");
		PreloadSound("VO_LOEA07_MINE_BARREL_FORWARD.prefab:16e53571a0835b3459dac9228e127fc9");
		PreloadSound("VO_LOEA07_MINE_HUNKER_DOWN.prefab:f7ee4ae0725e8e44bafaf7918e5c8164");
		PreloadSound("VO_LOEA07_MINE_SPIKED_DECOY.prefab:0ee62cdb45e19e1499e78b4c23aa1ef2");
		PreloadSound("VO_LOEA07_MINE_REPAIRS.prefab:f933572c4ce958646ad8530cb42caabb");
		PreloadSound("VO_BRANN_WIN_07_ALT_07.prefab:69e827ff4bdd1074abec8904aadf9729");
		PreloadSound("VO_LOE_07_RESPONSE.prefab:2d967602b4fcf05478426a17998fe534");
		PreloadSound("Mine_response.prefab:5a1713d5acac1f34bacb52ef005eb417");
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	public override void NotifyOfMulliganInitialized()
	{
		base.NotifyOfMulliganInitialized();
		InitVisuals();
	}

	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor()
			.GetHealthObject()
			.Hide();
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

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		switch (turn)
		{
		case 1:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_07_START.prefab:9707703d728d10a47a13b7fb9b978c15"));
			break;
		case 11:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_07_WIN.prefab:2eef218b9ec26a44cac30cbc83ea01b6"));
			break;
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			SoundManager.Get().LoadAndPlay("Mine_response.prefab:5a1713d5acac1f34bacb52ef005eb417", actor.gameObject);
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			string cardId = entity.GetCardId();
			if (cardId == "LOEA07_16")
			{
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_ARCHAEDAS.prefab:882b69209ca43844a9faa3767dc8fc84"));
			}
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			string cardId = entity.GetCardId();
			switch (cardId)
			{
			case "LOEA07_23":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_DETONATE.prefab:761ba0f22ade3c64c8826d5823ba060f"));
				break;
			case "LOEA07_19":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_RAMMING.prefab:a97ddf505676fc64db1bec8b236e3619"));
				break;
			case "LOEA07_25":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_PARROT.prefab:101be2462fc0a964d8dc7b328be15e20"));
				break;
			case "LOEA07_32":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_BOOMZOOKA.prefab:54d6205b2fe93334c8b32f302a7095e3"));
				break;
			case "LOEA07_18":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_DYNAMITE.prefab:dc00acb77f796994ba54c000ab83a313"));
				break;
			case "LOEA07_20":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_BOOM.prefab:a60ee4fd18095fc4795b96e72a3d30d9"));
				break;
			case "LOEA07_21":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_BARREL_FORWARD.prefab:16e53571a0835b3459dac9228e127fc9"));
				break;
			case "LOEA07_22":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_HUNKER_DOWN.prefab:f7ee4ae0725e8e44bafaf7918e5c8164"));
				break;
			case "LOEA07_24":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_SPIKED_DECOY.prefab:0ee62cdb45e19e1499e78b4c23aa1ef2"));
				break;
			case "LOEA07_28":
				m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_REPAIRS.prefab:f933572c4ce958646ad8530cb42caabb"));
				break;
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Brann_Quote.prefab:2c11651ab7740924189734944b8d7089", "VO_BRANN_WIN_07_ALT_07.prefab:69e827ff4bdd1074abec8904aadf9729", 0f, allowRepeatDuringSession: false));
		}
	}

	private void InitVisuals()
	{
		int cost = GetCost();
		InitMineCartArt();
		InitTurnCounter(cost);
	}

	private void InitMineCartArt()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("MineCartRushArt.prefab:77a3c4475a44dac4a91cef7cc9c621e5");
		m_mineCartArt = gameObject.GetComponent<MineCartRushArt>();
	}

	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22");
		m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = false;
		component.FsmVariables.GetFsmBool("MineCart").Value = true;
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
		UpdateMineCartArt();
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
		string headlineString = GameStrings.FormatPlurals("MISSION_DEFAULTCOUNTERNAME", pluralNumbers);
		m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}
}
