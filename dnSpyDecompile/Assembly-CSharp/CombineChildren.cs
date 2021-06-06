using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009A7 RID: 2471
[AddComponentMenu("Mesh/Combine Children")]
public class CombineChildren : MonoBehaviour
{
	// Token: 0x060086E1 RID: 34529 RVA: 0x002B8C20 File Offset: 0x002B6E20
	private void Start()
	{
		Component[] componentsInChildren = base.GetComponentsInChildren(typeof(MeshFilter));
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		Hashtable hashtable = new Hashtable();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MeshFilter meshFilter = (MeshFilter)componentsInChildren[i];
			Renderer component = componentsInChildren[i].GetComponent<Renderer>();
			MeshCombineUtility.MeshInstance meshInstance = default(MeshCombineUtility.MeshInstance);
			meshInstance.mesh = meshFilter.sharedMesh;
			if (component != null && component.enabled && meshInstance.mesh != null)
			{
				meshInstance.transform = worldToLocalMatrix * meshFilter.transform.localToWorldMatrix;
				List<Material> sharedMaterials = component.GetSharedMaterials();
				for (int j = 0; j < sharedMaterials.Count; j++)
				{
					meshInstance.subMeshIndex = Math.Min(j, meshInstance.mesh.subMeshCount - 1);
					ArrayList arrayList = (ArrayList)hashtable[sharedMaterials[j]];
					if (arrayList != null)
					{
						arrayList.Add(meshInstance);
					}
					else
					{
						arrayList = new ArrayList();
						arrayList.Add(meshInstance);
						hashtable.Add(sharedMaterials[j], arrayList);
					}
				}
				component.enabled = false;
			}
		}
		foreach (object obj in hashtable)
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
			MeshCombineUtility.MeshInstance[] combines = (MeshCombineUtility.MeshInstance[])((ArrayList)dictionaryEntry.Value).ToArray(typeof(MeshCombineUtility.MeshInstance));
			if (hashtable.Count == 1)
			{
				if (base.GetComponent(typeof(MeshFilter)) == null)
				{
					base.gameObject.AddComponent(typeof(MeshFilter));
				}
				if (!base.GetComponent("MeshRenderer"))
				{
					base.gameObject.AddComponent<MeshRenderer>();
				}
				((MeshFilter)base.GetComponent(typeof(MeshFilter))).mesh = MeshCombineUtility.Combine(combines, this.generateTriangleStrips);
				Renderer component2 = base.GetComponent<Renderer>();
				component2.SetMaterial((Material)dictionaryEntry.Key);
				component2.enabled = true;
			}
			else
			{
				GameObject gameObject = new GameObject("Combined mesh");
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.AddComponent(typeof(MeshFilter));
				gameObject.AddComponent<MeshRenderer>();
				gameObject.GetComponent<Renderer>().SetMaterial((Material)dictionaryEntry.Key);
				((MeshFilter)gameObject.GetComponent(typeof(MeshFilter))).mesh = MeshCombineUtility.Combine(combines, this.generateTriangleStrips);
			}
		}
	}

	// Token: 0x04007217 RID: 29207
	public bool generateTriangleStrips = true;
}
