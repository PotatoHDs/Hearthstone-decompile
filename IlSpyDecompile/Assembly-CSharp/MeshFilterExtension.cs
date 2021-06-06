using UnityEngine;

public static class MeshFilterExtension
{
	public static void SetMesh(this MeshFilter meshFilter, Mesh mesh)
	{
		meshFilter.mesh = mesh;
	}

	public static Mesh GetMesh(this MeshFilter meshFilter)
	{
		return meshFilter.mesh;
	}
}
