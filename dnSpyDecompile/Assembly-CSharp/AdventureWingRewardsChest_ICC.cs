using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005F RID: 95
[CustomEditClass]
public class AdventureWingRewardsChest_ICC : MonoBehaviour
{
	// Token: 0x06000585 RID: 1413 RVA: 0x00020063 File Offset: 0x0001E263
	public void SetBigChestColliderEnabled(bool isEnabled)
	{
		base.GetComponent<Collider>().enabled = isEnabled;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00020074 File Offset: 0x0001E274
	public bool ActivateChest(int index)
	{
		if (index >= this.m_chests.Count)
		{
			return false;
		}
		for (int i = 0; i < this.m_chests.Count; i++)
		{
			this.m_chests[i].SetActive(i == index);
		}
		return true;
	}

	// Token: 0x040003DF RID: 991
	[CustomEditField(Sections = "ICC")]
	public List<GameObject> m_chests = new List<GameObject>();
}
