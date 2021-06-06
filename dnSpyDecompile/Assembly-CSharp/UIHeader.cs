using System;

// Token: 0x02000B34 RID: 2868
public class UIHeader : ThreeSliceElement
{
	// Token: 0x0600985F RID: 39007 RVA: 0x003158A8 File Offset: 0x00313AA8
	public void SetText(string t)
	{
		this.m_headerUberText.Text = t;
		base.SetMiddleWidth(this.m_headerUberText.GetTextWorldSpaceBounds().size.x);
	}

	// Token: 0x04007F6A RID: 32618
	public UberText m_headerUberText;
}
