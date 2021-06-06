using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the last measured linear acceleration of a device and stores it in a Vector3 Variable.")]
	public class GetDeviceAcceleration : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector;

		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;

		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;

		[UIHint(UIHint.Variable)]
		public FsmFloat storeZ;

		public FsmFloat multiplier;

		public bool everyFrame;

		public override void Reset()
		{
			storeVector = null;
			storeX = null;
			storeY = null;
			storeZ = null;
			multiplier = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetDeviceAcceleration();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetDeviceAcceleration();
		}

		private void DoGetDeviceAcceleration()
		{
			Vector3 value = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
			if (!multiplier.IsNone)
			{
				value *= multiplier.Value;
			}
			storeVector.Value = value;
			storeX.Value = value.x;
			storeY.Value = value.y;
			storeZ.Value = value.z;
		}
	}
}
