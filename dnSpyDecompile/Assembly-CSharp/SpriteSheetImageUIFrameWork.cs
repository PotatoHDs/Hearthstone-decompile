using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000B07 RID: 2823
public class SpriteSheetImageUIFrameWork : MonoBehaviour
{
	// Token: 0x1700088A RID: 2186
	// (get) Token: 0x0600963B RID: 38459 RVA: 0x0030A478 File Offset: 0x00308678
	// (set) Token: 0x0600963C RID: 38460 RVA: 0x0030A480 File Offset: 0x00308680
	[Overridable]
	public int spriteIndex
	{
		get
		{
			return this.m_spriteIndex;
		}
		set
		{
			this.m_spriteIndex = value;
			this.UpdateSprite();
		}
	}

	// Token: 0x0600963D RID: 38461 RVA: 0x0030A48F File Offset: 0x0030868F
	private void Start()
	{
		this.UpdateSprite();
	}

	// Token: 0x0600963E RID: 38462 RVA: 0x0030A498 File Offset: 0x00308698
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

	// Token: 0x04007DEB RID: 32235
	private int m_spriteIndex;

	// Token: 0x04007DEC RID: 32236
	private Material spriteMaterial;
}
