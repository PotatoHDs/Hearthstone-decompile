using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D69 RID: 3433
	[ActionCategory("RectTransform")]
	[Tooltip("Set the local position of this RectTransform.")]
	public class RectTransformSetLocalPosition : BaseUpdateAction
	{
		// Token: 0x0600A414 RID: 42004 RVA: 0x00341EDC File Offset: 0x003400DC
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.position2d = new FsmVector2
			{
				UseVariable = true
			};
			this.position = new FsmVector3
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

		// Token: 0x0600A415 RID: 42005 RVA: 0x00341F50 File Offset: 0x00340150
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

		// Token: 0x0600A416 RID: 42006 RVA: 0x00341F98 File Offset: 0x00340198
		public override void OnActionUpdate()
		{
			this.DoSetValues();
		}

		// Token: 0x0600A417 RID: 42007 RVA: 0x00341FA0 File Offset: 0x003401A0
		private void DoSetValues()
		{
			if (this._rt == null)
			{
				return;
			}
			Vector3 localPosition = this._rt.localPosition;
			if (!this.position.IsNone)
			{
				localPosition = this.position.Value;
			}
			if (!this.position2d.IsNone)
			{
				localPosition.x = this.position2d.Value.x;
				localPosition.y = this.position2d.Value.y;
			}
			if (!this.x.IsNone)
			{
				localPosition.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				localPosition.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				localPosition.z = this.z.Value;
			}
			this._rt.localPosition = localPosition;
		}

		// Token: 0x04008A86 RID: 35462
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A87 RID: 35463
		[Tooltip("The position. Set to none for no effect")]
		public FsmVector2 position2d;

		// Token: 0x04008A88 RID: 35464
		[Tooltip("Or the 3d position. Set to none for no effect")]
		public FsmVector3 position;

		// Token: 0x04008A89 RID: 35465
		[Tooltip("The x component of the rotation. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008A8A RID: 35466
		[Tooltip("The y component of the rotation. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008A8B RID: 35467
		[Tooltip("The z component of the rotation. Set to none for no effect")]
		public FsmFloat z;

		// Token: 0x04008A8C RID: 35468
		private RectTransform _rt;
	}
}
