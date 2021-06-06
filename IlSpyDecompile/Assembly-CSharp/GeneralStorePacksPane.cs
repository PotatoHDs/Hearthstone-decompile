using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class GeneralStorePacksPane : GeneralStorePane
{
	[CustomEditField(Sections = "Layout")]
	[SerializeField]
	public Vector3 m_packButtonSpacing;

	[CustomEditField(Sections = "Content")]
	[SerializeField]
	public int m_maxRibbons;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	[SerializeField]
	public string m_boosterSelectionSound;

	[CustomEditField(Sections = "Purchase Flow")]
	[SerializeField]
	public GameObject m_purchaseAnimationBlocker;

	private List<GeneralStorePackSelectorButton> m_packButtons = new List<GeneralStorePackSelectorButton>();

	private GeneralStorePacksContent m_packsContent;

	private bool m_paneInitialized;

	private bool m_inRemovingBundleFlow;

	private CardReward m_randomCardReward;

	private bool m_deprioritizeClassic;

	private void Awake()
	{
		m_packsContent = m_parentContent as GeneralStorePacksContent;
		m_purchaseAnimationBlocker.SetActive(value: false);
		if (m_packsContent == null)
		{
			Debug.LogError("m_packsContent is not the correct type: GeneralStorePacksContent");
			return;
		}
		NetCache.Get().RegisterNewNoticesListener(OnNewNotices);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnItemPurchased);
	}

	private void OnDestroy()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			service.RemoveNewNoticesListener(OnNewNotices);
		}
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnItemPurchased);
	}

	public override void StoreShown(bool isCurrent)
	{
		if (!m_paneInitialized)
		{
			m_paneInitialized = true;
			SetupPackButtons();
			SetupInitialSelectedPack();
		}
		UpdatePackButtonPositions();
		UpdatePackButtonRecommendedIndicators();
		AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ADVENTURE);
	}

	public override void PrePaneSwappedIn()
	{
		if ((bool)UniversalInputManager.UsePhoneUI && m_inRemovingBundleFlow)
		{
			OnPackSelectorButtonClicked(m_packButtons[0], m_packButtons[0].GetStorePackId());
			m_inRemovingBundleFlow = false;
		}
	}

	public void RemoveFirstPurchaseBundle(float glowOutLength)
	{
		if (StoreManager.IsFirstPurchaseBundleOwned())
		{
			StartCoroutine(AnimateRemoveFirstPurchaseBundle(glowOutLength));
		}
	}

	private void OnItemPurchased(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		foreach (Network.BundleItem item in bundle.Items)
		{
			StorePackId storePackId = m_packsContent.GetStorePackId();
			if (item != null && item.ItemType == ProductType.PRODUCT_TYPE_RANDOM_CARD && storePackId.Type == StorePackType.BOOSTER && storePackId.Id == 181)
			{
				OnRandomCardPurchased(m_randomCardReward);
				break;
			}
			if (item != null && item.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE && storePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				m_packsContent.AnimateModularBundleAfterPurchase(storePackId);
				break;
			}
		}
	}

	private void OnRandomCardPurchased(CardReward cardReward)
	{
		if (m_packsContent == null)
		{
			CardRewardData cardRewardData = cardReward.Data as CardRewardData;
			Debug.LogWarningFormat("OnRandomCardPurchased() m_packsContent == null for cardID {0}", cardRewardData.CardID);
		}
		else
		{
			m_packsContent.FirstPurchaseBundlePurchased(cardReward);
		}
	}

	private void OnPackSelectorButtonClicked(GeneralStorePackSelectorButton btn, StorePackId storePackId)
	{
		if (!m_parentContent.IsContentActive())
		{
			return;
		}
		m_packsContent.SetBoosterId(storePackId);
		foreach (GeneralStorePackSelectorButton packButton in m_packButtons)
		{
			packButton.Unselect();
		}
		btn.Select();
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_BOOSTER_ID, btn.GetStorePackId().Id);
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_PACK_TYPE, (int)btn.GetStorePackId().Type);
		if (!string.IsNullOrEmpty(m_boosterSelectionSound))
		{
			SoundManager.Get().LoadAndPlay(m_boosterSelectionSound);
		}
	}

	private void SetupPackButtons()
	{
		Map<StorePackId, IStorePackDef> storePackDefs = m_packsContent.GetStorePackDefs();
		StorePackId storePackId2 = m_packsContent.GetStorePackId();
		bool flag = !HearthstoneApplication.IsPublic() && Vars.Key("ModularBundle.ShowAll").GetBool(def: false);
		foreach (KeyValuePair<StorePackId, IStorePackDef> item in storePackDefs)
		{
			StorePackId storePackId = item.Key;
			ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
			if (productTypeFromStorePackType == ProductType.PRODUCT_TYPE_BOOSTER && !CanShowBooster(storePackId.Id))
			{
				continue;
			}
			if (GameUtils.IsHiddenLicenseBundleBooster(storePackId))
			{
				bool flag2 = false;
				bool flag3 = false;
				int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
				for (int i = 0; i < productDataCountFromStorePackId; i++)
				{
					int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId, i);
					Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(productTypeFromStorePackType, requireRealMoneyOption: true, productDataFromStorePackId).FirstOrDefault();
					if (bundle != null)
					{
						flag2 = true;
						if (!StoreManager.Get().IsProductAlreadyOwned(bundle))
						{
							flag3 = true;
						}
					}
				}
				bool flag4 = false;
				if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
				{
					ModularBundleDbfRecord record = GameDbf.ModularBundle.GetRecord(storePackId.Id);
					flag4 = flag || (flag2 && (flag3 || record.ShowAfterPurchase));
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
			IStorePackDef value = item.Value;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(value.GetSelectorButtonPrefab(), AssetLoadingOptions.IgnorePrefabPosition);
			GameUtils.SetParent(gameObject, m_paneContainer, withRotation: true);
			SceneUtils.SetLayer(gameObject, m_paneContainer.layer);
			GeneralStorePackSelectorButton newPackButton = gameObject.GetComponent<GeneralStorePackSelectorButton>();
			newPackButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnPackSelectorButtonClicked(newPackButton, storePackId);
			});
			newPackButton.SetStorePackId(storePackId);
			if (storePackId == storePackId2)
			{
				newPackButton.Select();
				StoreManager.Get().SetCurrentlySelectedStorePack(storePackId2);
			}
			m_packButtons.Add(newPackButton);
		}
		UpdatePackButtonPositions();
	}

	private bool CanShowBooster(int boosterDbId)
	{
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterDbId);
		if (record == null)
		{
			return false;
		}
		if (!SpecialEventManager.Get().IsEventActive(record.BuyWithGoldEvent, activeIfDoesNotExist: false))
		{
			return false;
		}
		if (GameUtils.IsBoosterWild(record))
		{
			return false;
		}
		return true;
	}

	private void SortPackButtons()
	{
		m_packButtons.Sort(delegate(GeneralStorePackSelectorButton lhs, GeneralStorePackSelectorButton rhs)
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
			if (flag7 && flag8)
			{
				ModularBundleDbfRecord modularBundleDbfRecord = (ModularBundleDbfRecord)lhs.GetRecord();
				ModularBundleDbfRecord modularBundleDbfRecord2 = (ModularBundleDbfRecord)rhs.GetRecord();
				int num = modularBundleDbfRecord?.SortOrder ?? 0;
				int num2 = modularBundleDbfRecord2?.SortOrder ?? 0;
				if (num != num2)
				{
					return Mathf.Clamp(num2 - num, -1, 1);
				}
				int num3 = modularBundleDbfRecord?.ID ?? 0;
				int num4 = modularBundleDbfRecord2?.ID ?? 0;
				if (num3 >= num4)
				{
					return 1;
				}
				return -1;
			}
			if (flag3 != flag4)
			{
				if (!flag3)
				{
					return 1;
				}
				return -1;
			}
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
			BoosterDbfRecord boosterDbfRecord = (BoosterDbfRecord)lhs.GetRecord();
			BoosterDbfRecord boosterDbfRecord2 = (BoosterDbfRecord)rhs.GetRecord();
			bool flag13 = boosterDbfRecord != null && boosterDbfRecord.ID == 1;
			bool flag14 = boosterDbfRecord2 != null && boosterDbfRecord2.ID == 1;
			if (flag13 != flag14)
			{
				if (m_deprioritizeClassic)
				{
					if (!flag13)
					{
						return -1;
					}
					return 1;
				}
				if (!flag13)
				{
					return 1;
				}
				return -1;
			}
			int num5 = boosterDbfRecord?.ListDisplayOrderCategory ?? 0;
			int num6 = boosterDbfRecord2?.ListDisplayOrderCategory ?? 0;
			if (num5 != num6)
			{
				return Mathf.Clamp(num6 - num5, -1, 1);
			}
			int num7 = boosterDbfRecord?.ListDisplayOrder ?? 0;
			return Mathf.Clamp((boosterDbfRecord2?.ListDisplayOrder ?? 0) - num7, -1, 1);
		});
	}

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
				m_deprioritizeClassic = netObject.Store.NumClassicPacksUntilDeprioritize >= 0 && num >= netObject.Store.NumClassicPacksUntilDeprioritize;
			}
		}
		SortPackButtons();
		Vector3 zero = Vector3.zero;
		Vector3 onNormal = Vector3.Normalize(m_packButtonSpacing);
		for (int i = 0; i < m_packButtons.Count; i++)
		{
			GeneralStorePackSelectorButton generalStorePackSelectorButton = m_packButtons[i];
			bool flag = generalStorePackSelectorButton.HasPurchasableProducts();
			generalStorePackSelectorButton.gameObject.SetActive(flag);
			if (!flag)
			{
				continue;
			}
			generalStorePackSelectorButton.transform.localPosition = zero;
			Vector3 vector = m_packButtonSpacing;
			if (generalStorePackSelectorButton.m_useScrollableItemBoundsToStack)
			{
				UIBScrollableItem component = generalStorePackSelectorButton.GetComponent<UIBScrollableItem>();
				if (component != null)
				{
					vector = Vector3.Project(Matrix4x4.TRS(Vector3.zero, component.transform.localRotation, component.transform.localScale) * component.m_size, onNormal);
				}
			}
			zero += vector;
		}
	}

	private void UpdatePackButtonRecommendedIndicators()
	{
		int num = 0;
		GeneralStorePackSelectorButton[] array = m_packButtons.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			bool hideRibbon = num >= m_maxRibbons;
			if (array[i].UpdateRibbonIndicator(hideRibbon))
			{
				num++;
			}
		}
	}

	private bool ShouldResetPackSelection()
	{
		List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(ProductType.PRODUCT_TYPE_BOOSTER, requireRealMoneyOption: true);
		List<string> list = new List<string>(Options.Get().GetString(Option.SEEN_PACK_PRODUCT_LIST, string.Empty).Split(':'));
		bool result = false;
		foreach (Network.Bundle item in allBundlesForProduct)
		{
			if (!list.Contains(item.PMTProductID.ToString()))
			{
				list.Add(item.PMTProductID.ToString());
				result = true;
			}
		}
		Options.Get().SetString(Option.SEEN_PACK_PRODUCT_LIST, string.Join(":", list.ToArray()));
		return result;
	}

	private void SetupInitialSelectedPack()
	{
		StorePackId storePackId = default(StorePackId);
		if (ShouldResetPackSelection())
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
		foreach (GeneralStorePackSelectorButton packButton in m_packButtons)
		{
			if (packButton.GetStorePackId() == storePackId)
			{
				m_packsContent.SetBoosterId(storePackId, forceImmediate: true, InitialSelection: true);
				packButton.Select();
				break;
			}
		}
	}

	private IEnumerator AnimateRemoveFirstPurchaseBundle(float glowOutLength)
	{
		m_purchaseAnimationBlocker.SetActive(value: true);
		m_inRemovingBundleFlow = true;
		yield return new WaitForSeconds(glowOutLength + 1f);
		GeneralStorePackSelectorButton buttonToRemove = null;
		foreach (GeneralStorePackSelectorButton packButton in m_packButtons)
		{
			StorePackId storePackId = packButton.GetStorePackId();
			if (storePackId.Type == StorePackType.BOOSTER && storePackId.Id == 181)
			{
				buttonToRemove = packButton;
				break;
			}
		}
		if (buttonToRemove != null)
		{
			GeneralStore.Get().HidePacksPane(hide: true);
			if (!UniversalInputManager.UsePhoneUI)
			{
				yield return new WaitForSeconds(0.25f);
			}
			m_packButtons.Remove(buttonToRemove);
			Object.Destroy(buttonToRemove.gameObject);
			UpdatePackButtonPositions();
			UpdatePackButtonRecommendedIndicators();
			if (!UniversalInputManager.UsePhoneUI)
			{
				OnPackSelectorButtonClicked(m_packButtons[0], m_packButtons[0].GetStorePackId());
				m_inRemovingBundleFlow = false;
				yield return new WaitForSeconds(0.75f);
			}
			GeneralStore.Get().HidePacksPane(hide: false);
		}
		m_purchaseAnimationBlocker.SetActive(value: false);
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		NetCache.ProfileNotice profileNotice = newNotices.FirstOrDefault(WillStoreDisplayNotice);
		if (profileNotice != null)
		{
			RewardUtils.GetRewards(newNotices)[0].LoadRewardObject(RewardObjectLoaded);
		}
	}

	public bool WillStoreDisplayNotice(NetCache.ProfileNotice notice)
	{
		return WillStoreDisplayNotice(notice.Origin, notice.Type, notice.OriginData);
	}

	public bool WillStoreDisplayNotice(NetCache.ProfileNotice.NoticeOrigin noticeOrigin, NetCache.ProfileNotice.NoticeType noticeType, long noticeOriginData)
	{
		if (noticeOrigin == NetCache.ProfileNotice.NoticeOrigin.FROM_PURCHASE && noticeType == NetCache.ProfileNotice.NoticeType.REWARD_CARD && m_packsContent != null && m_packsContent.GetStorePackId().Type == StorePackType.BOOSTER && m_packsContent.GetStorePackId().Id == 181)
		{
			if (StoreManager.Get().IsSimpleCheckoutFeatureEnabled() && StoreManager.Get().ActiveTransactionProvider() == BattlePayProvider.BP_PROVIDER_BLIZZARD)
			{
				if (StoreManager.Get().IsPMTProductIDActiveTransaction(noticeOriginData))
				{
					return true;
				}
			}
			else if (StoreManager.Get().IsIdActiveTransaction(noticeOriginData))
			{
				return true;
			}
		}
		return false;
	}

	private void RewardObjectLoaded(Reward reward, object callbackData)
	{
		reward.Hide();
		m_randomCardReward = reward as CardReward;
	}
}
