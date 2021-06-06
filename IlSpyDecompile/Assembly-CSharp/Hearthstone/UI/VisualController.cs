using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class VisualController : WidgetBehavior, IWidgetEventListener
	{
		public delegate void OnStateChangedDelegate(VisualController controller);

		[SerializeField]
		private WidgetBehaviorStateCollection m_stateCollection;

		private WidgetBehaviorState.ActivateCallbackDelegate m_handleStateChangedDelegate;

		private WidgetBehaviorStateCollection.ChangingStatesDelegate m_handleStartChangingStatesDelegate;

		private WidgetBehaviorStateCollection.ChangingStatesDelegate m_handleDoneChangingStatesDelegate;

		private bool m_isPendingDefaultState;

		[Overridable]
		public string State
		{
			get
			{
				if (m_stateCollection == null)
				{
					return null;
				}
				return m_stateCollection.ActiveStateName;
			}
			set
			{
				SetState(value);
			}
		}

		public string RequestedState
		{
			get
			{
				if (m_stateCollection == null)
				{
					return null;
				}
				return m_stateCollection.RequestedStateName;
			}
		}

		public bool HasPendingActions
		{
			get
			{
				if (base.IsActive)
				{
					if (m_stateCollection != null)
					{
						return m_stateCollection.HasPendingActions;
					}
					return false;
				}
				return false;
			}
		}

		public override bool IsChangingStates
		{
			get
			{
				if (base.IsActive)
				{
					if (m_stateCollection != null)
					{
						return m_stateCollection.IsChangingStates;
					}
					return true;
				}
				return false;
			}
		}

		public WidgetTemplate OwningWidget => base.Owner;

		private event OnStateChangedDelegate m_onStateChanged;

		protected override void Awake()
		{
			base.Awake();
			m_handleStateChangedDelegate = HandleStateChanged;
			m_handleStartChangingStatesDelegate = base.HandleStartChangingStates;
			m_handleDoneChangingStatesDelegate = base.HandleDoneChangingStates;
		}

		public bool HasState(string stateName)
		{
			if (m_stateCollection != null)
			{
				return m_stateCollection.DoesStateExist(stateName);
			}
			return false;
		}

		protected override void OnInitialize()
		{
			if (m_stateCollection != null)
			{
				m_stateCollection.WillLoadSynchronously = base.WillLoadSynchronously;
			}
			m_isPendingDefaultState = true;
		}

		public bool SetState(string eventName)
		{
			if (m_stateCollection == null || !m_stateCollection.DoesStateExist(eventName))
			{
				return false;
			}
			m_stateCollection.OnStateActivated = m_handleStateChangedDelegate;
			m_stateCollection.OnStartChangingStates = m_handleStartChangingStatesDelegate;
			m_stateCollection.OnDoneChangingStates = m_handleDoneChangingStatesDelegate;
			m_stateCollection.ActivateState(this, eventName, base.CanTick);
			return true;
		}

		private void HandleStateChanged(AsyncOperationResult result, object userData)
		{
			if (result != AsyncOperationResult.Aborted)
			{
				this.m_onStateChanged?.Invoke(this);
			}
		}

		public override void OnUpdate()
		{
			if (m_stateCollection == null)
			{
				return;
			}
			m_stateCollection.OnStateActivated = m_handleStateChangedDelegate;
			m_stateCollection.OnStartChangingStates = m_handleStartChangingStatesDelegate;
			m_stateCollection.OnDoneChangingStates = m_handleDoneChangingStatesDelegate;
			m_stateCollection.Update(this);
			if (m_isPendingDefaultState)
			{
				if (m_stateCollection.States != null && m_stateCollection.States.Count > 0 && m_stateCollection.RequestedState == null)
				{
					SetState(m_stateCollection.States[0].Name);
				}
				m_isPendingDefaultState = false;
			}
		}

		public override bool TryIncrementDataVersion(int id)
		{
			HashSet<int> hashSet = null;
			if (m_stateCollection != null)
			{
				hashSet = m_stateCollection.GetDataModelIDsFromTriggers();
			}
			if (hashSet?.Contains(id) ?? false)
			{
				IncrementLocalDataVersion();
				return true;
			}
			return false;
		}

		public void RegisterStateChangedListener(OnStateChangedDelegate listener)
		{
			m_onStateChanged -= listener;
			m_onStateChanged += listener;
		}

		public WidgetEventListenerResponse EventReceived(string eventName)
		{
			WidgetEventListenerResponse result = default(WidgetEventListenerResponse);
			if (m_stateCollection != null)
			{
				result.Consumed = m_stateCollection.CanConsumeEvent(eventName);
				WidgetBehaviorState nextEnqueuedState = m_stateCollection.GetNextEnqueuedState();
				if (nextEnqueuedState != null && nextEnqueuedState.Name == eventName)
				{
					return result;
				}
				m_stateCollection.OnStartChangingStates = base.HandleStartChangingStates;
				m_stateCollection.OnDoneChangingStates = base.HandleDoneChangingStates;
				m_stateCollection.EnqueueState(this, eventName, base.CanTick);
			}
			return result;
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}
	}
}
