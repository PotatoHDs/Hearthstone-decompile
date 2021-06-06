using System;
using UnityEngine;

public class ArenaPhoneControl : MonoBehaviour
{
	public enum ControlMode
	{
		ChooseHero,
		ChooseHeroPower,
		ChooseCard,
		CardCountViewDeck,
		ViewDeck,
		Rewards
	}

	public UberText m_ChooseText;

	public UberText m_ChooseDetailText;

	public GameObject m_ViewDeckButton;

	public BoxCollider m_ButtonCollider;

	public Vector3 m_CountAndViewDeckCollCenter;

	public Vector3 m_CountAndViewDeckCollSize;

	public Vector3 m_ViewDeckCollCenter;

	public Vector3 m_ViewDeckCollSize;

	private ControlMode m_CurrentMode;

	private void Awake()
	{
		m_CurrentMode = ControlMode.ChooseHero;
		m_ButtonCollider.enabled = false;
		m_ChooseText.Text = GameStrings.Get("GLUE_CHOOSE_YOUR_HERO");
	}

	[ContextMenu("ChooseHero")]
	public void SetModeChooseHero()
	{
		SetMode(ControlMode.ChooseHero);
	}

	[ContextMenu("ChooseCard")]
	public void SetModeChooseCard()
	{
		SetMode(ControlMode.ChooseCard);
	}

	[ContextMenu("CardCountViewDeck")]
	public void SetModeCardCountViewDeck()
	{
		SetMode(ControlMode.CardCountViewDeck);
	}

	[ContextMenu("ViewDeck")]
	public void SetModeViewDeck()
	{
		SetMode(ControlMode.ViewDeck);
	}

	[ContextMenu("ChooseHeroPower")]
	public void SetModeChooseHeroPower()
	{
		SetMode(ControlMode.ChooseHeroPower);
	}

	public void SetMode(ControlMode mode)
	{
		if (mode == m_CurrentMode)
		{
			return;
		}
		switch (mode)
		{
		case ControlMode.ChooseHero:
			m_ViewDeckButton.SetActive(value: false);
			m_ButtonCollider.enabled = false;
			m_ChooseText.Text = GameStrings.Get("GLUE_CHOOSE_YOUR_HERO");
			m_ChooseDetailText.Text = string.Empty;
			if (m_CurrentMode == ControlMode.CardCountViewDeck)
			{
				RotateTo(180f, 0f);
			}
			break;
		case ControlMode.ChooseHeroPower:
			m_ViewDeckButton.SetActive(value: false);
			m_ButtonCollider.enabled = false;
			m_ChooseText.Text = GameStrings.Get("GLUE_DRAFT_HERO_POWER_INSTRUCTIONS_TITLE");
			m_ChooseDetailText.Text = GameStrings.Get("GLUE_DRAFT_HERO_POWER_INSTRUCTIONS_DETAIL");
			if (m_CurrentMode == ControlMode.CardCountViewDeck)
			{
				RotateTo(180f, 0f);
			}
			break;
		case ControlMode.ChooseCard:
			m_ViewDeckButton.SetActive(value: false);
			m_ButtonCollider.enabled = false;
			m_ChooseText.Text = GameStrings.Get("GLUE_DRAFT_INSTRUCTIONS");
			m_ChooseDetailText.Text = string.Empty;
			if (m_CurrentMode == ControlMode.CardCountViewDeck)
			{
				RotateTo(180f, 0f);
			}
			break;
		case ControlMode.CardCountViewDeck:
			m_ButtonCollider.center = m_CountAndViewDeckCollCenter;
			m_ButtonCollider.size = m_CountAndViewDeckCollSize;
			m_ButtonCollider.enabled = true;
			RotateTo(0f, 180f);
			break;
		case ControlMode.ViewDeck:
			m_ButtonCollider.center = m_ViewDeckCollCenter;
			m_ButtonCollider.size = m_ViewDeckCollSize;
			m_ViewDeckButton.SetActive(value: true);
			m_ButtonCollider.enabled = true;
			if (m_CurrentMode == ControlMode.CardCountViewDeck)
			{
				RotateTo(180f, 0f);
			}
			break;
		case ControlMode.Rewards:
			m_ViewDeckButton.SetActive(value: false);
			m_ButtonCollider.enabled = false;
			m_ChooseText.Text = string.Empty;
			m_ChooseDetailText.Text = string.Empty;
			if (m_CurrentMode == ControlMode.CardCountViewDeck)
			{
				RotateTo(180f, 0f);
			}
			break;
		}
		m_CurrentMode = mode;
	}

	private void RotateTo(float rotFrom, float rotTo)
	{
		iTween.ValueTo(base.gameObject, iTween.Hash("from", rotFrom, "to", rotTo, "time", 1f, "easetype", iTween.EaseType.easeOutBounce, "onupdate", (Action<object>)delegate(object val)
		{
			base.transform.localRotation = Quaternion.Euler((float)val, 0f, 0f);
		}));
	}
}
