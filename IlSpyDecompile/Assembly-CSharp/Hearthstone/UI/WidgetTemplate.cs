using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class WidgetTemplate : Widget, ISerializationCallbackReceiver, IWidgetEventListener
	{
		public enum InitializationState
		{
			NotStarted,
			InitializingWidget,
			InitializingWidgetBehaviors,
			Done
		}

		private enum DataChangeSource
		{
			Parent,
			Template,
			GameObjectBinding,
			Global
		}

		[Flags]
		public enum UpdateTargets
		{
			None = 0x0,
			Children = 0x2,
			Behaviors = 0x4,
			All = 0x6
		}

		private class GameObjectBinding
		{
			public delegate void DataChangedDelegate(IDataModel dataModel, GameObjectBinding binding);

			private DataContext m_dataContext;

			private HashSet<int> m_ownedDataModels = new HashSet<int>();

			private DataChangedDelegate m_onDataChanged;

			public DataContext DataContext => m_dataContext;

			public GameObjectBinding(DataContext dataContext)
			{
				m_dataContext = dataContext;
				m_dataContext.RegisterChangedListener(HandleDataContextChanged);
			}

			public void BindDataModel(IDataModel dataModel, bool owned)
			{
				if (owned)
				{
					m_ownedDataModels.Add(dataModel.DataModelId);
				}
				else
				{
					m_ownedDataModels.Remove(dataModel.DataModelId);
				}
				m_dataContext.BindDataModel(dataModel);
			}

			public void UnbindDataModel(int dataModelId)
			{
				m_dataContext.UnbindDataModel(dataModelId);
				m_ownedDataModels.Remove(dataModelId);
			}

			public bool OwnsDataModel(IDataModel dataModel)
			{
				return m_ownedDataModels.Contains(dataModel.DataModelId);
			}

			public void RegisterChangedListener(DataChangedDelegate listener)
			{
				m_onDataChanged = (DataChangedDelegate)Delegate.Remove(m_onDataChanged, listener);
				m_onDataChanged = (DataChangedDelegate)Delegate.Combine(m_onDataChanged, listener);
			}

			public void RemoveChangedListener(DataChangedDelegate listener)
			{
				m_onDataChanged = (DataChangedDelegate)Delegate.Remove(m_onDataChanged, listener);
			}

			private void HandleDataContextChanged(IDataModel dataModel)
			{
				m_onDataChanged?.Invoke(dataModel, this);
			}
		}

		[Serializable]
		private struct KeyValuePair
		{
			public class Comparer : IEqualityComparer<KeyValuePair>
			{
				public bool Equals(KeyValuePair x, KeyValuePair y)
				{
					return x.Value == y.Value;
				}

				public int GetHashCode(KeyValuePair obj)
				{
					return obj.Key.GetHashCode();
				}
			}

			public long Key;

			public Component Value;
		}

		[HideInInspector]
		[SerializeField]
		private List<KeyValuePair> m_pairs;

		private HashSet<Component> m_componentsSet;

		private Map<long, Component> m_componentsById;

		private int m_numComponentsPendingInitialization;

		private DataContext m_dataContext = new DataContext();

		private List<WidgetBehavior> m_widgetBehaviors;

		private List<WidgetInstance> m_nestedInstances;

		private List<WidgetInstance> m_addedInstances;

		private List<VisualController> m_visualControllers;

		private List<IAsyncInitializationBehavior> m_deactivatedComponents;

		private Map<GameObject, GameObjectBinding> m_gameObjectsToBindingsMap;

		private List<GameObject> m_newlyBoundGameObjects;

		private InitializationState m_initializationState;

		private WidgetTemplate m_parentWidgetTemplate;

		private float m_initializationStartTime;

		private bool m_waitForParentToShow = true;

		private UpdateTargets m_updateTargets = UpdateTargets.All;

		private HashSet<WidgetTemplate> m_widgetsPendingTick;

		private HashSet<WidgetTemplate> m_widgetsToTickThisIteration;

		private List<IStatefulWidgetComponent> m_componentsChangingStates = new List<IStatefulWidgetComponent>();

		private bool m_enabledInternally;

		private bool m_willTickWhileInactive;

		private bool m_changingStatesInternally;

		private Map<GameObject, int> m_prevLayersByObject;

		[HideInInspector]
		[SerializeField]
		private List<int> m_dataModelHints_editorOnly;

		private Map<Renderer, int> m_originalRendererLayers;

		public List<int> DataModelHints_EditorOnly => m_dataModelHints_editorOnly;

		public bool WillTickWhileInactive => m_willTickWhileInactive;

		public bool WaitForParentToShow
		{
			get
			{
				if (m_waitForParentToShow && !m_willTickWhileInactive && m_parentWidgetTemplate != null)
				{
					return m_parentWidgetTemplate.GetInitializationState() != InitializationState.Done;
				}
				return false;
			}
			set
			{
				m_waitForParentToShow = value;
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
			}
		}

		public DataContext DataContext => m_dataContext;

		public int DataVersion { get; set; } = 1;


		public override bool IsInitialized => m_initializationState == InitializationState.Done;

		public override bool IsChangingStates
		{
			get
			{
				if (!base.IsActive)
				{
					return false;
				}
				if (m_initializationState <= InitializationState.InitializingWidget)
				{
					return true;
				}
				if (m_visualControllers != null)
				{
					foreach (VisualController visualController in m_visualControllers)
					{
						if (visualController.IsChangingStates)
						{
							return true;
						}
					}
				}
				if (m_nestedInstances != null)
				{
					foreach (WidgetInstance nestedInstance in m_nestedInstances)
					{
						if (nestedInstance.IsChangingStates)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public override bool HasPendingActions
		{
			get
			{
				if (!base.IsActive)
				{
					return false;
				}
				if (m_visualControllers != null)
				{
					foreach (VisualController visualController in m_visualControllers)
					{
						if (visualController.HasPendingActions)
						{
							return true;
						}
					}
				}
				if (m_nestedInstances != null)
				{
					foreach (WidgetInstance nestedInstance in m_nestedInstances)
					{
						if (nestedInstance.HasPendingActions)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public bool IsDesiredHiddenInHierarchy
		{
			get
			{
				if (IsDesiredHidden)
				{
					return true;
				}
				WidgetTemplate parentWidgetTemplate = ParentWidgetTemplate;
				while (parentWidgetTemplate != null)
				{
					if (parentWidgetTemplate.IsDesiredHidden)
					{
						return true;
					}
					parentWidgetTemplate = parentWidgetTemplate.ParentWidgetTemplate;
				}
				return false;
			}
		}

		public bool IsDesiredHidden { get; private set; }

		private bool ListeningToNestedStateChanges
		{
			get
			{
				if (m_visualControllers == null || m_visualControllers.Count <= 0)
				{
					if (m_nestedInstances != null)
					{
						return m_nestedInstances.Count > 0;
					}
					return false;
				}
				return true;
			}
		}

		protected bool CanSendStateChanges
		{
			get
			{
				if (!m_enabledInternally)
				{
					return m_willTickWhileInactive;
				}
				return true;
			}
		}

		public WidgetTemplate OwningWidget => this;

		private event EventListenerDelegate m_eventListeners;

		public InitializationState GetInitializationState()
		{
			return m_initializationState;
		}

		public override bool GetIsChangingStates(Func<GameObject, bool> includeGameObject)
		{
			if (!base.IsActive)
			{
				return false;
			}
			if (!includeGameObject(base.gameObject))
			{
				return false;
			}
			if (m_initializationState <= InitializationState.InitializingWidget)
			{
				return true;
			}
			if (m_visualControllers != null)
			{
				foreach (VisualController visualController in m_visualControllers)
				{
					if (visualController.IsChangingStates)
					{
						return true;
					}
				}
			}
			if (m_nestedInstances != null)
			{
				foreach (WidgetInstance nestedInstance in m_nestedInstances)
				{
					if (nestedInstance.GetIsChangingStates(includeGameObject))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void OnInstantiated()
		{
			if (Application.IsPlaying(this))
			{
				ShowOrHide(show: false, recursive: false);
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_enabledInternally = true;
			if (m_changingStatesInternally && !m_willTickWhileInactive && !m_startChangingStatesEvent.IsSet)
			{
				m_doneChangingStatesEvent.Clear();
				m_startChangingStatesEvent.SetAndDispatch();
			}
		}

		protected override void OnDisable()
		{
			m_enabledInternally = false;
			base.OnDisable();
			if (m_changingStatesInternally && !m_willTickWhileInactive && m_startChangingStatesEvent.IsSet)
			{
				m_startChangingStatesEvent.Clear();
				m_doneChangingStatesEvent.SetAndDispatch();
			}
		}

		private void Start()
		{
			Initialize();
		}

		private void OnDestroy()
		{
			if (m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in m_widgetBehaviors)
				{
					widgetBehavior.RemoveStartChangingStatesListener(HandleStartChangingStates);
					widgetBehavior.RemoveDoneChangingStatesListener(HandleDoneChangingStates);
				}
			}
			if (m_nestedInstances != null)
			{
				foreach (WidgetInstance nestedInstance in m_nestedInstances)
				{
					nestedInstance.ParentWidgetTemplate = this;
					nestedInstance.RemoveStartChangingStatesListener(HandleStartChangingStates);
					nestedInstance.RemoveDoneChangingStatesListener(HandleDoneChangingStates);
				}
			}
			if (HearthstoneServices.Get<WidgetRunner>() != null)
			{
				HearthstoneServices.Get<WidgetRunner>().UnregisterWidget(this);
			}
		}

		private void PreInitialize()
		{
			if (m_dataContext != null)
			{
				m_dataContext.RegisterChangedListener(HandleDataChanged);
				GlobalDataContext.Get().RegisterChangedListener(HandleGlobalDataChanged);
			}
			if (m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in m_widgetBehaviors)
				{
					widgetBehavior.RegisterStartChangingStatesListener(HandleStartChangingStates, widgetBehavior);
					widgetBehavior.RegisterDoneChangingStatesListener(HandleDoneChangingStates, widgetBehavior, callImmediatelyIfSet: false);
				}
			}
			if (m_nestedInstances == null)
			{
				return;
			}
			foreach (WidgetInstance nestedInstance in m_nestedInstances)
			{
				nestedInstance.ParentWidgetTemplate = this;
				nestedInstance.RegisterStartChangingStatesListener(HandleStartChangingStates, nestedInstance);
				nestedInstance.RegisterDoneChangingStatesListener(HandleDoneChangingStates, nestedInstance, callImmediatelyIfSet: false);
				nestedInstance.PreInitialize();
			}
		}

		public void Initialize(bool shouldPreload = false)
		{
			if (m_initializationState == InitializationState.NotStarted)
			{
				m_willTickWhileInactive = shouldPreload;
				m_initializationState = InitializationState.InitializingWidget;
				PreInitialize();
				HandleStartChangingStates(this);
				HearthstoneServices.InitializeDynamicServicesIfEditor(out var serviceDependencies, typeof(IAssetLoader), typeof(WidgetRunner));
				Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("WidgetTemplate.Initialize", InitializeInternal, JobFlags.StartImmediately, serviceDependencies));
			}
		}

		private void InitializeInternal()
		{
			if (m_parentWidgetTemplate == null)
			{
				HearthstoneServices.Get<WidgetRunner>().RegisterWidget(this);
			}
			m_initializationStartTime = Time.realtimeSinceStartup;
			List<IAsyncInitializationBehavior> list = new List<IAsyncInitializationBehavior>();
			if (m_pairs != null)
			{
				foreach (KeyValuePair pair in m_pairs)
				{
					IAsyncInitializationBehavior asyncInitializationBehavior = pair.Value as IAsyncInitializationBehavior;
					if (asyncInitializationBehavior != null && asyncInitializationBehavior.Container != this && !asyncInitializationBehavior.IsReady && asyncInitializationBehavior.IsActive)
					{
						list.Add(asyncInitializationBehavior);
					}
				}
			}
			if (m_nestedInstances != null)
			{
				foreach (WidgetInstance nestedInstance in m_nestedInstances)
				{
					nestedInstance.WillLoadSynchronously = nestedInstance.WillLoadSynchronously || base.WillLoadSynchronously;
					if (Application.IsPlaying(this) && nestedInstance.IsActive)
					{
						nestedInstance.DeferredWidgetBehaviorInitialization = true;
					}
				}
			}
			m_numComponentsPendingInitialization = list.Count;
			if (m_numComponentsPendingInitialization > 0)
			{
				foreach (IAsyncInitializationBehavior item in list)
				{
					item.RegisterActivatedListener(HandleComponentActivated, item);
					item.RegisterDeactivatedListener(HandleComponentDeactivated, item);
					item.RegisterReadyListener(HandleComponentReady, item);
				}
			}
			else
			{
				HandleAllAsyncBehaviorsReady();
			}
		}

		private void HandleAllAsyncBehaviorsReady()
		{
			TriggerOnReady();
			if (!DeferredWidgetBehaviorInitialization)
			{
				InitializeWidgetBehaviors();
			}
		}

		public void ResetUpdateTargets()
		{
			m_updateTargets = UpdateTargets.All;
			if (m_nestedInstances == null)
			{
				return;
			}
			foreach (WidgetInstance nestedInstance in m_nestedInstances)
			{
				if (nestedInstance != null && nestedInstance.Widget != null)
				{
					nestedInstance.Widget.ResetUpdateTargets();
				}
			}
		}

		private void AddUpdateTarget(UpdateTargets flag)
		{
			m_updateTargets |= flag;
		}

		private void ClearUpdateTarget(UpdateTargets flag)
		{
			m_updateTargets &= ~flag;
		}

		private bool ShouldUpdateTarget(UpdateTargets flag)
		{
			return (m_updateTargets & flag) != 0;
		}

		public void Tick()
		{
			if (m_nestedInstances != null && ShouldUpdateTarget(UpdateTargets.Children))
			{
				ClearUpdateTarget(UpdateTargets.Children);
				if (m_widgetsToTickThisIteration == null)
				{
					m_widgetsToTickThisIteration = new HashSet<WidgetTemplate>();
				}
				foreach (WidgetInstance nestedInstance in m_nestedInstances)
				{
					if (nestedInstance != null && nestedInstance.Widget != null)
					{
						m_widgetsToTickThisIteration.Add(nestedInstance.Widget);
					}
				}
			}
			if (m_widgetsPendingTick != null)
			{
				if (m_widgetsToTickThisIteration == null)
				{
					m_widgetsToTickThisIteration = new HashSet<WidgetTemplate>();
				}
				foreach (WidgetTemplate item in m_widgetsPendingTick)
				{
					if (item != null)
					{
						m_widgetsToTickThisIteration.Add(item);
					}
				}
				m_widgetsPendingTick.Clear();
			}
			if (m_widgetsToTickThisIteration != null)
			{
				foreach (WidgetTemplate item2 in m_widgetsToTickThisIteration)
				{
					if (item2 != null)
					{
						item2.Tick();
					}
				}
			}
			if (ShouldUpdateTarget(UpdateTargets.Behaviors) && m_widgetBehaviors != null)
			{
				ClearUpdateTarget(UpdateTargets.Behaviors);
				foreach (WidgetBehavior widgetBehavior in m_widgetBehaviors)
				{
					if (widgetBehavior != null && widgetBehavior.CanTick)
					{
						widgetBehavior.OnUpdate();
					}
				}
			}
			if (m_deactivatedComponents != null && m_deactivatedComponents.Count > 0 && m_initializationState <= InitializationState.InitializingWidget)
			{
				for (int num = m_deactivatedComponents.Count - 1; num >= 0; num--)
				{
					IAsyncInitializationBehavior asyncBehavior = m_deactivatedComponents[num];
					HandleComponentReady(asyncBehavior);
					m_deactivatedComponents.RemoveAt(num);
				}
			}
			CheckIfDoneChangingStates();
		}

		private void CheckIfDoneChangingStates()
		{
			List<IStatefulWidgetComponent> list = new List<IStatefulWidgetComponent>();
			foreach (IStatefulWidgetComponent componentsChangingState in m_componentsChangingStates)
			{
				if (!componentsChangingState.IsChangingStates)
				{
					Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " did not receive HandleDoneChangingStates from " + Widget.GetObjectDebugName(componentsChangingState));
					list.Add(componentsChangingState);
				}
			}
			foreach (IStatefulWidgetComponent item in list)
			{
				HandleDoneChangingStates(item);
			}
			if (m_componentsChangingStates.Count != 0)
			{
				return;
			}
			if (m_initializationState == InitializationState.InitializingWidgetBehaviors && !WaitForParentToShow)
			{
				FinalizeInitialization(!IsDesiredHiddenInHierarchy);
			}
			if (m_changingStatesInternally && (m_widgetsPendingTick == null || m_widgetsPendingTick.Count == 0))
			{
				m_changingStatesInternally = false;
				if (CanSendStateChanges && m_startChangingStatesEvent.IsSet)
				{
					m_startChangingStatesEvent.Clear();
					m_doneChangingStatesEvent.SetAndDispatch();
				}
			}
		}

		private void RegisterChildPendingTick(WidgetTemplate nestedWidget)
		{
			if (m_widgetsPendingTick == null)
			{
				m_widgetsPendingTick = new HashSet<WidgetTemplate>();
			}
			m_widgetsPendingTick.Add(nestedWidget);
			if (m_parentWidgetTemplate == null)
			{
				HearthstoneServices.Get<WidgetRunner>()?.AddWidgetPendingTick(this);
			}
			else
			{
				m_parentWidgetTemplate.RegisterChildPendingTick(this);
			}
		}

		public void InitializeWidgetBehaviors()
		{
			if (m_initializationState != InitializationState.InitializingWidget)
			{
				return;
			}
			DeferredWidgetBehaviorInitialization = false;
			m_initializationState = InitializationState.InitializingWidgetBehaviors;
			if (m_nestedInstances != null)
			{
				foreach (WidgetInstance nestedInstance in m_nestedInstances)
				{
					if (nestedInstance != null)
					{
						nestedInstance.InitializeWidgetBehaviors();
					}
				}
			}
			if (m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in m_widgetBehaviors)
				{
					if (widgetBehavior != null)
					{
						widgetBehavior.Initialize();
						if (widgetBehavior.CanTick)
						{
							widgetBehavior.OnUpdate();
						}
					}
				}
			}
			HandleDoneChangingStates(this);
			CheckIfDoneChangingStates();
		}

		public override void SetLayerOverride(GameLayer layerOverride)
		{
			if (layerOverride >= GameLayer.Default)
			{
				if (m_prevLayersByObject == null)
				{
					m_prevLayersByObject = new Map<GameObject, int>();
				}
				SetLayerOverrideForObject(layerOverride, base.gameObject, m_prevLayersByObject);
			}
		}

		public void SetLayerOverrideForObject(GameLayer layerOverride, GameObject go, Map<GameObject, int> originalLayers = null)
		{
			if (layerOverride < GameLayer.Default)
			{
				return;
			}
			SceneUtils.WalkSelfAndChildren(go.transform, delegate(Transform child)
			{
				bool result = true;
				ILayerOverridable[] components = child.GetComponents<ILayerOverridable>();
				if (components != null && components.Length != 0)
				{
					ILayerOverridable[] array = components;
					foreach (ILayerOverridable layerOverridable in array)
					{
						if (layerOverridable != this)
						{
							layerOverridable.SetLayerOverride(layerOverride);
							if (layerOverridable.HandlesChildLayers)
							{
								result = false;
								break;
							}
						}
					}
				}
				child.gameObject.layer = (int)layerOverride;
				if (originalLayers != null)
				{
					originalLayers[child.gameObject] = child.gameObject.layer;
				}
				return result;
			});
		}

		public override void ClearLayerOverride()
		{
			if (m_prevLayersByObject == null || !m_prevLayersByObject.ContainsKey(base.gameObject))
			{
				return;
			}
			int originalRootLayer = m_prevLayersByObject[base.gameObject];
			SceneUtils.WalkSelfAndChildren(base.transform, delegate(Transform child)
			{
				bool result = true;
				ILayerOverridable[] components = child.GetComponents<ILayerOverridable>();
				if (components != null && components.Length != 0)
				{
					ILayerOverridable[] array = components;
					foreach (ILayerOverridable obj in array)
					{
						obj.ClearLayerOverride();
						if (obj.HandlesChildLayers)
						{
							result = false;
							break;
						}
					}
				}
				if (m_prevLayersByObject.TryGetValue(child.gameObject, out var value))
				{
					child.gameObject.layer = value;
				}
				else
				{
					child.gameObject.layer = originalRootLayer;
					Log.UIStatus.PrintWarning("Couldn't find original layer for GameObject " + $"{child.name} ({child.gameObject.GetInstanceID()}) so setting it to widget owner's layer.");
				}
				return result;
			});
			m_prevLayersByObject = null;
		}

		private void HandleComponentActivated(object payload)
		{
			if (payload == null)
			{
				Log.UIStatus.PrintError($"WidgetTemplate {base.name} ({GetInstanceID()}) attempted to handle activated async component but it was null!");
				return;
			}
			IAsyncInitializationBehavior asyncInitializationBehavior = (IAsyncInitializationBehavior)payload;
			if (m_deactivatedComponents != null && m_deactivatedComponents.Contains(payload) && m_initializationState == InitializationState.InitializingWidget)
			{
				Widget widget = asyncInitializationBehavior as Widget;
				if (widget != null)
				{
					widget.DeferredWidgetBehaviorInitialization = true;
				}
				asyncInitializationBehavior.RegisterReadyListener(HandleComponentReady, payload);
				m_deactivatedComponents.Remove(asyncInitializationBehavior);
			}
		}

		private void HandleComponentDeactivated(object payload)
		{
			if (m_initializationState != InitializationState.InitializingWidget)
			{
				return;
			}
			if (payload == null)
			{
				Log.UIStatus.PrintError($"WidgetTemplate {base.name} ({GetInstanceID()}) attempted to handle deactivated async component but it was null!");
				return;
			}
			IAsyncInitializationBehavior asyncInitializationBehavior = (IAsyncInitializationBehavior)payload;
			if (m_deactivatedComponents == null)
			{
				m_deactivatedComponents = new List<IAsyncInitializationBehavior>();
			}
			Widget widget = asyncInitializationBehavior as Widget;
			if (widget != null)
			{
				widget.DeferredWidgetBehaviorInitialization = false;
			}
			if (!asyncInitializationBehavior.IsReady && !m_deactivatedComponents.Contains(asyncInitializationBehavior))
			{
				m_deactivatedComponents.Add(asyncInitializationBehavior);
				asyncInitializationBehavior.RemoveReadyListener(HandleComponentReady);
			}
		}

		private void HandleComponentReady(object asyncBehavior)
		{
			if (m_numComponentsPendingInitialization > 0)
			{
				m_numComponentsPendingInitialization--;
				if (m_numComponentsPendingInitialization == 0)
				{
					HandleAllAsyncBehaviorsReady();
				}
			}
		}

		private void HandleStartChangingStates(object context)
		{
			IStatefulWidgetComponent statefulWidgetComponent = context as IStatefulWidgetComponent;
			if (statefulWidgetComponent == null)
			{
				Log.UIStatus.PrintWarning("WidgetTemplate " + base.gameObject.name + " received HandleStartChangingStates with invalid context");
				return;
			}
			if (!m_componentsChangingStates.Contains(statefulWidgetComponent))
			{
				m_componentsChangingStates.Add(statefulWidgetComponent);
			}
			else
			{
				Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " received HandleStartChangingStates more than once without a HandleDoneChangingStates for " + Widget.GetObjectDebugName(statefulWidgetComponent));
			}
			if (!m_changingStatesInternally)
			{
				m_changingStatesInternally = true;
				if (CanSendStateChanges && !m_startChangingStatesEvent.IsSet)
				{
					m_doneChangingStatesEvent.Clear();
					m_startChangingStatesEvent.SetAndDispatch();
				}
			}
		}

		private void HandleDoneChangingStates(object context)
		{
			IStatefulWidgetComponent statefulWidgetComponent = context as IStatefulWidgetComponent;
			if (statefulWidgetComponent == null)
			{
				Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " received HandleDoneChangingStates with invalid context");
			}
			else if (!m_componentsChangingStates.Remove(statefulWidgetComponent))
			{
				Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " received HandleDoneChangingStates without HandleStartChangingStates for " + Widget.GetObjectDebugName(context));
			}
		}

		private void FinalizeInitialization(bool tryShow)
		{
			if (tryShow && !IsDesiredHidden)
			{
				ShowOrHide(show: true, recursive: false);
			}
			if (m_nestedInstances != null)
			{
				foreach (WidgetInstance nestedInstance in m_nestedInstances)
				{
					if (nestedInstance.Widget != null && nestedInstance.Widget.WaitForParentToShow && nestedInstance.Widget.IsActive)
					{
						if (nestedInstance.Widget.m_initializationState != InitializationState.InitializingWidgetBehaviors)
						{
							Log.UIStatus.PrintError("WidgetTemplate " + Widget.GetObjectDebugName(this) + " attempted to finalize and show child widget " + Widget.GetObjectDebugName(nestedInstance.Widget) + ", but child was not done with its init!");
						}
						else
						{
							nestedInstance.Widget.FinalizeInitialization(tryShow && !IsDesiredHidden);
						}
					}
				}
			}
			m_willTickWhileInactive = false;
			m_initializationState = InitializationState.Done;
		}

		public DataContext GetDataContextForGameObject(GameObject go)
		{
			if (m_gameObjectsToBindingsMap != null && m_gameObjectsToBindingsMap.TryGetValue(go, out var value))
			{
				return value.DataContext;
			}
			return m_dataContext;
		}

		private GameObjectBinding CreateCustomDataContextForGameObject(GameObject go, DataContext original)
		{
			if (m_gameObjectsToBindingsMap == null)
			{
				m_gameObjectsToBindingsMap = new Map<GameObject, GameObjectBinding>();
			}
			DataContext dataContext = new DataContext();
			GameObjectBinding gameObjectBinding = new GameObjectBinding(dataContext);
			m_gameObjectsToBindingsMap[go] = gameObjectBinding;
			if (original != null && original != m_dataContext)
			{
				foreach (IDataModel dataModel in original.GetDataModels())
				{
					dataContext.BindDataModel(dataModel);
				}
				return gameObjectBinding;
			}
			return gameObjectBinding;
		}

		public WidgetEventListenerResponse EventReceived(string eventName)
		{
			if (this.m_eventListeners != null)
			{
				this.m_eventListeners(eventName);
			}
			return default(WidgetEventListenerResponse);
		}

		public override void RegisterEventListener(EventListenerDelegate listener)
		{
			m_eventListeners -= listener;
			m_eventListeners += listener;
		}

		public override void RemoveEventListener(EventListenerDelegate listener)
		{
			m_eventListeners -= listener;
		}

		public override void Show()
		{
			IsDesiredHidden = false;
			if (m_initializationState == InitializationState.Done)
			{
				ShowOrHide(show: true, recursive: true);
			}
		}

		public override void Hide()
		{
			IsDesiredHidden = true;
			if (m_initializationState == InitializationState.Done)
			{
				ShowOrHide(show: false, recursive: true);
			}
		}

		private void ShowOrHide(bool show, bool recursive)
		{
			SceneUtils.WalkSelfAndChildren(base.transform, delegate(Transform current)
			{
				bool flag = false;
				Component[] components = current.GetComponents<Component>();
				Renderer renderer = null;
				PegUIElement pegUIElement = null;
				UberText uberText = null;
				WidgetInstance widgetInstance = null;
				IVisibleWidgetComponent visibleWidgetComponent = null;
				Component[] array = components;
				foreach (Component component in array)
				{
					if (component is Renderer)
					{
						renderer = (Renderer)component;
					}
					else if (component is PegUIElement)
					{
						pegUIElement = (PegUIElement)component;
					}
					else if (component is UberText)
					{
						uberText = (UberText)component;
					}
					else if (component is IVisibleWidgetComponent)
					{
						visibleWidgetComponent = (IVisibleWidgetComponent)component;
					}
					else if (component is WidgetInstance)
					{
						widgetInstance = (WidgetInstance)component;
					}
				}
				if (renderer != null)
				{
					SceneUtils.SetInvisibleRenderer(renderer, show, ref m_originalRendererLayers);
				}
				if (pegUIElement != null && (m_initializationStartTime > pegUIElement.SetEnabledLastCallTime || m_initializationState == InitializationState.Done))
				{
					pegUIElement.SetEnabled(show, isInternal: true);
				}
				if (uberText != null)
				{
					if (show)
					{
						uberText.Show();
					}
					else
					{
						uberText.Hide();
					}
					flag = true;
				}
				if (visibleWidgetComponent != null)
				{
					visibleWidgetComponent.SetVisibility(show && !visibleWidgetComponent.IsDesiredHidden, isInternal: true);
					flag = flag || visibleWidgetComponent.HandlesChildVisibility;
				}
				if (widgetInstance != null)
				{
					if (recursive && widgetInstance.Widget != null && !widgetInstance.Widget.IsDesiredHidden && widgetInstance.Widget.GetInitializationState() == InitializationState.Done)
					{
						widgetInstance.Widget.ShowOrHide(show, recursive: true);
					}
					flag = true;
				}
				return !flag;
			});
		}

		public override void UnbindDataModel(int id)
		{
			if (m_dataContext != null)
			{
				m_dataContext.UnbindDataModel(id);
			}
			if (m_gameObjectsToBindingsMap != null)
			{
				foreach (KeyValuePair<GameObject, GameObjectBinding> item in m_gameObjectsToBindingsMap)
				{
					item.Value.UnbindDataModel(id);
				}
			}
			if (m_nestedInstances == null)
			{
				return;
			}
			foreach (WidgetInstance nestedInstance in m_nestedInstances)
			{
				nestedInstance.UnbindDataModel(id);
			}
		}

		public override void BindDataModel(IDataModel dataModel, bool overrideChildren = false)
		{
			if (m_dataContext == null)
			{
				m_dataContext = new DataContext();
				m_dataContext.RegisterChangedListener(HandleDataChanged);
			}
			if (m_gameObjectsToBindingsMap == null)
			{
				m_gameObjectsToBindingsMap = new Map<GameObject, GameObjectBinding>();
			}
			m_dataContext.BindDataModel(dataModel);
			if (!overrideChildren)
			{
				return;
			}
			if (m_nestedInstances != null)
			{
				foreach (WidgetInstance nestedInstance in m_nestedInstances)
				{
					nestedInstance.UnbindDataModel(dataModel.DataModelId);
				}
			}
			foreach (KeyValuePair<GameObject, GameObjectBinding> item in m_gameObjectsToBindingsMap)
			{
				item.Value.UnbindDataModel(dataModel.DataModelId);
			}
		}

		public override bool BindDataModel(IDataModel dataModel, string targetName, bool propagateToChildren = true, bool overrideChildren = false)
		{
			if (m_dataContext == null)
			{
				m_dataContext = new DataContext();
				m_dataContext.RegisterChangedListener(HandleDataChanged);
			}
			if (m_gameObjectsToBindingsMap == null)
			{
				m_gameObjectsToBindingsMap = new Map<GameObject, GameObjectBinding>();
			}
			GameObject target = null;
			foreach (KeyValuePair pair in m_pairs)
			{
				if (pair.Value != null && pair.Value.name == targetName)
				{
					target = pair.Value.gameObject;
					break;
				}
			}
			return BindDataModel(dataModel, target, propagateToChildren, overrideChildren);
		}

		public bool BindDataModel(IDataModel dataModel, GameObject target, bool propagateToChildren = true, bool overrideChildren = false)
		{
			if (target == null)
			{
				return false;
			}
			if (m_componentsSet == null)
			{
				m_componentsSet = new HashSet<Component>();
				foreach (KeyValuePair pair in m_pairs)
				{
					m_componentsSet.Add(pair.Value);
				}
				if (m_addedInstances != null)
				{
					foreach (WidgetInstance addedInstance in m_addedInstances)
					{
						m_componentsSet.Add(addedInstance.transform);
					}
				}
			}
			if (!m_componentsSet.Contains(target.transform))
			{
				Hearthstone.UI.Logging.Log.Get().AddMessage("Tried binding a data model to a game object that does not belong to this template", base.gameObject, LogLevel.Error);
				return false;
			}
			if (target == base.gameObject)
			{
				BindDataModel(dataModel, overrideChildren);
				return true;
			}
			BindDataModel_Recursive(dataModel, target, propagateToChildren, overrideChildren, target: true, m_dataContext);
			ProcessNewGameObjectBinds();
			return true;
		}

		private void BindDataModel_Recursive(IDataModel dataModel, GameObject current, bool propagateToChildren, bool overrideChildren, bool target, DataContext parentDataContext)
		{
			if (propagateToChildren)
			{
				WidgetInstance component = current.GetComponent<WidgetInstance>();
				if (component != null)
				{
					if (overrideChildren)
					{
						component.UnbindDataModel(dataModel.DataModelId);
					}
					propagateToChildren = false;
				}
			}
			int num = 0;
			if (m_gameObjectsToBindingsMap == null || !m_gameObjectsToBindingsMap.TryGetValue(current, out var value))
			{
				value = CreateCustomDataContextForGameObject(current, parentDataContext);
			}
			else
			{
				num = value.DataContext.GetLocalDataVersion();
			}
			IDataModel model;
			if (target)
			{
				value.BindDataModel(dataModel, owned: true);
			}
			else if (overrideChildren || !value.DataContext.GetDataModel(dataModel.DataModelId, out model) || !value.OwnsDataModel(dataModel))
			{
				value.BindDataModel(dataModel, owned: false);
			}
			if (num != value.DataContext.GetLocalDataVersion())
			{
				if (m_newlyBoundGameObjects == null)
				{
					m_newlyBoundGameObjects = new List<GameObject>();
				}
				m_newlyBoundGameObjects.Add(current);
			}
			if (!propagateToChildren)
			{
				return;
			}
			Transform transform = current.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (m_componentsSet.Contains(child))
				{
					BindDataModel_Recursive(dataModel, child.gameObject, propagateToChildren: true, overrideChildren, target: false, value.DataContext);
				}
			}
		}

		public override bool GetDataModel(int id, out IDataModel dataModel)
		{
			if (m_dataContext.GetDataModel(id, out dataModel))
			{
				return true;
			}
			Transform parent = base.transform.parent;
			if (ParentWidgetTemplate != null && parent != null)
			{
				return ParentWidgetTemplate.GetDataModel(id, parent.gameObject, out dataModel);
			}
			return false;
		}

		public override bool GetDataModel(int id, string targetName, out IDataModel model)
		{
			GameObject gameObject = null;
			foreach (KeyValuePair pair in m_pairs)
			{
				if (pair.Value != null && pair.Value.name == targetName)
				{
					gameObject = pair.Value.gameObject;
					break;
				}
			}
			if (gameObject == null)
			{
				model = null;
				return false;
			}
			return GetDataModel(id, gameObject, out model);
		}

		public bool GetDataModel(int id, GameObject target, out IDataModel model)
		{
			DataContext dataContextForGameObject = GetDataContextForGameObject(target);
			if (dataContextForGameObject != null && dataContextForGameObject.GetDataModel(id, out model))
			{
				return true;
			}
			return GetDataModel(id, out model);
		}

		public ICollection<IDataModel> GetDataModels()
		{
			return m_dataContext.GetDataModels();
		}

		private void TryHandleDataChanged(IDataModel dataModel, DataChangeSource changeType)
		{
			if (changeType == DataChangeSource.Parent && m_dataContext.HasDataModel(dataModel.DataModelId))
			{
				return;
			}
			bool flag = false;
			if (m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in m_widgetBehaviors)
				{
					if (!(widgetBehavior == null))
					{
						bool flag2 = HasGameObjectBinding(widgetBehavior.gameObject, dataModel.DataModelId);
						if ((changeType != DataChangeSource.GameObjectBinding || flag2) && !(changeType != DataChangeSource.GameObjectBinding && flag2) && widgetBehavior.TryIncrementDataVersion(dataModel.DataModelId))
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				DataVersion++;
				if (m_parentWidgetTemplate == null)
				{
					HearthstoneServices.Get<WidgetRunner>()?.AddWidgetPendingTick(this);
				}
				else
				{
					m_parentWidgetTemplate.RegisterChildPendingTick(this);
					AddUpdateTarget(UpdateTargets.Behaviors);
				}
			}
			if (changeType == DataChangeSource.Global || m_nestedInstances == null)
			{
				return;
			}
			foreach (WidgetInstance nestedInstance in m_nestedInstances)
			{
				if (!(nestedInstance == null))
				{
					bool flag3 = HasGameObjectBinding(nestedInstance.gameObject, dataModel.DataModelId);
					if ((changeType != DataChangeSource.GameObjectBinding || flag3) && !(changeType != DataChangeSource.GameObjectBinding && flag3) && nestedInstance.Widget != null)
					{
						nestedInstance.Widget.TryHandleDataChanged(dataModel, DataChangeSource.Parent);
					}
				}
			}
		}

		private void HandleDataChanged(IDataModel dataModel)
		{
			TryHandleDataChanged(dataModel, DataChangeSource.Template);
		}

		private void HandleBindingDataChanged(IDataModel dataModel, GameObjectBinding binding)
		{
			if (binding.OwnsDataModel(dataModel) && binding.DataContext.HasDataModelInstance(dataModel))
			{
				TryHandleDataChanged(dataModel, DataChangeSource.GameObjectBinding);
			}
		}

		private void HandleGlobalDataChanged(IDataModel dataModel)
		{
			TryHandleDataChanged(dataModel, DataChangeSource.Global);
		}

		private void ProcessNewGameObjectBinds()
		{
			if (m_newlyBoundGameObjects != null)
			{
				for (int i = 0; i < m_newlyBoundGameObjects.Count; i++)
				{
					ProcessNewGameObjectBinding(m_newlyBoundGameObjects[i]);
				}
				m_newlyBoundGameObjects.Clear();
			}
		}

		private void ProcessNewGameObjectBinding(GameObject go)
		{
			GameObjectBinding gameObjectBinding = m_gameObjectsToBindingsMap[go];
			foreach (IDataModel dataModel in gameObjectBinding.DataContext.GetDataModels())
			{
				HandleBindingDataChanged(dataModel, gameObjectBinding);
			}
			gameObjectBinding.RegisterChangedListener(HandleBindingDataChanged);
		}

		private bool HasGameObjectBinding(GameObject go, int id)
		{
			GameObjectBinding value = null;
			m_gameObjectsToBindingsMap?.TryGetValue(go, out value);
			return value?.DataContext.HasDataModel(id) ?? false;
		}

		public override Widget FindWidget(string childWidgetName)
		{
			return FindChildOfType<Widget>(childWidgetName);
		}

		public override T FindWidgetComponent<T>(params string[] path)
		{
			if (path == null || path.Length == 0)
			{
				return GetComponent<T>();
			}
			if (path.Length == 1)
			{
				return FindChildOfType<T>(path[0]);
			}
			Widget widget = this;
			int i;
			for (i = 0; i < path.Length - 1; i++)
			{
				if (!(widget != null))
				{
					break;
				}
				if ((widget = widget.FindWidget(path[i])) == null)
				{
					return null;
				}
			}
			if (!(widget != null))
			{
				return null;
			}
			return widget.FindWidgetComponent<T>(new string[1] { path[i] });
		}

		public override bool TriggerEvent(string eventName, TriggerEventParameters parameters = default(TriggerEventParameters))
		{
			return EventFunctions.TriggerEvent(base.transform, eventName, parameters);
		}

		public void AddNestedInstance(WidgetInstance nestedInstance, GameObject parent = null)
		{
			if (m_addedInstances == null)
			{
				m_addedInstances = new List<WidgetInstance>();
			}
			m_addedInstances.Add(nestedInstance);
			if (m_nestedInstances == null)
			{
				m_nestedInstances = new List<WidgetInstance>();
			}
			m_nestedInstances.Add(nestedInstance);
			if (m_componentsSet != null)
			{
				m_componentsSet.Add(nestedInstance.transform);
			}
			nestedInstance.ParentWidgetTemplate = this;
			nestedInstance.RegisterStartChangingStatesListener(HandleStartChangingStates, nestedInstance);
			nestedInstance.RegisterDoneChangingStatesListener(HandleDoneChangingStates, nestedInstance, callImmediatelyIfSet: false);
			nestedInstance.PreInitialize();
			if (parent != null)
			{
				DataContext dataContextForGameObject = GetDataContextForGameObject(parent);
				if (dataContextForGameObject != null && m_dataContext != dataContextForGameObject)
				{
					CreateCustomDataContextForGameObject(nestedInstance.gameObject, dataContextForGameObject);
					ProcessNewGameObjectBinding(nestedInstance.gameObject);
				}
			}
		}

		public void RemoveNestedInstance(WidgetInstance nestedInstance)
		{
			nestedInstance.RemoveStartChangingStatesListener(HandleStartChangingStates);
			nestedInstance.RemoveDoneChangingStatesListener(HandleDoneChangingStates);
			if (nestedInstance.StartedChangingStates)
			{
				HandleDoneChangingStates(nestedInstance);
			}
			if (m_addedInstances != null)
			{
				m_addedInstances.Remove(nestedInstance);
			}
			if (m_nestedInstances != null)
			{
				m_nestedInstances.Remove(nestedInstance);
			}
			if (nestedInstance.ParentWidgetTemplate == this)
			{
				nestedInstance.ParentWidgetTemplate = null;
			}
			if (m_gameObjectsToBindingsMap != null)
			{
				m_gameObjectsToBindingsMap.Remove(nestedInstance.gameObject);
			}
		}

		public Component GetComponentById(long id)
		{
			if (id == 0L)
			{
				return base.transform;
			}
			m_componentsById.TryGetValue(id, out var value);
			return value;
		}

		public bool GetComponentId(Component component, out long id)
		{
			if (component == base.transform)
			{
				id = 0L;
				return true;
			}
			foreach (KeyValuePair pair in m_pairs)
			{
				if (pair.Value == component)
				{
					id = pair.Key;
					return true;
				}
			}
			id = -1L;
			return false;
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			BuildComponentMap();
		}

		private void BuildComponentMap()
		{
			m_componentsById = new Map<long, Component>();
			foreach (KeyValuePair pair in m_pairs)
			{
				if (pair.Key != 0L)
				{
					m_componentsById.Add(pair.Key, pair.Value);
				}
			}
			m_nestedInstances?.Clear();
			m_visualControllers?.Clear();
			m_widgetBehaviors?.Clear();
			if (m_addedInstances != null)
			{
				if (m_nestedInstances == null)
				{
					m_nestedInstances = new List<WidgetInstance>();
				}
				foreach (WidgetInstance addedInstance in m_addedInstances)
				{
					if (addedInstance != null)
					{
						m_nestedInstances.Add(addedInstance);
					}
				}
			}
			foreach (KeyValuePair pair2 in m_pairs)
			{
				WidgetInstance widgetInstance = pair2.Value as WidgetInstance;
				if (widgetInstance != null)
				{
					if (m_nestedInstances == null)
					{
						m_nestedInstances = new List<WidgetInstance>();
					}
					m_nestedInstances.Add(widgetInstance);
				}
				WidgetBehavior widgetBehavior = pair2.Value as WidgetBehavior;
				if (widgetBehavior != null)
				{
					if (m_widgetBehaviors == null)
					{
						m_widgetBehaviors = new List<WidgetBehavior>();
					}
					m_widgetBehaviors.Add(widgetBehavior);
				}
				VisualController visualController = pair2.Value as VisualController;
				if (visualController != null)
				{
					if (m_visualControllers == null)
					{
						m_visualControllers = new List<VisualController>();
					}
					m_visualControllers.Add(visualController);
				}
			}
			ReconcileDataContextMap();
		}

		private void ReconcileDataContextMap()
		{
			if (m_gameObjectsToBindingsMap == null || m_gameObjectsToBindingsMap.Count <= 0)
			{
				return;
			}
			HashSet<GameObject> hashSet = new HashSet<GameObject>();
			foreach (KeyValuePair pair in m_pairs)
			{
				hashSet.Add(pair.Value.gameObject);
			}
			if (m_addedInstances != null)
			{
				foreach (WidgetInstance addedInstance in m_addedInstances)
				{
					if (!(addedInstance == null))
					{
						hashSet.Add(addedInstance.gameObject);
					}
				}
			}
			Map<GameObject, GameObjectBinding> gameObjectsToBindingsMap = m_gameObjectsToBindingsMap;
			m_gameObjectsToBindingsMap = new Map<GameObject, GameObjectBinding>();
			foreach (KeyValuePair<GameObject, GameObjectBinding> item in gameObjectsToBindingsMap)
			{
				GameObjectBinding value = null;
				if (hashSet.Contains(item.Key) && gameObjectsToBindingsMap.TryGetValue(item.Key, out value))
				{
					m_gameObjectsToBindingsMap.Add(item.Key, value);
				}
			}
		}

		private T FindChildOfType<T>(string childName) where T : Component
		{
			Queue<Transform> queue = new Queue<Transform>();
			queue.Enqueue(base.transform);
			while (queue.Count > 0)
			{
				Transform transform = queue.Dequeue();
				if (transform.name.Equals(childName, StringComparison.InvariantCultureIgnoreCase))
				{
					return transform.GetComponent<T>();
				}
				if (transform.GetComponent<WidgetInstance>() == null)
				{
					for (int i = 0; i < transform.childCount; i++)
					{
						queue.Enqueue(transform.GetChild(i));
					}
				}
			}
			return null;
		}
	}
}
