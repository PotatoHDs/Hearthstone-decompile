using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A9E RID: 2718
public class TentacleFX : MonoBehaviour
{
	// Token: 0x06009113 RID: 37139 RVA: 0x002F0D0C File Offset: 0x002EEF0C
	private void Start()
	{
		foreach (GameObject gameObject in this.m_Tentacles)
		{
			gameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Seed", UnityEngine.Random.Range(0f, 2f));
		}
		if (this.doRotate)
		{
			this.m_TentacleRoot.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0f, 360f), Space.Self);
		}
	}

	// Token: 0x040079DB RID: 31195
	public GameObject m_TentacleRoot;

	// Token: 0x040079DC RID: 31196
	public List<GameObject> m_Tentacles;

	// Token: 0x040079DD RID: 31197
	public bool doRotate = true;
}
