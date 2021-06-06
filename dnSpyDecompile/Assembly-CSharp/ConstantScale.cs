using System;
using UnityEngine;

// Token: 0x020007DA RID: 2010
public class ConstantScale : MonoBehaviour
{
	// Token: 0x06006E3C RID: 28220 RVA: 0x00238E6C File Offset: 0x0023706C
	private void LateUpdate()
	{
		if (!this.everyFrame)
		{
			if (!this.isItFirstIteration)
			{
				return;
			}
			this.isItFirstIteration = false;
		}
		Vector3 vector = Vector3.one;
		if (base.transform.parent != null)
		{
			vector = base.transform.parent.transform.lossyScale;
		}
		if (vector.x + vector.y + vector.z == 0f)
		{
			vector = new Vector3(1E-05f, 1E-05f, 1E-05f);
		}
		base.transform.localScale = Vector3.Scale(new Vector3(1f / vector.x, 1f / vector.y, 1f / vector.z), this.scale);
	}

	// Token: 0x04005879 RID: 22649
	public Vector3 scale = Vector3.one;

	// Token: 0x0400587A RID: 22650
	public bool everyFrame;

	// Token: 0x0400587B RID: 22651
	private bool isItFirstIteration = true;
}
