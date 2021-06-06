using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Cause the Play Sound Spells to fire on the selected card")]
	public class ActivatePlaySoundSpellsAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public Which m_WhichCard;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Card card = GetCard(m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("ActivatePlaySoundSpellsAction.OnEnter() - Card not found!");
				Finish();
				return;
			}
			if (card != null)
			{
				card.ActivateSoundSpellList(card.GetPlaySoundSpells(0));
			}
			Finish();
		}
	}
}
