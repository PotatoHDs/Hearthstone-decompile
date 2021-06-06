using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A7A RID: 2682
[ExecuteAlways]
[CustomEditClass]
public class ReplaceMaterials : MonoBehaviour
{
	// Token: 0x06009010 RID: 36880 RVA: 0x002EC310 File Offset: 0x002EA510
	private void Start()
	{
		foreach (ReplaceMaterials.MaterialData materialData in this.m_Materials)
		{
			if (!(materialData.NewMaterial == null))
			{
				GameObject gameObject = this.FindGameObject(materialData.GameObjectName);
				if (gameObject == null && !materialData.ReplaceChildMaterials)
				{
					Log.Graphics.Print("ReplaceMaterials failed to locate object: {0}", new object[]
					{
						materialData.GameObjectName
					});
				}
				else if (materialData.ReplaceChildMaterials)
				{
					foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
					{
						if (!(renderer == null))
						{
							renderer.SetMaterial(materialData.MaterialIndex, materialData.NewMaterial);
						}
					}
				}
				else
				{
					Renderer component = gameObject.GetComponent<Renderer>();
					if (component == null)
					{
						Log.Graphics.Print("ReplaceMaterials failed to get Renderer: {0}", new object[]
						{
							materialData.GameObjectName
						});
					}
					else
					{
						component.SetMaterial(materialData.MaterialIndex, materialData.NewMaterial);
					}
				}
			}
		}
	}

	// Token: 0x06009011 RID: 36881 RVA: 0x002EC440 File Offset: 0x002EA640
	private GameObject FindGameObject(string gameObjName)
	{
		if (gameObjName[0] != '/')
		{
			return GameObject.Find(gameObjName);
		}
		string[] array = gameObjName.Split(new char[]
		{
			'/'
		});
		return GameObject.Find(array[array.Length - 1]);
	}

	// Token: 0x040078F0 RID: 30960
	public List<ReplaceMaterials.MaterialData> m_Materials;

	// Token: 0x020026D3 RID: 9939
	[Serializable]
	public class MaterialData
	{
		// Token: 0x0400F234 RID: 62004
		[CustomEditField(T = EditType.SCENE_OBJECT)]
		public string GameObjectName;

		// Token: 0x0400F235 RID: 62005
		public int MaterialIndex;

		// Token: 0x0400F236 RID: 62006
		public Material NewMaterial;

		// Token: 0x0400F237 RID: 62007
		public bool ReplaceChildMaterials;

		// Token: 0x0400F238 RID: 62008
		public GameObject DisplayGameObject;
	}
}
