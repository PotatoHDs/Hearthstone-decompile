using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D6A RID: 3434
	[ActionCategory("RectTransform")]
	[Tooltip("Set the local rotation of this RectTransform.")]
	public class RectTransformSetLocalRotation : BaseUpdateAction
	{
		// Token: 0x0600A419 RID: 42009 RVA: 0x00342088 File Offset: 0x00340288
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.rotation = new FsmVector3
			{
				UseVariable = true
			};
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.z = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A41A RID: 42010 RVA: 0x003420EC File Offset: 0x003402EC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetValues();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A41B RID: 42011 RVA: 0x00342134 File Offset: 0x00340334
		public override void OnActionUpdate()
		{
			this.DoSetValues();
		}

		// Token: 0x0600A41C RID: 42012 RVA: 0x0034213C File Offset: 0x0034033C
		private void DoSetValues()
		{
			if (this._rt == null)
			{
				return;
			}
			Vector3 eulerAngles = this._rt.eulerAngles;
			if (!this.rotation.IsNone)
			{
				eulerAngles = this.rotation.Value;
			}
			if (!this.x.IsNone)
			{
				eulerAngles.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				eulerAngles.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				eulerAngles.z = this.z.Value;
			}
			this._rt.eulerAngles = eulerAngles;
		}

		// Token: 0x04008A8D RID: 35469
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A8E RID: 35470
		[Tooltip("The rotation. Set to none for no effect")]
		public FsmVector3 rotation;

		// Token: 0x04008A8F RID: 35471
		[Tooltip("The x component of the rotation. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008A90 RID: 35472
		[Tooltip("The y component of the rotation. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008A91 RID: 35473
		[Tooltip("The z component of the rotation. Set to none for no effect")]
		public FsmFloat z;

		// Token: 0x04008A92 RID: 35474
		private RectTransform _rt;
	}
}
