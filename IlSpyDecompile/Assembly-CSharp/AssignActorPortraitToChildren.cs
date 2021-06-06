using System.Collections.Generic;
using UnityEngine;

public class AssignActorPortraitToChildren : MonoBehaviour
{
	private Actor m_Actor;

	private void Start()
	{
		m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
	}

	public void AssignPortraitToAllChildren()
	{
		if (!m_Actor || m_Actor.m_portraitMesh == null)
		{
			return;
		}
		List<Material> materials = m_Actor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
		if (materials.Count == 0 || m_Actor.m_portraitMatIdx < 0)
		{
			return;
		}
		Texture mainTexture = materials[m_Actor.m_portraitMatIdx].mainTexture;
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
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
}
