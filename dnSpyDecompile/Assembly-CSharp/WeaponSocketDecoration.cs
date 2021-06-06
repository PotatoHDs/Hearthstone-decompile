using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class WeaponSocketDecoration : MonoBehaviour
{
	// Token: 0x0600317D RID: 12669 RVA: 0x000FDE1E File Offset: 0x000FC01E
	public bool IsShown()
	{
		return base.GetComponent<Renderer>().enabled;
	}

	// Token: 0x0600317E RID: 12670 RVA: 0x000FDE2B File Offset: 0x000FC02B
	public void UpdateVisibility()
	{
		if (this.AreVisibilityRequirementsMet())
		{
			this.Show();
			return;
		}
		this.Hide();
	}

	// Token: 0x0600317F RID: 12671 RVA: 0x000FDE44 File Offset: 0x000FC044
	public bool AreVisibilityRequirementsMet()
	{
		Map<int, Player> playerMap = GameState.Get().GetPlayerMap();
		if (playerMap == null)
		{
			return false;
		}
		if (this.m_VisibilityRequirements == null)
		{
			return false;
		}
		foreach (WeaponSocketRequirement weaponSocketRequirement in this.m_VisibilityRequirements)
		{
			bool flag = false;
			foreach (Player player in playerMap.Values)
			{
				if (weaponSocketRequirement.m_Side == player.GetSide())
				{
					Entity hero = player.GetHero();
					if (hero == null)
					{
						Debug.LogWarning(string.Format("WeaponSocketDecoration.AreVisibilityRequirementsMet() - player {0} has no hero", player));
						return false;
					}
					if (weaponSocketRequirement.m_HasWeapon != WeaponSocketMgr.ShouldSeeWeaponSocket(hero.GetClass()))
					{
						return false;
					}
					flag = true;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003180 RID: 12672 RVA: 0x000FDF48 File Offset: 0x000FC148
	public void Show()
	{
		SceneUtils.EnableRenderers(base.gameObject, true);
	}

	// Token: 0x06003181 RID: 12673 RVA: 0x000FDF56 File Offset: 0x000FC156
	public void Hide()
	{
		SceneUtils.EnableRenderers(base.gameObject, false);
	}

	// Token: 0x04001B8B RID: 7051
	public List<WeaponSocketRequirement> m_VisibilityRequirements;
}
