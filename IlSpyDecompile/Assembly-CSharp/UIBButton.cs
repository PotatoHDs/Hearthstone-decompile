using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
[RequireComponent(typeof(Collider))]
public class UIBButton : PegUIElement
{
	[CustomEditField(Sections = "Button Objects")]
	public GameObject m_RootObject;

	[CustomEditField(Sections = "Text Object")]
	public UberText m_ButtonText;

	[CustomEditField(Sections = "Click Depress Behavior")]
	public Vector3 m_ClickDownOffset = new Vector3(0f, -0.05f, 0f);

	[CustomEditField(Sections = "Click Depress Behavior")]
	public float m_RaiseTime = 0.1f;

	[CustomEditField(Sections = "Click Depress Behavior")]
	public float m_DepressTime = 0.1f;

	[CustomEditField(Sections = "Click Depress Behavior")]
	public iTween.EaseType m_DepressEaseType = iTween.EaseType.linear;

	[CustomEditField(Sections = "Click Depress Behavior")]
	public bool m_HoldDepressionOnRelease;

	[CustomEditField(Sections = "Click Depress Behavior")]
	public bool m_DepressOnPhone;

	[CustomEditField(Sections = "Roll Over Depress Behavior")]
	public bool m_DepressOnOver;

	[CustomEditField(Sections = "Wiggle Behavior")]
	public Vector3 m_WiggleAmount = new Vector3(90f, 0f, 0f);

	[CustomEditField(Sections = "Wiggle Behavior")]
	public float m_WiggleTime = 0.5f;

	[CustomEditField(Sections = "Flip Enable Behavior")]
	public Vector3 m_DisabledRotation = Vector3.zero;

	[CustomEditField(Sections = "Flip Enable Behavior")]
	public bool m_AnimateFlip;

	[CustomEditField(Sections = "Flip Enable Behavior")]
	public float m_AnimateFlipTime = 0.25f;

	[CustomEditField(Sections = "Flip Enable Behavior")]
	public bool m_WigglePostFlip;

	[CustomEditField(Sections = "Flip Enable Behavior")]
	public Vector3 m_PostFlipWiggleAmount = new Vector3(90f, 0f, 0f);

	[CustomEditField(Sections = "Flip Enable Behavior")]
	public float m_PostFlipWiggleTime = 0.5f;

	[CustomEditField(Sections = "Events")]
	public string m_bubbleUpEvent;

	private Vector3? m_RootObjectOriginalPosition;

	private Vector3? m_RootObjectOriginalRotation;

	private bool m_Depressed;

	private bool m_HoldingDepression;

	private Vector3 m_targetRotation;

	protected override void OnPress()
	{
		if (!m_DepressOnOver)
		{
			Depress();
		}
	}

