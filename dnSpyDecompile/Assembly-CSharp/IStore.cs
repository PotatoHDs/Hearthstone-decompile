using System;
using System.Collections.Generic;

// Token: 0x02000708 RID: 1800
public interface IStore
{
	// Token: 0x14000051 RID: 81
	// (add) Token: 0x060064B9 RID: 25785
	// (remove) Token: 0x060064BA RID: 25786
	event Action OnOpened;

	// Token: 0x14000052 RID: 82
	// (add) Token: 0x060064BB RID: 25787
	// (remove) Token: 0x060064BC RID: 25788
	event Action<StoreClosedArgs> OnClosed;

	// Token: 0x14000053 RID: 83
	// (add) Token: 0x060064BD RID: 25789
	// (remove) Token: 0x060064BE RID: 25790
	event Action OnReady;

	// Token: 0x14000054 RID: 84
	// (add) Token: 0x060064BF RID: 25791
	// (remove) Token: 0x060064C0 RID: 25792
	event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	// Token: 0x060064C1 RID: 25793
	void Open();

	// Token: 0x060064C2 RID: 25794
	void Close();

	// Token: 0x060064C3 RID: 25795
	void BlockInterface(bool blocked);

	// Token: 0x060064C4 RID: 25796
	bool IsReady();

	// Token: 0x060064C5 RID: 25797
	bool IsOpen();

	// Token: 0x060064C6 RID: 25798
	void Unload();

	// Token: 0x060064C7 RID: 25799
	IEnumerable<CurrencyType> GetVisibleCurrencies();
}
