using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EDA RID: 3802
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Check the active Transition user-specified name on a specified layer.")]
	public class GetAnimatorCurrentTransitionInfoIsUserName : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AABA RID: 43706 RVA: 0x00356A0D File Offset: 0x00354C0D
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.layerIndex = null;
			this.userName = null;
			this.nameMatch = null;
			this.nameMatchEvent = null;
			this.nameDoNotMatchEvent = null;
		}

		// Token: 0x0600AABB RID: 43707 RVA: 0x00356A40 File Offset: 0x00354C40
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
			this.IsName();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AABC RID: 43708 RVA: 0x00356AA4 File Offset: 0x00354CA4
		public override void OnActionUpdate()
		{
			this.IsName();
		}

		// Token: 0x0600AABD RID: 43709 RVA: 0x00356AAC File Offset: 0x00354CAC
		private void IsName()
		{
			if (this._animator != null)
			{
				bool flag = this._animator.GetAnimatorTransitionInfo(this.layerIndex.Value).IsUserName(this.userName.Value);
				if (!this.nameMatch.IsNone)
				{
					this.nameMatch.Value = flag;
				}
				if (flag)
				{
					base.Fsm.Event(this.nameMatchEvent);
					return;
				}
				base.Fsm.Event(this.nameDoNotMatchEvent);
			}
		}

		// Token: 0x04009175 RID: 37237
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009176 RID: 37238
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x04009177 RID: 37239
		[Tooltip("The user-specified name to check the transition against.")]
		public FsmString userName;

		// Token: 0x04009178 RID: 37240
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if name matches")]
		public FsmBool nameMatch;

		// Token: 0x04009179 RID: 37241
		[Tooltip("Event send if name matches")]
		public FsmEvent nameMatchEvent;

		// Token: 0x0400917A RID: 37242
		[Tooltip("Event send if name doesn't match")]
		public FsmEvent nameDoNotMatchEvent;

		// Token: 0x0400917B RID: 37243
		private Animator _animator;
	}
}
