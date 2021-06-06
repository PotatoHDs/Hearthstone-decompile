using System.Collections;
using UnityEngine;

public class CollectionCoverDisplay : PegUIElement
{
	public delegate void DelOnOpened();

	public GameObject m_bookCoverLatch;

	public GameObject m_bookCoverLatchJoint;

	public GameObject m_bookCover;

	public Material m_latchFadeMaterial;

	public Material m_latchOpaqueMaterial;

	private readonly string CRACK_LATCH_OPEN_ANIM_COROUTINE = "AnimateLatchCrackOpen";

	private readonly string LATCH_OPEN_ANIM_NAME = "CollectionManagerCoverV2_Lock_edit";

	private readonly float LATCH_OPEN_ANIM_SPEED = 4f;

	private readonly float LATCH_FADE_TIME = 0.1f;

	private readonly float LATCH_FADE_DELAY = 0.15f;

	private readonly float BOOK_COVER_FULLY_CLOSED_Z_ROTATION;

	private readonly float BOOK_COVER_FULLY_OPEN_Z_ROTATION = 280f;

	private readonly float BOOK_COVER_FULL_ANIM_TIME = 0.75f;

	private bool m_isAnimating;

	private BoxCollider m_boxCollider;

	protected override void Awake()
	{
		base.Awake();
		m_boxCollider = base.transform.GetComponent<BoxCollider>();
	}

	public bool IsAnimating()
	{
		return m_isAnimating;
	}

	public void Open(DelOnOpened callback)
	{
		if (m_bookCover.transform.localEulerAngles.z != BOOK_COVER_FULLY_OPEN_Z_ROTATION)
		{
			EnableCollider(enabled: false);
			SetIsAnimating(animating: true);
			AnimateLatchOpening();
			AnimateCoverOpening(callback);
			SoundManager.Get().LoadAndPlay("collection_manager_book_open.prefab:e32dc00de806ee1478b67810b89947bb");
		}
	}

	public void SetOpenState()
	{
		if (m_bookCover.activeSelf)
		{
			EnableCollider(enabled: false);
			SetIsAnimating(animating: false);
			m_bookCover.SetActive(value: false);
			m_bookCoverLatchJoint.GetComponent<Renderer>().enabled = false;
		}
	}

	public void Close()
	{
		m_bookCover.SetActive(value: true);
		if (m_bookCover.transform.localEulerAngles.z != BOOK_COVER_FULLY_CLOSED_Z_ROTATION)
		{
			SetIsAnimating(animating: true);
			AnimateCoverClosing();
			SoundManager.Get().LoadAndPlay("collection_manager_book_close.prefab:872608cda202ca440aa60cd0918be9ad");
		}
	}

	private void SetIsAnimating(bool animating)
	{
		m_isAnimating = animating;
	}

	private void EnableCollider(bool enabled)
	{
		SetEnabled(enabled);
		m_boxCollider.enabled = enabled;
	}

