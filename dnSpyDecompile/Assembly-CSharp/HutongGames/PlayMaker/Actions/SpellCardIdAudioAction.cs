using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F80 RID: 3968
	[ActionCategory("Pegasus")]
	[Tooltip("INTERNAL USE ONLY. Do not put this on your FSMs.")]
	public abstract class SpellCardIdAudioAction : SpellAction
	{
		// Token: 0x0600AD8F RID: 44431 RVA: 0x003617D8 File Offset: 0x0035F9D8
		protected AudioSource GetAudioSource(FsmOwnerDefault ownerDefault)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(ownerDefault);
			if (ownerDefaultTarget == null)
			{
				return null;
			}
			return ownerDefaultTarget.GetComponent<AudioSource>();
		}

		// Token: 0x0600AD90 RID: 44432 RVA: 0x00361804 File Offset: 0x0035FA04
		protected SoundDef GetClipMatchingCardId(SpellAction.Which whichCard, string[] cardIds, SoundDef[] clips, SoundDef defaultClip)
		{
			Card card = base.GetCard(whichCard);
			if (card == null)
			{
				Debug.LogWarningFormat("SpellCardIdAudioAction.GetClipMatchingCardId() - could not find {0} card", new object[]
				{
					whichCard
				});
				return null;
			}
			string cardId = card.GetEntity().GetCardId();
			int indexMatchingCardId = base.GetIndexMatchingCardId(cardId, cardIds);
			if (indexMatchingCardId < 0)
			{
				return defaultClip;
			}
			return clips[indexMatchingCardId];
		}

		// Token: 0x0600AD91 RID: 44433 RVA: 0x0036185C File Offset: 0x0035FA5C
		protected AudioSource GetSourceMatchingCardId(SpellAction.Which whichCard, string[] cardIds, FsmGameObject[] sources, FsmGameObject defaultSource)
		{
			Card card = base.GetCard(whichCard);
			if (card == null)
			{
				Debug.LogWarningFormat("SpellCardIdAudioAction.GetSourceMatchingCardId() - could not find {0} card", new object[]
				{
					whichCard
				});
				return null;
			}
			string cardId = card.GetEntity().GetCardId();
			int indexMatchingCardId = base.GetIndexMatchingCardId(cardId, cardIds);
			if (indexMatchingCardId < 0)
			{
				return defaultSource.Value.GetComponent<AudioSource>();
			}
			return sources[indexMatchingCardId].Value.GetComponent<AudioSource>();
		}
	}
}
