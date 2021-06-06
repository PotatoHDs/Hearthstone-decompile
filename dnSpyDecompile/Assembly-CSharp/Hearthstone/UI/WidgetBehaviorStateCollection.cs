using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200102E RID: 4142
	[Serializable]
	public class WidgetBehaviorStateCollection
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600B39A RID: 45978 RVA: 0x0037404E File Offset: 0x0037224E
		// (set) Token: 0x0600B39B RID: 45979 RVA: 0x00374056 File Offset: 0x00372256
		public bool WillLoadSynchronously { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600B39D RID: 45981 RVA: 0x00374068 File Offset: 0x00372268
		// (set) Token: 0x0600B39C RID: 45980 RVA: 0x0037405F File Offset: 0x0037225F
		public bool IndependentStates { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600B39E RID: 45982 RVA: 0x00374070 File Offset: 0x00372270
		public IList<WidgetBehaviorState> States
		{
			get
			{
				return this.m_states;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600B39F RID: 45983 RVA: 0x00374078 File Offset: 0x00372278
		public IDataModelProvider DataModelProvider
		{
			get
			{
				return this.m_dataModelProvider;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600B3A0 RID: 45984 RVA: 0x00374080 File Offset: 0x00372280
		public int DataVersion
		{
			get
			{
				if (this.DataModelProvider == null)
				{
					return 0;
				}
				return this.DataModelProvider.GetLocalDataVersion();
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600B3A1 RID: 45985 RVA: 0x00374097 File Offset: 0x00372297
		public string ActiveStateName
		{
			get
			{
				if (this.m_activeState != null)
				{
					return this.m_activeState.Name;
				}
				return null;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600B3A2 RID: 45986 RVA: 0x003740AE File Offset: 0x003722AE
		public string RequestedStateName
		{
			get
			{
				if (this.m_requestedState != null)
				{
					return this.m_requestedState.Name;
				}
				return null;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600B3A3 RID: 45987 RVA: 0x003740C5 File Offset: 0x003722C5
		public bool IsChangingStates
		{
			get
			{
				return this.m_isChangingStates || this.m_lastDataVersion != this.DataVersion;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600B3A4 RID: 45988 RVA: 0x003740E2 File Offset: 0x003722E2
		public bool HasPendingActions
		{
			get
			{
				return this.m_transitioning || this.m_lastDataVersion != this.DataVersion;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600B3A5 RID: 45989 RVA: 0x003740FF File Offset: 0x003722FF
		public WidgetBehaviorState ActiveState
		{
			get
			{
				return this.m_activeState;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600B3A6 RID: 45990 RVA: 0x00374107 File Offset: 0x00372307
		public WidgetBehaviorState RequestedState
		{
			get
			{
				return this.m_requestedState;
			}
		}

		// Token: 0x0600B3A7 RID: 45991 RVA: 0x0037410F File Offset: 0x0037230F
		public void ActivateFirstState()
		{
			if (this.m_states.Count > 0)
			{
				this.ActivateState(this.m_states[0], true);
			}
		}

		// Token: 0x0600B3A8 RID: 45992 RVA: 0x00374134 File Offset: 0x00372334
		public bool DoesStateExist(string name)
		{
			using (List<WidgetBehaviorState>.Enumerator enumerator = this.m_states.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name == name)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600B3A9 RID: 45993 RVA: 0x00374194 File Offset: 0x00372394
		public bool ActivateState(IDataModelProvider dataModelProvider, string name, bool updateImmediately = true, bool mustExist = false)
		{
			this.m_dataModelProvider = dataModelProvider;
			WidgetBehaviorState widgetBehaviorState = this.FindStateByName(name, mustExist);
			if (widgetBehaviorState != null)
			{
				this.ActivateState(widgetBehaviorState, updateImmediately);
				return true;
			}
			return false;
		}

		// Token: 0x0600B3AA RID: 45994 RVA: 0x003741C0 File Offset: 0x003723C0
		public void AbortState(string name)
		{
			WidgetBehaviorState widgetBehaviorState = this.FindStateByName(name, false);
			if (widgetBehaviorState != null)
			{
				widgetBehaviorState.Abort();
			}
		}

		// Token: 0x0600B3AB RID: 45995 RVA: 0x003741E0 File Offset: 0x003723E0
		public bool EnqueueState(IDataModelProvider dataModelProvider, string name, bool updateImmediately = true, bool mustExist = false)
		{
			this.m_dataModelProvider = dataModelProvider;
			WidgetBehaviorState widgetBehaviorState = this.FindStateByName(name, mustExist);
			if (widgetBehaviorState != null)
			{
				this.EnqueueState(widgetBehaviorState, updateImmediately);
				return true;
			}
			return false;
		}

		// Token: 0x0600B3AC RID: 45996 RVA: 0x0037420C File Offset: 0x0037240C
		public HashSet<int> GetDataModelIDsFromTriggers()
		{
			if (this.m_dataModelIDs == null)
			{
				this.m_dataModelIDs = new HashSet<int>();
			}
			else
			{
				this.m_dataModelIDs.Clear();
			}
			foreach (WidgetBehaviorState widgetBehaviorState in this.m_states)
			{
				HashSet<int> dataModelIDsFromTrigger = widgetBehaviorState.GetDataModelIDsFromTrigger();
				if (dataModelIDsFromTrigger != null)
				{
					this.m_dataModelIDs.UnionWith(dataModelIDsFromTrigger);
				}
			}
			return this.m_dataModelIDs;
		}

		// Token: 0x0600B3AD RID: 45997 RVA: 0x00374294 File Offset: 0x00372494
		private WidgetBehaviorState FindStateByName(string stateName, bool logErrorIfNotFound)
		{
			if (this.m_states != null)
			{
				foreach (WidgetBehaviorState widgetBehaviorState in this.m_states)
				{
					if (widgetBehaviorState.Name == stateName)
					{
						return widgetBehaviorState;
					}
				}
			}
			if (logErrorIfNotFound)
			{
				Debug.LogErrorFormat("State '{0}' does not exist", new object[]
				{
					stateName
				});
			}
			return null;
		}

		// Token: 0x0600B3AE RID: 45998 RVA: 0x00374314 File Offset: 0x00372514
		public void Update(IDataModelProvider dataModelProvider)
		{
			this.m_dataModelProvider = dataModelProvider;
			if (this.m_dataModelProvider != null)
			{
				this.UpdateStateTriggers();
			}
			if (this.m_requestedState != null)
			{
				this.m_requestedState.Update(this.WillLoadSynchronously);
			}
			if (this.m_requestedStates != null)
			{
				for (int i = 0; i < this.m_requestedStates.Count; i++)
				{
					this.m_requestedStates[i].Update(this.WillLoadSynchronously);
				}
			}
			if (this.m_isChangingStates && (!this.m_transitioning || this.m_isInWaitOperation))
			{
				this.m_isChangingStates = false;
				WidgetBehaviorStateCollection.ChangingStatesDelegate onDoneChangingStates = this.OnDoneChangingStates;
				if (onDoneChangingStates == null)
				{
					return;
				}
				onDoneChangingStates();
			}
		}

		// Token: 0x0600B3AF RID: 45999 RVA: 0x003743B4 File Offset: 0x003725B4
		public bool CanConsumeEvent(string eventName)
		{
			WidgetBehaviorState widgetBehaviorState = this.FindStateByName(eventName, false);
			return widgetBehaviorState != null && widgetBehaviorState.ConsumeEvent;
		}

		// Token: 0x0600B3B0 RID: 46000 RVA: 0x003743D8 File Offset: 0x003725D8
		private void UpdateStateTriggers()
		{
			int dataVersion = this.DataVersion;
			if (this.m_states == null || this.m_lastDataVersion == dataVersion)
			{
				return;
			}
			this.m_lastDataVersion = dataVersion;
			WidgetBehaviorState widgetBehaviorState = null;
			bool flag = false;
			foreach (WidgetBehaviorState widgetBehaviorState2 in this.m_states)
			{
				bool flag2;
				if (widgetBehaviorState2.EvaluateTriggers(this.m_dataModelProvider, out flag2) && widgetBehaviorState == null)
				{
					flag = (flag || flag2);
					widgetBehaviorState = widgetBehaviorState2;
				}
			}
			if (widgetBehaviorState != null && (widgetBehaviorState != this.m_activeState || flag))
			{
				this.ActivateState(widgetBehaviorState, true);
			}
		}

		// Token: 0x0600B3B1 RID: 46001 RVA: 0x00374480 File Offset: 0x00372680
		private void EnqueueState(WidgetBehaviorState state, bool updateImmediately = true)
		{
			if (!this.m_transitioning && !this.m_isInWaitOperation)
			{
				this.ActivateState(state, updateImmediately);
				return;
			}
			if (this.m_enqueuedStates == null)
			{
				this.m_enqueuedStates = new Queue<WidgetBehaviorState>();
			}
			this.m_enqueuedStates.Enqueue(state);
		}

		// Token: 0x0600B3B2 RID: 46002 RVA: 0x003744BA File Offset: 0x003726BA
		public WidgetBehaviorState GetNextEnqueuedState()
		{
			if (this.m_enqueuedStates != null && this.m_enqueuedStates.Count > 0)
			{
				return this.m_enqueuedStates.Peek();
			}
			return null;
		}

		// Token: 0x0600B3B3 RID: 46003 RVA: 0x003744E0 File Offset: 0x003726E0
		private void ActivateState(WidgetBehaviorState state, bool updateImmediately = true)
		{
			if (!this.m_isChangingStates)
			{
				this.m_isChangingStates = true;
				WidgetBehaviorStateCollection.ChangingStatesDelegate onStartChangingStates = this.OnStartChangingStates;
				if (onStartChangingStates != null)
				{
					onStartChangingStates();
				}
			}
			this.m_transitioning = true;
			this.m_requestedState = state;
			this.HideIfInRadioGroup(this.m_requestedState);
			if (this.IndependentStates)
			{
				if (this.m_requestedStates == null)
				{
					this.m_requestedStates = new List<WidgetBehaviorState>();
				}
				if (!this.m_requestedStates.Contains(state))
				{
					this.m_requestedStates.Add(state);
				}
			}
			state.Activate(this.m_dataModelProvider, new WidgetBehaviorState.ActivateCallbackDelegate(this.HandleStateActivationResult), state);
			if (updateImmediately)
			{
				state.Update(this.WillLoadSynchronously);
			}
		}

		// Token: 0x0600B3B4 RID: 46004 RVA: 0x00374584 File Offset: 0x00372784
		private void HandleStateActivationResult(AsyncOperationResult result, object callbackObject)
		{
			WidgetBehaviorState widgetBehaviorState = (WidgetBehaviorState)callbackObject;
			this.m_transitioning = ((this.m_requestedStates != null && this.m_requestedStates.Count > 0) || widgetBehaviorState.HasActionsToRun);
			if (!this.m_isChangingStates && this.m_transitioning && this.m_isInWaitOperation && result != AsyncOperationResult.Wait)
			{
				this.m_isChangingStates = true;
				WidgetBehaviorStateCollection.ChangingStatesDelegate onStartChangingStates = this.OnStartChangingStates;
				if (onStartChangingStates != null)
				{
					onStartChangingStates();
				}
			}
			this.m_isInWaitOperation = (result == AsyncOperationResult.Wait);
			if (!this.m_isInWaitOperation)
			{
				if (this.m_requestedStates != null)
				{
					this.m_requestedStates.Remove(widgetBehaviorState);
				}
				if (result != AsyncOperationResult.Aborted)
				{
					this.m_activeState = this.m_requestedState;
				}
				WidgetBehaviorState.ActivateCallbackDelegate onStateActivated = this.OnStateActivated;
				if (onStateActivated != null)
				{
					onStateActivated(result, null);
				}
				if (this.m_enqueuedStates != null && this.m_enqueuedStates.Count > 0)
				{
					this.ActivateState(this.m_enqueuedStates.Dequeue(), true);
				}
			}
		}

		// Token: 0x0600B3B5 RID: 46005 RVA: 0x00374664 File Offset: 0x00372864
		private void HideIfInRadioGroup(WidgetBehaviorState ignoredState = null)
		{
			if (this.m_isRadioGroup && this.m_states != null)
			{
				foreach (WidgetBehaviorState widgetBehaviorState in this.m_states)
				{
					if (widgetBehaviorState != ignoredState)
					{
						foreach (GameObject gameObject in widgetBehaviorState.GetGameObjectsTargetedByShowActions())
						{
							gameObject.SetActive(false);
						}
					}
				}
			}
		}

		// Token: 0x0400968C RID: 38540
		[SerializeField]
		private List<WidgetBehaviorState> m_states;

		// Token: 0x0400968D RID: 38541
		[SerializeField]
		private bool m_isRadioGroup;

		// Token: 0x0400968E RID: 38542
		private WidgetBehaviorState m_activeState;

		// Token: 0x0400968F RID: 38543
		private WidgetBehaviorState m_requestedState;

		// Token: 0x04009690 RID: 38544
		private IDataModelProvider m_dataModelProvider;

		// Token: 0x04009691 RID: 38545
		private bool m_isChangingStates;

		// Token: 0x04009692 RID: 38546
		private bool m_transitioning;

		// Token: 0x04009693 RID: 38547
		private bool m_isInWaitOperation;

		// Token: 0x04009694 RID: 38548
		private int m_lastDataVersion;

		// Token: 0x04009695 RID: 38549
		private Queue<WidgetBehaviorState> m_enqueuedStates;

		// Token: 0x04009696 RID: 38550
		private List<WidgetBehaviorState> m_requestedStates;

		// Token: 0x04009697 RID: 38551
		private HashSet<int> m_dataModelIDs;

		// Token: 0x04009698 RID: 38552
		public WidgetBehaviorState.ActivateCallbackDelegate OnStateActivated;

		// Token: 0x04009699 RID: 38553
		public WidgetBehaviorStateCollection.ChangingStatesDelegate OnStartChangingStates;

		// Token: 0x0400969A RID: 38554
		public WidgetBehaviorStateCollection.ChangingStatesDelegate OnDoneChangingStates;

		// Token: 0x02002846 RID: 10310
		// (Invoke) Token: 0x06013B7E RID: 80766
		public delegate void ChangingStatesDelegate();
	}
}
