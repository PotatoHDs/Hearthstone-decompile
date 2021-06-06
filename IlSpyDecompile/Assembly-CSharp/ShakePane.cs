using System;
using System.Collections;
using Hearthstone.Core;
using UnityEngine;

public class ShakePane : MonoBehaviour
{
	[SerializeField]
	private GameObject m_shakeyObject;

	[SerializeField]
	private float m_multipleShakeTolerance = 1.2f;

	[SerializeField]
	private float m_maxRotation = 30f;

	private Coroutine m_shakeyStoreAnimCoroutine;

	private Vector3 m_shakeyObjectOriginalLocalRotation = Vector3.zero;

	private Vector3 m_shakeyObjectOriginalLocalPosition = Vector3.zero;

	private float m_lastShakeAmount;

	private bool m_stillShaking;

	protected void Awake()
	{
		if (m_shakeyObject != null)
		{
			m_shakeyObjectOriginalLocalRotation = m_shakeyObject.transform.localEulerAngles;
			m_shakeyObjectOriginalLocalPosition = m_shakeyObject.transform.localPosition;
		}
	}

	public void Shake(float xRotationAmount, float shakeTime, float delay = 0f, float translateAmount = 0f)
	{
		if (!(m_shakeyObject == null) && base.gameObject.activeInHierarchy)
		{
			m_shakeyStoreAnimCoroutine = StartCoroutine(AnimateShakeyObjectCoroutine(xRotationAmount, translateAmount, shakeTime, delay));
		}
	}

	public void Reset()
	{
		if (m_shakeyStoreAnimCoroutine != null)
		{
			StopCoroutine(m_shakeyStoreAnimCoroutine);
		}
	}

	private void OnStopShaking(object obj)
	{
		m_stillShaking = false;
	}

	private IEnumerator AnimateShakeyObjectCoroutine(float xRotationAmount, float translationAmount, float shakeTime, float delay)
	{
		xRotationAmount = Mathf.Clamp(xRotationAmount, 0f - m_maxRotation, m_maxRotation);
		float absRotation = Mathf.Abs(xRotationAmount);
		if (absRotation - m_lastShakeAmount < m_multipleShakeTolerance && m_stillShaking)
		{
			yield break;
		}
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		m_lastShakeAmount = absRotation;
		m_stillShaking = true;
		Processor.CancelScheduledCallback(OnStopShaking);
		Processor.ScheduleCallback(shakeTime * 0.25f, realTime: false, OnStopShaking);
		iTween.Stop(m_shakeyObject);
		m_shakeyObject.transform.localEulerAngles = m_shakeyObjectOriginalLocalRotation;
		iTween.PunchRotation(m_shakeyObject, iTween.Hash("x", xRotationAmount, "time", shakeTime, "delay", 0.001f, "oncomplete", (Action<object>)delegate
		{
			m_shakeyObject.transform.localEulerAngles = m_shakeyObjectOriginalLocalRotation;
			m_lastShakeAmount = 0f;
		}));
		if (translationAmount != 0f)
		{
			m_shakeyObject.transform.localPosition = m_shakeyObjectOriginalLocalPosition;
			iTween.PunchPosition(m_shakeyObject, iTween.Hash("y", translationAmount, "time", shakeTime, "delay", 0.001f, "oncomplete", (Action<object>)delegate
			{
				m_shakeyObject.transform.localPosition = m_shakeyObjectOriginalLocalPosition;
			}));
		}
	}
}
