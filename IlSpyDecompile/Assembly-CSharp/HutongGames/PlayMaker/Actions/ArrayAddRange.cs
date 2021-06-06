namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Add values to an array.")]
	public class ArrayAddRange : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("The variables to add.")]
		public FsmVar[] variables;

		public override void Reset()
		{
			array = null;
			variables = new FsmVar[2];
		}

		public override void OnEnter()
		{
			DoAddRange();
			Finish();
		}

		private void DoAddRange()
		{
			int num = variables.Length;
			if (num > 0)
			{
				this.array.Resize(this.array.Length + num);
				FsmVar[] array = variables;
				foreach (FsmVar fsmVar in array)
				{
					fsmVar.UpdateValue();
					this.array.Set(this.array.Length - num, fsmVar.GetValue());
					num--;
				}
			}
		}
	}
}
