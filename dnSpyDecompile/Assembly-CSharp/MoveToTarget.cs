using System;
using UnityEngine;

// Token: 0x02000A53 RID: 2643
public class MoveToTarget : MonoBehaviour
{
	// Token: 0x06008E85 RID: 36485 RVA: 0x002DFD38 File Offset: 0x002DDF38
	private void Start()
	{
		if (this.m_AnimateOnStart)
		{
			this.StartAnimation();
		}
	}

	// Token: 0x06008E86 RID: 36486 RVA: 0x002DFD48 File Offset: 0x002DDF48
	private void Update()
	{
		if (this.m_MoveType == MoveToTarget.MoveType.MoveByTime)
		{
			this.MoveTime();
			return;
		}
		this.MoveSpeed();
	}

	// Token: 0x06008E87 RID: 36487 RVA: 0x002DFD60 File Offset: 0x002DDF60
	private void MoveTime()
	{
		if (this.m_isDone)
		{
			base.transform.position = this.m_TargetObject.position;
		}
		if (!this.m_Animate)
		{
			return;
		}
		Vector3 position = this.m_TargetObject.position;
		float num = 1f / this.m_Time;
		this.m_LerpPosition += num * Time.deltaTime;
		if (this.m_LerpPosition > 1f)
		{
			this.m_isDone = true;
			base.transform.position = this.m_TargetObject.position;
			return;
		}
		Vector3 position2 = Vector3.Lerp(this.m_StartPosition.position, position, this.m_LerpPosition);
		base.transform.position = position2;
	}

	// Token: 0x06008E88 RID: 36488 RVA: 0x002DFE10 File Offset: 0x002DE010
	private void MoveSpeed()
	{
		if (this.m_isDone)
		{
			base.transform.position = this.m_TargetObject.position;
		}
		if (!this.m_Animate)
		{
			return;
		}
		if (Vector3.Distance(base.transform.position, this.m_TargetObject.position) < this.m_SnapDistance)
		{
			this.m_isDone = true;
			base.transform.position = this.m_TargetObject.position;
			return;
		}
		Vector3 a = this.m_TargetObject.position - base.transform.position;
		a.Normalize();
		float d = this.m_Speed * Time.deltaTime;
		base.transform.position = base.transform.position + a * d;
	}

	// Token: 0x06008E89 RID: 36489 RVA: 0x002DFED7 File Offset: 0x002DE0D7
	private void StartAnimation()
	{
		if (this.m_StartPosition)
		{
			base.transform.position = this.m_StartPosition.position;
		}
		this.m_Animate = true;
		this.m_LerpPosition = 0f;
	}

	// Token: 0x040076B6 RID: 30390
	public Transform m_StartPosition;

	// Token: 0x040076B7 RID: 30391
	public Transform m_TargetObject;

	// Token: 0x040076B8 RID: 30392
	public MoveToTarget.MoveType m_MoveType;

	// Token: 0x040076B9 RID: 30393
	public float m_Time = 1f;

	// Token: 0x040076BA RID: 30394
	public float m_Speed = 1f;

	// Token: 0x040076BB RID: 30395
	public float m_SnapDistance = 0.1f;

	// Token: 0x040076BC RID: 30396
	public bool m_OrientToPath;

	// Token: 0x040076BD RID: 30397
	public bool m_AnimateOnStart;

	// Token: 0x040076BE RID: 30398
	private bool m_Animate;

	// Token: 0x040076BF RID: 30399
	private bool m_isDone;

	// Token: 0x040076C0 RID: 30400
	private Vector3 m_LastTargetPosition;

	// Token: 0x040076C1 RID: 30401
	private float m_LerpPosition;

	// Token: 0x020026B9 RID: 9913
	public enum MoveType
	{
		// Token: 0x0400F1D3 RID: 61907
		MoveByTime,
		// Token: 0x0400F1D4 RID: 61908
		MoveBySpeed
	}
}
