using System.Linq;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class GeneralStorePackSelectorButton : PegUIElement
{
	public UberText m_packText;

	public HighlightState m_highlight;

	public GameObject m_ribbonIndicator;

	public UberText m_ribbonIndicatorText;

	public GameObject m_packAmountBanner;

	public UberText m_packAmountBannerText;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_selectSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_unselectSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_mouseOverSound;

	public bool m_checkNewPlayer;

	[CustomEditField(Parent = "m_checkNewPlayer")]
	public int m_recommendedExpertSetOwnedCardCount = 100;

	public bool m_useScrollableItemBoundsToStack;

	private bool m_selected;

	private DbfRecord m_dbfRecord;

	private StorePackId m_storePackId;

	private bool m_isLatestExpansion;

	private float m_collectionManagerLastModifiedTime = float.NaN;

	private bool m_cachedRecommendedForNewPlayer;

	public void SetStorePackId(StorePackId storePackId)
	{
		m_storePackId = storePackId;
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			m_dbfRecord = GameDbf.Booster.GetRecord(storePackId.Id);
			m_isLatestExpansion = GameUtils.IsBoosterLatestActiveExpansion(storePackId.Id);
			SetBoosterName(((BoosterDbfRecord)m_dbfRecord).Name);
		}
		else
		{
			if (storePackId.Type != StorePackType.MODULAR_BUNDLE)
			{
				return;
			}
			ModularBundleDbfRecord modularBundleDbfRecord = (ModularBundleDbfRecord)(m_dbfRecord = GameDbf.ModularBundle.GetRecord(storePackId.Id));
			SetBoosterName(modularBundleDbfRecord.Name);
			if (m_packAmountBanner != null && m_packAmountBannerText != null)
			{
				if (modularBundleDbfRecord.SelectorPackAmountBanner > 0)
				{
					m_packAmountBanner.SetActive(value: true);
					m_packAmountBannerText.Text = modularBundleDbfRecord.SelectorPackAmountBanner.ToString();
				}
				else
				{
					m_packAmountBanner.SetActive(value: false);
				}
			}
		}
	}

	public void SetBoosterName(string name)
	{
		if (m_packText != null)
		{
			m_packText.Text = name;
		}
	}

	public StorePackId GetStorePackId()
	{
		return m_storePackId;
	}

	public DbfRecord GetRecord()
	{
		return m_dbfRecord;
	}

	public void Select()
	{
		if (!m_selected)
		{
			m_selected = true;
			m_highlight.ChangeState((GetInteractionState() == InteractionState.Up) ? ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE : ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			if (!string.IsNullOrEmpty(m_selectSound))
			{
				SoundManager.Get().LoadAndPlay(m_selectSound);
			}
		}
	}

	public void Unselect()
	{
		if (m_selected)
		{
			m_selected = false;
			m_highlight.ChangeState(ActorStateType.NONE);
			if (!string.IsNullOrEmpty(m_unselectSound))
			{
				SoundManager.Get().LoadAndPlay(m_unselectSound);
			}
		}
	}

	public bool UpdateRibbonIndicator(bool hideRibbon)
	{
		if (m_ribbonIndicator == null || GetStorePackId().Type == StorePackType.INVALID)
		{
			return false;
		}
		if (hideRibbon)
		{
			m_ribbonIndicator.SetActive(value: false);
			return false;
		}
		bool flag = false;
		StorePackId storePackId = GetStorePackId();
		if (GameUtils.IsFirstPurchaseBundleBooster(storePackId))
		{
			flag = true;
			m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKBUY_BEST_VALUE");
		}
		else if (IsPreorder())
		{
			flag = true;
			m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKS_PREORDER_TEXT");
		}
		else if (GameUtils.IsLimitedTimeOffer(storePackId))
		{
			flag = true;
			m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKBUY_LIMITED_TIME");
		}
		else if (IsRecommendedForNewPlayer() && StoreManager.IsFirstPurchaseBundleOwned())
		{
			flag = true;
			m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKBUY_SUGGESTION");
		}
		else if (IsLatestExpansion())
		{
			flag = true;
			m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKS_LATEST_EXPANSION");
		}
		m_ribbonIndicator.SetActive(flag);
		return flag;
	}

	public bool HasPurchasableProducts()
	{
		StorePackId storePackId = GetStorePackId();
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
		for (int i = 0; i < productDataCountFromStorePackId; i++)
		{
			if (StoreManager.Get().EnumerateBundlesForProductType(StorePackId.GetProductTypeFromStorePackType(storePackId), requireRealMoneyOption: true, GameUtils.GetProductDataFromStorePackId(storePackId, i)).Any())
			{
				return true;
			}
		}
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(storePackId.Id);
			if (record != null && SpecialEventManager.Get().IsEventActive(record.BuyWithGoldEvent, activeIfDoesNotExist: false))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsRecommendedForNewPlayer()
	{
		float num = CollectionManager.Get().CollectionLastModifiedTime();
		if (m_collectionManagerLastModifiedTime == num)
		{
			return m_cachedRecommendedForNewPlayer;
		}
		m_collectionManagerLastModifiedTime = num;
		if (m_checkNewPlayer)
		{
			int num2 = CollectionManager.Get().NumCardsOwnedInSet(TAG_CARD_SET.EXPERT1);
			if (GameUtils.GetBoosterCount(1) * 5 + num2 <= m_recommendedExpertSetOwnedCardCount)
			{
				m_cachedRecommendedForNewPlayer = true;
				return true;
			}
		}
		m_cachedRecommendedForNewPlayer = false;
		return false;
	}

	public bool IsPreorder()
	{
		Network.Bundle preOrderBundle = null;
		StorePackId storePackId = GetStorePackId();
		int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId);
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
		return StoreManager.Get().IsBoosterPreorderActive(productDataFromStorePackId, productTypeFromStorePackType, out preOrderBundle);
	}

	public bool IsLatestExpansion()
	{
		if (m_isLatestExpansion)
		{
			return !IsPreorder();
		}
		return false;
	}

	protected override void OnOver(InteractionState oldState)
	{
		base.OnOver(oldState);
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
		if (!string.IsNullOrEmpty(m_mouseOverSound))
		{
			SoundManager.Get().LoadAndPlay(m_mouseOverSound);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		base.OnOut(oldState);
		m_highlight.ChangeState(m_selected ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
	}

	protected override void OnRelease()
	{
		base.OnRelease();
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
	}

	protected override void OnPress()
	{
		base.OnPress();
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
	}
}
