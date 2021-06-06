using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A09 RID: 2569
public class AnimateVars : MonoBehaviour
{
	// Token: 0x06008B19 RID: 35609 RVA: 0x002C7ABC File Offset: 0x002C5CBC
	public void AnimateValue()
	{
		foreach (Renderer renderer in this.m_renderers)
		{
			if (renderer != null)
			{
				renderer.GetMaterial().SetFloat(this.varName, this.amount);
			}
		}
	}

	// Token: 0x06008B1A RID: 35610 RVA: 0x002C7B28 File Offset: 0x002C5D28
	private void Start()
	{
		this.m_renderers = new List<Renderer>();
		foreach (GameObject gameObject in this.m_objects)
		{
			if (!(gameObject == null))
			{
				this.m_renderers.Add(gameObject.GetComponent<Renderer>());
			}
		}
	}

	// Token: 0x06008B1B RID: 35611 RVA: 0x002C7B9C File Offset: 0x002C5D9C
	private void Update()
	{
		this.AnimateValue();
	}

	// Token: 0x0400739F RID: 29599
	public List<GameObject> m_objects;

	// Token: 0x040073A0 RID: 29600
	public float amount;

	// Token: 0x040073A1 RID: 29601
	public string varName;

	// Token: 0x040073A2 RID: 29602
	private List<Renderer> m_renderers;
}
