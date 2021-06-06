using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.Core
{
	public abstract class LoadAsset<T> : IUnreliableJobDependency, IJobDependency, IAsyncJobResult where T : Object
	{
		public AssetHandle<T> loadedAsset;

		protected bool m_callbackReceived;

		public AssetReference AssetRef { get; private set; }

		public LoadAssetFlags Flags { get; private set; }

		public LoadAsset(AssetReference assetRef)
		{
			AssetRef = assetRef;
			Flags = LoadAssetFlags.None;
		}

		public void OnAssetLoaded(AssetReference assetRef, AssetHandle<T> obj, object callbackData)
		{
			m_callbackReceived = true;
			AssetHandle.Take(ref loadedAsset, obj);
		}

		public bool IsReady()
		{
			if (m_callbackReceived)
			{
				if (!loadedAsset)
				{
					return !HasFlag(LoadAssetFlags.FailOnError);
				}
				return true;
			}
			return false;
		}

		public bool HasFailed()
		{
			if (m_callbackReceived && !loadedAsset)
			{
				return HasFlag(LoadAssetFlags.FailOnError);
			}
			return false;
		}

		public bool HasFlag(LoadAssetFlags flag)
		{
			return (Flags & flag) != 0;
		}

		public override string ToString()
		{
			return string.Format("{0}: AssetRef - {1}", GetType(), AssetRef ?? ((AssetReference)"null"));
		}
	}
}
