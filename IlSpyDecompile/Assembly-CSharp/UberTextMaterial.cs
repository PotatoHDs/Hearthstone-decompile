using System.Collections.Generic;
using UnityEngine;

public class UberTextMaterial
{
	public const int INVALID_RENDER_QUEUE = -1;

	protected Shader m_shader;

	protected Material m_material;

	protected int m_referenceCounter;

	private static List<Material> m_temporalRendererMaterialList = new List<Material>();

	public UberTextMaterialManager.MaterialType Type { get; private set; }

	public UberTextMaterialQuery Query { get; private set; }

	public UberTextMaterial(UberTextMaterialManager.MaterialType type, string shaderName, UberTextMaterialQuery query)
	{
		Type = type;
		Query = query.Clone();
		m_shader = ShaderUtils.FindShader(shaderName);
		if (!m_shader)
		{
			Debug.LogError("UberText Failed to load Shader: " + shaderName);
		}
		m_material = new Material(m_shader);
		Query.ApplyToMaterial(m_material);
	}

	public void Destroy()
	{
		Object.Destroy(m_material);
	}

	public bool HasQuery(UberTextMaterialQuery query)
	{
		if (IsValid())
		{
			return Query.Equals(query);
		}
		return false;
	}

	public Material Acquire()
	{
		m_referenceCounter++;
		return m_material;
	}

	public void Release()
	{
		m_referenceCounter--;
	}

	public bool CanDestroy()
	{
		return m_referenceCounter <= 0;
	}

	public int GetRenderQueue()
	{
		if ((bool)m_material)
		{
			return m_material.renderQueue;
		}
		return -1;
	}

	public void SetRenderQueue(int renderQueue)
	{
		if ((bool)m_material)
		{
			m_material.renderQueue = renderQueue;
		}
	}

	public bool IsValid()
	{
		return m_material;
	}

	public bool IsStillBound(Renderer renderer)
	{
		m_temporalRendererMaterialList.Clear();
		renderer.GetSharedMaterials(m_temporalRendererMaterialList);
		foreach (Material temporalRendererMaterial in m_temporalRendererMaterialList)
		{
			if (temporalRendererMaterial == m_material)
			{
				return true;
			}
		}
		return false;
	}

	public void Rebound(Renderer renderer, int index)
	{
		m_temporalRendererMaterialList.Clear();
		renderer.GetSharedMaterials(m_temporalRendererMaterialList);
		if (m_temporalRendererMaterialList.Count > index)
		{
			m_temporalRendererMaterialList[index] = m_material;
		}
		else
		{
			m_temporalRendererMaterialList.Add(m_material);
		}
		renderer.SetSharedMaterials(m_temporalRendererMaterialList.ToArray());
	}

	public void Unbound(Renderer renderer)
	{
		m_temporalRendererMaterialList.Clear();
		renderer.GetSharedMaterials(m_temporalRendererMaterialList);
		int num = -1;
		for (int num2 = m_temporalRendererMaterialList.Count - 1; num2 >= 0; num2--)
		{
			if (m_temporalRendererMaterialList[num2] == m_material)
			{
				num = num2;
				break;
			}
		}
		if (num != -1)
		{
			m_temporalRendererMaterialList.RemoveAt(num);
			renderer.SetSharedMaterials(m_temporalRendererMaterialList.ToArray());
		}
	}
}
