using System;

namespace Hearthstone.Core
{
	[Flags]
	public enum LoadAssetFlags : byte
	{
		None = 0x0,
		FailOnError = 0x2
	}
}
