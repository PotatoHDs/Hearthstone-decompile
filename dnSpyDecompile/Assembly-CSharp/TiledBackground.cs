using System;
using UnityEngine;

// Token: 0x02000AE5 RID: 2789
public class TiledBackground : MonoBehaviour
{
	// Token: 0x17000876 RID: 2166
	// (get) Token: 0x06009486 RID: 38022 RVA: 0x003026E9 File Offset: 0x003008E9
	private Renderer TiledRenderer
	{
		get
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer;
		}
	}

	// Token: 0x17000877 RID: 2167
	// (get) Token: 0x06009487 RID: 38023 RVA: 0x0030270B File Offset: 0x0030090B
	private Material TiledMaterial
	{
		get
		{
			if (this.m_material == null && this.TiledRenderer != null)
			{
				this.m_material = this.TiledRenderer.GetMaterial();
			}
			return this.m_material;
		}
	}

	// Token: 0x17000878 RID: 2168
	// (get) Token: 0x06009488 RID: 38024 RVA: 0x00302740 File Offset: 0x00300940
	// (set) Token: 0x06009489 RID: 38025 RVA: 0x003027A4 File Offset: 0x003009A4
	public Vector2 Offset
	{
		get
		{
			if (this.TiledMaterial == null)
			{
				return Vector2.zero;
			}
			Vector3 vector = this.TiledMaterial.mainTextureOffset;
			Vector3 vector2 = this.TiledMaterial.mainTextureScale;
			return new Vector2(vector.x / vector2.x, vector.y / vector2.y);
		}
		set
		{
			if (this.TiledMaterial == null)
			{
				return;
			}
			Vector3 vector = this.TiledMaterial.mainTextureScale;
			this.TiledMaterial.mainTextureOffset = new Vector2(vector.x * value.x, vector.y * value.y);
		}
	}

	// Token: 0x0600948A RID: 38026 RVA: 0x003027FB File Offset: 0x003009FB
	private void Awake()
	{
		if (this.TiledMaterial == null)
		{
			Debug.LogError("TiledBackground requires the mesh renderer and for it to have a material!");
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x0600948B RID: 38027 RVA: 0x0030281C File Offset: 0x00300A1C
	public void SetBounds(Bounds bounds)
	{
		if (this.TiledRenderer == null)
		{
			Debug.LogError("TiledBackground.SetBounds - no renderer was found on this game object!");
			return;
		}
		base.transform.localScale = Vector3.one;
		Bounds bounds2 = this.TiledRenderer.bounds;
		Vector3 vector = bounds2.min;
		Vector3 vector2 = bounds2.max;
		if (base.transform.parent != null)
		{
			vector = base.transform.parent.InverseTransformPoint(vector);
			vector2 = base.transform.parent.InverseTransformPoint(vector2);
		}
		Vector3 vector3 = VectorUtils.Abs(vector2 - vector);
		Vector3 vector4 = new Vector3((vector3.x > 0f) ? (bounds.size.x / vector3.x) : 0f, (vector3.y > 0f) ? (bounds.size.y / vector3.y) : 0f, (vector3.z > 0f) ? (bounds.size.z / vector3.z) : 0f);
		base.transform.localScale = vector4;
		base.transform.localPosition = bounds.center + new Vector3(0f, 0f, -this.Depth);
		if (this.TiledMaterial == null)
		{
			Debug.LogError("TiledBackground.SetBounds - no material was found on this component!");
			return;
		}
		this.TiledMaterial.mainTextureScale = vector4;
	}

	// Token: 0x04007C7D RID: 31869
	private Renderer m_renderer;

	// Token: 0x04007C7E RID: 31870
	private Material m_material;

	// Token: 0x04007C7F RID: 31871
	public float Depth;
}
