using System;
using System.Collections.Generic;
using Blizzard.BlizzardErrorMobile;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200101B RID: 4123
	[Serializable]
	public class PrefabInstance
	{
		// Token: 0x140000AB RID: 171
		// (add) Token: 0x0600B2F7 RID: 45815 RVA: 0x003727D4 File Offset: 0x003709D4
		// (remove) Token: 0x0600B2F8 RID: 45816 RVA: 0x0037280C File Offset: 0x00370A0C
		private event Action m_loadFailed;

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x0600B2FA RID: 45818 RVA: 0x0037284A File Offset: 0x00370A4A
		// (set) Token: 0x0600B2F9 RID: 45817 RVA: 0x00372841 File Offset: 0x00370A41
		public GameObject Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				this.m_owner = value;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600B2FB RID: 45819 RVA: 0x00372852 File Offset: 0x00370A52
		public AssetHandle<GameObject> Prefab
		{
			get
			{
				return this.m_prefab;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600B2FC RID: 45820 RVA: 0x0037285A File Offset: 0x00370A5A
		public GameObject Instance
		{
			get
			{
				return this.m_cachedInstance;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600B2FD RID: 45821 RVA: 0x00372862 File Offset: 0x00370A62
		public bool IsInstanceReady
		{
			get
			{
				return this.m_cachedInstance != null;
			}
		}

		// Token: 0x0600B2FE RID: 45822 RVA: 0x00372870 File Offset: 0x00370A70
		public PrefabInstance(GameObject owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x0600B2FF RID: 45823 RVA: 0x0037287F File Offset: 0x00370A7F
		public void RegisterInstanceReadyListener(Action<object> listener)
		{
			this.m_instanceReadyState.RegisterSetListener(listener, null, true, false);
		}

		// Token: 0x0600B300 RID: 45824 RVA: 0x00372890 File Offset: 0x00370A90
		public void RegisterPrefabLoadFailedListener(Action listener)
		{
			this.m_loadFailed += listener;
		}

		// Token: 0x0600B301 RID: 45825 RVA: 0x0037289C File Offset: 0x00370A9C
		public bool LoadPrefab(WeakAssetReference assetReference, bool loadSynchronously)
		{
			if (this.m_assetReference.AssetString == assetReference.AssetString && this.m_prefab != null)
			{
				return false;
			}
			if (this.m_cachedInstance != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_cachedInstance);
			}
			this.m_assetReference = assetReference;
			AssetReference assetReference2 = AssetReference.CreateFromAssetString(assetReference.AssetString);
			if (assetReference2 == null)
			{
				AssetHandle.SafeDispose<GameObject>(ref this.m_prefab);
				return false;
			}
			this.m_wasDestroyed = false;
			if (!this.m_isLoading)
			{
				this.m_isLoading = true;
				IJobDependency[] dependencies;
				HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
				{
					typeof(IAssetLoader),
					typeof(WidgetRunner)
				});
				Processor.QueueJob(new JobDefinition("PrefabInstance.LoadGameObject", this.Job_LoadGameObject(assetReference2, loadSynchronously), JobFlags.StartImmediately, dependencies));
			}
			return true;
		}

		// Token: 0x0600B302 RID: 45826 RVA: 0x00372960 File Offset: 0x00370B60
		private IEnumerator<IAsyncJobResult> Job_LoadGameObject(AssetReference assetReference, bool loadSynchronously)
		{
			if (loadSynchronously)
			{
				IAssetLoader assetLoader = AssetLoader.Get();
				this.HandlePrefabReady(assetReference, assetLoader.LoadAsset<GameObject>(assetReference, AssetLoadingOptions.None), null);
			}
			else
			{
				LoadPrefab loadPrefab = new LoadPrefab(assetReference);
				yield return loadPrefab;
				this.HandlePrefabReady(assetReference, loadPrefab.loadedAsset, null);
				loadPrefab = null;
			}
			yield break;
		}

		// Token: 0x0600B303 RID: 45827 RVA: 0x00372980 File Offset: 0x00370B80
		public void InstantiateWhenReady()
		{
			this.m_instantiateWhenPrefabReady = true;
			this.DestroyInstance();
			if (this.m_prefab == null || this.m_owner == null)
			{
				return;
			}
			GameObject gameObject = null;
			try
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_prefab.Asset, this.m_owner.transform);
			}
			catch (Exception ex)
			{
				ExceptionReporter.Get().ReportCaughtException(ex.Message, ex.StackTrace);
				Debug.LogError(string.Concat(new string[]
				{
					string.Format("GameObject Instantiation failed in PrefabInstance.InstantiateWhenReady(), Error:{0}, ", ex),
					"Asset.Name:",
					(this.m_prefab.Asset != null) ? this.m_prefab.Asset.name : string.Empty,
					", AssetAddress:",
					this.m_prefab.AssetAddress
				}));
				return;
			}
			gameObject.transform.SetParent(this.m_owner.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.name = this.m_prefab.Asset.name;
			this.m_cachedInstance = gameObject;
			this.m_instanceReadyState.SetAndDispatch();
		}

		// Token: 0x0600B304 RID: 45828 RVA: 0x00372AB8 File Offset: 0x00370CB8
		private void HandlePrefabReady(AssetReference assetRef, AssetHandle<GameObject> prefab, object unused)
		{
			this.m_isLoading = false;
			if (this.m_wasDestroyed)
			{
				this.Destroy();
				return;
			}
			AssetHandle.Take<GameObject>(ref this.m_prefab, prefab);
			if (this.m_prefab != null)
			{
				if (this.m_instantiateWhenPrefabReady)
				{
					this.InstantiateWhenReady();
				}
				return;
			}
			Action loadFailed = this.m_loadFailed;
			if (loadFailed == null)
			{
				return;
			}
			loadFailed();
		}

		// Token: 0x0600B305 RID: 45829 RVA: 0x00372B0E File Offset: 0x00370D0E
		public void Destroy()
		{
			this.DestroyInstance();
			this.UnloadPrefab();
			this.m_wasDestroyed = true;
			this.m_instantiateWhenPrefabReady = false;
		}

		// Token: 0x0600B306 RID: 45830 RVA: 0x00372B2A File Offset: 0x00370D2A
		private void DestroyInstance()
		{
			if (this.m_cachedInstance != null)
			{
				this.m_instanceReadyState.Clear();
				UnityEngine.Object.DestroyImmediate(this.m_cachedInstance.gameObject);
			}
		}

		// Token: 0x0600B307 RID: 45831 RVA: 0x00372B55 File Offset: 0x00370D55
		private void UnloadPrefab()
		{
			AssetHandle.SafeDispose<GameObject>(ref this.m_prefab);
		}

		// Token: 0x0400964F RID: 38479
		private AssetHandle<GameObject> m_prefab;

		// Token: 0x04009650 RID: 38480
		private GameObject m_owner;

		// Token: 0x04009651 RID: 38481
		private WeakAssetReference m_assetReference;

		// Token: 0x04009652 RID: 38482
		private GameObject m_cachedInstance;

		// Token: 0x04009653 RID: 38483
		private FlagStateTracker m_instanceReadyState;

		// Token: 0x04009655 RID: 38485
		private bool m_instantiateWhenPrefabReady;

		// Token: 0x04009656 RID: 38486
		private bool m_isLoading;

		// Token: 0x04009657 RID: 38487
		private bool m_wasDestroyed;
	}
}
