using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class WeaponSocketMgr : MonoBehaviour
{
	// Token: 0x06003183 RID: 12675 RVA: 0x000FDF64 File Offset: 0x000FC164
	public void UpdateSockets()
	{
		if (this.m_Decorations != null)
		{
			foreach (WeaponSocketDecoration weaponSocketDecoration in this.m_Decorations)
			{
				weaponSocketDecoration.UpdateVisibility();
			}
		}
	}

	// Token: 0x06003184 RID: 12676 RVA: 0x000FDFBC File Offset: 0x000FC1BC
	public static bool ShouldSeeWeaponSocket(TAG_CLASS tagVal)
	{
		switch (tagVal)
		{
		case TAG_CLASS.DRUID:
		case TAG_CLASS.MAGE:
		case TAG_CLASS.PRIEST:
			break;
		case TAG_CLASS.HUNTER:
		case TAG_CLASS.PALADIN:
			return true;
		default:
			if (tagVal != TAG_CLASS.WARLOCK)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x04001B8C RID: 7052
	public List<WeaponSocketDecoration> m_Decorations;
}
