using System;

namespace Hearthstone.Core
{
	[Serializable]
	public class AssetManifest
	{
		private static IAssetManifest s_instance;

		public static IAssetManifest Get()
		{
			if (s_instance == null)
			{
				s_instance = ScriptableAssetManifest.Load();
			}
			return s_instance;
		}
	}
}
