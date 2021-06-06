using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Use the spell to find the hero power")]
	public class GetHeroPowerAction : FsmStateAction
	{
		public FsmGameObject m_HeroPowerGameObject;

		public override void Reset()
		{
			m_HeroPowerGameObject = null;
		}

		public override void OnEnter()
		{
			Spell spell = base.Owner.gameObject.GetComponentInChildren<Spell>();
			if (spell == null)
			{
				spell = SceneUtils.FindComponentInThisOrParents<Spell>(base.Owner);
				if (spell == null)
				{
					Finish();
					return;
				}
			}
			if (spell == null)
			{
				Debug.LogWarning("GetHeroPowerAction: spell is null!");
				return;
			}
			Card card = spell.GetSourceCard();
			if (card == null)
			{
				card = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner).GetCard();
				if (card == null)
				{
					Debug.LogWarning("GetHeroPowerAction: card is null!");
					return;
				}
			}
			Card heroPowerCard = card.GetHeroPowerCard();
			if (heroPowerCard == null)
			{
				Debug.LogWarning("GetHeroPowerAction: heroPowerCard is null!");
				return;
			}
			Actor actor = heroPowerCard.GetActor();
			if (actor == null)
			{
				Debug.LogWarning("GetHeroPowerAction: heroPowerCardActor is null!");
				return;
			}
			GameObject gameObject = actor.gameObject;
			if (!m_HeroPowerGameObject.IsNone)
			{
				m_HeroPowerGameObject.Value = gameObject;
			}
			Finish();
		}
	}
}
