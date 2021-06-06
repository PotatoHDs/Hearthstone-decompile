using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ShopSection : ShopBrowserElement
{
	private enum SlotCertainty
	{
		UNKNOWN,
		MAYBE,
		KNOWN
	}

	protected Widget m_widget;

	protected UIBScrollableItem m_scrollableItem;

	protected List<ShopSlot> m_slots = new List<ShopSlot>();

	protected bool m_slotsDirty;

	public bool IsResolvingLayout { get; private set; }

	public bool IsResolvingSlotVisuals { get; private set; }

	public bool SuppressResolvingSlots { get; set; }

	public bool SuppressSelfRefresh { get; set; }

	public Widget widget => m_widget;

	private void Start()
	{
		m_widget = GetComponent<Widget>();
		m_scrollableItem = GetComponentInChildren<UIBScrollableItem>();
		ShopBrowser shopBrowser = SceneUtils.FindComponentInParents<ShopBrowser>(base.gameObject);
		if (shopBrowser != null)
		{
			shopBrowser.RegisterSection(this);
		}
		if (!SuppressSelfRefresh)
		{
			ProductTierDataModel tierDataModel = GetTierDataModel();
			if (tierDataModel != null && tierDataModel.BrowserButtons.Count > 0)
			{
				ScheduleRefresh();
			}
		}
	}

	private void OnEnable()
	{
		if (m_slotsDirty && !SuppressSelfRefresh)
		{
			StartCoroutine(RefreshSlotsWhenReady());
		}
	}

	private void Update()
	{
		if (!SuppressSelfRefresh)
		{
			CheckDoneResolvingLayout();
		}
	}

	public void ScheduleRefresh()
	{
		if (!m_slotsDirty)
		{
			m_slotsDirty = true;
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(RefreshSlotsWhenReady());
			}
		}
	}

	public void RegisterSlot(ShopSlot slot)
	{
		if (!m_slots.Contains(slot))
		{
			m_slots.Add(slot);
		}
		if (!SuppressSelfRefresh)
		{
			ScheduleRefresh();
		}
	}

	public void ResizeHeightForStacking()
	{
		List<ShopBrowserElement> list = new List<ShopBrowserElement>();
		foreach (Transform item in base.transform)
		{
			GetActiveElementsExcludeShopSlots(item.gameObject, list);
		}
		if (list.Count <= 0)
		{
			return;
		}
		float num = float.MaxValue;
		float num2 = float.MinValue;
		foreach (ShopBrowserElement item2 in list)
		{
			num = Mathf.Min(num, item2.Bounds.yMin);
			num2 = Mathf.Max(num2, item2.Bounds.yMax);
		}
		Bounds.yMin = num;
		Bounds.yMax = num2;
		OnElementBoundsChanged();
	}

	public ProductTierDataModel GetTierDataModel()
	{
		return m_widget.GetDataModel<ProductTierDataModel>();
	}

	public List<ShopSlot> GetSortedEnabledSlots()
	{
		List<ShopSlot> list = m_slots.Where((ShopSlot s) => IsSlotEnabled(s)).ToList();
		list.Sort((ShopSlot A, ShopSlot B) => ShopBrowserElement.ComparePosition(A, B));
		return list;
	}

	public void BindDataModelsToSlots()
	{
		m_slotsDirty = false;
		IsResolvingSlotVisuals = true;
		ProductTierDataModel tierDataModel = GetTierDataModel();
		if (tierDataModel == null || tierDataModel.BrowserButtons == null)
		{
			base.IsElementEnabled = false;
			IsResolvingSlotVisuals = false;
			return;
		}
		DataModelList<ShopBrowserButtonDataModel> browserButtons = tierDataModel.BrowserButtons;
		List<ShopSlot> sortedEnabledSlots = GetSortedEnabledSlots();
		bool flag = false;
		int i = 0;
		for (int num = Math.Min(sortedEnabledSlots.Count, browserButtons.Count); i < num; i++)
		{
			ShopBrowserButtonDataModel shopBrowserButtonDataModel = browserButtons.ElementAt(i);
			if (!shopBrowserButtonDataModel.IsFiller && shopBrowserButtonDataModel.DisplayProduct.Availability == ProductAvailability.CAN_PURCHASE)
			{
				flag = true;
			}
		}
		bool flag2 = StoreManager.Get().Catalog.CurrentTestDataMode == ProductCatalog.TestDataMode.TIER_TEST_DATA;
		base.IsElementEnabled = flag || (flag2 && browserButtons.Count > 0);
		if (!base.IsElementEnabled)
		{
			IsResolvingSlotVisuals = false;
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
		m_widget.RegisterDoneChangingStatesListener(delegate
		{
			IsResolvingSlotVisuals = false;
		}, null, callImmediatelyIfSet: true, doOnce: true);
	}

	private IEnumerator RefreshSlotsWhenReady()
	{
		Log.Store.PrintDebug("{0} resolving layout...", base.gameObject.name);
		IsResolvingLayout = true;
		while (!CheckDoneResolvingLayout())
		{
			yield return null;
		}
		Log.Store.PrintDebug("{0} layout resolved", base.gameObject.name);
		if (!m_slotsDirty)
		{
			yield break;
		}
		if (SuppressResolvingSlots)
		{
			Log.Store.PrintDebug("{0} suppressing slot visuals...", base.gameObject.name);
			IsResolvingSlotVisuals = true;
			m_slots.ForEach(delegate(ShopSlot s)
			{
				s.Reset();
			});
			while (SuppressResolvingSlots)
			{
				yield return null;
			}
		}
		BindDataModelsToSlots();
	}

	private bool CheckDoneResolvingLayout()
	{
		if (IsResolvingLayout && !m_widget.GetIsChangingStates((GameObject go) => go.GetComponent<ShopSlot>() == null))
		{
			Log.Store.PrintDebug("ShopSection done resolving layout");
			IsResolvingLayout = false;
		}
		return !IsResolvingLayout;
	}

	private bool IsSlotEnabled(ShopSlot slot)
	{
		if (!slot.IsElementEnabled)
		{
			return false;
		}
		return slot.gameObject.activeInHierarchy;
	}

	private float GetFarthestSlotSide(Side side)
	{
		SlotCertainty slotCertainty = SlotCertainty.UNKNOWN;
		float num = ((side == Side.TOP || side == Side.RIGHT) ? float.MinValue : float.MaxValue);
		foreach (ShopSlot slot in m_slots)
		{
			if (slot.IsFilled)
			{
				slotCertainty = SlotCertainty.KNOWN;
			}
			else
			{
				if (slotCertainty >= SlotCertainty.KNOWN)
				{
					continue;
				}
				slotCertainty = SlotCertainty.MAYBE;
			}
			switch (side)
			{
			case Side.TOP:
				num = Mathf.Max(num, slot.Top);
				break;
			case Side.BOTTOM:
				num = Mathf.Min(num, slot.Bottom);
				break;
			case Side.LEFT:
				num = Mathf.Min(num, slot.Left);
				break;
			case Side.RIGHT:
				num = Mathf.Max(num, slot.Right);
				break;
			}
		}
		if (slotCertainty < SlotCertainty.KNOWN && (side == Side.BOTTOM || side == Side.RIGHT))
		{
			return GetFarthestSlotSide((side != Side.BOTTOM) ? Side.LEFT : Side.TOP);
		}
		if (slotCertainty == SlotCertainty.UNKNOWN)
		{
			num = 0f;
		}
		return num;
	}

	protected override void OnElementBoundsChanged()
	{
		if (m_scrollableItem != null)
		{
			m_scrollableItem.m_offset.x = base.BoundsX + base.Width / 2f;
			m_scrollableItem.m_offset.z = base.BoundsY + base.Height / 2f;
			m_scrollableItem.m_size.x = base.Width;
			m_scrollableItem.m_size.z = base.Height;
		}
	}

	protected override void OnElementEnabled()
	{
		if (m_scrollableItem != null)
		{
			if (base.IsElementEnabled)
			{
				m_scrollableItem.m_active = UIBScrollableItem.ActiveState.Active;
			}
			else
			{
				m_scrollableItem.m_active = UIBScrollableItem.ActiveState.Inactive;
			}
		}
	}

	protected static void GetActiveElementsExcludeShopSlots(GameObject gameObj, List<ShopBrowserElement> elements)
	{
		if (!gameObj.activeInHierarchy || gameObj.GetComponent<ShopSlot>() != null)
		{
			return;
		}
		elements.AddRange(from comp in gameObj.GetComponents<ShopBrowserElement>()
			where comp.enabled
			select comp);
		foreach (Transform item in gameObj.transform)
		{
			GetActiveElementsExcludeShopSlots(item.gameObject, elements);
		}
	}
}
