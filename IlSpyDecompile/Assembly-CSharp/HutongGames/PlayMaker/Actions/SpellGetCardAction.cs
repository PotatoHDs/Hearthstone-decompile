using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Put a Spell's Source or Target Card into a GameObject variable.")]
	public class SpellGetCardAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public Which m_WhichCard;

		public FsmGameObject m_GameObject;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			m_SpellObject = null;
			m_WhichCard = Which.SOURCE;
			m_GameObject = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Card card = GetCard(m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellGetCardAction.OnEnter() - Card not found!");
				Finish();
				return;
			}
			if (!m_GameObject.IsNone)
			{
				m_GameObject.Value = card.gameObject;
			}
			Finish();
		}
	}
}
