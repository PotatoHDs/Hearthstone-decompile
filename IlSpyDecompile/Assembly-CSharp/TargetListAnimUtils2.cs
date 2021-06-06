using System.Collections.Generic;
using UnityEngine;

public class TargetListAnimUtils2 : MonoBehaviour
{
	public List<GameObject> m_TargetList;

	public void PlayAnimationList2()
	{
		foreach (GameObject target in m_TargetList)
		{
			if (!(target == null))
			{
				target.GetComponent<Animation>().Play();
			}
		}
	}

	public void StopAnimationList2()
	{
		foreach (GameObject target in m_TargetList)
		{
			if (!(target == null))
			{
				target.GetComponent<Animation>().Stop();
			}
		}
	}

	public void PlayAnimationListInChildren2()
	{
		foreach (GameObject target in m_TargetList)
		{
			if (!(target == null))
			{
				Animation[] componentsInChildren = target.GetComponentsInChildren<Animation>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Play();
				}
			}
		}
	}

	public void StopAnimationListInChildren2()
	{
		foreach (GameObject target in m_TargetList)
		{
			if (!(target == null))
			{
				Animation[] componentsInChildren = target.GetComponentsInChildren<Animation>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Stop();
				}
			}
		}
	}

	public void ActivateHierarchyList2()
	{
		foreach (GameObject target in m_TargetList)
		{
			if (!(target == null))
			{
				target.SetActive(value: true);
			}
		}
	}

	public void DeactivateHierarchyList2()
	{
		foreach (GameObject target in m_TargetList)
		{
			if (!(target == null))
			{
				target.SetActive(value: false);
			}
		}
	}

	public void DestroyHierarchyList2()
	{
		foreach (GameObject target in m_TargetList)
		{
			Object.Destroy(target);
		}
	}

	public void FadeInList2(float FadeSec)
	{
		foreach (GameObject target in m_TargetList)
		{
			iTween.FadeTo(target, 1f, FadeSec);
		}
	}

	public void FadeOutList2(float FadeSec)
	{
		foreach (GameObject target in m_TargetList)
		{
			iTween.FadeTo(target, 0f, FadeSec);
		}
	}

	public void SetAlphaHierarchyList2(float alpha)
	{
		foreach (GameObject target in m_TargetList)
		{
			if (target == null)
			{
				continue;
			}
			Renderer[] componentsInChildren = target.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Material material = componentsInChildren[i].GetMaterial();
				if (material.HasProperty("_Color"))
				{
					Color color = material.color;
					color.a = alpha;
					material.color = color;
				}
			}
		}
	}
}
