using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	[Serializable]
	public class WidgetBehaviorStateCollection
	{
		public delegate void ChangingStatesDelegate();

		[SerializeField]
		private List<WidgetBehaviorState> m_states;

		[SerializeField]
		private bool m_isRadioGroup;

		private WidgetBehaviorState m_activeState;

		private WidgetBehaviorState m_requestedState;

		private IDataModelProvider m_dataModelProvider;

		private bool m_isChangingStates;

		private bool m_transitioning;

		private bool m_isInWaitOperation;

		private int m_lastDataVersion;

		private Queue<WidgetBehaviorState> m_enqueuedStates;

		private List<WidgetBehaviorState> m_requestedStates;

		private HashSet<int> m_dataModelIDs;

		public WidgetBehaviorState.ActivateCallbackDelegate OnStateActivated;

		public ChangingStatesDelegate OnStartChangingStates;

		public ChangingStatesDelegate OnDoneChangingStates;

		public bool WillLoadSynchronously { get; set; }

		public bool IndependentStates { get; set; }

		public IList<WidgetBehaviorState> States => m_states;

		public IDataModelProvider DataModelProvider => m_dataModelProvider;

		public int DataVersion
		{
			get
			{
				if (DataModelProvider == null)
				{
					return 0;
				}
				return DataModelProvider.GetLocalDataVersion();
			}
		}

		public string ActiveStateName
		{
			get
			{
				if (m_activeState != null)
				{
					return m_activeState.Name;
				}
				return null;
			}
		}

		public string RequestedStateName
		{
			get
			{
				if (m_requestedState != null)
				{
					return m_requestedState.Name;
				}
				return null;
			}
		}

		public bool IsChangingStates
		{
			get
			{
				if (!m_isChangingStates)
				{
					return m_lastDataVersion != DataVersion;
				}
				return true;
			}
		}

		public bool HasPendingActions
		{
			get
			{
				if (!m_transitioning)
				{
					return m_lastDataVersion != DataVersion;
				}
				return true;
			}
		}

		public WidgetBehaviorState ActiveState => m_activeState;

		public WidgetBehaviorState RequestedState => m_requestedState;

		public void ActivateFirstState()
		{
			if (m_states.Count > 0)
			{
				ActivateState(m_states[0]);
			}
		}

		public bool DoesStateExist(string name)
		{
			foreach (WidgetBehaviorState state in m_states)
			{
				if (state.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		public bool ActivateState(IDataModelProvider dataModelProvider, string name, bool updateImmediately = true, bool mustExist = false)
		{
			m_dataModelProvider = dataModelProvider;
			WidgetBehaviorState widgetBehaviorState = FindStateByName(name, mustExist);
			if (widgetBehaviorState != null)
			{
				ActivateState(widgetBehaviorState, updateImmediately);
				return true;
			}
			return false;
		}

		public void AbortState(string name)
		{
			FindStateByName(name, logErrorIfNotFound: false)?.Abort();
		}

		public bool EnqueueState(IDataModelProvider dataModelProvider, string name, bool updateImmediately = true, bool mustExist = false)
		{
			m_dataModelProvider = dataModelProvider;
			WidgetBehaviorState widgetBehaviorState = FindStateByName(name, mustExist);
			if (widgetBehaviorState != null)
			{
				EnqueueState(widgetBehaviorState, updateImmediately);
				return true;
			}
			return false;
		}

		public HashSet<int> GetDataModelIDsFromTriggers()
		{
			if (m_dataModelIDs == null)
			{
				m_dataModelIDs = new HashSet<int>();
			}
			else
			{
				m_dataModelIDs.Clear();
			}
			foreach (WidgetBehaviorState state in m_states)
			{
				HashSet<int> dataModelIDsFromTrigger = state.GetDataModelIDsFromTrigger();
				if (dataModelIDsFromTrigger != null)
				{
					m_dataModelIDs.UnionWith(dataModelIDsFromTrigger);
				}
			}
			return m_dataModelIDs;
		}

		private WidgetBehaviorState FindStateByName(string stateName, bool logErrorIfNotFound)
		{
			if (m_states != null)
			{
				foreach (WidgetBehaviorState state in m_states)
				{
					if (state.Name == stateName)
					{
						return state;
					}
				}
			}
			if (logErrorIfNotFound)
			{
				Debug.LogErrorFormat("State '{0}' does not exist", stateName);
			}
			return null;
		}

		public void Update(IDataModelProvider dataModelProvider)
		{
			m_dataModelProvider = dataModelProvider;
			if (m_dataModelProvider != null)
			{
				UpdateStateTriggers();
			}
			if (m_requestedState != null)
			{
				m_requestedState.Update(WillLoadSynchronously);
			}
			if (m_requestedStates != null)
			{
				for (int i = 0; i < m_requestedStates.Count; i++)
				{
					m_requestedStates[i].Update(WillLoadSynchronously);
				}
			}
			if (m_isChangingStates && (!m_transitioning || m_isInWaitOperation))
			{
				m_isChangingStates = false;
				OnDoneChangingStates?.Invoke();
			}
		}

		public bool CanConsumeEvent(string eventName)
		{
			return FindStateByName(eventName, logErrorIfNotFound: false)?.ConsumeEvent ?? false;
		}

		private void UpdateStateTriggers()
		{
			int dataVersion = DataVersion;
			if (m_states == null || m_lastDataVersion == dataVersion)
			{
				return;
			}
			m_lastDataVersion = dataVersion;
			WidgetBehaviorState widgetBehaviorState = null;
			bool flag = false;
			foreach (WidgetBehaviorState state in m_states)
			{
				if (state.EvaluateTriggers(m_dataModelProvider, out var eventsRaised) && widgetBehaviorState == null)
				{
					flag = flag || eventsRaised;
					widgetBehaviorState = state;
				}
			}
			if (widgetBehaviorState != null && (widgetBehaviorState != m_activeState || flag))
			{
				ActivateState(widgetBehaviorState);
			}
		}

		private void EnqueueState(WidgetBehaviorState state, bool updateImmediately = true)
		{
			if (!m_transitioning && !m_isInWaitOperation)
			{
				ActivateState(state, updateImmediately);
				return;
			}
			if (m_enqueuedStates == null)
			{
				m_enqueuedStates = new Queue<WidgetBehaviorState>();
			}
			m_enqueuedStates.Enqueue(state);
		}

		public WidgetBehaviorState GetNextEnqueuedState()
		{
			if (m_enqueuedStates != null && m_enqueuedStates.Count > 0)
			{
				return m_enqueuedStates.Peek();
			}
			return null;
		}

		private void ActivateState(WidgetBehaviorState state, bool updateImmediately = true)
		{
			if (!m_isChangingStates)
			{
				m_isChangingStates = true;
				OnStartChangingStates?.Invoke();
			}
			m_transitioning = true;
			m_requestedState = state;
			HideIfInRadioGroup(m_requestedState);
			if (IndependentStates)
			{
				if (m_requestedStates == null)
				{
					m_requestedStates = new List<WidgetBehaviorState>();
				}
				if (!m_requestedStates.Contains(state))
				{
					m_requestedStates.Add(state);
				}
			}
			state.Activate(m_dataModelProvider, HandleStateActivationResult, state);
			if (updateImmediately)
			{
				state.Update(WillLoadSynchronously);
			}
		}

		private void HandleStateActivationResult(AsyncOperationResult result, object callbackObject)
		{
			WidgetBehaviorState widgetBehaviorState = (WidgetBehaviorState)callbackObject;
			m_transitioning = (m_requestedStates != null && m_requestedStates.Count > 0) || widgetBehaviorState.HasActionsToRun;
			if (!m_isChangingStates && m_transitioning && m_isInWaitOperation && result != AsyncOperationResult.Wait)
			{
				m_isChangingStates = true;
				OnStartChangingStates?.Invoke();
			}
			m_isInWaitOperation = result == AsyncOperationResult.Wait;
			if (!m_isInWaitOperation)
			{
				if (m_requestedStates != null)
				{
					m_requestedStates.Remove(widgetBehaviorState);
				}
				if (result != AsyncOperationResult.Aborted)
				{
					m_activeState = m_requestedState;
				}
				OnStateActivated?.Invoke(result, null);
				if (m_enqueuedStates != null && m_enqueuedStates.Count > 0)
				{
					ActivateState(m_enqueuedStates.Dequeue());
				}
			}
		}

		private void HideIfInRadioGroup(WidgetBehaviorState ignoredState = null)
		{
			if (!m_isRadioGroup || m_states == null)
			{
				return;
			}
			foreach (WidgetBehaviorState state in m_states)
			{
				if (state == ignoredState)
				{
					continue;
				}
				foreach (GameObject gameObjectsTargetedByShowAction in state.GetGameObjectsTargetedByShowActions())
				{
					gameObjectsTargetedByShowAction.SetActive(value: false);
				}
			}
		}
	}
}
