public class MountRewardData : RewardData
{
	public enum MountType
	{
		UNKNOWN,
		WOW_HEARTHSTEED,
		HEROES_MAGIC_CARPET_CARD
	}

	public MountType Mount { get; set; }

	public MountRewardData()
		: this(MountType.UNKNOWN)
	{
	}

	public MountRewardData(MountType mount)
		: base(Reward.Type.MOUNT)
	{
		Mount = mount;
	}

	public override string ToString()
	{
		return $"[MountRewardData Mount={Mount} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return Mount switch
		{
			MountType.WOW_HEARTHSTEED => "HearthSteedReward.prefab:fca8aa4ddb6e1304f8382f4091b250a0", 
			MountType.HEROES_MAGIC_CARPET_CARD => "CardMountReward.prefab:9da51e41fcae3ae46b95b4859ea85205", 
			_ => string.Empty, 
		};
	}
}
