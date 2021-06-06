using System;

public class CurrencyBalanceChangedEventArgs : EventArgs
{
	public CurrencyType Currency { get; private set; }

	public long OldAmount { get; private set; }

	public long NewAmount { get; private set; }

	public CurrencyBalanceChangedEventArgs(CurrencyType type, long oldAmount, long newAmount)
	{
		Currency = type;
		OldAmount = oldAmount;
		NewAmount = newAmount;
	}
}
