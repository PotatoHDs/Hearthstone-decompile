using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F41 RID: 3905
	[ActionCategory("Pegasus")]
	[Tooltip("Use the spell to find the hero power")]
	public class GetHeroPowerAction : FsmStateAction
	{
		// Token: 0x0600AC8B RID: 44171 RVA: 0x0035D68A File Offset: 0x0035B88A
		public override void Reset()
		{
			this.m_HeroPowerGameObject = null;
		}

		// Token: 0x0600AC8C RID: 44172 RVA: 0x0035D694 File Offset: 0x0035B894
		public override void OnEnter()
		{
			Spell spell = base.Owner.gameObject.GetComponentInChildren<Spell>();
			if (spell == null)
			{
				spell = SceneUtils.FindComponentInThisOrParents<Spell>(base.Owner);
				if (spell == null)
				{
					base.Finish();
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
			if (!this.m_HeroPowerGameObject.IsNone)
			{
				this.m_HeroPowerGameObject.Value = gameObject;
			}
			base.Finish();
		}

		// Token: 0x0400935F RID: 37727
		public FsmGameObject m_HeroPowerGameObject;
	}
}
