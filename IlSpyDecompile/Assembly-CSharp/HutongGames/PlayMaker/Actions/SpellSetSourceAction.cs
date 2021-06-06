using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the source for a Spell.")]
	public class SpellSetSourceAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public FsmGameObject m_SourceObject;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (!(m_spell == null))
			{
				GameObject value = m_SourceObject.Value;
				m_spell.SetSource(value);
				Finish();
			}
		}
	}
}
