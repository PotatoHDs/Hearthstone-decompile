using System;
using System.Collections.Generic;

public interface IStore
{
	event Action OnOpened;

	event Action<StoreClosedArgs> OnClosed;

	event Action OnReady;

	event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	void Open();

	void Close();

	void BlockInterface(bool blocked);

	bool IsReady();

	bool IsOpen();

	void Unload();

	IEnumerable<CurrencyType> GetVisibleCurrencies();
}
