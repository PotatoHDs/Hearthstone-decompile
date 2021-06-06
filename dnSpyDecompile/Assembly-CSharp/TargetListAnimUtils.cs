using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A9B RID: 2715
public class TargetListAnimUtils : MonoBehaviour
{
	// Token: 0x060090F1 RID: 37105 RVA: 0x002EFD08 File Offset: 0x002EDF08
	public void PlayNewParticlesListInChildren()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Play();
				}
			}
		}
	}

	// Token: 0x060090F2 RID: 37106 RVA: 0x002EFD7C File Offset: 0x002EDF7C
	public void StopNewParticlesListInChildren()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Stop();
				}
			}
		}
	}

	// Token: 0x060090F3 RID: 37107 RVA: 0x002EFDF0 File Offset: 0x002EDFF0
	public void PlayAnimationList()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Animation>().Play();
			}
		}
	}

	// Token: 0x060090F4 RID: 37108 RVA: 0x002EFE54 File Offset: 0x002EE054
	public void StopAnimationList()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Animation>().Stop();
			}
		}
	}

	// Token: 0x060090F5 RID: 37109 RVA: 0x002EFEB4 File Offset: 0x002EE0B4
	public void PlayAnimationListInChildren()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				Animation[] componentsInChildren = gameObject.GetComponentsInChildren<Animation>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Play();
				}
			}
		}
	}

	// Token: 0x060090F6 RID: 37110 RVA: 0x002EFF28 File Offset: 0x002EE128
	public void StopAnimationListInChildren()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				Animation[] componentsInChildren = gameObject.GetComponentsInChildren<Animation>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Stop();
				}
			}
		}
	}

	// Token: 0x060090F7 RID: 37111 RVA: 0x002EFF9C File Offset: 0x002EE19C
	public void ActivateHierarchyList()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x060090F8 RID: 37112 RVA: 0x002EFFF8 File Offset: 0x002EE1F8
	public void DeactivateHierarchyList()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060090F9 RID: 37113 RVA: 0x002F0054 File Offset: 0x002EE254
	public void DestroyHierarchyList()
	{
		foreach (GameObject obj in this.m_TargetList)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	// Token: 0x060090FA RID: 37114 RVA: 0x002F00A4 File Offset: 0x002EE2A4
	public void FadeInList(float FadeSec)
	{
		foreach (GameObject target in this.m_TargetList)
		{
			iTween.FadeTo(target, 1f, FadeSec);
		}
	}

	// Token: 0x060090FB RID: 37115 RVA: 0x002F00FC File Offset: 0x002EE2FC
	public void FadeOutList(float FadeSec)
	{
		foreach (GameObject target in this.m_TargetList)
		{
			iTween.FadeTo(target, 0f, FadeSec);
		}
	}

	// Token: 0x060090FC RID: 37116 RVA: 0x002F0154 File Offset: 0x002EE354
	public void SetAlphaHierarchyList(float alpha)
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
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

	// Token: 0x040079BA RID: 31162
	public List<GameObject> m_TargetList;
}
