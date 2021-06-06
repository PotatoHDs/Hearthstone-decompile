using System;

// Token: 0x020006EB RID: 1771
public class BuyNoGTAPPEventArgs : BuyProductEventArgs
{
	// Token: 0x060062AF RID: 25263 RVA: 0x00202EFD File Offset: 0x002010FD
	public BuyNoGTAPPEventArgs(NoGTAPPTransactionData data)
	{
		this.transactionData = data;
		this.quantity = data.Quantity;
	}

	// Token: 0x170005DC RID: 1500
	// (get) Token: 0x060062B0 RID: 25264 RVA: 0x000052EC File Offset: 0x000034EC
	public override CurrencyType PaymentCurrency
	{
		get
		{
			return CurrencyType.GOLD;
		}
	}

	// Token: 0x040051FB RID: 20987
	public readonly NoGTAPPTransactionData transactionData;
}
