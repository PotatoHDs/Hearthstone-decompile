using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006B6 RID: 1718
public class ShopBrowser : MonoBehaviour
{
	// Token: 0x06006052 RID: 24658 RVA: 0x001F667C File Offset: 0x001F487C
	private void Start()
	{
		this.m_widget = base.GetComponent<Widget>();
		if (StoreManager.Get().Catalog.CurrentTestDataMode != ProductCatalog.TestDataMode.NO_TEST_DATA)
		{
			this.m_appliedTestData = new ProductCatalog.TestDataMode?(StoreManager.Get().Catalog.CurrentTestDataMode);
		}
		else
		{
			this.ApplyTestData();
		}
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.OnCloseCompleted += this.HandleShopCloseCompleted;
		}
	}

	// Token: 0x06006053 RID: 24659 RVA: 0x001F66E9 File Offset: 0x001F48E9
	public void RefreshContents()
	{
		this.m_dataDirty = true;
	}

	// Token: 0x06006054 RID: 24660 RVA: 0x001F66F4 File Offset: 0x001F48F4
	public void EnableInput(bool enabled)
	{
		ShopSlot[] componentsInChildren = base.GetComponentsInChildren<ShopSlot>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].EnableInput(enabled);
		}
	}

	// Token: 0x06006055 RID: 24661 RVA: 0x001F671F File Offset: 0x001F491F
	public void RegisterSection(ShopSection section)
	{
		section.SuppressSelfRefresh = this.m_loadSectionsSequentially;
		if (!this.m_shopSections.Contains(section))
		{
			this.m_shopSections.Add(section);
		}
	}

	// Token: 0x06006056 RID: 24662 RVA: 0x001F6747 File Offset: 0x001F4947
	public bool IsReady()
	{
		if (this.AreLayoutsResolved())
		{
			return !this.m_shopSections.Any((ShopSection s) => s.IsResolvingSlotVisuals);
		}
		return false;
	}

	// Token: 0x06006057 RID: 24663 RVA: 0x001F6780 File Offset: 0x001F4980
	public bool IsLayoutDirty()
	{
		return this.m_layoutDirty || this.m_dataDirty;
	}

	// Token: 0x06006058 RID: 24664 RVA: 0x001F6792 File Offset: 0x001F4992
	public List<ShopSection> GetActiveSections()
	{
		return (from s in this.m_shopSections
		where s != null && s.isActiveAndEnabled
		select s).ToList<ShopSection>();
	}

	// Token: 0x06006059 RID: 24665 RVA: 0x001F67C4 File Offset: 0x001F49C4
	private void ApplyTestData()
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		if (((this.m_appliedTestData != null) ? this.m_appliedTestData.Value : ProductCatalog.TestDataMode.NO_TEST_DATA) != this.m_testDataMode)
		{
			this.m_appliedTestData = new ProductCatalog.TestDataMode?(this.m_testDataMode);
			catalog.SetTestDataMode(this.m_testDataMode);
			if (this.HasTestData())
			{
				this.LoadTestData();
			}
			this.m_dataDirty = true;
		}
	}

	// Token: 0x0600605A RID: 24666 RVA: 0x001F6834 File Offset: 0x001F4A34
	private void Update()
	{
		this.ApplyTestData();
		if (Shop.Get().IsOpen() && StoreManager.Get().Catalog.TryRefreshStaleProductAvailability())
		{
			this.m_dataDirty = true;
		}
		if (this.m_loadSectionsSequentially)
		{
			if (this.m_dataDirty && Shop.Get().IsOpen())
			{
				base.StartCoroutine(this.LoadSectionsSequentiallyCoroutine());
			}
		}
		else
		{
			if (this.m_dataDirty && Shop.Get().IsOpen())
			{
				this.BindData();
			}
			if (this.m_layoutDirty && this.AreLayoutsResolved())
			{
				this.StackSections();
			}
		}
		this.UpdateLoadingStats();
	}

	// Token: 0x0600605B RID: 24667 RVA: 0x001F68CC File Offset: 0x001F4ACC
	private void BindData()
	{
		this.m_dataDirty = false;
		ShopDataModel dataModel = this.m_widget.GetDataModel<ShopDataModel>();
		if (dataModel == null)
		{
			this.ResizeTierCount(0);
			return;
		}
		this.RecordStartLoading();
		this.ResizeTierCount(dataModel.Tiers.Count);
		for (int i = 0; i < this.m_tierInstances.Count; i++)
		{
			WidgetInstance widgetInstance = this.m_tierInstances[i];
			ProductTierDataModel productTierDataModel = dataModel.Tiers.ElementAtOrDefault(i);
			widgetInstance.BindDataModel(productTierDataModel ?? ProductFactory.CreateEmptyProductTier(), false);
			if (widgetInstance.WillLoadSynchronously)
			{
				widgetInstance.Initialize();
			}
		}
		foreach (ShopSection shopSection in this.m_shopSections)
		{
			shopSection.ScheduleRefresh();
		}
		this.m_layoutDirty = true;
	}

	// Token: 0x0600605C RID: 24668 RVA: 0x001F69A8 File Offset: 0x001F4BA8
	private void ResizeTierCount(int targetCount)
	{
		while (this.m_tierInstances.Count > targetCount)
		{
			WidgetInstance instance = this.m_tierInstances.LastOrDefault<WidgetInstance>();
			this.m_tierInstances.Remove(instance);
			if (instance != null)
			{
				this.m_shopSections.RemoveAll((ShopSection s) => s == null || s.transform.IsChildOf(instance.transform));
				UnityEngine.Object.Destroy(instance.gameObject);
			}
		}
		while (this.m_tierInstances.Count < targetCount)
		{
			WidgetInstance widgetInstance = WidgetInstance.Create("ShopMasterTier.prefab:28b5d7137297f234ebe64c4499d41901", false);
			widgetInstance.SetLayerOverride((GameLayer)base.gameObject.layer);
			widgetInstance.transform.SetParent(base.transform, false);
			widgetInstance.name = string.Format("tier {0}", this.m_tierInstances.Count);
			widgetInstance.WillLoadSynchronously = this.m_loadSynchronously;
			this.m_tierInstances.Add(widgetInstance);
		}
	}

	// Token: 0x0600605D RID: 24669 RVA: 0x001F6A9C File Offset: 0x001F4C9C
	private void StackSections()
	{
		Log.Store.PrintDebug("Shop ready to stack sections at {0} seconds {1} frames", new object[]
		{
			this.m_timeSpentLoading.TotalSeconds,
			this.m_framesSpentLoading
		});
		Widget.TriggerEventParameters parameters = new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = true
		};
		ShopSection shopSection = null;
		foreach (ShopSection shopSection2 in this.m_shopSections)
		{
			if (!(shopSection2 == null))
			{
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
						shopSection2.Top = shopSection.Bottom - this.m_stackingMargins;
					}
					shopSection2.widget.TriggerEvent("TIER_POSITIONED", parameters);
					shopSection = shopSection2;
				}
			}
		}
		this.m_layoutDirty = false;
		base.StartCoroutine(this.LoadSectionSlotsCoroutine());
	}

	// Token: 0x0600605E RID: 24670 RVA: 0x001F6BC0 File Offset: 0x001F4DC0
	private IEnumerator LoadSectionSlotsCoroutine()
	{
		Log.Store.PrintDebug("Shop start loading buttons on all sections at {0} seconds {1} frames", new object[]
		{
			this.m_timeSpentLoading.TotalSeconds,
			this.m_framesSpentLoading
		});
		Widget.TriggerEventParameters triggerParams = new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = true
		};
		int sectionIndex = 0;
		foreach (ShopSection section in this.m_shopSections)
		{
			section.SuppressResolvingSlots = false;
			while (section.IsResolvingSlotVisuals)
			{
				yield return null;
				if (section == null)
				{
					Log.Store.PrintDebug("Shop ABORTED loading", Array.Empty<object>());
					this.RecordStopLoading();
					yield break;
				}
			}
			Log.Store.PrintDebug("Shop finished loading buttons on section {0} at {1} seconds {2} frames", new object[]
			{
				sectionIndex,
				this.m_timeSpentLoading.TotalSeconds,
				this.m_framesSpentLoading
			});
			section.widget.TriggerEvent("TIER_SLOTS_LOADED", triggerParams);
			int num = sectionIndex + 1;
			sectionIndex = num;
			section = null;
		}
		List<ShopSection>.Enumerator enumerator = default(List<ShopSection>.Enumerator);
		this.RecordStopLoading();
		yield break;
		yield break;
	}

	// Token: 0x0600605F RID: 24671 RVA: 0x001F6BD0 File Offset: 0x001F4DD0
	private bool AreLayoutsResolved()
	{
		if (this.m_shopSections.Count == this.m_tierInstances.Count)
		{
			return !this.m_shopSections.Any((ShopSection s) => s.IsElementEnabled && s.IsResolvingLayout);
		}
		return false;
	}

	// Token: 0x06006060 RID: 24672 RVA: 0x001F6C24 File Offset: 0x001F4E24
	private bool HasTestData()
	{
		return this.m_testData.Length != 0;
	}

	// Token: 0x06006061 RID: 24673 RVA: 0x001F6C30 File Offset: 0x001F4E30
	private void LoadTestData()
	{
		ShopProductData shopProductData = ScriptableObject.CreateInstance<ShopProductData>();
		List<ShopProductData.ProductData> list = new List<ShopProductData.ProductData>();
		List<ShopProductData.ProductItemData> list2 = new List<ShopProductData.ProductItemData>();
		List<ShopProductData.ProductTierData> list3 = new List<ShopProductData.ProductTierData>();
		foreach (ShopProductData shopProductData2 in this.m_testData)
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

	// Token: 0x06006062 RID: 24674 RVA: 0x001F6CD8 File Offset: 0x001F4ED8
	private void HandleShopCloseCompleted()
	{
		if (this.m_loadSectionsSequentially)
		{
			base.StopCoroutine(this.LoadSectionsSequentiallyCoroutine());
			if (this.m_isLoading)
			{
				this.RecordStopLoading();
			}
		}
		this.ResizeTierCount(0);
	}

	// Token: 0x06006063 RID: 24675 RVA: 0x001F6D03 File Offset: 0x001F4F03
	private void RecordStartLoading()
	{
		Log.Store.PrintDebug("Shop load start", Array.Empty<object>());
		this.m_isLoading = true;
		this.m_framesSpentLoading = 0;
		this.m_timeSpentLoading = default(TimeSpan);
		this.m_timeStartedLoading = DateTime.UtcNow;
	}

	// Token: 0x06006064 RID: 24676 RVA: 0x001F6D40 File Offset: 0x001F4F40
	private void RecordStopLoading()
	{
		this.m_isLoading = false;
		this.m_timeSpentLoading = DateTime.UtcNow - this.m_timeStartedLoading;
		Log.Store.PrintDebug("Shop load done at {0} seconds {1} frames", new object[]
		{
			this.m_timeSpentLoading.TotalSeconds,
			this.m_framesSpentLoading
		});
	}

	// Token: 0x06006065 RID: 24677 RVA: 0x001F6DA0 File Offset: 0x001F4FA0
	private void UpdateLoadingStats()
	{
		if (this.m_isLoading)
		{
			this.m_timeSpentLoading = DateTime.UtcNow - this.m_timeStartedLoading;
			this.m_framesSpentLoading++;
		}
	}

	// Token: 0x06006066 RID: 24678 RVA: 0x001F6DCE File Offset: 0x001F4FCE
	private IEnumerator LoadSectionsSequentiallyCoroutine()
	{
		this.RecordStartLoading();
		this.m_dataDirty = false;
		this.m_layoutDirty = false;
		WidgetInstance[] array = this.m_tierInstances.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(false);
		}
		ShopDataModel shopData = this.m_widget.GetDataModel<ShopDataModel>();
		if (shopData == null)
		{
			Log.Store.PrintError("Failed to load sections: no shop data model", Array.Empty<object>());
			this.ResizeTierCount(0);
			this.RecordStopLoading();
			yield break;
		}
		this.ResizeTierCount(shopData.Tiers.Count);
		ShopSection previousSection = null;
		int sectionIndex = 0;
		bool aborted = false;
		foreach (WidgetInstance widgetInstance in this.m_tierInstances.ToArray())
		{
			ShopBrowser.<>c__DisplayClass38_0 CS$<>8__locals1 = new ShopBrowser.<>c__DisplayClass38_0();
			if (widgetInstance == null)
			{
				aborted = true;
				break;
			}
			ProductTierDataModel productTierDataModel = shopData.Tiers.ElementAtOrDefault(sectionIndex) ?? ProductFactory.CreateEmptyProductTier();
			Log.Store.PrintDebug("Loading section {0} with style = {1}, header = {2} at frame = {3}, time = {4}", new object[]
			{
				sectionIndex,
				productTierDataModel.Style,
				productTierDataModel.Header,
				this.m_framesSpentLoading,
				this.m_timeSpentLoading.TotalSeconds
			});
			CS$<>8__locals1.section = null;
			yield return base.StartCoroutine(this.InitSectionCoroutine(sectionIndex, widgetInstance, productTierDataModel, previousSection, delegate(ShopSection result)
			{
				CS$<>8__locals1.section = result;
			}));
			if (CS$<>8__locals1.section == null)
			{
				aborted = true;
				break;
			}
			CS$<>8__locals1.populateSucceeded = false;
			yield return base.StartCoroutine(this.PopulateSectionCoroutine(sectionIndex, CS$<>8__locals1.section, delegate(bool result)
			{
				CS$<>8__locals1.populateSucceeded = result;
			}));
			if (!CS$<>8__locals1.populateSucceeded)
			{
				aborted = true;
				break;
			}
			if (CS$<>8__locals1.section.IsElementEnabled)
			{
				previousSection = CS$<>8__locals1.section;
			}
			int i = sectionIndex + 1;
			sectionIndex = i;
			CS$<>8__locals1 = null;
		}
		WidgetInstance[] array2 = null;
		if (aborted)
		{
			Log.Store.PrintDebug("Aborted loading sections: ShopSection destroyed", Array.Empty<object>());
		}
		this.RecordStopLoading();
		yield break;
	}

	// Token: 0x06006067 RID: 24679 RVA: 0x001F6DDD File Offset: 0x001F4FDD
	private IEnumerator InitSectionCoroutine(int sectionIndex, WidgetInstance inst, ProductTierDataModel tierData, ShopSection previousSection, Action<ShopSection> onComplete)
	{
		Log.Store.PrintDebug("Loading section {0} with style = {1}, header = {2} at frame = {3}, time = {4}", new object[]
		{
			sectionIndex,
			tierData.Style,
			tierData.Header,
			this.m_framesSpentLoading,
			this.m_timeSpentLoading.TotalSeconds
		});
		inst.BindDataModel(tierData, false);
		inst.gameObject.SetActive(true);
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
			Log.Store.PrintError("Aborted loading sections: tier instance failed to load template", Array.Empty<object>());
			onComplete(null);
			yield break;
		}
		ShopSection section = inst.Widget.GetComponent<ShopSection>();
		if (section == null)
		{
			Log.Store.PrintError("Aborted loading sections: tier widget has no ShopSection component", Array.Empty<object>());
			onComplete(null);
			yield break;
		}
		section.gameObject.SetActive(true);
		do
		{
			if (!inst.Widget.GetIsChangingStates((GameObject go) => go.GetComponent<ShopSlot>() == null))
			{
				goto Block_8;
			}
			yield return null;
		}
		while (!(inst == null));
		onComplete(null);
		yield break;
		Block_8:
		section.ResizeHeightForStacking();
		if (previousSection == null)
		{
			section.Top = 0f;
		}
		else
		{
			section.Top = previousSection.Bottom - this.m_stackingMargins;
		}
		inst.Widget.TriggerEvent("TIER_POSITIONED", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = true
		});
		Log.Store.PrintDebug("Finished loading section {0} style at frame = {1}, time = {2}", new object[]
		{
			sectionIndex,
			this.m_framesSpentLoading,
			this.m_timeSpentLoading.TotalSeconds
		});
		onComplete(section);
		yield break;
	}

	// Token: 0x06006068 RID: 24680 RVA: 0x001F6E11 File Offset: 0x001F5011
	private IEnumerator PopulateSectionCoroutine(int sectionIndex, ShopSection section, Action<bool> onComplete)
	{
		section.BindDataModelsToSlots();
		if (!section.IsElementEnabled)
		{
			Log.Store.PrintDebug("Section {0} disabled itself", new object[]
			{
				sectionIndex
			});
			section.gameObject.SetActive(false);
			onComplete(true);
			yield break;
		}
		while (section.IsResolvingSlotVisuals)
		{
			yield return null;
			if (section == null)
			{
				onComplete(false);
				yield break;
			}
		}
		section.widget.TriggerEvent("TIER_SLOTS_LOADED", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = true
		});
		Log.Store.PrintDebug("Finished loading section {0} contents at frame = {1}, time = {2}", new object[]
		{
			sectionIndex,
			this.m_framesSpentLoading,
			this.m_timeSpentLoading.TotalSeconds
		});
		onComplete(true);
		yield break;
	}

	// Token: 0x040050AD RID: 20653
	[SerializeField]
	protected ShopProductData[] m_testData;

	// Token: 0x040050AE RID: 20654
	[SerializeField]
	protected ProductCatalog.TestDataMode m_testDataMode = ProductCatalog.TestDataMode.ADD_PRODUCT_TEST_DATA;

	// Token: 0x040050AF RID: 20655
	[SerializeField]
	private bool m_dataDirty;

	// Token: 0x040050B0 RID: 20656
	[SerializeField]
	protected float m_stackingMargins;

	// Token: 0x040050B1 RID: 20657
	[SerializeField]
	private bool m_layoutDirty;

	// Token: 0x040050B2 RID: 20658
	[SerializeField]
	private bool m_loadSynchronously = true;

	// Token: 0x040050B3 RID: 20659
	[SerializeField]
	private bool m_loadSectionsSequentially = true;

	// Token: 0x040050B4 RID: 20660
	private const string c_tierWidgetPrefab = "ShopMasterTier.prefab:28b5d7137297f234ebe64c4499d41901";

	// Token: 0x040050B5 RID: 20661
	private const string TIER_POSITIONED_EVENT = "TIER_POSITIONED";

	// Token: 0x040050B6 RID: 20662
	private const string TIER_SLOTS_LOADED_EVENT = "TIER_SLOTS_LOADED";

	// Token: 0x040050B7 RID: 20663
	protected List<ShopSection> m_shopSections = new List<ShopSection>();

	// Token: 0x040050B8 RID: 20664
	protected List<WidgetInstance> m_tierInstances = new List<WidgetInstance>();

	// Token: 0x040050B9 RID: 20665
	private Widget m_widget;

	// Token: 0x040050BA RID: 20666
	private ProductCatalog.TestDataMode? m_appliedTestData;

	// Token: 0x040050BB RID: 20667
	private bool m_isLoading;

	// Token: 0x040050BC RID: 20668
	private int m_framesSpentLoading;

	// Token: 0x040050BD RID: 20669
	private DateTime m_timeStartedLoading;

	// Token: 0x040050BE RID: 20670
	private TimeSpan m_timeSpentLoading;
}
