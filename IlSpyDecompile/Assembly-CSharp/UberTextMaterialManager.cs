using System;
using System.Collections.Generic;
using UnityEngine;

public class UberTextMaterialManager
{
	public enum MaterialType
	{
		TEXT,
		TEXT_OUTLINE,
		BOLD,
		BOLD_OUTLINE,
		TEXT_ANTIALIASING,
		INLINE_IMAGE,
		SHADOW,
		PLANE
	}

	private struct MaterialTypeReference
	{
		public MaterialType Type;

		public List<UberTextMaterial> Materials;

		public MaterialTypeReference(MaterialType t)
		{
			Type = t;
			Materials = new List<UberTextMaterial>();
		}
	}

	private static readonly string TEXT_SHADER_NAME = "Hero/Text_Unlit";

	private static readonly string BOLD_SHADER_NAME = "Hidden/Text_Bold";

	private static readonly string BOLD_OUTLINE_TEXT_SHADER_NAME = "Hidden/TextBoldOutline_Unlit";

	private static readonly string OUTLINE_TEXT_SHADER_NAME = "Hidden/TextOutline_Unlit";

	private static readonly string OUTLINE_TEXT_2PASS_SHADER_NAME = "Hidden/TextOutline_Unlit_2pass";

	private static readonly string OUTLINE_NO_VERT_COLOR_TEXT_SHADER_NAME = "Hidden/TextOutline_Unlit_NoVertColor";

	private static readonly string OUTLINE_NO_VERT_COLOR_TEXT_2PASS_SHADER_NAME = "Hidden/TextOutline_Unlit_NoVertColor_2pass";

	private static readonly string TEXT_ANTIALAISING_SHADER_NAME = "Hidden/TextAntialiasing";

	private static readonly string INLINE_IMAGE_SHADER_NAME = "Hero/Unlit_Transparent";

	private static readonly string SHADOW_SHADER_NAME = "Hidden/TextShadow";

	private static readonly string PLANE_SHADER_NAME = "Hidden/TextPlane";

	private const int EXPECTED_NUMBER_QUEUES_PER_TYPE = 10;

	private static UberTextMaterialManager m_instance;

	private List<MaterialTypeReference> m_references;

	private List<int> m_availableRenderQueues;

	private UberTextMaterialManager()
	{
		Array values = Enum.GetValues(typeof(MaterialType));
		m_references = new List<MaterialTypeReference>(values.Length);
		foreach (object item in values)
		{
			m_references.Add(new MaterialTypeReference((MaterialType)item));
		}
		m_availableRenderQueues = new List<int>(values.Length * 10);
	}

	public static UberTextMaterialManager Get()
	{
		if (m_instance == null)
		{
			m_instance = new UberTextMaterialManager();
		}
		return m_instance;
	}

	public UberTextMaterial FetchMaterial(UberTextMaterialQuery query)
	{
		UberTextMaterial uberTextMaterial = FindMaterial(query);
		if (uberTextMaterial == null)
		{
			uberTextMaterial = CreateMaterialWithProperties(query);
		}
		else if (!uberTextMaterial.IsValid())
		{
			uberTextMaterial = CreateMaterialWithProperties(query);
		}
		return uberTextMaterial;
	}

