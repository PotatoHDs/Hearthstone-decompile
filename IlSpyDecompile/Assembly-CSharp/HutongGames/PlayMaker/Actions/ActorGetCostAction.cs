using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Put an Actor's cost data into variables.")]
	public class ActorGetCostAction : ActorAction
	{
		public FsmOwnerDefault m_ActorObject;

		public FsmGameObject m_ManaGem;

		public FsmGameObject m_UberText;

		public FsmInt m_Cost;

		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_ActorObject);
		}

		public override void Reset()
		{
			m_ActorObject = null;
			m_ManaGem = new FsmGameObject
			{
				UseVariable = true
			};
			m_UberText = new FsmGameObject
			{
				UseVariable = true
			};
			m_Cost = new FsmInt
			{
				UseVariable = true
			};
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			if (!m_ManaGem.IsNone)
			{
				m_ManaGem.Value = m_actor.m_manaObject;
			}
			if (!m_UberText.IsNone)
			{
				m_UberText.Value = m_actor.GetCostTextObject();
			}
			if (!m_Cost.IsNone)
			{
				Entity entity = m_actor.GetEntity();
				if (entity != null)
				{
					m_Cost.Value = entity.GetCost();
				}
				else
				{
					EntityDef entityDef = m_actor.GetEntityDef();
					if (entityDef != null)
					{
						m_Cost.Value = entityDef.GetCost();
					}
				}
			}
			Finish();
		}
	}
}
