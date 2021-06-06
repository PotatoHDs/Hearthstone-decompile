using System;
using UnityEngine;

// Token: 0x02000757 RID: 1879
public class BranchScript : MonoBehaviour
{
	// Token: 0x060069C3 RID: 27075 RVA: 0x00227157 File Offset: 0x00225357
	private void Awake()
	{
		this.timeSpawned = Time.time;
	}

	// Token: 0x060069C4 RID: 27076 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x04005684 RID: 22148
	public float timeSpawned;
}
