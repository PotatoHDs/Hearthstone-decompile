using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F84 RID: 3972
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on a Spell's Card's Type.")]
	public class SpellCardTypeAction : SpellAction
	{
		// Token: 0x0600ADA7 RID: 44455 RVA: 0x00361DFF File Offset: 0x0035FFFF
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADA8 RID: 44456 RVA: 0x00361E12 File Offset: 0x00360012
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichCard = SpellAction.Which.SOURCE;
			this.m_MinionEvent = null;
			this.m_HeroEvent = null;
			this.m_HeroPowerEvent = null;
			this.m_WeaponEvent = null;
			this.m_SpellEvent = null;
		}

		// Token: 0x0600ADA9 RID: 44457 RVA: 0x00361E48 File Offset: 0x00360048
		public override void OnEnter()
		{
			base.OnEnter();
			Card card = base.GetCard(this.m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellCardTypeAction.OnEnter() - Card not found!", Array.Empty<object>());
				base.Finish();
				return;
			}
			TAG_CARDTYPE cardType = card.GetEntity().GetCardType();
			switch (cardType)
			{
			case TAG_CARDTYPE.HERO:
				base.Fsm.Event(this.m_HeroEvent);
				goto IL_E4;
			case TAG_CARDTYPE.MINION:
				base.Fsm.Event(this.m_MinionEvent);
				goto IL_E4;
			case TAG_CARDTYPE.SPELL:
				base.Fsm.Event(this.m_SpellEvent);
				goto IL_E4;
			case TAG_CARDTYPE.WEAPON:
				base.Fsm.Event(this.m_WeaponEvent);
				goto IL_E4;
			case TAG_CARDTYPE.HERO_POWER:
				base.Fsm.Event(this.m_HeroPowerEvent);
				goto IL_E4;
			}
			Error.AddDevFatal("SpellCardTypeAction.OnEnter() - unknown type {0} on {1}", new object[]
			{
				cardType,
				card
			});
			IL_E4:
			base.Finish();
		}

		// Token: 0x04009486 RID: 38022
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009487 RID: 38023
		public SpellAction.Which m_WhichCard;

		// Token: 0x04009488 RID: 38024
		public FsmEvent m_MinionEvent;

		// Token: 0x04009489 RID: 38025
		public FsmEvent m_HeroEvent;

		// Token: 0x0400948A RID: 38026
		public FsmEvent m_HeroPowerEvent;

		// Token: 0x0400948B RID: 38027
		public FsmEvent m_WeaponEvent;

		// Token: 0x0400948C RID: 38028
		public FsmEvent m_SpellEvent;
	}
}
