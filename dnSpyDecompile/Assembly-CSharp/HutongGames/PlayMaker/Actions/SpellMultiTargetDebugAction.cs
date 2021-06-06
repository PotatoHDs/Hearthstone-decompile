using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F8E RID: 3982
	[ActionCategory("Pegasus")]
	[Tooltip("[DEBUG] Setup a Spell to affect multiple targets.")]
	public class SpellMultiTargetDebugAction : SpellAction
	{
		// Token: 0x0600ADCE RID: 44494 RVA: 0x0036265B File Offset: 0x0036085B
		protected override GameObject GetSpellOwner()
		{
			return this.m_SpellObject.Value;
		}

		// Token: 0x0600ADCF RID: 44495 RVA: 0x00362668 File Offset: 0x00360868
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				return;
			}
			this.m_spell.SetSource(this.m_SourceObject.Value);
			this.m_spell.RemoveAllTargets();
			for (int i = 0; i < this.m_TargetObjects.Length; i++)
			{
				FsmGameObject fsmGameObject = this.m_TargetObjects[i];
				if (!(fsmGameObject.Value == null) && !this.m_spell.IsTarget(fsmGameObject.Value))
				{
					this.m_spell.AddTarget(fsmGameObject.Value);
				}
			}
			base.Finish();
		}

		// Token: 0x040094A5 RID: 38053
		public FsmGameObject m_SpellObject;

		// Token: 0x040094A6 RID: 38054
		public FsmGameObject m_SourceObject;

		// Token: 0x040094A7 RID: 38055
		public FsmGameObject[] m_TargetObjects;
	}
}
