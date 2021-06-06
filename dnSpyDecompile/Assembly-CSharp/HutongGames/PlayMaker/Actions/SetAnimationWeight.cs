using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA8 RID: 3496
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Sets the Blend Weight of an Animation. Check Every Frame to update the weight continuously, e.g., if you're manipulating a variable that controls the weight.")]
	public class SetAnimationWeight : BaseAnimationAction
	{
		// Token: 0x0600A543 RID: 42307 RVA: 0x00346449 File Offset: 0x00344649
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
			this.weight = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A544 RID: 42308 RVA: 0x00346470 File Offset: 0x00344670
		public override void OnEnter()
		{
			this.DoSetAnimationWeight((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A545 RID: 42309 RVA: 0x003464AB File Offset: 0x003446AB
		public override void OnUpdate()
		{
			this.DoSetAnimationWeight((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
		}

		// Token: 0x0600A546 RID: 42310 RVA: 0x003464D8 File Offset: 0x003446D8
		private void DoSetAnimationWeight(GameObject go)
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
			animationState.weight = this.weight.Value;
		}

		// Token: 0x04008BD8 RID: 35800
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BD9 RID: 35801
		[RequiredField]
		[UIHint(UIHint.Animation)]
		public FsmString animName;

		// Token: 0x04008BDA RID: 35802
		public FsmFloat weight = 1f;

		// Token: 0x04008BDB RID: 35803
		public bool everyFrame;
	}
}
