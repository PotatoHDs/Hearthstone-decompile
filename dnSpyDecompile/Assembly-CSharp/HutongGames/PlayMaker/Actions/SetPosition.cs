using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF0 RID: 3568
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets the Position of a Game Object. To leave any axis unchanged, set variable to 'None'.")]
	public class SetPosition : FsmStateAction
	{
		// Token: 0x0600A67C RID: 42620 RVA: 0x00349598 File Offset: 0x00347798
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
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
			this.space = Space.Self;
			this.everyFrame = false;
			this.lateUpdate = false;
		}

		// Token: 0x0600A67D RID: 42621 RVA: 0x003495FE File Offset: 0x003477FE
		public override void OnPreprocess()
		{
			if (this.lateUpdate)
			{
				base.Fsm.HandleLateUpdate = true;
			}
		}

		// Token: 0x0600A67E RID: 42622 RVA: 0x00349614 File Offset: 0x00347814
		public override void OnEnter()
		{
			if (!this.everyFrame && !this.lateUpdate)
			{
				this.DoSetPosition();
				base.Finish();
			}
		}

		// Token: 0x0600A67F RID: 42623 RVA: 0x00349632 File Offset: 0x00347832
		public override void OnUpdate()
		{
			if (!this.lateUpdate)
			{
				this.DoSetPosition();
			}
		}

		// Token: 0x0600A680 RID: 42624 RVA: 0x00349642 File Offset: 0x00347842
		public override void OnLateUpdate()
		{
			if (this.lateUpdate)
			{
				this.DoSetPosition();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A681 RID: 42625 RVA: 0x00349660 File Offset: 0x00347860
		private void DoSetPosition()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector;
			if (this.vector.IsNone)
			{
				vector = ((this.space == Space.World) ? ownerDefaultTarget.transform.position : ownerDefaultTarget.transform.localPosition);
			}
			else
			{
				vector = this.vector.Value;
			}
			if (!this.x.IsNone)
			{
				vector.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				vector.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				vector.z = this.z.Value;
			}
			if (this.space == Space.World)
			{
				ownerDefaultTarget.transform.position = vector;
				return;
			}
			ownerDefaultTarget.transform.localPosition = vector;
		}

		// Token: 0x04008CFF RID: 36095
		[RequiredField]
		[Tooltip("The GameObject to position.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D00 RID: 36096
		[UIHint(UIHint.Variable)]
		[Tooltip("Use a stored Vector3 position, and/or set individual axis below.")]
		public FsmVector3 vector;

		// Token: 0x04008D01 RID: 36097
		public FsmFloat x;

		// Token: 0x04008D02 RID: 36098
		public FsmFloat y;

		// Token: 0x04008D03 RID: 36099
		public FsmFloat z;

		// Token: 0x04008D04 RID: 36100
		[Tooltip("Use local or world space.")]
		public Space space;

		// Token: 0x04008D05 RID: 36101
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008D06 RID: 36102
		[Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
		public bool lateUpdate;
	}
}
