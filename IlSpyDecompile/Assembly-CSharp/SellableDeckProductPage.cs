using System;
using System.Linq;
using Hearthstone.UI;

public class SellableDeckProductPage : ProductPage
{
	public Widget deckWidget;

	public AsyncReference classSelectorReference;

	public UIBScrollable m_scrollbar;

	private ShopCardList m_cardList;

	protected override void Awake()
	{
		base.Awake();
		base.OnProductVariantSet += HandleProductVariantSet;
	}

	public override void Open()
	{
		m_cardList = new ShopCardList(m_widget, m_scrollbar);
		base.Open();
		SelectVariant(ProductFactory.CreateEmptyProductDataModel());
		base.OnOpened += InitInput;
		m_preBuyPopupInfo = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_SELLABLE_DECK_CONFIRMATION_HEADER"),
			m_text = GameStrings.Get("GLUE_SELLABLE_DECK_CONFIRMATION_BODY"),
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_showAlertIcon = true,
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle
		};
	}

	public override void Close()
	{
		base.Close();
		m_cardList.RemoveListeners();
	}

	public void InitInput()
	{
		base.OnOpened -= InitInput;
		m_cardList.InitInput();
	}

	protected override void OnProductSet()
	{
		if (base.Product.Items.Count == 0)
		{
			Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] Received product {0} (ID: {1}) with no items in the list!", base.Product.Name, base.Product.PmtId);
		}
		else
		{
			if (base.Selection?.Variant == null || base.Selection.Variant.Items.Count == 0)
			{
				return;
			}
			int itemId = base.Selection.Variant.Items.First().ItemId;
			SellableDeckDbfRecord record = GameDbf.SellableDeck.GetRecord(itemId);
			if (record == null)
			{
				Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] Failed to find DB record {0} for product {1} (ID: {2}) !", itemId, base.Product.Name, base.Product.PmtId);
				return;
			}
			if (record.DeckTemplateRecord == null || record.DeckTemplateRecord.DeckRecord == null)
			{
				Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] The DB record {0} for product {1} (ID: {2}) does NOT have a deck template with a valid deck record!", record.ID, base.Product.Name, base.Product.PmtId);
				return;
			}
			DeckDbfRecord deckRecord = record.DeckTemplateRecord.DeckRecord;
			BoosterDbId boosterId = BoosterDbId.INVALID;
			if (record.BoosterRecord != null)
			{
				int iD = record.BoosterRecord.ID;
				if (!Enum.IsDefined(typeof(BoosterDbId), iD))
				{
					Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] The DB record {0} for product {1} (ID: {2}) uses an invalid BoosterDbId ({3})!", record.ID, base.Product.Name, base.Product.PmtId, iD);
					return;
				}
				boosterId = (BoosterDbId)record.BoosterRecord.ID;
			}
			ShopDeckPouchDisplay component = deckWidget.GetComponent<ShopDeckPouchDisplay>();
			if (component == null)
			{
				Log.Store.PrintWarning("[SellableDeckProductPage.OnProductSet] Deck Widget is missing a ShopDeckPouchDisplay!");
				return;
			}
			component.SetDeckPouchData(deckWidget, record.DeckTemplateRecord);
			RefreshClassSelectorData();
			m_cardList.SetData(deckRecord, boosterId);
			m_scrollbar.SetScrollImmediate(0f);
		}
	}

	public void HandleProductVariantSet()
	{
		OnProductSet();
	}

	private void RefreshClassSelectorData()
	{
		if (classSelectorReference == null)
		{
			Log.Store.PrintWarning("[SellableDeckProductPage.RefreshClassSelectorData] Class selector reference is null!");
			return;
		}
		classSelectorReference.RegisterReadyListener(delegate(ShopClassVariantSelector selector)
		{
			if (selector == null)
			{
				Log.Store.PrintWarning("[SellableDeckProductPage.RefreshClassSelectorData] Class selector object is null!");
			}
			else
			{
				selector.SetProductPage(this);
				selector.OnProductSet();
			}
		});
	}
}
