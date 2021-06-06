using System;
using UnityEngine;

// Token: 0x02000B38 RID: 2872
[ExecuteAlways]
public class FrameAnimation : MonoBehaviour
{
	// Token: 0x06009876 RID: 39030 RVA: 0x00315C3F File Offset: 0x00313E3F
	private void Start()
	{
		this.scaleOffsetUV = new Vector4(1f / this.tiles.x, 1f / this.tiles.y);
	}

	// Token: 0x06009877 RID: 39031 RVA: 0x00315C70 File Offset: 0x00313E70
	private void Update()
	{
		if (this.material == null)
		{
			return;
		}
		int num = Mathf.FloorToInt(this.currentFrame);
		this.scaleOffsetUV.z = (float)num % this.tiles.x;
		this.scaleOffsetUV.w = this.tiles.y - 1f - ((float)num - this.scaleOffsetUV.z) / this.tiles.x % this.tiles.y;
		this.scaleOffsetUV.z = this.scaleOffsetUV.z * this.scaleOffsetUV.x;
		this.scaleOffsetUV.w = this.scaleOffsetUV.w * this.scaleOffsetUV.y;
		this.material.SetVector(this.scaleOffsetUVParametrName, this.scaleOffsetUV);
	}

	// Token: 0x04007F74 RID: 32628
	public Vector2 tiles = Vector2.one;

	// Token: 0x04007F75 RID: 32629
	public float currentFrame;

	// Token: 0x04007F76 RID: 32630
	public Material material;

	// Token: 0x04007F77 RID: 32631
	private Vector4 scaleOffsetUV;

	// Token: 0x04007F78 RID: 32632
	private string scaleOffsetUVParametrName = "_MainTex_ST";
}
