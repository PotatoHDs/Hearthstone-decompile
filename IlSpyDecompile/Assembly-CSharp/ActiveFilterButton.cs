using UnityEngine;

public class ActiveFilterButton : MonoBehaviour
{
	public SlidingTray m_manaFilterTray;

	public SlidingTray m_setFilterTray;

	public UberText m_searchText;

	public GameObject m_manaFilterIcon;

	public UberText m_manaFilterText;

	public PegUIElement m_activeFilterButton;

	public PegUIElement m_inactiveFilterButton;

	public ManaFilterTabManager m_manaFilter;

	public SetFilterTray m_setFilter;

	public NestedPrefab m_setFilterContainer;

	public CollectionSearch m_search;

	public PegUIElement m_offClickCatcher;

	public UIBButton m_doneButton;

	public Material m_enabledMaterial;

	public Material m_disabledMaterial;

	public MeshRenderer m_inactiveFilterButtonRenderer;

	public GameObject m_inactiveFilterButtonText;

	public Transform m_manaFilterIconCenterBone;

	public Transform m_setFilterIconCenterBone;

	private bool m_filtersShown;

	private bool m_manaFilterActive;

	private string m_manaFilterValue = "";

	private bool m_searchFilterActive;

	private string m_searchFilterValue = "";

	private Vector3 m_manaFilterIconDefaultPos;

	private Vector3 m_setFilterIconDefaultPos;

	protected void Awake()
	{
		if (m_inactiveFilterButton != null)
		{
			m_inactiveFilterButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				ShowFilters();
			});
		}
		if (m_activeFilterButton != null)
		{
			m_activeFilterButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				ClearFilters();
			});
		}
		if (m_doneButton != null)
		{
			m_doneButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				OffClickPressed();
			});
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.RegisterManaFilterListener(ManaFilterUpdate);
			collectionManagerDisplay.RegisterSearchFilterListener(SearchFilterUpdate);
		}
		m_manaFilter.OnFilterCleared += ManaFilter_OnFilterCleared;
	}

	protected void Start()
	{
		if (m_setFilterContainer != null)
		{
			m_setFilter = m_setFilterContainer.PrefabGameObject().GetComponent<SetFilterTray>();
			m_setFilter.m_toggleButton.transform.parent = base.transform;
			m_setFilterIconDefaultPos = m_setFilter.m_toggleButton.transform.localPosition;
		}
		m_manaFilterIconDefaultPos = m_manaFilterIcon.transform.localPosition;
		FiltersUpdated();
	}

	protected void OnDestroy()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.UnregisterManaFilterListener(ManaFilterUpdate);
			collectionManagerDisplay.UnregisterSearchFilterListener(SearchFilterUpdate);
		}
		if (m_manaFilter != null)
		{
			m_manaFilter.OnFilterCleared -= ManaFilter_OnFilterCleared;
		}
	}

	private void ShowFilters()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideDeckHelpPopup();
		}
		Navigation.Push(HideFilters);
		m_manaFilterTray.ToggleTraySlider(show: true);
		m_setFilterTray.ToggleTraySlider(show: true);
		m_setFilter.Show(show: true);
		m_manaFilter.m_manaCrystalContainer.UpdateSlices();
	}

	private bool HideFilters()
	{
		m_manaFilterTray.ToggleTraySlider(show: false);
		m_setFilterTray.ToggleTraySlider(show: false);
		CollectibleDisplay collectibleDisplay = CollectionManager.Get().GetCollectibleDisplay();
		if (collectibleDisplay != null)
		{
			collectibleDisplay.m_search.Deactivate();
		}
		m_setFilter.Show(show: false);
		return true;
	}

	private void OffClickPressed()
	{
		Navigation.GoBack();
		FiltersUpdated();
	}

	public void ClearFilters()
	{
		m_manaFilter.ClearFilter(transitionPage: false);
		m_search.ClearFilter(updateVisuals: false);
		m_setFilter.ClearFilter();
	}

	public void SetEnabled(bool enabled)
	{
		m_inactiveFilterButton.SetEnabled(enabled);
		m_inactiveFilterButtonText.SetActive(enabled);
		m_inactiveFilterButtonRenderer.SetSharedMaterial(enabled ? m_enabledMaterial : m_disabledMaterial);
	}

	private void ManaFilter_OnFilterCleared(bool transitionPage)
	{
		ManaFilterUpdate(state: false, string.Empty);
	}

	private void ManaFilterUpdate(bool state, object description)
	{
		m_manaFilterActive = state;
		if (description == null)
		{
			m_manaFilterValue = "";
		}
		else
		{
			m_manaFilterValue = (string)description;
		}
		FiltersUpdated();
	}

	private void SearchFilterUpdate(bool state, object description)
	{
		m_searchFilterActive = state;
		if (description == null)
		{
			m_searchFilterValue = "";
		}
		else
		{
			m_searchFilterValue = (string)description;
		}
		FiltersUpdated();
	}

	private void FiltersUpdated()
	{
		bool flag = m_manaFilterActive || m_searchFilterActive || m_setFilter.HasActiveFilter();
		bool active = m_manaFilterActive && !m_searchFilterActive;
		bool flag2 = m_searchFilterActive;
		bool flag3 = m_setFilter.HasActiveFilter() && !m_searchFilterActive;
		string text = m_manaFilterValue;
		string searchFilterValue = m_searchFilterValue;
		if (m_manaFilter.IsFilterOddOrEvenValues)
		{
			active = false;
			flag2 = true;
			flag3 = false;
			text = string.Empty;
		}
		if (m_inactiveFilterButton != null)
		{
			m_activeFilterButton.gameObject.SetActive(flag);
			m_inactiveFilterButton.gameObject.SetActive(!flag);
		}
		else
		{
			if (m_filtersShown != flag)
			{
				Vector3 euler = (flag ? new Vector3(180f, 0f, 0f) : new Vector3(0f, 0f, 0f));
				float num = (flag ? 0.5f : (-0.5f));
				iTween.Stop(m_activeFilterButton.gameObject);
				m_activeFilterButton.gameObject.transform.localRotation = Quaternion.Euler(euler);
				iTween.RotateBy(m_activeFilterButton.gameObject, iTween.Hash("x", num, "time", 0.25f, "easetype", iTween.EaseType.easeInOutExpo));
			}
			m_filtersShown = flag;
		}
		if (flag2)
		{
			m_searchText.gameObject.SetActive(value: true);
			m_searchText.Text = searchFilterValue;
		}
		else
		{
			m_searchText.gameObject.SetActive(value: false);
			m_searchText.Text = string.Empty;
		}
		m_manaFilterIcon.SetActive(active);
		m_manaFilterText.Text = text;
		m_setFilter.SetButtonShown(flag3);
		if (m_manaFilterIcon.activeSelf && !flag3)
		{
			m_manaFilterIcon.transform.localPosition = m_manaFilterIconCenterBone.localPosition;
			return;
		}
		if (!m_manaFilterIcon.activeSelf && flag3)
		{
			m_setFilter.m_toggleButton.gameObject.transform.localPosition = m_setFilterIconCenterBone.localPosition;
			return;
		}
		m_manaFilterIcon.transform.localPosition = m_manaFilterIconDefaultPos;
		m_setFilter.m_toggleButton.gameObject.transform.localPosition = m_setFilterIconDefaultPos;
	}
}
