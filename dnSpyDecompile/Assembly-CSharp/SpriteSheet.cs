using System;
using UnityEngine;

// Token: 0x02000A96 RID: 2710
public class SpriteSheet : MonoBehaviour
{
	// Token: 0x060090B8 RID: 37048 RVA: 0x002EEF94 File Offset: 0x002ED194
	private void Start()
	{
		this.m_NextFrame = Time.timeSinceLevelLoad + 1f / this.m_fps;
		this.m_renderer = base.GetComponent<Renderer>();
		if (this.m_renderer == null)
		{
			Debug.LogError("SpriteSheet needs a Renderer on: " + base.gameObject.name);
			base.enabled = false;
		}
		this.m_Size = new Vector2(1f / (float)this._uvTieX, 1f / (float)this._uvTieY);
	}

	// Token: 0x060090B9 RID: 37049 RVA: 0x002EF01C File Offset: 0x002ED21C
	private void Update()
	{
		if (this.m_Old_Mode)
		{
			int num = (int)(Time.time * this.m_fps % (float)(this._uvTieX * this._uvTieY));
			if (num != this.m_LastIdx)
			{
				Material material = this.m_renderer.GetMaterial();
				material.mainTextureOffset = new Vector2((float)(num % this._uvTieX) * this.m_Size.x, 1f - this.m_Size.y - (float)(num / this._uvTieY) * this.m_Size.y);
				material.mainTextureScale = this.m_Size;
				this.m_LastIdx = num;
				return;
			}
		}
		else
		{
			if (Time.timeSinceLevelLoad < this.m_NextFrame)
			{
				return;
			}
			this.m_X++;
			if (this.m_X > this._uvTieX - 1)
			{
				this.m_Y++;
				this.m_X = 0;
			}
			if (this.m_Y > this._uvTieY - 1)
			{
				this.m_Y = 0;
			}
			Material material2 = this.m_renderer.GetMaterial();
			material2.mainTextureOffset = new Vector2((float)this.m_X * this.m_Size.x, 1f - (float)this.m_Y * this.m_Size.y);
			material2.mainTextureScale = this.m_Size;
			this.m_NextFrame = Time.timeSinceLevelLoad + 1f / this.m_fps;
		}
	}

	// Token: 0x04007988 RID: 31112
	public int _uvTieX = 1;

	// Token: 0x04007989 RID: 31113
	public int _uvTieY = 1;

	// Token: 0x0400798A RID: 31114
	public float m_fps = 30f;

	// Token: 0x0400798B RID: 31115
	public bool m_Old_Mode;

	// Token: 0x0400798C RID: 31116
	private int m_LastIdx = -1;

	// Token: 0x0400798D RID: 31117
	private Vector2 m_Size = Vector2.one;

	// Token: 0x0400798E RID: 31118
	private float m_NextFrame;

	// Token: 0x0400798F RID: 31119
	private int m_X;

	// Token: 0x04007990 RID: 31120
	private int m_Y;

	// Token: 0x04007991 RID: 31121
	private Renderer m_renderer;
}
