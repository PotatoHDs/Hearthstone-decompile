using System;
using System.Collections;
using UnityEngine;

public class ClockOverlayText : MonoBehaviour
{
	public GameObject m_bannerStandard;

	public UberText m_detailsTextStandard;

	public GameObject m_bannerYear;

	public UberText m_detailsText;

	public Vector3 m_maxScale;

	public Vector3 m_maxScale_phone;

	private Vector3 m_minScale = new Vector3(0.01f, 0.01f, 0.01f);

	public void Show()
	{
		Vector3 vector = m_maxScale;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			vector = m_maxScale_phone;
		}
		iTween.Stop(base.gameObject);
		base.gameObject.SetActive(value: true);
		Hashtable args = iTween.Hash("scale", vector, "time", 0.4f, "easetype", iTween.EaseType.easeOutQuad);
		iTween.ScaleTo(base.gameObject, args);
	}

	public void Hide()
	{
		iTween.Stop(base.gameObject);
		Hashtable args = iTween.Hash("scale", m_minScale, "time", 0.15f, "easetype", iTween.EaseType.easeInQuad, "oncomplete", (Action<object>)delegate
		{
			base.gameObject.SetActive(value: false);
		});
		iTween.ScaleTo(base.gameObject, args);
	}

	public void HideImmediate()
	{
		base.gameObject.SetActive(value: false);
		base.transform.localScale = m_minScale;
	}

	public void UpdateText(int step)
	{
		if (step == 0)
		{
			m_bannerYear.SetActive(value: false);
			m_detailsText.gameObject.SetActive(value: false);
			m_bannerStandard.SetActive(value: true);
			m_detailsTextStandard.gameObject.SetActive(value: true);
		}
		else
		{
			m_bannerStandard.SetActive(value: false);
			m_detailsTextStandard.gameObject.SetActive(value: false);
			m_bannerYear.SetActive(value: true);
			m_detailsText.gameObject.SetActive(value: true);
		}
	}
}
