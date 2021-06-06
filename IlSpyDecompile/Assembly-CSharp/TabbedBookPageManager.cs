using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TabbedBookPageManager : BookPageManager
{
	public GameObject m_classTabContainer;

	public BookTab m_classTabPrefab;

	public float m_spaceBetweenTabs;

	protected BookTab m_currentClassTab;

	protected List<BookTab> m_allTabs = new List<BookTab>();

	protected Map<BookTab, bool> m_tabVisibility = new Map<BookTab, bool>();

	protected bool m_tabsAreAnimating;

	protected override void Start()
	{
		SetUpBookTabs();
		base.Start();
	}

	public void UpdateVisibleTabs()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		bool flag = false;
		foreach (BookTab allTab in m_allTabs)
		{
			bool num = m_tabVisibility[allTab];
			bool flag2 = ShouldShowTab(allTab);
			if (num != flag2)
			{
				flag = true;
				m_tabVisibility[allTab] = flag2;
			}
		}
		if (flag)
		{
			PositionBookTabs(animate: true);
		}
	}

	protected abstract bool ShouldShowTab(BookTab tab);

	protected abstract void SetUpBookTabs();

	protected abstract void PositionBookTabs(bool animate);

	protected void DeselectCurrentClassTab()
	{
		if (!(m_currentClassTab == null))
		{
			m_currentClassTab.SetSelected(selected: false);
			m_currentClassTab.SetLargeTab(large: false);
			m_currentClassTab = null;
		}
	}

	protected void OnTabOver(UIEvent e)
	{
		BookTab bookTab = e.GetElement() as BookTab;
		if (!(bookTab == null))
		{
			bookTab.SetGlowActive(active: true);
		}
	}

	protected void OnTabOut(UIEvent e)
	{
		BookTab bookTab = e.GetElement() as BookTab;
		if (!(bookTab == null))
		{
			bookTab.SetGlowActive(active: false);
		}
	}

	protected void OnTabOver_Touch(UIEvent e)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			(e.GetElement() as BookTab).SetLargeTab(large: true);
		}
	}

	protected void OnTabOut_Touch(UIEvent e)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			BookTab bookTab = e.GetElement() as BookTab;
			if (bookTab != m_currentClassTab)
			{
				bookTab.SetLargeTab(large: false);
			}
		}
	}

	protected override void TransitionPage(object callbackData)
	{
		base.TransitionPage(callbackData);
		UpdateVisibleTabs();
	}

	protected override void HandleTouchModeChanged()
	{
		base.HandleTouchModeChanged();
		foreach (BookTab allTab in m_allTabs)
		{
			allTab.SetReceiveReleaseWithoutMouseDown(UniversalInputManager.Get().IsTouchMode());
		}
	}

	protected void PositionFixedTab(bool showTab, BookTab tab, Vector3 originalPos, bool animate)
	{
		if (!showTab)
		{
			originalPos.z -= 0.5f;
		}
		tab.SetTargetVisibility(showTab);
		tab.SetTargetLocalPosition(originalPos);
		if (animate)
		{
			tab.AnimateToTargetPosition(0.4f, iTween.EaseType.easeOutQuad);
			return;
		}
		tab.SetIsVisible(tab.ShouldBeVisible());
		tab.transform.localPosition = originalPos;
	}

	protected IEnumerator SelectTabWhenReady(BookTab tab)
	{
		while (m_tabsAreAnimating)
		{
			yield return 0;
		}
		if (!(m_currentClassTab != tab))
		{
			tab.SetSelected(selected: true);
			tab.SetLargeTab(large: true);
		}
	}
}
