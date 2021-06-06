using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F90 RID: 3984
	[ActionCategory("Pegasus")]
	[Tooltip("Removes all targets from a Spell.")]
	public class SpellRemoveAllTargetsAction : SpellAction
	{
		// Token: 0x0600ADD5 RID: 44501 RVA: 0x003627D4 File Offset: 0x003609D4
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADD6 RID: 44502 RVA: 0x003627E7 File Offset: 0x003609E7
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				return;
			}
			this.m_spell.RemoveAllTargets();
			base.Finish();
		}

		// Token: 0x040094AD RID: 38061
		public FsmOwnerDefault m_SpellObject;
	}
}
