using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED4 RID: 3796
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the culling of this Animator component. Optionally sends events.\nIf true ('AlwaysAnimate'): always animate the entire character. Object is animated even when offscreen.\nIf False ('BasedOnRenderers') animation is disabled when renderers are not visible.")]
	public class GetAnimatorCullingMode : FsmStateAction
	{
		// Token: 0x0600AA9D RID: 43677 RVA: 0x003561EA File Offset: 0x003543EA
		public override void Reset()
		{
			this.gameObject = null;
			this.alwaysAnimate = null;
			this.alwaysAnimateEvent = null;
			this.basedOnRenderersEvent = null;
		}

		// Token: 0x0600AA9E RID: 43678 RVA: 0x00356208 File Offset: 0x00354408
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
			this.DoCheckCulling();
			base.Finish();
		}

		// Token: 0x0600AA9F RID: 43679 RVA: 0x00356264 File Offset: 0x00354464
		private void DoCheckCulling()
		{
			if (this._animator == null)
			{
				return;
			}
			bool flag = this._animator.cullingMode == AnimatorCullingMode.AlwaysAnimate;
			this.alwaysAnimate.Value = flag;
			if (flag)
			{
				base.Fsm.Event(this.alwaysAnimateEvent);
				return;
			}
			base.Fsm.Event(this.basedOnRenderersEvent);
		}

		// Token: 0x04009147 RID: 37191
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009148 RID: 37192
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("If true, always animate the entire character, else animation is disabled when renderers are not visible")]
		public FsmBool alwaysAnimate;

		// Token: 0x04009149 RID: 37193
		[Tooltip("Event send if culling mode is 'AlwaysAnimate'")]
		public FsmEvent alwaysAnimateEvent;

		// Token: 0x0400914A RID: 37194
		[Tooltip("Event send if culling mode is 'BasedOnRenders'")]
		public FsmEvent basedOnRenderersEvent;

		// Token: 0x0400914B RID: 37195
		private Animator _animator;
	}
}
