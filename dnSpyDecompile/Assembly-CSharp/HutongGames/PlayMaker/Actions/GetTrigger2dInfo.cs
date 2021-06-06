using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D0C RID: 3340
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets info on the last Trigger 2d event and store in variables.  See Unity and PlayMaker docs on Unity 2D physics.")]
	public class GetTrigger2dInfo : FsmStateAction
	{
		// Token: 0x0600A239 RID: 41529 RVA: 0x0033B311 File Offset: 0x00339511
		public override void Reset()
		{
			this.gameObjectHit = null;
			this.shapeCount = null;
			this.physics2dMaterialName = null;
		}

		// Token: 0x0600A23A RID: 41530 RVA: 0x0033B328 File Offset: 0x00339528
		private void StoreTriggerInfo()
		{
			if (base.Fsm.TriggerCollider2D == null)
			{
				return;
			}
			this.gameObjectHit.Value = base.Fsm.TriggerCollider2D.gameObject;
			this.shapeCount.Value = base.Fsm.TriggerCollider2D.shapeCount;
			this.physics2dMaterialName.Value = ((base.Fsm.TriggerCollider2D.sharedMaterial != null) ? base.Fsm.TriggerCollider2D.sharedMaterial.name : "");
		}

		// Token: 0x0600A23B RID: 41531 RVA: 0x0033B3BE File Offset: 0x003395BE
		public override void OnEnter()
		{
			this.StoreTriggerInfo();
			base.Finish();
		}

		// Token: 0x04008877 RID: 34935
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit.")]
		public FsmGameObject gameObjectHit;

		// Token: 0x04008878 RID: 34936
		[UIHint(UIHint.Variable)]
		[Tooltip("The number of separate shaped regions in the collider.")]
		public FsmInt shapeCount;

		// Token: 0x04008879 RID: 34937
		[UIHint(UIHint.Variable)]
		[Tooltip("Useful for triggering different effects. Audio, particles...")]
		public FsmString physics2dMaterialName;
	}
}
