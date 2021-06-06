using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C93 RID: 3219
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last Trigger event and store in variables.")]
	public class GetTriggerInfo : FsmStateAction
	{
		// Token: 0x0600A018 RID: 40984 RVA: 0x0032FFAE File Offset: 0x0032E1AE
		public override void Reset()
		{
			this.gameObjectHit = null;
			this.physicsMaterialName = null;
		}

		// Token: 0x0600A019 RID: 40985 RVA: 0x0032FFC0 File Offset: 0x0032E1C0
		private void StoreTriggerInfo()
		{
			if (base.Fsm.TriggerCollider == null)
			{
				return;
			}
			this.gameObjectHit.Value = base.Fsm.TriggerCollider.gameObject;
			this.physicsMaterialName.Value = base.Fsm.TriggerCollider.material.name;
		}

		// Token: 0x0600A01A RID: 40986 RVA: 0x0033001C File Offset: 0x0032E21C
		public override void OnEnter()
		{
			this.StoreTriggerInfo();
			base.Finish();
		}

		// Token: 0x04008598 RID: 34200
		[UIHint(UIHint.Variable)]
		public FsmGameObject gameObjectHit;

		// Token: 0x04008599 RID: 34201
		[UIHint(UIHint.Variable)]
		[Tooltip("Useful for triggering different effects. Audio, particles...")]
		public FsmString physicsMaterialName;
	}
}
