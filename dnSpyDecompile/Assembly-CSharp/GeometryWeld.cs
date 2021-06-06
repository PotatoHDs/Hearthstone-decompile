using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000ACF RID: 2767
public class GeometryWeld
{
	// Token: 0x0600938C RID: 37772 RVA: 0x002FD37C File Offset: 0x002FB57C
	public GeometryWeld(GameObject root, params GameObject[] objectsToWeld)
	{
		if (!Application.IsPlaying(root))
		{
			return;
		}
		IEnumerable<GameObject> source = from x in new GameObject[]
		{
			root
		}.Concat(from x in objectsToWeld
		where x != root
		select x)
		where x.GetComponent<MeshRenderer>() != null
		select x;
		IEnumerable<MeshFilter> source2 = from x in source
		select x.GetComponent<MeshFilter>();
		this.meshRenderers = from x in source
		select x.GetComponent<MeshRenderer>();
		List<Material> rootMaterials = this.meshRenderers.First<MeshRenderer>().GetSharedMaterials();
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
		if (!(from x in this.meshRenderers.Skip(1)
		select x.GetSharedMaterials().ToArray()).All(predicate))
		{
			string message = "Unable to weld {0} to {1}.  Materials differ.";
			object[] array = new object[2];
			array[0] = root.name;
			array[1] = string.Join(", ", (from x in objectsToWeld
			select x.name).ToArray<string>());
			Error.AddDevFatal(message, array);
			return;
		}
		this.weldedGameObject = new GameObject("Welded_" + root.name);
		this.weldedGameObject.AddComponent<MeshFilter>().sharedMesh = GeometryWeld.CombineMeshes((from x in source2
		select new CombineInstance
		{
			mesh = x.sharedMesh,
			transform = root.transform.worldToLocalMatrix * x.transform.localToWorldMatrix
		}).ToArray<CombineInstance>());
		this.weldedGameObject.AddComponent<MeshRenderer>().SetSharedMaterials(rootMaterials);
		this.weldedGameObject.transform.SetParent(root.transform.parent);
		this.weldedGameObject.transform.position = root.transform.position;
		this.weldedGameObject.transform.rotation = root.transform.rotation;
		this.weldedGameObject.transform.localScale = root.transform.localScale;
		this.weldedGameObject.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
		if (!GeometryWeld.DEBUG)
		{
			foreach (MeshRenderer meshRenderer in this.meshRenderers)
			{
				meshRenderer.enabled = false;
			}
		}
	}

	// Token: 0x0600938D RID: 37773 RVA: 0x002FD638 File Offset: 0x002FB838
	private static Mesh CombineMeshes(IEnumerable<CombineInstance> combines)
	{
		Mesh mesh = new Mesh();
		int num = 0;
		int num2 = 0;
		int num3 = (from x in combines
		select x.mesh.vertexCount).Sum();
		Vector3[] array = new Vector3[num3];
		int[] array2 = new int[(from x in combines
		select x.mesh.triangles.Length).Sum()];
		Vector2[] array3 = new Vector2[num3];
		Vector3[] array4 = new Vector3[num3];
		foreach (CombineInstance combineInstance in combines)
		{
			Vector3[] vertices = combineInstance.mesh.vertices;
			int num4 = vertices.Length;
			Array.Copy(combineInstance.mesh.uv, 0, array3, num, num4);
			Array.Copy(combineInstance.mesh.normals, 0, array4, num, num4);
			for (int i = 0; i < num4; i++)
			{
				Vector3 vector = vertices[i];
				Vector4 vector2 = new Vector4(vector.x, vector.y, vector.z, 1f);
				vector2 = combineInstance.transform * vector2;
				array[num + i] = vector2;
			}
			int[] triangles = combineInstance.mesh.triangles;
			int num5 = triangles.Length;
			for (int j = 0; j < num5; j++)
			{
				array2[num2++] = triangles[j] + num;
			}
			num += num4;
		}
		GeometryWeld.ClampMeshes(array, combines, 0.03f, 20f);
		GeometryWeld.StretchTriangles(array, combines, 0.005f);
		mesh.vertices = array;
		mesh.triangles = array2;
		mesh.uv = array3;
		mesh.normals = array4;
		return mesh;
	}

