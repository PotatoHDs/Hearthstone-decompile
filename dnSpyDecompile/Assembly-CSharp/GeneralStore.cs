using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x020006F2 RID: 1778
[CustomEditClass]
public class GeneralStore : Store
{
	// Token: 0x060062F4 RID: 25332 RVA: 0x00203B94 File Offset: 0x00201D94
	protected override void Start()
	{
		base.Start();
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.SuccessfulPurchaseAckEvent));
		SoundManager.Get().Load("gold_spend_plate_flip_on.prefab:e490542c7405fce45a46c7b9aad5aeab");
		SoundManager.Get().Load("gold_spend_plate_flip_off.prefab:8e19277d18c845547af53064aade9b2c");
		this.UpdateModeButtons(this.m_currentMode);
		foreach (GeneralStore.ModeObjects modeObjects in this.m_modeObjects)
		{
			if (modeObjects.m_content != null)
			{
				modeObjects.m_content.gameObject.SetActive(modeObjects.m_mode == this.m_currentMode);
			}
		}
		this.m_shakePane = base.GetComponent<ShakePane>();
		if (this.m_offClicker != null)
		{
			this.m_offClicker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClosePressed));
		}
	}

	// Token: 0x060062F5 RID: 25333 RVA: 0x00203C94 File Offset: 0x00201E94
	protected override void Awake()
	{
		GeneralStore.s_instance = this;
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_scaleMode = (TransformUtil.IsExtraWideAspectRatio() ? CanvasScaleMode.HEIGHT : CanvasScaleMode.WIDTH);
		}
		base.Awake();
		this.m_buyWithMoneyButton.SetText(GameStrings.Get("GLUE_STORE_BUY_TEXT"));
		this.m_buyWithGoldButton.SetText(GameStrings.Get("GLUE_STORE_BUY_TEXT"));
		foreach (GeneralStore.ModeObjects modeObjects in this.m_modeObjects)
		{
			GeneralStoreContent content = modeObjects.m_content;
			UIBButton button = modeObjects.m_button;
			GeneralStoreMode mode = modeObjects.m_mode;
			GeneralStorePane pane = modeObjects.m_pane;
			if (content != null)
			{
				content.SetParentStore(this);
				content.RegisterCurrentBundleChanged(delegate(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
				{
					this.UpdateCostAndButtonState(goldBundle, moneyBundle);
				});
			}
			if (button != null)
			{
				button.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
				{
					this.SetMode(mode);
				});
			}
			if (pane != null)
			{
				pane.transform.localPosition = this.m_paneSwapOutOffset;
				this.m_paneStartPositions[mode] = pane.m_paneContainer.transform.localPosition;
			}
		}
		if (this.m_defaultPane != null)
		{
			this.m_defaultPane.transform.localPosition = this.m_paneSwapOutOffset;
		}
	}

	// Token: 0x060062F6 RID: 25334 RVA: 0x00203E0C File Offset: 0x0020200C
	protected override void OnDestroy()
	{
		base.OnDestroy();
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.SuccessfulPurchaseAckEvent));
		AssetHandle.SafeDispose<Texture>(ref this.m_accentTexture);
		this.m_mainPanel = null;
		GeneralStore.s_instance = null;
	}

	// Token: 0x060062F7 RID: 25335 RVA: 0x00203E42 File Offset: 0x00202042
	public GeneralStoreContent GetCurrentContent()
	{
		return this.GetContent(this.m_currentMode);
	}

	// Token: 0x060062F8 RID: 25336 RVA: 0x00203E50 File Offset: 0x00202050
	public GeneralStorePane GetCurrentPane()
	{
		return this.GetPane(this.m_currentMode);
	}

	// Token: 0x060062F9 RID: 25337 RVA: 0x00203E60 File Offset: 0x00202060
	public GeneralStoreContent GetContent(GeneralStoreMode mode)
	{
		GeneralStore.ModeObjects modeObjects = this.m_modeObjects.Find((GeneralStore.ModeObjects obj) => obj.m_mode == mode);
		if (modeObjects == null)
		{
			return null;
		}
		return modeObjects.m_content;
	}

	// Token: 0x060062FA RID: 25338 RVA: 0x00203EA0 File Offset: 0x002020A0
	public GeneralStorePane GetPane(GeneralStoreMode mode)
	{
		GeneralStore.ModeObjects modeObjects = this.m_modeObjects.Find((GeneralStore.ModeObjects obj) => obj.m_mode == mode);
		if (modeObjects != null && modeObjects.m_pane != null)
		{
			return modeObjects.m_pane;
		}
		return this.m_defaultPane;
	}

	// Token: 0x060062FB RID: 25339 RVA: 0x00203EF0 File Offset: 0x002020F0
	public void Close(bool closeWithAnimation)
	{
		if (!this.m_shown)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(GeneralStorePhoneCover.OnNavigateBack));
		}
		Navigation.Pop();
		this.CloseImpl(closeWithAnimation);
	}

	// Token: 0x060062FC RID: 25340 RVA: 0x00203F25 File Offset: 0x00202125
	public override void Close()
	{
		if (!this.m_shown)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(GeneralStorePhoneCover.OnNavigateBack));
		}
		if (this.m_settingNewModeCount == 0)
		{
			Navigation.GoBack();
		}
	}

	// Token: 0x060062FD RID: 25341 RVA: 0x00203F5C File Offset: 0x0020215C
	public void SetMode(GeneralStoreMode mode)
	{
		base.StartCoroutine(this.AnimateAndUpdateStoreMode(this.m_currentMode, mode));
	}

	// Token: 0x060062FE RID: 25342 RVA: 0x00203F72 File Offset: 0x00202172
	public GeneralStoreMode GetMode()
	{
		return this.m_currentMode;
	}

	// Token: 0x060062FF RID: 25343 RVA: 0x00203F7A File Offset: 0x0020217A
	public void ShakeStore(float xRotationAmount, float shakeTime, float delay = 0f, float translateAmount = 0f)
	{
		if (this.m_shakePane == null || GeneralStoreMode.CARDS != this.m_currentMode)
		{
			return;
		}
		this.m_shakePane.Shake(xRotationAmount, shakeTime, delay, translateAmount);
	}

	// Token: 0x06006300 RID: 25344 RVA: 0x00203FA4 File Offset: 0x002021A4
	public void SetDescription(string title, string desc, string warning = null)
	{
		this.HideChooseDescription();
		if (this.m_productDetailsContainer != null)
		{
			this.m_productDetailsContainer.gameObject.SetActive(true);
		}
		bool flag = StoreManager.Get().IsKoreanCustomer();
		bool flag2 = !string.IsNullOrEmpty(title);
		this.m_productDetailsHeadlineText.gameObject.SetActive(flag2);
		this.m_productDetailsText.gameObject.SetActive(!flag);
		this.m_koreanProductDetailsText.gameObject.SetActive(flag);
		this.m_koreanWarningText.gameObject.SetActive(flag);
		this.m_productDetailsText.Height = (flag2 ? this.m_productDetailsRegularHeight : this.m_productDetailsExtendedHeight);
		this.m_productDetailsHeadlineText.Text = title;
		this.m_koreanProductDetailsText.Text = desc;
		this.m_productDetailsText.Text = desc;
		this.m_koreanProductDetailsText.Height = (flag2 ? this.m_koreanProductDetailsRegularHeight : this.m_koreanProductDetailsExtendedHeight);
		this.m_koreanWarningText.Text = ((warning == null) ? string.Empty : warning);
		if (this.m_productDetailsContainer != null)
		{
			this.m_productDetailsContainer.UpdateSlices();
		}
	}

	// Token: 0x06006301 RID: 25345 RVA: 0x002040BD File Offset: 0x002022BD
	public void HideDescription()
	{
		if (this.m_productDetailsContainer != null)
		{
			this.m_productDetailsContainer.gameObject.SetActive(false);
		}
	}

	// Token: 0x06006302 RID: 25346 RVA: 0x002040E0 File Offset: 0x002022E0
	public void SetChooseDescription(string chooseText)
	{
		this.HideDescription();
		this.SetAccentTexture(null);
		if (this.m_chooseArrowContainer != null)
		{
			this.m_chooseArrowContainer.SetActive(true);
		}
		if (this.m_chooseArrowText != null)
		{
			this.m_chooseArrowText.Text = chooseText;
		}
	}

	// Token: 0x06006303 RID: 25347 RVA: 0x0020412E File Offset: 0x0020232E
	public void HideChooseDescription()
	{
		if (this.m_chooseArrowContainer != null)
		{
			this.m_chooseArrowContainer.SetActive(false);
		}
	}

	// Token: 0x06006304 RID: 25348 RVA: 0x0020414C File Offset: 0x0020234C
	public void SetAccentTexture(AssetHandle<Texture> texture)
	{
		if (this.m_accentIcon != null)
		{
			bool flag = texture != null;
			this.m_accentIcon.gameObject.SetActive(flag);
			if (flag)
			{
				AssetHandle.Set<Texture>(ref this.m_accentTexture, texture);
				this.m_accentIcon.GetMaterial().mainTexture = this.m_accentTexture;
			}
		}
	}

	// Token: 0x06006305 RID: 25349 RVA: 0x002041A7 File Offset: 0x002023A7
	public void HideAccentTexture()
	{
		if (this.m_accentIcon != null)
		{
			this.m_accentIcon.gameObject.SetActive(false);
		}
	}

	// Token: 0x06006306 RID: 25350 RVA: 0x002041C8 File Offset: 0x002023C8
	public void HidePacksPane(bool hide)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			base.StartCoroutine(this.AnimateHideStorePane(hide));
			return;
		}
		if (hide)
		{
			base.StartCoroutine(this.AnimateAndUpdateStoreMode(GeneralStoreMode.CARDS, GeneralStoreMode.NONE));
			return;
		}
		base.StartCoroutine(this.AnimateAndUpdateStoreMode(GeneralStoreMode.NONE, GeneralStoreMode.CARDS));
	}

	// Token: 0x06006307 RID: 25351 RVA: 0x00204207 File Offset: 0x00202407
	public void ResumePreviousMusicPlaylist()
	{
		if (this.m_prevPlaylist != MusicPlaylistType.Invalid)
		{
			MusicManager.Get().StartPlaylist(this.m_prevPlaylist);
		}
	}

	// Token: 0x06006308 RID: 25352 RVA: 0x00204222 File Offset: 0x00202422
	public void RegisterModeChangedListener(GeneralStore.ModeChanged dlg)
	{
		this.m_modeChangedListeners.Add(dlg);
	}

	// Token: 0x06006309 RID: 25353 RVA: 0x00204230 File Offset: 0x00202430
	public void UnregisterModeChangedListener(GeneralStore.ModeChanged dlg)
	{
		this.m_modeChangedListeners.Remove(dlg);
	}

	// Token: 0x0600630A RID: 25354 RVA: 0x0020423F File Offset: 0x0020243F
	public static GeneralStore Get()
	{
		return GeneralStore.s_instance;
	}

	// Token: 0x0600630B RID: 25355 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool IsReady()
	{
		return true;
	}

	// Token: 0x0600630C RID: 25356 RVA: 0x00204248 File Offset: 0x00202448
	public override void OnMoneySpent()
	{
		GeneralStoreContent currentContent = this.GetCurrentContent();
		if (currentContent != null)
		{
			currentContent.Refresh();
		}
		GeneralStorePane currentPane = this.GetCurrentPane();
		if (currentPane != null)
		{
			currentPane.Refresh();
		}
	}

	// Token: 0x0600630D RID: 25357 RVA: 0x00204281 File Offset: 0x00202481
	public override void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		this.UpdateGoldButtonState(balance);
	}

	// Token: 0x0600630E RID: 25358 RVA: 0x0020428C File Offset: 0x0020248C
	protected override void ShowImpl(bool isTotallyFake)
	{
		if (this.m_shown)
		{
			return;
		}
		if (this.m_root != null && UniversalInputManager.UsePhoneUI && TransformUtil.IsExtraWideAspectRatio())
		{
			this.m_root.transform.localScale = Vector3.one * this.m_rootScaleExtraWideAspectRatio;
			TransformUtil.SetLocalPosX(this.m_root.transform, this.m_rootXPosExtraWideAspectRatio);
			TransformUtil.SetLocalPosZ(this.m_root.transform, this.m_rootZPosExtraWideAspectRatio);
		}
		this.m_prevPlaylist = MusicManager.Get().GetCurrentPlaylist();
		foreach (GeneralStore.ModeObjects modeObjects in this.m_modeObjects)
		{
			GeneralStoreContent content = modeObjects.m_content;
			GeneralStorePane pane = modeObjects.m_pane;
			if (content != null)
			{
				content.StoreShown(this.GetCurrentContent() == content);
			}
			if (pane != null)
			{
				pane.StoreShown(this.GetCurrentPane() == pane);
			}
		}
		ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.GENERAL_STORE);
		FriendChallengeMgr.Get().OnStoreOpened();
		this.PreRender();
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.STORE
		});
		if (!UniversalInputManager.UsePhoneUI && !Options.Get().GetBool(Option.HAS_SEEN_GOLD_QTY_INSTRUCTION, false) && UserAttentionManager.CanShowAttentionGrabber("GeneralStore.Show:" + Option.HAS_SEEN_GOLD_QTY_INSTRUCTION) && NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>().GetTotal() >= (long)GeneralStore.MIN_GOLD_FOR_CHANGE_QTY_TOOLTIP)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_attentionCategory = UserAttentionBlocker.NONE;
			popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_GOLD_QTY_CHANGE_HEADER");
			if (UniversalInputManager.Get().IsTouchMode())
			{
				popupInfo.m_text = GameStrings.Get("GLUE_STORE_GOLD_QTY_CHANGE_DESC_TOUCH");
			}
			else
			{
				popupInfo.m_text = GameStrings.Get("GLUE_STORE_GOLD_QTY_CHANGE_DESC");
			}
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Options.Get().SetBool(Option.HAS_SEEN_GOLD_QTY_INSTRUCTION, true);
		}
		this.UpdateGoldButtonState();
		this.m_shown = true;
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.EnableFullScreenEffects(true);
		SoundManager.Get().LoadAndPlay("Store_window_expand.prefab:050bf879a3e32d04999427c262baaf09", base.gameObject);
		base.DoShowAnimation(delegate()
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				base.transform.localPosition = this.m_showPosition;
				this.m_root.transform.localPosition = Vector3.zero;
			}
			base.FireOpenedEvent();
		});
	}

	// Token: 0x0600630F RID: 25359 RVA: 0x002044F4 File Offset: 0x002026F4
	private void OnClosePressed(UIEvent e)
	{
		if (!this.m_shown)
		{
			return;
		}
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.CurrentState == HearthstoneCheckout.State.InProgress)
		{
			return;
		}
		if (StoreManager.Get().CanTapOutConfirmationUI())
		{
			this.Close();
		}
	}

	// Token: 0x06006310 RID: 25360 RVA: 0x00204530 File Offset: 0x00202730
	protected override void BuyWithMoney(UIEvent e)
	{
		GeneralStoreContent currentContent = this.GetCurrentContent();
		Network.Bundle bundle = currentContent.GetCurrentMoneyBundle();
		if (bundle == null)
		{
			Debug.LogWarning("GeneralStore.OnBuyWithMoneyPressed(): SelectedBundle is null");
			return;
		}
		GeneralStoreContent.BuyEvent successBuyCB = delegate()
		{
			this.FireBuyWithMoneyEvent(bundle, 1);
		};
		currentContent.TryBuyWithMoney(bundle, successBuyCB, null);
	}

	// Token: 0x06006311 RID: 25361 RVA: 0x0020458C File Offset: 0x0020278C
	protected override void BuyWithGold(UIEvent e)
	{
		GeneralStoreContent currentContent = this.GetCurrentContent();
		NoGTAPPTransactionData bundle = currentContent.GetCurrentGoldBundle();
		if (bundle == null)
		{
			Debug.LogWarning("GeneralStore.OnBuyWithGoldPressed(): SelectedGoldPrice is null");
			return;
		}
		GeneralStoreContent.BuyEvent buyEvent = delegate()
		{
			this.FireBuyWithGoldEventNoGTAPP(bundle);
		};
		currentContent.TryBuyWithGold(buyEvent, buyEvent);
	}

	// Token: 0x06006312 RID: 25362 RVA: 0x002045E4 File Offset: 0x002027E4
	private void UpdateMoneyButtonState()
	{
		Store.BuyButtonState moneyButtonState = Store.BuyButtonState.ENABLED;
		if (!StoreManager.Get().IsOpen(true))
		{
			moneyButtonState = Store.BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsBattlePayFeatureEnabled())
		{
			moneyButtonState = Store.BuyButtonState.DISABLED_FEATURE;
		}
		else
		{
			Network.Bundle currentMoneyBundle = this.GetCurrentContent().GetCurrentMoneyBundle();
			if (currentMoneyBundle == null || StoreManager.Get().IsProductAlreadyOwned(currentMoneyBundle))
			{
				moneyButtonState = Store.BuyButtonState.DISABLED_OWNED;
			}
		}
		base.SetMoneyButtonState(moneyButtonState);
	}

	// Token: 0x06006313 RID: 25363 RVA: 0x0020463C File Offset: 0x0020283C
	private void UpdateGoldButtonState(NetCache.NetCacheGoldBalance balance)
	{
		Store.BuyButtonState goldButtonState = Store.BuyButtonState.ENABLED;
		GeneralStoreContent currentContent = this.GetCurrentContent();
		if (currentContent == null)
		{
			return;
		}
		NoGTAPPTransactionData currentGoldBundle = currentContent.GetCurrentGoldBundle();
		long num;
		if (currentGoldBundle == null)
		{
			goldButtonState = Store.BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsOpen(true))
		{
			goldButtonState = Store.BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsBuyWithGoldFeatureEnabled())
		{
			goldButtonState = Store.BuyButtonState.DISABLED_FEATURE;
		}
		else if (balance == null)
		{
			goldButtonState = Store.BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().GetGoldCostNoGTAPP(currentGoldBundle, out num))
		{
			goldButtonState = Store.BuyButtonState.DISABLED_NO_TOOLTIP;
		}
		else if (balance.GetTotal() < num)
		{
			goldButtonState = Store.BuyButtonState.DISABLED_NOT_ENOUGH_GOLD;
		}
		base.SetGoldButtonState(goldButtonState);
	}

	// Token: 0x06006314 RID: 25364 RVA: 0x002046B8 File Offset: 0x002028B8
	private void UpdateGoldButtonState()
	{
		NetCache.NetCacheGoldBalance netObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
		this.UpdateGoldButtonState(netObject);
	}

	// Token: 0x06006315 RID: 25365 RVA: 0x002046D8 File Offset: 0x002028D8
	private void UpdateCostDisplay(NoGTAPPTransactionData goldBundle)
	{
		long num;
		if (goldBundle == null || !StoreManager.Get().GetGoldCostNoGTAPP(goldBundle, out num))
		{
			this.UpdateCostDisplay(GeneralStore.BuyPanelState.BUY_GOLD, string.Empty);
			return;
		}
		this.UpdateCostDisplay(GeneralStore.BuyPanelState.BUY_GOLD, num.ToString());
	}

	// Token: 0x06006316 RID: 25366 RVA: 0x00204712 File Offset: 0x00202912
	private void UpdateCostDisplay(Network.Bundle moneyBundle)
	{
		if (moneyBundle == null)
		{
			this.UpdateCostDisplay(GeneralStore.BuyPanelState.BUY_MONEY, GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_COST_OWNED_TEXT"));
			return;
		}
		this.UpdateCostDisplay(GeneralStore.BuyPanelState.BUY_MONEY, StoreManager.Get().FormatCostBundle(moneyBundle));
	}

	// Token: 0x06006317 RID: 25367 RVA: 0x0020473C File Offset: 0x0020293C
	private void UpdateCostDisplay(GeneralStore.BuyPanelState newPanelState, string costText = "")
	{
		if (newPanelState == GeneralStore.BuyPanelState.BUY_MONEY)
		{
			this.m_moneyCostText.Text = costText;
			this.m_moneyCostText.UpdateNow(false);
		}
		else if (newPanelState == GeneralStore.BuyPanelState.BUY_GOLD)
		{
			this.m_goldCostText.Text = costText;
			this.m_goldCostText.UpdateNow(false);
		}
		this.ShowBuyPanel(newPanelState);
	}

	// Token: 0x06006318 RID: 25368 RVA: 0x0020478C File Offset: 0x0020298C
	private void ShowBuyPanel(GeneralStore.BuyPanelState setPanelState)
	{
		if (this.m_buyPanelState == setPanelState)
		{
			return;
		}
		GameObject buyPanelObject = this.GetBuyPanelObject(setPanelState);
		GameObject oldPanelObject = this.GetBuyPanelObject(this.m_buyPanelState);
		this.m_buyPanelState = setPanelState;
		iTween.StopByName(buyPanelObject, "rotation");
		iTween.StopByName(oldPanelObject, "rotation");
		buyPanelObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		oldPanelObject.transform.localEulerAngles = Vector3.zero;
		buyPanelObject.SetActive(true);
		iTween.RotateTo(oldPanelObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 180f),
			"isLocal",
			true,
			"time",
			GeneralStore.FLIP_BUY_PANEL_ANIM_TIME,
			"easeType",
			iTween.EaseType.linear,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				oldPanelObject.SetActive(false);
			}),
			"name",
			"rotation"
		}));
		iTween.RotateTo(buyPanelObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 0f),
			"isLocal",
			true,
			"time",
			GeneralStore.FLIP_BUY_PANEL_ANIM_TIME,
			"easeType",
			iTween.EaseType.linear,
			"name",
			"rotation"
		}));
		SoundManager.Get().LoadAndPlay((setPanelState == GeneralStore.BuyPanelState.BUY_GOLD) ? "gold_spend_plate_flip_on.prefab:e490542c7405fce45a46c7b9aad5aeab" : "gold_spend_plate_flip_off.prefab:8e19277d18c845547af53064aade9b2c");
	}

	// Token: 0x06006319 RID: 25369 RVA: 0x0020495B File Offset: 0x00202B5B
	private GameObject GetBuyPanelObject(GeneralStore.BuyPanelState buyPanelState)
	{
		if (buyPanelState == GeneralStore.BuyPanelState.BUY_GOLD)
		{
			return this.m_buyGoldPanel;
		}
		if (buyPanelState != GeneralStore.BuyPanelState.BUY_MONEY)
		{
			return this.m_buyEmptyPanel;
		}
		return this.m_buyMoneyPanel;
	}

	// Token: 0x0600631A RID: 25370 RVA: 0x0020497C File Offset: 0x00202B7C
	public void RefreshContent()
	{
		GeneralStoreContent currentContent = this.GetCurrentContent();
		GeneralStorePane currentPane = this.GetCurrentPane();
		StoreManager storeManager = StoreManager.Get();
		base.BlockInterface(storeManager.TransactionInProgress() || storeManager.IsPromptShowing);
		if (currentContent != null)
		{
			currentContent.Refresh();
		}
		if (currentPane != null)
		{
			currentPane.Refresh();
		}
	}

	// Token: 0x0600631B RID: 25371 RVA: 0x002049D4 File Offset: 0x00202BD4
	protected override void Hide(bool animate)
	{
		if (this.m_settingNewModeCount > 0)
		{
			return;
		}
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		FriendChallengeMgr.Get().OnStoreClosed();
		GeneralStoreContent content = this.GetContent(GeneralStoreMode.CARDS);
		if (content != null && this.GetCurrentContent() == content)
		{
			GeneralStorePacksContent generalStorePacksContent = content as GeneralStorePacksContent;
			if (generalStorePacksContent.m_quantityPrompt != null)
			{
				generalStorePacksContent.m_quantityPrompt.Hide();
			}
		}
		this.ResumePreviousMusicPlaylist();
		base.DoHideAnimation(!animate, new UIBPopup.OnAnimationComplete(this.OnHidden));
	}

	// Token: 0x0600631C RID: 25372 RVA: 0x00204A64 File Offset: 0x00202C64
	protected override void OnHidden()
	{
		this.m_shown = false;
		foreach (GeneralStore.ModeObjects modeObjects in this.m_modeObjects)
		{
			GeneralStorePane pane = modeObjects.m_pane;
			GeneralStoreContent content = modeObjects.m_content;
			if (pane != null)
			{
				pane.StoreHidden(this.GetCurrentPane() == pane);
			}
			if (content != null)
			{
				content.StoreHidden(this.GetCurrentContent() == content);
			}
		}
	}

	// Token: 0x0600631D RID: 25373 RVA: 0x00204AFC File Offset: 0x00202CFC
	private void PreRender()
	{
		if (!this.m_staticTextResized)
		{
			this.m_buyWithMoneyButton.m_ButtonText.UpdateNow(false);
			this.m_buyWithGoldButton.m_ButtonText.UpdateNow(false);
			this.m_staticTextResized = true;
		}
		this.RefreshContent();
	}

	// Token: 0x0600631E RID: 25374 RVA: 0x00204B38 File Offset: 0x00202D38
	private bool IsContentFlipClockwise(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < GeneralStore.s_ContentOrdering.Length; i++)
		{
			if (GeneralStore.s_ContentOrdering[i] == oldMode)
			{
				num = i;
			}
			else if (GeneralStore.s_ContentOrdering[i] == newMode)
			{
				num2 = i;
			}
		}
		return num < num2;
	}

	// Token: 0x0600631F RID: 25375 RVA: 0x00204B79 File Offset: 0x00202D79
	private IEnumerator AnimateAndUpdateStoreMode(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		this.ResetAnimations();
		while (this.m_settingNewModeCount > 0)
		{
			yield return null;
		}
		this.FireModeChangedEvent(oldMode, newMode);
		if (this.m_currentMode == newMode)
		{
			yield break;
		}
		this.m_settingNewModeCount++;
		if (this.m_modeButtonBlocker != null)
		{
			this.m_modeButtonBlocker.SetActive(true);
		}
		this.UpdateModeButtons(newMode);
		this.m_currentMode = newMode;
		base.StartCoroutine(this.AnimateAndUpdateStorePane(oldMode, newMode));
		GeneralStoreContent prevContent = this.GetContent(oldMode);
		GeneralStoreContent nextContent = this.GetContent(newMode);
		if (prevContent != null)
		{
			prevContent.SetContentActive(false);
			prevContent.PreStoreFlipOut();
			while (!prevContent.AnimateExitStart())
			{
				yield return null;
			}
			while (!prevContent.AnimateExitEnd())
			{
				yield return null;
			}
		}
		bool flag = this.IsContentFlipClockwise(oldMode, newMode);
		Vector3 newPanelRotation;
		Vector3 localPosition;
		Vector3 localEulerAngles;
		Vector3 localEulerAngles2;
		this.GetContentPositionIndex(flag, out localPosition, out localEulerAngles, out localEulerAngles2, out newPanelRotation);
		if (nextContent != null)
		{
			nextContent.transform.localPosition = localPosition;
			nextContent.transform.localEulerAngles = localEulerAngles;
			nextContent.gameObject.SetActive(true);
		}
		iTween.StopByName(this.m_mainPanel, "PANEL_ROTATION");
		this.m_mainPanel.transform.localEulerAngles = localEulerAngles2;
		bool rotationDone = false;
		float flipAnimTime = this.m_contentFlipAnimationTime;
		float num = flag ? 1f : -1f;
		this.ShakeStore(10f * num, 1.5f, flipAnimTime * 0.3f, 0f);
		if (!string.IsNullOrEmpty(this.m_contentFlipSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_contentFlipSound);
		}
		Action<object> action = delegate(object o)
		{
			this.m_mainPanel.transform.localEulerAngles = newPanelRotation;
			rotationDone = true;
			if (prevContent != null)
			{
				prevContent.gameObject.SetActive(false);
			}
		};
		if (flipAnimTime > 0f)
		{
			iTween.RotateBy(this.m_mainPanel, iTween.Hash(new object[]
			{
				"name",
				"PANEL_ROTATION",
				"amount",
				GeneralStore.MAIN_PANEL_ANGLE_TO_ROTATE * num,
				"time",
				flipAnimTime,
				"easetype",
				this.m_contentFlipEaseType,
				"oncomplete",
				action
			}));
		}
		else
		{
			action(null);
		}
		if (nextContent != null)
		{
			nextContent.PreStoreFlipIn();
		}
		while (!rotationDone)
		{
			yield return null;
		}
		if (nextContent != null)
		{
			this.UpdateCostAndButtonState(nextContent.GetCurrentGoldBundle(), nextContent.GetCurrentMoneyBundle());
			while (!nextContent.AnimateEntranceStart())
			{
				yield return null;
			}
			while (!nextContent.AnimateEntranceEnd())
			{
				yield return null;
			}
			nextContent.SetContentActive(true);
			nextContent.PostStoreFlipIn(flipAnimTime > 0f);
		}
		if (prevContent != null)
		{
			prevContent.PostStoreFlipOut();
		}
		this.m_settingNewModeCount--;
		this.RefreshContent();
		while (this.m_settingNewModeCount > 0)
		{
			yield return null;
		}
		int currencyChangedVersion = StoreManager.Get().GetCurrencyChangedVersion();
		if (currencyChangedVersion != 0 && currencyChangedVersion != Options.Get().GetInt(Option.LATEST_SEEN_CURRENCY_CHANGED_VERSION) && UserAttentionManager.CanShowAttentionGrabber("GeneralStore.AnimateAndUpdateStoreMode:" + Option.LATEST_SEEN_CURRENCY_CHANGED_VERSION))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_attentionCategory = UserAttentionBlocker.NONE;
			popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_CURRENCY_CHANGED_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_STORE_CURRENCY_CHANGED_DESC");
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Options.Get().SetInt(Option.LATEST_SEEN_CURRENCY_CHANGED_VERSION, currencyChangedVersion);
		}
		if (this.m_modeButtonBlocker != null)
		{
			this.m_modeButtonBlocker.SetActive(false);
		}
		if (newMode == GeneralStoreMode.NONE)
		{
			this.ResumePreviousMusicPlaylist();
		}
		yield break;
	}

	// Token: 0x06006320 RID: 25376 RVA: 0x00204B96 File Offset: 0x00202D96
	private IEnumerator AnimateHideStorePane(bool hide)
	{
		GeneralStore.<>c__DisplayClass96_0 CS$<>8__locals1 = new GeneralStore.<>c__DisplayClass96_0();
		GeneralStorePane nextPane;
		if (hide)
		{
			CS$<>8__locals1.prevPane = this.GetPane(GeneralStoreMode.CARDS);
			nextPane = this.m_defaultPane;
		}
		else
		{
			CS$<>8__locals1.prevPane = this.m_defaultPane;
			nextPane = this.GetPane(GeneralStoreMode.CARDS);
		}
		this.m_settingNewModeCount++;
		if (CS$<>8__locals1.prevPane != null)
		{
			CS$<>8__locals1.prevPane.PrePaneSwappedOut();
			while (!CS$<>8__locals1.prevPane.AnimateExitStart())
			{
				yield return null;
			}
			while (!CS$<>8__locals1.prevPane.AnimateExitEnd())
			{
				yield return null;
			}
			CS$<>8__locals1.prevPane.PostPaneSwappedOut();
		}
		if (this.m_paneSwapAnimationTime > 0f)
		{
			GeneralStore.<>c__DisplayClass96_1 CS$<>8__locals2 = new GeneralStore.<>c__DisplayClass96_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			if (!string.IsNullOrEmpty(this.m_contentFlipSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_contentFlipSound);
			}
			CS$<>8__locals2.swapCount = 0;
			float num = 0f;
			if (CS$<>8__locals2.CS$<>8__locals1.prevPane != null)
			{
				int swapCount = CS$<>8__locals2.swapCount + 1;
				CS$<>8__locals2.swapCount = swapCount;
				CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject.SetActive(true);
				CS$<>8__locals2.CS$<>8__locals1.prevPane.transform.localPosition = Vector3.zero;
				iTween.MoveTo(CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject, iTween.Hash(new object[]
				{
					"position",
					this.m_paneSwapOutOffset,
					"islocal",
					true,
					"time",
					this.m_paneSwapAnimationTime,
					"easetype",
					iTween.EaseType.linear,
					"oncomplete",
					new Action<object>(delegate(object o)
					{
						if (CS$<>8__locals2.CS$<>8__locals1.prevPane != null && CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject != null)
						{
							CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject.SetActive(false);
						}
						int swapCount2 = CS$<>8__locals2.swapCount - 1;
						CS$<>8__locals2.swapCount = swapCount2;
					})
				}));
				num = this.m_paneSwapAnimationTime;
			}
			if (nextPane != null)
			{
				int swapCount = CS$<>8__locals2.swapCount + 1;
				CS$<>8__locals2.swapCount = swapCount;
				nextPane.gameObject.SetActive(true);
				nextPane.transform.localPosition = this.m_paneSwapInOffset;
				iTween.MoveTo(nextPane.gameObject, iTween.Hash(new object[]
				{
					"position",
					Vector3.zero,
					"islocal",
					true,
					"time",
					this.m_paneSwapAnimationTime,
					"delay",
					num,
					"oncomplete",
					new Action<object>(delegate(object o)
					{
						int swapCount2 = CS$<>8__locals2.swapCount - 1;
						CS$<>8__locals2.swapCount = swapCount2;
					})
				}));
			}
			while (CS$<>8__locals2.swapCount > 0)
			{
				yield return null;
			}
			CS$<>8__locals2 = null;
		}
		else
		{
			CS$<>8__locals1.prevPane.transform.localPosition = this.m_paneSwapOutOffset;
			nextPane.transform.localPosition = Vector3.zero;
			CS$<>8__locals1.prevPane.gameObject.SetActive(false);
			nextPane.gameObject.SetActive(true);
		}
		this.m_settingNewModeCount--;
		yield break;
	}

	// Token: 0x06006321 RID: 25377 RVA: 0x00204BAC File Offset: 0x00202DAC
	private IEnumerator AnimateAndUpdateStorePane(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		GeneralStore.<>c__DisplayClass97_0 CS$<>8__locals1 = new GeneralStore.<>c__DisplayClass97_0();
		CS$<>8__locals1.prevPane = this.GetPane(oldMode);
		GeneralStorePane nextPane = this.GetPane(newMode);
		if (oldMode == newMode)
		{
			yield break;
		}
		this.m_settingNewModeCount++;
		if (this.m_paneScrollbar != null)
		{
			this.m_paneScrollbar.SaveScroll("STORE_MODE_" + oldMode);
			this.m_paneScrollbar.m_ScrollObject = null;
		}
		if (this.m_paneScrollbar != null && nextPane != null && nextPane.m_paneContainer != null)
		{
			Vector3 position;
			this.m_paneStartPositions.TryGetValue(newMode, out position);
			this.m_paneScrollbar.m_ScrollObject = nextPane.m_paneContainer;
			this.m_paneScrollbar.ResetScrollStartPosition(position);
			this.m_paneScrollbar.LoadScroll("STORE_MODE_" + newMode, true);
			this.m_paneScrollbar.EnableIfNeeded();
		}
		if (CS$<>8__locals1.prevPane != null)
		{
			CS$<>8__locals1.prevPane.PrePaneSwappedOut();
			while (!CS$<>8__locals1.prevPane.AnimateExitStart())
			{
				yield return null;
			}
			while (!CS$<>8__locals1.prevPane.AnimateExitEnd())
			{
				yield return null;
			}
			CS$<>8__locals1.prevPane.PostPaneSwappedOut();
		}
		if (this.m_paneSwapAnimationTime > 0f)
		{
			GeneralStore.<>c__DisplayClass97_1 CS$<>8__locals2 = new GeneralStore.<>c__DisplayClass97_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			CS$<>8__locals2.swapCount = 0;
			float num = 0f;
			if (CS$<>8__locals2.CS$<>8__locals1.prevPane != null)
			{
				int swapCount = CS$<>8__locals2.swapCount + 1;
				CS$<>8__locals2.swapCount = swapCount;
				CS$<>8__locals2.CS$<>8__locals1.prevPane.transform.localPosition = Vector3.zero;
				CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject.SetActive(true);
				iTween.MoveTo(CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject, iTween.Hash(new object[]
				{
					"position",
					this.m_paneSwapOutOffset,
					"islocal",
					true,
					"time",
					this.m_paneSwapAnimationTime,
					"easetype",
					iTween.EaseType.linear,
					"oncomplete",
					new Action<object>(delegate(object o)
					{
						if (CS$<>8__locals2.CS$<>8__locals1.prevPane != null && CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject != null)
						{
							CS$<>8__locals2.CS$<>8__locals1.prevPane.gameObject.SetActive(false);
						}
						int swapCount2 = CS$<>8__locals2.swapCount - 1;
						CS$<>8__locals2.swapCount = swapCount2;
					})
				}));
				num = this.m_paneSwapAnimationTime;
			}
			if (nextPane != null)
			{
				int swapCount = CS$<>8__locals2.swapCount + 1;
				CS$<>8__locals2.swapCount = swapCount;
				nextPane.transform.localPosition = this.m_paneSwapInOffset;
				nextPane.gameObject.SetActive(true);
				iTween.MoveTo(nextPane.gameObject, iTween.Hash(new object[]
				{
					"position",
					Vector3.zero,
					"islocal",
					true,
					"time",
					this.m_paneSwapAnimationTime,
					"delay",
					num,
					"oncomplete",
					new Action<object>(delegate(object o)
					{
						int swapCount2 = CS$<>8__locals2.swapCount - 1;
						CS$<>8__locals2.swapCount = swapCount2;
					})
				}));
			}
			while (CS$<>8__locals2.swapCount > 0)
			{
				yield return null;
			}
			CS$<>8__locals2 = null;
		}
		else
		{
			CS$<>8__locals1.prevPane.transform.localPosition = this.m_paneSwapOutOffset;
			nextPane.transform.localPosition = Vector3.zero;
			CS$<>8__locals1.prevPane.gameObject.SetActive(false);
			nextPane.gameObject.SetActive(true);
		}
		if (nextPane != null)
		{
			nextPane.PrePaneSwappedIn();
			while (!nextPane.AnimateEntranceStart())
			{
				yield return null;
			}
			while (!nextPane.AnimateEntranceEnd())
			{
				yield return null;
			}
			nextPane.PostPaneSwappedIn();
		}
		this.m_settingNewModeCount--;
		yield break;
	}

	// Token: 0x06006322 RID: 25378 RVA: 0x00204BC9 File Offset: 0x00202DC9
	private void ResetAnimations()
	{
		if (this.m_shakePane != null)
		{
			this.m_shakePane.Reset();
		}
	}

	// Token: 0x06006323 RID: 25379 RVA: 0x00204BE4 File Offset: 0x00202DE4
	private void UpdateModeButtons(GeneralStoreMode mode)
	{
		foreach (GeneralStore.ModeObjects modeObjects in this.m_modeObjects)
		{
			if (!(modeObjects.m_button == null))
			{
				UIBHighlight component = modeObjects.m_button.GetComponent<UIBHighlight>();
				if (!(component == null))
				{
					if (mode == modeObjects.m_mode)
					{
						component.SelectNoSound();
					}
					else
					{
						component.Reset();
					}
				}
			}
		}
	}

	// Token: 0x06006324 RID: 25380 RVA: 0x00204C6C File Offset: 0x00202E6C
	private void GetContentPositionIndex(bool clockwise, out Vector3 contentPosition, out Vector3 contentRotation, out Vector3 lastPanelRotation, out Vector3 newPanelRotation)
	{
		lastPanelRotation = GeneralStore.s_MainPanelTriangularRotations[this.m_currentContentPositionIdx];
		if (clockwise)
		{
			this.m_currentContentPositionIdx = (this.m_currentContentPositionIdx + 1) % GeneralStore.s_ContentTriangularPositions.Length;
		}
		else
		{
			this.m_currentContentPositionIdx--;
			if (this.m_currentContentPositionIdx < 0)
			{
				this.m_currentContentPositionIdx = GeneralStore.s_ContentTriangularPositions.Length - 1;
			}
		}
		contentPosition = GeneralStore.s_ContentTriangularPositions[this.m_currentContentPositionIdx];
		contentRotation = GeneralStore.s_ContentTriangularRotations[this.m_currentContentPositionIdx];
		newPanelRotation = GeneralStore.s_MainPanelTriangularRotations[this.m_currentContentPositionIdx];
	}

	// Token: 0x06006325 RID: 25381 RVA: 0x00204D14 File Offset: 0x00202F14
	private void SuccessfulPurchaseAckEvent(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (this.IsShown() && SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE)
		{
			this.Close();
			return;
		}
		this.RefreshContent();
	}

	// Token: 0x06006326 RID: 25382 RVA: 0x00204D3C File Offset: 0x00202F3C
	private void UpdateCostAndButtonState(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
		if (moneyBundle != null && !StoreManager.Get().IsProductAlreadyOwned(moneyBundle))
		{
			this.UpdateCostDisplay(moneyBundle);
			this.UpdateMoneyButtonState();
			return;
		}
		if (goldBundle != null)
		{
			this.UpdateCostDisplay(goldBundle);
			this.UpdateGoldButtonState();
			return;
		}
		GeneralStoreContent currentContent = this.GetCurrentContent();
		if (currentContent == null || currentContent.IsPurchaseDisabled())
		{
			this.UpdateCostDisplay(GeneralStore.BuyPanelState.DISABLED, "");
			return;
		}
		this.UpdateCostDisplay(GeneralStore.BuyPanelState.BUY_MONEY, currentContent.GetMoneyDisplayOwnedText());
		this.UpdateMoneyButtonState();
	}

	// Token: 0x06006327 RID: 25383 RVA: 0x00204DB0 File Offset: 0x00202FB0
	private void FireModeChangedEvent(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		GeneralStore.ModeChanged[] array = this.m_modeChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](oldMode, newMode);
		}
	}

	// Token: 0x06006328 RID: 25384 RVA: 0x00204DE1 File Offset: 0x00202FE1
	private bool OnNavigateBack()
	{
		this.CloseImpl(true);
		return true;
	}

	// Token: 0x06006329 RID: 25385 RVA: 0x00204DEC File Offset: 0x00202FEC
	private void CloseImpl(bool closeWithAnimation)
	{
		if (this.m_settingNewModeCount > 0)
		{
			return;
		}
		PresenceMgr.Get().SetPrevStatus();
		this.Hide(closeWithAnimation);
		SoundManager.Get().LoadAndPlay("Store_window_shrink.prefab:b68247126e211224e8a904142d2a9895", base.gameObject);
		base.EnableFullScreenEffects(false);
		base.FireExitEvent(false);
	}

	// Token: 0x0600632A RID: 25386 RVA: 0x00204E40 File Offset: 0x00203040
	protected override string GetOwnedTooltipString()
	{
		switch (this.m_currentMode)
		{
		case GeneralStoreMode.CARDS:
			return GameStrings.Get("GLUE_STORE_PACK_BUTTON_TEXT_PURCHASED");
		case GeneralStoreMode.ADVENTURE:
			return GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_TEXT_PURCHASED");
		case GeneralStoreMode.HEROES:
			return GameStrings.Get("GLUE_STORE_HERO_BUTTON_TEXT_PURCHASED");
		default:
			return string.Empty;
		}
	}

	// Token: 0x0400521D RID: 21021
	[CustomEditField(Sections = "General Store")]
	public GameObject m_mainPanel;

	// Token: 0x0400521E RID: 21022
	[CustomEditField(Sections = "General Store")]
	public GameObject m_buyGoldPanel;

	// Token: 0x0400521F RID: 21023
	[CustomEditField(Sections = "General Store")]
	public GameObject m_buyMoneyPanel;

	// Token: 0x04005220 RID: 21024
	[CustomEditField(Sections = "General Store")]
	public GameObject m_buyEmptyPanel;

	// Token: 0x04005221 RID: 21025
	[CustomEditField(Sections = "General Store", ListTable = true)]
	public List<GeneralStore.ModeObjects> m_modeObjects = new List<GeneralStore.ModeObjects>();

	// Token: 0x04005222 RID: 21026
	[CustomEditField(Sections = "General Store")]
	public MeshRenderer m_accentIcon;

	// Token: 0x04005223 RID: 21027
	[CustomEditField(Sections = "General Store/Mode Buttons")]
	public GameObject m_modeButtonBlocker;

	// Token: 0x04005224 RID: 21028
	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_moneyCostText;

	// Token: 0x04005225 RID: 21029
	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_goldCostText;

	// Token: 0x04005226 RID: 21030
	[CustomEditField(Sections = "General Store/Text")]
	public MultiSliceElement m_productDetailsContainer;

	// Token: 0x04005227 RID: 21031
	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_productDetailsHeadlineText;

	// Token: 0x04005228 RID: 21032
	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_productDetailsText;

	// Token: 0x04005229 RID: 21033
	[CustomEditField(Sections = "General Store/Text")]
	public float m_productDetailsRegularHeight = 13f;

	// Token: 0x0400522A RID: 21034
	[CustomEditField(Sections = "General Store/Text")]
	public float m_productDetailsExtendedHeight = 15.5f;

	// Token: 0x0400522B RID: 21035
	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_koreanProductDetailsText;

	// Token: 0x0400522C RID: 21036
	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_koreanWarningText;

	// Token: 0x0400522D RID: 21037
	[CustomEditField(Sections = "General Store/Text")]
	public float m_koreanProductDetailsRegularHeight = 8f;

	// Token: 0x0400522E RID: 21038
	[CustomEditField(Sections = "General Store/Text")]
	public float m_koreanProductDetailsExtendedHeight = 10.5f;

	// Token: 0x0400522F RID: 21039
	[CustomEditField(Sections = "General Store/Text")]
	public GameObject m_chooseArrowContainer;

	// Token: 0x04005230 RID: 21040
	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_chooseArrowText;

	// Token: 0x04005231 RID: 21041
	[CustomEditField(Sections = "General Store/Content")]
	public float m_contentFlipAnimationTime = 0.5f;

	// Token: 0x04005232 RID: 21042
	[CustomEditField(Sections = "General Store/Content")]
	public iTween.EaseType m_contentFlipEaseType = iTween.EaseType.easeOutBounce;

	// Token: 0x04005233 RID: 21043
	[CustomEditField(Sections = "General Store/Panes")]
	public GeneralStorePane m_defaultPane;

	// Token: 0x04005234 RID: 21044
	[CustomEditField(Sections = "General Store/Panes")]
	public Vector3 m_paneSwapOutOffset = new Vector3(0.05f, 0f, 0f);

	// Token: 0x04005235 RID: 21045
	[CustomEditField(Sections = "General Store/Panes")]
	public Vector3 m_paneSwapInOffset = new Vector3(0f, -0.05f, 0f);

	// Token: 0x04005236 RID: 21046
	[CustomEditField(Sections = "General Store/Panes")]
	public float m_paneSwapAnimationTime = 1f;

	// Token: 0x04005237 RID: 21047
	[CustomEditField(Sections = "General Store/Panes")]
	public UIBScrollable m_paneScrollbar;

	// Token: 0x04005238 RID: 21048
	[CustomEditField(Sections = "General Store/Sounds", T = EditType.SOUND_PREFAB)]
	public string m_contentFlipSound;

	// Token: 0x04005239 RID: 21049
	[CustomEditField(Sections = "Aspect Ratio")]
	public float m_rootScaleExtraWideAspectRatio = 1.9f;

	// Token: 0x0400523A RID: 21050
	[CustomEditField(Sections = "Aspect Ratio")]
	public float m_rootXPosExtraWideAspectRatio = 0.077f;

	// Token: 0x0400523B RID: 21051
	[CustomEditField(Sections = "Aspect Ratio")]
	public float m_rootZPosExtraWideAspectRatio = 0.0431f;

	// Token: 0x0400523C RID: 21052
	private static readonly int MIN_GOLD_FOR_CHANGE_QTY_TOOLTIP = 500;

	// Token: 0x0400523D RID: 21053
	private static readonly float FLIP_BUY_PANEL_ANIM_TIME = 0.1f;

	// Token: 0x0400523E RID: 21054
	private static readonly Vector3 MAIN_PANEL_ANGLE_TO_ROTATE = new Vector3(0.33333334f, 0f, 0f);

	// Token: 0x0400523F RID: 21055
	private static readonly GeneralStoreMode[] s_ContentOrdering = new GeneralStoreMode[]
	{
		GeneralStoreMode.ADVENTURE,
		GeneralStoreMode.CARDS
	};

	// Token: 0x04005240 RID: 21056
	private static readonly Vector3[] s_ContentTriangularPositions = new Vector3[]
	{
		new Vector3(0f, 0.125f, 0f),
		new Vector3(0f, -0.064f, -0.109f),
		new Vector3(0f, -0.064f, 0.109f)
	};

	// Token: 0x04005241 RID: 21057
	private static readonly Vector3[] s_ContentTriangularRotations = new Vector3[]
	{
		new Vector3(-60f, 0f, -180f),
		new Vector3(0f, -180f, 0f),
		new Vector3(60f, 0f, 180f)
	};

	// Token: 0x04005242 RID: 21058
	private static readonly Vector3[] s_MainPanelTriangularRotations = new Vector3[]
	{
		new Vector3(0f, 0f, 0f),
		new Vector3(-240f, 0f, 0f),
		new Vector3(-120f, 0f, 0f)
	};

	// Token: 0x04005243 RID: 21059
	private static GeneralStore s_instance;

	// Token: 0x04005244 RID: 21060
	private GeneralStore.BuyPanelState m_buyPanelState;

	// Token: 0x04005245 RID: 21061
	private bool m_staticTextResized;

	// Token: 0x04005246 RID: 21062
	private GeneralStoreMode m_currentMode;

	// Token: 0x04005247 RID: 21063
	private int m_settingNewModeCount;

	// Token: 0x04005248 RID: 21064
	private ShakePane m_shakePane;

	// Token: 0x04005249 RID: 21065
	private List<GeneralStore.ModeChanged> m_modeChangedListeners = new List<GeneralStore.ModeChanged>();

	// Token: 0x0400524A RID: 21066
	private int m_currentContentPositionIdx;

	// Token: 0x0400524B RID: 21067
	private MusicPlaylistType m_prevPlaylist;

	// Token: 0x0400524C RID: 21068
	private Map<GeneralStoreMode, Vector3> m_paneStartPositions = new Map<GeneralStoreMode, Vector3>();

	// Token: 0x0400524D RID: 21069
	private AssetHandle<Texture> m_accentTexture;

	// Token: 0x02002255 RID: 8789
	// (Invoke) Token: 0x060126F0 RID: 75504
	public delegate void ModeChanged(GeneralStoreMode oldMode, GeneralStoreMode newMode);

	// Token: 0x02002256 RID: 8790
	[Serializable]
	public class ModeObjects
	{
		// Token: 0x0400E33A RID: 58170
		public GeneralStoreMode m_mode;

		// Token: 0x0400E33B RID: 58171
		public GeneralStoreContent m_content;

		// Token: 0x0400E33C RID: 58172
		public GeneralStorePane m_pane;

		// Token: 0x0400E33D RID: 58173
		public UIBButton m_button;
	}

	// Token: 0x02002257 RID: 8791
	private enum BuyPanelState
	{
		// Token: 0x0400E33F RID: 58175
		DISABLED,
		// Token: 0x0400E340 RID: 58176
		BUY_GOLD,
		// Token: 0x0400E341 RID: 58177
		BUY_MONEY
	}
}
