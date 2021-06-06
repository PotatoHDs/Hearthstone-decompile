using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006BE RID: 1726
public class ShopSection : ShopBrowserElement
{
	// Token: 0x060060B4 RID: 24756 RVA: 0x001F8A84 File Offset: 0x001F6C84
	private void Start()
	{
		this.m_widget = base.GetComponent<Widget>();
		this.m_scrollableItem = base.GetComponentInChildren<UIBScrollableItem>();
		ShopBrowser shopBrowser = SceneUtils.FindComponentInParents<ShopBrowser>(base.gameObject);
		if (shopBrowser != null)
		{
			shopBrowser.RegisterSection(this);
		}
		if (!this.SuppressSelfRefresh)
		{
			ProductTierDataModel tierDataModel = this.GetTierDataModel();
			if (tierDataModel != null && tierDataModel.BrowserButtons.Count > 0)
			{
				this.ScheduleRefresh();
			}
		}
	}

	// Token: 0x060060B5 RID: 24757 RVA: 0x001F8AEB File Offset: 0x001F6CEB
	private void OnEnable()
	{
		if (this.m_slotsDirty && !this.SuppressSelfRefresh)
		{
			base.StartCoroutine(this.RefreshSlotsWhenReady());
		}
	}

	// Token: 0x060060B6 RID: 24758 RVA: 0x001F8B0A File Offset: 0x001F6D0A
	private void Update()
	{
		if (!this.SuppressSelfRefresh)
		{
			this.CheckDoneResolvingLayout();
		}
	}

	// Token: 0x170005D0 RID: 1488
	// (get) Token: 0x060060B7 RID: 24759 RVA: 0x001F8B1B File Offset: 0x001F6D1B
	// (set) Token: 0x060060B8 RID: 24760 RVA: 0x001F8B23 File Offset: 0x001F6D23
	public bool IsResolvingLayout { get; private set; }

	// Token: 0x170005D1 RID: 1489
	// (get) Token: 0x060060B9 RID: 24761 RVA: 0x001F8B2C File Offset: 0x001F6D2C
	// (set) Token: 0x060060BA RID: 24762 RVA: 0x001F8B34 File Offset: 0x001F6D34
	public bool IsResolvingSlotVisuals { get; private set; }

	// Token: 0x170005D2 RID: 1490
	// (get) Token: 0x060060BB RID: 24763 RVA: 0x001F8B3D File Offset: 0x001F6D3D
	// (set) Token: 0x060060BC RID: 24764 RVA: 0x001F8B45 File Offset: 0x001F6D45
	public bool SuppressResolvingSlots { get; set; }

	// Token: 0x170005D3 RID: 1491
	// (get) Token: 0x060060BD RID: 24765 RVA: 0x001F8B4E File Offset: 0x001F6D4E
	// (set) Token: 0x060060BE RID: 24766 RVA: 0x001F8B56 File Offset: 0x001F6D56
	public bool SuppressSelfRefresh { get; set; }

	// Token: 0x170005D4 RID: 1492
	// (get) Token: 0x060060BF RID: 24767 RVA: 0x001F8B5F File Offset: 0x001F6D5F
	public Widget widget
	{
		get
		{
			return this.m_widget;
		}
	}

	// Token: 0x060060C0 RID: 24768 RVA: 0x001F8B67 File Offset: 0x001F6D67
	public void ScheduleRefresh()
	{
		if (!this.m_slotsDirty)
		{
			this.m_slotsDirty = true;
			if (base.gameObject.activeInHierarchy)
			{
				base.StartCoroutine(this.RefreshSlotsWhenReady());
			}
		}
	}

	// Token: 0x060060C1 RID: 24769 RVA: 0x001F8B92 File Offset: 0x001F6D92
	public void RegisterSlot(ShopSlot slot)
	{
		if (!this.m_slots.Contains(slot))
		{
			this.m_slots.Add(slot);
		}
		if (!this.SuppressSelfRefresh)
		{
			this.ScheduleRefresh();
		}
	}

	// Token: 0x060060C2 RID: 24770 RVA: 0x001F8BBC File Offset: 0x001F6DBC
	public void ResizeHeightForStacking()
	{
		List<ShopBrowserElement> list = new List<ShopBrowserElement>();
		foreach (object obj in base.transform)
		{
			ShopSection.GetActiveElementsExcludeShopSlots(((Transform)obj).gameObject, list);
		}
		if (list.Count > 0)
		{
			float num = float.MaxValue;
			float num2 = float.MinValue;
			foreach (ShopBrowserElement shopBrowserElement in list)
			{
				num = Mathf.Min(num, shopBrowserElement.Bounds.yMin);
				num2 = Mathf.Max(num2, shopBrowserElement.Bounds.yMax);
			}
			this.Bounds.yMin = num;
			this.Bounds.yMax = num2;
			this.OnElementBoundsChanged();
		}
	}

