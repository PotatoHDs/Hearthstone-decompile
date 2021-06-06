using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000972 RID: 2418
public class SpellMoveToTargetAuto : Spell
{
	// Token: 0x0600855A RID: 34138 RVA: 0x002B0E57 File Offset: 0x002AF057
	public override void SetSource(GameObject go)
	{
		if (base.GetSource() != go)
		{
			this.m_sourceComputed = false;
		}
		base.SetSource(go);
	}

	// Token: 0x0600855B RID: 34139 RVA: 0x002B0E75 File Offset: 0x002AF075
	public override void RemoveSource()
	{
		base.RemoveSource();
		this.m_sourceComputed = false;
	}

	// Token: 0x0600855C RID: 34140 RVA: 0x002B0E84 File Offset: 0x002AF084
	public override void AddTarget(GameObject go)
	{
		if (base.GetTarget() != go)
		{
			this.m_targetComputed = false;
		}
		base.AddTarget(go);
	}

	// Token: 0x0600855D RID: 34141 RVA: 0x002B0EA4 File Offset: 0x002AF0A4
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

	// Token: 0x0600855E RID: 34142 RVA: 0x002B0ED4 File Offset: 0x002AF0D4
	public override void RemoveAllTargets()
	{
		bool flag = this.m_targets.Count > 0;
		base.RemoveAllTargets();
		if (flag)
		{
			this.m_targetComputed = false;
		}
	}

	// Token: 0x0600855F RID: 34143 RVA: 0x002B0937 File Offset: 0x002AEB37
	public override bool AddPowerTargets()
	{
		return base.CanAddPowerTargets() && base.AddSinglePowerTarget();
	}

	// Token: 0x06008560 RID: 34144 RVA: 0x002B0EF4 File Offset: 0x002AF0F4
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

	// Token: 0x06008561 RID: 34145 RVA: 0x002B0F6C File Offset: 0x002AF16C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		if (this.m_pathNodes == null)
		{
			this.ResetPath();
		}
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

	// Token: 0x06008562 RID: 34146 RVA: 0x002B1027 File Offset: 0x002AF227
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

	// Token: 0x06008563 RID: 34147 RVA: 0x002B1036 File Offset: 0x002AF236
	private void OnMoveToTargetComplete()
	{
		if (this.m_DisableContainerAfterAction)
		{
			base.ActivateObjectContainer(false);
		}
		this.ChangeState(SpellStateType.DEATH);
	}

	// Token: 0x06008564 RID: 34148 RVA: 0x002B104E File Offset: 0x002AF24E
	private void StopWaitingToAct()
	{
		this.m_waitingToAct = false;
	}

	// Token: 0x06008565 RID: 34149 RVA: 0x002B1057 File Offset: 0x002AF257
	private void ResetPath()
	{
		this.m_pathNodes = new Vector3[]
		{
			Vector3.zero,
			Vector3.zero,
			Vector3.zero
		};
		this.m_sourceComputed = false;
		this.m_targetComputed = false;
	}

	// Token: 0x06008566 RID: 34150 RVA: 0x002B0ACD File Offset: 0x002AECCD
	private void DoActionFallback(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.ChangeState(SpellStateType.DEATH);
	}

	// Token: 0x06008567 RID: 34151 RVA: 0x002B1097 File Offset: 0x002AF297
	private void SetStartPosition()
	{
		base.transform.position = this.m_pathNodes[0];
		if (this.m_OnlyMoveContainer)
		{
			this.m_ObjectContainer.transform.position = base.transform.position;
		}
	}

	// Token: 0x06008568 RID: 34152 RVA: 0x002B10D3 File Offset: 0x002AF2D3
	private bool DeterminePath(Player sourcePlayer, Card sourceCard, Card targetCard)
	{
		this.FixupPathNodes(sourcePlayer, sourceCard, targetCard);
		this.SetStartPosition();
		return true;
	}

