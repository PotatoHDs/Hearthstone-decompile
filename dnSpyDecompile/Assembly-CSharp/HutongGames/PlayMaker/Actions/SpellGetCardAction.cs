using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F89 RID: 3977
	[ActionCategory("Pegasus")]
	[Tooltip("Put a Spell's Source or Target Card into a GameObject variable.")]
	public class SpellGetCardAction : SpellAction
	{
		// Token: 0x0600ADBC RID: 44476 RVA: 0x00362213 File Offset: 0x00360413
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADBD RID: 44477 RVA: 0x00362226 File Offset: 0x00360426
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichCard = SpellAction.Which.SOURCE;
			this.m_GameObject = null;
		}

		// Token: 0x0600ADBE RID: 44478 RVA: 0x00362240 File Offset: 0x00360440
		public override void OnEnter()
		{
			base.OnEnter();
			Card card = base.GetCard(this.m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellGetCardAction.OnEnter() - Card not found!", Array.Empty<object>());
				base.Finish();
				return;
			}
			if (!this.m_GameObject.IsNone)
			{
				this.m_GameObject.Value = card.gameObject;
			}
			base.Finish();
		}

		// Token: 0x04009498 RID: 38040
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009499 RID: 38041
		public SpellAction.Which m_WhichCard;

		// Token: 0x0400949A RID: 38042
		public FsmGameObject m_GameObject;
	}
}
