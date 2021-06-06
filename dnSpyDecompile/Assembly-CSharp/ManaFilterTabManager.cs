using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class ManaFilterTabManager : MonoBehaviour
{
	// Token: 0x1400001E RID: 30
	// (add) Token: 0x0600140F RID: 5135 RVA: 0x000732FC File Offset: 0x000714FC
	// (remove) Token: 0x06001410 RID: 5136 RVA: 0x00073334 File Offset: 0x00071534
	public event Action<bool> OnFilterCleared;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06001411 RID: 5137 RVA: 0x0007336C File Offset: 0x0007156C
	// (remove) Token: 0x06001412 RID: 5138 RVA: 0x000733A4 File Offset: 0x000715A4
	public event Action<int, bool> OnManaValueActivated;

	// Token: 0x06001413 RID: 5139 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x000733D9 File Offset: 0x000715D9
	public void ClearFilter(bool transitionPage = true)
	{
		this.UpdateCurrentFilterToSingleValue(-1, transitionPage);
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06001415 RID: 5141 RVA: 0x000733E3 File Offset: 0x000715E3
	public bool IsFilterActive
	{
		get
		{
			return this.IsFilterOnExactValues || this.IsFilterOddOrEvenValues || this.IsFilterRange;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06001416 RID: 5142 RVA: 0x000733FD File Offset: 0x000715FD
	public bool IsFilterOnExactValues
	{
		get
		{
			return this.m_currentFilterExactValues.Count > 0;
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06001417 RID: 5143 RVA: 0x0007340D File Offset: 0x0007160D
	public bool IsFilterOddOrEvenValues
	{
		get
		{
			return this.IsFilterEvenValues || this.IsFilterOddValues;
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06001418 RID: 5144 RVA: 0x0007341F File Offset: 0x0007161F
	public bool IsFilterEvenValues
	{
		get
		{
			return this.m_currentFilterIsEven;
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06001419 RID: 5145 RVA: 0x00073427 File Offset: 0x00071627
	public bool IsFilterOddValues
	{
		get
		{
			return this.m_currentFilterIsOdd;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x0600141A RID: 5146 RVA: 0x0007342F File Offset: 0x0007162F
	public bool IsFilterRange
	{
		get
		{
			return this.m_currentFilterMinValue != null || this.m_currentFilterMaxValue != null;
		}
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0007344C File Offset: 0x0007164C
	public bool IsManaValueActive(int manaValue)
	{
		if (this.m_currentFilterExactValues.Contains(manaValue))
		{
			return true;
		}
		if (this.IsFilterRange)
		{
			return (this.m_currentFilterMinValue == null || manaValue >= this.m_currentFilterMinValue.Value) && (this.m_currentFilterMaxValue == null || manaValue <= this.m_currentFilterMaxValue.Value);
		}
		if (this.IsFilterEvenValues)
		{
			return manaValue % 2 == 0;
		}
		return this.IsFilterOddValues && manaValue % 2 == 1;
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x000734CC File Offset: 0x000716CC
	public void SetFilter_Range(int minCost, int maxCost)
	{
		if (this.m_currentFilterMinValue == null || this.m_currentFilterMinValue.Value != minCost || this.m_currentFilterMaxValue == null || this.m_currentFilterMaxValue.Value != maxCost)
		{
			SoundManager.Get().LoadAndPlay("mana_crystal_refresh.prefab:ea5c456dd852f904e9828db66636f54d");
		}
		this.m_currentFilterExactValues.Clear();
		this.m_currentFilterMinValue = new int?(minCost);
		this.m_currentFilterMaxValue = new int?(maxCost);
		this.m_currentFilterIsEven = false;
		this.m_currentFilterIsOdd = false;
		this.UpdateFilterStates();
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x00073564 File Offset: 0x00071764
	public void SetFilter_EvenOdd(bool isOdd)
	{
		if (!this.IsFilterOddOrEvenValues || isOdd != this.IsFilterOddValues)
		{
			SoundManager.Get().LoadAndPlay("mana_crystal_refresh.prefab:ea5c456dd852f904e9828db66636f54d");
		}
		this.m_currentFilterExactValues.Clear();
		this.m_currentFilterMinValue = (this.m_currentFilterMaxValue = null);
		this.m_currentFilterIsEven = !isOdd;
		this.m_currentFilterIsOdd = isOdd;
		this.UpdateFilterStates();
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000735D8 File Offset: 0x000717D8
	public void SetUpTabs()
	{
		for (int i = 0; i <= 6; i++)
		{
			this.CreateNewTab(this.m_singleManaFilterPrefab, i);
		}
		this.CreateNewTab(this.m_dynamicManaFilterPrefab, 7);
		this.m_manaCrystalContainer.UpdateSlices();
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x00073616 File Offset: 0x00071816
	public void ActivateTabs(bool active)
	{
		this.m_tabsActive = active;
		this.UpdateFilterStates();
		if (active)
		{
			this.m_manaCrystalContainer.UpdateSlices();
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06001420 RID: 5152 RVA: 0x00073634 File Offset: 0x00071834
	// (set) Token: 0x06001421 RID: 5153 RVA: 0x00073690 File Offset: 0x00071890
	public bool Enabled
	{
		get
		{
			using (List<ManaFilterTab>.Enumerator enumerator = this.m_tabs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsEnabled())
					{
						return false;
					}
				}
			}
			return true;
		}
		set
		{
			foreach (ManaFilterTab manaFilterTab in this.m_tabs)
			{
				manaFilterTab.SetEnabled(value, false);
				ManaFilterTab.FilterState filterState = ManaFilterTab.FilterState.DISABLED;
				if (manaFilterTab.IsEnabled() && this.m_tabsActive)
				{
					filterState = this.GetTabFilterState(manaFilterTab.GetManaID());
				}
				manaFilterTab.SetFilterState(filterState);
				if (manaFilterTab.m_costText != null)
				{
					manaFilterTab.m_costText.gameObject.SetActive(value);
				}
			}
		}
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x0007372C File Offset: 0x0007192C
	private void CreateNewTab(ManaFilterTab tabPrefab, int index)
	{
		ManaFilterTab manaFilterTab = (ManaFilterTab)GameUtils.Instantiate(tabPrefab, this.m_manaCrystalContainer.gameObject, false);
		manaFilterTab.SetManaID(index);
		manaFilterTab.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnTabPressed));
		manaFilterTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnTabMousedOver));
		manaFilterTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnTabMousedOut));
		manaFilterTab.SetFilterState(ManaFilterTab.FilterState.DISABLED);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			manaFilterTab.SetReceiveReleaseWithoutMouseDown(true);
		}
		this.m_tabs.Add(manaFilterTab);
		this.m_manaCrystalContainer.AddSlice(manaFilterTab.gameObject);
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000737CC File Offset: 0x000719CC
	private void OnTabPressed(UIEvent e)
	{
		if (!this.m_tabsActive)
		{
			return;
		}
		ManaFilterTab manaFilterTab = (ManaFilterTab)e.GetElement();
		if (!UniversalInputManager.UsePhoneUI && !Options.Get().GetBool(Option.HAS_CLICKED_MANA_TAB, false) && UserAttentionManager.CanShowAttentionGrabber("ManaFilterTabManager.OnTabPressed:" + Option.HAS_CLICKED_MANA_TAB))
		{
			Options.Get().SetBool(Option.HAS_CLICKED_MANA_TAB, true);
			this.ShowManaTabHint(manaFilterTab);
		}
		if (this.IsManaValueActive(manaFilterTab.GetManaID()))
		{
			TelemetryManager.Client().SendManaFilterToggleOff();
			this.UpdateCurrentFilterToSingleValue(-1, true);
			return;
		}
		this.UpdateCurrentFilterToSingleValue(manaFilterTab.GetManaID(), true);
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x0007386C File Offset: 0x00071A6C
	private void OnTabMousedOver(UIEvent e)
	{
		if (!this.m_tabsActive)
		{
			return;
		}
		((ManaFilterTab)e.GetElement()).NotifyMousedOver();
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x00073887 File Offset: 0x00071A87
	private void OnTabMousedOut(UIEvent e)
	{
		if (!this.m_tabsActive)
		{
			return;
		}
		((ManaFilterTab)e.GetElement()).NotifyMousedOut();
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x000738A2 File Offset: 0x00071AA2
	private ManaFilterTab.FilterState GetTabFilterState(int manaValue)
	{
		if (!this.IsManaValueActive(manaValue))
		{
			return ManaFilterTab.FilterState.OFF;
		}
		return ManaFilterTab.FilterState.ON;
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000738B0 File Offset: 0x00071AB0
	private void UpdateCurrentFilterToSingleValue(int manaValue, bool transitionPage = true)
	{
		bool flag = this.m_currentFilterIsEven || this.m_currentFilterIsOdd || this.m_currentFilterExactValues.Count != 1 || !this.m_currentFilterExactValues.Contains(manaValue);
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("mana_crystal_refresh.prefab:ea5c456dd852f904e9828db66636f54d");
		}
		this.m_currentFilterExactValues.Clear();
		if (manaValue != -1)
		{
			this.m_currentFilterExactValues.Add(manaValue);
		}
		this.m_currentFilterMinValue = (this.m_currentFilterMaxValue = null);
		this.m_currentFilterIsEven = false;
		this.m_currentFilterIsOdd = false;
		this.UpdateFilterStates();
		if (flag)
		{
			if (manaValue == -1)
			{
				if (this.OnFilterCleared != null)
				{
					this.OnFilterCleared(transitionPage);
					return;
				}
			}
			else if (this.OnManaValueActivated != null)
			{
				this.OnManaValueActivated(manaValue, transitionPage);
			}
		}
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x0007397C File Offset: 0x00071B7C
	private void UpdateFilterStates()
	{
		foreach (ManaFilterTab manaFilterTab in this.m_tabs)
		{
			ManaFilterTab.FilterState filterState = ManaFilterTab.FilterState.DISABLED;
			if (manaFilterTab.IsEnabled() && this.m_tabsActive)
			{
				filterState = this.GetTabFilterState(manaFilterTab.GetManaID());
			}
			manaFilterTab.SetFilterState(filterState);
		}
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x000739F0 File Offset: 0x00071BF0
	private void ShowManaTabHint(ManaFilterTab tabButton)
	{
		Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, tabButton.transform.position + new Vector3(0f, 0f, 7f), TutorialEntity.GetTextScale(), GameStrings.Get("GLUE_COLLECTION_MANAGER_MANA_TAB_FIRST_CLICK"), true, NotificationManager.PopupTextType.BASIC);
		if (notification == null)
		{
			return;
		}
		notification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		NotificationManager.Get().DestroyNotification(notification, 3f);
	}

	// Token: 0x04000D31 RID: 3377
	public ManaFilterTab m_singleManaFilterPrefab;

	// Token: 0x04000D32 RID: 3378
	public ManaFilterTab m_dynamicManaFilterPrefab;

	// Token: 0x04000D33 RID: 3379
	public MultiSliceElement m_manaCrystalContainer;

	// Token: 0x04000D34 RID: 3380
	private bool m_tabsActive;

	// Token: 0x04000D35 RID: 3381
	private List<ManaFilterTab> m_tabs = new List<ManaFilterTab>();

	// Token: 0x04000D36 RID: 3382
	private HashSet<int> m_currentFilterExactValues = new HashSet<int>();

	// Token: 0x04000D37 RID: 3383
	private int? m_currentFilterMinValue;

	// Token: 0x04000D38 RID: 3384
	private int? m_currentFilterMaxValue;

	// Token: 0x04000D39 RID: 3385
	private bool m_currentFilterIsEven;

	// Token: 0x04000D3A RID: 3386
	private bool m_currentFilterIsOdd;
}
