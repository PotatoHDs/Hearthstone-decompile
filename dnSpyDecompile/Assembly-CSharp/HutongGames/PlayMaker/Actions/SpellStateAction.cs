using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F94 RID: 3988
	[ActionCategory("Pegasus")]
	[Tooltip("Handles communication between a Spell and the SpellStates in an FSM.")]
	public class SpellStateAction : SpellAction
	{
		// Token: 0x0600ADE1 RID: 44513 RVA: 0x00362946 File Offset: 0x00360B46
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADE2 RID: 44514 RVA: 0x0036295C File Offset: 0x00360B5C
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				return;
			}
			this.DiscoverSpellStateType();
			if (this.m_stateInvalid)
			{
				return;
			}
			this.m_spell.OnFsmStateStarted(base.State, this.m_spellStateType);
			base.Finish();
		}

		// Token: 0x0600ADE3 RID: 44515 RVA: 0x003629AC File Offset: 0x00360BAC
		private void DiscoverSpellStateType()
		{
			if (!this.m_stateDirty)
			{
				return;
			}
			string name = base.State.Name;
			foreach (FsmTransition fsmTransition in base.Fsm.GlobalTransitions)
			{
				if (name.Equals(fsmTransition.ToState))
				{
					this.m_spellStateType = EnumUtils.GetEnum<SpellStateType>(fsmTransition.EventName);
					this.m_stateDirty = false;
					return;
				}
			}
			this.m_stateInvalid = true;
		}

		// Token: 0x040094B5 RID: 38069
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x040094B6 RID: 38070
		private SpellStateType m_spellStateType;

		// Token: 0x040094B7 RID: 38071
		private bool m_stateInvalid;

		// Token: 0x040094B8 RID: 38072
		private bool m_stateDirty = true;
	}
}
