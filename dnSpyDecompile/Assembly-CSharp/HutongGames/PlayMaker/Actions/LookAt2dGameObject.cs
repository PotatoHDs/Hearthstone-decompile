using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D13 RID: 3347
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a 2d Game Object on it's z axis so its forward vector points at a Target.")]
	public class LookAt2dGameObject : FsmStateAction
	{
		// Token: 0x0600A25B RID: 41563 RVA: 0x0033BB6C File Offset: 0x00339D6C
		public override void Reset()
		{
			this.gameObject = null;
			this.targetObject = null;
			this.debug = false;
			this.debugLineColor = Color.green;
			this.everyFrame = true;
		}

		// Token: 0x0600A25C RID: 41564 RVA: 0x0033BB9F File Offset: 0x00339D9F
		public override void OnEnter()
		{
			this.DoLookAt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A25D RID: 41565 RVA: 0x0033BBB5 File Offset: 0x00339DB5
		public override void OnUpdate()
		{
			this.DoLookAt();
		}

		// Token: 0x0600A25E RID: 41566 RVA: 0x0033BBC0 File Offset: 0x00339DC0
		private void DoLookAt()
		{
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			this.goTarget = this.targetObject.Value;
			if (this.go == null || this.targetObject == null)
			{
				return;
			}
			Vector3 vector = this.goTarget.transform.position - this.go.transform.position;
			vector.Normalize();
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			this.go.transform.rotation = Quaternion.Euler(0f, 0f, num - this.rotationOffset.Value);
			if (this.debug.Value)
			{
				Debug.DrawLine(this.go.transform.position, this.goTarget.transform.position, this.debugLineColor.Value);
			}
		}

		// Token: 0x040088AA RID: 34986
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088AB RID: 34987
		[Tooltip("The GameObject to Look At.")]
		public FsmGameObject targetObject;

		// Token: 0x040088AC RID: 34988
		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;

		// Token: 0x040088AD RID: 34989
		[Title("Draw Debug Line")]
		[Tooltip("Draw a debug line from the GameObject to the Target.")]
		public FsmBool debug;

		// Token: 0x040088AE RID: 34990
		[Tooltip("Color to use for the debug line.")]
		public FsmColor debugLineColor;

		// Token: 0x040088AF RID: 34991
		[Tooltip("Repeat every frame.")]
		public bool everyFrame = true;

		// Token: 0x040088B0 RID: 34992
		private GameObject go;

		// Token: 0x040088B1 RID: 34993
		private GameObject goTarget;
	}
}
