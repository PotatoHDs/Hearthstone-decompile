using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Set the actor's card's input as enabled or disabled.")]
	public class SetCardInputEnabledAction : ActorAction
	{
		public FsmOwnerDefault m_ActorObject;

		public FsmBool m_InputEnabled;

		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_ActorObject);
		}

		public override void Reset()
		{
			m_ActorObject = null;
			m_InputEnabled = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			Card card = m_actor.GetCard();
			if (card == null)
			{
				Debug.LogError($"SetCardInputEnabledAction.OnEnter() - No card for actor {m_actor}");
				Finish();
			}
			else
			{
				card.SetInputEnabled(m_InputEnabled.Value);
				Finish();
			}
		}
	}
}
