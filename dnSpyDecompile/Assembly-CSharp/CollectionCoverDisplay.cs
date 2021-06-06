using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class CollectionCoverDisplay : PegUIElement
{
	// Token: 0x06000EB5 RID: 3765 RVA: 0x00052A0F File Offset: 0x00050C0F
	protected override void Awake()
	{
		base.Awake();
		this.m_boxCollider = base.transform.GetComponent<BoxCollider>();
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x00052A28 File Offset: 0x00050C28
	public bool IsAnimating()
	{
		return this.m_isAnimating;
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x00052A30 File Offset: 0x00050C30
	public void Open(CollectionCoverDisplay.DelOnOpened callback)
	{
		if (this.m_bookCover.transform.localEulerAngles.z == this.BOOK_COVER_FULLY_OPEN_Z_ROTATION)
		{
			return;
		}
		this.EnableCollider(false);
		this.SetIsAnimating(true);
		this.AnimateLatchOpening();
		this.AnimateCoverOpening(callback);
		SoundManager.Get().LoadAndPlay("collection_manager_book_open.prefab:e32dc00de806ee1478b67810b89947bb");
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x00052A8A File Offset: 0x00050C8A
	public void SetOpenState()
	{
		if (!this.m_bookCover.activeSelf)
		{
			return;
		}
		this.EnableCollider(false);
		this.SetIsAnimating(false);
		this.m_bookCover.SetActive(false);
		this.m_bookCoverLatchJoint.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x00052AC8 File Offset: 0x00050CC8
	public void Close()
	{
		this.m_bookCover.SetActive(true);
		if (this.m_bookCover.transform.localEulerAngles.z == this.BOOK_COVER_FULLY_CLOSED_Z_ROTATION)
		{
			return;
		}
		this.SetIsAnimating(true);
		this.AnimateCoverClosing();
		SoundManager.Get().LoadAndPlay("collection_manager_book_close.prefab:872608cda202ca440aa60cd0918be9ad");
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x00052B20 File Offset: 0x00050D20
	private void SetIsAnimating(bool animating)
	{
		this.m_isAnimating = animating;
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x00052B29 File Offset: 0x00050D29
	private void EnableCollider(bool enabled)
	{
		this.SetEnabled(enabled, false);
		this.m_boxCollider.enabled = enabled;
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00052B40 File Offset: 0x00050D40
	private void AnimateLatchOpening()
	{
		this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].speed = this.LATCH_OPEN_ANIM_SPEED;
		if (this.m_bookCoverLatch.GetComponent<Animation>().IsPlaying(this.LATCH_OPEN_ANIM_NAME))
		{
			base.StopCoroutine(this.CRACK_LATCH_OPEN_ANIM_COROUTINE);
		}
		else
		{
			this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].time = 0f;
			this.m_bookCoverLatch.GetComponent<Animation>().Play(this.LATCH_OPEN_ANIM_NAME);
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0,
			"delay",
			this.LATCH_FADE_DELAY,
			"time",
			this.LATCH_FADE_TIME,
			"easeType",
			iTween.EaseType.linear,
			"oncomplete",
			"OnLatchOpened",
			"oncompletetarget",
			base.gameObject
		});
		iTween.FadeTo(this.m_bookCoverLatchJoint, args);
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x00052C58 File Offset: 0x00050E58
	private void AnimateCoverOpening(CollectionCoverDisplay.DelOnOpened callback)
	{
		this.m_bookCoverLatchJoint.GetComponent<Renderer>().SetMaterial(this.m_latchFadeMaterial);
		Vector3 localEulerAngles = this.m_bookCover.transform.localEulerAngles;
		localEulerAngles.z = this.BOOK_COVER_FULLY_OPEN_Z_ROTATION;
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			localEulerAngles,
			"isLocal",
			true,
			"time",
			this.BOOK_COVER_FULL_ANIM_TIME,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"OnCoverOpened",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			callback,
			"name",
			"rotation"
		});
		iTween.StopByName(this.m_bookCover.gameObject, "rotation");
		iTween.RotateTo(this.m_bookCover.gameObject, args);
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00052D5C File Offset: 0x00050F5C
	private void AnimateCoverClosing()
	{
		Vector3 localEulerAngles = this.m_bookCover.transform.localEulerAngles;
		localEulerAngles.z = this.BOOK_COVER_FULLY_CLOSED_Z_ROTATION;
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			localEulerAngles,
			"isLocal",
			true,
			"time",
			this.BOOK_COVER_FULL_ANIM_TIME,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"AnimateLatchClosing",
			"oncompletetarget",
			base.gameObject,
			"name",
			"rotation"
		});
		iTween.StopByName(this.m_bookCover.gameObject, "rotation");
		iTween.RotateTo(this.m_bookCover.gameObject, args);
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00052E3C File Offset: 0x0005103C
	private void AnimateLatchClosing()
	{
		this.m_bookCoverLatchJoint.GetComponent<Renderer>().enabled = true;
		this.m_bookCoverLatchJoint.GetComponent<Renderer>().SetMaterial(this.m_latchFadeMaterial);
		this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].time = this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].length;
		this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].speed = -this.LATCH_OPEN_ANIM_SPEED * 2f;
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1,
			"time",
			this.LATCH_FADE_TIME,
			"easeType",
			iTween.EaseType.linear,
			"oncomplete",
			"OnLatchClosed",
			"oncompletetarget",
			base.gameObject
		});
		this.m_bookCoverLatch.GetComponent<Animation>().Play(this.LATCH_OPEN_ANIM_NAME);
		iTween.FadeTo(this.m_bookCoverLatchJoint, args);
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x00052F59 File Offset: 0x00051159
	private void OnCoverOpened(CollectionCoverDisplay.DelOnOpened callback)
	{
		this.m_bookCover.SetActive(false);
		this.SetIsAnimating(false);
		if (callback == null)
		{
			return;
		}
		callback();
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x00052F78 File Offset: 0x00051178
	private void OnLatchOpened()
	{
		this.m_bookCoverLatchJoint.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00052F8B File Offset: 0x0005118B
	private void OnLatchClosed()
	{
		this.EnableCollider(true);
		this.SetIsAnimating(false);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00052F9B File Offset: 0x0005119B
	private void CrackOpen()
	{
		if (this.IsAnimating())
		{
			return;
		}
		base.StopCoroutine(this.CRACK_LATCH_OPEN_ANIM_COROUTINE);
		base.StartCoroutine(this.CRACK_LATCH_OPEN_ANIM_COROUTINE);
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00052FBF File Offset: 0x000511BF
	private IEnumerator AnimateLatchCrackOpen()
	{
		this.m_bookCoverLatchJoint.GetComponent<Renderer>().SetMaterial(this.m_latchOpaqueMaterial);
		this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].time = 0f;
		this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].speed = this.LATCH_OPEN_ANIM_SPEED;
		SoundManager.Get().LoadAndPlay("collection_manager_book_latch_jiggle.prefab:45ddcdb304889ac48b14478fc78991ba");
		this.m_bookCoverLatch.GetComponent<Animation>().Play(this.LATCH_OPEN_ANIM_NAME);
		while (this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].time < 0.75f)
		{
			yield return null;
		}
		this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].speed = 0f;
		yield break;
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00052FD0 File Offset: 0x000511D0
	private void CrackClose()
	{
		if (this.IsAnimating())
		{
			return;
		}
		if (!this.m_bookCoverLatch.GetComponent<Animation>().IsPlaying(this.LATCH_OPEN_ANIM_NAME))
		{
			return;
		}
		base.StopCoroutine(this.CRACK_LATCH_OPEN_ANIM_COROUTINE);
		this.m_bookCoverLatch.GetComponent<Animation>()[this.LATCH_OPEN_ANIM_NAME].speed = -this.LATCH_OPEN_ANIM_SPEED;
	}

	// Token: 0x04000A26 RID: 2598
	public GameObject m_bookCoverLatch;

	// Token: 0x04000A27 RID: 2599
	public GameObject m_bookCoverLatchJoint;

	// Token: 0x04000A28 RID: 2600
	public GameObject m_bookCover;

	// Token: 0x04000A29 RID: 2601
	public Material m_latchFadeMaterial;

	// Token: 0x04000A2A RID: 2602
	public Material m_latchOpaqueMaterial;

	// Token: 0x04000A2B RID: 2603
	private readonly string CRACK_LATCH_OPEN_ANIM_COROUTINE = "AnimateLatchCrackOpen";

	// Token: 0x04000A2C RID: 2604
	private readonly string LATCH_OPEN_ANIM_NAME = "CollectionManagerCoverV2_Lock_edit";

	// Token: 0x04000A2D RID: 2605
	private readonly float LATCH_OPEN_ANIM_SPEED = 4f;

	// Token: 0x04000A2E RID: 2606
	private readonly float LATCH_FADE_TIME = 0.1f;

	// Token: 0x04000A2F RID: 2607
	private readonly float LATCH_FADE_DELAY = 0.15f;

	// Token: 0x04000A30 RID: 2608
	private readonly float BOOK_COVER_FULLY_CLOSED_Z_ROTATION;

	// Token: 0x04000A31 RID: 2609
	private readonly float BOOK_COVER_FULLY_OPEN_Z_ROTATION = 280f;

	// Token: 0x04000A32 RID: 2610
	private readonly float BOOK_COVER_FULL_ANIM_TIME = 0.75f;

	// Token: 0x04000A33 RID: 2611
	private bool m_isAnimating;

	// Token: 0x04000A34 RID: 2612
	private BoxCollider m_boxCollider;

	// Token: 0x0200141D RID: 5149
	// (Invoke) Token: 0x0600D9BD RID: 55741
	public delegate void DelOnOpened();
}
