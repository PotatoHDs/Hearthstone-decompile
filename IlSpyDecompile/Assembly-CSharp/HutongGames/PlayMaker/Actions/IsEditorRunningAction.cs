using Blizzard.T5.Core;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Is Editor Running sends Events based on the result.")]
	public class IsEditorRunningAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Event to use if Editor is running.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to use if Editor is NOT running.")]
		public FsmEvent falseEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;

		public override void Reset()
		{
			trueEvent = null;
			falseEvent = null;
			storeResult = null;
		}

		public override void OnEnter()
		{
			IsEditorRunning();
			Finish();
		}

		public override void OnUpdate()
		{
			IsEditorRunning();
		}

		private void IsEditorRunning()
		{
			storeResult.Value = GeneralUtils.IsEditorPlaying();
			if (storeResult.Value)
			{
				base.Fsm.Event(trueEvent);
			}
			else
			{
				base.Fsm.Event(falseEvent);
			}
		}
	}
}
