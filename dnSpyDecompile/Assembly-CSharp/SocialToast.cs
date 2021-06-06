using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class SocialToast : MonoBehaviour
{
	// Token: 0x06000A94 RID: 2708 RVA: 0x0003E694 File Offset: 0x0003C894
	public void SetText(string text)
	{
		this.m_text.Text = text;
		ThreeSliceElement component = base.GetComponent<ThreeSliceElement>();
		if (component != null)
		{
			component.SetMiddleWidth(this.m_text.GetTextWorldSpaceBounds().size.x * 0.95f);
		}
	}

	// Token: 0x040006BC RID: 1724
	private const float FUDGE_FACTOR = 0.95f;

	// Token: 0x040006BD RID: 1725
	public UberText m_text;
}
