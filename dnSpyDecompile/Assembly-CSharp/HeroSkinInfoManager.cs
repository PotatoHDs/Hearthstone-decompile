using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200012B RID: 299
[CustomEditClass]
public class HeroSkinInfoManager : MonoBehaviour, IStore
{
	// Token: 0x1400001A RID: 26
	// (add) Token: 0x060013C2 RID: 5058 RVA: 0x000719CC File Offset: 0x0006FBCC
	// (remove) Token: 0x060013C3 RID: 5059 RVA: 0x00071A04 File Offset: 0x0006FC04
	public event Action OnOpened;

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x060013C4 RID: 5060 RVA: 0x00071A3C File Offset: 0x0006FC3C
	// (remove) Token: 0x060013C5 RID: 5061 RVA: 0x00071A74 File Offset: 0x0006FC74
	public event Action<StoreClosedArgs> OnClosed;

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x060013C6 RID: 5062 RVA: 0x00071AAC File Offset: 0x0006FCAC
	// (remove) Token: 0x060013C7 RID: 5063 RVA: 0x00071AE4 File Offset: 0x0006FCE4
	public event Action OnReady;

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x060013C8 RID: 5064 RVA: 0x00071B1C File Offset: 0x0006FD1C
	// (remove) Token: 0x060013C9 RID: 5065 RVA: 0x00071B54 File Offset: 0x0006FD54
	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x060013CA RID: 5066 RVA: 0x00071B89 File Offset: 0x0006FD89
	public bool IsShowingPreview
	{
		get
		{
			return this.m_animating || this.m_hasEnteredHeroSkinPreview;
		}
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x00071B9B File Offset: 0x0006FD9B
	public static HeroSkinInfoManager Get()
	{
		return HeroSkinInfoManager.s_instance;
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x00071BA4 File Offset: 0x0006FDA4
	public static void EnterPreviewWhenReady(CollectionCardVisual cardVisual)
	{
		HeroSkinInfoManager heroSkinInfoManager = HeroSkinInfoManager.Get();
		if (heroSkinInfoManager != null)
		{
			heroSkinInfoManager.EnterPreview(cardVisual);
			return;
		}
		if (HeroSkinInfoManager.s_isReadyingInstance)
		{
			Debug.LogWarning("HeroSkinInfoManager:EnterPreviewWhenReady called while the info manager instance was being readied");
			return;
		}
		string assetString = "HeroSkinInfoManager.prefab:9d5b641eb672c491f8cbd2f20d2cbb61";
		Widget widget = WidgetInstance.Create(assetString, false);
		if (widget == null)
		{
			Debug.LogError("HeroSkinInfoManager:EnterPreviewWhenReady failed to create widget instance");
			return;
		}
		HeroSkinInfoManager.s_isReadyingInstance = true;
		widget.RegisterReadyListener(delegate(object _)
		{
			HeroSkinInfoManager.s_instance = widget.GetComponentInChildren<HeroSkinInfoManager>();
			HeroSkinInfoManager.s_isReadyingInstance = false;
			if (HeroSkinInfoManager.s_instance == null)
			{
				Debug.LogError("HeroSkinInfoManager:EnterPreviewWhenReady created widget instance but failed to get HeroSkinInfoManager component");
				return;
			}
			HeroSkinInfoManager.s_instance.EnterPreview(cardVisual);
		}, null, true);
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x00071C37 File Offset: 0x0006FE37
	public static bool IsLoadedAndShowingPreview()
	{
		return HeroSkinInfoManager.s_instance && HeroSkinInfoManager.s_instance.IsShowingPreview;
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x00071C51 File Offset: 0x0006FE51
	private void Awake()
	{
		this.m_previewPane.SetActive(false);
		this.SetupUI();
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x00071C68 File Offset: 0x0006FE68
	private void Start()
	{
		this.m_visibilityVisualControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnVisibilityVisualControllerReady));
		this.m_userActionVisualControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnUserActionVisualControllerReady));
		this.m_fullScreenBlockerWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnFullScreenBlockerWidgetReady));
		this.m_vanillaHeroCurrencyIconWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnVanillaHeroCurrencyButtonWidgetReady));
		this.m_newHeroCurrencyIconWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnNewHeroCurrencyButtonWidgetReady));
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x00071CE8 File Offset: 0x0006FEE8
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef currentHeroCardDef = this.m_currentHeroCardDef;
		if (currentHeroCardDef != null)
		{
			currentHeroCardDef.Dispose();
		}
		this.m_currentHeroCardDef = null;
		AssetHandle.SafeDispose<UberShaderAnimation>(ref this.m_currentHeroGoldenAnimation);
		this.CancelPreview();
		HeroSkinInfoManager.s_instance = null;
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x00071D1C File Offset: 0x0006FF1C
	public void EnterPreview(CollectionCardVisual cardVisual)
	{
		if (this.m_animating)
		{
			return;
		}
		Actor actor = cardVisual.GetActor();
		if (actor == null)
		{
			Log.CollectionManager.PrintError("HeroSkinInfoManager.EnterPreview - Could not get actor from card visual. Not displaying preview", Array.Empty<object>());
			return;
		}
		this.m_currentEntityDef = actor.GetEntityDef();
		this.m_currentPremium = actor.GetPremium();
		if (this.m_currentEntityDef == null)
		{
			Log.CollectionManager.PrintError("HeroSkinInfoManager.EnterPreview - Actor entity def not set. Not displaying preview", Array.Empty<object>());
			return;
		}
		Navigation.PushUnique(new Navigation.NavigateBackHandler(HeroSkinInfoManager.OnNavigateBack));
		string cardId = this.m_currentEntityDef.GetCardId();
		this.m_currentHeroRecord = GameDbf.CardHero.GetRecords().Find((CardHeroDbfRecord r) => GameUtils.TranslateDbIdToCardId(r.CardId, false) == cardId);
		bool flag = CollectionManager.GetHeroCardId(this.m_currentEntityDef.GetClass(), CardHero.HeroType.HONORED) == cardId;
		bool flag2 = GameUtils.IsVanillaHero(cardId);
		bool flag3 = (actor.PremiumAnimationAvailable && flag2 && this.m_currentPremium == TAG_PREMIUM.GOLDEN) || !flag2;
		bool flag4 = flag || flag2;
		if (this.LoadHeroDef(cardId))
		{
			if (this.m_currentHeroDef.m_collectionManagerPreviewEmote != EmoteType.INVALID)
			{
				GameUtils.LoadCardDefEmoteSound(cardVisual.GetActor().EmoteDefs, this.m_currentHeroDef.m_collectionManagerPreviewEmote, delegate(CardSoundSpell cardSpell)
				{
					if (cardSpell != null)
					{
						cardSpell.AddFinishedCallback(delegate(Spell spell, object data)
						{
							UnityEngine.Object.Destroy(cardSpell.gameObject);
						});
						cardSpell.Reactivate();
					}
				});
			}
			flag4 = (flag4 || !this.m_currentHeroDef.m_previewMaterial.IsInitialized());
			if ((flag3 ? cardVisual.GetActor().PremiumPortraitMaterial : null) == null)
			{
				this.m_currentHeroDef.m_previewMaterial.GetMaterial();
				flag4 = true;
			}
			Material material = null;
			if (!flag4)
			{
				material = this.m_currentHeroDef.m_previewMaterial.GetMaterial();
			}
			Texture portraitTexture = cardVisual.GetActor().GetPortraitTexture();
			flag4 = (flag4 || material == null);
			if (!flag4)
			{
				string heroUberShaderAnimationPath = this.m_currentHeroDef.GetHeroUberShaderAnimationPath();
				bool flag5 = !string.IsNullOrWhiteSpace(heroUberShaderAnimationPath);
				if (flag5)
				{
					AssetHandle.SafeDispose<UberShaderAnimation>(ref this.m_currentHeroGoldenAnimation);
					AssetLoader.Get().LoadAsset<UberShaderAnimation>(ref this.m_currentHeroGoldenAnimation, heroUberShaderAnimationPath, AssetLoadingOptions.None);
				}
				if (flag5 && this.m_currentHeroGoldenAnimation == null)
				{
					Debug.LogWarning(string.Format("HeroSkinInfoManager.EnterPreview - {0} hero shader could not be loaded {1}", cardId, heroUberShaderAnimationPath));
				}
			}
			if (flag4)
			{
				this.m_vanillaHeroTitle.Text = this.m_currentEntityDef.GetName();
				this.m_vanillaHeroDescription.Text = this.m_currentHeroRecord.Description;
				material = this.m_vanillaHeroNonPremiumMaterial;
				if (flag3)
				{
					Material premiumPortraitMaterial = cardVisual.GetActor().PremiumPortraitMaterial;
					if (premiumPortraitMaterial != null)
					{
						material = premiumPortraitMaterial;
						portraitTexture = material.mainTexture;
					}
					else
					{
						Log.CollectionManager.PrintWarning(string.Format("HeroSkinInfoManager.EnterPreview - premium material missing for {0}", cardId), Array.Empty<object>());
					}
				}
				this.AssignVanillaHeroPreviewMaterial(material, portraitTexture, cardVisual.GetActor().PremiumPortraitAnimation);
			}
			else
			{
				this.m_newHeroTitle.Text = this.m_currentEntityDef.GetName();
				this.m_newHeroDescription.Text = this.m_currentHeroRecord.Description;
				this.AssignNewHeroPreviewMaterial(material, portraitTexture, this.m_currentHeroGoldenAnimation);
			}
			this.m_desiredVisibilityState = (flag4 ? "SHOW_VANILLA_HERO" : "SHOW_NEW_HERO");
			this.m_hasEnteredHeroSkinPreview = true;
			this.m_previewPane.SetActive(true);
			this.m_offClicker.gameObject.SetActive(true);
			this.m_animating = true;
			iTween.ScaleFrom(this.m_previewPane, iTween.Hash(new object[]
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
				})
			}));
			this.SetupHeroSkinStore();
			this.UpdateView();
			if (!string.IsNullOrEmpty(this.m_enterPreviewSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_enterPreviewSound);
			}
			this.PlayHeroMusic();
			FullScreenFXMgr.Get().StartStandardBlurVignette(this.m_animationTime);
			return;
		}
		Debug.LogError("Could not load entity def for hero skin, preview will not be shown. Set the CollectionHeroDefPath on the HERO_0X.prefab.");
		this.m_desiredVisibilityState = "INVALID_HERO";
		this.SetupHeroSkinStore();
		this.UpdateView();
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x00072164 File Offset: 0x00070364
	public void CancelPreview()
	{
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(HeroSkinInfoManager.OnNavigateBack));
		if (this.m_animating)
		{
			return;
		}
		if (!this.m_hasEnteredHeroSkinPreview)
		{
			return;
		}
		this.m_hasEnteredHeroSkinPreview = false;
		this.ShutDownHeroSkinStore();
		Vector3 origScale = this.m_previewPane.transform.localScale;
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
				if (this.m_previewPane)
				{
					this.m_previewPane.transform.localScale = origScale;
					this.m_previewPane.SetActive(false);
				}
				if (this.m_offClicker)
				{
					this.m_offClicker.gameObject.SetActive(false);
				}
			})
		}));
		if (!string.IsNullOrEmpty(this.m_exitPreviewSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_exitPreviewSound);
		}
		this.StopHeroMusic();
		FullScreenFXMgr.Get().EndStandardBlurVignette(this.m_animationTime, null);
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x0007227E File Offset: 0x0007047E
	private void OnVisibilityVisualControllerReady(VisualController visualController)
	{
		this.m_visibilityVisualController = visualController;
		this.UpdateView();
		if (this.OnReady != null)
		{
			this.OnReady();
		}
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000722A0 File Offset: 0x000704A0
	private void OnUserActionVisualControllerReady(VisualController visualController)
	{
		this.m_userActionVisualController = visualController;
		this.UpdateView();
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x000722AF File Offset: 0x000704AF
	private void OnFullScreenBlockerWidgetReady(Widget fullScreenBlockerWidget)
	{
		this.m_fullScreenBlockerWidget = fullScreenBlockerWidget;
		this.UpdateView();
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000722BE File Offset: 0x000704BE
	private void OnVanillaHeroCurrencyButtonWidgetReady(Widget currencyButtonWidget)
	{
		this.m_vanillaHeroCurrencyButtonWidget = currencyButtonWidget;
		this.UpdateView();
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x000722CD File Offset: 0x000704CD
	private void OnNewHeroCurrencyButtonWidgetReady(Widget currencyButtonWidget)
	{
		this.m_newHeroCurrencyButtonWidget = currencyButtonWidget;
		this.UpdateView();
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x000722DC File Offset: 0x000704DC
	private void UpdateView()
	{
		if (this.m_visibilityVisualController == null || this.m_userActionVisualController == null || this.m_fullScreenBlockerWidget == null || this.m_vanillaHeroCurrencyButtonWidget == null || this.m_newHeroCurrencyButtonWidget == null || this.m_currentEntityDef == null || this.m_currentHeroRecord == null || string.IsNullOrEmpty(this.m_currentCardId))
		{
			return;
		}
		this.m_visibilityVisualController.SetState(this.m_desiredVisibilityState);
		bool enabled = false;
		if (CollectionManager.Get().IsInEditMode())
		{
			this.m_userActionVisualController.SetState("DISABLED");
		}
		else if (HeroSkinUtils.IsHeroSkinOwned(this.m_currentEntityDef.GetCardId()))
		{
			this.m_userActionVisualController.SetState("MAKE_FAVORITE");
		}
		else if (!HeroSkinUtils.IsHeroSkinPurchasableFromCollectionManager(this.m_currentEntityDef.GetCardId()))
		{
			this.m_userActionVisualController.SetState("DISABLED");
		}
		else
		{
			PriceDataModel collectionManagerHeroSkinPriceDataModel = HeroSkinUtils.GetCollectionManagerHeroSkinPriceDataModel(this.m_currentEntityDef.GetCardId());
			this.m_userActionVisualController.BindDataModel(collectionManagerHeroSkinPriceDataModel, true, false);
			if (!HeroSkinUtils.CanBuyHeroSkinFromCollectionManager(this.m_currentEntityDef.GetCardId()))
			{
				this.m_userActionVisualController.SetState("INSUFFICIENT_CURRENCY");
			}
			else
			{
				this.m_userActionVisualController.SetState("SUFFICIENT_CURRENCY");
				enabled = true;
			}
		}
		bool flag = HeroSkinUtils.CanFavoriteHeroSkin(this.m_currentEntityDef.GetClass(), this.m_currentEntityDef.GetCardId());
		UIBButton uibbutton = (this.m_desiredVisibilityState == "SHOW_VANILLA_HERO") ? this.m_vanillaHeroFavoriteButton : this.m_newHeroFavoriteButton;
		uibbutton.SetEnabled(flag, false);
		uibbutton.Flip(flag, false);
		UIBButton uibbutton2 = (this.m_desiredVisibilityState == "SHOW_VANILLA_HERO") ? this.m_vanillaHeroBuyButton : this.m_newHeroBuyButton;
		uibbutton2.SetEnabled(enabled, false);
		uibbutton2.Flip(true, false);
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x000724A0 File Offset: 0x000706A0
	private static bool OnNavigateBack()
	{
		HeroSkinInfoManager heroSkinInfoManager = HeroSkinInfoManager.Get();
		if (heroSkinInfoManager != null)
		{
			heroSkinInfoManager.CancelPreview();
		}
		return true;
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x000724C4 File Offset: 0x000706C4
	private bool LoadHeroDef(string cardId)
	{
		if (this.m_currentCardId == cardId && string.IsNullOrEmpty(cardId))
		{
			return true;
		}
		DefLoader.DisposableCardDef currentHeroCardDef = this.m_currentHeroCardDef;
		if (currentHeroCardDef != null)
		{
			currentHeroCardDef.Dispose();
		}
		this.m_currentHeroCardDef = DefLoader.Get().GetCardDef(cardId, null);
		DefLoader.DisposableCardDef currentHeroCardDef2 = this.m_currentHeroCardDef;
		if (((currentHeroCardDef2 != null) ? currentHeroCardDef2.CardDef : null) == null || string.IsNullOrEmpty(this.m_currentHeroCardDef.CardDef.m_CollectionHeroDefPath))
		{
			return false;
		}
		CollectionHeroDef collectionHeroDef = GameUtils.LoadGameObjectWithComponent<CollectionHeroDef>(this.m_currentHeroCardDef.CardDef.m_CollectionHeroDefPath);
		if (collectionHeroDef == null)
		{
			Debug.LogWarning(string.Format("Hero def does not exist on object: {0}", this.m_currentHeroCardDef.CardDef.m_CollectionHeroDefPath));
			return false;
		}
		if (this.m_currentHeroDef != null)
		{
			UnityEngine.Object.Destroy(this.m_currentHeroDef.gameObject);
		}
		this.m_currentCardId = cardId;
		this.m_currentHeroDef = collectionHeroDef;
		return true;
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x000725AC File Offset: 0x000707AC
	private void SetupUI()
	{
		this.m_newHeroFavoriteButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.SetFavoriteHero();
			this.CancelPreview();
		});
		if (this.m_vanillaHeroFavoriteButton != null && this.m_vanillaHeroFavoriteButton != this.m_newHeroFavoriteButton)
		{
			this.m_vanillaHeroFavoriteButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.SetFavoriteHero();
				this.CancelPreview();
			});
		}
		this.m_newHeroBuyButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnBuyButtonReleased();
		});
		if (this.m_vanillaHeroBuyButton != null && this.m_vanillaHeroBuyButton != this.m_newHeroBuyButton)
		{
			this.m_vanillaHeroBuyButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnBuyButtonReleased();
			});
		}
		this.m_offClicker.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.CancelPreview();
		});
		this.m_offClicker.AddEventListener(UIEventType.RIGHTCLICK, delegate(UIEvent e)
		{
			this.CancelPreview();
		});
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x00072694 File Offset: 0x00070894
	private void OnBuyButtonReleased()
	{
		if (!this.IsHeroSkinCardIdValid())
		{
			Debug.LogError("HeroSkinInfoManager:OnBuyButtonReleased called when the hero skin card id was invalid");
			return;
		}
		this.m_visibilityVisualController.SetState("HIDDEN");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Format("GLUE_HERO_SKIN_PURCHASE_CONFIRMATION_HEADER", Array.Empty<object>());
		popupInfo.m_text = GameStrings.Format("GLUE_HERO_SKIN_PURCHASE_CONFIRMATION_MESSAGE", new object[]
		{
			this.m_currentEntityDef.GetName()
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
			this.UpdateView();
		};
		popupInfo.m_responseCallback = responseCallback;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x00072734 File Offset: 0x00070934
	private void SetFavoriteHero()
	{
		NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition
		{
			Name = this.m_currentEntityDef.GetCardId(),
			Premium = this.m_currentPremium
		};
		TAG_CLASS @class = this.m_currentEntityDef.GetClass();
		Network.Get().SetFavoriteHero(@class, cardDefinition);
		if (!Network.IsLoggedIn())
		{
			CollectionManager.Get().UpdateFavoriteHero(@class, cardDefinition.Name, this.m_currentPremium);
		}
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x0007279C File Offset: 0x0007099C
	private void AssignVanillaHeroPreviewMaterial(Material material, Texture portraitTexture, UberShaderAnimation portraitAnimation)
	{
		Renderer component = this.m_vanillaHeroPreviewQuad.GetComponent<Renderer>();
		if (portraitTexture != null)
		{
			component.SetMaterial(1, material);
			component.GetMaterial(1).SetTexture("_MainTex", portraitTexture);
		}
		else
		{
			component.SetMaterial(1, material);
		}
		this.AssignVanillaHeroUberAnimation(portraitAnimation);
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x000727EC File Offset: 0x000709EC
	private void AssignNewHeroPreviewMaterial(Material material, Texture portraitTexture, UberShaderAnimation portraitAnimation)
	{
		Renderer component = this.m_newHeroPreviewQuad.GetComponent<Renderer>();
		if (material != null)
		{
			component.SetMaterial(material);
		}
		else
		{
			component.SetMaterial(this.m_defaultPreviewMaterial);
			component.GetMaterial().mainTexture = portraitTexture;
		}
		this.AssignNewHeroUberAnimation(portraitAnimation);
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x00072838 File Offset: 0x00070A38
	private void AssignVanillaHeroUberAnimation(UberShaderAnimation portraitAnimation)
	{
		UberShaderController uberShaderController = this.m_vanillaHeroPreviewQuad.GetComponent<UberShaderController>();
		if (portraitAnimation != null)
		{
			if (uberShaderController == null)
			{
				uberShaderController = this.m_vanillaHeroPreviewQuad.gameObject.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(portraitAnimation);
			uberShaderController.m_MaterialIndex = 1;
			uberShaderController.enabled = true;
			return;
		}
		if (uberShaderController != null)
		{
			UnityEngine.Object.Destroy(uberShaderController);
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x000728A0 File Offset: 0x00070AA0
	private void AssignNewHeroUberAnimation(UberShaderAnimation portraitAnimation)
	{
		UberShaderController uberShaderController = this.m_newHeroPreviewQuad.GetComponent<UberShaderController>();
		if (portraitAnimation != null)
		{
			if (uberShaderController == null)
			{
				uberShaderController = this.m_newHeroPreviewQuad.gameObject.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(portraitAnimation);
			uberShaderController.m_MaterialIndex = 0;
			uberShaderController.enabled = true;
			return;
		}
		if (uberShaderController != null)
		{
			UnityEngine.Object.Destroy(uberShaderController);
		}
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x00072908 File Offset: 0x00070B08
	private void PlayHeroMusic()
	{
		MusicPlaylistType musicPlaylistType;
		if (this.m_currentHeroDef == null || this.m_currentHeroDef.m_heroPlaylist == MusicPlaylistType.Invalid)
		{
			musicPlaylistType = this.m_defaultHeroMusic;
		}
		else
		{
			musicPlaylistType = this.m_currentHeroDef.m_heroPlaylist;
		}
		if (musicPlaylistType != MusicPlaylistType.Invalid)
		{
			this.m_prevPlaylist = MusicManager.Get().GetCurrentPlaylist();
			MusicManager.Get().StartPlaylist(musicPlaylistType);
		}
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x00072964 File Offset: 0x00070B64
	private void StopHeroMusic()
	{
		MusicManager.Get().StartPlaylist(this.m_prevPlaylist);
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x00072978 File Offset: 0x00070B78
	private void BlockInputs(bool blocked)
	{
		if (this.m_fullScreenBlockerWidget == null)
		{
			Debug.LogError("Failed to toggle interface blocker from Duels Popup Manager");
			return;
		}
		this.m_fullScreenBlockerWidget.TriggerEvent(blocked ? "BLOCK_SCREEN" : "UNBLOCK_SCREEN", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = false
		});
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x000729D2 File Offset: 0x00070BD2
	private bool IsHeroSkinCardIdValid()
	{
		return this.m_currentEntityDef != null && !string.IsNullOrEmpty(this.m_currentEntityDef.GetCardId());
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x000729F4 File Offset: 0x00070BF4
	private void SetupHeroSkinStore()
	{
		if (this.m_isStoreOpen)
		{
			Debug.LogError("CardBackInfoManager:SetupHeroSkinStore called when the store was already open");
			return;
		}
		if (this.m_currentHeroRecord == null)
		{
			Debug.LogError("CardBackInfoManager:SetupHeroSkinStore: m_currentHeroRecord was null");
			return;
		}
		StoreManager storeManager = StoreManager.Get();
		if (!storeManager.IsOpen(true))
		{
			return;
		}
		storeManager.SetupHeroSkinStore(this, this.m_currentHeroRecord.CardId);
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

	// Token: 0x060013E7 RID: 5095 RVA: 0x00072A90 File Offset: 0x00070C90
	private void ShutDownHeroSkinStore()
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
		storeManager.ShutDownHeroSkinStore();
		this.OnProductPurchaseAttempt = null;
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.RefreshCurrency();
		}
		this.BlockInputs(false);
		this.m_isStoreOpen = false;
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnSuccessfulPurchase(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x00072B28 File Offset: 0x00070D28
	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		this.EndPurchaseTransaction();
		this.UpdateView();
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x00072B28 File Offset: 0x00070D28
	private void OnFailedPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		this.EndPurchaseTransaction();
		this.UpdateView();
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x00072B38 File Offset: 0x00070D38
	private void StartPurchaseTransaction()
	{
		if (!this.IsHeroSkinCardIdValid())
		{
			Debug.LogError("HeroSkinInfoManager:StartPurchaseTransaction called when the hero skin card id was invalid");
			return;
		}
		if (this.m_isStoreTransactionActive)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_HERO_SKIN_PURCHASE_ERROR_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Debug.LogWarning("Attempted to start a hero skin purchase transaction while an existing transaction was in progress (CardId = " + this.m_currentEntityDef.GetCardId() + ")");
			return;
		}
		if (this.OnProductPurchaseAttempt == null)
		{
			Debug.LogError("Attempted to start a hero skin purchase transaction while OnProductPurchaseAttempt was null (CardId = " + this.m_currentEntityDef.GetCardId() + ")");
			return;
		}
		Network.Bundle collectionManagerHeroSkinProductBundle = HeroSkinUtils.GetCollectionManagerHeroSkinProductBundle(this.m_currentEntityDef.GetCardId());
		if (collectionManagerHeroSkinProductBundle == null)
		{
			Debug.LogError("Attempted to start a hero skin purchase transaction with a null bundle (CardId = " + this.m_currentEntityDef.GetCardId() + ")");
			return;
		}
		this.m_isStoreTransactionActive = true;
		this.OnProductPurchaseAttempt(new BuyPmtProductEventArgs(collectionManagerHeroSkinProductBundle, CurrencyType.GOLD, 1));
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x00072C35 File Offset: 0x00070E35
	private void CancelPurchaseTransaction()
	{
		this.EndPurchaseTransaction();
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x00072C3D File Offset: 0x00070E3D
	private void EndPurchaseTransaction()
	{
		if (!this.m_isStoreTransactionActive)
		{
			return;
		}
		this.m_isStoreTransactionActive = false;
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x00072C50 File Offset: 0x00070E50
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
		Debug.LogError("HeroSkinInfoManager:IStore.Open: Could not get the Bnet bar to reflect the required currency");
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x00072C9F File Offset: 0x00070E9F
	void IStore.Close()
	{
		if (this.m_isStoreTransactionActive)
		{
			this.CancelPurchaseTransaction();
		}
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x00072CAF File Offset: 0x00070EAF
	void IStore.BlockInterface(bool blocked)
	{
		this.BlockInputs(blocked);
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x000052EC File Offset: 0x000034EC
	bool IStore.IsReady()
	{
		return true;
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x00072CB8 File Offset: 0x00070EB8
	bool IStore.IsOpen()
	{
		return this.m_isStoreOpen;
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x00003BE8 File Offset: 0x00001DE8
	void IStore.Unload()
	{
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x0004EA58 File Offset: 0x0004CC58
	IEnumerable<CurrencyType> IStore.GetVisibleCurrencies()
	{
		return new CurrencyType[]
		{
			CurrencyType.GOLD
		};
	}

	// Token: 0x04000CED RID: 3309
	private const string STATE_SHOW_VANILLA_HERO = "SHOW_VANILLA_HERO";

	// Token: 0x04000CEE RID: 3310
	private const string STATE_SHOW_NEW_HERO = "SHOW_NEW_HERO";

	// Token: 0x04000CEF RID: 3311
	private const string STATE_INVALID_HERO = "INVALID_HERO";

	// Token: 0x04000CF0 RID: 3312
	private const string STATE_HIDDEN = "HIDDEN";

	// Token: 0x04000CF1 RID: 3313
	private const string MAKE_FAVORITE_STATE = "MAKE_FAVORITE";

	// Token: 0x04000CF2 RID: 3314
	private const string SUFFICIENT_CURRENCY_STATE = "SUFFICIENT_CURRENCY";

	// Token: 0x04000CF3 RID: 3315
	private const string INSUFFICIENT_CURRENCY_STATE = "INSUFFICIENT_CURRENCY";

	// Token: 0x04000CF4 RID: 3316
	private const string DISABLED_STATE = "DISABLED";

	// Token: 0x04000CF5 RID: 3317
	private const string STATE_BLOCK_SCREEN = "BLOCK_SCREEN";

	// Token: 0x04000CF6 RID: 3318
	private const string STATE_UNBLOCK_SCREEN = "UNBLOCK_SCREEN";

	// Token: 0x04000CF7 RID: 3319
	public GameObject m_previewPane;

	// Token: 0x04000CF8 RID: 3320
	public GameObject m_vanillaHeroFrame;

	// Token: 0x04000CF9 RID: 3321
	public MeshRenderer m_vanillaHeroPreviewQuad;

	// Token: 0x04000CFA RID: 3322
	public UberText m_vanillaHeroTitle;

	// Token: 0x04000CFB RID: 3323
	public UberText m_vanillaHeroDescription;

	// Token: 0x04000CFC RID: 3324
	public UIBButton m_vanillaHeroFavoriteButton;

	// Token: 0x04000CFD RID: 3325
	public UIBButton m_vanillaHeroBuyButton;

	// Token: 0x04000CFE RID: 3326
	public GameObject m_newHeroFrame;

	// Token: 0x04000CFF RID: 3327
	public MeshRenderer m_newHeroPreviewQuad;

	// Token: 0x04000D00 RID: 3328
	public UberText m_newHeroTitle;

	// Token: 0x04000D01 RID: 3329
	public UberText m_newHeroDescription;

	// Token: 0x04000D02 RID: 3330
	public UIBButton m_newHeroFavoriteButton;

	// Token: 0x04000D03 RID: 3331
	public UIBButton m_newHeroBuyButton;

	// Token: 0x04000D04 RID: 3332
	public PegUIElement m_offClicker;

	// Token: 0x04000D05 RID: 3333
	public float m_animationTime = 0.5f;

	// Token: 0x04000D06 RID: 3334
	public Material m_defaultPreviewMaterial;

	// Token: 0x04000D07 RID: 3335
	public Material m_vanillaHeroNonPremiumMaterial;

	// Token: 0x04000D08 RID: 3336
	public AsyncReference m_visibilityVisualControllerReference;

	// Token: 0x04000D09 RID: 3337
	public AsyncReference m_userActionVisualControllerReference;

	// Token: 0x04000D0A RID: 3338
	public AsyncReference m_vanillaHeroCurrencyIconWidgetReference;

	// Token: 0x04000D0B RID: 3339
	public AsyncReference m_newHeroCurrencyIconWidgetReference;

	// Token: 0x04000D0C RID: 3340
	public AsyncReference m_fullScreenBlockerWidgetReference;

	// Token: 0x04000D0D RID: 3341
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_enterPreviewSound;

	// Token: 0x04000D0E RID: 3342
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_exitPreviewSound;

	// Token: 0x04000D0F RID: 3343
	public MusicPlaylistType m_defaultHeroMusic = MusicPlaylistType.UI_CMHeroSkinPreview;

	// Token: 0x04000D10 RID: 3344
	private string m_currentCardId;

	// Token: 0x04000D11 RID: 3345
	private DefLoader.DisposableCardDef m_currentHeroCardDef;

	// Token: 0x04000D12 RID: 3346
	private CollectionHeroDef m_currentHeroDef;

	// Token: 0x04000D13 RID: 3347
	private AssetHandle<UberShaderAnimation> m_currentHeroGoldenAnimation;

	// Token: 0x04000D14 RID: 3348
	private CardHeroDbfRecord m_currentHeroRecord;

	// Token: 0x04000D15 RID: 3349
	private EntityDef m_currentEntityDef;

	// Token: 0x04000D16 RID: 3350
	private TAG_PREMIUM m_currentPremium;

	// Token: 0x04000D17 RID: 3351
	private static HeroSkinInfoManager s_instance;

	// Token: 0x04000D18 RID: 3352
	private static bool s_isReadyingInstance;

	// Token: 0x04000D19 RID: 3353
	private bool m_animating;

	// Token: 0x04000D1A RID: 3354
	private bool m_hasEnteredHeroSkinPreview;

	// Token: 0x04000D1B RID: 3355
	private MusicPlaylistType m_prevPlaylist;

	// Token: 0x04000D1C RID: 3356
	private string m_desiredVisibilityState = "INVALID_HERO";

	// Token: 0x04000D1D RID: 3357
	private VisualController m_visibilityVisualController;

	// Token: 0x04000D1E RID: 3358
	private VisualController m_userActionVisualController;

	// Token: 0x04000D1F RID: 3359
	private Widget m_fullScreenBlockerWidget;

	// Token: 0x04000D20 RID: 3360
	private Widget m_vanillaHeroCurrencyButtonWidget;

	// Token: 0x04000D21 RID: 3361
	private Widget m_newHeroCurrencyButtonWidget;

	// Token: 0x04000D22 RID: 3362
	private bool m_isStoreOpen;

	// Token: 0x04000D23 RID: 3363
	private bool m_isStoreTransactionActive;
}
