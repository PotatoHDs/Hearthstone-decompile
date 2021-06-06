using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F09 RID: 3849
	[ActionCategory("Pegasus")]
	[Tooltip("Put an Actor's cost data into variables.")]
	public class ActorGetCostAction : ActorAction
	{
		// Token: 0x0600AB9A RID: 43930 RVA: 0x0035948B File Offset: 0x0035768B
		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_ActorObject);
		}

		// Token: 0x0600AB9B RID: 43931 RVA: 0x0035949E File Offset: 0x0035769E
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_ManaGem = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_UberText = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_Cost = new FsmInt
			{
				UseVariable = true
			};
		}

		// Token: 0x0600AB9C RID: 43932 RVA: 0x003594E0 File Offset: 0x003576E0
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			if (!this.m_ManaGem.IsNone)
			{
				this.m_ManaGem.Value = this.m_actor.m_manaObject;
			}
			if (!this.m_UberText.IsNone)
			{
				this.m_UberText.Value = this.m_actor.GetCostTextObject();
			}
			if (!this.m_Cost.IsNone)
			{
				Entity entity = this.m_actor.GetEntity();
				if (entity != null)
				{
					this.m_Cost.Value = entity.GetCost();
				}
				else
				{
					EntityDef entityDef = this.m_actor.GetEntityDef();
					if (entityDef != null)
					{
						this.m_Cost.Value = entityDef.GetCost();
					}
				}
			}
			base.Finish();
		}

		// Token: 0x04009254 RID: 37460
		public FsmOwnerDefault m_ActorObject;

		// Token: 0x04009255 RID: 37461
		public FsmGameObject m_ManaGem;

		// Token: 0x04009256 RID: 37462
		public FsmGameObject m_UberText;

		// Token: 0x04009257 RID: 37463
		public FsmInt m_Cost;
	}
}
