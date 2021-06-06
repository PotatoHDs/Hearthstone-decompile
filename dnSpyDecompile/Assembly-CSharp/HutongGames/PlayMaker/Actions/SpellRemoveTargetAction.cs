using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F91 RID: 3985
	[ActionCategory("Pegasus")]
	[Tooltip("Removes a target from a Spell.")]
	public class SpellRemoveTargetAction : SpellAction
	{
		// Token: 0x0600ADD8 RID: 44504 RVA: 0x0036280F File Offset: 0x00360A0F
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADD9 RID: 44505 RVA: 0x00362824 File Offset: 0x00360A24
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				return;
			}
			GameObject value = this.m_TargetObject.Value;
			if (value == null)
			{
				return;
			}
			this.m_spell.RemoveTarget(value);
			base.Finish();
		}

		// Token: 0x040094AE RID: 38062
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x040094AF RID: 38063
		public FsmGameObject m_TargetObject;
	}
}
