using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006FF RID: 1791
[CustomEditClass]
public class GeneralStorePacksContent : GeneralStoreContent
{
	// Token: 0x060063FC RID: 25596 RVA: 0x00208FB4 File Offset: 0x002071B4
	public override void PostStoreFlipIn(bool animatedFlipIn)
	{
		this.UpdatePacksTypeMusic();
		this.AnimateLogo(animatedFlipIn, false);
		if (GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
		{
			int firstValidBundleIndex = this.GetFirstValidBundleIndex(this.m_selectedStorePackId);
			this.HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
			Network.Bundle currentMoneyBundle = base.GetCurrentMoneyBundle();
			if (StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle))
			{
				this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, true, true);
				base.StartCoroutine(this.ShowFeaturedDustJar(false));
				if (GameUtils.IsFirstPurchaseBundleBooster(this.m_selectedStorePackId))
				{
					this.ShowHiddenBundleCard();
				}
			}
			else
			{
				float delay = 0f;
				if (UniversalInputManager.UsePhoneUI)
				{
					delay = 1f;
				}
				this.AnimatePacksFlying(this.m_visiblePackCount, false, delay, false, true);
			}
		}
		else
		{
			this.AnimatePacksFlying(this.m_visiblePackCount, !animatedFlipIn, 0f, false, true);
			this.HideDust();
		}
		this.UpdateKoreaInfoButton();
		this.m_savedLocalPosition = base.gameObject.transform.localPosition;
	}

	// Token: 0x060063FD RID: 25597 RVA: 0x002090A1 File Offset: 0x002072A1
	public override void PreStoreFlipOut()
	{
		this.ResetAnimations();
		this.GetCurrentDisplay().ClearContents();
		this.UpdateKoreaInfoButton();
	}

	// Token: 0x060063FE RID: 25598 RVA: 0x002090BC File Offset: 0x002072BC
	public override void StoreShown(bool isCurrent)
	{
		if (!isCurrent)
		{
			return;
		}
		this.AnimateLogo(false, false);
		if (GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
		{
			int firstValidBundleIndex = this.GetFirstValidBundleIndex(this.m_selectedStorePackId);
			this.HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
			Network.Bundle currentMoneyBundle = base.GetCurrentMoneyBundle();
			if (StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle))
			{
				this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, true, true);
				base.StartCoroutine(this.ShowFeaturedDustJar(false));
				if (GameUtils.IsFirstPurchaseBundleBooster(this.m_selectedStorePackId))
				{
					this.ShowHiddenBundleCard();
				}
			}
			else
			{
				this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, false, true);
			}
		}
		else
		{
			Network.Bundle currentMoneyBundle2 = base.GetCurrentMoneyBundle();
			bool flag = false;
			if (currentMoneyBundle2 != null)
			{
				flag = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle2);
			}
			if (flag)
			{
				this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, true, UniversalInputManager.UsePhoneUI);
			}
			else
			{
				this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, false, true);
				this.HideDust();
			}
		}
		this.UpdatePackBuyButtons();
		this.UpdatePacksTypeMusic();
		this.UpdateKoreaInfoButton();
	}

	// Token: 0x060063FF RID: 25599 RVA: 0x002091BD File Offset: 0x002073BD
	public override void StoreHidden(bool isCurrent)
	{
		if (!isCurrent)
		{
			return;
		}
		this.ResetAnimations();
		this.GetCurrentDisplay().ClearContents();
	}

	// Token: 0x06006400 RID: 25600 RVA: 0x002091D4 File Offset: 0x002073D4
	public override bool IsPurchaseDisabled()
	{
		return this.IsPackIdInvalid(this.m_selectedStorePackId);
	}

	// Token: 0x06006401 RID: 25601 RVA: 0x002091E2 File Offset: 0x002073E2
	public override string GetMoneyDisplayOwnedText()
	{
		return GameStrings.Get("GLUE_STORE_PACK_BUTTON_COST_OWNED_TEXT");
	}

	// Token: 0x06006402 RID: 25602 RVA: 0x002091F0 File Offset: 0x002073F0
	public void SetBoosterId(StorePackId storePackId, bool forceImmediate = false, bool InitialSelection = false)
	{
		if (this.m_selectedStorePackId == storePackId)
		{
			return;
		}
		bool flag = this.IsPackIdInvalid(this.m_selectedStorePackId);
		StoreManager.Get().SetCurrentlySelectedStorePack(storePackId);
		this.GetCurrentDisplay().ClearContents();
		this.m_visiblePackCount = 0;
		this.m_visibleDustCount = 0;
		this.m_selectedStorePackId = storePackId;
		if (flag)
		{
			this.UpdateSelectedBundle(false);
		}
		this.ResetAnimations();
		this.AnimateAndUpdateDisplay(storePackId, forceImmediate);
		if (InitialSelection)
		{
			this.GetCurrentLogo().gameObject.SetActive(false);
		}
		this.AnimateLogo(!forceImmediate, InitialSelection);
		bool flag2 = false;
		if (GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
		{
			int firstValidBundleIndex = this.GetFirstValidBundleIndex(this.m_selectedStorePackId);
			this.HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
			Network.Bundle currentMoneyBundle = base.GetCurrentMoneyBundle();
			flag2 = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle);
			this.m_selectedBoosterIsPrePurchase = false;
		}
		else if (base.GetCurrentGoldBundle() != null)
		{
			base.SetCurrentGoldBundle(this.GetCurrentGTAPPTransactionData());
		}
		else if (base.GetCurrentMoneyBundle() != null)
		{
			this.HandleMoneyPackBuyButtonClick(this.m_lastBundleIndex);
			Network.Bundle currentMoneyBundle2 = base.GetCurrentMoneyBundle();
			flag2 = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle2);
			this.m_selectedBoosterIsPrePurchase = StoreManager.Get().IsProductPrePurchase(currentMoneyBundle2);
		}
		Log.Store.Print("InitialSelection = {0}", new object[]
		{
			InitialSelection
		});
		if (GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
		{
			float num = InitialSelection ? 0.5f : 0f;
			Log.Store.Print("InitialSelection delay={0}", new object[]
			{
				num
			});
			if (flag2)
			{
				this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, true, true);
				base.StartCoroutine(this.ShowFeaturedDustJar(false));
				if (GameUtils.IsFirstPurchaseBundleBooster(this.m_selectedStorePackId))
				{
					this.ShowHiddenBundleCard();
				}
			}
			else
			{
				this.AnimatePacksFlying(this.m_visiblePackCount, forceImmediate, num, false, true);
			}
		}
		else if (flag2)
		{
			bool waitForLogo = UniversalInputManager.UsePhoneUI;
			this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, !this.m_selectedBoosterIsPrePurchase, waitForLogo);
			base.StartCoroutine(this.ShowFeaturedDustJar(waitForLogo));
		}
		else
		{
			this.AnimatePacksFlying(this.m_visiblePackCount, forceImmediate, 0f, false, true);
			this.HideDust();
		}
		this.UpdatePackBuyButtons();
		this.UpdatePacksDescriptionFromSelectedStorePack();
		this.UpdatePacksTypeMusic();
		this.UpdateKoreaInfoButton();
	}

	// Token: 0x06006403 RID: 25603 RVA: 0x00209423 File Offset: 0x00207623
	public StorePackId GetStorePackId()
	{
		return this.m_selectedStorePackId;
	}

	// Token: 0x06006404 RID: 25604 RVA: 0x0020942C File Offset: 0x0020762C
	private int GetFirstValidBundleIndex(StorePackId storePackId)
	{
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
		for (int i = 0; i < productDataCountFromStorePackId; i++)
		{
			int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId, i);
			if (StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, true, productDataFromStorePackId, 0, true).Any<Network.Bundle>())
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06006405 RID: 25605 RVA: 0x0020946E File Offset: 0x0020766E
	public int GetLastBundleIndex()
	{
		return this.m_lastBundleIndex;
	}

	// Token: 0x06006406 RID: 25606 RVA: 0x00209478 File Offset: 0x00207678
	public bool SelectedBundleFeaturesDustJar()
	{
		Network.Bundle currentMoneyBundle = base.GetCurrentMoneyBundle();
		return StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle);
	}

	// Token: 0x06006407 RID: 25607 RVA: 0x00209498 File Offset: 0x00207698
	public void FirstPurchaseBundlePurchased(CardReward cardReward)
	{
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay == null)
		{
			CardRewardData cardRewardData = cardReward.Data as CardRewardData;
			Debug.LogWarningFormat("FirstPurchaseBundlePurchased() failed to get GeneralStorePacksContentDisplay for cardID {0}", new object[]
			{
				cardRewardData.CardID
			});
			return;
		}
		currentDisplay.PurchaseBundleBox(cardReward);
	}

	// Token: 0x06006408 RID: 25608 RVA: 0x002094E2 File Offset: 0x002076E2
	public Map<StorePackId, IStorePackDef> GetStorePackDefs()
	{
		return this.m_storePackDefs;
	}

	// Token: 0x06006409 RID: 25609 RVA: 0x002094EC File Offset: 0x002076EC
	public IStorePackDef GetStorePackDef(StorePackId packId)
	{
		IStorePackDef result = null;
		this.m_storePackDefs.TryGetValue(packId, out result);
		return result;
	}

	// Token: 0x0600640A RID: 25610 RVA: 0x0020950C File Offset: 0x0020770C
	public void ShakeStore(int numPacks, float maxXRotation, float delay = 0f, float translationAmount = 0f, int weight = 0)
	{
		if (numPacks == 0)
		{
			return;
		}
		int num = 1;
		float xRotationAmount = 0f;
		List<Network.Bundle> packBundles = this.GetPackBundles(false);
		if (this.m_selectedStorePackId.Type == StorePackType.BOOSTER)
		{
			using (List<Network.Bundle>.Enumerator enumerator = packBundles.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Network.Bundle bundle = enumerator.Current;
					Network.BundleItem packsBundleItemFromBundle = GeneralStorePacksContent.GetPacksBundleItemFromBundle(bundle);
					if (packsBundleItemFromBundle != null)
					{
						num = Mathf.Max(packsBundleItemFromBundle.Quantity, num);
						int num2 = num - 1;
						if (num2 == 0)
						{
							return;
						}
						xRotationAmount = (float)numPacks / (float)num2 * maxXRotation;
					}
				}
				goto IL_9F;
			}
		}
		if (this.m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			int num3 = 100;
			if (weight > num3)
			{
				weight = num3;
			}
			xRotationAmount = maxXRotation * (float)weight / (float)num3;
		}
		IL_9F:
		float translateAmount = 0f;
		if (GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
		{
			translateAmount = translationAmount;
		}
		this.m_parentStore.ShakeStore(xRotationAmount, this.m_packFlyShakeTime, delay, translateAmount);
	}

	// Token: 0x0600640B RID: 25611 RVA: 0x002095F4 File Offset: 0x002077F4
	public void StartAnimatingPacks()
	{
		this.m_animatingPacks = true;
	}

	// Token: 0x0600640C RID: 25612 RVA: 0x002095FD File Offset: 0x002077FD
	public void DoneAnimatingPacks()
	{
		this.m_animatingPacks = false;
	}

	// Token: 0x0600640D RID: 25613 RVA: 0x00209608 File Offset: 0x00207808
	protected override void OnBundleChanged(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
		if (this.IsPackIdFirstPurchaseBundle(this.m_selectedStorePackId) && moneyBundle == null)
		{
			int firstValidBundleIndex = this.GetFirstValidBundleIndex(this.m_selectedStorePackId);
			this.HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
			return;
		}
		if (this.m_selectedStorePackId.Type == StorePackType.BOOSTER && GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId) && !StoreManager.Get().ShouldShowFeaturedDustJar(moneyBundle))
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		if (goldBundle != null)
		{
			this.m_visiblePackCount = goldBundle.Quantity;
			this.m_visibleDustCount = 0;
			this.m_selectedBoosterIsPrePurchase = false;
		}
		else if (moneyBundle != null)
		{
			this.m_visiblePackCount = StoreManager.Get().PackQuantityInBundle(moneyBundle);
			int num = StoreManager.Get().DustQuantityInBundle(moneyBundle);
			int num2 = StoreManager.Get().DustBaseQuantityInBundle(moneyBundle);
			if (num2 > 0)
			{
				this.m_visibleDustCount = num2;
				this.m_visibleDustBonusCount = Math.Max(num - num2, 0);
			}
			else
			{
				this.m_visibleDustCount = num;
				this.m_visibleDustBonusCount = 0;
			}
			flag = (this.m_visibleDustCount > 0);
			flag2 = StoreManager.Get().ShouldShowFeaturedDustJar(moneyBundle);
			this.m_selectedBoosterIsPrePurchase = StoreManager.Get().IsProductPrePurchase(moneyBundle);
		}
		if (flag && flag2)
		{
			bool waitForLogo = UniversalInputManager.UsePhoneUI;
			this.AnimatePacksFlying(this.m_visiblePackCount, true, 0f, !this.m_selectedBoosterIsPrePurchase, waitForLogo);
			base.StartCoroutine(this.ShowFeaturedDustJar(waitForLogo));
			return;
		}
		this.AnimatePacksFlying(this.m_visiblePackCount, false, 0f, false, true);
		this.HideDust();
		this.HideHiddenBundleCard();
	}

	// Token: 0x0600640E RID: 25614 RVA: 0x00209776 File Offset: 0x00207976
	protected override void OnRefresh()
	{
		this.UpdatePackBuyButtons();
		this.UpdatePacksDescriptionFromSelectedStorePack();
		if (base.HasBundleSet())
		{
			return;
		}
		if (this.IsPackIdInvalid(this.m_selectedStorePackId))
		{
			return;
		}
		this.UpdateSelectedBundle(true);
	}

	// Token: 0x0600640F RID: 25615 RVA: 0x002097A4 File Offset: 0x002079A4
	private void Awake()
	{
		this.m_packDisplay1 = this.m_packDisplay;
		this.m_packDisplay2 = UnityEngine.Object.Instantiate<GeneralStorePacksContentDisplay>(this.m_packDisplay);
		this.m_packDisplay2.transform.parent = this.m_packDisplay1.transform.parent;
		this.m_packDisplay2.transform.localPosition = this.m_packDisplay1.transform.localPosition;
		this.m_packDisplay2.transform.localScale = this.m_packDisplay1.transform.localScale;
		this.m_packDisplay2.transform.localRotation = this.m_packDisplay1.transform.localRotation;
		this.m_packDisplay2.gameObject.SetActive(false);
		this.m_logoMesh1 = this.m_logoMesh;
		this.m_logoMesh2 = UnityEngine.Object.Instantiate<MeshRenderer>(this.m_logoMesh);
		this.m_logoMesh2.transform.parent = this.m_logoMesh1.transform.parent;
		this.m_logoMesh2.transform.localPosition = this.m_logoMesh1.transform.localPosition;
		this.m_logoMesh2.transform.localScale = this.m_logoMesh1.transform.localScale;
		this.m_logoMesh2.transform.localRotation = this.m_logoMesh1.transform.localRotation;
		this.m_logoMesh2.gameObject.SetActive(false);
		this.m_logoGlowMesh1 = this.m_logoGlowMesh;
		this.m_logoGlowMesh2 = this.m_logoMesh2.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
		this.m_packDisplay1.SetParent(this);
		this.m_packDisplay2.SetParent(this);
		this.m_packBuyContainer.SetActive(false);
		if (this.m_limitedTimeOfferText != null)
		{
			this.m_limitedTimeTextOrigScale = this.m_limitedTimeOfferText.transform.localScale;
		}
		if (this.m_packBuyBonusCallout != null)
		{
			this.m_packBuyBonusCallout.Init();
		}
		if (this.m_packBuyBonusText != null)
		{
			this.m_packBuyBonusText.gameObject.SetActive(false);
		}
		if (this.m_ChinaInfoButton != null)
		{
			this.m_ChinaInfoButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnKoreaInfoPressed));
		}
		foreach (BoosterDbfRecord boosterDbfRecord in GameUtils.GetPackRecordsWithStorePrefab())
		{
			int id = boosterDbfRecord.ID;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(boosterDbfRecord.StorePrefab, AssetLoadingOptions.IgnorePrefabPosition);
			if (gameObject == null)
			{
				Debug.LogError(string.Format("Unable to load store pack def: {0}", boosterDbfRecord.StorePrefab));
			}
			else
			{
				IStorePackDef component = gameObject.GetComponent<StorePackDef>();
				if (component == null)
				{
					Debug.LogError(string.Format("StorePackDef component not found: {0}", boosterDbfRecord.StorePrefab));
				}
				else
				{
					StorePackId key = new StorePackId
					{
						Type = StorePackType.BOOSTER,
						Id = id
					};
					this.m_storePackDefs.Add(key, component);
				}
			}
		}
		foreach (ModularBundleDbfRecord modularBundleDbfRecord in GameDbf.ModularBundle.GetRecords())
		{
			ModularBundleStorePackDef value = new ModularBundleStorePackDef(modularBundleDbfRecord);
			StorePackId key2 = new StorePackId
			{
				Type = StorePackType.MODULAR_BUNDLE,
				Id = modularBundleDbfRecord.ID
			};
			this.m_storePackDefs.Add(key2, value);
		}
		this.UpdateKoreaInfoButton();
	}

	// Token: 0x06006410 RID: 25616 RVA: 0x00209B30 File Offset: 0x00207D30
	private GameObject GetCurrentDisplayContainer()
	{
		return this.GetCurrentDisplay().gameObject;
	}

	// Token: 0x06006411 RID: 25617 RVA: 0x00209B3D File Offset: 0x00207D3D
	private GameObject GetNextDisplayContainer()
	{
		if ((this.m_currentDisplay + 1) % 2 != 0)
		{
			return this.m_packDisplay2.gameObject;
		}
		return this.m_packDisplay1.gameObject;
	}

	// Token: 0x06006412 RID: 25618 RVA: 0x00209B62 File Offset: 0x00207D62
	private GeneralStorePacksContentDisplay GetCurrentDisplay()
	{
		if (this.m_currentDisplay != 0)
		{
			return this.m_packDisplay2;
		}
		return this.m_packDisplay1;
	}

	// Token: 0x06006413 RID: 25619 RVA: 0x00209B79 File Offset: 0x00207D79
	private MeshRenderer GetCurrentLogo()
	{
		if (this.m_currentDisplay != 0)
		{
			return this.m_logoMesh2;
		}
		return this.m_logoMesh1;
	}

	// Token: 0x06006414 RID: 25620 RVA: 0x00209B90 File Offset: 0x00207D90
	private MeshRenderer GetCurrentGlowLogo()
	{
		if (this.m_currentDisplay != 0)
		{
			return this.m_logoGlowMesh2;
		}
		return this.m_logoGlowMesh1;
	}

	// Token: 0x06006415 RID: 25621 RVA: 0x00209BA8 File Offset: 0x00207DA8
	private void UpdateSelectedBundle(bool forceUpdate = false)
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(this.m_selectedStorePackId);
		int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(this.m_selectedStorePackId, this.m_lastBundleIndex);
		NoGTAPPTransactionData noGTAPPTransactionData = new NoGTAPPTransactionData
		{
			Product = productTypeFromStorePackType,
			ProductData = productDataFromStorePackId,
			Quantity = 1
		};
		long num;
		if (StoreManager.Get().GetGoldCostNoGTAPP(noGTAPPTransactionData, out num))
		{
			base.SetCurrentGoldBundle(noGTAPPTransactionData);
			return;
		}
		Network.Bundle lowestCostBundle = StoreManager.Get().GetLowestCostBundle(productTypeFromStorePackType, false, productDataFromStorePackId, 0);
		if (lowestCostBundle != null)
		{
			base.SetCurrentMoneyBundle(lowestCostBundle, forceUpdate);
		}
	}

	// Token: 0x06006416 RID: 25622 RVA: 0x00209C24 File Offset: 0x00207E24
	private void UpdatePacksDescriptionFromSelectedStorePack()
	{
		if (this.IsPackIdInvalid(this.m_selectedStorePackId))
		{
			this.m_parentStore.HideAccentTexture();
			this.m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_PACK"));
			return;
		}
		if (this.m_selectedStorePackId.Type == StorePackType.BOOSTER)
		{
			this.UpdatePacksDescriptionForBooster();
			return;
		}
		if (this.m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			this.UpdatePacksDescriptionForModularBundle();
		}
	}

	// Token: 0x06006417 RID: 25623 RVA: 0x00209C8C File Offset: 0x00207E8C
	private void UpdatePacksDescriptionForBooster()
	{
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(this.m_selectedStorePackId.Id);
		string text = record.Name;
		string packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_PACK");
		string packDescription = GameStrings.Format("GLUE_STORE_PRODUCT_DETAILS_PACK", new object[]
		{
			text
		});
		Network.Bundle currentMoneyBundle = base.GetCurrentMoneyBundle();
		bool flag = false;
		if (currentMoneyBundle != null)
		{
			flag = StoreManager.Get().IsProductPrePurchase(currentMoneyBundle);
			bool flag2 = GameUtils.IsFirstPurchaseBundleBooster(this.m_selectedStorePackId);
			bool flag3 = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle);
			if (!flag && !flag2 && flag3)
			{
				packDescription = GameStrings.Format("GLUE_STORE_PRODUCT_DETAILS_DUST", new object[]
				{
					text
				});
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_DUST");
			}
			if (flag && 10 == record.ID)
			{
				packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_TGT_PACK_PRESALE");
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_TGT_PACK_PRESALE");
			}
			if (flag && 11 == record.ID)
			{
				packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_OG_PACK_PRESALE");
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_OG_PACK_PRESALE");
			}
			if (flag && 20 == record.ID)
			{
				packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_GORO_PACK_PRESALE");
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_GORO_PACK_PRESALE");
			}
			if (GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
			{
				if (flag2)
				{
					packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_FIRST_PURCHASE_BUNDLE");
					if (flag3)
					{
						packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_FIRST_PURCHASE_BUNDLE_DUST");
					}
					else
					{
						packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_FIRST_PURCHASE_BUNDLE");
					}
				}
				else if (GameUtils.IsMammothBundleBooster(this.m_selectedStorePackId))
				{
					packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_MAMMOTH_BUNDLE");
					if (flag3)
					{
						packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_MAMMOTH_BUNDLE_DUST");
					}
					else
					{
						packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_MAMMOTH_BUNDLE");
					}
				}
			}
			if (flag && 21 == record.ID)
			{
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_ICC_PACK_PRESALE");
				if (flag3)
				{
					packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_ICC_CN_DUST_PRESALE");
				}
				else
				{
					packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_ICC_PACK_PRESALE");
				}
			}
			if (flag && 30 == record.ID)
			{
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_LOOT_PACK_PRESALE");
				if (flag3)
				{
					packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_LOOT_CN_DUST_PRESALE");
				}
				else
				{
					packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_LOOT_PACK_PRESALE");
				}
			}
			if (flag && 31 == record.ID)
			{
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_GIL_PACK_PRESALE");
				if (flag3)
				{
					packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_GIL_CN_DUST_PRESALE");
				}
				else
				{
					packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_GIL_PACK_PRESALE");
				}
			}
		}
		string accentTextureName = "";
		IStorePackDef storePackDef = this.GetStorePackDef(this.m_selectedStorePackId);
		if (storePackDef != null)
		{
			accentTextureName = storePackDef.GetAccentTextureName();
		}
		this.UpdatePacksDescription(packDescriptionHeadline, packDescription, accentTextureName, flag);
	}

	// Token: 0x06006418 RID: 25624 RVA: 0x00209EF0 File Offset: 0x002080F0
	private void UpdatePacksDescriptionForModularBundle()
	{
		int id = this.m_selectedStorePackId.Id;
		List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(id);
		if (this.m_lastBundleIndex >= regionNodeLayoutsForBundle.Count)
		{
			Log.Store.PrintWarning(string.Format("Selected invalid layout at index={0}. Defaulting to layout at index=0.", this.m_lastBundleIndex), Array.Empty<object>());
			this.m_lastBundleIndex = 0;
		}
		ModularBundleLayoutDbfRecord modularBundleLayoutDbfRecord = regionNodeLayoutsForBundle[this.m_lastBundleIndex];
		Network.Bundle currentMoneyBundle = base.GetCurrentMoneyBundle();
		bool isPreorder = StoreManager.Get().IsProductPrePurchase(currentMoneyBundle);
		this.UpdatePacksDescription(modularBundleLayoutDbfRecord.DescriptionHeadline, modularBundleLayoutDbfRecord.Description, modularBundleLayoutDbfRecord.AccentTexture, isPreorder);
	}

	// Token: 0x06006419 RID: 25625 RVA: 0x00209F94 File Offset: 0x00208194
	private void UpdatePacksDescription(string packDescriptionHeadline, string packDescription, string accentTextureName, bool isPreorder)
	{
		string warning = string.Empty;
		if (StoreManager.Get().IsKoreanCustomer())
		{
			if (isPreorder)
			{
				warning = GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_PACKS_PREORDER");
			}
			else if (GameUtils.IsFirstPurchaseBundleBooster(this.m_selectedStorePackId))
			{
				warning = GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_FIRST_PURCHASE_BUNDLE");
			}
			else if (base.GetCurrentMoneyBundle() != null && AdventureUtils.IsAdventureBundle(base.GetCurrentMoneyBundle()))
			{
				warning = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_BUNDLE");
			}
			else
			{
				warning = GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_EXPERT_PACK");
			}
		}
		this.m_parentStore.SetDescription(packDescriptionHeadline, packDescription, warning);
		using (AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(accentTextureName, AssetLoadingOptions.None))
		{
			this.m_parentStore.SetAccentTexture(assetHandle);
		}
	}

	// Token: 0x0600641A RID: 25626 RVA: 0x0020A054 File Offset: 0x00208254
	private NoGTAPPTransactionData GetCurrentGTAPPTransactionData()
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(this.m_selectedStorePackId);
		return new NoGTAPPTransactionData
		{
			Product = productTypeFromStorePackType,
			ProductData = this.m_selectedStorePackId.Id,
			Quantity = this.m_currentGoldPackQuantity
		};
	}

	// Token: 0x0600641B RID: 25627 RVA: 0x0020A098 File Offset: 0x00208298
	private void UpdatePackBuyButtons()
	{
		if (this.IsPackIdInvalid(this.m_selectedStorePackId))
		{
			return;
		}
		Network.Bundle bundle;
		if (StoreManager.Get().IsBoosterHiddenLicenseBundle(this.m_selectedStorePackId, out bundle) && this.m_selectedStorePackId.Type != StorePackType.MODULAR_BUNDLE)
		{
			this.ShowHiddenLicenseBundleBuyButtons(bundle);
			return;
		}
		if (this.m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			this.ShowModularBundleBuyButtons();
			return;
		}
		this.ShowStandardBuyButtons();
	}

	// Token: 0x0600641C RID: 25628 RVA: 0x0020A0F9 File Offset: 0x002082F9
	private static Network.BundleItem GetPacksBundleItemFromBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return null;
		}
		return bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER);
	}

	// Token: 0x0600641D RID: 25629 RVA: 0x0020A12C File Offset: 0x0020832C
	private void ShowStandardBuyButtons()
	{
		this.m_packBuyPreorderContainer.SetActive(false);
		this.m_packBuyContainer.SetActive(true);
		Action action = null;
		this.ClearButtonEventListeners();
		int num = 0;
		GeneralStorePackBuyButton goldButton = this.GetPackBuyButton(num);
		if (goldButton == null)
		{
			goldButton = this.CreatePackBuyButton(num);
		}
		goldButton.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
		{
			if (!this.IsContentActive())
			{
				return;
			}
			this.HandleGoldPackBuyButtonClick();
			this.SelectPackBuyButton(goldButton);
		});
		if (!UniversalInputManager.UsePhoneUI)
		{
			goldButton.AddEventListener(UIEventType.DOUBLECLICK, delegate(UIEvent e)
			{
				this.HandleGoldPackBuyButtonDoubleClick(goldButton);
			});
		}
		if (!this.IsPackIdInvalid(this.m_selectedStorePackId))
		{
			goldButton.UpdateFromGTAPP(this.GetCurrentGTAPPTransactionData());
		}
		action = delegate()
		{
			this.HandleGoldPackBuyButtonClick();
			this.SelectPackBuyButton(goldButton);
		};
		goldButton.Unselect();
		List<Network.Bundle> list = this.GetPackBundles(true);
		if (list.Count > this.m_maxPackBuyButtons - 1)
		{
			list = list.GetRange(0, this.m_maxPackBuyButtons - 1);
		}
		bool flag = false;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < list.Count; i++)
		{
			num++;
			int bundleIndexCopy = i;
			Network.Bundle bundle = list[i];
			Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER);
			if (bundleItem == null)
			{
				Debug.LogWarning(string.Format("GeneralStorePacksContent.UpdatePackBuyButtons() bundle {0} has no packs bundle item!", bundle.PMTProductID));
			}
			else
			{
				GeneralStorePackBuyButton moneyButton = this.GetPackBuyButton(num);
				if (moneyButton == null)
				{
					moneyButton = this.CreatePackBuyButton(num);
				}
				moneyButton.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
				{
					if (!this.IsContentActive())
					{
						return;
					}
					this.HandleMoneyPackBuyButtonClick(bundleIndexCopy);
					this.SelectPackBuyButton(moneyButton);
				});
				string packBuyButtonText = this.GetPackBuyButtonText(bundle, bundleItem, false);
				if (bundleItem.BaseQuantity > 0)
				{
					if (!flag)
					{
						flag = true;
						num2 = num;
					}
					num3 = num;
				}
				moneyButton.SetMoneyValue(bundle, bundleItem, packBuyButtonText);
				moneyButton.gameObject.SetActive(true);
				if (moneyButton.IsSelected() || base.GetCurrentMoneyBundle() == bundle)
				{
					action = delegate()
					{
						this.HandleMoneyPackBuyButtonClick(bundleIndexCopy);
						this.SelectPackBuyButton(moneyButton);
					};
				}
				moneyButton.Unselect();
			}
		}
		bool active = StoreManager.Get().CanBuyStorePackWithGold(this.m_selectedStorePackId);
		goldButton.gameObject.SetActive(active);
		for (int j = num + 1; j < this.m_packBuyButtons.Count; j++)
		{
			GeneralStorePackBuyButton generalStorePackBuyButton = this.m_packBuyButtons[j];
			if (generalStorePackBuyButton != null)
			{
				generalStorePackBuyButton.gameObject.SetActive(false);
			}
		}
		int num4 = num + 1;
		this.UpdateToggleableSections(this.m_toggleableButtonFrames, num4);
		bool flag2 = num4 >= this.m_toggleableButtonFrames.Count;
		foreach (GeneralStorePacksContent.MultiSliceEndCaps multiSliceEndCaps in this.m_buyBarEndCaps)
		{
			multiSliceEndCaps.m_FullBar.SetActive(flag2);
			multiSliceEndCaps.m_SmallerBar.SetActive(!flag2);
		}
		if (this.m_packBuyFrameContainer != null)
		{
			this.m_packBuyFrameContainer.UpdateSlices();
		}
		this.m_packBuyButtonContainer.UpdateSlices();
		if (action != null)
		{
			action();
		}
		if (this.m_packBuyBonusCallout != null)
		{
			if (this.m_packBuyBonusCalloutOnlyOncePerSession && this.m_packBuyBonusCalloutSeenForPackId.Contains(this.m_selectedStorePackId))
			{
				flag = false;
			}
			if (flag || this.m_packBuyBonusCalloutDebugForceDisplay > 0)
			{
				this.HideBonusPacksText();
				int num5;
				GeneralStorePackBuyButton packBuyButton;
				GeneralStorePackBuyButton packBuyButton2;
				if (this.m_packBuyBonusCalloutDebugForceDisplay > 0)
				{
					num5 = this.m_packBuyBonusCalloutDebugForceDisplay;
					packBuyButton = this.GetPackBuyButton(Math.Max(num - (num5 - 1), 0));
					packBuyButton2 = this.GetPackBuyButton(num);
				}
				else
				{
					num5 = 1 + Math.Max(num3 - num2, 0);
					packBuyButton = this.GetPackBuyButton(num2);
					packBuyButton2 = this.GetPackBuyButton(num3);
				}
				if (this.m_bonusPacksCalloutCoroutine != null)
				{
					base.StopCoroutine(this.m_bonusPacksCalloutCoroutine);
				}
				this.m_bonusPacksCalloutCoroutine = base.StartCoroutine(this.DelayedShowBonusPacksCallout(1f, packBuyButton, packBuyButton2, num5));
				return;
			}
			this.m_packBuyBonusCallout.HideCallout();
		}
	}

	// Token: 0x0600641E RID: 25630 RVA: 0x0020A578 File Offset: 0x00208778
	private IEnumerator DelayedShowBonusPacksCallout(float delay, GeneralStorePackBuyButton firstButton, GeneralStorePackBuyButton lastButton, int numButtons)
	{
		yield return new WaitForSeconds(delay);
		this.m_packBuyBonusCallout.ShowCallout(firstButton, lastButton, numButtons);
		yield break;
	}

	// Token: 0x0600641F RID: 25631 RVA: 0x0020A5A4 File Offset: 0x002087A4
	private void UpdateBonusPacksUI(Network.Bundle bundle)
	{
		int num = 0;
		if (bundle != null)
		{
			Network.BundleItem packsBundleItemFromBundle = GeneralStorePacksContent.GetPacksBundleItemFromBundle(bundle);
			if (packsBundleItemFromBundle != null && packsBundleItemFromBundle.BaseQuantity > 0)
			{
				num = Math.Max(packsBundleItemFromBundle.Quantity - packsBundleItemFromBundle.BaseQuantity, 0);
			}
		}
		if (num > 0)
		{
			if (this.m_packBuyBonusCallout != null)
			{
				if (this.m_packBuyBonusCallout.IsShown())
				{
					this.m_packBuyBonusCalloutSeenForPackId.Add(this.m_selectedStorePackId);
				}
				this.m_packBuyBonusCallout.HideCallout();
			}
			this.ShowBonusPacksText(num, StoreManager.Get().ShouldShowFeaturedDustJar(bundle));
			return;
		}
		this.HideBonusPacksText();
	}

	// Token: 0x06006420 RID: 25632 RVA: 0x0020A634 File Offset: 0x00208834
	private void ShowBonusPacksText(int numBonusPacks, bool isShowingDustJar)
	{
		this.m_packBuyBonusText.gameObject.SetActive(true);
		if (isShowingDustJar)
		{
			this.m_packBuyBonusText.Text = GameStrings.Format("GLUE_CHINA_STORE_DUST_PLUS_BONUS_DETAILED", new object[]
			{
				numBonusPacks,
				numBonusPacks
			});
			return;
		}
		this.m_packBuyBonusText.Text = GameStrings.Format("GLUE_STORE_BONUS_PACKS", new object[]
		{
			numBonusPacks
		});
	}

	// Token: 0x06006421 RID: 25633 RVA: 0x0020A6A9 File Offset: 0x002088A9
	private void HideBonusPacksText()
	{
		this.m_packBuyBonusText.gameObject.SetActive(false);
	}

	// Token: 0x06006422 RID: 25634 RVA: 0x0020A6BC File Offset: 0x002088BC
	private void UpdateToggleableSections(List<GeneralStorePacksContent.ToggleableButtonFrame> sections, int numSectionsNeeded)
	{
		int num = numSectionsNeeded - 1;
		for (int i = 0; i < sections.Count; i++)
		{
			GeneralStorePacksContent.ToggleableButtonFrame toggleableButtonFrame = sections[i];
			bool active = i <= num;
			if (toggleableButtonFrame.m_IBar != null)
			{
				toggleableButtonFrame.m_IBar.SetActive(active);
			}
			toggleableButtonFrame.m_Middle.SetActive(active);
		}
	}

	// Token: 0x06006423 RID: 25635 RVA: 0x0020A714 File Offset: 0x00208914
	private void ShowModularBundleBuyButtons()
	{
		Action action = null;
		Action<GeneralStorePackBuyButton> selectButtonFunc = null;
		ModularBundleDbfRecord record = GameDbf.ModularBundle.GetRecord(this.m_selectedStorePackId.Id);
		GeneralStorePacksContent.ModularBundleLayoutButtonSize modularBundleLayoutButtonSize = EnumUtils.SafeParse<GeneralStorePacksContent.ModularBundleLayoutButtonSize>(record.LayoutButtonSize, GeneralStorePacksContent.ModularBundleLayoutButtonSize.None, true);
		bool flag = modularBundleLayoutButtonSize == GeneralStorePacksContent.ModularBundleLayoutButtonSize.Large;
		Func<int, GeneralStorePackBuyButton> func;
		Func<int, GeneralStorePackBuyButton> func2;
		MultiSliceElement multiSliceElement;
		MultiSliceElement multiSliceElement2;
		List<GeneralStorePacksContent.ToggleableButtonFrame> sections;
		List<GeneralStorePackBuyButton> list;
		if (flag)
		{
			this.m_packBuyContainer.SetActive(false);
			this.m_packBuyPreorderContainer.SetActive(true);
			func = new Func<int, GeneralStorePackBuyButton>(this.GetPackPreorderBuyButton);
			func2 = new Func<int, GeneralStorePackBuyButton>(this.CreatePackPreorderBuyButton);
			selectButtonFunc = new Action<GeneralStorePackBuyButton>(this.SelectPackBuyPreorderButton);
			multiSliceElement = this.m_packBuyPreorderFrameContainer;
			multiSliceElement2 = this.m_packBuyPreorderButtonContainer;
			sections = this.m_toggleablePreorderButtonFrames;
			list = this.m_packPreorderBuyButtons;
		}
		else
		{
			this.m_packBuyContainer.SetActive(true);
			this.m_packBuyPreorderContainer.SetActive(false);
			func = new Func<int, GeneralStorePackBuyButton>(this.GetPackBuyButton);
			func2 = new Func<int, GeneralStorePackBuyButton>(this.CreatePackBuyButton);
			selectButtonFunc = new Action<GeneralStorePackBuyButton>(this.SelectPackBuyButton);
			multiSliceElement = this.m_packBuyFrameContainer;
			multiSliceElement2 = this.m_packBuyButtonContainer;
			sections = this.m_toggleableButtonFrames;
			list = this.m_packBuyButtons;
		}
		bool isDev = !HearthstoneApplication.IsPublic() && Vars.Key("ModularBundle.ShowAll").GetBool(false);
		ModularBundleLayoutDbfRecord[] array = StoreManager.Get().GetRegionNodeLayoutsForBundle(record.ID).ToArray();
		if (array.Length < 2 || modularBundleLayoutButtonSize == GeneralStorePacksContent.ModularBundleLayoutButtonSize.None)
		{
			this.m_packBuyContainer.SetActive(false);
			this.m_packBuyPreorderContainer.SetActive(false);
			this.HandleMoneyBuyModularBundleButtonClick(0, isDev);
			return;
		}
		this.ClearButtonEventListeners();
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			GeneralStorePackBuyButton moneyButton = func(num);
			int bundleIndexCopy = i;
			if (moneyButton == null)
			{
				moneyButton = func2(i);
			}
			moneyButton.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				if (!this.IsContentActive())
				{
					return;
				}
				this.HandleMoneyBuyModularBundleButtonClick(bundleIndexCopy, isDev);
				selectButtonFunc(moneyButton);
			});
			if (i == 0)
			{
				action = delegate()
				{
					selectButtonFunc(moneyButton);
				};
			}
			int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(this.m_selectedStorePackId, i);
			Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, true, productDataFromStorePackId, 0, true).FirstOrDefault<Network.Bundle>();
			if (isDev || bundle != null)
			{
				Network.BundleItem packsBundleItemFromBundle = GeneralStorePacksContent.GetPacksBundleItemFromBundle(bundle);
				string packBuyButtonText = this.GetPackBuyButtonText(bundle, packsBundleItemFromBundle, flag);
				moneyButton.SetMoneyValue(bundle, packsBundleItemFromBundle, packBuyButtonText);
				moneyButton.gameObject.SetActive(true);
				if (moneyButton.IsSelected() || base.GetCurrentMoneyBundle() == bundle)
				{
					action = delegate()
					{
						this.HandleMoneyBuyModularBundleButtonClick(bundleIndexCopy, isDev);
						selectButtonFunc(moneyButton);
					};
				}
				moneyButton.Unselect();
				num++;
			}
		}
		if (num == 0)
		{
			this.m_packBuyPreorderContainer.SetActive(false);
			this.m_packBuyContainer.SetActive(false);
		}
		for (int j = num; j < list.Count; j++)
		{
			GeneralStorePackBuyButton generalStorePackBuyButton = list[j];
			if (generalStorePackBuyButton != null)
			{
				generalStorePackBuyButton.gameObject.SetActive(false);
			}
		}
		this.UpdateToggleableSections(sections, num);
		if (multiSliceElement != null)
		{
			multiSliceElement.UpdateSlices();
		}
		multiSliceElement2.UpdateSlices();
		if (action != null)
		{
			action();
		}
		this.UpdatePacksDescriptionFromSelectedStorePack();
	}

	// Token: 0x06006424 RID: 25636 RVA: 0x0020AA74 File Offset: 0x00208C74
	public string GetPackBuyButtonText(Network.Bundle bundle, Network.BundleItem bundleItem, bool useLargeButtons = false)
	{
		if (bundle == null || bundleItem == null)
		{
			return string.Empty;
		}
		bool flag = StoreManager.Get().ShouldShowFeaturedDustJar(bundle);
		if (useLargeButtons)
		{
			if (flag)
			{
				return GameStrings.Format("GLUE_STORE_QUANTITY_DUST_BUNDLE", new object[]
				{
					bundleItem.Quantity
				});
			}
			return GameStrings.Format("GLUE_STORE_QUANTITY_PACK_BUNDLE", new object[]
			{
				bundleItem.Quantity
			});
		}
		else
		{
			if (flag)
			{
				return StoreManager.Get().GetProductQuantityText(ProductType.PRODUCT_TYPE_CURRENCY, bundleItem.ProductData, bundleItem.Quantity, bundleItem.BaseQuantity);
			}
			return StoreManager.Get().GetProductQuantityText(bundleItem.ItemType, bundleItem.ProductData, bundleItem.Quantity, bundleItem.BaseQuantity);
		}
	}

	// Token: 0x06006425 RID: 25637 RVA: 0x0020AB24 File Offset: 0x00208D24
	private void ShowHiddenLicenseBundleBuyButtons(Network.Bundle bundle)
	{
		this.m_packBuyContainer.SetActive(false);
		this.m_packBuyPreorderContainer.SetActive(false);
		int firstValidBundleIndex = this.GetFirstValidBundleIndex(this.m_selectedStorePackId);
		this.HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
	}

	// Token: 0x06006426 RID: 25638 RVA: 0x0020AB60 File Offset: 0x00208D60
	private void UpdatePacksTypeMusic()
	{
		if (this.m_parentStore.GetMode() == GeneralStoreMode.NONE)
		{
			return;
		}
		IStorePackDef storePackDef = this.GetStorePackDef(this.m_selectedStorePackId);
		if (storePackDef == null || storePackDef.GetPlaylist() == MusicPlaylistType.Invalid || !MusicManager.Get().StartPlaylist(storePackDef.GetPlaylist()))
		{
			this.m_parentStore.ResumePreviousMusicPlaylist();
		}
	}

	// Token: 0x06006427 RID: 25639 RVA: 0x0020ABB0 File Offset: 0x00208DB0
	private void HandleGoldPackBuyButtonClick()
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(this.m_selectedStorePackId);
		base.SetCurrentGoldBundle(new NoGTAPPTransactionData
		{
			Product = productTypeFromStorePackType,
			ProductData = this.m_selectedStorePackId.Id,
			Quantity = this.m_currentGoldPackQuantity
		});
		this.UpdatePacksDescriptionFromSelectedStorePack();
	}

	// Token: 0x06006428 RID: 25640 RVA: 0x0020AC00 File Offset: 0x00208E00
	private void HandleGoldPackBuyButtonDoubleClick(GeneralStorePackBuyButton button)
	{
		if (this.m_selectedStorePackId.Type == StorePackType.BOOSTER)
		{
			TelemetryManager.Client().SendChangePackQuantity(this.m_selectedStorePackId.Id);
		}
		this.m_parentStore.BlockInterface(true);
		this.m_quantityPrompt.Show(GeneralStorePacksContent.MAX_QUANTITY_BOUGHT_WITH_GOLD, delegate(int quantity)
		{
			this.m_parentStore.BlockInterface(false);
			this.m_currentGoldPackQuantity = quantity;
			NoGTAPPTransactionData currentGTAPPTransactionData = this.GetCurrentGTAPPTransactionData();
			button.UpdateFromGTAPP(currentGTAPPTransactionData);
			this.SetCurrentGoldBundle(currentGTAPPTransactionData);
		}, delegate()
		{
			this.m_parentStore.BlockInterface(false);
		});
	}

	// Token: 0x06006429 RID: 25641 RVA: 0x0020AC7C File Offset: 0x00208E7C
	private void HandleMoneyPackBuyButtonClick(int bundleIndex)
	{
		Network.Bundle bundle = null;
		List<Network.Bundle> packBundles = this.GetPackBundles(true);
		if (packBundles != null && packBundles.Count > 0)
		{
			if (bundleIndex >= packBundles.Count)
			{
				bundleIndex = 0;
			}
			bundle = packBundles[bundleIndex];
		}
		base.SetCurrentMoneyBundle(bundle, true);
		this.m_lastBundleIndex = bundleIndex;
		this.UpdatePacksDescriptionFromSelectedStorePack();
		this.UpdateBonusPacksUI(bundle);
	}

	// Token: 0x0600642A RID: 25642 RVA: 0x0020ACD0 File Offset: 0x00208ED0
	private void HandleMoneyBuyModularBundleButtonClick(int bundleIndex, bool isDev = false)
	{
		List<ModularBundleLayoutDbfRecord> records = GameDbf.ModularBundleLayout.GetRecords((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == this.m_selectedStorePackId.Id, -1);
		if (bundleIndex >= records.Count)
		{
			bundleIndex = 0;
		}
		int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(this.m_selectedStorePackId, bundleIndex);
		Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, true, productDataFromStorePackId, 0, true).FirstOrDefault<Network.Bundle>();
		if (!isDev && bundle == null)
		{
			return;
		}
		this.m_lastBundleIndex = bundleIndex;
		base.SetCurrentMoneyBundle(bundle, true);
		this.UpdatePacksDescriptionFromSelectedStorePack();
	}

	// Token: 0x0600642B RID: 25643 RVA: 0x0020AD40 File Offset: 0x00208F40
	private void SelectPackBuyButton(GeneralStorePackBuyButton packBuyBtn)
	{
		foreach (GeneralStorePackBuyButton generalStorePackBuyButton in this.m_packBuyButtons)
		{
			generalStorePackBuyButton.Unselect();
		}
		packBuyBtn.Select();
	}

	// Token: 0x0600642C RID: 25644 RVA: 0x0020AD98 File Offset: 0x00208F98
	private void SelectPackBuyPreorderButton(GeneralStorePackBuyButton packBuyBtn)
	{
		foreach (GeneralStorePackBuyButton generalStorePackBuyButton in this.m_packPreorderBuyButtons)
		{
			generalStorePackBuyButton.Unselect();
		}
		packBuyBtn.Select();
	}

	// Token: 0x0600642D RID: 25645 RVA: 0x0020ADF0 File Offset: 0x00208FF0
	private GeneralStorePackBuyButton GetPackBuyButton(int index)
	{
		if (index < this.m_packBuyButtons.Count)
		{
			return this.m_packBuyButtons[index];
		}
		return null;
	}

	// Token: 0x0600642E RID: 25646 RVA: 0x0020AE10 File Offset: 0x00209010
	private GeneralStorePackBuyButton CreatePackBuyButton(int buttonIndex)
	{
		if (buttonIndex >= this.m_packBuyButtons.Count)
		{
			int num = buttonIndex - this.m_packBuyButtons.Count + 1;
			for (int i = 0; i < num; i++)
			{
				GeneralStorePackBuyButton generalStorePackBuyButton = (GeneralStorePackBuyButton)GameUtils.Instantiate(this.m_packBuyButtonPrefab, this.m_packBuyButtonContainer.gameObject, true);
				SceneUtils.SetLayer(generalStorePackBuyButton.gameObject, this.m_packBuyButtonContainer.gameObject.layer, null);
				generalStorePackBuyButton.transform.localRotation = Quaternion.identity;
				generalStorePackBuyButton.transform.localScale = Vector3.one;
				this.m_packBuyButtonContainer.AddSlice(generalStorePackBuyButton.gameObject);
				this.m_packBuyButtons.Add(generalStorePackBuyButton);
			}
			this.m_packBuyButtonContainer.UpdateSlices();
		}
		return this.m_packBuyButtons[buttonIndex];
	}

	// Token: 0x0600642F RID: 25647 RVA: 0x0020AEE5 File Offset: 0x002090E5
	private GeneralStorePackBuyButton GetPackPreorderBuyButton(int index)
	{
		if (index < this.m_packPreorderBuyButtons.Count)
		{
			return this.m_packPreorderBuyButtons[index];
		}
		return null;
	}

	// Token: 0x06006430 RID: 25648 RVA: 0x0020AF04 File Offset: 0x00209104
	private GeneralStorePackBuyButton CreatePackPreorderBuyButton(int buttonIndex)
	{
		if (buttonIndex >= this.m_packPreorderBuyButtons.Count)
		{
			int num = buttonIndex - this.m_packPreorderBuyButtons.Count + 1;
			for (int i = 0; i < num; i++)
			{
				GeneralStorePackBuyButton generalStorePackBuyButton = (GeneralStorePackBuyButton)GameUtils.Instantiate(this.m_packBuyPreorderButtonPrefab, this.m_packBuyPreorderButtonContainer.gameObject, true);
				SceneUtils.SetLayer(generalStorePackBuyButton.gameObject, this.m_packBuyPreorderButtonContainer.gameObject.layer, null);
				generalStorePackBuyButton.transform.localRotation = Quaternion.identity;
				generalStorePackBuyButton.transform.localScale = Vector3.one;
				this.m_packBuyPreorderButtonContainer.AddSlice(generalStorePackBuyButton.gameObject);
				this.m_packPreorderBuyButtons.Add(generalStorePackBuyButton);
			}
			this.m_packBuyPreorderButtonContainer.UpdateSlices();
		}
		return this.m_packPreorderBuyButtons[buttonIndex];
	}

	// Token: 0x06006431 RID: 25649 RVA: 0x0020AFDC File Offset: 0x002091DC
	private List<Network.Bundle> GetPackBundles(bool sortByPackQuantity)
	{
		ProductType selectedProductType = StorePackId.GetProductTypeFromStorePackType(this.m_selectedStorePackId);
		List<Network.Bundle> list = new List<Network.Bundle>();
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(this.m_selectedStorePackId);
		int i;
		for (i = 0; i < productDataCountFromStorePackId; i++)
		{
			List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(selectedProductType, true, GameUtils.GetProductDataFromStorePackId(this.m_selectedStorePackId, i), 0, true);
			list = list.Concat(allBundlesForProduct).ToList<Network.Bundle>();
		}
		if (!GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
		{
			list.RemoveAll((Network.Bundle obj) => obj.Items.Find((Network.BundleItem item) => item.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE) != null);
		}
		if (sortByPackQuantity)
		{
			Func<Network.BundleItem, bool> <>9__3;
			Func<Network.BundleItem, bool> <>9__5;
			list.Sort(delegate(Network.Bundle left, Network.Bundle right)
			{
				int num;
				if (left != null)
				{
					IEnumerable<Network.BundleItem> items = left.Items;
					Func<Network.BundleItem, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((Network.BundleItem i) => i.ItemType == selectedProductType));
					}
					num = items.Where(predicate).Max((Network.BundleItem i) => i.Quantity);
				}
				else
				{
					num = 0;
				}
				int num2;
				if (right != null)
				{
					IEnumerable<Network.BundleItem> items2 = right.Items;
					Func<Network.BundleItem, bool> predicate2;
					if ((predicate2 = <>9__5) == null)
					{
						predicate2 = (<>9__5 = ((Network.BundleItem i) => i.ItemType == selectedProductType));
					}
					num2 = items2.Where(predicate2).Max((Network.BundleItem i) => i.Quantity);
				}
				else
				{
					num2 = 0;
				}
				int num3 = num2;
				return num - num3;
			});
		}
		return list;
	}

	// Token: 0x06006432 RID: 25650 RVA: 0x0020B098 File Offset: 0x00209298
	private void AnimateLogo(bool animateLogo, bool isFirstStoreOpen = false)
	{
		if (!this.m_hasLogo || !base.gameObject.activeInHierarchy || this.IsPackIdInvalid(this.m_selectedStorePackId))
		{
			return;
		}
		MeshRenderer currentLogo = this.GetCurrentLogo();
		GeneralStorePacksContent.LogoAnimation logoAnimation = this.m_logoAnimation;
		if (logoAnimation != GeneralStorePacksContent.LogoAnimation.Slam)
		{
			if (logoAnimation != GeneralStorePacksContent.LogoAnimation.Fade)
			{
				return;
			}
			if (animateLogo)
			{
				this.m_logoAnimCoroutine = base.StartCoroutine(this.AnimateFadeLogo(currentLogo));
				return;
			}
			if (!this.m_animatingLogo)
			{
				currentLogo.gameObject.SetActive(false);
			}
		}
		else
		{
			if (animateLogo)
			{
				this.m_logoAnimCoroutine = base.StartCoroutine(this.AnimateSlamLogo(currentLogo));
				return;
			}
			if (!this.m_animatingLogo && !isFirstStoreOpen)
			{
				currentLogo.transform.localPosition = this.m_logoAnimationEndBone.transform.localPosition;
				currentLogo.gameObject.SetActive(true);
				return;
			}
		}
	}

	// Token: 0x06006433 RID: 25651 RVA: 0x0020B158 File Offset: 0x00209358
	private void AnimatePacksFlying(int numVisiblePacks, bool forceImmediate = false, float delay = 0f, bool showAsSingleStack = false, bool waitForLogo = true)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (this.m_packAnimCoroutine != null)
		{
			base.StopCoroutine(this.m_packAnimCoroutine);
		}
		if (this.m_limitedTimeOfferAnimCoroutine != null)
		{
			base.StopCoroutine(this.m_limitedTimeOfferAnimCoroutine);
		}
		if (GameUtils.IsHiddenLicenseBundleBooster(this.m_selectedStorePackId))
		{
			if (this.m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				this.m_packAnimCoroutine = base.StartCoroutine(this.AnimateModularBundle(currentDisplay, forceImmediate, delay, waitForLogo));
			}
			else if (showAsSingleStack && GameUtils.IsFirstPurchaseBundleBooster(this.m_selectedStorePackId))
			{
				this.m_packAnimCoroutine = base.StartCoroutine(this.AnimatePacks(currentDisplay, numVisiblePacks, forceImmediate, showAsSingleStack, waitForLogo));
			}
			else
			{
				if (StoreManager.IsHiddenLicenseBundleOwned(GameUtils.GetProductDataFromStorePackId(this.m_selectedStorePackId, this.m_lastBundleIndex)) && GameUtils.IsFirstPurchaseBundleBooster(this.m_selectedStorePackId))
				{
					forceImmediate = true;
				}
				this.m_packAnimCoroutine = base.StartCoroutine(this.AnimateBundleBox(currentDisplay, delay, forceImmediate));
			}
		}
		else
		{
			this.m_packAnimCoroutine = base.StartCoroutine(this.AnimatePacks(currentDisplay, numVisiblePacks, forceImmediate, showAsSingleStack, waitForLogo));
		}
		this.m_limitedTimeOfferAnimCoroutine = base.StartCoroutine(this.AnimateLimitedTimeOfferUI(currentDisplay, waitForLogo));
	}

	// Token: 0x06006434 RID: 25652 RVA: 0x0020B275 File Offset: 0x00209475
	private IEnumerator AnimateFadeLogo(MeshRenderer logo)
	{
		if (logo == null || !this.m_hasLogo || !logo.transform.parent.gameObject.activeInHierarchy)
		{
			yield break;
		}
		while (this.m_animatingLogo || this.m_loadingLogoTexture || this.m_loadingLogoGlowTexture)
		{
			yield return null;
		}
		logo.gameObject.SetActive(true);
		this.m_animatingLogo = true;
		PlayMakerFSM logoFSM = logo.GetComponent<PlayMakerFSM>();
		logo.transform.localPosition = this.m_logoAnimationStartBone.transform.localPosition;
		iTween.MoveFrom(logo.gameObject, iTween.Hash(new object[]
		{
			"position",
			logo.transform.localPosition - this.m_logoAppearOffset,
			"easetype",
			iTween.EaseType.easeInQuint,
			"time",
			this.m_logoIntroTime,
			"islocal",
			true
		}));
		AnimationUtil.FadeTexture(logo, 0f, 1f, this.m_logoIntroTime, 0f, null);
		if (logoFSM != null)
		{
			logoFSM.SendEvent("FadeIn");
		}
		if (this.m_logoHoldTime > 0f)
		{
			yield return new WaitForSeconds(this.m_logoHoldTime);
		}
		AnimationUtil.FadeTexture(logo, 1f, 0f, this.m_logoOutroTime, 0f, null);
		if (logoFSM != null)
		{
			logoFSM.SendEvent("FadeOut");
		}
		yield return new WaitForSeconds(this.m_logoOutroTime);
		this.m_animatingLogo = false;
		yield break;
	}

	// Token: 0x06006435 RID: 25653 RVA: 0x0020B28B File Offset: 0x0020948B
	private IEnumerator AnimateSlamLogo(MeshRenderer logo)
	{
		if (logo == null || !this.m_hasLogo || !logo.transform.parent.gameObject.activeInHierarchy)
		{
			yield break;
		}
		while (this.m_animatingLogo || this.m_loadingLogoTexture || this.m_loadingLogoGlowTexture)
		{
			yield return null;
		}
		logo.gameObject.SetActive(true);
		this.m_animatingLogo = true;
		PlayMakerFSM logoFSM = logo.GetComponent<PlayMakerFSM>();
		logo.transform.localPosition = this.m_logoAnimationStartBone.transform.localPosition;
		iTween.MoveFrom(logo.gameObject, iTween.Hash(new object[]
		{
			"position",
			logo.transform.localPosition - this.m_logoAppearOffset,
			"easetype",
			iTween.EaseType.easeInQuint,
			"time",
			this.m_logoIntroTime,
			"islocal",
			true
		}));
		AnimationUtil.FadeTexture(logo, 0f, 1f, this.m_logoIntroTime, 0f, null);
		if (logoFSM != null)
		{
			logoFSM.SendEvent("FadeIn");
		}
		yield return new WaitForSeconds(this.m_logoIntroTime);
		if (this.m_logoHoldTime > 0f)
		{
			yield return new WaitForSeconds(this.m_logoHoldTime);
		}
		iTween.MoveTo(logo.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.m_logoAnimationEndBone.transform.localPosition,
			"easetype",
			iTween.EaseType.easeInQuint,
			"time",
			this.m_logoOutroTime,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(this.m_logoOutroTime);
		if (logoFSM != null)
		{
			logoFSM.SendEvent("PostSlamIn");
		}
		base.gameObject.transform.localPosition = this.m_savedLocalPosition;
		iTween.Stop(base.gameObject);
		iTween.PunchScale(base.gameObject, this.m_punchAmount, this.m_logoDisplayPunchTime);
		yield return new WaitForSeconds(this.m_logoDisplayPunchTime * 0.5f);
		this.m_animatingLogo = false;
		yield break;
	}

	// Token: 0x06006436 RID: 25654 RVA: 0x0020B2A1 File Offset: 0x002094A1
	private IEnumerator AnimatePacks(GeneralStorePacksContentDisplay display, int numVisiblePacks, bool forceImmediate, bool showAsSingleStack, bool waitForLogo)
	{
		while ((this.m_animatingLogo || this.m_loadingLogoTexture || this.m_loadingLogoGlowTexture) && waitForLogo)
		{
			yield return null;
		}
		this.StartAnimatingPacks();
		int num = display.ShowPacks(numVisiblePacks, this.m_packFlyInAnimTime, this.m_packFlyOutAnimTime, this.m_packFlyInDelay, this.m_packFlyOutDelay, forceImmediate, showAsSingleStack);
		if (!forceImmediate && num != 0)
		{
			int num2 = Mathf.Abs(num);
			float maxXRotation = (num2 > 0) ? this.m_maxPackFlyInXShake : this.m_maxPackFlyOutXShake;
			float num3 = (num2 > 0) ? this.m_packFlyInDelay : this.m_packFlyOutDelay;
			this.ShakeStore(num2, maxXRotation, (float)num2 * num3 * this.m_shakeObjectDelayMultiplier, 0f, 0);
			yield return new WaitForSeconds(num3);
		}
		this.DoneAnimatingPacks();
		yield break;
	}

	// Token: 0x06006437 RID: 25655 RVA: 0x0020B2D8 File Offset: 0x002094D8
	public void AnimateModularBundleAfterPurchase(StorePackId storePack)
	{
		List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(storePack.Id);
		if (this.m_lastBundleIndex >= regionNodeLayoutsForBundle.Count)
		{
			Log.Store.PrintWarning(string.Format("Selected invalid layout at index={0}. Skipping post-purchase animation.", this.m_lastBundleIndex), Array.Empty<object>());
			return;
		}
		if (!regionNodeLayoutsForBundle[this.m_lastBundleIndex].AnimateAfterPurchase)
		{
			return;
		}
		base.StartCoroutine(this.AnimateModularBundle(this.GetCurrentDisplay(), false, 0f, true));
	}

	// Token: 0x06006438 RID: 25656 RVA: 0x0020B357 File Offset: 0x00209557
	private IEnumerator AnimateModularBundle(GeneralStorePacksContentDisplay display, bool forceImmediate, float delayAnim, bool waitForLogo)
	{
		while ((this.m_animatingLogo || this.m_loadingLogoTexture || this.m_loadingLogoGlowTexture || this.m_animatingPacks) && waitForLogo)
		{
			yield return null;
		}
		this.StartAnimatingPacks();
		ModularBundleDbfRecord record = GameDbf.ModularBundle.GetRecord(this.m_selectedStorePackId.Id);
		float storeShakeDelay = 0f;
		int storeShakeAmount = 0;
		ModularBundleNodeLayout previousBundle = null;
		int nodesAnimatingIn = display.ShowModularBundle(record, forceImmediate, out storeShakeDelay, out storeShakeAmount, out previousBundle, this.m_lastBundleIndex);
		if (!forceImmediate && nodesAnimatingIn != 0)
		{
			while (previousBundle != null && previousBundle.IsAnimating)
			{
				yield return null;
			}
			this.ShakeStore(nodesAnimatingIn, this.m_maxPackFlyInXShake, storeShakeDelay, 0f, storeShakeAmount);
			yield return new WaitForSeconds(delayAnim + 1f);
		}
		yield break;
	}

	// Token: 0x06006439 RID: 25657 RVA: 0x0020B383 File Offset: 0x00209583
	private IEnumerator ShowFeaturedDustJar(bool waitForLogo = false)
	{
		while ((this.m_animatingLogo || this.m_loadingLogoTexture || this.m_loadingLogoGlowTexture) && waitForLogo)
		{
			yield return null;
		}
		if (this.m_visibleDustCount == 0)
		{
			this.HideDust();
			yield break;
		}
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay != null)
		{
			base.StartCoroutine(currentDisplay.ShowDustJar(this.m_visibleDustCount, this.m_visibleDustBonusCount, this.m_selectedBoosterIsPrePurchase, this.m_selectedStorePackId));
			this.ShowGiftDescription(this.m_visibleDustCount, this.m_visibleDustBonusCount, this.m_selectedBoosterIsPrePurchase, this.m_selectedStorePackId);
		}
		yield break;
	}

	// Token: 0x0600643A RID: 25658 RVA: 0x0020B39C File Offset: 0x0020959C
	private void HideDust()
	{
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.HideDustJar();
			this.HideGiftDescription();
		}
	}

	// Token: 0x0600643B RID: 25659 RVA: 0x0020B3C8 File Offset: 0x002095C8
	private void ShowGiftDescription(int amount, int bonusAmount, bool prePurchase, StorePackId selectedStorePackId)
	{
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.ShowGiftDescription(amount, bonusAmount, prePurchase, selectedStorePackId);
		}
	}

	// Token: 0x0600643C RID: 25660 RVA: 0x0020B3F0 File Offset: 0x002095F0
	private void HideGiftDescription()
	{
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.HideGiftDescription();
		}
	}

	// Token: 0x0600643D RID: 25661 RVA: 0x0020B414 File Offset: 0x00209614
	private void ShowHiddenBundleCard()
	{
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.ShowHiddenBundleCard();
		}
	}

	// Token: 0x0600643E RID: 25662 RVA: 0x0020B438 File Offset: 0x00209638
	private void HideHiddenBundleCard()
	{
		GeneralStorePacksContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.HideHiddenBundleCard();
		}
	}

	// Token: 0x0600643F RID: 25663 RVA: 0x0020B45B File Offset: 0x0020965B
	private IEnumerator AnimateBundleBox(GeneralStorePacksContentDisplay display, float delayAnim, bool forceImmediate)
	{
		if (this.m_waitingForBoxAnim)
		{
			yield break;
		}
		while (this.m_animatingLogo)
		{
			yield return null;
		}
		Log.Store.Print("AnimateBundleBox: delay = {0}", new object[]
		{
			delayAnim
		});
		this.StartAnimatingPacks();
		if (delayAnim > 0f)
		{
			this.m_waitingForBoxAnim = true;
		}
		int num = display.ShowBundleBox(this.m_boxFlyInAnimTime, this.m_boxFlyOutAnimTime, this.m_boxFlyInDelay, this.m_boxFlyOutDelay, delayAnim, forceImmediate);
		if (!forceImmediate && num != 0)
		{
			this.ShakeStore(1, this.m_boxFlyInXShake, delayAnim + this.m_boxFlyInAnimTime, this.m_boxStoreImpactTranslation, 0);
			yield return new WaitForSeconds(delayAnim + 1f);
		}
		this.DoneAnimatingPacks();
		this.m_waitingForBoxAnim = false;
		yield break;
	}

	// Token: 0x06006440 RID: 25664 RVA: 0x0020B47F File Offset: 0x0020967F
	private IEnumerator AnimateLimitedTimeOfferUI(GeneralStorePacksContentDisplay display, bool waitForLogo)
	{
		if (this.m_limitedTimeOfferText != null)
		{
			this.m_limitedTimeOfferText.gameObject.SetActive(false);
			this.m_limitedTimeOfferText.transform.localScale = this.m_limitedTimeTextOrigScale;
		}
		if (!base.IsContentActive() || this.IsPackIdInvalid(this.m_selectedStorePackId) || !this.m_showLimitedTimeOfferText)
		{
			yield break;
		}
		if (!GameUtils.IsLimitedTimeOffer(this.m_selectedStorePackId))
		{
			yield break;
		}
		while ((this.m_animatingLogo && waitForLogo) || this.m_animatingPacks)
		{
			yield return null;
		}
		if (this.m_limitedTimeOfferText != null)
		{
			Network.Bundle bundle;
			StoreManager.Get().IsBoosterHiddenLicenseBundle(this.m_selectedStorePackId, out bundle);
			if (StoreManager.Get().ShouldShowFeaturedDustJar(bundle))
			{
				this.m_limitedTimeOfferText.transform.position = this.m_limitedTimeOfferDustBone.position;
			}
			else
			{
				this.m_limitedTimeOfferText.transform.position = this.m_limitedTimeOfferBone.position;
			}
			this.m_limitedTimeOfferText.Text = GameStrings.Get("GLUE_STORE_LIMITED_TIME_OFFER");
			this.m_limitedTimeOfferText.gameObject.SetActive(true);
			this.m_limitedTimeOfferText.transform.localScale = this.m_limitedTimeTextOrigScale * 0.01f;
			iTween.ScaleTo(this.m_limitedTimeOfferText.gameObject, iTween.Hash(new object[]
			{
				"scale",
				this.m_limitedTimeTextOrigScale,
				"time",
				0.25f,
				"easetype",
				iTween.EaseType.easeOutQuad
			}));
		}
		yield break;
	}

	// Token: 0x06006441 RID: 25665 RVA: 0x0020B498 File Offset: 0x00209698
	private void ResetAnimations()
	{
		if (this.m_preorderCardBackReward != null)
		{
			this.m_preorderCardBackReward.HideCardBackReward();
		}
		if (this.m_availableDateText != null)
		{
			this.m_availableDateText.gameObject.SetActive(false);
		}
		if (this.m_limitedTimeOfferText != null)
		{
			this.m_limitedTimeOfferText.gameObject.SetActive(false);
		}
		if (this.m_logoAnimCoroutine != null)
		{
			iTween.Stop(this.m_logoMesh1.gameObject);
			iTween.Stop(this.m_logoMesh2.gameObject);
			base.StopCoroutine(this.m_logoAnimCoroutine);
		}
		this.m_logoMesh1.gameObject.SetActive(false);
		this.m_logoMesh2.gameObject.SetActive(false);
		if (this.m_packAnimCoroutine != null)
		{
			base.StopCoroutine(this.m_packAnimCoroutine);
		}
		if (this.m_limitedTimeOfferAnimCoroutine != null)
		{
			base.StopCoroutine(this.m_limitedTimeOfferAnimCoroutine);
		}
		if (this.m_packBuyBonusCallout != null)
		{
			this.m_packBuyBonusCallout.DeactivateCallout();
		}
		if (this.m_bonusPacksCalloutCoroutine != null)
		{
			base.StopCoroutine(this.m_bonusPacksCalloutCoroutine);
		}
		this.m_animatingLogo = false;
		this.m_animatingPacks = false;
		this.m_waitingForBoxAnim = false;
	}

	// Token: 0x06006442 RID: 25666 RVA: 0x0020B5BC File Offset: 0x002097BC
	private void AnimateAndUpdateDisplay(StorePackId storePackId, bool forceImmediate = false)
	{
		if (this.m_preorderCardBackReward != null)
		{
			this.m_preorderCardBackReward.HideCardBackReward();
		}
		GameObject currDisplay = null;
		if (this.m_currentDisplay == -1)
		{
			this.m_currentDisplay = 1;
			currDisplay = this.m_packEmptyDisplay;
		}
		else
		{
			currDisplay = this.GetCurrentDisplayContainer();
		}
		GameObject nextDisplayContainer = this.GetNextDisplayContainer();
		this.GetCurrentLogo().gameObject.SetActive(false);
		this.GetCurrentDisplay().ClearContents();
		this.m_currentDisplay = (this.m_currentDisplay + 1) % 2;
		nextDisplayContainer.SetActive(true);
		if (!forceImmediate)
		{
			currDisplay.transform.localRotation = Quaternion.identity;
			nextDisplayContainer.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			iTween.StopByName(currDisplay, "ROTATION_TWEEN");
			iTween.StopByName(nextDisplayContainer, "ROTATION_TWEEN");
			iTween.RotateBy(currDisplay, iTween.Hash(new object[]
			{
				"amount",
				new Vector3(0.5f, 0f, 0f),
				"time",
				0.5f,
				"name",
				"ROTATION_TWEEN",
				"oncomplete",
				new Action<object>(delegate(object o)
				{
					currDisplay.SetActive(false);
				})
			}));
			iTween.RotateBy(nextDisplayContainer, iTween.Hash(new object[]
			{
				"amount",
				new Vector3(0.5f, 0f, 0f),
				"time",
				0.5f,
				"name",
				"ROTATION_TWEEN"
			}));
			if (!string.IsNullOrEmpty(this.m_backgroundFlipSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_backgroundFlipSound);
			}
		}
		else
		{
			nextDisplayContainer.transform.localRotation = Quaternion.identity;
			currDisplay.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			currDisplay.SetActive(false);
		}
		IStorePackDef packDef = this.GetStorePackDef(storePackId);
		this.GetCurrentDisplay().UpdatePackType(packDef);
		MeshRenderer currLogo = this.GetCurrentLogo();
		if (currLogo != null)
		{
			this.m_hasLogo = !string.IsNullOrEmpty(packDef.GetLogoTextureName());
			if (this.m_hasLogo)
			{
				this.m_loadingLogoTexture = true;
				AssetHandleCallback<Texture> onTextureLoaded = null;
				onTextureLoaded = delegate(AssetReference name, AssetHandle<Texture> loadedTexture, object data)
				{
					this.m_loadingLogoTexture = false;
					if (loadedTexture == null)
					{
						if ((bool)data)
						{
							Error.AddDevFatal("Loading localized logo failed.  This is normal if we're on android and just switched.  Trying unlocalized.", Array.Empty<object>());
							this.m_loadingLogoTexture = true;
							AssetLoader.Get().LoadAsset<Texture>(packDef.GetLogoTextureName(), onTextureLoaded, false, AssetLoadingOptions.DisableLocalization);
							return;
						}
						Debug.LogError(string.Format("Failed to load logo with texture {0}!", this.name));
						return;
					}
					else
					{
						if (!(currLogo != null))
						{
							loadedTexture.Dispose();
							return;
						}
						currLogo.GetMaterial().mainTexture = loadedTexture;
						DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
						if (disposablesCleaner == null)
						{
							return;
						}
						disposablesCleaner.Attach(currLogo, loadedTexture);
						return;
					}
				};
				AssetLoader.Get().LoadAsset<Texture>(packDef.GetLogoTextureName(), onTextureLoaded, true, AssetLoadingOptions.None);
				MeshRenderer glowLogo = this.GetCurrentGlowLogo();
				if (glowLogo != null)
				{
					this.m_loadingLogoGlowTexture = true;
					AssetLoader.Get().LoadAsset<Texture>(packDef.GetLogoTextureGlowName(), delegate(AssetReference name, AssetHandle<Texture> loadedTexture, object data)
					{
						this.m_loadingLogoGlowTexture = false;
						if (loadedTexture == null)
						{
							Debug.LogError(string.Format("Failed to load texture {0}!", this.name));
							return;
						}
						if (!(glowLogo != null))
						{
							loadedTexture.Dispose();
							return;
						}
						glowLogo.GetMaterial().mainTexture = loadedTexture;
						DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
						if (disposablesCleaner == null)
						{
							return;
						}
						disposablesCleaner.Attach(glowLogo, loadedTexture);
					}, null, AssetLoadingOptions.None);
				}
			}
		}
		this.AnimateBuyBar();
	}

	// Token: 0x06006443 RID: 25667 RVA: 0x0020B8F4 File Offset: 0x00209AF4
	private void AnimateBuyBar()
	{
		Network.Bundle bundle;
		GameObject gameObject = StoreManager.Get().IsBoosterPreorderActive(GameUtils.GetProductDataFromStorePackId(this.m_selectedStorePackId, this.m_lastBundleIndex), StorePackId.GetProductTypeFromStorePackType(this.m_selectedStorePackId), out bundle) ? this.m_packBuyContainer : this.m_packBuyPreorderContainer;
		if (this.IsPackIdInvalid(this.m_selectedStorePackId))
		{
			return;
		}
		iTween.Stop(gameObject);
		gameObject.transform.localRotation = Quaternion.identity;
		iTween.RotateBy(gameObject, iTween.Hash(new object[]
		{
			"amount",
			new Vector3(-1f, 0f, 0f),
			"time",
			this.m_backgroundFlipAnimTime,
			"delay",
			0.001f
		}));
	}

	// Token: 0x06006444 RID: 25668 RVA: 0x0020B9C0 File Offset: 0x00209BC0
	private void UpdateKoreaInfoButton()
	{
		if (this.m_ChinaInfoButton == null)
		{
			return;
		}
		bool active = StoreManager.Get().IsKoreanCustomer() && base.IsContentActive() && !this.IsPackIdInvalid(this.m_selectedStorePackId);
		this.m_ChinaInfoButton.gameObject.SetActive(active);
	}

	// Token: 0x06006445 RID: 25669 RVA: 0x0020BA14 File Offset: 0x00209C14
	private void OnKoreaInfoPressed(UIEvent e)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_KOREAN_DISCLAIMER_HEADLINE");
		popupInfo.m_text = GameStrings.Get("GLUE_STORE_KOREAN_DISCLAIMER_DETAILS");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06006446 RID: 25670 RVA: 0x0020BA60 File Offset: 0x00209C60
	private bool IsPackIdFirstPurchaseBundle(StorePackId storePackId)
	{
		return storePackId.Type == StorePackType.BOOSTER && storePackId.Id == 181;
	}

	// Token: 0x06006447 RID: 25671 RVA: 0x0020BA7A File Offset: 0x00209C7A
	private bool IsPackIdInvalid(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			return storePackId.Id == 0;
		}
		return storePackId.Type != StorePackType.MODULAR_BUNDLE || storePackId.Id == 0;
	}

	// Token: 0x06006448 RID: 25672 RVA: 0x0020BAA4 File Offset: 0x00209CA4
	private void ClearButtonEventListeners()
	{
		foreach (GeneralStorePackBuyButton generalStorePackBuyButton in this.m_packBuyButtons)
		{
			generalStorePackBuyButton.ClearEventListeners();
		}
		foreach (GeneralStorePackBuyButton generalStorePackBuyButton2 in this.m_packPreorderBuyButtons)
		{
			generalStorePackBuyButton2.ClearEventListeners();
		}
	}

	// Token: 0x040052FF RID: 21247
	public StoreQuantityPrompt m_quantityPrompt;

	// Token: 0x04005300 RID: 21248
	public GameObject m_packContainer;

	// Token: 0x04005301 RID: 21249
	public GameObject m_packEmptyDisplay;

	// Token: 0x04005302 RID: 21250
	public GeneralStorePacksContentDisplay m_packDisplay;

	// Token: 0x04005303 RID: 21251
	[CustomEditField(Sections = "Pack Buy Buttons")]
	public GameObject m_packBuyContainer;

	// Token: 0x04005304 RID: 21252
	[CustomEditField(Sections = "Pack Buy Buttons")]
	public MultiSliceElement m_packBuyButtonContainer;

	// Token: 0x04005305 RID: 21253
	[CustomEditField(Sections = "Pack Buy Buttons")]
	public GeneralStorePackBuyButton m_packBuyButtonPrefab;

	// Token: 0x04005306 RID: 21254
	[CustomEditField(Sections = "Pack Buy Buttons")]
	public MultiSliceElement m_packBuyFrameContainer;

	// Token: 0x04005307 RID: 21255
	[CustomEditField(Sections = "Pack Buy Buttons", ListTable = true)]
	public List<GeneralStorePacksContent.ToggleableButtonFrame> m_toggleableButtonFrames = new List<GeneralStorePacksContent.ToggleableButtonFrame>();

	// Token: 0x04005308 RID: 21256
	[CustomEditField(Sections = "Pack Buy Buttons", ListTable = true)]
	public List<GeneralStorePacksContent.MultiSliceEndCaps> m_buyBarEndCaps = new List<GeneralStorePacksContent.MultiSliceEndCaps>();

	// Token: 0x04005309 RID: 21257
	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public GeneralStorePackBuyCallout m_packBuyBonusCallout;

	// Token: 0x0400530A RID: 21258
	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public bool m_packBuyBonusCalloutOnlyOncePerSession;

	// Token: 0x0400530B RID: 21259
	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public int m_packBuyBonusCalloutDebugForceDisplay;

	// Token: 0x0400530C RID: 21260
	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public UberText m_packBuyBonusText;

	// Token: 0x0400530D RID: 21261
	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public UberText m_limitedTimeOfferText;

	// Token: 0x0400530E RID: 21262
	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public bool m_showLimitedTimeOfferText;

	// Token: 0x0400530F RID: 21263
	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public Transform m_limitedTimeOfferBone;

	// Token: 0x04005310 RID: 21264
	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public Transform m_limitedTimeOfferDustBone;

	// Token: 0x04005311 RID: 21265
	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public GameObject m_packBuyPreorderContainer;

	// Token: 0x04005312 RID: 21266
	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public GeneralStorePackBuyButton m_packBuyPreorderButtonPrefab;

	// Token: 0x04005313 RID: 21267
	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public MultiSliceElement m_packBuyPreorderButtonContainer;

	// Token: 0x04005314 RID: 21268
	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public MultiSliceElement m_packBuyPreorderFrameContainer;

	// Token: 0x04005315 RID: 21269
	[CustomEditField(Sections = "Pack Buy Buttons/Preorder", ListTable = true)]
	public List<GeneralStorePacksContent.ToggleableButtonFrame> m_toggleablePreorderButtonFrames = new List<GeneralStorePacksContent.ToggleableButtonFrame>();

	// Token: 0x04005316 RID: 21270
	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public UberText m_availableDateText;

	// Token: 0x04005317 RID: 21271
	[CustomEditField(Sections = "China Button")]
	public UIBButton m_ChinaInfoButton;

	// Token: 0x04005318 RID: 21272
	[CustomEditField(Sections = "Packs")]
	public int m_maxPackBuyButtons = 10;

	// Token: 0x04005319 RID: 21273
	[CustomEditField(Sections = "Packs")]
	public GeneralStorePacksContent.LogoAnimation m_logoAnimation;

	// Token: 0x0400531A RID: 21274
	[CustomEditField(Sections = "Animation")]
	public float m_packFlyOutAnimTime = 0.1f;

	// Token: 0x0400531B RID: 21275
	[CustomEditField(Sections = "Animation")]
	public float m_packFlyOutDelay = 0.005f;

	// Token: 0x0400531C RID: 21276
	[CustomEditField(Sections = "Animation")]
	public float m_packFlyInAnimTime = 0.2f;

	// Token: 0x0400531D RID: 21277
	[CustomEditField(Sections = "Animation")]
	public float m_packFlyInDelay = 0.01f;

	// Token: 0x0400531E RID: 21278
	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyOutAnimTime = 0.2f;

	// Token: 0x0400531F RID: 21279
	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyOutDelay = 0.005f;

	// Token: 0x04005320 RID: 21280
	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyInAnimTime = 0.5f;

	// Token: 0x04005321 RID: 21281
	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyInDelay = 0.1f;

	// Token: 0x04005322 RID: 21282
	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyInXShake = 35f;

	// Token: 0x04005323 RID: 21283
	[CustomEditField(Sections = "Animation")]
	public float m_boxStoreImpactTranslation = -70f;

	// Token: 0x04005324 RID: 21284
	[CustomEditField(Sections = "Animation")]
	public float m_shakeObjectDelayMultiplier = 0.7f;

	// Token: 0x04005325 RID: 21285
	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipAnimTime = 0.5f;

	// Token: 0x04005326 RID: 21286
	[CustomEditField(Sections = "Animation")]
	public float m_maxPackFlyInXShake = 20f;

	// Token: 0x04005327 RID: 21287
	[CustomEditField(Sections = "Animation")]
	public float m_maxPackFlyOutXShake = 12f;

	// Token: 0x04005328 RID: 21288
	[CustomEditField(Sections = "Animation")]
	public float m_packFlyShakeTime = 2f;

	// Token: 0x04005329 RID: 21289
	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipShake = 20f;

	// Token: 0x0400532A RID: 21290
	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipShakeDelay;

	// Token: 0x0400532B RID: 21291
	[CustomEditField(Sections = "Animation")]
	public float m_PackYDegreeVariationMag = 2f;

	// Token: 0x0400532C RID: 21292
	[CustomEditField(Sections = "Animation")]
	public float m_BoxYDegreeVariationMag = 1f;

	// Token: 0x0400532D RID: 21293
	[CustomEditField(Sections = "Animation/Appear")]
	public GameObject m_logoAnimationStartBone;

	// Token: 0x0400532E RID: 21294
	[CustomEditField(Sections = "Animation/Appear")]
	public GameObject m_logoAnimationEndBone;

	// Token: 0x0400532F RID: 21295
	[CustomEditField(Sections = "Animation/Appear")]
	public MeshRenderer m_logoMesh;

	// Token: 0x04005330 RID: 21296
	[CustomEditField(Sections = "Animation/Appear")]
	public MeshRenderer m_logoGlowMesh;

	// Token: 0x04005331 RID: 21297
	[CustomEditField(Sections = "Animation/Appear")]
	public Vector3 m_punchAmount;

	// Token: 0x04005332 RID: 21298
	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoHoldTime = 1f;

	// Token: 0x04005333 RID: 21299
	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoDisplayPunchTime = 0.5f;

	// Token: 0x04005334 RID: 21300
	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoIntroTime = 0.25f;

	// Token: 0x04005335 RID: 21301
	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoOutroTime = 0.25f;

	// Token: 0x04005336 RID: 21302
	[CustomEditField(Sections = "Animation/Appear")]
	public Vector3 m_logoAppearOffset;

	// Token: 0x04005337 RID: 21303
	[CustomEditField(Sections = "Animation/Preorder")]
	public GeneralStoreRewardsCardBack m_preorderCardBackReward;

	// Token: 0x04005338 RID: 21304
	[CustomEditField(Sections = "Sounds & Music", T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	// Token: 0x04005339 RID: 21305
	public const bool REQUIRE_REAL_MONEY_BUNDLE_OPTION = true;

	// Token: 0x0400533A RID: 21306
	private static readonly int MAX_QUANTITY_BOUGHT_WITH_GOLD = 50;

	// Token: 0x0400533B RID: 21307
	private const float FIRST_PURCHASE_BUNDLE_INIT_DELAY = 0.5f;

	// Token: 0x0400533C RID: 21308
	private StorePackId m_selectedStorePackId;

	// Token: 0x0400533D RID: 21309
	private List<GeneralStorePackBuyButton> m_packBuyButtons = new List<GeneralStorePackBuyButton>();

	// Token: 0x0400533E RID: 21310
	private List<GeneralStorePackBuyButton> m_packPreorderBuyButtons = new List<GeneralStorePackBuyButton>();

	// Token: 0x0400533F RID: 21311
	private int m_currentGoldPackQuantity = 1;

	// Token: 0x04005340 RID: 21312
	private int m_visiblePackCount;

	// Token: 0x04005341 RID: 21313
	private int m_visibleDustCount;

	// Token: 0x04005342 RID: 21314
	private int m_visibleDustBonusCount;

	// Token: 0x04005343 RID: 21315
	private bool m_selectedBoosterIsPrePurchase;

	// Token: 0x04005344 RID: 21316
	private int m_lastBundleIndex;

	// Token: 0x04005345 RID: 21317
	private int m_currentDisplay = -1;

	// Token: 0x04005346 RID: 21318
	private Map<StorePackId, IStorePackDef> m_storePackDefs = new Map<StorePackId, IStorePackDef>();

	// Token: 0x04005347 RID: 21319
	private HashSet<StorePackId> m_packBuyBonusCalloutSeenForPackId = new HashSet<StorePackId>();

	// Token: 0x04005348 RID: 21320
	private const string PREV_PLAYLIST_NAME = "StorePrevCurrentPlaylist";

	// Token: 0x04005349 RID: 21321
	private GeneralStorePacksContentDisplay m_packDisplay1;

	// Token: 0x0400534A RID: 21322
	private GeneralStorePacksContentDisplay m_packDisplay2;

	// Token: 0x0400534B RID: 21323
	private MeshRenderer m_logoMesh1;

	// Token: 0x0400534C RID: 21324
	private MeshRenderer m_logoMesh2;

	// Token: 0x0400534D RID: 21325
	private MeshRenderer m_logoGlowMesh1;

	// Token: 0x0400534E RID: 21326
	private MeshRenderer m_logoGlowMesh2;

	// Token: 0x0400534F RID: 21327
	private Coroutine m_logoAnimCoroutine;

	// Token: 0x04005350 RID: 21328
	private Coroutine m_packAnimCoroutine;

	// Token: 0x04005351 RID: 21329
	private Coroutine m_limitedTimeOfferAnimCoroutine;

	// Token: 0x04005352 RID: 21330
	private Coroutine m_bonusPacksCalloutCoroutine;

	// Token: 0x04005353 RID: 21331
	private Vector3 m_savedLocalPosition;

	// Token: 0x04005354 RID: 21332
	private Vector3 m_limitedTimeTextOrigScale;

	// Token: 0x04005355 RID: 21333
	private bool m_animatingLogo;

	// Token: 0x04005356 RID: 21334
	private bool m_animatingPacks;

	// Token: 0x04005357 RID: 21335
	private bool m_hasLogo;

	// Token: 0x04005358 RID: 21336
	private bool m_waitingForBoxAnim;

	// Token: 0x04005359 RID: 21337
	private bool m_loadingLogoTexture;

	// Token: 0x0400535A RID: 21338
	private bool m_loadingLogoGlowTexture;

	// Token: 0x02002278 RID: 8824
	[Serializable]
	public class ToggleableButtonFrame
	{
		// Token: 0x0400E396 RID: 58262
		public GameObject m_Middle;

		// Token: 0x0400E397 RID: 58263
		public GameObject m_IBar;
	}

	// Token: 0x02002279 RID: 8825
	[Serializable]
	public class MultiSliceEndCaps
	{
		// Token: 0x0400E398 RID: 58264
		public GameObject m_FullBar;

		// Token: 0x0400E399 RID: 58265
		public GameObject m_SmallerBar;
	}

	// Token: 0x0200227A RID: 8826
	public enum LogoAnimation
	{
		// Token: 0x0400E39B RID: 58267
		None,
		// Token: 0x0400E39C RID: 58268
		Slam,
		// Token: 0x0400E39D RID: 58269
		Fade
	}

	// Token: 0x0200227B RID: 8827
	public enum ModularBundleLayoutButtonSize
	{
		// Token: 0x0400E39F RID: 58271
		None,
		// Token: 0x0400E3A0 RID: 58272
		Small,
		// Token: 0x0400E3A1 RID: 58273
		Large
	}
}
