using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A39 RID: 2617
public class Gryphon : MonoBehaviour
{
	// Token: 0x06008CD0 RID: 36048 RVA: 0x002D1580 File Offset: 0x002CF780
	private void Start()
	{
		this.m_Animator = base.GetComponent<Animator>();
		this.m_UniversalInputManager = UniversalInputManager.Get();
		this.m_ScreechSound = base.GetComponent<AudioSource>();
		this.m_SnapWaitTime = UnityEngine.Random.Range(5f, 20f);
		this.m_Animator.SetLayerWeight(1, 1f);
	}

	// Token: 0x06008CD1 RID: 36049 RVA: 0x002D15D8 File Offset: 0x002CF7D8
	private void LateUpdate()
	{
		bool flag = false;
		this.m_CurrentBaseLayerState = this.m_Animator.GetCurrentAnimatorStateInfo(0);
		if (this.m_UniversalInputManager != null)
		{
			if (GameState.Get() != null && GameState.Get().IsMulliganManagerActive())
			{
				return;
			}
			if (this.m_UniversalInputManager.InputIsOver(base.gameObject))
			{
				flag = UniversalInputManager.Get().GetMouseButtonDown(0);
			}
			if (flag)
			{
				if (Time.time - this.m_lastScreech > 5f)
				{
					this.m_Animator.SetBool("Screech", true);
					SoundManager.Get().Play(this.m_ScreechSound, null, null, null);
					this.m_lastScreech = Time.time;
				}
			}
			else
			{
				this.m_Animator.SetBool("Screech", false);
			}
		}
		if (this.m_CurrentBaseLayerState.fullPathHash == Gryphon.lookState)
		{
			return;
		}
		if (this.m_CurrentBaseLayerState.fullPathHash == Gryphon.cleanState)
		{
			return;
		}
		if (this.m_CurrentBaseLayerState.fullPathHash == Gryphon.screechState)
		{
			return;
		}
		this.m_Animator.SetBool("Look", false);
		this.m_Animator.SetBool("Clean", false);
		this.PlayAniamtion();
	}

	// Token: 0x06008CD2 RID: 36050 RVA: 0x002D16F1 File Offset: 0x002CF8F1
	private void FindEndTurnButton()
	{
		this.m_EndTurnButton = EndTurnButton.Get();
		if (this.m_EndTurnButton == null)
		{
			return;
		}
		this.m_EndTurnButtonTransform = this.m_EndTurnButton.transform;
	}

	// Token: 0x06008CD3 RID: 36051 RVA: 0x002D1720 File Offset: 0x002CF920
	private void FindSomethingToLookAt()
	{
		List<Vector3> list = new List<Vector3>();
		ZoneMgr zoneMgr = ZoneMgr.Get();
		if (zoneMgr == null)
		{
			this.PlayAniamtion();
			return;
		}
		foreach (ZonePlay zonePlay in zoneMgr.FindZonesOfType<ZonePlay>())
		{
			foreach (Card card in zonePlay.GetCards())
			{
				if (card.IsMousedOver())
				{
					this.m_LookAtPosition = card.transform.position;
					return;
				}
				list.Add(card.transform.position);
			}
		}
		if (UnityEngine.Random.Range(0, 100) < this.m_LookAtHeroesPercent)
		{
			foreach (ZoneHero zoneHero in ZoneMgr.Get().FindZonesOfType<ZoneHero>())
			{
				foreach (Card card2 in zoneHero.GetCards())
				{
					if (card2.IsMousedOver())
					{
						this.m_LookAtPosition = card2.transform.position;
						return;
					}
					list.Add(card2.transform.position);
				}
			}
		}
		if (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			this.m_LookAtPosition = list[index];
			return;
		}
		this.PlayAniamtion();
	}

