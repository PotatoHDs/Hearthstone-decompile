using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020000EE RID: 238
[CustomEditClass]
public class CardBackInfoManager : MonoBehaviour, IStore
{
	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0004DE3A File Offset: 0x0004C03A
	// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0004DE42 File Offset: 0x0004C042
	public bool IsPreviewing { get; private set; }

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06000DC2 RID: 3522 RVA: 0x0004DE4C File Offset: 0x0004C04C
	// (remove) Token: 0x06000DC3 RID: 3523 RVA: 0x0004DE84 File Offset: 0x0004C084
	public event Action OnOpened;

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06000DC4 RID: 3524 RVA: 0x0004DEBC File Offset: 0x0004C0BC
	// (remove) Token: 0x06000DC5 RID: 3525 RVA: 0x0004DEF4 File Offset: 0x0004C0F4
	public event Action<StoreClosedArgs> OnClosed;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06000DC6 RID: 3526 RVA: 0x0004DF2C File Offset: 0x0004C12C
	// (remove) Token: 0x06000DC7 RID: 3527 RVA: 0x0004DF64 File Offset: 0x0004C164
	public event Action OnReady;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06000DC8 RID: 3528 RVA: 0x0004DF9C File Offset: 0x0004C19C
	// (remove) Token: 0x06000DC9 RID: 3529 RVA: 0x0004DFD4 File Offset: 0x0004C1D4
	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	// Token: 0x06000DCA RID: 3530 RVA: 0x0004E009 File Offset: 0x0004C209
	public static CardBackInfoManager Get()
	{
		return CardBackInfoManager.s_instance;
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x0004E010 File Offset: 0x0004C210
	public static void EnterPreviewWhenReady(CollectionCardVisual cardVisual)
	{
		CardBackInfoManager cardBackInfoManager = CardBackInfoManager.Get();
		if (cardBackInfoManager != null)
		{
			cardBackInfoManager.EnterPreview(cardVisual);
			return;
		}
		if (CardBackInfoManager.s_isReadyingInstance)
		{
			Debug.LogWarning("CardBackInfoManager:EnterPreviewWhenReady called while the info manager instance was being readied");
			return;
		}
		string assetString = "CardBackInfoManager.prefab:d53d863de659e4cce97ba2ce0107fb49";
		Widget widget = WidgetInstance.Create(assetString, false);
		if (widget == null)
		{
			Debug.LogError("CardBackInfoManager:EnterPreviewWhenReady failed to create widget instance");
			return;
		}
		CardBackInfoManager.s_isReadyingInstance = true;
		widget.RegisterReadyListener(delegate(object _)
		{
			CardBackInfoManager.s_instance = widget.GetComponentInChildren<CardBackInfoManager>();
			CardBackInfoManager.s_isReadyingInstance = false;
			if (CardBackInfoManager.s_instance == null)
			{
				Debug.LogError("CardBackInfoManager:EnterPreviewWhenReady created widget instance but failed to get CardBackInfoManager component");
				return;
			}
			CardBackInfoManager.s_instance.EnterPreview(cardVisual);
		}, null, true);
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x0004E0A3 File Offset: 0x0004C2A3
	public static bool IsLoadedAndShowingPreview()
	{
		return CardBackInfoManager.s_instance && CardBackInfoManager.s_instance.IsPreviewing;
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x0004E0BD File Offset: 0x0004C2BD
	private void Awake()
	{
		this.m_previewPane.SetActive(false);
		this.SetupUI();
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0004E0D4 File Offset: 0x0004C2D4
	private void Start()
	{
		this.m_userActionVisualControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnUserActionVisualControllerReady));
		this.m_visibilityVisualControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnVisibilityVisualControllerReady));
		this.m_fullScreenBlockerWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnFullScreenBlockerWidgetReady));
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0004E126 File Offset: 0x0004C326
	private void OnDestroy()
	{
		CardBackInfoManager.s_instance = null;
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x0004E130 File Offset: 0x0004C330
	public void EnterPreview(CollectionCardVisual cardVisual)
	{
		Actor actor = cardVisual.GetActor();
		if (actor == null)
		{
			Debug.LogError("Unable to obtain actor from card visual.");
			return;
		}
		CollectionCardBack component = actor.GetComponent<CollectionCardBack>();
		if (component == null)
		{
			Debug.LogError("Actor does not contain a CollectionCardBack component!");
			return;
		}
		this.EnterPreview(component.GetCardBackId(), cardVisual);
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0004E180 File Offset: 0x0004C380
	public void EnterPreview(int cardBackIdx, CollectionCardVisual cardVisual)
	{
		if (this.m_animating)
		{
			return;
		}
		if (this.m_currentCardBack != null)
		{
			UnityEngine.Object.Destroy(this.m_currentCardBack);
			this.m_currentCardBack = null;
		}
		this.m_animating = true;
		CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBackIdx);
		this.m_title.Text = record.Name;
		this.m_description.Text = record.Description;
		this.m_currentCardBackIdx = new int?(cardBackIdx);
		this.IsPreviewing = true;
		this.SetupCardBackStore();
		this.UpdateView();
		Action<object> <>9__1;
		if (!CardBackManager.Get().LoadCardBackByIndex(cardBackIdx, delegate(CardBackManager.LoadCardBackData cardBackData)
		{
			GameObject gameObject = cardBackData.m_GameObject;
			gameObject.name = "CARD_BACK_" + cardBackIdx;
			SceneUtils.SetLayer(gameObject, this.m_cardBackContainer.gameObject.layer, null);
			GameUtils.SetParent(gameObject, this.m_cardBackContainer, false);
			this.m_currentCardBack = gameObject;
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_currentCardBack.transform.localPosition = Vector3.zero;
			}
			else
			{
				this.m_currentCardBack.transform.position = cardVisual.transform.position;
				iTween.MoveTo(this.m_currentCardBack.gameObject, iTween.Hash(new object[]
				{
					"name",
					"FinishBigCardMove",
					"position",
					this.m_cardBackContainer.transform.position,
					"time",
					this.m_animationTime
				}));
				iTween.ScaleTo(this.m_currentCardBack.gameObject, iTween.Hash(new object[]
				{
					"scale",
					Vector3.one,
					"time",
					this.m_animationTime,
					"easeType",
					iTween.EaseType.easeOutQuad
				}));
				iTween.PunchRotation(this.m_currentCardBack, iTween.Hash(new object[]
				{
					"amount",
					new Vector3(0f, 0f, 75f),
					"time",
					2.5f
				}));
			}
			this.m_currentCardBack.transform.localScale = Vector3.one;
			this.m_currentCardBack.transform.localRotation = Quaternion.identity;
			this.m_previewPane.SetActive(true);
			this.m_offClicker.gameObject.SetActive(true);
			GameObject previewPane = this.m_previewPane;
			object[] array = new object[8];
			array[0] = "scale";
			array[1] = new Vector3(0.01f, 0.01f, 0.01f);
			array[2] = "time";
			array[3] = this.m_animationTime;
			array[4] = "easeType";
			array[5] = iTween.EaseType.easeOutCirc;
			array[6] = "oncomplete";
			int num = 7;
			Action<object> action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate(object e)
				{
					this.m_animating = false;
				});
			}
			array[num] = action;
			iTween.ScaleFrom(previewPane, iTween.Hash(array));
		}, null))
		{
			Debug.LogError(string.Format("Unable to load card back ID {0} for preview.", cardBackIdx));
			this.m_animating = false;
		}
		if (!string.IsNullOrEmpty(this.m_enterPreviewSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_enterPreviewSound);
		}
		FullScreenFXMgr.Get().StartStandardBlurVignette(this.m_animationTime);
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0004E2AC File Offset: 0x0004C4AC
	public void CancelPreview()
	{
		if (this.m_animating)
		{
			return;
		}
		this.ShutDownCardBackStore();
		Vector3 origScale = this.m_previewPane.transform.localScale;
		this.IsPreviewing = false;
		this.m_animating = true;
		iTween.ScaleTo(this.m_previewPane, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"time",
			this.m_animationTime,
			"easeType",
			iTween.EaseType.easeOutCirc,
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				this.m_animating = false;
				this.m_previewPane.transform.localScale = origScale;
				this.m_previewPane.SetActive(false);
				this.m_offClicker.gameObject.SetActive(false);
			})
		}));
		iTween.ScaleTo(this.m_currentCardBack, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"time",
			this.m_animationTime,
			"easeType",
			iTween.EaseType.easeOutCirc,
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				this.m_currentCardBack.SetActive(false);
			})
		}));
		if (!string.IsNullOrEmpty(this.m_exitPreviewSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_exitPreviewSound);
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(this.m_animationTime, null);
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x0004E41E File Offset: 0x0004C61E
	private void OnUserActionVisualControllerReady(VisualController visualController)
	{
		this.m_userActionVisualController = visualController;
		this.UpdateView();
		if (this.OnReady != null)
		{
			this.OnReady();
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0004E440 File Offset: 0x0004C640
	private void OnVisibilityVisualControllerReady(VisualController visualController)
	{
		this.m_visibilityVisualController = visualController;
		this.UpdateView();
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0004E44F File Offset: 0x0004C64F
	private void OnFullScreenBlockerWidgetReady(Widget fullScreenBlockerWidget)
	{
		this.m_fullScreenBlockerWidget = fullScreenBlockerWidget;
		this.UpdateView();
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0004E460 File Offset: 0x0004C660
	private void SetupUI()
	{
		this.m_buyButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnBuyButtonReleased();
		});
		this.m_favoriteButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			if (this.m_currentCardBackIdx == null)
			{
				Debug.LogError("CardBackInfoManager:FavoriteButtonRelease: m_currentCardBackIdx did not have a value");
				return;
			}
			CardBackManager.Get().RequestSetFavoriteCardBack(this.m_currentCardBackIdx.Value);
			this.CancelPreview();
		});
		this.m_offClicker.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.CancelPreview();
		});
		this.m_offClicker.AddEventListener(UIEventType.RIGHTCLICK, delegate(UIEvent e)
		{
			this.CancelPreview();
		});
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
	private void OnBuyButtonReleased()
	{
		if (this.m_currentCardBackIdx == null)
		{
			Debug.LogError("CardBackInfoManager:OnBuyButtonReleased: m_currentCardBackIdx did not have a value");
			return;
		}
		this.m_visibilityVisualController.SetState("HIDDEN");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Format("GLUE_CARD_BACK_PURCHASE_CONFIRMATION_HEADER", Array.Empty<object>());
		popupInfo.m_text = GameStrings.Format("GLUE_CARD_BACK_PURCHASE_CONFIRMATION_MESSAGE", new object[]
		{
			this.m_title.Text
		});
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		AlertPopup.ResponseCallback responseCallback = delegate(AlertPopup.Response response, object userdata)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				this.StartPurchaseTransaction();
				return;
			}
			this.m_visibilityVisualController.SetState("VISIBLE");
		};
		popupInfo.m_responseCallback = responseCallback;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0004E578 File Offset: 0x0004C778
	private void UpdateView()
	{
		if (this.m_userActionVisualController == null || this.m_visibilityVisualController == null || this.m_fullScreenBlockerWidget == null || this.m_currentCardBackIdx == null)
		{
			return;
		}
		CardBackManager cardBackManager = CardBackManager.Get();
		bool enabled = false;
		if (CollectionManager.Get().IsInEditMode())
		{
			this.m_userActionVisualController.SetState("DISABLED");
		}
		else if (cardBackManager.IsCardBackOwned(this.m_currentCardBackIdx.Value))
		{
			this.m_userActionVisualController.SetState("MAKE_FAVORITE");
		}
		else if (!cardBackManager.IsCardBackPurchasableFromCollectionManager(this.m_currentCardBackIdx.Value))
		{
			this.m_userActionVisualController.SetState("DISABLED");
		}
		else
		{
			PriceDataModel collectionManagerCardBackPriceDataModel = cardBackManager.GetCollectionManagerCardBackPriceDataModel(this.m_currentCardBackIdx.Value);
			this.m_userActionVisualController.BindDataModel(collectionManagerCardBackPriceDataModel, true, false);
			if (!cardBackManager.CanBuyCardBackFromCollectionManager(this.m_currentCardBackIdx.Value))
			{
				this.m_userActionVisualController.SetState("INSUFFICIENT_CURRENCY");
			}
			else
			{
				this.m_userActionVisualController.SetState("SUFFICIENT_CURRENCY");
				enabled = true;
			}
		}
		bool flag = cardBackManager.CanFavoriteCardBack(this.m_currentCardBackIdx.Value);
		this.m_favoriteButton.SetEnabled(flag, false);
		this.m_favoriteButton.Flip(flag, false);
		this.m_buyButton.SetEnabled(enabled, false);
		this.m_buyButton.Flip(true, false);
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0004E6D4 File Offset: 0x0004C8D4
	private void BlockInputs(bool blocked)
	{
		if (this.m_fullScreenBlockerWidget == null)
		{
			Debug.LogError("Failed to toggle interface blocker from Duels Popup Manager");
			return;
		}
		if (blocked)
		{
			this.m_fullScreenBlockerWidget.TriggerEvent("BLOCK_SCREEN", new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			});
			return;
		}
		this.m_fullScreenBlockerWidget.TriggerEvent("UNBLOCK_SCREEN", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = false
		});
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0004E754 File Offset: 0x0004C954
	private void SetupCardBackStore()
	{
		if (this.m_isStoreOpen)
		{
			Debug.LogError("CardBackInfoManager:SetupCardBackStore called when the store was already open");
			return;
		}
		if (this.m_currentCardBackIdx == null)
		{
			Debug.LogError("CardBackInfoManager:SetupCardBackStore: m_currentCardBackIdx did not have a value");
			return;
		}
		StoreManager storeManager = StoreManager.Get();
		if (!storeManager.IsOpen(true))
		{
			return;
		}
		storeManager.SetupCardBackStore(this, this.m_currentCardBackIdx.Value);
		storeManager.RegisterSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchase));
		storeManager.RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		storeManager.RegisterFailedPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnFailedPurchaseAck));
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar == null)
		{
			return;
		}
		bnetBar.RefreshCurrency();
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0004E7F4 File Offset: 0x0004C9F4
	private void ShutDownCardBackStore()
	{
		if (!this.m_isStoreOpen)
		{
			return;
		}
		this.CancelPurchaseTransaction();
		Action<StoreClosedArgs> onClosed = this.OnClosed;
		if (onClosed != null)
		{
			onClosed(new StoreClosedArgs(false));
		}
		StoreManager storeManager = StoreManager.Get();
		storeManager.RemoveFailedPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnFailedPurchaseAck));
		storeManager.RemoveSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchase));
		storeManager.RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		storeManager.ShutDownCardBackStore();
		this.OnProductPurchaseAttempt = null;
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.RefreshCurrency();
		}
		this.BlockInputs(false);
		this.m_isStoreOpen = false;
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnSuccessfulPurchase(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x0004E88C File Offset: 0x0004CA8C
	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		this.EndPurchaseTransaction();
		CardBackManager.Get().AddNewCardBack(this.m_currentCardBackIdx.Value);
		CollectionManager.Get().RefreshCurrentPageContents();
		this.m_visibilityVisualController.SetState("VISIBLE");
		this.UpdateView();
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0004E8CA File Offset: 0x0004CACA
	private void OnFailedPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		this.EndPurchaseTransaction();
		this.m_visibilityVisualController.SetState("VISIBLE");
		this.UpdateView();
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0004E8EC File Offset: 0x0004CAEC
	private void StartPurchaseTransaction()
	{
		if (this.m_currentCardBackIdx == null)
		{
			Debug.LogError("CardBackInfoManager:StartPurchaseTransaction: m_currentCardBackIdx did not have a value");
			return;
		}
		if (this.m_isStoreTransactionActive)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_CARD_BACK_PURCHASE_ERROR_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Debug.LogWarning("CardBackInfoManager:StartPurchaseTransaction: Attempted to start a card back transaction while an existing transaction was in progress");
			return;
		}
		if (this.OnProductPurchaseAttempt == null)
		{
			Debug.LogError("CardBackInfoManager:StartPurchaseTransaction: Attempted to start a card back purchase transaction while OnProductPurchaseAttempt was null");
			return;
		}
		this.m_isStoreTransactionActive = true;
		Network.Bundle collectionManagerCardBackProductBundle = CardBackManager.Get().GetCollectionManagerCardBackProductBundle(this.m_currentCardBackIdx.Value);
		if (collectionManagerCardBackProductBundle == null)
		{
			Debug.LogError("CardBackInfoManager:StartPurchaseTransaction: Attempted to start a card back purchase transaction with a null product bundle for card back " + this.m_currentCardBackIdx.Value.ToString());
			return;
		}
		this.OnProductPurchaseAttempt(new BuyPmtProductEventArgs(collectionManagerCardBackProductBundle, CurrencyType.GOLD, 1));
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0004E9CC File Offset: 0x0004CBCC
	private void CancelPurchaseTransaction()
	{
		this.EndPurchaseTransaction();
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0004E9D4 File Offset: 0x0004CBD4
	private void EndPurchaseTransaction()
	{
		if (!this.m_isStoreTransactionActive)
		{
			return;
		}
		this.m_isStoreTransactionActive = false;
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x0004E9E8 File Offset: 0x0004CBE8
	void IStore.Open()
	{
		Shop.Get().RefreshDataModel();
		this.m_isStoreOpen = true;
		Action onOpened = this.OnOpened;
		if (onOpened != null)
		{
			onOpened();
		}
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.RefreshCurrency();
			return;
		}
		Debug.LogError("CardBackInfoManager:IStore.Open: Could not get the Bnet bar to reflect the required currency");
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x0004EA37 File Offset: 0x0004CC37
	void IStore.Close()
	{
		if (this.m_isStoreTransactionActive)
		{
			this.CancelPurchaseTransaction();
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0004EA47 File Offset: 0x0004CC47
	void IStore.BlockInterface(bool blocked)
	{
		this.BlockInputs(blocked);
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x000052EC File Offset: 0x000034EC
	bool IStore.IsReady()
	{
		return true;
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0004EA50 File Offset: 0x0004CC50
	bool IStore.IsOpen()
	{
		return this.m_isStoreOpen;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00003BE8 File Offset: 0x00001DE8
	void IStore.Unload()
	{
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x0004EA58 File Offset: 0x0004CC58
	IEnumerable<CurrencyType> IStore.GetVisibleCurrencies()
	{
		return new CurrencyType[]
		{
			CurrencyType.GOLD
		};
	}

	// Token: 0x0400097A RID: 2426
	private const string STATE_MAKE_FAVORITE = "MAKE_FAVORITE";

	// Token: 0x0400097B RID: 2427
	private const string STATE_SUFFICIENT_CURRENCY = "SUFFICIENT_CURRENCY";

	// Token: 0x0400097C RID: 2428
	private const string STATE_INSUFFICIENT_CURRENCY = "INSUFFICIENT_CURRENCY";

	// Token: 0x0400097D RID: 2429
	private const string STATE_DISABLED = "DISABLED";

	// Token: 0x0400097E RID: 2430
	private const string STATE_VISIBLE = "VISIBLE";

	// Token: 0x0400097F RID: 2431
	private const string STATE_HIDDEN = "HIDDEN";

	// Token: 0x04000980 RID: 2432
	private const string STATE_BLOCK_SCREEN = "BLOCK_SCREEN";

	// Token: 0x04000981 RID: 2433
	private const string STATE_UNBLOCK_SCREEN = "UNBLOCK_SCREEN";

	// Token: 0x04000982 RID: 2434
	public GameObject m_previewPane;

	// Token: 0x04000983 RID: 2435
	public GameObject m_cardBackContainer;

	// Token: 0x04000984 RID: 2436
	public UberText m_title;

	// Token: 0x04000985 RID: 2437
	public UberText m_description;

	// Token: 0x04000986 RID: 2438
	public UIBButton m_buyButton;

	// Token: 0x04000987 RID: 2439
	public UIBButton m_favoriteButton;

	// Token: 0x04000988 RID: 2440
	public PegUIElement m_offClicker;

	// Token: 0x04000989 RID: 2441
	public float m_animationTime = 0.5f;

	// Token: 0x0400098A RID: 2442
	public AsyncReference m_userActionVisualControllerReference;

	// Token: 0x0400098B RID: 2443
	public AsyncReference m_visibilityVisualControllerReference;

	// Token: 0x0400098C RID: 2444
	public AsyncReference m_fullScreenBlockerWidgetReference;

	// Token: 0x0400098E RID: 2446
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_enterPreviewSound;

	// Token: 0x0400098F RID: 2447
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_exitPreviewSound;

	// Token: 0x04000990 RID: 2448
	private int? m_currentCardBackIdx;

	// Token: 0x04000991 RID: 2449
	private GameObject m_currentCardBack;

	// Token: 0x04000992 RID: 2450
	private bool m_animating;

	// Token: 0x04000993 RID: 2451
	private VisualController m_userActionVisualController;

	// Token: 0x04000994 RID: 2452
	private VisualController m_visibilityVisualController;

	// Token: 0x04000995 RID: 2453
	private Widget m_fullScreenBlockerWidget;

	// Token: 0x04000996 RID: 2454
	private bool m_isStoreOpen;

	// Token: 0x04000997 RID: 2455
	private bool m_isStoreTransactionActive;

	// Token: 0x04000998 RID: 2456
	private static CardBackInfoManager s_instance;

	// Token: 0x04000999 RID: 2457
	private static bool s_isReadyingInstance;
}
