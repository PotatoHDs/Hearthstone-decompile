using System.Collections;
using UnityEngine;

public class ICC_03_SECRETS : ICC_MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification m_turnCounter;

	private TempleArt m_templeArt;

	private int m_mostRecentMissionEvent;

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

	public ICC_03_SECRETS()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_03_TURN_5_4.prefab:d35f6f70b2c0eca44920996f1d1b280b");
		PreloadSound("VO_LOE_03_TURN_6.prefab:43a3554df2b302c428fdc7325ab913ed");
		PreloadSound("VO_LOE_03_TURN_9.prefab:3f4b6112b2fea054693987a5cfbaf29b");
		PreloadSound("VO_LOE_03_TURN_5.prefab:cd01ccc585ea5e541bb6d6bf014ab57f");
		PreloadSound("VO_LOE_03_TURN_4_GOOD.prefab:04e58ac2fa0d1874caa45fe4bd009c16");
		PreloadSound("VO_LOE_03_TURN_4_BAD.prefab:2f27d2a958ffdd44387be7a1fe070234");
		PreloadSound("VO_LOE_03_TURN_6_2.prefab:e648bc075f6c30249a9221100bec6c06");
		PreloadSound("VO_LOE_03_TURN_4_NEITHER.prefab:ddb8aae686b170648898dc591fc7a554");
		PreloadSound("VO_LOE_03_TURN_3_WARNING.prefab:e9e9a86a32fba3842b95b38d71afe678");
		PreloadSound("VO_LOE_03_TURN_2.prefab:053b45bd9efaedb4f9178135644347b5");
		PreloadSound("VO_LOE_03_TURN_2_2.prefab:da4366528d8d3e84397dde52274585b0");
		PreloadSound("VO_LOE_03_TURN_4.prefab:d81cbf6a2f8657740a049208a3cc48e6");
		PreloadSound("VO_LOE_03_TURN_7.prefab:3ba8d9e054983934798a9ba027841605");
		PreloadSound("VO_LOE_03_TURN_7_2.prefab:79e0b4c1a04228749897dff2ca0d9edd");
		PreloadSound("VO_LOE_03_TURN_3_BOULDER.prefab:d633a3b18e9199b4d9f61b7fe8ce6527");
		PreloadSound("VO_LOE_03_TURN_1.prefab:90d2bc2b8b80a5444b9c15759d179dd7");
		PreloadSound("VO_LOE_03_TURN_8.prefab:7047eaf23edc34b42b0b171b7124d1b5");
		PreloadSound("VO_LOE_03_TURN_10.prefab:621285e424dd9e74c85573e03f34fb68");
		PreloadSound("VO_LOE_03_WIN.prefab:3b8d8d12b4f129c428c975ccc353a785");
		PreloadSound("VO_LOE_WING_1_WIN_2.prefab:db68235e589657e4ba8a94ff1458e299");
		PreloadSound("VO_LOE_03_RESPONSE.prefab:e38dfdb8a73972343b81c64ddb44171b");
	}

	public override void NotifyOfMulliganInitialized()
	{
		base.NotifyOfMulliganInitialized();
		m_mostRecentMissionEvent = GetTag(GAME_TAG.MISSION_EVENT);
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
		if (tag == GAME_TAG.COST)
		{
			UpdateVisuals(change.newValue);
		}
	}

	public override string CustomChoiceBannerText()
	{
		if (GetTag<TAG_STEP>(GAME_TAG.STEP) == TAG_STEP.MAIN_START)
		{
			string text = null;
			switch (m_mostRecentMissionEvent)
			{
			case 10:
				text = "MISSION_GLOWING_POOL";
				break;
			case 11:
				text = "MISSION_PIT_OF_SPIKES";
				break;
			case 4:
				text = "MISSION_STATUES_EYE";
				break;
			case 12:
				text = "MISSION_TAKE_THE_SHORTCUT";
				break;
			}
			if (text != null)
			{
				return GameStrings.Get(text);
			}
		}
		return null;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayMissionFlavorLine("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_Sidekick_Mission03_01");
			GameState.Get().SetBusy(busy: false);
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor()
			.ActivateSpellDeathState(SpellType.IMMUNE);
		if (turn == 1)
		{
			int cost = GetCost();
			InitTurnCounter(cost);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_1.prefab:90d2bc2b8b80a5444b9c15759d179dd7"));
		}
		if (turn % 2 == 0)
		{
			switch (GetCost())
			{
			case 3:
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_8.prefab:7047eaf23edc34b42b0b171b7124d1b5"));
				break;
			case 1:
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_10.prefab:621285e424dd9e74c85573e03f34fb68"));
				break;
			}
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_RESPONSE.prefab:e38dfdb8a73972343b81c64ddb44171b"));
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_03_WIN.prefab:3b8d8d12b4f129c428c975ccc353a785", 0f, allowRepeatDuringSession: false));
		}
	}

	private void InitVisuals()
	{
		int cost = GetCost();
		int tag = GetTag(GAME_TAG.TURN);
		InitTempleArt(cost);
		if (tag >= 1 && GameState.Get().IsPastBeginPhase())
		{
			InitTurnCounter(cost);
		}
	}

	private void InitTempleArt(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("TempleArt.prefab:c5d0fc0812982fc4ba576e2b0cdfa548");
		m_templeArt = gameObject.GetComponent<TempleArt>();
		UpdateTempleArt(cost);
	}

	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22");
		m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = true;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
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
		UpdateTempleArt(cost);
		if ((bool)m_turnCounter)
		{
			UpdateTurnCounter(cost);
		}
	}

	private void UpdateTempleArt(int cost)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		m_templeArt.DoPortraitSwap(actor, cost);
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
