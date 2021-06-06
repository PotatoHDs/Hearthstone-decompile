using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000911 RID: 2321
[CustomEditClass]
public class StateEventTable : MonoBehaviour
{
	// Token: 0x06008183 RID: 33155 RVA: 0x002A28D8 File Offset: 0x002A0AD8
	public void TriggerState(string eventName, bool saveLastState = true, string nameOverride = null)
	{
		StateEventTable.StateEvent stateEvent = this.GetStateEvent(eventName);
		if (stateEvent == null)
		{
			Debug.LogError(string.Format("{0} not defined in event table.", eventName), base.gameObject);
			return;
		}
		this.m_QueuedEvents.Enqueue(new StateEventTable.QueueStateEvent
		{
			m_StateEvent = stateEvent,
			m_NameOverride = nameOverride,
			m_SaveAsLastState = saveLastState
		});
		Log.EventTable.Print("Enqueuing event {0}", new object[]
		{
			eventName
		});
		if (this.m_QueuedEvents.Count == 1)
		{
			this.StartNextQueuedState(null);
			return;
		}
		Log.EventTable.Print("Event {0} will not start yet, currently waiting on event {1}.", new object[]
		{
			eventName,
			this.m_QueuedEvents.Peek().m_StateEvent.m_Name
		});
	}

	// Token: 0x06008184 RID: 33156 RVA: 0x002A2990 File Offset: 0x002A0B90
	public bool HasState(string eventName)
	{
		return this.m_Events.Find((StateEventTable.StateEvent e) => e.m_Name == eventName) != null;
	}

	// Token: 0x06008185 RID: 33157 RVA: 0x002A29C4 File Offset: 0x002A0BC4
	public void CancelQueuedStates()
	{
		this.m_QueuedEvents.Clear();
	}

	// Token: 0x06008186 RID: 33158 RVA: 0x002A29D4 File Offset: 0x002A0BD4
	public Spell GetSpellEvent(string eventName)
	{
		StateEventTable.StateEvent stateEvent = this.GetStateEvent(eventName);
		if (stateEvent != null)
		{
			return stateEvent.m_Event;
		}
		return null;
	}

	// Token: 0x06008187 RID: 33159 RVA: 0x002A29F4 File Offset: 0x002A0BF4
	public string GetLastState()
	{
		return this.m_LastState;
	}

	// Token: 0x06008188 RID: 33160 RVA: 0x002A29FC File Offset: 0x002A0BFC
	public void AddStateEventStartListener(string eventName, StateEventTable.StateEventTrigger dlg, bool once = false)
	{
		this.AddStateEventListener(once ? this.m_StateEventStartOnceListeners : this.m_StateEventStartListeners, eventName, dlg);
	}

	// Token: 0x06008189 RID: 33161 RVA: 0x002A2A17 File Offset: 0x002A0C17
	public void RemoveStateEventStartListener(string eventName, StateEventTable.StateEventTrigger dlg)
	{
		this.RemoveStateEventListener(this.m_StateEventStartListeners, eventName, dlg);
	}

	// Token: 0x0600818A RID: 33162 RVA: 0x002A2A27 File Offset: 0x002A0C27
	public void AddStateEventEndListener(string eventName, StateEventTable.StateEventTrigger dlg, bool once = false)
	{
		this.AddStateEventListener(once ? this.m_StateEventEndOnceListeners : this.m_StateEventEndListeners, eventName, dlg);
	}

	// Token: 0x0600818B RID: 33163 RVA: 0x002A2A42 File Offset: 0x002A0C42
	public void RemoveStateEventEndListener(string eventName, StateEventTable.StateEventTrigger dlg)
	{
		this.RemoveStateEventListener(this.m_StateEventEndListeners, eventName, dlg);
	}

	// Token: 0x0600818C RID: 33164 RVA: 0x002A2A54 File Offset: 0x002A0C54
	public PlayMakerFSM GetFSMFromEvent(string evtName)
	{
		Spell spellEvent = this.GetSpellEvent(evtName);
		if (spellEvent != null)
		{
			return spellEvent.GetComponent<PlayMakerFSM>();
		}
		return null;
	}

	// Token: 0x0600818D RID: 33165 RVA: 0x002A2A7C File Offset: 0x002A0C7C
	public void SetFloatVar(string eventName, string varName, float value)
	{
		PlayMakerFSM fsmfromEvent = this.GetFSMFromEvent(eventName);
		if (fsmfromEvent == null)
		{
			return;
		}
		fsmfromEvent.FsmVariables.GetFsmFloat(varName).Value = value;
	}

