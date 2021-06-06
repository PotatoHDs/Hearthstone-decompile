using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class FriendListButton : FriendListUIElement
{
	// Token: 0x06000832 RID: 2098 RVA: 0x000300C6 File Offset: 0x0002E2C6
	public string GetText()
	{
		return this.m_Text.Text;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x000300D3 File Offset: 0x0002E2D3
	public void SetText(string text)
	{
		this.m_Text.Text = text;
		this.UpdateAll();
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x000300E8 File Offset: 0x0002E2E8
	public void ShowActiveGlow(bool show)
	{
		if (this.m_ActiveGlow != null)
		{
			HighlightState componentInChildren = this.m_ActiveGlow.GetComponentInChildren<HighlightState>();
			if (componentInChildren != null)
			{
				if (show)
				{
					componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
					return;
				}
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			}
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0003012E File Offset: 0x0002E32E
	private void UpdateAll()
	{
		base.UpdateHighlight();
	}

	// Token: 0x0400058F RID: 1423
	public GameObject m_Background;

	// Token: 0x04000590 RID: 1424
	public UberText m_Text;

	// Token: 0x04000591 RID: 1425
	public GameObject m_ActiveGlow;
}