	// Token: 0x06008CD4 RID: 36052 RVA: 0x002D18DC File Offset: 0x002CFADC
	private void PlayAniamtion()
	{
		if (Time.time < this.m_idleEndTime)
		{
			return;
		}
		if (UnityEngine.Random.value > 0.5f)
		{
			this.m_idleEndTime = Time.time + 4f;
			this.m_Animator.SetBool("Look", false);
			this.m_Animator.SetBool("Clean", false);
			return;
		}
		if (UnityEngine.Random.value > 0.25f)
		{
			this.m_Animator.SetBool("Look", true);
			return;
		}
		this.m_Animator.SetBool("Clean", true);
	}

	// Token: 0x06008CD5 RID: 36053 RVA: 0x002D1968 File Offset: 0x002CFB68
	private bool LookAtTurnButton()
	{
		if (this.m_EndTurnButton == null)
		{
			this.FindEndTurnButton();
		}
		if (this.m_EndTurnButton == null)
		{
			return false;
		}
		if (this.m_EndTurnButton.IsInNMPState() && this.m_EndTurnButtonTransform != null)
		{
			this.m_LookAtPosition = this.m_EndTurnButtonTransform.position;
			return true;
		}
		return false;
	}

	// Token: 0x06008CD6 RID: 36054 RVA: 0x002D19C8 File Offset: 0x002CFBC8
	private void AniamteHead()
	{
		if (this.m_CurrentBaseLayerState.fullPathHash == Gryphon.lookState)
		{
			return;
		}
		if (this.m_CurrentBaseLayerState.fullPathHash == Gryphon.cleanState)
		{
			return;
		}
		if (this.m_CurrentBaseLayerState.fullPathHash == Gryphon.screechState)
		{
			return;
		}
		Quaternion b = Quaternion.LookRotation(this.m_LookAtPosition - this.m_HeadBone.position);
		this.m_HeadBone.rotation = Quaternion.Slerp(this.m_HeadBone.rotation, b, Time.deltaTime * this.m_HeadRotationSpeed);
	}

	// Token: 0x0400758B RID: 30091
	public float m_HeadRotationSpeed = 15f;

	// Token: 0x0400758C RID: 30092
	public float m_MinFocusTime = 1.2f;

	// Token: 0x0400758D RID: 30093
	public float m_MaxFocusTime = 5.5f;

	// Token: 0x0400758E RID: 30094
	public int m_PlayAnimationPercent = 20;

	// Token: 0x0400758F RID: 30095
	public int m_LookAtHeroesPercent = 20;

	// Token: 0x04007590 RID: 30096
	public int m_LookAtTurnButtonPercent = 75;

	// Token: 0x04007591 RID: 30097
	public float m_TurnButtonLookAwayTime = 0.5f;

	// Token: 0x04007592 RID: 30098
	public float m_SnapWaitTime = 1f;

	// Token: 0x04007593 RID: 30099
	public Transform m_HeadBone;

	// Token: 0x04007594 RID: 30100
	public GameObject m_SnapCollider;

	// Token: 0x04007595 RID: 30101
	private float m_WaitStartTime;

	// Token: 0x04007596 RID: 30102
	private float m_RandomWeightsTotal;

	// Token: 0x04007597 RID: 30103
	private Vector3 m_LookAtPosition;

	// Token: 0x04007598 RID: 30104
	private Animator m_Animator;

	// Token: 0x04007599 RID: 30105
	private EndTurnButton m_EndTurnButton;

	// Token: 0x0400759A RID: 30106
	private Transform m_EndTurnButtonTransform;

	// Token: 0x0400759B RID: 30107
	private UniversalInputManager m_UniversalInputManager;

	// Token: 0x0400759C RID: 30108
	private AnimatorStateInfo m_CurrentBaseLayerState;

	// Token: 0x0400759D RID: 30109
	private AudioSource m_ScreechSound;

	// Token: 0x0400759E RID: 30110
	private float m_lastScreech;

	// Token: 0x0400759F RID: 30111
	private float m_idleEndTime;

	// Token: 0x040075A0 RID: 30112
	private static int lookState = Animator.StringToHash("Base Layer.Look");

	// Token: 0x040075A1 RID: 30113
	private static int cleanState = Animator.StringToHash("Base Layer.Clean");

	// Token: 0x040075A2 RID: 30114
	private static int screechState = Animator.StringToHash("Base Layer.Screech");
}
