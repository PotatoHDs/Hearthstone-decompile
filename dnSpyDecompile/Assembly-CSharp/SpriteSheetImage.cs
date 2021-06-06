using System;
using UnityEngine;

// Token: 0x02000B06 RID: 2822
public class SpriteSheetImage : MonoBehaviour
{
	// Token: 0x06009638 RID: 38456 RVA: 0x0030A3AB File Offset: 0x003085AB
	private void Start()
	{
		this.UpdateSprite();
	}

	// Token: 0x06009639 RID: 38457 RVA: 0x0030A3B4 File Offset: 0x003085B4
	private void UpdateSprite()
	{
		this.spriteMaterial = base.GetComponent<MeshRenderer>().GetMaterial();
		if (this.spriteMaterial == null)
		{
			return;
		}
		float x = this.spriteMaterial.mainTextureScale.x;
		float y = this.spriteMaterial.mainTextureScale.y;
		int num = (int)(1f / x);
		float x2;
		if (this.spriteIndex > num)
		{
			x2 = (float)(this.spriteIndex % num) * x;
		}
		else
		{
			x2 = (float)this.spriteIndex * x;
		}
		float y2 = 1f - ((float)Mathf.CeilToInt((float)(this.spriteIndex / num)) * y + y);
		this.spriteMaterial.mainTextureOffset = new Vector2(x2, y2);
		this.spriteMaterial.renderQueue += 1000;
	}

	// Token: 0x04007DE9 RID: 32233
	public int spriteIndex;

	// Token: 0x04007DEA RID: 32234
	private Material spriteMaterial;
}
