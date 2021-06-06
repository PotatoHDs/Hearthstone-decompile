using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CFC RID: 3324
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last particle collision event. See Unity Particle System docs.")]
	public class GetParticleCollisionInfo : FsmStateAction
	{
		// Token: 0x0600A1DC RID: 41436 RVA: 0x003394EE File Offset: 0x003376EE
		public override void Reset()
		{
			this.gameObjectHit = null;
		}

		// Token: 0x0600A1DD RID: 41437 RVA: 0x003394F7 File Offset: 0x003376F7
		private void StoreCollisionInfo()
		{
			this.gameObjectHit.Value = base.Fsm.ParticleCollisionGO;
		}

		// Token: 0x0600A1DE RID: 41438 RVA: 0x0033950F File Offset: 0x0033770F
		public override void OnEnter()
		{
			this.StoreCollisionInfo();
			base.Finish();
		}

		// Token: 0x040087ED RID: 34797
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit.")]
		public FsmGameObject gameObjectHit;
	}
}