	// Token: 0x06008569 RID: 34153 RVA: 0x002B10E8 File Offset: 0x002AF2E8
	private void FixupPathNodes(Player sourcePlayer, Card sourceCard, Card targetCard)
	{
		if (!this.m_sourceComputed)
		{
			this.m_pathNodes[0] = base.transform.position;
			this.m_sourceComputed = true;
		}
		if (!this.m_targetComputed && targetCard != null)
		{
			this.m_pathNodes[this.m_pathNodes.Length - 1] = targetCard.transform.position;
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
		this.MoveCenterPoint();
	}

	// Token: 0x0600856A RID: 34154 RVA: 0x002B11F8 File Offset: 0x002AF3F8
	private void MoveCenterPoint()
	{
		if (this.m_pathNodes.Length < 3)
		{
			return;
		}
		Vector3 vector = this.m_pathNodes[0];
		Vector3 vector2 = this.m_pathNodes[this.m_pathNodes.Length - 1];
		float num = Vector3.Distance(vector, vector2);
		Vector3 a = vector2 - vector;
		a /= num;
		Vector3 vector3 = vector + a * (num * (this.CenterOffsetPercent * 0.01f));
		float num2 = num / this.DistanceScaleFactor;
		if (this.CenterPointHeightMin == this.CenterPointHeightMax)
		{
			ref Vector3 ptr = ref vector3;
			ptr[1] = ptr[1] + this.CenterPointHeightMax * num2;
		}
		else
		{
			ref Vector3 ptr = ref vector3;
			ptr[1] = ptr[1] + UnityEngine.Random.Range(this.CenterPointHeightMin * num2, this.CenterPointHeightMax * num2);
		}
		float num3 = 1f;
		if (vector[2] > vector2[2])
		{
			num3 = -1f;
		}
		bool flag = false;
		if (UnityEngine.Random.value > 0.5f)
		{
			flag = true;
		}
		if (this.RightMin == 0f && this.RightMax == 0f)
		{
			flag = false;
		}
		if (this.LeftMin == 0f && this.LeftMax == 0f)
		{
			flag = true;
		}
		if (flag)
		{
			if (this.RightMin == this.RightMax || this.DebugForceMax)
			{
				ref Vector3 ptr = ref vector3;
				ptr[0] = ptr[0] + this.RightMax * num2 * num3;
			}
			else
			{
				ref Vector3 ptr = ref vector3;
				ptr[0] = ptr[0] + UnityEngine.Random.Range(this.RightMin * num2, this.RightMax * num2) * num3;
			}
		}
		else if (this.LeftMin == this.LeftMax || this.DebugForceMax)
		{
			ref Vector3 ptr = ref vector3;
			ptr[0] = ptr[0] - this.LeftMax * num2 * num3;
		}
		else
		{
			ref Vector3 ptr = ref vector3;
			ptr[0] = ptr[0] - UnityEngine.Random.Range(this.LeftMin * num2, this.LeftMax * num2) * num3;
		}
		this.m_pathNodes[1] = vector3;
	}

	// Token: 0x04006FBD RID: 28605
	public float m_MovementDurationSec = 0.5f;

	// Token: 0x04006FBE RID: 28606
	public iTween.EaseType m_EaseType = iTween.EaseType.linear;

	// Token: 0x04006FBF RID: 28607
	public bool m_DisableContainerAfterAction;

	// Token: 0x04006FC0 RID: 28608
	public bool m_OnlyMoveContainer;

	// Token: 0x04006FC1 RID: 28609
	public bool m_OrientToPath;

	// Token: 0x04006FC2 RID: 28610
	public float CenterOffsetPercent = 50f;

	// Token: 0x04006FC3 RID: 28611
	public float CenterPointHeightMin;

	// Token: 0x04006FC4 RID: 28612
	public float CenterPointHeightMax;

	// Token: 0x04006FC5 RID: 28613
	public float RightMin;

	// Token: 0x04006FC6 RID: 28614
	public float RightMax;

	// Token: 0x04006FC7 RID: 28615
	public float LeftMin;

	// Token: 0x04006FC8 RID: 28616
	public float LeftMax;

	// Token: 0x04006FC9 RID: 28617
	public bool DebugForceMax;

	// Token: 0x04006FCA RID: 28618
	public float DistanceScaleFactor = 8f;

	// Token: 0x04006FCB RID: 28619
	private bool m_waitingToAct = true;

	// Token: 0x04006FCC RID: 28620
	private Vector3[] m_pathNodes;

	// Token: 0x04006FCD RID: 28621
	private bool m_sourceComputed;

	// Token: 0x04006FCE RID: 28622
	private bool m_targetComputed;
}
