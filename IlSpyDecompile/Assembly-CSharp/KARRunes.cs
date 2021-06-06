using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class KARRunes : MonoBehaviour
{
	public GameObject m_BookPageR;

	public GameObject m_BookPageL;

	public GameObject m_FloorRune;

	public Flipbook m_BookPageR_TurnedMesh;

	public Flipbook m_BookPageR_NotTurnedMesh;

	public Flipbook m_BookPageR_StaticMesh;

	public Flipbook m_BookPageL_StaticMesh;

	public Flipbook m_BookPageR_Glow;

	public Flipbook m_BookPageL_Glow;

	public Flipbook m_FloorPage;

	public Animator m_BookBooshAnim;

	public string m_BookBooshAnimState;

	public Animator m_BookGlowRAnim;

	public string m_BookGlowAnimRState;

	public Animator m_BookGlowLAnim;

	public string m_BookGlowAnimLState;

	public ParticleSystem m_FloorParticles;

	public Animator m_FloorGlowAnim;

	public string m_FloorGlowAnimState;

	public float m_bookBooshDelay = 0.5f;

	public float m_floorGlowDelay = 2.5f;

	public Animator m_LibraryBook;

	public string m_PageFlipRightAnimState;

	public string m_PageFlipLeftAnimState;

	public string m_BookShakeAnimState;

	public string m_RuneMatchSound;

	public List<string> m_PageFlipSounds;

	private bool m_isAnimating;

	private int m_leftIdx;

	private int m_rightIdx;

	private int m_floorRuneIdx;

	private void Start()
	{
		FlipBookPages();
		m_floorRuneIdx = Random.Range(0, 15);
		m_FloorPage.SetIndex(m_floorRuneIdx);
	}

	private void Update()
	{
		HandleHits();
	}

	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && IsOver(m_BookPageR) && !m_isAnimating)
		{
			FlipBookPages();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && IsOver(m_BookPageL) && !m_isAnimating)
		{
			FlipBookPages(isRight: false);
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && IsOver(m_FloorRune) && !m_isAnimating)
		{
			StartCoroutine(CheckRuneMatches());
		}
	}

	private void FlipBookPages(bool isRight = true)
	{
		m_isAnimating = true;
		m_LibraryBook.enabled = true;
		if (m_PageFlipSounds.Count > 0)
		{
			string text = m_PageFlipSounds[Random.Range(0, m_PageFlipSounds.Count - 1)];
			if (text != null)
			{
				SoundManager.Get().LoadAndPlay(text);
			}
		}
		if (isRight)
		{
			m_LibraryBook.Play(m_PageFlipRightAnimState, -1, 0f);
			m_BookPageL_StaticMesh.SetIndex(m_leftIdx);
			m_leftIdx = Random.Range(0, 15);
			m_BookPageR_TurnedMesh.SetIndex(m_leftIdx);
			m_BookPageL_Glow.SetIndex(m_leftIdx);
			m_BookPageR_NotTurnedMesh.SetIndex(m_rightIdx);
			for (m_rightIdx = Random.Range(0, 15); m_rightIdx == m_leftIdx; m_rightIdx = Random.Range(0, 15))
			{
			}
			m_BookPageR_StaticMesh.SetIndex(m_rightIdx);
			m_BookPageR_Glow.SetIndex(m_rightIdx);
		}
		else
		{
			m_LibraryBook.Play(m_PageFlipLeftAnimState, -1, 0f);
			m_BookPageR_StaticMesh.SetIndex(m_rightIdx);
			m_rightIdx = Random.Range(0, 15);
			m_BookPageR_NotTurnedMesh.SetIndex(m_rightIdx);
			m_BookPageR_Glow.SetIndex(m_rightIdx);
			m_BookPageR_TurnedMesh.SetIndex(m_leftIdx);
			for (m_leftIdx = Random.Range(0, 15); m_leftIdx == m_rightIdx; m_leftIdx = Random.Range(0, 15))
			{
			}
			m_BookPageL_StaticMesh.SetIndex(m_leftIdx);
			m_BookPageL_Glow.SetIndex(m_leftIdx);
		}
		m_isAnimating = false;
	}

	private IEnumerator CheckRuneMatches()
	{
		if ((m_floorRuneIdx == m_leftIdx || m_floorRuneIdx == m_rightIdx) && !m_isAnimating)
		{
			m_isAnimating = true;
			if (m_RuneMatchSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(m_RuneMatchSound);
			}
			if (m_BookGlowRAnim != null && m_BookGlowAnimRState != null && m_floorRuneIdx == m_rightIdx)
			{
				m_BookGlowRAnim.enabled = true;
				m_BookGlowRAnim.Play(m_BookGlowAnimRState, -1, 0f);
			}
			if (m_BookGlowLAnim != null && m_BookGlowAnimLState != null && m_floorRuneIdx == m_leftIdx)
			{
				m_BookGlowLAnim.enabled = true;
				m_BookGlowLAnim.Play(m_BookGlowAnimLState, -1, 0f);
			}
			if (m_BookBooshAnim != null && m_BookBooshAnimState != null)
			{
				yield return new WaitForSeconds(m_bookBooshDelay);
				m_BookBooshAnim.enabled = true;
				m_BookBooshAnim.Play(m_BookBooshAnimState, -1, 0f);
				m_LibraryBook.Play(m_BookShakeAnimState, -1, 0f);
			}
			if (m_FloorGlowAnim != null && m_FloorGlowAnimState != null)
			{
				yield return new WaitForSeconds(m_floorGlowDelay);
				m_FloorGlowAnim.enabled = true;
				m_FloorGlowAnim.Play(m_FloorGlowAnimState, -1, 0f);
				m_FloorParticles.Play();
			}
			yield return new WaitForSeconds(0.5f);
			m_floorRuneIdx = Random.Range(0, 15);
			m_FloorPage.SetIndex(m_floorRuneIdx);
			m_isAnimating = false;
		}
	}

	private bool IsOver(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		if (!InputUtil.IsPlayMakerMouseInputAllowed(go))
		{
			return false;
		}
		if (!UniversalInputManager.Get().InputIsOver(go))
		{
			return false;
		}
		return true;
	}
}
