using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001008 RID: 4104
	[ExecuteAlways]
	[AddComponentMenu("")]
	public abstract class WidgetBehavior : MonoBehaviour, IDataModelProvider, IStatefulWidgetComponent
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600B29B RID: 45723 RVA: 0x003705FF File Offset: 0x0036E7FF
		protected bool EnabledInterally
		{
			get
			{
				return this.m_enabledInternally;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600B29C RID: 45724 RVA: 0x00370607 File Offset: 0x0036E807
		protected bool IsChangingStatesInternally
		{
			get
			{
				return this.m_changingStatesInternally;
			}
		}

		// Token: 0x0600B29D RID: 45725 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual void Awake()
		{
		}

		// Token: 0x0600B29E RID: 45726 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600B29F RID: 45727 RVA: 0x0037060F File Offset: 0x0036E80F
		public bool IsActive
		{
			get
			{
				return base.enabled && base.gameObject.activeInHierarchy;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600B2A0 RID: 45728 RVA: 0x00370626 File Offset: 0x0036E826
		public bool IsWaitingForOwnerInit
		{
			get
			{
				return this.Owner != null && this.Owner.GetInitializationState() < WidgetTemplate.InitializationState.InitializingWidgetBehaviors;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600B2A1 RID: 45729 RVA: 0x00370646 File Offset: 0x0036E846
		public bool WillTickWhileInactive
		{
			get
			{
				return this.Owner != null && this.Owner.WillTickWhileInactive;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600B2A2 RID: 45730 RVA: 0x00370663 File Offset: 0x0036E863
		public bool CanTick
		{
			get
			{
				return !this.IsWaitingForOwnerInit && (this.IsActive || this.WillTickWhileInactive);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600B2A3 RID: 45731 RVA: 0x0037067F File Offset: 0x0036E87F
		protected bool CanSendStateChanges
		{
			get
			{
				return this.m_enabledInternally || this.WillTickWhileInactive;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600B2A4 RID: 45732 RVA: 0x00370691 File Offset: 0x0036E891
		public float InitializationStartTime
		{
			get
			{
				return this.m_initializationStartTime;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600B2A5 RID: 45733 RVA: 0x00370699 File Offset: 0x0036E899
		public WidgetTemplate Owner
		{
			get
			{
				if (this.m_owner == null)
				{
					this.m_owner = base.GetComponentInParent<WidgetTemplate>();
				}
				return this.m_owner;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600B2A6 RID: 45734 RVA: 0x003706BC File Offset: 0x0036E8BC
		public bool WillLoadSynchronously
		{
			get
			{
				WidgetTemplate owner = this.Owner;
				return owner != null && owner.WillLoadSynchronously;
			}
		}

		// Token: 0x0600B2A7 RID: 45735 RVA: 0x003706E1 File Offset: 0x0036E8E1
		protected virtual void OnEnable()
		{
			this.m_enabledInternally = true;
			if (this.m_changingStatesInternally && !this.WillTickWhileInactive && !this.m_startChangingStatesEvent.IsSet)
			{
				this.m_startChangingStatesEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B2A8 RID: 45736 RVA: 0x00370713 File Offset: 0x0036E913
		protected virtual void OnDisable()
		{
			this.m_enabledInternally = false;
			if (this.m_changingStatesInternally && !this.WillTickWhileInactive && this.m_startChangingStatesEvent.IsSet)
			{
				this.m_startChangingStatesEvent.Clear();
				this.m_doneChangingStatesEvent.SetAndDispatch();
			}
		}

		// Token: 0x0600B2A9 RID: 45737 RVA: 0x00370750 File Offset: 0x0036E950
		protected void IncrementLocalDataVersion()
		{
			this.m_dataVersion++;
		}

		// Token: 0x0600B2AA RID: 45738 RVA: 0x00370760 File Offset: 0x0036E960
		public int GetLocalDataVersion()
		{
			return this.m_dataVersion;
		}

		// Token: 0x0600B2AB RID: 45739 RVA: 0x00370768 File Offset: 0x0036E968
		public void BindDataModel(IDataModel dataModel, bool propagateToChildren = true, bool overrideChildren = false)
		{
			this.Owner.BindDataModel(dataModel, base.name, propagateToChildren, overrideChildren);
		}

		// Token: 0x0600B2AC RID: 45740 RVA: 0x0037077F File Offset: 0x0036E97F
		public bool GetDataModel(int id, out IDataModel dataModel)
		{
			if (this.Owner == null)
			{
				dataModel = null;
				return false;
			}
			return this.Owner.GetDataModel(id, base.gameObject, out dataModel) || GlobalDataContext.Get().GetDataModel(id, out dataModel);
		}

		// Token: 0x0600B2AD RID: 45741 RVA: 0x003707B7 File Offset: 0x0036E9B7
		public ICollection<IDataModel> GetDataModels()
		{
			if (this.Owner != null)
			{
				return this.Owner.GetDataModels();
			}
			return GlobalDataContext.Get().GetDataModels();
		}

		// Token: 0x0600B2AE RID: 45742 RVA: 0x003707DD File Offset: 0x0036E9DD
		public void Initialize()
		{
			this.OnInitialize();
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600B2AF RID: 45743
		public abstract bool IsChangingStates { get; }

		// Token: 0x0600B2B0 RID: 45744 RVA: 0x003707E5 File Offset: 0x0036E9E5
		public void RegisterStartChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			this.m_startChangingStatesEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet, doOnce);
		}

		// Token: 0x0600B2B1 RID: 45745 RVA: 0x003707F7 File Offset: 0x0036E9F7
		public void RemoveStartChangingStatesListener(Action<object> listener)
		{
			this.m_startChangingStatesEvent.RemoveSetListener(listener);
		}

		// Token: 0x0600B2B2 RID: 45746 RVA: 0x00370805 File Offset: 0x0036EA05
		public void RegisterDoneChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			this.m_doneChangingStatesEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet, doOnce);
		}

		// Token: 0x0600B2B3 RID: 45747 RVA: 0x00370817 File Offset: 0x0036EA17
		public void RemoveDoneChangingStatesListener(Action<object> listener)
		{
			this.m_doneChangingStatesEvent.RemoveSetListener(listener);
		}

		// Token: 0x0600B2B4 RID: 45748 RVA: 0x00370825 File Offset: 0x0036EA25
		protected void HandleStartChangingStates()
		{
			if (!this.m_changingStatesInternally)
			{
				this.m_changingStatesInternally = true;
				if (this.CanSendStateChanges && !this.m_startChangingStatesEvent.IsSet)
				{
					this.m_doneChangingStatesEvent.Clear();
					this.m_startChangingStatesEvent.SetAndDispatch();
				}
			}
		}

		// Token: 0x0600B2B5 RID: 45749 RVA: 0x00370862 File Offset: 0x0036EA62
		protected void HandleDoneChangingStates()
		{
			if (this.m_changingStatesInternally)
			{
				this.m_changingStatesInternally = false;
				if (this.CanSendStateChanges && this.m_startChangingStatesEvent.IsSet)
				{
					this.m_startChangingStatesEvent.Clear();
					this.m_doneChangingStatesEvent.SetAndDispatch();
				}
			}
		}

		// Token: 0x0600B2B6 RID: 45750
		protected abstract void OnInitialize();

		// Token: 0x0600B2B7 RID: 45751 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public virtual void OnUpdate()
		{
		}

		// Token: 0x0600B2B8 RID: 45752 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public virtual bool TryIncrementDataVersion(int id)
		{
			return false;
		}

		// Token: 0x04009632 RID: 38450
		[HideInInspector]
		[SerializeField]
		private WidgetTemplate m_owner;

		// Token: 0x04009633 RID: 38451
		private float m_initializationStartTime;

		// Token: 0x04009634 RID: 38452
		protected FlagStateTracker m_startChangingStatesEvent;

		// Token: 0x04009635 RID: 38453
		private FlagStateTracker m_doneChangingStatesEvent;

		// Token: 0x04009636 RID: 38454
		private bool m_enabledInternally;

		// Token: 0x04009637 RID: 38455
		private bool m_changingStatesInternally;

		// Token: 0x04009638 RID: 38456
		private int m_dataVersion = 1;
	}
}
