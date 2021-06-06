using PegasusShared;

public class MoneyOrGTAPPTransaction
{
	public static readonly BattlePayProvider? UNKNOWN_PROVIDER;

	public const int LOCKED_THIRD_PARTY_QUANTITY = 1;

	public long ID { get; }

	public long? PMTProductID { get; }

	public bool IsGTAPP { get; }

	public BattlePayProvider? Provider { get; }

	public bool ClosedStore { get; set; }

	public MoneyOrGTAPPTransaction(long id, long? pmtProductID, BattlePayProvider? provider, bool isGTAPP)
	{
		ID = id;
		PMTProductID = pmtProductID;
		IsGTAPP = isGTAPP;
		Provider = provider;
		ClosedStore = false;
	}

	public override int GetHashCode()
	{
		return ID.GetHashCode() * PMTProductID.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		MoneyOrGTAPPTransaction moneyOrGTAPPTransaction = obj as MoneyOrGTAPPTransaction;
		if (moneyOrGTAPPTransaction == null)
		{
			return false;
		}
		bool flag = false;
		flag = !Provider.HasValue || !moneyOrGTAPPTransaction.Provider.HasValue || Provider.Value == moneyOrGTAPPTransaction.Provider.Value;
		return moneyOrGTAPPTransaction.ID == ID && moneyOrGTAPPTransaction.PMTProductID == PMTProductID && flag;
	}

	public override string ToString()
	{
		return string.Format("[MoneyOrGTAPPTransaction: ID={0}, PmtProductID='{1}', IsGTAPP={2}, Provider={3}]", ID, PMTProductID, IsGTAPP, Provider.HasValue ? Provider.Value.ToString() : "UNKNOWN");
	}

	public bool ShouldShowMiniSummary()
	{
		if (StoreManager.HasExternalStore)
		{
			return true;
		}
		return ClosedStore;
	}
}
