using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D70 RID: 3440
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Removes a mixing transform previously added with Add Mixing Transform. If transform has been added as recursive, then it will be removed as recursive. Once you remove all mixing transforms added to animation state all curves become animated again.")]
	public class RemoveMixingTransform : BaseAnimationAction
	{
		// Token: 0x0600A437 RID: 42039 RVA: 0x00342766 File Offset: 0x00340966
		public override void Reset()
		{
			this.gameObject = null;
			this.animationName = "";
		}

		// Token: 0x0600A438 RID: 42040 RVA: 0x0034277F File Offset: 0x0034097F
		public override void OnEnter()
		{
			this.DoRemoveMixingTransform();
			base.Finish();
		}

		// Token: 0x0600A439 RID: 42041 RVA: 0x00342790 File Offset: 0x00340990
		private void DoRemoveMixingTransform()
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
			Transform mix = ownerDefaultTarget.transform.Find(this.transfrom.Value);
			animationState.AddMixingTransform(mix);
		}

		// Token: 0x04008AAF RID: 35503
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject playing the animation.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008AB0 RID: 35504
		[RequiredField]
		[Tooltip("The name of the animation.")]
		public FsmString animationName;

		// Token: 0x04008AB1 RID: 35505
		[RequiredField]
		[Tooltip("The mixing transform to remove. E.g., root/upper_body/left_shoulder")]
		public FsmString transfrom;
	}
}
