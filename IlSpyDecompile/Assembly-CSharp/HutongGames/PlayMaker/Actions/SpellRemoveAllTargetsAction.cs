using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Removes all targets from a Spell.")]
	public class SpellRemoveAllTargetsAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (!(m_spell == null))
			{
				m_spell.RemoveAllTargets();
				Finish();
			}
		}
	}
}