	// Token: 0x0600938E RID: 37774 RVA: 0x002FD818 File Offset: 0x002FBA18
	private static void ClampMeshes(Vector3[] verticies, IEnumerable<CombineInstance> meshRanges, float clampSqrDistance, float clampErrorAngle)
	{
		List<GeometryWeld.SuggestedTranslation> list = new List<GeometryWeld.SuggestedTranslation>();
		int num = -1;
		foreach (CombineInstance combineInstance in meshRanges)
		{
			list.Clear();
			int num2 = num;
			num += combineInstance.mesh.vertexCount;
			for (int i = 0; i <= num2; i++)
			{
				Vector3 a = verticies[i];
				for (int j = num2 + 1; j <= num; j++)
				{
					Vector3 vector = verticies[j];
					Vector3 vector2 = a - vector;
					if (vector2.sqrMagnitude <= clampSqrDistance)
					{
						Vector4 vector3 = new Vector4(vector.x, vector.y, vector.z, 1f);
						Vector3 to = combineInstance.transform * vector3 - vector3;
						float num3 = Vector3.Angle(vector2, to);
						if (num3 < clampErrorAngle)
						{
							GeometryWeld.SuggestedTranslation suggestedTranslation = new GeometryWeld.SuggestedTranslation
							{
								translation = vector2
							};
							suggestedTranslation.startIndicies.Add(j);
							suggestedTranslation.endIndicies.Add(i);
							list.Add(suggestedTranslation);
						}
						else if (num3 + clampErrorAngle > 180f)
						{
							GeometryWeld.SuggestedTranslation suggestedTranslation2 = new GeometryWeld.SuggestedTranslation
							{
								translation = -vector2
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
			GeometryWeld.SuggestedTranslation suggestedTranslation3 = (from x in list
			orderby x.startIndicies.Count
			select x).FirstOrDefault<GeometryWeld.SuggestedTranslation>();
			if (suggestedTranslation3 != null && suggestedTranslation3.startIndicies.Count > count / 2)
			{
				for (int m = num2 + 1; m <= num; m++)
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
	}

	// Token: 0x0600938F RID: 37775 RVA: 0x002FDABC File Offset: 0x002FBCBC
	private static void StretchTriangles(Vector3[] verticies, IEnumerable<CombineInstance> meshRanges, float strechSqrDistance)
	{
		int num = -1;
		int num2 = -1;
		foreach (CombineInstance combineInstance in meshRanges)
		{
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

	// Token: 0x06009390 RID: 37776 RVA: 0x002FDB50 File Offset: 0x002FBD50
	public void Unweld()
	{
		if (this.weldedGameObject == null || !Application.IsPlaying(this.weldedGameObject))
		{
			return;
		}
		UnityEngine.Object.Destroy(this.weldedGameObject);
		foreach (MeshRenderer meshRenderer in this.meshRenderers)
		{
			meshRenderer.enabled = true;
		}
	}

	// Token: 0x04007B9A RID: 31642
	private static readonly bool DEBUG;

	// Token: 0x04007B9B RID: 31643
	public GameObject weldedGameObject;

	// Token: 0x04007B9C RID: 31644
	private IEnumerable<MeshRenderer> meshRenderers;

	// Token: 0x020026FF RID: 9983
	protected class SuggestedTranslation
	{
		// Token: 0x060138BB RID: 80059 RVA: 0x00537B24 File Offset: 0x00535D24
		public bool MergeWith(GeometryWeld.SuggestedTranslation other, float clampErrorAngle)
		{
			if (Vector3.Angle(this.translation, other.translation) > clampErrorAngle)
			{
				return false;
			}
			this.startIndicies.AddRange(other.startIndicies);
			this.endIndicies.AddRange(other.endIndicies);
			return true;
		}

		// Token: 0x0400F2FB RID: 62203
		public Vector3 translation;

		// Token: 0x0400F2FC RID: 62204
		public List<int> startIndicies = new List<int>();

		// Token: 0x0400F2FD RID: 62205
		public List<int> endIndicies = new List<int>();
	}
}
