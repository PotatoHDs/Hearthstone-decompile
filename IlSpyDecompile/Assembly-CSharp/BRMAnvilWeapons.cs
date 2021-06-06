using System;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class BRMAnvilWeapons : MonoBehaviour
{
	[Serializable]
	public class AnvilWeapon
	{
		public PlayMakerFSM m_FSM;

		public List<string> m_Events;

		[HideInInspector]
		public int m_CurrentWeaponIndex;
	}

	public List<AnvilWeapon> m_Weapons;

	private int m_LastWeaponIndex;

	public void RandomWeaponEvent()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < m_Weapons.Count; i++)
		{
			if (i != m_LastWeaponIndex)
			{
				list.Add(i);
			}
		}
		if (m_Weapons.Count > 0 && list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			AnvilWeapon anvilWeapon = m_Weapons[list[index]];
			m_LastWeaponIndex = list[index];
			anvilWeapon.m_FSM.SendEvent(anvilWeapon.m_Events[RandomSubWeapon(anvilWeapon)]);
		}
	}

	public int RandomSubWeapon(AnvilWeapon weapon)
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
}
