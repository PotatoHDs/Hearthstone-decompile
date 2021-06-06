using System.Collections;
using UnityEngine;

[CustomEditClass]
public class LOE_DeckTakeEvent : MonoBehaviour
{
	public Renderer m_friendlyDeckRenderer;

	public Animator m_takeDeckAnimator;

	public string m_takeDeckAnimName = "LOE_TakeDeck";

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_takeDeckSoundPrefab;

	public Animator m_replacementDeckAnimator;

	public string m_replacementDeckAnimName = "CardsToPlayerDeck";

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_replacementDeckSoundPrefab;

	private bool m_animIsPlaying;

	private void Start()
	{
		CardBackManager.Get().SetCardBackTexture(m_friendlyDeckRenderer, 0, CardBackManager.CardBackSlot.FRIENDLY);
	}

	public IEnumerator PlayTakeDeckAnim()
	{
		m_animIsPlaying = true;
		m_takeDeckAnimator.enabled = true;
		m_takeDeckAnimator.Play(m_takeDeckAnimName);
		if (!string.IsNullOrEmpty(m_takeDeckSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_takeDeckSoundPrefab);
		}
		yield return new WaitForEndOfFrame();
		float length = m_takeDeckAnimator.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(length);
		m_animIsPlaying = false;
	}

	public IEnumerator PlayReplacementDeckAnim()
	{
		m_animIsPlaying = true;
		m_replacementDeckAnimator.enabled = true;
		m_replacementDeckAnimator.Play(m_replacementDeckAnimName);
		if (!string.IsNullOrEmpty(m_replacementDeckSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_replacementDeckSoundPrefab);
		}
		yield return new WaitForEndOfFrame();
		float length = m_replacementDeckAnimator.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(length);
		m_animIsPlaying = false;
	}

	public bool AnimIsPlaying()
	{
		return m_animIsPlaying;
	}
}
