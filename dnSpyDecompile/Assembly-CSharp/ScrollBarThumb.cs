using System;

// Token: 0x02000AE1 RID: 2785
public class ScrollBarThumb : PegUIElement
{
	// Token: 0x0600946C RID: 37996 RVA: 0x00301FEF File Offset: 0x003001EF
	private void Update()
	{
		if (this.IsDragging() && UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			this.StopDragging();
		}
	}

	// Token: 0x0600946D RID: 37997 RVA: 0x0030200C File Offset: 0x0030020C
	public bool IsDragging()
	{
		return this.m_isBarDragging;
	}

	// Token: 0x0600946E RID: 37998 RVA: 0x00302014 File Offset: 0x00300214
	public void StartDragging()
	{
		this.m_isBarDragging = true;
	}

	// Token: 0x0600946F RID: 37999 RVA: 0x0030201D File Offset: 0x0030021D
	public void StopDragging()
	{
		this.m_isBarDragging = false;
	}

	// Token: 0x06009470 RID: 38000 RVA: 0x00302026 File Offset: 0x00300226
	protected override void OnDrag()
	{
		this.StartDragging();
	}

	// Token: 0x04007C62 RID: 31842
	private bool m_isBarDragging;
}
