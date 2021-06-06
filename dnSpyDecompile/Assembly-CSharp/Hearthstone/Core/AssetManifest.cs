using System;

namespace Hearthstone.Core
{
	// Token: 0x0200107F RID: 4223
	[Serializable]
	public class AssetManifest
	{
		// Token: 0x0600B6A2 RID: 46754 RVA: 0x003801A6 File Offset: 0x0037E3A6
		public static IAssetManifest Get()
		{
			if (AssetManifest.s_instance == null)
			{
				AssetManifest.s_instance = ScriptableAssetManifest.Load();
			}
			return AssetManifest.s_instance;
		}

		// Token: 0x040097BB RID: 38843
		private static IAssetManifest s_instance;
	}
}
