using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class CardBackInfoManager : MonoBehaviour, IStore
{
	private const string STATE_MAKE_FAVORITE = "MAKE_FAVORITE";

	private const string STATE_SUFFICIENT_CURRENCY = "SUFFICIENT_CURRENCY";

	private const string STATE_INSUFFICIENT_CURRENCY = "INSUFFICIENT_CURRENCY";

	private const string STATE_DISABLED = "DISABLED";

	private const string STATE_VISIBLE = "VISIBLE";

	private const string STATE_HIDDEN = "HIDDEN";

	private const string STATE_BLOCK_SCREEN = "BLOCK_SCREEN";

	private const string STATE_UNBLOCK_SCREEN = "UNBLOCK_SCREEN";

	public GameObject m_previewPane;

	public GameObject m_cardBackContainer;

	public UberText m_title;

	public UberText m_description;

	public UIBButton m_buyButton;

	public UIBButton m_favoriteButton;

	public PegUIElement m_offClicker;

	public float m_animationTime = 0.5f;

	public AsyncReference m_userActionVisualControllerReference;

	public AsyncReference m_visibilityVisualControllerReference;

	public AsyncReference m_fullScreenBlockerWidgetReference;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_enterPreviewSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_exitPreviewSound;

	private int? m_currentCardBackIdx;

	private GameObject m_currentCardBack;

	private bool m_animating;

	private VisualController m_userActionVisualController;

	private VisualController m_visibilityVisualController;

	private Widget m_fullScreenBlockerWidget;

	private bool m_isStoreOpen;

	private bool m_isStoreTransactionActive;

	private static CardBackInfoManager s_instance;

	private static bool s_isReadyingInstance;

	public bool IsPreviewing { get; private set; }

	public event Action OnOpened;

	public event Action<StoreClosedArgs> OnClosed;

	public event Action OnReady;

	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	public static CardBackInfoManager Get()
	{
		return s_instance;
	}

	public static void EnterPreviewWhenReady(CollectionCardVisual cardVisual)
	{
		CardBackInfoManager cardBackInfoManager = Get();
		if (cardBackInfoManager != null)
		{
			cardBackInfoManager.EnterPreview(cardVisual);
			return;
		}
		if (s_isReadyingInstance)
		{
			Debug.LogWarning("CardBackInfoManager:EnterPreviewWhenReady called while the info manager instance was being readied");
			return;
		}
		string assetString = "CardBackInfoManager.prefab:d53d863de659e4cce97ba2ce0107fb49";
		Widget widget = WidgetInstance.Create(assetString);
		if (widget == null)
		{
			Debug.LogError("CardBackInfoManager:EnterPreviewWhenReady failed to create widget instance");
			return;
		}
		s_isReadyingInstance = true;
		widget.RegisterReadyListener(delegate
		{
			s_instance = widget.GetComponentInChildren<CardBackInfoManager>();
			s_isReadyingInstance = false;
			if (s_instance == null)
			{
				Debug.LogError("CardBackInfoManager:EnterPreviewWhenReady created widget instance but failed to get CardBackInfoManager component");
			}
			else
			{
				s_instance.EnterPreview(cardVisual);
			}
		});
	}

	public static bool IsLoadedAndShowingPreview()
	{
		if (!s_instance)
		{
			return false;
		}
		return s_instance.IsPreviewing;
	}

	private void Awake()
	{
		m_previewPane.SetActive(value: false);
		SetupUI();
	}

	private void Start()
	{
		m_userActionVisualControllerReference.RegisterReadyListener<VisualController>(OnUserActionVisualControllerReady);
		m_visibilityVisualControllerReference.RegisterReadyListener<VisualController>(OnVisibilityVisualControllerReady);
		m_fullScreenBlockerWidgetReference.RegisterReadyListener<Widget>(OnFullScreenBlockerWidgetReady);
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

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
		}
		else
		{
			EnterPreview(component.GetCardBackId(), cardVisual);
		}
	}

	public void EnterPreview(int cardBackIdx, CollectionCardVisual cardVisual)
	{
		if (!m_animating)
		{
			if (m_currentCardBack != null)
			{
				UnityEngine.Object.Destroy(m_currentCardBack);
				m_currentCardBack = null;
			}
			m_animating = true;
			CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBackIdx);
			m_title.Text = record.Name;
			m_description.Text = record.Description;
			m_currentCardBackIdx = cardBackIdx;
			IsPreviewing = true;
			SetupCardBackStore();
			UpdateView();
			if (!CardBackManager.Get().LoadCardBackByIndex(cardBackIdx, delegate(CardBackManager.LoadCardBackData cardBackData)
			{
				GameObject gameObject = cardBackData.m_GameObject;
				gameObject.name = "CARD_BACK_" + cardBackIdx;
				SceneUtils.SetLayer(gameObject, m_cardBackContainer.gameObject.layer);
				GameUtils.SetParent(gameObject, m_cardBackContainer);
				m_currentCardBack = gameObject;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					m_currentCardBack.transform.localPosition = Vector3.zero;
				}
				else
				{
					m_currentCardBack.transform.position = cardVisual.transform.position;
					iTween.MoveTo(m_currentCardBack.gameObject, iTween.Hash("name", "FinishBigCardMove", "position", m_cardBackContainer.transform.position, "time", m_animationTime));
					iTween.ScaleTo(m_currentCardBack.gameObject, iTween.Hash("scale", Vector3.one, "time", m_animationTime, "easeType", iTween.EaseType.easeOutQuad));
					iTween.PunchRotation(m_currentCardBack, iTween.Hash("amount", new Vector3(0f, 0f, 75f), "time", 2.5f));
				}
				m_currentCardBack.transform.localScale = Vector3.one;
				m_currentCardBack.transform.localRotation = Quaternion.identity;
				m_previewPane.SetActive(value: true);
				m_offClicker.gameObject.SetActive(value: true);
				iTween.ScaleFrom(m_previewPane, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", m_animationTime, "easeType", iTween.EaseType.easeOutCirc, "oncomplete", (Action<object>)delegate
				{
					m_animating = false;
				}));
			}))
			{
				Debug.LogError($"Unable to load card back ID {cardBackIdx} for preview.");
				m_animating = false;
			}
			if (!string.IsNullOrEmpty(m_enterPreviewSound))
			{
				SoundManager.Get().LoadAndPlay(m_enterPreviewSound);
			}
			FullScreenFXMgr.Get().StartStandardBlurVignette(m_animationTime);
		}
	}

	public void CancelPreview()
	{
		if (!m_animating)
		{
			ShutDownCardBackStore();
			Vector3 origScale = m_previewPane.transform.localScale;
			IsPreviewing = false;
			m_animating = true;
			iTween.ScaleTo(m_previewPane, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", m_animationTime, "easeType", iTween.EaseType.easeOutCirc, "oncomplete", (Action<object>)delegate
			{
				m_animating = false;
				m_previewPane.transform.localScale = origScale;
				m_previewPane.SetActive(value: false);
				m_offClicker.gameObject.SetActive(value: false);
			}));
			iTween.ScaleTo(m_currentCardBack, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", m_animationTime, "easeType", iTween.EaseType.easeOutCirc, "oncomplete", (Action<object>)delegate
			{
				m_currentCardBack.SetActive(value: false);
			}));
			if (!string.IsNullOrEmpty(m_exitPreviewSound))
			{
				SoundManager.Get().LoadAndPlay(m_exitPreviewSound);
			}
			FullScreenFXMgr.Get().EndStandardBlurVignette(m_animationTime);
		}
	}

	private void OnUserActionVisualControllerReady(VisualController visualController)
	{
		m_userActionVisualController = visualController;
		UpdateView();
		if (this.OnReady != null)
		{
			this.OnReady();
		}
	}

	private void OnVisibilityVisualControllerReady(VisualController visualController)
	{
		m_visibilityVisualController = visualController;
		UpdateView();
	}

	private void OnFullScreenBlockerWidgetReady(Widget fullScreenBlockerWidget)
	{
		m_fullScreenBlockerWidget = fullScreenBlockerWidget;
		UpdateView();
	}

	private void SetupUI()
	{
		m_buyButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnBuyButtonReleased();
		});
		m_favoriteButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			if (!m_currentCardBackIdx.HasValue)
			{
				Debug.LogError("CardBackInfoManager:FavoriteButtonRelease: m_currentCardBackIdx did not have a value");
			}
			else
			{
				CardBackManager.Get().RequestSetFavoriteCardBack(m_currentCardBackIdx.Value);
				CancelPreview();
			}
		});
		m_offClicker.AddEventListener(UIEventType.RELEASE, delegate
		{
			CancelPreview();
		});
		m_offClicker.AddEventListener(UIEventType.RIGHTCLICK, delegate
		{
			CancelPreview();
		});
	}

	private void OnBuyButtonReleased()
	{
		if (!m_currentCardBackIdx.HasValue)
		{
			Debug.LogError("CardBackInfoManager:OnBuyButtonReleased: m_currentCardBackIdx did not have a value");
			return;
		}
		m_visibilityVisualController.SetState("HIDDEN");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Format("GLUE_CARD_BACK_PURCHASE_CONFIRMATION_HEADER");
		popupInfo.m_text = GameStrings.Format("GLUE_CARD_BACK_PURCHASE_CONFIRMATION_MESSAGE", m_title.Text);
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		AlertPopup.ResponseCallback responseCallback = (popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userdata)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				StartPurchaseTransaction();
			}
			else
			{
				m_visibilityVisualController.SetState("VISIBLE");
			}
		});
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void UpdateView()
	{
		if (m_userActionVisualController == null || m_visibilityVisualController == null || m_fullScreenBlockerWidget == null || !m_currentCardBackIdx.HasValue)
		{
			return;
		}
		CardBackManager cardBackManager = CardBackManager.Get();
		bool flag = false;
		if (CollectionManager.Get().IsInEditMode())
		{
			m_userActionVisualController.SetState("DISABLED");
		}
		else if (cardBackManager.IsCardBackOwned(m_currentCardBackIdx.Value))
		{
			m_userActionVisualController.SetState("MAKE_FAVORITE");
		}
		else if (!cardBackManager.IsCardBackPurchasableFromCollectionManager(m_currentCardBackIdx.Value))
		{
			m_userActionVisualController.SetState("DISABLED");
		}
		else
		{
			PriceDataModel collectionManagerCardBackPriceDataModel = cardBackManager.GetCollectionManagerCardBackPriceDataModel(m_currentCardBackIdx.Value);
			m_userActionVisualController.BindDataModel(collectionManagerCardBackPriceDataModel);
			if (!cardBackManager.CanBuyCardBackFromCollectionManager(m_currentCardBackIdx.Value))
			{
				m_userActionVisualController.SetState("INSUFFICIENT_CURRENCY");
			}
			else
			{
				m_userActionVisualController.SetState("SUFFICIENT_CURRENCY");
				flag = true;
			}
		}
		bool faceUp = cardBackManager.CanFavoriteCardBack(m_currentCardBackIdx.Value);
		m_favoriteButton.SetEnabled(faceUp);
		m_favoriteButton.Flip(faceUp);
		m_buyButton.SetEnabled(flag);
		m_buyButton.Flip(faceUp: true);
	}

	private void BlockInputs(bool blocked)
	{
		Widget.TriggerEventParameters parameters;
		if (m_fullScreenBlockerWidget == null)
		{
			Debug.LogError("Failed to toggle interface blocker from Duels Popup Manager");
		}
		else if (blocked)
		{
			Widget fullScreenBlockerWidget = m_fullScreenBlockerWidget;
			parameters = new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			};
			fullScreenBlockerWidget.TriggerEvent("BLOCK_SCREEN", parameters);
		}
		else
		{
			Widget fullScreenBlockerWidget2 = m_fullScreenBlockerWidget;
			parameters = new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			};
			fullScreenBlockerWidget2.TriggerEvent("UNBLOCK_SCREEN", parameters);
		}
	}

	private void SetupCardBackStore()
	{
		if (m_isStoreOpen)
		{
			Debug.LogError("CardBackInfoManager:SetupCardBackStore called when the store was already open");
			return;
		}
		if (!m_currentCardBackIdx.HasValue)
		{
			Debug.LogError("CardBackInfoManager:SetupCardBackStore: m_currentCardBackIdx did not have a value");
			return;
		}
		StoreManager storeManager = StoreManager.Get();
		if (storeManager.IsOpen())
		{
			storeManager.SetupCardBackStore(this, m_currentCardBackIdx.Value);
			storeManager.RegisterSuccessfulPurchaseListener(OnSuccessfulPurchase);
			storeManager.RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
			storeManager.RegisterFailedPurchaseAckListener(OnFailedPurchaseAck);
			BnetBar.Get()?.RefreshCurrency();
		}
	}

	private void ShutDownCardBackStore()
	{
		if (m_isStoreOpen)
		{
			CancelPurchaseTransaction();
			this.OnClosed?.Invoke(new StoreClosedArgs());
			StoreManager storeManager = StoreManager.Get();
			storeManager.RemoveFailedPurchaseAckListener(OnFailedPurchaseAck);
			storeManager.RemoveSuccessfulPurchaseListener(OnSuccessfulPurchase);
			storeManager.RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
			storeManager.ShutDownCardBackStore();
			this.OnProductPurchaseAttempt = null;
			BnetBar.Get()?.RefreshCurrency();
			BlockInputs(blocked: false);
			m_isStoreOpen = false;
		}
	}

	private void OnSuccessfulPurchase(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
	}

	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		EndPurchaseTransaction();
		CardBackManager.Get().AddNewCardBack(m_currentCardBackIdx.Value);
		CollectionManager.Get().RefreshCurrentPageContents();
		m_visibilityVisualController.SetState("VISIBLE");
		UpdateView();
	}

	private void OnFailedPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		EndPurchaseTransaction();
		m_visibilityVisualController.SetState("VISIBLE");
		UpdateView();
	}

	private void StartPurchaseTransaction()
	{
		if (!m_currentCardBackIdx.HasValue)
		{
			Debug.LogError("CardBackInfoManager:StartPurchaseTransaction: m_currentCardBackIdx did not have a value");
		}
		else if (m_isStoreTransactionActive)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_CARD_BACK_PURCHASE_ERROR_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Debug.LogWarning("CardBackInfoManager:StartPurchaseTransaction: Attempted to start a card back transaction while an existing transaction was in progress");
		}
		else if (this.OnProductPurchaseAttempt == null)
		{
			Debug.LogError("CardBackInfoManager:StartPurchaseTransaction: Attempted to start a card back purchase transaction while OnProductPurchaseAttempt was null");
		}
		else
		{
			m_isStoreTransactionActive = true;
			Network.Bundle collectionManagerCardBackProductBundle = CardBackManager.Get().GetCollectionManagerCardBackProductBundle(m_currentCardBackIdx.Value);
			if (collectionManagerCardBackProductBundle == null)
			{
				Debug.LogError("CardBackInfoManager:StartPurchaseTransaction: Attempted to start a card back purchase transaction with a null product bundle for card back " + m_currentCardBackIdx.Value);
			}
			else
			{
				this.OnProductPurchaseAttempt(new BuyPmtProductEventArgs(collectionManagerCardBackProductBundle, CurrencyType.GOLD, 1));
			}
		}
	}

	private void CancelPurchaseTransaction()
	{
		EndPurchaseTransaction();
	}

	private void EndPurchaseTransaction()
	{
		if (m_isStoreTransactionActive)
		{
			m_isStoreTransactionActive = false;
		}
	}

	void IStore.Open()
	{
		Shop.Get().RefreshDataModel();
		m_isStoreOpen = true;
		this.OnOpened?.Invoke();
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.RefreshCurrency();
		}
		else
		{
			Debug.LogError("CardBackInfoManager:IStore.Open: Could not get the Bnet bar to reflect the required currency");
		}
	}

	void IStore.Close()
	{
		if (m_isStoreTransactionActive)
		{
			CancelPurchaseTransaction();
		}
	}

	void IStore.BlockInterface(bool blocked)
	{
		BlockInputs(blocked);
	}

	bool IStore.IsReady()
	{
		return true;
	}

	bool IStore.IsOpen()
	{
		return m_isStoreOpen;
	}

	void IStore.Unload()
	{
	}

	IEnumerable<CurrencyType> IStore.GetVisibleCurrencies()
	{
		return new CurrencyType[1] { CurrencyType.GOLD };
	}
}
