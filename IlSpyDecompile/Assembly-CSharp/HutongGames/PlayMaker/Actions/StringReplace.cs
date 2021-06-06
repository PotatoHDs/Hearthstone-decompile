namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Replace a substring with a new String.")]
	public class StringReplace : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		public FsmString replace;

		public FsmString with;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		public bool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			replace = "";
			with = "";
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoReplace();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoReplace();
		}

		private void DoReplace()
		{
			if (stringVariable != null && storeResult != null)
			{
				storeResult.Value = stringVariable.Value.Replace(replace.Value, with.Value);
			}
		}
	}
}
