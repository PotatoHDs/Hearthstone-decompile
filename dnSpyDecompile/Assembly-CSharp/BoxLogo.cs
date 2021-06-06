using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class BoxLogo : MonoBehaviour
{
	// Token: 0x06000C67 RID: 3175 RVA: 0x00048FEB File Offset: 0x000471EB
	public Box GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00048FF3 File Offset: 0x000471F3
	public void SetParent(Box parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x00048FFC File Offset: 0x000471FC
	public BoxLogoStateInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00049004 File Offset: 0x00047204
	public void SetInfo(BoxLogoStateInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00049010 File Offset: 0x00047210
	public bool ChangeState(BoxLogo.State state)
	{
		if (this.m_state == state)
		{
			return false;
		}
		this.m_state = state;
		if (state == BoxLogo.State.SHOWN)
		{
			this.m_parent.OnAnimStarted();
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				this.m_info.m_ShownAlpha,
				"delay",
				this.m_info.m_ShownDelaySec,
				"time",
				this.m_info.m_ShownFadeSec,
				"easeType",
				this.m_info.m_ShownFadeEaseType,
				"oncomplete",
				"OnAnimFinished",
				"oncompletetarget",
				this.m_parent.gameObject
			});
			iTween.FadeTo(base.gameObject, args);
		}
		else if (state == BoxLogo.State.HIDDEN)
		{
			this.m_parent.OnAnimStarted();
			Hashtable args2 = iTween.Hash(new object[]
			{
				"amount",
				this.m_info.m_HiddenAlpha,
				"delay",
				this.m_info.m_HiddenDelaySec,
				"time",
				this.m_info.m_HiddenFadeSec,
				"easeType",
				this.m_info.m_HiddenFadeEaseType,
				"oncomplete",
				"OnAnimFinished",
				"oncompletetarget",
				this.m_parent.gameObject
			});
			iTween.FadeTo(base.gameObject, args2);
		}
		return true;
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x000491B4 File Offset: 0x000473B4
	public void UpdateState(BoxLogo.State state)
	{
		this.m_state = state;
		if (state == BoxLogo.State.SHOWN)
		{
			RenderUtils.SetAlpha(base.gameObject, this.m_info.m_ShownAlpha);
			return;
		}
		if (state == BoxLogo.State.HIDDEN)
		{
			RenderUtils.SetAlpha(base.gameObject, this.m_info.m_HiddenAlpha);
		}
	}

	// Token: 0x0400089F RID: 2207
	private Box m_parent;

	// Token: 0x040008A0 RID: 2208
	private BoxLogoStateInfo m_info;

	// Token: 0x040008A1 RID: 2209
	private BoxLogo.State m_state;

	// Token: 0x020013E0 RID: 5088
	public enum State
	{
		// Token: 0x0400A822 RID: 43042
		SHOWN,
		// Token: 0x0400A823 RID: 43043
		HIDDEN
	}
}
