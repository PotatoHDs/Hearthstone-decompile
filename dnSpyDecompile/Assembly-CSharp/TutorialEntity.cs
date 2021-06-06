using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005DE RID: 1502
public class TutorialEntity : MissionEntity
{
	// Token: 0x06005248 RID: 21064 RVA: 0x0016FDF5 File Offset: 0x0016DFF5
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

	// Token: 0x06005249 RID: 21065 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x0600524A RID: 21066 RVA: 0x001B06CC File Offset: 0x001AE8CC
	public TutorialEntity()
	{
		this.m_gameOptions.AddOptions(TutorialEntity.s_booleanOptions, TutorialEntity.s_stringOptions);
	}

	// Token: 0x0600524B RID: 21067 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected override void HandleMulliganTagChange()
	{
	}

	// Token: 0x0600524C RID: 21068 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x0600524D RID: 21069 RVA: 0x001B06E9 File Offset: 0x001AE8E9
	public static Vector3 GetTextScale()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return TutorialEntity.HELP_POPUP_SCALE * TutorialEntity.TUTORIAL_DIALOG_SCALE_PHONE;
		}
		return TutorialEntity.HELP_POPUP_SCALE;
	}

	// Token: 0x0600524E RID: 21070 RVA: 0x001B070C File Offset: 0x001AE90C
	public override bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		if (error == PlayErrors.ErrorType.REQ_ENOUGH_MANA)
		{
			Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
			if (errorSource.GetCost() > GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.RESOURCES))
			{
				Notification notification = NotificationManager.Get().CreateSpeechBubble(GameStrings.Get("TUTORIAL02_JAINA_05"), Notification.SpeechBubbleDirection.BottomLeft, actor, true, true, 0f);
				SoundManager.Get().LoadAndPlay("VO_TUTORIAL_02_JAINA_05_20.prefab:700f7c6b778de5d41bf6d45a2e01b13d");
				NotificationManager.Get().DestroyNotification(notification, 3.5f);
				Gameplay.Get().StartCoroutine(this.DisplayManaReminder(GameStrings.Get("TUTORIAL02_HELP_01")));
			}
			else
			{
				Notification notification2 = NotificationManager.Get().CreateSpeechBubble(GameStrings.Get("TUTORIAL02_JAINA_04"), Notification.SpeechBubbleDirection.BottomLeft, actor, true, true, 0f);
				SoundManager.Get().LoadAndPlay("VO_TUTORIAL_02_JAINA_04_19.prefab:af04fcf53166d04469dc1b22b4181bf9");
				NotificationManager.Get().DestroyNotification(notification2, 3.5f);
				Gameplay.Get().StartCoroutine(this.DisplayManaReminder(GameStrings.Get("TUTORIAL02_HELP_03")));
			}
			return true;
		}
		if (error == PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0 && errorSource.GetCardId() == "TU4a_006")
		{
			return true;
		}
		if (error == PlayErrors.ErrorType.REQ_TARGET_TAUNTER)
		{
			SoundManager.Get().LoadAndPlay("UI_no_can_do.prefab:7b1a22774f818544387c0f2ca4fea02c");
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_TAUNT);
			GameState.Get().ShowEnemyTauntCharacters();
			this.HighlightTaunters();
			return true;
		}
		return false;
	}

	// Token: 0x0600524F RID: 21071 RVA: 0x001B086E File Offset: 0x001AEA6E
	private IEnumerator DisplayManaReminder(string reminderText)
	{
		yield return new WaitForSeconds(0.5f);
		if (this.manaReminder != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.manaReminder);
		}
		this.NotifyOfManaError();
		Vector3 manaCrystalSpawnPosition = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
		Vector3 position;
		Notification.PopUpArrowDirection direction;
		if (UniversalInputManager.UsePhoneUI)
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.7f, manaCrystalSpawnPosition.y + 1.14f, manaCrystalSpawnPosition.z + 4.33f);
			direction = Notification.PopUpArrowDirection.RightDown;
		}
		else
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.02f, manaCrystalSpawnPosition.y + 0.2f, manaCrystalSpawnPosition.z + 1.93f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		this.manaReminder = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), reminderText, true, NotificationManager.PopupTextType.BASIC);
		this.manaReminder.ShowPopUpArrow(direction);
		NotificationManager.Get().DestroyNotification(this.manaReminder, 4f);
		yield break;
	}

	// Token: 0x06005250 RID: 21072 RVA: 0x001B0884 File Offset: 0x001AEA84
	private void HighlightTaunters()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
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

	// Token: 0x06005251 RID: 21073 RVA: 0x001B097C File Offset: 0x001AEB7C
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
			if (UniversalInputManager.UsePhoneUI)
			{
				tooltip.ShowGameplayTooltipLarge(headline, bodytext, 0);
			}
			else
			{
				tooltip.ShowGameplayTooltip(headline, bodytext, 0);
			}
			return true;
		}
		if (component.m_Side == Player.Side.OPPOSING)
		{
			string headline = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYDECK_HEADLINE");
			string bodytext = GameStrings.Get("TUTORIAL_TOOLTIP_ENEMYDECK_DESC");
			if (UniversalInputManager.UsePhoneUI)
			{
				tooltip.ShowGameplayTooltipLarge(headline, bodytext, 0);
			}
			else
			{
				tooltip.ShowGameplayTooltip(headline, bodytext, 0);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06005252 RID: 21074 RVA: 0x001B0A23 File Offset: 0x001AEC23
	public override void NotifyOfHeroesFinishedAnimatingInMulligan()
	{
		Board.Get().FindCollider("DragPlane").GetComponent<Collider>().enabled = false;
		base.HandleMissionEvent(54);
	}

	// Token: 0x06005253 RID: 21075 RVA: 0x001B0A48 File Offset: 0x001AEC48
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
		string cardId = entity.GetCardId();
		if (cardId == "CS2_022" || cardId == "CS2_029" || cardId == "CS2_034")
		{
			this.ShowDontHurtYourselfPopup(clickedEntity.GetCard().transform.position);
			return false;
		}
		return true;
	}

	// Token: 0x06005254 RID: 21076 RVA: 0x001B0AE4 File Offset: 0x001AECE4
	private void ShowDontHurtYourselfPopup(Vector3 origin)
	{
		if (this.thatsABadPlayPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.thatsABadPlayPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		this.thatsABadPlayPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"), true, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.thatsABadPlayPopup, 2.5f);
	}

	// Token: 0x06005255 RID: 21077 RVA: 0x001B0B66 File Offset: 0x001AED66
	protected void HandleGameStartEvent()
	{
		MulliganManager.Get().ForceMulliganActive(true);
		MulliganManager.Get().SkipCardChoosing();
		TurnStartManager.Get().BeginListeningForTurnEvents(false);
	}

	// Token: 0x06005256 RID: 21078 RVA: 0x001B0B88 File Offset: 0x001AED88
	protected void UserPressedStartButton(UIEvent e)
	{
		base.HandleMissionEvent(55);
	}

	// Token: 0x06005257 RID: 21079 RVA: 0x001B0B92 File Offset: 0x001AED92
	protected TutorialNotification ShowTutorialDialog(string headlineGameString, string bodyTextGameString, string buttonGameString, Vector2 materialOffset, bool swapMaterial = false)
	{
		return NotificationManager.Get().CreateTutorialDialog(headlineGameString, bodyTextGameString, buttonGameString, new UIEvent.Handler(this.UserPressedStartButton), materialOffset, swapMaterial);
	}

	// Token: 0x06005258 RID: 21080 RVA: 0x001B0BB4 File Offset: 0x001AEDB4
	public override void NotifyOfHistoryTokenMousedOver(GameObject mousedOverTile)
	{
		this.historyTooltip = TooltipPanelManager.Get().CreateKeywordPanel(0);
		this.historyTooltip.Reset();
		this.historyTooltip.Initialize(GameStrings.Get("TUTORIAL_TOOLTIP_HISTORY_HEADLINE"), GameStrings.Get("TUTORIAL_TOOLTIP_HISTORY_DESC"));
		Vector3 localPosition;
		if (UniversalInputManager.Get().IsTouchMode())
		{
			localPosition = new Vector3(1f, 0.1916952f, 1.2f);
		}
		else
		{
			localPosition = new Vector3(-1.140343f, 0.1916952f, 0.4895353f);
		}
		this.historyTooltip.transform.parent = mousedOverTile.GetComponent<HistoryCard>().m_mainCardActor.transform;
		float num = 0.4792188f;
		this.historyTooltip.transform.localPosition = localPosition;
		this.historyTooltip.transform.localScale = new Vector3(num, num, num);
	}

	// Token: 0x06005259 RID: 21081 RVA: 0x001B0C85 File Offset: 0x001AEE85
	public override void NotifyOfHistoryTokenMousedOut()
	{
		if (this.historyTooltip != null)
		{
			UnityEngine.Object.Destroy(this.historyTooltip.gameObject);
		}
	}

	// Token: 0x0600525A RID: 21082 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void NotifyOfManaError()
	{
	}

	// Token: 0x0600525B RID: 21083 RVA: 0x001B0CA8 File Offset: 0x001AEEA8
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
				Options.Get().SetBool(Option.CONNECT_TO_AURORA, true);
			}
			Options.Get().SetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS, val);
		}
		AdTrackingManager.Get().TrackTutorialProgress(val, true);
		NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
		if (netObject != null)
		{
			netObject.CampaignProgress = val;
		}
		NetCache.Get().NetCacheChanged<NetCache.NetCacheProfileProgress>();
	}

	// Token: 0x0600525C RID: 21084 RVA: 0x001B0D18 File Offset: 0x001AEF18
	protected void SetTutorialLostProgress(TutorialProgress val)
	{
		int num = Options.Get().GetInt(Option.TUTORIAL_LOST_PROGRESS);
		num |= 1 << (int)val;
		Options.Get().SetInt(Option.TUTORIAL_LOST_PROGRESS, num);
		AdTrackingManager.Get().TrackTutorialProgress(val, false);
	}

	// Token: 0x0600525D RID: 21085 RVA: 0x001B0D54 File Offset: 0x001AEF54
	protected bool DidLoseTutorial(TutorialProgress val)
	{
		int @int = Options.Get().GetInt(Option.TUTORIAL_LOST_PROGRESS);
		bool result = false;
		if ((@int & 1 << (int)val) > 0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600525E RID: 21086 RVA: 0x001B0D7C File Offset: 0x001AEF7C
	protected void ResetTutorialLostProgress()
	{
		Options.Get().SetInt(Option.TUTORIAL_LOST_PROGRESS, 0);
	}

	// Token: 0x0400497B RID: 18811
	private static Map<GameEntityOption, bool> s_booleanOptions = TutorialEntity.InitBooleanOptions();

	// Token: 0x0400497C RID: 18812
	private static Map<GameEntityOption, string> s_stringOptions = TutorialEntity.InitStringOptions();

	// Token: 0x0400497D RID: 18813
	private Notification thatsABadPlayPopup;

	// Token: 0x0400497E RID: 18814
	private Notification manaReminder;

	// Token: 0x0400497F RID: 18815
	private TooltipPanel historyTooltip;

	// Token: 0x04004980 RID: 18816
	private static readonly float TUTORIAL_DIALOG_SCALE_PHONE = 1.4f;

	// Token: 0x04004981 RID: 18817
	private static readonly Vector3 HELP_POPUP_SCALE = 16f * Vector3.one;
}
