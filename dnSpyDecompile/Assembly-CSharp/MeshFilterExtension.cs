using System;
using UnityEngine;

// Token: 0x0200083C RID: 2108
public static class MeshFilterExtension
{
	// Token: 0x06007089 RID: 28809 RVA: 0x00244D4C File Offset: 0x00242F4C
	public static void SetMesh(this MeshFilter meshFilter, Mesh mesh)
	{
		meshFilter.mesh = mesh;
	}

	// Token: 0x0600708A RID: 28810 RVA: 0x00244D55 File Offset: 0x00242F55
	public static Mesh GetMesh(this MeshFilter meshFilter)
	{
		return meshFilter.mesh;
	}
}
