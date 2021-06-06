using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000971 RID: 2417
public class SpellMoveToTarget : Spell
{
	// Token: 0x06008545 RID: 34117 RVA: 0x002B089A File Offset: 0x002AEA9A
	public override void SetSource(GameObject go)
	{
		if (base.GetSource() != go)
		{
			this.m_sourceComputed = false;
		}
		base.SetSource(go);
	}

	// Token: 0x06008546 RID: 34118 RVA: 0x002B08B8 File Offset: 0x002AEAB8
	public override void RemoveSource()
	{
		base.RemoveSource();
		this.m_sourceComputed = false;
	}

	// Token: 0x06008547 RID: 34119 RVA: 0x002B08C7 File Offset: 0x002AEAC7
	public override void AddTarget(GameObject go)
	{
		if (base.GetTarget() != go)
		{
			this.m_targetComputed = false;
		}
		base.AddTarget(go);
	}

	// Token: 0x06008548 RID: 34120 RVA: 0x002B08E8 File Offset: 0x002AEAE8
	public override bool RemoveTarget(GameObject go)
	{
		GameObject target = base.GetTarget();
		if (!base.RemoveTarget(go))
		{
			return false;
		}
		if (target == go)
		{
			this.m_targetComputed = false;
		}
		return true;
	}

	// Token: 0x06008549 RID: 34121 RVA: 0x002B0918 File Offset: 0x002AEB18
	public override void RemoveAllTargets()
	{
		bool flag = this.m_targets.Count > 0;
		base.RemoveAllTargets();
		if (flag)
		{
			this.m_targetComputed = false;
		}
	}

	// Token: 0x0600854A RID: 34122 RVA: 0x002B0937 File Offset: 0x002AEB37
	public override bool AddPowerTargets()
	{
		return base.CanAddPowerTargets() && base.AddSinglePowerTarget();
	}

