using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EEA RID: 3818
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Get the left foot bottom height.")]
	public class GetAnimatorLeftFootBottomHeight : FsmStateAction
	{
		// Token: 0x0600AB03 RID: 43779 RVA: 0x003577C5 File Offset: 0x003559C5
		public override void Reset()
		{
			this.gameObject = null;
			this.leftFootHeight = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB04 RID: 43780 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x0600AB05 RID: 43781 RVA: 0x003577DC File Offset: 0x003559DC
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
			this._getLeftFootBottonHeight();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB06 RID: 43782 RVA: 0x00357840 File Offset: 0x00355A40
		public override void OnLateUpdate()
		{
			this._getLeftFootBottonHeight();
		}

		// Token: 0x0600AB07 RID: 43783 RVA: 0x00357848 File Offset: 0x00355A48
		private void _getLeftFootBottonHeight()
		{
			if (this._animator != null)
			{
				this.leftFootHeight.Value = this._animator.leftFeetBottomHeight;
			}
		}

		// Token: 0x040091C3 RID: 37315
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091C4 RID: 37316
		[ActionSection("Result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the left foot bottom height.")]
		public FsmFloat leftFootHeight;

		// Token: 0x040091C5 RID: 37317
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;

		// Token: 0x040091C6 RID: 37318
		private Animator _animator;
	}
}
