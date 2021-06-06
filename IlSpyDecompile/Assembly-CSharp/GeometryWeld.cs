using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeometryWeld
{
	protected class SuggestedTranslation
	{
		public Vector3 translation;

		public List<int> startIndicies = new List<int>();

		public List<int> endIndicies = new List<int>();

		public bool MergeWith(SuggestedTranslation other, float clampErrorAngle)
		{
			if (Vector3.Angle(translation, other.translation) > clampErrorAngle)
			{
				return false;
			}
			startIndicies.AddRange(other.startIndicies);
			endIndicies.AddRange(other.endIndicies);
			return true;
		}
	}

	private static readonly bool DEBUG;

	public GameObject weldedGameObject;

	private IEnumerable<MeshRenderer> meshRenderers;

	public GeometryWeld(GameObject root, params GameObject[] objectsToWeld)
	{
		if (!Application.IsPlaying(root))
		{
			return;
		}
		IEnumerable<GameObject> source = from x in new GameObject[1] { root }.Concat(objectsToWeld.Where((GameObject x) => x != root))
			where x.GetComponent<MeshRenderer>() != null
			select x;
		IEnumerable<MeshFilter> source2 = source.Select((GameObject x) => x.GetComponent<MeshFilter>());
		meshRenderers = source.Select((GameObject x) => x.GetComponent<MeshRenderer>());
		List<Material> rootMaterials = meshRenderers.First().GetSharedMaterials();
		Func<Material[], bool> predicate = delegate(Material[] materials)
		{
			if (materials.Length != rootMaterials.Count)
			{
				return false;
			}
			for (int i = 0; i < rootMaterials.Count; i++)
			{
				if (materials[i] != rootMaterials[i])
				{
					return false;
				}
			}
			return true;
		};
		if (!(from x in meshRenderers.Skip(1)
			select x.GetSharedMaterials().ToArray()).All(predicate))
		{
			Error.AddDevFatal("Unable to weld {0} to {1}.  Materials differ.", root.name, string.Join(", ", objectsToWeld.Select((GameObject x) => x.name).ToArray()));
			return;
		}
		weldedGameObject = new GameObject("Welded_" + root.name);
		weldedGameObject.AddComponent<MeshFilter>().sharedMesh = CombineMeshes(source2.Select(delegate(MeshFilter x)
		{
			CombineInstance result = default(CombineInstance);
			result.mesh = x.sharedMesh;
			result.transform = root.transform.worldToLocalMatrix * x.transform.localToWorldMatrix;
			return result;
		}).ToArray());
		weldedGameObject.AddComponent<MeshRenderer>().SetSharedMaterials(rootMaterials);
		weldedGameObject.transform.SetParent(root.transform.parent);
		weldedGameObject.transform.position = root.transform.position;
		weldedGameObject.transform.rotation = root.transform.rotation;
		weldedGameObject.transform.localScale = root.transform.localScale;
		weldedGameObject.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
		if (DEBUG)
		{
			return;
		}
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			meshRenderer.enabled = false;
		}
	}

	private static Mesh CombineMeshes(IEnumerable<CombineInstance> combines)
	{
		Mesh mesh = new Mesh();
		int num = 0;
		int num2 = 0;
		int num3 = combines.Select((CombineInstance x) => x.mesh.vertexCount).Sum();
		Vector3[] array = new Vector3[num3];
		int[] array2 = new int[combines.Select((CombineInstance x) => x.mesh.triangles.Length).Sum()];
		Vector2[] array3 = new Vector2[num3];
		Vector3[] array4 = new Vector3[num3];
		foreach (CombineInstance combine in combines)
		{
			Vector3[] vertices = combine.mesh.vertices;
			int num4 = vertices.Length;
			Array.Copy(combine.mesh.uv, 0, array3, num, num4);
			Array.Copy(combine.mesh.normals, 0, array4, num, num4);
			for (int i = 0; i < num4; i++)
			{
				Vector3 vector = vertices[i];
				Vector4 vector2 = new Vector4(vector.x, vector.y, vector.z, 1f);
				vector2 = combine.transform * vector2;
				array[num + i] = vector2;
			}
			int[] triangles = combine.mesh.triangles;
			int num5 = triangles.Length;
			for (int j = 0; j < num5; j++)
			{
				array2[num2++] = triangles[j] + num;
			}
			num += num4;
		}
		ClampMeshes(array, combines, 0.03f, 20f);
		StretchTriangles(array, combines, 0.005f);
		mesh.vertices = array;
		mesh.triangles = array2;
		mesh.uv = array3;
		mesh.normals = array4;
		return mesh;
	}

	private static void ClampMeshes(Vector3[] verticies, IEnumerable<CombineInstance> meshRanges, float clampSqrDistance, float clampErrorAngle)
	{
		List<SuggestedTranslation> list = new List<SuggestedTranslation>();
		int num = -1;
		int num2 = -1;
		foreach (CombineInstance meshRange in meshRanges)
		{
			list.Clear();
			num = num2;
			num2 += meshRange.mesh.vertexCount;
			for (int i = 0; i <= num; i++)
			{
				Vector3 vector = verticies[i];
				for (int j = num + 1; j <= num2; j++)
				{
					Vector3 vector2 = verticies[j];
					Vector3 vector3 = vector - vector2;
					if (vector3.sqrMagnitude <= clampSqrDistance)
					{
						Vector4 vector4 = new Vector4(vector2.x, vector2.y, vector2.z, 1f);
						Vector3 to = meshRange.transform * vector4 - vector4;
						float num3 = Vector3.Angle(vector3, to);
						if (num3 < clampErrorAngle)
						{
							SuggestedTranslation suggestedTranslation = new SuggestedTranslation
							{
								translation = vector3
							};
							suggestedTranslation.startIndicies.Add(j);
							suggestedTranslation.endIndicies.Add(i);
							list.Add(suggestedTranslation);
						}
						else if (num3 + clampErrorAngle > 180f)
						{
							SuggestedTranslation suggestedTranslation2 = new SuggestedTranslation
							{
								translation = -vector3
							};
							suggestedTranslation2.startIndicies.Add(j);
							suggestedTranslation2.endIndicies.Add(i);
							list.Add(suggestedTranslation2);
						}
					}
				}
			}
			int count = list.Count;
			for (int k = 0; k < list.Count; k++)
			{
				for (int l = k + 1; l < list.Count; l++)
				{
					if (list[k].MergeWith(list[l], clampErrorAngle))
					{
						list.RemoveAt(l);
						l--;
					}
				}
			}
			SuggestedTranslation suggestedTranslation3 = list.OrderBy((SuggestedTranslation x) => x.startIndicies.Count).FirstOrDefault();
			if (suggestedTranslation3 == null || suggestedTranslation3.startIndicies.Count <= count / 2)
			{
				continue;
			}
			for (int m = num + 1; m <= num2; m++)
			{
				int num4 = suggestedTranslation3.startIndicies.IndexOf(m);
				if (num4 == -1)
				{
					verticies[m] += suggestedTranslation3.translation;
				}
				else
				{
					verticies[suggestedTranslation3.startIndicies[num4]] = verticies[suggestedTranslation3.endIndicies[num4]];
				}
			}
		}
	}

	private static void StretchTriangles(Vector3[] verticies, IEnumerable<CombineInstance> meshRanges, float strechSqrDistance)
	{
		int num = -1;
		int num2 = -1;
		foreach (CombineInstance meshRange in meshRanges)
		{
			_ = meshRange;
			for (int i = 0; i <= num; i++)
			{
				for (int j = num + 1; j <= num2; j++)
				{
					if ((verticies[i] - verticies[j]).sqrMagnitude <= strechSqrDistance)
					{
						verticies[j] = verticies[i];
					}
				}
			}
		}
	}

	public void Unweld()
	{
		if (weldedGameObject == null || !Application.IsPlaying(weldedGameObject))
		{
			return;
		}
		UnityEngine.Object.Destroy(weldedGameObject);
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			meshRenderer.enabled = true;
		}
	}
}
