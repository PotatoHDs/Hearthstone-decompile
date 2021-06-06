using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("GUI base action - don't use!")]
	public abstract class GUIAction : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmRect screenRect;

		public FsmFloat left;

		public FsmFloat top;

		public FsmFloat width;

		public FsmFloat height;

		[RequiredField]
		public FsmBool normalized;

		internal Rect rect;

		public override void Reset()
		{
			screenRect = null;
			left = 0f;
			top = 0f;
			width = 1f;
			height = 1f;
			normalized = true;
		}

		public override void OnGUI()
		{
			rect = ((!screenRect.IsNone) ? screenRect.Value : default(Rect));
			if (!left.IsNone)
			{
				rect.x = left.Value;
			}
			if (!top.IsNone)
			{
				rect.y = top.Value;
			}
			if (!width.IsNone)
			{
				rect.width = width.Value;
			}
			if (!height.IsNone)
			{
				rect.height = height.Value;
			}
			if (normalized.Value)
			{
				rect.x *= Screen.width;
				rect.width *= Screen.width;
				rect.y *= Screen.height;
				rect.height *= Screen.height;
			}
		}
	}
}
