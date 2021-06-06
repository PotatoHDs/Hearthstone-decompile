using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Initialize a spell state, setting variables that reference the parent actor and its contents.")]
	public class SpellInitActorAndEntityVariables : SpellInitActorVariables
	{
		[UIHint(UIHint.Variable)]
		public FsmInt m_entityID;

		public override void Reset()
		{
			base.Reset();
			if (m_entityID != null)
			{
				m_entityID.UseVariable = true;
			}
		}

		public override void OnEnter()
		{
			Actor actor = GetActor();
			if (actor == null)
			{
				Finish();
				return;
			}
			GameObject gameObject = actor.gameObject;
			if (!m_actorObject.IsNone)
			{
				m_actorObject.Value = gameObject;
			}
			if (!m_rootObject.IsNone)
			{
				m_rootObject.Value = actor.GetRootObject();
			}
			if (!m_rootObjectMesh.IsNone)
			{
				m_rootObjectMesh.Value = actor.GetMeshRenderer().gameObject;
			}
			if (!m_entityID.IsNone)
			{
				if (actor.GetEntity() == null)
				{
					global::Log.Spells.PrintError("{0}.OnEnter(): An Entity ID FSM Variable was hooked up but actor {1} doesn't have an entity!", this, actor);
				}
				else
				{
					m_entityID.Value = actor.GetEntity().GetEntityId();
				}
			}
			Finish();
		}
	}
}
