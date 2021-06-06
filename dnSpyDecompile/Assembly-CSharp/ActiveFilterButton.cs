using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class ActiveFilterButton : MonoBehaviour
{
	// Token: 0x06000DAB RID: 3499 RVA: 0x0004D7C0 File Offset: 0x0004B9C0
	protected void Awake()
	{
		if (this.m_inactiveFilterButton != null)
		{
			this.m_inactiveFilterButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.ShowFilters();
			});
		}
		if (this.m_activeFilterButton != null)
		{
			this.m_activeFilterButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.ClearFilters();
			});
		}
		if (this.m_doneButton != null)
		{
			this.m_doneButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OffClickPressed();
			});
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.RegisterManaFilterListener(new CollectibleDisplay.FilterStateListener(this.ManaFilterUpdate));
			collectionManagerDisplay.RegisterSearchFilterListener(new CollectibleDisplay.FilterStateListener(this.SearchFilterUpdate));
		}
		this.m_manaFilter.OnFilterCleared += this.ManaFilter_OnFilterCleared;
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x0004D898 File Offset: 0x0004BA98
	protected void Start()
	{
		if (this.m_setFilterContainer != null)
		{
			this.m_setFilter = this.m_setFilterContainer.PrefabGameObject(false).GetComponent<SetFilterTray>();
			this.m_setFilter.m_toggleButton.transform.parent = base.transform;
			this.m_setFilterIconDefaultPos = this.m_setFilter.m_toggleButton.transform.localPosition;
		}
		this.m_manaFilterIconDefaultPos = this.m_manaFilterIcon.transform.localPosition;
		this.FiltersUpdated();
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0004D91C File Offset: 0x0004BB1C
	protected void OnDestroy()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.UnregisterManaFilterListener(new CollectibleDisplay.FilterStateListener(this.ManaFilterUpdate));
			collectionManagerDisplay.UnregisterSearchFilterListener(new CollectibleDisplay.FilterStateListener(this.SearchFilterUpdate));
		}
		if (this.m_manaFilter != null)
		{
			this.m_manaFilter.OnFilterCleared -= this.ManaFilter_OnFilterCleared;
		}
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0004D98C File Offset: 0x0004BB8C
	private void ShowFilters()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideDeckHelpPopup();
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.HideFilters));
		this.m_manaFilterTray.ToggleTraySlider(true, null, true);
		this.m_setFilterTray.ToggleTraySlider(true, null, true);
		this.m_setFilter.Show(true);
		this.m_manaFilter.m_manaCrystalContainer.UpdateSlices();
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0004DA04 File Offset: 0x0004BC04
	private bool HideFilters()
	{
		this.m_manaFilterTray.ToggleTraySlider(false, null, true);
		this.m_setFilterTray.ToggleTraySlider(false, null, true);
		CollectibleDisplay collectibleDisplay = CollectionManager.Get().GetCollectibleDisplay();
		if (collectibleDisplay != null)
		{
			collectibleDisplay.m_search.Deactivate();
		}
		this.m_setFilter.Show(false);
		return true;
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0004DA59 File Offset: 0x0004BC59
	private void OffClickPressed()
	{
		Navigation.GoBack();
		this.FiltersUpdated();
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0004DA67 File Offset: 0x0004BC67
	public void ClearFilters()
	{
		this.m_manaFilter.ClearFilter(false);
		this.m_search.ClearFilter(false);
		this.m_setFilter.ClearFilter(true);
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0004DA8D File Offset: 0x0004BC8D
	public void SetEnabled(bool enabled)
	{
		this.m_inactiveFilterButton.SetEnabled(enabled, false);
		this.m_inactiveFilterButtonText.SetActive(enabled);
		this.m_inactiveFilterButtonRenderer.SetSharedMaterial(enabled ? this.m_enabledMaterial : this.m_disabledMaterial);
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0004DAC4 File Offset: 0x0004BCC4
	private void ManaFilter_OnFilterCleared(bool transitionPage)
	{
		this.ManaFilterUpdate(false, string.Empty);
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0004DAD2 File Offset: 0x0004BCD2
	private void ManaFilterUpdate(bool state, object description)
	{
		this.m_manaFilterActive = state;
		if (description == null)
		{
			this.m_manaFilterValue = "";
		}
		else
		{
			this.m_manaFilterValue = (string)description;
		}
		this.FiltersUpdated();
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x0004DAFD File Offset: 0x0004BCFD
	private void SearchFilterUpdate(bool state, object description)
	{
		this.m_searchFilterActive = state;
		if (description == null)
		{
			this.m_searchFilterValue = "";
		}
		else
		{
			this.m_searchFilterValue = (string)description;
		}
		this.FiltersUpdated();
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x0004DB28 File Offset: 0x0004BD28
	private void FiltersUpdated()
	{
		bool flag = this.m_manaFilterActive || this.m_searchFilterActive || this.m_setFilter.HasActiveFilter();
		bool active = this.m_manaFilterActive && !this.m_searchFilterActive;
		bool flag2 = this.m_searchFilterActive;
		bool flag3 = this.m_setFilter.HasActiveFilter() && !this.m_searchFilterActive;
		string text = this.m_manaFilterValue;
		string searchFilterValue = this.m_searchFilterValue;
		if (this.m_manaFilter.IsFilterOddOrEvenValues)
		{
			active = false;
			flag2 = true;
			flag3 = false;
			text = string.Empty;
		}
		if (this.m_inactiveFilterButton != null)
		{
			this.m_activeFilterButton.gameObject.SetActive(flag);
			this.m_inactiveFilterButton.gameObject.SetActive(!flag);
		}
		else
		{
			if (this.m_filtersShown != flag)
			{
				Vector3 euler = flag ? new Vector3(180f, 0f, 0f) : new Vector3(0f, 0f, 0f);
				float num = flag ? 0.5f : -0.5f;
				iTween.Stop(this.m_activeFilterButton.gameObject);
				this.m_activeFilterButton.gameObject.transform.localRotation = Quaternion.Euler(euler);
				iTween.RotateBy(this.m_activeFilterButton.gameObject, iTween.Hash(new object[]
				{
					"x",
					num,
					"time",
					0.25f,
					"easetype",
					iTween.EaseType.easeInOutExpo
				}));
			}
			this.m_filtersShown = flag;
		}
		if (flag2)
		{
			this.m_searchText.gameObject.SetActive(true);
			this.m_searchText.Text = searchFilterValue;
		}
		else
		{
			this.m_searchText.gameObject.SetActive(false);
			this.m_searchText.Text = string.Empty;
		}
		this.m_manaFilterIcon.SetActive(active);
		this.m_manaFilterText.Text = text;
		this.m_setFilter.SetButtonShown(flag3);
		if (this.m_manaFilterIcon.activeSelf && !flag3)
		{
			this.m_manaFilterIcon.transform.localPosition = this.m_manaFilterIconCenterBone.localPosition;
			return;
		}
		if (!this.m_manaFilterIcon.activeSelf && flag3)
		{
			this.m_setFilter.m_toggleButton.gameObject.transform.localPosition = this.m_setFilterIconCenterBone.localPosition;
			return;
		}
		this.m_manaFilterIcon.transform.localPosition = this.m_manaFilterIconDefaultPos;
		this.m_setFilter.m_toggleButton.gameObject.transform.localPosition = this.m_setFilterIconDefaultPos;
	}

	// Token: 0x04000958 RID: 2392
	public SlidingTray m_manaFilterTray;

	// Token: 0x04000959 RID: 2393
	public SlidingTray m_setFilterTray;

	// Token: 0x0400095A RID: 2394
	public UberText m_searchText;

	// Token: 0x0400095B RID: 2395
	public GameObject m_manaFilterIcon;

	// Token: 0x0400095C RID: 2396
	public UberText m_manaFilterText;

	// Token: 0x0400095D RID: 2397
	public PegUIElement m_activeFilterButton;

	// Token: 0x0400095E RID: 2398
	public PegUIElement m_inactiveFilterButton;

	// Token: 0x0400095F RID: 2399
	public ManaFilterTabManager m_manaFilter;

	// Token: 0x04000960 RID: 2400
	public SetFilterTray m_setFilter;

	// Token: 0x04000961 RID: 2401
	public NestedPrefab m_setFilterContainer;

	// Token: 0x04000962 RID: 2402
	public CollectionSearch m_search;

	// Token: 0x04000963 RID: 2403
	public PegUIElement m_offClickCatcher;

	// Token: 0x04000964 RID: 2404
	public UIBButton m_doneButton;

	// Token: 0x04000965 RID: 2405
	public Material m_enabledMaterial;

	// Token: 0x04000966 RID: 2406
	public Material m_disabledMaterial;

	// Token: 0x04000967 RID: 2407
	public MeshRenderer m_inactiveFilterButtonRenderer;

	// Token: 0x04000968 RID: 2408
	public GameObject m_inactiveFilterButtonText;

	// Token: 0x04000969 RID: 2409
	public Transform m_manaFilterIconCenterBone;

	// Token: 0x0400096A RID: 2410
	public Transform m_setFilterIconCenterBone;

	// Token: 0x0400096B RID: 2411
	private bool m_filtersShown;

	// Token: 0x0400096C RID: 2412
	private bool m_manaFilterActive;

	// Token: 0x0400096D RID: 2413
	private string m_manaFilterValue = "";

	// Token: 0x0400096E RID: 2414
	private bool m_searchFilterActive;

	// Token: 0x0400096F RID: 2415
	private string m_searchFilterValue = "";

	// Token: 0x04000970 RID: 2416
	private Vector3 m_manaFilterIconDefaultPos;

	// Token: 0x04000971 RID: 2417
	private Vector3 m_setFilterIconDefaultPos;
}
