using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF8 RID: 3832
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Controls culling of this Animator component.\nIf true, set to 'AlwaysAnimate': always animate the entire character. Object is animated even when offscreen.\nIf False, set to 'BasedOnRenderes' or CullUpdateTransforms ( On Unity 5) animation is disabled when renderers are not visible.")]
	public class SetAnimatorCullingMode : FsmStateAction
	{
		// Token: 0x0600AB4A RID: 43850 RVA: 0x00358576 File Offset: 0x00356776
		public override void Reset()
		{
			this.gameObject = null;
			this.alwaysAnimate = null;
		}

		// Token: 0x0600AB4B RID: 43851 RVA: 0x00358588 File Offset: 0x00356788
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
			this.SetCullingMode();
			base.Finish();
		}

		// Token: 0x0600AB4C RID: 43852 RVA: 0x003585E4 File Offset: 0x003567E4
		private void SetCullingMode()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.cullingMode = (this.alwaysAnimate.Value ? AnimatorCullingMode.AlwaysAnimate : AnimatorCullingMode.CullUpdateTransforms);
		}

		// Token: 0x04009209 RID: 37385
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400920A RID: 37386
		[Tooltip("If true, always animate the entire character, else animation is disabled when renderers are not visible")]
		public FsmBool alwaysAnimate;

		// Token: 0x0400920B RID: 37387
		private Animator _animator;
	}
}
