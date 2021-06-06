using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009C8 RID: 2504
[ExecuteAlways]
[CustomEditClass]
public abstract class NestedPrefabBase : MonoBehaviour
{
	// Token: 0x060088B1 RID: 34993 RVA: 0x002C06F4 File Offset: 0x002BE8F4
	public GameObject PrefabGameObject(bool instantiateIfNeeded = false)
	{
		if (this.m_PrefabGameObject == null && instantiateIfNeeded)
		{
			this.UpdateMesh();
		}
		return this.m_PrefabGameObject;
	}

	// Token: 0x060088B2 RID: 34994 RVA: 0x002C0712 File Offset: 0x002BE912
	public bool PrefabIsLoaded()
	{
		return this.m_PrefabGameObject != null;
	}

	// Token: 0x060088B3 RID: 34995 RVA: 0x002C0720 File Offset: 0x002BE920
	private void OnEnable()
	{
		if (this.m_PrefabGameObject == null)
		{
			this.UpdateMesh();
		}
	}

	// Token: 0x060088B4 RID: 34996 RVA: 0x002C0736 File Offset: 0x002BE936
	private void UpdateMesh()
	{
		this.LoadPrefab();
		this.m_EditorMeshes.Clear();
		if (base.enabled && this.m_PrefabGameObject != null)
		{
			this.SetupEditorMesh(this.m_PrefabGameObject, Matrix4x4.identity);
		}
	}

	// Token: 0x060088B5 RID: 34997 RVA: 0x002C0770 File Offset: 0x002BE970
	private void SetupEditorMesh(GameObject go, Matrix4x4 goMtx)
	{
		if (!go)
		{
			return;
		}
		Vector3 pos = go.transform.position * -1f;
		Matrix4x4 lhs = goMtx * Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
		foreach (Renderer renderer in go.GetComponentsInChildren(typeof(Renderer), true))
		{
			MeshFilter component = renderer.GetComponent<MeshFilter>();
			if (!(component == null))
			{
				List<Material> sharedMaterials = renderer.GetSharedMaterials();
				if (sharedMaterials.Count != 0)
				{
					this.m_EditorMeshes.Add(new NestedPrefabBase.EditorMesh
					{
						mesh = component.sharedMesh,
						matrix = lhs * renderer.transform.localToWorldMatrix,
						materials = new List<Material>(sharedMaterials)
					});
				}
			}
		}
		foreach (NestedPrefabBase nestedPrefabBase in go.GetComponentsInChildren(typeof(NestedPrefabBase), true))
		{
			if (nestedPrefabBase.enabled && nestedPrefabBase.gameObject.activeSelf)
			{
				this.SetupEditorMesh(nestedPrefabBase.m_PrefabGameObject, lhs * nestedPrefabBase.transform.localToWorldMatrix);
			}
		}
	}

	// Token: 0x060088B6 RID: 34998
	protected abstract void LoadPrefab();

	// Token: 0x060088B7 RID: 34999 RVA: 0x002C08B0 File Offset: 0x002BEAB0
	protected void LoadPrefab(string prefabToLoad)
	{
		this.m_PrefabGameObject = this.LoadWithAssetLoader(prefabToLoad);
		Quaternion localRotation = this.m_PrefabGameObject.transform.localRotation;
		Vector3 localScale = this.m_PrefabGameObject.transform.localScale;
		this.m_PrefabGameObject.transform.parent = base.transform;
		this.m_PrefabGameObject.transform.localPosition = Vector3.zero;
		this.m_PrefabGameObject.transform.localRotation = localRotation;
		this.m_PrefabGameObject.transform.localScale = localScale;
		this.m_PrefabGameObject.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
	}

	// Token: 0x060088B8 RID: 35000 RVA: 0x002C0946 File Offset: 0x002BEB46
	private GameObject LoadWithAssetLoader(string prefab)
	{
		return AssetLoader.Get().InstantiatePrefab(prefab, AssetLoadingOptions.None);
	}

	// Token: 0x040072D3 RID: 29395
	private List<NestedPrefabBase.EditorMesh> m_EditorMeshes = new List<NestedPrefabBase.EditorMesh>();

	// Token: 0x040072D4 RID: 29396
	private string m_lastPrefab;

	// Token: 0x040072D5 RID: 29397
	private GameObject m_PrefabGameObject;

	// Token: 0x0200267D RID: 9853
	private struct EditorMesh
	{
		// Token: 0x0400F0C0 RID: 61632
		public Mesh mesh;

		// Token: 0x0400F0C1 RID: 61633
		public Matrix4x4 matrix;

		// Token: 0x0400F0C2 RID: 61634
		public List<Material> materials;
	}
}
