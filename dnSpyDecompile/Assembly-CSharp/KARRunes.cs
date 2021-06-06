using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AE RID: 174
[CustomEditClass]
public class KARRunes : MonoBehaviour
{
	// Token: 0x06000AF3 RID: 2803 RVA: 0x00040E34 File Offset: 0x0003F034
	private void Start()
	{
		this.FlipBookPages(true);
		this.m_floorRuneIdx = UnityEngine.Random.Range(0, 15);
		this.m_FloorPage.SetIndex(this.m_floorRuneIdx);
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00040E5C File Offset: 0x0003F05C
	private void Update()
	{
		this.HandleHits();
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00040E64 File Offset: 0x0003F064
	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_BookPageR) && !this.m_isAnimating)
		{
			this.FlipBookPages(true);
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_BookPageL) && !this.m_isAnimating)
		{
			this.FlipBookPages(false);
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_FloorRune) && !this.m_isAnimating)
		{
			base.StartCoroutine(this.CheckRuneMatches());
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00040EF8 File Offset: 0x0003F0F8
	private void FlipBookPages(bool isRight = true)
	{
		this.m_isAnimating = true;
		this.m_LibraryBook.enabled = true;
		if (this.m_PageFlipSounds.Count > 0)
		{
			string text = this.m_PageFlipSounds[UnityEngine.Random.Range(0, this.m_PageFlipSounds.Count - 1)];
			if (text != null)
			{
				SoundManager.Get().LoadAndPlay(text);
			}
		}
		if (isRight)
		{
			this.m_LibraryBook.Play(this.m_PageFlipRightAnimState, -1, 0f);
			this.m_BookPageL_StaticMesh.SetIndex(this.m_leftIdx);
			this.m_leftIdx = UnityEngine.Random.Range(0, 15);
			this.m_BookPageR_TurnedMesh.SetIndex(this.m_leftIdx);
			this.m_BookPageL_Glow.SetIndex(this.m_leftIdx);
			this.m_BookPageR_NotTurnedMesh.SetIndex(this.m_rightIdx);
			this.m_rightIdx = UnityEngine.Random.Range(0, 15);
			while (this.m_rightIdx == this.m_leftIdx)
			{
				this.m_rightIdx = UnityEngine.Random.Range(0, 15);
			}
			this.m_BookPageR_StaticMesh.SetIndex(this.m_rightIdx);
			this.m_BookPageR_Glow.SetIndex(this.m_rightIdx);
		}
		else
		{
			this.m_LibraryBook.Play(this.m_PageFlipLeftAnimState, -1, 0f);
			this.m_BookPageR_StaticMesh.SetIndex(this.m_rightIdx);
			this.m_rightIdx = UnityEngine.Random.Range(0, 15);
			this.m_BookPageR_NotTurnedMesh.SetIndex(this.m_rightIdx);
			this.m_BookPageR_Glow.SetIndex(this.m_rightIdx);
			this.m_BookPageR_TurnedMesh.SetIndex(this.m_leftIdx);
			this.m_leftIdx = UnityEngine.Random.Range(0, 15);
			while (this.m_leftIdx == this.m_rightIdx)
			{
				this.m_leftIdx = UnityEngine.Random.Range(0, 15);
			}
			this.m_BookPageL_StaticMesh.SetIndex(this.m_leftIdx);
			this.m_BookPageL_Glow.SetIndex(this.m_leftIdx);
		}
		this.m_isAnimating = false;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x000410D8 File Offset: 0x0003F2D8
	private IEnumerator CheckRuneMatches()
	{
		if ((this.m_floorRuneIdx == this.m_leftIdx || this.m_floorRuneIdx == this.m_rightIdx) && !this.m_isAnimating)
		{
			this.m_isAnimating = true;
			if (this.m_RuneMatchSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(this.m_RuneMatchSound);
			}
			if (this.m_BookGlowRAnim != null && this.m_BookGlowAnimRState != null && this.m_floorRuneIdx == this.m_rightIdx)
			{
				this.m_BookGlowRAnim.enabled = true;
				this.m_BookGlowRAnim.Play(this.m_BookGlowAnimRState, -1, 0f);
			}
			if (this.m_BookGlowLAnim != null && this.m_BookGlowAnimLState != null && this.m_floorRuneIdx == this.m_leftIdx)
			{
				this.m_BookGlowLAnim.enabled = true;
				this.m_BookGlowLAnim.Play(this.m_BookGlowAnimLState, -1, 0f);
			}
			if (this.m_BookBooshAnim != null && this.m_BookBooshAnimState != null)
			{
				yield return new WaitForSeconds(this.m_bookBooshDelay);
				this.m_BookBooshAnim.enabled = true;
				this.m_BookBooshAnim.Play(this.m_BookBooshAnimState, -1, 0f);
				this.m_LibraryBook.Play(this.m_BookShakeAnimState, -1, 0f);
			}
			if (this.m_FloorGlowAnim != null && this.m_FloorGlowAnimState != null)
			{
				yield return new WaitForSeconds(this.m_floorGlowDelay);
				this.m_FloorGlowAnim.enabled = true;
				this.m_FloorGlowAnim.Play(this.m_FloorGlowAnimState, -1, 0f);
				this.m_FloorParticles.Play();
			}
			yield return new WaitForSeconds(0.5f);
			this.m_floorRuneIdx = UnityEngine.Random.Range(0, 15);
			this.m_FloorPage.SetIndex(this.m_floorRuneIdx);
			this.m_isAnimating = false;
		}
		yield break;
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x040006F3 RID: 1779
	public GameObject m_BookPageR;

	// Token: 0x040006F4 RID: 1780
	public GameObject m_BookPageL;

	// Token: 0x040006F5 RID: 1781
	public GameObject m_FloorRune;

	// Token: 0x040006F6 RID: 1782
	public Flipbook m_BookPageR_TurnedMesh;

	// Token: 0x040006F7 RID: 1783
	public Flipbook m_BookPageR_NotTurnedMesh;

	// Token: 0x040006F8 RID: 1784
	public Flipbook m_BookPageR_StaticMesh;

	// Token: 0x040006F9 RID: 1785
	public Flipbook m_BookPageL_StaticMesh;

	// Token: 0x040006FA RID: 1786
	public Flipbook m_BookPageR_Glow;

	// Token: 0x040006FB RID: 1787
	public Flipbook m_BookPageL_Glow;

	// Token: 0x040006FC RID: 1788
	public Flipbook m_FloorPage;

	// Token: 0x040006FD RID: 1789
	public Animator m_BookBooshAnim;

	// Token: 0x040006FE RID: 1790
	public string m_BookBooshAnimState;

	// Token: 0x040006FF RID: 1791
	public Animator m_BookGlowRAnim;

	// Token: 0x04000700 RID: 1792
	public string m_BookGlowAnimRState;

	// Token: 0x04000701 RID: 1793
	public Animator m_BookGlowLAnim;

	// Token: 0x04000702 RID: 1794
	public string m_BookGlowAnimLState;

	// Token: 0x04000703 RID: 1795
	public ParticleSystem m_FloorParticles;

	// Token: 0x04000704 RID: 1796
	public Animator m_FloorGlowAnim;

	// Token: 0x04000705 RID: 1797
	public string m_FloorGlowAnimState;

	// Token: 0x04000706 RID: 1798
	public float m_bookBooshDelay = 0.5f;

	// Token: 0x04000707 RID: 1799
	public float m_floorGlowDelay = 2.5f;

	// Token: 0x04000708 RID: 1800
	public Animator m_LibraryBook;

	// Token: 0x04000709 RID: 1801
	public string m_PageFlipRightAnimState;

	// Token: 0x0400070A RID: 1802
	public string m_PageFlipLeftAnimState;

	// Token: 0x0400070B RID: 1803
	public string m_BookShakeAnimState;

	// Token: 0x0400070C RID: 1804
	public string m_RuneMatchSound;

	// Token: 0x0400070D RID: 1805
	public List<string> m_PageFlipSounds;

	// Token: 0x0400070E RID: 1806
	private bool m_isAnimating;

	// Token: 0x0400070F RID: 1807
	private int m_leftIdx;

	// Token: 0x04000710 RID: 1808
	private int m_rightIdx;

	// Token: 0x04000711 RID: 1809
	private int m_floorRuneIdx;
}
