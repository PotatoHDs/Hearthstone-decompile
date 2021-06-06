using System;
using UnityEngine;

// Token: 0x02000AF2 RID: 2802
[CustomEditClass]
public class UIBHighlightStateControl : MonoBehaviour
{
	// Token: 0x06009513 RID: 38163 RVA: 0x00304A94 File Offset: 0x00302C94
	private void Awake()
	{
		PegUIElement component = base.gameObject.GetComponent<PegUIElement>();
		if (component != null)
		{
			component.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				if (this.m_EnableResponse)
				{
					this.OnRollOver();
				}
			});
			component.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
			{
				if (this.m_EnableResponse)
				{
					this.OnRollOut();
				}
			});
			component.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				if (this.m_EnableResponse)
				{
					this.OnRelease();
				}
			});
		}
	}

	// Token: 0x06009514 RID: 38164 RVA: 0x00304AF4 File Offset: 0x00302CF4
	public void Select(bool selected, bool primary = false)
	{
		if (selected)
		{
			this.m_HighlightState.ChangeState(primary ? this.m_PrimarySelectedStateType : this.m_SecondarySelectedStateType);
			return;
		}
		if (this.m_MouseOver)
		{
			this.m_HighlightState.ChangeState(this.m_MouseOverStateType);
			return;
		}
		this.m_HighlightState.ChangeState(ActorStateType.NONE);
	}

	// Token: 0x06009515 RID: 38165 RVA: 0x00304B4A File Offset: 0x00302D4A
	public bool IsReady()
	{
		return this.m_HighlightState.IsReady();
	}

	// Token: 0x06009516 RID: 38166 RVA: 0x00304B57 File Offset: 0x00302D57
	private void OnRollOver()
	{
		if (this.m_UseMouseOver)
		{
			this.m_MouseOver = true;
			this.m_HighlightState.ChangeState(this.m_MouseOverStateType);
		}
	}

	// Token: 0x06009517 RID: 38167 RVA: 0x00304B7A File Offset: 0x00302D7A
	private void OnRollOut()
	{
		if (this.m_UseMouseOver)
		{
			this.m_MouseOver = false;
			if (!this.m_AllowSelection)
			{
				this.m_HighlightState.ChangeState(ActorStateType.NONE);
			}
		}
	}

	// Token: 0x06009518 RID: 38168 RVA: 0x00304BA0 File Offset: 0x00302DA0
	private void OnRelease()
	{
		if (this.m_AllowSelection)
		{
			this.Select(true, false);
			return;
		}
		if (this.m_MouseOver)
		{
			this.m_HighlightState.ChangeState(this.m_MouseOverStateType);
			return;
		}
		this.m_HighlightState.ChangeState(ActorStateType.NONE);
	}

	// Token: 0x04007D05 RID: 32005
	[CustomEditField(Sections = "Highlight State Reference")]
	public HighlightState m_HighlightState;

	// Token: 0x04007D06 RID: 32006
	[CustomEditField(Sections = "Highlight State Type")]
	public ActorStateType m_MouseOverStateType = ActorStateType.HIGHLIGHT_MOUSE_OVER;

	// Token: 0x04007D07 RID: 32007
	[CustomEditField(Sections = "Highlight State Type")]
	public ActorStateType m_PrimarySelectedStateType = ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE;

	// Token: 0x04007D08 RID: 32008
	[CustomEditField(Sections = "Highlight State Type")]
	public ActorStateType m_SecondarySelectedStateType = ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE;

	// Token: 0x04007D09 RID: 32009
	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_UseMouseOver;

	// Token: 0x04007D0A RID: 32010
	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_AllowSelection;

	// Token: 0x04007D0B RID: 32011
	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_EnableResponse = true;

	// Token: 0x04007D0C RID: 32012
	private PegUIElement m_PegUIElement;

	// Token: 0x04007D0D RID: 32013
	private bool m_MouseOver;
}
