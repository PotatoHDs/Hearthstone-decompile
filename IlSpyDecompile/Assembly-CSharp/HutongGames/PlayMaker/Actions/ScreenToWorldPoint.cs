using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Transforms position from screen space into world space. NOTE: Uses the MainCamera!")]
	public class ScreenToWorldPoint : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Screen position as a vector.")]
		public FsmVector3 screenVector;

		[Tooltip("Screen X position in pixels or normalized. See Normalized.")]
		public FsmFloat screenX;

		[Tooltip("Screen X position in pixels or normalized. See Normalized.")]
		public FsmFloat screenY;

		[Tooltip("Distance into the screen in world units.")]
		public FsmFloat screenZ;

		[Tooltip("If true, X/Y coordinates are considered normalized (0-1), otherwise they are expected to be in pixels")]
		public FsmBool normalized;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world position in a vector3 variable.")]
		public FsmVector3 storeWorldVector;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world X position in a float variable.")]
		public FsmFloat storeWorldX;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world Y position in a float variable.")]
		public FsmFloat storeWorldY;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world Z position in a float variable.")]
		public FsmFloat storeWorldZ;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			screenVector = null;
			screenX = new FsmFloat
			{
				UseVariable = true
			};
			screenY = new FsmFloat
			{
				UseVariable = true
			};
			screenZ = 1f;
			normalized = false;
			storeWorldVector = null;
			storeWorldX = null;
			storeWorldY = null;
			storeWorldZ = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoScreenToWorldPoint();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoScreenToWorldPoint();
		}

		private void DoScreenToWorldPoint()
		{
			if (Camera.main == null)
			{
				LogError("No MainCamera defined!");
				Finish();
				return;
			}
			Vector3 vector = Vector3.zero;
			if (!screenVector.IsNone)
			{
				vector = screenVector.Value;
			}
			if (!screenX.IsNone)
			{
				vector.x = screenX.Value;
			}
			if (!screenY.IsNone)
			{
				vector.y = screenY.Value;
			}
			if (!screenZ.IsNone)
			{
				vector.z = screenZ.Value;
			}
			if (normalized.Value)
			{
				vector.x *= Screen.width;
				vector.y *= Screen.height;
			}
			vector = Camera.main.ScreenToWorldPoint(vector);
			storeWorldVector.Value = vector;
			storeWorldX.Value = vector.x;
			storeWorldY.Value = vector.y;
			storeWorldZ.Value = vector.z;
		}
	}
}
