using System;

namespace Hearthstone.Core
{
	[Flags]
	public enum InstantiatePrefabFlags : byte
	{
		None = 0x0,
		UsePrefabPosition = 0x2,
		FailOnError = 0x4
	}
}
