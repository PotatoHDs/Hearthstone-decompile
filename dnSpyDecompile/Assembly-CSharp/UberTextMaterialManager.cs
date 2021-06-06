using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AAD RID: 2733
public class UberTextMaterialManager
{
	// Token: 0x06009240 RID: 37440 RVA: 0x002F830C File Offset: 0x002F650C
	private UberTextMaterialManager()
	{
		Array values = Enum.GetValues(typeof(UberTextMaterialManager.MaterialType));
		this.m_references = new List<UberTextMaterialManager.MaterialTypeReference>(values.Length);
		foreach (object obj in values)
		{
			this.m_references.Add(new UberTextMaterialManager.MaterialTypeReference((UberTextMaterialManager.MaterialType)obj));
		}
		this.m_availableRenderQueues = new List<int>(values.Length * 10);
	}

	// Token: 0x06009241 RID: 37441 RVA: 0x002F83A8 File Offset: 0x002F65A8
	public static UberTextMaterialManager Get()
	{
		if (UberTextMaterialManager.m_instance == null)
		{
			UberTextMaterialManager.m_instance = new UberTextMaterialManager();
		}
		return UberTextMaterialManager.m_instance;
	}

	// Token: 0x06009242 RID: 37442 RVA: 0x002F83C0 File Offset: 0x002F65C0
	public UberTextMaterial FetchMaterial(UberTextMaterialQuery query)
	{
		UberTextMaterial uberTextMaterial = this.FindMaterial(query);
		if (uberTextMaterial == null)
		{
			uberTextMaterial = this.CreateMaterialWithProperties(query);
		}
		else if (!uberTextMaterial.IsValid())
		{
			uberTextMaterial = this.CreateMaterialWithProperties(query);
		}
		return uberTextMaterial;
	}

	// Token: 0x06009243 RID: 37443 RVA: 0x002F83F3 File Offset: 0x002F65F3
	public void UnboundMaterial(UberTextMaterial material, Renderer renderer)
	{
		if (material != null && renderer)
		{
			material.Unbound(renderer);
			this.ReleaseMaterial(material);
		}
	}

	// Token: 0x06009244 RID: 37444 RVA: 0x002F8410 File Offset: 0x002F6610
	public void ReleaseMaterial(UberTextMaterial material)
	{
		if (material == null)
		{
			return;
		}
		material.Release();
		if (!material.CanDestroy())
		{
			return;
		}
		int renderQueue = material.GetRenderQueue();
		material.Destroy();
		UberTextMaterialManager.MaterialType type = material.Type;
		foreach (UberTextMaterialManager.MaterialTypeReference materialTypeReference in this.m_references)
		{
			if (materialTypeReference.Type == type)
			{
				this.FreeRenderQueueValue(renderQueue);
				this.RemoveMaterialFromList(materialTypeReference.Materials, material);
			}
		}
	}

	// Token: 0x06009245 RID: 37445 RVA: 0x002F84A0 File Offset: 0x002F66A0
	private UberTextMaterial CreateMaterialWithProperties(UberTextMaterialQuery query)
	{
		UberTextMaterial uberTextMaterial = null;
		foreach (UberTextMaterialManager.MaterialTypeReference materialTypeReference in this.m_references)
		{
			if (materialTypeReference.Type == query.Type)
			{
				uberTextMaterial = this.CreateUberTextMaterial(query);
				materialTypeReference.Materials.Add(uberTextMaterial);
				break;
			}
		}
		return uberTextMaterial;
	}

