using System;
using System.Reflection;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	[DisallowMultipleComponent]
	public abstract class Widget : MonoBehaviour, IAsyncInitializationBehavior, IStatefulWidgetComponent, ILayerOverridable
	{
		public delegate void EventListenerDelegate(string eventName);

		public struct TriggerEventParameters
		{
			public string SourceName;

			public object Payload;

			public bool NoDownwardPropagation;

			public bool IgnorePlaymaker;
		}

		protected FlagStateTracker m_startChangingStatesEvent;

		protected FlagStateTracker m_doneChangingStatesEvent;

		protected FlagStateTracker m_activatedEvent;

		protected FlagStateTracker m_deactivatedEvent;

		private FlagStateTracker m_readyState;

		public abstract bool IsInitialized { get; }

		public abstract bool IsChangingStates { get; }

		public abstract bool HasPendingActions { get; }

		public virtual bool DeferredWidgetBehaviorInitialization { get; set; }

		public bool StartedChangingStates => m_startChangingStatesEvent.IsSet;

		public Behaviour Container => this;

		public bool IsReady => m_readyState.IsSet;

		public bool IsActive
		{
			get
			{
				if (base.enabled)
				{
					return base.gameObject.activeInHierarchy;
				}
				return false;
			}
		}

		public bool WillLoadSynchronously { get; set; }

		public virtual bool HandlesChildLayers => true;

		protected virtual void OnEnable()
		{
			m_deactivatedEvent.Clear();
			m_activatedEvent.SetAndDispatch();
		}

		protected virtual void OnDisable()
		{
			m_activatedEvent.Clear();
			m_deactivatedEvent.SetAndDispatch();
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

		protected void TriggerOnReady()
		{
			m_readyState.SetAndDispatch();
		}

		public abstract void RegisterEventListener(EventListenerDelegate listener);

		public abstract void RemoveEventListener(EventListenerDelegate listener);

		public virtual void RegisterDoneChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			m_doneChangingStatesEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet, doOnce);
		}

		public virtual void RemoveDoneChangingStatesListener(Action<object> listener)
		{
			m_doneChangingStatesEvent.RemoveSetListener(listener);
		}

		public virtual void RegisterStartChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			m_startChangingStatesEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet, doOnce);
		}

		public virtual void RemoveStartChangingStatesListener(Action<object> listener)
		{
			m_startChangingStatesEvent.RemoveSetListener(listener);
		}

		public abstract void Show();

		public abstract void Hide();

		public abstract void BindDataModel(IDataModel dataModel, bool overrideChildren = false);

		public abstract bool BindDataModel(IDataModel dataModel, string targetName, bool propagateToChildren = true, bool overrideChildren = false);

		public abstract void UnbindDataModel(int id);

		public abstract bool GetDataModel(int id, out IDataModel model);

		public abstract bool GetDataModel(int id, string targetName, out IDataModel model);

		public abstract bool TriggerEvent(string eventName, TriggerEventParameters parameters = default(TriggerEventParameters));

		public abstract T FindWidgetComponent<T>(params string[] path) where T : Component;

		public abstract Widget FindWidget(string name);

		public abstract bool GetIsChangingStates(Func<GameObject, bool> includeGameObject);

		public T GetDataModel<T>() where T : class, IDataModel
		{
			if (GetDataModel((typeof(T).GetField("ModelId", BindingFlags.Static | BindingFlags.Public).GetValue(null) as int?).Value, out var model))
			{
				return model as T;
			}
			return null;
		}

		public static string GetObjectDebugName(object obj)
		{
			UnityEngine.Object @object = obj as UnityEngine.Object;
			if (@object == null)
			{
				return "null";
			}
			return $"{@object.name} ({@object.GetInstanceID()})";
		}

		public abstract void SetLayerOverride(GameLayer layerOverride);

		public abstract void ClearLayerOverride();
	}
}
