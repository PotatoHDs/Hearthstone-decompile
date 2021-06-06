using System;

// Token: 0x02000B0A RID: 2826
public class DialItem : PegUIElement
{
	// Token: 0x06009649 RID: 38473 RVA: 0x0030AB9D File Offset: 0x00308D9D
	public object GetValue()
	{
		return this.m_value;
	}

	// Token: 0x0600964A RID: 38474 RVA: 0x0030ABA5 File Offset: 0x00308DA5
	public void SetValue(object val, string text)
	{
		this.m_value = val;
		this.m_Text.Text = text;
	}

	// Token: 0x04007DFF RID: 32255
	public UberText m_Text;

	// Token: 0x04007E00 RID: 32256
	private object m_value;
}