	protected override void OnRelease()
	{
		if ((!m_DepressOnOver && !m_HoldDepressionOnRelease) || (m_HoldingDepression && m_HoldDepressionOnRelease))
		{
			Raise();
		}
		else if (m_HoldDepressionOnRelease)
		{
			m_HoldingDepression = true;
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		if (m_Depressed && !m_HoldingDepression)
		{
			Raise();
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (m_DepressOnOver)
		{
			Depress();
		}
		Wiggle();
	}

	public void Select()
	{
		Depress();
	}

	public void Deselect()
	{
		Raise();
	}

	public void Flip(bool faceUp, bool forceImmediate = false)
	{
		if (m_RootObject == null)
		{
			return;
		}
		InitOriginalRotation();
		m_targetRotation = (faceUp ? m_RootObjectOriginalRotation.Value : (m_RootObjectOriginalRotation.Value + m_DisabledRotation));
		iTween.StopByName(m_RootObject, "flip");
		if (m_AnimateFlip && !forceImmediate)
		{
			Vector3 vector = (faceUp ? (-m_DisabledRotation) : m_DisabledRotation);
			Hashtable args = iTween.Hash("amount", vector, "time", m_AnimateFlipTime, "easeType", iTween.EaseType.linear, "isLocal", true, "name", "flip", "oncomplete", (Action<object>)delegate
			{
				m_RootObject.transform.localEulerAngles = m_targetRotation;
			});
			iTween.RotateAdd(m_RootObject, args);
			if (m_WigglePostFlip)
			{
				Wiggle(m_PostFlipWiggleAmount, m_targetRotation, m_PostFlipWiggleTime, m_AnimateFlipTime);
			}
		}
		else
		{
			m_RootObject.transform.localEulerAngles = m_targetRotation;
		}
	}

	public void SetText(string text)
	{
		if (m_ButtonText != null)
		{
			m_ButtonText.Text = text;
		}
	}

	public string GetText()
	{
		if (!m_ButtonText.GameStringLookup)
		{
			return m_ButtonText.Text;
		}
		return GameStrings.Get(m_ButtonText.Text);
	}

	public bool IsHoldingDepression()
	{
		return m_HoldingDepression;
	}

	private void Raise()
	{
		if (!(m_RootObject == null) && m_Depressed)
		{
			m_Depressed = false;
			m_HoldingDepression = false;
			iTween.StopByName(m_RootObject, "depress");
			if (m_RaiseTime > 0f)
			{
				Hashtable args = iTween.Hash("position", m_RootObjectOriginalPosition, "time", m_RaiseTime, "easeType", m_DepressEaseType, "isLocal", true, "name", "depress");
				iTween.MoveTo(m_RootObject, args);
			}
			else
			{
				m_RootObject.transform.localPosition = m_RootObjectOriginalPosition.Value;
			}
		}
	}

	private void Depress()
	{
		if (!(m_RootObject == null) && !m_Depressed && (!UniversalInputManager.UsePhoneUI || m_DepressOnPhone))
		{
			InitOriginalPosition();
			m_Depressed = true;
			iTween.StopByName(m_RootObject, "depress");
			Vector3 vector = m_RootObjectOriginalPosition.Value + m_ClickDownOffset;
			if (m_DepressTime > 0f)
			{
				Hashtable args = iTween.Hash("position", vector, "time", m_DepressTime, "easeType", m_DepressEaseType, "isLocal", true, "name", "depress");
				iTween.MoveTo(m_RootObject, args);
			}
			else
			{
				m_RootObject.transform.localPosition = vector;
			}
		}
	}

	private void Wiggle()
	{
		if (!(m_RootObject == null) && !UniversalInputManager.UsePhoneUI)
		{
			InitOriginalRotation();
			Wiggle(m_WiggleAmount, m_RootObjectOriginalRotation.Value, m_WiggleTime, 0f);
		}
	}

	private void Wiggle(Vector3 amount, Vector3 originalRotation, float time, float delay)
	{
		if (!(m_RootObject == null) && amount.sqrMagnitude != 0f && !(time <= 0f))
		{
			InitOriginalRotation();
			if (iTween.CountByName(m_RootObject, "wiggle") > 0)
			{
				iTween.StopByName(m_RootObject, "wiggle");
				m_RootObject.transform.localEulerAngles = m_targetRotation;
			}
			Hashtable args = iTween.Hash("amount", amount, "time", time, "delay", delay, "name", "wiggle", "onstart", (Action<object>)delegate
			{
				m_RootObject.transform.localEulerAngles = m_targetRotation;
			}, "oncomplete", (Action<object>)delegate
			{
				m_RootObject.transform.localEulerAngles = m_targetRotation;
			});
			iTween.PunchRotation(m_RootObject, args);
		}
	}

	private void InitOriginalRotation()
	{
		if (!(m_RootObject == null) && !m_RootObjectOriginalRotation.HasValue)
		{
			m_RootObjectOriginalRotation = m_RootObject.transform.localEulerAngles;
		}
	}

	private void InitOriginalPosition()
	{
		if (!(m_RootObject == null) && !m_RootObjectOriginalPosition.HasValue)
		{
			m_RootObjectOriginalPosition = m_RootObject.transform.localPosition;
		}
	}

	private void OnDisable()
	{
		iTween.StopByName("wiggle");
		if (!(m_RootObject == null))
		{
			m_RootObject.transform.localEulerAngles = m_targetRotation;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!(m_RootObject == null))
		{
			m_targetRotation = m_RootObject.transform.localEulerAngles;
		}
	}

	protected override void OnTap()
	{
		base.OnTap();
		if (!string.IsNullOrEmpty(m_bubbleUpEvent))
		{
			SendEventUpwardStateAction.SendEventUpward(base.gameObject, m_bubbleUpEvent);
		}
	}
}
