using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A9C RID: 2716
public class TargetListAnimUtils2 : MonoBehaviour
{
	// Token: 0x060090FE RID: 37118 RVA: 0x002F01F0 File Offset: 0x002EE3F0
	public void PlayAnimationList2()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Animation>().Play();
			}
		}
	}

	// Token: 0x060090FF RID: 37119 RVA: 0x002F0254 File Offset: 0x002EE454
	public void StopAnimationList2()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Animation>().Stop();
			}
		}
	}

	// Token: 0x06009100 RID: 37120 RVA: 0x002F02B4 File Offset: 0x002EE4B4
	public void PlayAnimationListInChildren2()
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

	// Token: 0x06009101 RID: 37121 RVA: 0x002F0328 File Offset: 0x002EE528
	public void StopAnimationListInChildren2()
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

	// Token: 0x06009102 RID: 37122 RVA: 0x002F039C File Offset: 0x002EE59C
	public void ActivateHierarchyList2()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06009103 RID: 37123 RVA: 0x002F03F8 File Offset: 0x002EE5F8
	public void DeactivateHierarchyList2()
	{
		foreach (GameObject gameObject in this.m_TargetList)
		{
			if (!(gameObject == null))
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06009104 RID: 37124 RVA: 0x002F0454 File Offset: 0x002EE654
	public void DestroyHierarchyList2()
	{
		foreach (GameObject obj in this.m_TargetList)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	// Token: 0x06009105 RID: 37125 RVA: 0x002F04A4 File Offset: 0x002EE6A4
	public void FadeInList2(float FadeSec)
	{
		foreach (GameObject target in this.m_TargetList)
		{
			iTween.FadeTo(target, 1f, FadeSec);
		}
	}

	// Token: 0x06009106 RID: 37126 RVA: 0x002F04FC File Offset: 0x002EE6FC
	public void FadeOutList2(float FadeSec)
	{
		foreach (GameObject target in this.m_TargetList)
		{
			iTween.FadeTo(target, 0f, FadeSec);
		}
	}

	// Token: 0x06009107 RID: 37127 RVA: 0x002F0554 File Offset: 0x002EE754
	public void SetAlphaHierarchyList2(float alpha)
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

	// Token: 0x040079BB RID: 31163
	public List<GameObject> m_TargetList;
}
