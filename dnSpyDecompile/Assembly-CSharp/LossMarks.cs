using System;
using UnityEngine;

// Token: 0x02000735 RID: 1845
public class LossMarks : MonoBehaviour
{
	// Token: 0x060067AC RID: 26540 RVA: 0x0021C8A4 File Offset: 0x0021AAA4
	public void Init(int numMarks)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(numMarks > 0);
			numMarks--;
		}
	}

	// Token: 0x060067AD RID: 26541 RVA: 0x0021C8E8 File Offset: 0x0021AAE8
	public void SetNumMarked(int numMarked)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).GetChild(0).gameObject.SetActive(numMarked > 0);
			numMarked--;
		}
	}
}
