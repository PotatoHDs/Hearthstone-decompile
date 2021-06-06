using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001001 RID: 4097
	[ExecuteAlways]
	[AddComponentMenu("")]
	[SelectionBase]
	[NestedReferenceScope(NestedReference.Scope.Children)]
	public abstract class Geometry : WidgetBehavior, INestedReferenceResolver, IAsyncInitializationBehavior
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x0600B1FB RID: 45563
		protected abstract WeakAssetReference[] ModelReferences { get; }

		// Token: 0x0600B1FC RID: 45564 RVA: 0x0036DD59 File Offset: 0x0036BF59
		protected override void Awake()
		{
			base.Awake();
		}

		// Token: 0x0600B1FD RID: 45565 RVA: 0x0036DD64 File Offset: 0x0036BF64
		protected override void OnDestroy()
		{
			if (this.m_prefabInstances != null)
			{
				PrefabInstance[] prefabInstances = this.m_prefabInstances;
				for (int i = 0; i < prefabInstances.Length; i++)
				{
					prefabInstances[i].Destroy();
				}
			}
			base.OnDestroy();
		}

		// Token: 0x0600B1FE RID: 45566 RVA: 0x0036DD9C File Offset: 0x0036BF9C
		protected override void OnInitialize()
		{
			this.InstantiateModels();
		}

		// Token: 0x0600B1FF RID: 45567 RVA: 0x0036DDA4 File Offset: 0x0036BFA4
		private void InstantiateModels()
		{
			WeakAssetReference[] modelReferences = this.ModelReferences;
			if (this.m_prefabInstances == null)
			{
				this.m_prefabInstances = new PrefabInstance[modelReferences.Length];
			}
			this.m_instancesPendingInitialization = this.m_prefabInstances.Length;
			for (int i = 0; i < this.m_prefabInstances.Length; i++)
			{
				PrefabInstance prefabInstance = this.m_prefabInstances[i];
				if (prefabInstance == null)
				{
					prefabInstance = new PrefabInstance(base.gameObject);
					this.m_prefabInstances[i] = prefabInstance;
				}
				if (!prefabInstance.IsInstanceReady)
				{
					this.m_readyState.Clear();
				}
				prefabInstance.LoadPrefab(modelReferences[i], base.WillLoadSynchronously);
				prefabInstance.InstantiateWhenReady();
				prefabInstance.RegisterInstanceReadyListener(new Action<object>(this.HandleInstanceReady));
			}
		}

		// Token: 0x0600B200 RID: 45568 RVA: 0x0036DE4F File Offset: 0x0036C04F
		private void HandleInstanceReady(object unused)
		{
			this.m_instancesPendingInitialization--;
			if (this.m_instancesPendingInitialization == 0)
			{
				this.OnInstancesReady(this.m_prefabInstances);
			}
		}

		// Token: 0x0600B201 RID: 45569 RVA: 0x0036DE73 File Offset: 0x0036C073
		protected virtual void OnInstancesReady(PrefabInstance[] instances)
		{
			this.m_readyState.SetAndDispatch();
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600B202 RID: 45570 RVA: 0x0036DE84 File Offset: 0x0036C084
		public ICollection<DynamicPropertyInfo> DynamicProperties
		{
			get
			{
				List<DynamicPropertyInfo> properties = new List<DynamicPropertyInfo>();
				this.ForEachProperty(delegate(string id, Renderer renderer)
				{
					properties.Add(new DynamicPropertyInfo
					{
						Id = id,
						Name = id,
						Type = typeof(Material),
						Value = renderer.GetSharedMaterial()
					});
				});
				return properties;
			}
		}

		// Token: 0x0600B203 RID: 45571 RVA: 0x0036DEBC File Offset: 0x0036C0BC
		private void ForEachProperty(Action<string, Renderer> callback)
		{
			for (int i = 0; i < this.m_prefabInstances.Length; i++)
			{
				PrefabInstance prefabInstance = this.m_prefabInstances[i];
				if (prefabInstance.IsInstanceReady && prefabInstance.Instance != null)
				{
					foreach (Renderer renderer in prefabInstance.Instance.GetComponentsInChildren<Renderer>(true))
					{
						string arg = string.Format("{0}.{1}", i, renderer.name);
						callback(arg, renderer);
					}
				}
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600B204 RID: 45572 RVA: 0x0036DF3E File Offset: 0x0036C13E
		public bool IsReady
		{
			get
			{
				return this.m_readyState.IsSet;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600B205 RID: 45573 RVA: 0x00005576 File Offset: 0x00003776
		public Behaviour Container
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600B206 RID: 45574 RVA: 0x0036DF4B File Offset: 0x0036C14B
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_activatedEvent.SetAndDispatch();
		}

		// Token: 0x0600B207 RID: 45575 RVA: 0x0036DF5F File Offset: 0x0036C15F
		protected override void OnDisable()
		{
			this.m_deactivatedEvent.SetAndDispatch();
			base.OnDisable();
		}

		// Token: 0x0600B208 RID: 45576 RVA: 0x0036DF73 File Offset: 0x0036C173
		public void RegisterActivatedListener(Action<object> listener, object payload = null)
		{
			this.m_activatedEvent.RegisterSetListener(listener, payload, false, false);
		}

		// Token: 0x0600B209 RID: 45577 RVA: 0x0036DF84 File Offset: 0x0036C184
		public void RegisterDeactivatedListener(Action<object> listener, object payload = null)
		{
			this.m_deactivatedEvent.RegisterSetListener(listener, payload, false, false);
		}

		// Token: 0x0600B20A RID: 45578 RVA: 0x0036DF95 File Offset: 0x0036C195
		public void RegisterReadyListener(Action<object> listener, object payload = null, bool callImmediatelyIfReady = true)
		{
			this.m_readyState.RegisterSetListener(listener, payload, callImmediatelyIfReady, false);
		}

		// Token: 0x0600B20B RID: 45579 RVA: 0x0036DFA6 File Offset: 0x0036C1A6
		public void RemoveReadyListener(Action<object> listener)
		{
			this.m_readyState.RemoveSetListener(listener);
		}

		// Token: 0x0600B20C RID: 45580 RVA: 0x0036DFB4 File Offset: 0x0036C1B4
		public Component GetComponentById(long id)
		{
			foreach (Component component in base.GetComponentsInChildren<Component>(true))
			{
				if ((long)component.ToString().GetHashCode() == id)
				{
					return component;
				}
			}
			return null;
		}

		// Token: 0x0600B20D RID: 45581 RVA: 0x0036DFED File Offset: 0x0036C1ED
		public bool GetComponentId(Component component, out long id)
		{
			id = (long)component.ToString().GetHashCode();
			return true;
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600B20E RID: 45582 RVA: 0x0036DFFE File Offset: 0x0036C1FE
		public override bool IsChangingStates
		{
			get
			{
				return !this.IsReady;
			}
		}

		// Token: 0x040095ED RID: 38381
		protected PrefabInstance[] m_prefabInstances;

		// Token: 0x040095EE RID: 38382
		private FlagStateTracker m_readyState;

		// Token: 0x040095EF RID: 38383
		private FlagStateTracker m_activatedEvent;

		// Token: 0x040095F0 RID: 38384
		private FlagStateTracker m_deactivatedEvent;

		// Token: 0x040095F1 RID: 38385
		private int m_instancesPendingInitialization;
	}
}
