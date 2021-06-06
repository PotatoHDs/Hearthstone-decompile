using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Call UpdateAllComponents on an actor, to refresh it's textures, health icon, attack icon, etc.")]
	public class ActorUpdateAllComponentsAction : ActorAction
	{
		public FsmGameObject m_ActorObject;

		protected override GameObject GetActorOwner()
		{
			return m_ActorObject.Value;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			m_actor.UpdateAllComponents();
			Finish();
		}
	}
}
