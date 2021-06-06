using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED8 RID: 3800
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the current transition information on a specified layer. Only valid when during a transition.")]
	public class GetAnimatorCurrentTransitionInfo : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAB0 RID: 43696 RVA: 0x00356785 File Offset: 0x00354985
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.layerIndex = null;
			this.name = null;
			this.nameHash = null;
			this.userNameHash = null;
			this.normalizedTime = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AAB1 RID: 43697 RVA: 0x003567C0 File Offset: 0x003549C0
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
			this.GetTransitionInfo();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AAB2 RID: 43698 RVA: 0x00356824 File Offset: 0x00354A24
		public override void OnActionUpdate()
		{
			this.GetTransitionInfo();
		}

		// Token: 0x0600AAB3 RID: 43699 RVA: 0x0035682C File Offset: 0x00354A2C
		private void GetTransitionInfo()
		{
			if (this._animator != null)
			{
				AnimatorTransitionInfo animatorTransitionInfo = this._animator.GetAnimatorTransitionInfo(this.layerIndex.Value);
				if (!this.name.IsNone)
				{
					this.name.Value = this._animator.GetLayerName(this.layerIndex.Value);
				}
				if (!this.nameHash.IsNone)
				{
					this.nameHash.Value = animatorTransitionInfo.nameHash;
				}
				if (!this.userNameHash.IsNone)
				{
					this.userNameHash.Value = animatorTransitionInfo.userNameHash;
				}
				if (!this.normalizedTime.IsNone)
				{
					this.normalizedTime.Value = animatorTransitionInfo.normalizedTime;
				}
			}
		}

		// Token: 0x04009167 RID: 37223
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009168 RID: 37224
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x04009169 RID: 37225
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The unique name of the Transition")]
		public FsmString name;

		// Token: 0x0400916A RID: 37226
		[UIHint(UIHint.Variable)]
		[Tooltip("The unique name of the Transition")]
		public FsmInt nameHash;

		// Token: 0x0400916B RID: 37227
		[UIHint(UIHint.Variable)]
		[Tooltip("The user-specified name of the Transition")]
		public FsmInt userNameHash;

		// Token: 0x0400916C RID: 37228
		[UIHint(UIHint.Variable)]
		[Tooltip("Normalized time of the Transition")]
		public FsmFloat normalizedTime;

		// Token: 0x0400916D RID: 37229
		private Animator _animator;
	}
}
