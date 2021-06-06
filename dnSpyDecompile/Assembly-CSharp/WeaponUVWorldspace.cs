using System;
using UnityEngine;

// Token: 0x02000AC1 RID: 2753
public class WeaponUVWorldspace : MonoBehaviour
{
	// Token: 0x0600931C RID: 37660 RVA: 0x002FB467 File Offset: 0x002F9667
	private void Start()
	{
		this.m_material = base.gameObject.GetComponent<Renderer>().GetMaterial();
	}

	// Token: 0x0600931D RID: 37661 RVA: 0x002FB480 File Offset: 0x002F9680
	private void Update()
	{
		Vector3 vector = base.transform.position * 0.7f;
		this.m_material.SetFloat("_OffsetX", -vector.z - this.xOffset);
		this.m_material.SetFloat("_OffsetY", -vector.x - this.yOffset);
	}

	// Token: 0x04007B3E RID: 31550
	public float xOffset;

	// Token: 0x04007B3F RID: 31551
	public float yOffset;

	// Token: 0x04007B40 RID: 31552
	private Material m_material;
}
