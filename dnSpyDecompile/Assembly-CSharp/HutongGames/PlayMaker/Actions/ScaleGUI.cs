using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D7A RID: 3450
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Scales the GUI around a pivot point. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class ScaleGUI : FsmStateAction
	{
		// Token: 0x0600A474 RID: 42100 RVA: 0x00342FD0 File Offset: 0x003411D0
		public override void Reset()
		{
			this.scaleX = 1f;
			this.scaleY = 1f;
			this.pivotX = 0.5f;
			this.pivotY = 0.5f;
			this.normalized = true;
			this.applyGlobally = false;
		}

		// Token: 0x0600A475 RID: 42101 RVA: 0x0034302C File Offset: 0x0034122C
		public override void OnGUI()
		{
			if (this.applied)
			{
				return;
			}
			Vector2 vector = new Vector2(this.scaleX.Value, this.scaleY.Value);
			if (object.Equals(vector.x, 0))
			{
				vector.x = 0.0001f;
			}
			if (object.Equals(vector.y, 0))
			{
				vector.x = 0.0001f;
			}
			Vector2 pivotPoint = new Vector2(this.pivotX.Value, this.pivotY.Value);
			if (this.normalized)
			{
				pivotPoint.x *= (float)Screen.width;
				pivotPoint.y *= (float)Screen.height;
			}
			GUIUtility.ScaleAroundPivot(vector, pivotPoint);
			if (this.applyGlobally)
			{
				PlayMakerGUI.GUIMatrix = GUI.matrix;
				this.applied = true;
			}
		}

		// Token: 0x0600A476 RID: 42102 RVA: 0x0034310E File Offset: 0x0034130E
		public override void OnUpdate()
		{
			this.applied = false;
		}

		// Token: 0x04008ACC RID: 35532
		[RequiredField]
		public FsmFloat scaleX;

		// Token: 0x04008ACD RID: 35533
		[RequiredField]
		public FsmFloat scaleY;

		// Token: 0x04008ACE RID: 35534
		[RequiredField]
		public FsmFloat pivotX;

		// Token: 0x04008ACF RID: 35535
		[RequiredField]
		public FsmFloat pivotY;

		// Token: 0x04008AD0 RID: 35536
		[Tooltip("Pivot point uses normalized coordinates. E.g. 0.5 is the center of the screen.")]
		public bool normalized;

		// Token: 0x04008AD1 RID: 35537
		public bool applyGlobally;

		// Token: 0x04008AD2 RID: 35538
		private bool applied;
	}
}
