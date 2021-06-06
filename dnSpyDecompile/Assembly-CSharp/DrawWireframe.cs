using System;
using UnityEngine;

// Token: 0x02000A28 RID: 2600
public class DrawWireframe : MonoBehaviour
{
	// Token: 0x06008BE1 RID: 35809 RVA: 0x002CC6EC File Offset: 0x002CA8EC
	private void OnPreRender()
	{
		GL.wireframe = true;
	}

	// Token: 0x06008BE2 RID: 35810 RVA: 0x002CC6F4 File Offset: 0x002CA8F4
	private void OnPostRender()
	{
		GL.wireframe = false;
	}
}
