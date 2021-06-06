using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	[NestedReferenceScope(NestedReference.Scope.Children)]
	public class WidgetInstance : Widget, INestedReferenceResolver, IAsyncInitializationBehavior
	{
		private enum LoadingPolicy
		{
			LoadOnActivation,
			UnloadOnDeactivation,
			LoadAlways
		}

		public const int UseTemplateLayer = -1;

		public const int UseInstanceLayer = -2;

		[HideInInspector]
		[SerializeField]
		private WeakAssetReference m_widgetTemplate;

		[HideInInspector]
		[SerializeField]
		private int m_layerOverride = -1;

		[HideInInspector]
		[SerializeField]
		private LoadingPolicy m_loadingPolicy;

		private PrefabInstance m_prefabInstance;

		private bool m_startedInitialization;

		private List<Action> m_pendingPostInitializeActions;

		private List<Action> m_pendingPreInitializeActions;

		private DataContext m_pendingDataContext = new DataContext();

		private WidgetTemplate m_parentWidgetTemplate;

		private bool m_pendingWaitForParentToShow = true;

		private bool m_wasInitialized;

		private bool m_requestedPreload;

		private int? m_prevLayerOverride;

		private WidgetTemplate m_template;

		public override bool DeferredWidgetBehaviorInitialization
		{
			get
			{
				return base.DeferredWidgetBehaviorInitialization;
			}
			set
			{
				base.DeferredWidgetBehaviorInitialization = value;
				if (Widget != null)
				{
					Widget.DeferredWidgetBehaviorInitialization = value;
				}
			}
		}

		public bool WaitForParentToShow
		{
			get
			{
				if (((Widget != null && Widget.WaitForParentToShow) || (Widget == null && m_pendingWaitForParentToShow)) && m_parentWidgetTemplate != null)
				{
					return m_parentWidgetTemplate.GetInitializationState() != WidgetTemplate.InitializationState.Done;
				}
				return false;
			}
			set
			{
				if (Widget != null)
				{
					Widget.WaitForParentToShow = value;
				}
				m_pendingWaitForParentToShow = value;
			}
		}

		public bool WillPreload
		{
			get
			{
				bool flag = m_parentWidgetTemplate != null && m_parentWidgetTemplate.WillTickWhileInactive;
				if (m_loadingPolicy != LoadingPolicy.LoadAlways && !m_requestedPreload)
				{
					if (flag)
					{
						return base.gameObject.activeSelf;
					}
					return false;
				}
				return true;
			}
		}

		public bool StartedInitialization => m_startedInitialization;

		public override bool IsInitialized
		{
			get
			{
				if (Widget != null)
				{
					return Widget.IsInitialized;
				}
				return false;
			}
		}

		public WidgetTemplate Widget
		{
			get
			{
				if (m_template == null)
				{
					m_template = ((m_prefabInstance != null && m_prefabInstance?.Instance != null) ? m_prefabInstance.Instance.GetComponent<WidgetTemplate>() : null);
				}
				return m_template;
			}
		}

		public WidgetTemplate ParentWidgetTemplate
		{
			get
			{
				return m_parentWidgetTemplate;
			}
			set
			{
				m_parentWidgetTemplate = value;
				if (Widget != null)
				{
					Widget.ParentWidgetTemplate = value;
				}
			}
		}

		public override bool IsChangingStates
		{
			get
			{
				if (!(Widget == null) || !base.IsActive)
				{
					if (Widget != null)
					{
						return Widget.IsChangingStates;
					}
					return false;
				}
				return true;
			}
		}

		public override bool HasPendingActions
		{
			get
			{
				if (Widget != null)
				{
					return Widget.HasPendingActions;
				}
				return false;
			}
		}

		public override bool GetIsChangingStates(Func<GameObject, bool> includeGameObject)
		{
			if (!(Widget == null) || !base.IsActive)
			{
				if (Widget != null)
				{
					return Widget.GetIsChangingStates(includeGameObject);
				}
				return false;
			}
			return true;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (!IsInitialized)
			{
				HandleStartChangingStates(null);
			}
			if (Application.IsPlaying(this) && m_wasInitialized && m_loadingPolicy == LoadingPolicy.UnloadOnDeactivation)
			{
				Initialize();
			}
			else if (m_startedInitialization && !m_wasInitialized)
			{
				m_startedInitialization = false;
				Initialize();
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (!IsInitialized)
			{
				HandleDoneChangingStates(null);
			}
			if (Application.IsPlaying(this) && m_startedInitialization && m_loadingPolicy == LoadingPolicy.UnloadOnDeactivation)
			{
				Unload();
			}
		}

		private void Start()
		{
			if (!m_startedInitialization)
			{
				Initialize();
			}
		}

		private void OnDestroy()
		{
			if (m_prefabInstance != null)
			{
				m_prefabInstance.Destroy();
			}
		}

		public void PreInitialize()
		{
			if (!m_startedInitialization && WillPreload)
			{
				Preload();
			}
		}

		public void Initialize()
		{
			if (!m_startedInitialization)
			{
				m_wasInitialized = true;
				if (!string.IsNullOrEmpty(m_widgetTemplate.AssetString))
				{
					m_startedInitialization = true;
					m_prefabInstance = GetOrCreatePrefabInstance();
					m_prefabInstance.InstantiateWhenReady();
				}
			}
		}

		public void Preload()
		{
			m_requestedPreload = true;
			Initialize();
		}

		public void Unload()
		{
			if (m_startedInitialization)
			{
				m_startedInitialization = false;
				m_startChangingStatesEvent.Clear();
				m_doneChangingStatesEvent.Clear();
				m_pendingPostInitializeActions = null;
				m_pendingPreInitializeActions = null;
				m_pendingDataContext = new DataContext();
				DeferredWidgetBehaviorInitialization = false;
				if (Widget != null)
				{
					m_pendingWaitForParentToShow = Widget.WaitForParentToShow;
					m_template = null;
				}
				if (m_prefabInstance != null)
				{
					m_prefabInstance.Destroy();
				}
			}
		}

		public void InitializeWidgetBehaviors()
		{
			if (Widget != null)
			{
				Widget.InitializeWidgetBehaviors();
			}
			else
			{
				DeferredWidgetBehaviorInitialization = false;
			}
		}

		public Component GetComponentById(long id)
		{
			if (!(Widget != null))
			{
				return null;
			}
			return Widget.GetComponentById(id);
		}

		public bool GetComponentId(Component component, out long id)
		{
			id = -1L;
			if (Widget == null)
			{
				return false;
			}
			return Widget.GetComponentId(component, out id);
		}

		private void HandleWidgetTemplateInstantiated(object unused)
		{
			if (!(Widget != null))
			{
				return;
			}
			Widget.WillLoadSynchronously = Widget.WillLoadSynchronously || base.WillLoadSynchronously;
			Widget.RegisterStartChangingStatesListener(HandleStartChangingStates);
			Widget.RegisterDoneChangingStatesListener(HandleDoneChangingStates);
			if (m_layerOverride >= 0)
			{
				Widget.SetLayerOverride((GameLayer)m_layerOverride);
			}
			else if (m_layerOverride == -2)
			{
				Widget.SetLayerOverride((GameLayer)base.gameObject.layer);
			}
			m_pendingDataContext.Clear();
			Widget.OnInstantiated();
			if (m_pendingPreInitializeActions != null)
			{
				foreach (Action pendingPreInitializeAction in m_pendingPreInitializeActions)
				{
					pendingPreInitializeAction();
				}
			}
			Widget.RegisterReadyListener(HandleWidgetTemplateReady);
			Widget.RegisterActivatedListener(HandleWidgetTemplateActivated);
			Widget.RegisterDeactivatedListener(HandleWidgetTemplateDeactivated);
			Widget.ParentWidgetTemplate = m_parentWidgetTemplate;
			Widget.DeferredWidgetBehaviorInitialization = DeferredWidgetBehaviorInitialization;
			if (Application.IsPlaying(this))
			{
				Widget.WaitForParentToShow = m_pendingWaitForParentToShow;
			}
			if (!Application.IsPlaying(this) || WillPreload || Widget.WillLoadSynchronously)
			{
				Widget.Initialize(WillPreload);
				m_requestedPreload = false;
			}
		}

		private void HandleWidgetTemplateLoadFailed()
		{
			if (Widget == null)
			{
				Log.UIStatus.PrintError("WidgetInstance " + base.name + " failed to load its prefab " + ((!string.IsNullOrEmpty(m_widgetTemplate.AssetString)) ? m_widgetTemplate.AssetString : "ASSET REFERENCE MISSING") + ".\n Check that the widget prefab with this GUID isn't missing from the project or your widget(s) will be sad :(");
				DeferredWidgetBehaviorInitialization = false;
				m_wasInitialized = false;
				base.gameObject.SetActive(value: false);
			}
		}

		private void HandleWidgetTemplateReady(object unused)
		{
			ExecutePendingActionsAndTriggerReady();
		}

		private void HandleWidgetTemplateActivated(object unused)
		{
			if (!m_activatedEvent.IsSet)
			{
				m_deactivatedEvent.Clear();
				m_activatedEvent.SetAndDispatch();
			}
		}

		private void HandleWidgetTemplateDeactivated(object unused)
		{
			if (!m_deactivatedEvent.IsSet)
			{
				m_activatedEvent.Clear();
				m_deactivatedEvent.SetAndDispatch();
			}
		}

		private void ExecutePendingActionsAndTriggerReady()
		{
			if (m_pendingPostInitializeActions != null)
			{
				foreach (Action pendingPostInitializeAction in m_pendingPostInitializeActions)
				{
					pendingPostInitializeAction();
				}
			}
			TriggerOnReady();
		}

		public override void RegisterEventListener(EventListenerDelegate listener)
		{
			if (Widget != null)
			{
				Widget.RegisterEventListener(listener);
				return;
			}
			GetOrCreatePendingPreInitActions().Add(delegate
			{
				Widget.RegisterEventListener(listener);
			});
		}

		public override void RemoveEventListener(EventListenerDelegate listener)
		{
			if (Widget != null)
			{
				Widget.RemoveEventListener(listener);
				return;
			}
			GetOrCreatePendingPreInitActions().Add(delegate
			{
				Widget.RemoveEventListener(listener);
			});
		}

		private List<Action> GetOrCreatePendingPreInitActions()
		{
			if (m_pendingPreInitializeActions == null)
			{
				m_pendingPreInitializeActions = new List<Action>();
			}
			return m_pendingPreInitializeActions;
		}

		private void HandleStartChangingStates(object unused)
		{
			if (!m_startChangingStatesEvent.IsSet)
			{
				m_doneChangingStatesEvent.Clear();
				m_startChangingStatesEvent.SetAndDispatch();
			}
		}

		private void HandleDoneChangingStates(object unused)
		{
			if (m_startChangingStatesEvent.IsSet)
			{
				m_startChangingStatesEvent.Clear();
				m_doneChangingStatesEvent.SetAndDispatch();
			}
		}

		public override void Show()
		{
			if (Widget != null)
			{
				Widget.Show();
			}
			else
			{
				GetOrCreatePendingPreInitActions().Add(Show);
			}
		}

		public override void Hide()
		{
			if (Widget != null)
			{
				Widget.Hide();
			}
			else
			{
				GetOrCreatePendingPreInitActions().Add(Hide);
			}
		}

		public override void BindDataModel(IDataModel dataModel, bool overrideChildren = false)
		{
			if (Widget == null)
			{
				m_pendingDataContext.BindDataModel(dataModel);
				GetOrCreatePendingPreInitActions().Add(delegate
				{
					Widget.BindDataModel(dataModel, overrideChildren);
				});
			}
			else
			{
				Widget.BindDataModel(dataModel, overrideChildren);
			}
		}

		public override bool BindDataModel(IDataModel dataModel, string target, bool propagateToChildren = true, bool overrideChildren = false)
		{
			if (Widget == null)
			{
				GetOrCreatePendingPreInitActions().Add(delegate
				{
					Widget.BindDataModel(dataModel, target, propagateToChildren, overrideChildren);
				});
				return true;
			}
			return Widget.BindDataModel(dataModel, target, propagateToChildren, overrideChildren);
		}

		public override bool GetDataModel(int id, out IDataModel model)
		{
			if (Widget != null)
			{
				return Widget.GetDataModel(id, out model);
			}
			if (m_pendingDataContext.GetDataModel(id, out model))
			{
				return true;
			}
			if (m_parentWidgetTemplate != null)
			{
				m_parentWidgetTemplate.GetDataModel(id, base.gameObject, out model);
			}
			return false;
		}

		public override bool GetDataModel(int id, string targetName, out IDataModel model)
		{
			if (Widget == null)
			{
				model = null;
				return false;
			}
			return Widget.GetDataModel(id, targetName, out model);
		}

		public override void UnbindDataModel(int id)
		{
			if (Widget == null)
			{
				m_pendingDataContext.UnbindDataModel(id);
			}
			else
			{
				Widget.UnbindDataModel(id);
			}
		}

		public override bool TriggerEvent(string eventName, TriggerEventParameters parameters = default(TriggerEventParameters))
		{
			if (Widget == null)
			{
				if (m_pendingPostInitializeActions == null)
				{
					m_pendingPostInitializeActions = new List<Action>();
				}
				m_pendingPostInitializeActions.Add(delegate
				{
					Widget.TriggerEvent(eventName, parameters);
				});
				return false;
			}
			return Widget.TriggerEvent(eventName, parameters);
		}

		public override T FindWidgetComponent<T>(params string[] path)
		{
			if (!(Widget != null))
			{
				return null;
			}
			return Widget.FindWidgetComponent<T>(path);
		}

		public override Widget FindWidget(string name)
		{
			if (!(Widget != null))
			{
				return null;
			}
			return Widget.FindWidget(name);
		}

		public static WidgetInstance Create(string assetString, bool deferred = false)
		{
			GameObject obj = new GameObject(assetString);
			obj.SetActive(value: false);
			WidgetInstance widgetInstance = obj.AddComponent<WidgetInstance>();
			widgetInstance.m_widgetTemplate.AssetString = assetString;
			obj.SetActive(!deferred);
			return widgetInstance;
		}

		private PrefabInstance GetOrCreatePrefabInstance()
		{
			if (m_prefabInstance == null)
			{
				m_prefabInstance = new PrefabInstance(base.gameObject);
				m_prefabInstance.LoadPrefab(m_widgetTemplate, base.WillLoadSynchronously);
				m_prefabInstance.RegisterInstanceReadyListener(HandleWidgetTemplateInstantiated);
				m_prefabInstance.RegisterPrefabLoadFailedListener(HandleWidgetTemplateLoadFailed);
			}
			else if (m_prefabInstance.Prefab == null)
			{
				m_prefabInstance.LoadPrefab(m_widgetTemplate, base.WillLoadSynchronously);
			}
			m_prefabInstance.Owner = base.gameObject;
			return m_prefabInstance;
		}

		public override void SetLayerOverride(GameLayer layerOverride)
		{
			m_prevLayerOverride = m_layerOverride;
			m_layerOverride = (int)layerOverride;
			if (Widget != null)
			{
				Widget.SetLayerOverride(layerOverride);
			}
		}

		public override void ClearLayerOverride()
		{
			if (m_prevLayerOverride.HasValue)
			{
				m_layerOverride = m_prevLayerOverride.Value;
				m_prevLayerOverride = null;
				if (Widget != null)
				{
					Widget.ClearLayerOverride();
				}
			}
		}
	}
}
