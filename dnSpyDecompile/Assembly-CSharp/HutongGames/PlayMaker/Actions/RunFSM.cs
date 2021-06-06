using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D77 RID: 3447
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Creates an FSM from a saved FSM Template.")]
	public class RunFSM : RunFSMAction
	{
		// Token: 0x0600A452 RID: 42066 RVA: 0x00342B98 File Offset: 0x00340D98
		public override void Reset()
		{
			this.fsmTemplateControl = new FsmTemplateControl();
			this.storeID = null;
			this.runFsm = null;
		}

		// Token: 0x0600A453 RID: 42067 RVA: 0x00342BB3 File Offset: 0x00340DB3
		public override void Awake()
		{
			if (this.fsmTemplateControl.fsmTemplate != null && Application.isPlaying)
			{
				this.runFsm = base.Fsm.CreateSubFsm(this.fsmTemplateControl);
			}
		}

		// Token: 0x0600A454 RID: 42068 RVA: 0x00342BE8 File Offset: 0x00340DE8
		public override void OnEnter()
		{
			if (this.runFsm == null)
			{
				base.Finish();
				return;
			}
			this.fsmTemplateControl.UpdateValues();
			this.fsmTemplateControl.ApplyOverrides(this.runFsm);
			this.runFsm.OnEnable();
			if (!this.runFsm.Started)
			{
				this.runFsm.Start();
			}
			this.storeID.Value = this.fsmTemplateControl.ID;
			this.CheckIfFinished();
		}

		// Token: 0x0600A455 RID: 42069 RVA: 0x00342C5F File Offset: 0x00340E5F
		protected override void CheckIfFinished()
		{
			if (this.runFsm == null || this.runFsm.Finished)
			{
				base.Finish();
				base.Fsm.Event(this.finishEvent);
			}
		}

		// Token: 0x04008AC4 RID: 35524
		public FsmTemplateControl fsmTemplateControl = new FsmTemplateControl();

		// Token: 0x04008AC5 RID: 35525
		[UIHint(UIHint.Variable)]
		public FsmInt storeID;

		// Token: 0x04008AC6 RID: 35526
		[Tooltip("Event to send when the FSM has finished (usually because it ran a Finish FSM action).")]
		public FsmEvent finishEvent;
	}
}
