using System.Collections;
using UnityEngine;

public class TutorialEntity : MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification thatsABadPlayPopup;

	private Notification manaReminder;

	private TooltipPanel historyTooltip;

	private static readonly float TUTORIAL_DIALOG_SCALE_PHONE = 1.4f;

	private static readonly Vector3 HELP_POPUP_SCALE = 16f * Vector3.one;

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

	public TutorialEntity()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	protected override void HandleMulliganTagChange()
	{
	}

	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	public static Vector3 GetTextScale()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return HELP_POPUP_SCALE * TUTORIAL_DIALOG_SCALE_PHONE;
		}
		return HELP_POPUP_SCALE;
	}

	public override bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		switch (error)
		{
		case PlayErrors.ErrorType.REQ_ENOUGH_MANA:
		{
			Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (errorSource.GetCost() > GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.RESOURCES))
			{
				Notification notification = NotificationManager.Get().CreateSpeechBubble(GameStrings.Get("TUTORIAL02_JAINA_05"), Notification.SpeechBubbleDirection.BottomLeft, actor, bDestroyWhenNewCreated: true);
				SoundManager.Get().LoadAndPlay("VO_TUTORIAL_02_JAINA_05_20.prefab:700f7c6b778de5d41bf6d45a2e01b13d");
				NotificationManager.Get().DestroyNotification(notification, 3.5f);
				Gameplay.Get().StartCoroutine(DisplayManaReminder(GameStrings.Get("TUTORIAL02_HELP_01")));
			}
			else
			{
				Notification notification2 = NotificationManager.Get().CreateSpeechBubble(GameStrings.Get("TUTORIAL02_JAINA_04"), Notification.SpeechBubbleDirection.BottomLeft, actor, bDestroyWhenNewCreated: true);
				SoundManager.Get().LoadAndPlay("VO_TUTORIAL_02_JAINA_04_19.prefab:af04fcf53166d04469dc1b22b4181bf9");
				NotificationManager.Get().DestroyNotification(notification2, 3.5f);
				Gameplay.Get().StartCoroutine(DisplayManaReminder(GameStrings.Get("TUTORIAL02_HELP_03")));
			}
			return true;
		}
		case PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0:
			if (errorSource.GetCardId() == "TU4a_006")
			{
				return true;
			}
			break;
		}
		if (error == PlayErrors.ErrorType.REQ_TARGET_TAUNTER)
		{
			SoundManager.Get().LoadAndPlay("UI_no_can_do.prefab:7b1a22774f818544387c0f2ca4fea02c");
			GameState.Get().GetFriendlySidePlayer().GetHeroCard()
				.PlayEmote(EmoteType.ERROR_TAUNT);
			GameState.Get().ShowEnemyTauntCharacters();
			HighlightTaunters();
			return true;
		}
		return false;
	}

	private IEnumerator DisplayManaReminder(string reminderText)
	{
		yield return new WaitForSeconds(0.5f);
		if (manaReminder != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(manaReminder);
		}
		NotifyOfManaError();
		Vector3 manaCrystalSpawnPosition = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
		Vector3 position;
		Notification.PopUpArrowDirection direction;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.7f, manaCrystalSpawnPosition.y + 1.14f, manaCrystalSpawnPosition.z + 4.33f);
			direction = Notification.PopUpArrowDirection.RightDown;
		}
		else
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.02f, manaCrystalSpawnPosition.y + 0.2f, manaCrystalSpawnPosition.z + 1.93f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		manaReminder = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, GetTextScale(), reminderText);
		manaReminder.ShowPopUpArrow(direction);
		NotificationManager.Get().DestroyNotification(manaReminder, 4f);
	}

	private void HighlightTaunters()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().HasTaunt() && !card.GetEntity().IsStealthed())
			{
				NotificationManager.Get().DestroyAllPopUps();
				Vector3 position = new Vector3(card.transform.position.x - 2f, card.transform.position.y, card.transform.position.z);
				Notification notification = NotificationManager.Get().CreateFadeArrow(position, new Vector3(0f, 270f, 0f));
				NotificationManager.Get().DestroyNotification(notification, 3f);
				break;
			}
		}
	}

	public override bool NotifyOfTooltipDisplay(TooltipZone tooltip)
	{
		ZoneDeck component = tooltip.targetObject.GetComponent<ZoneDeck>();
		if (component == null)
		{
			return false;
		}
		if (component.m_Side == Player.Side.FRIENDLY)
		{
			string headline = GameStrings.Get("GAMEPLAY_TOOLTIP_DECK_HEADLINE");
			string bodytext = GameStrings.Get("TUTORIAL_TOOLTIP_DECK_DESCRIPTION");
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				tooltip.ShowGameplayTooltipLarge(headline, bodytext);
			}
			else
			{
				tooltip.ShowGameplayTooltip(headline, bodytext);
			}
			return true;
		}
		if (component.m_Side == Player.Side.OPPOSING)
		{
			string headline = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYDECK_HEADLINE");
			string bodytext = GameStrings.Get("TUTORIAL_TOOLTIP_ENEMYDECK_DESC");
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				tooltip.ShowGameplayTooltipLarge(headline, bodytext);
			}
			else
			{
				tooltip.ShowGameplayTooltip(headline, bodytext);
			}
			return true;
		}
		return false;
	}

	public override void NotifyOfHeroesFinishedAnimatingInMulligan()
	{
		Board.Get().FindCollider("DragPlane").GetComponent<Collider>()
			.enabled = false;
		HandleMissionEvent(54);
	}

	public override bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		if (!clickedEntity.IsControlledByLocalUser())
		{
			return true;
		}
		Network.Options.Option selectedNetworkOption = GameState.Get().GetSelectedNetworkOption();
		if (selectedNetworkOption == null || selectedNetworkOption.Main == null)
		{
			return true;
		}
		Entity entity = GameState.Get().GetEntity(selectedNetworkOption.Main.ID);
		if (!wasInTargetMode || entity == null)
		{
			return true;
		}
		if (clickedEntity == entity)
		{
			return true;
		}
		switch (entity.GetCardId())
		{
		case "CS2_022":
		case "CS2_029":
		case "CS2_034":
			ShowDontHurtYourselfPopup(clickedEntity.GetCard().transform.position);
			return false;
		default:
			return true;
		}
	}

	private void ShowDontHurtYourselfPopup(Vector3 origin)
	{
		if (thatsABadPlayPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(thatsABadPlayPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		thatsABadPlayPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"));
		NotificationManager.Get().DestroyNotification(thatsABadPlayPopup, 2.5f);
	}

	protected void HandleGameStartEvent()
	{
		MulliganManager.Get().ForceMulliganActive(active: true);
		MulliganManager.Get().SkipCardChoosing();
		TurnStartManager.Get().BeginListeningForTurnEvents();
	}

	protected void UserPressedStartButton(UIEvent e)
	{
		HandleMissionEvent(55);
	}

	protected TutorialNotification ShowTutorialDialog(string headlineGameString, string bodyTextGameString, string buttonGameString, Vector2 materialOffset, bool swapMaterial = false)
	{
		return NotificationManager.Get().CreateTutorialDialog(headlineGameString, bodyTextGameString, buttonGameString, UserPressedStartButton, materialOffset, swapMaterial);
	}

	public override void NotifyOfHistoryTokenMousedOver(GameObject mousedOverTile)
	{
		historyTooltip = TooltipPanelManager.Get().CreateKeywordPanel(0);
		historyTooltip.Reset();
		historyTooltip.Initialize(GameStrings.Get("TUTORIAL_TOOLTIP_HISTORY_HEADLINE"), GameStrings.Get("TUTORIAL_TOOLTIP_HISTORY_DESC"));
		Vector3 localPosition = ((!UniversalInputManager.Get().IsTouchMode()) ? new Vector3(-1.140343f, 0.1916952f, 0.4895353f) : new Vector3(1f, 0.1916952f, 1.2f));
		historyTooltip.transform.parent = mousedOverTile.GetComponent<HistoryCard>().m_mainCardActor.transform;
		float num = 0.4792188f;
		historyTooltip.transform.localPosition = localPosition;
		historyTooltip.transform.localScale = new Vector3(num, num, num);
	}

	public override void NotifyOfHistoryTokenMousedOut()
	{
		if (historyTooltip != null)
		{
			Object.Destroy(historyTooltip.gameObject);
		}
	}

	protected virtual void NotifyOfManaError()
	{
	}

	protected void SetTutorialProgress(TutorialProgress val)
	{
		if (GameMgr.Get().IsSpectator())
		{
			return;
		}
		if (!Network.ShouldBeConnectedToAurora())
		{
			if (GameUtils.AreAllTutorialsComplete(val))
			{
				Options.Get().SetBool(Option.CONNECT_TO_AURORA, val: true);
			}
			Options.Get().SetEnum(Option.LOCAL_TUTORIAL_PROGRESS, val);
		}
		AdTrackingManager.Get().TrackTutorialProgress(val);
		NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
		if (netObject != null)
		{
			netObject.CampaignProgress = val;
		}
		NetCache.Get().NetCacheChanged<NetCache.NetCacheProfileProgress>();
	}

	protected void SetTutorialLostProgress(TutorialProgress val)
	{
		int @int = Options.Get().GetInt(Option.TUTORIAL_LOST_PROGRESS);
		@int |= 1 << (int)val;
		Options.Get().SetInt(Option.TUTORIAL_LOST_PROGRESS, @int);
		AdTrackingManager.Get().TrackTutorialProgress(val, isVictory: false);
	}

	protected bool DidLoseTutorial(TutorialProgress val)
	{
		int @int = Options.Get().GetInt(Option.TUTORIAL_LOST_PROGRESS);
		bool result = false;
		if ((@int & (1 << (int)val)) > 0)
		{
			result = true;
		}
		return result;
	}

	protected void ResetTutorialLostProgress()
	{
		Options.Get().SetInt(Option.TUTORIAL_LOST_PROGRESS, 0);
	}
}
