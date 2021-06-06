using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Sets the individual fields of a Rect Variable. To leave any field unchanged, set variable to 'None'.")]
	public class SetRectFields : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		public FsmFloat x;

		public FsmFloat y;

		public FsmFloat width;

		public FsmFloat height;

		public bool everyFrame;

		public override void Reset()
		{
			rectVariable = null;
			x = new FsmFloat
			{
				UseVariable = true
			};
			y = new FsmFloat
			{
				UseVariable = true
			};
			width = new FsmFloat
			{
				UseVariable = true
			};
			height = new FsmFloat
			{
				UseVariable = true
			};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetRectFields();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetRectFields();
		}

		private void DoSetRectFields()
		{
			if (!rectVariable.IsNone)
			{
				Rect value = rectVariable.Value;
				if (!x.IsNone)
				{
					value.x = x.Value;
				}
				if (!y.IsNone)
				{
					value.y = y.Value;
				}
				if (!width.IsNone)
				{
					value.width = width.Value;
				}
				if (!height.IsNone)
				{
					value.height = height.Value;
				}
				rectVariable.Value = value;
			}
		}
	}
}
