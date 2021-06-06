using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C23 RID: 3107
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Applies an explosion Force to all Game Objects with a Rigid Body inside a Radius.")]
	public class Explosion : FsmStateAction
	{
		// Token: 0x06009E1F RID: 40479 RVA: 0x0032AB1D File Offset: 0x00328D1D
		public override void Reset()
		{
			this.center = null;
			this.upwardsModifier = 0f;
			this.forceMode = ForceMode.Force;
			this.everyFrame = false;
		}

		// Token: 0x06009E20 RID: 40480 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x06009E21 RID: 40481 RVA: 0x0032AB44 File Offset: 0x00328D44
		public override void OnEnter()
		{
			this.DoExplosion();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E22 RID: 40482 RVA: 0x0032AB5A File Offset: 0x00328D5A
		public override void OnFixedUpdate()
		{
			this.DoExplosion();
		}

		// Token: 0x06009E23 RID: 40483 RVA: 0x0032AB64 File Offset: 0x00328D64
		private void DoExplosion()
		{
			foreach (Collider collider in Physics.OverlapSphere(this.center.Value, this.radius.Value))
			{
				Rigidbody component = collider.gameObject.GetComponent<Rigidbody>();
				if (component != null && this.ShouldApplyForce(collider.gameObject))
				{
					component.AddExplosionForce(this.force.Value, this.center.Value, this.radius.Value, this.upwardsModifier.Value, this.forceMode);
				}
			}
		}

		// Token: 0x06009E24 RID: 40484 RVA: 0x0032ABFC File Offset: 0x00328DFC
		private bool ShouldApplyForce(GameObject go)
		{
			int num = ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value);
			return (1 << go.layer & num) > 0;
		}

		// Token: 0x04008376 RID: 33654
		[RequiredField]
		[Tooltip("The world position of the center of the explosion.")]
		public FsmVector3 center;

		// Token: 0x04008377 RID: 33655
		[RequiredField]
		[Tooltip("The strength of the explosion.")]
		public FsmFloat force;

		// Token: 0x04008378 RID: 33656
		[RequiredField]
		[Tooltip("The radius of the explosion. Force falls of linearly with distance.")]
		public FsmFloat radius;

		// Token: 0x04008379 RID: 33657
		[Tooltip("Applies the force as if it was applied from beneath the object. This is useful since explosions that throw things up instead of pushing things to the side look cooler. A value of 2 will apply a force as if it is applied from 2 meters below while not changing the actual explosion position.")]
		public FsmFloat upwardsModifier;

		// Token: 0x0400837A RID: 33658
		[Tooltip("The type of force to apply.")]
		public ForceMode forceMode;

		// Token: 0x0400837B RID: 33659
		[UIHint(UIHint.Layer)]
		public FsmInt layer;

		// Token: 0x0400837C RID: 33660
		[UIHint(UIHint.Layer)]
		[Tooltip("Layers to effect.")]
		public FsmInt[] layerMask;

		// Token: 0x0400837D RID: 33661
		[Tooltip("Invert the mask, so you effect all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x0400837E RID: 33662
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
