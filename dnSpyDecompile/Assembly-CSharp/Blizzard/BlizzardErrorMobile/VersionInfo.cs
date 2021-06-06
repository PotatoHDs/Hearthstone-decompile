using System;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x0200121A RID: 4634
	public static class VersionInfo
	{
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x0600D031 RID: 53297 RVA: 0x003DED26 File Offset: 0x003DCF26
		public static string VERSION
		{
			get
			{
				return string.Concat(new object[]
				{
					1,
					".",
					1,
					".",
					3
				});
			}
		}

		// Token: 0x0400A264 RID: 41572
		public const int MAJOR = 1;

		// Token: 0x0400A265 RID: 41573
		public const int MINOR = 1;

		// Token: 0x0400A266 RID: 41574
		public const int PACTH = 3;
	}
}
