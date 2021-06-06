using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class HeroSkinInfoManager : MonoBehaviour, IStore
{
	private const string STATE_SHOW_VANILLA_HERO = "SHOW_VANILLA_HERO";

	private const string STATE_SHOW_NEW_HERO = "SHOW_NEW_HERO";

	private const string STATE_INVALID_HERO = "INVALID_HERO";

	private const string STATE_HIDDEN = "HIDDEN";

	private const string MAKE_FAVORITE_STATE = "MAKE_FAVORITE";

	private const string SUFFICIENT_CURRENCY_STATE = "SUFFICIENT_CURRENCY";

	private const string INSUFFICIENT_CURRENCY_STATE = "INSUFFICIENT_CURRENCY";

	private const string DISABLED_STATE = "DISABLED";

	private const string STATE_BLOCK_SCREEN = "BLOCK_SCREEN";

	private const string STATE_UNBLOCK_SCREEN = "UNBLOCK_SCREEN";

	public GameObject m_previewPane;

	public GameObject m_vanillaHeroFrame;

	public MeshRenderer m_vanillaHeroPreviewQuad;

	public UberText m_vanillaHeroTitle;

	public UberText m_vanillaHeroDescription;

	public UIBButton m_vanillaHeroFavoriteButton;

	public UIBButton m_vanillaHeroBuyButton;

	public GameObject m_newHeroFrame;

	public MeshRenderer m_newHeroPreviewQuad;

	public UberText m_newHeroTitle;

	public UberText m_newHeroDescription;

	public UIBButton m_newHeroFavoriteButton;

	public UIBButton m_newHeroBuyButton;

	public PegUIElement m_offClicker;

	public float m_animationTime = 0.5f;

	public Material m_defaultPreviewMaterial;

	public Material m_vanillaHeroNonPremiumMaterial;

	public AsyncReference m_visibilityVisualControllerReference;

	public AsyncReference m_userActionVisualControllerReference;

	public AsyncReference m_vanillaHeroCurrencyIconWidgetReference;

	public AsyncReference m_newHeroCurrencyIconWidgetReference;

	public AsyncReference m_fullScreenBlockerWidgetReference;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_enterPreviewSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_exitPreviewSound;

	public MusicPlaylistType m_defaultHeroMusic = MusicPlaylistType.UI_CMHeroSkinPreview;

	private string m_currentCardId;

	private DefLoader.DisposableCardDef m_currentHeroCardDef;

	private CollectionHeroDef m_currentHeroDef;

	private AssetHandle<UberShaderAnimation> m_currentHeroGoldenAnimation;

	private CardHeroDbfRecord m_currentHeroRecord;

	private EntityDef m_currentEntityDef;

	private TAG_PREMIUM m_currentPremium;

	private static HeroSkinInfoManager s_instance;

	private static bool s_isReadyingInstance;

	private bool m_animating;

	private bool m_hasEnteredHeroSkinPreview;

	private MusicPlaylistType m_prevPlaylist;

	private string m_desiredVisibilityState = "INVALID_HERO";

	private VisualController m_visibilityVisualController;

	private VisualController m_userActionVisualController;

	private Widget m_fullScreenBlockerWidget;

	private Widget m_vanillaHeroCurrencyButtonWidget;

	private Widget m_newHeroCurrencyButtonWidget;

	private bool m_isStoreOpen;

	private bool m_isStoreTransactionActive;

	public bool IsShowingPreview
	{
		get
		{
			if (!m_animating)
			{
				return m_hasEnteredHeroSkinPreview;
			}
			return true;
		}
	}

	public event Action OnOpened;

	public event Action<StoreClosedArgs> OnClosed;

	public event Action OnReady;

	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	public static HeroSkinInfoManager Get()
	{
		return s_instance;
	}

	public static void EnterPreviewWhenReady(CollectionCardVisual cardVisual)
	{
		HeroSkinInfoManager heroSkinInfoManager = Get();
		if (heroSkinInfoManager != null)
		{
			heroSkinInfoManager.EnterPreview(cardVisual);
			return;
		}
		if (s_isReadyingInstance)
		{
			Debug.LogWarning("HeroSkinInfoManager:EnterPreviewWhenReady called while the info manager instance was being readied");
			return;
		}
		string assetString = "HeroSkinInfoManager.prefab:9d5b641eb672c491f8cbd2f20d2cbb61";
		Widget widget = WidgetInstance.Create(assetString);
		if (widget == null)
		{
			Debug.LogError("HeroSkinInfoManager:EnterPreviewWhenReady failed to create widget instance");
			return;
		}
		s_isReadyingInstance = true;
		widget.RegisterReadyListener(delegate
		{
			s_instance = widget.GetComponentInChildren<HeroSkinInfoManager>();
			s_isReadyingInstance = false;
			if (s_instance == null)
			{
				Debug.LogError("HeroSkinInfoManager:EnterPreviewWhenReady created widget instance but failed to get HeroSkinInfoManager component");
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
		return s_instance.IsShowingPreview;
	}

	private void Awake()
	{
		m_previewPane.SetActive(value: false);
		SetupUI();
	}

	private void Start()
	{
		m_visibilityVisualControllerReference.RegisterReadyListener<VisualController>(OnVisibilityVisualControllerReady);
		m_userActionVisualControllerReference.RegisterReadyListener<VisualController>(OnUserActionVisualControllerReady);
		m_fullScreenBlockerWidgetReference.RegisterReadyListener<Widget>(OnFullScreenBlockerWidgetReady);
		m_vanillaHeroCurrencyIconWidgetReference.RegisterReadyListener<Widget>(OnVanillaHeroCurrencyButtonWidgetReady);
		m_newHeroCurrencyIconWidgetReference.RegisterReadyListener<Widget>(OnNewHeroCurrencyButtonWidgetReady);
	}

	private void OnDestroy()
	{
		m_currentHeroCardDef?.Dispose();
		m_currentHeroCardDef = null;
		AssetHandle.SafeDispose(ref m_currentHeroGoldenAnimation);
		CancelPreview();
		s_instance = null;
	}

	public void EnterPreview(CollectionCardVisual cardVisual)
	{
		if (m_animating)
		{
			return;
		}
		Actor actor = cardVisual.GetActor();
		if (actor == null)
		{
			Log.CollectionManager.PrintError("HeroSkinInfoManager.EnterPreview - Could not get actor from card visual. Not displaying preview");
			return;
		}
		m_currentEntityDef = actor.GetEntityDef();
		m_currentPremium = actor.GetPremium();
		if (m_currentEntityDef == null)
		{
			Log.CollectionManager.PrintError("HeroSkinInfoManager.EnterPreview - Actor entity def not set. Not displaying preview");
			return;
		}
		Navigation.PushUnique(OnNavigateBack);
		string cardId = m_currentEntityDef.GetCardId();
		m_currentHeroRecord = GameDbf.CardHero.GetRecords().Find((CardHeroDbfRecord r) => GameUtils.TranslateDbIdToCardId(r.CardId) == cardId);
		bool num = CollectionManager.GetHeroCardId(m_currentEntityDef.GetClass(), CardHero.HeroType.HONORED) == cardId;
		bool flag = GameUtils.IsVanillaHero(cardId);
		bool flag2 = (actor.PremiumAnimationAvailable && flag && m_currentPremium == TAG_PREMIUM.GOLDEN) || !flag;
		bool flag3 = num || flag;
		if (LoadHeroDef(cardId))
		{
			if (m_currentHeroDef.m_collectionManagerPreviewEmote != 0)
			{
				GameUtils.LoadCardDefEmoteSound(cardVisual.GetActor().EmoteDefs, m_currentHeroDef.m_collectionManagerPreviewEmote, delegate(CardSoundSpell cardSpell)
				{
					if (cardSpell != null)
					{
						cardSpell.AddFinishedCallback(delegate
						{
							UnityEngine.Object.Destroy(cardSpell.gameObject);
						});
						cardSpell.Reactivate();
					}
				});
			}
			flag3 = flag3 || !m_currentHeroDef.m_previewMaterial.IsInitialized();
			if ((flag2 ? cardVisual.GetActor().PremiumPortraitMaterial : null) == null)
			{
				m_currentHeroDef.m_previewMaterial.GetMaterial();
				flag3 = true;
			}
			Material material = null;
			if (!flag3)
			{
				material = m_currentHeroDef.m_previewMaterial.GetMaterial();
			}
			Texture portraitTexture = cardVisual.GetActor().GetPortraitTexture();
			flag3 = flag3 || material == null;
			if (!flag3)
			{
				string heroUberShaderAnimationPath = m_currentHeroDef.GetHeroUberShaderAnimationPath();
				bool num2 = !string.IsNullOrWhiteSpace(heroUberShaderAnimationPath);
				if (num2)
				{
					AssetHandle.SafeDispose(ref m_currentHeroGoldenAnimation);
					AssetLoader.Get().LoadAsset(ref m_currentHeroGoldenAnimation, heroUberShaderAnimationPath);
				}
				if (num2 && m_currentHeroGoldenAnimation == null)
				{
					Debug.LogWarning($"HeroSkinInfoManager.EnterPreview - {cardId} hero shader could not be loaded {heroUberShaderAnimationPath}");
				}
			}
			if (flag3)
			{
				m_vanillaHeroTitle.Text = m_currentEntityDef.GetName();
				m_vanillaHeroDescription.Text = m_currentHeroRecord.Description;
				material = m_vanillaHeroNonPremiumMaterial;
				if (flag2)
				{
					Material premiumPortraitMaterial = cardVisual.GetActor().PremiumPortraitMaterial;
					if (premiumPortraitMaterial != null)
					{
						material = premiumPortraitMaterial;
						portraitTexture = material.mainTexture;
					}
					else
					{
						Log.CollectionManager.PrintWarning($"HeroSkinInfoManager.EnterPreview - premium material missing for {cardId}");
					}
				}
				AssignVanillaHeroPreviewMaterial(material, portraitTexture, cardVisual.GetActor().PremiumPortraitAnimation);
			}
			else
			{
				m_newHeroTitle.Text = m_currentEntityDef.GetName();
				m_newHeroDescription.Text = m_currentHeroRecord.Description;
				AssignNewHeroPreviewMaterial(material, portraitTexture, m_currentHeroGoldenAnimation);
			}
			m_desiredVisibilityState = (flag3 ? "SHOW_VANILLA_HERO" : "SHOW_NEW_HERO");
			m_hasEnteredHeroSkinPreview = true;
			m_previewPane.SetActive(value: true);
			m_offClicker.gameObject.SetActive(value: true);
			m_animating = true;
			iTween.ScaleFrom(m_previewPane, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", m_animationTime, "easeType", iTween.EaseType.easeOutCirc, "oncomplete", (Action<object>)delegate
			{
				m_animating = false;
			}));
			SetupHeroSkinStore();
			UpdateView();
			if (!string.IsNullOrEmpty(m_enterPreviewSound))
			{
				SoundManager.Get().LoadAndPlay(m_enterPreviewSound);
			}
			PlayHeroMusic();
			FullScreenFXMgr.Get().StartStandardBlurVignette(m_animationTime);
		}
		else
		{
			Debug.LogError("Could not load entity def for hero skin, preview will not be shown. Set the CollectionHeroDefPath on the HERO_0X.prefab.");
			m_desiredVisibilityState = "INVALID_HERO";
			SetupHeroSkinStore();
			UpdateView();
		}
	}

	public void CancelPreview()
	{
		Navigation.RemoveHandler(OnNavigateBack);
		if (m_animating || !m_hasEnteredHeroSkinPreview)
		{
			return;
		}
		m_hasEnteredHeroSkinPreview = false;
		ShutDownHeroSkinStore();
		Vector3 origScale = m_previewPane.transform.localScale;
		m_animating = true;
		iTween.ScaleTo(m_previewPane, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", m_animationTime, "easeType", iTween.EaseType.easeOutCirc, "oncomplete", (Action<object>)delegate
		{
			m_animating = false;
			if ((bool)m_previewPane)
			{
				m_previewPane.transform.localScale = origScale;
				m_previewPane.SetActive(value: false);
			}
			if ((bool)m_offClicker)
			{
				m_offClicker.gameObject.SetActive(value: false);
			}
		}));
		if (!string.IsNullOrEmpty(m_exitPreviewSound))
		{
			SoundManager.Get().LoadAndPlay(m_exitPreviewSound);
		}
		StopHeroMusic();
		FullScreenFXMgr.Get().EndStandardBlurVignette(m_animationTime);
	}

	private void OnVisibilityVisualControllerReady(VisualController visualController)
	{
		m_visibilityVisualController = visualController;
		UpdateView();
		if (this.OnReady != null)
		{
			this.OnReady();
		}
	}

	private void OnUserActionVisualControllerReady(VisualController visualController)
	{
		m_userActionVisualController = visualController;
		UpdateView();
	}

	private void OnFullScreenBlockerWidgetReady(Widget fullScreenBlockerWidget)
	{
		m_fullScreenBlockerWidget = fullScreenBlockerWidget;
		UpdateView();
	}

	private void OnVanillaHeroCurrencyButtonWidgetReady(Widget currencyButtonWidget)
	{
		m_vanillaHeroCurrencyButtonWidget = currencyButtonWidget;
		UpdateView();
	}

	private void OnNewHeroCurrencyButtonWidgetReady(Widget currencyButtonWidget)
	{
		m_newHeroCurrencyButtonWidget = currencyButtonWidget;
		UpdateView();
	}

	private void UpdateView()
	{
		if (m_visibilityVisualController == null || m_userActionVisualController == null || m_fullScreenBlockerWidget == null || m_vanillaHeroCurrencyButtonWidget == null || m_newHeroCurrencyButtonWidget == null || m_currentEntityDef == null || m_currentHeroRecord == null || string.IsNullOrEmpty(m_currentCardId))
		{
			return;
		}
		m_visibilityVisualController.SetState(m_desiredVisibilityState);
		bool flag = false;
		if (CollectionManager.Get().IsInEditMode())
		{
			m_userActionVisualController.SetState("DISABLED");
		}
		else if (HeroSkinUtils.IsHeroSkinOwned(m_currentEntityDef.GetCardId()))
		{
			m_userActionVisualController.SetState("MAKE_FAVORITE");
		}
		else if (!HeroSkinUtils.IsHeroSkinPurchasableFromCollectionManager(m_currentEntityDef.GetCardId()))
		{
			m_userActionVisualController.SetState("DISABLED");
		}
		else
		{
			PriceDataModel collectionManagerHeroSkinPriceDataModel = HeroSkinUtils.GetCollectionManagerHeroSkinPriceDataModel(m_currentEntityDef.GetCardId());
			m_userActionVisualController.BindDataModel(collectionManagerHeroSkinPriceDataModel);
			if (!HeroSkinUtils.CanBuyHeroSkinFromCollectionManager(m_currentEntityDef.GetCardId()))
			{
				m_userActionVisualController.SetState("INSUFFICIENT_CURRENCY");
			}
			else
			{
				m_userActionVisualController.SetState("SUFFICIENT_CURRENCY");
				flag = true;
			}
		}
		bool faceUp = HeroSkinUtils.CanFavoriteHeroSkin(m_currentEntityDef.GetClass(), m_currentEntityDef.GetCardId());
		UIBButton obj = ((m_desiredVisibilityState == "SHOW_VANILLA_HERO") ? m_vanillaHeroFavoriteButton : m_newHeroFavoriteButton);
		obj.SetEnabled(faceUp);
		obj.Flip(faceUp);
		UIBButton obj2 = ((m_desiredVisibilityState == "SHOW_VANILLA_HERO") ? m_vanillaHeroBuyButton : m_newHeroBuyButton);
		obj2.SetEnabled(flag);
		obj2.Flip(faceUp: true);
	}

	private static bool OnNavigateBack()
	{
		HeroSkinInfoManager heroSkinInfoManager = Get();
		if (heroSkinInfoManager != null)
		{
			heroSkinInfoManager.CancelPreview();
		}
		return true;
	}

	private bool LoadHeroDef(string cardId)
	{
		if (m_currentCardId == cardId && string.IsNullOrEmpty(cardId))
		{
			return true;
		}
		m_currentHeroCardDef?.Dispose();
		m_currentHeroCardDef = DefLoader.Get().GetCardDef(cardId);
		if (m_currentHeroCardDef?.CardDef == null || string.IsNullOrEmpty(m_currentHeroCardDef.CardDef.m_CollectionHeroDefPath))
		{
			return false;
		}
		CollectionHeroDef collectionHeroDef = GameUtils.LoadGameObjectWithComponent<CollectionHeroDef>(m_currentHeroCardDef.CardDef.m_CollectionHeroDefPath);
		if (collectionHeroDef == null)
		{
			Debug.LogWarning($"Hero def does not exist on object: {m_currentHeroCardDef.CardDef.m_CollectionHeroDefPath}");
			return false;
		}
		if (m_currentHeroDef != null)
		{
			UnityEngine.Object.Destroy(m_currentHeroDef.gameObject);
		}
		m_currentCardId = cardId;
		m_currentHeroDef = collectionHeroDef;
		return true;
	}

	private void SetupUI()
	{
		m_newHeroFavoriteButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			SetFavoriteHero();
			CancelPreview();
		});
		if (m_vanillaHeroFavoriteButton != null && m_vanillaHeroFavoriteButton != m_newHeroFavoriteButton)
		{
			m_vanillaHeroFavoriteButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				SetFavoriteHero();
				CancelPreview();
			});
		}
		m_newHeroBuyButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnBuyButtonReleased();
		});
		if (m_vanillaHeroBuyButton != null && m_vanillaHeroBuyButton != m_newHeroBuyButton)
		{
			m_vanillaHeroBuyButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnBuyButtonReleased();
			});
		}
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
		if (!IsHeroSkinCardIdValid())
		{
			Debug.LogError("HeroSkinInfoManager:OnBuyButtonReleased called when the hero skin card id was invalid");
			return;
		}
		m_visibilityVisualController.SetState("HIDDEN");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Format("GLUE_HERO_SKIN_PURCHASE_CONFIRMATION_HEADER");
		popupInfo.m_text = GameStrings.Format("GLUE_HERO_SKIN_PURCHASE_CONFIRMATION_MESSAGE", m_currentEntityDef.GetName());
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
				UpdateView();
			}
		});
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void SetFavoriteHero()
	{
		NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition
		{
			Name = m_currentEntityDef.GetCardId(),
			Premium = m_currentPremium
		};
		TAG_CLASS @class = m_currentEntityDef.GetClass();
		Network.Get().SetFavoriteHero(@class, cardDefinition);
		if (!Network.IsLoggedIn())
		{
			CollectionManager.Get().UpdateFavoriteHero(@class, cardDefinition.Name, m_currentPremium);
		}
	}

	private void AssignVanillaHeroPreviewMaterial(Material material, Texture portraitTexture, UberShaderAnimation portraitAnimation)
	{
		Renderer component = m_vanillaHeroPreviewQuad.GetComponent<Renderer>();
		if (portraitTexture != null)
		{
			component.SetMaterial(1, material);
			component.GetMaterial(1).SetTexture("_MainTex", portraitTexture);
		}
		else
		{
			component.SetMaterial(1, material);
		}
		AssignVanillaHeroUberAnimation(portraitAnimation);
	}

	private void AssignNewHeroPreviewMaterial(Material material, Texture portraitTexture, UberShaderAnimation portraitAnimation)
	{
		Renderer component = m_newHeroPreviewQuad.GetComponent<Renderer>();
		if (material != null)
		{
			component.SetMaterial(material);
		}
		else
		{
			component.SetMaterial(m_defaultPreviewMaterial);
			component.GetMaterial().mainTexture = portraitTexture;
		}
		AssignNewHeroUberAnimation(portraitAnimation);
	}

	private void AssignVanillaHeroUberAnimation(UberShaderAnimation portraitAnimation)
	{
		UberShaderController uberShaderController = m_vanillaHeroPreviewQuad.GetComponent<UberShaderController>();
		if (portraitAnimation != null)
		{
			if (uberShaderController == null)
			{
				uberShaderController = m_vanillaHeroPreviewQuad.gameObject.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate(portraitAnimation);
			uberShaderController.m_MaterialIndex = 1;
			uberShaderController.enabled = true;
		}
		else if (uberShaderController != null)
		{
			UnityEngine.Object.Destroy(uberShaderController);
		}
	}

	private void AssignNewHeroUberAnimation(UberShaderAnimation portraitAnimation)
	{
		UberShaderController uberShaderController = m_newHeroPreviewQuad.GetComponent<UberShaderController>();
		if (portraitAnimation != null)
		{
			if (uberShaderController == null)
			{
				uberShaderController = m_newHeroPreviewQuad.gameObject.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate(portraitAnimation);
			uberShaderController.m_MaterialIndex = 0;
			uberShaderController.enabled = true;
		}
		else if (uberShaderController != null)
		{
			UnityEngine.Object.Destroy(uberShaderController);
		}
	}

	private void PlayHeroMusic()
	{
		MusicPlaylistType musicPlaylistType = ((!(m_currentHeroDef == null) && m_currentHeroDef.m_heroPlaylist != 0) ? m_currentHeroDef.m_heroPlaylist : m_defaultHeroMusic);
		if (musicPlaylistType != 0)
		{
			m_prevPlaylist = MusicManager.Get().GetCurrentPlaylist();
			MusicManager.Get().StartPlaylist(musicPlaylistType);
		}
	}

	private void StopHeroMusic()
	{
		MusicManager.Get().StartPlaylist(m_prevPlaylist);
	}

	private void BlockInputs(bool blocked)
	{
		if (m_fullScreenBlockerWidget == null)
		{
			Debug.LogError("Failed to toggle interface blocker from Duels Popup Manager");
			return;
		}
		m_fullScreenBlockerWidget.TriggerEvent(blocked ? "BLOCK_SCREEN" : "UNBLOCK_SCREEN", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = false
		});
	}

	private bool IsHeroSkinCardIdValid()
	{
		if (m_currentEntityDef != null)
		{
			return !string.IsNullOrEmpty(m_currentEntityDef.GetCardId());
		}
		return false;
	}

	private void SetupHeroSkinStore()
	{
		if (m_isStoreOpen)
		{
			Debug.LogError("CardBackInfoManager:SetupHeroSkinStore called when the store was already open");
			return;
		}
		if (m_currentHeroRecord == null)
		{
			Debug.LogError("CardBackInfoManager:SetupHeroSkinStore: m_currentHeroRecord was null");
			return;
		}
		StoreManager storeManager = StoreManager.Get();
		if (storeManager.IsOpen())
		{
			storeManager.SetupHeroSkinStore(this, m_currentHeroRecord.CardId);
			storeManager.RegisterSuccessfulPurchaseListener(OnSuccessfulPurchase);
			storeManager.RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
			storeManager.RegisterFailedPurchaseAckListener(OnFailedPurchaseAck);
			BnetBar.Get()?.RefreshCurrency();
		}
	}

	private void ShutDownHeroSkinStore()
	{
		if (m_isStoreOpen)
		{
			CancelPurchaseTransaction();
			this.OnClosed?.Invoke(new StoreClosedArgs());
			StoreManager storeManager = StoreManager.Get();
			storeManager.RemoveFailedPurchaseAckListener(OnFailedPurchaseAck);
			storeManager.RemoveSuccessfulPurchaseListener(OnSuccessfulPurchase);
			storeManager.RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
			storeManager.ShutDownHeroSkinStore();
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
		UpdateView();
	}

	private void OnFailedPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		EndPurchaseTransaction();
		UpdateView();
	}

	private void StartPurchaseTransaction()
	{
		if (!IsHeroSkinCardIdValid())
		{
			Debug.LogError("HeroSkinInfoManager:StartPurchaseTransaction called when the hero skin card id was invalid");
		}
		else if (m_isStoreTransactionActive)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_HERO_SKIN_PURCHASE_ERROR_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Debug.LogWarning("Attempted to start a hero skin purchase transaction while an existing transaction was in progress (CardId = " + m_currentEntityDef.GetCardId() + ")");
		}
		else if (this.OnProductPurchaseAttempt == null)
		{
			Debug.LogError("Attempted to start a hero skin purchase transaction while OnProductPurchaseAttempt was null (CardId = " + m_currentEntityDef.GetCardId() + ")");
		}
		else
		{
			Network.Bundle collectionManagerHeroSkinProductBundle = HeroSkinUtils.GetCollectionManagerHeroSkinProductBundle(m_currentEntityDef.GetCardId());
			if (collectionManagerHeroSkinProductBundle == null)
			{
				Debug.LogError("Attempted to start a hero skin purchase transaction with a null bundle (CardId = " + m_currentEntityDef.GetCardId() + ")");
				return;
			}
			m_isStoreTransactionActive = true;
			this.OnProductPurchaseAttempt(new BuyPmtProductEventArgs(collectionManagerHeroSkinProductBundle, CurrencyType.GOLD, 1));
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
			Debug.LogError("HeroSkinInfoManager:IStore.Open: Could not get the Bnet bar to reflect the required currency");
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