	// Token: 0x0600818E RID: 33166 RVA: 0x002A2AB0 File Offset: 0x002A0CB0
	public void SetIntVar(string eventName, string varName, int value)
	{
		PlayMakerFSM fsmfromEvent = this.GetFSMFromEvent(eventName);
		if (fsmfromEvent == null)
		{
			return;
		}
		fsmfromEvent.FsmVariables.GetFsmInt(varName).Value = value;
	}

	// Token: 0x0600818F RID: 33167 RVA: 0x002A2AE4 File Offset: 0x002A0CE4
	public void SetBoolVar(string eventName, string varName, bool value)
	{
		PlayMakerFSM fsmfromEvent = this.GetFSMFromEvent(eventName);
		if (fsmfromEvent == null)
		{
			return;
		}
		fsmfromEvent.FsmVariables.GetFsmBool(varName).Value = value;
	}

	// Token: 0x06008190 RID: 33168 RVA: 0x002A2B18 File Offset: 0x002A0D18
	public void SetGameObjectVar(string eventName, string varName, GameObject value)
	{
		PlayMakerFSM fsmfromEvent = this.GetFSMFromEvent(eventName);
		if (fsmfromEvent == null)
		{
			return;
		}
		fsmfromEvent.FsmVariables.GetFsmGameObject(varName).Value = value;
	}

	// Token: 0x06008191 RID: 33169 RVA: 0x002A2B4C File Offset: 0x002A0D4C
	public void SetGameObjectVar(string eventName, string varName, Component value)
	{
		PlayMakerFSM fsmfromEvent = this.GetFSMFromEvent(eventName);
		if (fsmfromEvent == null)
		{
			return;
		}
		fsmfromEvent.FsmVariables.GetFsmGameObject(varName).Value = value.gameObject;
	}

	// Token: 0x06008192 RID: 33170 RVA: 0x002A2B84 File Offset: 0x002A0D84
	public void SetVector3Var(string eventName, string varName, Vector3 value)
	{
		PlayMakerFSM fsmfromEvent = this.GetFSMFromEvent(eventName);
		if (fsmfromEvent == null)
		{
			return;
		}
		fsmfromEvent.FsmVariables.GetFsmVector3(varName).Value = value;
	}

	// Token: 0x06008193 RID: 33171 RVA: 0x002A2BB8 File Offset: 0x002A0DB8
	public void SetVar(string eventName, string varName, object value)
	{
		if (value is GameObject)
		{
			this.SetGameObjectVar(eventName, varName, (GameObject)value);
			return;
		}
		if (value is Component)
		{
			this.SetGameObjectVar(eventName, varName, (Component)value);
			return;
		}
		Action action;
		if (new Map<Type, Action>
		{
			{
				typeof(float),
				delegate()
				{
					this.SetFloatVar(eventName, varName, (float)value);
				}
			},
			{
				typeof(int),
				delegate()
				{
					this.SetIntVar(eventName, varName, (int)value);
				}
			},
			{
				typeof(bool),
				delegate()
				{
					this.SetBoolVar(eventName, varName, (bool)value);
				}
			}
		}.TryGetValue(value.GetType(), out action))
		{
			action();
			return;
		}
		Debug.LogError(string.Format("Set var type ({0}) not supported.", value.GetType()));
	}

	// Token: 0x06008194 RID: 33172 RVA: 0x002A2CCC File Offset: 0x002A0ECC
	protected StateEventTable.StateEvent GetStateEvent(string eventName)
	{
		return this.m_Events.Find((StateEventTable.StateEvent e) => e.m_Name == eventName);
	}

