using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class AdventureWingRewardsChest_ICC : MonoBehaviour
{
	[CustomEditField(Sections = "ICC")]
	public List<GameObject> m_chests = new List<GameObject>();

	public void SetBigChestColliderEnabled(bool isEnabled)
	{
		GetComponent<Collider>().enabled = isEnabled;
	}

	public bool ActivateChest(int index)
	{
		if (index >= m_chests.Count)
		{
			return false;
		}
		for (int i = 0; i < m_chests.Count; i++)
		{
			m_chests[i].SetActive(i == index);
		}
		return true;
	}
}
