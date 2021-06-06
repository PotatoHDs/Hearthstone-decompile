using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Repeat Button. Sends an Event while pressed. Optionally store the button state in a Bool Variable.")]
	public class GUILayoutRepeatButton : GUILayoutAction
	{
		public FsmEvent sendEvent;

		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;

		public FsmTexture image;

		public FsmString text;

		public FsmString tooltip;

		public FsmString style;

		public override void Reset()
		{
			base.Reset();
			sendEvent = null;
			storeButtonState = null;
			text = "";
			image = null;
			tooltip = "";
			style = "";
		}

		public override void OnGUI()
		{
			bool flag = ((!string.IsNullOrEmpty(style.Value)) ? GUILayout.RepeatButton(new GUIContent(text.Value, image.Value, tooltip.Value), style.Value, base.LayoutOptions) : GUILayout.RepeatButton(new GUIContent(text.Value, image.Value, tooltip.Value), base.LayoutOptions));
			if (flag)
			{
				base.Fsm.Event(sendEvent);
			}
			storeButtonState.Value = flag;
		}
	}
}
