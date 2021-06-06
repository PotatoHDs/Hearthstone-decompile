using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F93 RID: 3987
	[ActionCategory("Pegasus")]
	[Tooltip("[DEBUG] Setup a Spell to go from a source to a target.")]
	public class SpellSourceTargetDebugAction : SpellAction
	{
		// Token: 0x0600ADDE RID: 44510 RVA: 0x003628C4 File Offset: 0x00360AC4
		protected override GameObject GetSpellOwner()
		{
			return this.m_SpellObject.Value;
		}

		// Token: 0x0600ADDF RID: 44511 RVA: 0x003628D4 File Offset: 0x00360AD4
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				return;
			}
			this.m_spell.RemoveAllTargets();
			this.m_spell.SetSource(this.m_SourceObject.Value);
			if (this.m_TargetObject.Value != null)
			{
				this.m_spell.AddTarget(this.m_TargetObject.Value);
			}
			base.Finish();
		}

		// Token: 0x040094B2 RID: 38066
		public FsmGameObject m_SpellObject;

		// Token: 0x040094B3 RID: 38067
		public FsmGameObject m_SourceObject;

		// Token: 0x040094B4 RID: 38068
		public FsmGameObject m_TargetObject;
	}
}
