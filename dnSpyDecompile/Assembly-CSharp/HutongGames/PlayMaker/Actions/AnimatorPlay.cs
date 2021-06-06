using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ECB RID: 3787
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Plays a state. This could be used to synchronize your animation with audio or synchronize an Animator over the network.")]
	public class AnimatorPlay : FsmStateAction
	{
		// Token: 0x0600AA79 RID: 43641 RVA: 0x00355B93 File Offset: 0x00353D93
		public override void Reset()
		{
			this.gameObject = null;
			this.stateName = null;
			this.layer = new FsmInt
			{
				UseVariable = true
			};
			this.normalizedTime = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600AA7A RID: 43642 RVA: 0x00355BD0 File Offset: 0x00353DD0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this._animator = ownerDefaultTarget.GetComponent<Animator>();
			this.DoAnimatorPlay();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA7B RID: 43643 RVA: 0x00355C1F File Offset: 0x00353E1F
		public override void OnUpdate()
		{
			this.DoAnimatorPlay();
		}

		// Token: 0x0600AA7C RID: 43644 RVA: 0x00355C28 File Offset: 0x00353E28
		private void DoAnimatorPlay()
		{
			if (this._animator != null)
			{
				int num = this.layer.IsNone ? -1 : this.layer.Value;
				float num2 = this.normalizedTime.IsNone ? float.NegativeInfinity : this.normalizedTime.Value;
				this._animator.Play(this.stateName.Value, num, num2);
			}
		}

		// Token: 0x04009126 RID: 37158
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009127 RID: 37159
		[Tooltip("The name of the state that will be played.")]
		public FsmString stateName;

		// Token: 0x04009128 RID: 37160
		[Tooltip("The layer where the state is.")]
		public FsmInt layer;

		// Token: 0x04009129 RID: 37161
		[Tooltip("The normalized time at which the state will play")]
		public FsmFloat normalizedTime;

		// Token: 0x0400912A RID: 37162
		[Tooltip("Repeat every frame. Useful when using normalizedTime to manually control the animation.")]
		public bool everyFrame;

		// Token: 0x0400912B RID: 37163
		private Animator _animator;
	}
}
