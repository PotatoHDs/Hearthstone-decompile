using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ShopBrowser : MonoBehaviour
{
	[SerializeField]
	protected ShopProductData[] m_testData;

	[SerializeField]
	protected ProductCatalog.TestDataMode m_testDataMode = ProductCatalog.TestDataMode.ADD_PRODUCT_TEST_DATA;

	[SerializeField]
	private bool m_dataDirty;

	[SerializeField]
	protected float m_stackingMargins;

	[SerializeField]
	private bool m_layoutDirty;

	[SerializeField]
	private bool m_loadSynchronously = true;

	[SerializeField]
	private bool m_loadSectionsSequentially = true;

	private const string c_tierWidgetPrefab = "ShopMasterTier.prefab:28b5d7137297f234ebe64c4499d41901";

	private const string TIER_POSITIONED_EVENT = "TIER_POSITIONED";

	private const string TIER_SLOTS_LOADED_EVENT = "TIER_SLOTS_LOADED";

	protected List<ShopSection> m_shopSections = new List<ShopSection>();

	protected List<WidgetInstance> m_tierInstances = new List<WidgetInstance>();

	private Widget m_widget;

	private ProductCatalog.TestDataMode? m_appliedTestData;

	private bool m_isLoading;

	private int m_framesSpentLoading;

	private DateTime m_timeStartedLoading;

	private TimeSpan m_timeSpentLoading;

	private void Start()
	{
		m_widget = GetComponent<Widget>();
		if (StoreManager.Get().Catalog.CurrentTestDataMode != 0)
		{
			m_appliedTestData = StoreManager.Get().Catalog.CurrentTestDataMode;
		}
		else
		{
			ApplyTestData();
		}
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.OnCloseCompleted += HandleShopCloseCompleted;
		}
	}

	public void RefreshContents()
	{
		m_dataDirty = true;
	}

	public void EnableInput(bool enabled)
	{
		ShopSlot[] componentsInChildren = GetComponentsInChildren<ShopSlot>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].EnableInput(enabled);
		}
	}

	public void RegisterSection(ShopSection section)
	{
		section.SuppressSelfRefresh = m_loadSectionsSequentially;
		if (!m_shopSections.Contains(section))
		{
			m_shopSections.Add(section);
		}
	}

	public bool IsReady()
	{
		if (AreLayoutsResolved())
		{
			return !m_shopSections.Any((ShopSection s) => s.IsResolvingSlotVisuals);
		}
		return false;
	}

	public bool IsLayoutDirty()
	{
		if (!m_layoutDirty)
		{
			return m_dataDirty;
		}
		return true;
	}

	public List<ShopSection> GetActiveSections()
	{
		return m_shopSections.Where((ShopSection s) => s != null && s.isActiveAndEnabled).ToList();
	}

	private void ApplyTestData()
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		if ((m_appliedTestData.HasValue ? m_appliedTestData.Value : ProductCatalog.TestDataMode.NO_TEST_DATA) != m_testDataMode)
		{
			m_appliedTestData = m_testDataMode;
			catalog.SetTestDataMode(m_testDataMode);
			if (HasTestData())
			{
				LoadTestData();
			}
			m_dataDirty = true;
		}
	}

	private void Update()
	{
		ApplyTestData();
		if (Shop.Get().IsOpen() && StoreManager.Get().Catalog.TryRefreshStaleProductAvailability())
		{
			m_dataDirty = true;
		}
		if (m_loadSectionsSequentially)
		{
			if (m_dataDirty && Shop.Get().IsOpen())
			{
				StartCoroutine(LoadSectionsSequentiallyCoroutine());
			}
		}
		else
		{
			if (m_dataDirty && Shop.Get().IsOpen())
			{
				BindData();
			}
			if (m_layoutDirty && AreLayoutsResolved())
			{
				StackSections();
			}
		}
		UpdateLoadingStats();
	}

	private void BindData()
	{
		m_dataDirty = false;
		ShopDataModel dataModel = m_widget.GetDataModel<ShopDataModel>();
		if (dataModel == null)
		{
			ResizeTierCount(0);
			return;
		}
		RecordStartLoading();
		ResizeTierCount(dataModel.Tiers.Count);
		for (int i = 0; i < m_tierInstances.Count; i++)
		{
			WidgetInstance widgetInstance = m_tierInstances[i];
			ProductTierDataModel productTierDataModel = dataModel.Tiers.ElementAtOrDefault(i);
			widgetInstance.BindDataModel(productTierDataModel ?? ProductFactory.CreateEmptyProductTier());
			if (widgetInstance.WillLoadSynchronously)
			{
				widgetInstance.Initialize();
			}
		}
		foreach (ShopSection shopSection in m_shopSections)
		{
			shopSection.ScheduleRefresh();
		}
		m_layoutDirty = true;
	}

	private void ResizeTierCount(int targetCount)
	{
		while (m_tierInstances.Count > targetCount)
		{
			WidgetInstance instance = m_tierInstances.LastOrDefault();
			m_tierInstances.Remove(instance);
			if (instance != null)
			{
				m_shopSections.RemoveAll((ShopSection s) => s == null || s.transform.IsChildOf(instance.transform));
				UnityEngine.Object.Destroy(instance.gameObject);
			}
		}
		while (m_tierInstances.Count < targetCount)
		{
			WidgetInstance widgetInstance = WidgetInstance.Create("ShopMasterTier.prefab:28b5d7137297f234ebe64c4499d41901");
			widgetInstance.SetLayerOverride((GameLayer)base.gameObject.layer);
			widgetInstance.transform.SetParent(base.transform, worldPositionStays: false);
			widgetInstance.name = $"tier {m_tierInstances.Count}";
			widgetInstance.WillLoadSynchronously = m_loadSynchronously;
			m_tierInstances.Add(widgetInstance);
		}
	}

	private void StackSections()
	{
		Log.Store.PrintDebug("Shop ready to stack sections at {0} seconds {1} frames", m_timeSpentLoading.TotalSeconds, m_framesSpentLoading);
		Widget.TriggerEventParameters triggerEventParameters = default(Widget.TriggerEventParameters);
		triggerEventParameters.IgnorePlaymaker = true;
		triggerEventParameters.NoDownwardPropagation = true;
		Widget.TriggerEventParameters parameters = triggerEventParameters;
		ShopSection shopSection = null;
		foreach (ShopSection shopSection2 in m_shopSections)
		{
			if (shopSection2 == null)
			{
				continue;
			}
			shopSection2.ResizeHeightForStacking();
			shopSection2.gameObject.SetActive(shopSection2.IsElementEnabled);
			if (shopSection2.IsElementEnabled)
			{
				if (shopSection == null)
				{
					shopSection2.Top = 0f;
				}
				else
				{
					shopSection2.Top = shopSection.Bottom - m_stackingMargins;
				}
				shopSection2.widget.TriggerEvent("TIER_POSITIONED", parameters);
				shopSection = shopSection2;
			}
		}
		m_layoutDirty = false;
		StartCoroutine(LoadSectionSlotsCoroutine());
	}

	private IEnumerator LoadSectionSlotsCoroutine()
	{
		Log.Store.PrintDebug("Shop start loading buttons on all sections at {0} seconds {1} frames", m_timeSpentLoading.TotalSeconds, m_framesSpentLoading);
		Widget.TriggerEventParameters triggerParams = new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = true
		};
		int sectionIndex = 0;
		foreach (ShopSection section in m_shopSections)
		{
			section.SuppressResolvingSlots = false;
			while (section.IsResolvingSlotVisuals)
			{
				yield return null;
				if (section == null)
				{
					Log.Store.PrintDebug("Shop ABORTED loading");
					RecordStopLoading();
					yield break;
				}
			}
			Log.Store.PrintDebug("Shop finished loading buttons on section {0} at {1} seconds {2} frames", sectionIndex, m_timeSpentLoading.TotalSeconds, m_framesSpentLoading);
			section.widget.TriggerEvent("TIER_SLOTS_LOADED", triggerParams);
			int num = sectionIndex + 1;
			sectionIndex = num;
		}
		RecordStopLoading();
	}

	private bool AreLayoutsResolved()
	{
		if (m_shopSections.Count == m_tierInstances.Count)
		{
			return !m_shopSections.Any((ShopSection s) => s.IsElementEnabled && s.IsResolvingLayout);
		}
		return false;
	}

	private bool HasTestData()
	{
		return m_testData.Length != 0;
	}

	private void LoadTestData()
	{
		ShopProductData shopProductData = ScriptableObject.CreateInstance<ShopProductData>();
		List<ShopProductData.ProductData> list = new List<ShopProductData.ProductData>();
		List<ShopProductData.ProductItemData> list2 = new List<ShopProductData.ProductItemData>();
		List<ShopProductData.ProductTierData> list3 = new List<ShopProductData.ProductTierData>();
		ShopProductData[] testData = m_testData;
		foreach (ShopProductData shopProductData2 in testData)
		{
			list.AddRange(shopProductData2.productCatalog);
			list2.AddRange(shopProductData2.productItemCatalog);
			list3.AddRange(shopProductData2.productTierCatalog);
		}
		shopProductData.productCatalog = list.ToArray();
		shopProductData.productItemCatalog = list2.ToArray();
		shopProductData.productTierCatalog = list3.ToArray();
		StoreManager.Get().Catalog.PopulateWithTestData(shopProductData);
		UnityEngine.Object.Destroy(shopProductData);
	}

	private void HandleShopCloseCompleted()
	{
		if (m_loadSectionsSequentially)
		{
			StopCoroutine(LoadSectionsSequentiallyCoroutine());
			if (m_isLoading)
			{
				RecordStopLoading();
			}
		}
		ResizeTierCount(0);
	}

	private void RecordStartLoading()
	{
		Log.Store.PrintDebug("Shop load start");
		m_isLoading = true;
		m_framesSpentLoading = 0;
		m_timeSpentLoading = default(TimeSpan);
		m_timeStartedLoading = DateTime.UtcNow;
	}

	private void RecordStopLoading()
	{
		m_isLoading = false;
		m_timeSpentLoading = DateTime.UtcNow - m_timeStartedLoading;
		Log.Store.PrintDebug("Shop load done at {0} seconds {1} frames", m_timeSpentLoading.TotalSeconds, m_framesSpentLoading);
	}

	private void UpdateLoadingStats()
	{
		if (m_isLoading)
		{
			m_timeSpentLoading = DateTime.UtcNow - m_timeStartedLoading;
			m_framesSpentLoading++;
		}
	}

	private IEnumerator LoadSectionsSequentiallyCoroutine()
	{
		RecordStartLoading();
		m_dataDirty = false;
		m_layoutDirty = false;
		WidgetInstance[] array = m_tierInstances.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		ShopDataModel shopData = m_widget.GetDataModel<ShopDataModel>();
		if (shopData == null)
		{
			Log.Store.PrintError("Failed to load sections: no shop data model");
			ResizeTierCount(0);
			RecordStopLoading();
			yield break;
		}
		ResizeTierCount(shopData.Tiers.Count);
		ShopSection previousSection = null;
		int sectionIndex = 0;
		bool aborted = false;
		WidgetInstance[] array2 = m_tierInstances.ToArray();
		foreach (WidgetInstance widgetInstance in array2)
		{
			if (widgetInstance == null)
			{
				aborted = true;
				break;
			}
			ProductTierDataModel productTierDataModel = shopData.Tiers.ElementAtOrDefault(sectionIndex) ?? ProductFactory.CreateEmptyProductTier();
			Log.Store.PrintDebug("Loading section {0} with style = {1}, header = {2} at frame = {3}, time = {4}", sectionIndex, productTierDataModel.Style, productTierDataModel.Header, m_framesSpentLoading, m_timeSpentLoading.TotalSeconds);
			ShopSection section = null;
			yield return StartCoroutine(InitSectionCoroutine(sectionIndex, widgetInstance, productTierDataModel, previousSection, delegate(ShopSection result)
			{
				section = result;
			}));
			if (section == null)
			{
				aborted = true;
				break;
			}
			bool populateSucceeded = false;
			yield return StartCoroutine(PopulateSectionCoroutine(sectionIndex, section, delegate(bool result)
			{
				populateSucceeded = result;
			}));
			if (!populateSucceeded)
			{
				aborted = true;
				break;
			}
			if (section.IsElementEnabled)
			{
				previousSection = section;
			}
			int i = sectionIndex + 1;
			sectionIndex = i;
		}
		if (aborted)
		{
			Log.Store.PrintDebug("Aborted loading sections: ShopSection destroyed");
		}
		RecordStopLoading();
	}

	private IEnumerator InitSectionCoroutine(int sectionIndex, WidgetInstance inst, ProductTierDataModel tierData, ShopSection previousSection, Action<ShopSection> onComplete)
	{
		Log.Store.PrintDebug("Loading section {0} with style = {1}, header = {2} at frame = {3}, time = {4}", sectionIndex, tierData.Style, tierData.Header, m_framesSpentLoading, m_timeSpentLoading.TotalSeconds);
		inst.BindDataModel(tierData);
		inst.gameObject.SetActive(value: true);
		if (inst.WillLoadSynchronously)
		{
			inst.Initialize();
		}
		while (!inst.IsReady)
		{
			yield return null;
			if (inst == null)
			{
				onComplete(null);
				yield break;
			}
		}
		if (inst.Widget == null)
		{
			Log.Store.PrintError("Aborted loading sections: tier instance failed to load template");
			onComplete(null);
			yield break;
		}
		ShopSection section = inst.Widget.GetComponent<ShopSection>();
		if (section == null)
		{
			Log.Store.PrintError("Aborted loading sections: tier widget has no ShopSection component");
			onComplete(null);
			yield break;
		}
		section.gameObject.SetActive(value: true);
		while (inst.Widget.GetIsChangingStates((GameObject go) => go.GetComponent<ShopSlot>() == null))
		{
			yield return null;
			if (inst == null)
			{
				onComplete(null);
				yield break;
			}
		}
		section.ResizeHeightForStacking();
		if (previousSection == null)
		{
			section.Top = 0f;
		}
		else
		{
			section.Top = previousSection.Bottom - m_stackingMargins;
		}
		inst.Widget.TriggerEvent("TIER_POSITIONED", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = true
		});
		Log.Store.PrintDebug("Finished loading section {0} style at frame = {1}, time = {2}", sectionIndex, m_framesSpentLoading, m_timeSpentLoading.TotalSeconds);
		onComplete(section);
	}

	private IEnumerator PopulateSectionCoroutine(int sectionIndex, ShopSection section, Action<bool> onComplete)
	{
		section.BindDataModelsToSlots();
		if (!section.IsElementEnabled)
		{
			Log.Store.PrintDebug("Section {0} disabled itself", sectionIndex);
			section.gameObject.SetActive(value: false);
			onComplete(obj: true);
			yield break;
		}
		while (section.IsResolvingSlotVisuals)
		{
			yield return null;
			if (section == null)
			{
				onComplete(obj: false);
				yield break;
			}
		}
		section.widget.TriggerEvent("TIER_SLOTS_LOADED", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = true
		});
		Log.Store.PrintDebug("Finished loading section {0} contents at frame = {1}, time = {2}", sectionIndex, m_framesSpentLoading, m_timeSpentLoading.TotalSeconds);
		onComplete(obj: true);
	}
}
