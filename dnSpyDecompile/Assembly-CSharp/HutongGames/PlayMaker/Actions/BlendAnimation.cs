using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BDB RID: 3035
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Blends an Animation towards a Target Weight over a specified Time.\nOptionally sends an Event when finished.")]
	public class BlendAnimation : BaseAnimationAction
	{
		// Token: 0x06009CD1 RID: 40145 RVA: 0x003269B4 File Offset: 0x00324BB4
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
			this.targetWeight = 1f;
			this.time = 0.3f;
			this.finishEvent = null;
		}

		// Token: 0x06009CD2 RID: 40146 RVA: 0x003269EB File Offset: 0x00324BEB
		public override void OnEnter()
		{
			this.DoBlendAnimation((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
		}

		// Token: 0x06009CD3 RID: 40147 RVA: 0x00326A18 File Offset: 0x00324C18
		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(this.delayedFinishEvent))
			{
				base.Finish();
			}
		}

		// Token: 0x06009CD4 RID: 40148 RVA: 0x00326A30 File Offset: 0x00324C30
		private void DoBlendAnimation(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			Animation component = go.GetComponent<Animation>();
			if (component == null)
			{
				base.LogWarning("Missing Animation component on GameObject: " + go.name);
				base.Finish();
				return;
			}
			AnimationState animationState = component[this.animName.Value];
			if (animationState == null)
			{
				base.LogWarning("Missing animation: " + this.animName.Value);
				base.Finish();
				return;
			}
			float value = this.time.Value;
			component.Blend(this.animName.Value, this.targetWeight.Value, value);
			if (this.finishEvent != null)
			{
				this.delayedFinishEvent = base.Fsm.DelayedEvent(this.finishEvent, animationState.length);
				return;
			}
			base.Finish();
		}

		// Token: 0x04008246 RID: 33350
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject to animate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008247 RID: 33351
		[RequiredField]
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to blend.")]
		public FsmString animName;

		// Token: 0x04008248 RID: 33352
		[RequiredField]
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Target weight to blend to.")]
		public FsmFloat targetWeight;

		// Token: 0x04008249 RID: 33353
		[RequiredField]
		[HasFloatSlider(0f, 5f)]
		[Tooltip("How long should the blend take.")]
		public FsmFloat time;

		// Token: 0x0400824A RID: 33354
		[Tooltip("Event to send when the blend has finished.")]
		public FsmEvent finishEvent;

		// Token: 0x0400824B RID: 33355
		private DelayedEvent delayedFinishEvent;
	}
}