	public void UnboundMaterial(UberTextMaterial material, Renderer renderer)
	{
		if (material != null && (bool)renderer)
		{
			material.Unbound(renderer);
			ReleaseMaterial(material);
		}
	}

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
		MaterialType type = material.Type;
		foreach (MaterialTypeReference reference in m_references)
		{
			if (reference.Type == type)
			{
				FreeRenderQueueValue(renderQueue);
				RemoveMaterialFromList(reference.Materials, material);
			}
		}
	}

	private UberTextMaterial CreateMaterialWithProperties(UberTextMaterialQuery query)
	{
		UberTextMaterial result = null;
		foreach (MaterialTypeReference reference in m_references)
		{
			if (reference.Type == query.Type)
			{
				result = CreateUberTextMaterial(query);
				reference.Materials.Add(result);
				return result;
			}
		}
		return result;
	}

	private UberTextMaterial FindMaterial(UberTextMaterialQuery query)
	{
		UberTextMaterial result = null;
		foreach (MaterialTypeReference reference in m_references)
		{
			if (reference.Type != query.Type)
			{
				continue;
			}
			{
				foreach (UberTextMaterial material in reference.Materials)
				{
					if (material.HasQuery(query))
					{
						return material;
					}
				}
				return result;
			}
		}
		return result;
	}

	private UberTextMaterial CreateUberTextMaterial(UberTextMaterialQuery query)
	{
		UberTextMaterial result = null;
		if (query.Type == MaterialType.TEXT)
		{
			result = new UberTextMaterial(MaterialType.TEXT, TEXT_SHADER_NAME, query);
		}
		else if (query.Type == MaterialType.TEXT_OUTLINE)
		{
			OutlineTextMaterialQuery outlineTextMaterialQuery = query as OutlineTextMaterialQuery;
			if (outlineTextMaterialQuery != null)
			{
				string shaderName = OUTLINE_TEXT_SHADER_NAME;
				if (outlineTextMaterialQuery.Locale == Locale.thTH)
				{
					shaderName = OUTLINE_TEXT_2PASS_SHADER_NAME;
				}
				if (!outlineTextMaterialQuery.RichTextEnabled)
				{
					shaderName = ((outlineTextMaterialQuery.Locale != Locale.thTH) ? OUTLINE_NO_VERT_COLOR_TEXT_SHADER_NAME : OUTLINE_NO_VERT_COLOR_TEXT_2PASS_SHADER_NAME);
				}
				result = new UberTextMaterial(MaterialType.TEXT_OUTLINE, shaderName, query);
			}
		}
		else if (query.Type == MaterialType.BOLD)
		{
			result = new UberTextMaterial(MaterialType.BOLD, BOLD_SHADER_NAME, query);
		}
		else if (query.Type == MaterialType.BOLD_OUTLINE)
		{
			result = new UberTextMaterial(MaterialType.BOLD_OUTLINE, BOLD_OUTLINE_TEXT_SHADER_NAME, query);
		}
		else if (query.Type == MaterialType.TEXT_ANTIALIASING)
		{
			result = new UberTextMaterial(MaterialType.TEXT_ANTIALIASING, TEXT_ANTIALAISING_SHADER_NAME, query);
		}
		else if (query.Type == MaterialType.INLINE_IMAGE)
		{
			result = new UberTextMaterial(MaterialType.INLINE_IMAGE, INLINE_IMAGE_SHADER_NAME, query);
		}
		else if (query.Type == MaterialType.SHADOW)
		{
			result = new UberTextMaterial(MaterialType.SHADOW, SHADOW_SHADER_NAME, query);
		}
		else if (query.Type == MaterialType.PLANE)
		{
			result = new UberTextMaterial(MaterialType.PLANE, PLANE_SHADER_NAME, query);
		}
		return result;
	}

	private void RemoveMaterialFromList(List<UberTextMaterial> materialList, UberTextMaterial material)
	{
		for (int num = materialList.Count - 1; num >= 0; num--)
		{
			if (materialList[num] == material)
			{
				materialList.RemoveAt(num);
				break;
			}
		}
	}

	private void AssignNextAvailableQueue(UberTextMaterial material)
	{
		int num = material.GetRenderQueue();
		int minimalRenderQueueValue = material.Query.GetMinimalRenderQueueValue();
		if (num == -1)
		{
			return;
		}
		m_availableRenderQueues.Sort();
		foreach (int availableRenderQueue in m_availableRenderQueues)
		{
			if (availableRenderQueue == num || availableRenderQueue < minimalRenderQueueValue)
			{
				num++;
			}
			else if (availableRenderQueue > num)
			{
				break;
			}
		}
		m_availableRenderQueues.Add(num);
		material.SetRenderQueue(num);
	}

	private void FreeRenderQueueValue(int renderQueueValue)
	{
		if (renderQueueValue == -1)
		{
			return;
		}
		for (int num = m_availableRenderQueues.Count - 1; num >= 0; num--)
		{
			if (m_availableRenderQueues[num] == renderQueueValue)
			{
				m_availableRenderQueues.RemoveAt(num);
				break;
			}
		}
	}
}
