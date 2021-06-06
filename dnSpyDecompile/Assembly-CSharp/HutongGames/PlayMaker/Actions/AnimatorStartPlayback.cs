using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ECC RID: 3788
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the animator in playback mode.")]
	public class AnimatorStartPlayback : FsmStateAction
	{
		// Token: 0x0600AA7E RID: 43646 RVA: 0x00355C97 File Offset: 0x00353E97
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600AA7F RID: 43647 RVA: 0x00355CA0 File Offset: 0x00353EA0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			Animator component = ownerDefaultTarget.GetComponent<Animator>();
			if (component != null)
			{
				component.StartPlayback();
			}
			base.Finish();
		}

		// Token: 0x0400912C RID: 37164
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
	}
}
