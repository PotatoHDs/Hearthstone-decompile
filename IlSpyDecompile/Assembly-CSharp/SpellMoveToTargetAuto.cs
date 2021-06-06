using System.Collections;
using UnityEngine;

public class SpellMoveToTargetAuto : Spell
{
	public float m_MovementDurationSec = 0.5f;

	public iTween.EaseType m_EaseType = iTween.EaseType.linear;

	public bool m_DisableContainerAfterAction;

	public bool m_OnlyMoveContainer;

	public bool m_OrientToPath;

	public float CenterOffsetPercent = 50f;

	public float CenterPointHeightMin;

	public float CenterPointHeightMax;

	public float RightMin;

	public float RightMax;

	public float LeftMin;

	public float LeftMax;

	public bool DebugForceMax;

	public float DistanceScaleFactor = 8f;

	private bool m_waitingToAct = true;

	private Vector3[] m_pathNodes;

	private bool m_sourceComputed;

	private bool m_targetComputed;

	public override void SetSource(GameObject go)
	{
		if (GetSource() != go)
		{
			m_sourceComputed = false;
		}
		base.SetSource(go);
	}

	public override void RemoveSource()
	{
		base.RemoveSource();
		m_sourceComputed = false;
	}

	public override void AddTarget(GameObject go)
	{
		if (GetTarget() != go)
		{
			m_targetComputed = false;
		}
		base.AddTarget(go);
	}

	public override bool RemoveTarget(GameObject go)
	{
		GameObject target = GetTarget();
		if (!base.RemoveTarget(go))
		{
			return false;
		}
		if (target == go)
		{
			m_targetComputed = false;
		}
		return true;
	}

	public override void RemoveAllTargets()
	{
		bool num = m_targets.Count > 0;
		base.RemoveAllTargets();
		if (num)
		{
			m_targetComputed = false;
		}
	}

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		return AddSinglePowerTarget();
	}

	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		ResetPath();
		m_waitingToAct = true;
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			Debug.LogError($"{this}.OnBirth() - sourceCard is null");
			base.OnBirth(prevStateType);
			return;
		}
		Player controller = sourceCard.GetEntity().GetController();
		if (!DeterminePath(controller, sourceCard, null))
		{
			Debug.LogError($"{this}.OnBirth() - no paths available");
			base.OnBirth(prevStateType);
		}
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		if (m_pathNodes == null)
		{
			ResetPath();
		}
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			Debug.LogError($"SpellMoveToTarget.OnAction() - no source card");
			DoActionFallback(prevStateType);
			return;
		}
		Card targetCard = GetTargetCard();
		if (targetCard == null)
		{
			Debug.LogError($"SpellMoveToTarget.OnAction() - no target card");
			DoActionFallback(prevStateType);
			return;
		}
		Player controller = sourceCard.GetEntity().GetController();
		if (!DeterminePath(controller, sourceCard, targetCard))
		{
			Debug.LogError($"SpellMoveToTarget.DoAction() - no paths available, going to DEATH state");
			DoActionFallback(prevStateType);
		}
		else
		{
			StartCoroutine(WaitThenDoAction(prevStateType));
		}
	}

	protected IEnumerator WaitThenDoAction(SpellStateType prevStateType)
	{
		while (m_waitingToAct)
		{
			yield return null;
		}
		Hashtable args = iTween.Hash("path", m_pathNodes, "time", m_MovementDurationSec, "easetype", m_EaseType, "oncomplete", "OnMoveToTargetComplete", "oncompletetarget", base.gameObject, "orienttopath", m_OrientToPath);
		iTween.MoveTo(m_OnlyMoveContainer ? m_ObjectContainer : base.gameObject, args);
	}

	private void OnMoveToTargetComplete()
	{
		if (m_DisableContainerAfterAction)
		{
			ActivateObjectContainer(enable: false);
		}
		ChangeState(SpellStateType.DEATH);
	}

	private void StopWaitingToAct()
	{
		m_waitingToAct = false;
	}

	private void ResetPath()
	{
		m_pathNodes = new Vector3[3]
		{
			Vector3.zero,
			Vector3.zero,
			Vector3.zero
		};
		m_sourceComputed = false;
		m_targetComputed = false;
	}

	private void DoActionFallback(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		ChangeState(SpellStateType.DEATH);
	}

	private void SetStartPosition()
	{
		base.transform.position = m_pathNodes[0];
		if (m_OnlyMoveContainer)
		{
			m_ObjectContainer.transform.position = base.transform.position;
		}
	}

	private bool DeterminePath(Player sourcePlayer, Card sourceCard, Card targetCard)
	{
		FixupPathNodes(sourcePlayer, sourceCard, targetCard);
		SetStartPosition();
		return true;
	}

	private void FixupPathNodes(Player sourcePlayer, Card sourceCard, Card targetCard)
	{
		if (!m_sourceComputed)
		{
			m_pathNodes[0] = base.transform.position;
			m_sourceComputed = true;
		}
		if (!m_targetComputed && targetCard != null)
		{
			m_pathNodes[m_pathNodes.Length - 1] = targetCard.transform.position;
			float num = targetCard.transform.position.x - base.transform.position.x;
			float a = num / Mathf.Abs(num);
			for (int i = 1; i < m_pathNodes.Length - 1; i++)
			{
				float num2 = m_pathNodes[i].x - base.transform.position.x;
				float b = num2 / Mathf.Sqrt(num2 * num2);
				if (Mathf.Approximately(a, b))
				{
					m_pathNodes[i].x = base.transform.position.x - num2;
				}
			}
			m_targetComputed = true;
		}
		MoveCenterPoint();
	}

	private void MoveCenterPoint()
	{
		if (m_pathNodes.Length < 3)
		{
			return;
		}
		Vector3 vector = m_pathNodes[0];
		Vector3 vector2 = m_pathNodes[m_pathNodes.Length - 1];
		float num = Vector3.Distance(vector, vector2);
		Vector3 vector3 = vector2 - vector;
		vector3 /= num;
		Vector3 vector4 = vector + vector3 * (num * (CenterOffsetPercent * 0.01f));
		float num2 = num / DistanceScaleFactor;
		if (CenterPointHeightMin == CenterPointHeightMax)
		{
			vector4[1] += CenterPointHeightMax * num2;
		}
		else
		{
			vector4[1] += Random.Range(CenterPointHeightMin * num2, CenterPointHeightMax * num2);
		}
		float num3 = 1f;
		if (vector[2] > vector2[2])
		{
			num3 = -1f;
		}
		bool flag = false;
		if (Random.value > 0.5f)
		{
			flag = true;
		}
		if (RightMin == 0f && RightMax == 0f)
		{
			flag = false;
		}
		if (LeftMin == 0f && LeftMax == 0f)
		{
			flag = true;
		}
		if (flag)
		{
			if (RightMin == RightMax || DebugForceMax)
			{
				vector4[0] += RightMax * num2 * num3;
			}
			else
			{
				vector4[0] += Random.Range(RightMin * num2, RightMax * num2) * num3;
			}
		}
		else if (LeftMin == LeftMax || DebugForceMax)
		{
			vector4[0] -= LeftMax * num2 * num3;
		}
		else
		{
			vector4[0] -= Random.Range(LeftMin * num2, LeftMax * num2) * num3;
		}
		m_pathNodes[1] = vector4;
	}
}
