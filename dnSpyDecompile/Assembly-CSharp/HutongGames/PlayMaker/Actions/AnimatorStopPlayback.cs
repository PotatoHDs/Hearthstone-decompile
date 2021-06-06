using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ECE RID: 3790
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Stops the animator playback mode. When playback stops, the avatar resumes getting control from game logic")]
	public class AnimatorStopPlayback : FsmStateAction
	{
		// Token: 0x0600AA84 RID: 43652 RVA: 0x00355D56 File Offset: 0x00353F56
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600AA85 RID: 43653 RVA: 0x00355D60 File Offset: 0x00353F60
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
				component.StopPlayback();
			}
			base.Finish();
		}

		// Token: 0x0400912F RID: 37167
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
	}
}
