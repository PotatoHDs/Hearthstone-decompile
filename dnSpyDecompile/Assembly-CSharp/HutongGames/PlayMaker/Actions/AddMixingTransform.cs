using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA0 RID: 2976
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Play an animation on a subset of the hierarchy. E.g., A waving animation on the upper body.")]
	public class AddMixingTransform : BaseAnimationAction
	{
		// Token: 0x06009BB2 RID: 39858 RVA: 0x003203FE File Offset: 0x0031E5FE
		public override void Reset()
		{
			this.gameObject = null;
			this.animationName = "";
			this.transform = "";
			this.recursive = true;
		}

		// Token: 0x06009BB3 RID: 39859 RVA: 0x00320433 File Offset: 0x0031E633
		public override void OnEnter()
		{
			this.DoAddMixingTransform();
			base.Finish();
		}

		// Token: 0x06009BB4 RID: 39860 RVA: 0x00320444 File Offset: 0x0031E644
		private void DoAddMixingTransform()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			AnimationState animationState = base.animation[this.animationName.Value];
			if (animationState == null)
			{
				return;
			}
			Transform mix = ownerDefaultTarget.transform.Find(this.transform.Value);
			animationState.AddMixingTransform(mix, this.recursive.Value);
		}

		// Token: 0x04008102 RID: 33026
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject playing the animation.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008103 RID: 33027
		[RequiredField]
		[Tooltip("The name of the animation to mix. NOTE: The animation should already be added to the Animation Component on the GameObject.")]
		public FsmString animationName;

		// Token: 0x04008104 RID: 33028
		[RequiredField]
		[Tooltip("The mixing transform. E.g., root/upper_body/left_shoulder")]
		public FsmString transform;

		// Token: 0x04008105 RID: 33029
		[Tooltip("If recursive is true all children of the mix transform will also be animated.")]
		public FsmBool recursive;
	}
}
