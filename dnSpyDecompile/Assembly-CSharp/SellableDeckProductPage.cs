using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;

// Token: 0x020006B3 RID: 1715
public class SellableDeckProductPage : ProductPage
{
	// Token: 0x06005FE1 RID: 24545 RVA: 0x001F43FC File Offset: 0x001F25FC
	protected override void Awake()
	{
		base.Awake();
		base.OnProductVariantSet += this.HandleProductVariantSet;
	}

	// Token: 0x06005FE2 RID: 24546 RVA: 0x001F4418 File Offset: 0x001F2618
	public override void Open()
	{
		this.m_cardList = new ShopCardList(this.m_widget, this.m_scrollbar);
		base.Open();
		this.SelectVariant(ProductFactory.CreateEmptyProductDataModel());
		base.OnOpened += this.InitInput;
		this.m_preBuyPopupInfo = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_SELLABLE_DECK_CONFIRMATION_HEADER"),
			m_text = GameStrings.Get("GLUE_SELLABLE_DECK_CONFIRMATION_BODY"),
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_showAlertIcon = true,
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle
		};
	}

	// Token: 0x06005FE3 RID: 24547 RVA: 0x001F44A6 File Offset: 0x001F26A6
	public override void Close()
	{
		base.Close();
		this.m_cardList.RemoveListeners();
	}

	// Token: 0x06005FE4 RID: 24548 RVA: 0x001F44B9 File Offset: 0x001F26B9
	public void InitInput()
	{
		base.OnOpened -= this.InitInput;
		this.m_cardList.InitInput();
	}

	// Token: 0x06005FE5 RID: 24549 RVA: 0x001F44D8 File Offset: 0x001F26D8
	protected override void OnProductSet()
	{
		if (base.Product.Items.Count == 0)
		{
			Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] Received product {0} (ID: {1}) with no items in the list!", new object[]
			{
				base.Product.Name,
				base.Product.PmtId
			});
			return;
		}
		ProductSelectionDataModel selection = base.Selection;
		if (((selection != null) ? selection.Variant : null) == null || base.Selection.Variant.Items.Count == 0)
		{
			return;
		}
		int itemId = base.Selection.Variant.Items.First<RewardItemDataModel>().ItemId;
		SellableDeckDbfRecord record = GameDbf.SellableDeck.GetRecord(itemId);
		if (record == null)
		{
			Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] Failed to find DB record {0} for product {1} (ID: {2}) !", new object[]
			{
				itemId,
				base.Product.Name,
				base.Product.PmtId
			});
			return;
		}
		if (record.DeckTemplateRecord == null || record.DeckTemplateRecord.DeckRecord == null)
		{
			Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] The DB record {0} for product {1} (ID: {2}) does NOT have a deck template with a valid deck record!", new object[]
			{
				record.ID,
				base.Product.Name,
				base.Product.PmtId
			});
			return;
		}
		DeckDbfRecord deckRecord = record.DeckTemplateRecord.DeckRecord;
		BoosterDbId boosterId = BoosterDbId.INVALID;
		if (record.BoosterRecord != null)
		{
			int id = record.BoosterRecord.ID;
			if (!Enum.IsDefined(typeof(BoosterDbId), id))
			{
				Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] The DB record {0} for product {1} (ID: {2}) uses an invalid BoosterDbId ({3})!", new object[]
				{
					record.ID,
					base.Product.Name,
					base.Product.PmtId,
					id
				});
				return;
			}
			boosterId = (BoosterDbId)record.BoosterRecord.ID;
		}
		ShopDeckPouchDisplay component = this.deckWidget.GetComponent<ShopDeckPouchDisplay>();
		if (component == null)
		{
			Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] Deck Widget is missing a ShopDeckPouchDisplay!", Array.Empty<object>());
			return;
		}
		component.SetDeckPouchData(this.deckWidget, record.DeckTemplateRecord);
		this.RefreshClassSelectorData();
		this.m_cardList.SetData(deckRecord, boosterId);
		this.m_scrollbar.SetScrollImmediate(0f);
	}

	// Token: 0x06005FE6 RID: 24550 RVA: 0x001F4719 File Offset: 0x001F2919
	public void HandleProductVariantSet()
	{
		this.OnProductSet();
	}

	// Token: 0x06005FE7 RID: 24551 RVA: 0x001F4721 File Offset: 0x001F2921
	private void RefreshClassSelectorData()
	{
		if (this.classSelectorReference == null)
		{
			Log.Store.PrintWarning("[SellableDeckProductPage.RefreshClassSelectorData] Class selector reference is null!", Array.Empty<object>());
			return;
		}
		this.classSelectorReference.RegisterReadyListener<ShopClassVariantSelector>(delegate(ShopClassVariantSelector selector)
		{
			if (selector == null)
			{
				Log.Store.PrintWarning("[SellableDeckProductPage.RefreshClassSelectorData] Class selector object is null!", Array.Empty<object>());
				return;
			}
			selector.SetProductPage(this);
			selector.OnProductSet();
		});
	}

	// Token: 0x0400507B RID: 20603
	public Widget deckWidget;

	// Token: 0x0400507C RID: 20604
	public AsyncReference classSelectorReference;

	// Token: 0x0400507D RID: 20605
	public UIBScrollable m_scrollbar;

	// Token: 0x0400507E RID: 20606
	private ShopCardList m_cardList;
}
