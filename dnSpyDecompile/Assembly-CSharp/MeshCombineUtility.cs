using System;
using UnityEngine;

// Token: 0x020009C6 RID: 2502
public class MeshCombineUtility
{
	// Token: 0x060088A8 RID: 34984 RVA: 0x002C01B0 File Offset: 0x002BE3B0
	public static Mesh Combine(MeshCombineUtility.MeshInstance[] combines, bool generateStrips)
	{
		int num = 0;
		int num2 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance in combines)
		{
			if (meshInstance.mesh)
			{
				num += meshInstance.mesh.vertexCount;
			}
		}
		foreach (MeshCombineUtility.MeshInstance meshInstance2 in combines)
		{
			if (meshInstance2.mesh)
			{
				num2 += meshInstance2.mesh.GetTriangles(meshInstance2.subMeshIndex).Length;
			}
		}
		Vector3[] array = new Vector3[num];
		Vector3[] array2 = new Vector3[num];
		Vector4[] array3 = new Vector4[num];
		Vector2[] array4 = new Vector2[num];
		Vector2[] array5 = new Vector2[num];
		Color[] array6 = new Color[num];
		int[] array7 = new int[num2];
		int num3 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance3 in combines)
		{
			if (meshInstance3.mesh)
			{
				MeshCombineUtility.Copy(meshInstance3.mesh.vertexCount, meshInstance3.mesh.vertices, array, ref num3, meshInstance3.transform);
			}
		}
		num3 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance4 in combines)
		{
			if (meshInstance4.mesh)
			{
				Matrix4x4 transform = meshInstance4.transform;
				transform = transform.inverse.transpose;
				MeshCombineUtility.CopyNormal(meshInstance4.mesh.vertexCount, meshInstance4.mesh.normals, array2, ref num3, transform);
			}
		}
		num3 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance5 in combines)
		{
			if (meshInstance5.mesh)
			{
				Matrix4x4 transform2 = meshInstance5.transform;
				transform2 = transform2.inverse.transpose;
				MeshCombineUtility.CopyTangents(meshInstance5.mesh.vertexCount, meshInstance5.mesh.tangents, array3, ref num3, transform2);
			}
		}
		num3 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance6 in combines)
		{
			if (meshInstance6.mesh)
			{
				MeshCombineUtility.Copy(meshInstance6.mesh.vertexCount, meshInstance6.mesh.uv, array4, ref num3);
			}
		}
		num3 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance7 in combines)
		{
			if (meshInstance7.mesh)
			{
				MeshCombineUtility.Copy(meshInstance7.mesh.vertexCount, meshInstance7.mesh.uv2, array5, ref num3);
			}
		}
		num3 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance8 in combines)
		{
			if (meshInstance8.mesh)
			{
				MeshCombineUtility.CopyColors(meshInstance8.mesh.vertexCount, meshInstance8.mesh.colors, array6, ref num3);
			}
		}
		int num4 = 0;
		int num5 = 0;
		foreach (MeshCombineUtility.MeshInstance meshInstance9 in combines)
		{
			if (meshInstance9.mesh)
			{
				int[] triangles = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
				for (int j = 0; j < triangles.Length; j++)
				{
					array7[j + num4] = triangles[j] + num5;
				}
				num4 += triangles.Length;
				num5 += meshInstance9.mesh.vertexCount;
			}
		}
		return new Mesh
		{
			name = "Combined Mesh",
			vertices = array,
			normals = array2,
			colors = array6,
			uv = array4,
			uv2 = array5,
			tangents = array3,
			triangles = array7
		};
	}

	// Token: 0x060088A9 RID: 34985 RVA: 0x002C057C File Offset: 0x002BE77C
	private static void Copy(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = transform.MultiplyPoint(src[i]);
		}
		offset += vertexcount;
	}

	// Token: 0x060088AA RID: 34986 RVA: 0x002C05B8 File Offset: 0x002BE7B8
	private static void CopyNormal(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = transform.MultiplyVector(src[i]).normalized;
		}
		offset += vertexcount;
	}

	// Token: 0x060088AB RID: 34987 RVA: 0x002C05FC File Offset: 0x002BE7FC
	private static void Copy(int vertexcount, Vector2[] src, Vector2[] dst, ref int offset)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = src[i];
		}
		offset += vertexcount;
	}

	// Token: 0x060088AC RID: 34988 RVA: 0x002C0630 File Offset: 0x002BE830
	private static void CopyColors(int vertexcount, Color[] src, Color[] dst, ref int offset)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = src[i];
		}
		offset += vertexcount;
	}

	// Token: 0x060088AD RID: 34989 RVA: 0x002C0664 File Offset: 0x002BE864
	private static void CopyTangents(int vertexcount, Vector4[] src, Vector4[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			Vector4 vector = src[i];
			Vector3 normalized = new Vector3(vector.x, vector.y, vector.z);
			normalized = transform.MultiplyVector(normalized).normalized;
			dst[i + offset] = new Vector4(normalized.x, normalized.y, normalized.z, vector.w);
		}
		offset += vertexcount;
	}

	// Token: 0x0200267C RID: 9852
	public struct MeshInstance
	{
		// Token: 0x0400F0BD RID: 61629
		public Mesh mesh;

		// Token: 0x0400F0BE RID: 61630
		public int subMeshIndex;

		// Token: 0x0400F0BF RID: 61631
		public Matrix4x4 transform;
	}
}
