using System;

// Token: 0x02000677 RID: 1655
public class MountRewardData : RewardData
{
	// Token: 0x06005C9B RID: 23707 RVA: 0x001E0E1D File Offset: 0x001DF01D
	public MountRewardData() : this(MountRewardData.MountType.UNKNOWN)
	{
	}

	// Token: 0x06005C9C RID: 23708 RVA: 0x001E0E26 File Offset: 0x001DF026
	public MountRewardData(MountRewardData.MountType mount) : base(Reward.Type.MOUNT)
	{
		this.Mount = mount;
	}

	// Token: 0x17000576 RID: 1398
	// (get) Token: 0x06005C9D RID: 23709 RVA: 0x001E0E36 File Offset: 0x001DF036
	// (set) Token: 0x06005C9E RID: 23710 RVA: 0x001E0E3E File Offset: 0x001DF03E
	public MountRewardData.MountType Mount { get; set; }

	// Token: 0x06005C9F RID: 23711 RVA: 0x001E0E47 File Offset: 0x001DF047
	public override string ToString()
	{
		return string.Format("[MountRewardData Mount={0} Origin={1} OriginData={2}]", this.Mount, base.Origin, base.OriginData);
	}

	// Token: 0x06005CA0 RID: 23712 RVA: 0x001E0E74 File Offset: 0x001DF074
	protected override string GetAssetPath()
	{
		MountRewardData.MountType mount = this.Mount;
		if (mount == MountRewardData.MountType.WOW_HEARTHSTEED)
		{
			return "HearthSteedReward.prefab:fca8aa4ddb6e1304f8382f4091b250a0";
		}
		if (mount != MountRewardData.MountType.HEROES_MAGIC_CARPET_CARD)
		{
			return string.Empty;
		}
		return "CardMountReward.prefab:9da51e41fcae3ae46b95b4859ea85205";
	}

	// Token: 0x0200217D RID: 8573
	public enum MountType
	{
		// Token: 0x0400E077 RID: 57463
		UNKNOWN,
		// Token: 0x0400E078 RID: 57464
		WOW_HEARTHSTEED,
		// Token: 0x0400E079 RID: 57465
		HEROES_MAGIC_CARPET_CARD
	}
}
