using System;
using UnityEngine;

// Token: 0x02000A7C RID: 2684
public class ReticlePerspectiveAdjust : MonoBehaviour
{
	// Token: 0x06009019 RID: 36889 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x0600901A RID: 36890 RVA: 0x002EC9E4 File Offset: 0x002EABE4
	private void Update()
	{
		Camera main = Camera.main;
		if (main == null)
		{
			return;
		}
		Vector3 vector = main.WorldToScreenPoint(base.transform.position);
		float num = vector.x / (float)main.pixelWidth - 0.5f;
		float num2 = -(vector.y / (float)main.pixelHeight - 0.5f);
		base.transform.rotation = Quaternion.identity;
		base.transform.Rotate(new Vector3(this.m_VertialAdjustment * num2, 0f, this.m_HorizontalAdjustment * num), Space.World);
	}

	// Token: 0x040078FC RID: 30972
	public float m_HorizontalAdjustment = 20f;

	// Token: 0x040078FD RID: 30973
	public float m_VertialAdjustment = 20f;
}
