using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D5A RID: 3418
	[ActionCategory("RectTransform")]
	[Tooltip("Gets the local rotation of this RectTransform.")]
	public class RectTransformGetLocalRotation : BaseUpdateAction
	{
		// Token: 0x0600A3C9 RID: 41929 RVA: 0x00340930 File Offset: 0x0033EB30
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

		// Token: 0x0600A3CA RID: 41930 RVA: 0x00340994 File Offset: 0x0033EB94
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoGetValues();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A3CB RID: 41931 RVA: 0x003409DC File Offset: 0x0033EBDC
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3CC RID: 41932 RVA: 0x003409E4 File Offset: 0x0033EBE4
		private void DoGetValues()
		{
			if (this._rt == null)
			{
				return;
			}
			if (!this.rotation.IsNone)
			{
				this.rotation.Value = this._rt.eulerAngles;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.eulerAngles.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.eulerAngles.y;
			}
			if (!this.z.IsNone)
			{
				this.z.Value = this._rt.eulerAngles.z;
			}
		}

		// Token: 0x04008A22 RID: 35362
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A23 RID: 35363
		[Tooltip("The rotation")]
		public FsmVector3 rotation;

		// Token: 0x04008A24 RID: 35364
		[Tooltip("The x component of the rotation")]
		public FsmFloat x;

		// Token: 0x04008A25 RID: 35365
		[Tooltip("The y component of the rotation")]
		public FsmFloat y;

		// Token: 0x04008A26 RID: 35366
		[Tooltip("The z component of the rotation")]
		public FsmFloat z;

		// Token: 0x04008A27 RID: 35367
		private RectTransform _rt;
	}
}
