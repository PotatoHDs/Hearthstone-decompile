using System;

namespace Hearthstone.UI.Editor.Tests
{
	// Token: 0x02001061 RID: 4193
	public static class TestUtils
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600B54E RID: 46414 RVA: 0x0037BEBB File Offset: 0x0037A0BB
		public static bool s_IsRunningUnitTest
		{
			get
			{
				return TestUtils.s_isRunningUnitTest;
			}
		}

		// Token: 0x0400973A RID: 38714
		private static bool s_isRunningUnitTest;
	}
}
