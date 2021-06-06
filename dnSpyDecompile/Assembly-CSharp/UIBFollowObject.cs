using System;
using UnityEngine;

// Token: 0x02000AF0 RID: 2800
[ExecuteAlways]
[CustomEditClass]
public class UIBFollowObject : MonoBehaviour
{
	// Token: 0x060094FA RID: 38138 RVA: 0x0030457C File Offset: 0x0030277C
	public void UpdateFollowPosition()
	{
		if (this.m_rootObject == null || this.m_objectToFollow == null)
		{
			return;
		}
		Vector3 vector = this.m_objectToFollow.transform.position;
		if (this.m_offset.sqrMagnitude > 0f)
		{
			vector += this.m_objectToFollow.transform.localToWorldMatrix * this.m_offset;
		}
		this.m_rootObject.transform.position = vector;
	}

	// Token: 0x04007CF2 RID: 31986
	public GameObject m_rootObject;

	// Token: 0x04007CF3 RID: 31987
	public GameObject m_objectToFollow;

	// Token: 0x04007CF4 RID: 31988
	public Vector3 m_offset;

	// Token: 0x04007CF5 RID: 31989
	public bool m_useWorldOffset;
}
