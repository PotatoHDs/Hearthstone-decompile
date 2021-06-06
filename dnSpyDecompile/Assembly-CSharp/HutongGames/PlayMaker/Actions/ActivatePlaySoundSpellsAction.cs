using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F06 RID: 3846
	[ActionCategory("Pegasus")]
	[Tooltip("Cause the Play Sound Spells to fire on the selected card")]
	public class ActivatePlaySoundSpellsAction : SpellAction
	{
		// Token: 0x0600AB8E RID: 43918 RVA: 0x00359316 File Offset: 0x00357516
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600AB8F RID: 43919 RVA: 0x0035932C File Offset: 0x0035752C
		public override void OnEnter()
		{
			base.OnEnter();
			Card card = base.GetCard(this.m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("ActivatePlaySoundSpellsAction.OnEnter() - Card not found!", Array.Empty<object>());
				base.Finish();
				return;
			}
			if (card != null)
			{
				card.ActivateSoundSpellList(card.GetPlaySoundSpells(0, true));
			}
			base.Finish();
		}

		// Token: 0x0400924E RID: 37454
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x0400924F RID: 37455
		public SpellAction.Which m_WhichCard;
	}
}
