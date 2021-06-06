namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Get the Entity's game object from an Entity ID")]
	public class GetEntityActorAction : FsmStateAction
	{
		public FsmInt m_EntityId;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Stores the Entity's actor in this output variable.")]
		public FsmGameObject m_EntityActor;

		public override void Reset()
		{
			m_EntityActor = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_EntityId.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No entity ID was passed", this);
				Finish();
				return;
			}
			if (m_EntityActor.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No variable hooked up to store Entity's actor!", this);
				Finish();
				return;
			}
			Entity entity = GameState.Get().GetEntity(m_EntityId.Value);
			if (entity == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find entity with id {1}", this, m_EntityId.Value);
				Finish();
				return;
			}
			Card card = entity.GetCard();
			if (card == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find card with entity id {1}", this, m_EntityId.Value);
				Finish();
				return;
			}
			Actor actor = card.GetActor();
			if (actor == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find actor with entity id {1}", this, m_EntityId.Value);
				Finish();
			}
			else
			{
				m_EntityActor.Value = actor.gameObject;
				Finish();
			}
		}
	}
}
