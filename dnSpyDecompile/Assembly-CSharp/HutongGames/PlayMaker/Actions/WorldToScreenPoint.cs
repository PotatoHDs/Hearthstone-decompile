using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC5 RID: 3781
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Transforms position from world space into screen space. NOTE: Uses the MainCamera!")]
	public class WorldToScreenPoint : FsmStateAction
	{
		// Token: 0x0600AA60 RID: 43616 RVA: 0x0035529C File Offset: 0x0035349C
		public override void Reset()
		{
			this.worldPosition = null;
			this.worldX = new FsmFloat
			{
				UseVariable = true
			};
			this.worldY = new FsmFloat
			{
				UseVariable = true
			};
			this.worldZ = new FsmFloat
			{
				UseVariable = true
			};
			this.storeScreenPoint = null;
			this.storeScreenX = null;
			this.storeScreenY = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA61 RID: 43617 RVA: 0x00355302 File Offset: 0x00353502
		public override void OnEnter()
		{
			this.DoWorldToScreenPoint();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA62 RID: 43618 RVA: 0x00355318 File Offset: 0x00353518
		public override void OnUpdate()
		{
			this.DoWorldToScreenPoint();
		}

		// Token: 0x0600AA63 RID: 43619 RVA: 0x00355320 File Offset: 0x00353520
		private void DoWorldToScreenPoint()
		{
			if (Camera.main == null)
			{
				base.LogError("No MainCamera defined!");
				base.Finish();
				return;
			}
			Vector3 vector = Vector3.zero;
			if (!this.worldPosition.IsNone)
			{
				vector = this.worldPosition.Value;
			}
			if (!this.worldX.IsNone)
			{
				vector.x = this.worldX.Value;
			}
			if (!this.worldY.IsNone)
			{
				vector.y = this.worldY.Value;
			}
			if (!this.worldZ.IsNone)
			{
				vector.z = this.worldZ.Value;
			}
			vector = Camera.main.WorldToScreenPoint(vector);
			if (this.normalize.Value)
			{
				vector.x /= (float)Screen.width;
				vector.y /= (float)Screen.height;
			}
			this.storeScreenPoint.Value = vector;
			this.storeScreenX.Value = vector.x;
			this.storeScreenY.Value = vector.y;
		}

		// Token: 0x040090F3 RID: 37107
		[UIHint(UIHint.Variable)]
		[Tooltip("World position to transform into screen coordinates.")]
		public FsmVector3 worldPosition;

		// Token: 0x040090F4 RID: 37108
		[Tooltip("World X position.")]
		public FsmFloat worldX;

		// Token: 0x040090F5 RID: 37109
		[Tooltip("World Y position.")]
		public FsmFloat worldY;

		// Token: 0x040090F6 RID: 37110
		[Tooltip("World Z position.")]
		public FsmFloat worldZ;

		// Token: 0x040090F7 RID: 37111
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen position in a Vector3 Variable. Z will equal zero.")]
		public FsmVector3 storeScreenPoint;

		// Token: 0x040090F8 RID: 37112
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen X position in a Float Variable.")]
		public FsmFloat storeScreenX;

		// Token: 0x040090F9 RID: 37113
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen Y position in a Float Variable.")]
		public FsmFloat storeScreenY;

		// Token: 0x040090FA RID: 37114
		[Tooltip("Normalize screen coordinates (0-1). Otherwise coordinates are in pixels.")]
		public FsmBool normalize;

		// Token: 0x040090FB RID: 37115
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
