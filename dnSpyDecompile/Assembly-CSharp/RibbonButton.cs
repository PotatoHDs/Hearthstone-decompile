using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class RibbonButton : PegUIElement
{
	// Token: 0x06000C93 RID: 3219 RVA: 0x0004966A File Offset: 0x0004786A
	public void Start()
	{
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnButtonOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnButtonOut));
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x00049694 File Offset: 0x00047894
	public void OnButtonOver(UIEvent e)
	{
		if (this.m_highlight != null)
		{
			this.m_highlight.SetActive(true);
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x000496B0 File Offset: 0x000478B0
	public void OnButtonOut(UIEvent e)
	{
		if (this.m_highlight != null)
		{
			this.m_highlight.SetActive(false);
		}
	}

	// Token: 0x040008C2 RID: 2242
	public GameObject m_highlight;
}
