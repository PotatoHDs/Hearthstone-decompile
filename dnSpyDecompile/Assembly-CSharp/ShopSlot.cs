using System;
using System.Collections;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006BF RID: 1727
public class ShopSlot : ShopBrowserElement
{
	// Token: 0x170005D5 RID: 1493
	// (get) Token: 0x060060D0 RID: 24784 RVA: 0x001F914D File Offset: 0x001F734D
	// (set) Token: 0x060060D1 RID: 24785 RVA: 0x001F9160 File Offset: 0x001F7360
	[Overridable]
	public string Size
	{
		get
		{
			return this.m_slotSize.ToString();
		}
		set
		{
			this.m_slotSize = ShopSlot.GetSlotSizeFromString(value);
			this.UpdateSize();
		}
	}

	// Token: 0x060060D2 RID: 24786 RVA: 0x001F9174 File Offset: 0x001F7374
	private void Start()
	{
		this.m_widget = base.GetComponent<Widget>();
		this.m_shopCardTelemetry = new ShopCard();
		this.m_isFilled = false;
		if (this.m_boxCollider == null)
		{
			this.m_boxCollider = base.GetComponent<BoxCollider>();
			Clickable component = base.GetComponent<Clickable>();
			if (component)
			{
				component.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRelease));
				component.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnRollOver));
				component.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnRollOut));
			}
		}
		this.RefreshEnableInput();
		this.UpdateSize();
		this.DataModel = new ShopBrowserButtonDataModel
		{
			SlotWidth = base.Width,
			SlotHeight = base.Height
		};
		this.m_section = SceneUtils.FindComponentInParents<ShopSection>(base.gameObject);
		if (this.m_section != null)
		{
			this.m_section.RegisterSlot(this);
		}
	}

	// Token: 0x170005D6 RID: 1494
	// (get) Token: 0x060060D3 RID: 24787 RVA: 0x001F925E File Offset: 0x001F745E
	public bool IsFilled
	{
		get
		{
			return this.m_isFilled;
		}
	}

	// Token: 0x060060D4 RID: 24788 RVA: 0x001F9266 File Offset: 0x001F7466
	public void Reset()
	{
		if (this.m_isFilled)
		{
			this.SetBrowserButton(new ShopBrowserButtonDataModel
			{
				SlotWidth = base.Width,
				SlotHeight = base.Height
			});
		}
	}

	// Token: 0x060060D5 RID: 24789 RVA: 0x001F9294 File Offset: 0x001F7494
	public static Vector2 GetSlotSizeDims(ShopSlot.SlotSize size)
	{
		switch (size)
		{
		case ShopSlot.SlotSize.M:
			return new Vector2(1f, 1.3f);
		case ShopSlot.SlotSize.MWide:
			return new Vector2(2f, 1.3f);
		case ShopSlot.SlotSize.L:
			return new Vector2(2f, 2f);
		case ShopSlot.SlotSize.XL:
			return new Vector2(3f, 2f);
		case ShopSlot.SlotSize.XXL:
			return new Vector2(4f, 2f);
		default:
			return new Vector2(0f, 0f);
		}
	}

	// Token: 0x060060D6 RID: 24790 RVA: 0x001F9320 File Offset: 0x001F7520
	public static ShopSlot.SlotSize GetSlotSizeFromString(string size)
	{
		string a = size.ToUpper();
		if (a == "M")
		{
			return ShopSlot.SlotSize.M;
		}
		if (a == "MWIDE")
		{
			return ShopSlot.SlotSize.MWide;
		}
		if (a == "L")
		{
			return ShopSlot.SlotSize.L;
		}
		if (a == "XL")
		{
			return ShopSlot.SlotSize.XL;
		}
		if (!(a == "XXL"))
		{
			return ShopSlot.SlotSize.Custom;
		}
		return ShopSlot.SlotSize.XXL;
	}

	// Token: 0x170005D7 RID: 1495
	// (get) Token: 0x060060D7 RID: 24791 RVA: 0x001F9384 File Offset: 0x001F7584
	// (set) Token: 0x060060D8 RID: 24792 RVA: 0x001F93B7 File Offset: 0x001F75B7
	protected ShopBrowserButtonDataModel DataModel
	{
		get
		{
			IDataModel dataModel = null;
			if (this.m_widget != null)
			{
				this.m_widget.GetDataModel(19, out dataModel);
			}
			return dataModel as ShopBrowserButtonDataModel;
		}
		set
		{
			if (this.m_widget != null)
			{
				value.DisplayProduct = (value.DisplayProduct ?? ProductFactory.CreateEmptyProductDataModel());
				this.m_widget.BindDataModel(value, false);
				this.UpdateShopCardTelemetry();
			}
		}
	}

	// Token: 0x060060D9 RID: 24793 RVA: 0x001F93EF File Offset: 0x001F75EF
	public ShopCard GetShopCardTelemetry()
	{
		this.UpdateShopCardTelemetryTimeRemaining();
		return this.m_shopCardTelemetry;
	}

	// Token: 0x060060DA RID: 24794 RVA: 0x001F9400 File Offset: 0x001F7600
	public void SetBrowserButton(ShopBrowserButtonDataModel buttonDataModel)
	{
		this.DataModel = buttonDataModel;
		ProductDataModel productDataModel = buttonDataModel.DisplayProduct ?? ProductFactory.CreateEmptyProductDataModel();
		this.m_isFilled = (productDataModel != ProductFactory.CreateEmptyProductDataModel());
		this.RefreshEnableInput();
		if (this.m_widget != null)
		{
			this.m_widget.BindDataModel(productDataModel, false);
		}
		base.StartCoroutine(this.DisableChildCollidersCoroutine());
		this.UpdateSize();
	}

	// Token: 0x060060DB RID: 24795 RVA: 0x001F9469 File Offset: 0x001F7669
	public void EnableInput(bool enabled)
	{
		this.m_inputBlocked = !enabled;
		this.RefreshEnableInput();
	}

	// Token: 0x060060DC RID: 24796 RVA: 0x001F947B File Offset: 0x001F767B
	protected void RefreshEnableInput()
	{
		if (this.m_boxCollider == null)
		{
			return;
		}
		this.m_boxCollider.enabled = (this.m_isFilled && !this.m_inputBlocked);
	}

	// Token: 0x060060DD RID: 24797 RVA: 0x001F94AC File Offset: 0x001F76AC
	private void UpdateSize()
	{
		if (this.m_slotSize != ShopSlot.SlotSize.Custom)
		{
			Vector2 slotSizeDims = ShopSlot.GetSlotSizeDims(this.m_slotSize);
			this.Bounds.Set(-slotSizeDims.x / 2f, -slotSizeDims.y / 2f, slotSizeDims.x, slotSizeDims.y);
		}
		this.OnElementBoundsChanged();
	}

	// Token: 0x060060DE RID: 24798 RVA: 0x001F9504 File Offset: 0x001F7704
	protected override void OnElementBoundsChanged()
	{
		ShopBrowserButtonDataModel dataModel = this.DataModel;
		if (dataModel != null)
		{
			dataModel.SlotWidth = base.Width;
			dataModel.SlotHeight = base.Height;
		}
		if (this.m_boxCollider != null)
		{
			this.m_boxCollider.transform.localPosition = new Vector3(this.Bounds.center.x, this.m_boxCollider.transform.localPosition.y, this.Bounds.center.y);
			this.m_boxCollider.size = new Vector3(base.Width, this.m_boxCollider.size.y, base.Height);
		}
	}

	// Token: 0x060060DF RID: 24799 RVA: 0x001F95B8 File Offset: 0x001F77B8
	private void OnRelease(UIEvent e)
	{
		if (!this.m_isFilled)
		{
			Log.Store.PrintWarning("Ignoring click on shop slot that is not filled. The clickable for this ShopSlot should be disabled.", Array.Empty<object>());
			return;
		}
		TelemetryManager.Client().SendShopCardClick(this.m_shopCardTelemetry);
		Shop.Get().OpenProductPage(this.DataModel.DisplayProduct, null);
	}

	// Token: 0x060060E0 RID: 24800 RVA: 0x001F9608 File Offset: 0x001F7808
	private void OnRollOver(UIEvent e)
	{
		if (this.DataModel != null)
		{
			this.DataModel.Hovered = true;
		}
	}

	// Token: 0x060060E1 RID: 24801 RVA: 0x001F961E File Offset: 0x001F781E
	private void OnRollOut(UIEvent e)
	{
		if (this.DataModel != null)
		{
			this.DataModel.Hovered = false;
		}
	}

	// Token: 0x060060E2 RID: 24802 RVA: 0x001F9634 File Offset: 0x001F7834
	private IEnumerator DisableChildCollidersCoroutine()
	{
		while (this.m_widget.IsChangingStates)
		{
			yield return null;
		}
		foreach (Collider collider in base.GetComponentsInChildren<Collider>())
		{
			if (!(collider == this.m_boxCollider))
			{
				collider.enabled = false;
			}
		}
		yield break;
	}

	// Token: 0x060060E3 RID: 24803 RVA: 0x001F9644 File Offset: 0x001F7844
	private void UpdateShopCardTelemetry()
	{
		if (this.m_section == null)
		{
			return;
		}
		this.m_shopCardTelemetry = new ShopCard();
		ProductCatalog catalog = StoreManager.Get().Catalog;
		ProductTierDataModel tierDataModel = this.m_section.GetTierDataModel();
		if (tierDataModel != null)
		{
			this.m_shopCardTelemetry.SectionIndex = catalog.Tiers.IndexOf(tierDataModel);
			Network.ShopSection networkSection = catalog.GetNetworkSection(tierDataModel);
			if (networkSection != null)
			{
				this.m_shopCardTelemetry.SectionName = networkSection.InternalName;
			}
		}
		this.m_shopCardTelemetry.SlotIndex = this.m_section.GetSortedEnabledSlots().IndexOf(this);
		if (this.DataModel.DisplayProduct != ProductFactory.CreateEmptyProductDataModel())
		{
			this.m_shopCardTelemetry.Product = new Product
			{
				ProductId = this.DataModel.DisplayProduct.PmtId
			};
		}
		this.UpdateShopCardTelemetryTimeRemaining();
	}

	// Token: 0x060060E4 RID: 24804 RVA: 0x001F9714 File Offset: 0x001F7914
	private void UpdateShopCardTelemetryTimeRemaining()
	{
		StoreManager storeManager = StoreManager.Get();
		Network.Bundle bundleFromPmtProductId = storeManager.GetBundleFromPmtProductId(this.DataModel.DisplayProduct.PmtId);
		if (bundleFromPmtProductId != null)
		{
			ProductAvailabilityRange bundleAvailabilityRange = storeManager.GetBundleAvailabilityRange(bundleFromPmtProductId);
			if (bundleAvailabilityRange != null)
			{
				DateTime? endDateTime = bundleAvailabilityRange.EndDateTime;
				if (endDateTime != null)
				{
					DateTime utcNow = DateTime.UtcNow;
					this.m_shopCardTelemetry.SecondsRemaining = (int)Math.Min((endDateTime.Value - utcNow).TotalSeconds, 2147483647.0);
				}
			}
		}
	}

	// Token: 0x040050EE RID: 20718
	[SerializeField]
	private ShopSlot.SlotSize m_slotSize;

	// Token: 0x040050EF RID: 20719
	protected BoxCollider m_boxCollider;

	// Token: 0x040050F0 RID: 20720
	protected Widget m_widget;

	// Token: 0x040050F1 RID: 20721
	protected ShopSection m_section;

	// Token: 0x040050F2 RID: 20722
	protected bool m_isFilled;

	// Token: 0x040050F3 RID: 20723
	protected bool m_inputBlocked;

	// Token: 0x040050F4 RID: 20724
	private ShopCard m_shopCardTelemetry;

	// Token: 0x0200220E RID: 8718
	public enum SlotSize
	{
		// Token: 0x0400E242 RID: 57922
		Custom,
		// Token: 0x0400E243 RID: 57923
		M,
		// Token: 0x0400E244 RID: 57924
		MWide,
		// Token: 0x0400E245 RID: 57925
		L,
		// Token: 0x0400E246 RID: 57926
		XL,
		// Token: 0x0400E247 RID: 57927
		XXL
	}
}
