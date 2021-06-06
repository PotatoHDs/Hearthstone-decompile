using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000B9C RID: 2972
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Adds a named Animation Clip to a Game Object. Optionally trims the Animation.")]
	public class AddAnimationClip : FsmStateAction
	{
		// Token: 0x06009B9D RID: 39837 RVA: 0x0031FF9C File Offset: 0x0031E19C
		public override void Reset()
		{
			this.gameObject = null;
			this.animationClip = null;
			this.animationName = "";
			this.firstFrame = 0;
			this.lastFrame = 0;
			this.addLoopFrame = false;
		}

		// Token: 0x06009B9E RID: 39838 RVA: 0x0031FFEB File Offset: 0x0031E1EB
		public override void OnEnter()
		{
			this.DoAddAnimationClip();
			base.Finish();
		}

		// Token: 0x06009B9F RID: 39839 RVA: 0x0031FFFC File Offset: 0x0031E1FC
		private void DoAddAnimationClip()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			AnimationClip animationClip = this.animationClip.Value as AnimationClip;
			if (animationClip == null)
			{
				return;
			}
			Animation component = ownerDefaultTarget.GetComponent<Animation>();
			if (this.firstFrame.Value == 0 && this.lastFrame.Value == 0)
			{
				component.AddClip(animationClip, this.animationName.Value);
				return;
			}
			component.AddClip(animationClip, this.animationName.Value, this.firstFrame.Value, this.lastFrame.Value, this.addLoopFrame.Value);
		}

		// Token: 0x040080E7 RID: 32999
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject to add the Animation Clip to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040080E8 RID: 33000
		[RequiredField]
		[ObjectType(typeof(AnimationClip))]
		[Tooltip("The animation clip to add. NOTE: Make sure the clip is compatible with the object's hierarchy.")]
		public FsmObject animationClip;

		// Token: 0x040080E9 RID: 33001
		[RequiredField]
		[Tooltip("Name the animation. Used by other actions to reference this animation.")]
		public FsmString animationName;

		// Token: 0x040080EA RID: 33002
		[Tooltip("Optionally trim the animation by specifying a first and last frame.")]
		public FsmInt firstFrame;

		// Token: 0x040080EB RID: 33003
		[Tooltip("Optionally trim the animation by specifying a first and last frame.")]
		public FsmInt lastFrame;

		// Token: 0x040080EC RID: 33004
		[Tooltip("Add an extra looping frame that matches the first frame.")]
		public FsmBool addLoopFrame;
	}
}
