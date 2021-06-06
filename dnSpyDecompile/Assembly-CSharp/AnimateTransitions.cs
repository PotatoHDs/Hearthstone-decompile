using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A08 RID: 2568
public class AnimateTransitions : MonoBehaviour
{
	// Token: 0x06008B15 RID: 35605 RVA: 0x002C79E0 File Offset: 0x002C5BE0
	public void StartTransitions()
	{
		foreach (Renderer renderer in this.rend)
		{
			renderer.GetMaterial().SetFloat("_Transistion", this.amount);
		}
	}

	// Token: 0x06008B16 RID: 35606 RVA: 0x002C7A40 File Offset: 0x002C5C40
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

	// Token: 0x06008B17 RID: 35607 RVA: 0x002C7AB4 File Offset: 0x002C5CB4
	private void Update()
	{
		this.StartTransitions();
	}

	// Token: 0x0400739C RID: 29596
	public List<GameObject> m_TargetList;

	// Token: 0x0400739D RID: 29597
	public float amount;

	// Token: 0x0400739E RID: 29598
	private List<Renderer> rend;
}
