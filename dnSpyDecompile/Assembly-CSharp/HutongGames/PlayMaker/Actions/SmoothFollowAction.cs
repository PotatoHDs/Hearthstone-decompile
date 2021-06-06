using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E04 RID: 3588
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Action version of Unity's Smooth Follow script.")]
	public class SmoothFollowAction : FsmStateAction
	{
		// Token: 0x0600A6DF RID: 42719 RVA: 0x0034A80C File Offset: 0x00348A0C
		public override void Reset()
		{
			this.gameObject = null;
			this.targetObject = null;
			this.distance = 10f;
			this.height = 5f;
			this.heightDamping = 2f;
			this.rotationDamping = 3f;
		}

		// Token: 0x0600A6E0 RID: 42720 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x0600A6E1 RID: 42721 RVA: 0x0034A868 File Offset: 0x00348A68
		public override void OnLateUpdate()
		{
			if (this.targetObject.Value == null)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.cachedObject != ownerDefaultTarget)
			{
				this.cachedObject = ownerDefaultTarget;
				this.myTransform = ownerDefaultTarget.transform;
			}
			if (this.cachedTarget != this.targetObject.Value)
			{
				this.cachedTarget = this.targetObject.Value;
				this.targetTransform = this.cachedTarget.transform;
			}
			float y = this.targetTransform.eulerAngles.y;
			float b = this.targetTransform.position.y + this.height.Value;
			float num = this.myTransform.eulerAngles.y;
			float num2 = this.myTransform.position.y;
			num = Mathf.LerpAngle(num, y, this.rotationDamping.Value * Time.deltaTime);
			num2 = Mathf.Lerp(num2, b, this.heightDamping.Value * Time.deltaTime);
			Quaternion rotation = Quaternion.Euler(0f, num, 0f);
			this.myTransform.position = this.targetTransform.position;
			this.myTransform.position -= rotation * Vector3.forward * this.distance.Value;
			this.myTransform.position = new Vector3(this.myTransform.position.x, num2, this.myTransform.position.z);
			this.myTransform.LookAt(this.targetTransform);
		}

		// Token: 0x04008D59 RID: 36185
		[RequiredField]
		[Tooltip("The game object to control. E.g. The camera.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D5A RID: 36186
		[Tooltip("The GameObject to follow.")]
		public FsmGameObject targetObject;

		// Token: 0x04008D5B RID: 36187
		[RequiredField]
		[Tooltip("The distance in the x-z plane to the target.")]
		public FsmFloat distance;

		// Token: 0x04008D5C RID: 36188
		[RequiredField]
		[Tooltip("The height we want the camera to be above the target")]
		public FsmFloat height;

		// Token: 0x04008D5D RID: 36189
		[RequiredField]
		[Tooltip("How much to dampen height movement.")]
		public FsmFloat heightDamping;

		// Token: 0x04008D5E RID: 36190
		[RequiredField]
		[Tooltip("How much to dampen rotation changes.")]
		public FsmFloat rotationDamping;

		// Token: 0x04008D5F RID: 36191
		private GameObject cachedObject;

		// Token: 0x04008D60 RID: 36192
		private Transform myTransform;

		// Token: 0x04008D61 RID: 36193
		private GameObject cachedTarget;

		// Token: 0x04008D62 RID: 36194
		private Transform targetTransform;
	}
}
