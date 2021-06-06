using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Creates an FSM from a saved FSM Template.")]
	public class RunFSM : RunFSMAction
	{
		public FsmTemplateControl fsmTemplateControl = new FsmTemplateControl();

		[UIHint(UIHint.Variable)]
		public FsmInt storeID;

		[Tooltip("Event to send when the FSM has finished (usually because it ran a Finish FSM action).")]
		public FsmEvent finishEvent;

		public override void Reset()
		{
			fsmTemplateControl = new FsmTemplateControl();
			storeID = null;
			runFsm = null;
		}

		public override void Awake()
		{
			if (fsmTemplateControl.fsmTemplate != null && Application.isPlaying)
			{
				runFsm = base.Fsm.CreateSubFsm(fsmTemplateControl);
			}
		}

		public override void OnEnter()
		{
			if (runFsm == null)
			{
				Finish();
				return;
			}
			fsmTemplateControl.UpdateValues();
			fsmTemplateControl.ApplyOverrides(runFsm);
			runFsm.OnEnable();
			if (!runFsm.Started)
			{
				runFsm.Start();
			}
			storeID.Value = fsmTemplateControl.ID;
			CheckIfFinished();
		}

		protected override void CheckIfFinished()
		{
			if (runFsm == null || runFsm.Finished)
			{
				Finish();
				base.Fsm.Event(finishEvent);
			}
		}
	}
}
