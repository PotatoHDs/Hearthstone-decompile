using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200102D RID: 4141
	[Serializable]
	public class WidgetBehaviorState
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600B38B RID: 45963 RVA: 0x00373CD5 File Offset: 0x00371ED5
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600B38C RID: 45964 RVA: 0x00373CDD File Offset: 0x00371EDD
		public bool ConsumeEvent
		{
			get
			{
				return this.m_consumeEvent;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600B38D RID: 45965 RVA: 0x00373CE5 File Offset: 0x00371EE5
		public bool HasActionsToRun
		{
			get
			{
				return this.m_actions != null && this.m_currentIndex >= 0 && this.m_currentIndex < this.m_actions.Count;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600B38E RID: 45966 RVA: 0x00373D0D File Offset: 0x00371F0D
		public IEnumerable<ScriptString> Triggers
		{
			get
			{
				return this.m_triggers;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600B38F RID: 45967 RVA: 0x00373D15 File Offset: 0x00371F15
		public IEnumerable<StateAction> Actions
		{
			get
			{
				return this.m_actions;
			}
		}

		// Token: 0x0600B390 RID: 45968 RVA: 0x00373D1D File Offset: 0x00371F1D
		public void Activate(IDataModelProvider dataModelProvider, WidgetBehaviorState.ActivateCallbackDelegate callback, object userData = null)
		{
			this.Abort();
			this.m_dataModelProvider = dataModelProvider;
			this.m_callback = callback;
			this.m_callbackUserData = userData;
			this.m_currentIndex = -1;
			this.m_actionEnqueued = false;
			this.EnqueueNextAction(AsyncOperationResult.Success);
		}

		// Token: 0x0600B391 RID: 45969 RVA: 0x00373D50 File Offset: 0x00371F50
		private void EnqueueNextAction(AsyncOperationResult result)
		{
			if (result == AsyncOperationResult.Aborted)
			{
				this.m_currentIndex = -1;
				this.m_actionEnqueued = false;
				return;
			}
			if (result == AsyncOperationResult.Wait)
			{
				this.m_actionEnqueued = false;
				this.InvokeCallback(result, this.m_callbackUserData);
				return;
			}
			if (this.m_currentIndex == this.m_actions.Count - 1)
			{
				this.m_currentIndex = -1;
				this.m_actionEnqueued = false;
				this.InvokeCallback(result, this.m_callbackUserData);
				return;
			}
			this.m_actionEnqueued = true;
			this.m_currentIndex++;
		}

		// Token: 0x0600B392 RID: 45970 RVA: 0x00373DD0 File Offset: 0x00371FD0
		public void Update(bool loadSynchronously)
		{
			while (this.m_actionEnqueued && this.m_currentIndex < this.m_actions.Count)
			{
				this.m_actionEnqueued = false;
				if (!this.m_actions[this.m_currentIndex].TryRun(new StateAction.RunCallbackDelegate(this.EnqueueNextAction), this.m_dataModelProvider, loadSynchronously))
				{
					this.EnqueueNextAction(AsyncOperationResult.Success);
				}
			}
			if (this.HasActionsToRun)
			{
				this.m_actions[this.m_currentIndex].Update();
			}
		}

		// Token: 0x0600B393 RID: 45971 RVA: 0x00373E51 File Offset: 0x00372051
		public void Abort()
		{
			this.m_actionEnqueued = false;
			if (this.HasActionsToRun)
			{
				StateAction stateAction = this.m_actions[this.m_currentIndex];
				this.m_currentIndex = -1;
				stateAction.Abort();
			}
			this.InvokeCallback(AsyncOperationResult.Aborted, this.m_callbackUserData);
		}

		// Token: 0x0600B394 RID: 45972 RVA: 0x00373E8C File Offset: 0x0037208C
		private void InvokeCallback(AsyncOperationResult result, object userData)
		{
			if (this.m_callback != null)
			{
				if (result == AsyncOperationResult.Wait)
				{
					WidgetBehaviorState.ActivateCallbackDelegate callback = this.m_callback;
					if (callback == null)
					{
						return;
					}
					callback(result, userData);
					return;
				}
				else
				{
					WidgetBehaviorState.ActivateCallbackDelegate callback2 = this.m_callback;
					this.m_callback = null;
					if (callback2 == null)
					{
						return;
					}
					callback2(result, userData);
				}
			}
		}

		// Token: 0x0600B395 RID: 45973 RVA: 0x00373EC8 File Offset: 0x003720C8
		public ICollection<GameObject> GetGameObjectsTargetedByShowActions()
		{
			List<GameObject> list = new List<GameObject>();
			foreach (StateAction stateAction in this.m_actions)
			{
				if (stateAction.Type == StateAction.ActionType.ShowGameObject)
				{
					GameObject targetGameObject = stateAction.TargetGameObject;
					if (targetGameObject != null)
					{
						list.Add(targetGameObject);
					}
				}
			}
			return list;
		}

		// Token: 0x0600B396 RID: 45974 RVA: 0x00373F3C File Offset: 0x0037213C
		public bool EvaluateTriggers(IDataModelProvider dataModelProvider, out bool eventsRaised)
		{
			eventsRaised = false;
			if (this.m_triggers == null || this.m_triggers.Count == 0)
			{
				return false;
			}
			if (this.m_triggerContext == null)
			{
				this.m_triggerContext = new ScriptContext();
			}
			bool result = false;
			ScriptString scriptString = this.m_triggers[0];
			if (!string.IsNullOrEmpty(scriptString.Script))
			{
				ScriptContext.EvaluationResults evaluationResults = this.m_triggerContext.Evaluate(scriptString.Script, dataModelProvider);
				if (object.Equals(evaluationResults.Value, true))
				{
					eventsRaised |= evaluationResults.EventRaised;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600B397 RID: 45975 RVA: 0x00373FC8 File Offset: 0x003721C8
		public HashSet<int> GetDataModelIDsFromTrigger()
		{
			if (this.m_triggers == null || this.m_triggers.Count == 0)
			{
				return null;
			}
			ScriptString scriptString = this.m_triggers[0];
			if (this.m_dataModelIDs == null)
			{
				this.m_dataModelIDs = new HashSet<int>();
			}
			if (this.m_lastTriggerScriptString != scriptString.Script)
			{
				this.m_dataModelIDs.Clear();
				this.m_dataModelIDs.UnionWith(scriptString.GetDataModelIDs());
				this.m_lastTriggerScriptString = scriptString.Script;
			}
			return this.m_dataModelIDs;
		}

		// Token: 0x0600B398 RID: 45976 RVA: 0x0036C1AE File Offset: 0x0036A3AE
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, level, type);
		}

		// Token: 0x04009680 RID: 38528
		[SerializeField]
		private string m_name;

		// Token: 0x04009681 RID: 38529
		[SerializeField]
		private List<ScriptString> m_triggers;

		// Token: 0x04009682 RID: 38530
		[SerializeField]
		private List<StateAction> m_actions;

		// Token: 0x04009683 RID: 38531
		[SerializeField]
		private bool m_consumeEvent;

		// Token: 0x04009684 RID: 38532
		private int m_currentIndex;

		// Token: 0x04009685 RID: 38533
		private bool m_actionEnqueued;

		// Token: 0x04009686 RID: 38534
		private IDataModelProvider m_dataModelProvider;

		// Token: 0x04009687 RID: 38535
		private ScriptContext m_triggerContext;

		// Token: 0x04009688 RID: 38536
		private HashSet<int> m_dataModelIDs;

		// Token: 0x04009689 RID: 38537
		private string m_lastTriggerScriptString;

		// Token: 0x0400968A RID: 38538
		private WidgetBehaviorState.ActivateCallbackDelegate m_callback;

		// Token: 0x0400968B RID: 38539
		private object m_callbackUserData;

		// Token: 0x02002845 RID: 10309
		// (Invoke) Token: 0x06013B7A RID: 80762
		public delegate void ActivateCallbackDelegate(AsyncOperationResult result, object userData);
	}
}
