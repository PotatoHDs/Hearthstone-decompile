using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	[SelectionBase]
	[NestedReferenceScope(NestedReference.Scope.Children)]
	public abstract class Geometry : WidgetBehavior, INestedReferenceResolver, IAsyncInitializationBehavior
	{
		protected PrefabInstance[] m_prefabInstances;

		private FlagStateTracker m_readyState;

		private FlagStateTracker m_activatedEvent;

		private FlagStateTracker m_deactivatedEvent;

		private int m_instancesPendingInitialization;

		protected abstract WeakAssetReference[] ModelReferences { get; }

		public ICollection<DynamicPropertyInfo> DynamicProperties
		{
			get
			{
				List<DynamicPropertyInfo> properties = new List<DynamicPropertyInfo>();
				ForEachProperty(delegate(string id, Renderer renderer)
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

		public bool IsReady => m_readyState.IsSet;

		public Behaviour Container => this;

		public override bool IsChangingStates => !IsReady;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void OnDestroy()
		{
			if (m_prefabInstances != null)
			{
				PrefabInstance[] prefabInstances = m_prefabInstances;
				for (int i = 0; i < prefabInstances.Length; i++)
				{
					prefabInstances[i].Destroy();
				}
			}
			base.OnDestroy();
		}

		protected override void OnInitialize()
		{
			InstantiateModels();
		}

		private void InstantiateModels()
		{
			WeakAssetReference[] modelReferences = ModelReferences;
			if (m_prefabInstances == null)
			{
				m_prefabInstances = new PrefabInstance[modelReferences.Length];
			}
			m_instancesPendingInitialization = m_prefabInstances.Length;
			for (int i = 0; i < m_prefabInstances.Length; i++)
			{
				PrefabInstance prefabInstance = m_prefabInstances[i];
				if (prefabInstance == null)
				{
					prefabInstance = new PrefabInstance(base.gameObject);
					m_prefabInstances[i] = prefabInstance;
				}
				if (!prefabInstance.IsInstanceReady)
				{
					m_readyState.Clear();
				}
				prefabInstance.LoadPrefab(modelReferences[i], base.WillLoadSynchronously);
				prefabInstance.InstantiateWhenReady();
				prefabInstance.RegisterInstanceReadyListener(HandleInstanceReady);
			}
		}

		private void HandleInstanceReady(object unused)
		{
			m_instancesPendingInitialization--;
			if (m_instancesPendingInitialization == 0)
			{
				OnInstancesReady(m_prefabInstances);
			}
		}

		protected virtual void OnInstancesReady(PrefabInstance[] instances)
		{
			m_readyState.SetAndDispatch();
		}

		private void ForEachProperty(Action<string, Renderer> callback)
		{
			for (int i = 0; i < m_prefabInstances.Length; i++)
			{
				PrefabInstance prefabInstance = m_prefabInstances[i];
				if (prefabInstance.IsInstanceReady && prefabInstance.Instance != null)
				{
					Renderer[] componentsInChildren = prefabInstance.Instance.GetComponentsInChildren<Renderer>(includeInactive: true);
					foreach (Renderer renderer in componentsInChildren)
					{
						string arg = $"{i}.{renderer.name}";
						callback(arg, renderer);
					}
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_activatedEvent.SetAndDispatch();
		}

		protected override void OnDisable()
		{
			m_deactivatedEvent.SetAndDispatch();
			base.OnDisable();
		}

		public void RegisterActivatedListener(Action<object> listener, object payload = null)
		{
			m_activatedEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet: false);
		}

		public void RegisterDeactivatedListener(Action<object> listener, object payload = null)
		{
			m_deactivatedEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet: false);
		}

		public void RegisterReadyListener(Action<object> listener, object payload = null, bool callImmediatelyIfReady = true)
		{
			m_readyState.RegisterSetListener(listener, payload, callImmediatelyIfReady);
		}

		public void RemoveReadyListener(Action<object> listener)
		{
			m_readyState.RemoveSetListener(listener);
		}

		public Component GetComponentById(long id)
		{
			Component[] componentsInChildren = GetComponentsInChildren<Component>(includeInactive: true);
			foreach (Component component in componentsInChildren)
			{
				if (component.ToString().GetHashCode() == id)
				{
					return component;
				}
			}
			return null;
		}

		public bool GetComponentId(Component component, out long id)
		{
			id = component.ToString().GetHashCode();
			return true;
		}
	}
}
