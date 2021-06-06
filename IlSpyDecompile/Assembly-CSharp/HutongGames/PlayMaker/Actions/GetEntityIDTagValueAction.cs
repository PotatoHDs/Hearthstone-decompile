namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Stores the specified tag value for Entity with passed Entity ID in passed int.")]
	public class GetEntityIDTagValueAction : FsmStateAction
	{
		public FsmInt m_entityID;

		public GAME_TAG m_tagToCheck;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmInt m_TagValue;

		public override void Reset()
		{
			m_TagValue = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_entityID.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No entity ID was passed!", this);
				Finish();
				return;
			}
			if (m_TagValue.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No variable hooked up to store tag value!", this);
				Finish();
				return;
			}
			Entity entity = GameState.Get().GetEntity(m_entityID.Value);
			if (entity == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find entity with id {1}", this, m_entityID.Value);
				Finish();
			}
			else
			{
				m_TagValue = entity.GetTag(m_tagToCheck);
				Finish();
			}
		}
	}
}
