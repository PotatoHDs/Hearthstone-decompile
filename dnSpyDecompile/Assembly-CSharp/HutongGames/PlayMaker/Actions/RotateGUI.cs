using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D76 RID: 3446
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Rotates the GUI around a pivot point. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class RotateGUI : FsmStateAction
	{
		// Token: 0x0600A44E RID: 42062 RVA: 0x00342AC6 File Offset: 0x00340CC6
		public override void Reset()
		{
			this.angle = 0f;
			this.pivotX = 0.5f;
			this.pivotY = 0.5f;
			this.normalized = true;
			this.applyGlobally = false;
		}

		// Token: 0x0600A44F RID: 42063 RVA: 0x00342B08 File Offset: 0x00340D08
		public override void OnGUI()
		{
			if (this.applied)
			{
				return;
			}
			Vector2 pivotPoint = new Vector2(this.pivotX.Value, this.pivotY.Value);
			if (this.normalized)
			{
				pivotPoint.x *= (float)Screen.width;
				pivotPoint.y *= (float)Screen.height;
			}
			GUIUtility.RotateAroundPivot(this.angle.Value, pivotPoint);
			if (this.applyGlobally)
			{
				PlayMakerGUI.GUIMatrix = GUI.matrix;
				this.applied = true;
			}
		}

		// Token: 0x0600A450 RID: 42064 RVA: 0x00342B8F File Offset: 0x00340D8F
		public override void OnUpdate()
		{
			this.applied = false;
		}

		// Token: 0x04008ABE RID: 35518
		[RequiredField]
		public FsmFloat angle;

		// Token: 0x04008ABF RID: 35519
		[RequiredField]
		public FsmFloat pivotX;

		// Token: 0x04008AC0 RID: 35520
		[RequiredField]
		public FsmFloat pivotY;

		// Token: 0x04008AC1 RID: 35521
		public bool normalized;

		// Token: 0x04008AC2 RID: 35522
		public bool applyGlobally;

		// Token: 0x04008AC3 RID: 35523
		private bool applied;
	}
}
