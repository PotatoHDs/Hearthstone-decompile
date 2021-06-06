using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class GeneralStore : Store
{
	public delegate void ModeChanged(GeneralStoreMode oldMode, GeneralStoreMode newMode);

	[Serializable]
	public class ModeObjects
	{
		public GeneralStoreMode m_mode;

		public GeneralStoreContent m_content;

		public GeneralStorePane m_pane;

		public UIBButton m_button;
	}

	private enum BuyPanelState
	{
		DISABLED,
		BUY_GOLD,
		BUY_MONEY
	}

	[CustomEditField(Sections = "General Store")]
	public GameObject m_mainPanel;

	[CustomEditField(Sections = "General Store")]
	public GameObject m_buyGoldPanel;

	[CustomEditField(Sections = "General Store")]
	public GameObject m_buyMoneyPanel;

	[CustomEditField(Sections = "General Store")]
	public GameObject m_buyEmptyPanel;

	[CustomEditField(Sections = "General Store", ListTable = true)]
	public List<ModeObjects> m_modeObjects = new List<ModeObjects>();

	[CustomEditField(Sections = "General Store")]
	public MeshRenderer m_accentIcon;

	[CustomEditField(Sections = "General Store/Mode Buttons")]
	public GameObject m_modeButtonBlocker;

	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_moneyCostText;

	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_goldCostText;

	[CustomEditField(Sections = "General Store/Text")]
	public MultiSliceElement m_productDetailsContainer;

	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_productDetailsHeadlineText;

	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_productDetailsText;

	[CustomEditField(Sections = "General Store/Text")]
	public float m_productDetailsRegularHeight = 13f;

	[CustomEditField(Sections = "General Store/Text")]
	public float m_productDetailsExtendedHeight = 15.5f;

	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_koreanProductDetailsText;

	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_koreanWarningText;

	[CustomEditField(Sections = "General Store/Text")]
	public float m_koreanProductDetailsRegularHeight = 8f;

	[CustomEditField(Sections = "General Store/Text")]
	public float m_koreanProductDetailsExtendedHeight = 10.5f;

	[CustomEditField(Sections = "General Store/Text")]
	public GameObject m_chooseArrowContainer;

	[CustomEditField(Sections = "General Store/Text")]
	public UberText m_chooseArrowText;

	[CustomEditField(Sections = "General Store/Content")]
	public float m_contentFlipAnimationTime = 0.5f;

	[CustomEditField(Sections = "General Store/Content")]
	public iTween.EaseType m_contentFlipEaseType = iTween.EaseType.easeOutBounce;

	[CustomEditField(Sections = "General Store/Panes")]
	public GeneralStorePane m_defaultPane;

	[CustomEditField(Sections = "General Store/Panes")]
	public Vector3 m_paneSwapOutOffset = new Vector3(0.05f, 0f, 0f);

	[CustomEditField(Sections = "General Store/Panes")]
	public Vector3 m_paneSwapInOffset = new Vector3(0f, -0.05f, 0f);

	[CustomEditField(Sections = "General Store/Panes")]
	public float m_paneSwapAnimationTime = 1f;

	[CustomEditField(Sections = "General Store/Panes")]
	public UIBScrollable m_paneScrollbar;

	[CustomEditField(Sections = "General Store/Sounds", T = EditType.SOUND_PREFAB)]
	public string m_contentFlipSound;

	[CustomEditField(Sections = "Aspect Ratio")]
	public float m_rootScaleExtraWideAspectRatio = 1.9f;

	[CustomEditField(Sections = "Aspect Ratio")]
	public float m_rootXPosExtraWideAspectRatio = 0.077f;

	[CustomEditField(Sections = "Aspect Ratio")]
	public float m_rootZPosExtraWideAspectRatio = 0.0431f;

	private static readonly int MIN_GOLD_FOR_CHANGE_QTY_TOOLTIP = 500;

	private static readonly float FLIP_BUY_PANEL_ANIM_TIME = 0.1f;

	private static readonly Vector3 MAIN_PANEL_ANGLE_TO_ROTATE = new Vector3(0.333333343f, 0f, 0f);

	private static readonly GeneralStoreMode[] s_ContentOrdering = new GeneralStoreMode[2]
	{
		GeneralStoreMode.ADVENTURE,
		GeneralStoreMode.CARDS
	};

	private static readonly Vector3[] s_ContentTriangularPositions = new Vector3[3]
	{
		new Vector3(0f, 0.125f, 0f),
		new Vector3(0f, -0.064f, -0.109f),
		new Vector3(0f, -0.064f, 0.109f)
	};

	private static readonly Vector3[] s_ContentTriangularRotations = new Vector3[3]
	{
		new Vector3(-60f, 0f, -180f),
		new Vector3(0f, -180f, 0f),
		new Vector3(60f, 0f, 180f)
	};

	private static readonly Vector3[] s_MainPanelTriangularRotations = new Vector3[3]
	{
		new Vector3(0f, 0f, 0f),
		new Vector3(-240f, 0f, 0f),
		new Vector3(-120f, 0f, 0f)
	};

	private static GeneralStore s_instance;

	private BuyPanelState m_buyPanelState;

	private bool m_staticTextResized;

	private GeneralStoreMode m_currentMode;

	private int m_settingNewModeCount;

	private ShakePane m_shakePane;

	private List<ModeChanged> m_modeChangedListeners = new List<ModeChanged>();

	private int m_currentContentPositionIdx;

	private MusicPlaylistType m_prevPlaylist;

	private Map<GeneralStoreMode, Vector3> m_paneStartPositions = new Map<GeneralStoreMode, Vector3>();

	private AssetHandle<Texture> m_accentTexture;

	protected override void Start()
	{
		base.Start();
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(SuccessfulPurchaseAckEvent);
		SoundManager.Get().Load("gold_spend_plate_flip_on.prefab:e490542c7405fce45a46c7b9aad5aeab");
		SoundManager.Get().Load("gold_spend_plate_flip_off.prefab:8e19277d18c845547af53064aade9b2c");
		UpdateModeButtons(m_currentMode);
		foreach (ModeObjects modeObject in m_modeObjects)
		{
			if (modeObject.m_content != null)
			{
				modeObject.m_content.gameObject.SetActive(modeObject.m_mode == m_currentMode);
			}
		}
		m_shakePane = GetComponent<ShakePane>();
		if (m_offClicker != null)
		{
			m_offClicker.AddEventListener(UIEventType.RELEASE, OnClosePressed);
		}
	}

	protected override void Awake()
	{
		s_instance = this;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_scaleMode = (TransformUtil.IsExtraWideAspectRatio() ? CanvasScaleMode.HEIGHT : CanvasScaleMode.WIDTH);
		}
		base.Awake();
		m_buyWithMoneyButton.SetText(GameStrings.Get("GLUE_STORE_BUY_TEXT"));
		m_buyWithGoldButton.SetText(GameStrings.Get("GLUE_STORE_BUY_TEXT"));
		foreach (ModeObjects modeObject in m_modeObjects)
		{
			GeneralStoreContent content = modeObject.m_content;
			UIBButton button = modeObject.m_button;
			GeneralStoreMode mode = modeObject.m_mode;
			GeneralStorePane pane = modeObject.m_pane;
			if (content != null)
			{
				content.SetParentStore(this);
				content.RegisterCurrentBundleChanged(delegate(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
				{
					UpdateCostAndButtonState(goldBundle, moneyBundle);
				});
			}
			if (button != null)
			{
				button.AddEventListener(UIEventType.PRESS, delegate
				{
					SetMode(mode);
				});
			}
			if (pane != null)
			{
				pane.transform.localPosition = m_paneSwapOutOffset;
				m_paneStartPositions[mode] = pane.m_paneContainer.transform.localPosition;
			}
		}
		if (m_defaultPane != null)
		{
			m_defaultPane.transform.localPosition = m_paneSwapOutOffset;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(SuccessfulPurchaseAckEvent);
		AssetHandle.SafeDispose(ref m_accentTexture);
		m_mainPanel = null;
		s_instance = null;
	}

	public GeneralStoreContent GetCurrentContent()
	{
		return GetContent(m_currentMode);
	}

	public GeneralStorePane GetCurrentPane()
	{
		return GetPane(m_currentMode);
	}

	public GeneralStoreContent GetContent(GeneralStoreMode mode)
	{
		return m_modeObjects.Find((ModeObjects obj) => obj.m_mode == mode)?.m_content;
	}

	public GeneralStorePane GetPane(GeneralStoreMode mode)
	{
		ModeObjects modeObjects = m_modeObjects.Find((ModeObjects obj) => obj.m_mode == mode);
		if (modeObjects != null && modeObjects.m_pane != null)
		{
			return modeObjects.m_pane;
		}
		return m_defaultPane;
	}

	public void Close(bool closeWithAnimation)
	{
		if (m_shown)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				Navigation.RemoveHandler(GeneralStorePhoneCover.OnNavigateBack);
			}
			Navigation.Pop();
			CloseImpl(closeWithAnimation);
		}
	}

	public override void Close()
	{
		if (m_shown)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				Navigation.RemoveHandler(GeneralStorePhoneCover.OnNavigateBack);
			}
			if (m_settingNewModeCount == 0)
			{
				Navigation.GoBack();
			}
		}
	}

	public void SetMode(GeneralStoreMode mode)
	{
		StartCoroutine(AnimateAndUpdateStoreMode(m_currentMode, mode));
	}

	public GeneralStoreMode GetMode()
	{
		return m_currentMode;
	}

	public void ShakeStore(float xRotationAmount, float shakeTime, float delay = 0f, float translateAmount = 0f)
	{
		if (!(m_shakePane == null) && GeneralStoreMode.CARDS == m_currentMode)
		{
			m_shakePane.Shake(xRotationAmount, shakeTime, delay, translateAmount);
		}
	}

	public void SetDescription(string title, string desc, string warning = null)
	{
		HideChooseDescription();
		if (m_productDetailsContainer != null)
		{
			m_productDetailsContainer.gameObject.SetActive(value: true);
		}
		bool flag = StoreManager.Get().IsKoreanCustomer();
		bool flag2 = !string.IsNullOrEmpty(title);
		m_productDetailsHeadlineText.gameObject.SetActive(flag2);
		m_productDetailsText.gameObject.SetActive(!flag);
		m_koreanProductDetailsText.gameObject.SetActive(flag);
		m_koreanWarningText.gameObject.SetActive(flag);
		m_productDetailsText.Height = (flag2 ? m_productDetailsRegularHeight : m_productDetailsExtendedHeight);
		m_productDetailsHeadlineText.Text = title;
		m_koreanProductDetailsText.Text = desc;
		m_productDetailsText.Text = desc;
		m_koreanProductDetailsText.Height = (flag2 ? m_koreanProductDetailsRegularHeight : m_koreanProductDetailsExtendedHeight);
		m_koreanWarningText.Text = ((warning == null) ? string.Empty : warning);
		if (m_productDetailsContainer != null)
		{
			m_productDetailsContainer.UpdateSlices();
		}
	}

	public void HideDescription()
	{
		if (m_productDetailsContainer != null)
		{
			m_productDetailsContainer.gameObject.SetActive(value: false);
		}
	}

	public void SetChooseDescription(string chooseText)
	{
		HideDescription();
		SetAccentTexture(null);
		if (m_chooseArrowContainer != null)
		{
			m_chooseArrowContainer.SetActive(value: true);
		}
		if (m_chooseArrowText != null)
		{
			m_chooseArrowText.Text = chooseText;
		}
	}

	public void HideChooseDescription()
	{
		if (m_chooseArrowContainer != null)
		{
			m_chooseArrowContainer.SetActive(value: false);
		}
	}

	public void SetAccentTexture(AssetHandle<Texture> texture)
	{
		if (m_accentIcon != null)
		{
			bool flag = texture != null;
			m_accentIcon.gameObject.SetActive(flag);
			if (flag)
			{
				AssetHandle.Set(ref m_accentTexture, texture);
				m_accentIcon.GetMaterial().mainTexture = m_accentTexture;
			}
		}
	}

	public void HideAccentTexture()
	{
		if (m_accentIcon != null)
		{
			m_accentIcon.gameObject.SetActive(value: false);
		}
	}

	public void HidePacksPane(bool hide)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (hide)
			{
				StartCoroutine(AnimateAndUpdateStoreMode(GeneralStoreMode.CARDS, GeneralStoreMode.NONE));
			}
			else
			{
				StartCoroutine(AnimateAndUpdateStoreMode(GeneralStoreMode.NONE, GeneralStoreMode.CARDS));
			}
		}
		else
		{
			StartCoroutine(AnimateHideStorePane(hide));
		}
	}

	public void ResumePreviousMusicPlaylist()
	{
		if (m_prevPlaylist != 0)
		{
			MusicManager.Get().StartPlaylist(m_prevPlaylist);
		}
	}

	public void RegisterModeChangedListener(ModeChanged dlg)
	{
		m_modeChangedListeners.Add(dlg);
	}

	public void UnregisterModeChangedListener(ModeChanged dlg)
	{
		m_modeChangedListeners.Remove(dlg);
	}

	public static GeneralStore Get()
	{
		return s_instance;
	}

	public override bool IsReady()
	{
		return true;
	}

	public override void OnMoneySpent()
	{
		GeneralStoreContent currentContent = GetCurrentContent();
		if (currentContent != null)
		{
			currentContent.Refresh();
		}
		GeneralStorePane currentPane = GetCurrentPane();
		if (currentPane != null)
		{
			currentPane.Refresh();
		}
	}

	public override void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		UpdateGoldButtonState(balance);
	}

	protected override void ShowImpl(bool isTotallyFake)
	{
		if (m_shown)
		{
			return;
		}
		if (m_root != null && (bool)UniversalInputManager.UsePhoneUI && TransformUtil.IsExtraWideAspectRatio())
		{
			m_root.transform.localScale = Vector3.one * m_rootScaleExtraWideAspectRatio;
			TransformUtil.SetLocalPosX(m_root.transform, m_rootXPosExtraWideAspectRatio);
			TransformUtil.SetLocalPosZ(m_root.transform, m_rootZPosExtraWideAspectRatio);
		}
		m_prevPlaylist = MusicManager.Get().GetCurrentPlaylist();
		foreach (ModeObjects modeObject in m_modeObjects)
		{
			GeneralStoreContent content = modeObject.m_content;
			GeneralStorePane pane = modeObject.m_pane;
			if (content != null)
			{
				content.StoreShown(GetCurrentContent() == content);
			}
			if (pane != null)
			{
				pane.StoreShown(GetCurrentPane() == pane);
			}
		}
		ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.GENERAL_STORE);
		FriendChallengeMgr.Get().OnStoreOpened();
		PreRender();
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.STORE);
		if (!UniversalInputManager.UsePhoneUI && !Options.Get().GetBool(Option.HAS_SEEN_GOLD_QTY_INSTRUCTION, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("GeneralStore.Show:" + Option.HAS_SEEN_GOLD_QTY_INSTRUCTION) && NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>().GetTotal() >= MIN_GOLD_FOR_CHANGE_QTY_TOOLTIP)
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
			Options.Get().SetBool(Option.HAS_SEEN_GOLD_QTY_INSTRUCTION, val: true);
		}
		UpdateGoldButtonState();
		m_shown = true;
		Navigation.Push(OnNavigateBack);
		EnableFullScreenEffects(enable: true);
		SoundManager.Get().LoadAndPlay("Store_window_expand.prefab:050bf879a3e32d04999427c262baaf09", base.gameObject);
		DoShowAnimation(delegate
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				base.transform.localPosition = m_showPosition;
				m_root.transform.localPosition = Vector3.zero;
			}
			FireOpenedEvent();
		});
	}

	private void OnClosePressed(UIEvent e)
	{
		if (m_shown && (!HearthstoneServices.TryGet<HearthstoneCheckout>(out var service) || service.CurrentState != HearthstoneCheckout.State.InProgress) && StoreManager.Get().CanTapOutConfirmationUI())
		{
			Close();
		}
	}

	protected override void BuyWithMoney(UIEvent e)
	{
		GeneralStoreContent currentContent = GetCurrentContent();
		Network.Bundle bundle = currentContent.GetCurrentMoneyBundle();
		if (bundle == null)
		{
			Debug.LogWarning("GeneralStore.OnBuyWithMoneyPressed(): SelectedBundle is null");
			return;
		}
		GeneralStoreContent.BuyEvent successBuyCB = delegate
		{
			FireBuyWithMoneyEvent(bundle, 1);
		};
		currentContent.TryBuyWithMoney(bundle, successBuyCB, null);
	}

	protected override void BuyWithGold(UIEvent e)
	{
		GeneralStoreContent currentContent = GetCurrentContent();
		NoGTAPPTransactionData bundle = currentContent.GetCurrentGoldBundle();
		if (bundle == null)
		{
			Debug.LogWarning("GeneralStore.OnBuyWithGoldPressed(): SelectedGoldPrice is null");
			return;
		}
		GeneralStoreContent.BuyEvent buyEvent = delegate
		{
			FireBuyWithGoldEventNoGTAPP(bundle);
		};
		currentContent.TryBuyWithGold(buyEvent, buyEvent);
	}

	private void UpdateMoneyButtonState()
	{
		BuyButtonState moneyButtonState = BuyButtonState.ENABLED;
		if (!StoreManager.Get().IsOpen())
		{
			moneyButtonState = BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsBattlePayFeatureEnabled())
		{
			moneyButtonState = BuyButtonState.DISABLED_FEATURE;
		}
		else
		{
			Network.Bundle currentMoneyBundle = GetCurrentContent().GetCurrentMoneyBundle();
			if (currentMoneyBundle == null || StoreManager.Get().IsProductAlreadyOwned(currentMoneyBundle))
			{
				moneyButtonState = BuyButtonState.DISABLED_OWNED;
			}
		}
		SetMoneyButtonState(moneyButtonState);
	}

	private void UpdateGoldButtonState(NetCache.NetCacheGoldBalance balance)
	{
		BuyButtonState goldButtonState = BuyButtonState.ENABLED;
		GeneralStoreContent currentContent = GetCurrentContent();
		if (!(currentContent == null))
		{
			NoGTAPPTransactionData currentGoldBundle = currentContent.GetCurrentGoldBundle();
			long cost;
			if (currentGoldBundle == null)
			{
				goldButtonState = BuyButtonState.DISABLED;
			}
			else if (!StoreManager.Get().IsOpen())
			{
				goldButtonState = BuyButtonState.DISABLED;
			}
			else if (!StoreManager.Get().IsBuyWithGoldFeatureEnabled())
			{
				goldButtonState = BuyButtonState.DISABLED_FEATURE;
			}
			else if (balance == null)
			{
				goldButtonState = BuyButtonState.DISABLED;
			}
			else if (!StoreManager.Get().GetGoldCostNoGTAPP(currentGoldBundle, out cost))
			{
				goldButtonState = BuyButtonState.DISABLED_NO_TOOLTIP;
			}
			else if (balance.GetTotal() < cost)
			{
				goldButtonState = BuyButtonState.DISABLED_NOT_ENOUGH_GOLD;
			}
			SetGoldButtonState(goldButtonState);
		}
	}

	private void UpdateGoldButtonState()
	{
		NetCache.NetCacheGoldBalance netObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
		UpdateGoldButtonState(netObject);
	}

	private void UpdateCostDisplay(NoGTAPPTransactionData goldBundle)
	{
		if (goldBundle == null || !StoreManager.Get().GetGoldCostNoGTAPP(goldBundle, out var cost))
		{
			UpdateCostDisplay(BuyPanelState.BUY_GOLD, string.Empty);
		}
		else
		{
			UpdateCostDisplay(BuyPanelState.BUY_GOLD, cost.ToString());
		}
	}

	private void UpdateCostDisplay(Network.Bundle moneyBundle)
	{
		if (moneyBundle == null)
		{
			UpdateCostDisplay(BuyPanelState.BUY_MONEY, GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_COST_OWNED_TEXT"));
		}
		else
		{
			UpdateCostDisplay(BuyPanelState.BUY_MONEY, StoreManager.Get().FormatCostBundle(moneyBundle));
		}
	}

	private void UpdateCostDisplay(BuyPanelState newPanelState, string costText = "")
	{
		switch (newPanelState)
		{
		case BuyPanelState.BUY_MONEY:
			m_moneyCostText.Text = costText;
			m_moneyCostText.UpdateNow();
			break;
		case BuyPanelState.BUY_GOLD:
			m_goldCostText.Text = costText;
			m_goldCostText.UpdateNow();
			break;
		}
		ShowBuyPanel(newPanelState);
	}

	private void ShowBuyPanel(BuyPanelState setPanelState)
	{
		if (m_buyPanelState != setPanelState)
		{
			GameObject buyPanelObject = GetBuyPanelObject(setPanelState);
			GameObject oldPanelObject = GetBuyPanelObject(m_buyPanelState);
			m_buyPanelState = setPanelState;
			iTween.StopByName(buyPanelObject, "rotation");
			iTween.StopByName(oldPanelObject, "rotation");
			buyPanelObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			oldPanelObject.transform.localEulerAngles = Vector3.zero;
			buyPanelObject.SetActive(value: true);
			iTween.RotateTo(oldPanelObject, iTween.Hash("rotation", new Vector3(0f, 0f, 180f), "isLocal", true, "time", FLIP_BUY_PANEL_ANIM_TIME, "easeType", iTween.EaseType.linear, "oncomplete", (Action<object>)delegate
			{
				oldPanelObject.SetActive(value: false);
			}, "name", "rotation"));
			iTween.RotateTo(buyPanelObject, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "isLocal", true, "time", FLIP_BUY_PANEL_ANIM_TIME, "easeType", iTween.EaseType.linear, "name", "rotation"));
			SoundManager.Get().LoadAndPlay((setPanelState == BuyPanelState.BUY_GOLD) ? "gold_spend_plate_flip_on.prefab:e490542c7405fce45a46c7b9aad5aeab" : "gold_spend_plate_flip_off.prefab:8e19277d18c845547af53064aade9b2c");
		}
	}

	private GameObject GetBuyPanelObject(BuyPanelState buyPanelState)
	{
		return buyPanelState switch
		{
			BuyPanelState.BUY_GOLD => m_buyGoldPanel, 
			BuyPanelState.BUY_MONEY => m_buyMoneyPanel, 
			_ => m_buyEmptyPanel, 
		};
	}

	public void RefreshContent()
	{
		GeneralStoreContent currentContent = GetCurrentContent();
		GeneralStorePane currentPane = GetCurrentPane();
		StoreManager storeManager = StoreManager.Get();
		BlockInterface(storeManager.TransactionInProgress() || storeManager.IsPromptShowing);
		if (currentContent != null)
		{
			currentContent.Refresh();
		}
		if (currentPane != null)
		{
			currentPane.Refresh();
		}
	}

	protected override void Hide(bool animate)
	{
		if (m_settingNewModeCount > 0)
		{
			return;
		}
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		FriendChallengeMgr.Get().OnStoreClosed();
		GeneralStoreContent content = GetContent(GeneralStoreMode.CARDS);
		if (content != null && GetCurrentContent() == content)
		{
			GeneralStorePacksContent generalStorePacksContent = content as GeneralStorePacksContent;
			if (generalStorePacksContent.m_quantityPrompt != null)
			{
				generalStorePacksContent.m_quantityPrompt.Hide();
			}
		}
		ResumePreviousMusicPlaylist();
		DoHideAnimation(!animate, OnHidden);
	}

	protected override void OnHidden()
	{
		m_shown = false;
		foreach (ModeObjects modeObject in m_modeObjects)
		{
			GeneralStorePane pane = modeObject.m_pane;
			GeneralStoreContent content = modeObject.m_content;
			if (pane != null)
			{
				pane.StoreHidden(GetCurrentPane() == pane);
			}
			if (content != null)
			{
				content.StoreHidden(GetCurrentContent() == content);
			}
		}
	}

	private void PreRender()
	{
		if (!m_staticTextResized)
		{
			m_buyWithMoneyButton.m_ButtonText.UpdateNow();
			m_buyWithGoldButton.m_ButtonText.UpdateNow();
			m_staticTextResized = true;
		}
		RefreshContent();
	}

	private bool IsContentFlipClockwise(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < s_ContentOrdering.Length; i++)
		{
			if (s_ContentOrdering[i] == oldMode)
			{
				num = i;
			}
			else if (s_ContentOrdering[i] == newMode)
			{
				num2 = i;
			}
		}
		return num < num2;
	}

	private IEnumerator AnimateAndUpdateStoreMode(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		ResetAnimations();
		while (m_settingNewModeCount > 0)
		{
			yield return null;
		}
		FireModeChangedEvent(oldMode, newMode);
		if (m_currentMode == newMode)
		{
			yield break;
		}
		m_settingNewModeCount++;
		if (m_modeButtonBlocker != null)
		{
			m_modeButtonBlocker.SetActive(value: true);
		}
		UpdateModeButtons(newMode);
		m_currentMode = newMode;
		StartCoroutine(AnimateAndUpdateStorePane(oldMode, newMode));
		GeneralStoreContent prevContent = GetContent(oldMode);
		GeneralStoreContent nextContent = GetContent(newMode);
		if (prevContent != null)
		{
			prevContent.SetContentActive(active: false);
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
		bool flag = IsContentFlipClockwise(oldMode, newMode);
		GetContentPositionIndex(flag, out var contentPosition, out var contentRotation, out var lastPanelRotation, out var newPanelRotation);
		if (nextContent != null)
		{
			nextContent.transform.localPosition = contentPosition;
			nextContent.transform.localEulerAngles = contentRotation;
			nextContent.gameObject.SetActive(value: true);
		}
		iTween.StopByName(m_mainPanel, "PANEL_ROTATION");
		m_mainPanel.transform.localEulerAngles = lastPanelRotation;
		bool rotationDone = false;
		float flipAnimTime = m_contentFlipAnimationTime;
		float num = (flag ? 1f : (-1f));
		ShakeStore(10f * num, 1.5f, flipAnimTime * 0.3f);
		if (!string.IsNullOrEmpty(m_contentFlipSound))
		{
			SoundManager.Get().LoadAndPlay(m_contentFlipSound);
		}
		Action<object> action = delegate
		{
			m_mainPanel.transform.localEulerAngles = newPanelRotation;
			rotationDone = true;
			if (prevContent != null)
			{
				prevContent.gameObject.SetActive(value: false);
			}
		};
		if (flipAnimTime > 0f)
		{
			iTween.RotateBy(m_mainPanel, iTween.Hash("name", "PANEL_ROTATION", "amount", MAIN_PANEL_ANGLE_TO_ROTATE * num, "time", flipAnimTime, "easetype", m_contentFlipEaseType, "oncomplete", action));
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
			UpdateCostAndButtonState(nextContent.GetCurrentGoldBundle(), nextContent.GetCurrentMoneyBundle());
			while (!nextContent.AnimateEntranceStart())
			{
				yield return null;
			}
			while (!nextContent.AnimateEntranceEnd())
			{
				yield return null;
			}
			nextContent.SetContentActive(active: true);
			nextContent.PostStoreFlipIn(flipAnimTime > 0f);
		}
		if (prevContent != null)
		{
			prevContent.PostStoreFlipOut();
		}
		m_settingNewModeCount--;
		RefreshContent();
		while (m_settingNewModeCount > 0)
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
		if (m_modeButtonBlocker != null)
		{
			m_modeButtonBlocker.SetActive(value: false);
		}
		if (newMode == GeneralStoreMode.NONE)
		{
			ResumePreviousMusicPlaylist();
		}
	}

	private IEnumerator AnimateHideStorePane(bool hide)
	{
		GeneralStorePane prevPane;
		GeneralStorePane nextPane;
		if (hide)
		{
			prevPane = GetPane(GeneralStoreMode.CARDS);
			nextPane = m_defaultPane;
		}
		else
		{
			prevPane = m_defaultPane;
			nextPane = GetPane(GeneralStoreMode.CARDS);
		}
		m_settingNewModeCount++;
		if (prevPane != null)
		{
			prevPane.PrePaneSwappedOut();
			while (!prevPane.AnimateExitStart())
			{
				yield return null;
			}
			while (!prevPane.AnimateExitEnd())
			{
				yield return null;
			}
			prevPane.PostPaneSwappedOut();
		}
		if (m_paneSwapAnimationTime > 0f)
		{
			if (!string.IsNullOrEmpty(m_contentFlipSound))
			{
				SoundManager.Get().LoadAndPlay(m_contentFlipSound);
			}
			int swapCount = 0;
			float num = 0f;
			if (prevPane != null)
			{
				swapCount++;
				prevPane.gameObject.SetActive(value: true);
				prevPane.transform.localPosition = Vector3.zero;
				iTween.MoveTo(prevPane.gameObject, iTween.Hash("position", m_paneSwapOutOffset, "islocal", true, "time", m_paneSwapAnimationTime, "easetype", iTween.EaseType.linear, "oncomplete", (Action<object>)delegate
				{
					if (prevPane != null && prevPane.gameObject != null)
					{
						prevPane.gameObject.SetActive(value: false);
					}
					swapCount--;
				}));
				num = m_paneSwapAnimationTime;
			}
			if (nextPane != null)
			{
				swapCount++;
				nextPane.gameObject.SetActive(value: true);
				nextPane.transform.localPosition = m_paneSwapInOffset;
				iTween.MoveTo(nextPane.gameObject, iTween.Hash("position", Vector3.zero, "islocal", true, "time", m_paneSwapAnimationTime, "delay", num, "oncomplete", (Action<object>)delegate
				{
					swapCount--;
				}));
			}
			while (swapCount > 0)
			{
				yield return null;
			}
		}
		else
		{
			prevPane.transform.localPosition = m_paneSwapOutOffset;
			nextPane.transform.localPosition = Vector3.zero;
			prevPane.gameObject.SetActive(value: false);
			nextPane.gameObject.SetActive(value: true);
		}
		m_settingNewModeCount--;
	}

	private IEnumerator AnimateAndUpdateStorePane(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		GeneralStorePane prevPane = GetPane(oldMode);
		GeneralStorePane nextPane = GetPane(newMode);
		if (oldMode == newMode)
		{
			yield break;
		}
		m_settingNewModeCount++;
		if (m_paneScrollbar != null)
		{
			m_paneScrollbar.SaveScroll("STORE_MODE_" + oldMode);
			m_paneScrollbar.m_ScrollObject = null;
		}
		if (m_paneScrollbar != null && nextPane != null && nextPane.m_paneContainer != null)
		{
			m_paneStartPositions.TryGetValue(newMode, out var value);
			m_paneScrollbar.m_ScrollObject = nextPane.m_paneContainer;
			m_paneScrollbar.ResetScrollStartPosition(value);
			m_paneScrollbar.LoadScroll("STORE_MODE_" + newMode, snap: true);
			m_paneScrollbar.EnableIfNeeded();
		}
		if (prevPane != null)
		{
			prevPane.PrePaneSwappedOut();
			while (!prevPane.AnimateExitStart())
			{
				yield return null;
			}
			while (!prevPane.AnimateExitEnd())
			{
				yield return null;
			}
			prevPane.PostPaneSwappedOut();
		}
		if (m_paneSwapAnimationTime > 0f)
		{
			int swapCount = 0;
			float num = 0f;
			if (prevPane != null)
			{
				swapCount++;
				prevPane.transform.localPosition = Vector3.zero;
				prevPane.gameObject.SetActive(value: true);
				iTween.MoveTo(prevPane.gameObject, iTween.Hash("position", m_paneSwapOutOffset, "islocal", true, "time", m_paneSwapAnimationTime, "easetype", iTween.EaseType.linear, "oncomplete", (Action<object>)delegate
				{
					if (prevPane != null && prevPane.gameObject != null)
					{
						prevPane.gameObject.SetActive(value: false);
					}
					swapCount--;
				}));
				num = m_paneSwapAnimationTime;
			}
			if (nextPane != null)
			{
				swapCount++;
				nextPane.transform.localPosition = m_paneSwapInOffset;
				nextPane.gameObject.SetActive(value: true);
				iTween.MoveTo(nextPane.gameObject, iTween.Hash("position", Vector3.zero, "islocal", true, "time", m_paneSwapAnimationTime, "delay", num, "oncomplete", (Action<object>)delegate
				{
					swapCount--;
				}));
			}
			while (swapCount > 0)
			{
				yield return null;
			}
		}
		else
		{
			prevPane.transform.localPosition = m_paneSwapOutOffset;
			nextPane.transform.localPosition = Vector3.zero;
			prevPane.gameObject.SetActive(value: false);
			nextPane.gameObject.SetActive(value: true);
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
		m_settingNewModeCount--;
	}

	private void ResetAnimations()
	{
		if (m_shakePane != null)
		{
			m_shakePane.Reset();
		}
	}

	private void UpdateModeButtons(GeneralStoreMode mode)
	{
		foreach (ModeObjects modeObject in m_modeObjects)
		{
			if (modeObject.m_button == null)
			{
				continue;
			}
			UIBHighlight component = modeObject.m_button.GetComponent<UIBHighlight>();
			if (!(component == null))
			{
				if (mode == modeObject.m_mode)
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

	private void GetContentPositionIndex(bool clockwise, out Vector3 contentPosition, out Vector3 contentRotation, out Vector3 lastPanelRotation, out Vector3 newPanelRotation)
	{
		lastPanelRotation = s_MainPanelTriangularRotations[m_currentContentPositionIdx];
		if (clockwise)
		{
			m_currentContentPositionIdx = (m_currentContentPositionIdx + 1) % s_ContentTriangularPositions.Length;
		}
		else
		{
			m_currentContentPositionIdx--;
			if (m_currentContentPositionIdx < 0)
			{
				m_currentContentPositionIdx = s_ContentTriangularPositions.Length - 1;
			}
		}
		contentPosition = s_ContentTriangularPositions[m_currentContentPositionIdx];
		contentRotation = s_ContentTriangularRotations[m_currentContentPositionIdx];
		newPanelRotation = s_MainPanelTriangularRotations[m_currentContentPositionIdx];
	}

	private void SuccessfulPurchaseAckEvent(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (IsShown() && SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE)
		{
			Close();
		}
		else
		{
			RefreshContent();
		}
	}

	private void UpdateCostAndButtonState(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
		if (moneyBundle != null && !StoreManager.Get().IsProductAlreadyOwned(moneyBundle))
		{
			UpdateCostDisplay(moneyBundle);
			UpdateMoneyButtonState();
			return;
		}
		if (goldBundle != null)
		{
			UpdateCostDisplay(goldBundle);
			UpdateGoldButtonState();
			return;
		}
		GeneralStoreContent currentContent = GetCurrentContent();
		if (currentContent == null || currentContent.IsPurchaseDisabled())
		{
			UpdateCostDisplay(BuyPanelState.DISABLED);
			return;
		}
		UpdateCostDisplay(BuyPanelState.BUY_MONEY, currentContent.GetMoneyDisplayOwnedText());
		UpdateMoneyButtonState();
	}

	private void FireModeChangedEvent(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		ModeChanged[] array = m_modeChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](oldMode, newMode);
		}
	}

	private bool OnNavigateBack()
	{
		CloseImpl(closeWithAnimation: true);
		return true;
	}

	private void CloseImpl(bool closeWithAnimation)
	{
		if (m_settingNewModeCount <= 0)
		{
			PresenceMgr.Get().SetPrevStatus();
			Hide(closeWithAnimation);
			SoundManager.Get().LoadAndPlay("Store_window_shrink.prefab:b68247126e211224e8a904142d2a9895", base.gameObject);
			EnableFullScreenEffects(enable: false);
			FireExitEvent(authorizationBackButtonPressed: false);
		}
	}

	protected override string GetOwnedTooltipString()
	{
		return m_currentMode switch
		{
			GeneralStoreMode.CARDS => GameStrings.Get("GLUE_STORE_PACK_BUTTON_TEXT_PURCHASED"), 
			GeneralStoreMode.ADVENTURE => GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_TEXT_PURCHASED"), 
			GeneralStoreMode.HEROES => GameStrings.Get("GLUE_STORE_HERO_BUTTON_TEXT_PURCHASED"), 
			_ => string.Empty, 
		};
	}
}
