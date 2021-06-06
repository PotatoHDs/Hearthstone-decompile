using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("GUI base action - don't use!")]
	public abstract class GUIContentAction : GUIAction
	{
		public FsmTexture image;

		public FsmString text;

		public FsmString tooltip;

		public FsmString style;

		internal GUIContent content;

		public override void Reset()
		{
			base.Reset();
			image = null;
			text = "";
			tooltip = "";
			style = "";
		}

		public override void OnGUI()
		{
			base.OnGUI();
			content = new GUIContent(text.Value, image.Value, tooltip.Value);
		}
	}
}
