using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F7E RID: 3966
	[ActionCategory("Pegasus")]
	[Tooltip("Adds a target to a Spell.")]
	public class SpellAddTargetAction : SpellAction
	{
		// Token: 0x0600AD87 RID: 44423 RVA: 0x0036168F File Offset: 0x0035F88F
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600AD88 RID: 44424 RVA: 0x003616A2 File Offset: 0x0035F8A2
		public override void Reset()
		{
			base.Reset();
			this.m_AllowDuplicates = false;
		}

		// Token: 0x0600AD89 RID: 44425 RVA: 0x003616B8 File Offset: 0x0035F8B8
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
			if (!this.m_spell.IsTarget(value) || this.m_AllowDuplicates.Value)
			{
				this.m_spell.AddTarget(value);
			}
			base.Finish();
		}

		// Token: 0x0400945E RID: 37982
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x0400945F RID: 37983
		public FsmGameObject m_TargetObject;

		// Token: 0x04009460 RID: 37984
		public FsmBool m_AllowDuplicates;
	}
}
