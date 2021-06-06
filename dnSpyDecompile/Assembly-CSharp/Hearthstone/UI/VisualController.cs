using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001007 RID: 4103
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class VisualController : WidgetBehavior, IWidgetEventListener
	{
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x0600B288 RID: 45704 RVA: 0x00370280 File Offset: 0x0036E480
		// (remove) Token: 0x0600B289 RID: 45705 RVA: 0x003702B8 File Offset: 0x0036E4B8
		private event VisualController.OnStateChangedDelegate m_onStateChanged;

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600B28B RID: 45707 RVA: 0x003702F7 File Offset: 0x0036E4F7
		// (set) Token: 0x0600B28A RID: 45706 RVA: 0x003702ED File Offset: 0x0036E4ED
		[Overridable]
		public string State
		{
			get
			{
				if (this.m_stateCollection == null)
				{
					return null;
				}
				return this.m_stateCollection.ActiveStateName;
			}
			set
			{
				this.SetState(value);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600B28C RID: 45708 RVA: 0x0037030E File Offset: 0x0036E50E
		public string RequestedState
		{
			get
			{
				if (this.m_stateCollection == null)
				{
					return null;
				}
				return this.m_stateCollection.RequestedStateName;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600B28D RID: 45709 RVA: 0x00370325 File Offset: 0x0036E525
		public bool HasPendingActions
		{
			get
			{
				return base.IsActive && this.m_stateCollection != null && this.m_stateCollection.HasPendingActions;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600B28E RID: 45710 RVA: 0x00370346 File Offset: 0x0036E546
		public override bool IsChangingStates
		{
			get
			{
				return base.IsActive && (this.m_stateCollection == null || this.m_stateCollection.IsChangingStates);
			}
		}

		// Token: 0x0600B28F RID: 45711 RVA: 0x00370367 File Offset: 0x0036E567
		protected override void Awake()
		{
			base.Awake();
			this.m_handleStateChangedDelegate = new WidgetBehaviorState.ActivateCallbackDelegate(this.HandleStateChanged);
			this.m_handleStartChangingStatesDelegate = new WidgetBehaviorStateCollection.ChangingStatesDelegate(base.HandleStartChangingStates);
			this.m_handleDoneChangingStatesDelegate = new WidgetBehaviorStateCollection.ChangingStatesDelegate(base.HandleDoneChangingStates);
		}

		// Token: 0x0600B290 RID: 45712 RVA: 0x003703A5 File Offset: 0x0036E5A5
		public bool HasState(string stateName)
		{
			return this.m_stateCollection != null && this.m_stateCollection.DoesStateExist(stateName);
		}

		// Token: 0x0600B291 RID: 45713 RVA: 0x003703BD File Offset: 0x0036E5BD
		protected override void OnInitialize()
		{
			if (this.m_stateCollection != null)
			{
				this.m_stateCollection.WillLoadSynchronously = base.WillLoadSynchronously;
			}
			this.m_isPendingDefaultState = true;
		}

		// Token: 0x0600B292 RID: 45714 RVA: 0x003703E0 File Offset: 0x0036E5E0
		public bool SetState(string eventName)
		{
			if (this.m_stateCollection == null || !this.m_stateCollection.DoesStateExist(eventName))
			{
				return false;
			}
			this.m_stateCollection.OnStateActivated = this.m_handleStateChangedDelegate;
			this.m_stateCollection.OnStartChangingStates = this.m_handleStartChangingStatesDelegate;
			this.m_stateCollection.OnDoneChangingStates = this.m_handleDoneChangingStatesDelegate;
			this.m_stateCollection.ActivateState(this, eventName, base.CanTick, false);
			return true;
		}

		// Token: 0x0600B293 RID: 45715 RVA: 0x0037044E File Offset: 0x0036E64E
		private void HandleStateChanged(AsyncOperationResult result, object userData)
		{
			if (result != AsyncOperationResult.Aborted)
			{
				VisualController.OnStateChangedDelegate onStateChanged = this.m_onStateChanged;
				if (onStateChanged == null)
				{
					return;
				}
				onStateChanged(this);
			}
		}

		// Token: 0x0600B294 RID: 45716 RVA: 0x00370468 File Offset: 0x0036E668
		public override void OnUpdate()
		{
			if (this.m_stateCollection != null)
			{
				this.m_stateCollection.OnStateActivated = this.m_handleStateChangedDelegate;
				this.m_stateCollection.OnStartChangingStates = this.m_handleStartChangingStatesDelegate;
				this.m_stateCollection.OnDoneChangingStates = this.m_handleDoneChangingStatesDelegate;
				this.m_stateCollection.Update(this);
				if (this.m_isPendingDefaultState)
				{
					if (this.m_stateCollection.States != null && this.m_stateCollection.States.Count > 0 && this.m_stateCollection.RequestedState == null)
					{
						this.SetState(this.m_stateCollection.States[0].Name);
					}
					this.m_isPendingDefaultState = false;
				}
			}
		}

		// Token: 0x0600B295 RID: 45717 RVA: 0x00370518 File Offset: 0x0036E718
		public override bool TryIncrementDataVersion(int id)
		{
			HashSet<int> hashSet = null;
			if (this.m_stateCollection != null)
			{
				hashSet = this.m_stateCollection.GetDataModelIDsFromTriggers();
			}
			if (hashSet != null && hashSet.Contains(id))
			{
				base.IncrementLocalDataVersion();
				return true;
			}
			return false;
		}

		// Token: 0x0600B296 RID: 45718 RVA: 0x00370553 File Offset: 0x0036E753
		public void RegisterStateChangedListener(VisualController.OnStateChangedDelegate listener)
		{
			this.m_onStateChanged -= listener;
			this.m_onStateChanged += listener;
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600B297 RID: 45719 RVA: 0x00370563 File Offset: 0x0036E763
		public WidgetTemplate OwningWidget
		{
			get
			{
				return base.Owner;
			}
		}

		// Token: 0x0600B298 RID: 45720 RVA: 0x0037056C File Offset: 0x0036E76C
		public WidgetEventListenerResponse EventReceived(string eventName)
		{
			WidgetEventListenerResponse result = default(WidgetEventListenerResponse);
			if (this.m_stateCollection != null)
			{
				result.Consumed = this.m_stateCollection.CanConsumeEvent(eventName);
				WidgetBehaviorState nextEnqueuedState = this.m_stateCollection.GetNextEnqueuedState();
				if (nextEnqueuedState != null && nextEnqueuedState.Name == eventName)
				{
					return result;
				}
				this.m_stateCollection.OnStartChangingStates = new WidgetBehaviorStateCollection.ChangingStatesDelegate(base.HandleStartChangingStates);
				this.m_stateCollection.OnDoneChangingStates = new WidgetBehaviorStateCollection.ChangingStatesDelegate(base.HandleDoneChangingStates);
				this.m_stateCollection.EnqueueState(this, eventName, base.CanTick, false);
			}
			return result;
		}

		// Token: 0x0600B299 RID: 45721 RVA: 0x0036D90B File Offset: 0x0036BB0B
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}

		// Token: 0x0400962C RID: 38444
		[SerializeField]
		private WidgetBehaviorStateCollection m_stateCollection;

		// Token: 0x0400962E RID: 38446
		private WidgetBehaviorState.ActivateCallbackDelegate m_handleStateChangedDelegate;

		// Token: 0x0400962F RID: 38447
		private WidgetBehaviorStateCollection.ChangingStatesDelegate m_handleStartChangingStatesDelegate;

		// Token: 0x04009630 RID: 38448
		private WidgetBehaviorStateCollection.ChangingStatesDelegate m_handleDoneChangingStatesDelegate;

		// Token: 0x04009631 RID: 38449
		private bool m_isPendingDefaultState;

		// Token: 0x0200283A RID: 10298
		// (Invoke) Token: 0x06013B54 RID: 80724
		public delegate void OnStateChangedDelegate(VisualController controller);
	}
}
