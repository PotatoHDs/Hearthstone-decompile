using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Adds a target to a Spell.")]
	public class SpellAddTargetAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public FsmGameObject m_TargetObject;

		public FsmBool m_AllowDuplicates;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			base.Reset();
			m_AllowDuplicates = false;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_spell == null)
			{
				return;
			}
			GameObject value = m_TargetObject.Value;
			if (!(value == null))
			{
				if (!m_spell.IsTarget(value) || m_AllowDuplicates.Value)
				{
					m_spell.AddTarget(value);
				}
				Finish();
			}
		}
	}
}
