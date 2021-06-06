using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("A Horizontal Slider linked to a Float Variable.")]
	public class GUILayoutHorizontalSlider : GUILayoutAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		[RequiredField]
		public FsmFloat leftValue;

		[RequiredField]
		public FsmFloat rightValue;

		public FsmEvent changedEvent;

		public override void Reset()
		{
			base.Reset();
			floatVariable = null;
			leftValue = 0f;
			rightValue = 100f;
			changedEvent = null;
		}

		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			if (floatVariable != null)
			{
				floatVariable.Value = GUILayout.HorizontalSlider(floatVariable.Value, leftValue.Value, rightValue.Value, base.LayoutOptions);
			}
			if (GUI.changed)
			{
				base.Fsm.Event(changedEvent);
				GUIUtility.ExitGUI();
			}
			else
			{
				GUI.changed = changed;
			}
		}
	}
}
