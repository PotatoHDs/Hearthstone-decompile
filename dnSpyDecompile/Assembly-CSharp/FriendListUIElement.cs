using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class FriendListUIElement : PegUIElement
{
	// Token: 0x06000964 RID: 2404 RVA: 0x0003701B File Offset: 0x0003521B
	protected override void Awake()
	{
		base.Awake();
		this.UpdateHighlight();
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00037029 File Offset: 0x00035229
	public bool IsSelected()
	{
		return this.m_selected;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00037031 File Offset: 0x00035231
	public void SetSelected(bool enable)
	{
		if (enable == this.m_selected)
		{
			return;
		}
		this.m_selected = enable;
		this.UpdateHighlight();
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0003704A File Offset: 0x0003524A
	protected virtual bool ShouldBeHighlighted()
	{
		return this.m_selected || base.GetInteractionState() == PegUIElement.InteractionState.Over;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00037060 File Offset: 0x00035260
	protected void UpdateHighlight()
	{
		bool flag = this.ShouldBeHighlighted();
		if (!flag)
		{
			flag = this.ShouldChildBeHighlighted();
		}
		this.UpdateSelfHighlight(flag);
		if (this.m_ParentElement != null)
		{
			this.m_ParentElement.UpdateHighlight();
		}
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x000370A0 File Offset: 0x000352A0
	protected bool ShouldChildBeHighlighted()
	{
		FriendListUIElement[] componentsInChildrenOnly = SceneUtils.GetComponentsInChildrenOnly<FriendListUIElement>(this, true);
		for (int i = 0; i < componentsInChildrenOnly.Length; i++)
		{
			if (componentsInChildrenOnly[i].ShouldBeHighlighted())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x000370D0 File Offset: 0x000352D0
	protected void UpdateSelfHighlight(bool shouldHighlight)
	{
		if (this.m_Highlight == null)
		{
			return;
		}
		if (this.m_Highlight.activeSelf == shouldHighlight)
		{
			return;
		}
		this.m_Highlight.SetActive(shouldHighlight);
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0003012E File Offset: 0x0002E32E
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		this.UpdateHighlight();
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0003012E File Offset: 0x0002E32E
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.UpdateHighlight();
	}

	// Token: 0x04000652 RID: 1618
	public FriendListUIElement m_ParentElement;

	// Token: 0x04000653 RID: 1619
	public GameObject m_Highlight;

	// Token: 0x04000654 RID: 1620
	private bool m_selected;
}
