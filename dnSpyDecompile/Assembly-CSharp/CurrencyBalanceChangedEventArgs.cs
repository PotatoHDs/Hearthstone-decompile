using System;

// Token: 0x020006B4 RID: 1716
public class CurrencyBalanceChangedEventArgs : EventArgs
{
	// Token: 0x170005BE RID: 1470
	// (get) Token: 0x06005FEA RID: 24554 RVA: 0x001F4784 File Offset: 0x001F2984
	// (set) Token: 0x06005FEB RID: 24555 RVA: 0x001F478C File Offset: 0x001F298C
	public CurrencyType Currency { get; private set; }

	// Token: 0x170005BF RID: 1471
	// (get) Token: 0x06005FEC RID: 24556 RVA: 0x001F4795 File Offset: 0x001F2995
	// (set) Token: 0x06005FED RID: 24557 RVA: 0x001F479D File Offset: 0x001F299D
	public long OldAmount { get; private set; }

	// Token: 0x170005C0 RID: 1472
	// (get) Token: 0x06005FEE RID: 24558 RVA: 0x001F47A6 File Offset: 0x001F29A6
	// (set) Token: 0x06005FEF RID: 24559 RVA: 0x001F47AE File Offset: 0x001F29AE
	public long NewAmount { get; private set; }

	// Token: 0x06005FF0 RID: 24560 RVA: 0x001F47B7 File Offset: 0x001F29B7
	public CurrencyBalanceChangedEventArgs(CurrencyType type, long oldAmount, long newAmount)
	{
		this.Currency = type;
		this.OldAmount = oldAmount;
		this.NewAmount = newAmount;
	}
}
