using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C1C RID: 3100
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Enables/Disables an Animation on a GameObject.\nAnimation time is paused while disabled. Animation must also have a non zero weight to play.")]
	public class EnableAnimation : BaseAnimationAction
	{
		// Token: 0x06009DFE RID: 40446 RVA: 0x0032A571 File Offset: 0x00328771
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
			this.enable = true;
			this.resetOnExit = false;
		}

		// Token: 0x06009DFF RID: 40447 RVA: 0x0032A599 File Offset: 0x00328799
		public override void OnEnter()
		{
			this.DoEnableAnimation(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x06009E00 RID: 40448 RVA: 0x0032A5B8 File Offset: 0x003287B8
		private void DoEnableAnimation(GameObject go)
		{
			if (base.UpdateCache(go))
			{
				this.anim = base.animation[this.animName.Value];
				if (this.anim != null)
				{
					this.anim.enabled = this.enable.Value;
				}
			}
		}

		// Token: 0x06009E01 RID: 40449 RVA: 0x0032A60E File Offset: 0x0032880E
		public override void OnExit()
		{
			if (this.resetOnExit.Value && this.anim != null)
			{
				this.anim.enabled = !this.enable.Value;
			}
		}

		// Token: 0x04008359 RID: 33625
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject playing the animation.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400835A RID: 33626
		[RequiredField]
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to enable/disable.")]
		public FsmString animName;

		// Token: 0x0400835B RID: 33627
		[RequiredField]
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;

		// Token: 0x0400835C RID: 33628
		[Tooltip("Reset the initial enabled state when exiting the state.")]
		public FsmBool resetOnExit;

		// Token: 0x0400835D RID: 33629
		private AnimationState anim;
	}
}
