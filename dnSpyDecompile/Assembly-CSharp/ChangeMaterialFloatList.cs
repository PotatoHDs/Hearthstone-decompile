using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A1A RID: 2586
public class ChangeMaterialFloatList : MonoBehaviour
{
	// Token: 0x06008B72 RID: 35698 RVA: 0x002C982C File Offset: 0x002C7A2C
	private void Start()
	{
		this.rend = new List<Renderer>();
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				this.rend.Add(gameObject.GetComponent<Renderer>());
			}
		}
	}

	// Token: 0x06008B73 RID: 35699 RVA: 0x002C98A0 File Offset: 0x002C7AA0
	private void Update()
	{
		foreach (Renderer renderer in this.rend)
		{
			renderer.GetMaterial().SetFloat(this.m_propertyName, this.m_propertyValue);
		}
	}

	// Token: 0x040073FF RID: 29695
	public List<GameObject> m_TargetList;

	// Token: 0x04007400 RID: 29696
	public string m_propertyName;

	// Token: 0x04007401 RID: 29697
	public float m_propertyValue;

	// Token: 0x04007402 RID: 29698
	private List<Renderer> rend;

	// Token: 0x04007403 RID: 29699
	private int m_materialProperty;

	// Token: 0x04007404 RID: 29700
	private Material m_mat;
}
