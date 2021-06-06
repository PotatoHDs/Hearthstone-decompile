using System;
using System.Collections.Generic;

// Token: 0x0200032F RID: 815
public class MultiColumnTooltipPanel : ResizableTooltipPanel
{
	// Token: 0x06002E54 RID: 11860 RVA: 0x000EC35C File Offset: 0x000EA55C
	public override void Initialize(string keywordName, string keywordText)
	{
		base.Initialize(keywordName, keywordText);
		float num = 0f;
		foreach (UberText uberText in this.m_textColumns)
		{
			if (uberText != null && uberText.Height > num)
			{
				num = uberText.Height;
			}
		}
		float backgroundSize = (this.m_name.Height + this.m_bodyTextHeight + num) * this.m_heightPadding;
		base.SetBackgroundSize(backgroundSize);
	}

	// Token: 0x040019C0 RID: 6592
	public List<UberText> m_textColumns = new List<UberText>();
}
