using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F92 RID: 3986
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the source for a Spell.")]
	public class SpellSetSourceAction : SpellAction
	{
		// Token: 0x0600ADDB RID: 44507 RVA: 0x0036286F File Offset: 0x00360A6F
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADDC RID: 44508 RVA: 0x00362884 File Offset: 0x00360A84
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				return;
			}
			GameObject value = this.m_SourceObject.Value;
			this.m_spell.SetSource(value);
			base.Finish();
		}

		// Token: 0x040094B0 RID: 38064
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x040094B1 RID: 38065
		public FsmGameObject m_SourceObject;
	}
}
