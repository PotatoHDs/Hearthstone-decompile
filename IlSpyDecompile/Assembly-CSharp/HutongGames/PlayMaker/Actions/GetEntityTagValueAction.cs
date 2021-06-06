using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Stores the value of an Entity's tag in passed int.")]
	public class GetEntityTagValueAction : SpellAction
	{
		public FsmOwnerDefault m_spellObject;

		public Which m_whichEntity;

		public GAME_TAG m_tagToCheck;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmInt m_TagValue;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_spellObject);
		}

		public override void Reset()
		{
			m_TagValue = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_TagValue == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store tag value!", this);
				Finish();
				return;
			}
			Entity entity = GetEntity(m_whichEntity);
			if (entity == null)
			{
				global::Log.All.PrintError("{0}.OnEnter() - FAILED to find relevant entity: \"{1}\"", this, m_whichEntity);
				Finish();
			}
			else
			{
				m_TagValue.Value = entity.GetTag(m_tagToCheck);
				Finish();
			}
		}
	}
}
