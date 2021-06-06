using System;
using UnityEngine;

// Token: 0x02000A0C RID: 2572
public class AnimationEventSetParent : MonoBehaviour
{
	// Token: 0x06008B27 RID: 35623 RVA: 0x002C7C39 File Offset: 0x002C5E39
	private void Start()
	{
		if (!this.m_Parent)
		{
			Debug.LogError("Animation Event Set Parent is null!");
			base.enabled = false;
		}
	}

	// Token: 0x06008B28 RID: 35624 RVA: 0x002C7C59 File Offset: 0x002C5E59
	public void SetParent()
	{
		if (this.m_Parent)
		{
			base.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x040073A4 RID: 29604
	public GameObject m_Parent;
}
