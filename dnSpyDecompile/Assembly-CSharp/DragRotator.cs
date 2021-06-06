using System;
using UnityEngine;

// Token: 0x02000A27 RID: 2599
public class DragRotator : MonoBehaviour
{
	// Token: 0x06008BDB RID: 35803 RVA: 0x002CC4B8 File Offset: 0x002CA6B8
	private void Awake()
	{
		this.m_prevPos = base.transform.position;
		this.m_originalAngles = base.transform.localRotation.eulerAngles;
	}

	// Token: 0x06008BDC RID: 35804 RVA: 0x002CC4F0 File Offset: 0x002CA6F0
	private void Update()
	{
		Vector3 position = base.transform.position;
		Vector3 vector = position - this.m_prevPos;
		if (vector.sqrMagnitude > 0.0001f)
		{
			this.m_pitchDeg += vector.z * this.m_info.m_PitchInfo.m_ForceMultiplier;
			this.m_pitchDeg = Mathf.Clamp(this.m_pitchDeg, this.m_info.m_PitchInfo.m_MinDegrees, this.m_info.m_PitchInfo.m_MaxDegrees);
			this.m_rollDeg -= vector.x * this.m_info.m_RollInfo.m_ForceMultiplier;
			this.m_rollDeg = Mathf.Clamp(this.m_rollDeg, this.m_info.m_RollInfo.m_MinDegrees, this.m_info.m_RollInfo.m_MaxDegrees);
		}
		this.m_pitchDeg = Mathf.SmoothDamp(this.m_pitchDeg, 0f, ref this.m_pitchVel, this.m_info.m_PitchInfo.m_RestSeconds * 0.1f);
		this.m_rollDeg = Mathf.SmoothDamp(this.m_rollDeg, 0f, ref this.m_rollVel, this.m_info.m_RollInfo.m_RestSeconds * 0.1f);
		base.transform.localRotation = Quaternion.Euler(this.m_originalAngles.x + this.m_pitchDeg, this.m_originalAngles.y, this.m_originalAngles.z + this.m_rollDeg);
		this.m_prevPos = position;
	}

	// Token: 0x06008BDD RID: 35805 RVA: 0x002CC67B File Offset: 0x002CA87B
	public DragRotatorInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06008BDE RID: 35806 RVA: 0x002CC683 File Offset: 0x002CA883
	public void SetInfo(DragRotatorInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06008BDF RID: 35807 RVA: 0x002CC68C File Offset: 0x002CA88C
	public void Reset()
	{
		this.m_prevPos = base.transform.position;
		base.transform.localRotation = Quaternion.Euler(this.m_originalAngles);
		this.m_rollDeg = 0f;
		this.m_rollVel = 0f;
		this.m_pitchDeg = 0f;
		this.m_pitchVel = 0f;
	}

	// Token: 0x040074C1 RID: 29889
	private const float EPSILON = 0.0001f;

	// Token: 0x040074C2 RID: 29890
	private const float SMOOTH_DAMP_SEC_FUDGE = 0.1f;

	// Token: 0x040074C3 RID: 29891
	private DragRotatorInfo m_info;

	// Token: 0x040074C4 RID: 29892
	private float m_pitchDeg;

	// Token: 0x040074C5 RID: 29893
	private float m_rollDeg;

	// Token: 0x040074C6 RID: 29894
	private float m_pitchVel;

	// Token: 0x040074C7 RID: 29895
	private float m_rollVel;

	// Token: 0x040074C8 RID: 29896
	private Vector3 m_prevPos;

	// Token: 0x040074C9 RID: 29897
	private Vector3 m_originalAngles;
}
