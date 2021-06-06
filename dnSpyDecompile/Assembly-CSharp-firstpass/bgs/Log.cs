using System;

namespace bgs
{
	// Token: 0x02000256 RID: 598
	public class Log
	{
		// Token: 0x060024DC RID: 9436 RVA: 0x00082555 File Offset: 0x00080755
		public static Log Get()
		{
			if (Log.s_instance == null)
			{
				Log.s_instance = new Log();
				Log.s_instance.Initialize();
			}
			return Log.s_instance;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00003FD0 File Offset: 0x000021D0
		private void Initialize()
		{
		}

		// Token: 0x04000F52 RID: 3922
		public static Logger BattleNet = new Logger();

		// Token: 0x04000F53 RID: 3923
		public static Logger Party = new Logger();

		// Token: 0x04000F54 RID: 3924
		private static Log s_instance;
	}
}
