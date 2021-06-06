using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D12 RID: 3346
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a 2d Game Object on it's z axis so its forward vector points at a 2d or 3d position.")]
	public class LookAt2d : FsmStateAction
	{
		// Token: 0x0600A256 RID: 41558 RVA: 0x0033B9F8 File Offset: 0x00339BF8
		public override void Reset()
		{
			this.gameObject = null;
			this.vector2Target = null;
			this.vector3Target = new FsmVector3
			{
				UseVariable = true
			};
			this.debug = false;
			this.debugLineColor = Color.green;
			this.everyFrame = true;
		}

		// Token: 0x0600A257 RID: 41559 RVA: 0x0033BA48 File Offset: 0x00339C48
		public override void OnEnter()
		{
			this.DoLookAt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A258 RID: 41560 RVA: 0x0033BA5E File Offset: 0x00339C5E
		public override void OnUpdate()
		{
			this.DoLookAt();
		}

		// Token: 0x0600A259 RID: 41561 RVA: 0x0033BA68 File Offset: 0x00339C68
		private void DoLookAt()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = new Vector3(this.vector2Target.Value.x, this.vector2Target.Value.y, 0f);
			if (!this.vector3Target.IsNone)
			{
				vector += this.vector3Target.Value;
			}
			Vector3 vector2 = vector - ownerDefaultTarget.transform.position;
			vector2.Normalize();
			float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			ownerDefaultTarget.transform.rotation = Quaternion.Euler(0f, 0f, num - this.rotationOffset.Value);
			if (this.debug.Value)
			{
				Debug.DrawLine(ownerDefaultTarget.transform.position, vector, this.debugLineColor.Value);
			}
		}

		// Token: 0x040088A3 RID: 34979
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088A4 RID: 34980
		[Tooltip("The 2d position to Look At.")]
		public FsmVector2 vector2Target;

		// Token: 0x040088A5 RID: 34981
		[Tooltip("The 3d position to Look At. If not set to none, will be added to the 2d target")]
		public FsmVector3 vector3Target;

		// Token: 0x040088A6 RID: 34982
		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;

		// Token: 0x040088A7 RID: 34983
		[Title("Draw Debug Line")]
		[Tooltip("Draw a debug line from the GameObject to the Target.")]
		public FsmBool debug;

		// Token: 0x040088A8 RID: 34984
		[Tooltip("Color to use for the debug line.")]
		public FsmColor debugLineColor;

		// Token: 0x040088A9 RID: 34985
		[Tooltip("Repeat every frame.")]
		public bool everyFrame = true;
	}
}