	// Token: 0x060060C3 RID: 24771 RVA: 0x001F8CB8 File Offset: 0x001F6EB8
	public ProductTierDataModel GetTierDataModel()
	{
		return this.m_widget.GetDataModel<ProductTierDataModel>();
	}

	// Token: 0x060060C4 RID: 24772 RVA: 0x001F8CC8 File Offset: 0x001F6EC8
	public List<ShopSlot> GetSortedEnabledSlots()
	{
		List<ShopSlot> list = (from s in this.m_slots
		where this.IsSlotEnabled(s)
		select s).ToList<ShopSlot>();
		list.Sort((ShopSlot A, ShopSlot B) => ShopBrowserElement.ComparePosition(A, B));
		return list;
	}

	// Token: 0x060060C5 RID: 24773 RVA: 0x001F8D18 File Offset: 0x001F6F18
	public void BindDataModelsToSlots()
	{
		this.m_slotsDirty = false;
		this.IsResolvingSlotVisuals = true;
		ProductTierDataModel tierDataModel = this.GetTierDataModel();
		if (tierDataModel == null || tierDataModel.BrowserButtons == null)
		{
			base.IsElementEnabled = false;
			this.IsResolvingSlotVisuals = false;
			return;
		}
		DataModelList<ShopBrowserButtonDataModel> browserButtons = tierDataModel.BrowserButtons;
		List<ShopSlot> sortedEnabledSlots = this.GetSortedEnabledSlots();
		bool flag = false;
		int i = 0;
		int num = Math.Min(sortedEnabledSlots.Count, browserButtons.Count);
		while (i < num)
		{
			ShopBrowserButtonDataModel shopBrowserButtonDataModel = browserButtons.ElementAt(i);
			if (!shopBrowserButtonDataModel.IsFiller && shopBrowserButtonDataModel.DisplayProduct.Availability == ProductAvailability.CAN_PURCHASE)
			{
				flag = true;
			}
			i++;
		}
		bool flag2 = StoreManager.Get().Catalog.CurrentTestDataMode == ProductCatalog.TestDataMode.TIER_TEST_DATA;
		base.IsElementEnabled = (flag || (flag2 && browserButtons.Count > 0));
		if (!base.IsElementEnabled)
		{
			this.IsResolvingSlotVisuals = false;
			return;
		}
		for (int j = 0; j < sortedEnabledSlots.Count; j++)
		{
			ShopSlot shopSlot = sortedEnabledSlots[j];
			if (j < browserButtons.Count)
			{
				shopSlot.SetBrowserButton(browserButtons.ElementAt(j));
			}
			else
			{
				shopSlot.Reset();
			}
		}
		this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
		{
			this.IsResolvingSlotVisuals = false;
		}, null, true, true);
	}

	// Token: 0x060060C6 RID: 24774 RVA: 0x001F8E47 File Offset: 0x001F7047
	private IEnumerator RefreshSlotsWhenReady()
	{
		Log.Store.PrintDebug("{0} resolving layout...", new object[]
		{
			base.gameObject.name
		});
		this.IsResolvingLayout = true;
		while (!this.CheckDoneResolvingLayout())
		{
			yield return null;
		}
		Log.Store.PrintDebug("{0} layout resolved", new object[]
		{
			base.gameObject.name
		});
		if (!this.m_slotsDirty)
		{
			yield break;
		}
		if (this.SuppressResolvingSlots)
		{
			Log.Store.PrintDebug("{0} suppressing slot visuals...", new object[]
			{
				base.gameObject.name
			});
			this.IsResolvingSlotVisuals = true;
			this.m_slots.ForEach(delegate(ShopSlot s)
			{
				s.Reset();
			});
			while (this.SuppressResolvingSlots)
			{
				yield return null;
			}
		}
		this.BindDataModelsToSlots();
		yield break;
	}

	// Token: 0x060060C7 RID: 24775 RVA: 0x001F8E58 File Offset: 0x001F7058
	private bool CheckDoneResolvingLayout()
	{
		if (this.IsResolvingLayout)
		{
			if (!this.m_widget.GetIsChangingStates((GameObject go) => go.GetComponent<ShopSlot>() == null))
			{
				Log.Store.PrintDebug("ShopSection done resolving layout", Array.Empty<object>());
				this.IsResolvingLayout = false;
			}
		}
		return !this.IsResolvingLayout;
	}

	// Token: 0x060060C8 RID: 24776 RVA: 0x001F8EBD File Offset: 0x001F70BD
	private bool IsSlotEnabled(ShopSlot slot)
	{
		return slot.IsElementEnabled && slot.gameObject.activeInHierarchy;
	}

	// Token: 0x060060C9 RID: 24777 RVA: 0x001F8ED4 File Offset: 0x001F70D4
	private float GetFarthestSlotSide(ShopBrowserElement.Side side)
	{
		ShopSection.SlotCertainty slotCertainty = ShopSection.SlotCertainty.UNKNOWN;
		float num = (side == ShopBrowserElement.Side.TOP || side == ShopBrowserElement.Side.RIGHT) ? float.MinValue : float.MaxValue;
		foreach (ShopSlot shopSlot in this.m_slots)
		{
			if (shopSlot.IsFilled)
			{
				slotCertainty = ShopSection.SlotCertainty.KNOWN;
			}
			else
			{
				if (slotCertainty >= ShopSection.SlotCertainty.KNOWN)
				{
					continue;
				}
				slotCertainty = ShopSection.SlotCertainty.MAYBE;
			}
			switch (side)
			{
			case ShopBrowserElement.Side.TOP:
				num = Mathf.Max(num, shopSlot.Top);
				break;
			case ShopBrowserElement.Side.BOTTOM:
				num = Mathf.Min(num, shopSlot.Bottom);
				break;
			case ShopBrowserElement.Side.LEFT:
				num = Mathf.Min(num, shopSlot.Left);
				break;
			case ShopBrowserElement.Side.RIGHT:
				num = Mathf.Max(num, shopSlot.Right);
				break;
			}
		}
		if (slotCertainty < ShopSection.SlotCertainty.KNOWN && (side == ShopBrowserElement.Side.BOTTOM || side == ShopBrowserElement.Side.RIGHT))
		{
			return this.GetFarthestSlotSide((side == ShopBrowserElement.Side.BOTTOM) ? ShopBrowserElement.Side.TOP : ShopBrowserElement.Side.LEFT);
		}
		if (slotCertainty == ShopSection.SlotCertainty.UNKNOWN)
		{
			num = 0f;
		}
		return num;
	}

	// Token: 0x060060CA RID: 24778 RVA: 0x001F8FC4 File Offset: 0x001F71C4
	protected override void OnElementBoundsChanged()
	{
		if (this.m_scrollableItem != null)
		{
			this.m_scrollableItem.m_offset.x = base.BoundsX + base.Width / 2f;
			this.m_scrollableItem.m_offset.z = base.BoundsY + base.Height / 2f;
			this.m_scrollableItem.m_size.x = base.Width;
			this.m_scrollableItem.m_size.z = base.Height;
		}
	}

	// Token: 0x060060CB RID: 24779 RVA: 0x001F9051 File Offset: 0x001F7251
	protected override void OnElementEnabled()
	{
		if (this.m_scrollableItem != null)
		{
			if (base.IsElementEnabled)
			{
				this.m_scrollableItem.m_active = UIBScrollableItem.ActiveState.Active;
				return;
			}
			this.m_scrollableItem.m_active = UIBScrollableItem.ActiveState.Inactive;
		}
	}

	// Token: 0x060060CC RID: 24780 RVA: 0x001F9084 File Offset: 0x001F7284
	protected static void GetActiveElementsExcludeShopSlots(GameObject gameObj, List<ShopBrowserElement> elements)
	{
		if (!gameObj.activeInHierarchy)
		{
			return;
		}
		if (gameObj.GetComponent<ShopSlot>() != null)
		{
			return;
		}
		elements.AddRange(from comp in gameObj.GetComponents<ShopBrowserElement>()
		where comp.enabled
		select comp);
		foreach (object obj in gameObj.transform)
		{
			ShopSection.GetActiveElementsExcludeShopSlots(((Transform)obj).gameObject, elements);
		}
	}

	// Token: 0x040050E6 RID: 20710
	protected Widget m_widget;

	// Token: 0x040050E7 RID: 20711
	protected UIBScrollableItem m_scrollableItem;

	// Token: 0x040050E8 RID: 20712
	protected List<ShopSlot> m_slots = new List<ShopSlot>();

	// Token: 0x040050E9 RID: 20713
	protected bool m_slotsDirty;

	// Token: 0x0200220B RID: 8715
	private enum SlotCertainty
	{
		// Token: 0x0400E236 RID: 57910
		UNKNOWN,
		// Token: 0x0400E237 RID: 57911
		MAYBE,
		// Token: 0x0400E238 RID: 57912
		KNOWN
	}
}
