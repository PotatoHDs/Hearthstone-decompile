using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AAC RID: 2732
public class UberTextMaterial
{
	// Token: 0x17000868 RID: 2152
	// (get) Token: 0x0600922F RID: 37423 RVA: 0x002F807D File Offset: 0x002F627D
	// (set) Token: 0x06009230 RID: 37424 RVA: 0x002F8085 File Offset: 0x002F6285
	public UberTextMaterialManager.MaterialType Type { get; private set; }

	// Token: 0x17000869 RID: 2153
	// (get) Token: 0x06009231 RID: 37425 RVA: 0x002F808E File Offset: 0x002F628E
	// (set) Token: 0x06009232 RID: 37426 RVA: 0x002F8096 File Offset: 0x002F6296
	public UberTextMaterialQuery Query { get; private set; }

	// Token: 0x06009233 RID: 37427 RVA: 0x002F80A0 File Offset: 0x002F62A0
	public UberTextMaterial(UberTextMaterialManager.MaterialType type, string shaderName, UberTextMaterialQuery query)
	{
		this.Type = type;
		this.Query = query.Clone();
		this.m_shader = ShaderUtils.FindShader(shaderName);
		if (!this.m_shader)
		{
			Debug.LogError("UberText Failed to load Shader: " + shaderName);
		}
		this.m_material = new Material(this.m_shader);
		this.Query.ApplyToMaterial(this.m_material);
	}

	// Token: 0x06009234 RID: 37428 RVA: 0x002F8111 File Offset: 0x002F6311
	public void Destroy()
	{
		UnityEngine.Object.Destroy(this.m_material);
	}

	// Token: 0x06009235 RID: 37429 RVA: 0x002F811E File Offset: 0x002F631E
	public bool HasQuery(UberTextMaterialQuery query)
	{
		return this.IsValid() && this.Query.Equals(query);
	}

	// Token: 0x06009236 RID: 37430 RVA: 0x002F8136 File Offset: 0x002F6336
	public Material Acquire()
	{
		this.m_referenceCounter++;
		return this.m_material;
	}

	// Token: 0x06009237 RID: 37431 RVA: 0x002F814C File Offset: 0x002F634C
	public void Release()
	{
		this.m_referenceCounter--;
	}

	// Token: 0x06009238 RID: 37432 RVA: 0x002F815C File Offset: 0x002F635C
	public bool CanDestroy()
	{
		return this.m_referenceCounter <= 0;
	}

	// Token: 0x06009239 RID: 37433 RVA: 0x002F816A File Offset: 0x002F636A
	public int GetRenderQueue()
	{
		if (this.m_material)
		{
			return this.m_material.renderQueue;
		}
		return -1;
	}

	// Token: 0x0600923A RID: 37434 RVA: 0x002F8186 File Offset: 0x002F6386
	public void SetRenderQueue(int renderQueue)
	{
		if (this.m_material)
		{
			this.m_material.renderQueue = renderQueue;
		}
	}

	// Token: 0x0600923B RID: 37435 RVA: 0x002F81A1 File Offset: 0x002F63A1
	public bool IsValid()
	{
		return this.m_material;
	}

	// Token: 0x0600923C RID: 37436 RVA: 0x002F81B0 File Offset: 0x002F63B0
	public bool IsStillBound(Renderer renderer)
	{
		UberTextMaterial.m_temporalRendererMaterialList.Clear();
		renderer.GetSharedMaterials(UberTextMaterial.m_temporalRendererMaterialList);
		using (List<Material>.Enumerator enumerator = UberTextMaterial.m_temporalRendererMaterialList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == this.m_material)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600923D RID: 37437 RVA: 0x002F8224 File Offset: 0x002F6424
	public void Rebound(Renderer renderer, int index)
	{
		UberTextMaterial.m_temporalRendererMaterialList.Clear();
		renderer.GetSharedMaterials(UberTextMaterial.m_temporalRendererMaterialList);
		if (UberTextMaterial.m_temporalRendererMaterialList.Count > index)
		{
			UberTextMaterial.m_temporalRendererMaterialList[index] = this.m_material;
		}
		else
		{
			UberTextMaterial.m_temporalRendererMaterialList.Add(this.m_material);
		}
		renderer.SetSharedMaterials(UberTextMaterial.m_temporalRendererMaterialList.ToArray());
	}

	// Token: 0x0600923E RID: 37438 RVA: 0x002F8288 File Offset: 0x002F6488
	public void Unbound(Renderer renderer)
	{
		UberTextMaterial.m_temporalRendererMaterialList.Clear();
		renderer.GetSharedMaterials(UberTextMaterial.m_temporalRendererMaterialList);
		int num = -1;
		for (int i = UberTextMaterial.m_temporalRendererMaterialList.Count - 1; i >= 0; i--)
		{
			if (UberTextMaterial.m_temporalRendererMaterialList[i] == this.m_material)
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			UberTextMaterial.m_temporalRendererMaterialList.RemoveAt(num);
			renderer.SetSharedMaterials(UberTextMaterial.m_temporalRendererMaterialList.ToArray());
		}
	}

	// Token: 0x04007AB9 RID: 31417
	public const int INVALID_RENDER_QUEUE = -1;

	// Token: 0x04007ABC RID: 31420
	protected Shader m_shader;

	// Token: 0x04007ABD RID: 31421
	protected Material m_material;

	// Token: 0x04007ABE RID: 31422
	protected int m_referenceCounter;

	// Token: 0x04007ABF RID: 31423
	private static List<Material> m_temporalRendererMaterialList = new List<Material>();
}
