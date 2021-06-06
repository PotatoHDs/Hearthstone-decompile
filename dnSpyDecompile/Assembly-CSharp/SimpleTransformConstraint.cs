using System;
using UnityEngine;

// Token: 0x02000820 RID: 2080
[ExecuteInEditMode]
public class SimpleTransformConstraint : MonoBehaviour
{
	// Token: 0x06006FEF RID: 28655 RVA: 0x00241ABC File Offset: 0x0023FCBC
	private void Update()
	{
		if (this.position)
		{
			base.transform.position = this.parents[this.currentParent].position;
		}
		if (this.rotation)
		{
			base.transform.rotation = this.parents[this.currentParent].rotation;
		}
		if (this.scale)
		{
			base.transform.localScale = this.parents[this.currentParent].localScale;
		}
	}

	// Token: 0x040059C0 RID: 22976
	public int currentParent;

	// Token: 0x040059C1 RID: 22977
	public Transform[] parents;

	// Token: 0x040059C2 RID: 22978
	public bool position = true;

	// Token: 0x040059C3 RID: 22979
	public bool rotation = true;

	// Token: 0x040059C4 RID: 22980
	public bool scale = true;
}
