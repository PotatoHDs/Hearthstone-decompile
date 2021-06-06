using System.Collections.Generic;
using UnityEngine;

public class Gryphon : MonoBehaviour
{
	public float m_HeadRotationSpeed = 15f;

	public float m_MinFocusTime = 1.2f;

	public float m_MaxFocusTime = 5.5f;

	public int m_PlayAnimationPercent = 20;

	public int m_LookAtHeroesPercent = 20;

	public int m_LookAtTurnButtonPercent = 75;

	public float m_TurnButtonLookAwayTime = 0.5f;

	public float m_SnapWaitTime = 1f;

	public Transform m_HeadBone;

	public GameObject m_SnapCollider;

	private float m_WaitStartTime;

	private float m_RandomWeightsTotal;

	private Vector3 m_LookAtPosition;

	private Animator m_Animator;

	private EndTurnButton m_EndTurnButton;

	private Transform m_EndTurnButtonTransform;

	private UniversalInputManager m_UniversalInputManager;

	private AnimatorStateInfo m_CurrentBaseLayerState;

	private AudioSource m_ScreechSound;

	private float m_lastScreech;

	private float m_idleEndTime;

	private static int lookState = Animator.StringToHash("Base Layer.Look");

	private static int cleanState = Animator.StringToHash("Base Layer.Clean");

	private static int screechState = Animator.StringToHash("Base Layer.Screech");

	private void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_UniversalInputManager = UniversalInputManager.Get();
		m_ScreechSound = GetComponent<AudioSource>();
		m_SnapWaitTime = Random.Range(5f, 20f);
		m_Animator.SetLayerWeight(1, 1f);
	}

	private void LateUpdate()
	{
		bool flag = false;
		m_CurrentBaseLayerState = m_Animator.GetCurrentAnimatorStateInfo(0);
		if (m_UniversalInputManager != null)
		{
			if (GameState.Get() != null && GameState.Get().IsMulliganManagerActive())
			{
				return;
			}
			if (m_UniversalInputManager.InputIsOver(base.gameObject))
			{
				flag = UniversalInputManager.Get().GetMouseButtonDown(0);
			}
			if (flag)
			{
				if (Time.time - m_lastScreech > 5f)
				{
					m_Animator.SetBool("Screech", value: true);
					SoundManager.Get().Play(m_ScreechSound);
					m_lastScreech = Time.time;
				}
			}
			else
			{
				m_Animator.SetBool("Screech", value: false);
			}
		}
		if (m_CurrentBaseLayerState.fullPathHash != lookState && m_CurrentBaseLayerState.fullPathHash != cleanState && m_CurrentBaseLayerState.fullPathHash != screechState)
		{
			m_Animator.SetBool("Look", value: false);
			m_Animator.SetBool("Clean", value: false);
			PlayAniamtion();
		}
	}

	private void FindEndTurnButton()
	{
		m_EndTurnButton = EndTurnButton.Get();
		if (!(m_EndTurnButton == null))
		{
			m_EndTurnButtonTransform = m_EndTurnButton.transform;
		}
	}

	private void FindSomethingToLookAt()
	{
		List<Vector3> list = new List<Vector3>();
		ZoneMgr zoneMgr = ZoneMgr.Get();
		if (zoneMgr == null)
		{
			PlayAniamtion();
			return;
		}
		foreach (ZonePlay item in zoneMgr.FindZonesOfType<ZonePlay>())
		{
			foreach (Card card in item.GetCards())
			{
				if (card.IsMousedOver())
				{
					m_LookAtPosition = card.transform.position;
					return;
				}
				list.Add(card.transform.position);
			}
		}
		if (Random.Range(0, 100) < m_LookAtHeroesPercent)
		{
			foreach (ZoneHero item2 in ZoneMgr.Get().FindZonesOfType<ZoneHero>())
			{
				foreach (Card card2 in item2.GetCards())
				{
					if (card2.IsMousedOver())
					{
						m_LookAtPosition = card2.transform.position;
						return;
					}
					list.Add(card2.transform.position);
				}
			}
		}
		if (list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			m_LookAtPosition = list[index];
		}
		else
		{
			PlayAniamtion();
		}
	}

	private void PlayAniamtion()
	{
		if (!(Time.time < m_idleEndTime))
		{
			if (Random.value > 0.5f)
			{
				m_idleEndTime = Time.time + 4f;
				m_Animator.SetBool("Look", value: false);
				m_Animator.SetBool("Clean", value: false);
			}
			else if (Random.value > 0.25f)
			{
				m_Animator.SetBool("Look", value: true);
			}
			else
			{
				m_Animator.SetBool("Clean", value: true);
			}
		}
	}

	private bool LookAtTurnButton()
	{
		if (m_EndTurnButton == null)
		{
			FindEndTurnButton();
		}
		if (m_EndTurnButton == null)
		{
			return false;
		}
		if (m_EndTurnButton.IsInNMPState() && m_EndTurnButtonTransform != null)
		{
			m_LookAtPosition = m_EndTurnButtonTransform.position;
			return true;
		}
		return false;
	}

	private void AniamteHead()
	{
		if (m_CurrentBaseLayerState.fullPathHash != lookState && m_CurrentBaseLayerState.fullPathHash != cleanState && m_CurrentBaseLayerState.fullPathHash != screechState)
		{
			Quaternion b = Quaternion.LookRotation(m_LookAtPosition - m_HeadBone.position);
			m_HeadBone.rotation = Quaternion.Slerp(m_HeadBone.rotation, b, Time.deltaTime * m_HeadRotationSpeed);
		}
	}
}
