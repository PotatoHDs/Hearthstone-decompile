using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000A8D RID: 2701
public class ShaderOverrider : MonoBehaviour
{
	// Token: 0x06009083 RID: 36995 RVA: 0x002EE1ED File Offset: 0x002EC3ED
	private void OnValidate()
	{
		if (this.m_target == null)
		{
			this.m_target = base.gameObject;
		}
		this.Apply(this.m_override);
	}

	// Token: 0x06009084 RID: 36996 RVA: 0x002EE215 File Offset: 0x002EC415
	private void OnDestroy()
	{
		this.DestroyInstancedMaterials();
	}

	// Token: 0x1700082F RID: 2095
	// (get) Token: 0x06009085 RID: 36997 RVA: 0x002EE21D File Offset: 0x002EC41D
	// (set) Token: 0x06009086 RID: 36998 RVA: 0x002EE225 File Offset: 0x002EC425
	[Overridable]
	public bool Override
	{
		get
		{
			return this.m_override;
		}
		set
		{
			this.m_override = value;
			this.Apply(this.m_override);
		}
	}

	// Token: 0x06009087 RID: 36999 RVA: 0x002EE23A File Offset: 0x002EC43A
	private void Apply(bool applied)
	{
		if (applied)
		{
			this.InstantiateMaterials();
			this.ApplyShaderOverrides();
			return;
		}
		this.RestoreOriginalMaterials();
	}

	// Token: 0x06009088 RID: 37000 RVA: 0x002EE254 File Offset: 0x002EC454
	private void InstantiateMaterials()
	{
		if (this.m_target == null)
		{
			return;
		}
		foreach (Renderer renderer in this.m_target.GetComponentsInChildren<Renderer>(true))
		{
			Material sharedMaterial;
			if (!this.m_rendererMapping.TryGetValue(renderer, out sharedMaterial))
			{
				sharedMaterial = renderer.GetSharedMaterial();
				if (!(sharedMaterial == null))
				{
					this.m_rendererMapping[renderer] = sharedMaterial;
					Material material;
					if (!this.m_materialOverrides.TryGetValue(sharedMaterial, out material))
					{
						material = UnityEngine.Object.Instantiate<Material>(sharedMaterial);
						this.m_materialOverrides[sharedMaterial] = material;
					}
					renderer.SetSharedMaterial(material);
				}
			}
		}
	}

	// Token: 0x06009089 RID: 37001 RVA: 0x002EE2EC File Offset: 0x002EC4EC
	private void ApplyShaderOverrides()
	{
		foreach (KeyValuePair<Material, Material> keyValuePair in this.m_materialOverrides)
		{
			Material value = keyValuePair.Value;
			if (value.shader != this.m_shaderOverride && this.m_shaderOverride != null)
			{
				value.shader = this.m_shaderOverride;
			}
			foreach (ShaderOverrider.ShaderTweak shaderTweak in this.m_tweaks)
			{
				if (!value.HasProperty(shaderTweak.parameter))
				{
					Debug.LogWarningFormat("Property '{0}' does not exist on shader '{1}'", new object[]
					{
						shaderTweak.parameter,
						value.shader.name
					});
				}
				else
				{
					value.SetFloat(shaderTweak.parameter, shaderTweak.value);
				}
			}
		}
	}

	// Token: 0x0600908A RID: 37002 RVA: 0x002EE3FC File Offset: 0x002EC5FC
	private void RestoreOriginalMaterials()
	{
		foreach (KeyValuePair<Renderer, Material> keyValuePair in this.m_rendererMapping)
		{
			Renderer key = keyValuePair.Key;
			Material value = keyValuePair.Value;
			key.SetSharedMaterial(value);
		}
		this.m_rendererMapping.Clear();
	}

	// Token: 0x0600908B RID: 37003 RVA: 0x002EE468 File Offset: 0x002EC668
	private void DestroyInstancedMaterials()
	{
		foreach (KeyValuePair<Material, Material> keyValuePair in this.m_materialOverrides)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value);
		}
		this.m_materialOverrides.Clear();
	}

	// Token: 0x04007951 RID: 31057
	[SerializeField]
	private bool m_override;

	// Token: 0x04007952 RID: 31058
	[SerializeField]
	private GameObject m_target;

	// Token: 0x04007953 RID: 31059
	[SerializeField]
	protected Shader m_shaderOverride;

	// Token: 0x04007954 RID: 31060
	[SerializeField]
	private List<ShaderOverrider.ShaderTweak> m_tweaks = new List<ShaderOverrider.ShaderTweak>();

	// Token: 0x04007955 RID: 31061
	private Dictionary<Renderer, Material> m_rendererMapping = new Dictionary<Renderer, Material>();

	// Token: 0x04007956 RID: 31062
	private Dictionary<Material, Material> m_materialOverrides = new Dictionary<Material, Material>();

	// Token: 0x020026D9 RID: 9945
	private class MaterialOverride
	{
		// Token: 0x0400F24C RID: 62028
		private Material baseMaterial;

		// Token: 0x0400F24D RID: 62029
		private Material instancedMaterial;
	}

	// Token: 0x020026DA RID: 9946
	[Serializable]
	private class ShaderTweak
	{
		// Token: 0x0400F24E RID: 62030
		public string parameter;

		// Token: 0x0400F24F RID: 62031
		public float value;
	}
}
