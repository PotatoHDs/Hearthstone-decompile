using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001030 RID: 4144
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	[NestedReferenceScope(NestedReference.Scope.Children)]
	public class WidgetInstance : Widget, INestedReferenceResolver, IAsyncInitializationBehavior
	{
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600B3B8 RID: 46008 RVA: 0x00374716 File Offset: 0x00372916
		// (set) Token: 0x0600B3B9 RID: 46009 RVA: 0x0037471E File Offset: 0x0037291E
		public override bool DeferredWidgetBehaviorInitialization
		{
			get
			{
				return base.DeferredWidgetBehaviorInitialization;
			}
			set
			{
				base.DeferredWidgetBehaviorInitialization = value;
				if (this.Widget != null)
				{
					this.Widget.DeferredWidgetBehaviorInitialization = value;
				}
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600B3BA RID: 46010 RVA: 0x00374744 File Offset: 0x00372944
		// (set) Token: 0x0600B3BB RID: 46011 RVA: 0x003747A9 File Offset: 0x003729A9
		public bool WaitForParentToShow
		{
			get
			{
				return ((this.Widget != null && this.Widget.WaitForParentToShow) || (this.Widget == null && this.m_pendingWaitForParentToShow)) && this.m_parentWidgetTemplate != null && this.m_parentWidgetTemplate.GetInitializationState() != WidgetTemplate.InitializationState.Done;
			}
			set
			{
				if (this.Widget != null)
				{
					this.Widget.WaitForParentToShow = value;
				}
				this.m_pendingWaitForParentToShow = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600B3BC RID: 46012 RVA: 0x003747CC File Offset: 0x003729CC
		public bool WillPreload
		{
			get
			{
				bool flag = this.m_parentWidgetTemplate != null && this.m_parentWidgetTemplate.WillTickWhileInactive;
				return this.m_loadingPolicy == WidgetInstance.LoadingPolicy.LoadAlways || this.m_requestedPreload || (flag && base.gameObject.activeSelf);
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600B3BD RID: 46013 RVA: 0x00374819 File Offset: 0x00372A19
		public bool StartedInitialization
		{
			get
			{
				return this.m_startedInitialization;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600B3BE RID: 46014 RVA: 0x00374821 File Offset: 0x00372A21
		public override bool IsInitialized
		{
			get
			{
				return this.Widget != null && this.Widget.IsInitialized;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600B3BF RID: 46015 RVA: 0x00374840 File Offset: 0x00372A40
		public WidgetTemplate Widget
		{
			get
			{
				if (this.m_template == null)
				{
					WidgetTemplate template;
					if (this.m_prefabInstance != null)
					{
						PrefabInstance prefabInstance = this.m_prefabInstance;
						if (((prefabInstance != null) ? prefabInstance.Instance : null) != null)
						{
							template = this.m_prefabInstance.Instance.GetComponent<WidgetTemplate>();
							goto IL_44;
						}
					}
					template = null;
					IL_44:
					this.m_template = template;
				}
				return this.m_template;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600B3C0 RID: 46016 RVA: 0x0037489C File Offset: 0x00372A9C
		// (set) Token: 0x0600B3C1 RID: 46017 RVA: 0x003748A4 File Offset: 0x00372AA4
		public WidgetTemplate ParentWidgetTemplate
		{
			get
			{
				return this.m_parentWidgetTemplate;
			}
			set
			{
				this.m_parentWidgetTemplate = value;
				if (this.Widget != null)
				{
					this.Widget.ParentWidgetTemplate = value;
				}
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600B3C2 RID: 46018 RVA: 0x003748C7 File Offset: 0x00372AC7
		public override bool IsChangingStates
		{
			get
			{
				return (this.Widget == null && base.IsActive) || (this.Widget != null && this.Widget.IsChangingStates);
			}
		}

		// Token: 0x0600B3C3 RID: 46019 RVA: 0x003748FC File Offset: 0x00372AFC
		public override bool GetIsChangingStates(Func<GameObject, bool> includeGameObject)
		{
			return (this.Widget == null && base.IsActive) || (this.Widget != null && this.Widget.GetIsChangingStates(includeGameObject));
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600B3C4 RID: 46020 RVA: 0x00374932 File Offset: 0x00372B32
		public override bool HasPendingActions
		{
			get
			{
				return this.Widget != null && this.Widget.HasPendingActions;
			}
		}

		// Token: 0x0600B3C5 RID: 46021 RVA: 0x00374950 File Offset: 0x00372B50
		protected override void OnEnable()
		{
			base.OnEnable();
			if (!this.IsInitialized)
			{
				this.HandleStartChangingStates(null);
			}
			if (Application.IsPlaying(this) && this.m_wasInitialized && this.m_loadingPolicy == WidgetInstance.LoadingPolicy.UnloadOnDeactivation)
			{
				this.Initialize();
				return;
			}
			if (this.m_startedInitialization && !this.m_wasInitialized)
			{
				this.m_startedInitialization = false;
				this.Initialize();
			}
		}

		// Token: 0x0600B3C6 RID: 46022 RVA: 0x003749AF File Offset: 0x00372BAF
		protected override void OnDisable()
		{
			base.OnDisable();
			if (!this.IsInitialized)
			{
				this.HandleDoneChangingStates(null);
			}
			if (Application.IsPlaying(this) && this.m_startedInitialization && this.m_loadingPolicy == WidgetInstance.LoadingPolicy.UnloadOnDeactivation)
			{
				this.Unload();
			}
		}

		// Token: 0x0600B3C7 RID: 46023 RVA: 0x003749E5 File Offset: 0x00372BE5
		private void Start()
		{
			if (!this.m_startedInitialization)
			{
				this.Initialize();
			}
		}

		// Token: 0x0600B3C8 RID: 46024 RVA: 0x003749F5 File Offset: 0x00372BF5
		private void OnDestroy()
		{
			if (this.m_prefabInstance != null)
			{
				this.m_prefabInstance.Destroy();
			}
		}

		// Token: 0x0600B3C9 RID: 46025 RVA: 0x00374A0A File Offset: 0x00372C0A
		public void PreInitialize()
		{
			if (!this.m_startedInitialization && this.WillPreload)
			{
				this.Preload();
			}
		}

		// Token: 0x0600B3CA RID: 46026 RVA: 0x00374A24 File Offset: 0x00372C24
		public void Initialize()
		{
			if (!this.m_startedInitialization)
			{
				this.m_wasInitialized = true;
				if (!string.IsNullOrEmpty(this.m_widgetTemplate.AssetString))
				{
					this.m_startedInitialization = true;
					this.m_prefabInstance = this.GetOrCreatePrefabInstance();
					this.m_prefabInstance.InstantiateWhenReady();
				}
			}
		}

		// Token: 0x0600B3CB RID: 46027 RVA: 0x00374A70 File Offset: 0x00372C70
		public void Preload()
		{
			this.m_requestedPreload = true;
			this.Initialize();
		}

		// Token: 0x0600B3CC RID: 46028 RVA: 0x00374A80 File Offset: 0x00372C80
		public void Unload()
		{
			if (this.m_startedInitialization)
			{
				this.m_startedInitialization = false;
				this.m_startChangingStatesEvent.Clear();
				this.m_doneChangingStatesEvent.Clear();
				this.m_pendingPostInitializeActions = null;
				this.m_pendingPreInitializeActions = null;
				this.m_pendingDataContext = new DataContext();
				this.DeferredWidgetBehaviorInitialization = false;
				if (this.Widget != null)
				{
					this.m_pendingWaitForParentToShow = this.Widget.WaitForParentToShow;
					this.m_template = null;
				}
				if (this.m_prefabInstance != null)
				{
					this.m_prefabInstance.Destroy();
				}
			}
		}

		// Token: 0x0600B3CD RID: 46029 RVA: 0x00374B0B File Offset: 0x00372D0B
		public void InitializeWidgetBehaviors()
		{
			if (this.Widget != null)
			{
				this.Widget.InitializeWidgetBehaviors();
				return;
			}
			this.DeferredWidgetBehaviorInitialization = false;
		}

		// Token: 0x0600B3CE RID: 46030 RVA: 0x00374B2E File Offset: 0x00372D2E
		public Component GetComponentById(long id)
		{
			if (!(this.Widget != null))
			{
				return null;
			}
			return this.Widget.GetComponentById(id);
		}

		// Token: 0x0600B3CF RID: 46031 RVA: 0x00374B4C File Offset: 0x00372D4C
		public bool GetComponentId(Component component, out long id)
		{
			id = -1L;
			return !(this.Widget == null) && this.Widget.GetComponentId(component, out id);
		}

		// Token: 0x0600B3D0 RID: 46032 RVA: 0x00374B70 File Offset: 0x00372D70
		private void HandleWidgetTemplateInstantiated(object unused)
		{
			if (this.Widget != null)
			{
				this.Widget.WillLoadSynchronously = (this.Widget.WillLoadSynchronously || base.WillLoadSynchronously);
				this.Widget.RegisterStartChangingStatesListener(new Action<object>(this.HandleStartChangingStates), null, true, false);
				this.Widget.RegisterDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates), null, true, false);
				if (this.m_layerOverride >= 0)
				{
					this.Widget.SetLayerOverride((GameLayer)this.m_layerOverride);
				}
				else if (this.m_layerOverride == -2)
				{
					this.Widget.SetLayerOverride((GameLayer)base.gameObject.layer);
				}
				this.m_pendingDataContext.Clear();
				this.Widget.OnInstantiated();
				if (this.m_pendingPreInitializeActions != null)
				{
					foreach (Action action in this.m_pendingPreInitializeActions)
					{
						action();
					}
				}
				this.Widget.RegisterReadyListener(new Action<object>(this.HandleWidgetTemplateReady), null, true);
				this.Widget.RegisterActivatedListener(new Action<object>(this.HandleWidgetTemplateActivated), null);
				this.Widget.RegisterDeactivatedListener(new Action<object>(this.HandleWidgetTemplateDeactivated), null);
				this.Widget.ParentWidgetTemplate = this.m_parentWidgetTemplate;
				this.Widget.DeferredWidgetBehaviorInitialization = this.DeferredWidgetBehaviorInitialization;
				if (Application.IsPlaying(this))
				{
					this.Widget.WaitForParentToShow = this.m_pendingWaitForParentToShow;
				}
				if (!Application.IsPlaying(this) || this.WillPreload || this.Widget.WillLoadSynchronously)
				{
					this.Widget.Initialize(this.WillPreload);
					this.m_requestedPreload = false;
				}
			}
		}

		// Token: 0x0600B3D1 RID: 46033 RVA: 0x00374D3C File Offset: 0x00372F3C
		private void HandleWidgetTemplateLoadFailed()
		{
			if (this.Widget == null)
			{
				Log.UIStatus.PrintError(string.Concat(new string[]
				{
					"WidgetInstance ",
					base.name,
					" failed to load its prefab ",
					(!string.IsNullOrEmpty(this.m_widgetTemplate.AssetString)) ? this.m_widgetTemplate.AssetString : "ASSET REFERENCE MISSING",
					".\n Check that the widget prefab with this GUID isn't missing from the project or your widget(s) will be sad :("
				}), Array.Empty<object>());
				this.DeferredWidgetBehaviorInitialization = false;
				this.m_wasInitialized = false;
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600B3D2 RID: 46034 RVA: 0x00374DD3 File Offset: 0x00372FD3
		private void HandleWidgetTemplateReady(object unused)
		{
			this.ExecutePendingActionsAndTriggerReady();
		}

		// Token: 0x0600B3D3 RID: 46035 RVA: 0x00374DDB File Offset: 0x00372FDB
		private void HandleWidgetTemplateActivated(object unused)
		{
			if (!this.m_activatedEvent.IsSet)
			{
				this.m_deactivatedEvent.Clear();
				this.m_activatedEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B3D4 RID: 46036 RVA: 0x00374E01 File Offset: 0x00373001
		private void HandleWidgetTemplateDeactivated(object unused)
		{
			if (!this.m_deactivatedEvent.IsSet)
			{
				this.m_activatedEvent.Clear();
				this.m_deactivatedEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B3D5 RID: 46037 RVA: 0x00374E28 File Offset: 0x00373028
		private void ExecutePendingActionsAndTriggerReady()
		{
			if (this.m_pendingPostInitializeActions != null)
			{
				foreach (Action action in this.m_pendingPostInitializeActions)
				{
					action();
				}
			}
			base.TriggerOnReady();
		}

		// Token: 0x0600B3D6 RID: 46038 RVA: 0x00374E88 File Offset: 0x00373088
		public override void RegisterEventListener(Widget.EventListenerDelegate listener)
		{
			if (this.Widget != null)
			{
				this.Widget.RegisterEventListener(listener);
				return;
			}
			this.GetOrCreatePendingPreInitActions().Add(delegate
			{
				this.Widget.RegisterEventListener(listener);
			});
		}

		// Token: 0x0600B3D7 RID: 46039 RVA: 0x00374EE0 File Offset: 0x003730E0
		public override void RemoveEventListener(Widget.EventListenerDelegate listener)
		{
			if (this.Widget != null)
			{
				this.Widget.RemoveEventListener(listener);
				return;
			}
			this.GetOrCreatePendingPreInitActions().Add(delegate
			{
				this.Widget.RemoveEventListener(listener);
			});
		}

		// Token: 0x0600B3D8 RID: 46040 RVA: 0x00374F38 File Offset: 0x00373138
		private List<Action> GetOrCreatePendingPreInitActions()
		{
			if (this.m_pendingPreInitializeActions == null)
			{
				this.m_pendingPreInitializeActions = new List<Action>();
			}
			return this.m_pendingPreInitializeActions;
		}

		// Token: 0x0600B3D9 RID: 46041 RVA: 0x00374F53 File Offset: 0x00373153
		private void HandleStartChangingStates(object unused)
		{
			if (!this.m_startChangingStatesEvent.IsSet)
			{
				this.m_doneChangingStatesEvent.Clear();
				this.m_startChangingStatesEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B3DA RID: 46042 RVA: 0x00374F79 File Offset: 0x00373179
		private void HandleDoneChangingStates(object unused)
		{
			if (this.m_startChangingStatesEvent.IsSet)
			{
				this.m_startChangingStatesEvent.Clear();
				this.m_doneChangingStatesEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B3DB RID: 46043 RVA: 0x00374F9F File Offset: 0x0037319F
		public override void Show()
		{
			if (this.Widget != null)
			{
				this.Widget.Show();
				return;
			}
			this.GetOrCreatePendingPreInitActions().Add(new Action(this.Show));
		}

		// Token: 0x0600B3DC RID: 46044 RVA: 0x00374FD3 File Offset: 0x003731D3
		public override void Hide()
		{
			if (this.Widget != null)
			{
				this.Widget.Hide();
				return;
			}
			this.GetOrCreatePendingPreInitActions().Add(new Action(this.Hide));
		}

		// Token: 0x0600B3DD RID: 46045 RVA: 0x00375008 File Offset: 0x00373208
		public override void BindDataModel(IDataModel dataModel, bool overrideChildren = false)
		{
			if (this.Widget == null)
			{
				this.m_pendingDataContext.BindDataModel(dataModel);
				this.GetOrCreatePendingPreInitActions().Add(delegate
				{
					this.Widget.BindDataModel(dataModel, overrideChildren);
				});
				return;
			}
			this.Widget.BindDataModel(dataModel, overrideChildren);
		}

		// Token: 0x0600B3DE RID: 46046 RVA: 0x00375080 File Offset: 0x00373280
		public override bool BindDataModel(IDataModel dataModel, string target, bool propagateToChildren = true, bool overrideChildren = false)
		{
			if (this.Widget == null)
			{
				this.GetOrCreatePendingPreInitActions().Add(delegate
				{
					this.Widget.BindDataModel(dataModel, target, propagateToChildren, overrideChildren);
				});
				return true;
			}
			return this.Widget.BindDataModel(dataModel, target, propagateToChildren, overrideChildren);
		}

		// Token: 0x0600B3DF RID: 46047 RVA: 0x00375104 File Offset: 0x00373304
		public override bool GetDataModel(int id, out IDataModel model)
		{
			if (this.Widget != null)
			{
				return this.Widget.GetDataModel(id, out model);
			}
			if (this.m_pendingDataContext.GetDataModel(id, out model))
			{
				return true;
			}
			if (this.m_parentWidgetTemplate != null)
			{
				this.m_parentWidgetTemplate.GetDataModel(id, base.gameObject, out model);
			}
			return false;
		}

		// Token: 0x0600B3E0 RID: 46048 RVA: 0x00375161 File Offset: 0x00373361
		public override bool GetDataModel(int id, string targetName, out IDataModel model)
		{
			if (this.Widget == null)
			{
				model = null;
				return false;
			}
			return this.Widget.GetDataModel(id, targetName, out model);
		}

		// Token: 0x0600B3E1 RID: 46049 RVA: 0x00375184 File Offset: 0x00373384
		public override void UnbindDataModel(int id)
		{
			if (this.Widget == null)
			{
				this.m_pendingDataContext.UnbindDataModel(id);
				return;
			}
			this.Widget.UnbindDataModel(id);
		}

		// Token: 0x0600B3E2 RID: 46050 RVA: 0x003751B0 File Offset: 0x003733B0
		public override bool TriggerEvent(string eventName, Widget.TriggerEventParameters parameters = default(Widget.TriggerEventParameters))
		{
			if (this.Widget == null)
			{
				if (this.m_pendingPostInitializeActions == null)
				{
					this.m_pendingPostInitializeActions = new List<Action>();
				}
				this.m_pendingPostInitializeActions.Add(delegate
				{
					this.Widget.TriggerEvent(eventName, parameters);
				});
				return false;
			}
			return this.Widget.TriggerEvent(eventName, parameters);
		}

		// Token: 0x0600B3E3 RID: 46051 RVA: 0x0037522C File Offset: 0x0037342C
		public override T FindWidgetComponent<T>(params string[] path)
		{
			if (!(this.Widget != null))
			{
				return default(T);
			}
			return this.Widget.FindWidgetComponent<T>(path);
		}

		// Token: 0x0600B3E4 RID: 46052 RVA: 0x0037525D File Offset: 0x0037345D
		public override Widget FindWidget(string name)
		{
			if (!(this.Widget != null))
			{
				return null;
			}
			return this.Widget.FindWidget(name);
		}

		// Token: 0x0600B3E5 RID: 46053 RVA: 0x0037527C File Offset: 0x0037347C
		public static WidgetInstance Create(string assetString, bool deferred = false)
		{
			GameObject gameObject = new GameObject(assetString);
			gameObject.SetActive(false);
			WidgetInstance widgetInstance = gameObject.AddComponent<WidgetInstance>();
			widgetInstance.m_widgetTemplate.AssetString = assetString;
			gameObject.SetActive(!deferred);
			return widgetInstance;
		}

		// Token: 0x0600B3E6 RID: 46054 RVA: 0x003752B4 File Offset: 0x003734B4
		private PrefabInstance GetOrCreatePrefabInstance()
		{
			if (this.m_prefabInstance == null)
			{
				this.m_prefabInstance = new PrefabInstance(base.gameObject);
				this.m_prefabInstance.LoadPrefab(this.m_widgetTemplate, base.WillLoadSynchronously);
				this.m_prefabInstance.RegisterInstanceReadyListener(new Action<object>(this.HandleWidgetTemplateInstantiated));
				this.m_prefabInstance.RegisterPrefabLoadFailedListener(new Action(this.HandleWidgetTemplateLoadFailed));
			}
			else if (this.m_prefabInstance.Prefab == null)
			{
				this.m_prefabInstance.LoadPrefab(this.m_widgetTemplate, base.WillLoadSynchronously);
			}
			this.m_prefabInstance.Owner = base.gameObject;
			return this.m_prefabInstance;
		}

		// Token: 0x0600B3E7 RID: 46055 RVA: 0x0037535E File Offset: 0x0037355E
		public override void SetLayerOverride(GameLayer layerOverride)
		{
			this.m_prevLayerOverride = new int?(this.m_layerOverride);
			this.m_layerOverride = (int)layerOverride;
			if (this.Widget != null)
			{
				this.Widget.SetLayerOverride(layerOverride);
			}
		}

		// Token: 0x0600B3E8 RID: 46056 RVA: 0x00375394 File Offset: 0x00373594
		public override void ClearLayerOverride()
		{
			if (this.m_prevLayerOverride != null)
			{
				this.m_layerOverride = this.m_prevLayerOverride.Value;
				this.m_prevLayerOverride = null;
				if (this.Widget != null)
				{
					this.Widget.ClearLayerOverride();
				}
			}
		}

		// Token: 0x0400969F RID: 38559
		public const int UseTemplateLayer = -1;

		// Token: 0x040096A0 RID: 38560
		public const int UseInstanceLayer = -2;

		// Token: 0x040096A1 RID: 38561
		[HideInInspector]
		[SerializeField]
		private WeakAssetReference m_widgetTemplate;

		// Token: 0x040096A2 RID: 38562
		[HideInInspector]
		[SerializeField]
		private int m_layerOverride = -1;

		// Token: 0x040096A3 RID: 38563
		[HideInInspector]
		[SerializeField]
		private WidgetInstance.LoadingPolicy m_loadingPolicy;

		// Token: 0x040096A4 RID: 38564
		private PrefabInstance m_prefabInstance;

		// Token: 0x040096A5 RID: 38565
		private bool m_startedInitialization;

		// Token: 0x040096A6 RID: 38566
		private List<Action> m_pendingPostInitializeActions;

		// Token: 0x040096A7 RID: 38567
		private List<Action> m_pendingPreInitializeActions;

		// Token: 0x040096A8 RID: 38568
		private DataContext m_pendingDataContext = new DataContext();

		// Token: 0x040096A9 RID: 38569
		private WidgetTemplate m_parentWidgetTemplate;

		// Token: 0x040096AA RID: 38570
		private bool m_pendingWaitForParentToShow = true;

		// Token: 0x040096AB RID: 38571
		private bool m_wasInitialized;

		// Token: 0x040096AC RID: 38572
		private bool m_requestedPreload;

		// Token: 0x040096AD RID: 38573
		private int? m_prevLayerOverride;

		// Token: 0x040096AE RID: 38574
		private WidgetTemplate m_template;

		// Token: 0x02002847 RID: 10311
		private enum LoadingPolicy
		{
			// Token: 0x0400F903 RID: 63747
			LoadOnActivation,
			// Token: 0x0400F904 RID: 63748
			UnloadOnDeactivation,
			// Token: 0x0400F905 RID: 63749
			LoadAlways
		}
	}
}
