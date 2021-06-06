using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("INTERNAL USE ONLY. Do not put this on your FSMs.")]
	public abstract class SpellCardIdAudioAction : SpellAction
	{
		protected AudioSource GetAudioSource(FsmOwnerDefault ownerDefault)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(ownerDefault);
			if (ownerDefaultTarget == null)
			{
				return null;
			}
			return ownerDefaultTarget.GetComponent<AudioSource>();
		}

		protected SoundDef GetClipMatchingCardId(Which whichCard, string[] cardIds, SoundDef[] clips, SoundDef defaultClip)
		{
			Card card = GetCard(whichCard);
			if (card == null)
			{
				Debug.LogWarningFormat("SpellCardIdAudioAction.GetClipMatchingCardId() - could not find {0} card", whichCard);
				return null;
			}
			string cardId = card.GetEntity().GetCardId();
			int indexMatchingCardId = GetIndexMatchingCardId(cardId, cardIds);
			if (indexMatchingCardId < 0)
			{
				return defaultClip;
			}
			return clips[indexMatchingCardId];
		}

		protected AudioSource GetSourceMatchingCardId(Which whichCard, string[] cardIds, FsmGameObject[] sources, FsmGameObject defaultSource)
		{
			Card card = GetCard(whichCard);
			if (card == null)
			{
				Debug.LogWarningFormat("SpellCardIdAudioAction.GetSourceMatchingCardId() - could not find {0} card", whichCard);
				return null;
			}
			string cardId = card.GetEntity().GetCardId();
			int indexMatchingCardId = GetIndexMatchingCardId(cardId, cardIds);
			if (indexMatchingCardId < 0)
			{
				return defaultSource.Value.GetComponent<AudioSource>();
			}
			return sources[indexMatchingCardId].Value.GetComponent<AudioSource>();
		}
	}
}
