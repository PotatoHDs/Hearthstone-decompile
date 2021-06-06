using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("[DEBUG] Setup a Spell to affect multiple targets.")]
	public class SpellMultiTargetDebugAction : SpellAction
	{
		public FsmGameObject m_SpellObject;

		public FsmGameObject m_SourceObject;

		public FsmGameObject[] m_TargetObjects;

		protected override GameObject GetSpellOwner()
		{
			return m_SpellObject.Value;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_spell == null)
			{
				return;
			}
			m_spell.SetSource(m_SourceObject.Value);
			m_spell.RemoveAllTargets();
			for (int i = 0; i < m_TargetObjects.Length; i++)
			{
				FsmGameObject fsmGameObject = m_TargetObjects[i];
				if (!(fsmGameObject.Value == null) && !m_spell.IsTarget(fsmGameObject.Value))
				{
					m_spell.AddTarget(fsmGameObject.Value);
				}
			}
			Finish();
		}
	}
}
