using System;
using UnityEngine;

// Token: 0x02000A7E RID: 2686
public class RotateByMovement : MonoBehaviour
{
	// Token: 0x06009020 RID: 36896 RVA: 0x002ECB90 File Offset: 0x002EAD90
	private void Update()
	{
		Transform transform = this.mParent.transform;
		if (this.m_previousPos == transform.localPosition)
		{
			return;
		}
		if (this.m_previousPos == Vector3.zero)
		{
			this.m_previousPos = transform.localPosition;
			return;
		}
		Vector3 localPosition = transform.localPosition;
		float num = localPosition.z - this.m_previousPos.z;
		float num2 = Mathf.Sqrt(Mathf.Pow(localPosition.x - this.m_previousPos.x, 2f) + Mathf.Pow(num, 2f));
		float num3 = Mathf.Asin(num / num2) * 180f / 3.1415927f;
		num3 -= 90f;
		base.transform.localEulerAngles = new Vector3(90f, num3, 0f);
		this.m_previousPos = localPosition;
	}

	// Token: 0x04007903 RID: 30979
	private Vector3 m_previousPos;

	// Token: 0x04007904 RID: 30980
	public GameObject mParent;
}
