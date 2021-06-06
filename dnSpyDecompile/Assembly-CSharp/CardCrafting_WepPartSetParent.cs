using System;
using UnityEngine;

// Token: 0x02000A17 RID: 2583
public class CardCrafting_WepPartSetParent : MonoBehaviour
{
	// Token: 0x06008B61 RID: 35681 RVA: 0x002C9265 File Offset: 0x002C7465
	private void Start()
	{
		if (!this.m_Parent)
		{
			Debug.LogError("Animation Event Set Parent is null!");
			base.enabled = false;
		}
	}

	// Token: 0x06008B62 RID: 35682 RVA: 0x002C9285 File Offset: 0x002C7485
	public void SetParentWepParts()
	{
		if (this.m_Parent)
		{
			this.m_WepParts.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x040073E1 RID: 29665
	public GameObject m_Parent;

	// Token: 0x040073E2 RID: 29666
	public GameObject m_WepParts;
}
