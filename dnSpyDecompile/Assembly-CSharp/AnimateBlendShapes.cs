using System;
using UnityEngine;

// Token: 0x02000A07 RID: 2567
[ExecuteAlways]
public class AnimateBlendShapes : MonoBehaviour
{
	// Token: 0x06008B12 RID: 35602 RVA: 0x002C799D File Offset: 0x002C5B9D
	private void Start()
	{
		this.skinMR = base.GetComponent<SkinnedMeshRenderer>();
	}

	// Token: 0x06008B13 RID: 35603 RVA: 0x002C79AB File Offset: 0x002C5BAB
	private void Update()
	{
		if (this.prevBlendAmount == this.blendAmount)
		{
			return;
		}
		this.prevBlendAmount = this.blendAmount;
		this.skinMR.SetBlendShapeWeight(this.index, this.blendAmount);
	}

	// Token: 0x04007398 RID: 29592
	private float prevBlendAmount;

	// Token: 0x04007399 RID: 29593
	public float blendAmount;

	// Token: 0x0400739A RID: 29594
	public int index;

	// Token: 0x0400739B RID: 29595
	private SkinnedMeshRenderer skinMR;
}
