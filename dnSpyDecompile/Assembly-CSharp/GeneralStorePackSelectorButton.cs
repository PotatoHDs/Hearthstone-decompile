using System;
using System.Linq;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000701 RID: 1793
[CustomEditClass]
public class GeneralStorePackSelectorButton : PegUIElement
{
	// Token: 0x0600646D RID: 25709 RVA: 0x0020D244 File Offset: 0x0020B444
	public void SetStorePackId(StorePackId storePackId)
	{
		this.m_storePackId = storePackId;
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			this.m_dbfRecord = GameDbf.Booster.GetRecord(storePackId.Id);
			this.m_isLatestExpansion = GameUtils.IsBoosterLatestActiveExpansion(storePackId.Id);
			this.SetBoosterName(((BoosterDbfRecord)this.m_dbfRecord).Name);
			return;
		}
		if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			ModularBundleDbfRecord record = GameDbf.ModularBundle.GetRecord(storePackId.Id);
			this.m_dbfRecord = record;
			this.SetBoosterName(record.Name);
			if (this.m_packAmountBanner != null && this.m_packAmountBannerText != null)
			{
				if (record.SelectorPackAmountBanner > 0)
				{
					this.m_packAmountBanner.SetActive(true);
					this.m_packAmountBannerText.Text = record.SelectorPackAmountBanner.ToString();
					return;
				}
				this.m_packAmountBanner.SetActive(false);
			}
		}
	}

	// Token: 0x0600646E RID: 25710 RVA: 0x0020D330 File Offset: 0x0020B530
	public void SetBoosterName(string name)
	{
		if (this.m_packText != null)
		{
			this.m_packText.Text = name;
		}
	}

	// Token: 0x0600646F RID: 25711 RVA: 0x0020D34C File Offset: 0x0020B54C
	public StorePackId GetStorePackId()
	{
		return this.m_storePackId;
	}

	// Token: 0x06006470 RID: 25712 RVA: 0x0020D354 File Offset: 0x0020B554
	public DbfRecord GetRecord()
	{
		return this.m_dbfRecord;
	}

	// Token: 0x06006471 RID: 25713 RVA: 0x0020D35C File Offset: 0x0020B55C
	public void Select()
	{
		if (this.m_selected)
		{
			return;
		}
		this.m_selected = true;
		this.m_highlight.ChangeState((base.GetInteractionState() == PegUIElement.InteractionState.Up) ? ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE : ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		if (!string.IsNullOrEmpty(this.m_selectSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_selectSound);
		}
	}

	// Token: 0x06006472 RID: 25714 RVA: 0x0020D3B8 File Offset: 0x0020B5B8
	public void Unselect()
	{
		if (!this.m_selected)
		{
			return;
		}
		this.m_selected = false;
		this.m_highlight.ChangeState(ActorStateType.NONE);
		if (!string.IsNullOrEmpty(this.m_unselectSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_unselectSound);
		}
	}

	// Token: 0x06006473 RID: 25715 RVA: 0x0020D404 File Offset: 0x0020B604
	public bool UpdateRibbonIndicator(bool hideRibbon)
	{
		if (this.m_ribbonIndicator == null || this.GetStorePackId().Type == StorePackType.INVALID)
		{
			return false;
		}
		if (hideRibbon)
		{
			this.m_ribbonIndicator.SetActive(false);
			return false;
		}
		bool flag = false;
		StorePackId storePackId = this.GetStorePackId();
		if (GameUtils.IsFirstPurchaseBundleBooster(storePackId))
		{
			flag = true;
			this.m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKBUY_BEST_VALUE");
		}
		else if (this.IsPreorder())
		{
			flag = true;
			this.m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKS_PREORDER_TEXT");
		}
		else if (GameUtils.IsLimitedTimeOffer(storePackId))
		{
			flag = true;
			this.m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKBUY_LIMITED_TIME");
		}
		else if (this.IsRecommendedForNewPlayer() && StoreManager.IsFirstPurchaseBundleOwned())
		{
			flag = true;
			this.m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKBUY_SUGGESTION");
		}
		else if (this.IsLatestExpansion())
		{
			flag = true;
			this.m_ribbonIndicatorText.Text = GameStrings.Get("GLUE_STORE_PACKS_LATEST_EXPANSION");
		}
		this.m_ribbonIndicator.SetActive(flag);
		return flag;
	}

	// Token: 0x06006474 RID: 25716 RVA: 0x0020D504 File Offset: 0x0020B704
	public bool HasPurchasableProducts()
	{
		StorePackId storePackId = this.GetStorePackId();
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
		for (int i = 0; i < productDataCountFromStorePackId; i++)
		{
			if (StoreManager.Get().EnumerateBundlesForProductType(StorePackId.GetProductTypeFromStorePackType(storePackId), true, GameUtils.GetProductDataFromStorePackId(storePackId, i), 0, true).Any<Network.Bundle>())
			{
				return true;
			}
		}
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(storePackId.Id);
			if (record != null && SpecialEventManager.Get().IsEventActive(record.BuyWithGoldEvent, false))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006475 RID: 25717 RVA: 0x0020D584 File Offset: 0x0020B784
	public bool IsRecommendedForNewPlayer()
	{
		float num = CollectionManager.Get().CollectionLastModifiedTime();
		if (this.m_collectionManagerLastModifiedTime == num)
		{
			return this.m_cachedRecommendedForNewPlayer;
		}
		this.m_collectionManagerLastModifiedTime = num;
		if (this.m_checkNewPlayer)
		{
			int num2 = CollectionManager.Get().NumCardsOwnedInSet(TAG_CARD_SET.EXPERT1);
			if (GameUtils.GetBoosterCount(1) * 5 + num2 <= this.m_recommendedExpertSetOwnedCardCount)
			{
				this.m_cachedRecommendedForNewPlayer = true;
				return true;
			}
		}
		this.m_cachedRecommendedForNewPlayer = false;
		return false;
	}

	// Token: 0x06006476 RID: 25718 RVA: 0x0020D5EC File Offset: 0x0020B7EC
	public bool IsPreorder()
	{
		Network.Bundle bundle = null;
		StorePackId storePackId = this.GetStorePackId();
		int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId, 0);
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
		return StoreManager.Get().IsBoosterPreorderActive(productDataFromStorePackId, productTypeFromStorePackType, out bundle);
	}

	// Token: 0x06006477 RID: 25719 RVA: 0x0020D61D File Offset: 0x0020B81D
	public bool IsLatestExpansion()
	{
		return this.m_isLatestExpansion && !this.IsPreorder();
	}

	// Token: 0x06006478 RID: 25720 RVA: 0x0020D632 File Offset: 0x0020B832
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		base.OnOver(oldState);
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
		if (!string.IsNullOrEmpty(this.m_mouseOverSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_mouseOverSound);
		}
	}

	// Token: 0x06006479 RID: 25721 RVA: 0x0020D66B File Offset: 0x0020B86B
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.OnOut(oldState);
		this.m_highlight.ChangeState(this.m_selected ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
	}

	// Token: 0x0600647A RID: 25722 RVA: 0x0020D68D File Offset: 0x0020B88D
	protected override void OnRelease()
	{
		base.OnRelease();
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
	}

	// Token: 0x0600647B RID: 25723 RVA: 0x0020D6A3 File Offset: 0x0020B8A3
	protected override void OnPress()
	{
		base.OnPress();
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
	}

	// Token: 0x04005387 RID: 21383
	public UberText m_packText;

	// Token: 0x04005388 RID: 21384
	public HighlightState m_highlight;

	// Token: 0x04005389 RID: 21385
	public GameObject m_ribbonIndicator;

	// Token: 0x0400538A RID: 21386
	public UberText m_ribbonIndicatorText;

	// Token: 0x0400538B RID: 21387
	public GameObject m_packAmountBanner;

	// Token: 0x0400538C RID: 21388
	public UberText m_packAmountBannerText;

	// Token: 0x0400538D RID: 21389
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_selectSound;

	// Token: 0x0400538E RID: 21390
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_unselectSound;

	// Token: 0x0400538F RID: 21391
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_mouseOverSound;

	// Token: 0x04005390 RID: 21392
	public bool m_checkNewPlayer;

	// Token: 0x04005391 RID: 21393
	[CustomEditField(Parent = "m_checkNewPlayer")]
	public int m_recommendedExpertSetOwnedCardCount = 100;

	// Token: 0x04005392 RID: 21394
	public bool m_useScrollableItemBoundsToStack;

	// Token: 0x04005393 RID: 21395
	private bool m_selected;

	// Token: 0x04005394 RID: 21396
	private DbfRecord m_dbfRecord;

	// Token: 0x04005395 RID: 21397
	private StorePackId m_storePackId;

	// Token: 0x04005396 RID: 21398
	private bool m_isLatestExpansion;

	// Token: 0x04005397 RID: 21399
	private float m_collectionManagerLastModifiedTime = float.NaN;

	// Token: 0x04005398 RID: 21400
	private bool m_cachedRecommendedForNewPlayer;
}
