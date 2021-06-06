using System;
using PegasusShared;

// Token: 0x020006EE RID: 1774
public class Currency
{
	// Token: 0x060062C5 RID: 25285 RVA: 0x002032F2 File Offset: 0x002014F2
	public Currency()
	{
		this.ID = 0;
		this.RoundingExponent = 0;
		this.TaxText = global::Currency.Tax.TAX_INCLUDED;
		this.ChangedVersion = 0;
	}

	// Token: 0x060062C6 RID: 25286 RVA: 0x00203318 File Offset: 0x00201518
	public Currency(PegasusShared.Currency currency)
	{
		this.Code = currency.Code;
		this.ID = currency.CurrencyId;
		this.SubRegion = currency.SubRegionId;
		this.Symbol = currency.Symbol;
		this.RoundingExponent = currency.RoundingExponent;
		this.TaxText = (global::Currency.Tax)currency.TaxText;
		this.ChangedVersion = currency.ChangedVersion;
	}

	// Token: 0x060062C7 RID: 25287 RVA: 0x0020337F File Offset: 0x0020157F
	public PegasusShared.Currency toProto()
	{
		return new PegasusShared.Currency
		{
			CurrencyId = this.ID,
			Code = this.Code
		};
	}

	// Token: 0x060062C8 RID: 25288 RVA: 0x002033A0 File Offset: 0x002015A0
	public string GetFormat()
	{
		return "{0:C" + this.RoundingExponent.ToString() + "}";
	}

	// Token: 0x060062C9 RID: 25289 RVA: 0x002033CA File Offset: 0x002015CA
	public static bool IsGTAPP(string currencyCode)
	{
		return currencyCode == global::Currency.GTAPP.Code;
	}

	// Token: 0x060062CA RID: 25290 RVA: 0x002033DC File Offset: 0x002015DC
	public bool IsGTAPP()
	{
		return this.ID == global::Currency.GTAPP.ID;
	}

	// Token: 0x060062CB RID: 25291 RVA: 0x002033F0 File Offset: 0x002015F0
	public ulong RoundingOffset()
	{
		ulong num = 1UL;
		for (int i = 0; i < this.RoundingExponent; i++)
		{
			num *= 10UL;
		}
		return num;
	}

	// Token: 0x170005E0 RID: 1504
	// (get) Token: 0x060062CC RID: 25292 RVA: 0x00203418 File Offset: 0x00201618
	// (set) Token: 0x060062CD RID: 25293 RVA: 0x00203420 File Offset: 0x00201620
	public string Code { get; private set; }

	// Token: 0x170005E1 RID: 1505
	// (get) Token: 0x060062CE RID: 25294 RVA: 0x00203429 File Offset: 0x00201629
	// (set) Token: 0x060062CF RID: 25295 RVA: 0x00203431 File Offset: 0x00201631
	public int ID { get; private set; }

	// Token: 0x170005E2 RID: 1506
	// (get) Token: 0x060062D0 RID: 25296 RVA: 0x0020343A File Offset: 0x0020163A
	// (set) Token: 0x060062D1 RID: 25297 RVA: 0x00203442 File Offset: 0x00201642
	public int SubRegion { get; private set; }

	// Token: 0x170005E3 RID: 1507
	// (get) Token: 0x060062D2 RID: 25298 RVA: 0x0020344B File Offset: 0x0020164B
	// (set) Token: 0x060062D3 RID: 25299 RVA: 0x00203453 File Offset: 0x00201653
	public string Symbol { get; private set; }

	// Token: 0x170005E4 RID: 1508
	// (get) Token: 0x060062D4 RID: 25300 RVA: 0x0020345C File Offset: 0x0020165C
	// (set) Token: 0x060062D5 RID: 25301 RVA: 0x00203464 File Offset: 0x00201664
	public int RoundingExponent { get; private set; }

	// Token: 0x170005E5 RID: 1509
	// (get) Token: 0x060062D6 RID: 25302 RVA: 0x0020346D File Offset: 0x0020166D
	// (set) Token: 0x060062D7 RID: 25303 RVA: 0x00203475 File Offset: 0x00201675
	public global::Currency.Tax TaxText { get; private set; }

	// Token: 0x170005E6 RID: 1510
	// (get) Token: 0x060062D8 RID: 25304 RVA: 0x0020347E File Offset: 0x0020167E
	// (set) Token: 0x060062D9 RID: 25305 RVA: 0x00203486 File Offset: 0x00201686
	public int ChangedVersion { get; private set; }

	// Token: 0x04005202 RID: 20994
	public static readonly global::Currency GTAPP = new global::Currency
	{
		Code = "XSG",
		ID = 19,
		SubRegion = 99,
		Symbol = "$",
		RoundingExponent = 0,
		TaxText = global::Currency.Tax.NO_TAX,
		ChangedVersion = 0
	};

	// Token: 0x02002252 RID: 8786
	public enum Tax
	{
		// Token: 0x0400E331 RID: 58161
		TAX_INCLUDED,
		// Token: 0x0400E332 RID: 58162
		TAX_ADDED,
		// Token: 0x0400E333 RID: 58163
		NO_TAX
	}
}
