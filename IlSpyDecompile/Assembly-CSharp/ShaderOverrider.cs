using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class ShaderOverrider : MonoBehaviour
{
	private class MaterialOverride
	{
		private Material baseMaterial;

		private Material instancedMaterial;
	}

	[Serializable]
	private class ShaderTweak
	{
		public string parameter;

		public float value;
	}

	[SerializeField]
	private bool m_override;

	[SerializeField]
	private GameObject m_target;

	[SerializeField]
	protected Shader m_shaderOverride;

	[SerializeField]
	private List<ShaderTweak> m_tweaks = new List<ShaderTweak>();

	private Dictionary<Renderer, Material> m_rendererMapping = new Dictionary<Renderer, Material>();

	private Dictionary<Material, Material> m_materialOverrides = new Dictionary<Material, Material>();

	[Overridable]
	public bool Override
	{
		get
		{
			return m_override;
		}
		set
		{
			m_override = value;
			Apply(m_override);
		}
	}

	private void OnValidate()
	{
		if (m_target == null)
		{
			m_target = base.gameObject;
		}
		Apply(m_override);
	}

	private void OnDestroy()
	{
		DestroyInstancedMaterials();
	}

	private void Apply(bool applied)
	{
		if (applied)
		{
			InstantiateMaterials();
			ApplyShaderOverrides();
		}
		else
		{
			RestoreOriginalMaterials();
		}
	}

	private void InstantiateMaterials()
	{
		if (m_target == null)
		{
			return;
		}
		Renderer[] componentsInChildren = m_target.GetComponentsInChildren<Renderer>(includeInactive: true);
		foreach (Renderer renderer in componentsInChildren)
		{
			if (m_rendererMapping.TryGetValue(renderer, out var value))
			{
				continue;
			}
			value = renderer.GetSharedMaterial();
			if (!(value == null))
			{
				m_rendererMapping[renderer] = value;
				if (!m_materialOverrides.TryGetValue(value, out var value2))
				{
					value2 = UnityEngine.Object.Instantiate(value);
					m_materialOverrides[value] = value2;
				}
				renderer.SetSharedMaterial(value2);
			}
		}
	}

	private void ApplyShaderOverrides()
	{
		foreach (KeyValuePair<Material, Material> materialOverride in m_materialOverrides)
		{
			Material value = materialOverride.Value;
			if (value.shader != m_shaderOverride && m_shaderOverride != null)
			{
				value.shader = m_shaderOverride;
			}
			foreach (ShaderTweak tweak in m_tweaks)
			{
				if (!value.HasProperty(tweak.parameter))
				{
					Debug.LogWarningFormat("Property '{0}' does not exist on shader '{1}'", tweak.parameter, value.shader.name);
				}
				else
				{
					value.SetFloat(tweak.parameter, tweak.value);
				}
			}
		}
	}

	private void RestoreOriginalMaterials()
	{
		foreach (KeyValuePair<Renderer, Material> item in m_rendererMapping)
		{
			Renderer key = item.Key;
			Material value = item.Value;
			key.SetSharedMaterial(value);
		}
		m_rendererMapping.Clear();
	}

	private void DestroyInstancedMaterials()
	{
		foreach (KeyValuePair<Material, Material> materialOverride in m_materialOverrides)
		{
			UnityEngine.Object.Destroy(materialOverride.Value);
		}
		m_materialOverrides.Clear();
	}
}
