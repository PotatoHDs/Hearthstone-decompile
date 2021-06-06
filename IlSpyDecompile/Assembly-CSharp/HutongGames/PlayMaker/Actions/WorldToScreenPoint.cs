using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Transforms position from world space into screen space. NOTE: Uses the MainCamera!")]
	public class WorldToScreenPoint : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("World position to transform into screen coordinates.")]
		public FsmVector3 worldPosition;

		[Tooltip("World X position.")]
		public FsmFloat worldX;

		[Tooltip("World Y position.")]
		public FsmFloat worldY;

		[Tooltip("World Z position.")]
		public FsmFloat worldZ;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen position in a Vector3 Variable. Z will equal zero.")]
		public FsmVector3 storeScreenPoint;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen X position in a Float Variable.")]
		public FsmFloat storeScreenX;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen Y position in a Float Variable.")]
		public FsmFloat storeScreenY;

		[Tooltip("Normalize screen coordinates (0-1). Otherwise coordinates are in pixels.")]
		public FsmBool normalize;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			worldPosition = null;
			worldX = new FsmFloat
			{
				UseVariable = true
			};
			worldY = new FsmFloat
			{
				UseVariable = true
			};
			worldZ = new FsmFloat
			{
				UseVariable = true
			};
			storeScreenPoint = null;
			storeScreenX = null;
			storeScreenY = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoWorldToScreenPoint();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoWorldToScreenPoint();
		}

		private void DoWorldToScreenPoint()
		{
			if (Camera.main == null)
			{
				LogError("No MainCamera defined!");
				Finish();
				return;
			}
			Vector3 vector = Vector3.zero;
			if (!worldPosition.IsNone)
			{
				vector = worldPosition.Value;
			}
			if (!worldX.IsNone)
			{
				vector.x = worldX.Value;
			}
			if (!worldY.IsNone)
			{
				vector.y = worldY.Value;
			}
			if (!worldZ.IsNone)
			{
				vector.z = worldZ.Value;
			}
			vector = Camera.main.WorldToScreenPoint(vector);
			if (normalize.Value)
			{
				vector.x /= Screen.width;
				vector.y /= Screen.height;
			}
			storeScreenPoint.Value = vector;
			storeScreenX.Value = vector.x;
			storeScreenY.Value = vector.y;
		}
	}
}
