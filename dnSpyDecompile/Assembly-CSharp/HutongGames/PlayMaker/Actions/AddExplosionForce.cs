using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000B9E RID: 2974
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Applies a force to a Game Object that simulates explosion effects. The explosion force will fall off linearly with distance. Hint: Use the Explosion Action instead to apply an explosion force to all objects in a blast radius.")]
	public class AddExplosionForce : ComponentAction<Rigidbody>
	{
		// Token: 0x06009BA6 RID: 39846 RVA: 0x00320173 File Offset: 0x0031E373
		public override void Reset()
		{
			this.gameObject = null;
			this.center = new FsmVector3
			{
				UseVariable = true
			};
			this.upwardsModifier = 0f;
			this.forceMode = ForceMode.Force;
			this.everyFrame = false;
		}

		// Token: 0x06009BA7 RID: 39847 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x06009BA8 RID: 39848 RVA: 0x003201BA File Offset: 0x0031E3BA
		public override void OnEnter()
		{
			this.DoAddExplosionForce();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009BA9 RID: 39849 RVA: 0x003201D0 File Offset: 0x0031E3D0
		public override void OnFixedUpdate()
		{
			this.DoAddExplosionForce();
		}

		// Token: 0x06009BAA RID: 39850 RVA: 0x003201D8 File Offset: 0x0031E3D8
		private void DoAddExplosionForce()
		{
			GameObject go = (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value;
			if (this.center == null || !base.UpdateCache(go))
			{
				return;
			}
			base.rigidbody.AddExplosionForce(this.force.Value, this.center.Value, this.radius.Value, this.upwardsModifier.Value, this.forceMode);
		}

		// Token: 0x040080F2 RID: 33010
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The GameObject to add the explosion force to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040080F3 RID: 33011
		[RequiredField]
		[Tooltip("The center of the explosion. Hint: this is often the position returned from a GetCollisionInfo action.")]
		public FsmVector3 center;

		// Token: 0x040080F4 RID: 33012
		[RequiredField]
		[Tooltip("The strength of the explosion.")]
		public FsmFloat force;

		// Token: 0x040080F5 RID: 33013
		[RequiredField]
		[Tooltip("The radius of the explosion. Force falls off linearly with distance.")]
		public FsmFloat radius;

		// Token: 0x040080F6 RID: 33014
		[Tooltip("Applies the force as if it was applied from beneath the object. This is useful since explosions that throw things up instead of pushing things to the side look cooler. A value of 2 will apply a force as if it is applied from 2 meters below while not changing the actual explosion position.")]
		public FsmFloat upwardsModifier;

		// Token: 0x040080F7 RID: 33015
		[Tooltip("The type of force to apply. See Unity Physics docs.")]
		public ForceMode forceMode;

		// Token: 0x040080F8 RID: 33016
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
