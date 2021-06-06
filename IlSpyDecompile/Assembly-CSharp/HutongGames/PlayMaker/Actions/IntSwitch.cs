namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on the value of an Integer Variable.")]
	public class IntSwitch : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		[CompoundArray("Int Switches", "Compare Int", "Send Event")]
		public FsmInt[] compareTo;

		public FsmEvent[] sendEvent;

		public bool everyFrame;

		public override void Reset()
		{
			intVariable = null;
			compareTo = new FsmInt[1];
			sendEvent = new FsmEvent[1];
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIntSwitch();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIntSwitch();
		}

		private void DoIntSwitch()
		{
			if (intVariable.IsNone)
			{
				return;
			}
			for (int i = 0; i < compareTo.Length; i++)
			{
				if (intVariable.Value == compareTo[i].Value)
				{
					base.Fsm.Event(sendEvent[i]);
					break;
				}
			}
		}
	}
}
