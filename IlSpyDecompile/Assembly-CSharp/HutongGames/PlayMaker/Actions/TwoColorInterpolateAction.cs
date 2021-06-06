using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Interpolate 2 Colors over a specified amount of Time.")]
	public class TwoColorInterpolateAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Color 1")]
		public FsmColor color1;

		[RequiredField]
		[Tooltip("Color 2")]
		public FsmColor color2;

		[RequiredField]
		[Tooltip("Interpolation time.")]
		public FsmFloat time;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the interpolated color in a Color variable.")]
		public FsmColor storeColor;

		[Tooltip("Event to send when the interpolation finishes.")]
		public FsmEvent finishEvent;

		[Tooltip("Ignore TimeScale")]
		public bool realTime;

		private float startTime;

		private float currentTime;

		public override void Reset()
		{
			color1 = new FsmColor();
			color2 = new FsmColor();
			color1.Value = Color.black;
			color2.Value = Color.white;
			time = 1f;
			storeColor = null;
			finishEvent = null;
			realTime = false;
		}

		public override void OnEnter()
		{
			startTime = FsmTime.RealtimeSinceStartup;
			currentTime = 0f;
			storeColor.Value = color1.Value;
		}

		public override void OnUpdate()
		{
			if (realTime)
			{
				currentTime = FsmTime.RealtimeSinceStartup - startTime;
			}
			else
			{
				currentTime += Time.deltaTime;
			}
			if (currentTime > time.Value)
			{
				Finish();
				storeColor.Value = color2.Value;
				if (finishEvent != null)
				{
					base.Fsm.Event(finishEvent);
				}
			}
			else
			{
				float num = currentTime / time.Value;
				Color value = (num.Equals(0f) ? color1.Value : ((!(num >= 1f)) ? Color.Lerp(color1.Value, color2.Value, num) : color2.Value));
				storeColor.Value = value;
			}
		}
	}
}
