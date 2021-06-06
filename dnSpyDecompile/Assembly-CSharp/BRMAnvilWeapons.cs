using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AB RID: 171
[CustomEditClass]
public class BRMAnvilWeapons : MonoBehaviour
{
	// Token: 0x06000AC4 RID: 2756 RVA: 0x0003FD60 File Offset: 0x0003DF60
	public void RandomWeaponEvent()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_Weapons.Count; i++)
		{
			if (i != this.m_LastWeaponIndex)
			{
				list.Add(i);
			}
		}
		if (this.m_Weapons.Count > 0 && list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			BRMAnvilWeapons.AnvilWeapon anvilWeapon = this.m_Weapons[list[index]];
			this.m_LastWeaponIndex = list[index];
			anvilWeapon.m_FSM.SendEvent(anvilWeapon.m_Events[this.RandomSubWeapon(anvilWeapon)]);
		}
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x0003FDFC File Offset: 0x0003DFFC
	public int RandomSubWeapon(BRMAnvilWeapons.AnvilWeapon weapon)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < weapon.m_Events.Count; i++)
		{
			if (i != weapon.m_CurrentWeaponIndex)
			{
				list.Add(i);
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		weapon.m_CurrentWeaponIndex = list[index];
		return list[index];
	}

	// Token: 0x040006D6 RID: 1750
	public List<BRMAnvilWeapons.AnvilWeapon> m_Weapons;

	// Token: 0x040006D7 RID: 1751
	private int m_LastWeaponIndex;

	// Token: 0x020013B4 RID: 5044
	[Serializable]
	public class AnvilWeapon
	{
		// Token: 0x0400A78D RID: 42893
		public PlayMakerFSM m_FSM;

		// Token: 0x0400A78E RID: 42894
		public List<string> m_Events;

		// Token: 0x0400A78F RID: 42895
		[HideInInspector]
		public int m_CurrentWeaponIndex;
	}
}
