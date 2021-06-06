using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("[DEBUG] Setup a Spell to go from a source to a target.")]
	public class SpellSourceTargetDebugAction : SpellAction
	{
		public FsmGameObject m_SpellObject;

		public FsmGameObject m_SourceObject;

		public FsmGameObject m_TargetObject;

		protected override GameObject GetSpellOwner()
		{
			return m_SpellObject.Value;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (!(m_spell == null))
			{
				m_spell.RemoveAllTargets();
				m_spell.SetSource(m_SourceObject.Value);
				if (m_TargetObject.Value != null)
				{
					m_spell.AddTarget(m_TargetObject.Value);
				}
				Finish();
			}
		}
	}
}
