using System;

// Token: 0x020007CB RID: 1995
public class RuleInvalidReason
{
	// Token: 0x06006DF9 RID: 28153 RVA: 0x002372DC File Offset: 0x002354DC
	public RuleInvalidReason(string error, int countParam = 0, bool isMinimum = false)
	{
		this.DisplayError = error;
		this.CountParam = countParam;
		this.IsMinimum = isMinimum;
	}

	// Token: 0x0400580F RID: 22543
	public string DisplayError;

	// Token: 0x04005810 RID: 22544
	public int CountParam;

	// Token: 0x04005811 RID: 22545
	public bool IsMinimum;
}