	// Token: 0x06009246 RID: 37446 RVA: 0x002F8514 File Offset: 0x002F6714
	private UberTextMaterial FindMaterial(UberTextMaterialQuery query)
	{
		UberTextMaterial result = null;
		foreach (UberTextMaterialManager.MaterialTypeReference materialTypeReference in this.m_references)
		{
			if (materialTypeReference.Type == query.Type)
			{
				using (List<UberTextMaterial>.Enumerator enumerator2 = materialTypeReference.Materials.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						UberTextMaterial uberTextMaterial = enumerator2.Current;
						if (uberTextMaterial.HasQuery(query))
						{
							result = uberTextMaterial;
							break;
						}
					}
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06009247 RID: 37447 RVA: 0x002F85BC File Offset: 0x002F67BC
	private UberTextMaterial CreateUberTextMaterial(UberTextMaterialQuery query)
	{
		UberTextMaterial result = null;
		if (query.Type == UberTextMaterialManager.MaterialType.TEXT)
		{
			result = new UberTextMaterial(UberTextMaterialManager.MaterialType.TEXT, UberTextMaterialManager.TEXT_SHADER_NAME, query);
		}
		else if (query.Type == UberTextMaterialManager.MaterialType.TEXT_OUTLINE)
		{
			OutlineTextMaterialQuery outlineTextMaterialQuery = query as OutlineTextMaterialQuery;
			if (outlineTextMaterialQuery != null)
			{
				string shaderName = UberTextMaterialManager.OUTLINE_TEXT_SHADER_NAME;
				if (outlineTextMaterialQuery.Locale == Locale.thTH)
				{
					shaderName = UberTextMaterialManager.OUTLINE_TEXT_2PASS_SHADER_NAME;
				}
				if (!outlineTextMaterialQuery.RichTextEnabled)
				{
					if (outlineTextMaterialQuery.Locale == Locale.thTH)
					{
						shaderName = UberTextMaterialManager.OUTLINE_NO_VERT_COLOR_TEXT_2PASS_SHADER_NAME;
					}
					else
					{
						shaderName = UberTextMaterialManager.OUTLINE_NO_VERT_COLOR_TEXT_SHADER_NAME;
					}
				}
				result = new UberTextMaterial(UberTextMaterialManager.MaterialType.TEXT_OUTLINE, shaderName, query);
			}
		}
		else if (query.Type == UberTextMaterialManager.MaterialType.BOLD)
		{
			result = new UberTextMaterial(UberTextMaterialManager.MaterialType.BOLD, UberTextMaterialManager.BOLD_SHADER_NAME, query);
		}
		else if (query.Type == UberTextMaterialManager.MaterialType.BOLD_OUTLINE)
		{
			result = new UberTextMaterial(UberTextMaterialManager.MaterialType.BOLD_OUTLINE, UberTextMaterialManager.BOLD_OUTLINE_TEXT_SHADER_NAME, query);
		}
		else if (query.Type == UberTextMaterialManager.MaterialType.TEXT_ANTIALIASING)
		{
			result = new UberTextMaterial(UberTextMaterialManager.MaterialType.TEXT_ANTIALIASING, UberTextMaterialManager.TEXT_ANTIALAISING_SHADER_NAME, query);
		}
		else if (query.Type == UberTextMaterialManager.MaterialType.INLINE_IMAGE)
		{
			result = new UberTextMaterial(UberTextMaterialManager.MaterialType.INLINE_IMAGE, UberTextMaterialManager.INLINE_IMAGE_SHADER_NAME, query);
		}
		else if (query.Type == UberTextMaterialManager.MaterialType.SHADOW)
		{
			result = new UberTextMaterial(UberTextMaterialManager.MaterialType.SHADOW, UberTextMaterialManager.SHADOW_SHADER_NAME, query);
		}
		else if (query.Type == UberTextMaterialManager.MaterialType.PLANE)
		{
			result = new UberTextMaterial(UberTextMaterialManager.MaterialType.PLANE, UberTextMaterialManager.PLANE_SHADER_NAME, query);
		}
		return result;
	}

	// Token: 0x06009248 RID: 37448 RVA: 0x002F86D0 File Offset: 0x002F68D0
	private void RemoveMaterialFromList(List<UberTextMaterial> materialList, UberTextMaterial material)
	{
		for (int i = materialList.Count - 1; i >= 0; i--)
		{
			if (materialList[i] == material)
			{
				materialList.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06009249 RID: 37449 RVA: 0x002F8704 File Offset: 0x002F6904
	private void AssignNextAvailableQueue(UberTextMaterial material)
	{
		int num = material.GetRenderQueue();
		int minimalRenderQueueValue = material.Query.GetMinimalRenderQueueValue();
		if (num != -1)
		{
			this.m_availableRenderQueues.Sort();
			foreach (int num2 in this.m_availableRenderQueues)
			{
				if (num2 == num || num2 < minimalRenderQueueValue)
				{
					num++;
				}
				else if (num2 > num)
				{
					break;
				}
			}
			this.m_availableRenderQueues.Add(num);
			material.SetRenderQueue(num);
		}
	}

	// Token: 0x0600924A RID: 37450 RVA: 0x002F879C File Offset: 0x002F699C
	private void FreeRenderQueueValue(int renderQueueValue)
	{
		if (renderQueueValue == -1)
		{
			return;
		}
		for (int i = this.m_availableRenderQueues.Count - 1; i >= 0; i--)
		{
			if (this.m_availableRenderQueues[i] == renderQueueValue)
			{
				this.m_availableRenderQueues.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x04007AC0 RID: 31424
	private static readonly string TEXT_SHADER_NAME = "Hero/Text_Unlit";

	// Token: 0x04007AC1 RID: 31425
	private static readonly string BOLD_SHADER_NAME = "Hidden/Text_Bold";

	// Token: 0x04007AC2 RID: 31426
	private static readonly string BOLD_OUTLINE_TEXT_SHADER_NAME = "Hidden/TextBoldOutline_Unlit";

	// Token: 0x04007AC3 RID: 31427
	private static readonly string OUTLINE_TEXT_SHADER_NAME = "Hidden/TextOutline_Unlit";

	// Token: 0x04007AC4 RID: 31428
	private static readonly string OUTLINE_TEXT_2PASS_SHADER_NAME = "Hidden/TextOutline_Unlit_2pass";

	// Token: 0x04007AC5 RID: 31429
	private static readonly string OUTLINE_NO_VERT_COLOR_TEXT_SHADER_NAME = "Hidden/TextOutline_Unlit_NoVertColor";

	// Token: 0x04007AC6 RID: 31430
	private static readonly string OUTLINE_NO_VERT_COLOR_TEXT_2PASS_SHADER_NAME = "Hidden/TextOutline_Unlit_NoVertColor_2pass";

	// Token: 0x04007AC7 RID: 31431
	private static readonly string TEXT_ANTIALAISING_SHADER_NAME = "Hidden/TextAntialiasing";

	// Token: 0x04007AC8 RID: 31432
	private static readonly string INLINE_IMAGE_SHADER_NAME = "Hero/Unlit_Transparent";

	// Token: 0x04007AC9 RID: 31433
	private static readonly string SHADOW_SHADER_NAME = "Hidden/TextShadow";

	// Token: 0x04007ACA RID: 31434
	private static readonly string PLANE_SHADER_NAME = "Hidden/TextPlane";

	// Token: 0x04007ACB RID: 31435
	private const int EXPECTED_NUMBER_QUEUES_PER_TYPE = 10;

	// Token: 0x04007ACC RID: 31436
	private static UberTextMaterialManager m_instance;

	// Token: 0x04007ACD RID: 31437
	private List<UberTextMaterialManager.MaterialTypeReference> m_references;

	// Token: 0x04007ACE RID: 31438
	private List<int> m_availableRenderQueues;

	// Token: 0x020026ED RID: 9965
	public enum MaterialType
	{
		// Token: 0x0400F299 RID: 62105
		TEXT,
		// Token: 0x0400F29A RID: 62106
		TEXT_OUTLINE,
		// Token: 0x0400F29B RID: 62107
		BOLD,
		// Token: 0x0400F29C RID: 62108
		BOLD_OUTLINE,
		// Token: 0x0400F29D RID: 62109
		TEXT_ANTIALIASING,
		// Token: 0x0400F29E RID: 62110
		INLINE_IMAGE,
		// Token: 0x0400F29F RID: 62111
		SHADOW,
		// Token: 0x0400F2A0 RID: 62112
		PLANE
	}

	// Token: 0x020026EE RID: 9966
	private struct MaterialTypeReference
	{
		// Token: 0x060138AB RID: 80043 RVA: 0x00537A30 File Offset: 0x00535C30
		public MaterialTypeReference(UberTextMaterialManager.MaterialType t)
		{
			this.Type = t;
			this.Materials = new List<UberTextMaterial>();
		}

		// Token: 0x0400F2A1 RID: 62113
		public UberTextMaterialManager.MaterialType Type;

		// Token: 0x0400F2A2 RID: 62114
		public List<UberTextMaterial> Materials;
	}
}
