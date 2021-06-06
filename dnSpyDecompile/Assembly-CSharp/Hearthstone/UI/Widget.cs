using System;
using System.Reflection;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200102B RID: 4139
	[DisallowMultipleComponent]
	public abstract class Widget : MonoBehaviour, IAsyncInitializationBehavior, IStatefulWidgetComponent, ILayerOverridable
	{
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x0600B361 RID: 45921
		public abstract bool IsInitialized { get; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600B362 RID: 45922
		public abstract bool IsChangingStates { get; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600B363 RID: 45923
		public abstract bool HasPendingActions { get; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600B365 RID: 45925 RVA: 0x00373B31 File Offset: 0x00371D31
		// (set) Token: 0x0600B364 RID: 45924 RVA: 0x00373B28 File Offset: 0x00371D28
		public virtual bool DeferredWidgetBehaviorInitialization { get; set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600B366 RID: 45926 RVA: 0x00373B39 File Offset: 0x00371D39
		public bool StartedChangingStates
		{
			get
			{
				return this.m_startChangingStatesEvent.IsSet;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600B367 RID: 45927 RVA: 0x00005576 File Offset: 0x00003776
		public Behaviour Container
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600B368 RID: 45928 RVA: 0x00373B46 File Offset: 0x00371D46
		public bool IsReady
		{
			get
			{
				return this.m_readyState.IsSet;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600B369 RID: 45929 RVA: 0x0037060F File Offset: 0x0036E80F
		public bool IsActive
		{
			get
			{
				return base.enabled && base.gameObject.activeInHierarchy;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600B36A RID: 45930 RVA: 0x00373B53 File Offset: 0x00371D53
		// (set) Token: 0x0600B36B RID: 45931 RVA: 0x00373B5B File Offset: 0x00371D5B
		public bool WillLoadSynchronously { get; set; }

		// Token: 0x0600B36C RID: 45932 RVA: 0x00373B64 File Offset: 0x00371D64
		protected virtual void OnEnable()
		{
			this.m_deactivatedEvent.Clear();
			this.m_activatedEvent.SetAndDispatch();
		}

		// Token: 0x0600B36D RID: 45933 RVA: 0x00373B7D File Offset: 0x00371D7D
		protected virtual void OnDisable()
		{
			this.m_activatedEvent.Clear();
			this.m_deactivatedEvent.SetAndDispatch();
		}

		// Token: 0x0600B36E RID: 45934 RVA: 0x00373B96 File Offset: 0x00371D96
		public void RegisterActivatedListener(Action<object> listener, object payload = null)
		{
			this.m_activatedEvent.RegisterSetListener(listener, payload, false, false);
		}

		// Token: 0x0600B36F RID: 45935 RVA: 0x00373BA7 File Offset: 0x00371DA7
		public void RegisterDeactivatedListener(Action<object> listener, object payload = null)
		{
			this.m_deactivatedEvent.RegisterSetListener(listener, payload, false, false);
		}

		// Token: 0x0600B370 RID: 45936 RVA: 0x00373BB8 File Offset: 0x00371DB8
		public void RegisterReadyListener(Action<object> listener, object payload = null, bool callImmediatelyIfReady = true)
		{
			this.m_readyState.RegisterSetListener(listener, payload, callImmediatelyIfReady, false);
		}

		// Token: 0x0600B371 RID: 45937 RVA: 0x00373BC9 File Offset: 0x00371DC9
		public void RemoveReadyListener(Action<object> listener)
		{
			this.m_readyState.RemoveSetListener(listener);
		}

		// Token: 0x0600B372 RID: 45938 RVA: 0x00373BD7 File Offset: 0x00371DD7
		protected void TriggerOnReady()
		{
			this.m_readyState.SetAndDispatch();
		}

		// Token: 0x0600B373 RID: 45939
		public abstract void RegisterEventListener(Widget.EventListenerDelegate listener);

		// Token: 0x0600B374 RID: 45940
		public abstract void RemoveEventListener(Widget.EventListenerDelegate listener);

		// Token: 0x0600B375 RID: 45941 RVA: 0x00373BE5 File Offset: 0x00371DE5
		public virtual void RegisterDoneChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			this.m_doneChangingStatesEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet, doOnce);
		}

		// Token: 0x0600B376 RID: 45942 RVA: 0x00373BF7 File Offset: 0x00371DF7
		public virtual void RemoveDoneChangingStatesListener(Action<object> listener)
		{
			this.m_doneChangingStatesEvent.RemoveSetListener(listener);
		}

		// Token: 0x0600B377 RID: 45943 RVA: 0x00373C05 File Offset: 0x00371E05
		public virtual void RegisterStartChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			this.m_startChangingStatesEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet, doOnce);
		}

		// Token: 0x0600B378 RID: 45944 RVA: 0x00373C17 File Offset: 0x00371E17
		public virtual void RemoveStartChangingStatesListener(Action<object> listener)
		{
			this.m_startChangingStatesEvent.RemoveSetListener(listener);
		}

		// Token: 0x0600B379 RID: 45945
		public abstract void Show();

		// Token: 0x0600B37A RID: 45946
		public abstract void Hide();

		// Token: 0x0600B37B RID: 45947
		public abstract void BindDataModel(IDataModel dataModel, bool overrideChildren = false);

		// Token: 0x0600B37C RID: 45948
		public abstract bool BindDataModel(IDataModel dataModel, string targetName, bool propagateToChildren = true, bool overrideChildren = false);

		// Token: 0x0600B37D RID: 45949
		public abstract void UnbindDataModel(int id);

		// Token: 0x0600B37E RID: 45950
		public abstract bool GetDataModel(int id, out IDataModel model);

		// Token: 0x0600B37F RID: 45951
		public abstract bool GetDataModel(int id, string targetName, out IDataModel model);

		// Token: 0x0600B380 RID: 45952
		public abstract bool TriggerEvent(string eventName, Widget.TriggerEventParameters parameters = default(Widget.TriggerEventParameters));

		// Token: 0x0600B381 RID: 45953
		public abstract T FindWidgetComponent<T>(params string[] path) where T : Component;

		// Token: 0x0600B382 RID: 45954
		public abstract Widget FindWidget(string name);

		// Token: 0x0600B383 RID: 45955
		public abstract bool GetIsChangingStates(Func<GameObject, bool> includeGameObject);

		// Token: 0x0600B384 RID: 45956 RVA: 0x00373C28 File Offset: 0x00371E28
		public T GetDataModel<T>() where T : class, IDataModel
		{
			IDataModel dataModel;
			if (this.GetDataModel((typeof(T).GetField("ModelId", BindingFlags.Static | BindingFlags.Public).GetValue(null) as int?).Value, out dataModel))
			{
				return dataModel as T;
			}
			return default(T);
		}

		// Token: 0x0600B385 RID: 45957 RVA: 0x00373C84 File Offset: 0x00371E84
		public static string GetObjectDebugName(object obj)
		{
			UnityEngine.Object @object = obj as UnityEngine.Object;
			if (@object == null)
			{
				return "null";
			}
			return string.Format("{0} ({1})", @object.name, @object.GetInstanceID());
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600B386 RID: 45958 RVA: 0x000052EC File Offset: 0x000034EC
		public virtual bool HandlesChildLayers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B387 RID: 45959
		public abstract void SetLayerOverride(GameLayer layerOverride);

		// Token: 0x0600B388 RID: 45960
		public abstract void ClearLayerOverride();

		// Token: 0x04009677 RID: 38519
		protected FlagStateTracker m_startChangingStatesEvent;

		// Token: 0x04009678 RID: 38520
		protected FlagStateTracker m_doneChangingStatesEvent;

		// Token: 0x04009679 RID: 38521
		protected FlagStateTracker m_activatedEvent;

		// Token: 0x0400967A RID: 38522
		protected FlagStateTracker m_deactivatedEvent;

		// Token: 0x0400967B RID: 38523
		private FlagStateTracker m_readyState;

		// Token: 0x02002843 RID: 10307
		// (Invoke) Token: 0x06013B76 RID: 80758
		public delegate void EventListenerDelegate(string eventName);

		// Token: 0x02002844 RID: 10308
		public struct TriggerEventParameters
		{
			// Token: 0x0400F8FE RID: 63742
			public string SourceName;

			// Token: 0x0400F8FF RID: 63743
			public object Payload;

			// Token: 0x0400F900 RID: 63744
			public bool NoDownwardPropagation;

			// Token: 0x0400F901 RID: 63745
			public bool IgnorePlaymaker;
		}
	}
}
