using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005E0 RID: 1504
public class TutorialKeywordTooltip : MonoBehaviour
{
	// Token: 0x06005270 RID: 21104 RVA: 0x001B12D9 File Offset: 0x001AF4D9
	public void Initialize(string keywordName, string keywordText)
	{
		this.SetName(keywordName);
		this.SetBodyText(keywordText);
		base.StartCoroutine(this.WaitAFrameBeforeSendingEvent());
	}

	// Token: 0x06005271 RID: 21105 RVA: 0x001B12F6 File Offset: 0x001AF4F6
	private IEnumerator WaitAFrameBeforeSendingEvent()
	{
		RenderUtils.SetAlpha(base.gameObject, 0f);
		yield return null;
		this.playMakerComponent.SendEvent("Birth");
		iTween.FadeTo(base.gameObject, 1f, 0.5f);
		yield break;
	}

	// Token: 0x06005272 RID: 21106 RVA: 0x001B1305 File Offset: 0x001AF505
	public void SetName(string s)
	{
		this.m_name.Text = s;
	}

	// Token: 0x06005273 RID: 21107 RVA: 0x001B1313 File Offset: 0x001AF513
	public void SetBodyText(string s)
	{
		this.m_body.Text = s;
	}

	// Token: 0x06005274 RID: 21108 RVA: 0x001B1324 File Offset: 0x001AF524
	public float GetHeight()
	{
		return base.GetComponent<Renderer>().bounds.size.z;
	}

	// Token: 0x06005275 RID: 21109 RVA: 0x001B134C File Offset: 0x001AF54C
	public float GetWidth()
	{
		return base.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x04004987 RID: 18823
	public UberText m_name;

	// Token: 0x04004988 RID: 18824
	public UberText m_body;

	// Token: 0x04004989 RID: 18825
	public PlayMakerFSM playMakerComponent;
}
