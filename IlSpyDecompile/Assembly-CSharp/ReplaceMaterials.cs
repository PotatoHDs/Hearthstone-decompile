using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[CustomEditClass]
public class ReplaceMaterials : MonoBehaviour
{
	[Serializable]
	public class MaterialData
	{
		[CustomEditField(T = EditType.SCENE_OBJECT)]
		public string GameObjectName;

		public int MaterialIndex;

		public Material NewMaterial;

		public bool ReplaceChildMaterials;

		public GameObject DisplayGameObject;
	}

	public List<MaterialData> m_Materials;

	private void Start()
	{
		foreach (MaterialData material in m_Materials)
		{
			if (material.NewMaterial == null)
			{
				continue;
			}
			GameObject gameObject = FindGameObject(material.GameObjectName);
			if (gameObject == null && !material.ReplaceChildMaterials)
			{
				Log.Graphics.Print("ReplaceMaterials failed to locate object: {0}", material.GameObjectName);
			}
			else if (material.ReplaceChildMaterials)
			{
				Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
				foreach (Renderer renderer in componentsInChildren)
				{
					if (!(renderer == null))
					{
						renderer.SetMaterial(material.MaterialIndex, material.NewMaterial);
					}
				}
			}
			else
			{
				Renderer component = gameObject.GetComponent<Renderer>();
				if (component == null)
				{
					Log.Graphics.Print("ReplaceMaterials failed to get Renderer: {0}", material.GameObjectName);
				}
				else
				{
					component.SetMaterial(material.MaterialIndex, material.NewMaterial);
				}
			}
		}
	}

	private GameObject FindGameObject(string gameObjName)
	{
		if (gameObjName[0] != '/')
		{
			return GameObject.Find(gameObjName);
		}
		string[] array = gameObjName.Split('/');
		return GameObject.Find(array[array.Length - 1]);
	}
}