	private void AnimateLatchOpening()
	{
		m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].speed = LATCH_OPEN_ANIM_SPEED;
		if (m_bookCoverLatch.GetComponent<Animation>().IsPlaying(LATCH_OPEN_ANIM_NAME))
		{
			StopCoroutine(CRACK_LATCH_OPEN_ANIM_COROUTINE);
		}
		else
		{
			m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].time = 0f;
			m_bookCoverLatch.GetComponent<Animation>().Play(LATCH_OPEN_ANIM_NAME);
		}
		Hashtable args = iTween.Hash("amount", 0, "delay", LATCH_FADE_DELAY, "time", LATCH_FADE_TIME, "easeType", iTween.EaseType.linear, "oncomplete", "OnLatchOpened", "oncompletetarget", base.gameObject);
		iTween.FadeTo(m_bookCoverLatchJoint, args);
	}

	private void AnimateCoverOpening(DelOnOpened callback)
	{
		m_bookCoverLatchJoint.GetComponent<Renderer>().SetMaterial(m_latchFadeMaterial);
		Vector3 localEulerAngles = m_bookCover.transform.localEulerAngles;
		localEulerAngles.z = BOOK_COVER_FULLY_OPEN_Z_ROTATION;
		Hashtable args = iTween.Hash("rotation", localEulerAngles, "isLocal", true, "time", BOOK_COVER_FULL_ANIM_TIME, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "OnCoverOpened", "oncompletetarget", base.gameObject, "oncompleteparams", callback, "name", "rotation");
		iTween.StopByName(m_bookCover.gameObject, "rotation");
		iTween.RotateTo(m_bookCover.gameObject, args);
	}

	private void AnimateCoverClosing()
	{
		Vector3 localEulerAngles = m_bookCover.transform.localEulerAngles;
		localEulerAngles.z = BOOK_COVER_FULLY_CLOSED_Z_ROTATION;
		Hashtable args = iTween.Hash("rotation", localEulerAngles, "isLocal", true, "time", BOOK_COVER_FULL_ANIM_TIME, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "AnimateLatchClosing", "oncompletetarget", base.gameObject, "name", "rotation");
		iTween.StopByName(m_bookCover.gameObject, "rotation");
		iTween.RotateTo(m_bookCover.gameObject, args);
	}

	private void AnimateLatchClosing()
	{
		m_bookCoverLatchJoint.GetComponent<Renderer>().enabled = true;
		m_bookCoverLatchJoint.GetComponent<Renderer>().SetMaterial(m_latchFadeMaterial);
		m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].time = m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].length;
		m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].speed = (0f - LATCH_OPEN_ANIM_SPEED) * 2f;
		Hashtable args = iTween.Hash("amount", 1, "time", LATCH_FADE_TIME, "easeType", iTween.EaseType.linear, "oncomplete", "OnLatchClosed", "oncompletetarget", base.gameObject);
		m_bookCoverLatch.GetComponent<Animation>().Play(LATCH_OPEN_ANIM_NAME);
		iTween.FadeTo(m_bookCoverLatchJoint, args);
	}

	private void OnCoverOpened(DelOnOpened callback)
	{
		m_bookCover.SetActive(value: false);
		SetIsAnimating(animating: false);
		callback?.Invoke();
	}

	private void OnLatchOpened()
	{
		m_bookCoverLatchJoint.GetComponent<Renderer>().enabled = false;
	}

	private void OnLatchClosed()
	{
		EnableCollider(enabled: true);
		SetIsAnimating(animating: false);
	}

	private void CrackOpen()
	{
		if (!IsAnimating())
		{
			StopCoroutine(CRACK_LATCH_OPEN_ANIM_COROUTINE);
			StartCoroutine(CRACK_LATCH_OPEN_ANIM_COROUTINE);
		}
	}

	private IEnumerator AnimateLatchCrackOpen()
	{
		m_bookCoverLatchJoint.GetComponent<Renderer>().SetMaterial(m_latchOpaqueMaterial);
		m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].time = 0f;
		m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].speed = LATCH_OPEN_ANIM_SPEED;
		SoundManager.Get().LoadAndPlay("collection_manager_book_latch_jiggle.prefab:45ddcdb304889ac48b14478fc78991ba");
		m_bookCoverLatch.GetComponent<Animation>().Play(LATCH_OPEN_ANIM_NAME);
		while (m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].time < 0.75f)
		{
			yield return null;
		}
		m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].speed = 0f;
	}

	private void CrackClose()
	{
		if (!IsAnimating() && m_bookCoverLatch.GetComponent<Animation>().IsPlaying(LATCH_OPEN_ANIM_NAME))
		{
			StopCoroutine(CRACK_LATCH_OPEN_ANIM_COROUTINE);
			m_bookCoverLatch.GetComponent<Animation>()[LATCH_OPEN_ANIM_NAME].speed = 0f - LATCH_OPEN_ANIM_SPEED;
		}
	}
}
