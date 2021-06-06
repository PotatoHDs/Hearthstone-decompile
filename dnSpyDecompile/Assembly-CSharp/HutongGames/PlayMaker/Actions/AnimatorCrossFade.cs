using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC8 RID: 3784
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Create a dynamic transition between the current state and the destination state. Both states have to be on the same layer. Note: You cannot change the current state on a synchronized layer, you need to change it on the referenced layer.")]
	public class AnimatorCrossFade : FsmStateAction
	{
		// Token: 0x0600AA6E RID: 43630 RVA: 0x00355848 File Offset: 0x00353A48
		public override void Reset()
		{
			this.gameObject = null;
			this.stateName = null;
			this.transitionDuration = 1f;
			this.layer = new FsmInt
			{
				UseVariable = true
			};
			this.normalizedTime = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600AA6F RID: 43631 RVA: 0x00355898 File Offset: 0x00353A98
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this._animator = ownerDefaultTarget.GetComponent<Animator>();
			if (this._animator != null)
			{
				int num = this.layer.IsNone ? -1 : this.layer.Value;
				float normalizedTimeOffset = this.normalizedTime.IsNone ? float.NegativeInfinity : this.normalizedTime.Value;
				this._animator.CrossFade(this.stateName.Value, this.transitionDuration.Value, num, normalizedTimeOffset);
			}
			base.Finish();
		}

		// Token: 0x04009112 RID: 37138
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009113 RID: 37139
		[Tooltip("The name of the state that will be played.")]
		public FsmString stateName;

		// Token: 0x04009114 RID: 37140
		[Tooltip("The duration of the transition. Value is in source state normalized time.")]
		public FsmFloat transitionDuration;

		// Token: 0x04009115 RID: 37141
		[Tooltip("Layer index containing the destination state. Leave to none to ignore")]
		public FsmInt layer;

		// Token: 0x04009116 RID: 37142
		[Tooltip("Start time of the current destination state. Value is in source state normalized time, should be between 0 and 1.")]
		public FsmFloat normalizedTime;

		// Token: 0x04009117 RID: 37143
		private Animator _animator;
	}
}
