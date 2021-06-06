using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	public class RendererDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
		private const string MaterialTexturePath = ".texture";

		private const string MaterialTextureTilingPath = ".textureTiling";

		private const string MaterialTextureOffsetPath = ".textureOffset";

		private const string MaterialColorPath = ".color";

		private Renderer m_renderer;

		private List<DynamicPropertyInfo> m_properties = new List<DynamicPropertyInfo>();

		public ICollection<DynamicPropertyInfo> DynamicProperties => m_properties;

		public void SetTarget(object target)
		{
			m_renderer = (Renderer)target;
			m_properties.Clear();
			m_properties.Add(new DynamicPropertyInfo
			{
				Id = "enabled",
				Name = "Enabled",
				Type = typeof(bool),
				Value = m_renderer.enabled
			});
			List<Material> sharedMaterials = m_renderer.GetSharedMaterials();
			for (int i = 0; i < sharedMaterials.Count; i++)
			{
				string text = "Materials[" + i + "]";
				Material material = sharedMaterials[i];
				m_properties.Add(new DynamicPropertyInfo
				{
					Id = text,
					Name = text,
					Type = typeof(Material),
					Value = material
				});
				if (material != null && material.HasProperty("_MainTex"))
				{
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".texture",
						Name = text + ".texture",
						Type = typeof(Texture2D),
						Value = material.mainTexture
					});
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureTiling",
						Name = text + ".textureTiling",
						Type = typeof(Vector2),
						Value = material.mainTextureScale
					});
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureOffset",
						Name = text + ".textureOffset",
						Type = typeof(Vector2),
						Value = material.mainTextureOffset
					});
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureOffset_x",
						Name = text + ".textureOffset_X",
						Type = typeof(float),
						Value = material.mainTextureOffset.x
					});
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureOffset_y",
						Name = text + ".textureOffset_Y",
						Type = typeof(float),
						Value = material.mainTextureOffset.y
					});
				}
				if (material != null && material.HasProperty("_Color"))
				{
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".color",
						Name = text + ".color",
						Type = typeof(Color),
						Value = material.color
					});
				}
			}
		}

		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			if (!(id == "enabled"))
			{
				if (id == "sharedMaterial")
				{
					value = m_renderer.GetSharedMaterial();
					return true;
				}
				int index = -1;
				if (id.Contains("Materials") && GetIndex(id, out index))
				{
					Material material = (Material)(value = m_renderer.GetSharedMaterial(index));
					if (material != null)
					{
						string text = null;
						if (id.EndsWith(".color"))
						{
							value = material.color;
						}
						else if (id.EndsWith(".texture"))
						{
							value = material.mainTexture;
						}
						else if (id.EndsWith(".textureOffset"))
						{
							value = material.mainTextureOffset;
						}
						else if (id.EndsWith(".textureOffset_x"))
						{
							value = material.mainTextureOffset.x;
						}
						else if (id.EndsWith(".textureOffset_y"))
						{
							value = material.mainTextureOffset.y;
						}
						else if (id.EndsWith(".textureTiling"))
						{
							value = material.mainTextureScale;
						}
						else if (!string.IsNullOrEmpty(text = GetMaterialPropertyId(id)) && material.HasProperty(text))
						{
							value = material.GetFloat(text);
						}
					}
					return true;
				}
				return false;
			}
			value = m_renderer.enabled;
			return true;
		}

		public bool SetDynamicPropertyValue(string id, object value)
		{
			if (!(id == "enabled"))
			{
				if (id == "sharedMaterial")
				{
					m_renderer.SetSharedMaterial((Material)value);
					return true;
				}
				int index = -1;
				List<Material> sharedMaterials = m_renderer.GetSharedMaterials();
				if (id.Contains("Materials") && GetIndex(id, out index) && index < sharedMaterials.Count)
				{
					if (id.EndsWith("]"))
					{
						List<Material> list = sharedMaterials;
						Material material2 = (list[index] = (Material)value);
						m_renderer.SetSharedMaterials(list);
					}
					else if (value != null)
					{
						Material materialInstance = GetMaterialInstance(m_renderer, index);
						if (materialInstance != null)
						{
							string materialPropertyId;
							if (id.EndsWith(".color"))
							{
								materialInstance.color = (Color)value;
							}
							else if (id.EndsWith(".texture"))
							{
								materialInstance.mainTexture = value as Texture;
							}
							else if (id.EndsWith(".textureOffset"))
							{
								materialInstance.mainTextureOffset = (Vector4)value;
							}
							else if (id.EndsWith(".textureOffset_x"))
							{
								materialInstance.mainTextureOffset = new Vector2((float)value, materialInstance.mainTextureOffset.y);
							}
							else if (id.EndsWith(".textureOffset_y"))
							{
								materialInstance.mainTextureOffset = new Vector2(materialInstance.mainTextureOffset.x, (float)value);
							}
							else if (id.EndsWith(".textureTiling"))
							{
								materialInstance.mainTextureScale = (Vector4)value;
							}
							else if (!string.IsNullOrEmpty(materialPropertyId = GetMaterialPropertyId(id)) && materialInstance.HasProperty(materialPropertyId))
							{
								materialInstance.SetFloat(materialPropertyId, (value is float) ? ((float)value) : ((value is double) ? ((float)(double)value) : 0f));
							}
						}
					}
					return true;
				}
				return false;
			}
			m_renderer.enabled = (bool)value;
			return true;
		}

		private Material GetMaterialInstance(Renderer renderer, int index)
		{
			Material sharedMaterial = renderer.GetSharedMaterial(index);
			Material material = sharedMaterial;
			if (sharedMaterial.name[0] != '?')
			{
				material = new Material(sharedMaterial);
				material.name = "?" + material.name;
				renderer.SetSharedMaterial(index, material);
			}
			return material;
		}

		private string GetMaterialPropertyId(string idPath)
		{
			string[] array = idPath.Split('.');
			if (array.Length != 2)
			{
				return "";
			}
			return array[1];
		}

		private bool GetIndex(string id, out int index)
		{
			index = 0;
			int num = id.IndexOf('[') + 1;
			if (num == 0)
			{
				return false;
			}
			int num2 = id.IndexOf(']', num) - 1;
			if (num2 < num)
			{
				return false;
			}
			int num3 = num2 - num;
			for (int i = num; i <= num2; i++)
			{
				int num4 = id[i] - 48;
				index += num4 * (int)Mathf.Pow(10f, num3);
				num3--;
			}
			return true;
		}
	}
}
