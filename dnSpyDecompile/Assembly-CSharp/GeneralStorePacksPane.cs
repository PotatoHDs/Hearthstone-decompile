using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000702 RID: 1794
[CustomEditClass]
public class GeneralStorePacksPane : GeneralStorePane
{
	// Token: 0x0600647D RID: 25725 RVA: 0x0020D6D4 File Offset: 0x0020B8D4
	private void Awake()
	{
		this.m_packsContent = (this.m_parentContent as GeneralStorePacksContent);
		this.m_purchaseAnimationBlocker.SetActive(false);
		if (this.m_packsContent == null)
		{
			Debug.LogError("m_packsContent is not the correct type: GeneralStorePacksContent");
			return;
		}
		NetCache.Get().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnItemPurchased));
	}

	// Token: 0x0600647E RID: 25726 RVA: 0x0020D744 File Offset: 0x0020B944
	private void OnDestroy()
	{
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.RemoveNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		}
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnItemPurchased));
	}

	// Token: 0x0600647F RID: 25727 RVA: 0x0020D782 File Offset: 0x0020B982
	public override void StoreShown(bool isCurrent)
	{
		if (!this.m_paneInitialized)
		{
			this.m_paneInitialized = true;
			this.SetupPackButtons();
			this.SetupInitialSelectedPack();
		}
		this.UpdatePackButtonPositions();
		this.UpdatePackButtonRecommendedIndicators();
		AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ADVENTURE);
	}

	// Token: 0x06006480 RID: 25728 RVA: 0x0020D7B6 File Offset: 0x0020B9B6
	public override void PrePaneSwappedIn()
	{
		if (UniversalInputManager.UsePhoneUI && this.m_inRemovingBundleFlow)
		{
			this.OnPackSelectorButtonClicked(this.m_packButtons[0], this.m_packButtons[0].GetStorePackId());
			this.m_inRemovingBundleFlow = false;
		}
	}

	// Token: 0x06006481 RID: 25729 RVA: 0x0020D7F6 File Offset: 0x0020B9F6
	public void RemoveFirstPurchaseBundle(float glowOutLength)
	{
		if (!StoreManager.IsFirstPurchaseBundleOwned())
		{
			return;
		}
		base.StartCoroutine(this.AnimateRemoveFirstPurchaseBundle(glowOutLength));
	}

	// Token: 0x06006482 RID: 25730 RVA: 0x0020D810 File Offset: 0x0020BA10
	private void OnItemPurchased(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		foreach (Network.BundleItem bundleItem in bundle.Items)
		{
			StorePackId storePackId = this.m_packsContent.GetStorePackId();
			if (bundleItem != null && bundleItem.ItemType == ProductType.PRODUCT_TYPE_RANDOM_CARD && storePackId.Type == StorePackType.BOOSTER && storePackId.Id == 181)
			{
				this.OnRandomCardPurchased(this.m_randomCardReward);
				break;
			}
			if (bundleItem != null && bundleItem.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE && storePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				this.m_packsContent.AnimateModularBundleAfterPurchase(storePackId);
				break;
			}
		}
	}

	// Token: 0x06006483 RID: 25731 RVA: 0x0020D8CC File Offset: 0x0020BACC
	private void OnRandomCardPurchased(CardReward cardReward)
	{
		if (this.m_packsContent == null)
		{
			CardRewardData cardRewardData = cardReward.Data as CardRewardData;
			Debug.LogWarningFormat("OnRandomCardPurchased() m_packsContent == null for cardID {0}", new object[]
			{
				cardRewardData.CardID
			});
			return;
		}
		this.m_packsContent.FirstPurchaseBundlePurchased(cardReward);
	}

	// Token: 0x06006484 RID: 25732 RVA: 0x0020D91C File Offset: 0x0020BB1C
	private void OnPackSelectorButtonClicked(GeneralStorePackSelectorButton btn, StorePackId storePackId)
	{
		if (!this.m_parentContent.IsContentActive())
		{
			return;
		}
		this.m_packsContent.SetBoosterId(storePackId, false, false);
		foreach (GeneralStorePackSelectorButton generalStorePackSelectorButton in this.m_packButtons)
		{
			generalStorePackSelectorButton.Unselect();
		}
		btn.Select();
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_BOOSTER_ID, btn.GetStorePackId().Id);
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_PACK_TYPE, (int)btn.GetStorePackId().Type);
		if (!string.IsNullOrEmpty(this.m_boosterSelectionSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_boosterSelectionSound);
		}
	}

	// Token: 0x06006485 RID: 25733 RVA: 0x0020D9E4 File Offset: 0x0020BBE4
	private void SetupPackButtons()
	{
		Map<StorePackId, IStorePackDef> storePackDefs = this.m_packsContent.GetStorePackDefs();
		StorePackId storePackId2 = this.m_packsContent.GetStorePackId();
		bool flag = !HearthstoneApplication.IsPublic() && Vars.Key("ModularBundle.ShowAll").GetBool(false);
		foreach (KeyValuePair<StorePackId, IStorePackDef> keyValuePair in storePackDefs)
		{
			StorePackId storePackId = keyValuePair.Key;
			ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
			if (productTypeFromStorePackType != ProductType.PRODUCT_TYPE_BOOSTER || this.CanShowBooster(storePackId.Id))
			{
				if (GameUtils.IsHiddenLicenseBundleBooster(storePackId))
				{
					bool flag2 = false;
					bool flag3 = false;
					int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
					for (int i = 0; i < productDataCountFromStorePackId; i++)
					{
						int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId, i);
						Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(productTypeFromStorePackType, true, productDataFromStorePackId, 0, true).FirstOrDefault<Network.Bundle>();
						if (bundle != null)
						{
							flag2 = true;
							if (!StoreManager.Get().IsProductAlreadyOwned(bundle))
							{
								flag3 = true;
							}
						}
					}
					bool flag4;
					if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
					{
						ModularBundleDbfRecord record = GameDbf.ModularBundle.GetRecord(storePackId.Id);
						flag4 = (flag || (flag2 && (flag3 || record.ShowAfterPurchase)));
					}
					else
					{
						flag4 = flag3;
					}
					if (!flag4)
					{
						continue;
					}
				}
				IStorePackDef value = keyValuePair.Value;
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(value.GetSelectorButtonPrefab(), AssetLoadingOptions.IgnorePrefabPosition);
				GameUtils.SetParent(gameObject, this.m_paneContainer, true);
				SceneUtils.SetLayer(gameObject, this.m_paneContainer.layer, null);
				GeneralStorePackSelectorButton newPackButton = gameObject.GetComponent<GeneralStorePackSelectorButton>();
				newPackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
				{
					this.OnPackSelectorButtonClicked(newPackButton, storePackId);
				});
				newPackButton.SetStorePackId(storePackId);
				if (storePackId == storePackId2)
				{
					newPackButton.Select();
					StoreManager.Get().SetCurrentlySelectedStorePack(storePackId2);
				}
				this.m_packButtons.Add(newPackButton);
			}
		}
		this.UpdatePackButtonPositions();
	}

	// Token: 0x06006486 RID: 25734 RVA: 0x0020DC48 File Offset: 0x0020BE48
	private bool CanShowBooster(int boosterDbId)
	{
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterDbId);
		return record != null && SpecialEventManager.Get().IsEventActive(record.BuyWithGoldEvent, false) && !GameUtils.IsBoosterWild(record);
	}

	// Token: 0x06006487 RID: 25735 RVA: 0x0020DC86 File Offset: 0x0020BE86
	private void SortPackButtons()
	{
		this.m_packButtons.Sort(delegate(GeneralStorePackSelectorButton lhs, GeneralStorePackSelectorButton rhs)
		{
			bool flag = GameUtils.IsFirstPurchaseBundleBooster(lhs.GetStorePackId());
			bool flag2 = GameUtils.IsFirstPurchaseBundleBooster(rhs.GetStorePackId());
			if (flag != flag2)
			{
				if (!flag)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				bool flag3 = lhs.IsRecommendedForNewPlayer();
				bool flag4 = rhs.IsRecommendedForNewPlayer();
				bool flag5 = GameUtils.IsHiddenLicenseBundleBooster(lhs.GetStorePackId());
				bool flag6 = GameUtils.IsHiddenLicenseBundleBooster(rhs.GetStorePackId());
				if (flag5 != flag6)
				{
					if (!flag5)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					bool flag7 = lhs.GetStorePackId().Type == StorePackType.MODULAR_BUNDLE;
					bool flag8 = rhs.GetStorePackId().Type == StorePackType.MODULAR_BUNDLE;
					if (flag7 != flag8)
					{
						if (!flag7)
						{
							return 1;
						}
						return -1;
					}
					else if (flag7 && flag8)
					{
						ModularBundleDbfRecord modularBundleDbfRecord = (ModularBundleDbfRecord)lhs.GetRecord();
						ModularBundleDbfRecord modularBundleDbfRecord2 = (ModularBundleDbfRecord)rhs.GetRecord();
						int num = (modularBundleDbfRecord == null) ? 0 : modularBundleDbfRecord.SortOrder;
						int num2 = (modularBundleDbfRecord2 == null) ? 0 : modularBundleDbfRecord2.SortOrder;
						if (num != num2)
						{
							return Mathf.Clamp(num2 - num, -1, 1);
						}
						int num3 = (modularBundleDbfRecord == null) ? 0 : modularBundleDbfRecord.ID;
						int num4 = (modularBundleDbfRecord2 == null) ? 0 : modularBundleDbfRecord2.ID;
						if (num3 >= num4)
						{
							return 1;
						}
						return -1;
					}
					else if (flag3 != flag4)
					{
						if (!flag3)
						{
							return 1;
						}
						return -1;
					}
					else
					{
						bool flag9 = lhs.IsPreorder();
						bool flag10 = rhs.IsPreorder();
						if (flag9 != flag10)
						{
							if (!flag9)
							{
								return 1;
							}
							return -1;
						}
						else
						{
							bool flag11 = lhs.IsLatestExpansion();
							bool flag12 = rhs.IsLatestExpansion();
							if (flag11 != flag12)
							{
								if (!flag11)
								{
									return 1;
								}
								return -1;
							}
							else
							{
								BoosterDbfRecord boosterDbfRecord = (BoosterDbfRecord)lhs.GetRecord();
								BoosterDbfRecord boosterDbfRecord2 = (BoosterDbfRecord)rhs.GetRecord();
								bool flag13 = boosterDbfRecord != null && boosterDbfRecord.ID == 1;
								bool flag14 = boosterDbfRecord2 != null && boosterDbfRecord2.ID == 1;
								if (flag13 != flag14)
								{
									if (this.m_deprioritizeClassic)
									{
										if (!flag13)
										{
											return -1;
										}
										return 1;
									}
									else
									{
										if (!flag13)
										{
											return 1;
										}
										return -1;
									}
								}
								else
								{
									int num5 = (boosterDbfRecord == null) ? 0 : boosterDbfRecord.ListDisplayOrderCategory;
									int num6 = (boosterDbfRecord2 == null) ? 0 : boosterDbfRecord2.ListDisplayOrderCategory;
									if (num5 != num6)
									{
										return Mathf.Clamp(num6 - num5, -1, 1);
									}
									int num7 = (boosterDbfRecord == null) ? 0 : boosterDbfRecord.ListDisplayOrder;
									return Mathf.Clamp(((boosterDbfRecord2 == null) ? 0 : boosterDbfRecord2.ListDisplayOrder) - num7, -1, 1);
								}
							}
						}
					}
				}
			}
		});
	}

	// Token: 0x06006488 RID: 25736 RVA: 0x0020DCA0 File Offset: 0x0020BEA0
	private void UpdatePackButtonPositions()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null)
		{
			NetCache.NetCacheBoosters netObject2 = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
			if (netObject2 != null)
			{
				int num = 0;
				NetCache.BoosterStack boosterStack = netObject2.GetBoosterStack(1);
				if (boosterStack != null)
				{
					num = boosterStack.EverGrantedCount;
				}
				this.m_deprioritizeClassic = (netObject.Store.NumClassicPacksUntilDeprioritize >= 0 && num >= netObject.Store.NumClassicPacksUntilDeprioritize);
			}
		}
		this.SortPackButtons();
		Vector3 vector = Vector3.zero;
		Vector3 onNormal = Vector3.Normalize(this.m_packButtonSpacing);
		for (int i = 0; i < this.m_packButtons.Count; i++)
		{
			GeneralStorePackSelectorButton generalStorePackSelectorButton = this.m_packButtons[i];
			bool flag = generalStorePackSelectorButton.HasPurchasableProducts();
			generalStorePackSelectorButton.gameObject.SetActive(flag);
			if (flag)
			{
				generalStorePackSelectorButton.transform.localPosition = vector;
				Vector3 b = this.m_packButtonSpacing;
				if (generalStorePackSelectorButton.m_useScrollableItemBoundsToStack)
				{
					UIBScrollableItem component = generalStorePackSelectorButton.GetComponent<UIBScrollableItem>();
					if (component != null)
					{
						b = Vector3.Project(Matrix4x4.TRS(Vector3.zero, component.transform.localRotation, component.transform.localScale) * component.m_size, onNormal);
					}
				}
				vector += b;
			}
		}
	}

	// Token: 0x06006489 RID: 25737 RVA: 0x0020DDE8 File Offset: 0x0020BFE8
	private void UpdatePackButtonRecommendedIndicators()
	{
		int num = 0;
		GeneralStorePackSelectorButton[] array = this.m_packButtons.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			bool hideRibbon = num >= this.m_maxRibbons;
			if (array[i].UpdateRibbonIndicator(hideRibbon))
			{
				num++;
			}
		}
	}

	// Token: 0x0600648A RID: 25738 RVA: 0x0020DE30 File Offset: 0x0020C030
	private bool ShouldResetPackSelection()
	{
		List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(ProductType.PRODUCT_TYPE_BOOSTER, true, 0, 0, true);
		List<string> list = new List<string>(Options.Get().GetString(Option.SEEN_PACK_PRODUCT_LIST, string.Empty).Split(new char[]
		{
			':'
		}));
		bool result = false;
		foreach (Network.Bundle bundle in allBundlesForProduct)
		{
			if (!list.Contains(bundle.PMTProductID.ToString()))
			{
				list.Add(bundle.PMTProductID.ToString());
				result = true;
			}
		}
		Options.Get().SetString(Option.SEEN_PACK_PRODUCT_LIST, string.Join(":", list.ToArray()));
		return result;
	}

	// Token: 0x0600648B RID: 25739 RVA: 0x0020DF0C File Offset: 0x0020C10C
	private void SetupInitialSelectedPack()
	{
		StorePackId storePackId = default(StorePackId);
		if (this.ShouldResetPackSelection())
		{
			Options.Get().SetInt(Option.LAST_SELECTED_STORE_BOOSTER_ID, 0);
			Options.Get().SetInt(Option.LAST_SELECTED_STORE_PACK_TYPE, 0);
		}
		else
		{
			storePackId.Id = Options.Get().GetInt(Option.LAST_SELECTED_STORE_BOOSTER_ID, 0);
			storePackId.Type = (StorePackType)Options.Get().GetInt(Option.LAST_SELECTED_STORE_PACK_TYPE, 0);
			StoreManager.Get().SetCurrentlySelectedStorePack(storePackId);
		}
		foreach (GeneralStorePackSelectorButton generalStorePackSelectorButton in this.m_packButtons)
		{
			if (generalStorePackSelectorButton.GetStorePackId() == storePackId)
			{
				this.m_packsContent.SetBoosterId(storePackId, true, true);
				generalStorePackSelectorButton.Select();
				break;
			}
		}
	}

	// Token: 0x0600648C RID: 25740 RVA: 0x0020DFE8 File Offset: 0x0020C1E8
	private IEnumerator AnimateRemoveFirstPurchaseBundle(float glowOutLength)
	{
		this.m_purchaseAnimationBlocker.SetActive(true);
		this.m_inRemovingBundleFlow = true;
		yield return new WaitForSeconds(glowOutLength + 1f);
		GeneralStorePackSelectorButton buttonToRemove = null;
		foreach (GeneralStorePackSelectorButton generalStorePackSelectorButton in this.m_packButtons)
		{
			StorePackId storePackId = generalStorePackSelectorButton.GetStorePackId();
			if (storePackId.Type == StorePackType.BOOSTER && storePackId.Id == 181)
			{
				buttonToRemove = generalStorePackSelectorButton;
				break;
			}
		}
		if (buttonToRemove != null)
		{
			GeneralStore.Get().HidePacksPane(true);
			if (!UniversalInputManager.UsePhoneUI)
			{
				yield return new WaitForSeconds(0.25f);
			}
			this.m_packButtons.Remove(buttonToRemove);
			UnityEngine.Object.Destroy(buttonToRemove.gameObject);
			this.UpdatePackButtonPositions();
			this.UpdatePackButtonRecommendedIndicators();
			if (!UniversalInputManager.UsePhoneUI)
			{
				this.OnPackSelectorButtonClicked(this.m_packButtons[0], this.m_packButtons[0].GetStorePackId());
				this.m_inRemovingBundleFlow = false;
				yield return new WaitForSeconds(0.75f);
			}
			GeneralStore.Get().HidePacksPane(false);
		}
		this.m_purchaseAnimationBlocker.SetActive(false);
		yield break;
	}

	// Token: 0x0600648D RID: 25741 RVA: 0x0020E000 File Offset: 0x0020C200
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		if (newNotices.FirstOrDefault(new Func<NetCache.ProfileNotice, bool>(this.WillStoreDisplayNotice)) == null)
		{
			return;
		}
		RewardUtils.GetRewards(newNotices)[0].LoadRewardObject(new Reward.DelOnRewardLoaded(this.RewardObjectLoaded));
	}

	// Token: 0x0600648E RID: 25742 RVA: 0x0020E041 File Offset: 0x0020C241
	public bool WillStoreDisplayNotice(NetCache.ProfileNotice notice)
	{
		return this.WillStoreDisplayNotice(notice.Origin, notice.Type, notice.OriginData);
	}

	// Token: 0x0600648F RID: 25743 RVA: 0x0020E05C File Offset: 0x0020C25C
	public bool WillStoreDisplayNotice(NetCache.ProfileNotice.NoticeOrigin noticeOrigin, NetCache.ProfileNotice.NoticeType noticeType, long noticeOriginData)
	{
		if (noticeOrigin == NetCache.ProfileNotice.NoticeOrigin.FROM_PURCHASE && noticeType == NetCache.ProfileNotice.NoticeType.REWARD_CARD && (this.m_packsContent != null && this.m_packsContent.GetStorePackId().Type == StorePackType.BOOSTER && this.m_packsContent.GetStorePackId().Id == 181))
		{
			if (StoreManager.Get().IsSimpleCheckoutFeatureEnabled())
			{
				BattlePayProvider? battlePayProvider = StoreManager.Get().ActiveTransactionProvider();
				BattlePayProvider battlePayProvider2 = BattlePayProvider.BP_PROVIDER_BLIZZARD;
				if (battlePayProvider.GetValueOrDefault() == battlePayProvider2 & battlePayProvider != null)
				{
					if (StoreManager.Get().IsPMTProductIDActiveTransaction(noticeOriginData))
					{
						return true;
					}
					return false;
				}
			}
			if (StoreManager.Get().IsIdActiveTransaction(noticeOriginData))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006490 RID: 25744 RVA: 0x0020E101 File Offset: 0x0020C301
	private void RewardObjectLoaded(Reward reward, object callbackData)
	{
		reward.Hide(false);
		this.m_randomCardReward = (reward as CardReward);
	}

	// Token: 0x04005399 RID: 21401
	[CustomEditField(Sections = "Layout")]
	[SerializeField]
	public Vector3 m_packButtonSpacing;

	// Token: 0x0400539A RID: 21402
	[CustomEditField(Sections = "Content")]
	[SerializeField]
	public int m_maxRibbons;

	// Token: 0x0400539B RID: 21403
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	[SerializeField]
	public string m_boosterSelectionSound;

	// Token: 0x0400539C RID: 21404
	[CustomEditField(Sections = "Purchase Flow")]
	[SerializeField]
	public GameObject m_purchaseAnimationBlocker;

	// Token: 0x0400539D RID: 21405
	private List<GeneralStorePackSelectorButton> m_packButtons = new List<GeneralStorePackSelectorButton>();

	// Token: 0x0400539E RID: 21406
	private GeneralStorePacksContent m_packsContent;

	// Token: 0x0400539F RID: 21407
	private bool m_paneInitialized;

	// Token: 0x040053A0 RID: 21408
	private bool m_inRemovingBundleFlow;

	// Token: 0x040053A1 RID: 21409
	private CardReward m_randomCardReward;

	// Token: 0x040053A2 RID: 21410
	private bool m_deprioritizeClassic;
}
