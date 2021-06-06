using System;

namespace Hearthstone.UI.Scripting
{
	[Flags]
	public enum ScriptFeatureFlags
	{
		Conditionals = 0x1,
		Identifiers = 0x2,
		Keywords = 0x4,
		Arithmetic = 0x8,
		Relational = 0x10,
		Constants = 0x20,
		Events = 0x40,
		Tuples = 0x80,
		Methods = 0x100,
		All = 0x1FF
	}
}
