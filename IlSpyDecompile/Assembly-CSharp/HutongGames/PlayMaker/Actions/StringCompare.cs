namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Compares 2 Strings and sends Events based on the result.")]
	public class StringCompare : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		public FsmString compareTo;

		public FsmEvent equalEvent;

		public FsmEvent notEqualEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;

		[Tooltip("Repeat every frame. Useful if any of the strings are changing over time.")]
		public bool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			compareTo = "";
			equalEvent = null;
			notEqualEvent = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoStringCompare();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoStringCompare();
		}

		private void DoStringCompare()
		{
			if (stringVariable != null && compareTo != null)
			{
				bool flag = stringVariable.Value == compareTo.Value;
				if (storeResult != null)
				{
					storeResult.Value = flag;
				}
				if (flag && equalEvent != null)
				{
					base.Fsm.Event(equalEvent);
				}
				else if (!flag && notEqualEvent != null)
				{
					base.Fsm.Event(notEqualEvent);
				}
			}
		}
	}
}
