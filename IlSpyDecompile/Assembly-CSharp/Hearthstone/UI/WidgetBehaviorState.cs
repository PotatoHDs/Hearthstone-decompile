using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	[Serializable]
	public class WidgetBehaviorState
	{
		public delegate void ActivateCallbackDelegate(AsyncOperationResult result, object userData);

		[SerializeField]
		private string m_name;

		[SerializeField]
		private List<ScriptString> m_triggers;

		[SerializeField]
		private List<StateAction> m_actions;

		[SerializeField]
		private bool m_consumeEvent;

		private int m_currentIndex;

		private bool m_actionEnqueued;

		private IDataModelProvider m_dataModelProvider;

		private ScriptContext m_triggerContext;

		private HashSet<int> m_dataModelIDs;

		private string m_lastTriggerScriptString;

		private ActivateCallbackDelegate m_callback;

		private object m_callbackUserData;

		public string Name => m_name;

		public bool ConsumeEvent => m_consumeEvent;

		public bool HasActionsToRun
		{
			get
			{
				if (m_actions != null && m_currentIndex >= 0)
				{
					return m_currentIndex < m_actions.Count;
				}
				return false;
			}
		}

		public IEnumerable<ScriptString> Triggers => m_triggers;

		public IEnumerable<StateAction> Actions => m_actions;

		public void Activate(IDataModelProvider dataModelProvider, ActivateCallbackDelegate callback, object userData = null)
		{
			Abort();
			m_dataModelProvider = dataModelProvider;
			m_callback = callback;
			m_callbackUserData = userData;
			m_currentIndex = -1;
			m_actionEnqueued = false;
			EnqueueNextAction(AsyncOperationResult.Success);
		}

		private void EnqueueNextAction(AsyncOperationResult result)
		{
			switch (result)
			{
			case AsyncOperationResult.Aborted:
				m_currentIndex = -1;
				m_actionEnqueued = false;
				return;
			case AsyncOperationResult.Wait:
				m_actionEnqueued = false;
				InvokeCallback(result, m_callbackUserData);
				return;
			}
			if (m_currentIndex == m_actions.Count - 1)
			{
				m_currentIndex = -1;
				m_actionEnqueued = false;
				InvokeCallback(result, m_callbackUserData);
			}
			else
			{
				m_actionEnqueued = true;
				m_currentIndex++;
			}
		}

		public void Update(bool loadSynchronously)
		{
			while (m_actionEnqueued && m_currentIndex < m_actions.Count)
			{
				m_actionEnqueued = false;
				if (!m_actions[m_currentIndex].TryRun(EnqueueNextAction, m_dataModelProvider, loadSynchronously))
				{
					EnqueueNextAction(AsyncOperationResult.Success);
				}
			}
			if (HasActionsToRun)
			{
				m_actions[m_currentIndex].Update();
			}
		}

		public void Abort()
		{
			m_actionEnqueued = false;
			if (HasActionsToRun)
			{
				StateAction stateAction = m_actions[m_currentIndex];
				m_currentIndex = -1;
				stateAction.Abort();
			}
			InvokeCallback(AsyncOperationResult.Aborted, m_callbackUserData);
		}

		private void InvokeCallback(AsyncOperationResult result, object userData)
		{
			if (m_callback != null)
			{
				if (result == AsyncOperationResult.Wait)
				{
					m_callback?.Invoke(result, userData);
					return;
				}
				ActivateCallbackDelegate callback = m_callback;
				m_callback = null;
				callback?.Invoke(result, userData);
			}
		}

		public ICollection<GameObject> GetGameObjectsTargetedByShowActions()
		{
			List<GameObject> list = new List<GameObject>();
			foreach (StateAction action in m_actions)
			{
				if (action.Type == StateAction.ActionType.ShowGameObject)
				{
					GameObject targetGameObject = action.TargetGameObject;
					if (targetGameObject != null)
					{
						list.Add(targetGameObject);
					}
				}
			}
			return list;
		}

		public bool EvaluateTriggers(IDataModelProvider dataModelProvider, out bool eventsRaised)
		{
			eventsRaised = false;
			if (m_triggers == null || m_triggers.Count == 0)
			{
				return false;
			}
			if (m_triggerContext == null)
			{
				m_triggerContext = new ScriptContext();
			}
			bool result = false;
			ScriptString scriptString = m_triggers[0];
			if (!string.IsNullOrEmpty(scriptString.Script))
			{
				ScriptContext.EvaluationResults evaluationResults = m_triggerContext.Evaluate(scriptString.Script, dataModelProvider);
				if (object.Equals(evaluationResults.Value, true))
				{
					eventsRaised |= evaluationResults.EventRaised;
					result = true;
				}
			}
			return result;
		}

		public HashSet<int> GetDataModelIDsFromTrigger()
		{
			if (m_triggers == null || m_triggers.Count == 0)
			{
				return null;
			}
			ScriptString scriptString = m_triggers[0];
			if (m_dataModelIDs == null)
			{
				m_dataModelIDs = new HashSet<int>();
			}
			if (m_lastTriggerScriptString != scriptString.Script)
			{
				m_dataModelIDs.Clear();
				m_dataModelIDs.UnionWith(scriptString.GetDataModelIDs());
				m_lastTriggerScriptString = scriptString.Script;
			}
			return m_dataModelIDs;
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, level, type);
		}
	}
}
