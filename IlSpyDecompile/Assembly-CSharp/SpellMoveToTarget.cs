using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMoveToTarget : Spell
{
	public float m_MovementDurationSec = 1f;

	public iTween.EaseType m_EaseType = iTween.EaseType.easeInOutSine;

	public bool m_DisableContainerAfterAction;

	public bool m_OnlyMoveContainer;

	public bool m_OrientToPath;

	public List<SpellPath> m_Paths;

	private bool m_waitingToAct = true;

	private SpellPath m_spellPath;

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

	public override void OnSpellFinished()
	{
		base.OnSpellFinished();
		ResetPath();
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
		m_spellPath = null;
		m_pathNodes = null;
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
		if (m_pathNodes == null)
		{
			if (m_Paths == null || m_Paths.Count == 0)
			{
				Debug.LogError($"SpellMoveToTarget.DeterminePath() - no SpellPaths available");
				return false;
			}
			iTweenPath[] components = GetComponents<iTweenPath>();
			if (components == null || components.Length == 0)
			{
				Debug.LogError($"SpellMoveToTarget.DeterminePath() - no iTweenPaths available");
				return false;
			}
			if (!FindBestPath(sourcePlayer, sourceCard, components, out var tweenPath, out var spellPath) && !FindFallbackPath(components, out tweenPath, out spellPath))
			{
				return false;
			}
			m_spellPath = spellPath;
			m_pathNodes = tweenPath.nodes.ToArray();
		}
		FixupPathNodes(sourcePlayer, sourceCard, targetCard);
		SetStartPosition();
		return true;
	}

	private bool FindBestPath(Player sourcePlayer, Card sourceCard, iTweenPath[] pathComponents, out iTweenPath tweenPath, out SpellPath spellPath)
	{
		tweenPath = null;
		spellPath = null;
		if (sourcePlayer == null)
		{
			return false;
		}
		if (sourcePlayer.GetSide() == Player.Side.FRIENDLY)
		{
			Predicate<SpellPath> match = (SpellPath currSpellPath) => currSpellPath.m_Type == SpellPathType.FRIENDLY;
			return FindPath(pathComponents, out tweenPath, out spellPath, match);
		}
		if (sourcePlayer.GetSide() == Player.Side.OPPOSING)
		{
			Predicate<SpellPath> match2 = (SpellPath currSpellPath) => currSpellPath.m_Type == SpellPathType.OPPOSING;
			return FindPath(pathComponents, out tweenPath, out spellPath, match2);
		}
		return false;
	}

	private bool FindFallbackPath(iTweenPath[] pathComponents, out iTweenPath tweenPath, out SpellPath spellPath)
	{
		Predicate<SpellPath> match = (SpellPath currSpellPath) => currSpellPath != null;
		return FindPath(pathComponents, out tweenPath, out spellPath, match);
	}

	private bool FindPath(iTweenPath[] pathComponents, out iTweenPath tweenPath, out SpellPath spellPath, Predicate<SpellPath> match)
	{
		tweenPath = null;
		spellPath = null;
		SpellPath spellPath2 = m_Paths.Find(match);
		if (spellPath2 == null)
		{
			return false;
		}
		string desiredSpellPathName = spellPath2.m_PathName.ToLower().Trim();
		iTweenPath iTweenPath2 = Array.Find(pathComponents, (iTweenPath currTweenPath) => currTweenPath.pathName.ToLower().Trim() == desiredSpellPathName);
		if (iTweenPath2 == null)
		{
			return false;
		}
		if (iTweenPath2.nodes == null || iTweenPath2.nodes.Count == 0)
		{
			return false;
		}
		tweenPath = iTweenPath2;
		spellPath = spellPath2;
		return true;
	}

	private void FixupPathNodes(Player sourcePlayer, Card sourceCard, Card targetCard)
	{
		if (!m_sourceComputed)
		{
			m_pathNodes[0] = base.transform.position + m_spellPath.m_FirstNodeOffset;
			m_sourceComputed = true;
		}
		if (m_targetComputed || !(targetCard != null))
		{
			return;
		}
		m_pathNodes[m_pathNodes.Length - 1] = targetCard.transform.position + m_spellPath.m_LastNodeOffset;
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
}
