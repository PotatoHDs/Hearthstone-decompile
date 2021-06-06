using System;

public class GoldRewardData : RewardData
{
	public long Amount { get; set; }

	public DateTime? Date { get; set; }

	public GoldRewardData()
		: this(0L)
	{
	}

	public GoldRewardData(long amount)
		: this(amount, null)
	{
	}

	public GoldRewardData(long amount, DateTime? date)
		: this(amount, date, "", "")
	{
	}

	public GoldRewardData(long amount, DateTime? date, string nameOverride, string descriptionOverride)
		: base(Reward.Type.GOLD)
	{
		Amount = amount;
		Date = date;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	public GoldRewardData(GoldRewardData oldData)
		: base(Reward.Type.GOLD)
	{
		Amount = oldData.Amount;
		Date = oldData.Date;
		base.NameOverride = oldData.NameOverride;
		base.DescriptionOverride = oldData.DescriptionOverride;
	}

	public override string ToString()
	{
		return $"[GoldRewardData: Amount={Amount} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "GoldReward.prefab:8e5e9429ae51d8b4bac2a9fb3826e548";
	}
}
