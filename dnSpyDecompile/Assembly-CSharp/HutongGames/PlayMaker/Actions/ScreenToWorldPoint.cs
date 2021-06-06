using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D98 RID: 3480
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Transforms position from screen space into world space. NOTE: Uses the MainCamera!")]
	public class ScreenToWorldPoint : FsmStateAction
	{
		// Token: 0x0600A4FF RID: 42239 RVA: 0x00345364 File Offset: 0x00343564
		public override void Reset()
		{
			this.screenVector = null;
			this.screenX = new FsmFloat
			{
				UseVariable = true
			};
			this.screenY = new FsmFloat
			{
				UseVariable = true
			};
			this.screenZ = 1f;
			this.normalized = false;
			this.storeWorldVector = null;
			this.storeWorldX = null;
			this.storeWorldY = null;
			this.storeWorldZ = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A500 RID: 42240 RVA: 0x003453DB File Offset: 0x003435DB
		public override void OnEnter()
		{
			this.DoScreenToWorldPoint();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A501 RID: 42241 RVA: 0x003453F1 File Offset: 0x003435F1
		public override void OnUpdate()
		{
			this.DoScreenToWorldPoint();
		}

		// Token: 0x0600A502 RID: 42242 RVA: 0x003453FC File Offset: 0x003435FC
		private void DoScreenToWorldPoint()
		{
			if (Camera.main == null)
			{
				base.LogError("No MainCamera defined!");
				base.Finish();
				return;
			}
			Vector3 vector = Vector3.zero;
			if (!this.screenVector.IsNone)
			{
				vector = this.screenVector.Value;
			}
			if (!this.screenX.IsNone)
			{
				vector.x = this.screenX.Value;
			}
			if (!this.screenY.IsNone)
			{
				vector.y = this.screenY.Value;
			}
			if (!this.screenZ.IsNone)
			{
				vector.z = this.screenZ.Value;
			}
			if (this.normalized.Value)
			{
				vector.x *= (float)Screen.width;
				vector.y *= (float)Screen.height;
			}
			vector = Camera.main.ScreenToWorldPoint(vector);
			this.storeWorldVector.Value = vector;
			this.storeWorldX.Value = vector.x;
			this.storeWorldY.Value = vector.y;
			this.storeWorldZ.Value = vector.z;
		}

		// Token: 0x04008B94 RID: 35732
		[UIHint(UIHint.Variable)]
		[Tooltip("Screen position as a vector.")]
		public FsmVector3 screenVector;

		// Token: 0x04008B95 RID: 35733
		[Tooltip("Screen X position in pixels or normalized. See Normalized.")]
		public FsmFloat screenX;

		// Token: 0x04008B96 RID: 35734
		[Tooltip("Screen X position in pixels or normalized. See Normalized.")]
		public FsmFloat screenY;

		// Token: 0x04008B97 RID: 35735
		[Tooltip("Distance into the screen in world units.")]
		public FsmFloat screenZ;

		// Token: 0x04008B98 RID: 35736
		[Tooltip("If true, X/Y coordinates are considered normalized (0-1), otherwise they are expected to be in pixels")]
		public FsmBool normalized;

		// Token: 0x04008B99 RID: 35737
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world position in a vector3 variable.")]
		public FsmVector3 storeWorldVector;

		// Token: 0x04008B9A RID: 35738
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world X position in a float variable.")]
		public FsmFloat storeWorldX;

		// Token: 0x04008B9B RID: 35739
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world Y position in a float variable.")]
		public FsmFloat storeWorldY;

		// Token: 0x04008B9C RID: 35740
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world Z position in a float variable.")]
		public FsmFloat storeWorldZ;

		// Token: 0x04008B9D RID: 35741
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
