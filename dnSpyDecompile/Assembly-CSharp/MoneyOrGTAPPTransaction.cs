using System;
using PegasusShared;

// Token: 0x02000714 RID: 1812
public class MoneyOrGTAPPTransaction
{
	// Token: 0x170005F2 RID: 1522
	// (get) Token: 0x06006521 RID: 25889 RVA: 0x0020FC78 File Offset: 0x0020DE78
	public long ID { get; }

	// Token: 0x170005F3 RID: 1523
	// (get) Token: 0x06006522 RID: 25890 RVA: 0x0020FC80 File Offset: 0x0020DE80
	public long? PMTProductID { get; }

	// Token: 0x170005F4 RID: 1524
	// (get) Token: 0x06006523 RID: 25891 RVA: 0x0020FC88 File Offset: 0x0020DE88
	public bool IsGTAPP { get; }

	// Token: 0x170005F5 RID: 1525
	// (get) Token: 0x06006524 RID: 25892 RVA: 0x0020FC90 File Offset: 0x0020DE90
	public BattlePayProvider? Provider { get; }

	// Token: 0x170005F6 RID: 1526
	// (get) Token: 0x06006525 RID: 25893 RVA: 0x0020FC98 File Offset: 0x0020DE98
	// (set) Token: 0x06006526 RID: 25894 RVA: 0x0020FCA0 File Offset: 0x0020DEA0
	public bool ClosedStore { get; set; }

	// Token: 0x06006527 RID: 25895 RVA: 0x0020FCA9 File Offset: 0x0020DEA9
	public MoneyOrGTAPPTransaction(long id, long? pmtProductID, BattlePayProvider? provider, bool isGTAPP)
	{
		this.ID = id;
		this.PMTProductID = pmtProductID;
		this.IsGTAPP = isGTAPP;
		this.Provider = provider;
		this.ClosedStore = false;
	}

	// Token: 0x06006528 RID: 25896 RVA: 0x0020FCD8 File Offset: 0x0020DED8
	public override int GetHashCode()
	{
		return this.ID.GetHashCode() * this.PMTProductID.GetHashCode();
	}

	// Token: 0x06006529 RID: 25897 RVA: 0x0020FD08 File Offset: 0x0020DF08
	public override bool Equals(object obj)
	{
		MoneyOrGTAPPTransaction moneyOrGTAPPTransaction = obj as MoneyOrGTAPPTransaction;
		if (moneyOrGTAPPTransaction == null)
		{
			return false;
		}
		bool flag = this.Provider == null || moneyOrGTAPPTransaction.Provider == null || this.Provider.Value == moneyOrGTAPPTransaction.Provider.Value;
		bool flag2;
		if (moneyOrGTAPPTransaction.ID == this.ID)
		{
			long? pmtproductID = moneyOrGTAPPTransaction.PMTProductID;
			long? pmtproductID2 = this.PMTProductID;
			flag2 = (pmtproductID.GetValueOrDefault() == pmtproductID2.GetValueOrDefault() & pmtproductID != null == (pmtproductID2 != null));
		}
		else
		{
			flag2 = false;
		}
		return flag2 && flag;
	}

	// Token: 0x0600652A RID: 25898 RVA: 0x0020FDAC File Offset: 0x0020DFAC
	public override string ToString()
	{
		return string.Format("[MoneyOrGTAPPTransaction: ID={0}, PmtProductID='{1}', IsGTAPP={2}, Provider={3}]", new object[]
		{
			this.ID,
			this.PMTProductID,
			this.IsGTAPP,
			(this.Provider != null) ? this.Provider.Value.ToString() : "UNKNOWN"
		});
	}

	// Token: 0x0600652B RID: 25899 RVA: 0x0020FE29 File Offset: 0x0020E029
	public bool ShouldShowMiniSummary()
	{
		return StoreManager.HasExternalStore || this.ClosedStore;
	}

	// Token: 0x040053E7 RID: 21479
	public static readonly BattlePayProvider? UNKNOWN_PROVIDER;

	// Token: 0x040053E8 RID: 21480
	public const int LOCKED_THIRD_PARTY_QUANTITY = 1;
}
