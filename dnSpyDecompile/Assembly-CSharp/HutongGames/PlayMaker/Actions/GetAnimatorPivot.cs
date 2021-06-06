using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EEC RID: 3820
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the pivot weight and/or position. The pivot is the most stable point between the avatar's left and right foot.\n For a weight value of 0, the left foot is the most stable point For a value of 1, the right foot is the most stable point")]
	public class GetAnimatorPivot : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB0E RID: 43790 RVA: 0x00357AD2 File Offset: 0x00355CD2
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.pivotWeight = null;
			this.pivotPosition = null;
		}

		// Token: 0x0600AB0F RID: 43791 RVA: 0x00357AF0 File Offset: 0x00355CF0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this._animator = ownerDefaultTarget.GetComponent<Animator>();
			if (this._animator == null)
			{
				base.Finish();
				return;
			}
			this.DoCheckPivot();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB10 RID: 43792 RVA: 0x00357B54 File Offset: 0x00355D54
		public override void OnActionUpdate()
		{
			this.DoCheckPivot();
		}

		// Token: 0x0600AB11 RID: 43793 RVA: 0x00357B5C File Offset: 0x00355D5C
		private void DoCheckPivot()
		{
			if (this._animator == null)
			{
				return;
			}
			if (!this.pivotWeight.IsNone)
			{
				this.pivotWeight.Value = this._animator.pivotWeight;
			}
			if (!this.pivotPosition.IsNone)
			{
				this.pivotPosition.Value = this._animator.pivotPosition;
			}
		}

		// Token: 0x040091D4 RID: 37332
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091D5 RID: 37333
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The pivot is the most stable point between the avatar's left and right foot.\n For a value of 0, the left foot is the most stable point For a value of 1, the right foot is the most stable point")]
		public FsmFloat pivotWeight;

		// Token: 0x040091D6 RID: 37334
		[UIHint(UIHint.Variable)]
		[Tooltip("The pivot is the most stable point between the avatar's left and right foot.\n For a value of 0, the left foot is the most stable point For a value of 1, the right foot is the most stable point")]
		public FsmVector3 pivotPosition;

		// Token: 0x040091D7 RID: 37335
		private Animator _animator;
	}
}
