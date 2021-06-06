using System;
using Hearthstone.UI.Logging.Internal;

namespace Hearthstone.UI.Logging
{
	// Token: 0x0200105A RID: 4186
	public static class Log
	{
		// Token: 0x0600B537 RID: 46391 RVA: 0x0037BA6E File Offset: 0x00379C6E
		public static ILog Get()
		{
			if (Log.s_instance == null)
			{
				Log.s_instance = new RuntimeLog();
			}
			return Log.s_instance;
		}

		// Token: 0x04009727 RID: 38695
		private static ILog s_instance;
	}
}
