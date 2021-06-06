using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public abstract class TabbedBookPageManager : BookPageManager
{
	// Token: 0x06000B7C RID: 2940 RVA: 0x000434EF File Offset: 0x000416EF
	protected override void Start()
	{
		this.SetUpBookTabs();
		base.Start();
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00043500 File Offset: 0x00041700
	public void UpdateVisibleTabs()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		bool flag = false;
		foreach (BookTab bookTab in this.m_allTabs)
		{
			bool flag2 = this.m_tabVisibility[bookTab];
			bool flag3 = this.ShouldShowTab(bookTab);
			if (flag2 != flag3)
			{
				flag = true;
				this.m_tabVisibility[bookTab] = flag3;
			}
		}
		if (!flag)
		{
			return;
		}
		this.PositionBookTabs(true);
	}

	// Token: 0x06000B7E RID: 2942
	protected abstract bool ShouldShowTab(BookTab tab);

	// Token: 0x06000B7F RID: 2943
	protected abstract void SetUpBookTabs();

	// Token: 0x06000B80 RID: 2944
	protected abstract void PositionBookTabs(bool animate);

	// Token: 0x06000B81 RID: 2945 RVA: 0x0004358C File Offset: 0x0004178C
	protected void DeselectCurrentClassTab()
	{
		if (this.m_currentClassTab == null)
		{
			return;
		}
		this.m_currentClassTab.SetSelected(false);
		this.m_currentClassTab.SetLargeTab(false);
		this.m_currentClassTab = null;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x000435BC File Offset: 0x000417BC
	protected void OnTabOver(UIEvent e)
	{
		BookTab bookTab = e.GetElement() as BookTab;
		if (bookTab == null)
		{
			return;
		}
		bookTab.SetGlowActive(true);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x000435E8 File Offset: 0x000417E8
	protected void OnTabOut(UIEvent e)
	{
		BookTab bookTab = e.GetElement() as BookTab;
		if (bookTab == null)
		{
			return;
		}
		bookTab.SetGlowActive(false);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00043612 File Offset: 0x00041812
	protected void OnTabOver_Touch(UIEvent e)
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		(e.GetElement() as BookTab).SetLargeTab(true);
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00043634 File Offset: 0x00041834
	protected void OnTabOut_Touch(UIEvent e)
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		BookTab bookTab = e.GetElement() as BookTab;
		if (bookTab != this.m_currentClassTab)
		{
			bookTab.SetLargeTab(false);
		}
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0004366F File Offset: 0x0004186F
	protected override void TransitionPage(object callbackData)
	{
		base.TransitionPage(callbackData);
		this.UpdateVisibleTabs();
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00043680 File Offset: 0x00041880
	protected override void HandleTouchModeChanged()
	{
		base.HandleTouchModeChanged();
		foreach (BookTab bookTab in this.m_allTabs)
		{
			bookTab.SetReceiveReleaseWithoutMouseDown(UniversalInputManager.Get().IsTouchMode());
		}
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x000436E0 File Offset: 0x000418E0
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

	// Token: 0x06000B89 RID: 2953 RVA: 0x00043737 File Offset: 0x00041937
	protected IEnumerator SelectTabWhenReady(BookTab tab)
	{
		while (this.m_tabsAreAnimating)
		{
			yield return 0;
		}
		if (this.m_currentClassTab != tab)
		{
			yield break;
		}
		tab.SetSelected(true);
		tab.SetLargeTab(true);
		yield break;
	}

	// Token: 0x040007B7 RID: 1975
	public GameObject m_classTabContainer;

	// Token: 0x040007B8 RID: 1976
	public BookTab m_classTabPrefab;

	// Token: 0x040007B9 RID: 1977
	public float m_spaceBetweenTabs;

	// Token: 0x040007BA RID: 1978
	protected BookTab m_currentClassTab;

	// Token: 0x040007BB RID: 1979
	protected List<BookTab> m_allTabs = new List<BookTab>();

	// Token: 0x040007BC RID: 1980
	protected Map<BookTab, bool> m_tabVisibility = new Map<BookTab, bool>();

	// Token: 0x040007BD RID: 1981
	protected bool m_tabsAreAnimating;
}
