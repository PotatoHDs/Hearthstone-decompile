using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_04 : TutorialEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification endTurnNotifier;

	private Notification thatsABadPlayPopup;

	private Notification handBounceArrow;

	private Notification sheepTheBog;

	private Notification noSheepPopup;

	private int numBeastsPlayed;

	private GameObject m_heroPowerCostLabel;

	private Notification heroPowerHelp;

	private bool victory;

	private bool m_hemetSpeaking;

	private int numComplaintsMade;

	private bool m_shouldSignalPolymorph;

	private bool m_isPolymorphGrabbed;

	private bool m_isBogSheeped;

	private bool m_playOneHealthCommentNextTurn;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN,
			true
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public Tutorial_04()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_TUTORIAL04_HEMET_23_21.prefab:86859f5cb798f304395a63f446fe9d00");
		PreloadSound("VO_TUTORIAL04_HEMET_15_13.prefab:c0da1267215708947a954e9c0ea1b061");
		PreloadSound("VO_TUTORIAL04_HEMET_20_18.prefab:5d49a0bac4c03b94e9e13945624a581b");
		PreloadSound("VO_TUTORIAL04_HEMET_16_14.prefab:df368c7075e4a2649803729f7b86601e");
		PreloadSound("VO_TUTORIAL04_HEMET_13_12.prefab:fe14ab273aa4b7e4491f30310a7d0eca");
		PreloadSound("VO_TUTORIAL04_HEMET_19_17.prefab:b9d5bd30659aae84b8a1380cbdba0398");
		PreloadSound("VO_TUTORIAL_04_JAINA_09_43.prefab:1ee05d74948aba04ebd7065e44813921");
		PreloadSound("VO_TUTORIAL_04_JAINA_10_44.prefab:6f5921db1071ead4585c8cc9689d22ea");
		PreloadSound("VO_TUTORIAL04_HEMET_06_05.prefab:2527939914db3e543941a13266e88a01");
		PreloadSound("VO_TUTORIAL04_HEMET_07_06_ALT.prefab:c19475ec3c3b0e648a97f423e0e86143");
		PreloadSound("VO_TUTORIAL_04_JAINA_04_40.prefab:5bfc80c6184279140878a51eb1fa3469");
		PreloadSound("VO_TUTORIAL04_HEMET_08_07.prefab:68207d2681a60c84d840d37c4b90740f");
		PreloadSound("VO_TUTORIAL04_HEMET_09_08.prefab:2994b6b35f2e5f54782b6100ea92f40e");
		PreloadSound("VO_TUTORIAL04_HEMET_10_09.prefab:3282099b41c7ab94aa99e84c20dd7db7");
		PreloadSound("VO_TUTORIAL04_HEMET_11_10.prefab:db8c8cea0db51d14fbd5d4c782b8b160");
		PreloadSound("VO_TUTORIAL04_HEMET_12_11.prefab:b0ea652d6f1ec6845845226680ade252");
		PreloadSound("VO_TUTORIAL04_HEMET_12_11_ALT.prefab:b59f55e14876bee43a0d9ab4b7317f84");
		PreloadSound("VO_TUTORIAL_04_JAINA_08_42.prefab:36763b11766e2b64198719d44b0fa8bf");
		PreloadSound("VO_TUTORIAL04_HEMET_01_01.prefab:89be0839b16c1244a9221b373fd8fb61");
		PreloadSound("VO_TUTORIAL_04_JAINA_01_37.prefab:c7fc40d1595ca3c49b524b9929264477");
		PreloadSound("VO_TUTORIAL04_HEMET_02_02.prefab:c3ca1337cb01efe4194899d42918f80c");
		PreloadSound("VO_TUTORIAL04_HEMET_03_03.prefab:b014c684c85f1c440bed5560c6b6dbf5");
		PreloadSound("VO_TUTORIAL_04_JAINA_02_38.prefab:83b64d5eeb884db43b9fa5f20316da2c");
		PreloadSound("VO_TUTORIAL04_HEMET_04_04_ALT.prefab:bb3fadd78adce274993862115f3c5137");
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			victory = true;
		}
		base.NotifyOfGameOver(gameResult);
		if (m_heroPowerCostLabel != null)
		{
			Object.Destroy(m_heroPowerCostLabel);
		}
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			SetTutorialProgress(TutorialProgress.NESINGWARY_COMPLETE);
			PlaySound("VO_TUTORIAL04_HEMET_23_21.prefab:86859f5cb798f304395a63f446fe9d00");
			break;
		case TAG_PLAYSTATE.TIED:
			PlaySound("VO_TUTORIAL04_HEMET_23_21.prefab:86859f5cb798f304395a63f446fe9d00");
			break;
		case TAG_PLAYSTATE.LOST:
			SetTutorialLostProgress(TutorialProgress.NESINGWARY_COMPLETE);
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		m_shouldSignalPolymorph = false;
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (m_playOneHealthCommentNextTurn)
		{
			m_playOneHealthCommentNextTurn = false;
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_04_JAINA_08_42.prefab:36763b11766e2b64198719d44b0fa8bf", "TUTORIAL04_JAINA_08", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			GameState.Get().SetBusy(busy: false);
		}
		switch (turn)
		{
		case 1:
			if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_15_13.prefab:c0da1267215708947a954e9c0ea1b061", "TUTORIAL04_HEMET_15", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 4:
		{
			yield return new WaitForSeconds(1f);
			Vector3 position = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard()
				.transform.position;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				Vector3 position2 = new Vector3(position.x, position.y, position.z + 2.3f);
				heroPowerHelp = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_01"));
				heroPowerHelp.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			else
			{
				Vector3 position3 = new Vector3(position.x + 3f, position.y, position.z);
				heroPowerHelp = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position3, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_01"));
				heroPowerHelp.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
				AssetLoader.Get().InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", ManaLabelLoadedCallback, GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard(), AssetLoadingOptions.IgnorePrefabPosition);
			}
			break;
		}
		case 5:
			NotificationManager.Get().DestroyNotification(heroPowerHelp, 0f);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_20_18.prefab:5d49a0bac4c03b94e9e13945624a581b", "TUTORIAL04_HEMET_20", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 7:
			if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_16_14.prefab:df368c7075e4a2649803729f7b86601e", "TUTORIAL04_HEMET_16", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 9:
			if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_13_12.prefab:fe14ab273aa4b7e4491f30310a7d0eca", "TUTORIAL04_HEMET_13", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 11:
			GameState.Get().SetBusy(busy: true);
			Gameplay.Get().SetGameStateBusy(busy: false, 3f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_19_17.prefab:b9d5bd30659aae84b8a1380cbdba0398", "TUTORIAL04_HEMET_19", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return new WaitForSeconds(0.7f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_04_JAINA_09_43.prefab:1ee05d74948aba04ebd7065e44813921", "TUTORIAL04_JAINA_09", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			break;
		case 12:
		{
			if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				break;
			}
			m_shouldSignalPolymorph = true;
			List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
				.GetCards();
			if (!(InputManager.Get().GetHeldCard() == null))
			{
				break;
			}
			Card card = null;
			foreach (Card item in cards)
			{
				if (item.GetEntity().GetCardId() == "TU5_CS2_022")
				{
					card = item;
				}
			}
			if (!(card == null) && !card.IsMousedOver())
			{
				Gameplay.Get().StartCoroutine(ShowArrowInSeconds(0f));
			}
			break;
		}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			HandleGameStartEvent();
			break;
		case 2:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_06_05.prefab:2527939914db3e543941a13266e88a01", "TUTORIAL04_HEMET_06", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_07_06_ALT.prefab:c19475ec3c3b0e648a97f423e0e86143", "TUTORIAL04_HEMET_07", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield return Gameplay.Get().StartCoroutine(Wait(1f));
			GameState.Get().SetBusy(busy: false);
			break;
		case 3:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(Wait(2f));
			GameState.Get().SetBusy(busy: false);
			break;
		case 4:
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().GetFriendlyHand().SetHandEnlarged(enlarged: false);
			}
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_04_JAINA_04_40.prefab:5bfc80c6184279140878a51eb1fa3469", "TUTORIAL04_JAINA_04", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 5:
			if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				switch (numBeastsPlayed)
				{
				case 0:
					Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_08_07.prefab:68207d2681a60c84d840d37c4b90740f", "TUTORIAL04_HEMET_08", Notification.SpeechBubbleDirection.TopRight, enemyActor));
					break;
				case 1:
					Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_09_08.prefab:2994b6b35f2e5f54782b6100ea92f40e", "TUTORIAL04_HEMET_09", Notification.SpeechBubbleDirection.TopRight, enemyActor));
					break;
				case 2:
					Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_10_09.prefab:3282099b41c7ab94aa99e84c20dd7db7", "TUTORIAL04_HEMET_10", Notification.SpeechBubbleDirection.TopRight, enemyActor));
					break;
				case 3:
					Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_11_10.prefab:db8c8cea0db51d14fbd5d4c782b8b160", "TUTORIAL04_HEMET_11", Notification.SpeechBubbleDirection.TopRight, enemyActor));
					break;
				}
				numBeastsPlayed++;
			}
			break;
		case 6:
			if (numComplaintsMade == 0)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_12_11.prefab:b0ea652d6f1ec6845845226680ade252", "TUTORIAL04_HEMET_12a", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				numComplaintsMade++;
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_12_11_ALT.prefab:b59f55e14876bee43a0d9ab4b7317f84", "TUTORIAL04_HEMET_12b", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			break;
		case 7:
			m_playOneHealthCommentNextTurn = true;
			break;
		case 54:
		{
			yield return new WaitForSeconds(2f);
			string bodyTextGameString = ((!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE)) ? "TUTORIAL04_HELP_15" : "TUTORIAL04_HELP_16");
			ShowTutorialDialog("TUTORIAL04_HELP_14", bodyTextGameString, "TUTORIAL01_HELP_16", Vector2.zero, swapMaterial: true);
			break;
		}
		case 55:
			if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				FadeInHeroActor(enemyActor);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_01_01.prefab:89be0839b16c1244a9221b373fd8fb61", "TUTORIAL04_HEMET_01", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				FadeOutHeroActor(enemyActor);
				FadeInHeroActor(jainaActor);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_04_JAINA_01_37.prefab:c7fc40d1595ca3c49b524b9929264477", "TUTORIAL04_JAINA_01", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
				FadeOutHeroActor(jainaActor);
				yield return new WaitForSeconds(0.5f);
			}
			MulliganManager.Get().BeginMulligan();
			if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				m_hemetSpeaking = true;
				FadeInHeroActor(enemyActor);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_02_02.prefab:c3ca1337cb01efe4194899d42918f80c", "TUTORIAL04_HEMET_02", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				FadeOutHeroActor(enemyActor);
				m_hemetSpeaking = false;
			}
			break;
		}
	}

	public override void NotifyOfCoinFlipResult()
	{
		Gameplay.Get().StartCoroutine(HandleCoinFlip());
	}

	private IEnumerator HandleCoinFlip()
	{
		GameState.Get().SetBusy(busy: true);
		yield return Gameplay.Get().StartCoroutine(Wait(1f));
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		while (m_hemetSpeaking)
		{
			yield return null;
		}
		FadeInHeroActor(enemyActor);
		yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_03_03.prefab:b014c684c85f1c440bed5560c6b6dbf5", "TUTORIAL04_HEMET_03", Notification.SpeechBubbleDirection.TopRight, enemyActor));
		FadeOutHeroActor(enemyActor);
		yield return new WaitForSeconds(0.3f);
		FadeInHeroActor(jainaActor);
		yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_04_JAINA_02_38.prefab:83b64d5eeb884db43b9fa5f20316da2c", "TUTORIAL04_JAINA_02", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
		FadeOutHeroActor(jainaActor);
		yield return new WaitForSeconds(0.25f);
		if (!DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
		{
			FadeInHeroActor(enemyActor);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL04_HEMET_04_04_ALT.prefab:bb3fadd78adce274993862115f3c5137", "TUTORIAL04_HEMET_04", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			FadeOutHeroActor(enemyActor);
			yield return new WaitForSeconds(0.4f);
		}
		GameState.Get().SetBusy(busy: false);
	}

	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

	private void ShowEndTurnBouncingArrow()
	{
		if (!EndTurnButton.Get().IsInWaitingState())
		{
			Vector3 position = EndTurnButton.Get().transform.position;
			Vector3 position2 = new Vector3(position.x - 2f, position.y, position.z);
			NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, -90f, 0f));
		}
	}

	private bool AllowEndTurn()
	{
		if (!m_shouldSignalPolymorph || (m_shouldSignalPolymorph && m_isBogSheeped))
		{
			return true;
		}
		return false;
	}

	public override bool NotifyOfEndTurnButtonPushed()
	{
		if (GetTag(GAME_TAG.TURN) != 4 && AllowEndTurn())
		{
			NotificationManager.Get().DestroyAllArrows();
			return true;
		}
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket != null && !optionsPacket.HasValidOption())
		{
			NotificationManager.Get().DestroyAllArrows();
			return true;
		}
		if (endTurnNotifier != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(endTurnNotifier);
		}
		Vector3 position = EndTurnButton.Get().transform.position;
		Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
		string key = "TUTORIAL_NO_ENDTURN_HP";
		if (GameState.Get().GetFriendlySidePlayer().HasReadyAttackers())
		{
			key = "TUTORIAL_NO_ENDTURN_ATK";
		}
		if (m_shouldSignalPolymorph && !m_isBogSheeped)
		{
			key = "TUTORIAL_NO_ENDTURN";
		}
		endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		NotificationManager.Get().DestroyNotification(endTurnNotifier, 2.5f);
		return false;
	}

	public override void NotifyOfTargetModeCancelled()
	{
		if (!(sheepTheBog == null))
		{
			NotificationManager.Get().DestroyAllPopUps();
		}
	}

	public override void NotifyOfCardGrabbed(Entity entity)
	{
		if (!m_shouldSignalPolymorph)
		{
			return;
		}
		if (entity.GetCardId() == "TU5_CS2_022")
		{
			m_isPolymorphGrabbed = true;
			if (sheepTheBog != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(sheepTheBog);
			}
			if (handBounceArrow != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(handBounceArrow);
			}
			NotificationManager.Get().DestroyAllPopUps();
			Vector3 position = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
				.GetFirstCard()
				.transform.position;
			Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
			sheepTheBog = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_02"));
			sheepTheBog.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		}
		else
		{
			if (sheepTheBog != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(sheepTheBog);
			}
			NotificationManager.Get().DestroyAllPopUps();
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().ReturnHeldCardToHand();
			}
			else
			{
				InputManager.Get().DropHeldCard();
			}
		}
	}

	public override void NotifyOfCardDropped(Entity entity)
	{
		m_isPolymorphGrabbed = false;
		if (m_shouldSignalPolymorph)
		{
			if (sheepTheBog != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(sheepTheBog);
			}
			NotificationManager.Get().DestroyAllPopUps();
			if (ShouldShowArrowOnCardInHand(entity))
			{
				Gameplay.Get().StartCoroutine(ShowArrowInSeconds(0.5f));
			}
		}
	}

	public override bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		if (m_shouldSignalPolymorph)
		{
			if (!(clickedEntity.GetCardId() == "TU5_CS1_069" && wasInTargetMode))
			{
				if (m_isPolymorphGrabbed && wasInTargetMode)
				{
					if (noSheepPopup != null)
					{
						NotificationManager.Get().DestroyNotificationNowWithNoAnim(noSheepPopup);
					}
					Vector3 position = clickedEntity.GetCard().transform.position;
					Vector3 position2 = new Vector3(position.x + 2.5f, position.y, position.z);
					noSheepPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_03"));
					NotificationManager.Get().DestroyNotification(noSheepPopup, 3f);
					return false;
				}
				return false;
			}
			if (sheepTheBog != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(sheepTheBog);
			}
			NotificationManager.Get().DestroyAllPopUps();
			m_shouldSignalPolymorph = false;
			m_isBogSheeped = true;
		}
		return true;
	}

	public override bool ShouldAllowCardGrab(Entity entity)
	{
		if (m_shouldSignalPolymorph && entity.GetCardId() != "TU5_CS2_022")
		{
			return false;
		}
		return true;
	}

	private void ManaLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		GameObject costTextObject = ((Card)callbackData).GetActor().GetCostTextObject();
		if (costTextObject == null)
		{
			Object.Destroy(go);
			return;
		}
		m_heroPowerCostLabel = go;
		SceneUtils.SetLayer(go, GameLayer.Default);
		go.transform.parent = costTextObject.transform;
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localPosition = new Vector3(-0.02f, 0.38f, 0f);
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.x, go.transform.localScale.x);
		go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_COST");
	}

	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (ShouldShowArrowOnCardInHand(mousedOverEntity))
		{
			NotificationManager.Get().DestroyAllArrows();
		}
	}

	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (ShouldShowArrowOnCardInHand(mousedOffEntity))
		{
			Gameplay.Get().StartCoroutine(ShowArrowInSeconds(0.5f));
		}
	}

	private bool ShouldShowArrowOnCardInHand(Entity entity)
	{
		if (entity.GetZone() != TAG_ZONE.HAND)
		{
			return false;
		}
		if (m_shouldSignalPolymorph && entity.GetCardId() == "TU5_CS2_022")
		{
			return true;
		}
		return false;
	}

	private IEnumerator ShowArrowInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards();
		if (cards.Count == 0 || m_isPolymorphGrabbed)
		{
			yield break;
		}
		Card polyMorph = null;
		foreach (Card item in cards)
		{
			if (item.GetEntity().GetCardId() == "TU5_CS2_022")
			{
				polyMorph = item;
			}
		}
		if (!(polyMorph == null))
		{
			while (iTween.Count(polyMorph.gameObject) > 0)
			{
				yield return null;
			}
			if (!polyMorph.IsMousedOver() && !(InputManager.Get().GetHeldCard() == polyMorph))
			{
				ShowHandBouncingArrow();
			}
		}
	}

	private void ShowHandBouncingArrow()
	{
		if (handBounceArrow != null)
		{
			return;
		}
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards();
		if (cards.Count == 0)
		{
			return;
		}
		Card card = null;
		foreach (Card item in cards)
		{
			if (item.GetEntity().GetCardId() == "TU5_CS2_022")
			{
				card = item;
			}
		}
		if (!(card == null) && !m_isPolymorphGrabbed)
		{
			Vector3 position = card.transform.position;
			Vector3 position2 = new Vector3(position.x, position.y, position.z + 2f);
			handBounceArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, 0f, 0f));
			handBounceArrow.transform.parent = card.transform;
		}
	}

	public override void NotifyOfDefeatCoinAnimation()
	{
		if (victory)
		{
			PlaySound("VO_TUTORIAL_04_JAINA_10_44.prefab:6f5921db1071ead4585c8cc9689d22ea");
		}
	}

	public override List<RewardData> GetCustomRewards()
	{
		if (!victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_213", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}
}
