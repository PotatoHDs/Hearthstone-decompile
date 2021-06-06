using System.Collections;
using UnityEngine;

public class GameplayErrorCloud : MonoBehaviour
{
	public UberText m_errorText;

	public float initTime;

	public ParticleSystem m_psystem;

	public Vector3_MobileOverride m_psystemLocalPositionInCollectionManager;

	public Vector3_MobileOverride m_psystemLocalPositionInGame;

	private const float ERROR_MESSAGE_DURATION = 2f;

	private const float ERROR_MESSAGE_FADEIN = 0.15f;

	private const float ERROR_MESSAGE_FADEOUT = 0.5f;

	private readonly string START_COROUTINE_NAME = "StartHideMessageDelay";

	private float m_holdDuration;

	private Coroutine m_coroutine;

	private void Start()
	{
		RenderUtils.SetAlpha(base.gameObject, 0f);
		Hide();
	}

	public void Show()
	{
		m_errorText.gameObject.SetActive(value: true);
		m_psystem.gameObject.SetActive(value: true);
		SetParticleEmitterLocalPosition();
	}

	public void Hide()
	{
		iTween.Stop(base.gameObject);
		StopCoroutine(START_COROUTINE_NAME);
		m_coroutine = null;
		m_errorText.gameObject.SetActive(value: false);
		m_psystem.gameObject.SetActive(value: false);
	}

	public void ShowMessage(string message, float timeToDisplay)
	{
		if (m_coroutine != null)
		{
			Hide();
		}
		iTween.Stop(base.gameObject);
		m_holdDuration = Mathf.Max(2f, timeToDisplay);
		ParticleSystem.MainModule main = m_psystem.main;
		main.startLifetime = 0.15f + m_holdDuration * 1.4f + 0.5f;
		Show();
		m_errorText.Text = message;
		iTween.FadeTo(base.gameObject, iTween.Hash("alpha", 1f, "time", 0.15f));
		m_coroutine = StartCoroutine(START_COROUTINE_NAME);
	}

	public void HideMessage()
	{
		iTween.FadeTo(base.gameObject, iTween.Hash("alpha", 0f, "time", 0.5f, "oncomplete", "Hide"));
	}

	public IEnumerator StartHideMessageDelay()
	{
		yield return new WaitForSeconds(0.15f + m_holdDuration);
		HideMessage();
	}

	private void SetParticleEmitterLocalPosition()
	{
		if (CollectionManager.Get().IsInEditMode())
		{
			m_psystem.transform.localPosition = m_psystemLocalPositionInCollectionManager;
		}
		else
		{
			m_psystem.transform.localPosition = m_psystemLocalPositionInGame;
		}
	}
}
