using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Handles communication between a Spell and the SpellStates in an FSM.")]
	public class SpellStateAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		private SpellStateType m_spellStateType;

		private bool m_stateInvalid;

		private bool m_stateDirty = true;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (!(m_spell == null))
			{
				DiscoverSpellStateType();
				if (!m_stateInvalid)
				{
					m_spell.OnFsmStateStarted(base.State, m_spellStateType);
					Finish();
				}
			}
		}

		private void DiscoverSpellStateType()
		{
			if (!m_stateDirty)
			{
				return;
			}
			string text = base.State.Name;
			FsmTransition[] globalTransitions = base.Fsm.GlobalTransitions;
			foreach (FsmTransition fsmTransition in globalTransitions)
			{
				if (text.Equals(fsmTransition.ToState))
				{
					m_spellStateType = EnumUtils.GetEnum<SpellStateType>(fsmTransition.EventName);
					m_stateDirty = false;
					return;
				}
			}
			m_stateInvalid = true;
		}
	}
}
