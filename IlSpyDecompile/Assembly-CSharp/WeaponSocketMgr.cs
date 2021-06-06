using System.Collections.Generic;
using UnityEngine;

public class WeaponSocketMgr : MonoBehaviour
{
	public List<WeaponSocketDecoration> m_Decorations;

	public void UpdateSockets()
	{
		if (m_Decorations == null)
		{
			return;
		}
		foreach (WeaponSocketDecoration decoration in m_Decorations)
		{
			decoration.UpdateVisibility();
		}
	}

	public static bool ShouldSeeWeaponSocket(TAG_CLASS tagVal)
	{
		switch (tagVal)
		{
		case TAG_CLASS.DRUID:
		case TAG_CLASS.MAGE:
		case TAG_CLASS.PRIEST:
		case TAG_CLASS.WARLOCK:
			return false;
		default:
			return true;
		}
	}
}
