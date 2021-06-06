using System;
using UnityEngine;

// Token: 0x02000AE4 RID: 2788
public class ThreeSliceTextElement : MonoBehaviour
{
	// Token: 0x06009482 RID: 38018 RVA: 0x0030268E File Offset: 0x0030088E
	public void SetText(string text)
	{
		this.m_text.Text = text;
		this.m_text.UpdateNow(false);
		this.Resize();
	}

	// Token: 0x06009483 RID: 38019 RVA: 0x003026AE File Offset: 0x003008AE
	public void Resize()
	{
		this.m_threeSlice.SetMiddleWidth(this.GetTextWidth());
	}

	// Token: 0x06009484 RID: 38020 RVA: 0x003026C4 File Offset: 0x003008C4
	private float GetTextWidth()
	{
		return this.m_text.GetTextBounds().size.x;
	}

	// Token: 0x04007C7B RID: 31867
	public UberText m_text;

	// Token: 0x04007C7C RID: 31868
	public ThreeSliceElement m_threeSlice;
}
