using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA7 RID: 3495
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Sets the current Time of an Animation, Normalize time means 0 (start) to 1 (end); useful if you don't care about the exact time. Check Every Frame to update the time continuously.")]
	public class SetAnimationTime : BaseAnimationAction
	{
		// Token: 0x0600A53E RID: 42302 RVA: 0x00346310 File Offset: 0x00344510
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
			this.time = null;
			this.normalized = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A53F RID: 42303 RVA: 0x00346335 File Offset: 0x00344535
		public override void OnEnter()
		{
			this.DoSetAnimationTime((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A540 RID: 42304 RVA: 0x00346370 File Offset: 0x00344570
		public override void OnUpdate()
		{
			this.DoSetAnimationTime((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
		}

		// Token: 0x0600A541 RID: 42305 RVA: 0x003463A0 File Offset: 0x003445A0
		private void DoSetAnimationTime(GameObject go)
		{
			if (!base.UpdateCache(go))
			{
				return;
			}
			base.animation.Play(this.animName.Value);
			AnimationState animationState = base.animation[this.animName.Value];
			if (animationState == null)
			{
				base.LogWarning("Missing animation: " + this.animName.Value);
				return;
			}
			if (this.normalized)
			{
				animationState.normalizedTime = this.time.Value;
			}
			else
			{
				animationState.time = this.time.Value;
			}
			if (this.everyFrame)
			{
				animationState.speed = 0f;
			}
		}

		// Token: 0x04008BD3 RID: 35795
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BD4 RID: 35796
		[RequiredField]
		[UIHint(UIHint.Animation)]
		public FsmString animName;

		// Token: 0x04008BD5 RID: 35797
		public FsmFloat time;

		// Token: 0x04008BD6 RID: 35798
		public bool normalized;

		// Token: 0x04008BD7 RID: 35799
		public bool everyFrame;
	}
}
