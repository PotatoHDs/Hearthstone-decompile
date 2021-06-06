using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F74 RID: 3956
	[ActionCategory("Pegasus")]
	[Tooltip("Set the actor's card's input as enabled or disabled.")]
	public class SetCardInputEnabledAction : ActorAction
	{
		// Token: 0x0600AD59 RID: 44377 RVA: 0x00360A9B File Offset: 0x0035EC9B
		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_ActorObject);
		}

		// Token: 0x0600AD5A RID: 44378 RVA: 0x00360AAE File Offset: 0x0035ECAE
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_InputEnabled = null;
		}

		// Token: 0x0600AD5B RID: 44379 RVA: 0x00360AC0 File Offset: 0x0035ECC0
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			Card card = this.m_actor.GetCard();
			if (card == null)
			{
				Debug.LogError(string.Format("SetCardInputEnabledAction.OnEnter() - No card for actor {0}", this.m_actor));
				base.Finish();
				return;
			}
			card.SetInputEnabled(this.m_InputEnabled.Value);
			base.Finish();
		}

		// Token: 0x04009437 RID: 37943
		public FsmOwnerDefault m_ActorObject;

		// Token: 0x04009438 RID: 37944
		public FsmBool m_InputEnabled;
	}
}
