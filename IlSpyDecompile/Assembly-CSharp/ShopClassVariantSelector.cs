using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ShopClassVariantSelector : MonoBehaviour
{
	public AsyncReference m_chooseDeckReference;

	public AsyncReference[] m_classButtonReferences;

	private DeckChoiceDataModel m_deckChoiceDataModel;

	private DeckChoiceDataModel[] m_buttonDataModels;

	private Widget[] m_classButtonWidgets;

	private Widget m_chooseDeckWidget;

	private ProductPage m_productPage;

	private List<DeckTemplateDbfRecord> m_deckTemplates;

	private TAG_CLASS[] m_classByButtonIndex = new TAG_CLASS[10]
	{
		TAG_CLASS.DRUID,
		TAG_CLASS.HUNTER,
		TAG_CLASS.MAGE,
		TAG_CLASS.PALADIN,
		TAG_CLASS.PRIEST,
		TAG_CLASS.ROGUE,
		TAG_CLASS.SHAMAN,
		TAG_CLASS.WARLOCK,
		TAG_CLASS.WARRIOR,
		TAG_CLASS.DEMONHUNTER
	};

	protected virtual void Start()
	{
		m_classButtonWidgets = new Widget[m_classButtonReferences.Length];
		m_buttonDataModels = new DeckChoiceDataModel[m_classButtonReferences.Length];
		for (int i = 0; i < m_classButtonReferences.Length; i++)
		{
			int classIndex = i;
			m_classButtonReferences[classIndex].RegisterReadyListener(delegate(Widget w)
			{
				SetupDataModelForButton(w, classIndex);
			});
		}
		m_deckChoiceDataModel = new DeckChoiceDataModel();
		m_chooseDeckReference.RegisterReadyListener(delegate(Widget w)
		{
			m_chooseDeckWidget = w;
			w.BindDataModel(m_deckChoiceDataModel);
		});
	}

	public void SetProductPage(ProductPage productPage)
	{
		m_productPage = productPage;
	}

	public void OnProductSet()
	{
		if (!(m_productPage != null) || m_productPage.Product == null)
		{
			return;
		}
		DataModelList<ProductDataModel> variants = m_productPage.Product.Variants;
		int count = variants.Count;
		if (count <= m_classButtonWidgets.Length)
		{
			for (int i = 0; i < count; i++)
			{
				SetupDataModelForButton(i, variants[i]);
			}
		}
	}

	public void SetSelectedButtonIndex(int index)
	{
		m_deckChoiceDataModel = m_classButtonWidgets[index].GetDataModel<DeckChoiceDataModel>();
		if (m_productPage != null)
		{
			m_productPage.SelectVariantByIndex(index);
		}
	}

	private void SetupDataModelForButton(Widget w, int index)
	{
		string buttonClass = m_classByButtonIndex[index].ToString();
		DeckChoiceDataModel deckChoiceDataModel = new DeckChoiceDataModel();
		deckChoiceDataModel.ButtonClass = buttonClass;
		m_classButtonWidgets[index] = w;
		m_buttonDataModels[index] = deckChoiceDataModel;
		w.BindDataModel(deckChoiceDataModel);
	}

	private void SetupDataModelForButton(int index, ProductDataModel productVariant)
	{
		DeckTemplateDbfRecord deckTemplateRecordForProduct = GetDeckTemplateRecordForProduct(productVariant);
		if (deckTemplateRecordForProduct != null)
		{
			DeckChoiceDataModel deckChoiceDataModel = new DeckChoiceDataModel();
			int num = (deckChoiceDataModel.ChoiceClassID = deckTemplateRecordForProduct.ClassId);
			TAG_CLASS tAG_CLASS = (TAG_CLASS)num;
			deckChoiceDataModel.ButtonClass = tAG_CLASS.ToString();
			m_buttonDataModels[index] = deckChoiceDataModel;
			m_classButtonWidgets[index].BindDataModel(deckChoiceDataModel);
			m_classButtonWidgets[index].TriggerEvent("Default");
		}
	}

	private DeckTemplateDbfRecord GetDeckTemplateRecordForProduct(ProductDataModel productVariant)
	{
		if (productVariant.Items.Count < 1 || productVariant.Items[0].ItemType != RewardItemType.SELLABLE_DECK)
		{
			Log.Store.PrintWarning("[ShopClassVariantSelector.OnProductSet] Failed to find variant item!");
			return null;
		}
		int itemId = productVariant.Items[0].ItemId;
		SellableDeckDbfRecord record = GameDbf.SellableDeck.GetRecord(itemId);
		if (record == null)
		{
			Log.Store.PrintWarning("[ShopClassVariantSelector.OnProductSet] Failed to find DB record {0}!", itemId);
			return null;
		}
		if (record.DeckTemplateRecord == null || record.DeckTemplateRecord.DeckRecord == null)
		{
			Log.Store.PrintWarning("[ShopClassVariantSelector.OnProductSet] The DB record {0} does NOT have a deck template with a valid deck record!", record.ID);
			return null;
		}
		return record.DeckTemplateRecord;
	}
}
