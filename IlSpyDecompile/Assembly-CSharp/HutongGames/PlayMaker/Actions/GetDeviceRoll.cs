using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the rotation of the device around its z axis (into the screen). For example when you steer with the iPhone in a driving game.")]
	public class GetDeviceRoll : FsmStateAction
	{
		public enum BaseOrientation
		{
			Portrait,
			LandscapeLeft,
			LandscapeRight
		}

		[Tooltip("How the user is expected to hold the device (where angle will be zero).")]
		public BaseOrientation baseOrientation;

		[UIHint(UIHint.Variable)]
		public FsmFloat storeAngle;

		public FsmFloat limitAngle;

		public FsmFloat smoothing;

		public bool everyFrame;

		private float lastZAngle;

		public override void Reset()
		{
			baseOrientation = BaseOrientation.LandscapeLeft;
			storeAngle = null;
			limitAngle = new FsmFloat
			{
				UseVariable = true
			};
			smoothing = 5f;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoGetDeviceRoll();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetDeviceRoll();
		}

		private void DoGetDeviceRoll()
		{
			float x = Input.acceleration.x;
			float y = Input.acceleration.y;
			float num = 0f;
			switch (baseOrientation)
			{
			case BaseOrientation.Portrait:
				num = 0f - Mathf.Atan2(x, 0f - y);
				break;
			case BaseOrientation.LandscapeLeft:
				num = Mathf.Atan2(y, 0f - x);
				break;
			case BaseOrientation.LandscapeRight:
				num = 0f - Mathf.Atan2(y, x);
				break;
			}
			if (!limitAngle.IsNone)
			{
				num = Mathf.Clamp(57.29578f * num, 0f - limitAngle.Value, limitAngle.Value);
			}
			if (smoothing.Value > 0f)
			{
				num = Mathf.LerpAngle(lastZAngle, num, smoothing.Value * Time.deltaTime);
			}
			lastZAngle = num;
			storeAngle.Value = num;
		}
	}
}
