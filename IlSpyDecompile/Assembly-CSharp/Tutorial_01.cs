using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_01 : TutorialEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification endTurnNotifier;

	private Notification handBounceArrow;

	private Notification handFadeArrow;

	private Notification noFireballPopup;

	private Notification attackWithYourMinion;

	private Notification crushThisGnoll;

	private Notification freeCardsPopup;

	private TooltipPanel attackHelpPanel;

	private TooltipPanel healthHelpPanel;

	private Card mousedOverCard;

	private GameObject costLabel;

	private GameObject attackLabel;

	private GameObject healthLabel;

	private Card firstMurlocCard;

	private Card firstRaptorCard;

	private int numTimesTextSwapStarted;

	private string textToShowForAttackTip = GameStrings.Get("TUTORIAL01_HELP_02");

	private GameObject startingPack;

	private bool packOpened;

	private bool announcerIsFinishedYapping;

	private bool firstAttackFinished;

	private bool m_jainaSpeaking;

	private bool m_isShowingAttackHelpPanel;

	private PlatformDependentValue<float> m_gemScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 1.75f,
		Phone = 1.2f
	};

	private PlatformDependentValue<Vector3> m_attackTooltipPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-2.15f, 0f, -0.62f),
		Phone = new Vector3(-3.5f, 0f, -0.62f)
	};

	private PlatformDependentValue<Vector3> m_healthTooltipPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(2.05f, 0f, -0.62f),
		Phone = new Vector3(3.25f, 0f, -0.62f)
	};

	private PlatformDependentValue<Vector3> m_heroHealthTooltipPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(2.4f, 0.3f, -0.8f),
		Phone = new Vector3(3.5f, 0.3f, 0.6f)
	};

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN,
				true
			},
			{
				GameEntityOption.SHOW_HERO_TOOLTIPS,
				true
			},
			{
				GameEntityOption.DISABLE_TOOLTIPS,
				true
			}
		};
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public Tutorial_01()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
		MulliganManager.Get().ForceMulliganActive(active: true);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_TUTORIAL_01_ANNOUNCER_01.prefab:79419083a1b828341be6d208491a88f8");
		PreloadSound("VO_TUTORIAL_01_ANNOUNCER_02.prefab:d6b08fa7e06a51c4abd80eea2ea30a41");
		PreloadSound("VO_TUTORIAL_01_ANNOUNCER_03.prefab:f47d0faf9067b3341bb9adb38f90be5b");
		PreloadSound("VO_TUTORIAL_01_ANNOUNCER_04.prefab:e6fb72da1414d454f9d96a51c7009a82");
		PreloadSound("VO_TUTORIAL_01_ANNOUNCER_05.prefab:635b33010e4704a42a87c7625b5b5ada");
		PreloadSound("VO_TUTORIAL_01_JAINA_13_10.prefab:b13670e36c248e141837c4eb0645a000");
		PreloadSound("VO_TUTORIAL_01_JAINA_01_01.prefab:883391234efbde84eb99a16abd164d9d");
		PreloadSound("VO_TUTORIAL_01_JAINA_02_02.prefab:cccdcb509085a974d922ac1d545d9bb6");
		PreloadSound("VO_TUTORIAL_01_JAINA_03_03.prefab:4921407046d90bb44b2bfcf3984ffd47");
		PreloadSound("VO_TUTORIAL_01_JAINA_20_16.prefab:7980d02c581e4174991a8066e5785666");
		PreloadSound("VO_TUTORIAL_01_JAINA_05_05.prefab:982193e53ab81f04ba562de4b32dd39c");
		PreloadSound("VO_TUTORIAL_01_JAINA_06_06.prefab:ffe0ebdca06ca1d4c84cc28e4a1ed7cf");
		PreloadSound("VO_TUTORIAL_01_JAINA_07_07.prefab:a8bf811494e94d742a3910fac9da906f");
		PreloadSound("VO_TUTORIAL_01_JAINA_21_17.prefab:c1524bd0ef92bb845b5dab48cbd017f9");
		PreloadSound("VO_TUTORIAL_01_JAINA_09_08.prefab:b7b739d9e31865a478275394ee57ad89");
		PreloadSound("VO_TUTORIAL_01_JAINA_15_11.prefab:a644986d34ab8964582c6221cde54d45");
		PreloadSound("VO_TUTORIAL_01_JAINA_16_12.prefab:e6b4ab6fc1f11634e88f013ce5351e46");
		PreloadSound("VO_TUTORIAL_JAINA_02_55_ALT2.prefab:d049e67ad6c16db4da2c04be7a02a1ae");
		PreloadSound("VO_TUTORIAL_01_JAINA_10_09.prefab:5bf553d532aca174083f48bf407b2b11");
		PreloadSound("VO_TUTORIAL_01_JAINA_17_13.prefab:9b257c86e7c7f9045a2b819d35876aca");
		PreloadSound("VO_TUTORIAL_01_JAINA_18_14.prefab:fedcdecb3346ec745b6fb4204f7dd4e0");
		PreloadSound("VO_TUTORIAL_01_JAINA_19_15.prefab:659652a121ac01941a40c64c1c151f87");
		PreloadSound("VO_TUTORIAL_01_HOGGER_01_01.prefab:5833f4aeb72110741a2c9bc3a92f9bc8");
		PreloadSound("VO_TUTORIAL_01_HOGGER_02_02.prefab:7f321b26431a4974a82deefc368adf63");
		PreloadSound("VO_TUTORIAL_01_HOGGER_03_03.prefab:4ef21f71824b97842b33d8ebccb37ed2");
		PreloadSound("VO_TUTORIAL_01_HOGGER_04_04.prefab:3e16e42edb324e2469a25363ffd013a6");
		PreloadSound("VO_TUTORIAL_01_HOGGER_06_06_ALT.prefab:6c9ef3c501462474ab59a37b967cab6f");
		PreloadSound("VO_TUTORIAL_01_HOGGER_08_08_ALT.prefab:19ddb4ddaa4aee2468b17bae25da9419");
		PreloadSound("VO_TUTORIAL_01_HOGGER_09_09_ALT.prefab:70c4d2941509856448660f89d6c72b2b");
		PreloadSound("VO_TUTORIAL_01_HOGGER_11_11.prefab:1fdb0543bf56c4b4e95148a518bd9a2d");
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		if (attackHelpPanel != null)
		{
			Object.Destroy(attackHelpPanel.gameObject);
			attackHelpPanel = null;
		}
		if (healthHelpPanel != null)
		{
			Object.Destroy(healthHelpPanel.gameObject);
			healthHelpPanel = null;
		}
		EnsureCardGemsAreOnTheCorrectLayer();
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			SetTutorialProgress(TutorialProgress.HOGGER_COMPLETE);
			PlaySound("VO_TUTORIAL_01_HOGGER_11_11.prefab:1fdb0543bf56c4b4e95148a518bd9a2d");
			break;
		case TAG_PLAYSTATE.TIED:
			PlaySound("VO_TUTORIAL_01_HOGGER_11_11.prefab:1fdb0543bf56c4b4e95148a518bd9a2d");
			break;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			InputManager.Get().RemovePhoneHandShownListener(OnPhoneHandShown);
			InputManager.Get().RemovePhoneHandHiddenListener(OnPhoneHandHidden);
		}
	}

	private void EnsureCardGemsAreOnTheCorrectLayer()
	{
		List<Card> list = new List<Card>();
		list.AddRange(GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
			.GetCards());
		list.AddRange(GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards());
		list.Add(GameState.Get().GetFriendlySidePlayer().GetHeroCard());
		list.Add(GameState.Get().GetOpposingSidePlayer().GetHeroCard());
		foreach (Card item in list)
		{
			if (!(item == null) && !(item.GetActor() == null))
			{
				if (item.GetActor().GetAttackObject() != null)
				{
					SceneUtils.SetLayer(item.GetActor().GetAttackObject().gameObject, GameLayer.Default);
				}
				if (item.GetActor().GetHealthObject() != null)
				{
					SceneUtils.SetLayer(item.GetActor().GetHealthObject().gameObject, GameLayer.Default);
				}
			}
		}
	}

	public override void NotifyOfCardGrabbed(Entity entity)
	{
		if (GetTag(GAME_TAG.TURN) == 2 || entity.GetCardId() == "TU5_CS2_025")
		{
			BoardTutorial.Get().EnableHighlight(enable: true);
		}
		NukeNumberLabels();
	}

	public override void NotifyOfCardDropped(Entity entity)
	{
		if (GetTag(GAME_TAG.TURN) == 2 || entity.GetCardId() == "TU5_CS2_025")
		{
			BoardTutorial.Get().EnableHighlight(enable: false);
		}
	}

	public override bool NotifyOfEndTurnButtonPushed()
	{
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
		string key = "TUTORIAL_NO_ENDTURN_ATK";
		if (!GameState.Get().GetFriendlySidePlayer().HasReadyAttackers())
		{
			key = "TUTORIAL_NO_ENDTURN";
		}
		endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		NotificationManager.Get().DestroyNotification(endTurnNotifier, 2.5f);
		return false;
	}

	public override bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		if (error == PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0 && errorSource.GetCardId() == "TU4a_006")
		{
			return true;
		}
		return false;
	}

	public override void NotifyOfTargetModeCancelled()
	{
		if (!(crushThisGnoll == null))
		{
			NotificationManager.Get().DestroyAllPopUps();
			if (!(firstRaptorCard == null) && firstRaptorCard.GetZone() is ZonePlay)
			{
				ShowAttackWithYourMinionPopup();
			}
		}
	}

	public override bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		if (GetTag(GAME_TAG.TURN) == 4)
		{
			if (clickedEntity.GetCardId() == "TU5_CS2_168")
			{
				if (!wasInTargetMode && !firstAttackFinished)
				{
					if (crushThisGnoll != null)
					{
						NotificationManager.Get().DestroyNotificationNowWithNoAnim(crushThisGnoll);
					}
					NotificationManager.Get().DestroyAllPopUps();
					Vector3 position = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
						.GetFirstCard()
						.transform.position;
					Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
					crushThisGnoll = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_03"));
					crushThisGnoll.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
					numTimesTextSwapStarted++;
					Gameplay.Get().StartCoroutine(WaitAndThenHide(numTimesTextSwapStarted));
				}
			}
			else if (clickedEntity.GetCardId() == "TU4a_002" && wasInTargetMode)
			{
				if (crushThisGnoll != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(crushThisGnoll);
				}
				NotificationManager.Get().DestroyAllPopUps();
				firstAttackFinished = true;
			}
		}
		else if (GetTag(GAME_TAG.TURN) == 6 && clickedEntity.GetCardId() == "TU4a_001" && wasInTargetMode)
		{
			NotificationManager.Get().DestroyAllPopUps();
		}
		if (wasInTargetMode && InputManager.Get().GetHeldCard() != null && InputManager.Get().GetHeldCard().GetEntity()
			.GetCardId() == "TU5_CS2_029")
		{
			if (clickedEntity.IsControlledByLocalUser())
			{
				ShowDontFireballYourselfPopup(clickedEntity.GetCard().transform.position);
				return false;
			}
			if (clickedEntity.GetCardId() == "TU4a_003" && GetTag(GAME_TAG.TURN) >= 8)
			{
				if (noFireballPopup != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(noFireballPopup);
				}
				Vector3 position3 = clickedEntity.GetCard().transform.position;
				Vector3 position4 = new Vector3(position3.x - 3f, position3.y, position3.z);
				noFireballPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position4, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_08"));
				NotificationManager.Get().DestroyNotification(noFireballPopup, 3f);
				return false;
			}
		}
		return true;
	}

	private IEnumerator WaitAndThenHide(int numTimesStarted)
	{
		yield return new WaitForSeconds(6f);
		if (!(crushThisGnoll == null) && numTimesStarted == numTimesTextSwapStarted && !(GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetFirstCard() == null))
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(crushThisGnoll);
		}
	}

	public override bool NotifyOfCardTooltipDisplayShow(Card card)
	{
		if (GameState.Get().IsGameOver())
		{
			return false;
		}
		Entity entity = card.GetEntity();
		if (entity.IsMinion())
		{
			if (attackHelpPanel == null)
			{
				m_isShowingAttackHelpPanel = true;
				ShowAttackTooltip(card);
				Gameplay.Get().StartCoroutine(ShowHealthTooltipAfterWait(card));
			}
			return false;
		}
		if (entity.IsHero())
		{
			if (healthHelpPanel == null)
			{
				ShowHealthTooltip(card);
			}
			return false;
		}
		return true;
	}

	private void ShowAttackTooltip(Card card)
	{
		SceneUtils.SetLayer(card.GetActor().GetAttackObject().gameObject, GameLayer.Tooltip);
		Vector3 position = card.transform.position;
		Vector3 vector = m_attackTooltipPosition;
		Vector3 position2 = new Vector3(position.x + vector.x, position.y + vector.y, position.z + vector.z);
		attackHelpPanel = TooltipPanelManager.Get().CreateKeywordPanel(0);
		attackHelpPanel.Reset();
		attackHelpPanel.SetScale(TooltipPanel.GAMEPLAY_SCALE);
		attackHelpPanel.Initialize(GameStrings.Get("GLOBAL_ATTACK"), GameStrings.Get("TUTORIAL01_HELP_12"));
		attackHelpPanel.transform.position = position2;
		RenderUtils.SetAlpha(attackHelpPanel.gameObject, 0f);
		iTween.FadeTo(attackHelpPanel.gameObject, iTween.Hash("alpha", 1, "time", 0.25f));
		card.GetActor().GetAttackObject().Enlarge(m_gemScale);
	}

	private IEnumerator ShowHealthTooltipAfterWait(Card card)
	{
		yield return new WaitForSeconds(0.05f);
		if (!(InputManager.Get().GetMousedOverCard() != card))
		{
			ShowHealthTooltip(card);
		}
	}

	private void ShowHealthTooltip(Card card)
	{
		SceneUtils.SetLayer(card.GetActor().GetHealthObject().gameObject, GameLayer.Tooltip);
		Vector3 position = card.transform.position;
		Vector3 vector = m_healthTooltipPosition;
		if (card.GetEntity().IsHero())
		{
			vector = m_heroHealthTooltipPosition;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				if (!card.GetEntity().IsControlledByLocalUser())
				{
					vector.z -= 0.75f;
				}
				else if (Localization.GetLocale() == Locale.ruRU)
				{
					vector.z += 1f;
				}
			}
		}
		Vector3 position2 = new Vector3(position.x + vector.x, position.y + vector.y, position.z + vector.z);
		healthHelpPanel = TooltipPanelManager.Get().CreateKeywordPanel(0);
		healthHelpPanel.Reset();
		healthHelpPanel.SetScale(TooltipPanel.GAMEPLAY_SCALE);
		healthHelpPanel.Initialize(GameStrings.Get("GLOBAL_HEALTH"), GameStrings.Get("TUTORIAL01_HELP_13"));
		healthHelpPanel.transform.position = position2;
		RenderUtils.SetAlpha(healthHelpPanel.gameObject, 0f);
		iTween.FadeTo(healthHelpPanel.gameObject, iTween.Hash("alpha", 1, "time", 0.25f));
		card.GetActor().GetHealthObject().Enlarge(m_gemScale);
	}

	public override void NotifyOfCardTooltipDisplayHide(Card card)
	{
		if (attackHelpPanel != null)
		{
			if (card != null)
			{
				GemObject attackObject = card.GetActor().GetAttackObject();
				SceneUtils.SetLayer(attackObject.gameObject, GameLayer.Default);
				attackObject.Shrink();
			}
			Object.Destroy(attackHelpPanel.gameObject);
			m_isShowingAttackHelpPanel = false;
		}
		if (healthHelpPanel != null)
		{
			if (card != null)
			{
				GemObject healthObject = card.GetActor().GetHealthObject();
				SceneUtils.SetLayer(healthObject.gameObject, GameLayer.Default);
				healthObject.Shrink();
			}
			Object.Destroy(healthHelpPanel.gameObject);
		}
	}

	private void ManaLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (!m_isShowingAttackHelpPanel)
		{
			GameObject costTextObject = ((Card)callbackData).GetActor().GetCostTextObject();
			if (costTextObject == null)
			{
				Object.Destroy(go);
				return;
			}
			costLabel = go;
			go.transform.parent = costTextObject.transform;
			go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			go.transform.localPosition = new Vector3(-0.017f, 0.3512533f, 0f);
			go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_COST");
		}
	}

	private void AttackLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (!m_isShowingAttackHelpPanel)
		{
			GameObject attackTextObject = ((Card)callbackData).GetActor().GetAttackTextObject();
			if (attackTextObject == null)
			{
				Object.Destroy(go);
				return;
			}
			attackLabel = go;
			go.transform.parent = attackTextObject.transform;
			go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			go.transform.localPosition = new Vector3(-0.2f, -0.3039344f, 0f);
			go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_ATTACK");
		}
	}

	private void HealthLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (!m_isShowingAttackHelpPanel)
		{
			GameObject healthTextObject = ((Card)callbackData).GetActor().GetHealthTextObject();
			if (healthTextObject == null)
			{
				Object.Destroy(go);
				return;
			}
			healthLabel = go;
			go.transform.parent = healthTextObject.transform;
			go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			go.transform.localPosition = new Vector3(0.21f, -0.31f, 0f);
			go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_HEALTH");
		}
	}

	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (ShouldShowArrowOnCardInHand(mousedOverEntity))
		{
			NotificationManager.Get().DestroyAllArrows();
		}
		if (mousedOverEntity.GetZone() == TAG_ZONE.HAND)
		{
			mousedOverCard = mousedOverEntity.GetCard();
			IAssetLoader assetLoader = AssetLoader.Get();
			assetLoader.InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", ManaLabelLoadedCallback, mousedOverCard, AssetLoadingOptions.IgnorePrefabPosition);
			assetLoader.InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", AttackLabelLoadedCallback, mousedOverCard, AssetLoadingOptions.IgnorePrefabPosition);
			assetLoader.InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", HealthLabelLoadedCallback, mousedOverCard, AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (ShouldShowArrowOnCardInHand(mousedOffEntity))
		{
			Gameplay.Get().StartCoroutine(ShowArrowInSeconds(0.5f));
		}
		NukeNumberLabels();
	}

	private void NukeNumberLabels()
	{
		mousedOverCard = null;
		if (costLabel != null)
		{
			Object.Destroy(costLabel);
		}
		if (attackLabel != null)
		{
			Object.Destroy(attackLabel);
		}
		if (healthLabel != null)
		{
			Object.Destroy(healthLabel);
		}
	}

	private bool ShouldShowArrowOnCardInHand(Entity entity)
	{
		if (entity.GetZone() != TAG_ZONE.HAND)
		{
			return false;
		}
		switch (GetTag(GAME_TAG.TURN))
		{
		case 2:
			return true;
		case 4:
			if (GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
				.GetCards()
				.Count == 0)
			{
				return true;
			}
			break;
		}
		return false;
	}

	private IEnumerator ShowArrowInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards();
		if (cards.Count != 0)
		{
			Card cardInHand = cards[0];
			while (iTween.Count(cardInHand.gameObject) > 0)
			{
				yield return null;
			}
			if (!cardInHand.IsMousedOver() && !(InputManager.Get().GetHeldCard() == cardInHand))
			{
				ShowHandBouncingArrow();
			}
		}
	}

	private void ShowHandBouncingArrow()
	{
		if (!(handBounceArrow != null))
		{
			List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
				.GetCards();
			if (cards.Count != 0)
			{
				Card card = cards[0];
				Vector3 position = card.transform.position;
				Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z + 2f) : new Vector3(position.x - 0.08f, position.y + 0.2f, position.z + 1.2f));
				handBounceArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, 0f, 0f));
				handBounceArrow.transform.parent = card.transform;
			}
		}
	}

	private void ShowHandFadeArrow()
	{
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards();
		if (cards.Count != 0)
		{
			ShowFadeArrow(cards[0]);
		}
	}

	private void ShowFadeArrow(Card card, Card target = null)
	{
		if (!(handFadeArrow != null))
		{
			Vector3 position = card.transform.position;
			Vector3 rotation = new Vector3(0f, 180f, 0f);
			Vector3 vector2;
			if (target != null)
			{
				Vector3 vector = target.transform.position - position;
				vector2 = new Vector3(position.x, position.y + 0.47f, position.z + 0.27f);
				float num = Vector3.Angle(target.transform.position - vector2, new Vector3(0f, 0f, -1f));
				rotation = new Vector3(0f, (0f - Mathf.Sign(vector.x)) * num, 0f);
				vector2 += 0.3f * vector;
			}
			else
			{
				vector2 = new Vector3(position.x, position.y + 0.047f, position.z + 0.95f);
			}
			handFadeArrow = NotificationManager.Get().CreateFadeArrow(vector2, rotation);
			if (target != null)
			{
				handFadeArrow.transform.localScale = 1.25f * Vector3.one;
			}
			handFadeArrow.transform.parent = card.transform;
		}
	}

	private void HideFadeArrow()
	{
		if (handFadeArrow != null)
		{
			NotificationManager.Get().DestroyNotification(handFadeArrow, 0f);
			handFadeArrow = null;
		}
	}

	private void OnPhoneHandShown(object userData)
	{
		if (handBounceArrow != null)
		{
			NotificationManager.Get().DestroyNotification(handBounceArrow, 0f);
			handBounceArrow = null;
		}
		ShowHandFadeArrow();
	}

	private void OnPhoneHandHidden(object userData)
	{
		HideFadeArrow();
		ShowHandBouncingArrow();
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
		{
			List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetDeckZone()
				.GetCards();
			firstMurlocCard = cards[cards.Count - 1];
			firstRaptorCard = cards[cards.Count - 2];
			GameState.Get().SetBusy(busy: true);
			Board.Get().FindCollider("DragPlane").enabled = false;
			yield return new WaitForSeconds(1.25f);
			ShowTutorialDialog("TUTORIAL01_HELP_14", "TUTORIAL01_HELP_15", "TUTORIAL01_HELP_16", Vector2.zero).SetWantedText(GameStrings.Get("MISSION_PRE_TUTORIAL_WANTED"));
			break;
		}
		case 2:
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().RegisterPhoneHandShownListener(OnPhoneHandShown);
				InputManager.Get().RegisterPhoneHandHiddenListener(OnPhoneHandHidden);
			}
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_02_02.prefab:cccdcb509085a974d922ac1d545d9bb6", "TUTORIAL01_JAINA_02", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			List<Card> cards2 = GameState.Get().GetFriendlySidePlayer().GetHandZone()
				.GetCards();
			if (GetTag(GAME_TAG.TURN) == 2 && cards2.Count == 1 && InputManager.Get().GetHeldCard() == null && !cards2[0].IsMousedOver())
			{
				Gameplay.Get().StartCoroutine(ShowArrowInSeconds(0f));
			}
			break;
		}
		case 3:
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().RemovePhoneHandShownListener(OnPhoneHandShown);
				InputManager.Get().RemovePhoneHandHiddenListener(OnPhoneHandHidden);
			}
			break;
		case 4:
			actor.SetActorState(ActorStateType.CARD_IDLE);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_06_06.prefab:ffe0ebdca06ca1d4c84cc28e4a1ed7cf", "TUTORIAL01_JAINA_06", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			if (firstMurlocCard != null)
			{
				firstMurlocCard.GetActor().ToggleForceIdle(bOn: true);
				firstMurlocCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			}
			break;
		case 6:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_17_13.prefab:9b257c86e7c7f9045a2b819d35876aca", "TUTORIAL01_JAINA_17", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			break;
		case 8:
			m_jainaSpeaking = true;
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_18_14.prefab:fedcdecb3346ec745b6fb4204f7dd4e0", "TUTORIAL01_JAINA_18", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			m_jainaSpeaking = false;
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(FlashMinionUntilAttackBegins(firstRaptorCard));
			break;
		case 10:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_19_15.prefab:659652a121ac01941a40c64c1c151f87", "TUTORIAL01_JAINA_19", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor hoggerActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(busy: true);
			HistoryManager.Get().DisableHistory();
			break;
		case 2:
			GameState.Get().SetBusy(busy: true);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_01_01.prefab:883391234efbde84eb99a16abd164d9d", "TUTORIAL01_JAINA_01", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			Gameplay.Get().SetGameStateBusy(busy: false, 2.2f);
			break;
		case 3:
		{
			int turn = GameState.Get().GetTurn();
			yield return new WaitForSeconds(2f);
			if (turn == GameState.Get().GetTurn())
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_03_03.prefab:4921407046d90bb44b2bfcf3984ffd47", "TUTORIAL01_JAINA_03", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
				if (GetTag(GAME_TAG.TURN) == 2 && !EndTurnButton.Get().IsInWaitingState())
				{
					ShowEndTurnBouncingArrow();
				}
			}
			break;
		}
		case 4:
		{
			GameState.Get().SetBusy(busy: true);
			AudioSource prevLine = GetPreloadedSound("VO_TUTORIAL_01_JAINA_03_03.prefab:4921407046d90bb44b2bfcf3984ffd47");
			while (SoundManager.Get().IsPlaying(prevLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_20_16.prefab:7980d02c581e4174991a8066e5785666", "TUTORIAL01_JAINA_20", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_06_06_ALT.prefab:6c9ef3c501462474ab59a37b967cab6f", "TUTORIAL01_HOGGER_07", Notification.SpeechBubbleDirection.TopRight, hoggerActor));
			Vector3 position6 = jainaActor.transform.position;
			Vector3 position7 = new Vector3(position6.x + 3.3f, position6.y + 0.5f, position6.z - 0.85f);
			Notification.PopUpArrowDirection direction2 = Notification.PopUpArrowDirection.Left;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position7 = new Vector3(position6.x + 3f, position6.y + 0.5f, position6.z + 0.85f);
				direction2 = Notification.PopUpArrowDirection.LeftDown;
			}
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position7, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_01"));
			notification.ShowPopUpArrow(direction2);
			NotificationManager.Get().DestroyNotification(notification, 5f);
			Gameplay.Get().SetGameStateBusy(busy: false, 5.2f);
			break;
		}
		case 5:
			HideFadeArrow();
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_05_05.prefab:982193e53ab81f04ba562de4b32dd39c", "TUTORIAL01_JAINA_05", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			break;
		case 7:
			NotificationManager.Get().DestroyAllPopUps();
			yield return new WaitForSeconds(1.7f);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_07_07.prefab:a8bf811494e94d742a3910fac9da906f", "TUTORIAL01_JAINA_07", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
			if (firstRaptorCard != null)
			{
				Vector3 position4 = firstRaptorCard.transform.position;
				Notification notification2;
				if (firstMurlocCard != null && firstRaptorCard.GetZonePosition() > firstMurlocCard.GetZonePosition())
				{
					Vector3 position5 = new Vector3(position4.x + 3f, position4.y, position4.z);
					notification2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position5, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_04"));
					notification2.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
				}
				else
				{
					Vector3 position5 = new Vector3(position4.x - 3f, position4.y, position4.z);
					notification2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position5, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_04"));
					notification2.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				}
				NotificationManager.Get().DestroyNotification(notification2, 4f);
			}
			yield return new WaitForSeconds(4f);
			if (GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
				.GetCards()
				.Count > 1 && !GameState.Get().IsInTargetMode())
			{
				ShowAttackWithYourMinionPopup();
			}
			if (GetTag(GAME_TAG.TURN) == 4 && EndTurnButton.Get().IsInNMPState())
			{
				yield return new WaitForSeconds(1f);
				ShowEndTurnBouncingArrow();
			}
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_03_03.prefab:4ef21f71824b97842b33d8ebccb37ed2", "TUTORIAL01_HOGGER_05", Notification.SpeechBubbleDirection.TopRight, hoggerActor));
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_21_17.prefab:c1524bd0ef92bb845b5dab48cbd017f9", "TUTORIAL01_JAINA_21", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 12:
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_15_11.prefab:a644986d34ab8964582c6221cde54d45", "TUTORIAL01_JAINA_15", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			break;
		case 13:
			while (m_jainaSpeaking)
			{
				yield return null;
			}
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_16_12.prefab:e6b4ab6fc1f11634e88f013ce5351e46", "TUTORIAL01_JAINA_16", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			break;
		case 14:
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_08_08_ALT.prefab:19ddb4ddaa4aee2468b17bae25da9419", "TUTORIAL01_HOGGER_08", Notification.SpeechBubbleDirection.TopRight, hoggerActor));
			Vector3 position2 = hoggerActor.transform.position;
			Vector3 position3 = new Vector3(position2.x + 3.3f, position2.y + 0.5f, position2.z - 1f);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position3 = new Vector3(position2.x + 3f, position2.y + 0.5f, position2.z - 0.75f);
			}
			Notification.PopUpArrowDirection direction = Notification.PopUpArrowDirection.Left;
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position3, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_09"));
			notification.ShowPopUpArrow(direction);
			NotificationManager.Get().DestroyNotification(notification, 5f);
			if (GetTag(GAME_TAG.TURN) == 6 && EndTurnButton.Get().IsInNMPState())
			{
				yield return new WaitForSeconds(9f);
				ShowEndTurnBouncingArrow();
			}
			break;
		}
		case 15:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_JAINA_02_55_ALT2.prefab:d049e67ad6c16db4da2c04be7a02a1ae", "", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			break;
		case 20:
		{
			GameState.Get().SetBusy(busy: true);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_JAINA_10_09.prefab:5bf553d532aca174083f48bf407b2b11", "TUTORIAL01_JAINA_10", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			yield return new WaitForSeconds(1.5f);
			GameState.Get().SetBusy(busy: false);
			List<Card> cards = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
				.GetCards();
			cards[cards.Count - 1].GetActor().GetAttackObject().Jiggle();
			break;
		}
		case 22:
			GameState.Get().SetBusy(busy: true);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_09_09_ALT.prefab:70c4d2941509856448660f89d6c72b2b", "TUTORIAL01_HOGGER_02", Notification.SpeechBubbleDirection.TopRight, hoggerActor));
			Gameplay.Get().SetGameStateBusy(busy: false, 2f);
			break;
		case 55:
			GetGameOptions().SetBooleanOption(GameEntityOption.DISABLE_TOOLTIPS, value: false);
			Board.Get().FindCollider("DragPlane").enabled = true;
			while (!announcerIsFinishedYapping)
			{
				yield return null;
			}
			if (!SoundUtils.CanDetectVolume())
			{
				Notification battlebegin = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 84.8f), GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_05"), "", 15f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_05.prefab:635b33010e4704a42a87c7625b5b5ada", "", Notification.SpeechBubbleDirection.None, null));
				NotificationManager.Get().DestroyNotification(battlebegin, 0f);
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_05.prefab:635b33010e4704a42a87c7625b5b5ada", "", Notification.SpeechBubbleDirection.None, null));
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_01_01.prefab:5833f4aeb72110741a2c9bc3a92f9bc8", "TUTORIAL01_HOGGER_01", Notification.SpeechBubbleDirection.TopRight, hoggerActor));
			GameState.Get().SetBusy(busy: false);
			yield return new WaitForSeconds(4f);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_04_04.prefab:3e16e42edb324e2469a25363ffd013a6", "TUTORIAL01_HOGGER_06", Notification.SpeechBubbleDirection.TopRight, hoggerActor));
			break;
		case 66:
		{
			Vector3 position = new Vector3(136f, NotificationManager.DEPTH, 131f);
			Vector3 middleSpot = new Vector3(136f, NotificationManager.DEPTH, 80f);
			if (!SoundUtils.CanDetectVolume())
			{
				Notification innkeeperLine3 = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, position, GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_01"), "", 15f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_01.prefab:79419083a1b828341be6d208491a88f8", "", Notification.SpeechBubbleDirection.None, null));
				NotificationManager.Get().DestroyNotification(innkeeperLine3, 0f);
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_01.prefab:79419083a1b828341be6d208491a88f8", "", Notification.SpeechBubbleDirection.None, null));
			}
			yield return new WaitForSeconds(0.5f);
			if (!SoundUtils.CanDetectVolume())
			{
				Notification innkeeperLine3 = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, middleSpot, GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_02"), "", 15f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_02.prefab:d6b08fa7e06a51c4abd80eea2ea30a41", "", Notification.SpeechBubbleDirection.None, null));
				NotificationManager.Get().DestroyNotification(innkeeperLine3, 0f);
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_02.prefab:d6b08fa7e06a51c4abd80eea2ea30a41", "", Notification.SpeechBubbleDirection.None, null));
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_02_02.prefab:7f321b26431a4974a82deefc368adf63", "TUTORIAL01_HOGGER_04", Notification.SpeechBubbleDirection.TopRight, hoggerActor));
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				Gameplay.Get().AddGamePlayNameBannerPhone();
			}
			if (!SoundUtils.CanDetectVolume())
			{
				Notification innkeeperLine3 = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_03"), "", 15f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_03.prefab:f47d0faf9067b3341bb9adb38f90be5b", "", Notification.SpeechBubbleDirection.None, null));
				NotificationManager.Get().DestroyNotification(innkeeperLine3, 0f);
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_03.prefab:f47d0faf9067b3341bb9adb38f90be5b", "", Notification.SpeechBubbleDirection.None, null));
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_04.prefab:e6fb72da1414d454f9d96a51c7009a82", "", Notification.SpeechBubbleDirection.None, null));
			announcerIsFinishedYapping = true;
			break;
		}
		default:
			Debug.LogWarning("WARNING - Mission fired an event that we are not listening for.");
			break;
		case 6:
			break;
		}
	}

	private void ShowAttackWithYourMinionPopup()
	{
		if (attackWithYourMinion != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(attackWithYourMinion);
		}
		if (firstAttackFinished || firstMurlocCard == null)
		{
			return;
		}
		firstMurlocCard.GetActor().ToggleForceIdle(bOn: false);
		firstMurlocCard.GetActor().SetActorState(ActorStateType.CARD_PLAYABLE);
		Vector3 position = firstMurlocCard.transform.position;
		if (!firstMurlocCard.GetEntity().IsExhausted() && firstMurlocCard.GetZone() is ZonePlay)
		{
			if (firstRaptorCard != null && firstMurlocCard.GetZonePosition() < firstRaptorCard.GetZonePosition())
			{
				Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
				attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), textToShowForAttackTip);
				attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			}
			else
			{
				Vector3 position2 = new Vector3(position.x + 3f, position.y, position.z);
				attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), textToShowForAttackTip);
				attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
			Card firstCard = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
				.GetFirstCard();
			ShowFadeArrow(firstMurlocCard, firstCard);
			Gameplay.Get().StartCoroutine(SwapHelpTextAndFlashMinion());
		}
	}

	private IEnumerator SwapHelpTextAndFlashMinion()
	{
		if (firstMurlocCard == null)
		{
			yield break;
		}
		Gameplay.Get().StartCoroutine(BeginFlashingMinionLoop(firstMurlocCard));
		yield return new WaitForSeconds(4f);
		if (!(textToShowForAttackTip == GameStrings.Get("TUTORIAL01_HELP_10")) && !firstMurlocCard.GetEntity().IsExhausted() && firstMurlocCard.GetActor().GetActorStateType() != ActorStateType.CARD_IDLE && firstMurlocCard.GetActor().GetActorStateType() != ActorStateType.CARD_MOUSE_OVER && firstMurlocCard.GetZone() is ZonePlay && !firstAttackFinished)
		{
			Vector3 position = firstMurlocCard.transform.position;
			textToShowForAttackTip = GameStrings.Get("TUTORIAL01_HELP_10");
			if (attackWithYourMinion != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(attackWithYourMinion);
			}
			if (firstRaptorCard != null && firstMurlocCard.GetZonePosition() < firstRaptorCard.GetZonePosition())
			{
				Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
				attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), textToShowForAttackTip);
				attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			}
			else
			{
				Vector3 position2 = new Vector3(position.x + 3f, position.y, position.z);
				attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), textToShowForAttackTip);
				attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
		}
	}

	private IEnumerator FlashMinionUntilAttackBegins(Card minionToFlash)
	{
		yield return new WaitForSeconds(8f);
		Gameplay.Get().StartCoroutine(BeginFlashingMinionLoop(minionToFlash));
	}

	private IEnumerator BeginFlashingMinionLoop(Card minionToFlash)
	{
		if (!(minionToFlash == null) && !minionToFlash.GetEntity().IsExhausted() && minionToFlash.GetActor().GetActorStateType() != ActorStateType.CARD_IDLE && minionToFlash.GetActor().GetActorStateType() != ActorStateType.CARD_MOUSE_OVER)
		{
			minionToFlash.GetActorSpell(SpellType.WIGGLE).Activate();
			yield return new WaitForSeconds(1.5f);
			Gameplay.Get().StartCoroutine(BeginFlashingMinionLoop(minionToFlash));
		}
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

	private void ShowDontFireballYourselfPopup(Vector3 origin)
	{
		if (noFireballPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(noFireballPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		noFireballPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"));
		NotificationManager.Get().DestroyNotification(noFireballPopup, 2.5f);
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	public override bool DoAlternateMulliganIntro()
	{
		AssetLoader.Get().InstantiatePrefab("GameOpen_Pack.prefab:fca6ae094e9a74644b00fc9029f304c3", PackLoadedCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		return true;
	}

	private void PackLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.Misc_Tutorial01);
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		startingPack = go;
		Transform transform = SceneUtils.FindChildBySubstring(startingPack, "Hero_Dummy").transform;
		heroCard.transform.parent = transform;
		heroCard.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		heroCard.transform.localPosition = new Vector3(0f, 0f, 0f);
		SceneUtils.SetLayer(heroCard.GetActor().GetRootObject(), GameLayer.IgnoreFullScreenEffects);
		Transform transform2 = SceneUtils.FindChildBySubstring(startingPack, "HeroEnemy_Dummy").transform;
		heroCard2.transform.parent = transform2;
		heroCard2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		heroCard2.transform.localPosition = new Vector3(0f, 0f, 0f);
		heroCard.SetDoNotSort(on: true);
		Transform transform3 = Board.Get().FindBone("Tutorial1HeroStart");
		go.transform.position = transform3.position;
		heroCard.GetActor().GetHealthObject().Hide();
		heroCard2.GetActor().GetHealthObject().Hide();
		heroCard2.GetActor().Hide();
		heroCard.GetActor().Hide();
		SceneMgr.Get().NotifySceneLoaded();
		Gameplay.Get().StartCoroutine(UpdatePresence());
		Gameplay.Get().StartCoroutine(ShowPackOpeningArrow(transform3.position));
	}

	private IEnumerator UpdatePresence()
	{
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
	}

	private IEnumerator ShowPackOpeningArrow(Vector3 packSpot)
	{
		yield return new WaitForSeconds(4f);
		if (!packOpened)
		{
			Vector3 position = new Vector3(packSpot.x + 4.014574f, packSpot.y, packSpot.z + 0.2307634f);
			freeCardsPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_18"));
			freeCardsPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
		}
	}

	public override void NotifyOfGamePackOpened()
	{
		packOpened = true;
		if (freeCardsPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(freeCardsPopup);
		}
	}

	public override void NotifyOfCustomIntroFinished()
	{
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		heroCard.SetDoNotSort(on: false);
		heroCard2.GetActor().TurnOnCollider();
		heroCard.GetActor().TurnOnCollider();
		heroCard.transform.parent = null;
		heroCard2.transform.parent = null;
		SceneUtils.SetLayer(heroCard.GetActor().GetRootObject(), GameLayer.CardRaycast);
		Gameplay.Get().StartCoroutine(ContinueFinishingCustomIntro());
	}

	private IEnumerator ContinueFinishingCustomIntro()
	{
		yield return new WaitForSeconds(3f);
		Object.Destroy(startingPack);
		GameState.Get().SetBusy(busy: false);
		MulliganManager.Get().SkipMulligan();
	}

	public override bool ShouldShowBigCard()
	{
		return GetTag(GAME_TAG.TURN) > 8;
	}

	public override void NotifyOfDefeatCoinAnimation()
	{
		PlaySound("VO_TUTORIAL_01_JAINA_13_10.prefab:b13670e36c248e141837c4eb0645a000");
	}

	public override List<RewardData> GetCustomRewards()
	{
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_023", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}
}
