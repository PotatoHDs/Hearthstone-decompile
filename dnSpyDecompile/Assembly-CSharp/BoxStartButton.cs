using System;
using System.Collections;

// Token: 0x020000D3 RID: 211
public class BoxStartButton : PegUIElement
{
	// Token: 0x06000C85 RID: 3205 RVA: 0x000493AC File Offset: 0x000475AC
	public Box GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x000493B4 File Offset: 0x000475B4
	public void SetParent(Box parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x000493BD File Offset: 0x000475BD
	public BoxStartButtonStateInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000493C5 File Offset: 0x000475C5
	public void SetInfo(BoxStartButtonStateInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x000493CE File Offset: 0x000475CE
	public string GetText()
	{
		return this.m_Text.Text;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x000493DB File Offset: 0x000475DB
	public void SetText(string text)
	{
		this.m_Text.Text = text;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x000493EC File Offset: 0x000475EC
	public bool ChangeState(BoxStartButton.State state)
	{
		if (this.m_state == state)
		{
			return false;
		}
		this.m_state = state;
		if (state == BoxStartButton.State.SHOWN)
		{
			this.m_parent.OnAnimStarted();
			base.gameObject.SetActive(true);
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
				"OnShownAnimFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.FadeTo(base.gameObject, args);
		}
		else if (state == BoxStartButton.State.HIDDEN)
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
				"OnHiddenAnimFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.FadeTo(base.gameObject, args2);
		}
		return true;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00049594 File Offset: 0x00047794
	public void UpdateState(BoxStartButton.State state)
	{
		this.m_state = state;
		if (state == BoxStartButton.State.SHOWN)
		{
			RenderUtils.SetAlpha(base.gameObject, this.m_info.m_ShownAlpha);
			base.gameObject.SetActive(true);
			return;
		}
		if (state == BoxStartButton.State.HIDDEN)
		{
			RenderUtils.SetAlpha(base.gameObject, this.m_info.m_HiddenAlpha);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x000495F4 File Offset: 0x000477F4
	private void OnShownAnimFinished()
	{
		this.m_parent.OnAnimFinished();
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00049601 File Offset: 0x00047801
	private void OnHiddenAnimFinished()
	{
		base.gameObject.SetActive(false);
		this.m_parent.OnAnimFinished();
	}

	// Token: 0x040008BC RID: 2236
	public UberText m_Text;

	// Token: 0x040008BD RID: 2237
	private Box m_parent;

	// Token: 0x040008BE RID: 2238
	private BoxStartButtonStateInfo m_info;

	// Token: 0x040008BF RID: 2239
	private BoxStartButton.State m_state;

	// Token: 0x020013E1 RID: 5089
	public enum State
	{
		// Token: 0x0400A825 RID: 43045
		SHOWN,
		// Token: 0x0400A826 RID: 43046
		HIDDEN
	}
}
