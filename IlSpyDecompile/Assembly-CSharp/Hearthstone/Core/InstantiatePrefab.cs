using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.Core
{
	public class InstantiatePrefab : IUnreliableJobDependency, IJobDependency, IAsyncJobResult
	{
		protected bool m_callbackReceived;

		public AssetReference AssetRef { get; private set; }

		public InstantiatePrefabFlags Flags { get; private set; }

		public GameObject InstantiatedPrefab { get; private set; }

		public bool UsePrefabPosition => HasFlag(InstantiatePrefabFlags.UsePrefabPosition);

		public InstantiatePrefab(AssetReference assetRef)
			: this(assetRef, InstantiatePrefabFlags.UsePrefabPosition)
		{
		}

		public InstantiatePrefab(AssetReference assetRef, InstantiatePrefabFlags flags)
		{
			AssetRef = assetRef;
			InstantiatedPrefab = null;
			Flags = flags;
		}

		public void OnPrefabInstantiated(AssetReference assetRef, GameObject go, object callbackData)
		{
			m_callbackReceived = true;
			InstantiatedPrefab = go;
		}

		public bool IsReady()
		{
			if (m_callbackReceived)
			{
				if (!(InstantiatedPrefab != null))
				{
					return !HasFlag(InstantiatePrefabFlags.FailOnError);
				}
				return true;
			}
			return false;
		}

		public bool HasFailed()
		{
			if (m_callbackReceived && InstantiatedPrefab == null)
			{
				return HasFlag(InstantiatePrefabFlags.FailOnError);
			}
			return false;
		}

		public bool HasFlag(InstantiatePrefabFlags flag)
		{
			return (Flags & flag) != 0;
		}

		public override string ToString()
		{
			return string.Format("{0}: AssetRef - {1}", GetType(), AssetRef ?? ((AssetReference)"null"));
		}
	}
}