	// Token: 0x06008195 RID: 33173 RVA: 0x002A2D00 File Offset: 0x002A0F00
	private void StartNextQueuedState(StateEventTable.QueueStateEvent lastEvt)
	{
		if (this.m_QueuedEvents.Count == 0)
		{
			if (lastEvt != null)
			{
				this.FireStateEventFinishedEvent(this.m_StateEventEndListeners, lastEvt, false);
				this.FireStateEventFinishedEvent(this.m_StateEventEndOnceListeners, lastEvt, true);
			}
			return;
		}
		StateEventTable.QueueStateEvent queueStateEvent = this.m_QueuedEvents.Peek();
		StateEventTable.StateEvent stateEvent = queueStateEvent.m_StateEvent;
		if (queueStateEvent.m_SaveAsLastState)
		{
			this.m_LastState = queueStateEvent.GetEventName();
		}
		stateEvent.m_Event.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.QueueNextState), queueStateEvent);
		this.FireStateEventFinishedEvent(this.m_StateEventStartListeners, queueStateEvent, false);
		this.FireStateEventFinishedEvent(this.m_StateEventStartOnceListeners, queueStateEvent, true);
		stateEvent.m_Event.Activate();
	}

	// Token: 0x06008196 RID: 33174 RVA: 0x002A2D9E File Offset: 0x002A0F9E
	private void QueueNextState(Spell spell, SpellStateType prevStateType, object thisStateEvent)
	{
		if (this.m_QueuedEvents.Count == 0)
		{
			return;
		}
		this.m_QueuedEvents.Dequeue();
		this.StartNextQueuedState((StateEventTable.QueueStateEvent)thisStateEvent);
	}

	// Token: 0x06008197 RID: 33175 RVA: 0x002A2DC8 File Offset: 0x002A0FC8
	private void AddStateEventListener(Map<string, List<StateEventTable.StateEventTrigger>> listenerDict, string eventName, StateEventTable.StateEventTrigger dlg)
	{
		List<StateEventTable.StateEventTrigger> list;
		if (!listenerDict.TryGetValue(eventName, out list))
		{
			list = new List<StateEventTable.StateEventTrigger>();
			listenerDict[eventName] = list;
		}
		list.Add(dlg);
	}

	// Token: 0x06008198 RID: 33176 RVA: 0x002A2DF8 File Offset: 0x002A0FF8
	private void RemoveStateEventListener(Map<string, List<StateEventTable.StateEventTrigger>> listenerDict, string eventName, StateEventTable.StateEventTrigger dlg)
	{
		List<StateEventTable.StateEventTrigger> list;
		if (listenerDict.TryGetValue(eventName, out list))
		{
			list.Remove(dlg);
		}
	}

	// Token: 0x06008199 RID: 33177 RVA: 0x002A2E18 File Offset: 0x002A1018
	private void FireStateEventFinishedEvent(Map<string, List<StateEventTable.StateEventTrigger>> listenerDict, StateEventTable.QueueStateEvent stateEvt, bool clear = false)
	{
		List<StateEventTable.StateEventTrigger> list;
		if (!listenerDict.TryGetValue(stateEvt.GetEventName(), out list))
		{
			return;
		}
		StateEventTable.StateEventTrigger[] array = list.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](stateEvt.m_StateEvent.m_Event);
		}
		if (clear)
		{
			list.Clear();
		}
	}

	// Token: 0x040069C8 RID: 27080
	[CustomEditField(Sections = "Event Table", ListTable = true)]
	public List<StateEventTable.StateEvent> m_Events = new List<StateEventTable.StateEvent>();

	// Token: 0x040069C9 RID: 27081
	private Map<string, List<StateEventTable.StateEventTrigger>> m_StateEventStartListeners = new Map<string, List<StateEventTable.StateEventTrigger>>();

	// Token: 0x040069CA RID: 27082
	private Map<string, List<StateEventTable.StateEventTrigger>> m_StateEventEndListeners = new Map<string, List<StateEventTable.StateEventTrigger>>();

	// Token: 0x040069CB RID: 27083
	private Map<string, List<StateEventTable.StateEventTrigger>> m_StateEventStartOnceListeners = new Map<string, List<StateEventTable.StateEventTrigger>>();

	// Token: 0x040069CC RID: 27084
	private Map<string, List<StateEventTable.StateEventTrigger>> m_StateEventEndOnceListeners = new Map<string, List<StateEventTable.StateEventTrigger>>();

	// Token: 0x040069CD RID: 27085
	private QueueList<StateEventTable.QueueStateEvent> m_QueuedEvents = new QueueList<StateEventTable.QueueStateEvent>();

	// Token: 0x040069CE RID: 27086
	private string m_LastState;

	// Token: 0x020025EB RID: 9707
	[Serializable]
	public class StateEvent
	{
		// Token: 0x0400EF0B RID: 61195
		public string m_Name;

		// Token: 0x0400EF0C RID: 61196
		public Spell m_Event;
	}

	// Token: 0x020025EC RID: 9708
	protected class QueueStateEvent
	{
		// Token: 0x06013515 RID: 79125 RVA: 0x00530D1F File Offset: 0x0052EF1F
		public string GetEventName()
		{
			if (!string.IsNullOrEmpty(this.m_NameOverride))
			{
				return this.m_NameOverride;
			}
			return this.m_StateEvent.m_Name;
		}

		// Token: 0x0400EF0D RID: 61197
		public StateEventTable.StateEvent m_StateEvent;

		// Token: 0x0400EF0E RID: 61198
		public string m_NameOverride;

		// Token: 0x0400EF0F RID: 61199
		public bool m_SaveAsLastState = true;
	}

	// Token: 0x020025ED RID: 9709
	// (Invoke) Token: 0x06013518 RID: 79128
	public delegate void StateEventTrigger(Spell evt);
}
