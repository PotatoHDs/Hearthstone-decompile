using System;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.Core
{
	// Token: 0x02001084 RID: 4228
	public abstract class LoadAsset<T> : IUnreliableJobDependency, IJobDependency, IAsyncJobResult where T : UnityEngine.Object
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x0600B6BC RID: 46780 RVA: 0x003802AB File Offset: 0x0037E4AB
		// (set) Token: 0x0600B6BD RID: 46781 RVA: 0x003802B3 File Offset: 0x0037E4B3
		public AssetReference AssetRef { get; private set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600B6BE RID: 46782 RVA: 0x003802BC File Offset: 0x0037E4BC
		// (set) Token: 0x0600B6BF RID: 46783 RVA: 0x003802C4 File Offset: 0x0037E4C4
		public LoadAssetFlags Flags { get; private set; }

		// Token: 0x0600B6C0 RID: 46784 RVA: 0x003802CD File Offset: 0x0037E4CD
		public LoadAsset(AssetReference assetRef)
		{
			this.AssetRef = assetRef;
			this.Flags = LoadAssetFlags.None;
		}

		// Token: 0x0600B6C1 RID: 46785 RVA: 0x003802E3 File Offset: 0x0037E4E3
		public void OnAssetLoaded(AssetReference assetRef, AssetHandle<T> obj, object callbackData)
		{
			this.m_callbackReceived = true;
			AssetHandle.Take<T>(ref this.loadedAsset, obj);
		}

		// Token: 0x0600B6C2 RID: 46786 RVA: 0x003802F8 File Offset: 0x0037E4F8
		public bool IsReady()
		{
			return this.m_callbackReceived && (this.loadedAsset || !this.HasFlag(LoadAssetFlags.FailOnError));
		}

		// Token: 0x0600B6C3 RID: 46787 RVA: 0x0038031D File Offset: 0x0037E51D
		public bool HasFailed()
		{
			return this.m_callbackReceived && !this.loadedAsset && this.HasFlag(LoadAssetFlags.FailOnError);
		}

		// Token: 0x0600B6C4 RID: 46788 RVA: 0x0038033D File Offset: 0x0037E53D
		public bool HasFlag(LoadAssetFlags flag)
		{
			return (this.Flags & flag) > LoadAssetFlags.None;
		}

		// Token: 0x0600B6C5 RID: 46789 RVA: 0x0038034A File Offset: 0x0037E54A
		public override string ToString()
		{
			return string.Format("{0}: AssetRef - {1}", base.GetType(), this.AssetRef ?? "null");
		}

		// Token: 0x040097C8 RID: 38856
		public AssetHandle<T> loadedAsset;

		// Token: 0x040097CA RID: 38858
		protected bool m_callbackReceived;
	}
}
