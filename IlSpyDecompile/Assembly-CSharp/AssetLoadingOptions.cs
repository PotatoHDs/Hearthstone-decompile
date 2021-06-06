using System;

[Flags]
public enum AssetLoadingOptions
{
	None = 0x0,
	DisableLocalization = 0x1,
	IgnorePrefabPosition = 0x2,
	UseLowQuality = 0x4
}
