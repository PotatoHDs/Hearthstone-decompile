using UnityEngine;

public class TurnStartIndicator : MonoBehaviour
{
	public GameObject m_explosionFX;

	public GameObject m_godRays;

	public UberText m_labelTop;

	public UberText m_labelMiddle;

	public UberText m_labelBottom;

	public UberText m_reminderText;

	public float m_desiredDisplayDuration = 1.5f;

	public float m_desiredDelayDuration = 1f;

	private const float DISAPPEAR_SCALE_VAL = 0.01f;

	private const float START_SCALE_VAL = 1f;

	private const float AFTER_PUNCH_SCALE_VAL = 9.8f;

	private const float END_SCALE_VAL = 10f;

	private void Start()
	{
		iTween.FadeTo(base.gameObject, 0f, 0f);
		base.gameObject.transform.position = new Vector3(-7.8f, 8.2f, -5f);
		base.gameObject.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		base.gameObject.SetActive(value: false);
		SetReminderText("");
	}

	public bool IsShown()
	{
		return base.gameObject.activeSelf;
	}

	public float GetDesiredDelayDuration()
	{
		return m_desiredDelayDuration;
	}

	public virtual void Show()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			base.gameObject.transform.position = new Vector3(-7.8f, 8.2f, -4.2f);
		}
		else
		{
			base.gameObject.transform.position = new Vector3(-7.8f, 8.2f, -5f);
		}
		base.gameObject.SetActive(value: true);
		if (m_labelTop != null)
		{
			m_labelTop.Text = GameStrings.Get("GAMEPLAY_YOUR_TURN");
		}
		if (m_labelMiddle != null)
		{
			m_labelMiddle.Text = GameStrings.Get("GAMEPLAY_YOUR_TURN");
		}
		if (m_labelBottom != null)
		{
			m_labelBottom.Text = GameStrings.Get("GAMEPLAY_YOUR_TURN");
		}
		iTween.FadeTo(base.gameObject, 1f, 0.25f);
		base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", new Vector3(10f, 10f, 10f), "time", 0.25f, "oncomplete", "PunchTurnStartInstance", "oncompletetarget", base.gameObject));
		iTween.MoveTo(base.gameObject, iTween.Hash("position", base.gameObject.transform.position + new Vector3(0.02f, 0.02f, 0.02f), "time", m_desiredDisplayDuration, "oncomplete", "HideTurnStartInstance", "oncompletetarget", base.gameObject));
		m_explosionFX.GetComponent<ParticleSystem>().Play();
	}

	public void Hide()
	{
	}

	private void PunchTurnStartInstance()
	{
		iTween.ScaleTo(base.gameObject, new Vector3(9.8f, 9.8f, 9.8f), 0.15f);
	}

	private void HideTurnStartInstance()
	{
		iTween.FadeTo(base.gameObject, 0f, 0.25f);
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", 0.25f, "oncomplete", "DeactivateTurnStartInstance", "oncompletetarget", base.gameObject));
	}

	private void DeactivateTurnStartInstance()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetReminderText(string newText)
	{
		if (m_reminderText != null)
		{
			m_reminderText.Text = newText;
		}
	}
}
