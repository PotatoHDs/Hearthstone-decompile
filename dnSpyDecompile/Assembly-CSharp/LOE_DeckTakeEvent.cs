using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000388 RID: 904
[CustomEditClass]
public class LOE_DeckTakeEvent : MonoBehaviour
{
	// Token: 0x0600347B RID: 13435 RVA: 0x0010C588 File Offset: 0x0010A788
	private void Start()
	{
		CardBackManager.Get().SetCardBackTexture(this.m_friendlyDeckRenderer, 0, CardBackManager.CardBackSlot.FRIENDLY);
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x0010C59C File Offset: 0x0010A79C
	public IEnumerator PlayTakeDeckAnim()
	{
		this.m_animIsPlaying = true;
		this.m_takeDeckAnimator.enabled = true;
		this.m_takeDeckAnimator.Play(this.m_takeDeckAnimName);
		if (!string.IsNullOrEmpty(this.m_takeDeckSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_takeDeckSoundPrefab);
		}
		yield return new WaitForEndOfFrame();
		float length = this.m_takeDeckAnimator.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(length);
		this.m_animIsPlaying = false;
		yield break;
	}

	// Token: 0x0600347D RID: 13437 RVA: 0x0010C5AB File Offset: 0x0010A7AB
	public IEnumerator PlayReplacementDeckAnim()
	{
		this.m_animIsPlaying = true;
		this.m_replacementDeckAnimator.enabled = true;
		this.m_replacementDeckAnimator.Play(this.m_replacementDeckAnimName);
		if (!string.IsNullOrEmpty(this.m_replacementDeckSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_replacementDeckSoundPrefab);
		}
		yield return new WaitForEndOfFrame();
		float length = this.m_replacementDeckAnimator.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(length);
		this.m_animIsPlaying = false;
		yield break;
	}

	// Token: 0x0600347E RID: 13438 RVA: 0x0010C5BA File Offset: 0x0010A7BA
	public bool AnimIsPlaying()
	{
		return this.m_animIsPlaying;
	}

	// Token: 0x04001CB0 RID: 7344
	public Renderer m_friendlyDeckRenderer;

	// Token: 0x04001CB1 RID: 7345
	public Animator m_takeDeckAnimator;

	// Token: 0x04001CB2 RID: 7346
	public string m_takeDeckAnimName = "LOE_TakeDeck";

	// Token: 0x04001CB3 RID: 7347
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_takeDeckSoundPrefab;

	// Token: 0x04001CB4 RID: 7348
	public Animator m_replacementDeckAnimator;

	// Token: 0x04001CB5 RID: 7349
	public string m_replacementDeckAnimName = "CardsToPlayerDeck";

	// Token: 0x04001CB6 RID: 7350
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_replacementDeckSoundPrefab;

	// Token: 0x04001CB7 RID: 7351
	private bool m_animIsPlaying;
}
