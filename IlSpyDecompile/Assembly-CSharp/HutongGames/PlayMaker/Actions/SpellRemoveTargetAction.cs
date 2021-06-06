using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Removes a target from a Spell.")]
	public class SpellRemoveTargetAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public FsmGameObject m_TargetObject;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (!(m_spell == null))
			{
				GameObject value = m_TargetObject.Value;
				if (!(value == null))
				{
					m_spell.RemoveTarget(value);
					Finish();
				}
			}
		}
	}
}
