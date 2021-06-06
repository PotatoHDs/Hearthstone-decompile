using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A10 RID: 2576
public class AssignActorPortraitToChildren : MonoBehaviour
{
	// Token: 0x06008B3C RID: 35644 RVA: 0x002C8088 File Offset: 0x002C6288
	private void Start()
	{
		this.m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
	}

	// Token: 0x06008B3D RID: 35645 RVA: 0x002C809C File Offset: 0x002C629C
	public void AssignPortraitToAllChildren()
	{
		if (!this.m_Actor)
		{
			return;
		}
		if (this.m_Actor.m_portraitMesh == null)
		{
			return;
		}
		List<Material> materials = this.m_Actor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
		if (materials.Count == 0 || this.m_Actor.m_portraitMatIdx < 0)
		{
			return;
		}
		Texture mainTexture = materials[this.m_Actor.m_portraitMatIdx].mainTexture;
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			foreach (Material material in componentsInChildren[i].GetMaterials())
			{
				if (material.name.Contains("portrait"))
				{
					material.mainTexture = mainTexture;
				}
			}
		}
	}

	// Token: 0x040073B8 RID: 29624
	private Actor m_Actor;
}
