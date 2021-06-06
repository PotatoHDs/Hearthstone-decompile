using System;

// Token: 0x0200065D RID: 1629
public class ArcaneDustRewardData : RewardData
{
	// Token: 0x06005BD7 RID: 23511 RVA: 0x001DDF1D File Offset: 0x001DC11D
	public ArcaneDustRewardData() : this(0)
	{
	}

	// Token: 0x06005BD8 RID: 23512 RVA: 0x001DDF26 File Offset: 0x001DC126
	public ArcaneDustRewardData(int amount) : this(amount, "", "")
	{
	}

	// Token: 0x06005BD9 RID: 23513 RVA: 0x001DDF39 File Offset: 0x001DC139
	public ArcaneDustRewardData(int amount, string nameOverride, string descriptionOverride) : base(Reward.Type.ARCANE_DUST)
	{
		this.Amount = amount;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	// Token: 0x17000562 RID: 1378
	// (get) Token: 0x06005BDA RID: 23514 RVA: 0x001DDF57 File Offset: 0x001DC157
	// (set) Token: 0x06005BDB RID: 23515 RVA: 0x001DDF5F File Offset: 0x001DC15F
	public int Amount { get; set; }

	// Token: 0x06005BDC RID: 23516 RVA: 0x001DDF68 File Offset: 0x001DC168
	public override string ToString()
	{
		return string.Format("[ArcaneDustRewardData: Amount={0} Origin={1} OriginData={2}]", this.Amount, base.Origin, base.OriginData);
	}

	// Token: 0x06005BDD RID: 23517 RVA: 0x001DDF95 File Offset: 0x001DC195
	protected override string GetAssetPath()
	{
		return "ArcaneDustReward.prefab:606ad37f35d6c5642a5bd81c7f0aee77";
	}
}
