using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Makes an on/off Toggle Button and stores the button state in a Bool Variable.")]
	public class GUILayoutToggle : GUILayoutAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;

		public FsmTexture image;

		public FsmString text;

		public FsmString tooltip;

		public FsmString style;

		public FsmEvent changedEvent;

		public override void Reset()
		{
			base.Reset();
			storeButtonState = null;
			text = "";
			image = null;
			tooltip = "";
			style = "Toggle";
			changedEvent = null;
		}

		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			storeButtonState.Value = GUILayout.Toggle(storeButtonState.Value, new GUIContent(text.Value, image.Value, tooltip.Value), style.Value, base.LayoutOptions);
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
