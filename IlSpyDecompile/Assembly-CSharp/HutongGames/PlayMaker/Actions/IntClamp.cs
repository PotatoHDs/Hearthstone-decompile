using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Clamp the value of an Integer Variable to a Min/Max range.")]
	public class IntClamp : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		[RequiredField]
		public FsmInt minValue;

		[RequiredField]
		public FsmInt maxValue;

		public bool everyFrame;

		public override void Reset()
		{
			intVariable = null;
			minValue = null;
			maxValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoClamp();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoClamp();
		}

		private void DoClamp()
		{
			intVariable.Value = Mathf.Clamp(intVariable.Value, minValue.Value, maxValue.Value);
		}
	}
}
