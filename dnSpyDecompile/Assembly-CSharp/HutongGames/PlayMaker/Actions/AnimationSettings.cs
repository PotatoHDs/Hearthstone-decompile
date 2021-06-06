using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB3 RID: 2995
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Set the Wrap Mode, Blend Mode, Layer and Speed of an Animation.\nNOTE: Settings are applied once, on entering the state, NOT continuously. To dynamically control an animation's settings, use Set Animation Speed etc.")]
	public class AnimationSettings : BaseAnimationAction
	{
		// Token: 0x06009C34 RID: 39988 RVA: 0x00324B56 File Offset: 0x00322D56
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
			this.wrapMode = WrapMode.Loop;
			this.blendMode = AnimationBlendMode.Blend;
			this.speed = 1f;
			this.layer = 0;
		}

		// Token: 0x06009C35 RID: 39989 RVA: 0x00324B90 File Offset: 0x00322D90
		public override void OnEnter()
		{
			this.DoAnimationSettings();
			base.Finish();
		}

		// Token: 0x06009C36 RID: 39990 RVA: 0x00324BA0 File Offset: 0x00322DA0
		private void DoAnimationSettings()
		{
			if (string.IsNullOrEmpty(this.animName.Value))
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			AnimationState animationState = base.animation[this.animName.Value];
			if (animationState == null)
			{
				base.LogWarning("Missing animation: " + this.animName.Value);
				return;
			}
			animationState.wrapMode = this.wrapMode;
			animationState.blendMode = this.blendMode;
			if (!this.layer.IsNone)
			{
				animationState.layer = this.layer.Value;
			}
			if (!this.speed.IsNone)
			{
				animationState.speed = this.speed.Value;
			}
		}

		// Token: 0x040081B9 RID: 33209
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("A GameObject with an Animation Component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040081BA RID: 33210
		[RequiredField]
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation.")]
		public FsmString animName;

		// Token: 0x040081BB RID: 33211
		[Tooltip("The behavior of the animation when it wraps.")]
		public WrapMode wrapMode;

		// Token: 0x040081BC RID: 33212
		[Tooltip("How the animation is blended with other animations on the Game Object.")]
		public AnimationBlendMode blendMode;

		// Token: 0x040081BD RID: 33213
		[HasFloatSlider(0f, 5f)]
		[Tooltip("The speed of the animation. 1 = normal; 2 = double speed...")]
		public FsmFloat speed;

		// Token: 0x040081BE RID: 33214
		[Tooltip("The animation layer")]
		public FsmInt layer;
	}
}
