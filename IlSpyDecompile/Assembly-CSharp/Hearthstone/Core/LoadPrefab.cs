using UnityEngine;

namespace Hearthstone.Core
{
	public class LoadPrefab : LoadAsset<GameObject>
	{
		public LoadPrefab(AssetReference assetRef)
			: base(assetRef)
		{
		}
	}
}
