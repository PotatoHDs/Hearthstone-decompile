using System;
using UnityEngine;

// Token: 0x02000B09 RID: 2825
public class TokyoDriftOnActivate : MonoBehaviour
{
	// Token: 0x06009646 RID: 38470 RVA: 0x0030AB04 File Offset: 0x00308D04
	private void OnDisable()
	{
		base.transform.localPosition = this.m_originalLocalPosition;
		base.transform.localScale = this.m_originalLocalScale;
	}

	// Token: 0x06009647 RID: 38471 RVA: 0x0030AB28 File Offset: 0x00308D28
	private void OnEnable()
	{
		this.m_originalLocalPosition = base.transform.localPosition;
		this.m_originalWorldPosition = base.transform.position;
		this.m_originalLocalScale = base.transform.localScale;
		AnimationUtil.GrowThenDrift(base.gameObject, this.m_originalWorldPosition, this.m_DriftScale);
	}

	// Token: 0x04007DF9 RID: 32249
	public Transform m_DriftTarget;

	// Token: 0x04007DFA RID: 32250
	public float m_DriftDuration = 0.5f;

	// Token: 0x04007DFB RID: 32251
	public float m_DriftScale = 1f;

	// Token: 0x04007DFC RID: 32252
	private Vector3 m_originalLocalPosition;

	// Token: 0x04007DFD RID: 32253
	private Vector3 m_originalWorldPosition;

	// Token: 0x04007DFE RID: 32254
	private Vector3 m_originalLocalScale;
}
