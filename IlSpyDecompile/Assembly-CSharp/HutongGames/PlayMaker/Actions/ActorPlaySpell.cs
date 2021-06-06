namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Play a spell on the given Actor")]
	public class ActorPlaySpell : FsmStateAction
	{
		public SpellType m_Spell;

		public FsmGameObject m_actorObject;

		public override void Reset()
		{
			m_Spell = SpellType.NONE;
			m_actorObject = null;
		}

		public override void OnEnter()
		{
			if (!m_actorObject.IsNone)
			{
				Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(m_actorObject.Value);
				if (actor != null && m_Spell != 0)
				{
					actor.ActivateSpellBirthState(m_Spell);
				}
			}
			Finish();
		}

		public override void OnUpdate()
		{
		}
	}
}
