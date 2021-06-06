using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001032 RID: 4146
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class WidgetTemplate : Widget, ISerializationCallbackReceiver, IWidgetEventListener
	{
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x0600B3F6 RID: 46070 RVA: 0x00375688 File Offset: 0x00373888
		// (remove) Token: 0x0600B3F7 RID: 46071 RVA: 0x003756C0 File Offset: 0x003738C0
		private event Widget.EventListenerDelegate m_eventListeners;

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600B3F8 RID: 46072 RVA: 0x003756F5 File Offset: 0x003738F5
		public List<int> DataModelHints_EditorOnly
		{
			get
			{
				return this.m_dataModelHints_editorOnly;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600B3F9 RID: 46073 RVA: 0x003756FD File Offset: 0x003738FD
		public bool WillTickWhileInactive
		{
			get
			{
				return this.m_willTickWhileInactive;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600B3FA RID: 46074 RVA: 0x00375705 File Offset: 0x00373905
		// (set) Token: 0x0600B3FB RID: 46075 RVA: 0x00375738 File Offset: 0x00373938
		public bool WaitForParentToShow
		{
			get
			{
				return this.m_waitForParentToShow && !this.m_willTickWhileInactive && this.m_parentWidgetTemplate != null && this.m_parentWidgetTemplate.GetInitializationState() != WidgetTemplate.InitializationState.Done;
			}
			set
			{
				this.m_waitForParentToShow = value;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600B3FC RID: 46076 RVA: 0x00375741 File Offset: 0x00373941
		// (set) Token: 0x0600B3FD RID: 46077 RVA: 0x00375749 File Offset: 0x00373949
		public WidgetTemplate ParentWidgetTemplate
		{
			get
			{
				return this.m_parentWidgetTemplate;
			}
			set
			{
				this.m_parentWidgetTemplate = value;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x0600B3FE RID: 46078 RVA: 0x00375752 File Offset: 0x00373952
		public DataContext DataContext
		{
			get
			{
				return this.m_dataContext;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600B400 RID: 46080 RVA: 0x00375763 File Offset: 0x00373963
		// (set) Token: 0x0600B3FF RID: 46079 RVA: 0x0037575A File Offset: 0x0037395A
		public int DataVersion { get; set; } = 1;

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600B401 RID: 46081 RVA: 0x0037576B File Offset: 0x0037396B
		public override bool IsInitialized
		{
			get
			{
				return this.m_initializationState == WidgetTemplate.InitializationState.Done;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x0600B402 RID: 46082 RVA: 0x00375778 File Offset: 0x00373978
		public override bool IsChangingStates
		{
			get
			{
				if (!base.IsActive)
				{
					return false;
				}
				if (this.m_initializationState <= WidgetTemplate.InitializationState.InitializingWidget)
				{
					return true;
				}
				if (this.m_visualControllers != null)
				{
					using (List<VisualController>.Enumerator enumerator = this.m_visualControllers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.IsChangingStates)
							{
								return true;
							}
						}
					}
				}
				if (this.m_nestedInstances != null)
				{
					using (List<WidgetInstance>.Enumerator enumerator2 = this.m_nestedInstances.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.IsChangingStates)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x0600B403 RID: 46083 RVA: 0x0037583C File Offset: 0x00373A3C
		public override bool HasPendingActions
		{
			get
			{
				if (!base.IsActive)
				{
					return false;
				}
				if (this.m_visualControllers != null)
				{
					using (List<VisualController>.Enumerator enumerator = this.m_visualControllers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.HasPendingActions)
							{
								return true;
							}
						}
					}
				}
				if (this.m_nestedInstances != null)
				{
					using (List<WidgetInstance>.Enumerator enumerator2 = this.m_nestedInstances.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.HasPendingActions)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600B404 RID: 46084 RVA: 0x003758F4 File Offset: 0x00373AF4
		public bool IsDesiredHiddenInHierarchy
		{
			get
			{
				if (this.IsDesiredHidden)
				{
					return true;
				}
				WidgetTemplate parentWidgetTemplate = this.ParentWidgetTemplate;
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

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600B406 RID: 46086 RVA: 0x00375938 File Offset: 0x00373B38
		// (set) Token: 0x0600B405 RID: 46085 RVA: 0x0037592F File Offset: 0x00373B2F
		public bool IsDesiredHidden { get; private set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600B407 RID: 46087 RVA: 0x00375940 File Offset: 0x00373B40
		private bool ListeningToNestedStateChanges
		{
			get
			{
				return (this.m_visualControllers != null && this.m_visualControllers.Count > 0) || (this.m_nestedInstances != null && this.m_nestedInstances.Count > 0);
			}
		}

		// Token: 0x0600B408 RID: 46088 RVA: 0x00375972 File Offset: 0x00373B72
		public WidgetTemplate.InitializationState GetInitializationState()
		{
			return this.m_initializationState;
		}

		// Token: 0x0600B409 RID: 46089 RVA: 0x0037597C File Offset: 0x00373B7C
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
			if (this.m_initializationState <= WidgetTemplate.InitializationState.InitializingWidget)
			{
				return true;
			}
			if (this.m_visualControllers != null)
			{
				using (List<VisualController>.Enumerator enumerator = this.m_visualControllers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsChangingStates)
						{
							return true;
						}
					}
				}
			}
			if (this.m_nestedInstances != null)
			{
				using (List<WidgetInstance>.Enumerator enumerator2 = this.m_nestedInstances.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.GetIsChangingStates(includeGameObject))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600B40A RID: 46090 RVA: 0x00375A50 File Offset: 0x00373C50
		protected bool CanSendStateChanges
		{
			get
			{
				return this.m_enabledInternally || this.m_willTickWhileInactive;
			}
		}

		// Token: 0x0600B40B RID: 46091 RVA: 0x00375A62 File Offset: 0x00373C62
		public void OnInstantiated()
		{
			if (Application.IsPlaying(this))
			{
				this.ShowOrHide(false, false);
			}
		}

		// Token: 0x0600B40C RID: 46092 RVA: 0x00375A74 File Offset: 0x00373C74
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_enabledInternally = true;
			if (this.m_changingStatesInternally && !this.m_willTickWhileInactive && !this.m_startChangingStatesEvent.IsSet)
			{
				this.m_doneChangingStatesEvent.Clear();
				this.m_startChangingStatesEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B40D RID: 46093 RVA: 0x00375AC4 File Offset: 0x00373CC4
		protected override void OnDisable()
		{
			this.m_enabledInternally = false;
			base.OnDisable();
			if (this.m_changingStatesInternally && !this.m_willTickWhileInactive && this.m_startChangingStatesEvent.IsSet)
			{
				this.m_startChangingStatesEvent.Clear();
				this.m_doneChangingStatesEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B40E RID: 46094 RVA: 0x00375B12 File Offset: 0x00373D12
		private void Start()
		{
			this.Initialize(false);
		}

		// Token: 0x0600B40F RID: 46095 RVA: 0x00375B1C File Offset: 0x00373D1C
		private void OnDestroy()
		{
			if (this.m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in this.m_widgetBehaviors)
				{
					widgetBehavior.RemoveStartChangingStatesListener(new Action<object>(this.HandleStartChangingStates));
					widgetBehavior.RemoveDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates));
				}
			}
			if (this.m_nestedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					widgetInstance.ParentWidgetTemplate = this;
					widgetInstance.RemoveStartChangingStatesListener(new Action<object>(this.HandleStartChangingStates));
					widgetInstance.RemoveDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates));
				}
			}
			if (HearthstoneServices.Get<WidgetRunner>() != null)
			{
				HearthstoneServices.Get<WidgetRunner>().UnregisterWidget(this);
			}
		}

		// Token: 0x0600B410 RID: 46096 RVA: 0x00375C10 File Offset: 0x00373E10
		private void PreInitialize()
		{
			if (this.m_dataContext != null)
			{
				this.m_dataContext.RegisterChangedListener(new DataContext.DataChangedDelegate(this.HandleDataChanged));
				GlobalDataContext.Get().RegisterChangedListener(new DataContext.DataChangedDelegate(this.HandleGlobalDataChanged));
			}
			if (this.m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in this.m_widgetBehaviors)
				{
					widgetBehavior.RegisterStartChangingStatesListener(new Action<object>(this.HandleStartChangingStates), widgetBehavior, true, false);
					widgetBehavior.RegisterDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates), widgetBehavior, false, false);
				}
			}
			if (this.m_nestedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					widgetInstance.ParentWidgetTemplate = this;
					widgetInstance.RegisterStartChangingStatesListener(new Action<object>(this.HandleStartChangingStates), widgetInstance, true, false);
					widgetInstance.RegisterDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates), widgetInstance, false, false);
					widgetInstance.PreInitialize();
				}
			}
		}

		// Token: 0x0600B411 RID: 46097 RVA: 0x00375D40 File Offset: 0x00373F40
		public void Initialize(bool shouldPreload = false)
		{
			if (this.m_initializationState != WidgetTemplate.InitializationState.NotStarted)
			{
				return;
			}
			this.m_willTickWhileInactive = shouldPreload;
			this.m_initializationState = WidgetTemplate.InitializationState.InitializingWidget;
			this.PreInitialize();
			this.HandleStartChangingStates(this);
			IJobDependency[] dependencies;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
			{
				typeof(IAssetLoader),
				typeof(WidgetRunner)
			});
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("WidgetTemplate.Initialize", new Action(this.InitializeInternal), JobFlags.StartImmediately, dependencies));
		}

		// Token: 0x0600B412 RID: 46098 RVA: 0x00375DB8 File Offset: 0x00373FB8
		private void InitializeInternal()
		{
			if (this.m_parentWidgetTemplate == null)
			{
				HearthstoneServices.Get<WidgetRunner>().RegisterWidget(this);
			}
			this.m_initializationStartTime = Time.realtimeSinceStartup;
			List<IAsyncInitializationBehavior> list = new List<IAsyncInitializationBehavior>();
			if (this.m_pairs != null)
			{
				foreach (WidgetTemplate.KeyValuePair keyValuePair in this.m_pairs)
				{
					IAsyncInitializationBehavior asyncInitializationBehavior = keyValuePair.Value as IAsyncInitializationBehavior;
					if (asyncInitializationBehavior != null && asyncInitializationBehavior.Container != this && !asyncInitializationBehavior.IsReady && asyncInitializationBehavior.IsActive)
					{
						list.Add(asyncInitializationBehavior);
					}
				}
			}
			if (this.m_nestedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					widgetInstance.WillLoadSynchronously = (widgetInstance.WillLoadSynchronously || base.WillLoadSynchronously);
					if (Application.IsPlaying(this) && widgetInstance.IsActive)
					{
						widgetInstance.DeferredWidgetBehaviorInitialization = true;
					}
				}
			}
			this.m_numComponentsPendingInitialization = list.Count;
			if (this.m_numComponentsPendingInitialization > 0)
			{
				using (List<IAsyncInitializationBehavior>.Enumerator enumerator3 = list.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						IAsyncInitializationBehavior asyncInitializationBehavior2 = enumerator3.Current;
						asyncInitializationBehavior2.RegisterActivatedListener(new Action<object>(this.HandleComponentActivated), asyncInitializationBehavior2);
						asyncInitializationBehavior2.RegisterDeactivatedListener(new Action<object>(this.HandleComponentDeactivated), asyncInitializationBehavior2);
						asyncInitializationBehavior2.RegisterReadyListener(new Action<object>(this.HandleComponentReady), asyncInitializationBehavior2, true);
					}
					return;
				}
			}
			this.HandleAllAsyncBehaviorsReady();
		}

		// Token: 0x0600B413 RID: 46099 RVA: 0x00375F74 File Offset: 0x00374174
		private void HandleAllAsyncBehaviorsReady()
		{
			base.TriggerOnReady();
			if (!this.DeferredWidgetBehaviorInitialization)
			{
				this.InitializeWidgetBehaviors();
			}
		}

		// Token: 0x0600B414 RID: 46100 RVA: 0x00375F8C File Offset: 0x0037418C
		public void ResetUpdateTargets()
		{
			this.m_updateTargets = WidgetTemplate.UpdateTargets.All;
			if (this.m_nestedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					if (widgetInstance != null && widgetInstance.Widget != null)
					{
						widgetInstance.Widget.ResetUpdateTargets();
					}
				}
			}
		}

		// Token: 0x0600B415 RID: 46101 RVA: 0x0037600C File Offset: 0x0037420C
		private void AddUpdateTarget(WidgetTemplate.UpdateTargets flag)
		{
			this.m_updateTargets |= flag;
		}

		// Token: 0x0600B416 RID: 46102 RVA: 0x0037601C File Offset: 0x0037421C
		private void ClearUpdateTarget(WidgetTemplate.UpdateTargets flag)
		{
			this.m_updateTargets &= ~flag;
		}

		// Token: 0x0600B417 RID: 46103 RVA: 0x0037602D File Offset: 0x0037422D
		private bool ShouldUpdateTarget(WidgetTemplate.UpdateTargets flag)
		{
			return (this.m_updateTargets & flag) > WidgetTemplate.UpdateTargets.None;
		}

		// Token: 0x0600B418 RID: 46104 RVA: 0x0037603C File Offset: 0x0037423C
		public void Tick()
		{
			if (this.m_nestedInstances != null && this.ShouldUpdateTarget(WidgetTemplate.UpdateTargets.Children))
			{
				this.ClearUpdateTarget(WidgetTemplate.UpdateTargets.Children);
				if (this.m_widgetsToTickThisIteration == null)
				{
					this.m_widgetsToTickThisIteration = new HashSet<WidgetTemplate>();
				}
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					if (widgetInstance != null && widgetInstance.Widget != null)
					{
						this.m_widgetsToTickThisIteration.Add(widgetInstance.Widget);
					}
				}
			}
			if (this.m_widgetsPendingTick != null)
			{
				if (this.m_widgetsToTickThisIteration == null)
				{
					this.m_widgetsToTickThisIteration = new HashSet<WidgetTemplate>();
				}
				foreach (WidgetTemplate widgetTemplate in this.m_widgetsPendingTick)
				{
					if (widgetTemplate != null)
					{
						this.m_widgetsToTickThisIteration.Add(widgetTemplate);
					}
				}
				this.m_widgetsPendingTick.Clear();
			}
			if (this.m_widgetsToTickThisIteration != null)
			{
				foreach (WidgetTemplate widgetTemplate2 in this.m_widgetsToTickThisIteration)
				{
					if (widgetTemplate2 != null)
					{
						widgetTemplate2.Tick();
					}
				}
			}
			if (this.ShouldUpdateTarget(WidgetTemplate.UpdateTargets.Behaviors) && this.m_widgetBehaviors != null)
			{
				this.ClearUpdateTarget(WidgetTemplate.UpdateTargets.Behaviors);
				foreach (WidgetBehavior widgetBehavior in this.m_widgetBehaviors)
				{
					if (widgetBehavior != null && widgetBehavior.CanTick)
					{
						widgetBehavior.OnUpdate();
					}
				}
			}
			if (this.m_deactivatedComponents != null && this.m_deactivatedComponents.Count > 0 && this.m_initializationState <= WidgetTemplate.InitializationState.InitializingWidget)
			{
				for (int i = this.m_deactivatedComponents.Count - 1; i >= 0; i--)
				{
					IAsyncInitializationBehavior asyncBehavior = this.m_deactivatedComponents[i];
					this.HandleComponentReady(asyncBehavior);
					this.m_deactivatedComponents.RemoveAt(i);
				}
			}
			this.CheckIfDoneChangingStates();
		}

		// Token: 0x0600B419 RID: 46105 RVA: 0x0037627C File Offset: 0x0037447C
		private void CheckIfDoneChangingStates()
		{
			List<IStatefulWidgetComponent> list = new List<IStatefulWidgetComponent>();
			foreach (IStatefulWidgetComponent statefulWidgetComponent in this.m_componentsChangingStates)
			{
				if (!statefulWidgetComponent.IsChangingStates)
				{
					global::Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " did not receive HandleDoneChangingStates from " + Widget.GetObjectDebugName(statefulWidgetComponent), Array.Empty<object>());
					list.Add(statefulWidgetComponent);
				}
			}
			foreach (IStatefulWidgetComponent context in list)
			{
				this.HandleDoneChangingStates(context);
			}
			if (this.m_componentsChangingStates.Count == 0)
			{
				if (this.m_initializationState == WidgetTemplate.InitializationState.InitializingWidgetBehaviors && !this.WaitForParentToShow)
				{
					this.FinalizeInitialization(!this.IsDesiredHiddenInHierarchy);
				}
				if (this.m_changingStatesInternally && (this.m_widgetsPendingTick == null || this.m_widgetsPendingTick.Count == 0))
				{
					this.m_changingStatesInternally = false;
					if (this.CanSendStateChanges && this.m_startChangingStatesEvent.IsSet)
					{
						this.m_startChangingStatesEvent.Clear();
						this.m_doneChangingStatesEvent.SetAndDispatch();
					}
				}
			}
		}

		// Token: 0x0600B41A RID: 46106 RVA: 0x003763C4 File Offset: 0x003745C4
		private void RegisterChildPendingTick(WidgetTemplate nestedWidget)
		{
			if (this.m_widgetsPendingTick == null)
			{
				this.m_widgetsPendingTick = new HashSet<WidgetTemplate>();
			}
			this.m_widgetsPendingTick.Add(nestedWidget);
			if (!(this.m_parentWidgetTemplate == null))
			{
				this.m_parentWidgetTemplate.RegisterChildPendingTick(this);
				return;
			}
			WidgetRunner widgetRunner = HearthstoneServices.Get<WidgetRunner>();
			if (widgetRunner == null)
			{
				return;
			}
			widgetRunner.AddWidgetPendingTick(this);
		}

		// Token: 0x0600B41B RID: 46107 RVA: 0x0037641C File Offset: 0x0037461C
		public void InitializeWidgetBehaviors()
		{
			if (this.m_initializationState != WidgetTemplate.InitializationState.InitializingWidget)
			{
				return;
			}
			this.DeferredWidgetBehaviorInitialization = false;
			this.m_initializationState = WidgetTemplate.InitializationState.InitializingWidgetBehaviors;
			if (this.m_nestedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					if (widgetInstance != null)
					{
						widgetInstance.InitializeWidgetBehaviors();
					}
				}
			}
			if (this.m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in this.m_widgetBehaviors)
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
			this.HandleDoneChangingStates(this);
			this.CheckIfDoneChangingStates();
		}

		// Token: 0x0600B41C RID: 46108 RVA: 0x00376504 File Offset: 0x00374704
		public override void SetLayerOverride(GameLayer layerOverride)
		{
			if (layerOverride < GameLayer.Default)
			{
				return;
			}
			if (this.m_prevLayersByObject == null)
			{
				this.m_prevLayersByObject = new Map<GameObject, int>();
			}
			this.SetLayerOverrideForObject(layerOverride, base.gameObject, this.m_prevLayersByObject);
		}

		// Token: 0x0600B41D RID: 46109 RVA: 0x00376534 File Offset: 0x00374734
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
					foreach (ILayerOverridable layerOverridable in components)
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

		// Token: 0x0600B41E RID: 46110 RVA: 0x00376580 File Offset: 0x00374780
		public override void ClearLayerOverride()
		{
			if (this.m_prevLayersByObject != null && this.m_prevLayersByObject.ContainsKey(base.gameObject))
			{
				int originalRootLayer = this.m_prevLayersByObject[base.gameObject];
				SceneUtils.WalkSelfAndChildren(base.transform, delegate(Transform child)
				{
					bool result = true;
					ILayerOverridable[] components = child.GetComponents<ILayerOverridable>();
					if (components != null && components.Length != 0)
					{
						foreach (ILayerOverridable layerOverridable in components)
						{
							layerOverridable.ClearLayerOverride();
							if (layerOverridable.HandlesChildLayers)
							{
								result = false;
								break;
							}
						}
					}
					int layer;
					if (this.m_prevLayersByObject.TryGetValue(child.gameObject, out layer))
					{
						child.gameObject.layer = layer;
					}
					else
					{
						child.gameObject.layer = originalRootLayer;
						global::Log.UIStatus.PrintWarning("Couldn't find original layer for GameObject " + string.Format("{0} ({1}) so setting it to widget owner's layer.", child.name, child.gameObject.GetInstanceID()), Array.Empty<object>());
					}
					return result;
				});
				this.m_prevLayersByObject = null;
			}
		}

		// Token: 0x0600B41F RID: 46111 RVA: 0x003765EC File Offset: 0x003747EC
		private void HandleComponentActivated(object payload)
		{
			if (payload == null)
			{
				global::Log.UIStatus.PrintError(string.Format("WidgetTemplate {0} ({1}) attempted to handle activated async component but it was null!", base.name, base.GetInstanceID()), Array.Empty<object>());
				return;
			}
			IAsyncInitializationBehavior asyncInitializationBehavior = (IAsyncInitializationBehavior)payload;
			if (this.m_deactivatedComponents != null && this.m_deactivatedComponents.Contains(payload) && this.m_initializationState == WidgetTemplate.InitializationState.InitializingWidget)
			{
				Widget widget = asyncInitializationBehavior as Widget;
				if (widget != null)
				{
					widget.DeferredWidgetBehaviorInitialization = true;
				}
				asyncInitializationBehavior.RegisterReadyListener(new Action<object>(this.HandleComponentReady), payload, true);
				this.m_deactivatedComponents.Remove(asyncInitializationBehavior);
			}
		}

		// Token: 0x0600B420 RID: 46112 RVA: 0x00376688 File Offset: 0x00374888
		private void HandleComponentDeactivated(object payload)
		{
			if (this.m_initializationState != WidgetTemplate.InitializationState.InitializingWidget)
			{
				return;
			}
			if (payload == null)
			{
				global::Log.UIStatus.PrintError(string.Format("WidgetTemplate {0} ({1}) attempted to handle deactivated async component but it was null!", base.name, base.GetInstanceID()), Array.Empty<object>());
				return;
			}
			IAsyncInitializationBehavior asyncInitializationBehavior = (IAsyncInitializationBehavior)payload;
			if (this.m_deactivatedComponents == null)
			{
				this.m_deactivatedComponents = new List<IAsyncInitializationBehavior>();
			}
			Widget widget = asyncInitializationBehavior as Widget;
			if (widget != null)
			{
				widget.DeferredWidgetBehaviorInitialization = false;
			}
			if (!asyncInitializationBehavior.IsReady && !this.m_deactivatedComponents.Contains(asyncInitializationBehavior))
			{
				this.m_deactivatedComponents.Add(asyncInitializationBehavior);
				asyncInitializationBehavior.RemoveReadyListener(new Action<object>(this.HandleComponentReady));
			}
		}

		// Token: 0x0600B421 RID: 46113 RVA: 0x00376732 File Offset: 0x00374932
		private void HandleComponentReady(object asyncBehavior)
		{
			if (this.m_numComponentsPendingInitialization > 0)
			{
				this.m_numComponentsPendingInitialization--;
				if (this.m_numComponentsPendingInitialization == 0)
				{
					this.HandleAllAsyncBehaviorsReady();
				}
			}
		}

		// Token: 0x0600B422 RID: 46114 RVA: 0x0037675C File Offset: 0x0037495C
		private void HandleStartChangingStates(object context)
		{
			IStatefulWidgetComponent statefulWidgetComponent = context as IStatefulWidgetComponent;
			if (statefulWidgetComponent == null)
			{
				global::Log.UIStatus.PrintWarning("WidgetTemplate " + base.gameObject.name + " received HandleStartChangingStates with invalid context", Array.Empty<object>());
				return;
			}
			if (!this.m_componentsChangingStates.Contains(statefulWidgetComponent))
			{
				this.m_componentsChangingStates.Add(statefulWidgetComponent);
			}
			else
			{
				global::Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " received HandleStartChangingStates more than once without a HandleDoneChangingStates for " + Widget.GetObjectDebugName(statefulWidgetComponent), Array.Empty<object>());
			}
			if (!this.m_changingStatesInternally)
			{
				this.m_changingStatesInternally = true;
				if (this.CanSendStateChanges && !this.m_startChangingStatesEvent.IsSet)
				{
					this.m_doneChangingStatesEvent.Clear();
					this.m_startChangingStatesEvent.SetAndDispatch();
				}
			}
		}

		// Token: 0x0600B423 RID: 46115 RVA: 0x00376824 File Offset: 0x00374A24
		private void HandleDoneChangingStates(object context)
		{
			IStatefulWidgetComponent statefulWidgetComponent = context as IStatefulWidgetComponent;
			if (statefulWidgetComponent == null)
			{
				global::Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " received HandleDoneChangingStates with invalid context", Array.Empty<object>());
				return;
			}
			if (!this.m_componentsChangingStates.Remove(statefulWidgetComponent))
			{
				global::Log.UIStatus.PrintWarning("WidgetTemplate " + Widget.GetObjectDebugName(this) + " received HandleDoneChangingStates without HandleStartChangingStates for " + Widget.GetObjectDebugName(context), Array.Empty<object>());
				return;
			}
		}

		// Token: 0x0600B424 RID: 46116 RVA: 0x0037689C File Offset: 0x00374A9C
		private void FinalizeInitialization(bool tryShow)
		{
			if (tryShow && !this.IsDesiredHidden)
			{
				this.ShowOrHide(true, false);
			}
			if (this.m_nestedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					if (widgetInstance.Widget != null && widgetInstance.Widget.WaitForParentToShow && widgetInstance.Widget.IsActive)
					{
						if (widgetInstance.Widget.m_initializationState != WidgetTemplate.InitializationState.InitializingWidgetBehaviors)
						{
							global::Log.UIStatus.PrintError(string.Concat(new string[]
							{
								"WidgetTemplate ",
								Widget.GetObjectDebugName(this),
								" attempted to finalize and show child widget ",
								Widget.GetObjectDebugName(widgetInstance.Widget),
								", but child was not done with its init!"
							}), Array.Empty<object>());
						}
						else
						{
							widgetInstance.Widget.FinalizeInitialization(tryShow && !this.IsDesiredHidden);
						}
					}
				}
			}
			this.m_willTickWhileInactive = false;
			this.m_initializationState = WidgetTemplate.InitializationState.Done;
		}

		// Token: 0x0600B425 RID: 46117 RVA: 0x003769BC File Offset: 0x00374BBC
		public DataContext GetDataContextForGameObject(GameObject go)
		{
			WidgetTemplate.GameObjectBinding gameObjectBinding;
			if (this.m_gameObjectsToBindingsMap != null && this.m_gameObjectsToBindingsMap.TryGetValue(go, out gameObjectBinding))
			{
				return gameObjectBinding.DataContext;
			}
			return this.m_dataContext;
		}

		// Token: 0x0600B426 RID: 46118 RVA: 0x003769F0 File Offset: 0x00374BF0
		private WidgetTemplate.GameObjectBinding CreateCustomDataContextForGameObject(GameObject go, DataContext original)
		{
			if (this.m_gameObjectsToBindingsMap == null)
			{
				this.m_gameObjectsToBindingsMap = new Map<GameObject, WidgetTemplate.GameObjectBinding>();
			}
			DataContext dataContext = new DataContext();
			WidgetTemplate.GameObjectBinding gameObjectBinding = new WidgetTemplate.GameObjectBinding(dataContext);
			this.m_gameObjectsToBindingsMap[go] = gameObjectBinding;
			if (original != null && original != this.m_dataContext)
			{
				foreach (IDataModel model in original.GetDataModels())
				{
					dataContext.BindDataModel(model);
				}
			}
			return gameObjectBinding;
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600B427 RID: 46119 RVA: 0x00005576 File Offset: 0x00003776
		public WidgetTemplate OwningWidget
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600B428 RID: 46120 RVA: 0x00376A78 File Offset: 0x00374C78
		public WidgetEventListenerResponse EventReceived(string eventName)
		{
			if (this.m_eventListeners != null)
			{
				this.m_eventListeners(eventName);
			}
			return default(WidgetEventListenerResponse);
		}

		// Token: 0x0600B429 RID: 46121 RVA: 0x00376AA2 File Offset: 0x00374CA2
		public override void RegisterEventListener(Widget.EventListenerDelegate listener)
		{
			this.m_eventListeners -= listener;
			this.m_eventListeners += listener;
		}

		// Token: 0x0600B42A RID: 46122 RVA: 0x00376AB2 File Offset: 0x00374CB2
		public override void RemoveEventListener(Widget.EventListenerDelegate listener)
		{
			this.m_eventListeners -= listener;
		}

		// Token: 0x0600B42B RID: 46123 RVA: 0x00376ABB File Offset: 0x00374CBB
		public override void Show()
		{
			this.IsDesiredHidden = false;
			if (this.m_initializationState == WidgetTemplate.InitializationState.Done)
			{
				this.ShowOrHide(true, true);
			}
		}

		// Token: 0x0600B42C RID: 46124 RVA: 0x00376AD5 File Offset: 0x00374CD5
		public override void Hide()
		{
			this.IsDesiredHidden = true;
			if (this.m_initializationState == WidgetTemplate.InitializationState.Done)
			{
				this.ShowOrHide(false, true);
			}
		}

		// Token: 0x0600B42D RID: 46125 RVA: 0x00376AF0 File Offset: 0x00374CF0
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
				foreach (Component component in components)
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
					SceneUtils.SetInvisibleRenderer(renderer, show, ref this.m_originalRendererLayers);
				}
				if (pegUIElement != null && (this.m_initializationStartTime > pegUIElement.SetEnabledLastCallTime || this.m_initializationState == WidgetTemplate.InitializationState.Done))
				{
					pegUIElement.SetEnabled(show, true);
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
					visibleWidgetComponent.SetVisibility(show && !visibleWidgetComponent.IsDesiredHidden, true);
					flag = (flag || visibleWidgetComponent.HandlesChildVisibility);
				}
				if (widgetInstance != null)
				{
					if (recursive && widgetInstance.Widget != null && !widgetInstance.Widget.IsDesiredHidden && widgetInstance.Widget.GetInitializationState() == WidgetTemplate.InitializationState.Done)
					{
						widgetInstance.Widget.ShowOrHide(show, true);
					}
					flag = true;
				}
				return !flag;
			});
		}

		// Token: 0x0600B42E RID: 46126 RVA: 0x00376B30 File Offset: 0x00374D30
		public override void UnbindDataModel(int id)
		{
			if (this.m_dataContext != null)
			{
				this.m_dataContext.UnbindDataModel(id);
			}
			if (this.m_gameObjectsToBindingsMap != null)
			{
				foreach (KeyValuePair<GameObject, WidgetTemplate.GameObjectBinding> keyValuePair in this.m_gameObjectsToBindingsMap)
				{
					keyValuePair.Value.UnbindDataModel(id);
				}
			}
			if (this.m_nestedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
				{
					widgetInstance.UnbindDataModel(id);
				}
			}
		}

		// Token: 0x0600B42F RID: 46127 RVA: 0x00376BF0 File Offset: 0x00374DF0
		public override void BindDataModel(IDataModel dataModel, bool overrideChildren = false)
		{
			if (this.m_dataContext == null)
			{
				this.m_dataContext = new DataContext();
				this.m_dataContext.RegisterChangedListener(new DataContext.DataChangedDelegate(this.HandleDataChanged));
			}
			if (this.m_gameObjectsToBindingsMap == null)
			{
				this.m_gameObjectsToBindingsMap = new Map<GameObject, WidgetTemplate.GameObjectBinding>();
			}
			this.m_dataContext.BindDataModel(dataModel);
			if (overrideChildren)
			{
				if (this.m_nestedInstances != null)
				{
					foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
					{
						widgetInstance.UnbindDataModel(dataModel.DataModelId);
					}
				}
				foreach (KeyValuePair<GameObject, WidgetTemplate.GameObjectBinding> keyValuePair in this.m_gameObjectsToBindingsMap)
				{
					keyValuePair.Value.UnbindDataModel(dataModel.DataModelId);
				}
			}
		}

		// Token: 0x0600B430 RID: 46128 RVA: 0x00376CEC File Offset: 0x00374EEC
		public override bool BindDataModel(IDataModel dataModel, string targetName, bool propagateToChildren = true, bool overrideChildren = false)
		{
			if (this.m_dataContext == null)
			{
				this.m_dataContext = new DataContext();
				this.m_dataContext.RegisterChangedListener(new DataContext.DataChangedDelegate(this.HandleDataChanged));
			}
			if (this.m_gameObjectsToBindingsMap == null)
			{
				this.m_gameObjectsToBindingsMap = new Map<GameObject, WidgetTemplate.GameObjectBinding>();
			}
			GameObject target = null;
			foreach (WidgetTemplate.KeyValuePair keyValuePair in this.m_pairs)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.name == targetName)
				{
					target = keyValuePair.Value.gameObject;
					break;
				}
			}
			return this.BindDataModel(dataModel, target, propagateToChildren, overrideChildren);
		}

		// Token: 0x0600B431 RID: 46129 RVA: 0x00376DB4 File Offset: 0x00374FB4
		public bool BindDataModel(IDataModel dataModel, GameObject target, bool propagateToChildren = true, bool overrideChildren = false)
		{
			if (target == null)
			{
				return false;
			}
			if (this.m_componentsSet == null)
			{
				this.m_componentsSet = new HashSet<Component>();
				foreach (WidgetTemplate.KeyValuePair keyValuePair in this.m_pairs)
				{
					this.m_componentsSet.Add(keyValuePair.Value);
				}
				if (this.m_addedInstances != null)
				{
					foreach (WidgetInstance widgetInstance in this.m_addedInstances)
					{
						this.m_componentsSet.Add(widgetInstance.transform);
					}
				}
			}
			if (!this.m_componentsSet.Contains(target.transform))
			{
				Hearthstone.UI.Logging.Log.Get().AddMessage("Tried binding a data model to a game object that does not belong to this template", base.gameObject, LogLevel.Error, "");
				return false;
			}
			if (target == base.gameObject)
			{
				this.BindDataModel(dataModel, overrideChildren);
				return true;
			}
			this.BindDataModel_Recursive(dataModel, target, propagateToChildren, overrideChildren, true, this.m_dataContext);
			this.ProcessNewGameObjectBinds();
			return true;
		}

		// Token: 0x0600B432 RID: 46130 RVA: 0x00376EEC File Offset: 0x003750EC
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
			WidgetTemplate.GameObjectBinding gameObjectBinding;
			if (this.m_gameObjectsToBindingsMap == null || !this.m_gameObjectsToBindingsMap.TryGetValue(current, out gameObjectBinding))
			{
				gameObjectBinding = this.CreateCustomDataContextForGameObject(current, parentDataContext);
			}
			else
			{
				num = gameObjectBinding.DataContext.GetLocalDataVersion();
			}
			IDataModel dataModel2;
			if (target)
			{
				gameObjectBinding.BindDataModel(dataModel, true);
			}
			else if (overrideChildren || !gameObjectBinding.DataContext.GetDataModel(dataModel.DataModelId, out dataModel2) || !gameObjectBinding.OwnsDataModel(dataModel))
			{
				gameObjectBinding.BindDataModel(dataModel, false);
			}
			if (num != gameObjectBinding.DataContext.GetLocalDataVersion())
			{
				if (this.m_newlyBoundGameObjects == null)
				{
					this.m_newlyBoundGameObjects = new List<GameObject>();
				}
				this.m_newlyBoundGameObjects.Add(current);
			}
			if (propagateToChildren)
			{
				Transform transform = current.transform;
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if (this.m_componentsSet.Contains(child))
					{
						this.BindDataModel_Recursive(dataModel, child.gameObject, true, overrideChildren, false, gameObjectBinding.DataContext);
					}
				}
			}
		}

		// Token: 0x0600B433 RID: 46131 RVA: 0x0037700C File Offset: 0x0037520C
		public override bool GetDataModel(int id, out IDataModel dataModel)
		{
			if (this.m_dataContext.GetDataModel(id, out dataModel))
			{
				return true;
			}
			Transform parent = base.transform.parent;
			return this.ParentWidgetTemplate != null && parent != null && this.ParentWidgetTemplate.GetDataModel(id, parent.gameObject, out dataModel);
		}

		// Token: 0x0600B434 RID: 46132 RVA: 0x00377064 File Offset: 0x00375264
		public override bool GetDataModel(int id, string targetName, out IDataModel model)
		{
			GameObject gameObject = null;
			foreach (WidgetTemplate.KeyValuePair keyValuePair in this.m_pairs)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.name == targetName)
				{
					gameObject = keyValuePair.Value.gameObject;
					break;
				}
			}
			if (gameObject == null)
			{
				model = null;
				return false;
			}
			return this.GetDataModel(id, gameObject, out model);
		}

		// Token: 0x0600B435 RID: 46133 RVA: 0x003770F8 File Offset: 0x003752F8
		public bool GetDataModel(int id, GameObject target, out IDataModel model)
		{
			DataContext dataContextForGameObject = this.GetDataContextForGameObject(target);
			return (dataContextForGameObject != null && dataContextForGameObject.GetDataModel(id, out model)) || this.GetDataModel(id, out model);
		}

		// Token: 0x0600B436 RID: 46134 RVA: 0x00377124 File Offset: 0x00375324
		public ICollection<IDataModel> GetDataModels()
		{
			return this.m_dataContext.GetDataModels();
		}

		// Token: 0x0600B437 RID: 46135 RVA: 0x00377134 File Offset: 0x00375334
		private void TryHandleDataChanged(IDataModel dataModel, WidgetTemplate.DataChangeSource changeType)
		{
			if (changeType == WidgetTemplate.DataChangeSource.Parent && this.m_dataContext.HasDataModel(dataModel.DataModelId))
			{
				return;
			}
			bool flag = false;
			if (this.m_widgetBehaviors != null)
			{
				foreach (WidgetBehavior widgetBehavior in this.m_widgetBehaviors)
				{
					if (!(widgetBehavior == null))
					{
						bool flag2 = this.HasGameObjectBinding(widgetBehavior.gameObject, dataModel.DataModelId);
						if ((changeType != WidgetTemplate.DataChangeSource.GameObjectBinding || flag2) && (changeType == WidgetTemplate.DataChangeSource.GameObjectBinding || !flag2) && widgetBehavior.TryIncrementDataVersion(dataModel.DataModelId))
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				int dataVersion = this.DataVersion + 1;
				this.DataVersion = dataVersion;
				if (this.m_parentWidgetTemplate == null)
				{
					WidgetRunner widgetRunner = HearthstoneServices.Get<WidgetRunner>();
					if (widgetRunner != null)
					{
						widgetRunner.AddWidgetPendingTick(this);
					}
				}
				else
				{
					this.m_parentWidgetTemplate.RegisterChildPendingTick(this);
					this.AddUpdateTarget(WidgetTemplate.UpdateTargets.Behaviors);
				}
			}
			if (changeType == WidgetTemplate.DataChangeSource.Global || this.m_nestedInstances == null)
			{
				return;
			}
			foreach (WidgetInstance widgetInstance in this.m_nestedInstances)
			{
				if (!(widgetInstance == null))
				{
					bool flag3 = this.HasGameObjectBinding(widgetInstance.gameObject, dataModel.DataModelId);
					if ((changeType != WidgetTemplate.DataChangeSource.GameObjectBinding || flag3) && (changeType == WidgetTemplate.DataChangeSource.GameObjectBinding || !flag3) && widgetInstance.Widget != null)
					{
						widgetInstance.Widget.TryHandleDataChanged(dataModel, WidgetTemplate.DataChangeSource.Parent);
					}
				}
			}
		}

		// Token: 0x0600B438 RID: 46136 RVA: 0x003772C4 File Offset: 0x003754C4
		private void HandleDataChanged(IDataModel dataModel)
		{
			this.TryHandleDataChanged(dataModel, WidgetTemplate.DataChangeSource.Template);
		}

		// Token: 0x0600B439 RID: 46137 RVA: 0x003772CE File Offset: 0x003754CE
		private void HandleBindingDataChanged(IDataModel dataModel, WidgetTemplate.GameObjectBinding binding)
		{
			if (binding.OwnsDataModel(dataModel) && binding.DataContext.HasDataModelInstance(dataModel))
			{
				this.TryHandleDataChanged(dataModel, WidgetTemplate.DataChangeSource.GameObjectBinding);
			}
		}

		// Token: 0x0600B43A RID: 46138 RVA: 0x003772EF File Offset: 0x003754EF
		private void HandleGlobalDataChanged(IDataModel dataModel)
		{
			this.TryHandleDataChanged(dataModel, WidgetTemplate.DataChangeSource.Global);
		}

		// Token: 0x0600B43B RID: 46139 RVA: 0x003772FC File Offset: 0x003754FC
		private void ProcessNewGameObjectBinds()
		{
			if (this.m_newlyBoundGameObjects != null)
			{
				for (int i = 0; i < this.m_newlyBoundGameObjects.Count; i++)
				{
					this.ProcessNewGameObjectBinding(this.m_newlyBoundGameObjects[i]);
				}
				this.m_newlyBoundGameObjects.Clear();
			}
		}

		// Token: 0x0600B43C RID: 46140 RVA: 0x00377344 File Offset: 0x00375544
		private void ProcessNewGameObjectBinding(GameObject go)
		{
			WidgetTemplate.GameObjectBinding gameObjectBinding = this.m_gameObjectsToBindingsMap[go];
			foreach (IDataModel dataModel in gameObjectBinding.DataContext.GetDataModels())
			{
				this.HandleBindingDataChanged(dataModel, gameObjectBinding);
			}
			gameObjectBinding.RegisterChangedListener(new WidgetTemplate.GameObjectBinding.DataChangedDelegate(this.HandleBindingDataChanged));
		}

		// Token: 0x0600B43D RID: 46141 RVA: 0x003773B8 File Offset: 0x003755B8
		private bool HasGameObjectBinding(GameObject go, int id)
		{
			WidgetTemplate.GameObjectBinding gameObjectBinding = null;
			Map<GameObject, WidgetTemplate.GameObjectBinding> gameObjectsToBindingsMap = this.m_gameObjectsToBindingsMap;
			if (gameObjectsToBindingsMap != null)
			{
				gameObjectsToBindingsMap.TryGetValue(go, out gameObjectBinding);
			}
			return gameObjectBinding != null && gameObjectBinding.DataContext.HasDataModel(id);
		}

		// Token: 0x0600B43E RID: 46142 RVA: 0x003773ED File Offset: 0x003755ED
		public override Widget FindWidget(string childWidgetName)
		{
			return this.FindChildOfType<Widget>(childWidgetName);
		}

		// Token: 0x0600B43F RID: 46143 RVA: 0x003773F8 File Offset: 0x003755F8
		public override T FindWidgetComponent<T>(params string[] path)
		{
			if (path == null || path.Length == 0)
			{
				return base.GetComponent<T>();
			}
			if (path.Length == 1)
			{
				return this.FindChildOfType<T>(path[0]);
			}
			Widget widget = this;
			int num = 0;
			while (num < path.Length - 1 && widget != null)
			{
				if ((widget = widget.FindWidget(path[num])) == null)
				{
					return default(T);
				}
				num++;
			}
			if (!(widget != null))
			{
				return default(T);
			}
			return widget.FindWidgetComponent<T>(new string[]
			{
				path[num]
			});
		}

		// Token: 0x0600B440 RID: 46144 RVA: 0x00377480 File Offset: 0x00375680
		public override bool TriggerEvent(string eventName, Widget.TriggerEventParameters parameters = default(Widget.TriggerEventParameters))
		{
			return EventFunctions.TriggerEvent(base.transform, eventName, parameters);
		}

		// Token: 0x0600B441 RID: 46145 RVA: 0x00377490 File Offset: 0x00375690
		public void AddNestedInstance(WidgetInstance nestedInstance, GameObject parent = null)
		{
			if (this.m_addedInstances == null)
			{
				this.m_addedInstances = new List<WidgetInstance>();
			}
			this.m_addedInstances.Add(nestedInstance);
			if (this.m_nestedInstances == null)
			{
				this.m_nestedInstances = new List<WidgetInstance>();
			}
			this.m_nestedInstances.Add(nestedInstance);
			if (this.m_componentsSet != null)
			{
				this.m_componentsSet.Add(nestedInstance.transform);
			}
			nestedInstance.ParentWidgetTemplate = this;
			nestedInstance.RegisterStartChangingStatesListener(new Action<object>(this.HandleStartChangingStates), nestedInstance, true, false);
			nestedInstance.RegisterDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates), nestedInstance, false, false);
			nestedInstance.PreInitialize();
			if (parent != null)
			{
				DataContext dataContextForGameObject = this.GetDataContextForGameObject(parent);
				if (dataContextForGameObject != null && this.m_dataContext != dataContextForGameObject)
				{
					this.CreateCustomDataContextForGameObject(nestedInstance.gameObject, dataContextForGameObject);
					this.ProcessNewGameObjectBinding(nestedInstance.gameObject);
				}
			}
		}

		// Token: 0x0600B442 RID: 46146 RVA: 0x00377564 File Offset: 0x00375764
		public void RemoveNestedInstance(WidgetInstance nestedInstance)
		{
			nestedInstance.RemoveStartChangingStatesListener(new Action<object>(this.HandleStartChangingStates));
			nestedInstance.RemoveDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates));
			if (nestedInstance.StartedChangingStates)
			{
				this.HandleDoneChangingStates(nestedInstance);
			}
			if (this.m_addedInstances != null)
			{
				this.m_addedInstances.Remove(nestedInstance);
			}
			if (this.m_nestedInstances != null)
			{
				this.m_nestedInstances.Remove(nestedInstance);
			}
			if (nestedInstance.ParentWidgetTemplate == this)
			{
				nestedInstance.ParentWidgetTemplate = null;
			}
			if (this.m_gameObjectsToBindingsMap != null)
			{
				this.m_gameObjectsToBindingsMap.Remove(nestedInstance.gameObject);
			}
		}

		// Token: 0x0600B443 RID: 46147 RVA: 0x00377600 File Offset: 0x00375800
		public Component GetComponentById(long id)
		{
			if (id == 0L)
			{
				return base.transform;
			}
			Component result;
			this.m_componentsById.TryGetValue(id, out result);
			return result;
		}

		// Token: 0x0600B444 RID: 46148 RVA: 0x00377628 File Offset: 0x00375828
		public bool GetComponentId(Component component, out long id)
		{
			if (component == base.transform)
			{
				id = 0L;
				return true;
			}
			foreach (WidgetTemplate.KeyValuePair keyValuePair in this.m_pairs)
			{
				if (keyValuePair.Value == component)
				{
					id = keyValuePair.Key;
					return true;
				}
			}
			id = -1L;
			return false;
		}

		// Token: 0x0600B445 RID: 46149 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x0600B446 RID: 46150 RVA: 0x003776AC File Offset: 0x003758AC
		public void OnAfterDeserialize()
		{
			this.BuildComponentMap();
		}

		// Token: 0x0600B447 RID: 46151 RVA: 0x003776B4 File Offset: 0x003758B4
		private void BuildComponentMap()
		{
			this.m_componentsById = new Map<long, Component>();
			foreach (WidgetTemplate.KeyValuePair keyValuePair in this.m_pairs)
			{
				if (keyValuePair.Key != 0L)
				{
					this.m_componentsById.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			List<WidgetInstance> nestedInstances = this.m_nestedInstances;
			if (nestedInstances != null)
			{
				nestedInstances.Clear();
			}
			List<VisualController> visualControllers = this.m_visualControllers;
			if (visualControllers != null)
			{
				visualControllers.Clear();
			}
			List<WidgetBehavior> widgetBehaviors = this.m_widgetBehaviors;
			if (widgetBehaviors != null)
			{
				widgetBehaviors.Clear();
			}
			if (this.m_addedInstances != null)
			{
				if (this.m_nestedInstances == null)
				{
					this.m_nestedInstances = new List<WidgetInstance>();
				}
				foreach (WidgetInstance widgetInstance in this.m_addedInstances)
				{
					if (widgetInstance != null)
					{
						this.m_nestedInstances.Add(widgetInstance);
					}
				}
			}
			foreach (WidgetTemplate.KeyValuePair keyValuePair2 in this.m_pairs)
			{
				WidgetInstance widgetInstance2 = keyValuePair2.Value as WidgetInstance;
				if (widgetInstance2 != null)
				{
					if (this.m_nestedInstances == null)
					{
						this.m_nestedInstances = new List<WidgetInstance>();
					}
					this.m_nestedInstances.Add(widgetInstance2);
				}
				WidgetBehavior widgetBehavior = keyValuePair2.Value as WidgetBehavior;
				if (widgetBehavior != null)
				{
					if (this.m_widgetBehaviors == null)
					{
						this.m_widgetBehaviors = new List<WidgetBehavior>();
					}
					this.m_widgetBehaviors.Add(widgetBehavior);
				}
				VisualController visualController = keyValuePair2.Value as VisualController;
				if (visualController != null)
				{
					if (this.m_visualControllers == null)
					{
						this.m_visualControllers = new List<VisualController>();
					}
					this.m_visualControllers.Add(visualController);
				}
			}
			this.ReconcileDataContextMap();
		}

		// Token: 0x0600B448 RID: 46152 RVA: 0x003778B4 File Offset: 0x00375AB4
		private void ReconcileDataContextMap()
		{
			if (this.m_gameObjectsToBindingsMap == null || this.m_gameObjectsToBindingsMap.Count <= 0)
			{
				return;
			}
			HashSet<GameObject> hashSet = new HashSet<GameObject>();
			foreach (WidgetTemplate.KeyValuePair keyValuePair in this.m_pairs)
			{
				hashSet.Add(keyValuePair.Value.gameObject);
			}
			if (this.m_addedInstances != null)
			{
				foreach (WidgetInstance widgetInstance in this.m_addedInstances)
				{
					if (!(widgetInstance == null))
					{
						hashSet.Add(widgetInstance.gameObject);
					}
				}
			}
			Map<GameObject, WidgetTemplate.GameObjectBinding> gameObjectsToBindingsMap = this.m_gameObjectsToBindingsMap;
			this.m_gameObjectsToBindingsMap = new Map<GameObject, WidgetTemplate.GameObjectBinding>();
			foreach (KeyValuePair<GameObject, WidgetTemplate.GameObjectBinding> keyValuePair2 in gameObjectsToBindingsMap)
			{
				WidgetTemplate.GameObjectBinding value = null;
				if (hashSet.Contains(keyValuePair2.Key) && gameObjectsToBindingsMap.TryGetValue(keyValuePair2.Key, out value))
				{
					this.m_gameObjectsToBindingsMap.Add(keyValuePair2.Key, value);
				}
			}
		}

		// Token: 0x0600B449 RID: 46153 RVA: 0x00377A10 File Offset: 0x00375C10
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
			return default(T);
		}

		// Token: 0x040096B5 RID: 38581
		[HideInInspector]
		[SerializeField]
		private List<WidgetTemplate.KeyValuePair> m_pairs;

		// Token: 0x040096B6 RID: 38582
		private HashSet<Component> m_componentsSet;

		// Token: 0x040096B7 RID: 38583
		private Map<long, Component> m_componentsById;

		// Token: 0x040096B8 RID: 38584
		private int m_numComponentsPendingInitialization;

		// Token: 0x040096B9 RID: 38585
		private DataContext m_dataContext = new DataContext();

		// Token: 0x040096BA RID: 38586
		private List<WidgetBehavior> m_widgetBehaviors;

		// Token: 0x040096BB RID: 38587
		private List<WidgetInstance> m_nestedInstances;

		// Token: 0x040096BC RID: 38588
		private List<WidgetInstance> m_addedInstances;

		// Token: 0x040096BD RID: 38589
		private List<VisualController> m_visualControllers;

		// Token: 0x040096BE RID: 38590
		private List<IAsyncInitializationBehavior> m_deactivatedComponents;

		// Token: 0x040096BF RID: 38591
		private Map<GameObject, WidgetTemplate.GameObjectBinding> m_gameObjectsToBindingsMap;

		// Token: 0x040096C0 RID: 38592
		private List<GameObject> m_newlyBoundGameObjects;

		// Token: 0x040096C2 RID: 38594
		private WidgetTemplate.InitializationState m_initializationState;

		// Token: 0x040096C3 RID: 38595
		private WidgetTemplate m_parentWidgetTemplate;

		// Token: 0x040096C4 RID: 38596
		private float m_initializationStartTime;

		// Token: 0x040096C5 RID: 38597
		private bool m_waitForParentToShow = true;

		// Token: 0x040096C6 RID: 38598
		private WidgetTemplate.UpdateTargets m_updateTargets = WidgetTemplate.UpdateTargets.All;

		// Token: 0x040096C7 RID: 38599
		private HashSet<WidgetTemplate> m_widgetsPendingTick;

		// Token: 0x040096C8 RID: 38600
		private HashSet<WidgetTemplate> m_widgetsToTickThisIteration;

		// Token: 0x040096C9 RID: 38601
		private List<IStatefulWidgetComponent> m_componentsChangingStates = new List<IStatefulWidgetComponent>();

		// Token: 0x040096CA RID: 38602
		private bool m_enabledInternally;

		// Token: 0x040096CB RID: 38603
		private bool m_willTickWhileInactive;

		// Token: 0x040096CC RID: 38604
		private bool m_changingStatesInternally;

		// Token: 0x040096CD RID: 38605
		private Map<GameObject, int> m_prevLayersByObject;

		// Token: 0x040096CE RID: 38606
		[HideInInspector]
		[SerializeField]
		private List<int> m_dataModelHints_editorOnly;

		// Token: 0x040096D1 RID: 38609
		private Map<Renderer, int> m_originalRendererLayers;

		// Token: 0x0200284E RID: 10318
		public enum InitializationState
		{
			// Token: 0x0400F919 RID: 63769
			NotStarted,
			// Token: 0x0400F91A RID: 63770
			InitializingWidget,
			// Token: 0x0400F91B RID: 63771
			InitializingWidgetBehaviors,
			// Token: 0x0400F91C RID: 63772
			Done
		}

		// Token: 0x0200284F RID: 10319
		private enum DataChangeSource
		{
			// Token: 0x0400F91E RID: 63774
			Parent,
			// Token: 0x0400F91F RID: 63775
			Template,
			// Token: 0x0400F920 RID: 63776
			GameObjectBinding,
			// Token: 0x0400F921 RID: 63777
			Global
		}

		// Token: 0x02002850 RID: 10320
		[Flags]
		public enum UpdateTargets
		{
			// Token: 0x0400F923 RID: 63779
			None = 0,
			// Token: 0x0400F924 RID: 63780
			Children = 2,
			// Token: 0x0400F925 RID: 63781
			Behaviors = 4,
			// Token: 0x0400F926 RID: 63782
			All = 6
		}

		// Token: 0x02002851 RID: 10321
		private class GameObjectBinding
		{
			// Token: 0x17002D28 RID: 11560
			// (get) Token: 0x06013B91 RID: 80785 RVA: 0x0053AFA6 File Offset: 0x005391A6
			public DataContext DataContext
			{
				get
				{
					return this.m_dataContext;
				}
			}

			// Token: 0x06013B92 RID: 80786 RVA: 0x0053AFAE File Offset: 0x005391AE
			public GameObjectBinding(DataContext dataContext)
			{
				this.m_dataContext = dataContext;
				this.m_dataContext.RegisterChangedListener(new DataContext.DataChangedDelegate(this.HandleDataContextChanged));
			}

			// Token: 0x06013B93 RID: 80787 RVA: 0x0053AFDF File Offset: 0x005391DF
			public void BindDataModel(IDataModel dataModel, bool owned)
			{
				if (owned)
				{
					this.m_ownedDataModels.Add(dataModel.DataModelId);
				}
				else
				{
					this.m_ownedDataModels.Remove(dataModel.DataModelId);
				}
				this.m_dataContext.BindDataModel(dataModel);
			}

			// Token: 0x06013B94 RID: 80788 RVA: 0x0053B016 File Offset: 0x00539216
			public void UnbindDataModel(int dataModelId)
			{
				this.m_dataContext.UnbindDataModel(dataModelId);
				this.m_ownedDataModels.Remove(dataModelId);
			}

			// Token: 0x06013B95 RID: 80789 RVA: 0x0053B031 File Offset: 0x00539231
			public bool OwnsDataModel(IDataModel dataModel)
			{
				return this.m_ownedDataModels.Contains(dataModel.DataModelId);
			}

			// Token: 0x06013B96 RID: 80790 RVA: 0x0053B044 File Offset: 0x00539244
			public void RegisterChangedListener(WidgetTemplate.GameObjectBinding.DataChangedDelegate listener)
			{
				this.m_onDataChanged = (WidgetTemplate.GameObjectBinding.DataChangedDelegate)Delegate.Remove(this.m_onDataChanged, listener);
				this.m_onDataChanged = (WidgetTemplate.GameObjectBinding.DataChangedDelegate)Delegate.Combine(this.m_onDataChanged, listener);
			}

			// Token: 0x06013B97 RID: 80791 RVA: 0x0053B074 File Offset: 0x00539274
			public void RemoveChangedListener(WidgetTemplate.GameObjectBinding.DataChangedDelegate listener)
			{
				this.m_onDataChanged = (WidgetTemplate.GameObjectBinding.DataChangedDelegate)Delegate.Remove(this.m_onDataChanged, listener);
			}

			// Token: 0x06013B98 RID: 80792 RVA: 0x0053B08D File Offset: 0x0053928D
			private void HandleDataContextChanged(IDataModel dataModel)
			{
				WidgetTemplate.GameObjectBinding.DataChangedDelegate onDataChanged = this.m_onDataChanged;
				if (onDataChanged == null)
				{
					return;
				}
				onDataChanged(dataModel, this);
			}

			// Token: 0x0400F927 RID: 63783
			private DataContext m_dataContext;

			// Token: 0x0400F928 RID: 63784
			private HashSet<int> m_ownedDataModels = new HashSet<int>();

			// Token: 0x0400F929 RID: 63785
			private WidgetTemplate.GameObjectBinding.DataChangedDelegate m_onDataChanged;

			// Token: 0x020029A9 RID: 10665
			// (Invoke) Token: 0x06013FAE RID: 81838
			public delegate void DataChangedDelegate(IDataModel dataModel, WidgetTemplate.GameObjectBinding binding);
		}

		// Token: 0x02002852 RID: 10322
		[Serializable]
		private struct KeyValuePair
		{
			// Token: 0x0400F92A RID: 63786
			public long Key;

			// Token: 0x0400F92B RID: 63787
			public Component Value;

			// Token: 0x020029AA RID: 10666
			public class Comparer : IEqualityComparer<WidgetTemplate.KeyValuePair>
			{
				// Token: 0x06013FB1 RID: 81841 RVA: 0x00542078 File Offset: 0x00540278
				public bool Equals(WidgetTemplate.KeyValuePair x, WidgetTemplate.KeyValuePair y)
				{
					return x.Value == y.Value;
				}

				// Token: 0x06013FB2 RID: 81842 RVA: 0x0054208B File Offset: 0x0054028B
				public int GetHashCode(WidgetTemplate.KeyValuePair obj)
				{
					return obj.Key.GetHashCode();
				}
			}
		}
	}
}
