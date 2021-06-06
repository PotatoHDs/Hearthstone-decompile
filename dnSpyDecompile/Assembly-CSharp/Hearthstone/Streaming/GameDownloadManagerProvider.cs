using System;

namespace Hearthstone.Streaming
{
	// Token: 0x02001070 RID: 4208
	public static class GameDownloadManagerProvider
	{
		// Token: 0x0600B5D6 RID: 46550 RVA: 0x0037D6E1 File Offset: 0x0037B8E1
		public static IGameDownloadManager Get()
		{
			if (GameDownloadManagerProvider.s_instance == null && !HearthstoneServices.TryGet<GameDownloadManager>(out GameDownloadManagerProvider.s_instance))
			{
				return null;
			}
			return GameDownloadManagerProvider.s_instance;
		}

		// Token: 0x04009773 RID: 38771
		private static GameDownloadManager s_instance;
	}
}
