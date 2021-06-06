using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA6 RID: 3494
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Sets the Speed of an Animation. Check Every Frame to update the animation time continuously, e.g., if you're manipulating a variable that controls animation speed.")]
	public class SetAnimationSpeed : BaseAnimationAction
	{
		// Token: 0x0600A539 RID: 42297 RVA: 0x00346202 File Offset: 0x00344402
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
			this.speed = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A53A RID: 42298 RVA: 0x00346229 File Offset: 0x00344429
		public override void OnEnter()
		{
			this.DoSetAnimationSpeed((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A53B RID: 42299 RVA: 0x00346264 File Offset: 0x00344464
		public override void OnUpdate()
		{
			this.DoSetAnimationSpeed((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
		}

		// Token: 0x0600A53C RID: 42300 RVA: 0x00346294 File Offset: 0x00344494
		private void DoSetAnimationSpeed(GameObject go)
		{
			if (!base.UpdateCache(go))
			{
				return;
			}
			AnimationState animationState = base.animation[this.animName.Value];
			if (animationState == null)
			{
				base.LogWarning("Missing animation: " + this.animName.Value);
				return;
			}
			animationState.speed = this.speed.Value;
		}

		// Token: 0x04008BCF RID: 35791
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BD0 RID: 35792
		[RequiredField]
		[UIHint(UIHint.Animation)]
		public FsmString animName;

		// Token: 0x04008BD1 RID: 35793
		public FsmFloat speed = 1f;

		// Token: 0x04008BD2 RID: 35794
		public bool everyFrame;
	}
}