	// Token: 0x0600854B RID: 34123 RVA: 0x002B094C File Offset: 0x002AEB4C
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		this.ResetPath();
		this.m_waitingToAct = true;
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			Debug.LogError(string.Format("{0}.OnBirth() - sourceCard is null", this));
			base.OnBirth(prevStateType);
			return;
		}
		Player controller = sourceCard.GetEntity().GetController();
		if (!this.DeterminePath(controller, sourceCard, null))
		{
			Debug.LogError(string.Format("{0}.OnBirth() - no paths available", this));
			base.OnBirth(prevStateType);
			return;
		}
	}

	// Token: 0x0600854C RID: 34124 RVA: 0x002B09C4 File Offset: 0x002AEBC4
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			Debug.LogError(string.Format("SpellMoveToTarget.OnAction() - no source card", Array.Empty<object>()));
			this.DoActionFallback(prevStateType);
			return;
		}
		Card targetCard = base.GetTargetCard();
		if (targetCard == null)
		{
			Debug.LogError(string.Format("SpellMoveToTarget.OnAction() - no target card", Array.Empty<object>()));
			this.DoActionFallback(prevStateType);
			return;
		}
		Player controller = sourceCard.GetEntity().GetController();
		if (!this.DeterminePath(controller, sourceCard, targetCard))
		{
			Debug.LogError(string.Format("SpellMoveToTarget.DoAction() - no paths available, going to DEATH state", Array.Empty<object>()));
			this.DoActionFallback(prevStateType);
			return;
		}
		base.StartCoroutine(this.WaitThenDoAction(prevStateType));
	}

	// Token: 0x0600854D RID: 34125 RVA: 0x002B0A71 File Offset: 0x002AEC71
	public override void OnSpellFinished()
	{
		base.OnSpellFinished();
		this.ResetPath();
	}

	// Token: 0x0600854E RID: 34126 RVA: 0x002B0A7F File Offset: 0x002AEC7F
	protected IEnumerator WaitThenDoAction(SpellStateType prevStateType)
	{
		while (this.m_waitingToAct)
		{
			yield return null;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"path",
			this.m_pathNodes,
			"time",
			this.m_MovementDurationSec,
			"easetype",
			this.m_EaseType,
			"oncomplete",
			"OnMoveToTargetComplete",
			"oncompletetarget",
			base.gameObject,
			"orienttopath",
			this.m_OrientToPath
		});
		iTween.MoveTo(this.m_OnlyMoveContainer ? this.m_ObjectContainer : base.gameObject, args);
		yield break;
	}

	// Token: 0x0600854F RID: 34127 RVA: 0x002B0A8E File Offset: 0x002AEC8E
	private void OnMoveToTargetComplete()
	{
		if (this.m_DisableContainerAfterAction)
		{
			base.ActivateObjectContainer(false);
		}
		this.ChangeState(SpellStateType.DEATH);
	}

	// Token: 0x06008550 RID: 34128 RVA: 0x002B0AA6 File Offset: 0x002AECA6
	private void StopWaitingToAct()
	{
		this.m_waitingToAct = false;
	}

	// Token: 0x06008551 RID: 34129 RVA: 0x002B0AAF File Offset: 0x002AECAF
	private void ResetPath()
	{
		this.m_spellPath = null;
		this.m_pathNodes = null;
		this.m_sourceComputed = false;
		this.m_targetComputed = false;
	}

	// Token: 0x06008552 RID: 34130 RVA: 0x002B0ACD File Offset: 0x002AECCD
	private void DoActionFallback(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.ChangeState(SpellStateType.DEATH);
	}

	// Token: 0x06008553 RID: 34131 RVA: 0x002B0ADD File Offset: 0x002AECDD
	private void SetStartPosition()
	{
		base.transform.position = this.m_pathNodes[0];
		if (this.m_OnlyMoveContainer)
		{
			this.m_ObjectContainer.transform.position = base.transform.position;
		}
	}

	// Token: 0x06008554 RID: 34132 RVA: 0x002B0B1C File Offset: 0x002AED1C
	private bool DeterminePath(Player sourcePlayer, Card sourceCard, Card targetCard)
	{
		if (this.m_pathNodes == null)
		{
			if (this.m_Paths == null || this.m_Paths.Count == 0)
			{
				Debug.LogError(string.Format("SpellMoveToTarget.DeterminePath() - no SpellPaths available", Array.Empty<object>()));
				return false;
			}
			iTweenPath[] components = base.GetComponents<iTweenPath>();
			if (components == null || components.Length == 0)
			{
				Debug.LogError(string.Format("SpellMoveToTarget.DeterminePath() - no iTweenPaths available", Array.Empty<object>()));
				return false;
			}
			iTweenPath iTweenPath;
			SpellPath spellPath;
			if (!this.FindBestPath(sourcePlayer, sourceCard, components, out iTweenPath, out spellPath) && !this.FindFallbackPath(components, out iTweenPath, out spellPath))
			{
				return false;
			}
			this.m_spellPath = spellPath;
			this.m_pathNodes = iTweenPath.nodes.ToArray();
		}
		this.FixupPathNodes(sourcePlayer, sourceCard, targetCard);
		this.SetStartPosition();
		return true;
	}

	// Token: 0x06008555 RID: 34133 RVA: 0x002B0BCC File Offset: 0x002AEDCC
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
			return this.FindPath(pathComponents, out tweenPath, out spellPath, match);
		}
		if (sourcePlayer.GetSide() == Player.Side.OPPOSING)
		{
			Predicate<SpellPath> match2 = (SpellPath currSpellPath) => currSpellPath.m_Type == SpellPathType.OPPOSING;
			return this.FindPath(pathComponents, out tweenPath, out spellPath, match2);
		}
		return false;
	}

	// Token: 0x06008556 RID: 34134 RVA: 0x002B0C54 File Offset: 0x002AEE54
	private bool FindFallbackPath(iTweenPath[] pathComponents, out iTweenPath tweenPath, out SpellPath spellPath)
	{
		Predicate<SpellPath> match = (SpellPath currSpellPath) => currSpellPath != null;
		return this.FindPath(pathComponents, out tweenPath, out spellPath, match);
	}

	// Token: 0x06008557 RID: 34135 RVA: 0x002B0C8C File Offset: 0x002AEE8C
	private bool FindPath(iTweenPath[] pathComponents, out iTweenPath tweenPath, out SpellPath spellPath, Predicate<SpellPath> match)
	{
		tweenPath = null;
		spellPath = null;
		SpellPath spellPath2 = this.m_Paths.Find(match);
		if (spellPath2 == null)
		{
			return false;
		}
		string desiredSpellPathName = spellPath2.m_PathName.ToLower().Trim();
		iTweenPath iTweenPath = Array.Find<iTweenPath>(pathComponents, (iTweenPath currTweenPath) => currTweenPath.pathName.ToLower().Trim() == desiredSpellPathName);
		if (iTweenPath == null)
		{
			return false;
		}
		if (iTweenPath.nodes == null || iTweenPath.nodes.Count == 0)
		{
			return false;
		}
		tweenPath = iTweenPath;
		spellPath = spellPath2;
		return true;
	}

	// Token: 0x06008558 RID: 34136 RVA: 0x002B0D0C File Offset: 0x002AEF0C
	private void FixupPathNodes(Player sourcePlayer, Card sourceCard, Card targetCard)
	{
		if (!this.m_sourceComputed)
		{
			this.m_pathNodes[0] = base.transform.position + this.m_spellPath.m_FirstNodeOffset;
			this.m_sourceComputed = true;
		}
		if (!this.m_targetComputed && targetCard != null)
		{
			this.m_pathNodes[this.m_pathNodes.Length - 1] = targetCard.transform.position + this.m_spellPath.m_LastNodeOffset;
			float num = targetCard.transform.position.x - base.transform.position.x;
			float a = num / Mathf.Abs(num);
			for (int i = 1; i < this.m_pathNodes.Length - 1; i++)
			{
				float num2 = this.m_pathNodes[i].x - base.transform.position.x;
				float b = num2 / Mathf.Sqrt(num2 * num2);
				if (Mathf.Approximately(a, b))
				{
					this.m_pathNodes[i].x = base.transform.position.x - num2;
				}
			}
			this.m_targetComputed = true;
		}
	}

	// Token: 0x04006FB2 RID: 28594
	public float m_MovementDurationSec = 1f;

	// Token: 0x04006FB3 RID: 28595
	public iTween.EaseType m_EaseType = iTween.EaseType.easeInOutSine;

	// Token: 0x04006FB4 RID: 28596
	public bool m_DisableContainerAfterAction;

	// Token: 0x04006FB5 RID: 28597
	public bool m_OnlyMoveContainer;

	// Token: 0x04006FB6 RID: 28598
	public bool m_OrientToPath;

	// Token: 0x04006FB7 RID: 28599
	public List<SpellPath> m_Paths;

	// Token: 0x04006FB8 RID: 28600
	private bool m_waitingToAct = true;

	// Token: 0x04006FB9 RID: 28601
	private SpellPath m_spellPath;

	// Token: 0x04006FBA RID: 28602
	private Vector3[] m_pathNodes;

	// Token: 0x04006FBB RID: 28603
	private bool m_sourceComputed;

	// Token: 0x04006FBC RID: 28604
	private bool m_targetComputed;
}
