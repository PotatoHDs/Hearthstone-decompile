using PegasusShared;

public class Currency
{
	public enum Tax
	{
		TAX_INCLUDED,
		TAX_ADDED,
		NO_TAX
	}

	public static readonly Currency GTAPP = new Currency
	{
		Code = "XSG",
		ID = 19,
		SubRegion = 99,
		Symbol = "$",
		RoundingExponent = 0,
		TaxText = Tax.NO_TAX,
		ChangedVersion = 0
	};

	public string Code { get; private set; }

	public int ID { get; private set; }

	public int SubRegion { get; private set; }

	public string Symbol { get; private set; }

	public int RoundingExponent { get; private set; }

	public Tax TaxText { get; private set; }

	public int ChangedVersion { get; private set; }

	public Currency()
	{
		ID = 0;
		RoundingExponent = 0;
		TaxText = Tax.TAX_INCLUDED;
		ChangedVersion = 0;
	}

	public Currency(PegasusShared.Currency currency)
	{
		Code = currency.Code;
		ID = currency.CurrencyId;
		SubRegion = currency.SubRegionId;
		Symbol = currency.Symbol;
		RoundingExponent = currency.RoundingExponent;
		TaxText = (Tax)currency.TaxText;
		ChangedVersion = currency.ChangedVersion;
	}

	public PegasusShared.Currency toProto()
	{
		return new PegasusShared.Currency
		{
			CurrencyId = ID,
			Code = Code
		};
	}

	public string GetFormat()
	{
		return "{0:C" + RoundingExponent + "}";
	}

	public static bool IsGTAPP(string currencyCode)
	{
		return currencyCode == GTAPP.Code;
	}

	public bool IsGTAPP()
	{
		return ID == GTAPP.ID;
	}

	public ulong RoundingOffset()
	{
		ulong num = 1uL;
		for (int i = 0; i < RoundingExponent; i++)
		{
			num *= 10;
		}
		return num;
	}
}
