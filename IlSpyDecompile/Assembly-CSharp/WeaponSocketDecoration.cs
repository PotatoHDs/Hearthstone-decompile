using System.Collections.Generic;
using UnityEngine;

public class WeaponSocketDecoration : MonoBehaviour
{
	public List<WeaponSocketRequirement> m_VisibilityRequirements;

	public bool IsShown()
	{
		return GetComponent<Renderer>().enabled;
	}

	public void UpdateVisibility()
	{
		if (AreVisibilityRequirementsMet())
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	public bool AreVisibilityRequirementsMet()
	{
		Map<int, Player> playerMap = GameState.Get().GetPlayerMap();
		if (playerMap == null)
		{
			return false;
		}
		if (m_VisibilityRequirements == null)
		{
			return false;
		}
		foreach (WeaponSocketRequirement visibilityRequirement in m_VisibilityRequirements)
		{
			bool flag = false;
			foreach (Player value in playerMap.Values)
			{
				if (visibilityRequirement.m_Side == value.GetSide())
				{
					Entity hero = value.GetHero();
					if (hero == null)
					{
						Debug.LogWarning($"WeaponSocketDecoration.AreVisibilityRequirementsMet() - player {value} has no hero");
						return false;
					}
					if (visibilityRequirement.m_HasWeapon != WeaponSocketMgr.ShouldSeeWeaponSocket(hero.GetClass()))
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

	public void Show()
	{
		SceneUtils.EnableRenderers(base.gameObject, enable: true);
	}

	public void Hide()
	{
		SceneUtils.EnableRenderers(base.gameObject, enable: false);
	}
}
