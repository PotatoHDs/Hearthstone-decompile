using System;
using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.Core
{
	// Token: 0x02001083 RID: 4227
	public class InstantiatePrefab : IUnreliableJobDependency, IJobDependency, IAsyncJobResult
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600B6AE RID: 46766 RVA: 0x003801BE File Offset: 0x0037E3BE
		// (set) Token: 0x0600B6AF RID: 46767 RVA: 0x003801C6 File Offset: 0x0037E3C6
		public AssetReference AssetRef { get; private set; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600B6B0 RID: 46768 RVA: 0x003801CF File Offset: 0x0037E3CF
		// (set) Token: 0x0600B6B1 RID: 46769 RVA: 0x003801D7 File Offset: 0x0037E3D7
		public InstantiatePrefabFlags Flags { get; private set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600B6B2 RID: 46770 RVA: 0x003801E0 File Offset: 0x0037E3E0
		// (set) Token: 0x0600B6B3 RID: 46771 RVA: 0x003801E8 File Offset: 0x0037E3E8
		public GameObject InstantiatedPrefab { get; private set; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600B6B4 RID: 46772 RVA: 0x003801F1 File Offset: 0x0037E3F1
		public bool UsePrefabPosition
		{
			get
			{
				return this.HasFlag(InstantiatePrefabFlags.UsePrefabPosition);
			}
		}

		// Token: 0x0600B6B5 RID: 46773 RVA: 0x003801FA File Offset: 0x0037E3FA
		public InstantiatePrefab(AssetReference assetRef) : this(assetRef, InstantiatePrefabFlags.UsePrefabPosition)
		{
		}

		// Token: 0x0600B6B6 RID: 46774 RVA: 0x00380204 File Offset: 0x0037E404
		public InstantiatePrefab(AssetReference assetRef, InstantiatePrefabFlags flags)
		{
			this.AssetRef = assetRef;
			this.InstantiatedPrefab = null;
			this.Flags = flags;
		}

		// Token: 0x0600B6B7 RID: 46775 RVA: 0x00380221 File Offset: 0x0037E421
		public void OnPrefabInstantiated(AssetReference assetRef, GameObject go, object callbackData)
		{
			this.m_callbackReceived = true;
			this.InstantiatedPrefab = go;
		}

		// Token: 0x0600B6B8 RID: 46776 RVA: 0x00380231 File Offset: 0x0037E431
		public bool IsReady()
		{
			return this.m_callbackReceived && (this.InstantiatedPrefab != null || !this.HasFlag(InstantiatePrefabFlags.FailOnError));
		}

		// Token: 0x0600B6B9 RID: 46777 RVA: 0x00380257 File Offset: 0x0037E457
		public bool HasFailed()
		{
			return this.m_callbackReceived && this.InstantiatedPrefab == null && this.HasFlag(InstantiatePrefabFlags.FailOnError);
		}

		// Token: 0x0600B6BA RID: 46778 RVA: 0x00380278 File Offset: 0x0037E478
		public bool HasFlag(InstantiatePrefabFlags flag)
		{
			return (this.Flags & flag) > InstantiatePrefabFlags.None;
		}

		// Token: 0x0600B6BB RID: 46779 RVA: 0x00380285 File Offset: 0x0037E485
		public override string ToString()
		{
			return string.Format("{0}: AssetRef - {1}", base.GetType(), this.AssetRef ?? "null");
		}

		// Token: 0x040097C6 RID: 38854
		protected bool m_callbackReceived;
	}
}
