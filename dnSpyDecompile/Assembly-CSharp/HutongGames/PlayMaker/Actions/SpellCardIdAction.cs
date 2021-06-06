using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F7F RID: 3967
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on a Spell's Card's ID.")]
	public class SpellCardIdAction : SpellAction
	{
		// Token: 0x0600AD8B RID: 44427 RVA: 0x0036171D File Offset: 0x0035F91D
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600AD8C RID: 44428 RVA: 0x00361730 File Offset: 0x0035F930
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichCard = SpellAction.Which.SOURCE;
			this.m_Events = new FsmEvent[2];
			this.m_CardIds = new string[2];
		}

		// Token: 0x0600AD8D RID: 44429 RVA: 0x00361758 File Offset: 0x0035F958
		public override void OnEnter()
		{
			base.OnEnter();
			Card card = base.GetCard(this.m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellCardIdAction.OnEnter() - Card not found!", Array.Empty<object>());
				base.Finish();
				return;
			}
			string cardId = card.GetEntity().GetCardId();
			int indexMatchingCardId = base.GetIndexMatchingCardId(cardId, this.m_CardIds);
			if (indexMatchingCardId >= 0 && this.m_Events[indexMatchingCardId] != null)
			{
				base.Fsm.Event(this.m_Events[indexMatchingCardId]);
			}
			base.Finish();
		}

		// Token: 0x04009461 RID: 37985
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009462 RID: 37986
		[Tooltip("Which Card to check on the Spell.")]
		public SpellAction.Which m_WhichCard;

		// Token: 0x04009463 RID: 37987
		[RequiredField]
		[CompoundArray("Events", "Event", "Card Id")]
		public FsmEvent[] m_Events;

		// Token: 0x04009464 RID: 37988
		public string[] m_CardIds;
	}
}
