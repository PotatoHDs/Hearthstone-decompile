using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[CustomEditClass]
public abstract class NestedPrefabBase : MonoBehaviour
{
	private struct EditorMesh
	{
		public Mesh mesh;

		public Matrix4x4 matrix;

		public List<Material> materials;
	}

	private List<EditorMesh> m_EditorMeshes = new List<EditorMesh>();

	private string m_lastPrefab;

	private GameObject m_PrefabGameObject;

	public GameObject PrefabGameObject(bool instantiateIfNeeded = false)
	{
		if (m_PrefabGameObject == null && instantiateIfNeeded)
		{
			UpdateMesh();
		}
		return m_PrefabGameObject;
	}

	public bool PrefabIsLoaded()
	{
		return m_PrefabGameObject != null;
	}

	private void OnEnable()
	{
		if (m_PrefabGameObject == null)
		{
			UpdateMesh();
		}
	}

	private void UpdateMesh()
	{
		LoadPrefab();
		m_EditorMeshes.Clear();
		if (base.enabled && m_PrefabGameObject != null)
		{
			SetupEditorMesh(m_PrefabGameObject, Matrix4x4.identity);
		}
	}

	private void SetupEditorMesh(GameObject go, Matrix4x4 goMtx)
	{
		if (!go)
		{
			return;
		}
		Vector3 pos = go.transform.position * -1f;
		Matrix4x4 matrix4x = goMtx * Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
		Component[] componentsInChildren = go.GetComponentsInChildren(typeof(Renderer), includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = (Renderer)componentsInChildren[i];
			MeshFilter component = renderer.GetComponent<MeshFilter>();
			if (!(component == null))
			{
				List<Material> sharedMaterials = renderer.GetSharedMaterials();
				if (sharedMaterials.Count != 0)
				{
					m_EditorMeshes.Add(new EditorMesh
					{
						mesh = component.sharedMesh,
						matrix = matrix4x * renderer.transform.localToWorldMatrix,
						materials = new List<Material>(sharedMaterials)
					});
				}
			}
		}
		componentsInChildren = go.GetComponentsInChildren(typeof(NestedPrefabBase), includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			NestedPrefabBase nestedPrefabBase = (NestedPrefabBase)componentsInChildren[i];
			if (nestedPrefabBase.enabled && nestedPrefabBase.gameObject.activeSelf)
			{
				SetupEditorMesh(nestedPrefabBase.m_PrefabGameObject, matrix4x * nestedPrefabBase.transform.localToWorldMatrix);
			}
		}
	}

	protected abstract void LoadPrefab();

	protected void LoadPrefab(string prefabToLoad)
	{
		m_PrefabGameObject = LoadWithAssetLoader(prefabToLoad);
		Quaternion localRotation = m_PrefabGameObject.transform.localRotation;
		Vector3 localScale = m_PrefabGameObject.transform.localScale;
		m_PrefabGameObject.transform.parent = base.transform;
		m_PrefabGameObject.transform.localPosition = Vector3.zero;
		m_PrefabGameObject.transform.localRotation = localRotation;
		m_PrefabGameObject.transform.localScale = localScale;
		m_PrefabGameObject.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
	}

	private GameObject LoadWithAssetLoader(string prefab)
	{
		return AssetLoader.Get().InstantiatePrefab(prefab);
	}
}
