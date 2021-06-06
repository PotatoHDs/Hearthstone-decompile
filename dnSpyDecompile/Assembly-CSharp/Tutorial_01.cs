using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D8 RID: 1496
public class Tutorial_01 : TutorialEntity
{
	// Token: 0x060051AD RID: 20909 RVA: 0x001ACFD1 File Offset: 0x001AB1D1
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

	// Token: 0x060051AE RID: 20910 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x060051AF RID: 20911 RVA: 0x001ACFF4 File Offset: 0x001AB1F4
	public Tutorial_01()
	{
		this.m_gameOptions.AddOptions(Tutorial_01.s_booleanOptions, Tutorial_01.s_stringOptions);
		MulliganManager.Get().ForceMulliganActive(true);
	}

	// Token: 0x060051B0 RID: 20912 RVA: 0x001AD11C File Offset: 0x001AB31C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_TUTORIAL_01_ANNOUNCER_01.prefab:79419083a1b828341be6d208491a88f8");
		base.PreloadSound("VO_TUTORIAL_01_ANNOUNCER_02.prefab:d6b08fa7e06a51c4abd80eea2ea30a41");
		base.PreloadSound("VO_TUTORIAL_01_ANNOUNCER_03.prefab:f47d0faf9067b3341bb9adb38f90be5b");
		base.PreloadSound("VO_TUTORIAL_01_ANNOUNCER_04.prefab:e6fb72da1414d454f9d96a51c7009a82");
		base.PreloadSound("VO_TUTORIAL_01_ANNOUNCER_05.prefab:635b33010e4704a42a87c7625b5b5ada");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_13_10.prefab:b13670e36c248e141837c4eb0645a000");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_01_01.prefab:883391234efbde84eb99a16abd164d9d");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_02_02.prefab:cccdcb509085a974d922ac1d545d9bb6");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_03_03.prefab:4921407046d90bb44b2bfcf3984ffd47");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_20_16.prefab:7980d02c581e4174991a8066e5785666");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_05_05.prefab:982193e53ab81f04ba562de4b32dd39c");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_06_06.prefab:ffe0ebdca06ca1d4c84cc28e4a1ed7cf");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_07_07.prefab:a8bf811494e94d742a3910fac9da906f");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_21_17.prefab:c1524bd0ef92bb845b5dab48cbd017f9");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_09_08.prefab:b7b739d9e31865a478275394ee57ad89");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_15_11.prefab:a644986d34ab8964582c6221cde54d45");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_16_12.prefab:e6b4ab6fc1f11634e88f013ce5351e46");
		base.PreloadSound("VO_TUTORIAL_JAINA_02_55_ALT2.prefab:d049e67ad6c16db4da2c04be7a02a1ae");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_10_09.prefab:5bf553d532aca174083f48bf407b2b11");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_17_13.prefab:9b257c86e7c7f9045a2b819d35876aca");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_18_14.prefab:fedcdecb3346ec745b6fb4204f7dd4e0");
		base.PreloadSound("VO_TUTORIAL_01_JAINA_19_15.prefab:659652a121ac01941a40c64c1c151f87");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_01_01.prefab:5833f4aeb72110741a2c9bc3a92f9bc8");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_02_02.prefab:7f321b26431a4974a82deefc368adf63");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_03_03.prefab:4ef21f71824b97842b33d8ebccb37ed2");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_04_04.prefab:3e16e42edb324e2469a25363ffd013a6");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_06_06_ALT.prefab:6c9ef3c501462474ab59a37b967cab6f");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_08_08_ALT.prefab:19ddb4ddaa4aee2468b17bae25da9419");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_09_09_ALT.prefab:70c4d2941509856448660f89d6c72b2b");
		base.PreloadSound("VO_TUTORIAL_01_HOGGER_11_11.prefab:1fdb0543bf56c4b4e95148a518bd9a2d");
	}

	// Token: 0x060051B1 RID: 20913 RVA: 0x001AD274 File Offset: 0x001AB474
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		if (this.attackHelpPanel != null)
		{
			UnityEngine.Object.Destroy(this.attackHelpPanel.gameObject);
			this.attackHelpPanel = null;
		}
		if (this.healthHelpPanel != null)
		{
			UnityEngine.Object.Destroy(this.healthHelpPanel.gameObject);
			this.healthHelpPanel = null;
		}
		this.EnsureCardGemsAreOnTheCorrectLayer();
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			base.SetTutorialProgress(TutorialProgress.HOGGER_COMPLETE);
			base.PlaySound("VO_TUTORIAL_01_HOGGER_11_11.prefab:1fdb0543bf56c4b4e95148a518bd9a2d", 1f, true, false);
		}
		else if (gameResult == TAG_PLAYSTATE.TIED)
		{
			base.PlaySound("VO_TUTORIAL_01_HOGGER_11_11.prefab:1fdb0543bf56c4b4e95148a518bd9a2d", 1f, true, false);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			InputManager.Get().RemovePhoneHandShownListener(new InputManager.PhoneHandShownCallback(this.OnPhoneHandShown));
			InputManager.Get().RemovePhoneHandHiddenListener(new InputManager.PhoneHandHiddenCallback(this.OnPhoneHandHidden));
		}
	}

	// Token: 0x060051B2 RID: 20914 RVA: 0x001AD348 File Offset: 0x001AB548
	private void EnsureCardGemsAreOnTheCorrectLayer()
	{
		List<Card> list = new List<Card>();
		list.AddRange(GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards());
		list.AddRange(GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards());
		list.Add(GameState.Get().GetFriendlySidePlayer().GetHeroCard());
		list.Add(GameState.Get().GetOpposingSidePlayer().GetHeroCard());
		foreach (Card card in list)
		{
			if (!(card == null) && !(card.GetActor() == null))
			{
				if (card.GetActor().GetAttackObject() != null)
				{
					SceneUtils.SetLayer(card.GetActor().GetAttackObject().gameObject, GameLayer.Default);
				}
				if (card.GetActor().GetHealthObject() != null)
				{
					SceneUtils.SetLayer(card.GetActor().GetHealthObject().gameObject, GameLayer.Default);
				}
			}
		}
	}

	// Token: 0x060051B3 RID: 20915 RVA: 0x001AD45C File Offset: 0x001AB65C
	public override void NotifyOfCardGrabbed(Entity entity)
	{
		if (base.GetTag(GAME_TAG.TURN) == 2 || entity.GetCardId() == "TU5_CS2_025")
		{
			BoardTutorial.Get().EnableHighlight(true);
		}
		this.NukeNumberLabels();
	}

	// Token: 0x060051B4 RID: 20916 RVA: 0x001AD48C File Offset: 0x001AB68C
	public override void NotifyOfCardDropped(Entity entity)
	{
		if (base.GetTag(GAME_TAG.TURN) == 2 || entity.GetCardId() == "TU5_CS2_025")
		{
			BoardTutorial.Get().EnableHighlight(false);
		}
	}

	// Token: 0x060051B5 RID: 20917 RVA: 0x001AD4B8 File Offset: 0x001AB6B8
	public override bool NotifyOfEndTurnButtonPushed()
	{
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket != null && !optionsPacket.HasValidOption())
		{
			NotificationManager.Get().DestroyAllArrows();
			return true;
		}
		if (this.endTurnNotifier != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.endTurnNotifier);
		}
		Vector3 position = EndTurnButton.Get().transform.position;
		Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
		string key = "TUTORIAL_NO_ENDTURN_ATK";
		if (!GameState.Get().GetFriendlySidePlayer().HasReadyAttackers())
		{
			key = "TUTORIAL_NO_ENDTURN";
		}
		this.endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.endTurnNotifier, 2.5f);
		return false;
	}

	// Token: 0x060051B6 RID: 20918 RVA: 0x001AD586 File Offset: 0x001AB786
	public override bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		return error == PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0 && errorSource.GetCardId() == "TU4a_006";
	}

	// Token: 0x060051B7 RID: 20919 RVA: 0x001AD5A4 File Offset: 0x001AB7A4
	public override void NotifyOfTargetModeCancelled()
	{
		if (this.crushThisGnoll == null)
		{
			return;
		}
		NotificationManager.Get().DestroyAllPopUps();
		if (this.firstRaptorCard == null || !(this.firstRaptorCard.GetZone() is ZonePlay))
		{
			return;
		}
		this.ShowAttackWithYourMinionPopup();
	}

	// Token: 0x060051B8 RID: 20920 RVA: 0x001AD5F4 File Offset: 0x001AB7F4
	public override bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		if (base.GetTag(GAME_TAG.TURN) == 4)
		{
			if (clickedEntity.GetCardId() == "TU5_CS2_168")
			{
				if (!wasInTargetMode && !this.firstAttackFinished)
				{
					if (this.crushThisGnoll != null)
					{
						NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.crushThisGnoll);
					}
					NotificationManager.Get().DestroyAllPopUps();
					Vector3 position = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetFirstCard().transform.position;
					Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
					this.crushThisGnoll = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_03"), true, NotificationManager.PopupTextType.BASIC);
					this.crushThisGnoll.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
					this.numTimesTextSwapStarted++;
					Gameplay.Get().StartCoroutine(this.WaitAndThenHide(this.numTimesTextSwapStarted));
				}
			}
			else if (clickedEntity.GetCardId() == "TU4a_002" && wasInTargetMode)
			{
				if (this.crushThisGnoll != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.crushThisGnoll);
				}
				NotificationManager.Get().DestroyAllPopUps();
				this.firstAttackFinished = true;
			}
		}
		else if (base.GetTag(GAME_TAG.TURN) == 6 && clickedEntity.GetCardId() == "TU4a_001" && wasInTargetMode)
		{
			NotificationManager.Get().DestroyAllPopUps();
		}
		if (wasInTargetMode && InputManager.Get().GetHeldCard() != null && InputManager.Get().GetHeldCard().GetEntity().GetCardId() == "TU5_CS2_029")
		{
			if (clickedEntity.IsControlledByLocalUser())
			{
				this.ShowDontFireballYourselfPopup(clickedEntity.GetCard().transform.position);
				return false;
			}
			if (clickedEntity.GetCardId() == "TU4a_003" && base.GetTag(GAME_TAG.TURN) >= 8)
			{
				if (this.noFireballPopup != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.noFireballPopup);
				}
				Vector3 position3 = clickedEntity.GetCard().transform.position;
				Vector3 position4 = new Vector3(position3.x - 3f, position3.y, position3.z);
				this.noFireballPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position4, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_08"), true, NotificationManager.PopupTextType.BASIC);
				NotificationManager.Get().DestroyNotification(this.noFireballPopup, 3f);
				return false;
			}
		}
		return true;
	}

	// Token: 0x060051B9 RID: 20921 RVA: 0x001AD86A File Offset: 0x001ABA6A
	private IEnumerator WaitAndThenHide(int numTimesStarted)
	{
		yield return new WaitForSeconds(6f);
		if (this.crushThisGnoll == null)
		{
			yield break;
		}
		if (numTimesStarted != this.numTimesTextSwapStarted)
		{
			yield break;
		}
		if (GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetFirstCard() == null)
		{
			yield break;
		}
		NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.crushThisGnoll);
		yield break;
	}

	// Token: 0x060051BA RID: 20922 RVA: 0x001AD880 File Offset: 0x001ABA80
	public override bool NotifyOfCardTooltipDisplayShow(Card card)
	{
		if (GameState.Get().IsGameOver())
		{
			return false;
		}
		Entity entity = card.GetEntity();
		if (entity.IsMinion())
		{
			if (this.attackHelpPanel == null)
			{
				this.m_isShowingAttackHelpPanel = true;
				this.ShowAttackTooltip(card);
				Gameplay.Get().StartCoroutine(this.ShowHealthTooltipAfterWait(card));
			}
			return false;
		}
		if (entity.IsHero())
		{
			if (this.healthHelpPanel == null)
			{
				this.ShowHealthTooltip(card);
			}
			return false;
		}
		return true;
	}

	// Token: 0x060051BB RID: 20923 RVA: 0x001AD8FC File Offset: 0x001ABAFC
	private void ShowAttackTooltip(Card card)
	{
		SceneUtils.SetLayer(card.GetActor().GetAttackObject().gameObject, GameLayer.Tooltip);
		Vector3 position = card.transform.position;
		Vector3 vector = this.m_attackTooltipPosition;
		Vector3 position2 = new Vector3(position.x + vector.x, position.y + vector.y, position.z + vector.z);
		this.attackHelpPanel = TooltipPanelManager.Get().CreateKeywordPanel(0);
		this.attackHelpPanel.Reset();
		this.attackHelpPanel.SetScale(TooltipPanel.GAMEPLAY_SCALE);
		this.attackHelpPanel.Initialize(GameStrings.Get("GLOBAL_ATTACK"), GameStrings.Get("TUTORIAL01_HELP_12"));
		this.attackHelpPanel.transform.position = position2;
		RenderUtils.SetAlpha(this.attackHelpPanel.gameObject, 0f);
		iTween.FadeTo(this.attackHelpPanel.gameObject, iTween.Hash(new object[]
		{
			"alpha",
			1,
			"time",
			0.25f
		}));
		card.GetActor().GetAttackObject().Enlarge(this.m_gemScale);
	}

	// Token: 0x060051BC RID: 20924 RVA: 0x001ADA38 File Offset: 0x001ABC38
	private IEnumerator ShowHealthTooltipAfterWait(Card card)
	{
		yield return new WaitForSeconds(0.05f);
		if (InputManager.Get().GetMousedOverCard() != card)
		{
			yield break;
		}
		this.ShowHealthTooltip(card);
		yield break;
	}

	// Token: 0x060051BD RID: 20925 RVA: 0x001ADA50 File Offset: 0x001ABC50
	private void ShowHealthTooltip(Card card)
	{
		SceneUtils.SetLayer(card.GetActor().GetHealthObject().gameObject, GameLayer.Tooltip);
		Vector3 position = card.transform.position;
		Vector3 vector = this.m_healthTooltipPosition;
		if (card.GetEntity().IsHero())
		{
			vector = this.m_heroHealthTooltipPosition;
			if (UniversalInputManager.UsePhoneUI)
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
		this.healthHelpPanel = TooltipPanelManager.Get().CreateKeywordPanel(0);
		this.healthHelpPanel.Reset();
		this.healthHelpPanel.SetScale(TooltipPanel.GAMEPLAY_SCALE);
		this.healthHelpPanel.Initialize(GameStrings.Get("GLOBAL_HEALTH"), GameStrings.Get("TUTORIAL01_HELP_13"));
		this.healthHelpPanel.transform.position = position2;
		RenderUtils.SetAlpha(this.healthHelpPanel.gameObject, 0f);
		iTween.FadeTo(this.healthHelpPanel.gameObject, iTween.Hash(new object[]
		{
			"alpha",
			1,
			"time",
			0.25f
		}));
		card.GetActor().GetHealthObject().Enlarge(this.m_gemScale);
	}

	// Token: 0x060051BE RID: 20926 RVA: 0x001ADBE8 File Offset: 0x001ABDE8
	public override void NotifyOfCardTooltipDisplayHide(Card card)
	{
		if (this.attackHelpPanel != null)
		{
			if (card != null)
			{
				GemObject attackObject = card.GetActor().GetAttackObject();
				SceneUtils.SetLayer(attackObject.gameObject, GameLayer.Default);
				attackObject.Shrink();
			}
			UnityEngine.Object.Destroy(this.attackHelpPanel.gameObject);
			this.m_isShowingAttackHelpPanel = false;
		}
		if (this.healthHelpPanel != null)
		{
			if (card != null)
			{
				GemObject healthObject = card.GetActor().GetHealthObject();
				SceneUtils.SetLayer(healthObject.gameObject, GameLayer.Default);
				healthObject.Shrink();
			}
			UnityEngine.Object.Destroy(this.healthHelpPanel.gameObject);
		}
	}

	// Token: 0x060051BF RID: 20927 RVA: 0x001ADC84 File Offset: 0x001ABE84
	private void ManaLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (this.m_isShowingAttackHelpPanel)
		{
			return;
		}
		GameObject costTextObject = ((Card)callbackData).GetActor().GetCostTextObject();
		if (costTextObject == null)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		this.costLabel = go;
		go.transform.parent = costTextObject.transform;
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localPosition = new Vector3(-0.017f, 0.3512533f, 0f);
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_COST");
	}

	// Token: 0x060051C0 RID: 20928 RVA: 0x001ADD48 File Offset: 0x001ABF48
	private void AttackLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (this.m_isShowingAttackHelpPanel)
		{
			return;
		}
		GameObject attackTextObject = ((Card)callbackData).GetActor().GetAttackTextObject();
		if (attackTextObject == null)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		this.attackLabel = go;
		go.transform.parent = attackTextObject.transform;
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localPosition = new Vector3(-0.2f, -0.3039344f, 0f);
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_ATTACK");
	}

	// Token: 0x060051C1 RID: 20929 RVA: 0x001ADE0C File Offset: 0x001AC00C
	private void HealthLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (this.m_isShowingAttackHelpPanel)
		{
			return;
		}
		GameObject healthTextObject = ((Card)callbackData).GetActor().GetHealthTextObject();
		if (healthTextObject == null)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		this.healthLabel = go;
		go.transform.parent = healthTextObject.transform;
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localPosition = new Vector3(0.21f, -0.31f, 0f);
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_HEALTH");
	}

	// Token: 0x060051C2 RID: 20930 RVA: 0x001ADED0 File Offset: 0x001AC0D0
	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (this.ShouldShowArrowOnCardInHand(mousedOverEntity))
		{
			NotificationManager.Get().DestroyAllArrows();
		}
		if (mousedOverEntity.GetZone() == TAG_ZONE.HAND)
		{
			this.mousedOverCard = mousedOverEntity.GetCard();
			IAssetLoader assetLoader = AssetLoader.Get();
			assetLoader.InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", new PrefabCallback<GameObject>(this.ManaLabelLoadedCallback), this.mousedOverCard, AssetLoadingOptions.IgnorePrefabPosition);
			assetLoader.InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", new PrefabCallback<GameObject>(this.AttackLabelLoadedCallback), this.mousedOverCard, AssetLoadingOptions.IgnorePrefabPosition);
			assetLoader.InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", new PrefabCallback<GameObject>(this.HealthLabelLoadedCallback), this.mousedOverCard, AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	// Token: 0x060051C3 RID: 20931 RVA: 0x001ADF75 File Offset: 0x001AC175
	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (this.ShouldShowArrowOnCardInHand(mousedOffEntity))
		{
			Gameplay.Get().StartCoroutine(this.ShowArrowInSeconds(0.5f));
		}
		this.NukeNumberLabels();
	}

	// Token: 0x060051C4 RID: 20932 RVA: 0x001ADF9C File Offset: 0x001AC19C
	private void NukeNumberLabels()
	{
		this.mousedOverCard = null;
		if (this.costLabel != null)
		{
			UnityEngine.Object.Destroy(this.costLabel);
		}
		if (this.attackLabel != null)
		{
			UnityEngine.Object.Destroy(this.attackLabel);
		}
		if (this.healthLabel != null)
		{
			UnityEngine.Object.Destroy(this.healthLabel);
		}
	}

	// Token: 0x060051C5 RID: 20933 RVA: 0x001ADFFC File Offset: 0x001AC1FC
	private bool ShouldShowArrowOnCardInHand(Entity entity)
	{
		if (entity.GetZone() != TAG_ZONE.HAND)
		{
			return false;
		}
		int tag = base.GetTag(GAME_TAG.TURN);
		return tag == 2 || (tag == 4 && GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards().Count == 0);
	}

	// Token: 0x060051C6 RID: 20934 RVA: 0x001AE045 File Offset: 0x001AC245
	private IEnumerator ShowArrowInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		if (cards.Count == 0)
		{
			yield break;
		}
		Card cardInHand = cards[0];
		while (iTween.Count(cardInHand.gameObject) > 0)
		{
			yield return null;
		}
		if (cardInHand.IsMousedOver())
		{
			yield break;
		}
		if (InputManager.Get().GetHeldCard() == cardInHand)
		{
			yield break;
		}
		this.ShowHandBouncingArrow();
		yield break;
	}

	// Token: 0x060051C7 RID: 20935 RVA: 0x001AE05C File Offset: 0x001AC25C
	private void ShowHandBouncingArrow()
	{
		if (this.handBounceArrow != null)
		{
			return;
		}
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		if (cards.Count == 0)
		{
			return;
		}
		Card card = cards[0];
		Vector3 position = card.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x - 0.08f, position.y + 0.2f, position.z + 1.2f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2f);
		}
		this.handBounceArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, 0f, 0f));
		this.handBounceArrow.transform.parent = card.transform;
	}

	// Token: 0x060051C8 RID: 20936 RVA: 0x001AE140 File Offset: 0x001AC340
	private void ShowHandFadeArrow()
	{
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		if (cards.Count == 0)
		{
			return;
		}
		this.ShowFadeArrow(cards[0], null);
	}

	// Token: 0x060051C9 RID: 20937 RVA: 0x001AE17C File Offset: 0x001AC37C
	private void ShowFadeArrow(Card card, Card target = null)
	{
		if (this.handFadeArrow != null)
		{
			return;
		}
		Vector3 position = card.transform.position;
		Vector3 rotation = new Vector3(0f, 180f, 0f);
		Vector3 vector2;
		if (target != null)
		{
			Vector3 vector = target.transform.position - position;
			vector2 = new Vector3(position.x, position.y + 0.47f, position.z + 0.27f);
			float num = Vector3.Angle(target.transform.position - vector2, new Vector3(0f, 0f, -1f));
			rotation = new Vector3(0f, -Mathf.Sign(vector.x) * num, 0f);
			vector2 += 0.3f * vector;
		}
		else
		{
			vector2 = new Vector3(position.x, position.y + 0.047f, position.z + 0.95f);
		}
		this.handFadeArrow = NotificationManager.Get().CreateFadeArrow(vector2, rotation);
		if (target != null)
		{
			this.handFadeArrow.transform.localScale = 1.25f * Vector3.one;
		}
		this.handFadeArrow.transform.parent = card.transform;
	}

	// Token: 0x060051CA RID: 20938 RVA: 0x001AE2D2 File Offset: 0x001AC4D2
	private void HideFadeArrow()
	{
		if (this.handFadeArrow != null)
		{
			NotificationManager.Get().DestroyNotification(this.handFadeArrow, 0f);
			this.handFadeArrow = null;
		}
	}

	// Token: 0x060051CB RID: 20939 RVA: 0x001AE2FE File Offset: 0x001AC4FE
	private void OnPhoneHandShown(object userData)
	{
		if (this.handBounceArrow != null)
		{
			NotificationManager.Get().DestroyNotification(this.handBounceArrow, 0f);
			this.handBounceArrow = null;
		}
		this.ShowHandFadeArrow();
	}

	// Token: 0x060051CC RID: 20940 RVA: 0x001AE330 File Offset: 0x001AC530
	private void OnPhoneHandHidden(object userData)
	{
		this.HideFadeArrow();
		this.ShowHandBouncingArrow();
	}

	// Token: 0x060051CD RID: 20941 RVA: 0x001AE33E File Offset: 0x001AC53E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
		{
			List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetDeckZone().GetCards();
			this.firstMurlocCard = cards[cards.Count - 1];
			this.firstRaptorCard = cards[cards.Count - 2];
			GameState.Get().SetBusy(true);
			Board.Get().FindCollider("DragPlane").enabled = false;
			yield return new WaitForSeconds(1.25f);
			base.ShowTutorialDialog("TUTORIAL01_HELP_14", "TUTORIAL01_HELP_15", "TUTORIAL01_HELP_16", Vector2.zero, false).SetWantedText(GameStrings.Get("MISSION_PRE_TUTORIAL_WANTED"));
			break;
		}
		case 2:
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().RegisterPhoneHandShownListener(new InputManager.PhoneHandShownCallback(this.OnPhoneHandShown));
				InputManager.Get().RegisterPhoneHandHiddenListener(new InputManager.PhoneHandHiddenCallback(this.OnPhoneHandHidden));
			}
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_02_02.prefab:cccdcb509085a974d922ac1d545d9bb6", "TUTORIAL01_JAINA_02", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			List<Card> cards2 = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
			if (base.GetTag(GAME_TAG.TURN) == 2 && cards2.Count == 1 && InputManager.Get().GetHeldCard() == null && !cards2[0].IsMousedOver())
			{
				Gameplay.Get().StartCoroutine(this.ShowArrowInSeconds(0f));
			}
			break;
		}
		case 3:
			if (UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().RemovePhoneHandShownListener(new InputManager.PhoneHandShownCallback(this.OnPhoneHandShown));
				InputManager.Get().RemovePhoneHandHiddenListener(new InputManager.PhoneHandHiddenCallback(this.OnPhoneHandHidden));
			}
			break;
		case 4:
			actor.SetActorState(ActorStateType.CARD_IDLE);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_06_06.prefab:ffe0ebdca06ca1d4c84cc28e4a1ed7cf", "TUTORIAL01_JAINA_06", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			if (this.firstMurlocCard != null)
			{
				this.firstMurlocCard.GetActor().ToggleForceIdle(true);
				this.firstMurlocCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			}
			break;
		case 6:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_17_13.prefab:9b257c86e7c7f9045a2b819d35876aca", "TUTORIAL01_JAINA_17", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			break;
		case 8:
			this.m_jainaSpeaking = true;
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_18_14.prefab:fedcdecb3346ec745b6fb4204f7dd4e0", "TUTORIAL01_JAINA_18", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			this.m_jainaSpeaking = false;
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(this.FlashMinionUntilAttackBegins(this.firstRaptorCard));
			break;
		case 10:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_19_15.prefab:659652a121ac01941a40c64c1c151f87", "TUTORIAL01_JAINA_19", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			break;
		}
		yield break;
	}

	// Token: 0x060051CE RID: 20942 RVA: 0x001AE354 File Offset: 0x001AC554
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor hoggerActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		AudioSource prevLine;
		Vector3 middleSpot;
		Notification innkeeperLine;
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(true);
			HistoryManager.Get().DisableHistory();
			goto IL_F31;
		case 2:
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_01_01.prefab:883391234efbde84eb99a16abd164d9d", "TUTORIAL01_JAINA_01", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			Gameplay.Get().SetGameStateBusy(false, 2.2f);
			goto IL_F31;
		case 3:
		{
			int turn = GameState.Get().GetTurn();
			yield return new WaitForSeconds(2f);
			if (turn != GameState.Get().GetTurn())
			{
				yield break;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_03_03.prefab:4921407046d90bb44b2bfcf3984ffd47", "TUTORIAL01_JAINA_03", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			if (base.GetTag(GAME_TAG.TURN) == 2 && !EndTurnButton.Get().IsInWaitingState())
			{
				this.ShowEndTurnBouncingArrow();
				goto IL_F31;
			}
			goto IL_F31;
		}
		case 4:
		{
			GameState.Get().SetBusy(true);
			prevLine = base.GetPreloadedSound("VO_TUTORIAL_01_JAINA_03_03.prefab:4921407046d90bb44b2bfcf3984ffd47");
			while (SoundManager.Get().IsPlaying(prevLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_20_16.prefab:7980d02c581e4174991a8066e5785666", "TUTORIAL01_JAINA_20", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_06_06_ALT.prefab:6c9ef3c501462474ab59a37b967cab6f", "TUTORIAL01_HOGGER_07", Notification.SpeechBubbleDirection.TopRight, hoggerActor, 1f, true, false, 3f, 0f));
			Vector3 position = jainaActor.transform.position;
			Vector3 position2 = new Vector3(position.x + 3.3f, position.y + 0.5f, position.z - 0.85f);
			Notification.PopUpArrowDirection direction = Notification.PopUpArrowDirection.Left;
			if (UniversalInputManager.UsePhoneUI)
			{
				position2 = new Vector3(position.x + 3f, position.y + 0.5f, position.z + 0.85f);
				direction = Notification.PopUpArrowDirection.LeftDown;
			}
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_01"), true, NotificationManager.PopupTextType.BASIC);
			notification.ShowPopUpArrow(direction);
			NotificationManager.Get().DestroyNotification(notification, 5f);
			Gameplay.Get().SetGameStateBusy(false, 5.2f);
			goto IL_F31;
		}
		case 5:
			this.HideFadeArrow();
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_05_05.prefab:982193e53ab81f04ba562de4b32dd39c", "TUTORIAL01_JAINA_05", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			goto IL_F31;
		case 6:
			goto IL_F31;
		case 7:
			NotificationManager.Get().DestroyAllPopUps();
			yield return new WaitForSeconds(1.7f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_07_07.prefab:a8bf811494e94d742a3910fac9da906f", "TUTORIAL01_JAINA_07", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
			if (this.firstRaptorCard != null)
			{
				Vector3 position3 = this.firstRaptorCard.transform.position;
				Notification notification2;
				if (this.firstMurlocCard != null && this.firstRaptorCard.GetZonePosition() > this.firstMurlocCard.GetZonePosition())
				{
					Vector3 position4 = new Vector3(position3.x + 3f, position3.y, position3.z);
					notification2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position4, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_04"), true, NotificationManager.PopupTextType.BASIC);
					notification2.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
				}
				else
				{
					Vector3 position4 = new Vector3(position3.x - 3f, position3.y, position3.z);
					notification2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position4, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_04"), true, NotificationManager.PopupTextType.BASIC);
					notification2.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				}
				NotificationManager.Get().DestroyNotification(notification2, 4f);
			}
			yield return new WaitForSeconds(4f);
			if (GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards().Count > 1 && !GameState.Get().IsInTargetMode())
			{
				this.ShowAttackWithYourMinionPopup();
			}
			if (base.GetTag(GAME_TAG.TURN) == 4 && EndTurnButton.Get().IsInNMPState())
			{
				yield return new WaitForSeconds(1f);
				this.ShowEndTurnBouncingArrow();
				goto IL_F31;
			}
			goto IL_F31;
		case 8:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_03_03.prefab:4ef21f71824b97842b33d8ebccb37ed2", "TUTORIAL01_HOGGER_05", Notification.SpeechBubbleDirection.TopRight, hoggerActor, 1f, true, false, 3f, 0f));
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_21_17.prefab:c1524bd0ef92bb845b5dab48cbd017f9", "TUTORIAL01_JAINA_21", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			goto IL_F31;
		case 9:
		case 10:
		case 11:
		case 16:
		case 17:
		case 18:
		case 19:
		case 21:
			break;
		case 12:
			yield return new WaitForSeconds(1f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_15_11.prefab:a644986d34ab8964582c6221cde54d45", "TUTORIAL01_JAINA_15", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			goto IL_F31;
		case 13:
			while (this.m_jainaSpeaking)
			{
				yield return null;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_16_12.prefab:e6b4ab6fc1f11634e88f013ce5351e46", "TUTORIAL01_JAINA_16", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			goto IL_F31;
		case 14:
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_08_08_ALT.prefab:19ddb4ddaa4aee2468b17bae25da9419", "TUTORIAL01_HOGGER_08", Notification.SpeechBubbleDirection.TopRight, hoggerActor, 1f, true, false, 3f, 0f));
			Vector3 position5 = hoggerActor.transform.position;
			Vector3 position6 = new Vector3(position5.x + 3.3f, position5.y + 0.5f, position5.z - 1f);
			if (UniversalInputManager.UsePhoneUI)
			{
				position6 = new Vector3(position5.x + 3f, position5.y + 0.5f, position5.z - 0.75f);
			}
			Notification.PopUpArrowDirection direction2 = Notification.PopUpArrowDirection.Left;
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position6, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_09"), true, NotificationManager.PopupTextType.BASIC);
			notification.ShowPopUpArrow(direction2);
			NotificationManager.Get().DestroyNotification(notification, 5f);
			if (base.GetTag(GAME_TAG.TURN) == 6 && EndTurnButton.Get().IsInNMPState())
			{
				yield return new WaitForSeconds(9f);
				this.ShowEndTurnBouncingArrow();
				goto IL_F31;
			}
			goto IL_F31;
		}
		case 15:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_JAINA_02_55_ALT2.prefab:d049e67ad6c16db4da2c04be7a02a1ae", "", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			goto IL_F31;
		case 20:
		{
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_JAINA_10_09.prefab:5bf553d532aca174083f48bf407b2b11", "TUTORIAL01_JAINA_10", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			yield return new WaitForSeconds(1.5f);
			GameState.Get().SetBusy(false);
			List<Card> cards = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards();
			cards[cards.Count - 1].GetActor().GetAttackObject().Jiggle();
			goto IL_F31;
		}
		case 22:
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_09_09_ALT.prefab:70c4d2941509856448660f89d6c72b2b", "TUTORIAL01_HOGGER_02", Notification.SpeechBubbleDirection.TopRight, hoggerActor, 1f, true, false, 3f, 0f));
			Gameplay.Get().SetGameStateBusy(false, 2f);
			goto IL_F31;
		default:
			if (missionEvent == 55)
			{
				base.GetGameOptions().SetBooleanOption(GameEntityOption.DISABLE_TOOLTIPS, false);
				Board.Get().FindCollider("DragPlane").enabled = true;
				while (!this.announcerIsFinishedYapping)
				{
					yield return null;
				}
				if (!SoundUtils.CanDetectVolume())
				{
					Notification battlebegin = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 84.8f), GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_05"), "", 15f, null, false);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_05.prefab:635b33010e4704a42a87c7625b5b5ada", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
					NotificationManager.Get().DestroyNotification(battlebegin, 0f);
					battlebegin = null;
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_05.prefab:635b33010e4704a42a87c7625b5b5ada", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
				}
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_01_01.prefab:5833f4aeb72110741a2c9bc3a92f9bc8", "TUTORIAL01_HOGGER_01", Notification.SpeechBubbleDirection.TopRight, hoggerActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
				yield return new WaitForSeconds(4f);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_04_04.prefab:3e16e42edb324e2469a25363ffd013a6", "TUTORIAL01_HOGGER_06", Notification.SpeechBubbleDirection.TopRight, hoggerActor, 1f, true, false, 3f, 0f));
				goto IL_F31;
			}
			if (missionEvent == 66)
			{
				Vector3 position7 = new Vector3(136f, NotificationManager.DEPTH, 131f);
				middleSpot = new Vector3(136f, NotificationManager.DEPTH, 80f);
				innkeeperLine = null;
				if (!SoundUtils.CanDetectVolume())
				{
					innkeeperLine = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, position7, GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_01"), "", 15f, null, false);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_01.prefab:79419083a1b828341be6d208491a88f8", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
					NotificationManager.Get().DestroyNotification(innkeeperLine, 0f);
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_01.prefab:79419083a1b828341be6d208491a88f8", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
				}
				yield return new WaitForSeconds(0.5f);
				if (!SoundUtils.CanDetectVolume())
				{
					innkeeperLine = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, middleSpot, GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_02"), "", 15f, null, false);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_02.prefab:d6b08fa7e06a51c4abd80eea2ea30a41", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
					NotificationManager.Get().DestroyNotification(innkeeperLine, 0f);
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_02.prefab:d6b08fa7e06a51c4abd80eea2ea30a41", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
				}
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_HOGGER_02_02.prefab:7f321b26431a4974a82deefc368adf63", "TUTORIAL01_HOGGER_04", Notification.SpeechBubbleDirection.TopRight, hoggerActor, 1f, true, false, 3f, 0f));
				if (UniversalInputManager.UsePhoneUI)
				{
					Gameplay.Get().AddGamePlayNameBannerPhone();
				}
				if (!SoundUtils.CanDetectVolume())
				{
					innkeeperLine = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_TUTORIAL_01_ANNOUNCER_03"), "", 15f, null, false);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_03.prefab:f47d0faf9067b3341bb9adb38f90be5b", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
					NotificationManager.Get().DestroyNotification(innkeeperLine, 0f);
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_03.prefab:f47d0faf9067b3341bb9adb38f90be5b", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
				}
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_01_ANNOUNCER_04.prefab:e6fb72da1414d454f9d96a51c7009a82", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
				this.announcerIsFinishedYapping = true;
				goto IL_F31;
			}
			break;
		}
		Debug.LogWarning("WARNING - Mission fired an event that we are not listening for.");
		IL_F31:
		prevLine = null;
		middleSpot = default(Vector3);
		innkeeperLine = null;
		yield break;
	}

	// Token: 0x060051CF RID: 20943 RVA: 0x001AE36C File Offset: 0x001AC56C
	private void ShowAttackWithYourMinionPopup()
	{
		if (this.attackWithYourMinion != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.attackWithYourMinion);
		}
		if (this.firstAttackFinished)
		{
			return;
		}
		if (this.firstMurlocCard == null)
		{
			return;
		}
		this.firstMurlocCard.GetActor().ToggleForceIdle(false);
		this.firstMurlocCard.GetActor().SetActorState(ActorStateType.CARD_PLAYABLE);
		Vector3 position = this.firstMurlocCard.transform.position;
		if (this.firstMurlocCard.GetEntity().IsExhausted())
		{
			return;
		}
		if (!(this.firstMurlocCard.GetZone() is ZonePlay))
		{
			return;
		}
		if (this.firstRaptorCard != null && this.firstMurlocCard.GetZonePosition() < this.firstRaptorCard.GetZonePosition())
		{
			Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
			this.attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), this.textToShowForAttackTip, true, NotificationManager.PopupTextType.BASIC);
			this.attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		}
		else
		{
			Vector3 position2 = new Vector3(position.x + 3f, position.y, position.z);
			this.attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), this.textToShowForAttackTip, true, NotificationManager.PopupTextType.BASIC);
			this.attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
		}
		Card firstCard = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetFirstCard();
		this.ShowFadeArrow(this.firstMurlocCard, firstCard);
		Gameplay.Get().StartCoroutine(this.SwapHelpTextAndFlashMinion());
	}

	// Token: 0x060051D0 RID: 20944 RVA: 0x001AE4F7 File Offset: 0x001AC6F7
	private IEnumerator SwapHelpTextAndFlashMinion()
	{
		if (this.firstMurlocCard == null)
		{
			yield break;
		}
		Gameplay.Get().StartCoroutine(this.BeginFlashingMinionLoop(this.firstMurlocCard));
		yield return new WaitForSeconds(4f);
		if (this.textToShowForAttackTip == GameStrings.Get("TUTORIAL01_HELP_10"))
		{
			yield break;
		}
		if (this.firstMurlocCard.GetEntity().IsExhausted())
		{
			yield break;
		}
		if (this.firstMurlocCard.GetActor().GetActorStateType() == ActorStateType.CARD_IDLE || this.firstMurlocCard.GetActor().GetActorStateType() == ActorStateType.CARD_MOUSE_OVER)
		{
			yield break;
		}
		if (!(this.firstMurlocCard.GetZone() is ZonePlay))
		{
			yield break;
		}
		if (this.firstAttackFinished)
		{
			yield break;
		}
		Vector3 position = this.firstMurlocCard.transform.position;
		this.textToShowForAttackTip = GameStrings.Get("TUTORIAL01_HELP_10");
		if (this.attackWithYourMinion != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.attackWithYourMinion);
		}
		if (this.firstRaptorCard != null && this.firstMurlocCard.GetZonePosition() < this.firstRaptorCard.GetZonePosition())
		{
			Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
			this.attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), this.textToShowForAttackTip, true, NotificationManager.PopupTextType.BASIC);
			this.attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		}
		else
		{
			Vector3 position2 = new Vector3(position.x + 3f, position.y, position.z);
			this.attackWithYourMinion = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), this.textToShowForAttackTip, true, NotificationManager.PopupTextType.BASIC);
			this.attackWithYourMinion.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
		}
		yield break;
	}

	// Token: 0x060051D1 RID: 20945 RVA: 0x001AE506 File Offset: 0x001AC706
	private IEnumerator FlashMinionUntilAttackBegins(Card minionToFlash)
	{
		yield return new WaitForSeconds(8f);
		Gameplay.Get().StartCoroutine(this.BeginFlashingMinionLoop(minionToFlash));
		yield break;
	}

	// Token: 0x060051D2 RID: 20946 RVA: 0x001AE51C File Offset: 0x001AC71C
	private IEnumerator BeginFlashingMinionLoop(Card minionToFlash)
	{
		if (minionToFlash == null)
		{
			yield break;
		}
		if (minionToFlash.GetEntity().IsExhausted())
		{
			yield break;
		}
		if (minionToFlash.GetActor().GetActorStateType() == ActorStateType.CARD_IDLE || minionToFlash.GetActor().GetActorStateType() == ActorStateType.CARD_MOUSE_OVER)
		{
			yield break;
		}
		minionToFlash.GetActorSpell(SpellType.WIGGLE, true).Activate();
		yield return new WaitForSeconds(1.5f);
		Gameplay.Get().StartCoroutine(this.BeginFlashingMinionLoop(minionToFlash));
		yield break;
	}

	// Token: 0x060051D3 RID: 20947 RVA: 0x001AE534 File Offset: 0x001AC734
	private void ShowEndTurnBouncingArrow()
	{
		if (EndTurnButton.Get().IsInWaitingState())
		{
			return;
		}
		Vector3 position = EndTurnButton.Get().transform.position;
		Vector3 position2 = new Vector3(position.x - 2f, position.y, position.z);
		NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, -90f, 0f));
	}

	// Token: 0x060051D4 RID: 20948 RVA: 0x001AE5A0 File Offset: 0x001AC7A0
	private void ShowDontFireballYourselfPopup(Vector3 origin)
	{
		if (this.noFireballPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.noFireballPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		this.noFireballPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"), true, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.noFireballPopup, 2.5f);
	}

	// Token: 0x060051D5 RID: 20949 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x060051D6 RID: 20950 RVA: 0x001AE622 File Offset: 0x001AC822
	public override bool DoAlternateMulliganIntro()
	{
		AssetLoader.Get().InstantiatePrefab("GameOpen_Pack.prefab:fca6ae094e9a74644b00fc9029f304c3", new PrefabCallback<GameObject>(this.PackLoadedCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		return true;
	}

	// Token: 0x060051D7 RID: 20951 RVA: 0x001AE648 File Offset: 0x001AC848
	private void PackLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.Misc_Tutorial01);
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		this.startingPack = go;
		Transform transform = SceneUtils.FindChildBySubstring(this.startingPack, "Hero_Dummy").transform;
		heroCard.transform.parent = transform;
		heroCard.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		heroCard.transform.localPosition = new Vector3(0f, 0f, 0f);
		SceneUtils.SetLayer(heroCard.GetActor().GetRootObject(), GameLayer.IgnoreFullScreenEffects);
		Transform transform2 = SceneUtils.FindChildBySubstring(this.startingPack, "HeroEnemy_Dummy").transform;
		heroCard2.transform.parent = transform2;
		heroCard2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		heroCard2.transform.localPosition = new Vector3(0f, 0f, 0f);
		heroCard.SetDoNotSort(true);
		Transform transform3 = Board.Get().FindBone("Tutorial1HeroStart");
		go.transform.position = transform3.position;
		heroCard.GetActor().GetHealthObject().Hide();
		heroCard2.GetActor().GetHealthObject().Hide();
		heroCard2.GetActor().Hide();
		heroCard.GetActor().Hide();
		SceneMgr.Get().NotifySceneLoaded();
		Gameplay.Get().StartCoroutine(this.UpdatePresence());
		Gameplay.Get().StartCoroutine(this.ShowPackOpeningArrow(transform3.position));
	}

	// Token: 0x060051D8 RID: 20952 RVA: 0x001AE7EC File Offset: 0x001AC9EC
	private IEnumerator UpdatePresence()
	{
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		yield break;
	}

	// Token: 0x060051D9 RID: 20953 RVA: 0x001AE7F4 File Offset: 0x001AC9F4
	private IEnumerator ShowPackOpeningArrow(Vector3 packSpot)
	{
		yield return new WaitForSeconds(4f);
		if (this.packOpened)
		{
			yield break;
		}
		Vector3 position = new Vector3(packSpot.x + 4.014574f, packSpot.y, packSpot.z + 0.2307634f);
		this.freeCardsPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_18"), true, NotificationManager.PopupTextType.BASIC);
		this.freeCardsPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
		yield break;
	}

	// Token: 0x060051DA RID: 20954 RVA: 0x001AE80A File Offset: 0x001ACA0A
	public override void NotifyOfGamePackOpened()
	{
		this.packOpened = true;
		if (this.freeCardsPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.freeCardsPopup);
		}
	}

	// Token: 0x060051DB RID: 20955 RVA: 0x001AE834 File Offset: 0x001ACA34
	public override void NotifyOfCustomIntroFinished()
	{
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		heroCard.SetDoNotSort(false);
		heroCard2.GetActor().TurnOnCollider();
		heroCard.GetActor().TurnOnCollider();
		heroCard.transform.parent = null;
		heroCard2.transform.parent = null;
		SceneUtils.SetLayer(heroCard.GetActor().GetRootObject(), GameLayer.CardRaycast);
		Gameplay.Get().StartCoroutine(this.ContinueFinishingCustomIntro());
	}

	// Token: 0x060051DC RID: 20956 RVA: 0x001AE8B6 File Offset: 0x001ACAB6
	private IEnumerator ContinueFinishingCustomIntro()
	{
		yield return new WaitForSeconds(3f);
		UnityEngine.Object.Destroy(this.startingPack);
		GameState.Get().SetBusy(false);
		MulliganManager.Get().SkipMulligan();
		yield break;
	}

	// Token: 0x060051DD RID: 20957 RVA: 0x001AE8C5 File Offset: 0x001ACAC5
	public override bool ShouldShowBigCard()
	{
		return base.GetTag(GAME_TAG.TURN) > 8;
	}

	// Token: 0x060051DE RID: 20958 RVA: 0x001AE8D2 File Offset: 0x001ACAD2
	public override void NotifyOfDefeatCoinAnimation()
	{
		base.PlaySound("VO_TUTORIAL_01_JAINA_13_10.prefab:b13670e36c248e141837c4eb0645a000", 1f, true, false);
	}

	// Token: 0x060051DF RID: 20959 RVA: 0x001AE8E8 File Offset: 0x001ACAE8
	public override List<RewardData> GetCustomRewards()
	{
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_023", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}

	// Token: 0x04004930 RID: 18736
	private static Map<GameEntityOption, bool> s_booleanOptions = Tutorial_01.InitBooleanOptions();

	// Token: 0x04004931 RID: 18737
	private static Map<GameEntityOption, string> s_stringOptions = Tutorial_01.InitStringOptions();

	// Token: 0x04004932 RID: 18738
	private Notification endTurnNotifier;

	// Token: 0x04004933 RID: 18739
	private Notification handBounceArrow;

	// Token: 0x04004934 RID: 18740
	private Notification handFadeArrow;

	// Token: 0x04004935 RID: 18741
	private Notification noFireballPopup;

	// Token: 0x04004936 RID: 18742
	private Notification attackWithYourMinion;

	// Token: 0x04004937 RID: 18743
	private Notification crushThisGnoll;

	// Token: 0x04004938 RID: 18744
	private Notification freeCardsPopup;

	// Token: 0x04004939 RID: 18745
	private TooltipPanel attackHelpPanel;

	// Token: 0x0400493A RID: 18746
	private TooltipPanel healthHelpPanel;

	// Token: 0x0400493B RID: 18747
	private Card mousedOverCard;

	// Token: 0x0400493C RID: 18748
	private GameObject costLabel;

	// Token: 0x0400493D RID: 18749
	private GameObject attackLabel;

	// Token: 0x0400493E RID: 18750
	private GameObject healthLabel;

	// Token: 0x0400493F RID: 18751
	private Card firstMurlocCard;

	// Token: 0x04004940 RID: 18752
	private Card firstRaptorCard;

	// Token: 0x04004941 RID: 18753
	private int numTimesTextSwapStarted;

	// Token: 0x04004942 RID: 18754
	private string textToShowForAttackTip = GameStrings.Get("TUTORIAL01_HELP_02");

	// Token: 0x04004943 RID: 18755
	private GameObject startingPack;

	// Token: 0x04004944 RID: 18756
	private bool packOpened;

	// Token: 0x04004945 RID: 18757
	private bool announcerIsFinishedYapping;

	// Token: 0x04004946 RID: 18758
	private bool firstAttackFinished;

	// Token: 0x04004947 RID: 18759
	private bool m_jainaSpeaking;

	// Token: 0x04004948 RID: 18760
	private bool m_isShowingAttackHelpPanel;

	// Token: 0x04004949 RID: 18761
	private PlatformDependentValue<float> m_gemScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 1.75f,
		Phone = 1.2f
	};

	// Token: 0x0400494A RID: 18762
	private PlatformDependentValue<Vector3> m_attackTooltipPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-2.15f, 0f, -0.62f),
		Phone = new Vector3(-3.5f, 0f, -0.62f)
	};

	// Token: 0x0400494B RID: 18763
	private PlatformDependentValue<Vector3> m_healthTooltipPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(2.05f, 0f, -0.62f),
		Phone = new Vector3(3.25f, 0f, -0.62f)
	};

	// Token: 0x0400494C RID: 18764
	private PlatformDependentValue<Vector3> m_heroHealthTooltipPosition = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(2.4f, 0.3f, -0.8f),
		Phone = new Vector3(3.5f, 0.3f, 0.6f)
	};
}
