using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F8F RID: 3983
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on the player side of a Spell.")]
	public class SpellPlayerSideEventAction : SpellAction
	{
		// Token: 0x0600ADD1 RID: 44497 RVA: 0x003626FF File Offset: 0x003608FF
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADD2 RID: 44498 RVA: 0x00362712 File Offset: 0x00360912
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichCard = SpellAction.Which.SOURCE;
			this.m_FriendlyEvent = null;
			this.m_OpponentEvent = null;
			this.m_NeutralEvent = null;
		}

		// Token: 0x0600ADD3 RID: 44499 RVA: 0x00362738 File Offset: 0x00360938
		public override void OnEnter()
		{
			base.OnEnter();
			Card card = base.GetCard(this.m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellPlayerSideEventAction.OnEnter() - Card not found for spell {0}", new object[]
				{
					base.GetSpell()
				});
				base.Finish();
				return;
			}
			Player.Side controllerSide = card.GetEntity().GetControllerSide();
			if (controllerSide != Player.Side.FRIENDLY)
			{
				if (controllerSide != Player.Side.OPPOSING)
				{
					base.Fsm.Event(this.m_NeutralEvent);
				}
				else
				{
					base.Fsm.Event(this.m_OpponentEvent);
				}
			}
			else
			{
				base.Fsm.Event(this.m_FriendlyEvent);
			}
			base.Finish();
		}

		// Token: 0x040094A8 RID: 38056
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x040094A9 RID: 38057
		public SpellAction.Which m_WhichCard;

		// Token: 0x040094AA RID: 38058
		public FsmEvent m_FriendlyEvent;

		// Token: 0x040094AB RID: 38059
		public FsmEvent m_OpponentEvent;

		// Token: 0x040094AC RID: 38060
		public FsmEvent m_NeutralEvent;
	}
}
