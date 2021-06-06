using System;

namespace Hearthstone.UI
{
	// Token: 0x02000FE9 RID: 4073
	public class GlobalDataContext
	{
		// Token: 0x0600B14A RID: 45386 RVA: 0x0036B708 File Offset: 0x00369908
		public static DataContext Get()
		{
			if (GlobalDataContext.s_instance == null)
			{
				GlobalDataContext.s_instance = new DataContext();
			}
			return GlobalDataContext.s_instance;
		}

		// Token: 0x0400959D RID: 38301
		private static DataContext s_instance;
	}
}
