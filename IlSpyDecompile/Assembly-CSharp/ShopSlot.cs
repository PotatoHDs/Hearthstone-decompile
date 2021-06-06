using System;
using System.Collections;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ShopSlot : ShopBrowserElement
{
	public enum SlotSize
	{
		Custom,
		M,
		MWide,
		L,
		XL,
		XXL
	}

	[SerializeField]
	private SlotSize m_slotSize;

	protected BoxCollider m_boxCollider;

	protected Widget m_widget;

	protected ShopSection m_section;

	protected bool m_isFilled;

	protected bool m_inputBlocked;

	private ShopCard m_shopCardTelemetry;

	[Overridable]
	public string Size
	{
		get
		{
			return m_slotSize.ToString();
		}
		set
		{
			m_slotSize = GetSlotSizeFromString(value);
			UpdateSize();
		}
	}

	public bool IsFilled => m_isFilled;

	protected ShopBrowserButtonDataModel DataModel
	{
		get
		{
			IDataModel model = null;
			if (m_widget != null)
			{
				m_widget.GetDataModel(19, out model);
			}
			return model as ShopBrowserButtonDataModel;
		}
		set
		{
			if (m_widget != null)
			{
				value.DisplayProduct = value.DisplayProduct ?? ProductFactory.CreateEmptyProductDataModel();
				m_widget.BindDataModel(value);
				UpdateShopCardTelemetry();
			}
		}
	}

	private void Start()
	{
		m_widget = GetComponent<Widget>();
		m_shopCardTelemetry = new ShopCard();
		m_isFilled = false;
		if (m_boxCollider == null)
		{
			m_boxCollider = GetComponent<BoxCollider>();
			Clickable component = GetComponent<Clickable>();
			if ((bool)component)
			{
				component.AddEventListener(UIEventType.RELEASE, OnRelease);
				component.AddEventListener(UIEventType.ROLLOVER, OnRollOver);
				component.AddEventListener(UIEventType.ROLLOUT, OnRollOut);
			}
		}
		RefreshEnableInput();
		UpdateSize();
		DataModel = new ShopBrowserButtonDataModel
		{
			SlotWidth = base.Width,
			SlotHeight = base.Height
		};
		m_section = SceneUtils.FindComponentInParents<ShopSection>(base.gameObject);
		if (m_section != null)
		{
			m_section.RegisterSlot(this);
		}
	}

	public void Reset()
	{
		if (m_isFilled)
		{
			SetBrowserButton(new ShopBrowserButtonDataModel
			{
				SlotWidth = base.Width,
				SlotHeight = base.Height
			});
		}
	}

	public static Vector2 GetSlotSizeDims(SlotSize size)
	{
		return size switch
		{
			SlotSize.M => new Vector2(1f, 1.3f), 
			SlotSize.MWide => new Vector2(2f, 1.3f), 
			SlotSize.L => new Vector2(2f, 2f), 
			SlotSize.XL => new Vector2(3f, 2f), 
			SlotSize.XXL => new Vector2(4f, 2f), 
			_ => new Vector2(0f, 0f), 
		};
	}

	public static SlotSize GetSlotSizeFromString(string size)
	{
		return size.ToUpper() switch
		{
			"M" => SlotSize.M, 
			"MWIDE" => SlotSize.MWide, 
			"L" => SlotSize.L, 
			"XL" => SlotSize.XL, 
			"XXL" => SlotSize.XXL, 
			_ => SlotSize.Custom, 
		};
	}

	public ShopCard GetShopCardTelemetry()
	{
		UpdateShopCardTelemetryTimeRemaining();
		return m_shopCardTelemetry;
	}

	public void SetBrowserButton(ShopBrowserButtonDataModel buttonDataModel)
	{
		DataModel = buttonDataModel;
		ProductDataModel productDataModel = buttonDataModel.DisplayProduct ?? ProductFactory.CreateEmptyProductDataModel();
		m_isFilled = productDataModel != ProductFactory.CreateEmptyProductDataModel();
		RefreshEnableInput();
		if (m_widget != null)
		{
			m_widget.BindDataModel(productDataModel);
		}
		StartCoroutine(DisableChildCollidersCoroutine());
		UpdateSize();
	}

	public void EnableInput(bool enabled)
	{
		m_inputBlocked = !enabled;
		RefreshEnableInput();
	}

	protected void RefreshEnableInput()
	{
		if (!(m_boxCollider == null))
		{
			m_boxCollider.enabled = m_isFilled && !m_inputBlocked;
		}
	}

	private void UpdateSize()
	{
		if (m_slotSize != 0)
		{
			Vector2 slotSizeDims = GetSlotSizeDims(m_slotSize);
			Bounds.Set((0f - slotSizeDims.x) / 2f, (0f - slotSizeDims.y) / 2f, slotSizeDims.x, slotSizeDims.y);
		}
		OnElementBoundsChanged();
	}

	protected override void OnElementBoundsChanged()
	{
		ShopBrowserButtonDataModel dataModel = DataModel;
		if (dataModel != null)
		{
			dataModel.SlotWidth = base.Width;
			dataModel.SlotHeight = base.Height;
		}
		if (m_boxCollider != null)
		{
			m_boxCollider.transform.localPosition = new Vector3(Bounds.center.x, m_boxCollider.transform.localPosition.y, Bounds.center.y);
			m_boxCollider.size = new Vector3(base.Width, m_boxCollider.size.y, base.Height);
		}
	}

	private void OnRelease(UIEvent e)
	{
		if (!m_isFilled)
		{
			Log.Store.PrintWarning("Ignoring click on shop slot that is not filled. The clickable for this ShopSlot should be disabled.");
			return;
		}
		TelemetryManager.Client().SendShopCardClick(m_shopCardTelemetry);
		Shop.Get().OpenProductPage(DataModel.DisplayProduct);
	}

	private void OnRollOver(UIEvent e)
	{
		if (DataModel != null)
		{
			DataModel.Hovered = true;
		}
	}

	private void OnRollOut(UIEvent e)
	{
		if (DataModel != null)
		{
			DataModel.Hovered = false;
		}
	}

	private IEnumerator DisableChildCollidersCoroutine()
	{
		while (m_widget.IsChangingStates)
		{
			yield return null;
		}
		Collider[] componentsInChildren = GetComponentsInChildren<Collider>();
		foreach (Collider collider in componentsInChildren)
		{
			if (!(collider == m_boxCollider))
			{
				collider.enabled = false;
			}
		}
	}

	private void UpdateShopCardTelemetry()
	{
		if (m_section == null)
		{
			return;
		}
		m_shopCardTelemetry = new ShopCard();
		ProductCatalog catalog = StoreManager.Get().Catalog;
		ProductTierDataModel tierDataModel = m_section.GetTierDataModel();
		if (tierDataModel != null)
		{
			m_shopCardTelemetry.SectionIndex = catalog.Tiers.IndexOf(tierDataModel);
			Network.ShopSection networkSection = catalog.GetNetworkSection(tierDataModel);
			if (networkSection != null)
			{
				m_shopCardTelemetry.SectionName = networkSection.InternalName;
			}
		}
		m_shopCardTelemetry.SlotIndex = m_section.GetSortedEnabledSlots().IndexOf(this);
		if (DataModel.DisplayProduct != ProductFactory.CreateEmptyProductDataModel())
		{
			m_shopCardTelemetry.Product = new Product
			{
				ProductId = DataModel.DisplayProduct.PmtId
			};
		}
		UpdateShopCardTelemetryTimeRemaining();
	}

	private void UpdateShopCardTelemetryTimeRemaining()
	{
		StoreManager storeManager = StoreManager.Get();
		Network.Bundle bundleFromPmtProductId = storeManager.GetBundleFromPmtProductId(DataModel.DisplayProduct.PmtId);
		if (bundleFromPmtProductId == null)
		{
			return;
		}
		ProductAvailabilityRange bundleAvailabilityRange = storeManager.GetBundleAvailabilityRange(bundleFromPmtProductId);
		if (bundleAvailabilityRange != null)
		{
			DateTime? endDateTime = bundleAvailabilityRange.EndDateTime;
			if (endDateTime.HasValue)
			{
				DateTime utcNow = DateTime.UtcNow;
				m_shopCardTelemetry.SecondsRemaining = (int)Math.Min((endDateTime.Value - utcNow).TotalSeconds, 2147483647.0);
			}
		}
	}
}
