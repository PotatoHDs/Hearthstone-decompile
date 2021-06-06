using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Put a Spell's Source or Target Actor into a GameObject variable.")]
	public class SpellGetActorAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public Which m_WhichActor;

		public FsmGameObject m_GameObject;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			m_SpellObject = null;
			m_WhichActor = Which.SOURCE;
			m_GameObject = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Actor actor = GetActor(m_WhichActor);
			if (actor == null)
			{
				Error.AddDevFatal("SpellGetActorAction.OnEnter() - Actor not found!");
				Finish();
				return;
			}
			if (!m_GameObject.IsNone)
			{
				m_GameObject.Value = actor.gameObject;
			}
			Finish();
		}
	}
}
