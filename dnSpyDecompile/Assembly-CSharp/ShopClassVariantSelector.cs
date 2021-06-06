using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006BA RID: 1722
public class ShopClassVariantSelector : MonoBehaviour
{
	// Token: 0x06006091 RID: 24721 RVA: 0x001F74D4 File Offset: 0x001F56D4
	protected virtual void Start()
	{
		this.m_classButtonWidgets = new Widget[this.m_classButtonReferences.Length];
		this.m_buttonDataModels = new DeckChoiceDataModel[this.m_classButtonReferences.Length];
		for (int i = 0; i < this.m_classButtonReferences.Length; i++)
		{
			int classIndex = i;
			this.m_classButtonReferences[classIndex].RegisterReadyListener<Widget>(delegate(Widget w)
			{
				this.SetupDataModelForButton(w, classIndex);
			});
		}
		this.m_deckChoiceDataModel = new DeckChoiceDataModel();
		this.m_chooseDeckReference.RegisterReadyListener<Widget>(delegate(Widget w)
		{
			this.m_chooseDeckWidget = w;
			w.BindDataModel(this.m_deckChoiceDataModel, false);
		});
	}

	// Token: 0x06006092 RID: 24722 RVA: 0x001F756E File Offset: 0x001F576E
	public void SetProductPage(ProductPage productPage)
	{
		this.m_productPage = productPage;
	}

	// Token: 0x06006093 RID: 24723 RVA: 0x001F7578 File Offset: 0x001F5778
	public void OnProductSet()
	{
		if (this.m_productPage != null && this.m_productPage.Product != null)
		{
			DataModelList<ProductDataModel> variants = this.m_productPage.Product.Variants;
			int count = variants.Count;
			if (count <= this.m_classButtonWidgets.Length)
			{
				for (int i = 0; i < count; i++)
				{
					this.SetupDataModelForButton(i, variants[i]);
				}
			}
		}
	}

	// Token: 0x06006094 RID: 24724 RVA: 0x001F75DD File Offset: 0x001F57DD
	public void SetSelectedButtonIndex(int index)
	{
		this.m_deckChoiceDataModel = this.m_classButtonWidgets[index].GetDataModel<DeckChoiceDataModel>();
		if (this.m_productPage != null)
		{
			this.m_productPage.SelectVariantByIndex(index);
		}
	}

	// Token: 0x06006095 RID: 24725 RVA: 0x001F760C File Offset: 0x001F580C
	private void SetupDataModelForButton(Widget w, int index)
	{
		string buttonClass = this.m_classByButtonIndex[index].ToString();
		DeckChoiceDataModel deckChoiceDataModel = new DeckChoiceDataModel();
		deckChoiceDataModel.ButtonClass = buttonClass;
		this.m_classButtonWidgets[index] = w;
		this.m_buttonDataModels[index] = deckChoiceDataModel;
		w.BindDataModel(deckChoiceDataModel, false);
	}

	// Token: 0x06006096 RID: 24726 RVA: 0x001F7658 File Offset: 0x001F5858
	private void SetupDataModelForButton(int index, ProductDataModel productVariant)
	{
		DeckTemplateDbfRecord deckTemplateRecordForProduct = this.GetDeckTemplateRecordForProduct(productVariant);
		if (deckTemplateRecordForProduct != null)
		{
			DeckChoiceDataModel deckChoiceDataModel = new DeckChoiceDataModel();
			int classId = deckTemplateRecordForProduct.ClassId;
			deckChoiceDataModel.ChoiceClassID = classId;
			TAG_CLASS tag_CLASS = (TAG_CLASS)classId;
			deckChoiceDataModel.ButtonClass = tag_CLASS.ToString();
			this.m_buttonDataModels[index] = deckChoiceDataModel;
			this.m_classButtonWidgets[index].BindDataModel(deckChoiceDataModel, false);
			this.m_classButtonWidgets[index].TriggerEvent("Default", default(Widget.TriggerEventParameters));
		}
	}

	// Token: 0x06006097 RID: 24727 RVA: 0x001F76D0 File Offset: 0x001F58D0
	private DeckTemplateDbfRecord GetDeckTemplateRecordForProduct(ProductDataModel productVariant)
	{
		if (productVariant.Items.Count < 1 || productVariant.Items[0].ItemType != RewardItemType.SELLABLE_DECK)
		{
			Log.Store.PrintWarning("[ShopClassVariantSelector.OnProductSet] Failed to find variant item!", Array.Empty<object>());
			return null;
		}
		int itemId = productVariant.Items[0].ItemId;
		SellableDeckDbfRecord record = GameDbf.SellableDeck.GetRecord(itemId);
		if (record == null)
		{
			Log.Store.PrintWarning("[ShopClassVariantSelector.OnProductSet] Failed to find DB record {0}!", new object[]
			{
				itemId
			});
			return null;
		}
		if (record.DeckTemplateRecord == null || record.DeckTemplateRecord.DeckRecord == null)
		{
			Log.Store.PrintWarning("[ShopClassVariantSelector.OnProductSet] The DB record {0} does NOT have a deck template with a valid deck record!", new object[]
			{
				record.ID
			});
			return null;
		}
		return record.DeckTemplateRecord;
	}

	// Token: 0x040050CC RID: 20684
	public AsyncReference m_chooseDeckReference;

	// Token: 0x040050CD RID: 20685
	public AsyncReference[] m_classButtonReferences;

	// Token: 0x040050CE RID: 20686
	private DeckChoiceDataModel m_deckChoiceDataModel;

	// Token: 0x040050CF RID: 20687
	private DeckChoiceDataModel[] m_buttonDataModels;

	// Token: 0x040050D0 RID: 20688
	private Widget[] m_classButtonWidgets;

	// Token: 0x040050D1 RID: 20689
	private Widget m_chooseDeckWidget;

	// Token: 0x040050D2 RID: 20690
	private ProductPage m_productPage;

	// Token: 0x040050D3 RID: 20691
	private List<DeckTemplateDbfRecord> m_deckTemplates;

	// Token: 0x040050D4 RID: 20692
	private TAG_CLASS[] m_classByButtonIndex = new TAG_CLASS[]
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
}
